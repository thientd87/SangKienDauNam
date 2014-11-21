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
using DFISYS.User.Security;
using DFISYS.BO.Editoral.Newslist;

namespace DFISYS.GUI.EditoralOffice.MainOffce.OnLoad.UserControl
{
    public partial class SumNews : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                 MainSecurity objSecu = new MainSecurity();
                 Role objRole = objSecu.GetRole(Page.User.Identity.Name, DFISYS.API.Config.CurrentChannel);
                 Permission objPermission = objSecu.GetPermission(Page.User.Identity.Name);

                 

                 // Check permission doi voi tung loai thong ke
                 if (objPermission.isXuat_Ban_Bai)
                 {
                     ltrSumNewsPublished.Text = NewslistHelper.GetCountNews("publishedlist",false).ToString();
                     hplPublished.NavigateUrl = "/office/publishedlist.aspx";
                 }
                 else
                 {
                     hplPublished.Visible = false;
                 }

                 if (objPermission.isDuyet_Bai)
                 {
                     ltrSumNewsWaitingApprove.Text = NewslistHelper.GetCountNews("approvalwaitlist", false).ToString();
                     hplNewsWaitingApprove.NavigateUrl = "/office/approvalwaitlist.aspx";
                 }
                 else
                 {
                     hplNewsWaitingApprove.Visible = false;
                 }


                 if (objPermission.isBien_Tap_Bai)
                 {
                     ltrSumNewsWaitingEdit.Text = NewslistHelper.GetCountNews("editwaitlist", false).ToString();
                     hplNewsWaitingEdit.NavigateUrl = "/office/editwaitlist.aspx";
                 }
                 else
                 {
                     hplNewsWaitingEdit.Visible = false;
                 }

                 
            }
        }
    }
}