
using System;
using System.Data;

namespace DFISYS.User.Db
{
	public abstract class User_PermissionCollection_Base
	{
		// Constants
		public const string UP_IDColumnName = "UP_ID";
		public const string CUR_IDColumnName = "CUR_ID";
		public const string Permission_IDColumnName = "Permission_ID";

		// Instance fields
		private MainDB _db;

		public User_PermissionCollection_Base(MainDB db)
		{
			_db = db;
		}

		protected MainDB Database
		{
			get { return _db; }
		}

		public virtual User_PermissionRow[] GetAll()
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

		public User_PermissionRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			User_PermissionRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		public User_PermissionRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		public virtual User_PermissionRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[User_Permission]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}
		
		
		public virtual User_PermissionRow GetByPrimaryKey(int up_id)
		{
			string whereSql = "[UP_ID]=" + _db.CreateSqlParameterName("UP_ID");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "UP_ID", up_id);
			User_PermissionRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		
		
		public User_PermissionRow[] GetByCUR_ID(int cur_id)
		{
			return GetByCUR_ID(cur_id, false);
		}

		

		
		public virtual User_PermissionRow[] GetByCUR_ID(int cur_id, bool cur_idNull)
		{
			return MapRecords(CreateGetByCUR_IDCommand(cur_id, cur_idNull));
		}

		
		
		public DataTable GetByCUR_IDAsDataTable(int cur_id)
		{
			return GetByCUR_IDAsDataTable(cur_id, false);
		}

	
	
	
		public virtual DataTable GetByCUR_IDAsDataTable(int cur_id, bool cur_idNull)
		{
			return MapRecordsToDataTable(CreateGetByCUR_IDCommand(cur_id, cur_idNull));
		}




		protected virtual IDbCommand CreateGetByCUR_IDCommand(int cur_id, bool cur_idNull)
		{
			string whereSql = "";
			if(cur_idNull)
				whereSql += "[CUR_ID] IS NULL";
			else
				whereSql += "[CUR_ID]=" + _db.CreateSqlParameterName("CUR_ID");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!cur_idNull)
				AddParameter(cmd, "CUR_ID", cur_id);
			return cmd;
		}

		
		
		public User_PermissionRow[] GetByPermission_ID(int permission_ID)
		{
			return GetByPermission_ID(permission_ID, false);
		}

		

		
		public virtual User_PermissionRow[] GetByPermission_ID(int permission_ID, bool permission_IDNull)
		{
			return MapRecords(CreateGetByPermission_IDCommand(permission_ID, permission_IDNull));
		}

		
		
		public DataTable GetByPermission_IDAsDataTable(int permission_ID)
		{
			return GetByPermission_IDAsDataTable(permission_ID, false);
		}

	
	
	
		public virtual DataTable GetByPermission_IDAsDataTable(int permission_ID, bool permission_IDNull)
		{
			return MapRecordsToDataTable(CreateGetByPermission_IDCommand(permission_ID, permission_IDNull));
		}




		protected virtual IDbCommand CreateGetByPermission_IDCommand(int permission_ID, bool permission_IDNull)
		{
			string whereSql = "";
			if(permission_IDNull)
				whereSql += "[Permission_ID] IS NULL";
			else
				whereSql += "[Permission_ID]=" + _db.CreateSqlParameterName("Permission_ID");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!permission_IDNull)
				AddParameter(cmd, "Permission_ID", permission_ID);
			return cmd;
		}

		public virtual void Insert(User_PermissionRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[User_Permission] (" +
				"[CUR_ID], " +
				"[Permission_ID]" +
				") VALUES (" +
				_db.CreateSqlParameterName("CUR_ID") + ", " +
				_db.CreateSqlParameterName("Permission_ID") + ");SELECT @@IDENTITY";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "CUR_ID",
				value.IsCUR_IDNull ? DBNull.Value : (object)value.CUR_ID);
			AddParameter(cmd, "Permission_ID",
				value.IsPermission_IDNull ? DBNull.Value : (object)value.Permission_ID);
			value.UP_ID = Convert.ToInt32(cmd.ExecuteScalar());
		}
		public virtual bool Update(User_PermissionRow value)
		{
			string sqlStr = "UPDATE [dbo].[User_Permission] SET " +
				"[CUR_ID]=" + _db.CreateSqlParameterName("CUR_ID") + ", " +
				"[Permission_ID]=" + _db.CreateSqlParameterName("Permission_ID") +
				" WHERE " +
				"[UP_ID]=" + _db.CreateSqlParameterName("UP_ID");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "CUR_ID",
				value.IsCUR_IDNull ? DBNull.Value : (object)value.CUR_ID);
			AddParameter(cmd, "Permission_ID",
				value.IsPermission_IDNull ? DBNull.Value : (object)value.Permission_ID);
			AddParameter(cmd, "UP_ID", value.UP_ID);
			return 0 != cmd.ExecuteNonQuery();
		}

		public void Update(DataTable table)
		{
			Update(table, true);
		}

		public virtual void Update(DataTable table, bool acceptChanges)
		{
			DataRowCollection rows = table.Rows;
			for(int i = rows.Count - 1; i >= 0; i--)
			{
				DataRow row = rows[i];
				switch(row.RowState)
				{
					case DataRowState.Added:
						Insert(MapRow(row));
						if(acceptChanges)
							row.AcceptChanges();
						break;

					case DataRowState.Deleted:
						// Temporary reject changes to be able to access to the PK column(s)
						row.RejectChanges();
						try
						{
							DeleteByPrimaryKey((int)row["UP_ID"]);
						}
						finally
						{
							row.Delete();
						}
						if(acceptChanges)
							row.AcceptChanges();
						break;
						
					case DataRowState.Modified:
						Update(MapRow(row));
						if(acceptChanges)
							row.AcceptChanges();
						break;
				}
			}
		}

		public bool Delete(User_PermissionRow value)
		{
			return DeleteByPrimaryKey(value.UP_ID);
		}



		public virtual bool DeleteByPrimaryKey(int up_id)
		{
			string whereSql = "[UP_ID]=" + _db.CreateSqlParameterName("UP_ID");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "UP_ID", up_id);
			return 0 < cmd.ExecuteNonQuery();
		}


		public int DeleteByCUR_ID(int cur_id)
		{
			return DeleteByCUR_ID(cur_id, false);
		}



		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByCUR_ID(int cur_id, bool cur_idNull)
		{
			return CreateDeleteByCUR_IDCommand(cur_id, cur_idNull).ExecuteNonQuery();
		}



		protected virtual IDbCommand CreateDeleteByCUR_IDCommand(int cur_id, bool cur_idNull)
		{
			string whereSql = "";
			if(cur_idNull)
				whereSql += "[CUR_ID] IS NULL";
			else
				whereSql += "[CUR_ID]=" + _db.CreateSqlParameterName("CUR_ID");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!cur_idNull)
				AddParameter(cmd, "CUR_ID", cur_id);
			return cmd;
		}


		public int DeleteByPermission_ID(int permission_ID)
		{
			return DeleteByPermission_ID(permission_ID, false);
		}



		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByPermission_ID(int permission_ID, bool permission_IDNull)
		{
			return CreateDeleteByPermission_IDCommand(permission_ID, permission_IDNull).ExecuteNonQuery();
		}



		protected virtual IDbCommand CreateDeleteByPermission_IDCommand(int permission_ID, bool permission_IDNull)
		{
			string whereSql = "";
			if(permission_IDNull)
				whereSql += "[Permission_ID] IS NULL";
			else
				whereSql += "[Permission_ID]=" + _db.CreateSqlParameterName("Permission_ID");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!permission_IDNull)
				AddParameter(cmd, "Permission_ID", permission_ID);
			return cmd;
		}

		public int Delete(string whereSql)
		{
			return CreateDeleteCommand(whereSql).ExecuteNonQuery();
		}

		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[User_Permission]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		public int DeleteAll()
		{
			return Delete("");
		}

		protected User_PermissionRow[] MapRecords(IDbCommand command)
		{
			using(IDataReader reader = _db.ExecuteReader(command))
			{
				return MapRecords(reader);
			}
		}


		protected User_PermissionRow[] MapRecords(IDataReader reader)
		{
			int totalRecordCount = -1;
			return MapRecords(reader, 0, int.MaxValue, ref totalRecordCount);
		}

		protected virtual User_PermissionRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int up_idColumnIndex = reader.GetOrdinal("UP_ID");
			int cur_idColumnIndex = reader.GetOrdinal("CUR_ID");
			int permission_IDColumnIndex = reader.GetOrdinal("Permission_ID");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					User_PermissionRow record = new User_PermissionRow();
					recordList.Add(record);

					record.UP_ID = Convert.ToInt32(reader.GetValue(up_idColumnIndex));
					if(!reader.IsDBNull(cur_idColumnIndex))
						record.CUR_ID = Convert.ToInt32(reader.GetValue(cur_idColumnIndex));
					if(!reader.IsDBNull(permission_IDColumnIndex))
						record.Permission_ID = Convert.ToInt32(reader.GetValue(permission_IDColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (User_PermissionRow[])(recordList.ToArray(typeof(User_PermissionRow)));
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
		protected virtual User_PermissionRow MapRow(DataRow row)
		{
			User_PermissionRow mappedObject = new User_PermissionRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "UP_ID"
			dataColumn = dataTable.Columns["UP_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.UP_ID = (int)row[dataColumn];
			// Column "CUR_ID"
			dataColumn = dataTable.Columns["CUR_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.CUR_ID = (int)row[dataColumn];
			// Column "Permission_ID"
			dataColumn = dataTable.Columns["Permission_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.Permission_ID = (int)row[dataColumn];
			return mappedObject;
		}

		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "User_Permission";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("UP_ID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn.Unique = true;
			dataColumn.AutoIncrement = true;
			dataColumn = dataTable.Columns.Add("CUR_ID", typeof(int));
			dataColumn = dataTable.Columns.Add("Permission_ID", typeof(int));
			return dataTable;
		}
		
		protected virtual IDbDataParameter AddParameter(IDbCommand cmd, string paramName, object value)
		{
			IDbDataParameter parameter;
			switch(paramName)
			{
				case "UP_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "CUR_ID":
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
