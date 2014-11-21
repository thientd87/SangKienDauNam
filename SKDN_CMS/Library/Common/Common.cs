//Sharpcms.net is licensed under the open source license GPL - GNU General Public License.
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Xml;

namespace Portal.SiteSystem.Library
{
	public static class Common
	{
        public static string[] RemoveOne(string[] args)
        {
            if (args != null && args.Length > 1)
            {
                string[] argsNew = new string[args.Length - 1];
                for (int i = 1; i < args.Length; i++)
                {
                    argsNew[i - 1] = args[i];
                }
                return argsNew;
            }
            return null;
        }
        public static string[] RemoveOneLast(string[] args)
        {
            if (args !=null && args.Length > 1)
            {
                string[] argsNew = new string[args.Length - 1];
                for (int i = 0; i < args.Length-1; i++)
                {
                    argsNew[i] = args[i];
                }
                return argsNew;
            }
            return null;
        }

        public static bool StringArrayContains(string[] args, string value)
        {
            foreach (string currentValue in args)
            {
                if (currentValue == value)
                {
                    return true;
                }
            }
            return false;
        }

		public static string CombinePaths(params string[] paths)
		{//TODO: this is not safe yeat - a stack overflow happened...
			string combinedPath = string.Empty;
			for (int i = 1; i < paths.Length; i++)
			{
				if (i == 1)
				{
					combinedPath = Path.Combine(paths[i - 1], paths[i]);
				}
				else
				{
					combinedPath = Path.Combine(combinedPath, paths[i]);
				}
			}

			return combinedPath;
		}
       

		public static string CleanToSafeString(string dirtyString)
        {
            // TODO: quick hack to handle Danish characters (should be more generic)
            dirtyString = dirtyString.Replace("æ", "ae").Replace("ø", "oe").Replace("å", "aa");
            dirtyString = dirtyString.Replace("Æ", "Ae").Replace("Ø", "Oe").Replace("Å", "Aa");
  
            // Trim .'s
            dirtyString = dirtyString.Trim('.');

			string semiCleanChars = Settings.DefaultInstance["common/cleanChars/notInBeginning"];
			string cleanChars = Settings.DefaultInstance["common/cleanChars/anywhere"];
			char[] loweredDirtyChars = dirtyString.ToLower().ToCharArray();
			char[] originalChars = dirtyString.ToCharArray();

			bool allowed;
			char currentChar;
			for (int index = 0; index < dirtyString.Length; index++)
			{
				allowed = false;
				currentChar = loweredDirtyChars[index];
				
				if (index > 0)
				{
					if (semiCleanChars.IndexOf(currentChar) > -1)
					{
						allowed = true;
					}
				}

				if (cleanChars.IndexOf(currentChar) > -1)
				{
					allowed = true;
				}

				if (!allowed)
				{
					originalChars[index] = '_';
				}
			}

			string cleanString = new string(originalChars);

			// Remove double underscores
			bool doubleUnderscoresRemoved = true;
			string beforeRemoval;
			while (doubleUnderscoresRemoved)
			{
				beforeRemoval = cleanString;
				cleanString = cleanString.Replace("__", "_");
				if (cleanString == beforeRemoval)
				{
					doubleUnderscoresRemoved = false;
				}
			}

			return cleanString;
        }

		public static bool DeleteDirectory(string path)
		{
			string absolutePath = CombinePaths(Settings.DefaultInstance.RootPath, path);
			if (!Directory.Exists(absolutePath))
			{
				return false;
			}

			if (!PathIsInSite(path))
			{
				return false;
			}

			Directory.Delete(absolutePath, true);
			return true;
		}

		public static bool DeleteFile(string path)
		{
			string absolutePath = CombinePaths(Settings.DefaultInstance.RootPath, path);
			if (!File.Exists(absolutePath))
			{
				return false;
			}

			if (!PathIsInSite(path))
			{
				return false;
			}

			File.Delete(absolutePath);
			return true;
		}

		public static bool MoveFile(string path, string newContainingDirectory)
		{
			string sourceAbsolutePath = CheckedCombinePaths(path);
			string newContainingDirectoryAbsolutePath = CheckedCombinePaths(newContainingDirectory);

			if (!File.Exists(sourceAbsolutePath))
			{
				return false;
			}

			if (!Directory.Exists(newContainingDirectoryAbsolutePath))
			{
				return false;
			}

			string filename = new FileInfo(sourceAbsolutePath).Name;
			string newFilename = CombinePaths(newContainingDirectoryAbsolutePath, filename);

			File.Move(sourceAbsolutePath, newFilename);
			return true;
		}

		public static bool MoveDirectory(string path, string newContainingDirectory)
		{
			string sourceAbsolutePath = CheckedCombinePaths(path);
			string newContainingDirectoryAbsolutePath = CheckedCombinePaths(newContainingDirectory);

			if (!Directory.Exists(sourceAbsolutePath))
			{
				return false;
			}

			if (!Directory.Exists(newContainingDirectoryAbsolutePath))
			{
				return false;
			}

			string directoryName = new DirectoryInfo(sourceAbsolutePath).Name;
			string newDirectoryName = CombinePaths(newContainingDirectoryAbsolutePath, directoryName);
            try
            {
                Directory.Move(path, newDirectoryName);
            }
            catch
            { }// todo:this catch is not pretty handles the case when the folder


			return true;
		}

		/// <summary>
		/// Returns a path combined with the site root.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static string CheckedCombinePaths(string path)
		{
			return CheckedCombinePaths(Settings.DefaultInstance.RootPath, path);
		}

		public static string CheckedCombinePaths(string root, params string[] paths)
		{
			string[] allPaths = new string[paths.Length + 1];
			allPaths[0] = root;
			paths.CopyTo(allPaths, 1);

			string combinedPath = CombinePaths(allPaths);
            if (PathIsUnderRoot(root, combinedPath))
			{
				return combinedPath;
			}
			throw new ArgumentException("The combined path does not begin with the root path.");
		}

		public static bool PathIsUnderRoot(string root, string path)
		{
			if (path.StartsWith(root))
			{
				return true;
			}
			return false;
		}

		public static bool PathIsInSite(string path)
		{
			string absolutePath = CombinePaths(Settings.DefaultInstance.RootPath, path);
			return PathIsUnderRoot(Settings.DefaultInstance.RootPath, absolutePath);
		}

		public static string GetMainMimeType(string extension)
		{
			string mimeType = GetMimeType(extension);
            if (mimeType != null && mimeType != string.Empty)
            {
                return mimeType.Substring(0, mimeType.IndexOf('/'));
            }
            else
            {
                return string.Empty;
            }
		}

		public static string GetMimeType(string extension)
		{
			if (extension.StartsWith("."))
			{
				extension = extension.Substring(1);
			}

			string mimeType = Settings.DefaultInstance["common/mimetypes/" + extension];
			if (mimeType != string.Empty)
			{
				return mimeType;
			}

			return Settings.DefaultInstance["mimetypes/defaulttype"];
		}
	}
}