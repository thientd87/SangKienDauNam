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
using System.Collections;

namespace DFISYS.BO.Editoral.ModuleConfig
{
    [Serializable]
    [XmlRoot("module")]
    public class ModuleRuntimeSettings
    {
        // Default Contructor for Serialization
        public ModuleRuntimeSettings() { }

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
                    foreach (ModuleProperty _objProperty in ClientRuntimeProperties)
                    {
                        if (_objProperty.Name.ToUpper() == _strPropertyName.ToUpper())
                        {
                            return _objProperty.Value;
                        }
                    }
                }
                else
                {
                    foreach (ModuleProperty _objProperty in AdminRuntimeProperties)
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
                    foreach (ModuleProperty _objProperty in ClientRuntimeProperties)
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
                    foreach (ModuleProperty _objProperty in AdminRuntimeProperties)
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
