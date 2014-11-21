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
using Portal.BO.Editoral.Draft;


namespace Portal.GUI.EditoralOffice.MainOffce.Draft
{
    public partial class SaveDraft : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string temp_id = Request.Form["temp_id"] != null ? Request.Form["temp_id"].ToString() : "-1";
                if (temp_id.Trim() == "") temp_id = "-1";
                string news_id = Request.Form["news_id"] != null ? Request.Form["news_id"].ToString() : "-1";
                if (news_id.Trim() == "") news_id = "-1";
                string cat_id = Request.Form["cat_id"] != null ? Request.Form["cat_id"].ToString() : "";
                string news_title = Request.Form["title"] != null ? Request.Form["title"].ToString() : "";
                string news_sapo = Request.Form["news_sapo"] != null ? Request.Form["news_sapo"].ToString() : "";
                string news_image = Request.Form["news_image"] != null ? Request.Form["news_image"].ToString() : "";
                string news_content = Request.Form["news_content"] != null ? Request.Form["news_content"].ToString() : "";


                if (!DraftHelper.CheckExistTempID(Convert.ToInt64(temp_id)))
                {
                    DraftHelper.InsertNewsTemp(Convert.ToInt64(temp_id), Convert.ToInt64(news_id), cat_id, news_title, news_image, news_sapo, news_content);
                }
                else
                {
                    DraftHelper.UpdateNewsTemp(Convert.ToInt64(temp_id), Convert.ToInt64(news_id), cat_id, news_title, news_image, news_sapo, news_content);
                }
            }
        }
    }
}