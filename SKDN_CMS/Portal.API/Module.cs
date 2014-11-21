using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web.UI;

namespace DFISYS.API
{
	//public delegate void PageLoadEventHanlder(object sender, EventArgs e);

	/// <summary>
	/// Base class for each Portal Module Control. Provides the current Tab and Module Definition.
	/// </summary>
	public class Module : UserControl
	{
		/// <summary>
		/// Parent Tab
		/// </summary>
		private string m_TabRef = "";
		/// <summary>
		/// Module Definition from the Portal Definition
		/// </summary>
		private string m_ModuleRef = "";
		/// <summary>
		/// Modules virtual base path.
		/// </summary>
		private string m_ModuleVirtualPath = "";
		/// <summary>
		/// Modules Type.
		/// </summary>
		private string m_ModuleType = "";

		/// <summary>
		/// Giá trị xác định User có quyền xem Module này hay không
		/// </summary>
		private bool m_HasEditRights = false;

		/// <summary>
		/// Đối tượng lưu trữ giá trị các thông số cần có khi thực thi Module
		/// </summary>
		private ModuleRuntimeSettings moduleRuntimeSettings;

		public bool IsAjaxLoad = false;

		/// <summary>
		/// Initializes the Control. Called by the Protal Framework
		/// </summary>
		/// <param name="tabRef">Tab Reference</param>
		/// <param name="moduleRef">Module Reference</param>
		/// <param name="type">Module Type</param>
		/// <param name="virtualPath">Module Virtual Path</param>
		/// <param name="hasEditRights">User accessible</param>
		public void InitModule(string tabRef, string moduleRef, string type, string virtualPath, bool hasEditRights)
		{			
			m_TabRef = tabRef;
			m_ModuleRef = moduleRef;
			m_ModuleVirtualPath = virtualPath;
			m_HasEditRights = hasEditRights;
			m_ModuleType = type;
		}

		/// <summary>
		/// The Module can control its visibility. The Login Module does so
		/// </summary>
		/// <returns>true if the Module should be visible</returns>
		public virtual bool IsVisible()
		{
			return true;
		}

		/// <summary>
		/// The current Tab reference. Readonly
		/// </summary>
		public string TabRef
		{
			get 
			{ 
				return m_TabRef;
			}
		}
		/// <summary>
		/// The Modules reference. Readonly
		/// </summary>
		public string ModuleRef
		{
			get 
			{ 
				return m_ModuleRef;
			}
		}
		/// <summary>
		/// The Modules type. Readonly
		/// </summary>
		public string ModuleType
		{
			get 
			{ 
				return m_ModuleType;
			}
		}
		/// <summary>
		/// Modules virtual base path. Readonly
		/// </summary>
		public string ModuleVirtualPath
		{
			get { return m_ModuleVirtualPath; }
		}
		/// <summary>
		/// Modules physical base path. Readonly
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never), Browsable(true)]
		public string ModulePhysicalPath
		{
			get 
			{
				try
				{
					return Server.MapPath(m_ModuleVirtualPath);
				}
				catch // :D
				{
					return "";
				}
			}
		}

		/// <summary>
		/// Build a URL to the current Page. Use this method to implement Modules that needs URL Parameter.
		/// </summary>
		/// <param name="parameter">URL Parameter.</param>
		/// <returns>URL with parameter</returns>
		/// <example>Response.Redirect(BuildURL("dir=myPhotos&size=large"));</example>
		public string BuildURL(string parameter)
		{
			string p = "";
			if(!parameter.StartsWith("&")) 
			{
				p = "&" + parameter;
			}
			else
			{
				p = parameter;
			}
			return Config.GetTabURL(TabRef) + p;
		}

		public bool ModuleHasEditRights
		{
			get { return m_HasEditRights; }
		}

		/// <summary>
		/// 
		/// </summary>
		public string ModuleConfigFile
		{
			get
			{
				return ModulePhysicalPath + "/" + "Module_" + ModuleRef + ".config";
			}
		}

		public string ModuleConfigSchemaFile
		{
			get
			{
				return ModulePhysicalPath + "/" + "Module.xsd";
			}
		}

		/// <summary>
		/// Read module.config file in local folder - Extend:Store data in system cache and read from it
		/// </summary>
		/// <param name="t"></param>
		/// <returns>object value</returns>
		public object ReadCommonConfig(System.Type t)
		{
			string fileName = ModulePhysicalPath + "/Module.config";
			
			if(!System.IO.File.Exists(fileName))
				return null;

			XmlTextReader xmlReader = null;
			XmlSerializer xmlSerial = new XmlSerializer(t);
			object o = null;
			try
			{
				xmlReader = new XmlTextReader(fileName);
				o = xmlSerial.Deserialize(xmlReader);
				xmlReader.Close();
			}
			catch(Exception e)
			{
				if(xmlReader != null)
				{
					xmlReader.Close();
				}
				// Do not throw exceptions
				Trace.Warn("Module", "Error loading Modules Common Config", e);
			}

			return o;
		}
		
		/// <summary>
		/// Reads the config file. The schema is optional.
		/// </summary>
		/// <returns>
		/// Null if config file does not exists and no schema exists,
		/// else the schema is read and a empty DataSet is returned.
		/// </returns>
		public DataSet ReadConfig()
		{
			DataSet ds = null;
			try
			{
				if(System.IO.File.Exists(ModuleConfigFile))
				{
					ds = new DataSet();
					if(System.IO.File.Exists(ModuleConfigSchemaFile))
					{
						ds.ReadXmlSchema(ModuleConfigSchemaFile);
					}
					ds.ReadXml(ModuleConfigFile);
				}
			}
			catch(Exception e)
			{
				// Do not throw exceptions
				Trace.Warn("Module", "Error loading Modules Config as Dataset", e);
				return null;
			}

			if(ds == null && System.IO.File.Exists(ModuleConfigSchemaFile))
			{
				ds = new DataSet();
				ds.ReadXmlSchema(ModuleConfigSchemaFile);
			}

			return ds;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public object ReadConfig(System.Type t)
		{
			return ReadConfig(t, ModuleConfigFile);
		}

		/// <summary>
		/// Trigger child control
		/// </summary>
		public void ForceChildControls()
		{
			this.EnsureChildControls();
		}

//		public void CallPageLoad()
//		{
//			Page.Load(this, new EventArgs());
//		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="t"></param>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public object ReadConfig(System.Type t, string fileName)
		{
			if(!System.IO.File.Exists(fileName))
				return null;

			XmlTextReader xmlReader = null;
			XmlSerializer xmlSerial = new XmlSerializer(t);
			object o = null;
			try
			{
				xmlReader = new XmlTextReader(fileName);
				o = xmlSerial.Deserialize(xmlReader);
				xmlReader.Close();
			}
			catch(Exception e)
			{
				if(xmlReader != null)
				{
					xmlReader.Close();
				}
				// Do not throw exceptions
				Trace.Warn("Module", "Error loading Modules Config", e);
			}

			return o;
		}

		public void WriteConfig(DataSet ds)
		{
			try
			{
				ds.WriteXml(ModuleConfigFile);
			}
			catch(Exception e)
			{
				// Do not throw exceptions
				Trace.Warn("Module", "Error writing Modules Config Dataset", e);
			}
		}

		public void WriteConfig(object o)
		{
			WriteConfig(o, ModuleConfigFile);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="o"></param>
		/// <param name="fileName"></param>
		public void WriteConfig(object o, string  fileName)
		{
			XmlTextWriter xmlWriter = null;
			XmlSerializer xmlSerial = new XmlSerializer(o.GetType());
			try
			{
				xmlWriter = new XmlTextWriter(fileName, System.Text.Encoding.UTF8);
				xmlWriter.Formatting = Formatting.Indented;
				xmlSerial.Serialize(xmlWriter, o);
				xmlWriter.Close();
			}
			catch(Exception e)
			{
				if(xmlWriter != null)
				{
					xmlWriter.Close();
				}
				// Do not throw exceptions
				Trace.Warn("Module", "Error writing Modules Config", e);
			}
		}

		public static Module GetModuleControl(Control ctrl)
		{
			if(ctrl is Module) return ctrl as Module;
			if(ctrl.Parent == null) return null;
			return GetModuleControl(ctrl.Parent);
		}

		/// <summary>
		/// Thủ tục nạp các giá trị của thông số
		/// Được sử dụng trong Module class
		/// </summary>
		public void LoadRuntimeSettings()
		{
			string path = Config.GetModulePhysicalPath(this.ModuleType) + "Module_" + this.ModuleRef + ".config";
			string mrs = "ModuleRuntimeSettings_" + path;

			if(File.Exists(path))
			{
				// Lookup in Cache
				moduleRuntimeSettings = (ModuleRuntimeSettings)System.Web.HttpContext.Current.Cache[mrs];
				if(moduleRuntimeSettings != null) return;

				XmlTextReader xmlReader = new XmlTextReader(path);
				try
				{
					moduleRuntimeSettings = (ModuleRuntimeSettings)ModuleRuntimeSettings.xmlModuleSettings.Deserialize(xmlReader);
				}
				finally
				{
					xmlReader.Close();
				}

				// Add to Cache
				System.Web.HttpContext.Current.Cache.Insert(mrs, moduleRuntimeSettings, 
					new System.Web.Caching.CacheDependency(path));
			}
		}

		/// <summary>
		/// Hàm lấy giá trị của một thông số khi thực thi của Module hiện thời
		/// </summary>
		/// <param name="_blnIsClientControl">True: Module của người dùng, False: Module của người quản trị</param>
		/// <param name="_strPropertyName">Tên thông số</param>
		/// <returns>Giá trị của thông số</returns>
		public object ReadRuntimeConfig(bool _blnIsClientControl, string _strPropertyName)
		{
			if (moduleRuntimeSettings != null)
			{
				return moduleRuntimeSettings.GetRuntimePropertyValue(_blnIsClientControl, _strPropertyName);
			}
			else
			{
				return null;
			}
		}
	}

	/// <summary>
	/// Base class for each Portal Edit Module Control. Derived from Module
	/// </summary>
	public class EditModule : Module
	{
		/// <summary>
		/// Returns the callers URL. 
		/// The return value depends on the portals render settings "Table or Frame" 
		/// and "UseTabHttpModule"
		/// </summary>
		/// <returns>The BackURL</returns>
		public string GetBackURL()
		{
			return Config.GetTabURL(TabRef);
		}

		/// <summary>
		/// Redirects back to the callers URL. Uses the GetBackURL() Method.
		/// </summary>
		public void RedirectBack()
		{
			Response.Redirect(GetBackURL());
		}
	}

	/// <summary>
	/// Lớp khai chứa thông tin khai báo về một thông số của module
	/// </summary>
	[Serializable]
	public class ModulePropertyDeclaration
	{
		// Default Contructor for Serialization
		public ModulePropertyDeclaration(){}

		/// <summary>
		/// Mảng chứa danh sách các giá trị cho trước có thể có của Module
		/// </summary>
		[XmlArray("values"), XmlArrayItem("value", typeof(ModulePropertyValue))]
		public ArrayList AvaiableValues = new ArrayList();

		/// <summary>
		/// Tên thông số
		/// </summary>
		[XmlAttribute("name")]
		public string Name = "";

		/// <summary>
		/// Chú giải về thông số
		/// </summary>
		[XmlAttribute("caption")]
		public string Caption = "";
	}

	/// <summary>
	/// Lớp chứa nội dung của một thông số được dùng bởi một Module
	/// </summary>
	[Serializable]
	public class ModuleProperty
	{
		// Default Contructor for Serialization
		public ModuleProperty(){}

		/// <summary>
		/// Tên thông số
		/// </summary>
		[XmlAttribute("name")]
		public string Name = "";

		/// <summary>
		/// Giá trị của thông số
		/// </summary>
		[XmlText()]
		public string Value = "";

		/// <summary>
		/// Hàm khởi tạo
		/// </summary>
		/// <param name="_strName">Tên thông số</param>
		/// <param name="_strValue">Giá trị của thông số</param>
		public ModuleProperty(string _strName, string _strValue)
		{
			Name = _strName;
			Value = _strValue;
		}
	}

	/// <summary>
	/// Lớp chứa thông tin về một giá trị cho sẵn của một thông số của Module
	/// </summary>
	[Serializable]
	public class ModulePropertyValue
	{
		/// <summary>
		/// Tên của giá trị
		/// </summary>
		[XmlAttribute("valuename")]
		public string AvaiableKey = "";

		/// <summary>
		/// Nội dung giá trị
		/// </summary>
		[XmlAttribute("valuecontent")]
		public string AvaiableValue = "";
	}

	/// <summary>
	/// Lớp chứa dữ liệu về danh sách các thông số cần dùng khi thực thi của một Module
	/// </summary>
	[Serializable]
	[XmlRoot("module")]
	public class ModuleRuntimeSettings
	{
		// Default Contructor for Serialization
		public ModuleRuntimeSettings(){}

		// XmlSerializer
		public static XmlSerializer xmlModuleSettings = new XmlSerializer(typeof(ModuleRuntimeSettings));

		/// <summary>
		/// Mảng chứa danh sách các thông số dành cho Module bên phía người dùng
		/// </summary>
		[XmlArray("ctrlSettings"), XmlArrayItem("property", typeof(ModuleProperty))]
		public ArrayList ClientRuntimeProperties = new ArrayList();

		/// <summary>
		/// Mảng chứa danh sách các thông số dành cho Module phía người quản trị
		/// </summary>
		[XmlArray("editCtrlSettings"), XmlArrayItem("property", typeof(ModuleProperty))]
		public ArrayList AdminRuntimeProperties = new ArrayList();

		/// <summary>
		/// Hàm lấy giá trị một thông số
		/// </summary>
		/// <param name="_blnIsClientControl">True: Module của người dùng, False: Module của người quản trị</param>
		/// <param name="_strPropertyName">Tên thông số</param>
		/// <returns>Giá trị thông số</returns>
		public string GetRuntimePropertyValue(bool _blnIsClientControl, string _strPropertyName)
		{
			try
			{
				if (_blnIsClientControl)
				{
					foreach(ModuleProperty _objProperty in ClientRuntimeProperties)
					{
						if (_objProperty.Name.ToUpper() == _strPropertyName.ToUpper())
						{
							return _objProperty.Value;
						}
					}
				}
				else
				{
					foreach(ModuleProperty _objProperty in AdminRuntimeProperties)
					{
						if (_objProperty.Name.ToUpper() == _strPropertyName.ToUpper())
						{
							return _objProperty.Value;
						}
					}
				}
			}
			catch
			{
				return "";
			}

			return "";
		}

		/*/// <summary>
		/// Hàm lấy danh sách các giá trị cho trước có thể có của thuộc tính
		/// </summary>
		/// <param name="_blnIsClientControl"></param>
		/// <param name="_strPropertyName"></param>
		/// <returns></returns>
		public ArrayList GetAvaiablePropertyValues(bool _blnIsClientControl, string _strPropertyName)
		{
			try
			{
				if (_blnIsClientControl)
				{
					foreach(ModuleProperty _objProperty in ClientRuntimeProperties)
					{
						if (_objProperty.Name.ToUpper() == _strPropertyName.ToUpper())
						{
							return _objProperty.AvaiableValues;
						}
					}
				}
				else
				{
					foreach(ModuleProperty _objProperty in AdminRuntimeProperties)
					{
						if (_objProperty.Name.ToUpper() == _strPropertyName.ToUpper())
						{
							return _objProperty.AvaiableValues;
						}
					}
				}
			}
			catch
			{
				return null;
			}

			return null;
		}*/

		/// <summary>
		/// Thủ tục thiết lập giá trị của một thông số
		/// </summary>
		/// <param name="_blnIsClientControl">True: Module của người dùng, False: Module của người quản trị</param>
		/// <param name="_strPropertyName">Tên thông số</param>
		/// <param name="_strValue">Giá trị của thông số</param>
		public void SetRuntimePropertyValue(bool _blnIsClientControl, string _strPropertyName, string _strValue)
		{
			try
			{
				if (_blnIsClientControl)
				{
					foreach(ModuleProperty _objProperty in ClientRuntimeProperties)
					{
						if (_objProperty.Name.ToUpper() == _strPropertyName.ToUpper())
						{
							_objProperty.Value = _strValue;
							break;
						}
					}
				}
				else
				{
					foreach(ModuleProperty _objProperty in AdminRuntimeProperties)
					{
						if (_objProperty.Name.ToUpper() == _strPropertyName.ToUpper())
						{
							_objProperty.Value = _strValue;
							break;
						}
					}
				}
			}
			catch
			{
				return;
			}
		}
	}
}
