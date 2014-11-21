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

namespace DFISYS.BO.Editoral.ModuleConfig
{
    [Serializable]
    public class ModuleProperty
    {
        // Default Contructor for Serialization
        public ModuleProperty() { }

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
}
