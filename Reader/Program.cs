using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace Reader
{
    internal class Program
    {
        static void dump(string localpath)
        {
            string name = localpath.Split('\\')[localpath.Split('\\').Length-1];
            StreamWriter w = new StreamWriter("temp_"+name);
            StreamReader r = new StreamReader(localpath);
            BinaryReader bin = new BinaryReader(r.BaseStream);
            byte[] re = bin.ReadBytes((int)bin.BaseStream.Length);
            w.Write(Encoding.ASCII.GetString(re));
            Console.WriteLine("Dumping "+name+"..");
            //string ascii = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ\n\t ,.:()\"/123456789;";
            string line;
            r.Close();
            w.Close();
            StreamWriter silly = new StreamWriter("output\\"+name+".txt");
            StreamReader hihi = new StreamReader("temp_"+name);
            while ((line = hihi.ReadLine()) != null)
            {
                if (line.Contains("global/") && line.IndexOf(");") > line.IndexOf("global/") && line.IndexOf('(') > line.IndexOf("global/"))
                {
                    silly.WriteLine(line.Substring(line.IndexOf("global/"), line.IndexOf(");") + 2 - line.IndexOf("global/")));
                }
            }
            silly.Close();
            hihi.Close();
            File.Delete("temp_"+name);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Path to the PackFiles: ");
            string p = Console.ReadLine();
            Directory.CreateDirectory("output");
            var files = from file in Directory.EnumerateFiles(p) select file;
            foreach (string file in files)
            {
                if (file.Split('.')[1] == "csa")
                {
                    dump(file);
                }
            }
            Console.WriteLine("Done!!! Saved the files in the same Directory");
            Console.ReadKey();
        }
    }
}
