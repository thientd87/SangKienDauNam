
using System;
using System.Data;

namespace DFISYS.User.Db
{
	public abstract class User_CategoryCollection_Base
	{
		// Constants
		public const string UC_IDColumnName = "UC_ID";
		public const string UP_IDColumnName = "UP_ID";
		public const string Cat_IDColumnName = "Cat_ID";

		// Instance fields
		private MainDB _db;

		public User_CategoryCollection_Base(MainDB db)
		{
			_db = db;
		}

		protected MainDB Database
		{
			get { return _db; }
		}

		public virtual User_CategoryRow[] GetAll()
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

		public User_CategoryRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			User_CategoryRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		public User_CategoryRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		public virtual User_CategoryRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[User_Category]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}
		
		
		public virtual User_CategoryRow GetByPrimaryKey(int uc_id)
		{
			string whereSql = "[UC_ID]=" + _db.CreateSqlParameterName("UC_ID");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "UC_ID", uc_id);
			User_CategoryRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		
		
		public User_CategoryRow[] GetByUP_ID(int up_id)
		{
			return GetByUP_ID(up_id, false);
		}

		

		
		public virtual User_CategoryRow[] GetByUP_ID(int up_id, bool up_idNull)
		{
			return MapRecords(CreateGetByUP_IDCommand(up_id, up_idNull));
		}

		
		
		public DataTable GetByUP_IDAsDataTable(int up_id)
		{
			return GetByUP_IDAsDataTable(up_id, false);
		}

	
	
	
		public virtual DataTable GetByUP_IDAsDataTable(int up_id, bool up_idNull)
		{
			return MapRecordsToDataTable(CreateGetByUP_IDCommand(up_id, up_idNull));
		}




		protected virtual IDbCommand CreateGetByUP_IDCommand(int up_id, bool up_idNull)
		{
			string whereSql = "";
			if(up_idNull)
				whereSql += "[UP_ID] IS NULL";
			else
				whereSql += "[UP_ID]=" + _db.CreateSqlParameterName("UP_ID");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!up_idNull)
				AddParameter(cmd, "UP_ID", up_id);
			return cmd;
		}

		public virtual void Insert(User_CategoryRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[User_Category] (" +
				"[UP_ID], " +
				"[Cat_ID]" +
				") VALUES (" +
				_db.CreateSqlParameterName("UP_ID") + ", " +
				_db.CreateSqlParameterName("Cat_ID") + ");SELECT @@IDENTITY";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "UP_ID",
				value.IsUP_IDNull ? DBNull.Value : (object)value.UP_ID);
			AddParameter(cmd, "Cat_ID",
				value.IsCat_IDNull ? DBNull.Value : (object)value.Cat_ID);
			value.UC_ID = Convert.ToInt32(cmd.ExecuteScalar());
		}
		public virtual bool Update(User_CategoryRow value)
		{
			string sqlStr = "UPDATE [dbo].[User_Category] SET " +
				"[UP_ID]=" + _db.CreateSqlParameterName("UP_ID") + ", " +
				"[Cat_ID]=" + _db.CreateSqlParameterName("Cat_ID") +
				" WHERE " +
				"[UC_ID]=" + _db.CreateSqlParameterName("UC_ID");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "UP_ID",
				value.IsUP_IDNull ? DBNull.Value : (object)value.UP_ID);
			AddParameter(cmd, "Cat_ID",
				value.IsCat_IDNull ? DBNull.Value : (object)value.Cat_ID);
			AddParameter(cmd, "UC_ID", value.UC_ID);
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
							DeleteByPrimaryKey((int)row["UC_ID"]);
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

		public bool Delete(User_CategoryRow value)
		{
			return DeleteByPrimaryKey(value.UC_ID);
		}



		public virtual bool DeleteByPrimaryKey(int uc_id)
		{
			string whereSql = "[UC_ID]=" + _db.CreateSqlParameterName("UC_ID");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "UC_ID", uc_id);
			return 0 < cmd.ExecuteNonQuery();
		}


		public int DeleteByUP_ID(int up_id)
		{
			return DeleteByUP_ID(up_id, false);
		}



		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByUP_ID(int up_id, bool up_idNull)
		{
			return CreateDeleteByUP_IDCommand(up_id, up_idNull).ExecuteNonQuery();
		}



		protected virtual IDbCommand CreateDeleteByUP_IDCommand(int up_id, bool up_idNull)
		{
			string whereSql = "";
			if(up_idNull)
				whereSql += "[UP_ID] IS NULL";
			else
				whereSql += "[UP_ID]=" + _db.CreateSqlParameterName("UP_ID");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!up_idNull)
				AddParameter(cmd, "UP_ID", up_id);
			return cmd;
		}

		public int Delete(string whereSql)
		{
			return CreateDeleteCommand(whereSql).ExecuteNonQuery();
		}

		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[User_Category]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		public int DeleteAll()
		{
			return Delete("");
		}

		protected User_CategoryRow[] MapRecords(IDbCommand command)
		{
			using(IDataReader reader = _db.ExecuteReader(command))
			{
				return MapRecords(reader);
			}
		}


		protected User_CategoryRow[] MapRecords(IDataReader reader)
		{
			int totalRecordCount = -1;
			return MapRecords(reader, 0, int.MaxValue, ref totalRecordCount);
		}

		protected virtual User_CategoryRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int uc_idColumnIndex = reader.GetOrdinal("UC_ID");
			int up_idColumnIndex = reader.GetOrdinal("UP_ID");
			int cat_IDColumnIndex = reader.GetOrdinal("Cat_ID");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					User_CategoryRow record = new User_CategoryRow();
					recordList.Add(record);

					record.UC_ID = Convert.ToInt32(reader.GetValue(uc_idColumnIndex));
					if(!reader.IsDBNull(up_idColumnIndex))
						record.UP_ID = Convert.ToInt32(reader.GetValue(up_idColumnIndex));
					if(!reader.IsDBNull(cat_IDColumnIndex))
						record.Cat_ID = Convert.ToInt32(reader.GetValue(cat_IDColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (User_CategoryRow[])(recordList.ToArray(typeof(User_CategoryRow)));
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
		protected virtual User_CategoryRow MapRow(DataRow row)
		{
			User_CategoryRow mappedObject = new User_CategoryRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "UC_ID"
			dataColumn = dataTable.Columns["UC_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.UC_ID = (int)row[dataColumn];
			// Column "UP_ID"
			dataColumn = dataTable.Columns["UP_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.UP_ID = (int)row[dataColumn];
			// Column "Cat_ID"
			dataColumn = dataTable.Columns["Cat_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.Cat_ID = (int)row[dataColumn];
			return mappedObject;
		}

		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "User_Category";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("UC_ID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn.Unique = true;
			dataColumn.AutoIncrement = true;
			dataColumn = dataTable.Columns.Add("UP_ID", typeof(int));
			dataColumn = dataTable.Columns.Add("Cat_ID", typeof(int));
			return dataTable;
		}
		
		protected virtual IDbDataParameter AddParameter(IDbCommand cmd, string paramName, object value)
		{
			IDbDataParameter parameter;
			switch(paramName)
			{
				case "UC_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "UP_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Cat_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}
	}
}
