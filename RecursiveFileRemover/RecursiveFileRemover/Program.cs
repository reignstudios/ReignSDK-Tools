using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RecursiveFileRemover
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args != null && args.Length == 1 && args[0] == "-help")
			{
				Console.WriteLine(Environment.NewLine + "RecursiveFileRemover command format: [RecursiveFileRemover.exe] [FileExt or FolderName] [-file or -folder] [Directory to start search]");
				return;
			}

			if (args == null || args.Length == 0)
			{
				Console.WriteLine("You must supply arguments. Use -help.");
				return;
			}
			else if (args.Length != 3)
			{
				Console.WriteLine("Invalid argument length, must be 3. Use -help.");
				return;
			}

			if (args[1] == "-file")
			{
				removeFiles(args[0], args[2]);
				Console.WriteLine("DONE");

			}
			else if (args[1] == "-folder")
			{
				removeFolders(args[0], args[2]);
				Console.WriteLine("Done");
			}
			else
			{
				Console.WriteLine("Argument two must be [-file] or [-folder].");
			}
		}

		private static void removeFiles(string fileExt, string directory)
		{
			// delete files in current directory
			string[] files = null;
			try
			{
				files = Directory.GetFiles(directory);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return;
			}

			foreach (var file in files)
			{
				var fileInfo = new FileInfo(file);
				if (fileInfo.Extension == fileExt)
				{
					try
					{
						fileInfo.Delete();
					}
					catch (Exception e)
					{
						Console.WriteLine("DELETE ERROR: " + e.Message);
					}
				}
			}

			// check sub-directorys
			string[] dirs = null;
			try
			{
				dirs = Directory.GetDirectories(directory);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return;
			}

			foreach (var dir in dirs)
			{
				removeFiles(fileExt, dir);
			}
		}

		private static void removeFolders(string folderName, string directory)
		{
			Console.WriteLine("TODO: This method is not implomented yet.");
		}
	}
}
