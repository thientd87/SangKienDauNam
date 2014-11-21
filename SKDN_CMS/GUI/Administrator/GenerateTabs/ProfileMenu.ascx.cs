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

namespace Portal.GUI.Administrator.GenerateTabs
{
    public partial class ProfileMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void itemLogOut_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["PortalUser"];
            if (cookie != null)
            {
                cookie.Values["AC"] = "";
                cookie.Values["PW"] = "";
                DateTime dt = DateTime.Now;
                dt.AddDays(-1);
                cookie.Expires = dt;
                Response.Cookies.Add(cookie);
            }
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }
    }
}