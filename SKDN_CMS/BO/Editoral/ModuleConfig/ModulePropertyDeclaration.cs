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
    public class ModulePropertyDeclaration
    {
        // Default Contructor for Serialization
        public ModulePropertyDeclaration() { }

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
}
