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
using DFISYS.GUI.Users.Common;
using DFISYS.User.Db;
using DFISYS.User.Security;

namespace DFISYS.GUI.Users.User
{
    public partial class RolePermission : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["userid"] = Page.User.Identity.Name;
                //CheckPemission(Page.User.Identity.Name);
                using (MainDB db = new MainDB())
                {
                    LoadRole(db);
                    clbPermission.DataTextField = "Permission_Name";
                    clbPermission.DataValueField = "Permission_ID";
                    clbPermission.DataSource = db.PermissionCollection.GetAllAsDataTable();
                    clbPermission.DataBind();
                }
            }
        }
        private void CheckPemission(string userID)
        {
            MainSecurity ms = new MainSecurity();
            if (userID.Trim().ToLower() != Common.Const.TONG_BIEN_TAP)
            {
                Response.Redirect("users/" + Const.OBJECT_ERROR + ".aspx?message=" + Const.Message_BanKhongCoQuyen);
            }
        }
        private void LoadRole(MainDB db)
        {
            lbxRole.DataTextField = "Role_Name";
            lbxRole.DataValueField = "Role_ID";
            lbxRole.DataSource = db.RoleCollection.GetAllAsDataTable();            
            lbxRole.DataBind();
            lbxRole.Items.Remove(lbxRole.Items.FindByValue(RoleConst.TongBienTap.ToString()));
            lbxRole.Items.Remove(lbxRole.Items.FindByValue(RoleConst.Administrator.ToString()));
            lbxRole.Items.Remove(lbxRole.Items.FindByValue(RoleConst.QuanTriKenh.ToString()));
            lbxRole.Items.Remove(lbxRole.Items.FindByValue(RoleConst.EveryOne.ToString()));
        }
        private void LoadPermission(MainDB db)
        {
            Role_PermissionRow[] rpr = db.Role_PermissionCollection.GetByRole_ID(Int32.Parse(lbxRole.SelectedValue));
            if(rpr!=null)
            {
                foreach (Role_PermissionRow row in rpr)
                {
                    foreach (ListItem li in clbPermission.Items)
                    {
                        if(li.Value == row.Permission_ID.ToString())
                        {
                            li.Selected = true;
                            break;
                        }
                    }       
                }    
            }
        }

        protected void lbxRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetPermission();
            using(MainDB db = new MainDB())
            {
                LoadPermission(db);
            }
        }
        private void ResetPermission()
        {
            foreach (ListItem li in clbPermission.Items)
            {
                if (li.Selected)
                {
                    li.Selected = false;
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            using(MainDB db = new MainDB())
            {
                int roleID = Int32.Parse(lbxRole.SelectedValue);
                //xóa tất cả các permission của role 
                db.Role_PermissionCollection.DeleteByRole_ID(roleID);
                Role_PermissionRow rpr;
                //duyệt qua các item được chọn và đưa vào DB
                foreach (ListItem li in clbPermission.Items)
                {
                    if(li.Selected)
                    {
                        rpr = new Role_PermissionRow();
                        rpr.Role_ID = roleID;
                        rpr.Permission_ID = Int32.Parse(li.Value);
                        db.Role_PermissionCollection.Insert(rpr);
                    }
                }
            }
        }

        protected void lbtBack_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnGoback_Click(object sender, EventArgs e)
        {
            Response.Redirect(Const.OBJECT_USER + ".aspx");
        }
    }
}