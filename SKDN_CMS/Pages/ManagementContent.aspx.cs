using System;
using DFISYS.API;
using System.Web;
using System.Text;
using System.IO;
using System.Web.UI;
using DFISYS.User.Db;
using System.Data;
using System.Configuration;
namespace DFISYS
{
    public partial class ManagementContent : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e)
        {

            this.Page.Title = ConfigurationManager.AppSettings["icmsHeader"] ?? "NextCom Content Management System - NextCom.vn";

            // Kiem tra xem neu nguoi nay cua login thi quay ve trang Login.aspx
            if (Context.User.Identity.Name == "" && Context.Request.RawUrl.ToString().IndexOf("/login.aspx") == -1)
                Response.Redirect("/login.aspx");

            DateTime dt = DateTime.Now;
            TimeSpan ts = new TimeSpan(0, 30, 0);

            HttpCookie cookie1 = new HttpCookie("FileManager");
            cookie1.Values["PW"] = Crypto.EncryptByDay(Context.User.Identity.Name);
            cookie1.Expires = dt.Add(ts);
            Response.Cookies.Add(cookie1);

            PortalDefinition.Tab _objCurrentTab = PortalDefinition.GetCurrentTab();

            if (_objCurrentTab != null)
                if (_objCurrentTab.reference.IndexOf("office") < 0)
                    Session.Remove("cpmode");

        }

        

    }
}
