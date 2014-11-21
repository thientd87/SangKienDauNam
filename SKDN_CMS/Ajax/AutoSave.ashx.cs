using System;
using System.Collections.Generic;
using System.Web;
using DFISYS.BO.Editoral.Newsedit;
using DFISYS.Core.DAL;

namespace DFISYS.Ajax {
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    public class AutoSave : IHttpHandler {

        public void ProcessRequest(HttpContext context) {
            try {
                long NewsID = 0;
                Int64.TryParse(context.Request.Form["news_id"].ToString(), out NewsID);
                int CatID = 0;
                Int32.TryParse(context.Request.Form["cat_id"].ToString(), out CatID);

                //NewsEditHelper.AutoSave_Insert(NewsID, CatID, context.Request.Form["news_title"].ToString(), context.Request.Form["image"].ToString(), context.Request.Form["sapo"].ToString(), context.Request.Form["news_content"].ToString(), DateTime.Now);

                NewsRow objNewsRow = NewsEditHelper.GetNewsInfo_News(NewsID, false);
                if (objNewsRow == null)
                    NewsEditHelper.CreateNews(NewsID, CatID, string.Empty, context.Request.Form["news_title"].ToString(), context.Request.Form["image"].ToString(), context.Request.Form["news_title"].ToString(), context.Request.Form["sapo"].ToString(), context.Request.Form["news_content"].ToString(), string.Empty, false, 0/*lưu tạm*/, 0, string.Empty, string.Empty, string.Empty, DateTime.Now, false, false, 0, string.Empty, string.Empty, string.Empty, false, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty);
                else
                    if (objNewsRow.News_Status == 0)
                        NewsEditHelper.UpdateNews(NewsID, CatID, string.Empty, context.Request.Form["news_title"].ToString(), context.Request.Form["image"].ToString(), string.Empty, context.Request.Form["sapo"].ToString(), context.Request.Form["news_content"].ToString(), false, 0, 0, string.Empty, string.Empty, DateTime.Now, false, false, false, 0, string.Empty, string.Empty, string.Empty, string.Empty, false, string.Empty, string.Empty, string.Empty, 0, string.Empty, string.Empty);

                context.Response.Write("Lưu tự động lúc : " + DateTime.Now);
            }
            catch (Exception ex) {
                context.Response.Write(ex.Message);
            }
        }

        public bool IsReusable {
            get {
                return false;
            }
        }
    }
}
