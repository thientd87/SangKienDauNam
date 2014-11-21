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
}
