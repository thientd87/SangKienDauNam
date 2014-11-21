
using System;
using System.Data;

namespace DFISYS.User.Db {
    public abstract class PermissionCollection_Base {
        // Constants
        public const string Permission_IDColumnName = "Permission_ID";
        public const string Permission_NameColumnName = "Permission_Name";

        // Instance fields
        private MainDB _db;

        public PermissionCollection_Base(MainDB db) {
            _db = db;
        }

        protected MainDB Database {
            get { return _db; }
        }

        public virtual PermissionRow[] GetAll() {
            return MapRecords(CreateGetAllCommand());
        }

        public virtual DataTable GetAllAsDataTable() {
            return MapRecordsToDataTable(CreateGetAllCommand());
        }
        protected virtual IDbCommand CreateGetAllCommand() {
            return CreateGetCommand(null, null);
        }

        public PermissionRow GetRow(string whereSql) {
            int totalRecordCount = -1;
            PermissionRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
            return 0 == rows.Length ? null : rows[0];
        }

        public PermissionRow[] GetAsArray(string whereSql, string orderBySql) {
            int totalRecordCount = -1;
            return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
        }

        public virtual PermissionRow[] GetAsArray(string whereSql, string orderBySql,
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
            string sql = "SELECT Permission_ID, Permission_Name FROM [dbo].[Permission]";
            if (null != whereSql && 0 < whereSql.Length)
                sql += " WHERE " + whereSql;
            if (null != orderBySql && 0 < orderBySql.Length)
                sql += " ORDER BY " + orderBySql;
            return _db.CreateCommand(sql);
        }


        public virtual PermissionRow GetByPrimaryKey(int permission_ID) {
            string whereSql = "[Permission_ID]=" + _db.CreateSqlParameterName("Permission_ID");
            IDbCommand cmd = CreateGetCommand(whereSql, null);
            AddParameter(cmd, "Permission_ID", permission_ID);
            PermissionRow[] tempArray = MapRecords(cmd);
            return 0 == tempArray.Length ? null : tempArray[0];
        }

        public virtual void Insert(PermissionRow value) {
            string sqlStr = "INSERT INTO [dbo].[Permission] (" +
                "[Permission_Name]" +
                ") VALUES (" +
                _db.CreateSqlParameterName("Permission_Name") + ");SELECT @@IDENTITY";
            IDbCommand cmd = _db.CreateCommand(sqlStr);
            AddParameter(cmd, "Permission_Name", value.Permission_Name);
            value.Permission_ID = Convert.ToInt32(cmd.ExecuteScalar());
        }
        public virtual bool Update(PermissionRow value) {
            string sqlStr = "UPDATE [dbo].[Permission] SET " +
                "[Permission_Name]=" + _db.CreateSqlParameterName("Permission_Name") +
                " WHERE " +
                "[Permission_ID]=" + _db.CreateSqlParameterName("Permission_ID");
            IDbCommand cmd = _db.CreateCommand(sqlStr);
            AddParameter(cmd, "Permission_Name", value.Permission_Name);
            AddParameter(cmd, "Permission_ID", value.Permission_ID);
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
                            DeleteByPrimaryKey((int)row["Permission_ID"]);
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

        public bool Delete(PermissionRow value) {
            return DeleteByPrimaryKey(value.Permission_ID);
        }
        
        public virtual bool DeleteByPrimaryKey(int permission_ID) {
            string whereSql = "[Permission_ID]=" + _db.CreateSqlParameterName("Permission_ID");
            IDbCommand cmd = CreateDeleteCommand(whereSql);
            AddParameter(cmd, "Permission_ID", permission_ID);
            return 0 < cmd.ExecuteNonQuery();
        }

        public int Delete(string whereSql) {
            return CreateDeleteCommand(whereSql).ExecuteNonQuery();
        }

        protected virtual IDbCommand CreateDeleteCommand(string whereSql) {
            string sql = "DELETE FROM [dbo].[Permission]";
            if (null != whereSql && 0 < whereSql.Length)
                sql += " WHERE " + whereSql;
            return _db.CreateCommand(sql);
        }

        public int DeleteAll() {
            return Delete("");
        }

        protected PermissionRow[] MapRecords(IDbCommand command) {
            using (IDataReader reader = _db.ExecuteReader(command)) {
                return MapRecords(reader);
            }
        }


        protected PermissionRow[] MapRecords(IDataReader reader) {
            int totalRecordCount = -1;
            return MapRecords(reader, 0, int.MaxValue, ref totalRecordCount);
        }

        protected virtual PermissionRow[] MapRecords(IDataReader reader,
                                        int startIndex, int length, ref int totalRecordCount) {
            if (0 > startIndex)
                throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
            if (0 > length)
                throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

            int permission_IDColumnIndex = reader.GetOrdinal("Permission_ID");
            int permission_NameColumnIndex = reader.GetOrdinal("Permission_Name");

            System.Collections.ArrayList recordList = new System.Collections.ArrayList();
            int ri = -startIndex;
            while (reader.Read()) {
                ri++;
                if (ri > 0 && ri <= length) {
                    PermissionRow record = new PermissionRow();
                    recordList.Add(record);

                    record.Permission_ID = Convert.ToInt32(reader.GetValue(permission_IDColumnIndex));
                    if (!reader.IsDBNull(permission_NameColumnIndex))
                        record.Permission_Name = Convert.ToString(reader.GetValue(permission_NameColumnIndex));

                    if (ri == length && 0 != totalRecordCount)
                        break;
                }
            }

            totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
            return (PermissionRow[])(recordList.ToArray(typeof(PermissionRow)));
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
        protected virtual PermissionRow MapRow(DataRow row) {
            PermissionRow mappedObject = new PermissionRow();
            DataTable dataTable = row.Table;
            DataColumn dataColumn;
            // Column "Permission_ID"
            dataColumn = dataTable.Columns["Permission_ID"];
            if (!row.IsNull(dataColumn))
                mappedObject.Permission_ID = (int)row[dataColumn];
            // Column "Permission_Name"
            dataColumn = dataTable.Columns["Permission_Name"];
            if (!row.IsNull(dataColumn))
                mappedObject.Permission_Name = (string)row[dataColumn];
            return mappedObject;
        }

        protected virtual DataTable CreateDataTable() {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Permission";
            DataColumn dataColumn;
            dataColumn = dataTable.Columns.Add("Permission_ID", typeof(int));
            dataColumn.AllowDBNull = false;
            dataColumn.ReadOnly = true;
            dataColumn.Unique = true;
            dataColumn.AutoIncrement = true;
            dataColumn = dataTable.Columns.Add("Permission_Name", typeof(string));
            dataColumn.MaxLength = 50;
            return dataTable;
        }

        protected virtual IDbDataParameter AddParameter(IDbCommand cmd, string paramName, object value) {
            IDbDataParameter parameter;
            switch (paramName) {
                case "Permission_ID":
                    parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
                    break;

                case "Permission_Name":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                default:
                    throw new ArgumentException("Unknown parameter name (" + paramName + ").");
            }
            return parameter;
        }
    }
}
