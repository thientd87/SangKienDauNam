using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DFISYS.GUI.EditoralOffice.MainOffce.Votes
{
    public partial class VoteItem : System.Web.UI.UserControl
    {
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                grdThreadList.Columns[0].Visible = false;
            }
        }
        #endregion
        

        protected void grdThreadList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "NewVoteItem".ToLower())
            {
                GridViewRow grdrow = this.grdThreadList.FooterRow;
                if (grdrow != null)
                {
                    TextBox txtVoteItem = grdrow.FindControl("txtVoteItem") as TextBox;
                    TextBox txtVoteRank = grdrow.FindControl("txtVoteRank") as TextBox;
                    if (txtVoteItem != null && txtVoteItem.Text != "")
                    {
                        objVoteItemSource.InsertParameters[1].DefaultValue = txtVoteItem.Text;
                        try
                        {
                            objVoteItemSource.InsertParameters[2].DefaultValue = txtVoteRank.Text;
                        }
                        catch {
                            objVoteItemSource.InsertParameters[2].DefaultValue = 0.ToString();
                        }
                        objVoteItemSource.Insert();

                        #region Log
                        //LogRow logRow = new LogRow();
                        //LogHelper logHelper = new LogHelper();
                        //String[] param = { txtVoteItem.Text, logHelper.GetVoteTitle(Convert.ToInt32(Request.QueryString["Vote"])) };                        
                        //logRow.Action = LogListName.VOTE + String.Format(Log.THEM_CAUHOI_VOTE, param);
                        //logRow.CreatedDate = DateTime.Now;
                        //logRow.News_ID = 0; logRow.Object_ID = 0;
                        //logRow.Type = (int)LogType.Vote;
                        //logRow.UserName = HttpContext.Current.User.Identity.Name;
                        //logHelper.InsertFlowEvent(logRow);
                        #endregion
                    }
                }
                //Response.Redirect("/voteitem.aspx?vote=" + Request.QueryString["vote"]);
            }
           
        }
        protected void grdThreadList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grdThreadList.EditIndex =-1;
        }

        protected void grdThreadList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow grdrow = this.grdThreadList.Rows[e.RowIndex];
            if (grdrow != null)
            {
                TextBox txtEditVoteItem = grdrow.FindControl("txtEditVoteItem") as TextBox;
                TextBox txtVoteRank = grdrow.FindControl("txtEditVoteRank") as TextBox;
                ImageButton btnSave = grdrow.FindControl("imgSave") as ImageButton;
                if (btnSave != null)
                {
                    objVoteItemSource.UpdateParameters[0].DefaultValue = btnSave.CommandArgument;
                    objVoteItemSource.UpdateParameters[1].DefaultValue = txtEditVoteItem.Text;
                    try
                    {
                        objVoteItemSource.UpdateParameters[2].DefaultValue = txtVoteRank.Text;
                    }
                    catch
                    {
                        objVoteItemSource.UpdateParameters[2].DefaultValue = 0.ToString();
                    }
                    objVoteItemSource.Update();

                    #region Log
                    //LogRow logRow = new LogRow();
                    //LogHelper logHelper = new LogHelper();
                    //String[] param = { txtEditVoteItem.Text, logHelper.GetVoteTitle(Convert.ToInt32(Convert.ToInt32(Request.QueryString["Vote"]))) };
                    //logRow.Action = LogListName.VOTE + String.Format(Log.SUA_CAUHOI_VOTE, param);
                    //logRow.CreatedDate = DateTime.Now;
                    //logRow.News_ID = 0; logRow.Object_ID = 0;
                    //logRow.Type = (int)LogType.Vote;
                    //logRow.UserName = HttpContext.Current.User.Identity.Name;
                    //logHelper.InsertFlowEvent(logRow);
                    #endregion
                }
            }
           // Response.Redirect("/voteitem.aspx?vote=" + Request.QueryString["vote"]);
        }

        protected void grdThreadList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdThreadList.EditIndex = e.NewEditIndex;
        }

        protected void grdThreadList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
             GridViewRow grdrow = this.grdThreadList.Rows[e.RowIndex];
             if (grdrow != null)
             {
                 ImageButton btnSave = grdrow.FindControl("imgSave") as ImageButton;
                 if (btnSave == null)
                     btnSave = grdrow.FindControl("imgDel") as ImageButton;
                 TextBox txtEditVoteItem = grdrow.FindControl("txtEditVoteItem") as TextBox;
                 if (btnSave != null)
                 {
                     objVoteItemSource.DeleteParameters[0].DefaultValue = btnSave.CommandArgument;
                     objVoteItemSource.Delete();
                     #region Log
                     //LogRow logRow = new LogRow();
                     //LogHelper logHelper = new LogHelper();
                     //String[] param = { txtEditVoteItem != null ? txtEditVoteItem.Text : String.Empty, logHelper.GetVoteTitle(Convert.ToInt32(Request.QueryString["Vote"])) };
                     //logRow.Action = LogListName.VOTE + String.Format(Log.XOA_CAUHOI_VOTE, param);
                     //logRow.CreatedDate = DateTime.Now;
                     //logRow.News_ID = 0; logRow.Object_ID = 0;
                     //logRow.Type = (int)LogType.Vote;
                     //logRow.UserName = HttpContext.Current.User.Identity.Name;
                     //logHelper.InsertFlowEvent(logRow);
                     #endregion
                 }
             }
        }
    }
}