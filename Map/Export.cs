using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Xml;
using System.Xml.Serialization;


namespace Maps
{
    public class Export
    {
        public static bool ToXML ( FileInfo file, Map map )
        {
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(Map));
                Stream fs = file.OpenWrite();
                xml.Serialize(fs, map);
                fs.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
    }
}
