
using System;
using System.Data;

namespace DFISYS.User.Db
{
	public abstract class Channel_User_RoleCollection_Base
	{
		// Constants
		public const string CUR_IDColumnName = "CUR_ID";
		public const string CU_IDColumnName = "CU_ID";
		public const string Role_IDColumnName = "Role_ID";

		// Instance fields
		private MainDB _db;

		public Channel_User_RoleCollection_Base(MainDB db)
		{
			_db = db;
		}

		protected MainDB Database
		{
			get { return _db; }
		}

		public virtual Channel_User_RoleRow[] GetAll()
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

		public Channel_User_RoleRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			Channel_User_RoleRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		public Channel_User_RoleRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		public virtual Channel_User_RoleRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[Channel_User_Role]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}
		
		
		public virtual Channel_User_RoleRow GetByPrimaryKey(int cur_id)
		{
			string whereSql = "[CUR_ID]=" + _db.CreateSqlParameterName("CUR_ID");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "CUR_ID", cur_id);
			Channel_User_RoleRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		
		
		public Channel_User_RoleRow[] GetByCU_ID(int cu_id)
		{
			return GetByCU_ID(cu_id, false);
		}

		

		
		public virtual Channel_User_RoleRow[] GetByCU_ID(int cu_id, bool cu_idNull)
		{
			return MapRecords(CreateGetByCU_IDCommand(cu_id, cu_idNull));
		}

		
		
		public DataTable GetByCU_IDAsDataTable(int cu_id)
		{
			return GetByCU_IDAsDataTable(cu_id, false);
		}

	
	
	
		public virtual DataTable GetByCU_IDAsDataTable(int cu_id, bool cu_idNull)
		{
			return MapRecordsToDataTable(CreateGetByCU_IDCommand(cu_id, cu_idNull));
		}




		protected virtual IDbCommand CreateGetByCU_IDCommand(int cu_id, bool cu_idNull)
		{
			string whereSql = "";
			if(cu_idNull)
				whereSql += "[CU_ID] IS NULL";
			else
				whereSql += "[CU_ID]=" + _db.CreateSqlParameterName("CU_ID");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!cu_idNull)
				AddParameter(cmd, "CU_ID", cu_id);
			return cmd;
		}

		
		
		public Channel_User_RoleRow[] GetByRole_ID(int role_ID)
		{
			return GetByRole_ID(role_ID, false);
		}

		

		
		public virtual Channel_User_RoleRow[] GetByRole_ID(int role_ID, bool role_IDNull)
		{
			return MapRecords(CreateGetByRole_IDCommand(role_ID, role_IDNull));
		}

		
		
		public DataTable GetByRole_IDAsDataTable(int role_ID)
		{
			return GetByRole_IDAsDataTable(role_ID, false);
		}

	
	
	
		public virtual DataTable GetByRole_IDAsDataTable(int role_ID, bool role_IDNull)
		{
			return MapRecordsToDataTable(CreateGetByRole_IDCommand(role_ID, role_IDNull));
		}




		protected virtual IDbCommand CreateGetByRole_IDCommand(int role_ID, bool role_IDNull)
		{
			string whereSql = "";
			if(role_IDNull)
				whereSql += "[Role_ID] IS NULL";
			else
				whereSql += "[Role_ID]=" + _db.CreateSqlParameterName("Role_ID");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!role_IDNull)
				AddParameter(cmd, "Role_ID", role_ID);
			return cmd;
		}

		public virtual void Insert(Channel_User_RoleRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[Channel_User_Role] (" +
				"[CU_ID], " +
				"[Role_ID]" +
				") VALUES (" +
				_db.CreateSqlParameterName("CU_ID") + ", " +
				_db.CreateSqlParameterName("Role_ID") + ");SELECT @@IDENTITY";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "CU_ID",
				value.IsCU_IDNull ? DBNull.Value : (object)value.CU_ID);
			AddParameter(cmd, "Role_ID",
				value.IsRole_IDNull ? DBNull.Value : (object)value.Role_ID);
			value.CUR_ID = Convert.ToInt32(cmd.ExecuteScalar());
		}
		public virtual bool Update(Channel_User_RoleRow value)
		{
			string sqlStr = "UPDATE [dbo].[Channel_User_Role] SET " +
				"[CU_ID]=" + _db.CreateSqlParameterName("CU_ID") + ", " +
				"[Role_ID]=" + _db.CreateSqlParameterName("Role_ID") +
				" WHERE " +
				"[CUR_ID]=" + _db.CreateSqlParameterName("CUR_ID");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "CU_ID",
				value.IsCU_IDNull ? DBNull.Value : (object)value.CU_ID);
			AddParameter(cmd, "Role_ID",
				value.IsRole_IDNull ? DBNull.Value : (object)value.Role_ID);
			AddParameter(cmd, "CUR_ID", value.CUR_ID);
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
							DeleteByPrimaryKey((int)row["CUR_ID"]);
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

		public bool Delete(Channel_User_RoleRow value)
		{
			return DeleteByPrimaryKey(value.CUR_ID);
		}



		public virtual bool DeleteByPrimaryKey(int cur_id)
		{
			string whereSql = "[CUR_ID]=" + _db.CreateSqlParameterName("CUR_ID");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "CUR_ID", cur_id);
			return 0 < cmd.ExecuteNonQuery();
		}


		public int DeleteByCU_ID(int cu_id)
		{
			return DeleteByCU_ID(cu_id, false);
		}



		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByCU_ID(int cu_id, bool cu_idNull)
		{
			return CreateDeleteByCU_IDCommand(cu_id, cu_idNull).ExecuteNonQuery();
		}



		protected virtual IDbCommand CreateDeleteByCU_IDCommand(int cu_id, bool cu_idNull)
		{
			string whereSql = "";
			if(cu_idNull)
				whereSql += "[CU_ID] IS NULL";
			else
				whereSql += "[CU_ID]=" + _db.CreateSqlParameterName("CU_ID");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!cu_idNull)
				AddParameter(cmd, "CU_ID", cu_id);
			return cmd;
		}


		public int DeleteByRole_ID(int role_ID)
		{
			return DeleteByRole_ID(role_ID, false);
		}



		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByRole_ID(int role_ID, bool role_IDNull)
		{
			return CreateDeleteByRole_IDCommand(role_ID, role_IDNull).ExecuteNonQuery();
		}



		protected virtual IDbCommand CreateDeleteByRole_IDCommand(int role_ID, bool role_IDNull)
		{
			string whereSql = "";
			if(role_IDNull)
				whereSql += "[Role_ID] IS NULL";
			else
				whereSql += "[Role_ID]=" + _db.CreateSqlParameterName("Role_ID");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!role_IDNull)
				AddParameter(cmd, "Role_ID", role_ID);
			return cmd;
		}

		public int Delete(string whereSql)
		{
			return CreateDeleteCommand(whereSql).ExecuteNonQuery();
		}

		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[Channel_User_Role]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		public int DeleteAll()
		{
			return Delete("");
		}

		protected Channel_User_RoleRow[] MapRecords(IDbCommand command)
		{
			using(IDataReader reader = _db.ExecuteReader(command))
			{
				return MapRecords(reader);
			}
		}


		protected Channel_User_RoleRow[] MapRecords(IDataReader reader)
		{
			int totalRecordCount = -1;
			return MapRecords(reader, 0, int.MaxValue, ref totalRecordCount);
		}

		protected virtual Channel_User_RoleRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int cur_idColumnIndex = reader.GetOrdinal("CUR_ID");
			int cu_idColumnIndex = reader.GetOrdinal("CU_ID");
			int role_IDColumnIndex = reader.GetOrdinal("Role_ID");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					Channel_User_RoleRow record = new Channel_User_RoleRow();
					recordList.Add(record);

					record.CUR_ID = Convert.ToInt32(reader.GetValue(cur_idColumnIndex));
					if(!reader.IsDBNull(cu_idColumnIndex))
						record.CU_ID = Convert.ToInt32(reader.GetValue(cu_idColumnIndex));
					if(!reader.IsDBNull(role_IDColumnIndex))
						record.Role_ID = Convert.ToInt32(reader.GetValue(role_IDColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (Channel_User_RoleRow[])(recordList.ToArray(typeof(Channel_User_RoleRow)));
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
		protected virtual Channel_User_RoleRow MapRow(DataRow row)
		{
			Channel_User_RoleRow mappedObject = new Channel_User_RoleRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "CUR_ID"
			dataColumn = dataTable.Columns["CUR_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.CUR_ID = (int)row[dataColumn];
			// Column "CU_ID"
			dataColumn = dataTable.Columns["CU_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.CU_ID = (int)row[dataColumn];
			// Column "Role_ID"
			dataColumn = dataTable.Columns["Role_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.Role_ID = (int)row[dataColumn];
			return mappedObject;
		}

		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "Channel_User_Role";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("CUR_ID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn.Unique = true;
			dataColumn.AutoIncrement = true;
			dataColumn = dataTable.Columns.Add("CU_ID", typeof(int));
			dataColumn = dataTable.Columns.Add("Role_ID", typeof(int));
			return dataTable;
		}
		
		protected virtual IDbDataParameter AddParameter(IDbCommand cmd, string paramName, object value)
		{
			IDbDataParameter parameter;
			switch(paramName)
			{
				case "CUR_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "CU_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Role_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}
	}
}
