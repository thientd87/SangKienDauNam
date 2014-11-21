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
    [XmlRoot("module"), Serializable]
    public class ModuleSettings
    {
        public static XmlSerializer xmlModuleSettings = new XmlSerializer(typeof(ModuleSettings));

        [XmlArray("editCtrlSettings"), XmlArrayItem("property", typeof(ModulePropertyDeclaration))]
        public ArrayList AdminRuntimeDefinedProperties = new ArrayList();

        [XmlArray("ctrlSettings"), XmlArrayItem("property", typeof(ModulePropertyDeclaration))]
        public ArrayList ClientRuntimeDefinedProperties = new ArrayList();

        /// <summary>
        /// Modules View .ascx Control.
        /// </summary>
        [XmlElement("ctrl")]
        public string ctrl = "";

        /// <summary>
        /// Modules Edit .ascx Control. 'none' if the Module has no Edit Control.
        /// </summary>
        [XmlElement("editCtrl")]
        public string editCtrl = "";

        /// <summary>
        /// True if the Module has no Edit Control. Property editCtrl mus be set to 'none' (case sensitive!)
        /// </summary>
        [XmlIgnore]
        public bool HasEditCtrl
        {
            get { return editCtrl != "none"; }
        }


        /// <summary>
        /// Hàm lấy danh sách các tham số cần thiết khi thực thi Module
        /// </summary>
        /// <param name="_blnIsClientControl">True: Module dành cho người dùng, False: Module cho người quản trị</param>
        /// <returns>Danh sách các tham số cần thiết khi thực thi module</returns>
        public ArrayList GetRuntimeProperties(bool _blnIsClientControl)
        {
            return _blnIsClientControl ? ClientRuntimeDefinedProperties : AdminRuntimeDefinedProperties;
        }


        /// <summary>
        /// Hàm lấy thông tin về một tham số cần thiết khi thực thi Module
        /// </summary>
        /// <param name="_blnIsClientControl">True: Module dành cho người dùng, False: Module cho người quản trị</param>
        /// <param name="_intPropertyIndex">Chỉ số của tham số</param>
        /// <returns>ModulePropertyDeclaration chứa Thông tin về tham số</returns>
        public ModulePropertyDeclaration GetPropertyDeclaration(bool _blnIsClientControl, int _intPropertyIndex)
        {
            return
                _blnIsClientControl
                    ? (ClientRuntimeDefinedProperties[_intPropertyIndex] as ModulePropertyDeclaration)
                    : (AdminRuntimeDefinedProperties[_intPropertyIndex] as ModulePropertyDeclaration);
        }
    }
}
