using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.CoreBO;
using System.Data;

namespace DFISYS.GUI.EditoralOffice.MainOffce.GiaoLuu
{
    public partial class ListCourse : System.Web.UI.UserControl
    {
        Course course = new Course();
        private int pageSize = 40;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPage();
                List<Course> tbl = course.SelectAllSearch(pageSize, Convert.ToInt32(ddlPageUp.SelectedValue), txtKeyword.Text);
                grdListNews.DataSource = tbl;
                grdListNews.DataBind();
            }
        }

        private void BindPage()
        {
            ddlPageUp.Items.Clear();
            int totalPage = (course.SelectAllCount(txtKeyword.Text) - 1) / pageSize + 1;
            for (int i = 1; i <= totalPage; i++)
            {
                ddlPageUp.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

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


            //grdListNews.DataSource = course.GetAll(40, Convert.ToInt32(ddlPageUp.SelectedValue), 0, Convert.ToInt32(ddlChuyenmuc.SelectedValue), txtKeyword.Text);
            //grdListNews.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //grdListNews.DataSource = course.GetAll(40, Convert.ToInt32(ddlPageUp.SelectedValue), 0, Convert.ToInt32(ddlChuyenmuc.SelectedValue), txtKeyword.Text);
            //grdListNews.DataBind();

            //BindPage();
        }

        protected void grdListNews_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Course data = e.Row.DataItem as Course;
                DropDownList cboUser = e.Row.FindControl("cboIsHot") as DropDownList;
                var item = cboUser.Items.FindByValue(data.Status.ToString());//.Selected = true;
                if (item != null)
                    item.Selected = true;

            }
        }

    }
}