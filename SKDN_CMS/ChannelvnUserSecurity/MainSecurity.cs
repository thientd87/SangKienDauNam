using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using DFISYS.User.Db;
using DFISYS.API;
using System.Web.Caching;
using System.Web;

namespace DFISYS.User.Security {
    public class MainSecurity {
        public bool CanAccess(string userID) {
            using (MainDB db = new MainDB()) {
                //DataTable dt = db.SelectQuery("select Channel_ID,Channel_Name from VIEW_GetChannel where User_ID='" + userID + "'");
                DataTable dt = db.StoreProcedure.CanAccess(userID);

                UserRow ur = db.UserCollection.GetByPrimaryKey(userID.Trim());
                if (ur != null && ur.User_isActive && dt != null && dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
        }
        public DataTable getParentCategoryAssigned(string userID, int channelID) {
            DataTable dt = null;
            using (MainDB db = new MainDB()) {
                dt = db.StoreProcedure.GetParentCategoryAssigned(userID, channelID.ToString());
            }
            return dt;
        }

        public DataTable getParentCategoryAssigned(string userID, int channelID, int editionType)
        {
            DataTable dt = null;
            using (MainDB db = new MainDB())
            {
                dt = db.StoreProcedure.GetParentCategoryAssigned(userID, channelID.ToString(), editionType);
            }
            return dt;
        }

        /// <summary>
        /// Kiểm tra trạng thái của user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool IsUserActive(string userID) {
            using (MainDB db = new MainDB()) {
                UserRow ur = db.UserCollection.GetByPrimaryKey(userID.Trim());
                if (ur != null)
                    return ur.User_isActive;
                else
                    return false;
            }
        }
        /// <summary>
        /// Kiểm tra xem user có tồn tại không 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool IsUserExited(string userID) {
            using (MainDB db = new MainDB()) {
                UserRow ur = db.UserCollection.GetByPrimaryKey(userID.Trim());
                if (ur != null)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// Get mọi kênh người dùng có
        /// Channel_ID,Channel_Name
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns></returns>
        public DataTable GetChannelAsTable(string userID) {
            DataTable dt;
            using (MainDB db = new MainDB()) {
                dt = db.StoreProcedure.GetChannelAsTable(userID);
            }
            return dt;
        }
        /// <summary>
        /// Get mọi role
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllRole() {
            DataTable dt = null;
            using (MainDB db = new MainDB()) {
                //dt = db.RoleCollection.GetAllAsDataTable();
                dt = db.StoreProcedure.GetAllRole();
            }
            return dt;
        }
        /// <summary>
        /// Get ra mảng description role của 1 user theo kênh
        /// các tên cách nhau bởi dấu phẩy
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="channelID">Kênh</param>
        /// <returns></returns>
        public string GetRoleDesAsString(string userID, int channelID) {
            string roles = "";
            DataTable dt = null;
            using (MainDB db = new MainDB()) {
                //dt = db.SelectQuery("select Role_Description from View_GetRole where User_ID='" + userID +
                //                 "' and Channel_ID=" + channelID);
                //dt = db.StoreProcedure.vc_Execute_Sql("select Role_Description from View_GetRole where User_ID='" + userID +
                //"' and Channel_ID=" + channelID);
                dt = db.StoreProcedure.GetRoleDesAsString(userID, channelID.ToString());
            }
            if (dt != null && dt.Rows.Count > 0) {
                foreach (DataRow dr in dt.Rows) {
                    roles += dr[0].ToString() + ",";
                }
                roles = roles.Remove(roles.Length - 1);
            }
            return roles;
        }

        public string[] GetRoleSymbol(string userID, int channelID) {
            string strCacheName = "GetRoleSymbol_" + userID + "_" + channelID;
            string[] strTableCache = { "Channel_User", "Channel_User_Role", "Role" };
            string[] roles = null;
            DataTable dt = GetFromCache(strCacheName);
            if (dt == null) {
                using (MainDB db = new MainDB()) {
                    //dt = db.SelectQuery("select Role_Description from View_GetRole where User_ID='" + userID +
                    //                 "' and Channel_ID=" + channelID);
                    dt = db.StoreProcedure.GetRoleDesAsString(userID, channelID.ToString());
                }
                SetCache(dt, strCacheName, strTableCache);
            }


            if (dt != null && dt.Rows.Count > 0) {
                roles = new string[dt.Rows.Count];
                int i = 0;
                foreach (DataRow dr in dt.Rows) {
                    roles[i] = dr[0].ToString();
                }
            }
            return roles;
        }
        /// <summary>
        /// Get role của user theo kênh , trả ra table
        /// Role_ID , Role_Name , Role_Description
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="channelID">Kênh</param>
        /// <returns></returns>
        public DataTable GetRoleAsTable(string userID, int channelID) {
            string strCacheName = "GetRoleAsTable_" + userID + "_" + channelID;
            string[] strTableCache = { "Channel_User", "Channel_User_Role", "Role" };

            DataTable dt = GetFromCache(strCacheName);
            if (dt == null) {
                if (userID.ToLower() != "channelvn" && userID.ToLower() != "admin") {
                    using (MainDB db = new MainDB()) {
                        dt = db.StoreProcedure.GetRoleAsTable(userID, channelID.ToString());
                    }
                } else {
                    using (MainDB db = new MainDB()) {
                        dt = db.StoreProcedure.vc_Execute_Sql(" select * from role Where Role_ID IN(2,13,14,20)  ");
                    }

                }
                SetCache(dt, strCacheName, strTableCache);

            }

            return dt;
        }


        public DataTable GetRoleAsTableNoCache(string userID, int channelID) {

            DataTable dt = new DataTable();

            if (userID.ToLower() != "admin") {
                using (MainDB db = new MainDB()) {
                    dt = db.StoreProcedure.GetRoleAsTable(userID, channelID.ToString());
                }
            } else {
                using (MainDB db = new MainDB()) {
                    dt = db.StoreProcedure.vc_Execute_Sql(" select * from role Where Role_ID IN(2,13,14,20)  ");
                }

            }

            return dt;
        }

        /// <summary>
        /// Get chuyên mục của user theo kênh : Cat_ID,Cat_Name
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="channelID">Kênh</param>
        /// <returns></returns>
        public DataTable GetCategoryAsTable(string userID, int channelID) {
            DataTable dt = null;
            using (MainDB db = new MainDB()) {
                if (userID.ToLower().Trim() == "admin")
                    dt = db.StoreProcedure.GetAllCategoryByChannelId(channelID.ToString());
                else
                    dt = db.StoreProcedure.GetCategoryAsTable(userID, channelID.ToString());


            }
            return dt;
        }
        public DataTable GetCategoryByRoleAsTable(string userID, int roleID) {
            DataTable dt = null;
            using (MainDB db = new MainDB()) {
                if (userID.ToLower().Trim() == "admin")
                    dt = db.StoreProcedure.GetAllCategoryByChannelId(DFISYS.API.Config.CurrentChannel.ToString());
                else
                    dt = db.StoreProcedure.GetCategoryByRoleAsTable(userID, roleID.ToString());

            }
            return dt;
        }
        public Role GetRole(string userID) {
            string strCacheName = "GetRole_" + userID;
            Role r = new Role();
            RoleConst r_const = new RoleConst();
            //string sql = "select Distinct Role_ID from View_GetRole where User_ID = '" + userID + "'";
            DataTable dt = GetFromCache(strCacheName);
            if (dt == null) {
                if (userID.ToLower().Trim() == "admin") {
                    using (MainDB db = new MainDB()) {
                        dt = db.StoreProcedure.vc_Execute_Sql("select Role_ID from role");
                    }
                } else {
                    using (MainDB db = new MainDB()) {
                        //dt = db.SelectQuery(sql);
                        //dt = db.StoreProcedure.vc_Execute_Sql(sql);
                        dt = db.StoreProcedure.GetRole(userID);
                    }
                }

                string[] strTableCache = { "Channel_User", "Channel_User_Role", "Role" };
                SetCache(dt, strCacheName, strTableCache);
            }

            if (dt != null && dt.Rows.Count > 0) {
                int role_ID = -1;
                //lấy các field const của permisison
                FieldInfo[] fi = r_const.GetType().GetFields();
                foreach (DataRow dr in dt.Rows) {
                    role_ID = Convert.ToInt32(dr[0]);
                    //duyệt qua các field const
                    foreach (FieldInfo info in fi) {
                        //kiểm tra nếu field nào có giá trị bằng giá trị get ra được từ DB thì set biến cho đối tượng permission
                        if (Convert.ToInt32(info.GetValue(r_const)) == role_ID) {
                            //get ra field của đối tượng permission sau đó gán bằng true
                            if (r.GetType().GetField("is" + info.Name) != null)
                                r.GetType().GetField("is" + info.Name).SetValue(r, true);
                            else
                                r.GetType().GetProperty("is" + info.Name).SetValue(r, true, null);

                            break;
                        }
                    }
                }
            }
            return r;
        }
        /// <summary>
        /// Get ra đối tượng role
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="channelID"></param>
        /// <returns></returns>
        public Role GetRole(string userID, int channelID) {
            Role r = new Role();
            RoleConst r_const = new RoleConst();

            DataTable dt = GetRoleAsTable(userID, channelID);

            if (dt != null && dt.Rows.Count > 0) {
                int role_ID = -1;
                //lấy các field const của permisison
                FieldInfo[] fi = r_const.GetType().GetFields();
                foreach (DataRow dr in dt.Rows) {
                    role_ID = Convert.ToInt32(dr[0]);
                    //duyệt qua các field const
                    foreach (FieldInfo info in fi) {
                        //kiểm tra nếu field nào có giá trị bằng giá trị get ra được từ DB thì set biến cho đối tượng permission
                        if (Convert.ToInt32(info.GetValue(r_const)) == role_ID) {
                            //get ra field của đối tượng permission sau đó gán bằng true
                            if (r.GetType().GetField("is" + info.Name) != null)
                                r.GetType().GetField("is" + info.Name).SetValue(r, true);
                            else
                                r.GetType().GetProperty("is" + info.Name).SetValue(r, true, null);
                            break;
                        }
                    }
                }
            }
            return r;
        }
        /// <summary>
        /// Get ra các permission của user theo kênh
        /// Chú ý: đặt categoryID = -1 nếu không muốn chọn quyền theo chuyên mục
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="channelID">Kenh</param>
        /// <param name="categoryID">Chuyen muc</param>
        /// <returns></returns>
        public DataTable GetPermissionAsTable(string userID, int channelID, int categoryID) {
            DataTable dt = null;
            string sql = "";
            if (categoryID == -1) {
                //sql = "select Distinct Permission_ID,Permission_name from VIEW_PermissionWithoutCategory where User_ID='" + userID + "' and Channel_ID=" + channelID;
                using (MainDB db = new MainDB()) {
                    dt = db.StoreProcedure.GetPermissionAsTableByUserIDAndChannelID(userID, channelID.ToString());
                }
            } else {
                //sql = "select Distinct Permission_ID,Permission_name from VIEW_PermissionWithCategory where User_ID='" + userID + "' and Channel_ID=" + channelID + " and Cat_ID=" + categoryID;
                using (MainDB db = new MainDB()) {
                    dt = db.StoreProcedure.GetPermissionAsTableByUserIDAndChannelIDAndCatID(userID, channelID.ToString(), categoryID.ToString());
                }
            }
            return dt;
        }
        public DataTable GetPermissionAsTable(string userID, int channelID, int roleID, int categoryID) {
            DataTable dt = null;
            string sql = "";
            if (categoryID == -1) {
                //sql = "select Distinct Permission_ID,Permission_name from VIEW_PermissionWithoutCategory where Role_ID=" + roleID + " and User_ID='" + userID + "' and Channel_ID=" + channelID;

                using (MainDB db = new MainDB()) {
                    dt = db.StoreProcedure.GetPermissionAsTableByRoleIDAndUserIDAndChannelID(roleID.ToString(), userID, channelID.ToString());
                }
            } else {
                //sql = "select Distinct Permission_ID,Permission_name from VIEW_PermissionWithCategory where Role_ID=" + roleID + " and User_ID='" + userID + "' and Channel_ID=" + channelID + " and Cat_ID=" + categoryID;
                using (MainDB db = new MainDB()) {
                    dt = db.StoreProcedure.GetPermissionAsTableByRoleIDAndUserIDAndChannelIDAndCatID(roleID.ToString(), userID, channelID.ToString(), categoryID.ToString());
                }

            }
            //using (MainDB db = new MainDB())
            //{
            //    //dt = db.SelectQuery(sql);
            //    dt = db.StoreProcedure.vc_Execute_Sql(sql);
            //}
            return dt;
        }
        public DataTable GetPermissionAsTable(string userID, int roleID) {
            string strCacheName = "GetPermissionAsTable_" + userID + "_" + roleID;
            DataTable dt = GetFromCache(strCacheName);

            //string sql = "";
            //sql = "select Distinct Permission_ID,Permission_name from VIEW_PermissionWithCategory where Role_ID=" + roleID + " and User_ID='" + userID + "' ";

            // Neu la channelvn hoac Admin tu dong load tat ca cac quyen
            if (userID.ToLower().Trim() == "admin") {
                if (dt == null) {
                    using (MainDB db = new MainDB()) {
                        dt = db.StoreProcedure.GetAllPermission();
                    }
                    SetDataToCache(dt, strCacheName, "Permission");
                }

                // sql = " select * from Permission ";
            } else {
                if (dt == null) {
                    using (MainDB db = new MainDB()) {
                        dt = db.StoreProcedure.GetPermissionAsTableByUserIDAndRoleID(roleID.ToString(), userID);
                    }
                    string[] strTableCache = { "Category", "Channel", "Channel_User", "Channel_User_Role", "Permission", "Role", "Role_Permission", "User", "User_Category", "User_Permission" };
                    SetCache(dt, strCacheName, strTableCache);
                }

            }

            //using (MainDB db = new MainDB())
            //{
            //    //dt = db.SelectQuery(sql);
            //    dt = db.StoreProcedure.vc_Execute_Sql(sql);
            //}
            return dt;
        }
        public DataTable GetPermissionAsTable(string userID) {
            DataTable dt = null;
            //string sql = "";
            //sql = "select Distinct Permission_ID,Permission_name from VIEW_PermissionWithCategory where User_ID='" + userID + "' ";

            // Neu la channelvn hoac Admin tu dong load tat ca cac quyen
            if (userID.ToLower().Trim() == "admin") {
                string strCacheName = "GetAllPermission_" + userID;
                dt = GetFromCache(strCacheName);
                if (dt == null) {
                    using (MainDB db = new MainDB()) {
                        dt = db.StoreProcedure.GetAllPermission();
                    }
                    SetDataToCache(dt, strCacheName, "Permission");
                }

            } else {
                string strCacheName = "GetPermissionAsTable_" + userID;
                dt = GetFromCache(strCacheName);
                if (dt == null) {
                    using (MainDB db = new MainDB()) {
                        dt = db.StoreProcedure.GetPermissionAsTableByUserID(userID);
                    }
                    string[] strTableCache = { "Category", "Channel", "Channel_User", "Channel_User_Role", "Permission", "Role", "Role_Permission", "User", "User_Category", "User_Permission" };
                    SetCache(dt, strCacheName, strTableCache);
                }
            }

            //using (MainDB db = new MainDB())
            //{
            //    //dt = db.SelectQuery(sql);
            //    dt = db.StoreProcedure.vc_Execute_Sql(sql);
            //}
            return dt;
        }
        /// <summary>
        /// Get ra các permission của user theo kênh
        /// Chú ý: đặt categoryID = -1 nếu không muốn chọn quyền theo chuyên mục
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <param name="channelID">kênh</param>
        /// <param name="categoryID">chuyên mục</param>
        /// <returns></returns>
        public Permission GetPermission(string userID, int channelID, int categoryID) {
            Permission p = new Permission();
            PermissionConst p_const = new PermissionConst();
            DataTable dt = GetPermissionAsTable(userID, channelID, categoryID);
            if (dt != null && dt.Rows.Count > 0) {
                int permission_ID = -1;
                //lấy các field const của permisison
                FieldInfo[] fi = p_const.GetType().GetFields();
                foreach (DataRow dr in dt.Rows) {
                    permission_ID = Convert.ToInt32(dr[0]);
                    //duyệt qua các field const
                    foreach (FieldInfo info in fi) {
                        //kiểm tra nếu field nào có giá trị bằng giá trị get ra được từ DB thì set biến cho đối tượng permission
                        if (Convert.ToInt32(info.GetValue(p_const)) == permission_ID) {
                            //get ra field của đối tượng permission sau đó gán bằng true
                            p.GetType().GetField("is" + info.Name).SetValue(p, true);
                            break;
                        }
                    }
                }
            }
            return p;
        }
        /// <summary>
        /// Get các quyền của user không phụ thuộc vào channel và chuyên mục
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns></returns>
        public Permission GetPermission(string userID) {
            Permission p = new Permission();
            PermissionConst p_const = new PermissionConst();
            string strCacheName = "GetPermission1_" + userID;
            DataTable dt = GetFromCache(strCacheName);
            if (dt == null) {
                if (userID.ToLower().Trim() == "admin") {
                    using (MainDB db = new MainDB()) {
                        dt = db.StoreProcedure.vc_Execute_Sql("select Permission_ID from permission");
                    }
                    SetDataToCache(dt, strCacheName, "Permission");
                } else {
                    //string sql = "select Distinct Permission_ID from VIEW_PermissionWithoutCategory where User_ID='" + userID + "'";
                    using (MainDB db = new MainDB()) {
                        dt = db.StoreProcedure.GetPermission(userID.ToString());
                    }
                    string[] strTableCache = { "Channel", "Channel_User", "Channel_User_Role", "Permission", "Role", "User", "User_Permission" };
                    SetCache(dt, strCacheName, strTableCache);
                }

            }

            if (dt != null && dt.Rows.Count > 0) {
                int permission_ID = -1;
                //lấy các field const của permisison
                FieldInfo[] fi = p_const.GetType().GetFields();
                foreach (DataRow dr in dt.Rows) {
                    permission_ID = Convert.ToInt32(dr[0]);
                    //duyệt qua các field const
                    foreach (FieldInfo info in fi) {
                        //kiểm tra nếu field nào có giá trị bằng giá trị get ra được từ DB thì set biến cho đối tượng permission
                        if (Convert.ToInt32(info.GetValue(p_const)) == permission_ID) {
                            //get ra field của đối tượng permission sau đó gán bằng true
                            p.GetType().GetField("is" + info.Name).SetValue(p, true);
                            break;
                        }
                    }
                }
            }
            return p;
        }

        public Permission GetPermission(string userID, int roleID) {
            Permission p = new Permission();
            PermissionConst p_const = new PermissionConst();
            DataTable dt = GetPermissionAsTable(userID, roleID);
            if (dt != null && dt.Rows.Count > 0) {
                int permission_ID = -1;
                //lấy các field const của permisison
                FieldInfo[] fi = p_const.GetType().GetFields();
                foreach (DataRow dr in dt.Rows) {
                    permission_ID = Convert.ToInt32(dr[0]);
                    //duyệt qua các field const
                    foreach (FieldInfo info in fi) {
                        //kiểm tra nếu field nào có giá trị bằng giá trị get ra được từ DB thì set biến cho đối tượng permission
                        if (Convert.ToInt32(info.GetValue(p_const)) == permission_ID) {
                            //get ra field của đối tượng permission sau đó gán bằng true
                            p.GetType().GetField("is" + info.Name).SetValue(p, true);
                            break;
                        }
                    }
                }
            }
            return p;
        }

        public static DataTable GetUsersByPermission(int permissionID) {
            using (MainDB db = new MainDB())
                return db.CallStoredProcedure("s_GetUsersByPermission", new object[] { permissionID }, new string[] { "@permissionID" }, true);
        }

        public static void SetDataToCache(DataTable dataTableToCache, string cacheName, string tableNameInDatabase) {
            SqlCacheDependency sqlDependency = new SqlCacheDependency(GetUserDbName(), tableNameInDatabase);
            HttpContext.Current.Cache.Insert(cacheName, dataTableToCache, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }
        public static void SetDataToCache(int dataToCache, string cacheName, string tableNameInDatabase) {
            SqlCacheDependency sqlDependency = new SqlCacheDependency(GetUserDbName(), tableNameInDatabase);
            HttpContext.Current.Cache.Insert(cacheName, dataToCache, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }
        public static void SetDataToCache(string dataToCache, string cacheName, string tableNameInDatabase) {
            SqlCacheDependency sqlDependency = new SqlCacheDependency(GetUserDbName(), tableNameInDatabase);
            HttpContext.Current.Cache.Insert(cacheName, dataToCache, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }
        public static void SetDataToCache(DataTable[] dataToCache, string cacheName, string tableNameInDatabase) {
            SqlCacheDependency sqlDependency = new SqlCacheDependency(GetUserDbName(), tableNameInDatabase);
            HttpContext.Current.Cache.Insert(cacheName, dataToCache, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }
        public static void SetDataToCache(DataSet dataToCache, string cacheName, string tableNameInDatabase) {
            SqlCacheDependency sqlDependency = new SqlCacheDependency(GetUserDbName(), tableNameInDatabase);
            HttpContext.Current.Cache.Insert(cacheName, dataToCache, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }

        public static void SetCache(DataTable dataCache, string cacheName, string[] tableNameInDatabase) {
            //System.Web.Caching.SqlCacheDependency sqlDep1 = new System.Web.Caching.SqlCacheDependency(Const.DATABASE_NAME, "tblTradeTransaction");
            //System.Web.Caching.SqlCacheDependency sqlDep2 = new System.Web.Caching.SqlCacheDependency(Const.DATABASE_NAME, "tblRemainTransaction");
            System.Web.Caching.SqlCacheDependency[] sqlDep = new SqlCacheDependency[tableNameInDatabase.Length];
            for (int i = 0; i < tableNameInDatabase.Length; i++) {
                sqlDep[i] = new System.Web.Caching.SqlCacheDependency(GetUserDbName(), tableNameInDatabase[i]);
            }
            System.Web.Caching.AggregateCacheDependency agg = new System.Web.Caching.AggregateCacheDependency();
            //agg.Add(sqlDep1, sqlDep2);
            agg.Add(sqlDep);
            HttpContext.Current.Cache.Insert(cacheName, dataCache, agg, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);

        }
        public static DataTable GetFromCache(string cacheName) {
            return HttpContext.Current.Cache[cacheName] as DataTable;
        }
        public static int GetInt32FromCache(string cacheName) {
            return (int)HttpContext.Current.Cache[cacheName];
        }
        public static string GetStringFromCache(string cacheName) {
            return (string)HttpContext.Current.Cache[cacheName];
        }
        public static DataTable[] GetFromCacheAsTableArray(string cacheName) {
            return (DataTable[])HttpContext.Current.Cache[cacheName];
        }
        public static DataSet GetFromCacheAsDataSet(string cacheName) {
            return (DataSet)HttpContext.Current.Cache[cacheName];
        }

        public static string GetUserDbName() {
            return "ChannelvnCore";
        }
    }
}
