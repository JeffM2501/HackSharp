using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Drawing;

//using ComponentAce.Compression.Libs.zlib;

namespace Bitmaper
{
    public class ManaWorldMapReader
    {
        public class TilesetInfo
        {
            public int FirstGUID = 0;
        }

        public static IsoMap FromXML(string filename)
        {
            FileInfo file = new FileInfo(filename);
            if (!file.Exists)
                return null;

            XmlDocument doc = new XmlDocument();
            doc.Load(file.FullName);

            XmlElement root = doc.DocumentElement;

            IsoMap map = new IsoMap();

            if (root.Name == "map")
                ProcessMapElement(root, map);
            else
            {
                foreach (XmlElement child in root.ChildNodes)
                {
                    if (child.Name == "map")
                        ProcessMapElement(child, map);
                }
            }

            return map;
        }

        protected static void ProcessMapElement(XmlElement element, IsoMap map)
        {
            foreach (XmlAttribute attrib in element.Attributes)
            {
                if (attrib.Name == "orientation")
                    map.Options.Orthogonal = attrib.Value == "orthogonal";
                else if (attrib.Name == "width")
                    map.Options.Bounds = new Size(int.Parse(attrib.Value),map.Options.Bounds.Height);
                else if (attrib.Name == "height")
                    map.Options.Bounds = new Size( map.Options.Bounds.Width,int.Parse(attrib.Value));
                else if (attrib.Name == "tilewidth")
                    map.Options.Grid = new Size(int.Parse(attrib.Value), map.Options.Grid.Height);
                else if (attrib.Name == "tileheight")
                    map.Options.Grid = new Size(map.Options.Grid.Width, int.Parse(attrib.Value));
            }
            
            foreach (XmlElement child in element.GetElementsByTagName("properties"))
            {
                foreach (XmlElement property in child.ChildNodes)
                {
                    if (property.Name == "property")
                    {
                        string name = string.Empty;
                        string value = string.Empty;
                        foreach (XmlAttribute attrib in property.Attributes)
                        {
                            if (attrib.Name == "name")
                                name = attrib.Value;
                            else if (attrib.Name == "value")
                                value = attrib.Value;
                        }

                        if (name == "music")
                            map.Options.Music = value;
                        else if (name == "name")
                            map.Options.Name = value;
                    }
                }
            }

            foreach (XmlElement child in element.GetElementsByTagName("tileset"))
                ProcessTileset(child, map);

            foreach (XmlElement child in element.GetElementsByTagName("layer"))
                ProcessLayer(child, map);

            foreach (XmlElement child in element.GetElementsByTagName("objectgroup"))
            {
                foreach (XmlElement o in child.GetElementsByTagName("object"))
                    ProcessObject(o, map);
            }
        }

        protected static void ProcessObject(XmlElement element, IsoMap map)
        {
            MapObject obj = new MapObject();
            obj.Name = element.GetAttribute("name");
            obj.ObjectType = element.GetAttribute("type");
            if (element.HasAttribute("x") && element.HasAttribute("y"))
                obj.Origin = new Point(int.Parse(element.GetAttribute("x")), int.Parse(element.GetAttribute("y")));
            if (element.HasAttribute("width") && element.HasAttribute("height"))
                obj.Bounds = new Size(int.Parse(element.GetAttribute("width")), int.Parse(element.GetAttribute("height")));
            
            foreach (XmlElement p in element.GetElementsByTagName("properties"))
            {
                foreach (XmlElement prop in p.GetElementsByTagName("property"))
                    obj.Properties.Add(new KeyValuePair<string, string>(prop.GetAttribute("name"), prop.GetAttribute("value")));
            }

            map.Objects.Add(obj);
        }

        protected static void ProcessTileset(XmlElement element, IsoMap map)
        {
            MapImage img = new MapImage();

            TilesetInfo info = new TilesetInfo();
            img.Tag = info;

            foreach (XmlAttribute attrib in element.Attributes)
            {
                if (attrib.Name == "name")
                    img.Name = attrib.Value;
                else if (attrib.Name == "firstgid")
                    info.FirstGUID = int.Parse(attrib.Value);
                else if (attrib.Name == "tilewidth")
                    img.Grid = new Size(int.Parse(attrib.Value), img.Grid.Height);
                else if (attrib.Name == "tileheight")
                    img.Grid = new Size(img.Grid.Width,int.Parse(attrib.Value));
            }

            foreach (XmlElement child in element.ChildNodes)
            {
                if (child.Name == "image")
                {
                    foreach (XmlAttribute attrib in child.Attributes)
                    {
                        if (attrib.Name == "source")
                            img.FileName = attrib.Value;
                    }
                }
            }
            img.ID = map.Tilesets.Count;
            map.Tilesets.Add(img.ID, img);
        }

        protected static MapImage ImageFromID(int ID, out int imageIndex, Dictionary<int, MapImage> imageOffsets)
        {
            MapImage lastImage =  null;
            int lastIndex = 0;

            foreach (KeyValuePair<int, MapImage> item in imageOffsets)
            {
                if (ID < item.Key)
                {
                    imageIndex = ID - lastIndex;
                    return lastImage;
                }

                lastIndex = item.Key;
                lastImage = item.Value;
            }

            imageIndex = ID - lastIndex;
            return lastImage;
        }

        protected static void ProcessLayer(XmlElement element, IsoMap map)
        {
            Dictionary<int, MapImage> imageOffsets = new Dictionary<int, MapImage>();

            foreach (KeyValuePair<int,MapImage> img in map.Tilesets)
            {
                TilesetInfo info = img.Value.Tag as TilesetInfo;
                if (info != null)
                {
                    imageOffsets.Add(info.FirstGUID-1, img.Value);
                }
            }
            if (imageOffsets.Count == 0)
            {
                int offset = 0;
                foreach (KeyValuePair<int, MapImage> img in map.Tilesets)
                {
                    imageOffsets.Add(offset, img.Value);
                    offset += img.Value.Width * img.Value.Height;
                }
            }

            MapLayer layer = new MapLayer();

            Size layerBounds = new Size(map.Options.Bounds.Width, map.Options.Bounds.Height);

            foreach (XmlAttribute attrib in element.Attributes)
            {
                if (attrib.Name == "name")
                    layer.Name = attrib.Value;
                else if (attrib.Name == "width")
                    layerBounds = new Size(int.Parse(attrib.Value), layerBounds.Height);
                else if (attrib.Name == "height")
                    layerBounds = new Size(layerBounds.Width, int.Parse(attrib.Value));
            }

            if (layerBounds != map.Options.Bounds)
                return;

            int[] intList = null;

            foreach (XmlElement child in element.GetElementsByTagName("data"))
            {
                XmlText data = child.LastChild as XmlText;
                if (data == null)
                    continue;
                string val = data.Data.TrimStart(new char[] { '\n', ' ' });

                if (child.GetAttribute("encoding") == "base64")
                {
                    byte[] b = Convert.FromBase64String(val);
                    
                    MemoryStream stream = new MemoryStream(b);
                    byte[] outbuffer = null;
                    byte[] t = new byte[1024];

                    Stream readStream = null;

                    if (!child.HasAttribute("compression"))
                        readStream = stream;
                    else
                    {
                        string comp = child.GetAttribute("compression");
                        if (comp == "gzip")
                            readStream = new GZipStream(stream, CompressionMode.Decompress);
                        else if (comp == "zlib")
                            continue;// readStream = new ZOutputStream(stream);
                    }

                    if (readStream == null)
                        continue; // no clue what it is

                    int read = readStream.Read(t, 0, 1024);
                    while (read != 0)
                    {
                        if (outbuffer == null)
                        {
                            outbuffer = new byte[read];
                            Array.Copy(t, outbuffer, read);
                        }
                        else
                        {
                            byte[] n = new byte[outbuffer.Length + read];
                            Array.Copy(outbuffer, n, outbuffer.Length);
                            Array.Copy(t, 0, n, outbuffer.Length, read);

                            outbuffer = n;
                        }
                        read = readStream.Read(t, 0, 1024);
                    }
                    readStream.Close();
                    stream.Close();

                    if (outbuffer == null)
                        continue;

                    int intCount = outbuffer.Length / 4;
                    intList = new int[intCount];

                    for (int i = 0; i < intCount; i++)
                    {
                        int j = BitConverter.ToInt32(outbuffer, i * 4);
                        intList[i] = j;
                    }
                }
                else if (child.GetAttribute("encoding") == "csv")
                {
                    string[] list = val.Split(",".ToCharArray());
                    intList = new int[list.Length];

                    for (int i = 0; i < list.Length; i++)
                        intList[i] = int.Parse(list[i]);
                }
            }

            if (intList == null || intList.Length != map.Options.Bounds.Width * map.Options.Bounds.Height)
            {
                return;
            }

            for (int i = 0; i < intList.Length; i++)
            {
                MapLayerCell cell = new MapLayerCell();
                int offset = 0;
                cell.Source = ImageFromID(intList[i]-1, out offset, imageOffsets);

                if (cell.Source != null)
                {
                    int y = offset / cell.Source.Bounds.Width;
                    cell.Cell = new Point(offset - (y * cell.Source.Bounds.Width), y);
                }
                layer.Cells.Add(cell);
            }

            if (layer.Cells.Count > 0)
            {
                if (map.ForeLayers.Count > 0)
                {
                    if (layer.Name == "Collision")
                    {
                        map.CollisionMap.Clear();
                        foreach (MapLayerCell cell in layer.Cells)
                            map.CollisionMap.Add(cell.Source != null);
                    }
                    else
                        map.ForeLayers.Add(layer);
                }
                else
                {
                    if (layer.Name.ToLower() == "over" || layer.Name.ToLower() == "fringe")
                        map.ForeLayers.Add(layer);
                    else
                        map.BackgroundLayers.Add(layer);
                }
            }
        }
    }
}
