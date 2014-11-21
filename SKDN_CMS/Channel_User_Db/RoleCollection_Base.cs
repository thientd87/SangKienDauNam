
using System;
using System.Data;

namespace DFISYS.User.Db
{
	public abstract class RoleCollection_Base
	{
		// Constants
		public const string Role_IDColumnName = "Role_ID";
		public const string Role_NameColumnName = "Role_Name";
		public const string Role_DescriptionColumnName = "Role_Description";

		// Instance fields
		private MainDB _db;

		public RoleCollection_Base(MainDB db)
		{
			_db = db;
		}

		protected MainDB Database
		{
			get { return _db; }
		}

		public virtual RoleRow[] GetAll()
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

		public RoleRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			RoleRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		public RoleRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		public virtual RoleRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[Role]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}
		
		
		public virtual RoleRow GetByPrimaryKey(int role_ID)
		{
			string whereSql = "[Role_ID]=" + _db.CreateSqlParameterName("Role_ID");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Role_ID", role_ID);
			RoleRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		public virtual void Insert(RoleRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[Role] (" +
				"[Role_Name], " +
				"[Role_Description]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Role_Name") + ", " +
				_db.CreateSqlParameterName("Role_Description") + ");SELECT @@IDENTITY";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Role_Name", value.Role_Name);
			AddParameter(cmd, "Role_Description", value.Role_Description);
			value.Role_ID = Convert.ToInt32(cmd.ExecuteScalar());
		}
		public virtual bool Update(RoleRow value)
		{
			string sqlStr = "UPDATE [dbo].[Role] SET " +
				"[Role_Name]=" + _db.CreateSqlParameterName("Role_Name") + ", " +
				"[Role_Description]=" + _db.CreateSqlParameterName("Role_Description") +
				" WHERE " +
				"[Role_ID]=" + _db.CreateSqlParameterName("Role_ID");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Role_Name", value.Role_Name);
			AddParameter(cmd, "Role_Description", value.Role_Description);
			AddParameter(cmd, "Role_ID", value.Role_ID);
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
							DeleteByPrimaryKey((int)row["Role_ID"]);
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

		public bool Delete(RoleRow value)
		{
			return DeleteByPrimaryKey(value.Role_ID);
		}



		public virtual bool DeleteByPrimaryKey(int role_ID)
		{
			string whereSql = "[Role_ID]=" + _db.CreateSqlParameterName("Role_ID");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Role_ID", role_ID);
			return 0 < cmd.ExecuteNonQuery();
		}

		public int Delete(string whereSql)
		{
			return CreateDeleteCommand(whereSql).ExecuteNonQuery();
		}

		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[Role]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		public int DeleteAll()
		{
			return Delete("");
		}

		protected RoleRow[] MapRecords(IDbCommand command)
		{
			using(IDataReader reader = _db.ExecuteReader(command))
			{
				return MapRecords(reader);
			}
		}


		protected RoleRow[] MapRecords(IDataReader reader)
		{
			int totalRecordCount = -1;
			return MapRecords(reader, 0, int.MaxValue, ref totalRecordCount);
		}

		protected virtual RoleRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int role_IDColumnIndex = reader.GetOrdinal("Role_ID");
			int role_NameColumnIndex = reader.GetOrdinal("Role_Name");
			int role_DescriptionColumnIndex = reader.GetOrdinal("Role_Description");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					RoleRow record = new RoleRow();
					recordList.Add(record);

					record.Role_ID = Convert.ToInt32(reader.GetValue(role_IDColumnIndex));
					if(!reader.IsDBNull(role_NameColumnIndex))
						record.Role_Name = Convert.ToString(reader.GetValue(role_NameColumnIndex));
					if(!reader.IsDBNull(role_DescriptionColumnIndex))
						record.Role_Description = Convert.ToString(reader.GetValue(role_DescriptionColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (RoleRow[])(recordList.ToArray(typeof(RoleRow)));
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
		protected virtual RoleRow MapRow(DataRow row)
		{
			RoleRow mappedObject = new RoleRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Role_ID"
			dataColumn = dataTable.Columns["Role_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.Role_ID = (int)row[dataColumn];
			// Column "Role_Name"
			dataColumn = dataTable.Columns["Role_Name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Role_Name = (string)row[dataColumn];
			// Column "Role_Description"
			dataColumn = dataTable.Columns["Role_Description"];
			if(!row.IsNull(dataColumn))
				mappedObject.Role_Description = (string)row[dataColumn];
			return mappedObject;
		}

		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "Role";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Role_ID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn.Unique = true;
			dataColumn.AutoIncrement = true;
			dataColumn = dataTable.Columns.Add("Role_Name", typeof(string));
			dataColumn.MaxLength = 50;
			dataColumn = dataTable.Columns.Add("Role_Description", typeof(string));
			dataColumn.MaxLength = 1000;
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

				case "Role_Name":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "Role_Description":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}
	}
}
