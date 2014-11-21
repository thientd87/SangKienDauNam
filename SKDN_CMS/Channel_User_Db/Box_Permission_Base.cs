using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace DFISYS.User.Db
{
    //class Box_Permission_Base 
    public abstract class Box_Permission_Base
    {
        private MainDB _db;

        public Box_Permission_Base(MainDB db)
		{
			_db = db;
		}

		protected MainDB Database
		{
			get { return _db; }
		}
        //them
        public DataTable GetAllBox()
        {
            IDbCommand cmd = _db.CreateCommand("GetAllBox", true);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable GetAllUser()
        {
            IDbCommand cmd = _db.CreateCommand("GetAllUser", true);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        ////
        public virtual DataTable GetAllAsDataTable()
        {
            return MapRecordsToDataTable(CreateGetAllCommand());
        }
        protected virtual IDbCommand CreateGetAllCommand()
        {
            return CreateGetCommand(null, null);
        }
        protected virtual IDbCommand CreateGetCommand(string whereSql, string orderBySql)
        {
            string sql = "SELECT * FROM [dbo].[Box]";
            if (null != whereSql && 0 < whereSql.Length)
                sql += " WHERE " + whereSql;
            if (null != orderBySql && 0 < orderBySql.Length)
                sql += " ORDER BY " + orderBySql;
            return _db.CreateCommand(sql);
            //DataTable objresult;
            //using (MainDB objdb = new MainDB())
            //{
            //    string strWhere = "";
            //    //objresult = objdb.NewsCollection.GetPageAsDataTable(intPageNum, PageSize, strWhere, "News_ModifiedDate DESC"); ;
            //    objresult = objdb.StoreProcedure.News_GetListBox(strWhere);

            //}
            //return objresult;
            //return _db.CreateDataTable(objresult);
        }
        protected DataTable MapRecordsToDataTable(IDbCommand command)
        {
            using (IDataReader reader = _db.ExecuteReader(command))
            {
                return MapRecordsToDataTable(reader);
            }
        }
        protected DataTable MapRecordsToDataTable(IDataReader reader)
        {
            int totalRecordCount = 0;
            return MapRecordsToDataTable(reader, 0, int.MaxValue, ref totalRecordCount);
        }

        protected virtual DataTable MapRecordsToDataTable(IDataReader reader,
                                        int startIndex, int length, ref int totalRecordCount)
        {
            if (0 > startIndex)
                throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
            if (0 > length)
                throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

            int columnCount = reader.FieldCount;
            int ri = -startIndex;

            DataTable dataTable = CreateDataTable();
            dataTable.BeginLoadData();
            object[] values = new object[columnCount];

            while (reader.Read())
            {
                ri++;
                if (ri > 0 && ri <= length)
                {
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
        protected virtual DataTable CreateDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Box";
            DataColumn dataColumn;
            dataColumn = dataTable.Columns.Add("Box_ID", typeof(int));
            dataColumn.AllowDBNull = false;
            dataColumn = dataTable.Columns.Add("Box_Name", typeof(string));
            dataColumn.MaxLength = 50;
            dataColumn = dataTable.Columns.Add("Box_Index", typeof(int));
            dataColumn.AllowDBNull = false;
            return dataTable;
        }
        //////
        public virtual DataTable GetAllAsDataTablePer()
        {
            return MapRecordsToDataTablePer(CreateGetAllCommandPer());
        }
        protected virtual IDbCommand CreateGetAllCommandPer()
        {
            return CreateGetCommandPer(null, null);
        }
        protected DataTable MapRecordsToDataTablePer(IDbCommand command)
        {
            using (IDataReader reader = _db.ExecuteReader(command))
            {
                return MapRecordsToDataTablePer(reader);
            }
        }
        protected DataTable MapRecordsToDataTablePer(IDataReader reader)
        {
            int totalRecordCount = 0;
            return MapRecordsToDataTablePer(reader, 0, int.MaxValue, ref totalRecordCount);
        }
        protected virtual IDbCommand CreateGetCommandPer(string whereSql, string orderBySql)
        {
            string sql = "SELECT * FROM [dbo].[Permission]";
            if (null != whereSql && 0 < whereSql.Length)
                sql += " WHERE " + whereSql;
            if (null != orderBySql && 0 < orderBySql.Length)
                sql += " ORDER BY " + orderBySql;
            return _db.CreateCommand(sql);
        }
        protected virtual DataTable MapRecordsToDataTablePer(IDataReader reader,
                                       int startIndex, int length, ref int totalRecordCount)
        {
            if (0 > startIndex)
                throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
            if (0 > length)
                throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

            int columnCount = reader.FieldCount;
            int ri = -startIndex;

            DataTable dataTable = CreateDataTablePer();
            dataTable.BeginLoadData();
            object[] values = new object[columnCount];

            while (reader.Read())
            {
                ri++;
                if (ri > 0 && ri <= length)
                {
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
        protected virtual DataTable CreateDataTablePer()
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Permission";
            DataColumn dataColumn;
            dataColumn = dataTable.Columns.Add("Permission_ID", typeof(int));
            dataColumn.AllowDBNull = false;
            dataColumn = dataTable.Columns.Add("Permission_Name", typeof(string));
            dataColumn.MaxLength = 50;
            //dataColumn = dataTable.Columns.Add("Permission_Name", typeof(string));
            //dataColumn.MaxLength = 50;
            return dataTable;
        }
    }
}
