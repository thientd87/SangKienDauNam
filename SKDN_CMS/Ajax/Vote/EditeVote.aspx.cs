using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.Editoral.Category;

namespace DFISYS.Ajax.Vote
{
    public partial class EditeVote : System.Web.UI.Page
    {
        public int voteID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.Page.User.Identity.Name)) return;
            if (!IsPostBack)
            {
                var objVote = new BO.CoreBO.Vote();
                var objVoteItem = new BO.CoreBO.Vote_Item();


                CategoryHelper.Treebuild(ddlChuyenmuc);
                if (Request.QueryString["voteId"] != null 
                    && Request.QueryString["action"] != null 
                    && Request.QueryString["action"].ToString().ToLower().Equals("delete"))
                {
                    voteID = Convert.ToInt32(Request.QueryString["voteId"]);
                    objVote.Vote_ID = voteID;
                    objVote.Delete();
                    return;
                }
              
                if (Request.QueryString["voteId"] != null && Request.QueryString["post"] == null)
                {
                    voteID = Convert.ToInt32(Request.QueryString["voteId"]);
                    List<BO.CoreBO.Vote_Item> lsitem = new List<BO.CoreBO.Vote_Item>();
                    lsitem = objVoteItem.SelectAllByVoteID(voteID);
                    grdListVote.DataSource = lsitem;
                    grdListVote.DataBind();


                    btnSave.OnClientClick = String.Format("Save({0}, {1})", voteID, "false");
                    btnDelete.OnClientClick = String.Format("return Delete({0})", voteID);

                    objVote.Vote_ID = voteID;
                    var cmdItem = objVote.SelectOne();
                    if (cmdItem != null)
                    {
                        txtName.Text = cmdItem.Vote_Title;
                        txtContent.Text = cmdItem.Vote_InitContent;
                        txtFromDate.Text = cmdItem.Vote_StartDate.ToString("dd/MM/yyy");
                        txtToDate.Text = cmdItem.Vote_EndDate.ToString("dd/MM/yyy");
                        chkActive.Checked = cmdItem.isActive;
                        this.ddlChuyenmuc.Items.FindByValue(cmdItem.Cat_ID.ToString()).Selected = true;
                    }
                    return;
                }
                if ((Request.QueryString["action"] != null) && (Request.QueryString["action"].Equals("deleteitem")))
                {
                    try
                    {
                        objVoteItem.VoteIt_ID = Convert.ToInt32(Request.QueryString["voteitemId"]);
                        objVoteItem.Delete();
                    }
                    catch (Exception)
                    {
                    }
                    return;
                }
                if (Request.Form.Count > 0)
                {

                  
                    if (Request.QueryString["action"] != null
                         && Request.QueryString["action"].ToString().ToLower().Equals("insertvoteitem"))
                    {
                        //insert 
                        voteID = Convert.ToInt32(Request.QueryString["voteID"]);
                        objVoteItem.Vote_ID = voteID;
                        int stt = 0;
                        try
                        {
                            stt = Convert.ToInt32(Request.QueryString["stt"]);
                        }
                        catch (Exception)
                        {
                        } 
                        objVoteItem.VoteIt_STT = stt;
                        objVoteItem.VoteIt_Content = Request.QueryString["content"];
                        objVoteItem.VoteIt_Rate = 0;
                        objVoteItem.Insert();
                        return;
                    }

                    if (Request.QueryString["voteId"] != null)
                    {
                        voteID = Convert.ToInt32(Request.QueryString["voteId"]);
                        objVote.Vote_ID = voteID;
                        var cmdItem = objVote.SelectOne();
                        DateTime fromDate = DateTime.Now;
                        DateTime toDate = DateTime.Now;
                        fromDate = new DateTime(    //24/05/2012
                          Convert.ToInt16(Request.Form["txtFromDate"].ToString().Substring(6)),
                            Convert.ToInt16(Request.Form["txtFromDate"].ToString().Substring(3, 2)),
                            Convert.ToInt16(Request.Form["txtFromDate"].ToString().Substring(0, 2)));
                        toDate = new DateTime(    //24/05/2012
                          Convert.ToInt16(Request.Form["txtToDate"].ToString().Substring(6)),
                            Convert.ToInt16(Request.Form["txtToDate"].ToString().Substring(3, 2)),
                            Convert.ToInt16(Request.Form["txtToDate"].ToString().Substring(0, 2)));
                        objVote.Vote_StartDate = fromDate;
                        objVote.Vote_EndDate = toDate;
                        objVote.Vote_Title = Request.Form["txtName"].ToString();
                        objVote.Cat_ID = Convert.ToInt32(Request.Form["ddlChuyenmuc"].ToString());
                        objVote.Vote_InitContent = Request.Form["txtContent"].ToString();
                        Boolean active = false;
                        try
                        {
                            if (Request.Form["chkActive"].ToString() == "on")
                                active = true;
                        }
                        catch (Exception)
                        {
                        }

                        objVote.isActive = active;
                        if (objVote.Vote_ID > 0)
                        {
                            objVote.Update();
                        }
                        else
                        {
                            objVote.Insert();
                        }
                        return;
                    }
                    if (Request.QueryString["voteItemId"] != null)
                    {
                        voteID = Convert.ToInt32(Request.QueryString["status"]);
                        string voteItID = Request.QueryString["voteItemId"].ToString();
                        try
                        {
                            objVoteItem.VoteIt_ID = Convert.ToInt32(voteItID);
                            objVoteItem.SelectOne();
                            objVoteItem.Vote_ID = voteID;
                            int stt = 0;
                            try
                            {
                                stt = Convert.ToInt32(Request.QueryString["stt"]);
                            }
                            catch (Exception)
                            {
                            } 
                            objVoteItem.VoteIt_STT = stt;
                            objVoteItem.VoteIt_Content = Request.QueryString["content"];
                            objVoteItem.Update();
                        }
                        catch (Exception)
                        {
                          
                        }
                        
                    }

                    //webLink.WebLink_Description = Request.Form["txtContent"].ToString();
                    //webLink.WebLink_Name = Request.Form["txtName"].ToString();
                    //webLink.WebLink_URL = Request.Form["txtURL"].ToString();
                    //int stt = 0;
                    //try
                    //{
                    //    stt = int.Parse(Request.Form["txtSTT"].ToString());
                    //}
                    //catch (Exception)
                    //{
                    //}
                    //webLink.WebLink_Order = stt;
                    //if (webLinkID > 0)
                    //{
                    //    webLink.Update();
                    //}
                    //else
                    //{
                    //    webLink.Insert();
                    //}
                }
            }
        }
    }
}