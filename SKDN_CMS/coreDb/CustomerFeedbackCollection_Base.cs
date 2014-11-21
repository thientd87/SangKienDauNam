



using System;
using System.Data;
using System.Collections;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// The base class for <see cref="CustomerFeedbackCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="CustomerFeedbackCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class CustomerFeedbackCollection_Base
	{
		// Constants
		public const string Idea_IDColumnName = "Idea_ID";
		public const string News_IDColumnName = "News_ID";
		public const string Reader_NameColumnName = "Reader_Name";
		public const string Reader_AddrColumnName = "Reader_Addr";
		public const string Reader_MailColumnName = "Reader_Mail";
		public const string Idea_TitleColumnName = "Idea_Title";
		public const string Idea_ContentColumnName = "Idea_Content";
		public const string Idea_DateColumnName = "Idea_Date";
		public const string ApprovedColumnName = "Approved";

		// Instance fields
		protected MainDB _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomerFeedbackCollection_Base"/> 
		/// class with the specified <see cref="MainDB"/>.
		/// </summary>
		/// <param name="db">The <see cref="MainDB"/> object.</param>
		public CustomerFeedbackCollection_Base(MainDB db)
		{
			_db = db;
		}

		/// <summary>
		/// Gets the database object that this table belongs to.
		///	</summary>
		///	<value>The <see cref="MainDB"/> object.</value>
		protected MainDB Database
		{
			get { return _db; }
		}

		/// <summary>
		/// Gets an array of all records from the <c>CustomerFeedback</c> table.
		/// </summary>
		/// <returns>An array of <see cref="CustomerFeedbackRow"/> objects.</returns>
		public virtual CustomerFeedbackRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>CustomerFeedback</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAllAsDataTable()
		{
			return MapRecordsToDataTable(CreateGetAllCommand());
		}

		public string GetDateFormat(DateTime ObjDate)
		{
			string Dateformat = "#";
			Dateformat += ObjDate.Month.ToString();
			Dateformat += "-";
			Dateformat += ObjDate.Day.ToString();
			Dateformat += "-";
			Dateformat += ObjDate.Year.ToString();
			Dateformat += "#";
			return Dateformat;
		}
		
		public DataTable Searching(string StrCol, string StrCmp)
		{
			StrProcess ObjStr = new StrProcess();
			DataTable DTable = new DataTable("Unknown");
			DataTable Dt = GetAllAsDataTable();
			DTable = Dt.Clone();
			if (Dt.Columns.Contains(StrCol))
			{
				foreach (DataRow Dr in Dt.Rows)
				{
					if (ObjStr.IsSubString(ObjStr.StandNonUnicode(Dr[StrCol].ToString()),ObjStr.StandNonUnicode(StrCmp)))
					{	DTable.ImportRow(Dr);	}
				}
			} else DTable = null; 
			return DTable;
		}
		public DataTable Searching(string StrColumnName, int IntValue)
		{
			DataTable Dt = GetAllAsDataTable();
			if (Dt.Columns.Contains(StrColumnName))
			{
				return GetAsDataTable("" + StrColumnName + "="+ IntValue,"");
			} else return null;
		}
		public DataTable Searching(string StrColumnName, int IntValue, bool Above)
		{
			DataTable Dt = GetAllAsDataTable();
			if (Dt.Columns.Contains(StrColumnName))
			{
				if (Above)
					return GetAsDataTable("" + StrColumnName + ">="+ IntValue,"");
				else 
					return GetAsDataTable("" + StrColumnName + "<="+ IntValue,"");
			} 
			else return null;
		}
		public DataTable Searching(string StrColumnName, int IntValueStart, int IntValueEnd)
		{
			DataTable Dt = GetAllAsDataTable();
			if (Dt.Columns.Contains(StrColumnName))
			{
				return GetAsDataTable("" + StrColumnName + ">="+ IntValueStart +" AND " + StrColumnName + "<=" + IntValueEnd,"");
			} 
			else return null;
		}
		
		public DataTable Searching(string StrColumnName, DateTime ObjDate)
		{
			DataTable Dt = GetAllAsDataTable();
			if (Dt.Columns.Contains(StrColumnName))
				 return GetAsDataTable("" + StrColumnName + "="+ GetDateFormat(ObjDate),"");
			else return null;
		}
		public DataTable Searching(string StrColumnName, DateTime ObjDate, bool Before)
		{
			DataTable Dt = GetAllAsDataTable();
			if (Dt.Columns.Contains(StrColumnName))
				if  (Before)
					return GetAsDataTable("" + StrColumnName + "<="+ GetDateFormat(ObjDate),"");
				else
					return GetAsDataTable("" + StrColumnName + ">="+ GetDateFormat(ObjDate),"");
			else return null;
		}
		public DataTable Searching(string StrColumnName, DateTime ObjDateStart, DateTime ObjDateEnd)
		{
			DataTable Dt = GetAllAsDataTable();
			if (Dt.Columns.Contains(StrColumnName))
				 return GetAsDataTable("" + StrColumnName + ">="+ GetDateFormat(ObjDateStart)+" AND " + StrColumnName + "<=" + GetDateFormat(ObjDateEnd),"");
			else return null;
		}
		public DataTable Searching(string StrColumnName, bool Status)
		{
			DataTable Dt = GetAllAsDataTable();
			if (Dt.Columns.Contains(StrColumnName))
				return GetAsDataTable("" + StrColumnName + "="+ Status,"");
			else return null;
		}
		/// <summary>
		/// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
		/// to retrieve all records from the <c>CustomerFeedback</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="CustomerFeedbackRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="CustomerFeedbackRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public CustomerFeedbackRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			CustomerFeedbackRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="CustomerFeedbackRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="CustomerFeedbackRow"/> objects.</returns>
		public CustomerFeedbackRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="CustomerFeedbackRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example:
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <param name="startIndex">The index of the first record to return.</param>
		/// <param name="length">The number of records to return.</param>
		/// <param name="totalRecordCount">A reference parameter that returns the total number 
		/// of records in the reader object if 0 was passed into the method; otherwise it returns -1.</param>
		/// <returns>An array of <see cref="CustomerFeedbackRow"/> objects.</returns>
		public virtual CustomerFeedbackRow[] GetAsArray(string whereSql, string orderBySql,
							int startIndex, int length, ref int totalRecordCount)
		{
			using(IDataReader reader = _db.ExecuteReader(CreateGetCommand(whereSql, orderBySql)))
			{
				return MapRecords(reader, startIndex, length, ref totalRecordCount);
			}
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object filled with data that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: "FirstName='Smith' AND Zip=75038".</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: "LastName ASC, FirstName ASC".</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetAsDataTable(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsDataTable(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object filled with data that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: "FirstName='Smith' AND Zip=75038".</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: "LastName ASC, FirstName ASC".</param>
		/// <param name="startIndex">The index of the first record to return.</param>
		/// <param name="length">The number of records to return.</param>
		/// <param name="totalRecordCount">A reference parameter that returns the total number 
		/// of records in the reader object if 0 was passed into the method; otherwise it returns -1.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAsDataTable(string whereSql, string orderBySql,
							int startIndex, int length, ref int totalRecordCount)
		{
			using(IDataReader reader = _db.ExecuteReader(CreateGetCommand(whereSql, orderBySql)))
			{
				return MapRecordsToDataTable(reader, startIndex, length, ref totalRecordCount);
			}
		}

		public virtual CustomerFeedbackRow[] GetTopAsArray(int topNum, string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			using(IDataReader reader = _db.ExecuteReader(CreateGetTopCommand(topNum, whereSql, orderBySql)))
			{
				return MapRecords(reader, 0, int.MaxValue, ref totalRecordCount);
			}
		}

		public virtual DataTable GetTopAsDataTable(int topNum, string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			using(IDataReader reader = _db.ExecuteReader(CreateGetTopCommand(topNum, whereSql, orderBySql)))
			{
				return MapRecordsToDataTable(reader, 0, int.MaxValue, ref totalRecordCount);
			}
		}
		
		public virtual int GetCount(string sqlWhere)
		{
			string sqlStr = "Select Count(*) From [dbo].[CustomerFeedback]" + ((sqlWhere == "") ? "" : (" Where " + sqlWhere));
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			return Convert.ToInt32(cmd.ExecuteScalar());
		}
		
		public int GetPagesNum(int pageSize, string sqlWhere)
		{
			if (pageSize > 0)
			{
				int totalRows = GetCount(sqlWhere);
				int divPages = Convert.ToInt32(totalRows / pageSize);
				if (totalRows % pageSize > 0)
				{
					divPages++;
				}
				return divPages;
			}
			else
			{
				return 0;
			}
		}
		
		/// <summary>
		/// Gets Custom Page of CustomerFeedbackRow
		/// </summary>
		/// <param name="pageNumber">Selected Page Index</param>
		/// <param name="pageSize">Page Size</param>
		/// <param name="whereSql">Where Clause</param>
		/// <param name="orderBySql">Order By Clause</param>
		/// <returns>An Array of CustomerFeedbackRow</returns>
		public CustomerFeedbackRow[] GetPageAsArray(int pageNumber, int pageSize, string whereSql, string orderBySql)
		{
			int startIndex = (pageNumber - 1) * pageSize;
			int totalRecordCount = -1;
			return GetAsArray(whereSql,	orderBySql, startIndex, pageSize, ref totalRecordCount);
		}
		
		/// <summary>
		/// Gets Custom Page As DataTable
		/// </summary>
		/// <param name="pageNumber">Selected Page Index</param>
		/// <param name="pageSize">Page Size</param>
		/// <param name="whereSql">Where Clause</param>
		/// <param name="orderBySql">Order By Clause</param>
		/// <returns>A reference of DataTable</returns>
		public DataTable GetPageAsDataTable(int pageNumber, int pageSize, string whereSql, string orderBySql)
		{
			int startIndex = (pageNumber - 1) * pageSize;
			int totalRecordCount = -1;
			return GetAsDataTable(whereSql,	orderBySql, startIndex, pageSize, ref totalRecordCount);
		}
		
		/// <summary>
		/// Make Dynamic Fields List
		/// </summary>
		/// <param name="fields">Field separate by ','</param>
		/// <returns></returns>
		public string[] MakeListFields(params string[] fields)
		{
			return fields;
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object filled with data that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="listFields">Fields List.</param>
		/// <param name="whereSql">The SQL search condition. For example: "FirstName='Smith' AND Zip=75038".</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: "LastName ASC, FirstName ASC".</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetAsDataTable(string[] listFields, string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsDataTable(listFields, whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object filled with data that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="listFields">Fields List.</param>
		/// <param name="whereSql">The SQL search condition. For example: "FirstName='Smith' AND Zip=75038".</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: "LastName ASC, FirstName ASC".</param>
		/// <param name="startIndex">The index of the first record to return.</param>
		/// <param name="length">The number of records to return.</param>
		/// <param name="totalRecordCount">A reference parameter that returns the total number 
		/// of records in the reader object if 0 was passed into the method; otherwise it returns -1.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetAsDataTable(string[] listFields, string whereSql, string orderBySql,
			int startIndex, int length, ref int totalRecordCount)
		{
			using(IDataReader reader = _db.ExecuteReader(CreateGetCommand(listFields, whereSql, orderBySql)))
			{
				return MapRecordsToDataTable(listFields, reader, startIndex, length, ref totalRecordCount);
			}
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object for the specified search criteria.
		/// </summary>
		/// <param name="listFields">Fields List</param>
		/// <param name="whereSql">The SQL search condition. For example: "FirstName='Smith' AND Zip=75038".</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: "LastName ASC, FirstName ASC".</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetCommand(string[] listFields, string whereSql, string orderBySql)
		{
			string sql = "SELECT ";
			for (int _fieldCount = 0;_fieldCount < listFields.Length; _fieldCount++)
			{
				sql += ((_fieldCount > 0) ? ", " : "") + listFields[_fieldCount];
			}
			sql += " FROM [dbo].[CustomerFeedback]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}
		

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object for the specified search criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: "FirstName='Smith' AND Zip=75038".</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: "LastName ASC, FirstName ASC".</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetCommand(string whereSql, string orderBySql)
		{
			string sql = "SELECT * FROM [dbo].[CustomerFeedback]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object for the specified search criteria.
		/// </summary>
		/// <param name="topNum">Numer of Top Row to be returned</param>
		/// <param name="whereSql">The SQL search condition. For example: "FirstName='Smith' AND Zip=75038".</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: "LastName ASC, FirstName ASC".</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetTopCommand(int topNum, string whereSql, string orderBySql)
		{
			string sql = "SELECT TOP " + topNum.ToString() + " * FROM [dbo].[CustomerFeedback]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="CustomerFeedbackRow"/> by the primary key.
		/// </summary>
		/// <param name="idea_ID">The <c>Idea_ID</c> column value.</param>
		/// <returns>An instance of <see cref="CustomerFeedbackRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual CustomerFeedbackRow GetByPrimaryKey(int idea_ID)
		{
			string whereSql = "[Idea_ID]=" + _db.CreateSqlParameterName("@Idea_ID");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "@Idea_ID", idea_ID);
			CustomerFeedbackRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Adds a new record into the <c>CustomerFeedback</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CustomerFeedbackRow"/> object to be inserted.</param>
		public virtual void Insert(CustomerFeedbackRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[CustomerFeedback] (" +
				"[News_ID], " +
				"[Reader_Name], " +
				"[Reader_Addr], " +
				"[Reader_Mail], " +
				"[Idea_Title], " +
				"[Idea_Content], " +
				"[Idea_Date], " +
				"[Approved]" +
				") VALUES (" +
				_db.CreateSqlParameterName("@News_ID") + ", " +
				_db.CreateSqlParameterName("@Reader_Name") + ", " +
				_db.CreateSqlParameterName("@Reader_Addr") + ", " +
				_db.CreateSqlParameterName("@Reader_Mail") + ", " +
				_db.CreateSqlParameterName("@Idea_Title") + ", " +
				_db.CreateSqlParameterName("@Idea_Content") + ", " +
				_db.CreateSqlParameterName("@Idea_Date") + ", " +
				_db.CreateSqlParameterName("@Approved") + ");SELECT @@IDENTITY";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "@News_ID",
				value.IsNews_IDNull ? DBNull.Value : (object)value.News_ID);
			AddParameter(cmd, "@Reader_Name", value.Reader_Name);
			AddParameter(cmd, "@Reader_Addr", value.Reader_Addr);
			AddParameter(cmd, "@Reader_Mail", value.Reader_Mail);
			AddParameter(cmd, "@Idea_Title", value.Idea_Title);
			AddParameter(cmd, "@Idea_Content", value.Idea_Content);
			AddParameter(cmd, "@Idea_Date",
				value.IsIdea_DateNull ? DBNull.Value : (object)value.Idea_Date);
			AddParameter(cmd, "@Approved",
				value.IsApprovedNull ? DBNull.Value : (object)value.Approved);
			value.Idea_ID = Convert.ToInt32(cmd.ExecuteScalar());
		}

		/// <summary>
		/// Updates a record in the <c>CustomerFeedback</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CustomerFeedbackRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(CustomerFeedbackRow value)
		{
			string sqlStr = "UPDATE [dbo].[CustomerFeedback] SET " +
				"[News_ID]=" + _db.CreateSqlParameterName("@News_ID") + ", " +
				"[Reader_Name]=" + _db.CreateSqlParameterName("@Reader_Name") + ", " +
				"[Reader_Addr]=" + _db.CreateSqlParameterName("@Reader_Addr") + ", " +
				"[Reader_Mail]=" + _db.CreateSqlParameterName("@Reader_Mail") + ", " +
				"[Idea_Title]=" + _db.CreateSqlParameterName("@Idea_Title") + ", " +
				"[Idea_Content]=" + _db.CreateSqlParameterName("@Idea_Content") + ", " +
				"[Idea_Date]=" + _db.CreateSqlParameterName("@Idea_Date") + ", " +
				"[Approved]=" + _db.CreateSqlParameterName("@Approved") +
				" WHERE " +
				"[Idea_ID]=" + _db.CreateSqlParameterName("@Idea_ID");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "@News_ID",
				value.IsNews_IDNull ? DBNull.Value : (object)value.News_ID);
			AddParameter(cmd, "@Reader_Name", value.Reader_Name);
			AddParameter(cmd, "@Reader_Addr", value.Reader_Addr);
			AddParameter(cmd, "@Reader_Mail", value.Reader_Mail);
			AddParameter(cmd, "@Idea_Title", value.Idea_Title);
			AddParameter(cmd, "@Idea_Content", value.Idea_Content);
			AddParameter(cmd, "@Idea_Date",
				value.IsIdea_DateNull ? DBNull.Value : (object)value.Idea_Date);
			AddParameter(cmd, "@Approved",
				value.IsApprovedNull ? DBNull.Value : (object)value.Approved);
			AddParameter(cmd, "@Idea_ID", value.Idea_ID);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>CustomerFeedback</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>CustomerFeedback</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
		/// argument when your code calls this method in an ADO.NET transaction context. Note that in 
		/// this case, after you call the Update method you need call either <c>AcceptChanges</c> 
		/// or <c>RejectChanges</c> method on the DataTable object.
		/// <code>
		/// MyDb db = new MyDb();
		/// try
		/// {
		///		db.BeginTransaction();
		///		db.MyCollection.Update(myDataTable, false);
		///		db.CommitTransaction();
		///		myDataTable.AcceptChanges();
		/// }
		/// catch(Exception)
		/// {
		///		db.RollbackTransaction();
		///		myDataTable.RejectChanges();
		/// }
		/// </code>
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		/// <param name="acceptChanges">Specifies whether this method calls the <c>AcceptChanges</c>
		/// method on the changed DataRow objects.</param>
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
							DeleteByPrimaryKey((int)row["Idea_ID"]);
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

		/// <summary>
		/// Deletes the specified object from the <c>CustomerFeedback</c> table.
		/// </summary>
		/// <param name="value">The <see cref="CustomerFeedbackRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(CustomerFeedbackRow value)
		{
			return DeleteByPrimaryKey(value.Idea_ID);
		}

		/// <summary>
		/// Deletes a record from the <c>CustomerFeedback</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="idea_ID">The <c>Idea_ID</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(int idea_ID)
		{
			string whereSql = "[Idea_ID]=" + _db.CreateSqlParameterName("@Idea_ID");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "@Idea_ID", idea_ID);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes <c>CustomerFeedback</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>The number of deleted records.</returns>
		public int Delete(string whereSql)
		{
			return CreateDeleteCommand(whereSql).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used 
		/// to delete <c>CustomerFeedback</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[CustomerFeedback]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>CustomerFeedback</c> table.
		/// </summary>
		/// <returns>The number of deleted records.</returns>
		public int DeleteAll()
		{
			return Delete("");
		}

		/// <summary>
		/// Reads data using the specified command and returns 
		/// an array of mapped objects.
		/// </summary>
		/// <param name="command">The <see cref="System.Data.IDbCommand"/> object.</param>
		/// <returns>An array of <see cref="CustomerFeedbackRow"/> objects.</returns>
		protected CustomerFeedbackRow[] MapRecords(IDbCommand command)
		{
			using(IDataReader reader = _db.ExecuteReader(command))
			{
				return MapRecords(reader);
			}
		}

		/// <summary>
		/// Reads data from the provided data reader and returns 
		/// an array of mapped objects.
		/// </summary>
		/// <param name="reader">The <see cref="System.Data.IDataReader"/> object to read data from the table.</param>
		/// <returns>An array of <see cref="CustomerFeedbackRow"/> objects.</returns>
		protected CustomerFeedbackRow[] MapRecords(IDataReader reader)
		{
			int totalRecordCount = -1;
			return MapRecords(reader, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Reads data from the provided data reader and returns 
		/// an array of mapped objects.
		/// </summary>
		/// <param name="reader">The <see cref="System.Data.IDataReader"/> object to read data from the table.</param>
		/// <param name="startIndex">The index of the first record to map.</param>
		/// <param name="length">The number of records to map.</param>
		/// <param name="totalRecordCount">A reference parameter that returns the total number 
		/// of records in the reader object if 0 was passed into the method; otherwise it returns -1.</param>
		/// <returns>An array of <see cref="CustomerFeedbackRow"/> objects.</returns>
		protected virtual CustomerFeedbackRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int idea_IDColumnIndex = reader.GetOrdinal("Idea_ID");
			int news_IDColumnIndex = reader.GetOrdinal("News_ID");
			int reader_NameColumnIndex = reader.GetOrdinal("Reader_Name");
			int reader_AddrColumnIndex = reader.GetOrdinal("Reader_Addr");
			int reader_MailColumnIndex = reader.GetOrdinal("Reader_Mail");
			int idea_TitleColumnIndex = reader.GetOrdinal("Idea_Title");
			int idea_ContentColumnIndex = reader.GetOrdinal("Idea_Content");
			int idea_DateColumnIndex = reader.GetOrdinal("Idea_Date");
			int approvedColumnIndex = reader.GetOrdinal("Approved");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					CustomerFeedbackRow record = new CustomerFeedbackRow();
					recordList.Add(record);

					record.Idea_ID = Convert.ToInt32(reader.GetValue(idea_IDColumnIndex));
					if(!reader.IsDBNull(news_IDColumnIndex))
					record.News_ID = Convert.ToInt32(reader.GetValue(news_IDColumnIndex));
					if(!reader.IsDBNull(reader_NameColumnIndex))
					record.Reader_Name = Convert.ToString(reader.GetValue(reader_NameColumnIndex));
					if(!reader.IsDBNull(reader_AddrColumnIndex))
					record.Reader_Addr = Convert.ToString(reader.GetValue(reader_AddrColumnIndex));
					if(!reader.IsDBNull(reader_MailColumnIndex))
					record.Reader_Mail = Convert.ToString(reader.GetValue(reader_MailColumnIndex));
					if(!reader.IsDBNull(idea_TitleColumnIndex))
					record.Idea_Title = Convert.ToString(reader.GetValue(idea_TitleColumnIndex));
					if(!reader.IsDBNull(idea_ContentColumnIndex))
					record.Idea_Content = Convert.ToString(reader.GetValue(idea_ContentColumnIndex));
					if(!reader.IsDBNull(idea_DateColumnIndex))
					record.Idea_Date = Convert.ToDateTime(reader.GetValue(idea_DateColumnIndex));
					if(!reader.IsDBNull(approvedColumnIndex))
					record.Approved = Convert.ToBoolean(reader.GetValue(approvedColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (CustomerFeedbackRow[])(recordList.ToArray(typeof(CustomerFeedbackRow)));
		}

		/// <summary>
		/// Reads data using the specified command and returns 
		/// a filled <see cref="System.Data.DataTable"/> object.
		/// </summary>
		/// <param name="command">The <see cref="System.Data.IDbCommand"/> object.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected DataTable MapRecordsToDataTable(IDbCommand command)
		{
			using(IDataReader reader = _db.ExecuteReader(command))
			{
				return MapRecordsToDataTable(reader);
			}
		}

		/// <summary>
		/// Reads data from the provided data reader and returns 
		/// a filled <see cref="System.Data.DataTable"/> object.
		/// </summary>
		/// <param name="reader">The <see cref="System.Data.IDataReader"/> object to read data from the table.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected DataTable MapRecordsToDataTable(IDataReader reader)
		{
			int totalRecordCount = 0;
			return MapRecordsToDataTable(reader, 0, int.MaxValue, ref totalRecordCount);
		}
		
		/// <summary>
		/// Reads data from the provided data reader and returns 
		/// a filled <see cref="System.Data.DataTable"/> object.
		/// </summary>
		/// <param name="reader">The <see cref="System.Data.IDataReader"/> object to read data from the table.</param>
		/// <param name="startIndex">The index of the first record to read.</param>
		/// <param name="length">The number of records to read.</param>
		/// <param name="totalRecordCount">A reference parameter that returns the total number 
		/// of records in the reader object if 0 was passed into the method; otherwise it returns -1.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
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
		
		/// <summary>
		/// Reads data from the provided data reader and returns 
		/// a filled <see cref="System.Data.DataTable"/> object.
		/// </summary>
		/// <param name="listFields">Fields List.</param>
		/// <param name="reader">The <see cref="System.Data.IDataReader"/> object to read data from the table.</param>
		/// <param name="startIndex">The index of the first record to read.</param>
		/// <param name="length">The number of records to read.</param>
		/// <param name="totalRecordCount">A reference parameter that returns the total number 
		/// of records in the reader object if 0 was passed into the method; otherwise it returns -1.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable MapRecordsToDataTable(string[] listFields, IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int columnCount = reader.FieldCount;
			int ri = -startIndex;
			
			DataTable dataTable = CreateDataTable(listFields);
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

		/// <summary>
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="CustomerFeedbackRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="CustomerFeedbackRow"/> object.</returns>
		protected virtual CustomerFeedbackRow MapRow(DataRow row)
		{
			CustomerFeedbackRow mappedObject = new CustomerFeedbackRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "Idea_ID"
			dataColumn = dataTable.Columns["Idea_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.Idea_ID = (int)row[dataColumn];
			// Column "News_ID"
			dataColumn = dataTable.Columns["News_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_ID = (int)row[dataColumn];
			// Column "Reader_Name"
			dataColumn = dataTable.Columns["Reader_Name"];
			if(!row.IsNull(dataColumn))
				mappedObject.Reader_Name = (string)row[dataColumn];
			// Column "Reader_Addr"
			dataColumn = dataTable.Columns["Reader_Addr"];
			if(!row.IsNull(dataColumn))
				mappedObject.Reader_Addr = (string)row[dataColumn];
			// Column "Reader_Mail"
			dataColumn = dataTable.Columns["Reader_Mail"];
			if(!row.IsNull(dataColumn))
				mappedObject.Reader_Mail = (string)row[dataColumn];
			// Column "Idea_Title"
			dataColumn = dataTable.Columns["Idea_Title"];
			if(!row.IsNull(dataColumn))
				mappedObject.Idea_Title = (string)row[dataColumn];
			// Column "Idea_Content"
			dataColumn = dataTable.Columns["Idea_Content"];
			if(!row.IsNull(dataColumn))
				mappedObject.Idea_Content = (string)row[dataColumn];
			// Column "Idea_Date"
			dataColumn = dataTable.Columns["Idea_Date"];
			if(!row.IsNull(dataColumn))
				mappedObject.Idea_Date = (DateTime)row[dataColumn];
			// Column "Approved"
			dataColumn = dataTable.Columns["Approved"];
			if(!row.IsNull(dataColumn))
				mappedObject.Approved = (bool)row[dataColumn];
			return mappedObject;
		}
		
		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>CustomerFeedback</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable(string[] listFields)
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "CustomerFeedback";			
			foreach(string column in listFields)
			{
			dataTable.Columns.Add(column, typeof(string));
			}
			return dataTable;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>CustomerFeedback</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "CustomerFeedback";
			DataColumn dataColumn;
				dataColumn = dataTable.Columns.Add("Idea_ID", typeof(int));
				dataColumn.AllowDBNull = false;
				dataColumn.ReadOnly = true;
				dataColumn.Unique = true;
				dataColumn.AutoIncrement = true;
				dataColumn = dataTable.Columns.Add("News_ID", typeof(int));
				dataColumn = dataTable.Columns.Add("Reader_Name", typeof(string));
				dataColumn.MaxLength = 500;
				dataColumn = dataTable.Columns.Add("Reader_Addr", typeof(string));
				dataColumn.MaxLength = 500;
				dataColumn = dataTable.Columns.Add("Reader_Mail", typeof(string));
				dataColumn.MaxLength = 500;
				dataColumn = dataTable.Columns.Add("Idea_Title", typeof(string));
				dataColumn.MaxLength = 500;
				dataColumn = dataTable.Columns.Add("Idea_Content", typeof(string));
				dataColumn = dataTable.Columns.Add("Idea_Date", typeof(DateTime));
				dataColumn = dataTable.Columns.Add("Approved", typeof(bool));
			return dataTable;
		}
		
		/// <summary>
		/// Adds a new parameter to the specified command.
		/// </summary>
		/// <param name="cmd">The <see cref="System.Data.IDbCommand"/> object to add the parameter to.</param>
		/// <param name="paramName">The name of the parameter.</param>
		/// <param name="value">The value of the parameter.</param>
		/// <returns>A reference to the added parameter.</returns>
		protected virtual IDbDataParameter AddParameter(IDbCommand cmd, string paramName, object value)
		{
			IDbDataParameter parameter;
			switch(paramName)
			{
				case "@Idea_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "@News_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "@Reader_Name":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@Reader_Addr":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@Reader_Mail":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@Idea_Title":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@Idea_Content":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@Idea_Date":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "@Approved":
					parameter = _db.AddParameter(cmd, paramName, DbType.Boolean, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}
	} // End of CustomerFeedbackCollection_Base class
}  // End of namespace

