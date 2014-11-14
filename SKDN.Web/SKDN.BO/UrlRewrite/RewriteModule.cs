using System;
using System.Configuration;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

namespace BO.UrlRewrite
{
    public class RewriteModule : IHttpHandlerFactory, IRequiresSessionState {
        public IHttpHandler GetHandler(HttpContext context, string requestType, string url1, string pathTranslated) {

          

            if (HttpContext.Current.Request.Url.Query != String.Empty) {
                if (context.Request.Url.Query.Length > 0) {
                    context.Items["VirtualUrl"] = context.Request.Path + context.Request.Url.Query;
                }
            }
            if (context.Items["VirtualUrl"] == null) {
                context.Items["VirtualUrl"] = context.Request.Path;
            }

            string url = context.Request.Url.AbsolutePath;
            while (url.EndsWith("/") && !string.IsNullOrEmpty(url)) {
                url = url.Substring(0, url.Length - 1);
            }

            if (url == "/blank.aspx") {
                url = context.Request.Url.Query.ToLower();
                url = url.Replace("/:80", "");
                string host = "?404;" + ConfigurationManager.AppSettings["SITE_URL"].ToLower();
                url = url.Substring(url.IndexOf(host) + host.Length);
                if (!url.StartsWith("/")) url = "/" + url;
            }

            string rewrite = "";

            RewriteRules rewriteRules = RewriteRules.GetCurrentRewriteRules();
            rewrite = rewriteRules.GetMatchingRewrite(url);

            if (!string.IsNullOrEmpty(rewrite)) {
                if (context.Request.Url.AbsolutePath == "/blank.aspx") rewrite = rewrite.Replace("/:80", "");
                context.RewritePath("~" + rewrite + (context.Request.Url.Query.Length > 0 ? "?" + HttpContext.Current.Request.Url.Query.Replace("?", "") : ""));
            }
            else {
                if (context.Request.Url.AbsolutePath == "/blank.aspx") {
                    rewrite = "/error.aspx?status=404";
                }
                else {
                    rewrite = context.Request.Path + context.Request.Url.Query;
                }
            }

            string newFilePath = rewrite.IndexOf("?") > 0 ? rewrite.Substring(0, rewrite.IndexOf("?")) : rewrite;

            return PageParser.GetCompiledPageInstance(newFilePath, context.Server.MapPath(newFilePath), context);
        }

        public void ReleaseHandler(IHttpHandler handler) {
        }
    }
}