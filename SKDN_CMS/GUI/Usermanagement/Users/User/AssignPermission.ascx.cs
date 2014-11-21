using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.GUI.Users.Common;
using DFISYS.User.Db;
using DFISYS.User.Security;

namespace DFISYS.GUI.Users.User
{
    public partial class AssignPermission : UserControl
    {
        #region Page_load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["id"] == null || Session["id"].ToString() == "")
            {
                Response.Redirect("/users.aspx", false);
                return;
            }
            Page.RegisterHiddenField("hidIdPrefix", ClientID + "_");
            if (!IsPostBack)
            {
                //string userID = Request.QueryString["id"];
                string userID = Session["id"] != null ? Session["id"].ToString() : "";
                string catID = Session["CatID"] != null ? Session["CatID"].ToString() : "";

                //người gán
                string assignUser;
                ViewState["userid"] = assignUser = Page.User.Identity.Name;
                CheckPemission(ViewState["userid"].ToString());
                using (MainDB db = new MainDB())
                {
                    LoadCategory(db);
                    LoadRole(db);
                    //LoadPermission(db , Int32.Parse(lbxRole.SelectedValue));
                    lblPermission.Visible = false;
                }
                //người được gán

                lblUserName.Text = userID;
                //RemoveRole();
                //khoi tao bien secu de lay toan bo roles cua thang hien tai
                LoadLtsBox();
            }
        }
        private void LoadLtsBox()
        {
            //string userID = Request.Params.Get("id");
            string userID = Session["id"] != null ? Session["id"].ToString() : "";
            //khoi tao bien secu de lay toan bo roles cua thang hien tai
            lstRoles.Items.Clear();
            lstCat.Items.Clear();
            lstPer.Items.Clear();

            MainSecurity objscu = new MainSecurity();
            DataTable dt = objscu.GetRoleAsTableNoCache(userID, 1);
            if (dt.Rows.Count > 0)
            {
                lstRoles.DataSource = dt;
                lstRoles.DataTextField = "Role_Name";
                lstRoles.DataValueField = "Role_ID";
                lstRoles.DataBind();
            }

            //lay thong tin ve chuyen muc cua thang hien tai
            dt = objscu.getParentCategoryAssigned(userID, 1);
            if (dt.Rows.Count > 0)
            {
                lstCat.DataTextField = "Cat_Name";
                lstCat.DataValueField = "Cat_ID";
                lstCat.DataSource = dt;
                lstCat.DataBind();
            }

            //lay thong tin ve toan bo quyen
            dt = objscu.GetPermissionAsTable(userID, 1, -1);
            if (dt.Rows.Count > 0)
            {
                lstPer.DataSource = dt;
                lstPer.DataTextField = "Permission_Name";
                lstPer.DataValueField = "Permission_ID";
                lstPer.DataBind();
            }

        }
        #endregion
        #region Load Role
        private void LoadRole(MainDB db)
        {
            lbxRole.DataTextField = "Role_Name";
            lbxRole.DataValueField = "Role_ID";
            lbxRole.DataSource = db.RoleCollection.GetAllAsDataTable();
            lbxRole.DataBind();
            string userID = ViewState["userid"].ToString();
            //nếu user gán là quản trị kênh thì trong list role sẽ không nhìn thấy vai trò quản trị kênh
            if (userID != Const.Global_Admin && userID != Const.TONG_BIEN_TAP)
            {
                ListItem li = lbxRole.Items.FindByValue(RoleConst.QuanTriKenh.ToString());
                lbxRole.Items.Remove(li);
            }
            lbxRole.Items.Remove(lbxRole.Items.FindByValue(RoleConst.TongBienTap.ToString()));
            lbxRole.Items.Remove(lbxRole.Items.FindByValue(RoleConst.Administrator.ToString()));
            lbxRole.Items.Remove(lbxRole.Items.FindByValue(RoleConst.EveryOne.ToString()));
        }
        #endregion
        #region Load Category
        private void LoadCategory(MainDB db)
        {
            string editionType = "1";//Session["EditionType"] != null ? Session["EditionType"].ToString() : string.Empty;
            clbCategory.DataTextField = "Cat_Name";
            clbCategory.DataValueField = "Cat_ID";
            // lấy ra hết các chuyên mục của kênh
            if(!string.IsNullOrEmpty(editionType))
            {
                clbCategory.DataSource = db.CategoryCollection.GetAsDataTable(" (Cat_ParentId is null or Cat_ParentId = 0) and EditionType_ID = " + editionType, "");    
            }
            else
            {
                clbCategory.DataSource = db.CategoryCollection.GetAsDataTable(" Cat_ParentId is null or Cat_ParentId = 0", ""); 
            }
            clbCategory.DataBind();
            
        }
        #endregion

        #region khi chon channel
        protected void ddlChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (MainDB db = new MainDB())
            {
                LoadCategory(db);
                LoadRole(db);
            }
            ResetPermission();
            lblMessage.Visible = false;
            btnUpdate.Visible = false;
            btnCancel.Visible = false;
            btnAssignPermission.Visible = true;
        }
        #endregion
        #region Reset lại Permission
        private void ResetPermission()
        {
            foreach (ListItem li in clbPermission.Items)
            {
                if (li.Selected)
                    li.Selected = false;
                if (!li.Enabled)
                    li.Enabled = true;
            }
            if (!clbPermission.Enabled && lbxRole.SelectedValue != RoleConst.QuanTriKenh.ToString())
                clbPermission.Enabled = true;
        }
        #endregion
        #region Load Permission
        private void LoadPermission(MainDB db, int roleID)
        {
            //Role_PermissionRow[] rpr = db.Role_PermissionCollection.GetByRole_ID(Int32.Parse(lbxRole.SelectedValue));
            //if (rpr != null)
            //{
            //    foreach (Role_PermissionRow row in rpr)
            //    {
            //        foreach (ListItem li in clbPermission.Items)
            //        {
            //            if (li.Value == row.Permission_ID.ToString())
            //            {
            //                li.Selected = true;
            //                //li.Enabled = false;
            //                break;
            //            }
            //        }
            //    }
            //}
            if (lbxRole.SelectedIndex >= 0)
            {
                string sql = "SELECT dbo.Permission.Permission_ID, dbo.Permission.Permission_Name FROM dbo.Permission INNER JOIN dbo.Role_Permission ON dbo.Permission.Permission_ID = dbo.Role_Permission.Permission_ID where Role_ID=" + roleID;
                //DataTable dt = db.SelectQuery(sql);
                DataTable dt = db.StoreProcedure.vc_Execute_Sql(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    clbPermission.DataTextField = "Permission_Name";
                    clbPermission.DataValueField = "Permission_ID";
                    clbPermission.DataSource = dt;
                    clbPermission.DataBind();
                    foreach (ListItem li in clbPermission.Items)
                    {
                        li.Selected = true;
                    }
                    lblPermission.Visible = false;
                }
                else
                {
                    clbPermission.Items.Clear();
                    lblPermission.Visible = true;
                }
            }
        }
        #endregion

        
        #region Validate
        private bool Validated()
        {
            //nếu không chọn vai trò
            if (lbxRole.SelectedIndex == -1)
            {
                ShowMessage("Đề nghị chọn vai trò!");
                return false;
            }
            //nếu chọn vai trò mà không phải là quản trị kênh
            if (lbxRole.SelectedValue != RoleConst.QuanTriKenh.ToString())
            {
                bool isSelectPermission = false;
                bool isSelectCategory = false;
                foreach (ListItem li in clbPermission.Items)
                {
                    if (li.Selected)
                    {
                        isSelectPermission = true;
                        break;
                    }
                }
                if (!isSelectPermission)
                {
                    ShowMessage("Đề nghị chọn quyền!");
                    return false;
                }
                foreach (ListItem liCat in clbCategory.Items)
                {
                    if (liCat.Selected)
                    {
                        isSelectCategory = true;
                        break;
                    }
                }
                if (!isSelectCategory)
                {
                    ShowMessage("Đề nghị chọn chuyên mục!");
                    return false;
                }
            }
            return true;
        }
        #endregion
        private void ShowMessage(string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
        }
        #region Khi bấm nút gán quyền
        protected void btnAssignPermission_Click1(object sender, EventArgs e)
        {
            if (!Validated())
                return;
            int channelID = 1;
            string userID = lblUserName.Text;
            int roleID = Int32.Parse(lbxRole.SelectedValue);
            Business.User user = new Business.User();
            //gán dữ liệu cho bảng channel_user
            Channel_UserRow cur = new Channel_UserRow();
            cur.Channel_ID = channelID;
            cur.User_ID = userID;
            //---------------------------------
            //gán dữ liệu cho bảng channel_user_role
            Channel_User_RoleRow curr = new Channel_User_RoleRow();
            curr.Role_ID = roleID;
            //----------------------------------
            using (MainDB db = new MainDB())
            {
                //Kiểm tra xem user đã tồn tại trong kênh này chưa 
                Channel_UserRow[] chanU = db.Channel_UserCollection.GetAsArray("User_ID='" + userID + "' and Channel_ID=" + channelID, "");
                //nếu đã tồn tại thì lấy ID của bảng channel_user gán vào bảng channel_user_role
                if (chanU != null && chanU.Length > 0)
                {
                    curr.CU_ID = chanU[0].CU_ID;
                }
                else // nếu chưa tồn tại thì insert 1 bản ghi vào bảng channel_user sau đó lấy ID gán vào channel_user_role
                {
                    //inssert dữ liệu vào bảng channel_user
                    db.Channel_UserCollection.Insert(cur);
                    //gán ID của channel_user_role bằng id vừa thêm vào
                    curr.CU_ID = cur.CU_ID;
                }
                //Kiểm tra xem người dùng ở kênh này đã có role này chưa
                DataTable dtChannelUserRole = user.GetChannelUserRole(userID, channelID, roleID);
                int CUR_ID = -1;
                if (dtChannelUserRole != null && dtChannelUserRole.Rows.Count > 0)
                {
                    CUR_ID = Convert.ToInt32(dtChannelUserRole.Rows[0][0]);
                }
                else
                {
                    //gán channel_user vào role
                    db.Channel_User_RoleCollection.Insert(curr);
                }
                //nếu gán cho các role không phải là quản trị kênh thì gán đầy đủ quyền và chuyên mục
                if (lbxRole.SelectedValue != RoleConst.QuanTriKenh.ToString())
                {
                    User_PermissionRow upr;
                    User_CategoryRow ucr;
                    //duyệt qua các item của chuyên mục
                    foreach (ListItem li in clbCategory.Items)
                    {
                        //nếu chuyện mục nào được chọn thì gán cho chuyên mục đó
                        if (li.Selected)
                        {
                            upr = new User_PermissionRow();
                            //nếu không tồn tại role này thì lấy giá trị gán theo role mới
                            if (CUR_ID == -1)
                            {
                                upr.CUR_ID = curr.CUR_ID;
                            }
                            else // nếu đã tồn tại role này thì lấy giá trị role này luôn.
                            {
                                upr.CUR_ID = CUR_ID;
                            }
                            //duyệt qua các item của quyền
                            foreach (ListItem liPermission in clbPermission.Items)
                            {
                                //nếu quyền nào được chọn thì gán cho quyền đó
                                if (liPermission.Selected)
                                {
                                    upr.Permission_ID = Int32.Parse(liPermission.Value);
                                    //gán vào bảng permission
                                    db.User_PermissionCollection.Insert(upr);
                                    ucr = new User_CategoryRow();
                                    ucr.UP_ID = upr.UP_ID;
                                    ucr.Cat_ID = Int32.Parse(li.Value);
                                    //gán user permission vào categorry
                                    db.User_CategoryCollection.Insert(ucr);
                                }
                            }
                        }
                    }
                }
                //khi gán quyền sẽ đặt acive cho user = true
                UserRow ur = db.UserCollection.GetByPrimaryKey(userID);
                if (!ur.User_isActive)
                {
                    ur.User_isActive = true;
                    db.UserCollection.Update(ur);
                }
            }
            lblMessage.Text = "Gán thành công";
            ResetCategory();
            LoadLtsBox();
            //Response.Redirect("/users/userassign.aspx");

            DFISYS.BO.Editoral.Category.CategoryHelper.CleanCacheCategory();
            DFISYS.BO.Editoral.Category.CategoryHelper.CleanCachePermission();
            //GetPermission1_
        }
        #endregion

        private void ResetCategory()
        {
            foreach (ListItem li in clbCategory.Items)
            {
                if (li.Selected)
                    li.Selected = false;
                if (!li.Enabled)
                    li.Enabled = true;
            }
            if (!clbCategory.Enabled && lbxRole.SelectedValue != RoleConst.QuanTriKenh.ToString())
                clbCategory.Enabled = true;
        }
        private void SetListSelected(string id, CheckBoxList cbl)
        {
            if (id.IndexOf(',') >= 0)
            {
                foreach (ListItem li in cbl.Items)
                {
                    if (id.Contains("," + li.Value + ","))
                    {
                        li.Selected = true;
                    }
                }
            }
            else
            {
                foreach (ListItem li in cbl.Items)
                {
                    if (id.Trim() == li.Value.Trim())
                    {
                        li.Selected = true;
                    }
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnUpdate.Visible = false;
            btnCancel.Visible = false;
            lblMessage.Visible = false;
            btnAssignPermission.Visible = true;
            ResetCategory();
        }
        #region bấm nút update
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //Validate
            if (!Validated())
                return;
            //Delete old record
            Business.User user = new Business.User();
            user.DeleteByUserChannelRole(lblUserName.Text, ViewState["id"].ToString());
            //Insert new record
            btnAssignPermission_Click1(null, null);
            //show hide button
            btnUpdate.Visible = false;
            btnCancel.Visible = false;
            btnAssignPermission.Visible = true;
            lblMessage.Text = "Update thành công";
            lblMessage.Visible = true;
        }
        #endregion

        protected void lbxRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetCategory();
            if (lbxRole.SelectedValue == RoleConst.QuanTriKenh.ToString())
            {
                clbPermission.Enabled = false;
                clbCategory.Enabled = false;
            }
            else
            {
                clbPermission.Enabled = true;
                clbCategory.Enabled = true;
                using (MainDB db = new MainDB())
                {
                    LoadPermission(db, Int32.Parse(lbxRole.SelectedValue));
                }
            }
            lblMessage.Visible = false;
        }
        private void CheckPemission(string userID)
        {
            MainSecurity ms = new MainSecurity();
            if (userID.Trim().ToLower() != Common.Const.Global_Admin && userID.Trim().ToLower() != Common.Const.TONG_BIEN_TAP && !ms.GetRole(userID).isQuanTriKenh && !ms.GetRole(userID).isPhuTrachKenh)
            {
                Response.Redirect("/users/" + Const.OBJECT_ERROR + ".aspx?message=" + Const.Message_BanKhongCoQuyen);
            }
        }
        private void RemoveRole()
        {
            MainSecurity ms = new MainSecurity();
            DataTable dt = ms.GetRoleAsTable(lblUserName.Text, 1);
            string ids = ",";
            foreach (DataRow dr in dt.Rows)
            {
                ids += dr[0].ToString() + ",";
            }

            if (ids.Length > 1)
            {
                ArrayList al = new ArrayList();
                foreach (ListItem li in lbxRole.Items)
                {
                    if (ids.Contains("," + li.Value + ","))
                    {
                        //al.Add(li);
                        li.Enabled = false;
                    }
                }
                if (al != null && al.Count > 0)
                {
                    foreach (ListItem item in al)
                    {
                        lbxRole.Items.Remove(item);
                    }
                }
            }

        }

        protected void btnGoback_Click(object sender, EventArgs e)
        {
            Session["id"] = "";
            Session["CatID"] = "";
            Response.Redirect("/" + Const.OBJECT_USER + ".aspx");
        }

        protected void lstCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRoles.SelectedIndex >= 0)
            {
                //string userid = Request.Params.Get("id");
                string userid = Session["id"] != null ? Session["id"].ToString() : "";
                MainSecurity objscu = new MainSecurity();
                lstPer.DataSource = objscu.GetPermissionAsTable(userid, 1, Int32.Parse(lstRoles.SelectedValue), Int32.Parse(lstCat.SelectedValue));
                lstPer.DataTextField = "Permission_Name";
                lstPer.DataValueField = "Permission_ID";
                lstPer.DataBind();
            }
        }

        protected void lstRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string userid=Request.Params.Get("id");
            string userid = Session["id"] != null ? Session["id"].ToString() : "";
            MainSecurity objscu = new MainSecurity();
            //lay thong tin ve chuyen muc cua thang hien tai
            lstCat.DataTextField = "Cat_Name";
            lstCat.DataValueField = "Cat_ID";
            lstCat.DataSource = objscu.GetCategoryByRoleAsTable(userid, Int32.Parse(lstRoles.SelectedValue));
            lstCat.DataBind();
            //lay thong tin ve toan bo quyen
            lstPer.DataSource = objscu.GetPermissionAsTable(userid, 1, -1);
            lstPer.DataTextField = "Permission_Name";
            lstPer.DataValueField = "Permission_ID";
            lstPer.DataBind();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstRoles.SelectedIndex >= 0 && lstCat.SelectedIndex >= 0 && lstPer.SelectedIndex >= 0)
            {
                ResetCategory();
                string strPer = "";
                foreach (ListItem item in lstPer.Items)
                {
                    strPer += item.Value + ",";
                }
                /// nếu có chuyên mục : channel,role;category:permission
                string id = 1 + "," + lstRoles.SelectedValue + ";" + lstCat.SelectedValue + ":" + strPer.Substring(0, strPer.Length - 1);
                ViewState["id"] = id;
                Business.User user = new Business.User();
                int Role_ID = Int32.Parse(user.GetRoleID(id));
                using (MainDB db = new MainDB())
                {
                    lbxRole.SelectedIndex = 0;
                    LoadPermission(db, Role_ID);
                }
                ResetPermission();
                //set category selected
                if (user.GetCategoryID(id) != "")
                {
                    SetListSelected(user.GetCategoryID(id), clbCategory);
                    SetListSelected("," + id.Split(':')[1] + ",", clbPermission);
                    lbxRole.SelectedValue = Role_ID.ToString();
                }
                else
                {
                    lbxRole.SelectedValue = RoleConst.QuanTriKenh.ToString();
                    clbPermission.Enabled = false;
                    clbCategory.Enabled = false;
                }
                btnAssignPermission.Visible = false;
                btnUpdate.Visible = true;
                btnCancel.Visible = true;
                lblMessage.Visible = false;
            }
            else
            {
                lblMessageXoa.Text = "Bạn phải chọn vài trò , chuyên mục , quyền muốn sửa";
                return;
            }
        }

        private void ResetWhenDelete()
        {
            lblMessage.Visible = false;
            ResetCategory();
            btnUpdate.Visible = false;
            btnAssignPermission.Visible = true;
            btnCancel.Visible = false;
        }
        protected void btnRemoveQuyen_Click(object sender, EventArgs e)
        {
            if (lstRoles.SelectedIndex >= 0 && lstCat.SelectedIndex >= 0 && lstPer.SelectedIndex >= 0)
            {
                Business.User user = new DFISYS.GUI.Users.Business.User();
                user.RemovePermission(lblUserName.Text, "1", lstRoles.SelectedValue, lstCat.SelectedValue, lstPer.SelectedValue);
                lstCat_SelectedIndexChanged(null, null);
                lblMessageXoa.Text = "";
                ResetWhenDelete();

                DFISYS.BO.Editoral.Category.CategoryHelper.CleanCacheCategory();
                DFISYS.BO.Editoral.Category.CategoryHelper.CleanCachePermission();
            }
            else
            {
                lblMessageXoa.Text = "Bạn phải chọn vài trò , chuyên mục , quyền muốn xóa";
                return;
            }
        }

        protected void btnRemoveChuyenMuc_Click(object sender, EventArgs e)
        {
            if (lstRoles.SelectedIndex >= 0 && lstCat.SelectedIndex >= 0)
            {
                Business.User user = new DFISYS.GUI.Users.Business.User();
                user.RemoveCategory(lblUserName.Text, "1", lstRoles.SelectedValue, lstCat.SelectedValue);
                lstRoles_SelectedIndexChanged(null, null);
                lblMessageXoa.Text = "";
                ResetWhenDelete();
                DFISYS.BO.Editoral.Category.CategoryHelper.CleanCacheCategory();
                DFISYS.BO.Editoral.Category.CategoryHelper.CleanCachePermission();
            }
            else
            {
                lblMessageXoa.Text = "Bạn phải chọn vài trò , chuyên mục muốn xóa";
                return;
            }
        }

        protected void btnRemoveVaiTro_Click(object sender, EventArgs e)
        {
            if (lstRoles.SelectedIndex >= 0)
            {
                Business.User user = new DFISYS.GUI.Users.Business.User();
                user.RemoveRole(lblUserName.Text, "1", lstRoles.SelectedValue);
                LoadLtsBox();
                lblMessageXoa.Text = "";
                ResetWhenDelete();

                DFISYS.BO.Editoral.Category.CategoryHelper.CleanCacheCategory();
                DFISYS.BO.Editoral.Category.CategoryHelper.CleanCachePermission();

                //Response.Redirect("/users/userassign.aspx");
            }
            else
            {
                lblMessageXoa.Text = "Bạn phải chọn vài trò muốn xóa";
                return;
            }
        }
    }
}