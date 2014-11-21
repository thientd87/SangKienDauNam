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
using Portal.BO.Editoral.Newslist;
using Portal.Core.DAL;

namespace Portal.GUI.EditoralOffice.MainOffce.Newslist
{
    public partial class ShowCommentReturn : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String News_ID = Request.QueryString["News_ID"];
                ActionRow objActionRow = NewslistHelper.getLastestAction(Convert.ToInt64(News_ID));
                txtComment_Title.Text = objActionRow.Comment_Title;
                txtComment_Content.Text = objActionRow.Content;                
            }
        }
    }
}