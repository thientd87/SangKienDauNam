using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.CoreBO;

namespace DFISYS.GUI.EditoralOffice.MainOffce.GiaoLuu
{
    public partial class DuyetNoiDung : System.Web.UI.UserControl
    {
        Question question = new Question();
        private int pageSize = 40;
        public int courseID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            courseID = DFISYS.CoreBO.Common.function.Obj2Int(Request.QueryString["courseID"]);
            if (!IsPostBack)
            {
                string user = this.Page.User.Identity.Name;
                BindPage(courseID, user);
                List<Question> tbl = question.SelectAllStatusSearch(pageSize, Convert.ToInt32(ddlPageUp.SelectedValue), txtKeyword.Text, courseID, QuestionStatus.DA_TRA_LOI);
                grdListNews.DataSource = tbl;
                grdListNews.DataBind();
            }
        }

        private void BindPage(int id, string user)
        {
            ddlPageUp.Items.Clear();
            int totalPage = (question.SelectAllStatusCount(txtKeyword.Text, id,QuestionStatus.DA_TRA_LOI) - 1) / pageSize + 1;
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
                ChannelResponse obj = new ChannelResponse();
                Literal userSay = e.Row.FindControl("userSay") as Literal;
                if (data.Question_Answer.Length > 0)
                {
                    obj.ChannelResponse_ID = data.Channel_ID;
                    obj = obj.SelectOne();
                    userSay.Text = string.Format("<b>{0}</b> ({1}) trả lời:", obj.UserID, obj.ChannelResponse_NameManager);// "<b>" + obj.UserID + "()" + "</b> Trả lời:";
                }

                //DropDownList cboUser = e.Row.FindControl("cboUser") as DropDownList;
                //cboUser.DataTextField = "ChannelResponse_NameManager";
                //cboUser.DataValueField = "ChannelResponse_ID";
                //cboUser.DataSource = obj.SelectAllBySourseIDActive(idCourse);
                //cboUser.DataBind();

                //var item = cboUser.Items.FindByValue(data.Channel_ID.ToString());//.Selected = true;
                //if (item != null)
                //    item.Selected = true;

                Literal status = e.Row.FindControl("ltStatus") as Literal;

                Literal sex = e.Row.FindControl("ltSex") as Literal;

                if (data.User_Sex == Sex.NAM)
                {
                    sex.Text = "Nam";
                }
                else
                {
                    sex.Text = "Nữ";
                }
                //switch (data.Status)
                //{
                //    case QuestionStatus.DA_TRA_LOI:
                //        status.Text = "<b><span style='color:red;'>Đã trả lời</span></b><br/>";
                //        break;
                //    case QuestionStatus.DA_DIEU_PHOI:
                //        status.Text = "<b><span style='color:red;'>Đã điều phối cho</span></b><br/>";
                //        break;
                //    case QuestionStatus.CHUA_DIEU_PHOI:
                //        status.Text = "<b><span style='color:red;'>Chưa điều phối</span></b><br/>";
                //        break;
                //}

            }
        }
    }
}