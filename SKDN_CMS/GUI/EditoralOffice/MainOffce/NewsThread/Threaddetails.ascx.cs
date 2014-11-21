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
using Portal.Core.DAL;

namespace Portal.GUI.EditoralOffice.MainOffce.NewsThread
{
    public partial class Threaddetails : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["NewsRef"] != null)
            {
                objThreaddetailSource.SelectParameters[0].DefaultValue = "Thread_ID=" + Request.QueryString["NewsRef"];
                cboThread.SelectedValue = Request.QueryString["NewsRef"].ToString();
                lnkAddNews.Attributes.Add("onclick", "openpreview('/newsthread.aspx?thread=" + Request.QueryString["NewsRef"] + "&cpmode=publishedlist',800,600);return false;");

                //lblThreadTitle.Text = Thread
            }
        }

        protected void cboThread_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("/office/threaddetails/" + cboThread.SelectedValue + ".aspx");
        }

        protected void grdThreadDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "Delete".ToLower())
            {                   
                String ThreadDetail_ID = objThreaddetailSource.DeleteParameters[0].DefaultValue = e.CommandArgument.ToString();

               

                objThreaddetailSource.Delete();
               
            }
            Response.Redirect("/office/threaddetails/" + cboThread.SelectedValue + ".aspx");
        }
        private string getCheckedRow()
        {
            string strDel = "";
            foreach (GridViewRow grdRow in this.grdThreadDetails.Rows)
            {
                if (grdRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkSel = (CheckBox)grdRow.Cells[0].FindControl("chkSelect");
                    if (chkSel.Checked)
                    {
                        ImageButton btnItem = (ImageButton)grdRow.Cells[3].FindControl("btnDelete");
                        if (strDel != "")
                            strDel += ",";
                        strDel += btnItem.CommandArgument;
                    }
                }
            }
            return strDel;
        }

        protected void lnkRealDel_Click(object sender, EventArgs e)
        {
            String value = objThreaddetailSource.DeleteParameters[0].DefaultValue = getCheckedRow();
            if (value == "") { return; }          

            objThreaddetailSource.Delete();
            Response.Redirect("/office/threaddetails/" + cboThread.SelectedValue + ".aspx");
        }
    }
}