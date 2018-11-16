using System;
using System.Threading.Tasks;
using TinifyAPI;
using System.IO;

namespace TiniKompressor
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            Console.WriteLine("Paste path directory with images");
            var dir = Console.ReadLine();
            p.GetDirectoriesInAFolder(dir);
            //int x = p.GetFilesFromDir(dir);
            //MainAsync(args).GetAwaiter().GetResult();
        }
        static async Task MainAsync(string[] args) {

            //Tinify.Key = "cn978rXplwsVpq9Pgtsrddw9JfRHF6z5";
            //var source = Tinify.FromFile("bugs.jpg");
            //await source.ToFile("bugs_min.jpg");

            //Console.WriteLine("Done...", source);
        }

        private void GetDirectoriesInAFolder(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo info = new DirectoryInfo(path);

                GetAllDirectories(info);
            }
        }

        private void GetAllDirectories(DirectoryInfo directoryInfo)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            files = directoryInfo.GetFiles("*.*");
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
    }
}
