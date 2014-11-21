using System;
using System.Data;
using DFISYS.BO.Editoral.Category;
using DFISYS.BO.Editoral.Vote;

namespace DFISYS.GUI.EditoralOffice.MainOffce.Votes {
    public partial class VoteEdit : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                CategoryHelper.Treebuild(cboCategory);
                cboCategory.Items[0].Text = "Trang chủ";

                objParentSource.Select();
                if (Request.QueryString["NewsRef"] != null && Request.QueryString["NewsRef"] != "") {
                    string strVoteID = Request.QueryString["NewsRef"].ToString();
                    BindVoteForm(strVoteID);
                }
                else {
                    txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }

            //imgStartDate.Attributes.Add("onclick", "popUpCalendar(this," + txtStartDate.ClientID + ", 'dd/mm/yyyy'); return false;");
            //imgEndDate.Attributes.Add("onclick", "popUpCalendar(this," + txtEndDate.ClientID + ", 'dd/mm/yyyy'); return false;");
        }

        private void BindVoteForm(string _vote_id) {
            VoteHelper objHelp = new VoteHelper();
            DataRow objvoterow = objHelp.getVoteRow(_vote_id);
            if (objvoterow != null) {
                txtVote.Text = objvoterow["Vote_Title"].ToString();
                txtStartDate.Text = Convert.ToDateTime(objvoterow["Vote_StartDate"]).ToString("dd/MM/yyyy");
                txtEndDate.Text = Convert.ToDateTime(objvoterow["Vote_EndDate"]).ToString("dd/MM/yyyy");
                txtAvatar.Text = objvoterow["Vote_Parent_Image"] != null ? objvoterow["Vote_Parent_Image"].ToString() : String.Empty;
                //if(objvoterow["Vote_Parent"]!=null)
                //    cboParent.SelectedValue = objvoterow["Vote_Parent"].ToString();
                //if(objvoterow["isShow"]!=null)
                //    chkIsShow.Checked =Convert.ToBoolean(objvoterow["isShow"]);
                if (objvoterow["Vote_InitContent"] != null)
                    txtNote.Text = objvoterow["Vote_InitContent"].ToString();

                cboCategory.SelectedValue = objvoterow["Cat_ID"] != DBNull.Value ? objvoterow["Cat_ID"].ToString() : "0";
            }
        }
        private bool Validate() {
            if (txtVote.Text.Trim().Equals(String.Empty)) {
                lblMessage.Visible = true;
                return false;
            }
            return true;
        }

        protected void txtSave_Click(object sender, EventArgs e) {
            String action = String.Empty;
            if (!Validate()) { return; }
            if (Request.QueryString["NewsRef"] != null && Request.QueryString["NewsRef"] != "") {
                objVoteSource.UpdateParameters[0].DefaultValue = Request.QueryString["NewsRef"].ToString();
                objVoteSource.Update();
                //Response.Redirect("~/office/votemngr.chn");
                //action = LogListName.VOTE + String.Format(Log.CAPNHAT_VOTE, txtVote.Text);
            }
            else {
                objVoteSource.InsertParameters[6].DefaultValue = Page.User.Identity.Name;
                objVoteSource.Insert();
                //action = LogListName.VOTE + String.Format(Log.THEM_VOTE, txtVote.Text);
            }
            //Log
            #region Log
            //LogRow logRow = new LogRow();
            //logRow.Action = action;
            //logRow.CreatedDate = DateTime.Now;
            //logRow.News_ID = 0; logRow.Object_ID = 0;
            //logRow.Type = (int)LogType.Vote;
            //logRow.UserName = HttpContext.Current.User.Identity.Name;
            //LogHelper logHelper = new LogHelper();
            //logHelper.InsertFlowEvent(logRow);
            #endregion
            Response.Redirect("~/office/votelist.aspx");
        }

        protected void txtCancel_Click(object sender, EventArgs e) {
            Response.Redirect("~/office/votelist.aspx");
        }
    }
}