using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DFISYS.User.Security;

namespace DFISYS.GUI.Administrator.CategoryManager
{
    public partial class Menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            itemComeOffice.NavigateUrl = "~/office.aspx";
            MainSecurity objsec = new MainSecurity();
            Role objrole = objsec.GetRole(Page.User.Identity.Name, DFISYS.API.Config.CurrentChannel);
             
        }
    }
}