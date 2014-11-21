
using System;
using System.Data;

namespace DFISYS.User.Db {
    public abstract class UserCollection_Base {
        // Constants
        public const string User_IDColumnName = "User_ID";
        public const string User_NameColumnName = "User_Name";
        public const string User_PwdColumnName = "User_Pwd";
        public const string User_EmailColumnName = "User_Email";
        public const string User_AddressColumnName = "User_Address";
        public const string User_PhoneNumColumnName = "User_PhoneNum";
        public const string User_ImColumnName = "User_Im";
        public const string User_WebsiteColumnName = "User_Website";
        public const string User_isActiveColumnName = "User_isActive";
        public const string User_CreatedDateColumnName = "User_CreatedDate";
        public const string User_ModifiedDateColumnName = "User_ModifiedDate";

        // Instance fields
        private MainDB _db;

        //public DataTable Searching(string StrCol1, string StrCol2, string StrCmp, DataTable Dt) {
        //    StrProcess ObjStr = new StrProcess();
        //    DataTable DTable = new DataTable("Unknown");
        //    DTable = Dt.Clone();
        //    if (Dt.Columns.Contains(StrCol1) && Dt.Columns.Contains(StrCol2)) {
        //        foreach (DataRow Dr in Dt.Rows) {
        //            if (ObjStr.IsSubString(ObjStr.StandNonUnicode(Dr[StrCol1].ToString()), ObjStr.StandNonUnicode(StrCmp)) || ObjStr.IsSubString(ObjStr.StandNonUnicode(Dr[StrCol2].ToString()), ObjStr.StandNonUnicode(StrCmp))) { DTable.ImportRow(Dr); }
        //        }
        //    }
        //    else DTable = null;
        //    return DTable;
        //}

        public UserCollection_Base(MainDB db) {
            _db = db;
        }

        protected MainDB Database {
            get { return _db; }
        }

        public virtual UserRow[] GetAll() {
            return MapRecords(CreateGetAllCommand());
        }

        public virtual DataTable GetAllAsDataTable() {
            return MapRecordsToDataTable(CreateGetAllCommand());
        }
        protected virtual IDbCommand CreateGetAllCommand() {
            return CreateGetCommand(null, null);
        }

        public UserRow GetRow(string whereSql) {
            int totalRecordCount = -1;
            UserRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
            return 0 == rows.Length ? null : rows[0];
        }

        public UserRow[] GetAsArray(string whereSql, string orderBySql) {
            int totalRecordCount = -1;
            return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
        }

        public virtual UserRow[] GetAsArray(string whereSql, string orderBySql,
                            int startIndex, int length, ref int totalRecordCount) {
            using (IDataReader reader = _db.ExecuteReader(CreateGetCommand(whereSql, orderBySql))) {
                return MapRecords(reader, startIndex, length, ref totalRecordCount);
            }
        }

        public DataTable GetAsDataTable(string whereSql, string orderBySql) {
            int totalRecordCount = -1;
            return GetAsDataTable(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
        }

        public virtual DataTable GetAsDataTable(string whereSql, string orderBySql,
                            int startIndex, int length, ref int totalRecordCount) {
            using (IDataReader reader = _db.ExecuteReader(CreateGetCommand(whereSql, orderBySql))) {
                return MapRecordsToDataTable(reader, startIndex, length, ref totalRecordCount);
            }
        }

        protected virtual IDbCommand CreateGetCommand(string whereSql, string orderBySql) {
            string sql = "SELECT * FROM [dbo].[User]";
            if (null != whereSql && 0 < whereSql.Length)
                sql += " WHERE " + whereSql;
            if (null != orderBySql && 0 < orderBySql.Length)
                sql += " ORDER BY " + orderBySql;
            return _db.CreateCommand(sql);
        }


        public virtual UserRow GetByPrimaryKey(string user_ID) {
            string whereSql = "[User_ID]=" + _db.CreateSqlParameterName("User_ID");
            IDbCommand cmd = CreateGetCommand(whereSql, null);
            AddParameter(cmd, "User_ID", user_ID);
            UserRow[] tempArray = MapRecords(cmd);
            return 0 == tempArray.Length ? null : tempArray[0];
        }

        public virtual void Insert(UserRow value) {
            string sqlStr = "INSERT INTO [dbo].[User] (" +
                "[User_ID], " +
                "[User_Name], " +
                "[User_Pwd], " +
                "[User_Email], " +
                "[User_Address], " +
                "[User_PhoneNum], " +
                "[User_Im], " +
                "[User_Website], " +
                "[User_isActive], " +
                "[User_CreatedDate], " +
                "[User_ModifiedDate]" +
                ") VALUES (" +
                _db.CreateSqlParameterName("User_ID") + ", " +
                _db.CreateSqlParameterName("User_Name") + ", " +
                _db.CreateSqlParameterName("User_Pwd") + ", " +
                _db.CreateSqlParameterName("User_Email") + ", " +
                _db.CreateSqlParameterName("User_Address") + ", " +
                _db.CreateSqlParameterName("User_PhoneNum") + ", " +
                _db.CreateSqlParameterName("User_Im") + ", " +
                _db.CreateSqlParameterName("User_Website") + ", " +
                _db.CreateSqlParameterName("User_isActive") + ", " +
                _db.CreateSqlParameterName("User_CreatedDate") + ", " +
                _db.CreateSqlParameterName("User_ModifiedDate") + ")";
            IDbCommand cmd = _db.CreateCommand(sqlStr);
            AddParameter(cmd, "User_ID", value.User_ID);
            AddParameter(cmd, "User_Name", value.User_Name);
            AddParameter(cmd, "User_Pwd", value.User_Pwd);
            AddParameter(cmd, "User_Email", value.User_Email);
            AddParameter(cmd, "User_Address", value.User_Address);
            AddParameter(cmd, "User_PhoneNum", value.User_PhoneNum);
            AddParameter(cmd, "User_Im", value.User_Im);
            AddParameter(cmd, "User_Website", value.User_Website);
            AddParameter(cmd, "User_isActive",
                value.IsUser_isActiveNull ? DBNull.Value : (object)value.User_isActive);
            AddParameter(cmd, "User_CreatedDate",
                value.IsUser_CreatedDateNull ? DBNull.Value : (object)value.User_CreatedDate);
            AddParameter(cmd, "User_ModifiedDate",
                value.IsUser_ModifiedDateNull ? DBNull.Value : (object)value.User_ModifiedDate);
            cmd.ExecuteNonQuery();
        }
        public virtual bool Update(UserRow value) {
            string sqlStr = "UPDATE [dbo].[User] SET " +
                "[User_Name]=" + _db.CreateSqlParameterName("User_Name") + ", " +
                "[User_Pwd]=" + _db.CreateSqlParameterName("User_Pwd") + ", " +
                "[User_Email]=" + _db.CreateSqlParameterName("User_Email") + ", " +
                "[User_Address]=" + _db.CreateSqlParameterName("User_Address") + ", " +
                "[User_PhoneNum]=" + _db.CreateSqlParameterName("User_PhoneNum") + ", " +
                "[User_Im]=" + _db.CreateSqlParameterName("User_Im") + ", " +
                "[User_Website]=" + _db.CreateSqlParameterName("User_Website") + ", " +
                "[User_isActive]=" + _db.CreateSqlParameterName("User_isActive") + ", " +
                "[User_CreatedDate]=" + _db.CreateSqlParameterName("User_CreatedDate") + ", " +
                "[User_ModifiedDate]=" + _db.CreateSqlParameterName("User_ModifiedDate") +
                " WHERE " +
                "[User_ID]=" + _db.CreateSqlParameterName("User_ID");
            IDbCommand cmd = _db.CreateCommand(sqlStr);
            AddParameter(cmd, "User_Name", value.User_Name);
            AddParameter(cmd, "User_Pwd", value.User_Pwd);
            AddParameter(cmd, "User_Email", value.User_Email);
            AddParameter(cmd, "User_Address", value.User_Address);
            AddParameter(cmd, "User_PhoneNum", value.User_PhoneNum);
            AddParameter(cmd, "User_Im", value.User_Im);
            AddParameter(cmd, "User_Website", value.User_Website);
            AddParameter(cmd, "User_isActive",
                value.IsUser_isActiveNull ? DBNull.Value : (object)value.User_isActive);
            AddParameter(cmd, "User_CreatedDate",
                value.IsUser_CreatedDateNull ? DBNull.Value : (object)value.User_CreatedDate);
            AddParameter(cmd, "User_ModifiedDate",
                value.IsUser_ModifiedDateNull ? DBNull.Value : (object)value.User_ModifiedDate);
            AddParameter(cmd, "User_ID", value.User_ID);
            return 0 != cmd.ExecuteNonQuery();
        }

        public void Update(DataTable table) {
            Update(table, true);
        }

        public virtual void Update(DataTable table, bool acceptChanges) {
            DataRowCollection rows = table.Rows;
            for (int i = rows.Count - 1; i >= 0; i--) {
                DataRow row = rows[i];
                switch (row.RowState) {
                    case DataRowState.Added:
                        Insert(MapRow(row));
                        if (acceptChanges)
                            row.AcceptChanges();
                        break;

                    case DataRowState.Deleted:
                        // Temporary reject changes to be able to access to the PK column(s)
                        row.RejectChanges();
                        try {
                            DeleteByPrimaryKey((string)row["User_ID"]);
                        }
                        finally {
                            row.Delete();
                        }
                        if (acceptChanges)
                            row.AcceptChanges();
                        break;

                    case DataRowState.Modified:
                        Update(MapRow(row));
                        if (acceptChanges)
                            row.AcceptChanges();
                        break;
                }
            }
        }

        public bool Delete(UserRow value) {
            return DeleteByPrimaryKey(value.User_ID);
        }



        public virtual bool DeleteByPrimaryKey(string user_ID) {
            string whereSql = "[User_ID]=" + _db.CreateSqlParameterName("User_ID");
            IDbCommand cmd = CreateDeleteCommand(whereSql);
            AddParameter(cmd, "User_ID", user_ID);
            return 0 < cmd.ExecuteNonQuery();
        }

        public int Delete(string whereSql) {
            return CreateDeleteCommand(whereSql).ExecuteNonQuery();
        }

        protected virtual IDbCommand CreateDeleteCommand(string whereSql) {
            string sql = "DELETE FROM [dbo].[User]";
            if (null != whereSql && 0 < whereSql.Length)
                sql += " WHERE " + whereSql;
            return _db.CreateCommand(sql);
        }

        public int DeleteAll() {
            return Delete("");
        }

        protected UserRow[] MapRecords(IDbCommand command) {
            using (IDataReader reader = _db.ExecuteReader(command)) {
                return MapRecords(reader);
            }
        }


        protected UserRow[] MapRecords(IDataReader reader) {
            int totalRecordCount = -1;
            return MapRecords(reader, 0, int.MaxValue, ref totalRecordCount);
        }

        protected virtual UserRow[] MapRecords(IDataReader reader,
                                        int startIndex, int length, ref int totalRecordCount) {
            if (0 > startIndex)
                throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
            if (0 > length)
                throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

            int user_IDColumnIndex = reader.GetOrdinal("User_ID");
            int user_NameColumnIndex = reader.GetOrdinal("User_Name");
            int user_PwdColumnIndex = reader.GetOrdinal("User_Pwd");
            int user_EmailColumnIndex = reader.GetOrdinal("User_Email");
            int user_AddressColumnIndex = reader.GetOrdinal("User_Address");
            int user_PhoneNumColumnIndex = reader.GetOrdinal("User_PhoneNum");
            int user_ImColumnIndex = reader.GetOrdinal("User_Im");
            int user_WebsiteColumnIndex = reader.GetOrdinal("User_Website");
            int user_isActiveColumnIndex = reader.GetOrdinal("User_isActive");
            int user_CreatedDateColumnIndex = reader.GetOrdinal("User_CreatedDate");
            int user_ModifiedDateColumnIndex = reader.GetOrdinal("User_ModifiedDate");

            System.Collections.ArrayList recordList = new System.Collections.ArrayList();
            int ri = -startIndex;
            while (reader.Read()) {
                ri++;
                if (ri > 0 && ri <= length) {
                    UserRow record = new UserRow();
                    recordList.Add(record);

                    record.User_ID = Convert.ToString(reader.GetValue(user_IDColumnIndex));
                    if (!reader.IsDBNull(user_NameColumnIndex))
                        record.User_Name = Convert.ToString(reader.GetValue(user_NameColumnIndex));
                    if (!reader.IsDBNull(user_PwdColumnIndex))
                        record.User_Pwd = Convert.ToString(reader.GetValue(user_PwdColumnIndex));
                    if (!reader.IsDBNull(user_EmailColumnIndex))
                        record.User_Email = Convert.ToString(reader.GetValue(user_EmailColumnIndex));
                    if (!reader.IsDBNull(user_AddressColumnIndex))
                        record.User_Address = Convert.ToString(reader.GetValue(user_AddressColumnIndex));
                    if (!reader.IsDBNull(user_PhoneNumColumnIndex))
                        record.User_PhoneNum = Convert.ToString(reader.GetValue(user_PhoneNumColumnIndex));
                    if (!reader.IsDBNull(user_ImColumnIndex))
                        record.User_Im = Convert.ToString(reader.GetValue(user_ImColumnIndex));
                    if (!reader.IsDBNull(user_WebsiteColumnIndex))
                        record.User_Website = Convert.ToString(reader.GetValue(user_WebsiteColumnIndex));
                    if (!reader.IsDBNull(user_isActiveColumnIndex))
                        record.User_isActive = Convert.ToBoolean(reader.GetValue(user_isActiveColumnIndex));
                    if (!reader.IsDBNull(user_CreatedDateColumnIndex))
                        record.User_CreatedDate = Convert.ToDateTime(reader.GetValue(user_CreatedDateColumnIndex));
                    if (!reader.IsDBNull(user_ModifiedDateColumnIndex))
                        record.User_ModifiedDate = Convert.ToDateTime(reader.GetValue(user_ModifiedDateColumnIndex));

                    if (ri == length && 0 != totalRecordCount)
                        break;
                }
            }

            totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
            return (UserRow[])(recordList.ToArray(typeof(UserRow)));
        }

        protected DataTable MapRecordsToDataTable(IDbCommand command) {
            using (IDataReader reader = _db.ExecuteReader(command)) {
                return MapRecordsToDataTable(reader);
            }
        }

        protected DataTable MapRecordsToDataTable(IDataReader reader) {
            int totalRecordCount = 0;
            return MapRecordsToDataTable(reader, 0, int.MaxValue, ref totalRecordCount);
        }

        protected virtual DataTable MapRecordsToDataTable(IDataReader reader,
                                        int startIndex, int length, ref int totalRecordCount) {
            if (0 > startIndex)
                throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
            if (0 > length)
                throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

            int columnCount = reader.FieldCount;
            int ri = -startIndex;

            DataTable dataTable = CreateDataTable();
            dataTable.BeginLoadData();
            object[] values = new object[columnCount];

            while (reader.Read()) {
                ri++;
                if (ri > 0 && ri <= length) {
                    reader.GetValues(values);
                    dataTable.LoadDataRow(values, true);

                    if (ri == length && 0 != totalRecordCount)
                        break;
                }
            }
            dataTable.EndLoadData();

            totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
            return dataTable;
        }
        protected virtual UserRow MapRow(DataRow row) {
            UserRow mappedObject = new UserRow();
            DataTable dataTable = row.Table;
            DataColumn dataColumn;
            // Column "User_ID"
            dataColumn = dataTable.Columns["User_ID"];
            if (!row.IsNull(dataColumn))
                mappedObject.User_ID = (string)row[dataColumn];
            // Column "User_Name"
            dataColumn = dataTable.Columns["User_Name"];
            if (!row.IsNull(dataColumn))
                mappedObject.User_Name = (string)row[dataColumn];
            // Column "User_Pwd"
            dataColumn = dataTable.Columns["User_Pwd"];
            if (!row.IsNull(dataColumn))
                mappedObject.User_Pwd = (string)row[dataColumn];
            // Column "User_Email"
            dataColumn = dataTable.Columns["User_Email"];
            if (!row.IsNull(dataColumn))
                mappedObject.User_Email = (string)row[dataColumn];
            // Column "User_Address"
            dataColumn = dataTable.Columns["User_Address"];
            if (!row.IsNull(dataColumn))
                mappedObject.User_Address = (string)row[dataColumn];
            // Column "User_PhoneNum"
            dataColumn = dataTable.Columns["User_PhoneNum"];
            if (!row.IsNull(dataColumn))
                mappedObject.User_PhoneNum = (string)row[dataColumn];
            // Column "User_Im"
            dataColumn = dataTable.Columns["User_Im"];
            if (!row.IsNull(dataColumn))
                mappedObject.User_Im = (string)row[dataColumn];
            // Column "User_Website"
            dataColumn = dataTable.Columns["User_Website"];
            if (!row.IsNull(dataColumn))
                mappedObject.User_Website = (string)row[dataColumn];
            // Column "User_isActive"
            dataColumn = dataTable.Columns["User_isActive"];
            if (!row.IsNull(dataColumn))
                mappedObject.User_isActive = (bool)row[dataColumn];
            // Column "User_CreatedDate"
            dataColumn = dataTable.Columns["User_CreatedDate"];
            if (!row.IsNull(dataColumn))
                mappedObject.User_CreatedDate = (System.DateTime)row[dataColumn];
            // Column "User_ModifiedDate"
            dataColumn = dataTable.Columns["User_ModifiedDate"];
            if (!row.IsNull(dataColumn))
                mappedObject.User_ModifiedDate = (System.DateTime)row[dataColumn];
            return mappedObject;
        }

        protected virtual DataTable CreateDataTable() {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "User";
            DataColumn dataColumn;
            dataColumn = dataTable.Columns.Add("User_ID", typeof(string));
            dataColumn.MaxLength = 200;
            dataColumn.AllowDBNull = false;
            dataColumn = dataTable.Columns.Add("User_Name", typeof(string));
            dataColumn.MaxLength = 1000;
            dataColumn = dataTable.Columns.Add("User_Pwd", typeof(string));
            dataColumn.MaxLength = 50;
            dataColumn = dataTable.Columns.Add("User_Email", typeof(string));
            dataColumn.MaxLength = 50;
            dataColumn = dataTable.Columns.Add("User_Address", typeof(string));
            dataColumn.MaxLength = 50;
            dataColumn = dataTable.Columns.Add("User_PhoneNum", typeof(string));
            dataColumn.MaxLength = 50;
            dataColumn = dataTable.Columns.Add("User_Im", typeof(string));
            dataColumn.MaxLength = 50;
            dataColumn = dataTable.Columns.Add("User_Website", typeof(string));
            dataColumn.MaxLength = 50;
            dataColumn = dataTable.Columns.Add("User_isActive", typeof(bool));
            dataColumn = dataTable.Columns.Add("User_CreatedDate", typeof(System.DateTime));
            dataColumn = dataTable.Columns.Add("User_ModifiedDate", typeof(System.DateTime));
            return dataTable;
        }

        protected virtual IDbDataParameter AddParameter(IDbCommand cmd, string paramName, object value) {
            IDbDataParameter parameter;
            switch (paramName) {
                case "User_ID":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                case "User_Name":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                case "User_Pwd":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                case "User_Email":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                case "User_Address":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                case "User_PhoneNum":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                case "User_Im":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                case "User_Website":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                case "User_isActive":
                    parameter = _db.AddParameter(cmd, paramName, DbType.Boolean, value);
                    break;

                case "User_CreatedDate":
                    parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
                    break;

                case "User_ModifiedDate":
                    parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
                    break;

                default:
                    throw new ArgumentException("Unknown parameter name (" + paramName + ").");
            }
            return parameter;
        }
    }
}
