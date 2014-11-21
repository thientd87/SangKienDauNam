using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DFISYS.User.Db {
    public class StoreProcedure {
        private MainDB _db;

        public StoreProcedure(MainDB db) {
            _db = db;
        }

        public MainDB Database {
            get { return _db; }
        }

        protected internal DataTable CreateDataTable(IDbCommand command) {
            DataTable dataTable = new DataTable();
            new System.Data.SqlClient.SqlDataAdapter((System.Data.SqlClient.SqlCommand)command).Fill(dataTable);
            return dataTable;
        }

        public DataTable vc_Execute_Sql(String sSql) {
            IDbCommand cmd = _db.CreateCommand("vc_Execute_Sql", true);
            _db.AddParameter(cmd, "sSql", DbType.String, sSql);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable User_GetListByWhereForPaging(string whereSql) {
            IDbCommand cmd = _db.CreateCommand("User_GetListByWhereForPaging", true);
            _db.AddParameter(cmd, "whereSql", DbType.String, whereSql);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable CanAccess(string userid) {
            IDbCommand cmd = _db.CreateCommand("CanAccess", true);
            _db.AddParameter(cmd, "userid", DbType.String, userid);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable GetParentCategoryAssigned(string userid, string channelID) {
            IDbCommand cmd = _db.CreateCommand("CMS_GetParentCategoryAssigned", true);
            _db.AddParameter(cmd, "userid", DbType.String, userid);
            _db.AddParameter(cmd, "channelID", DbType.String, channelID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable GetParentCategoryAssigned(string userid, string channelID, int editionType)
        {
            IDbCommand cmd = _db.CreateCommand("CMS_GetParentCategoryAssignedWithEditionType", true);
            _db.AddParameter(cmd, "userid", DbType.String, userid);
            _db.AddParameter(cmd, "channelID", DbType.String, channelID);
            _db.AddParameter(cmd, "editionType", DbType.Int32, editionType);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable GetChannelAsTable(string userid) {
            IDbCommand cmd = _db.CreateCommand("GetChannelAsTable", true);
            _db.AddParameter(cmd, "userid", DbType.String, userid);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable GetAllRole() {
            IDbCommand cmd = _db.CreateCommand("GetAllRole", true);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable GetRoleDesAsString(string userid, string channelID) {
            IDbCommand cmd = _db.CreateCommand("CMS_GetRoleDesAsString", true);
            _db.AddParameter(cmd, "userid", DbType.String, userid);
            _db.AddParameter(cmd, "channelID", DbType.String, channelID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable GetRoleAsTable(string userid, string channelID) {
            IDbCommand cmd = _db.CreateCommand("CMS_GetRoleAsTable", true);
            _db.AddParameter(cmd, "userid", DbType.String, userid);
            _db.AddParameter(cmd, "channelID", DbType.String, channelID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable GetCategoryAsTable(string userid, string channelID) {
            IDbCommand cmd = _db.CreateCommand("GetCategoryAsTable", true);
            _db.AddParameter(cmd, "userid", DbType.String, userid);
            _db.AddParameter(cmd, "channelID", DbType.String, channelID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable GetAllCategoryByChannelId(string channelID) {
            IDbCommand cmd = _db.CreateCommand("CMS_GetAllCategoryByChannelId", true);
            _db.AddParameter(cmd, "channelID", DbType.String, channelID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable GetCategoryByRoleAsTable(string userid, string roleID) {
            IDbCommand cmd = _db.CreateCommand("CMS_GetCategoryByRoleAsTable", true);
            _db.AddParameter(cmd, "userid", DbType.String, userid);
            _db.AddParameter(cmd, "roleID", DbType.String, roleID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable GetRole(string userid) {
            IDbCommand cmd = _db.CreateCommand("GetRole", true);
            _db.AddParameter(cmd, "userid", DbType.String, userid);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable GetPermissionAsTableByUserIDAndChannelID(string userid, string channelID) {
            IDbCommand cmd = _db.CreateCommand("CMS_GetPermissionAsTableByUserIDAndChannelID", true);
            _db.AddParameter(cmd, "userid", DbType.String, userid);
            _db.AddParameter(cmd, "channelID", DbType.String, channelID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable GetPermissionAsTableByUserIDAndChannelIDAndCatID(string userid, string channelID, string catID) {
            IDbCommand cmd = _db.CreateCommand("GetPermissionAsTableByUserIDAndChannelIDAndCatID", true);
            _db.AddParameter(cmd, "userid", DbType.String, userid);
            _db.AddParameter(cmd, "channelID", DbType.String, channelID);
            _db.AddParameter(cmd, "catID", DbType.String, catID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable GetPermissionAsTableByRoleIDAndUserIDAndChannelID(string roleID, string userid, string channelID) {
            IDbCommand cmd = _db.CreateCommand("GetPermissionAsTableByRoleIDAndUserIDAndChannelID", true);
            _db.AddParameter(cmd, "roleID", DbType.String, roleID);
            _db.AddParameter(cmd, "userid", DbType.String, userid);
            _db.AddParameter(cmd, "channelID", DbType.String, channelID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable GetPermissionAsTableByRoleIDAndUserIDAndChannelIDAndCatID(string roleID, string userid, string channelID, string catID) {
            IDbCommand cmd = _db.CreateCommand("CMS_GetPermissionAsTableByRoleIDAndUserIDAndChannelIDAndCatID", true);
            _db.AddParameter(cmd, "roleID", DbType.String, roleID);
            _db.AddParameter(cmd, "userid", DbType.String, userid);
            _db.AddParameter(cmd, "channelID", DbType.String, channelID);
            _db.AddParameter(cmd, "catID", DbType.String, catID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable GetPermissionAsTableByUserIDAndRoleID(string roleID, string userid) {
            IDbCommand cmd = _db.CreateCommand("GetPermissionAsTableByUserIDAndRoleID", true);
            _db.AddParameter(cmd, "roleID", DbType.String, roleID);
            _db.AddParameter(cmd, "userid", DbType.String, userid);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable GetAllPermission() {
            IDbCommand cmd = _db.CreateCommand("CMS_GetAllPermission", true);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable GetPermissionAsTableByUserID(string userid) {
            IDbCommand cmd = _db.CreateCommand("GetPermissionAsTableByUserID", true);
            _db.AddParameter(cmd, "userid", DbType.String, userid);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable GetPermission(string userid) {
            IDbCommand cmd = _db.CreateCommand("GetPermission", true);
            _db.AddParameter(cmd, "userid", DbType.String, userid);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public int Box_Insert(string Box) {
            IDbCommand cmd = _db.CreateCommand("InsertBox", true);
            _db.AddParameter(cmd, "Box", DbType.String, Box);
            //DataTable table = _db.CreateDataTable(cmd);
            //return table;
            cmd.ExecuteNonQuery();
            return 1;
            //string sqlStr = "Insert into Box(Box_Name) values (N'" + Box + "') ";
            //IDbCommand cmd = _db.CreateCommand(sqlStr);
            //return Convert.ToInt32(cmd.ExecuteScalar());
        }
        public DataTable News_GetListBox(string Where) {
            IDbCommand cmd = _db.CreateCommand("News_GetListBox", true);
            _db.AddParameter(cmd, "Where", DbType.String, Where);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable News_GetPermisslist(int BoxID) {
            IDbCommand cmd = _db.CreateCommand("News_GetPermisslist", true);
            _db.AddParameter(cmd, "BoxID", DbType.Int32, BoxID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable News_GetListPerBox(string Where) {
            IDbCommand cmd = _db.CreateCommand("News_GetListPerBox", true);
            _db.AddParameter(cmd, "Where", DbType.String, Where);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable News_GetPermiss(int BoxID) {
            IDbCommand cmd = _db.CreateCommand("News_GetPermiss", true);
            _db.AddParameter(cmd, "BoxID", DbType.Int32, BoxID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable News_GetBoxUser(string UserID) {
            IDbCommand cmd = _db.CreateCommand("News_GetBoxUser", true);
            _db.AddParameter(cmd, "UserID", DbType.String, UserID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public string Box_Select(int BoxID) {
            //string sqlStr = "Select  Box_Name From Box where Box_ID = " + BoxID;
            //IDbCommand cmd = _db.CreateCommand(sqlStr);
            IDbCommand cmd = _db.CreateCommand("News_GetBox_Name", true);
            _db.AddParameter(cmd, "BoxID", DbType.Int32, BoxID);
            return Convert.ToString(cmd.ExecuteScalar());
        }
        public int Box_Update(int BoxID, string Box) {
            //string sqlStr = "Update Box set Box_Name = N'" + Box + "' where Box_ID =" + BoxID;
            //IDbCommand cmd = _db.CreateCommand(sqlStr);
            //cmd.ExecuteNonQuery();
            IDbCommand cmd = _db.CreateCommand("UpdateBoxName", true);
            _db.AddParameter(cmd, "BoxID", DbType.Int32, BoxID);
            _db.AddParameter(cmd, "Box", DbType.String, Box);
            return Convert.ToInt32(cmd.ExecuteScalar());
            //return Convert.ToInt32(cmd.ExecuteScalar());
            return 1;

        }
        public int Box_UpdateIndex(int Box_ID, int Index) {
            // string sqlStr = "Update Box set Box_Index = " + Index + " where Box_ID =" + BP_ID;
            //IDbCommand cmd = _db.CreateCommand(sqlStr);
            IDbCommand cmd = _db.CreateCommand("UpdateIndex", true);
            _db.AddParameter(cmd, "Box_ID", DbType.Int32, Box_ID);
            _db.AddParameter(cmd, "Index", DbType.Int32, Index);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        public int PerBox_UpdateIndex(int BP_ID, int Index) {
            //string sqlStr = "Update Box_Permission set BP_Index = " + Index + " where BP_ID =" + BP_ID;
            //IDbCommand cmd = _db.CreateCommand(sqlStr);
            IDbCommand cmd = _db.CreateCommand("PerBox_UpdateIndex", true);
            _db.AddParameter(cmd, "BP_ID", DbType.Int32, BP_ID);
            _db.AddParameter(cmd, "Index", DbType.Int32, Index);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        public int Box_Delete(int BoxID) {
            //string sqlStr = "Delete from Box where Box_ID = " + BoxID ;
            //IDbCommand cmd = _db.CreateCommand(sqlStr);
            IDbCommand cmd = _db.CreateCommand("Box_Delete", true);
            _db.AddParameter(cmd, "BoxID", DbType.Int32, BoxID);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        public int BoxPer_Delete(int BoxPerID) {
            IDbCommand cmd = _db.CreateCommand("BoxPer_Delete", true);
            _db.AddParameter(cmd, "BoxPerID", DbType.Int32, BoxPerID);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        public int PerBox_Delete(int BP_ID) {
            //string sqlStr = "Delete from Box_Permission where BP_ID = " + BP_ID;
            //IDbCommand cmd = _db.CreateCommand(sqlStr);
            IDbCommand cmd = _db.CreateCommand("PerBox_Delete", true);
            _db.AddParameter(cmd, "BP_ID", DbType.Int32, BP_ID);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        public int CheckPermiss(int Box, int Per) {
            IDbCommand cmd = _db.CreateCommand("CheckPermiss", true);
            _db.AddParameter(cmd, "Box", DbType.Int32, Box);
            _db.AddParameter(cmd, "Per", DbType.Int32, Per);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        public int CheckBox_User(string strUser, int Box_ID) {
            IDbCommand cmd = _db.CreateCommand("CheckBox_User", true);
            _db.AddParameter(cmd, "strUser", DbType.String, strUser);
            _db.AddParameter(cmd, "Box_ID", DbType.Int32, Box_ID);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        public int BoxPer_Insert(int Box, int Per, int index, string url, string control, int Iscount, int Isprefix, int Channel) {
            // SqlTransaction SQlTran;
            IDbCommand cmd = _db.CreateCommand("InsertBoxPer", true);
            _db.AddParameter(cmd, "Box", DbType.Int32, Box);
            _db.AddParameter(cmd, "Per", DbType.Int32, Per);
            _db.AddParameter(cmd, "index", DbType.Int32, index);
            _db.AddParameter(cmd, "url", DbType.String, url);
            _db.AddParameter(cmd, "control", DbType.String, control);
            _db.AddParameter(cmd, "Iscount", DbType.Int32, Iscount);
            _db.AddParameter(cmd, "Isprefix", DbType.Int32, Isprefix);
            _db.AddParameter(cmd, "Channel", DbType.Int32, Channel);
            cmd.ExecuteNonQuery();
            // SQlTran.Commit;
            return 1;
        }
        public int Permiss_Insert(int Box, int Per, int boxIndex, string Url, string Control) {
            // SqlTransaction SQlTran;
            IDbCommand cmd = _db.CreateCommand("InsertPermiss", true);
            _db.AddParameter(cmd, "Box", DbType.Int32, Box);
            _db.AddParameter(cmd, "Per", DbType.Int32, Per);
            _db.AddParameter(cmd, "boxIndex", DbType.Int32, boxIndex);
            _db.AddParameter(cmd, "Url", DbType.String, Url);
            _db.AddParameter(cmd, "Control", DbType.String, Control);
            cmd.ExecuteNonQuery();
            // SQlTran.Commit;
            return 1;
        }
        public int BoxUser_Insert(string strUser, int Box_ID) {
            IDbCommand cmd = _db.CreateCommand("InsertBoxUser", true);
            _db.AddParameter(cmd, "strUser", DbType.String, strUser);
            _db.AddParameter(cmd, "Box_ID", DbType.Int32, Box_ID);
            cmd.ExecuteNonQuery();
            return 1;
        }
        public int BoxPer_Update(int BP_ID, int Box, int Per, int index, string url, string control, int Iscount, int Isprefix, int Channel) {
            // SqlTransaction SQlTran;
            IDbCommand cmd = _db.CreateCommand("UpdateBoxPer", true);
            _db.AddParameter(cmd, "BP_ID", DbType.Int32, BP_ID);
            _db.AddParameter(cmd, "Box", DbType.Int32, Box);
            _db.AddParameter(cmd, "Per", DbType.Int32, Per);
            _db.AddParameter(cmd, "index", DbType.Int32, index);
            _db.AddParameter(cmd, "url", DbType.String, url);
            _db.AddParameter(cmd, "control", DbType.String, control);
            _db.AddParameter(cmd, "Iscount", DbType.Int32, Iscount);
            _db.AddParameter(cmd, "Isprefix", DbType.Int32, Isprefix);
            _db.AddParameter(cmd, "Channel", DbType.Int32, Channel);
            cmd.ExecuteNonQuery();
            // SQlTran.Commit;
            return 1;
        }
        public DataRow BoxPer_GetDataRow(int BP_ID) {
            // SqlTransaction SQlTran;
            IDbCommand cmd = _db.CreateCommand("SelectBoxPer", true);
            _db.AddParameter(cmd, "BP_ID", DbType.Int32, BP_ID);
            //cmd.ExecuteNonQuery();
            DataTable dataTable = CreateDataTable(cmd);
            //return dataTable;
            //return BoxPer_GetDataRow;
            DataRow iRow;
            if (dataTable.Rows.Count > 0) {
                iRow = dataTable.Rows[0];
            }
            else {
                iRow = null;
            }
            dataTable.Dispose();
            return iRow;
        }

        public bool Category_Delete(int catId) {
            IDbCommand cmd = _db.CreateCommand("Category_Delete", true);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, catId);
            return cmd.ExecuteNonQuery() == 1;
        }

        public void UpdateLastAccessTime(string UserName) {
            IDbCommand cmd = _db.CreateCommand("CMS_User_UpdateLastAccessTime", true);
            _db.AddParameter(cmd, "UserName", DbType.String, UserName);
            cmd.ExecuteNonQuery();
        }

        public void UpdateUserHistory(string UserName, string CurrentPassword) {
            IDbCommand cmd = _db.CreateCommand("CMS_UpdateUserHistory", true);
            _db.AddParameter(cmd, "UserName", DbType.String, UserName);
            _db.AddParameter(cmd, "CurrentPass", DbType.String, CurrentPassword);
            cmd.ExecuteNonQuery();
        }

        public void UpdateLastChanged(string UserName, string NewPassword) {
            IDbCommand cmd = _db.CreateCommand("CMS_User_UpdateLastChanged", true);
            _db.AddParameter(cmd, "UserName", DbType.String, UserName);
            _db.AddParameter(cmd, "NewPass", DbType.String, NewPassword);
            cmd.ExecuteNonQuery();
        }

        public DataTable GetUserHistoryDetails(string UserName) {
            IDbCommand cmd = _db.CreateCommand("CMS_User_GetUserHistoryDetails", true);
            _db.AddParameter(cmd, "UserName", DbType.String, UserName);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }


    }
}
