using System;
using System.IO;
using System.Text;

namespace RenameTo
{
    public class Segments
    {
        private string modul;
        private string name;
        private string newName;

        public string Modul { get { return modul; } set { modul = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string NewName { get { return newName; } set { newName = value; } }

        public Segments(string modul, string name, string newName)
        {
            Modul = modul;
            Name = name;
            NewName = newName;
        }

        public static void Rename(Segments file)
        {
            try
            {
                string pattern = file.Name + ".*";      // init searching pattern
                string[] dirs = Directory.GetFiles(".", pattern, SearchOption.AllDirectories);      // array of files in each directory

                Console.WriteLine("Renamed files of {0}: {1}.", file.Modul, dirs.Length);
                foreach (string dir in dirs)        // rename each file in directory but not directory itself
                {
                    if (dir.Contains(file.Name))
                    {
                        var newDir = Segments.DirFix(file, dir);
                        File.Move(dir, newDir);
                        File.Delete(dir);
                    }
                }
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.ToString());
            }
        }

        // the emphasizing of file name from path
        public static string DirFix(Segments file, string dir)
        {
            string[] path = dir.Split('\\');
            path[path.Length - 1] = path[path.Length - 1].Replace(file.Name, file.NewName);
            return String.Join("\\", path);
        }

        // Parse config file
        public static Segments[] ConfigIni()
        {
            var path = @"./config.txt";

            string[] readText = File.ReadAllLines(path, Encoding.GetEncoding(1251));

            var segments = new Segments[readText.Length];

            int i = 0;
            foreach (var l in readText)
            {
                if (!l.StartsWith("//") && l != "")
                { 
                    var fields = l.Split(',');
                    fields[1] = fields[1].Trim();
                    fields[2] = fields[2].Trim();
                    segments[i] = new Segments( fields[0],
                                                fields[1],
                                                fields[2]);
                    i++;
                }
            }
            return segments;
        }
    }
}
