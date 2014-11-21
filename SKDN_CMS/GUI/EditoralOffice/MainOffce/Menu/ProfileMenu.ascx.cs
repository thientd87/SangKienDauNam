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
using DFISYS.Core.DAL;

namespace DFISYS.GUI.EditoralOffice.MainOffice.Menu
{
    public partial class ProfileMenu : DFISYS.GUI.EditoralOffice.MainOffce.Main
    {
        private void SetClassForLink(HtmlGenericControl hl)
        {
            hl.Attributes.Remove("class");
            hl.Attributes.Add("class", "Menuleft_Item_Select");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String UserName = HttpContext.Current.User.Identity.Name.ToLower();
                if (UserName == "admin" || UserName == "channelvn")
                    div2.Visible = true;
                else
                    div2.Visible = false;

                itemProfile.NavigateUrl = "~/office/profile.aspx";
                String cpmode = Request.QueryString["cpmode"];
                if (cpmode == "profile")
                    SetClassForLink(div1);
            }
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
            Response.Redirect("/Login.aspx");
        }

        protected void itemAccount_Click(object sender, EventArgs e)
        {
            Control ctrNewsedit = LoadControl("../Newsedit/NewsEditor.ascx");
            //Control ctrNewsedit = LoadControl("editnews/editnews.ascx");
            plcMain.Controls.Add(ctrNewsedit);
        }
    }
}