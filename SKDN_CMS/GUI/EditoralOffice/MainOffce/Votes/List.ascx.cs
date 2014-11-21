using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.CoreBO;

namespace DFISYS.GUI.EditoralOffice.MainOffce.Votes
{
    public partial class List : System.Web.UI.UserControl
    {
        Vote objVote = new Vote();

        private int pageSize = 400;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindPage();
                List<Vote> tbl = objVote.SelectAllSearch(pageSize, 1, txtKeyword.Text);
                grdListNews.DataSource = tbl;
                grdListNews.DataBind();
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
                GridViewRow row = grdListNews.Rows[index];
                grdListNews.EditIndex = index;
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
            List<Vote> tbl = objVote.SelectAllSearch(pageSize, 1, txtKeyword.Text);
            grdListNews.DataSource = tbl;
            grdListNews.DataBind();

            //BindPage();
        }
    }
}