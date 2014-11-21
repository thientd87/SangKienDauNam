using System;
using System.Web;

namespace DFISYS.API
{
	/// <summary>
	/// Common Helper Class.
	/// </summary>
	public class Helper
	{
		/// <summary>
		/// Copies a .install file in the given virtual directory.
		/// </summary>
		/// <param name="vFileName">Virtual path to the file.</param>
		/// <returns>true if the file was copied.</returns>
		public static bool InstallFile(string vFileName, string _settingfolder, string _installFolder)
		{
			try
			{
                string settingPath = HttpContext.Current.Server.MapPath(_settingfolder + vFileName);
                string _installPath = HttpContext.Current.Server.MapPath(_installFolder + vFileName+".install");
                if (!System.IO.File.Exists(settingPath))
				{
                    System.IO.File.Copy(_installPath, settingPath);
					return true;
				}
			}
			catch
			{
			}

			return false;
		}

		public static bool HasScriptTags(string html)
		{
			return html.ToLower().Replace(" ", "").IndexOf("<script") != -1;
		}
	}
}
