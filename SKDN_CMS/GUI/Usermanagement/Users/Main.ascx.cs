namespace DFISYS.GUI.Users
{
	using System;
    using DFISYS.GUI.Users.Common;
    using System.Web.Security;
    using System.Web;
    using DFISYS.User.Security;
    /// <summary>
	///		Summary description for Template.
	/// </summary>
	public partial class Main : DFISYS.API.Module
	{

		protected void Page_Load(object sender, EventArgs e)
		{
            AssignLink();
            string mode = ""; ;
            if (Request.QueryString["cpmode"] == null || Request.QueryString["cpmode"] == "")
                mode = Const.OBJECT_USER;
            else
                mode = Request.QueryString["cpmode"].ToString();
            if (mode != null && mode.Trim() != "")
            {
                switch (mode)
                {
                    case Const.OBJECT_USER: this.Container.Controls.Add(LoadControl("User/ViewUser.ascx")); break;
                    case Const.OBJECT_USEREDIT: this.Container.Controls.Add(LoadControl("User/UserEditor.ascx")); break;
                    case Const.OBJECT_USERASSIGN: this.Container.Controls.Add(LoadControl("User/AssignPermission.ascx")); break;
                    case Const.OBJECT_QUANLY_DEFAULT: this.Container.Controls.Add(LoadControl("User/RolePermission.ascx")); break;
                    case Const.OBJECT_ERROR:
                    {
                        lblMessage.Text = Request.Params.Get("message"); ;
                        break;
                    }
                }
            }
		}
        private void AssignLink()
        {
            lnkUsers.NavigateUrl = "~/users.aspx";
            lnkDefault.NavigateUrl = "~/users/role_permission.aspx";
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
            Response.Redirect("~/login.aspx");
        }
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
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
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
