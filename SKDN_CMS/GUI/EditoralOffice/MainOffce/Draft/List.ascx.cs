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

namespace Portal.GUI.EditoralOffice.MainOffce.Draft
{
    public partial class List : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Portal.BO.Editoral.Category.CategoryHelper.TreebuildAllCat(cboCategory);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void grdListNews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "editdraft")
            {
                Session["temp_id"] = e.CommandArgument.ToString();
                Response.Redirect("/office/add.aspx");
            }
            if (e.CommandName.ToLower() == "deletedraft")
            {
                //Session["temp_id"] = e.CommandArgument.ToString();
                //Response.Redirect("/office/add.aspx");
                Portal.BO.Editoral.Draft.DraftHelper.DeleteNewsTemp(e.CommandArgument.ToString());
                Response.Redirect("/office/list_draft.aspx");
            }
        }
    }
}