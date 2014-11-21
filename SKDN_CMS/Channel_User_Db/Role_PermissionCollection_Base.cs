
using System;
using System.Data;

namespace DFISYS.User.Db
{
	public abstract class Role_PermissionCollection_Base
	{
		// Constants
		public const string Role_IDColumnName = "Role_ID";
		public const string Permission_IDColumnName = "Permission_ID";

		// Instance fields
		private MainDB _db;

		public Role_PermissionCollection_Base(MainDB db)
		{
			_db = db;
		}

		protected MainDB Database
		{
			get { return _db; }
		}

		public virtual Role_PermissionRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		public Role_PermissionRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			Role_PermissionRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		public Role_PermissionRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		public virtual Role_PermissionRow[] GetAsArray(string whereSql, string orderBySql,
							int startIndex, int length, ref int totalRecordCount)
		{
			using(IDataReader reader = _db.ExecuteReader(CreateGetCommand(whereSql, orderBySql)))
			{
				return MapRecords(reader, startIndex, length, ref totalRecordCount);
			}
		}

		public DataTable GetAsDataTable(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsDataTable(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		public virtual DataTable GetAsDataTable(string whereSql, string orderBySql,
							int startIndex, int length, ref int totalRecordCount)
		{
			using(IDataReader reader = _db.ExecuteReader(CreateGetCommand(whereSql, orderBySql)))
			{
				return MapRecordsToDataTable(reader, startIndex, length, ref totalRecordCount);
			}
		}

		protected virtual IDbCommand CreateGetCommand(string whereSql, string orderBySql)
		{
			string sql = "SELECT * FROM [dbo].[Role_Permission]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}
		
		
		
		public virtual Role_PermissionRow GetByPrimaryKey(int role_ID, int permission_ID)
		{
			string whereSql = "[Role_ID]=" + _db.CreateSqlParameterName("Role_ID") + " AND " +
							  "[Permission_ID]=" + _db.CreateSqlParameterName("Permission_ID");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Role_ID", role_ID);
			AddParameter(cmd, "Permission_ID", permission_ID);
			Role_PermissionRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		
		
		public virtual Role_PermissionRow[] GetByPermission_ID(int permission_ID)
		{
			return MapRecords(CreateGetByPermission_IDCommand(permission_ID));
		}

		
		
		public virtual DataTable GetByPermission_IDAsDataTable(int permission_ID)
		{
			return MapRecordsToDataTable(CreateGetByPermission_IDCommand(permission_ID));
		}



		protected virtual IDbCommand CreateGetByPermission_IDCommand(int permission_ID)
		{
			string whereSql = "";
			whereSql += "[Permission_ID]=" + _db.CreateSqlParameterName("Permission_ID");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Permission_ID", permission_ID);
			return cmd;
		}

		
		
		public virtual Role_PermissionRow[] GetByRole_ID(int role_ID)
		{
			return MapRecords(CreateGetByRole_IDCommand(role_ID));
		}

		
		
		public virtual DataTable GetByRole_IDAsDataTable(int role_ID)
		{
			return MapRecordsToDataTable(CreateGetByRole_IDCommand(role_ID));
		}



		protected virtual IDbCommand CreateGetByRole_IDCommand(int role_ID)
		{
			string whereSql = "";
			whereSql += "[Role_ID]=" + _db.CreateSqlParameterName("Role_ID");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Role_ID", role_ID);
			return cmd;
		}

		public virtual void Insert(Role_PermissionRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[Role_Permission] (" +
				"[Role_ID], " +
				"[Permission_ID]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Role_ID") + ", " +
				_db.CreateSqlParameterName("Permission_ID") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Role_ID", value.Role_ID);
			AddParameter(cmd, "Permission_ID", value.Permission_ID);
			cmd.ExecuteNonQuery();
		}

		public bool Delete(Role_PermissionRow value)
		{
			return DeleteByPrimaryKey(value.Role_ID, value.Permission_ID);
		}




		public virtual bool DeleteByPrimaryKey(int role_ID, int permission_ID)
		{
			string whereSql = "[Role_ID]=" + _db.CreateSqlParameterName("Role_ID") + " AND " +
							  "[Permission_ID]=" + _db.CreateSqlParameterName("Permission_ID");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Role_ID", role_ID);
			AddParameter(cmd, "Permission_ID", permission_ID);
			return 0 < cmd.ExecuteNonQuery();
		}


		public int DeleteByPermission_ID(int permission_ID)
		{
			return CreateDeleteByPermission_IDCommand(permission_ID).ExecuteNonQuery();
		}



		protected virtual IDbCommand CreateDeleteByPermission_IDCommand(int permission_ID)
		{
			string whereSql = "";
			whereSql += "[Permission_ID]=" + _db.CreateSqlParameterName("Permission_ID");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Permission_ID", permission_ID);
			return cmd;
		}


		public int DeleteByRole_ID(int role_ID)
		{
			return CreateDeleteByRole_IDCommand(role_ID).ExecuteNonQuery();
		}



		protected virtual IDbCommand CreateDeleteByRole_IDCommand(int role_ID)
		{
			string whereSql = "";
			whereSql += "[Role_ID]=" + _db.CreateSqlParameterName("Role_ID");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Role_ID", role_ID);
			return cmd;
		}

		public int Delete(string whereSql)
		{
			return CreateDeleteCommand(whereSql).ExecuteNonQuery();
		}

		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[Role_Permission]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		public int DeleteAll()
		{
			return Delete("");
		}

		protected Role_PermissionRow[] MapRecords(IDbCommand command)
		{
			using(IDataReader reader = _db.ExecuteReader(command))
			{
				return MapRecords(reader);
			}
		}


		protected Role_PermissionRow[] MapRecords(IDataReader reader)
		{
			int totalRecordCount = -1;
			return MapRecords(reader, 0, int.MaxValue, ref totalRecordCount);
		}

		protected virtual Role_PermissionRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int role_IDColumnIndex = reader.GetOrdinal("Role_ID");
			int permission_IDColumnIndex = reader.GetOrdinal("Permission_ID");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					Role_PermissionRow record = new Role_PermissionRow();
					recordList.Add(record);

					record.Role_ID = Convert.ToInt32(reader.GetValue(role_IDColumnIndex));
					record.Permission_ID = Convert.ToInt32(reader.GetValue(permission_IDColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (Role_PermissionRow[])(recordList.ToArray(typeof(Role_PermissionRow)));
		}

		protected DataTable MapRecordsToDataTable(IDbCommand command)
		{
			using(IDataReader reader = _db.ExecuteReader(command))
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
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int columnCount = reader.FieldCount;
			int ri = -startIndex;
			
			DataTable dataTable = CreateDataTable();
			dataTable.BeginLoadData();
			object[] values = new object[columnCount];

			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					reader.GetValues(values);
					dataTable.LoadDataRow(values, true);

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}
			dataTable.EndLoadData();

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return dataTable;
		}
		protected virtual Role_PermissionRow MapRow(DataRow row)
		{
			Role_PermissionRow mappedObject = new Role_PermissionRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Role_ID"
			dataColumn = dataTable.Columns["Role_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.Role_ID = (int)row[dataColumn];
			// Column "Permission_ID"
			dataColumn = dataTable.Columns["Permission_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.Permission_ID = (int)row[dataColumn];
			return mappedObject;
		}

		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "Role_Permission";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Role_ID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Permission_ID", typeof(int));
			dataColumn.AllowDBNull = false;
			return dataTable;
		}
		
		protected virtual IDbDataParameter AddParameter(IDbCommand cmd, string paramName, object value)
		{
			IDbDataParameter parameter;
			switch(paramName)
			{
				case "Role_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Permission_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}
	}
}
