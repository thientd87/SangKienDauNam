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
using Portal.User.Security;
namespace Portal.GUI.Administrator.GenerateTabs
{
    public partial class Menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            itemAddCat.NavigateUrl = "~/category/editcat.aspx";
            itemListCat.NavigateUrl = "~/category/catlist.aspx";
            itemComeAdmin.NavigateUrl = "~/adminportal.aspx";
            itemEditionType.NavigateUrl = "~/category/editiontype.aspx";
            itemComeOffice.NavigateUrl = "~/office.aspx";

            String cpmode = Request.QueryString["cpmode"];
            if (cpmode.ToLower().IndexOf("editcat") >= 0)
            {
                Div1.Attributes.Remove("class");
                Div1.Attributes.Add("class", "Menuleft_Item_Select");
            }
            else if (cpmode.ToLower().IndexOf("catlist") >= 0)
            {
                Div2.Attributes.Remove("class");
                Div2.Attributes.Add("class", "Menuleft_Item_Select");
            }
            else if (cpmode.ToLower().IndexOf("editiontype") >= 0)
            {
                Div5.Attributes.Remove("class");
                Div5.Attributes.Add("class", "Menuleft_Item_Select");
            }

            MainSecurity objsec = new MainSecurity();
            //string strRoles= objsec.GetRoleAsString(Page.User.Identity.Name, Portal.API.Config.CurrentChannel);
            Role objrole = objsec.GetRole(Page.User.Identity.Name, Portal.API.Config.CurrentChannel);
            if (objrole.isAdministrator)
                Div4.Visible =false;
            else 
                Div3.Visible =false;
        }
    }
}