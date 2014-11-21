using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.CoreBO;

namespace DFISYS.GUI.EditoralOffice.MainOffce.GiaoLuu
{
    public partial class DieuPhoiCauHoi : System.Web.UI.UserControl
    {
        Question question = new Question();
        private int pageSize = 40;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idCourse = DFISYS.CoreBO.Common.function.Obj2Int(Request.QueryString["courseID"]);
                BindPage(idCourse);
                List<Question> tbl = question.SelectAllSearch(pageSize, Convert.ToInt32(ddlPageUp.SelectedValue), txtKeyword.Text, idCourse);
                grdListNews.DataSource = tbl;
                grdListNews.DataBind();
            }
        }

        private void BindPage(int id)
        {
            ddlPageUp.Items.Clear();
            int totalPage = (question.SelectAllCount(txtKeyword.Text, id) - 1) / pageSize + 1;
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
                int idCourse = DFISYS.CoreBO.Common.function.Obj2Int(Request.QueryString["courseID"]);
                Question data = e.Row.DataItem as Question;
                DropDownList cboUser = e.Row.FindControl("cboUser") as DropDownList;

                ChannelResponse obj = new ChannelResponse();
                cboUser.DataTextField = "ChannelResponse_NameManager";
                cboUser.DataValueField = "ChannelResponse_ID";
                cboUser.DataSource = obj.SelectAllBySourseIDActive(idCourse);
                cboUser.DataBind();

                var item = cboUser.Items.FindByValue(data.Channel_ID.ToString());//.Selected = true;
                if (item != null)
                    item.Selected = true;

            }

        }
    }
}