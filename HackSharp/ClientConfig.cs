using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace HackSharp
{
    public class ClientConfig
    {
        public int Version = 1;

        protected static FileInfo ConfigFile = null;

        public static string GetAppDataDir ()
        {
            string dirName = Path.GetFileName(Assembly.GetEntryAssembly().FullName);
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), dirName);
        }

        public static void CreateAppDir()
        {
            Directory.CreateDirectory(GetAppDataDir());
        }

        public static ClientConfig Config = new ClientConfig();

        public static void Load ()
        {
            ConfigFile = new FileInfo(Path.Combine(GetAppDataDir(), "config.xml"));
            if (ConfigFile.Exists)
            {
                XmlSerializer xml = new XmlSerializer(typeof(ClientConfig));
                StreamReader reader = ConfigFile.OpenText();
                Config = (ClientConfig)xml.Deserialize(reader);
                reader.Close();
            }
        }

        public static void Save()
        {
            XmlSerializer xml = new XmlSerializer(typeof(ClientConfig));
            if (ConfigFile.Exists)
                ConfigFile.Delete();
            else
                CreateAppDir();

            Stream fs = ConfigFile.OpenWrite();
            xml.Serialize(fs, Config);
            fs.Close();
        }
    }
}
