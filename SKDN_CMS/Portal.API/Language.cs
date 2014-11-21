using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Web;

namespace DFISYS.API
{
	/// <summary>
	/// Summary description for Language.
	/// </summary>
	[XmlRoot("language"), Serializable]
	public class Language
	{
		private static XmlSerializer xmlLanguage = new XmlSerializer(typeof(Language));		

		public Language()
		{
		}

		public static string GetText(string reference)
		{
			Language l = Load(System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
			string w = (string)l.wordsTbl[reference];
			return w==null?"":w;
		}

		public static string GetText(Module module, string reference)
		{
			if(module == null)
			{
				return GetText(reference);
			}
			else
			{
				Language l = Load(module, System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
				string w = (string)l.wordsTbl[reference];
				return w==null?"":w;
			}
		}

		private static Language Load(string language)
		{
			string CacheKey = "Language_" + language;
			// Lookup in Cache
			Language l = (Language)HttpContext.Current.Cache[CacheKey];
			if(l != null) return l;

			// Load Language
			XmlTextReader xmlReader = new XmlTextReader(Config.GetLanguagePhysicalPath(language));
			try
			{
				l = (Language)xmlLanguage.Deserialize(xmlReader);
				if(l == null) throw new Exception("Unable to load Language " + language);

				UpdateLanguageProperties(l);

				// Add to Cache
				HttpContext.Current.Cache.Insert(CacheKey, l, 
					new System.Web.Caching.CacheDependency(Config.GetLanguagePhysicalPath(language)));
			}
			finally
			{
				xmlReader.Close();
			}

			return l;
		}

		private static Language Load(Module module, string language)
		{
			string CacheKey = "Language_" + module.ModuleType + "_" + language;
			// Lookup in Cache
			Language l = (Language)HttpContext.Current.Cache[CacheKey];
			if(l != null) return l;

			// Load Portaldefinition
			XmlTextReader xmlReader = new XmlTextReader(Config.GetModuleLanguagePhysicalPath(module.ModuleType, language));
			try
			{
				l = (Language)xmlLanguage.Deserialize(xmlReader);
				if(l == null) throw new Exception("Unable to load Language " + language);

				UpdateLanguageProperties(l);

				// Add to Cache
				HttpContext.Current.Cache.Insert(CacheKey, l, 
					new System.Web.Caching.CacheDependency(Config.GetModuleLanguagePhysicalPath(module.ModuleType, language)));
			}
			finally
			{
				xmlReader.Close();
			}

			return l;
		}

		private static void UpdateLanguageProperties(Language l)
		{
			if(l.words == null)
				throw new Exception("No words found in current Language");

			foreach(Word w in l.words)
			{
				l.wordsTbl[w.reference] = w.val;
			}
		}

		[XmlElement("word")]
		public Word[] words = new Word[] {};

		[XmlIgnore]
		private Hashtable wordsTbl = new Hashtable();

		public class Word
		{
			[XmlAttribute("ref")]
			public string reference = "";

			[XmlAttribute("value")]
			public string val = "";
		}
	}
}
