using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace DFISYS.BO.Editoral.ModuleConfig
{
	public class ModuleCommon
    {
        protected string configFileLocation = "";
        protected string configPhysicalFileLocation = "";
        protected string moduleReference = "reference";
        [XmlIgnore]
        private ModuleSettings moduleSettings = null;
        [XmlIgnore]
        private ModuleRuntimeSettings moduleRuntimeSettings = null;

        public string ConfigFileLocation
        {
            set
            {
                this.configFileLocation = value;
                this.configPhysicalFileLocation = HttpContext.Current.Server.MapPath(configFileLocation);
            }
            get
            {
                return this.configFileLocation;
            }
        }

        public ModuleSettings ModuleSettings
        {
            get
            {
                return moduleSettings;
            }
        }
        public string ConfigFilePath
        {
            get
            {
                return configFileLocation + "/Module_" + moduleReference + ".config";
            }
        }

        public string ModuleReference
        {
            set
            {
                this.moduleReference = value;
            }
            get
            {
                return this.moduleReference;
            }
        }

        internal void LoadModuleSettings()
        {
            string path = configPhysicalFileLocation + "/ModuleSettings.config";
            if (File.Exists(path))
            {
                XmlTextReader xmlReader = new XmlTextReader(path);
                moduleSettings = (ModuleSettings)ModuleSettings.xmlModuleSettings.Deserialize(xmlReader);
                xmlReader.Close();
            }
            else
            {
                moduleSettings = null;
            }
        }

        public void SaveRuntimeSetting()
        {
            string path = configPhysicalFileLocation + "/Module_" + moduleReference + ".config";
            XmlTextWriter _objXmlWriter = new XmlTextWriter(path, System.Text.Encoding.UTF8);
            _objXmlWriter.Formatting = Formatting.Indented;
            try
            {
                ModuleRuntimeSettings.xmlModuleSettings.Serialize(_objXmlWriter, moduleRuntimeSettings);
            }
            finally
            {
                _objXmlWriter.Close();
            }
        }

        public string getRuntimeProperty(string propertyName)
        {
            return moduleRuntimeSettings.GetRuntimePropertyValue(false, propertyName);
        }

        public void setRuntimeProperty(string propertyName, string propertyValue)
        {
            moduleRuntimeSettings.SetRuntimePropertyValue(false, propertyName, propertyValue);
        }

        internal ModuleRuntimeSettings LoadRuntimeProperties()
        {
            string path = configPhysicalFileLocation + "/Module_" + moduleReference + ".config";
            //string mrs = "ModuleRuntimeSettings_" + path;

            if (File.Exists(path))
            {
                // Lookup in Cache
                //moduleRuntimeSettings = (ModuleRuntimeSettings)HttpContext.Current.Cache[mrs];
                //if (moduleRuntimeSettings != null) return;

                XmlTextReader xmlReader = new XmlTextReader(path);
                try
                {
                    moduleRuntimeSettings =
                        (ModuleRuntimeSettings)ModuleRuntimeSettings.xmlModuleSettings.Deserialize(xmlReader);
                }
                finally
                {
                    xmlReader.Close();
                }
            }
            else
            {
                // Config file is not existed. Create new config File
                // Attemp to load Module Settings
                LoadModuleSettings();
                moduleRuntimeSettings = new ModuleRuntimeSettings();
                if (moduleSettings != null)
                {
                    // Read Declared Properties
                    foreach (
                        ModulePropertyDeclaration _objPropertyDeclaration in
                            moduleSettings.ClientRuntimeDefinedProperties)
                    {
                        moduleRuntimeSettings.ClientRuntimeProperties.Add(
                            new ModuleProperty(_objPropertyDeclaration.Name, ""));
                    }
                    foreach (
                        ModulePropertyDeclaration _objPropertyDeclaration in
                            moduleSettings.AdminRuntimeDefinedProperties)
                    {
                        moduleRuntimeSettings.AdminRuntimeProperties.Add(
                            new ModuleProperty(_objPropertyDeclaration.Name, ""));
                    }

                    // Save Module Runtime Settings

                    //XmlTextWriter _objXmlWriter = new XmlTextWriter(path, System.Text.Encoding.UTF8);
                    //_objXmlWriter.Formatting = Formatting.Indented;
                    //try
                    //{
                    //    ModuleRuntimeSettings.xmlModuleSettings.Serialize(_objXmlWriter, moduleRuntimeSettings);
                    //}
                    //finally
                    //{
                    //    _objXmlWriter.Close();
                    //}
                }
            }

            return moduleRuntimeSettings;
        }
    }
}
