using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace PewClient
{
    public class Prefs
    {
        public string UserName = string.Empty;
        public string Password = string.Empty;
        public bool Savepassword = false;

        public static bool Exists()
        {
            return GetConfig().Exists;
        }

        public static Prefs Load()
        {
            FileInfo file = GetConfig();
            if (!file.Exists)
                return new Prefs();

            XmlSerializer xml = new XmlSerializer(typeof(Prefs));
            StreamReader reader = file.OpenText();
            Prefs p = xml.Deserialize(reader) as Prefs;
            reader.Close();
            return p;
        }

        public static bool Save( Prefs p )
        {
            FileInfo file = GetConfig();
            if (file.Exists)
                file.Delete();

            XmlSerializer xml = new XmlSerializer(typeof(Prefs));
            FileStream fs= file.OpenWrite();
            if (fs == null)
                return false;

            xml.Serialize(fs, p);
            fs.Close();
            return true;
        }

        protected static DirectoryInfo GetDir()
        {
            return Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Pew"));
        }

        protected static FileInfo GetConfig()
        {
            return new FileInfo(Path.Combine(GetDir().FullName, "pew.xml"));
        }
    }
}
