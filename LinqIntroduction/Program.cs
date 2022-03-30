using System;
using System.IO;
using System.Linq;

namespace linqintroduction
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Windows";
            ShowLargeFilesWithoutLinq(path);
            Console.WriteLine("***");
            ShowLargeFilesWithLinq(path);
        }

		private static void ShowLargeFilesWithLinq(string path)
		{
			/*
            var query = from file in new DirectoryInfo(path).GetFiles()
                        orderby file.Length descending
                        select file;
            */
            var query = new DirectoryInfo(path).GetFiles()
                        .OrderByDescending(f => f.Length)
                        .Take(5);
                    
            foreach (var file in query.Take(5))
            {
                Console.WriteLine($"{file.Name, -20} : {file.Length, 10:N0}");
            }
		}

		private static void ShowLargeFilesWithoutLinq(string path)
		{
			DirectoryInfo directory = new DirectoryInfo(path);

            FileInfo[] files = directory.GetFiles();
            Array.Sort(files, new FileInfoComparer());

            for(int i = 0; i < 5; i++)
            {
                FileInfo file = files[i];
                Console.WriteLine($"{file.Name, -20} : {file.Length, 10:N0}");

                Console.WriteLine();
            }
        }
	}
}
