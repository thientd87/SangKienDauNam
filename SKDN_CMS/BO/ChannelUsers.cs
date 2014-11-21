using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Principal;
using System.Collections;
using DFISYS.User.Db;
using DFISYS.API;
using System.Web.Caching;
namespace DFISYS {
    public class ChannelUsers {
        private bool blnAuthenticate = false;

        public ChannelUsers() { }

        /// <summary>
        /// Ham thuc hien check quyen login cua mot user
        /// </summary>
        /// <param name="_userName">UserID su dung de dang nhap</param>
        /// <param name="_pwd">Mat khau dung de dang nhap</param>
        /// <returns>True nen xac thuc co quyen dang nhap, flase neu khong co quyen dang nhap</returns>
        public bool Login(string _userName, string _pwd) {
            bool blnResult = false;
            UserRow objrow;
            using (MainDB objDb = new MainDB()) {
                objrow = objDb.UserCollection.GetByPrimaryKey(_userName);
            }

            //Dung giai thuat ma hoa o day de kiem tra pwd
            if (objrow != null) {
                if (!objrow.IsUser_isActiveNull && objrow.User_isActive) {
                    string strPass = FormsAuthentication.HashPasswordForStoringInConfigFile(_pwd, "MD5");
                    if (objrow.User_Pwd.ToLower() == strPass.ToLower()) {
                        blnResult = true;
                        blnAuthenticate = true;
                        UpdateLastAccessTime(_userName);
                        UpdateUserHistory(_userName, strPass);
                        FormsAuthentication.SetAuthCookie(_userName, false);
                    }
                }
            }

            return blnResult;
        }

        public bool Login(string openID)
        {
            return false;
            //bool blnResult = false;
            //UserRow objrow;
            //using (MainDB objDb = new MainDB())
            //{
            //    objrow = objDb.UserCollection.GetByPrimaryKey(_userName);
            //}

            ////Dung giai thuat ma hoa o day de kiem tra pwd
            //if (objrow != null)
            //{
            //    if (!objrow.IsUser_isActiveNull && objrow.User_isActive)
            //    {
            //        string strPass = FormsAuthentication.HashPasswordForStoringInConfigFile(_pwd, "MD5");
            //        if (objrow.User_Pwd.ToLower() == strPass.ToLower())
            //        {
            //            blnResult = true;
            //            blnAuthenticate = true;
            //            UpdateLastAccessTime(_userName);
            //            UpdateUserHistory(_userName, strPass);
            //            FormsAuthentication.SetAuthCookie(_userName, false);
            //        }
            //    }
            //}

            //return blnResult;
        }

        private void UpdateLastAccessTime(string username) {
            using (MainDB objDb = new MainDB()) {
                objDb.StoreProcedure.UpdateLastAccessTime(username);
            }
        }

        private void UpdateUserHistory(string Username, string Password) {
            using (MainDB objDb = new MainDB()) {
                objDb.StoreProcedure.UpdateUserHistory(Username, Password);
            }
        }

        /// <summary>
        /// Cập nhật thời gian thay đổi pass lần cuối
        /// </summary>
        /// <param name="Username">Username</param>
        public static void UpdateLastChanged(string Username, string NewPassword) {
            using (MainDB objDb = new MainDB()) {
                objDb.StoreProcedure.UpdateLastChanged(Username, NewPassword);
            }
        }

        public static DataTable GetUserHistoryDetails(string Username){
            using (MainDB objDb = new MainDB()) {
                return objDb.StoreProcedure.GetUserHistoryDetails(Username);
            }
        }

        public bool isAuthenticate {
            get {
                return blnAuthenticate;
            }
        }

        /// <summary>
        /// Checks if a user has View Rights on a Tab or Module
        /// </summary>
        /// <param name="user">User Principal Object</param>
        /// <param name="roles">ArrayList with the users Roles</param>
        /// <returns>true if the user has View Rights</returns>
        public bool HasViewRights(IPrincipal user, ArrayList roles) {
            //trong truong hop khc, neu chuc nang dc xac dinh cho tat ca cung xem thi dc xem. Nguoc lai neu cho vai tro nao dc xem thi
            //chi vai tro do dc xem.
            string strCacheName;
            string strIsReturn;
            foreach (PortalDefinition.Role r in roles) {
                //#region check xem o da Cache User voi Roles nay hay chua

                //strCacheName = "HasViewRights" + user.Identity.Name + "_" + r.name + "_" + DFISYS.API.Config.CurrentChannel;
                //if (HttpContext.Current.Cache[strCacheName] != null)
                //{
                //    //try
                //    //{
                //        strIsReturn = HttpContext.Current.Cache[strCacheName].ToString();
                //        //neu co quyen thi tra ve gia tri luon
                //        if (strIsReturn.ToLower() == "true")
                //            return true;
                //        else if (strIsReturn.ToLower() == "false")
                //            continue;
                //    //}
                //    //catch { }
                //}

                //#endregion


                DFISYS.User.Security.MainSecurity objsecu = new DFISYS.User.Security.MainSecurity();
                //Neu la quyen tong bien tap & quyen admin cua kenh thi dc xem tat
                DFISYS.User.Security.Role objRole = objsecu.GetRole(user.Identity.Name, DFISYS.API.Config.CurrentChannel);

                if (objRole.isQuanTriKenh || objRole.isPhuTrachKenh || objRole.isTongBienTap || user.Identity.Name == "admin") {
                    //SaveCache(strCacheName, "true");
                    return true;
                }

                if (r.name == Config.EveryoneRoles) {
                    //SaveCache(strCacheName, "true");
                    return true;
                }
                if (user.IsInRole(r.name)) {
                    //SaveCache(strCacheName, "true");
                    return true;
                }

                //SaveCache(strCacheName, "false");
            }
            return false;
        }

        private void SaveCache(string strCacheName, string value) {
            HttpContext.Current.Cache.Add(strCacheName, value, null, DateTime.Now.AddHours(24), TimeSpan.Zero, CacheItemPriority.High, null);
        }

        /// <summary>
        /// Checks if a user has Edit Rights on a Tab or Module
        /// </summary>
        /// <param name="user">User Principal Object</param>
        /// <param name="roles">ArrayList with the users Roles</param>
        /// <returns>true if the user has Edit Rights</returns>
        public static bool HasEditRights(IPrincipal user, ArrayList roles) {
            if (user.IsInRole(Config.AdminRoles)) return true;

            foreach (PortalDefinition.Role r in roles) {
                PortalDefinition.EditRole er = r as PortalDefinition.EditRole;
                if (er != null) {
                    if (er.name == Config.EveryoneRoles) return true;
                    if (er.name == Config.AnonymousRole && !user.Identity.IsAuthenticated) return true;
                    //if (r.name == Config. && user.Identity.IsAuthenticated) return true;
                    if (user.IsInRole(er.name)) {
                        return true;
                    }
                }
            }

            return false;
        }

        public static string GetUserName() {

            string userName = HttpContext.Current.User.Identity.Name;

            if (string.IsNullOrEmpty(userName) && HttpContext.Current.Request.QueryString["username"] != null) userName = Crypto.DecryptFromHTML(HttpContext.Current.Request.QueryString["username"]);

            if (string.IsNullOrEmpty(userName) && HttpContext.Current.Request.Cookies["FileManager"] != null) {
                HttpCookie cookie = (HttpCookie)HttpContext.Current.Request.Cookies["FileManager"];
                userName = Crypto.DecryptByDay(cookie.Values["PW"]);
            }

            return userName;
        }
    }
}
