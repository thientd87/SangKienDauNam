using System;
using System.Web;
using System.Security.Principal;
using System.Data;
using DFISYS.Ultility;
using DFISYS.User.Security;
using System.IO;
using System.IO.Compression;
using System.Configuration;

namespace DFISYS {
    /// <summary>
    /// Summary description for Global.
    /// </summary>
    public class Global : System.Web.HttpApplication {
        public Global() {
            InitializeComponent();
        }
        /// <summary>
        /// Thuc hien Install mot so file cau hinh neu he thong khong co.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Start(Object sender, EventArgs e) {
            //Indexer indexer = new Indexer();
        }

        protected void Application_BeginRequest(Object sender, EventArgs e) {
            ////###### begin use compress ###################
            //HttpApplication app = (HttpApplication)sender;
            //string acceptEncoding = app.Request.Headers["Accept-Encoding"];
            //Stream prevUncompressedStream = app.Response.Filter;

            //if (acceptEncoding == null || acceptEncoding.Length == 0)
            //    return;

            //acceptEncoding = acceptEncoding.ToLower();
            //string _physicalPath = app.Request.Url.LocalPath;
            //if (_physicalPath.Contains(".aspx") || _physicalPath.Contains(".aspx"))
            //{
            //    if (acceptEncoding.Contains("gzip"))
            //    {
            //        // gzip
            //        app.Response.Filter = new GZipStream(prevUncompressedStream, CompressionMode.Compress);
            //        app.Response.AppendHeader("Content-Encoding", "gzip");
            //    }
            //    else if (acceptEncoding.Contains("deflate"))
            //    {
            //        // defalte
            //        app.Response.Filter = new DeflateStream(prevUncompressedStream, CompressionMode.Compress);
            //        app.Response.AppendHeader("Content-Encoding", "deflate");
            //    }
            //    //app.Response.Cache.SetExpires(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, (DateTime.Now.Minute + 10), 30));
            //}
            ////########### end use compress ########################
        }

        protected void Application_Error(Object sender, EventArgs e) {
            Exception ex = Server.GetLastError().GetBaseException();
            object WriteErrorToFile = ConfigurationManager.AppSettings["WriteErrorToFile"];
            if (WriteErrorToFile != null && WriteErrorToFile.ToString().ToUpper() == "TRUE") {
                CreateLogFiles Err = new CreateLogFiles();
                Err.ErrorLog(Server.MapPath("/ErrorLog"), Request.Url.ToString() + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        protected void Session_Start(Object sender, EventArgs e) {
        }



        protected void Application_EndRequest(Object sender, EventArgs e) {

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e) {
            if (Request.IsAuthenticated) {
                string[] roles = null;
                MainSecurity objscu = new MainSecurity();
                roles = objscu.GetRoleSymbol(HttpContext.Current.User.Identity.Name, DFISYS.API.Config.CurrentChannel);

                //ChannelUsers objUser = new ChannelUsers();
                //string[] roles = objUser.getRoles(HttpContext.Current.User.Identity.Name);//UserManagement.GetRoles(HttpContext.Current.User.Identity.Name);
                HttpContext.Current.User = new GenericPrincipal(HttpContext.Current.User.Identity, roles);
            }
        }

        protected void Session_End(Object sender, EventArgs e) {
        }
        protected void Application_End(Object sender, EventArgs e) {

        }

        public override string GetVaryByCustomString(HttpContext context, string custom) {
            if (custom.ToLower() == "cachingmultiversion") {
                HttpCookie cookie = context.Request.Cookies["CachingMultiVersion"];
                if (cookie != null) return cookie.Value;
            }
            return base.GetVaryByCustomString(context, custom);
        }
        public static string CachingMultiVersion {
            set {
                System.Web.HttpContext.Current.Response.Cookies["CachingMultiVersion"].Value = value;
            }
        }

        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion
    }
}

