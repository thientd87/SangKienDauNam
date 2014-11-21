using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace DFISYS.GUI.Users.Common
{
    public class Const
    {
        //User
        public const string OBJECT_USER = "users";
        public const string OBJECT_USEREDIT = "useredit";
        public const string OBJECT_USERASSIGN = "userassign";
        
        //Quản lý default 
        public const string OBJECT_QUANLY_DEFAULT = "role_permission";
        
        //error
        public const string OBJECT_ERROR = "error";

        //Channel
        public const string OBJECT_CHANNEL = "channel";
        public const string OBJECT_CHANNELEDIT = "channeledit";
        
        //Category
        public const string OBJECT_CATEGRY = "category";
        public const string OBJECT_CATEGORYEDIT = "categoryedit";
        
        //Role
        public const string OBJECT_ROLE = "role";
        public const string OBJECT_ROLE_EDIT = "roleedit";
        
        //View in DB
        public const string VIEW_Permission_withCategory = "VIEW_PermissionWithCategory";
        public const string VIEW_Permission_withoutCategory = "VIEW_PermissionWithoutCategory";
        
        //admin
        public const string Global_Admin = "admin";
        public const string TONG_BIEN_TAP = "channelvn";

        public const string Message_BanKhongCoQuyen = "Xin lỗi ! <br>Bạn không có quyền vào đây";
        
    }
}
