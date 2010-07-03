using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace Resources
{
    public class ResourceManager
    {
        static List<DirectoryInfo> Paths = new List<DirectoryInfo>();

        public static string FindFile ( string path )
        {
            SetupDirs();

            foreach (DirectoryInfo dir in Paths)
            {
                if (File.Exists(Path.Combine(dir.FullName, path)))
                    return new FileInfo(Path.Combine(dir.FullName, path)).FullName;
            }

            return string.Empty;
        }

        protected static void SetupDirs()
        {
            if (Paths.Count > 0)
                return;
           
            DirectoryInfo rootDir = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().FullName), "data"));
            if (rootDir.Exists)
                Paths.Add(rootDir);
        }
    }
}
