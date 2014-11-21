using System;
using System.Web.UI.WebControls;

namespace DFISYS.GUI.EditoralOffice.MainOffce.Votes
{
    public partial class VoteList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strcpmode = Request.QueryString["cpmode"].ToString();
            if (strcpmode.ToLower() != "listvote")
                objVoteSource.SelectParameters[0].DefaultValue = " UserID='" + Page.User.Identity.Name + "' ";
            else
                objVoteSource.SelectParameters[0].DefaultValue = "  ";

        }

        protected void grdVote_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "delete")
            {
                objVoteSource.DeleteParameters[0].DefaultValue = e.CommandArgument.ToString();

                #region Log
                //LogHelper logHelper = new LogHelper();
                //String VoteTitle = logHelper.GetVoteTitle(Convert.ToInt32(e.CommandArgument));
                //String action = LogListName.VOTE + String.Format(Log.XOA_VOTE, VoteTitle);
                //LogRow logRow = new LogRow();
                //logRow.UserName = HttpContext.Current.User.Identity.Name;
                //logRow.Type = (int)LogType.Vote;
                //logRow.Object_ID = 0; logRow.News_ID = 0;
                //logRow.CreatedDate = DateTime.Now;
                //logRow.Action = action;
                //logHelper.InsertFlowEvent(logRow);
                #endregion

                objVoteSource.Delete();
                Response.Redirect("/office/votemngr.chn");
            }
            /*if (e.CommandName.ToLower() == "delete")
            {

                btnRelaNews.Attributes.Add("onclick", "openpreview('/portal/newsthread.aspx?relat=" + hdRelatNews.ClientID + "&news=" + Request.QueryString["NewsRef"] + "&cpmode=publishedlist',800,600);return false;");
            }*/
        }

        protected void lnkRealDel_Click(object sender, EventArgs e)
        {
            String value = objVoteSource.DeleteParameters[0].DefaultValue = getCheckedRow();
            if (value.Equals(String.Empty))
            {
                Response.Redirect("/office/votemngr.chn");                
            }            
                        
            #region Log
            //String[] Vote_ID = value.Split(',');
            //int TotalDelete = Vote_ID.Length;
            //LogHelper logHelper = new LogHelper();
            //String VoteTitle = String.Empty;
            //LogRow logRow = new LogRow();

            //for (int i = 0; i < TotalDelete; i++)
            //{
            //    VoteTitle = logHelper.GetVoteTitle(Convert.ToInt32(Vote_ID[i]));
            //    String action = LogListName.VOTE + String.Format(Log.XOA_VOTE, VoteTitle);                
            //    logRow.UserName = HttpContext.Current.User.Identity.Name;
            //    logRow.Type = (int)LogType.Vote;
            //    logRow.Object_ID = 0; logRow.News_ID = 0;
            //    logRow.CreatedDate = DateTime.Now;
            //    logRow.Action = action;
            //    logHelper.InsertFlowEvent(logRow);
            //}
            #endregion

            objVoteSource.Delete();
            Response.Redirect("/office/votemngr.chn");
        }
        private string getCheckedRow()
        {
            string strDel = "";
            foreach (GridViewRow grdRow in this.grdVote.Rows)
            {
                if (grdRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkSel = (CheckBox)grdRow.Cells[0].FindControl("chkSelect");
                    if (chkSel.Checked)
                    {
                        ImageButton btnItem = (ImageButton)grdRow.Cells[5].FindControl("btnDelete");
                        if (strDel != "")
                            strDel += ",";
                        strDel += btnItem.CommandArgument;
                    }
                }
            }
            return strDel;
        }

        protected void lnkAddNews_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/office/voteadd.chn");
        }

        protected void cboPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdVote.PageIndex = Convert.ToInt32((string) cboPage.SelectedValue);
        }

        //protected void grdVote_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType != DataControlRowType.Header && e.Row.RowType != DataControlRowType.Footer)
        //    {
        //        Literal ltrStart = e.Row.FindControl("ltrStart") as Literal;
        //        Literal ltrStart = e.Row.FindControl("ltrEnd") as Literal;
        //        if (ltrStart != null)
        //        {
        //            ltrStart.Text =
        //        }
        //    }
        //}
    }
}