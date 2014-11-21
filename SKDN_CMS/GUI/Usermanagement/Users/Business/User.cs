using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DFISYS.GUI.Users.Common;
using DFISYS.User.Db;
using DFISYS.GUI.Users;
using DFISYS.User.Security;

namespace DFISYS.GUI.Users.Business
{
    public class User
    {
        /// <summary>
        /// Kiểm tra xem có user này trong DB chưa 
        /// </summary>
        /// <param name="userID">user ID</param>
        /// <returns>true nếu đã tồn tại</returns>
        public bool isUserExited(string userID)
        {
            using (MainDB db = new MainDB())
            {
                UserRow ur = db.UserCollection.GetByPrimaryKey(userID.Trim().ToLower());
                if (ur != null)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Kiểm tra 1 chuỗi không cho phép trùng lặp
        /// </summary>
        /// <param name="inputString">Chuỗi : VD : 1,2,3,2,1,</param>
        /// <returns>1,2,3</returns>
        public string CheckDupplicate(string inputString)
        {
            string value = "";
            if (inputString.Length > 0)
            {
                string[] arr = inputString.Split(',');
                foreach (string s in arr)
                {
                    if (s.Length > 0 && !value.Contains(s))
                    {
                        value += s + ",";
                    }
                }
            }
            return value;
        }
        /// <summary>
        /// lấy ra CUR_ID của bảng Channel_User_Role
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="channelID">Channel ID</param>
        /// <param name="roleID">Role ID</param>
        /// <returns>CUR_ID</returns>
        public DataTable GetChannelUserRole(string userID, int channelID, int roleID)
        {
            string que = "select Distinct CUR_ID from View_GetRole where Channel_ID=" + channelID + " and User_ID='" + userID + "' and Role_ID=" + roleID;
            DataTable kq = null;
            using (MainDB db = new MainDB())
            {
                //kq = db.SelectQuery(que);
                kq = db.StoreProcedure.vc_Execute_Sql(que);
            }
            return kq;
        }
        public DataTable GetAllUser( int roleID)
        {
            string que = "select Distinct User_ID from View_GetRole where Channel_ID= 1 and Role_ID=" + roleID;
            DataTable kq = null;
            using (MainDB db = new MainDB())
            {
                //kq = db.SelectQuery(que);
                kq = db.StoreProcedure.vc_Execute_Sql(que);
            }
            return kq;
        }
        /// <summary>
        /// remove role from channel
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="channelID"></param>
        /// <param name="roleID"></param>
        public void RemoveRole(string userID, string channelID, string roleID)
        {
            try
            {
                using (MainDB db = new MainDB())
                {
                    //lấy ra danh sách các Cat_ID
                    //DataTable category = db.SelectQuery("select Cat_ID from " + Const.VIEW_Permission_withCategory + " where User_ID='" + userID + "' and Channel_ID=" + channelID);

                    //DataTable category = db.StoreProcedure.vc_Execute_Sql("select Cat_ID from " + Const.VIEW_Permission_withCategory + " where User_ID='" + userID + "' and Channel_ID=" + channelID);

                    //DataRow[] drCat_ID = category.Select();
                    //if (drCat_ID != null && drCat_ID.Length > 0)
                    //{
                    //    for (int i = 0; i < drCat_ID.Length; i++)
                    //    {
                    //        string Cat_ID = drCat_ID[i][0].ToString();
                    //        RemoveCategory(userID, channelID, roleID, Cat_ID);
                    //    }
                    //}
                    Channel_UserRow channel_user = db.Channel_UserCollection.GetAsArray("User_ID='" + userID + "' and Channel_ID=" + channelID, "")[0];
                    //Channel_User_RoleRow channel_user_role = db.Channel_User_RoleCollection.GetAsArray("CU_ID=" + channel_user.CU_ID + " and Role_ID=" + roleID, "")[0];
                    db.Channel_User_RoleCollection.Delete("CU_ID=" + channel_user.CU_ID + " and Role_ID=" + roleID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// remove category and permisson from user_channel_role
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="channelID"></param>
        /// <param name="roleID"></param>
        /// <param name="categoryID"></param>
        public void RemoveCategory(string userID, string channelID, string roleID, string categoryID)
        {
            try
            {
                using (MainDB db = new MainDB())
                {
                    //lấy ra danh sách các ID thuộc category_user và user_permission
                    //DataTable category = db.SelectQuery("select UC_ID,UP_ID from " + Const.VIEW_Permission_withCategory + " where User_ID='" + userID + "' and Channel_ID=" + channelID + " and Cat_ID=" + categoryID);

                    DataTable category = db.StoreProcedure.vc_Execute_Sql("select UC_ID,UP_ID from " + Const.VIEW_Permission_withCategory + " where User_ID='" + userID + "' and Channel_ID=" + channelID + " and Cat_ID=" + categoryID);

                    DataRow[] drUC_ID = category.Select();
                    if (drUC_ID != null && drUC_ID.Length > 0)
                    {
                        for (int i = 0; i < drUC_ID.Length; i++)
                        {
                            int UC_ID = Convert.ToInt32(drUC_ID[i][0]);
                            int UP_ID = Convert.ToInt32(drUC_ID[i][1]);
                            //xóa các permission liên quan đến category đó
                            db.User_PermissionCollection.DeleteByPrimaryKey(UP_ID);
                            //xóa dữ liệu trong bảng liên quan giữa category và user_permission
                            db.User_CategoryCollection.DeleteByPrimaryKey(UC_ID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// remove permission from category
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="channelID"></param>
        /// <param name="roleID"></param>
        /// <param name="categoryID"></param>
        /// <param name="permissionID"></param>
        public void RemovePermission(string userID, string channelID, string roleID, string categoryID, string permissionID)
        {
            try
            {
                using (MainDB db = new MainDB())
                {
                    //lấy ra danh sách các ID thuộc category_user và user_permission
                    //DataTable category = db.SelectQuery("select UP_ID from " + Const.VIEW_Permission_withCategory + " where Permission_ID=" + permissionID + " and User_ID='" + userID + "' and Channel_ID=" + channelID + " and Cat_ID=" + categoryID);

                    DataTable category = db.StoreProcedure.vc_Execute_Sql("select UP_ID from " + Const.VIEW_Permission_withCategory + " where Permission_ID=" + permissionID + " and User_ID='" + userID + "' and Channel_ID=" + channelID + " and Cat_ID=" + categoryID);

                    DataRow[] drUC_ID = category.Select();
                    if (drUC_ID != null && drUC_ID.Length > 0)
                    {
                        for (int i = 0; i < drUC_ID.Length; i++)
                        {
                            int UP_ID = Convert.ToInt32(drUC_ID[i][0]);
                            //xóa các permission liên quan đến category đó
                            db.User_PermissionCollection.DeleteByPrimaryKey(UP_ID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //using (MainDB db = new MainDB())
            //{
            //    try
            //    {
            //        Channel_UserRow channel_user = db.Channel_UserCollection.GetAsArray("User_ID='" + userID + "' and Channel_ID=" + channelID, "")[0];
            //        Channel_User_RoleRow channel_user_role = db.Channel_User_RoleCollection.GetAsArray("CU_ID=" + channel_user.CU_ID + " and Role_ID=" + roleID, "")[0];
            //        User_PermissionRow user_permission = db.User_PermissionCollection.GetAsArray("CUR_ID=" + channel_user_role.CUR_ID + " and Permission_ID=" + permissionID, "")[0];
            //        db.User_CategoryCollection.Delete("UP_ID=" + user_permission.UP_ID + " and Cat_ID=" + categoryID);
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;                    
            //    }                
            //}
        }
        /// <summary>
        /// Xóa các bản ghi theo user , channel , role , permission , category
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="id">VD : 1,2;4:4,6,8</param>
        public void DeleteByUserChannelRole(string userID, string id)
        {
            int channelID = Int32.Parse(GetChannelID(id));
            int role = Int32.Parse(GetRoleID(id));
            int catID = -1;
            string[] permission = GetPermissionID(id);

            //khai báo primarykey
            int CUR_ID = -1;
            int UP_ID = -1;
            int UC_ID = -1;
            //nếu có category
            if (GetCategoryID(id) != "")
            {
                catID = Int32.Parse(GetCategoryID(id));
                using (MainDB db = new MainDB())
                {
                    DataTable temp;
                    foreach (string s in permission)
                    {
                        //temp = db.SelectQuery("select UP_ID,UC_ID from " + Const.VIEW_Permission_withCategory + " where Channel_ID=" + channelID + " and User_ID='" + userID + "' and Role_ID=" + role + " and Cat_ID=" + catID + " and Permission_ID=" + s);
                        temp = db.StoreProcedure.vc_Execute_Sql("select UP_ID,UC_ID from " + Const.VIEW_Permission_withCategory + " where Channel_ID=" + channelID + " and User_ID='" + userID + "' and Role_ID=" + role + " and Cat_ID=" + catID + " and Permission_ID=" + s);
                        UP_ID = Convert.ToInt32(temp.Rows[0][0]);
                        UC_ID = Convert.ToInt32(temp.Rows[0][1]);
                        db.User_CategoryCollection.DeleteByPrimaryKey(UC_ID);
                        db.User_PermissionCollection.DeleteByPrimaryKey(UP_ID);
                    }
                }
            }
            else // nếu không có category (có nghĩa đây là role permission)
            {
                string que = "select Distinct CUR_ID from View_GetRole where Channel_ID=" + channelID + " and User_ID='" + userID + "' and Role_ID=" + role;
                DataTable kq;
                using (MainDB db = new MainDB())
                {
//                    kq = db.SelectQuery(que);
                    kq = db.StoreProcedure.vc_Execute_Sql(que);
                    CUR_ID = Convert.ToInt32(kq.Rows[0][0]);
                    db.Channel_User_RoleCollection.DeleteByPrimaryKey(CUR_ID);
                }
                return;
            }
        }

        /// <summary>
        /// lấy thông tin quyền của user với mỗi kênh
        /// bảng trả về gồm 2 cột ID và Name
        /// cột ID chứa các ID của channel,role,category,permission và có định dạng như sau
        /// nếu không có chuyên mục : channel,role:permission
        /// nếu có chuyên mục : channel,role;category:permission
        /// Cột name chứa các tên quyền theo định dạng sau : channel / role / category / permission
        /// </summary>
        /// <param name="userID">user id</param>
        /// <param name="channelID">channel id</param>
        /// <returns>Table</returns>
        public DataTable GetPermissionInfo(string userID, int channelID)
        {
            DataTable result = new DataTable();
            result.Columns.Add("ID");
            result.Columns.Add("Channel");
            result.Columns.Add("Role");
            result.Columns.Add("Category");
            result.Columns.Add("Permission");
            using (MainDB db = new MainDB())
            {
                string channelName = db.ChannelCollection.GetByPrimaryKey(channelID).Channel_Name;
                //lấy các role theo channel và user
                //DataTable dtRole = db.SelectQuery(
                  //     "select DISTINCT Role_ID,Role_Name from View_GetRole where User_ID = '" + userID + "' and Channel_ID=" + channelID);
                DataTable dtRole = db.StoreProcedure.vc_Execute_Sql(
                       "select DISTINCT Role_ID,Role_Name from View_GetRole where User_ID = '" + userID + "' and Channel_ID=" + channelID);
                //nếu có role
                if (dtRole != null && dtRole.Rows.Count > 0)
                {
                    int roleID = -1;
                    string roleName = "";
                    string id = "";
                    DataTable dtCategory;
                    DataTable dtPermission;
                    //duyệt qua các role
                    foreach (DataRow dr in dtRole.Rows)
                    {
                        roleID = Convert.ToInt32(dr["Role_ID"]);
                        roleName = dr["Role_Name"].ToString();
                        //nếu không phải là quản trị kênh
                        if (roleID != RoleConst.QuanTriKenh)
                        {
                            //lấy các category theo channel , user , role
                            //dtCategory = db.SelectQuery("select DISTINCT Cat_ID,Cat_Name from " + Const.VIEW_Permission_withCategory + " where User_ID = '" + userID + "' and Channel_ID=" + channelID + " and Role_ID=" + roleID);
                            dtCategory = db.StoreProcedure.vc_Execute_Sql("select DISTINCT Cat_ID,Cat_Name from " + Const.VIEW_Permission_withCategory + " where User_ID = '" + userID + "' and Channel_ID=" + channelID + " and Role_ID=" + roleID);
                            //nếu có tồn tại chuyên mục
                            if (dtCategory != null && dtCategory.Rows.Count > 0)
                            {
                                int catID = -1;
                                string catName = "";
                                //duyệt qua các chuyên mục
                                foreach (DataRow drCat in dtCategory.Rows)
                                {
                                    catID = Convert.ToInt32(drCat["Cat_ID"]);
                                    catName = drCat["Cat_Name"].ToString();
                                    //lấy các quyền theo channel , user , role , category
                                    //dtPermission = db.SelectQuery(
                                      //  "select DISTINCT Permission_ID,Permission_Name from " + Const.VIEW_Permission_withCategory + " where User_ID = '" + userID + "' and Channel_ID=" + channelID + " and Role_ID=" + roleID + " and Cat_ID=" + catID);
                                    dtPermission = db.StoreProcedure.vc_Execute_Sql(
                                        "select DISTINCT Permission_ID,Permission_Name from " + Const.VIEW_Permission_withCategory + " where User_ID = '" + userID + "' and Channel_ID=" + channelID + " and Role_ID=" + roleID + " and Cat_ID=" + catID);
                                    if (dtPermission != null && dtPermission.Rows.Count > 0)
                                    {
                                        string permissionID = "";
                                        string permissionName = "";
                                        foreach (DataRow drPer in dtPermission.Rows)
                                        {
                                            permissionID += drPer["Permission_ID"].ToString() + ",";
                                            permissionName += " <img src='/images/Icons/ok.gif'/>" + drPer["Permission_Name"].ToString() + "<br>";
                                        }
                                        id = channelID.ToString() + "," + roleID.ToString() + ";" + catID.ToString() + ":" + permissionID;
                                        result.Rows.Add(new string[] { id.Remove(id.Length - 1), channelName, roleName, catName, permissionName.Remove(permissionName.Length - 2) });
                                    }
                                }
                            }
                        }
                        else // nếu là quản trị kênh 
                        {
                            id = channelID.ToString() + "," + roleID.ToString();
                            result.Rows.Add(new string[] { id, channelName, roleName, "", "" });
                        }
                    }
                }
            }
            return result;
        }
        public string[] GetPermissionID(string id)
        {
            if (id.IndexOf(":") > 0)
                return id.Split(':')[1].Split(',');
            else
                return null;
        }
        public string GetPermissionIDAsString(string id)
        {
            string result = ",";
            string[] temp = GetPermissionID(id);
            if (temp != null)
            {
                foreach (string s in temp)
                {
                    result += s + ",";
                }
            }
            else
                return "";

            return result;
        }
        public string GetChannelID(string id)
        {
            return id.Split(',')[0];
        }
        public string GetRoleID(string id)
        {
            //nếu tồn tại chuyên mục và quyền
            if (id.IndexOf(';') > 0 || id.IndexOf(':') > 0)
            {
                if (id.IndexOf(';') > 0)
                {
                    return id.Split(';')[0].Split(',')[1];
                }
                else
                {
                    return id.Split(':')[0].Split(',')[1];
                }
            }
            else//nếu không tồn tại chuyên mục và quyền
            {
                return id.Split(',')[1];
            }
        }
        public string GetCategoryID(string id)
        {
            if (id.IndexOf(';') > 0)
                return id.Split(':')[0].Split(';')[1];
            else
                return "";
        }
    }
}
