using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using DFISYS.API;

namespace DFISYS {
    /// <summary>
    /// UserControl for the EditLink. Derived form HtmlAnchor. 
    /// Link redirects either to EditPageTable.aspx or EditPageFrame.apsx
    /// </summary>
    [DefaultProperty("Text"),
        ToolboxData("<{0}:EditLink runat=server></{0}:EditLink>")]
    public class EditLink : HtmlAnchor {
        /// <summary>
        /// The Modules reference
        /// </summary>
        internal string ModuleRef = "";

        protected override void OnLoad(EventArgs args) {
            base.OnLoad(args);
            base.InnerText = Language.GetText("EditLink_Text");
            HRef = Helper.GetEditLink(ModuleRef);
        }
    }
}
