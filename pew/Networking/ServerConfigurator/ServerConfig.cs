/*
    Open Combat/Projekt 2501
    Copyright (C) 2010  Jeffery Allen Myers

    This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
    version 2.1 of the License, or (at your option) any later version.

    This library is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public
    License along with this library; if not, write to the Free Software
    Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ServerConfigurator
{
    public class ServerConfig
    {
        public class KeyPair
        {
            public string key = string.Empty;
            public string value = string.Empty;

            public KeyPair()
            {}

            public KeyPair (string k, string v)
            {
                key = k;
                value = v;
            }

            public KeyPair(KeyValuePair<string,string> pair)
            {
                key = pair.Key;
                value = pair.Value;
            }
        }

        public List<KeyPair> ConfigItems = new List<KeyPair>();

        [System.Xml.Serialization.XmlIgnoreAttribute]
        Dictionary<string, string> items = new Dictionary<string, string>();

        [System.Xml.Serialization.XmlIgnoreAttribute]
        FileInfo configFile = null;

        protected ServerConfig ()
        {
        }

        public ServerConfig(string file)
        {
            configFile = new FileInfo(file);
            if (configFile.Exists)
            {
                XmlSerializer XML = new XmlSerializer(typeof(ServerConfig));

                StreamReader reader = configFile.OpenText();
                ServerConfig cfg = (ServerConfig)XML.Deserialize(reader);
                ConfigItems = cfg.ConfigItems;

                reader.Close();

                foreach (KeyPair item in ConfigItems)
                    items.Add(item.key, item.value);

                ConfigItems.Clear();
            }

        }

        public void Save ()
        {
            ConfigItems.Clear();
            foreach (KeyValuePair<string, string> item in items)
                ConfigItems.Add(new KeyPair(item));

            if (configFile != null)
            {
                XmlSerializer XML = new XmlSerializer(typeof(ServerConfig));

                FileStream stream = configFile.OpenWrite();
                StreamWriter writer = new StreamWriter(stream);
                XML.Serialize(writer,this);
                writer.Close();
                stream.Close();
            }

            ConfigItems.Clear();
        }

        public string GetItem ( string name )
        {
            if (items.ContainsKey(name))
                return items[name];

            return string.Empty;
        }

        public int GetInt ( string name )
        {
            if (!ItemExists(name))
                return 0;

            int val = 0;
            try
            {
                int.TryParse(GetItem(name), out val);
            }
            catch (System.Exception /*ex*/)
            {
            	
            }
            return val;
        }

        public bool ItemExists ( string name )
        {
            return items.ContainsKey(name);
        }

        public void SetItem ( string name, string value )
        {
            if (items.ContainsKey(name))
                items[name] = value;
            else
                items.Add(name, value);
        }
    }
}
