
using System;
using System.Data;

namespace DFISYS.User.Db
{
	public abstract class Channel_UserCollection_Base
	{
		// Constants
		public const string CU_IDColumnName = "CU_ID";
		public const string User_IDColumnName = "User_ID";
		public const string Channel_IDColumnName = "Channel_ID";

		// Instance fields
		private MainDB _db;

		public Channel_UserCollection_Base(MainDB db)
		{
			_db = db;
		}

		protected MainDB Database
		{
			get { return _db; }
		}

		public virtual Channel_UserRow[] GetAll()
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

		public Channel_UserRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			Channel_UserRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		public Channel_UserRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		public virtual Channel_UserRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[Channel_User]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}
		
		
		public virtual Channel_UserRow GetByPrimaryKey(int cu_id)
		{
			string whereSql = "[CU_ID]=" + _db.CreateSqlParameterName("CU_ID");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "CU_ID", cu_id);
			Channel_UserRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		
		
		public Channel_UserRow[] GetByChannel_ID(int channel_ID)
		{
			return GetByChannel_ID(channel_ID, false);
		}

		

		
		public virtual Channel_UserRow[] GetByChannel_ID(int channel_ID, bool channel_IDNull)
		{
			return MapRecords(CreateGetByChannel_IDCommand(channel_ID, channel_IDNull));
		}

		
		
		public DataTable GetByChannel_IDAsDataTable(int channel_ID)
		{
			return GetByChannel_IDAsDataTable(channel_ID, false);
		}

	
	
	
		public virtual DataTable GetByChannel_IDAsDataTable(int channel_ID, bool channel_IDNull)
		{
			return MapRecordsToDataTable(CreateGetByChannel_IDCommand(channel_ID, channel_IDNull));
		}




		protected virtual IDbCommand CreateGetByChannel_IDCommand(int channel_ID, bool channel_IDNull)
		{
			string whereSql = "";
			if(channel_IDNull)
				whereSql += "[Channel_ID] IS NULL";
			else
				whereSql += "[Channel_ID]=" + _db.CreateSqlParameterName("Channel_ID");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!channel_IDNull)
				AddParameter(cmd, "Channel_ID", channel_ID);
			return cmd;
		}

		
		
		public virtual Channel_UserRow[] GetByUser_ID(string user_ID)
		{
			return MapRecords(CreateGetByUser_IDCommand(user_ID));
		}

		
		
		public virtual DataTable GetByUser_IDAsDataTable(string user_ID)
		{
			return MapRecordsToDataTable(CreateGetByUser_IDCommand(user_ID));
		}



		protected virtual IDbCommand CreateGetByUser_IDCommand(string user_ID)
		{
			string whereSql = "";
			if(null == user_ID)
				whereSql += "[User_ID] IS NULL";
			else
				whereSql += "[User_ID]=" + _db.CreateSqlParameterName("User_ID");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(null != user_ID)
				AddParameter(cmd, "User_ID", user_ID);
			return cmd;
		}

		public virtual void Insert(Channel_UserRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[Channel_User] (" +
				"[User_ID], " +
				"[Channel_ID]" +
				") VALUES (" +
				_db.CreateSqlParameterName("User_ID") + ", " +
				_db.CreateSqlParameterName("Channel_ID") + ");SELECT @@IDENTITY";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "User_ID", value.User_ID);
			AddParameter(cmd, "Channel_ID",
				value.IsChannel_IDNull ? DBNull.Value : (object)value.Channel_ID);
			value.CU_ID = Convert.ToInt32(cmd.ExecuteScalar());
		}
		public virtual bool Update(Channel_UserRow value)
		{
			string sqlStr = "UPDATE [dbo].[Channel_User] SET " +
				"[User_ID]=" + _db.CreateSqlParameterName("User_ID") + ", " +
				"[Channel_ID]=" + _db.CreateSqlParameterName("Channel_ID") +
				" WHERE " +
				"[CU_ID]=" + _db.CreateSqlParameterName("CU_ID");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "User_ID", value.User_ID);
			AddParameter(cmd, "Channel_ID",
				value.IsChannel_IDNull ? DBNull.Value : (object)value.Channel_ID);
			AddParameter(cmd, "CU_ID", value.CU_ID);
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
							DeleteByPrimaryKey((int)row["CU_ID"]);
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

		public bool Delete(Channel_UserRow value)
		{
			return DeleteByPrimaryKey(value.CU_ID);
		}



		public virtual bool DeleteByPrimaryKey(int cu_id)
		{
			string whereSql = "[CU_ID]=" + _db.CreateSqlParameterName("CU_ID");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "CU_ID", cu_id);
			return 0 < cmd.ExecuteNonQuery();
		}


		public int DeleteByChannel_ID(int channel_ID)
		{
			return DeleteByChannel_ID(channel_ID, false);
		}



		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByChannel_ID(int channel_ID, bool channel_IDNull)
		{
			return CreateDeleteByChannel_IDCommand(channel_ID, channel_IDNull).ExecuteNonQuery();
		}



		protected virtual IDbCommand CreateDeleteByChannel_IDCommand(int channel_ID, bool channel_IDNull)
		{
			string whereSql = "";
			if(channel_IDNull)
				whereSql += "[Channel_ID] IS NULL";
			else
				whereSql += "[Channel_ID]=" + _db.CreateSqlParameterName("Channel_ID");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!channel_IDNull)
				AddParameter(cmd, "Channel_ID", channel_ID);
			return cmd;
		}


		public int DeleteByUser_ID(string user_ID)
		{
			return CreateDeleteByUser_IDCommand(user_ID).ExecuteNonQuery();
		}



		protected virtual IDbCommand CreateDeleteByUser_IDCommand(string user_ID)
		{
			string whereSql = "";
			if(null == user_ID)
				whereSql += "[User_ID] IS NULL";
			else
				whereSql += "[User_ID]=" + _db.CreateSqlParameterName("User_ID");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(null != user_ID)
				AddParameter(cmd, "User_ID", user_ID);
			return cmd;
		}

		public int Delete(string whereSql)
		{
			return CreateDeleteCommand(whereSql).ExecuteNonQuery();
		}

		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[Channel_User]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		public int DeleteAll()
		{
			return Delete("");
		}

		protected Channel_UserRow[] MapRecords(IDbCommand command)
		{
			using(IDataReader reader = _db.ExecuteReader(command))
			{
				return MapRecords(reader);
			}
		}


		protected Channel_UserRow[] MapRecords(IDataReader reader)
		{
			int totalRecordCount = -1;
			return MapRecords(reader, 0, int.MaxValue, ref totalRecordCount);
		}

		protected virtual Channel_UserRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int cu_idColumnIndex = reader.GetOrdinal("CU_ID");
			int user_IDColumnIndex = reader.GetOrdinal("User_ID");
			int channel_IDColumnIndex = reader.GetOrdinal("Channel_ID");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					Channel_UserRow record = new Channel_UserRow();
					recordList.Add(record);

					record.CU_ID = Convert.ToInt32(reader.GetValue(cu_idColumnIndex));
					if(!reader.IsDBNull(user_IDColumnIndex))
						record.User_ID = Convert.ToString(reader.GetValue(user_IDColumnIndex));
					if(!reader.IsDBNull(channel_IDColumnIndex))
						record.Channel_ID = Convert.ToInt32(reader.GetValue(channel_IDColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (Channel_UserRow[])(recordList.ToArray(typeof(Channel_UserRow)));
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
		protected virtual Channel_UserRow MapRow(DataRow row)
		{
			Channel_UserRow mappedObject = new Channel_UserRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "CU_ID"
			dataColumn = dataTable.Columns["CU_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.CU_ID = (int)row[dataColumn];
			// Column "User_ID"
			dataColumn = dataTable.Columns["User_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.User_ID = (string)row[dataColumn];
			// Column "Channel_ID"
			dataColumn = dataTable.Columns["Channel_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.Channel_ID = (int)row[dataColumn];
			return mappedObject;
		}

		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "Channel_User";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("CU_ID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn.Unique = true;
			dataColumn.AutoIncrement = true;
			dataColumn = dataTable.Columns.Add("User_ID", typeof(string));
			dataColumn.MaxLength = 200;
			dataColumn = dataTable.Columns.Add("Channel_ID", typeof(int));
			return dataTable;
		}
		
		protected virtual IDbDataParameter AddParameter(IDbCommand cmd, string paramName, object value)
		{
			IDbDataParameter parameter;
			switch(paramName)
			{
				case "CU_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "User_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "Channel_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}
	}
}
