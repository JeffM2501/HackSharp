using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Drawing;
using System.Drawing.Imaging;

using Maps;

namespace ImageToMap
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("usage ImageToMap IMAGE_TO_CONVERT");
                return;
            }

            FileInfo file = new FileInfo(args[0]);
            if (!file.Exists)
                return;

            FileInfo output = new FileInfo(Path.Combine(Path.GetDirectoryName(file.FullName),Path.GetFileNameWithoutExtension(file.FullName)) + ".xml");
            if (output.Exists)
                output.Delete();

            Export.ToXML(output, Import.FromImage(new Bitmap(file.FullName)));
        }
    }
}
