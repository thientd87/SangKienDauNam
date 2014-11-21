using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdvSearching.LCS;
using DFISYS.BO.Editoral.Newsedit;
using DFISYS.Core.DAL;

namespace DFISYS.Ajax
{
    public partial class DifferentOfContent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                long news_ID = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["nid"]))
                    news_ID = Convert.ToInt64(Request.QueryString["nid"]);
                if(news_ID>0)
                {
                    NewsRow objNewsRow = NewsEditHelper.GetNewsInfo_News(news_ID, false);
                    NewsRow objNewsRow_Ex = NewsEditHelper.GetNewsInfo_NewsExtension(news_ID, false);

                    string title = LCSFinder.GetEditor(objNewsRow.News_Title, objNewsRow_Ex.News_Title)[0];
                    string Sapo = LCSFinder.GetEditor(objNewsRow.News_InitialContent, objNewsRow_Ex.News_InitialContent)[0];
                    string Content = LCSFinder.GetEditor(objNewsRow.News_Content, objNewsRow_Ex.News_Content)[0];
                    ltrTitle.Text = title;
                    ltrInit.Text = Sapo;
                    ltrContent.Text = Content;
                }
            }
        }
    }
}