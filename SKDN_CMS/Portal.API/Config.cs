using System;
using System.Web;

namespace DFISYS.API {
    /// <summary>
    /// Configuration Management Class
    /// </summary>
    public class Config {
        public const string AdminRoles = "ADMINISTRATOR";
        /// <summary>
        /// Quyen danh cho ban bien tap thuoc mot kenh nao do. Cho phep quan ly noi dung trong phan vi toa soan.
        /// </summary>
        public const string EditoralRoles = "EDITORALOFFICE";
        /// <summary>
        /// Quyen danh cho quan ly nguoi dung
        /// </summary>
        public const string UsermanageRoles = "USERMANAGEMENT";
        /// <summary>
        /// Nhom vai tro danh cho nguoi quan ly quang cao.
        /// </summary>
        public const string AdvsRoles = "ADVS";
        /// <summary>
        /// Signed in User
        /// </summary>
        public const string AnonymousRole = "FLATUSER";
        /// <summary>
        /// Not signed in User or Anonymous User
        /// </summary>
        public const string EveryoneRoles = "TATCA";
        /// <summary>
        /// Khai bao Channel hien tai la channel nao.
        /// </summary>
        /// 
        //private static int channel = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["CurrentChannel"]);

        public static int CurrentChannel {
            get {
                return 1;
            }
        }

        /// <summary>
        /// Dinh nghia nhung loai query string co the duoc su dung
        /// </summary>
        /// <summary>
        /// Dinh nghia query dung cho danh sach tin luu ta
        /// </summary>
        /// 


        public const string cpmode = "";


        /// <summary>
        /// Renders the Portal with Frames or as a Table
        /// </summary>
        public enum PortalType {
            /// <summary>
            /// Renders the Portal as a Table
            /// </summary>
            Table = 0,
            /// <summary>
            /// Renders the Portal with Frames
            /// </summary>
            Frames = 1
        }

        public static string GetPhysicalRootFolder() {
            return HttpContext.Current.Server.MapPath("~/");
        }

        /// <summary>
        /// Returns the virtual Modules Path
        /// </summary>
        /// <returns>~/Modules/</returns>
        public static string GetModuleVirtualPath() {
            return "~/GUI/";// "~/Modules/";
        }
        /// <summary>
        /// Returns the physical Module Path. Uses the "GetModuleVirtualPath()" Method
        /// </summary>
        /// <returns>[Application Base Path]/Modules</returns>
        public static string GetModulePhysicalPath() {
            return HttpContext.Current.Server.MapPath(GetModuleVirtualPath());
        }
        /// <summary>
        /// Returns the virtual Path of a Module. Uses the "GetModuleVirtualPath()" method 
        /// </summary>
        /// <param name="ctrlType">Module Type</param>
        /// <returns>~/Module/[ctrlType]/</returns>
        public static string GetModuleVirtualPath(string ctrlType) {
            if (ctrlType.ToLower().IndexOf("front_end") >= 0) {
                // Neu la Front End, thi tro toi FrontEnd
                return System.Configuration.ConfigurationSettings.AppSettings["ControlFrontEnd"] + ctrlType + "/";
            }

            return GetModuleVirtualPath() + ctrlType + "/";
        }
        /// <summary>
        /// Returns the physical Path of a Module. Uses the "GetModuleVirtualPath()" method 
        /// </summary>
        /// <param name="ctrlType"></param>
        /// <returns></returns>
        public static string GetModulePhysicalPath(string ctrlType) {
            if (ctrlType.ToLower().IndexOf("front_end") >= 0) {
                // Neu la Front End, thi tro toi FrontEnd
                return System.Configuration.ConfigurationSettings.AppSettings["ControlFrontEnd"] + ctrlType + "/";
            }
            return HttpContext.Current.Server.MapPath(GetModuleVirtualPath(ctrlType));
        }

        public static string GetModuleLanguagePhysicalPath(string ctrlType, string language) {
            if (language == "")
                return GetModulePhysicalPath(ctrlType) + @"\Language.config";
            else
                return GetModulePhysicalPath(ctrlType) + @"\Language." + language + ".config";
        }

        /// <summary>
        /// Returns the physical path to the Portal Definition File
        /// </summary>
        /// <returns>[Application Base Path]/Portal.config</returns>
        public static string GetPortalDefinitionPhysicalPath() {
            return HttpContext.Current.Server.MapPath("~/Settings/Portal.config");
        }

        /// <summary>
        /// Returns the physical path to the Portal Definition File
        /// </summary>
        /// <returns>[Application Base Path]/Portal.config</returns>
        public static string GetTemplateTabDefinitionPhysicalPath() {
            return HttpContext.Current.Server.MapPath("~/Settings/TemplateTab.config");
        }

        /// <summary>
        /// Returns the physical path to the Portal Definition File
        /// </summary>
        /// <returns>[Application Base Path]/Portal.config</returns>
        public static string GetTemplateDefinitionPhysicalPath() {
            return HttpContext.Current.Server.MapPath("~/Settings/Templates.config");
        }

        /// <summary>
        /// Return cache key to determize which project is running
        /// </summary>
        /// <returns></returns>
        public static string GetPortalUniqueCacheKey() {
            return "UniqueCacheKey";
        }

        /// <summary>
        /// Returns the physical path to the Clone Columns Definition File
        /// </summary>
        /// <returns>[Application Base Path]/Clone.config</returns>
        public static string GetCloneColumnsDefinitionPhysicalPath() {
            return HttpContext.Current.Server.MapPath("~/Settings/Clone.config");
        }

        /// <summary>
        /// Returns the physical path to the User List File
        /// </summary>
        /// <returns>[Application Base Path]/Users.config</returns>
        public static string GetUserListPhysicalPath() {
            return HttpContext.Current.Server.MapPath("~/Users.config");
        }

        public static string GetLanguagePhysicalPath(string language) {
            if (language == "")
                return HttpContext.Current.Server.MapPath("~/Language.config");
            else
                return HttpContext.Current.Server.MapPath("~/Language." + language + ".config");
        }

        /// <summary>
        /// Returns the Main Render Page in dependent of the Portal Type
        /// </summary>
        /// <returns>RenderTable.aspx or RenderFrame.aspx</returns>
        public static string GetMainPage(string strUser) {
           // if (GetPortalType() == PortalType.Table) {
                return "/Pages/ManagementContent.aspx";
            //} 
        }

        public static string GetAppConfigValue(string _strConfigName) {
            return System.Configuration.ConfigurationSettings.AppSettings[_strConfigName];
        }

        public static string CustomExtension = ".aspx";

        public static string GetTabURL(string tabRef) {
            if (UseTabHttpModule) {
                return tabRef + ".aspx";
            } else {
                return GetMainPage(HttpContext.Current.User.Identity.Name) + "?TabRef=" + tabRef;
            }
        }
        /// <summary>
        /// Ham thuc hien chuyen luong den tab hien tai  dung trong truong hop
        /// module co su dung postback - khi xu ly su kien postbak xong thi dung ham nay
        /// de chuyen den dung trang hien tai.
        /// </summary>
        /// <param name="tabref">Tab hien tai dang dung</param>
        public static void RedirectToCurrentTab(string tabref) {
            tabref = tabref.Replace(".", "/");
            tabref = tabref + ".aspx";
            //xay dung lai url
            HttpContext.Current.Response.Redirect(tabref);
        }
        /// <summary>
        /// Returns the current Portal Type
        /// </summary>
        /// <returns>Table or Frames</returns>
        public static PortalType GetPortalType() {
            return (PortalType)Enum.Parse(typeof(PortalType),
                "Table",
                true);
        }

        /// <summary>
        /// Use the Tab HTTP Module. 
        /// The Tab ID will be passed as a "Page", not encoded in the URL
        /// </summary>
        public static bool UseTabHttpModule {
            get {
                try {
                    return true;
                } catch {
                    return false;
                }
            }
        }

        /// <summary>
        /// Show SubTabs in the Tab Menu
        /// </summary>
        public static bool TabMenuShowSubTabs {
            get {

                return false;

            }
        }

        /// <summary>
        /// Log the Requests URLReferrer Property a startup.
        /// </summary>
        public static bool LogUrlReferrer {
            get {
                try {
                    return true;
                } catch {
                    return false;
                }
            }
        }
        /// <summary>
        /// Displays Module Exceptions
        /// </summary>
        public static bool ShowModuleExceptions {
            get {
                try {
                    return true;
                } catch {
                    return false;
                }
            }
        }
    }
}
