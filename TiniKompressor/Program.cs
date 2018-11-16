using System;
using System.Threading.Tasks;
using TinifyAPI;
using System.IO;
using System.Collections.Generic;
using System.Configuration;

namespace TiniKompressor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Paste path directory with images");
            var dir = Console.ReadLine();
            MainAsync(args, dir).GetAwaiter().GetResult();
        }
        static async Task MainAsync(string[] args, string path) {
            Program p = new Program();
            Tinify.Key = ConfigurationManager.AppSettings["TinyKey"];
            List<FileInfo> files = new List<FileInfo>();
            if (Directory.Exists(path))
            {
                files = p.TraverseTree(path);
                Console.WriteLine("\n------------------ {0} FILES FOUND!--------------------\n", files.Count);
                foreach (FileInfo f in files)
                {
                    long length = f.Length;
                    var source = Tinify.FromFile(f.FullName);
                    await source.ToFile(f.FullName);
                    Console.WriteLine("{0} compressed successfully!", f.Name, length, f.Length);
                }
            }
            Console.WriteLine("\n Complete! {0} files compressed. Press Enter to closed", files.Count);
        }

        private void GetAllDirectories(DirectoryInfo directoryInfo)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            files = directoryInfo.GetFiles("*.jpg*");
            if (files != null)
            {
                foreach (FileInfo f in files)
                {
                    Console.WriteLine(f.FullName);
                }
                subDirs = directoryInfo.GetDirectories();

                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    GetAllDirectories(dirInfo);
                }
            }
        }
        public List<FileInfo> TraverseTree(string root)
        {
            Stack<string> dirs = new Stack<string>();
            List<FileInfo> _files = new List<FileInfo>();

            if (!Directory.Exists(root))
            {
                throw new ArgumentException();
            }
            dirs.Push(root);

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs;
                try
                {
                    subDirs = Directory.GetDirectories(currentDir);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                string[] files = null;
                try
                {
                    files = Directory.GetFiles(currentDir);
                }

                catch (UnauthorizedAccessException e)
                {

                    Console.WriteLine(e.Message);
                    continue;
                }

                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                foreach (string file in files)
                {
                    FileInfo f = new FileInfo(file);
                    _files.Add(f);
                }
                foreach (string str in subDirs)
                    dirs.Push(str);
            }
            return _files;
        }
    }
}
