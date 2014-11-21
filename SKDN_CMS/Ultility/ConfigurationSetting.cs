using System;
using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Xml;
using System.Xml.Serialization;

namespace DFISYS.Ultility
{
	/// <summary>
	/// Cho phép đọc tệp Config dạng <item key="" value=""></Item>
	/// </summary>
	[XmlRoot("config"), Serializable]
	public class ConfigurationSetting
	{
		private static XmlSerializer xmlConfig = new XmlSerializer(typeof(DFISYS.Ultility.ConfigurationSetting));

		public ConfigurationSetting()
		{
		}

		/// <summary>
		/// Lấy đường dẫn tệp Config
		/// Tệp Config thường có dạng Prefix.ConfigName.config
		/// </summary>
		/// <param name="configName">Tên Module cần Config</param>
		/// <param name="configPrefix">Tiếp đầu ngữ của tên tệp. VD: Config, Skin ...</param>
		/// <param name="configFolderName">
		/// Đường dẫn đến thư mục chứa tệp Config (tương đối)
		/// Mẫu: /root/ConfigFolder/
		/// </param>
		/// <returns>Đường dẫn vật lý của tệp Config trên ổ cứng của server</returns>
		public static string GetConfigPhysicalPath(string configName, string configPrefix, string configFolderName)
		{
			if(configName == "")
				return HttpContext.Current.Server.MapPath(configFolderName + configPrefix + ".config");
			else
				//return HttpContext.Current.Server.MapPath(configFolderName + configPrefix + "." + configName + ".config");
				return HttpContext.Current.Server.MapPath("~/") + "/" + configFolderName + configName;
		}

		/// <summary>
		/// Hàm lấy giá trị thiết lập của một thông tin được định nghĩa trong tệp web.config
		/// </summary>
		/// <param name="configKey">Tên thông tin cần lấy</param>
		/// <returns>Giá trị đã được thiết lập của thông tin</returns>
		public static string GetConfigFolder(string configKey)
		{
			return System.Configuration.ConfigurationSettings.AppSettings[configKey];
		}

		/// <summary>
		/// Hàm lấy danh sách tên các thông tin được cấu hình trong tệp cấu hình
		/// </summary>
		/// <param name="_strConfigName">Tên Skin</param>
		/// <param name="_strSettingKey">
		/// Tên khóa chứa thông tin đường dẫn thư mục có tệp cấu hình
		/// Khóa này nằm trong tệp web.config
		/// </param>
		/// <param name="_strConfigPrefix">Tiếp đầu ngữ của tệp cấu hình</param>
		/// <returns>Mảng chứa danh sách tên của các thông tin cấu hình</returns>
		public static Hashtable GetItemList(string _strConfigName, string _strSettingKey, string _strConfigPrefix)
		{
			Hashtable _arrAllItems = new Hashtable();
			ConfigurationSetting l = Load(_strConfigName, _strConfigPrefix, _strSettingKey);

			if (l.configItems != null)
			{
				foreach (ConfigItem _objItem in l.configItems)
				{
					if (_objItem != null && _objItem.reference != null && !_arrAllItems.ContainsKey(_objItem.reference) && _objItem.reference.Trim() != "")
					{
						_arrAllItems.Add(_objItem.reference, _objItem.alias);
					}
				}
			}

			return _arrAllItems;
		}

		/// <summary>
		/// Hàm lấy nội dung (dạng đơn giản) đã được thiết lập của một thông tin cấu hình
		/// Nội dung này nằm trong tệp có dạng Prefix.ModuleName.Config
		/// </summary>
		/// <param name="_strReference">Tên thông tin cấu hình</param>
		/// <param name="_strConfigName">Tên module cấu hình</param>
		/// <param name="_strSettingKey">
		/// Tên khóa chứa thông tin đường dẫn thư mục có tệp cấu hình
		/// Khóa này nằm trong tệp web.config
		/// </param>
		/// <param name="_strConfigPrefix">Tiếp đầu ngữ của tệp cấu hình</param>
		/// <returns>Nội dung đã thiết lập của thông tin cần lấy (dạng đơn giản)</returns>
		public static string GetItemValue(string _strReference, string _strConfigName, string _strSettingKey, string _strConfigPrefix)
		{
			ConfigurationSetting l = Load(_strConfigName, _strConfigPrefix, _strSettingKey);
			string _strItemValue = (string)l.configItemsTbl[_strReference];
			return _strItemValue == null ? "" : _strItemValue;
		}

		/// <summary>
		/// Hàm lấy nội dung (dạng phức tạp) đã được thiết lập của một thông tin cấu hình
		/// Nội dung này nằm trong tệp có dạng Prefix.ModuleName.Config
		/// </summary>
		/// <param name="_strReference">Tên thông tin cấu hình</param>
		/// <param name="_strConfigName">Tên module cấu hình</param>
		/// <param name="_strSettingKey">
		/// Tên khóa chứa thông tin đường dẫn thư mục có tệp cấu hình
		/// Khóa này nằm trong tệp web.config
		/// </param>
		/// <param name="_strConfigPrefix">Tiếp đầu ngữ của tệp cấu hình</param>
		/// <returns>Nội dung đã thiết lập của thông tin cần lấy (nội dung Text phức tạp)</returns>
		public static string GetItemContent(string _strReference, string _strConfigName, string _strSettingKey, string _strConfigPrefix)
		{
			ConfigurationSetting l = Load(_strConfigName, _strConfigPrefix, _strSettingKey);
			string _strItemContent = (string)l.configItemsContentTbl[_strReference];
			return _strItemContent == null ? "" : _strItemContent;
		}

		/// <summary>
		/// Hàm lấy bí danh đã được thiết lập của một thông tin cấu hình
		/// Nội dung này nằm trong tệp có dạng Prefix.ModuleName.Config
		/// </summary>
		/// <param name="_strReference">Tên thông tin cấu hình</param>
		/// <param name="_strConfigName">Tên module cấu hình</param>
		/// <param name="_strSettingKey">
		/// Tên khóa chứa thông tin đường dẫn thư mục có tệp cấu hình
		/// Khóa này nằm trong tệp web.config
		/// </param>
		/// <param name="_strConfigPrefix">Tiếp đầu ngữ của tệp cấu hình</param>
		/// <returns>Nội dung đã thiết lập của thông tin cần lấy (nội dung Text phức tạp)</returns>
		public static string GetItemAlias(string _strReference, string _strConfigName, string _strSettingKey, string _strConfigPrefix)
		{
			ConfigurationSetting l = Load(_strConfigName, _strConfigPrefix, _strSettingKey);
			string _strItemContent = (string)l.configItemsAliasTbl[_strReference];
			return _strItemContent == null ? "" : _strItemContent;
		}

		/// <summary>
		/// Hàm lưu nội dung thông tin cấu hình vào tệp cấu hình
		/// Tệp cấu hình có dạng Prefix.ModuleName.Config
		/// </summary>
		/// <param name="_strReference">Tên thông tin cấu hình</param>
		/// <param name="_strConfigName">Tên module cấu hình</param>
		/// <param name="_strSettingKey">
		/// Tên khóa chứa thông tin đường dẫn thư mục có tệp cấu hình
		/// Khóa này nằm trong tệp web.config
		/// </param>
		/// <param name="_strConfigPrefix">Tiếp đầu ngữ của tệp cấu hình</param>
		/// <param name="_strNewValue">Nội dung mới của thông tin cấu hình</param>
		/// <returns>Nội dung đã thiết lập của thông tin cần lấy</returns>
		public static void SetItemContent(string _strReference, string _strConfigName, string _strSettingKey, string _strConfigPrefix, string _strNewValue)
		{
			// Nạp tệp cấu hình hiện thời
			ConfigurationSetting l = Load(_strConfigName, _strConfigPrefix, _strSettingKey);

			// Xác lập giá trị mới
			l.configItemsContentTbl[_strReference] = _strNewValue;

			// Cập nhật lại cấu hình hiện thời với thông tin vừa thay đổi
			UpdateConfigProperties(l, false);

			// Lưu cấu hình ra tệp cấu hình
			Save(_strConfigName, _strConfigPrefix, _strSettingKey, l);
		}

		/// <summary>
		/// Hàm lưu bí danh của cấu hình vào tệp cấu hình
		/// Tệp cấu hình có dạng Prefix.ModuleName.Config
		/// </summary>
		/// <param name="_strReference">Tên thông tin cấu hình</param>
		/// <param name="_strConfigName">Tên module cấu hình</param>
		/// <param name="_strSettingKey">
		/// Tên khóa chứa thông tin đường dẫn thư mục có tệp cấu hình
		/// Khóa này nằm trong tệp web.config
		/// </param>
		/// <param name="_strConfigPrefix">Tiếp đầu ngữ của tệp cấu hình</param>
		/// <param name="_strNewAlias">Nội dung mới của thông tin cấu hình</param>
		/// <returns>Nội dung đã thiết lập của thông tin cần lấy</returns>
		public static void SetItemAlias(string _strReference, string _strConfigName, string _strSettingKey, string _strConfigPrefix, string _strNewAlias)
		{
			// Nạp tệp cấu hình hiện thời
			ConfigurationSetting l = Load(_strConfigName, _strConfigPrefix, _strSettingKey);

			// Xác lập giá trị mới
			l.configItemsAliasTbl[_strReference] = _strNewAlias;

			// Cập nhật lại cấu hình hiện thời với thông tin vừa thay đổi
			UpdateConfigProperties(l, false);

			// Lưu cấu hình ra tệp cấu hình
			Save(_strConfigName, _strConfigPrefix, _strSettingKey, l);
		}

		/// <summary>
		/// Hàm lưu nội dung thông tin cấu hình vào tệp cấu hình
		/// Tệp cấu hình có dạng Prefix.ModuleName.Config
		/// </summary>
		/// <param name="_strReference">Tên thông tin cấu hình</param>
		/// <param name="_strConfigName">Tên module cấu hình</param>
		/// <param name="_strSettingKey">
		/// Tên khóa chứa thông tin đường dẫn thư mục có tệp cấu hình
		/// Khóa này nằm trong tệp web.config
		/// </param>
		/// <param name="_strConfigPrefix">Tiếp đầu ngữ của tệp cấu hình</param>
		/// <param name="_strNewValue">Nội dung mới của thông tin cấu hình</param>
		/// <returns>Nội dung đã thiết lập của thông tin cần lấy</returns>
		public static void SetItemValue(string _strReference, string _strConfigName, string _strSettingKey, string _strConfigPrefix, string _strNewValue)
		{
			// Nạp tệp cấu hình hiện thời
			ConfigurationSetting l = Load(_strConfigName, _strConfigPrefix, _strSettingKey);

			// Xác lập giá trị mới
			l.configItemsTbl[_strReference] = _strNewValue;

			// Cập nhật lại cấu hình hiện thời với thông tin vừa thay đổi
			UpdateConfigProperties(l, false);

			// Lưu cấu hình ra tệp cấu hình
			Save(_strConfigName, _strConfigPrefix, _strSettingKey, l);
		}

		/// <summary>
		/// Hàm lưu nội dung thông tin cấu hình vào tệp cấu hình
		/// Tệp cấu hình có dạng Prefix.ModuleName.Config
		/// </summary>
		/// <param name="_strReference">Tên thông tin cấu hình</param>
		/// <param name="_strConfigName">Tên module cấu hình</param>
		/// <param name="_strSettingKey">
		/// Tên khóa chứa thông tin đường dẫn thư mục có tệp cấu hình
		/// Khóa này nằm trong tệp web.config
		/// </param>
		/// <param name="_strConfigPrefix">Tiếp đầu ngữ của tệp cấu hình</param>
		/// <param name="_strNewAlias">Bí danh của thông tin mới</param>
		/// <param name="_strNewValue">Giá trị của thông tin mới</param>
		/// <param name="_strNewContent">Nội dung của thông tin mới</param>
		public static void AddNewItem(string _strReference, string _strConfigName, string _strSettingKey, string _strConfigPrefix, string _strNewAlias, string _strNewValue, string _strNewContent)
		{
			// Nạp tệp cấu hình hiện thời
			ConfigurationSetting l = Load(_strConfigName, _strConfigPrefix, _strSettingKey);

			// Nếu danh sách chưa có thì tạo mới ds
			if (l.configItems == null) l.configItems = new ConfigItem[0];

			// Kiểm tra xem đã có class hiện thời trong danh sách chưa, nếu đã có thì kết thúc thủ tục
			if (l.configItemsTbl.ContainsKey(_strReference)) return;

			// Thêm phần tử mới vào ds
			ConfigItem[] _newItems = new ConfigItem[l.configItems.Length + 1];
			for (int i = 0; i < l.configItems.Length; i++ )
			{
				_newItems[i] = l.configItems[i];
			}
			ConfigItem _objNewItem = new ConfigItem();
			_objNewItem.reference = _strReference;
			_objNewItem.alias = _strNewAlias;
			_objNewItem.content = _strNewContent;
			_objNewItem.val = _strNewValue;
			_newItems[l.configItems.Length] = _objNewItem;

			// Xác lập giá trị mới
			l.configItems = _newItems;
			l.configItemsTbl.Add(_strReference, _strNewValue);
			l.configItemsAliasTbl.Add(_strReference, _strNewAlias);
			l.configItemsContentTbl.Add(_strReference, _strNewContent);

			// Lưu cấu hình ra tệp cấu hình
			Save(_strConfigName, _strConfigPrefix, _strSettingKey, l);
		}

		/// <summary>
		/// Thủ tục loại bỏ 1 cấu hình
		/// </summary>
		/// <param name="_strReference">Tên thông tin cấu hình</param>
		/// <param name="_strConfigName">Tên module cấu hình</param>
		/// <param name="_strSettingKey">
		/// Tên khóa chứa thông tin đường dẫn thư mục có tệp cấu hình
		/// Khóa này nằm trong tệp web.config
		/// </param>
		/// <param name="_strConfigPrefix">Tiếp đầu ngữ của tệp cấu hình</param>
		public static void RemoveItem(string _strReference, string _strConfigName, string _strSettingKey, string _strConfigPrefix)
		{
			// Nạp tệp cấu hình hiện thời
			ConfigurationSetting l = Load(_strConfigName, _strConfigPrefix, _strSettingKey);

			// Nếu danh sách rỗng thì kết thúc thủ tục
			if (l.configItems == null) return;

			// Loại bỏ phần tử khỏi ds
			ConfigItem[] _newItems = (l.configItems.Length > 0) ? new ConfigItem[l.configItems.Length - 1] : null;
			if (_newItems != null)
			{
				int j = 0;
				for (int i = 0; i < l.configItems.Length; i++ )
				{
					if (l.configItems[i] != null && l.configItems[i].reference != _strReference)
					{
						_newItems[j] = l.configItems[i];
						j++;
					}
				}
			}

			// Xóa phần tử
			l.configItems = _newItems;
			l.configItemsTbl.Remove(_strReference);
			l.configItemsAliasTbl.Remove(_strReference);
			l.configItemsContentTbl.Remove(_strReference);

			// Lưu cấu hình ra tệp cấu hình
			Save(_strConfigName, _strConfigPrefix, _strSettingKey, l);
		}

		/// <summary>
		/// Nạp tệp cấu hình
		/// </summary>
		/// <param name="_strConfig">Tên Module cấu hình</param>
		/// <param name="_strConfigPrefix">Tiếp đầu ngữ của tệp cấu hình</param>
		/// <param name="_strSettingKey">
		/// Tên khóa chứa thông tin đường dẫn thư mục có tệp cấu hình
		/// Khóa này nằm trong tệp web.config
		/// </param>
		/// <returns>Đối tượng lưu trữ thông tin cấu hình</returns>
		private static ConfigurationSetting Load(string _strConfig, string _strConfigPrefix, string _strSettingKey)
		{
			string CacheKey = "Config_" + _strConfig;

			// Lookup in Cache
			ConfigurationSetting l = (ConfigurationSetting)HttpContext.Current.Cache[CacheKey];
			if(l != null) return l;

			_strSettingKey = GetConfigFolder(_strSettingKey);

			// Load Config
			XmlTextReader xmlReader = new XmlTextReader(ConfigurationSetting.GetConfigPhysicalPath(_strConfig, _strConfigPrefix, _strSettingKey));
			try
			{
				l = (ConfigurationSetting)xmlConfig.Deserialize(xmlReader);

				if(l == null) throw new Exception("Unable to load Config " + _strConfig);

				UpdateConfigProperties(l, true);

				// Add to Cache
				HttpContext.Current.Cache.Insert(CacheKey, l, 
				                                 new CacheDependency(ConfigurationSetting.GetConfigPhysicalPath(_strConfig, _strConfigPrefix, _strSettingKey)));
			}
			finally
			{
				xmlReader.Close();
			}

			return l;
		}

		/// <summary>
		/// Thủ tục lưu thông tin ra tệp cấu hình
		/// </summary>
		/// <param name="_strConfig">Tên Module cấu hình</param>
		/// <param name="_strConfigPrefix">Tiếp đầu ngữ của tệp cấu hình</param>
		/// <param name="_strSettingKey">
		/// Tên khóa chứa thông tin đường dẫn thư mục có tệp cấu hình
		/// Khóa này nằm trong tệp web.config
		/// </param>
		/// <param name="_objNewConfig">Đối tượng lưu thông tin cấu hình</param>
		private static void Save(string _strConfig, string _strConfigPrefix, string _strSettingKey, ConfigurationSetting _objNewConfig)
		{
			// Nếu đối tượng cấu hình rỗng thì kết thúc thủ tục
			if (_objNewConfig == null) return;

			// Khai báo tên Module nằm trong Cache
			string CacheKey = "Config_" + _strConfig;

			// Lấy đường dẫn thư mục chứa tệp cấu hình
			_strSettingKey = GetConfigFolder(_strSettingKey);

			// Mở luồng dữ liệu ghi vào tệp cấu hình
			XmlTextWriter xmlWriter = new XmlTextWriter(ConfigurationSetting.GetConfigPhysicalPath(_strConfig, _strConfigPrefix, _strSettingKey), System.Text.Encoding.UTF8);
			try
			{
				xmlWriter.Formatting = Formatting.Indented;
				// Chuyển dữ liệu từ đối tượng ra tệp cấu hình
				xmlConfig.Serialize(xmlWriter, _objNewConfig);

				// Lưu đối tượng mới vào Cache
				HttpContext.Current.Cache.Insert(CacheKey, _objNewConfig, 
					new System.Web.Caching.CacheDependency(ConfigurationSetting.GetConfigPhysicalPath(_strConfig, _strConfigPrefix, _strSettingKey)));
			}
			finally
			{
				// Đóng luồng dữ liệu ghi tệp
				xmlWriter.Close();
			}
		}

		/// <summary>
		/// Cập nhật nội dung các thông tin 
		/// </summary>
		/// <param name="l"></param>
		/// <summary>
		/// Cập nhật các giá trị các thuộc tính của đối tượng cấu hình
		/// </summary>
		/// <param name="l">Đối tượng chứa thông tin cấu hình</param>
		/// <param name="_blnReadingConfig">
		/// True: Đọc thông tin, thông tin được đọc từ tệp cấu hình ra các mảng
		/// False: Ghi thông tin, thông tin được ghi từ mảng vào configItems
		/// </param>
		private static void UpdateConfigProperties(ConfigurationSetting l, bool _blnReadingConfig)
		{
			if(l.configItems == null) return;
				//throw new Exception("No configItems found in current Config");

			foreach(ConfigItem w in l.configItems)
			{
				if (w != null)
				{
					if (_blnReadingConfig)
					{
						l.configItemsTbl[w.reference] = w.val;

						l.configItemsContentTbl[w.reference] = w.content;

						l.configItemsAliasTbl[w.reference] = w.alias;
					}
					else
					{
						w.val = (l.configItemsTbl[w.reference] != null) ? l.configItemsTbl[w.reference].ToString() : "";

						w.content = (l.configItemsContentTbl[w.reference] != null) ? l.configItemsContentTbl[w.reference].ToString() : "";

						w.alias = (l.configItemsAliasTbl[w.reference] != null) ? l.configItemsAliasTbl[w.reference].ToString() : "";
					}
				}
			}
		}

		/// <summary>
		/// Mảng chứa danh sách các thông tin cấu hình
		/// </summary>
		[XmlElement("item")]
		public ConfigItem[] configItems = new ConfigItem[] {};

		/// <summary>
		/// Tập hợp lưu danh sách giá trị của thuộc tính value
		/// </summary>
		[XmlIgnore]
		private Hashtable configItemsTbl = new Hashtable();

		/// <summary>
		/// Tập hợp lưu danh sách giá trị của nội dung cấu hình
		/// </summary>
		[XmlIgnore]
		private Hashtable configItemsContentTbl = new Hashtable();

		/// <summary>
		/// Tập hợp lưu danh sách giá trị của bí danh
		/// </summary>
		[XmlIgnore]
		private Hashtable configItemsAliasTbl = new Hashtable();

		public class ConfigItem
		{
			/// <summary>
			/// Từ khóa đại diện tên cấu hình
			/// </summary>
			[XmlAttribute("key")]
			public string reference;

			/// <summary>
			/// Giá trị thông tin cấu hình dạng đơn giản
			/// </summary>
			[XmlAttribute("value")]
			public string val;

			/// <summary>
			/// Giá trị thông tin cấu hình dạng phức tạp
			/// </summary>
			[XmlText()]
			public string content;

			[XmlAttribute("alias")]
			public string alias="";
		}
	}
}
