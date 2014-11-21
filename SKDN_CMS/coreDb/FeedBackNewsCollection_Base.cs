

using System;
using System.Data;
using System.Collections;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// The base class for <see cref="FeedBackNewsCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="FeedBackNewsCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class FeedBackNewsCollection_Base
	{
		// Constants
		public const string FeedBack_IDColumnName = "FeedBack_ID";
		public const string News_IDColumnName = "News_ID";
		public const string Cat_IDColumnName = "Cat_ID";
		public const string News_TitleColumnName = "News_Title";
		public const string News_SubtitleColumnName = "News_Subtitle";
		public const string News_ImageColumnName = "News_Image";
		public const string News_ImageNoteColumnName = "News_ImageNote";
		public const string News_SourceColumnName = "News_Source";
		public const string News_InitContentColumnName = "News_InitContent";
		public const string News_ContentColumnName = "News_Content";
		public const string News_AthorColumnName = "News_Athor";
		public const string News_ApproverColumnName = "News_Approver";
		public const string News_StatusColumnName = "News_Status";
		public const string News_PublishDateColumnName = "News_PublishDate";
		public const string News_isFocusColumnName = "News_isFocus";
		public const string News_ModeColumnName = "News_Mode";
		public const string News_RelationColumnName = "News_Relation";
		public const string News_RateColumnName = "News_Rate";
		public const string News_ModifedDateColumnName = "News_ModifedDate";
		public const string News_OtherCatColumnName = "News_OtherCat";
		public const string isCommentColumnName = "isComment";
		public const string isUserRateColumnName = "isUserRate";
		public const string TemplateColumnName = "Template";
		public const string IconColumnName = "Icon";
		public const string News_Relation1ColumnName = "News_Relation1";
		public const string NhuanbutColumnName = "Nhuanbut";

		// Instance fields
		protected MainDB _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="FeedBackNewsCollection_Base"/> 
		/// class with the specified <see cref="MainDB"/>.
		/// </summary>
		/// <param name="db">The <see cref="MainDB"/> object.</param>
		public FeedBackNewsCollection_Base(MainDB db)
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
		/// Gets an array of all records from the <c>FeedBackNews</c> table.
		/// </summary>
		/// <returns>An array of <see cref="FeedBackNewsRow"/> objects.</returns>
		public virtual FeedBackNewsRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>FeedBackNews</c> table.
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
		/// to retrieve all records from the <c>FeedBackNews</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="FeedBackNewsRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="FeedBackNewsRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public FeedBackNewsRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			FeedBackNewsRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="FeedBackNewsRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="FeedBackNewsRow"/> objects.</returns>
		public FeedBackNewsRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="FeedBackNewsRow"/> objects that 
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
		/// <returns>An array of <see cref="FeedBackNewsRow"/> objects.</returns>
		public virtual FeedBackNewsRow[] GetAsArray(string whereSql, string orderBySql,
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

		public virtual FeedBackNewsRow[] GetTopAsArray(int topNum, string whereSql, string orderBySql)
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
			string sqlStr = "Select Count(*) From [dbo].[FeedBackNews]" + ((sqlWhere == "") ? "" : (" Where " + sqlWhere));
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
		/// Gets Custom Page of FeedBackNewsRow
		/// </summary>
		/// <param name="pageNumber">Selected Page Index</param>
		/// <param name="pageSize">Page Size</param>
		/// <param name="whereSql">Where Clause</param>
		/// <param name="orderBySql">Order By Clause</param>
		/// <returns>An Array of FeedBackNewsRow</returns>
		public FeedBackNewsRow[] GetPageAsArray(int pageNumber, int pageSize, string whereSql, string orderBySql)
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
			sql += " FROM [dbo].[FeedBackNews]";
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
			string sql = "SELECT * FROM [dbo].[FeedBackNews]";
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
			string sql = "SELECT TOP " + topNum.ToString() + " * FROM [dbo].[FeedBackNews]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="FeedBackNewsRow"/> by the primary key.
		/// </summary>
		/// <param name="feedBack_ID">The <c>FeedBack_ID</c> column value.</param>
		/// <returns>An instance of <see cref="FeedBackNewsRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual FeedBackNewsRow GetByPrimaryKey(long feedBack_ID)
		{
			string whereSql = "[FeedBack_ID]=" + _db.CreateSqlParameterName("@FeedBack_ID");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "@FeedBack_ID", feedBack_ID);
			FeedBackNewsRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Adds a new record into the <c>FeedBackNews</c> table.
		/// </summary>
		/// <param name="value">The <see cref="FeedBackNewsRow"/> object to be inserted.</param>
		public virtual void Insert(FeedBackNewsRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[FeedBackNews] (" +
				"[News_ID], " +
				"[Cat_ID], " +
				"[News_Title], " +
				"[News_Subtitle], " +
				"[News_Image], " +
				"[News_ImageNote], " +
				"[News_Source], " +
				"[News_InitContent], " +
				"[News_Content], " +
				"[News_Athor], " +
				"[News_Approver], " +
				"[News_Status], " +
				"[News_PublishDate], " +
				"[News_isFocus], " +
				"[News_Mode], " +
				"[News_Relation], " +
				"[News_Rate], " +
				"[News_ModifedDate], " +
				"[News_OtherCat], " +
				"[isComment], " +
				"[isUserRate], " +
				"[Template], " +
				"[Icon], " +
				"[News_Relation1], " +
				"[Nhuanbut]" +
				") VALUES (" +
				_db.CreateSqlParameterName("@News_ID") + ", " +
				_db.CreateSqlParameterName("@Cat_ID") + ", " +
				_db.CreateSqlParameterName("@News_Title") + ", " +
				_db.CreateSqlParameterName("@News_Subtitle") + ", " +
				_db.CreateSqlParameterName("@News_Image") + ", " +
				_db.CreateSqlParameterName("@News_ImageNote") + ", " +
				_db.CreateSqlParameterName("@News_Source") + ", " +
				_db.CreateSqlParameterName("@News_InitContent") + ", " +
				_db.CreateSqlParameterName("@News_Content") + ", " +
				_db.CreateSqlParameterName("@News_Athor") + ", " +
				_db.CreateSqlParameterName("@News_Approver") + ", " +
				_db.CreateSqlParameterName("@News_Status") + ", " +
				_db.CreateSqlParameterName("@News_PublishDate") + ", " +
				_db.CreateSqlParameterName("@News_isFocus") + ", " +
				_db.CreateSqlParameterName("@News_Mode") + ", " +
				_db.CreateSqlParameterName("@News_Relation") + ", " +
				_db.CreateSqlParameterName("@News_Rate") + ", " +
				_db.CreateSqlParameterName("@News_ModifedDate") + ", " +
				_db.CreateSqlParameterName("@News_OtherCat") + ", " +
				_db.CreateSqlParameterName("@isComment") + ", " +
				_db.CreateSqlParameterName("@isUserRate") + ", " +
				_db.CreateSqlParameterName("@Template") + ", " +
				_db.CreateSqlParameterName("@Icon") + ", " +
				_db.CreateSqlParameterName("@News_Relation1") + ", " +
				_db.CreateSqlParameterName("@Nhuanbut") + ");SELECT @@IDENTITY";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "@News_ID", value.News_ID);
			AddParameter(cmd, "@Cat_ID",
				value.IsCat_IDNull ? DBNull.Value : (object)value.Cat_ID);
			AddParameter(cmd, "@News_Title", value.News_Title);
			AddParameter(cmd, "@News_Subtitle", value.News_Subtitle);
			AddParameter(cmd, "@News_Image", value.News_Image);
			AddParameter(cmd, "@News_ImageNote", value.News_ImageNote);
			AddParameter(cmd, "@News_Source", value.News_Source);
			AddParameter(cmd, "@News_InitContent", value.News_InitContent);
			AddParameter(cmd, "@News_Content", value.News_Content);
			AddParameter(cmd, "@News_Athor", value.News_Athor);
			AddParameter(cmd, "@News_Approver", value.News_Approver);
			AddParameter(cmd, "@News_Status",
				value.IsNews_StatusNull ? DBNull.Value : (object)value.News_Status);
			AddParameter(cmd, "@News_PublishDate",
				value.IsNews_PublishDateNull ? DBNull.Value : (object)value.News_PublishDate);
			AddParameter(cmd, "@News_isFocus",
				value.IsNews_isFocusNull ? DBNull.Value : (object)value.News_isFocus);
			AddParameter(cmd, "@News_Mode",
				value.IsNews_ModeNull ? DBNull.Value : (object)value.News_Mode);
			AddParameter(cmd, "@News_Relation", value.News_Relation);
			AddParameter(cmd, "@News_Rate",
				value.IsNews_RateNull ? DBNull.Value : (object)value.News_Rate);
			AddParameter(cmd, "@News_ModifedDate",
				value.IsNews_ModifedDateNull ? DBNull.Value : (object)value.News_ModifedDate);
			AddParameter(cmd, "@News_OtherCat", value.News_OtherCat);
			AddParameter(cmd, "@isComment",
				value.IsisCommentNull ? DBNull.Value : (object)value.isComment);
			AddParameter(cmd, "@isUserRate",
				value.IsisUserRateNull ? DBNull.Value : (object)value.isUserRate);
			AddParameter(cmd, "@Template",
				value.IsTemplateNull ? DBNull.Value : (object)value.Template);
			AddParameter(cmd, "@Icon", value.Icon);
			AddParameter(cmd, "@News_Relation1", value.News_Relation1);
			AddParameter(cmd, "@Nhuanbut",
				value.IsNhuanbutNull ? DBNull.Value : (object)value.Nhuanbut);
			value.FeedBack_ID = Convert.ToInt64(cmd.ExecuteScalar());
		}

		/// <summary>
		/// Updates a record in the <c>FeedBackNews</c> table.
		/// </summary>
		/// <param name="value">The <see cref="FeedBackNewsRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(FeedBackNewsRow value)
		{
			string sqlStr = "UPDATE [dbo].[FeedBackNews] SET " +
				"[News_ID]=" + _db.CreateSqlParameterName("@News_ID") + ", " +
				"[Cat_ID]=" + _db.CreateSqlParameterName("@Cat_ID") + ", " +
				"[News_Title]=" + _db.CreateSqlParameterName("@News_Title") + ", " +
				"[News_Subtitle]=" + _db.CreateSqlParameterName("@News_Subtitle") + ", " +
				"[News_Image]=" + _db.CreateSqlParameterName("@News_Image") + ", " +
				"[News_ImageNote]=" + _db.CreateSqlParameterName("@News_ImageNote") + ", " +
				"[News_Source]=" + _db.CreateSqlParameterName("@News_Source") + ", " +
				"[News_InitContent]=" + _db.CreateSqlParameterName("@News_InitContent") + ", " +
				"[News_Content]=" + _db.CreateSqlParameterName("@News_Content") + ", " +
				"[News_Athor]=" + _db.CreateSqlParameterName("@News_Athor") + ", " +
				"[News_Approver]=" + _db.CreateSqlParameterName("@News_Approver") + ", " +
				"[News_Status]=" + _db.CreateSqlParameterName("@News_Status") + ", " +
				"[News_PublishDate]=" + _db.CreateSqlParameterName("@News_PublishDate") + ", " +
				"[News_isFocus]=" + _db.CreateSqlParameterName("@News_isFocus") + ", " +
				"[News_Mode]=" + _db.CreateSqlParameterName("@News_Mode") + ", " +
				"[News_Relation]=" + _db.CreateSqlParameterName("@News_Relation") + ", " +
				"[News_Rate]=" + _db.CreateSqlParameterName("@News_Rate") + ", " +
				"[News_ModifedDate]=" + _db.CreateSqlParameterName("@News_ModifedDate") + ", " +
				"[News_OtherCat]=" + _db.CreateSqlParameterName("@News_OtherCat") + ", " +
				"[isComment]=" + _db.CreateSqlParameterName("@isComment") + ", " +
				"[isUserRate]=" + _db.CreateSqlParameterName("@isUserRate") + ", " +
				"[Template]=" + _db.CreateSqlParameterName("@Template") + ", " +
				"[Icon]=" + _db.CreateSqlParameterName("@Icon") + ", " +
				"[News_Relation1]=" + _db.CreateSqlParameterName("@News_Relation1") + ", " +
				"[Nhuanbut]=" + _db.CreateSqlParameterName("@Nhuanbut") +
				" WHERE " +
				"[FeedBack_ID]=" + _db.CreateSqlParameterName("@FeedBack_ID");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "@News_ID", value.News_ID);
			AddParameter(cmd, "@Cat_ID",
				value.IsCat_IDNull ? DBNull.Value : (object)value.Cat_ID);
			AddParameter(cmd, "@News_Title", value.News_Title);
			AddParameter(cmd, "@News_Subtitle", value.News_Subtitle);
			AddParameter(cmd, "@News_Image", value.News_Image);
			AddParameter(cmd, "@News_ImageNote", value.News_ImageNote);
			AddParameter(cmd, "@News_Source", value.News_Source);
			AddParameter(cmd, "@News_InitContent", value.News_InitContent);
			AddParameter(cmd, "@News_Content", value.News_Content);
			AddParameter(cmd, "@News_Athor", value.News_Athor);
			AddParameter(cmd, "@News_Approver", value.News_Approver);
			AddParameter(cmd, "@News_Status",
				value.IsNews_StatusNull ? DBNull.Value : (object)value.News_Status);
			AddParameter(cmd, "@News_PublishDate",
				value.IsNews_PublishDateNull ? DBNull.Value : (object)value.News_PublishDate);
			AddParameter(cmd, "@News_isFocus",
				value.IsNews_isFocusNull ? DBNull.Value : (object)value.News_isFocus);
			AddParameter(cmd, "@News_Mode",
				value.IsNews_ModeNull ? DBNull.Value : (object)value.News_Mode);
			AddParameter(cmd, "@News_Relation", value.News_Relation);
			AddParameter(cmd, "@News_Rate",
				value.IsNews_RateNull ? DBNull.Value : (object)value.News_Rate);
			AddParameter(cmd, "@News_ModifedDate",
				value.IsNews_ModifedDateNull ? DBNull.Value : (object)value.News_ModifedDate);
			AddParameter(cmd, "@News_OtherCat", value.News_OtherCat);
			AddParameter(cmd, "@isComment",
				value.IsisCommentNull ? DBNull.Value : (object)value.isComment);
			AddParameter(cmd, "@isUserRate",
				value.IsisUserRateNull ? DBNull.Value : (object)value.isUserRate);
			AddParameter(cmd, "@Template",
				value.IsTemplateNull ? DBNull.Value : (object)value.Template);
			AddParameter(cmd, "@Icon", value.Icon);
			AddParameter(cmd, "@News_Relation1", value.News_Relation1);
			AddParameter(cmd, "@Nhuanbut",
				value.IsNhuanbutNull ? DBNull.Value : (object)value.Nhuanbut);
			AddParameter(cmd, "@FeedBack_ID", value.FeedBack_ID);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>FeedBackNews</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>FeedBackNews</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((long)row["FeedBack_ID"]);
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
		/// Deletes the specified object from the <c>FeedBackNews</c> table.
		/// </summary>
		/// <param name="value">The <see cref="FeedBackNewsRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(FeedBackNewsRow value)
		{
			return DeleteByPrimaryKey(value.FeedBack_ID);
		}

		/// <summary>
		/// Deletes a record from the <c>FeedBackNews</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="feedBack_ID">The <c>FeedBack_ID</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(long feedBack_ID)
		{
			string whereSql = "[FeedBack_ID]=" + _db.CreateSqlParameterName("@FeedBack_ID");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "@FeedBack_ID", feedBack_ID);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes <c>FeedBackNews</c> records that match the specified criteria.
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
		/// to delete <c>FeedBackNews</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[FeedBackNews]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>FeedBackNews</c> table.
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
		/// <returns>An array of <see cref="FeedBackNewsRow"/> objects.</returns>
		protected FeedBackNewsRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="FeedBackNewsRow"/> objects.</returns>
		protected FeedBackNewsRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="FeedBackNewsRow"/> objects.</returns>
		protected virtual FeedBackNewsRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int feedBack_IDColumnIndex = reader.GetOrdinal("FeedBack_ID");
			int news_IDColumnIndex = reader.GetOrdinal("News_ID");
			int cat_IDColumnIndex = reader.GetOrdinal("Cat_ID");
			int news_TitleColumnIndex = reader.GetOrdinal("News_Title");
			int news_SubtitleColumnIndex = reader.GetOrdinal("News_Subtitle");
			int news_ImageColumnIndex = reader.GetOrdinal("News_Image");
			int news_ImageNoteColumnIndex = reader.GetOrdinal("News_ImageNote");
			int news_SourceColumnIndex = reader.GetOrdinal("News_Source");
			int news_InitContentColumnIndex = reader.GetOrdinal("News_InitContent");
			int news_ContentColumnIndex = reader.GetOrdinal("News_Content");
			int news_AthorColumnIndex = reader.GetOrdinal("News_Athor");
			int news_ApproverColumnIndex = reader.GetOrdinal("News_Approver");
			int news_StatusColumnIndex = reader.GetOrdinal("News_Status");
			int news_PublishDateColumnIndex = reader.GetOrdinal("News_PublishDate");
			int news_isFocusColumnIndex = reader.GetOrdinal("News_isFocus");
			int news_ModeColumnIndex = reader.GetOrdinal("News_Mode");
			int news_RelationColumnIndex = reader.GetOrdinal("News_Relation");
			int news_RateColumnIndex = reader.GetOrdinal("News_Rate");
			int news_ModifedDateColumnIndex = reader.GetOrdinal("News_ModifedDate");
			int news_OtherCatColumnIndex = reader.GetOrdinal("News_OtherCat");
			int isCommentColumnIndex = reader.GetOrdinal("isComment");
			int isUserRateColumnIndex = reader.GetOrdinal("isUserRate");
			int templateColumnIndex = reader.GetOrdinal("Template");
			int iconColumnIndex = reader.GetOrdinal("Icon");
			int news_Relation1ColumnIndex = reader.GetOrdinal("News_Relation1");
			int nhuanbutColumnIndex = reader.GetOrdinal("Nhuanbut");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					FeedBackNewsRow record = new FeedBackNewsRow();
					recordList.Add(record);

					record.FeedBack_ID = Convert.ToInt64(reader.GetValue(feedBack_IDColumnIndex));
					record.News_ID = Convert.ToInt64(reader.GetValue(news_IDColumnIndex));
					if(!reader.IsDBNull(cat_IDColumnIndex))
					record.Cat_ID = Convert.ToInt32(reader.GetValue(cat_IDColumnIndex));
					if(!reader.IsDBNull(news_TitleColumnIndex))
					record.News_Title = Convert.ToString(reader.GetValue(news_TitleColumnIndex));
					if(!reader.IsDBNull(news_SubtitleColumnIndex))
					record.News_Subtitle = Convert.ToString(reader.GetValue(news_SubtitleColumnIndex));
					if(!reader.IsDBNull(news_ImageColumnIndex))
					record.News_Image = Convert.ToString(reader.GetValue(news_ImageColumnIndex));
					if(!reader.IsDBNull(news_ImageNoteColumnIndex))
					record.News_ImageNote = Convert.ToString(reader.GetValue(news_ImageNoteColumnIndex));
					if(!reader.IsDBNull(news_SourceColumnIndex))
					record.News_Source = Convert.ToString(reader.GetValue(news_SourceColumnIndex));
					if(!reader.IsDBNull(news_InitContentColumnIndex))
					record.News_InitContent = Convert.ToString(reader.GetValue(news_InitContentColumnIndex));
					if(!reader.IsDBNull(news_ContentColumnIndex))
					record.News_Content = Convert.ToString(reader.GetValue(news_ContentColumnIndex));
					if(!reader.IsDBNull(news_AthorColumnIndex))
					record.News_Athor = Convert.ToString(reader.GetValue(news_AthorColumnIndex));
					if(!reader.IsDBNull(news_ApproverColumnIndex))
					record.News_Approver = Convert.ToString(reader.GetValue(news_ApproverColumnIndex));
					if(!reader.IsDBNull(news_StatusColumnIndex))
					record.News_Status = Convert.ToInt32(reader.GetValue(news_StatusColumnIndex));
					if(!reader.IsDBNull(news_PublishDateColumnIndex))
					record.News_PublishDate = Convert.ToDateTime(reader.GetValue(news_PublishDateColumnIndex));
					if(!reader.IsDBNull(news_isFocusColumnIndex))
					record.News_isFocus = Convert.ToBoolean(reader.GetValue(news_isFocusColumnIndex));
					if(!reader.IsDBNull(news_ModeColumnIndex))
					record.News_Mode = Convert.ToInt32(reader.GetValue(news_ModeColumnIndex));
					if(!reader.IsDBNull(news_RelationColumnIndex))
					record.News_Relation = Convert.ToString(reader.GetValue(news_RelationColumnIndex));
					if(!reader.IsDBNull(news_RateColumnIndex))
					record.News_Rate = Convert.ToDouble(reader.GetValue(news_RateColumnIndex));
					if(!reader.IsDBNull(news_ModifedDateColumnIndex))
					record.News_ModifedDate = Convert.ToDateTime(reader.GetValue(news_ModifedDateColumnIndex));
					if(!reader.IsDBNull(news_OtherCatColumnIndex))
					record.News_OtherCat = Convert.ToString(reader.GetValue(news_OtherCatColumnIndex));
					if(!reader.IsDBNull(isCommentColumnIndex))
					record.isComment = Convert.ToBoolean(reader.GetValue(isCommentColumnIndex));
					if(!reader.IsDBNull(isUserRateColumnIndex))
					record.isUserRate = Convert.ToBoolean(reader.GetValue(isUserRateColumnIndex));
					if(!reader.IsDBNull(templateColumnIndex))
					record.Template = Convert.ToInt32(reader.GetValue(templateColumnIndex));
					if(!reader.IsDBNull(iconColumnIndex))
					record.Icon = Convert.ToString(reader.GetValue(iconColumnIndex));
					if(!reader.IsDBNull(news_Relation1ColumnIndex))
					record.News_Relation1 = Convert.ToString(reader.GetValue(news_Relation1ColumnIndex));
					if(!reader.IsDBNull(nhuanbutColumnIndex))
					record.Nhuanbut = Convert.ToInt32(reader.GetValue(nhuanbutColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (FeedBackNewsRow[])(recordList.ToArray(typeof(FeedBackNewsRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="FeedBackNewsRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="FeedBackNewsRow"/> object.</returns>
		protected virtual FeedBackNewsRow MapRow(DataRow row)
		{
			FeedBackNewsRow mappedObject = new FeedBackNewsRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "FeedBack_ID"
			dataColumn = dataTable.Columns["FeedBack_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.FeedBack_ID = (long)row[dataColumn];
			// Column "News_ID"
			dataColumn = dataTable.Columns["News_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_ID = (long)row[dataColumn];
			// Column "Cat_ID"
			dataColumn = dataTable.Columns["Cat_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.Cat_ID = (int)row[dataColumn];
			// Column "News_Title"
			dataColumn = dataTable.Columns["News_Title"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_Title = (string)row[dataColumn];
			// Column "News_Subtitle"
			dataColumn = dataTable.Columns["News_Subtitle"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_Subtitle = (string)row[dataColumn];
			// Column "News_Image"
			dataColumn = dataTable.Columns["News_Image"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_Image = (string)row[dataColumn];
			// Column "News_ImageNote"
			dataColumn = dataTable.Columns["News_ImageNote"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_ImageNote = (string)row[dataColumn];
			// Column "News_Source"
			dataColumn = dataTable.Columns["News_Source"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_Source = (string)row[dataColumn];
			// Column "News_InitContent"
			dataColumn = dataTable.Columns["News_InitContent"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_InitContent = (string)row[dataColumn];
			// Column "News_Content"
			dataColumn = dataTable.Columns["News_Content"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_Content = (string)row[dataColumn];
			// Column "News_Athor"
			dataColumn = dataTable.Columns["News_Athor"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_Athor = (string)row[dataColumn];
			// Column "News_Approver"
			dataColumn = dataTable.Columns["News_Approver"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_Approver = (string)row[dataColumn];
			// Column "News_Status"
			dataColumn = dataTable.Columns["News_Status"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_Status = (int)row[dataColumn];
			// Column "News_PublishDate"
			dataColumn = dataTable.Columns["News_PublishDate"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_PublishDate = (DateTime)row[dataColumn];
			// Column "News_isFocus"
			dataColumn = dataTable.Columns["News_isFocus"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_isFocus = (bool)row[dataColumn];
			// Column "News_Mode"
			dataColumn = dataTable.Columns["News_Mode"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_Mode = (int)row[dataColumn];
			// Column "News_Relation"
			dataColumn = dataTable.Columns["News_Relation"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_Relation = (string)row[dataColumn];
			// Column "News_Rate"
			dataColumn = dataTable.Columns["News_Rate"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_Rate = (double)row[dataColumn];
			// Column "News_ModifedDate"
			dataColumn = dataTable.Columns["News_ModifedDate"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_ModifedDate = (DateTime)row[dataColumn];
			// Column "News_OtherCat"
			dataColumn = dataTable.Columns["News_OtherCat"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_OtherCat = (string)row[dataColumn];
			// Column "isComment"
			dataColumn = dataTable.Columns["isComment"];
			if(!row.IsNull(dataColumn))
				mappedObject.isComment = (bool)row[dataColumn];
			// Column "isUserRate"
			dataColumn = dataTable.Columns["isUserRate"];
			if(!row.IsNull(dataColumn))
				mappedObject.isUserRate = (bool)row[dataColumn];
			// Column "Template"
			dataColumn = dataTable.Columns["Template"];
			if(!row.IsNull(dataColumn))
				mappedObject.Template = (int)row[dataColumn];
			// Column "Icon"
			dataColumn = dataTable.Columns["Icon"];
			if(!row.IsNull(dataColumn))
				mappedObject.Icon = (string)row[dataColumn];
			// Column "News_Relation1"
			dataColumn = dataTable.Columns["News_Relation1"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_Relation1 = (string)row[dataColumn];
			// Column "Nhuanbut"
			dataColumn = dataTable.Columns["Nhuanbut"];
			if(!row.IsNull(dataColumn))
				mappedObject.Nhuanbut = (int)row[dataColumn];
			return mappedObject;
		}
		
		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>FeedBackNews</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable(string[] listFields)
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "FeedBackNews";			
			foreach(string column in listFields)
			{
			dataTable.Columns.Add(column, typeof(string));
			}
			return dataTable;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>FeedBackNews</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "FeedBackNews";
			DataColumn dataColumn;
				dataColumn = dataTable.Columns.Add("FeedBack_ID", typeof(long));
				dataColumn.AllowDBNull = false;
				dataColumn.ReadOnly = true;
				dataColumn.Unique = true;
				dataColumn.AutoIncrement = true;
				dataColumn = dataTable.Columns.Add("News_ID", typeof(long));
				dataColumn.AllowDBNull = false;
				dataColumn = dataTable.Columns.Add("Cat_ID", typeof(int));
				dataColumn = dataTable.Columns.Add("News_Title", typeof(string));
				dataColumn.MaxLength = 500;
				dataColumn = dataTable.Columns.Add("News_Subtitle", typeof(string));
				dataColumn.MaxLength = 500;
				dataColumn = dataTable.Columns.Add("News_Image", typeof(string));
				dataColumn.MaxLength = 200;
				dataColumn = dataTable.Columns.Add("News_ImageNote", typeof(string));
				dataColumn = dataTable.Columns.Add("News_Source", typeof(string));
				dataColumn.MaxLength = 200;
				dataColumn = dataTable.Columns.Add("News_InitContent", typeof(string));
				dataColumn = dataTable.Columns.Add("News_Content", typeof(string));
				dataColumn = dataTable.Columns.Add("News_Athor", typeof(string));
				dataColumn.MaxLength = 200;
				dataColumn = dataTable.Columns.Add("News_Approver", typeof(string));
				dataColumn.MaxLength = 200;
				dataColumn = dataTable.Columns.Add("News_Status", typeof(int));
				dataColumn = dataTable.Columns.Add("News_PublishDate", typeof(DateTime));
				dataColumn = dataTable.Columns.Add("News_isFocus", typeof(bool));
				dataColumn = dataTable.Columns.Add("News_Mode", typeof(int));
				dataColumn = dataTable.Columns.Add("News_Relation", typeof(string));
				dataColumn.MaxLength = 1000;
				dataColumn = dataTable.Columns.Add("News_Rate", typeof(double));
				dataColumn = dataTable.Columns.Add("News_ModifedDate", typeof(DateTime));
				dataColumn = dataTable.Columns.Add("News_OtherCat", typeof(string));
				dataColumn.MaxLength = 50;
				dataColumn = dataTable.Columns.Add("isComment", typeof(bool));
				dataColumn = dataTable.Columns.Add("isUserRate", typeof(bool));
				dataColumn = dataTable.Columns.Add("Template", typeof(int));
				dataColumn = dataTable.Columns.Add("Icon", typeof(string));
				dataColumn.MaxLength = 200;
				dataColumn = dataTable.Columns.Add("News_Relation1", typeof(string));
				dataColumn.MaxLength = 500;
				dataColumn = dataTable.Columns.Add("Nhuanbut", typeof(int));
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
				case "@FeedBack_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int64, value);
					break;

				case "@News_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int64, value);
					break;

				case "@Cat_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "@News_Title":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@News_Subtitle":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@News_Image":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@News_ImageNote":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@News_Source":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@News_InitContent":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@News_Content":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@News_Athor":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@News_Approver":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@News_Status":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "@News_PublishDate":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "@News_isFocus":
					parameter = _db.AddParameter(cmd, paramName, DbType.Boolean, value);
					break;

				case "@News_Mode":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "@News_Relation":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@News_Rate":
					parameter = _db.AddParameter(cmd, paramName, DbType.Double, value);
					break;

				case "@News_ModifedDate":
					parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
					break;

				case "@News_OtherCat":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@isComment":
					parameter = _db.AddParameter(cmd, paramName, DbType.Boolean, value);
					break;

				case "@isUserRate":
					parameter = _db.AddParameter(cmd, paramName, DbType.Boolean, value);
					break;

				case "@Template":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "@Icon":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@News_Relation1":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@Nhuanbut":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}
	} // End of FeedBackNewsCollection_Base class
}  // End of namespace

