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
    public partial class MainModule : Portal.API.Module 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Control ctrMenu = LoadControl("Menu.ascx");
            plcMenu.Controls.Add(ctrMenu);
            ctrMenu = LoadControl("ProfileMenu.ascx");
            plcProfile.Controls.Add(ctrMenu);
            if (Request.QueryString["cpmode"] != null)
            {
                string strMode = Request.QueryString["cpmode"].ToString();
                Control ctrCatEdit;
                switch (strMode.ToLower())
                {
                    case "editcat":
                        ctrCatEdit = LoadControl("CategoryEdit.ascx");
                        plcMain.Controls.Add(ctrCatEdit);
                        break;
                    case "catlist":
                        ctrCatEdit = LoadControl("GenerateTabs.ascx");
                        plcMain.Controls.Add(ctrCatEdit);
                        break;
                    case "editiontype":
                        ctrCatEdit = LoadControl("EditionTypeList.ascx");
                        plcMain.Controls.Add(ctrCatEdit);
                        break;
                    default:
                        ctrCatEdit = LoadControl("GenerateTabs.ascx");
                        plcMain.Controls.Add(ctrCatEdit);
                        break;
                }
            }
        }
    }
}