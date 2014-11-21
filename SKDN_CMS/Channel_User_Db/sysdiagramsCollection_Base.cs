
using System;
using System.Data;

namespace Portal.User.Db
{
	public abstract class sysdiagramsCollection_Base
	{
		// Constants
		public const string NameColumnName = "name";
		public const string Principal_idColumnName = "principal_id";
		public const string Diagram_idColumnName = "diagram_id";
		public const string VersionColumnName = "version";
		public const string DefinitionColumnName = "definition";

		// Instance fields
		private MainDB _db;

		public sysdiagramsCollection_Base(MainDB db)
		{
			_db = db;
		}

		protected MainDB Database
		{
			get { return _db; }
		}

		public virtual sysdiagramsRow[] GetAll()
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

		public sysdiagramsRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			sysdiagramsRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		public sysdiagramsRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		public virtual sysdiagramsRow[] GetAsArray(string whereSql, string orderBySql,
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
			string sql = "SELECT * FROM [dbo].[sysdiagrams]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}
		
		
		public virtual sysdiagramsRow GetByPrimaryKey(int diagram_id)
		{
			string whereSql = "[diagram_id]=" + _db.CreateSqlParameterName("Diagram_id");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "Diagram_id", diagram_id);
			sysdiagramsRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		public virtual void Insert(sysdiagramsRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[sysdiagrams] (" +
				"[name], " +
				"[principal_id], " +
				"[version], " +
				"[definition]" +
				") VALUES (" +
				_db.CreateSqlParameterName("Name") + ", " +
				_db.CreateSqlParameterName("Principal_id") + ", " +
				_db.CreateSqlParameterName("Version") + ", " +
				_db.CreateSqlParameterName("Definition") + ");SELECT @@IDENTITY";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Name", value.Name);
			AddParameter(cmd, "Principal_id", value.Principal_id);
			AddParameter(cmd, "Version",
				value.IsVersionNull ? DBNull.Value : (object)value.Version);
			AddParameter(cmd, "Definition", value.Definition);
			value.Diagram_id = Convert.ToInt32(cmd.ExecuteScalar());
		}
		public virtual bool Update(sysdiagramsRow value)
		{
			string sqlStr = "UPDATE [dbo].[sysdiagrams] SET " +
				"[name]=" + _db.CreateSqlParameterName("Name") + ", " +
				"[principal_id]=" + _db.CreateSqlParameterName("Principal_id") + ", " +
				"[version]=" + _db.CreateSqlParameterName("Version") + ", " +
				"[definition]=" + _db.CreateSqlParameterName("Definition") +
				" WHERE " +
				"[diagram_id]=" + _db.CreateSqlParameterName("Diagram_id");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "Name", value.Name);
			AddParameter(cmd, "Principal_id", value.Principal_id);
			AddParameter(cmd, "Version",
				value.IsVersionNull ? DBNull.Value : (object)value.Version);
			AddParameter(cmd, "Definition", value.Definition);
			AddParameter(cmd, "Diagram_id", value.Diagram_id);
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
							DeleteByPrimaryKey((int)row["Diagram_id"]);
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

		public bool Delete(sysdiagramsRow value)
		{
			return DeleteByPrimaryKey(value.Diagram_id);
		}



		public virtual bool DeleteByPrimaryKey(int diagram_id)
		{
			string whereSql = "[diagram_id]=" + _db.CreateSqlParameterName("Diagram_id");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "Diagram_id", diagram_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		public int Delete(string whereSql)
		{
			return CreateDeleteCommand(whereSql).ExecuteNonQuery();
		}

		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[sysdiagrams]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		public int DeleteAll()
		{
			return Delete("");
		}

		protected sysdiagramsRow[] MapRecords(IDbCommand command)
		{
			using(IDataReader reader = _db.ExecuteReader(command))
			{
				return MapRecords(reader);
			}
		}


		protected sysdiagramsRow[] MapRecords(IDataReader reader)
		{
			int totalRecordCount = -1;
			return MapRecords(reader, 0, int.MaxValue, ref totalRecordCount);
		}

		protected virtual sysdiagramsRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int nameColumnIndex = reader.GetOrdinal("name");
			int principal_idColumnIndex = reader.GetOrdinal("principal_id");
			int diagram_idColumnIndex = reader.GetOrdinal("diagram_id");
			int versionColumnIndex = reader.GetOrdinal("version");
			int definitionColumnIndex = reader.GetOrdinal("definition");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					sysdiagramsRow record = new sysdiagramsRow();
					recordList.Add(record);

					record.Name = Convert.ToString(reader.GetValue(nameColumnIndex));
					record.Principal_id = Convert.ToInt32(reader.GetValue(principal_idColumnIndex));
					record.Diagram_id = Convert.ToInt32(reader.GetValue(diagram_idColumnIndex));
					if(!reader.IsDBNull(versionColumnIndex))
						record.Version = Convert.ToInt32(reader.GetValue(versionColumnIndex));
					if(!reader.IsDBNull(definitionColumnIndex))
						record.Definition = (byte[])reader.GetValue(definitionColumnIndex);

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (sysdiagramsRow[])(recordList.ToArray(typeof(sysdiagramsRow)));
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
		protected virtual sysdiagramsRow MapRow(DataRow row)
		{
			sysdiagramsRow mappedObject = new sysdiagramsRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Name"
			dataColumn = dataTable.Columns["Name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Name = (string)row[dataColumn];
			// Column "Principal_id"
			dataColumn = dataTable.Columns["Principal_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Principal_id = (int)row[dataColumn];
			// Column "Diagram_id"
			dataColumn = dataTable.Columns["Diagram_id"];
			if(!row.IsNull(dataColumn))
				mappedObject.Diagram_id = (int)row[dataColumn];
			// Column "Version"
			dataColumn = dataTable.Columns["Version"];
			if(!row.IsNull(dataColumn))
				mappedObject.Version = (int)row[dataColumn];
			// Column "Definition"
			dataColumn = dataTable.Columns["Definition"];
			if(!row.IsNull(dataColumn))
				mappedObject.Definition = (byte[])row[dataColumn];
			return mappedObject;
		}

		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "sysdiagrams";
			DataColumn dataColumn;
			dataColumn = dataTable.Columns.Add("Name", typeof(string));
			dataColumn.Caption = "name";
			dataColumn.MaxLength = 128;
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Principal_id", typeof(int));
			dataColumn.Caption = "principal_id";
			dataColumn.AllowDBNull = false;
			dataColumn = dataTable.Columns.Add("Diagram_id", typeof(int));
			dataColumn.Caption = "diagram_id";
			dataColumn.AllowDBNull = false;
			dataColumn.ReadOnly = true;
			dataColumn.Unique = true;
			dataColumn.AutoIncrement = true;
			dataColumn = dataTable.Columns.Add("Version", typeof(int));
			dataColumn.Caption = "version";
			dataColumn = dataTable.Columns.Add("Definition", typeof(byte[]));
			dataColumn.Caption = "definition";
			return dataTable;
		}
		
		protected virtual IDbDataParameter AddParameter(IDbCommand cmd, string paramName, object value)
		{
			IDbDataParameter parameter;
			switch(paramName)
			{
				case "Name":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "Principal_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Diagram_id":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Version":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "Definition":
					parameter = _db.AddParameter(cmd, paramName, DbType.Binary, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}
	}
}
