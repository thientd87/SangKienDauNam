using System;
using System.Web;
using System.Web.UI;
using DFISYS.API;
using System.Web.UI.WebControls;
using DFISYS.Core.DAL;
using System.Data;
using System.Web.Caching;

namespace DFISYS {
    /// <summary>
    /// Collection of Helper Methods. For internal use only.
    /// </summary>
    internal class Helper {
        internal static Control GetEditControl(Page p) {
            PortalDefinition.Tab tab = PortalDefinition.GetCurrentTab();
            PortalDefinition.Module m = tab.GetModule(p.Request["ModuleRef"]);


            m.LoadModuleSettings();
            Module em = null;
            if (m.moduleSettings != null) {
                // Module Settings are present, use custom ascx Control
                em = (Module)p.LoadControl(Config.GetModuleVirtualPath(m.type) + m.moduleSettings.editCtrl);
            } else {
                // Use default ascx control (Edit[type].ascx)
                em = (Module)p.LoadControl(Config.GetModuleVirtualPath(m.type) + "Edit" + m.type + ".ascx");
            }

            // Initialize the control
            em.InitModule(
                tab.reference,
                m.reference,
                m.type,
                Config.GetModuleVirtualPath(m.type),
                true);

            return em;
        }

        internal static string GetEditLink(string ModuleRef) {
            if (Config.GetPortalType() == Config.PortalType.Table) {
                // Portal Type Table
                return "EditPageTable.aspx?ModuleRef=" + ModuleRef + "&TabRef=" + HttpContext.Current.Request["TabRef"];
            } else {
                // Portal Type Frame
                return "EditPageFrame.aspx?ModuleRef=" + ModuleRef + "&TabRef=" + HttpContext.Current.Request["TabRef"];
            }
        }

        internal static string GetEditModuleLink(string ModuleRef) {
            if (Config.GetPortalType() == Config.PortalType.Table) {
                // Portal Type Table
                return "EditModuleTable.aspx?ModuleRef=" + ModuleRef + "&TabRef=" + HttpContext.Current.Request["TabRef"];
            } else {
                // Portal Type Frame
                return "EditModuleFrame.aspx?ModuleRef=" + ModuleRef + "&TabRef=" + HttpContext.Current.Request["TabRef"];
            }
        }

        internal static string GetEditTabLink() {
            return GetEditTabLink(HttpContext.Current.Request["TabRef"]);
        }
        internal static string GetEditTabLink(string tabRef) {
            if (Config.GetPortalType() == Config.PortalType.Table) {
                // Portal Type Table
                return "EditTabTable.aspx?TabRef=" + tabRef;
            } else {
                // Portal Type Frame
                return "EditTabFrame.aspx?TabRef=" + tabRef;
            }
        }
        internal static string GetTabLink(string reference) {
            if (Config.GetPortalType() == Config.PortalType.Frames) {
                return "javascript:SelectTab('" + reference + "');";
            } else {
                return Config.GetTabURL(reference);
            }
        }

        /// <summary>
        /// Hàm xác định đang ở chế độ cho phép di chuyển Module hay không
        /// </summary>
        /// <returns></returns>
        public static bool IsAllowArrangeModules() {
            return HttpContext.Current.Session["AllowArrangeModules"] == null ? false : Convert.ToBoolean(HttpContext.Current.Session["AllowArrangeModules"]);
        }

        /// <summary>
        /// Hàm kiểm tra người dùng hiện thời có quyền cao nhất hay không 
        /// và có thể thực hiện kéo thả module hay không
        /// </summary>
        /// <returns>True: Đúng, False: Sai</returns>
        public static bool IsAdminMode() {
            return HttpContext.Current.User.IsInRole(DFISYS.API.Config.AdminRoles) && IsAllowArrangeModules();
        }

        public static void BindUser(DropDownList cboUser) {
            //cbo.Items.Clear();
            //using (MainDB db = new MainDB())
            //    cbo.DataSource = db.SelectQuery("select distinct News_Author from News order by News_Author");
            //cbo.DataBind();
            //cbo.Items.Insert(0, new ListItem("Tất cả", string.Empty));
            string cacheName = "cboUser";
            DataTable dt = HttpContext.Current.Cache.Get(cacheName) as DataTable;

            if (dt == null) {
                using (DFISYS.User.Db.MainDB db = new DFISYS.User.Db.MainDB()) {
                    dt = db.UserCollection.GetAsDataTable(" 1=1 ", " User_ID ");
                    HttpContext.Current.Cache.Insert(cacheName, dt, null, DateTime.Now.AddHours(1), Cache.NoSlidingExpiration);
                }
            }

            if (dt != null && dt.Rows.Count > 0) {
                cboUser.DataSource = dt;
                cboUser.DataValueField = "User_ID";
                cboUser.DataTextField = "User_ID";
                cboUser.DataBind();
                ListItem objItem = new ListItem("--- Tất cả ---", "-1");
                cboUser.Items.Insert(0, objItem);
            }
        }
     
    }
}
