using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DFISYS.BO.Editoral.Newslist;

using System.Web.Caching;
using System.Xml;

namespace DFISYS.BO.Editoral.Menu {
    
    public class MenuCommon {
        static int cacheTime = 60;
        public static string formatUrlNewsMenu(object urlObj) {
            try {
                return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["HostAddress"]) + Convert.ToString(urlObj).Replace("~/", "/");
            }
            catch {
                return "";
            }
        }

        public static string getCssClass(object cpmode) {
            return (Convert.ToString(cpmode) == Convert.ToString(HttpContext.Current.Session["cpmode"]) ? "Menuleft_Item_Select" : "Menuleft_Item");
        }

        public static string getNewsCountStr(string cpMode,bool isClear) {
            string strCpMode = cpMode.ToString().ToLower();
            string cacheName = string.Empty, str = string.Empty;

            switch (strCpMode) {
                
                case "allnewstemplist":
                    cacheName = "allnewstemplist";
                    if (isClear) HttpContext.Current.Cache.Remove(cacheName);
                    str = HttpContext.Current.Cache.Get(cacheName) as string;
                    if (str == null) {

                        str = DFISYS.BO.Editoral.Newslist.NewslistHelper.News_GetAllNewsTemplistCount("").ToString();
                        //HttpContext.Current.Cache.Insert(cacheName, str, null, DateTime.Now.AddMinutes(cacheTime), Cache.NoSlidingExpiration);
                    }
                    break;
                case "approvalwaitspeciallist":
                    //cacheName = "approvalwaitspeciallist";
                    //if (isClear) HttpContext.Current.Cache.Remove(cacheName);
                    //str = HttpContext.Current.Cache.Get(cacheName) as string;
                    //if (str == null)
                    //{
                        str = DFISYS.BO.Editoral.Newslist.NewslistHelper.GetRowsCountOfNewsSpecialListControl(" News_status = 2 and IsUserRate =1 ").ToString();
                        //HttpContext.Current.Cache.Insert(cacheName, str, null, DateTime.Now.AddMinutes(cacheTime), Cache.NoSlidingExpiration);
                   // }
                    break;
                default:
                    str = NewslistHelper.GetCountNews(Convert.ToString(cpMode), isClear).ToString();
                    break;
            }
            return str.Equals("0") ? str : int.Parse(str).ToString("#,###");
        }


        public static XmlDocument getXML() {
            string cacheName = "AdminMenu";
            if (HttpContext.Current.Cache[cacheName] != null)
                return (XmlDocument)HttpContext.Current.Cache[cacheName];
            else {
                XmlDocument doc = new XmlDocument();
                doc.Load(HttpContext.Current.Server.MapPath("~/GUI/EditoralOffice/MainOffce/menu/menu.xml"));
                HttpContext.Current.Cache.Insert(cacheName, doc, new CacheDependency(HttpContext.Current.Server.MapPath("~/GUI/EditoralOffice/MainOffce/menu/menu.xml")));
                return doc;
            }
        }

        internal static XmlDocument getHeaderMenuXML() {
            string cacheName = "Header_AdminMenu";
            if (HttpContext.Current.Cache[cacheName] != null)
                return (XmlDocument)HttpContext.Current.Cache[cacheName];
            else {
                XmlDocument doc = new XmlDocument();
                doc.Load(HttpContext.Current.Server.MapPath("~/GUI/EditoralOffice/MainOffce/menu/menu_header.xml"));
                HttpContext.Current.Cache.Insert(cacheName, doc, new CacheDependency(HttpContext.Current.Server.MapPath("~/GUI/EditoralOffice/MainOffce/menu/menu_header.xml")));
                return doc;
            }
        }
    }
}
