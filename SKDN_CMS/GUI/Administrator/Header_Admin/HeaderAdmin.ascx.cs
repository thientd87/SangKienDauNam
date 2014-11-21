namespace DFISYS.GUI.Administrator.Header_Admin {
    using System;
    using System.Web;
    using System.Web.Security;
    using DFISYS.Core.DAL;
    using DFISYS.User.Db;
    using DFISYS.User.Security;
    using System.Data;
    using DFISYS.BO.Editoral.Menu;
    using System.Xml;
    using System.Text;

    /// <summary>
    ///		Summary description for HeaderAdmin.
    /// </summary>
    public partial class HeaderAdmin : DFISYS.API.Module {
        XmlDocument doc = null;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here
            if (Page.User.Identity.IsAuthenticated) {
               // lbtLogout.Attributes.Add("style", "color:#fff;");
                string key = Page.User.Identity.Name + "_Full";
                if (Session[key] == null) {
                    using (DFISYS.User.Db.MainDB db = new DFISYS.User.Db.MainDB()) {
                        UserRow row = db.UserCollection.GetByPrimaryKey(Page.User.Identity.Name.Trim());
                        if (row != null)
                            Session[key] = row.User_Name;
                    }
                }
                ltrUser.Text = String.Format("{0}", Session[key] != "" ? Session[key].ToString() : Page.User.Identity.Name);

              

               
            }

          
        }

      #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {

        }
        #endregion

        protected void lbtLogout_Click(object sender, EventArgs e) {
            HttpCookie cookie = Request.Cookies["PortalUser"];
            if (cookie != null) {
                cookie.Values["AC"] = "";
                cookie.Values["PW"] = "";
                DateTime dt = DateTime.Now;
                dt.AddDays(-1);
                cookie.Expires = dt;
                Response.Cookies.Add(cookie);
            }
            FormsAuthentication.SignOut();
            Context.User = null;
            Response.Redirect("/login.aspx");
        }
    }
}
