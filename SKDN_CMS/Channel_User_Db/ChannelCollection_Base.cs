
using System;
using System.Data;

namespace DFISYS.User.Db
{
	public abstract class ChannelCollection_Base
	{
		// Constants
		public const string Channel_IDColumnName = "Channel_ID";
		public const string Channel_NameColumnName = "Channel_Name";
		public const string Channel_DescriptionColumnName = "Channel_Description";

		// Instance fields
		private MainDB _db;

		public ChannelCollection_Base(MainDB db)
		{
			_db = db;
		}

		protected MainDB Database
		{
			get { return _db; }
		}

		public virtual ChannelRow[] GetAll()
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

		public ChannelRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			ChannelRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		public ChannelRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		public virtual ChannelRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[Channel]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}
		
		
		public virtual ChannelRow GetByPrimaryKey(int channel_ID)
		{
			string whereSql = "[Channel_ID]=" + _db.CreateSqlParameterName("Channel_ID");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Channel_ID", channel_ID);
			ChannelRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		public virtual void Insert(ChannelRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[Channel] (" +
				"[Channel_ID], " +
				"[Channel_Name], " +
				"[Channel_Description]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Channel_ID") + ", " +
				_db.CreateSqlParameterName("Channel_Name") + ", " +
				_db.CreateSqlParameterName("Channel_Description") + ")";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Channel_ID", value.Channel_ID);
			AddParameter(cmd, "Channel_Name", value.Channel_Name);
			AddParameter(cmd, "Channel_Description", value.Channel_Description);
			cmd.ExecuteNonQuery();
		}
		public virtual bool Update(ChannelRow value)
		{
			string sqlStr = "UPDATE [dbo].[Channel] SET " +
				"[Channel_Name]=" + _db.CreateSqlParameterName("Channel_Name") + ", " +
				"[Channel_Description]=" + _db.CreateSqlParameterName("Channel_Description") +
				" WHERE " +
				"[Channel_ID]=" + _db.CreateSqlParameterName("Channel_ID");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Channel_Name", value.Channel_Name);
			AddParameter(cmd, "Channel_Description", value.Channel_Description);
			AddParameter(cmd, "Channel_ID", value.Channel_ID);
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
							DeleteByPrimaryKey((int)row["Channel_ID"]);
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

		public bool Delete(ChannelRow value)
		{
			return DeleteByPrimaryKey(value.Channel_ID);
		}



		public virtual bool DeleteByPrimaryKey(int channel_ID)
		{
			string whereSql = "[Channel_ID]=" + _db.CreateSqlParameterName("Channel_ID");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Channel_ID", channel_ID);
			return 0 < cmd.ExecuteNonQuery();
		}

		public int Delete(string whereSql)
		{
			return CreateDeleteCommand(whereSql).ExecuteNonQuery();
		}

		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[Channel]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		public int DeleteAll()
		{
			return Delete("");
		}

		protected ChannelRow[] MapRecords(IDbCommand command)
		{
			using(IDataReader reader = _db.ExecuteReader(command))
			{
				return MapRecords(reader);
			}
		}


		protected ChannelRow[] MapRecords(IDataReader reader)
		{
			int totalRecordCount = -1;
			return MapRecords(reader, 0, int.MaxValue, ref totalRecordCount);
		}

		protected virtual ChannelRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int channel_IDColumnIndex = reader.GetOrdinal("Channel_ID");
			int channel_NameColumnIndex = reader.GetOrdinal("Channel_Name");
			int channel_DescriptionColumnIndex = reader.GetOrdinal("Channel_Description");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					ChannelRow record = new ChannelRow();
					recordList.Add(record);

					record.Channel_ID = Convert.ToInt32(reader.GetValue(channel_IDColumnIndex));
					if(!reader.IsDBNull(channel_NameColumnIndex))
						record.Channel_Name = Convert.ToString(reader.GetValue(channel_NameColumnIndex));
					if(!reader.IsDBNull(channel_DescriptionColumnIndex))
						record.Channel_Description = Convert.ToString(reader.GetValue(channel_DescriptionColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (ChannelRow[])(recordList.ToArray(typeof(ChannelRow)));
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
		protected virtual ChannelRow MapRow(DataRow row)
		{
			ChannelRow mappedObject = new ChannelRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Channel_ID"
			dataColumn = dataTable.Columns["Channel_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.Channel_ID = (int)row[dataColumn];
			// Column "Channel_Name"
			dataColumn = dataTable.Columns["Channel_Name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Channel_Name = (string)row[dataColumn];
			// Column "Channel_Description"
			dataColumn = dataTable.Columns["Channel_Description"];
			if(!row.IsNull(dataColumn))
				mappedObject.Channel_Description = (string)row[dataColumn];
			return mappedObject;
		}

		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "Channel";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Channel_ID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Channel_Name", typeof(string));
			dataColumn.MaxLength = 50;
			dataColumn = dataTable.Columns.Add("Channel_Description", typeof(string));
			dataColumn.MaxLength = 1073741823;
			return dataTable;
		}
		
		protected virtual IDbDataParameter AddParameter(IDbCommand cmd, string paramName, object value)
		{
			IDbDataParameter parameter;
			switch(paramName)
			{
				case "Channel_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Channel_Name":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "Channel_Description":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}
	}
}
