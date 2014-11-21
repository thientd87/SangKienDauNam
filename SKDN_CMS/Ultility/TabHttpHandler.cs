using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.SessionState;
using DFISYS.API;
using System.Web.UI;
using Cache = DFISYS.SiteSystem.Cache;
namespace DFISYS.Ultility {
    /// <summary>
    /// Lớp thực hiện nhận các yêu cầu đến các tệp có đuôi được quy định trong CustomExtension
    /// và chuyển hướng các yêu cầu đến trang chứa Tab tương ứng.
    /// </summary>
    public class TabHttpHandler : IHttpHandler, IRequiresSessionState {
        /// <summary>
        /// Hàm xử lý Request đến các trang có đuôi quy định trong CustomExtension
        /// </summary>
        /// <param name="context">Biến Context chứa Request</param>
        public void ProcessRequest(HttpContext context) {
            if (context.Request.Url.AbsolutePath.ToLower().IndexOf("login.aspx") == -1) {
                if (!context.User.Identity.IsAuthenticated) context.Response.Redirect("/login.aspx");
            }
            context.Items["originalQuerystring"] = context.Request.QueryString.ToString();
            context.Items["originalPathInfo"] = context.Request.PathInfo;

            string url = "";

            HandlerServerRequest(context, ref url);
            string[] strQueries = url.Split("?".ToCharArray());
            string page = strQueries[0];
            string strQuery = "";
            if (strQueries.Length > 1)
                strQuery = strQueries[1];
            context.RewritePath(context.Request.Path, string.Empty, strQuery);
            Page hand = (Page)PageParser.GetCompiledPageInstance(page, context.Server.MapPath(page), context);
            hand.PreRenderComplete += new EventHandler(hand_PreRenderComplete);
            hand.ProcessRequest(context);
        }

        protected void hand_PreRenderComplete(object sender, EventArgs e) {
            HttpContext.Current.RewritePath(HttpContext.Current.Request.Path,
            HttpContext.Current.Items["originalPathInfo"].ToString(),
            HttpContext.Current.Items["originalQuerystring"].ToString());
        }

        private void HandlerServerRequest(HttpContext context, ref string url) {
            Cache cache = new Cache(HttpContext.Current.Application);
            string strRq = context.Request.Url.AbsolutePath;
            string currentUser = context.User.Identity.Name;

            string path = context.Request.Url.AbsolutePath.ToLower();

            // Kiểm tra Cache xem đường dẫn này đã xử lý lần nào chưa - so voi ca duong dan that

            string newsRef = "", templistRef = "", newstemp = "", multiref = "";//dung trong cac truong hop:Co so, lay cpmode, template, bien k so
            string[] newsParams;
            //cắt ký tự đầu "/"
            string tabRef = path.Substring(path.IndexOf("/") + 1); //path.Substring(path.LastIndexOf("/") + 1); Replace(strRoot, "");// get "TabRef.ext"
            //cắt ký tự cuối ".aspx"
            tabRef = tabRef.Substring(0, tabRef.LastIndexOf(".aspx")); // get "TabRef"

            // Kiểm tra để tách lọc phần mã tham chiếu của tin - Them co che do tin newsRef va luu url vao cache de khoi ton tai nguyen.
            if (tabRef != "") {
                //mảng các thành phần url
                string[] _arrReferenceParts = tabRef.Split("/".ToCharArray());

                //truong hop khi thong so cua tabref nhanh duoc co tu 2 phan tro len thi:
                //1. xac dinh xem thanh phan cuoi co phai ID cua tin khong? Neu phai thi day la dang tab --> News (Co the dat them dau hieu de nhan dien news
                //2. Neu khong phai la ID cua tin thi co 2 truong hop say ra 1- la tabref luon , 2- la mot loai query moi

                //nếu là tin
                if ((_arrReferenceParts.Length >= 2 && StringProcess.IsNumber(_arrReferenceParts[_arrReferenceParts.Length - 1])) || (_arrReferenceParts.Length >= 2 && _arrReferenceParts[_arrReferenceParts.Length - 1].IndexOf("_tm,") > 0)) {
                    string[] str = { "_tm," };
                    //mảng tách bởi chỗi _tm,
                    newsParams = _arrReferenceParts[_arrReferenceParts.Length - 1].Split(str, StringSplitOptions.None);

                    newsRef = newsParams[0];
                    if (newsParams.Length > 0) {
                        //chuoi co dang home/parent/child/newsid_tm,3_title.aspx
                        if (_arrReferenceParts[_arrReferenceParts.Length - 1].IndexOf("_tm,") > 0) {
                            //newstemp co dang 3_title
                            newstemp = newsParams[newsParams.Length - 1].Substring(0, 1);
                        }

                        //lay phan con lai lam tabref :home/parent/child
                        tabRef = tabRef.Replace("/" + _arrReferenceParts[_arrReferenceParts.Length - 1], "");
                    }
                    _arrReferenceParts = String.Join("/", _arrReferenceParts, 0, _arrReferenceParts.Length - 1).Split("/".ToCharArray());

                    templistRef = _arrReferenceParts[_arrReferenceParts.Length - 1];
                    tabRef = String.Join("/", _arrReferenceParts, 0, _arrReferenceParts.Length - 1);

                } else {
                    //truong hop khong phai newsref
                    if (_arrReferenceParts.Length >= 2) {
                        if (_arrReferenceParts.Length == 2) {
                            templistRef = _arrReferenceParts[_arrReferenceParts.Length - 1];
                            tabRef = String.Join("/", _arrReferenceParts, 0, _arrReferenceParts.Length - 1);
                        } else {
                            templistRef = _arrReferenceParts[_arrReferenceParts.Length - 2];
                            newsRef = _arrReferenceParts[_arrReferenceParts.Length - 1];
                            if (newsRef.IndexOf(",") > -1) {
                                multiref = newsRef;
                                newsRef = "";
                            }

                            tabRef = String.Join("/", _arrReferenceParts, 0, _arrReferenceParts.Length - 2);
                        }
                    }
                }
            }

            Dictionary<string, string> r = new Dictionary<string, string>();
            foreach (string key in context.Request.QueryString.Keys) {
                r[key] = context.Request[key];
            }
            //kiem tra xem - neu truong hop tabref co bao nhieu / thi lui ra bay nhieu lan de tro vao dung muc
            int intSecNum = 0;
            string[] strSecs = tabRef.Split("/".ToCharArray());
            intSecNum = strSecs.Length;

            if (":chuyenmuc:users:office:adminportal:login:".IndexOf(":" + tabRef.ToLower() + ":") != -1)
                url = Config.GetMainPage(currentUser);
            else
                url = context.Request.Url.AbsolutePath.TrimStart('/');

            r["TabRef"] = tabRef.Replace("/", ".");
            r["NewsRef"] = newsRef;
            r["cpmode"] = templistRef;
            r["multiref"] = multiref;
            bool firstParam = true;
            foreach (KeyValuePair<string, string> e in r) {
                if (firstParam) {
                    url += "?";
                    firstParam = false;
                } else {
                    url += "&";
                }

                url += e.Key + "=" + e.Value;
            }
            url = "~/" + url;
        }

        public bool IsReusable {
            get {
                return true;
            }
        }

    }
}
