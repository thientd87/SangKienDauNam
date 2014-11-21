using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO;
using DFISYS.BO.CoreBO;
using System.Data;

namespace DFISYS.GUI.EditoralOffice.MainOffce.Tool
{
    public partial class ListSupportOnine : System.Web.UI.UserControl
    {
        SupportOnline_Helper weblink = new SupportOnline_Helper();
        private int pageSize = 400;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindPage();
               // List<WebLink> tbl = SupportOnline_Helper.SelectSupportOnline();
                var tbl = SupportOnline_Helper.SelectSupportOnline();
                grdListSupport.DataSource = tbl;
                grdListSupport.DataBind();
            }
        }
        private void BindPage()
        {
            //ddlPageUp.Items.Clear();
            //int totalPage = (comment.Count(0, Convert.ToInt32(ddlChuyenmuc.SelectedValue), txtKeyword.Text) - 1) / 40 + 1;
            //for (int i = 1; i <= totalPage; i++)
            //{
            //    ddlPageUp.Items.Add(new ListItem(i.ToString(), i.ToString()));
            //}

        }
        protected void grdListNews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdListSupport.Rows[index];
                grdListSupport.EditIndex = index;
                var txtContent = row.FindControl("txtContent") as TextBox;
                if (txtContent != null) txtContent.Text = row.Cells[2].Text;
            }

        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            //grdListNews.DataSource = comment.GetAll(40, Convert.ToInt32(ddlPageUp.SelectedValue), 0, Convert.ToInt32(ddlChuyenmuc.SelectedValue), txtKeyword.Text);
            //grdListNews.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var tbl = SupportOnline_Helper.SelectSupportOnline();
            grdListSupport.DataSource = tbl;
            grdListSupport.DataBind();

            //BindPage();
        }         

    }
}