



using System;
using System.Data;
using System.Collections;

namespace DFISYS.Core.DAL
{
	/// <summary>
	/// The base class for <see cref="News_MediaCollection"/>. Provides methods 
	/// for common database table operations. 
	/// </summary>
	/// <remarks>
	/// Do not change this source code. Update the <see cref="News_MediaCollection"/>
	/// class if you need to add or change some functionality.
	/// </remarks>
	public abstract class News_MediaCollection_Base
	{
		// Constants
		public const string NM_IDColumnName = "NM_ID";
		public const string News_IDColumnName = "News_ID";
		public const string Object_IDColumnName = "Object_ID";
		public const string UseAvatarColumnName = "UseAvatar";
		public const string Use_NoteColumnName = "Use_Note";
		public const string Film_IDColumnName = "Film_ID";

		// Instance fields
		protected MainDB _db;

		/// <summary>
		/// Initializes a new instance of the <see cref="News_MediaCollection_Base"/> 
		/// class with the specified <see cref="MainDB"/>.
		/// </summary>
		/// <param name="db">The <see cref="MainDB"/> object.</param>
		public News_MediaCollection_Base(MainDB db)
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
		/// Gets an array of all records from the <c>News_Media</c> table.
		/// </summary>
		/// <returns>An array of <see cref="News_MediaRow"/> objects.</returns>
		public virtual News_MediaRow[] GetAll()
		{
			return MapRecords(CreateGetAllCommand());
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object that 
		/// includes all records from the <c>News_Media</c> table.
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
		/// to retrieve all records from the <c>News_Media</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetAllCommand()
		{
			return CreateGetCommand(null, null);
		}

		/// <summary>
		/// Gets the first <see cref="News_MediaRow"/> objects that 
		/// match the search condition.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>An instance of <see cref="News_MediaRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public News_MediaRow GetRow(string whereSql)
		{
			int totalRecordCount = -1;
			News_MediaRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
			return 0 == rows.Length ? null : rows[0];
		}

		/// <summary>
		/// Gets an array of <see cref="News_MediaRow"/> objects that 
		/// match the search condition, in the the specified sort order.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. For example: 
		/// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
		/// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
		/// <returns>An array of <see cref="News_MediaRow"/> objects.</returns>
		public News_MediaRow[] GetAsArray(string whereSql, string orderBySql)
		{
			int totalRecordCount = -1;
			return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
		}

		/// <summary>
		/// Gets an array of <see cref="News_MediaRow"/> objects that 
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
		/// <returns>An array of <see cref="News_MediaRow"/> objects.</returns>
		public virtual News_MediaRow[] GetAsArray(string whereSql, string orderBySql,
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

		public virtual News_MediaRow[] GetTopAsArray(int topNum, string whereSql, string orderBySql)
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
			string sqlStr = "Select Count(*) From [dbo].[News_Media]" + ((sqlWhere == "") ? "" : (" Where " + sqlWhere));
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
		/// Gets Custom Page of News_MediaRow
		/// </summary>
		/// <param name="pageNumber">Selected Page Index</param>
		/// <param name="pageSize">Page Size</param>
		/// <param name="whereSql">Where Clause</param>
		/// <param name="orderBySql">Order By Clause</param>
		/// <returns>An Array of News_MediaRow</returns>
		public News_MediaRow[] GetPageAsArray(int pageNumber, int pageSize, string whereSql, string orderBySql)
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
			sql += " FROM [dbo].[News_Media]";
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
			string sql = "SELECT * FROM [dbo].[News_Media]";
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
			string sql = "SELECT TOP " + topNum.ToString() + " * FROM [dbo].[News_Media]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			if(null != orderBySql && 0 < orderBySql.Length)
				sql += " ORDER BY " + orderBySql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Gets <see cref="News_MediaRow"/> by the primary key.
		/// </summary>
		/// <param name="nm_id">The <c>NM_ID</c> column value.</param>
		/// <returns>An instance of <see cref="News_MediaRow"/> or null reference 
		/// (Nothing in Visual Basic) if the object was not found.</returns>
		public virtual News_MediaRow GetByPrimaryKey(long nm_id)
		{
			string whereSql = "[NM_ID]=" + _db.CreateSqlParameterName("@NM_ID");
			IDbCommand cmd = CreateGetCommand(whereSql, null);
			AddParameter(cmd, "@NM_ID", nm_id);
			News_MediaRow[] tempArray = MapRecords(cmd);
			return 0 == tempArray.Length ? null : tempArray[0];
		}

		/// <summary>
		/// Gets an array of <see cref="News_MediaRow"/> objects 
		/// by the <c>FK_News_Media_Film</c> foreign key.
		/// </summary>
		/// <param name="film_ID">The <c>Film_ID</c> column value.</param>
		/// <returns>An array of <see cref="News_MediaRow"/> objects.</returns>
		public News_MediaRow[] GetByFilm_ID(int film_ID)
		{
			return GetByFilm_ID(film_ID, false);
		}

		/// <summary>
		/// Gets an array of <see cref="News_MediaRow"/> objects 
		/// by the <c>FK_News_Media_Film</c> foreign key.
		/// </summary>
		/// <param name="film_ID">The <c>Film_ID</c> column value.</param>
		/// <param name="film_IDNull">true if the method ignores the film_ID
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>An array of <see cref="News_MediaRow"/> objects.</returns>
		public virtual News_MediaRow[] GetByFilm_ID(int film_ID, bool film_IDNull)
		{
			return MapRecords(CreateGetByFilm_IDCommand(film_ID, film_IDNull));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>FK_News_Media_Film</c> foreign key.
		/// </summary>
		/// <param name="film_ID">The <c>Film_ID</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetByFilm_IDAsDataTable(int film_ID)
		{
			return GetByFilm_IDAsDataTable(film_ID, false);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>FK_News_Media_Film</c> foreign key.
		/// </summary>
		/// <param name="film_ID">The <c>Film_ID</c> column value.</param>
		/// <param name="film_IDNull">true if the method ignores the film_ID
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByFilm_IDAsDataTable(int film_ID, bool film_IDNull)
		{
			return MapRecordsToDataTable(CreateGetByFilm_IDCommand(film_ID, film_IDNull));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>FK_News_Media_Film</c> foreign key.
		/// </summary>
		/// <param name="film_ID">The <c>Film_ID</c> column value.</param>
		/// <param name="film_IDNull">true if the method ignores the film_ID
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByFilm_IDCommand(int film_ID, bool film_IDNull)
		{
			string whereSql = "";
			if(film_IDNull)
				whereSql += "[Film_ID] IS NULL";
			else
			whereSql += "[Film_ID]=" + _db.CreateSqlParameterName("@Film_ID");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!film_IDNull)
			AddParameter(cmd, "@Film_ID", film_ID);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="News_MediaRow"/> objects 
		/// by the <c>FK_News_Media_MediaObject</c> foreign key.
		/// </summary>
		/// <param name="object_ID">The <c>Object_ID</c> column value.</param>
		/// <returns>An array of <see cref="News_MediaRow"/> objects.</returns>
		public News_MediaRow[] GetByObject_ID(int object_ID)
		{
			return GetByObject_ID(object_ID, false);
		}

		/// <summary>
		/// Gets an array of <see cref="News_MediaRow"/> objects 
		/// by the <c>FK_News_Media_MediaObject</c> foreign key.
		/// </summary>
		/// <param name="object_ID">The <c>Object_ID</c> column value.</param>
		/// <param name="object_IDNull">true if the method ignores the object_ID
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>An array of <see cref="News_MediaRow"/> objects.</returns>
		public virtual News_MediaRow[] GetByObject_ID(int object_ID, bool object_IDNull)
		{
			return MapRecords(CreateGetByObject_IDCommand(object_ID, object_IDNull));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>FK_News_Media_MediaObject</c> foreign key.
		/// </summary>
		/// <param name="object_ID">The <c>Object_ID</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetByObject_IDAsDataTable(int object_ID)
		{
			return GetByObject_IDAsDataTable(object_ID, false);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>FK_News_Media_MediaObject</c> foreign key.
		/// </summary>
		/// <param name="object_ID">The <c>Object_ID</c> column value.</param>
		/// <param name="object_IDNull">true if the method ignores the object_ID
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByObject_IDAsDataTable(int object_ID, bool object_IDNull)
		{
			return MapRecordsToDataTable(CreateGetByObject_IDCommand(object_ID, object_IDNull));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>FK_News_Media_MediaObject</c> foreign key.
		/// </summary>
		/// <param name="object_ID">The <c>Object_ID</c> column value.</param>
		/// <param name="object_IDNull">true if the method ignores the object_ID
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByObject_IDCommand(int object_ID, bool object_IDNull)
		{
			string whereSql = "";
			if(object_IDNull)
				whereSql += "[Object_ID] IS NULL";
			else
			whereSql += "[Object_ID]=" + _db.CreateSqlParameterName("@Object_ID");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!object_IDNull)
			AddParameter(cmd, "@Object_ID", object_ID);
			return cmd;
		}

		/// <summary>
		/// Gets an array of <see cref="News_MediaRow"/> objects 
		/// by the <c>FK_News_Media_News</c> foreign key.
		/// </summary>
		/// <param name="news_ID">The <c>News_ID</c> column value.</param>
		/// <returns>An array of <see cref="News_MediaRow"/> objects.</returns>
		public News_MediaRow[] GetByNews_ID(long news_ID)
		{
			return GetByNews_ID(news_ID, false);
		}

		/// <summary>
		/// Gets an array of <see cref="News_MediaRow"/> objects 
		/// by the <c>FK_News_Media_News</c> foreign key.
		/// </summary>
		/// <param name="news_ID">The <c>News_ID</c> column value.</param>
		/// <param name="news_IDNull">true if the method ignores the news_ID
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>An array of <see cref="News_MediaRow"/> objects.</returns>
		public virtual News_MediaRow[] GetByNews_ID(long news_ID, bool news_IDNull)
		{
			return MapRecords(CreateGetByNews_IDCommand(news_ID, news_IDNull));
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>FK_News_Media_News</c> foreign key.
		/// </summary>
		/// <param name="news_ID">The <c>News_ID</c> column value.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public DataTable GetByNews_IDAsDataTable(long news_ID)
		{
			return GetByNews_IDAsDataTable(news_ID, false);
		}

		/// <summary>
		/// Gets a <see cref="System.Data.DataTable"/> object 
		/// by the <c>FK_News_Media_News</c> foreign key.
		/// </summary>
		/// <param name="news_ID">The <c>News_ID</c> column value.</param>
		/// <param name="news_IDNull">true if the method ignores the news_ID
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		public virtual DataTable GetByNews_IDAsDataTable(long news_ID, bool news_IDNull)
		{
			return MapRecordsToDataTable(CreateGetByNews_IDCommand(news_ID, news_IDNull));
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
		/// return records by the <c>FK_News_Media_News</c> foreign key.
		/// </summary>
		/// <param name="news_ID">The <c>News_ID</c> column value.</param>
		/// <param name="news_IDNull">true if the method ignores the news_ID
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateGetByNews_IDCommand(long news_ID, bool news_IDNull)
		{
			string whereSql = "";
			if(news_IDNull)
				whereSql += "[News_ID] IS NULL";
			else
			whereSql += "[News_ID]=" + _db.CreateSqlParameterName("@News_ID");

			IDbCommand cmd = CreateGetCommand(whereSql, null);
			if(!news_IDNull)
			AddParameter(cmd, "@News_ID", news_ID);
			return cmd;
		}

		/// <summary>
		/// Adds a new record into the <c>News_Media</c> table.
		/// </summary>
		/// <param name="value">The <see cref="News_MediaRow"/> object to be inserted.</param>
		public virtual void Insert(News_MediaRow value)
		{
			string sqlStr = "INSERT INTO [dbo].[News_Media] (" +
				"[News_ID], " +
				"[Object_ID], " +
				"[UseAvatar], " +
				"[Use_Note], " +
				"[Film_ID]" +
				") VALUES (" +
				_db.CreateSqlParameterName("@News_ID") + ", " +
				_db.CreateSqlParameterName("@Object_ID") + ", " +
				_db.CreateSqlParameterName("@UseAvatar") + ", " +
				_db.CreateSqlParameterName("@Use_Note") + ", " +
				_db.CreateSqlParameterName("@Film_ID") + ");SELECT @@IDENTITY";
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "@News_ID",
				value.IsNews_IDNull ? DBNull.Value : (object)value.News_ID);
			AddParameter(cmd, "@Object_ID",
				value.IsObject_IDNull ? DBNull.Value : (object)value.Object_ID);
			AddParameter(cmd, "@UseAvatar", value.UseAvatar);
			AddParameter(cmd, "@Use_Note", value.Use_Note);
			AddParameter(cmd, "@Film_ID",
				value.IsFilm_IDNull ? DBNull.Value : (object)value.Film_ID);
			value.NM_ID = Convert.ToInt64(cmd.ExecuteScalar());
		}

		/// <summary>
		/// Updates a record in the <c>News_Media</c> table.
		/// </summary>
		/// <param name="value">The <see cref="News_MediaRow"/>
		/// object used to update the table record.</param>
		/// <returns>true if the record was updated; otherwise, false.</returns>
		public virtual bool Update(News_MediaRow value)
		{
			string sqlStr = "UPDATE [dbo].[News_Media] SET " +
				"[News_ID]=" + _db.CreateSqlParameterName("@News_ID") + ", " +
				"[Object_ID]=" + _db.CreateSqlParameterName("@Object_ID") + ", " +
				"[UseAvatar]=" + _db.CreateSqlParameterName("@UseAvatar") + ", " +
				"[Use_Note]=" + _db.CreateSqlParameterName("@Use_Note") + ", " +
				"[Film_ID]=" + _db.CreateSqlParameterName("@Film_ID") +
				" WHERE " +
				"[NM_ID]=" + _db.CreateSqlParameterName("@NM_ID");
			IDbCommand cmd = _db.CreateCommand(sqlStr);
			AddParameter(cmd, "@News_ID",
				value.IsNews_IDNull ? DBNull.Value : (object)value.News_ID);
			AddParameter(cmd, "@Object_ID",
				value.IsObject_IDNull ? DBNull.Value : (object)value.Object_ID);
			AddParameter(cmd, "@UseAvatar", value.UseAvatar);
			AddParameter(cmd, "@Use_Note", value.Use_Note);
			AddParameter(cmd, "@Film_ID",
				value.IsFilm_IDNull ? DBNull.Value : (object)value.Film_ID);
			AddParameter(cmd, "@NM_ID", value.NM_ID);
			return 0 != cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates the <c>News_Media</c> table and calls the <c>AcceptChanges</c> method
		/// on the changed DataRow objects.
		/// </summary>
		/// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
		public void Update(DataTable table)
		{
			Update(table, true);
		}

		/// <summary>
		/// Updates the <c>News_Media</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
							DeleteByPrimaryKey((long)row["NM_ID"]);
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
		/// Deletes the specified object from the <c>News_Media</c> table.
		/// </summary>
		/// <param name="value">The <see cref="News_MediaRow"/> object to delete.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public bool Delete(News_MediaRow value)
		{
			return DeleteByPrimaryKey(value.NM_ID);
		}

		/// <summary>
		/// Deletes a record from the <c>News_Media</c> table using
		/// the specified primary key.
		/// </summary>
		/// <param name="nm_id">The <c>NM_ID</c> column value.</param>
		/// <returns>true if the record was deleted; otherwise, false.</returns>
		public virtual bool DeleteByPrimaryKey(long nm_id)
		{
			string whereSql = "[NM_ID]=" + _db.CreateSqlParameterName("@NM_ID");
			IDbCommand cmd = CreateDeleteCommand(whereSql);
			AddParameter(cmd, "@NM_ID", nm_id);
			return 0 < cmd.ExecuteNonQuery();
		}

		/// <summary>
		/// Deletes records from the <c>News_Media</c> table using the 
		/// <c>FK_News_Media_Film</c> foreign key.
		/// </summary>
		/// <param name="film_ID">The <c>Film_ID</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByFilm_ID(int film_ID)
		{
			return DeleteByFilm_ID(film_ID, false);
		}

		/// <summary>
		/// Deletes records from the <c>News_Media</c> table using the 
		/// <c>FK_News_Media_Film</c> foreign key.
		/// </summary>
		/// <param name="film_ID">The <c>Film_ID</c> column value.</param>
		/// <param name="film_IDNull">true if the method ignores the film_ID
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByFilm_ID(int film_ID, bool film_IDNull)
		{
			return CreateDeleteByFilm_IDCommand(film_ID, film_IDNull).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>FK_News_Media_Film</c> foreign key.
		/// </summary>
		/// <param name="film_ID">The <c>Film_ID</c> column value.</param>
		/// <param name="film_IDNull">true if the method ignores the film_ID
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByFilm_IDCommand(int film_ID, bool film_IDNull)
		{
			string whereSql = "";
			if(film_IDNull)
				whereSql += "[Film_ID] IS NULL";
			else
			whereSql += "[Film_ID]=" + _db.CreateSqlParameterName("@Film_ID");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!film_IDNull)
			AddParameter(cmd, "@Film_ID", film_ID);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>News_Media</c> table using the 
		/// <c>FK_News_Media_MediaObject</c> foreign key.
		/// </summary>
		/// <param name="object_ID">The <c>Object_ID</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByObject_ID(int object_ID)
		{
			return DeleteByObject_ID(object_ID, false);
		}

		/// <summary>
		/// Deletes records from the <c>News_Media</c> table using the 
		/// <c>FK_News_Media_MediaObject</c> foreign key.
		/// </summary>
		/// <param name="object_ID">The <c>Object_ID</c> column value.</param>
		/// <param name="object_IDNull">true if the method ignores the object_ID
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByObject_ID(int object_ID, bool object_IDNull)
		{
			return CreateDeleteByObject_IDCommand(object_ID, object_IDNull).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>FK_News_Media_MediaObject</c> foreign key.
		/// </summary>
		/// <param name="object_ID">The <c>Object_ID</c> column value.</param>
		/// <param name="object_IDNull">true if the method ignores the object_ID
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByObject_IDCommand(int object_ID, bool object_IDNull)
		{
			string whereSql = "";
			if(object_IDNull)
				whereSql += "[Object_ID] IS NULL";
			else
			whereSql += "[Object_ID]=" + _db.CreateSqlParameterName("@Object_ID");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!object_IDNull)
			AddParameter(cmd, "@Object_ID", object_ID);
			return cmd;
		}

		/// <summary>
		/// Deletes records from the <c>News_Media</c> table using the 
		/// <c>FK_News_Media_News</c> foreign key.
		/// </summary>
		/// <param name="news_ID">The <c>News_ID</c> column value.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByNews_ID(long news_ID)
		{
			return DeleteByNews_ID(news_ID, false);
		}

		/// <summary>
		/// Deletes records from the <c>News_Media</c> table using the 
		/// <c>FK_News_Media_News</c> foreign key.
		/// </summary>
		/// <param name="news_ID">The <c>News_ID</c> column value.</param>
		/// <param name="news_IDNull">true if the method ignores the news_ID
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>The number of records deleted from the table.</returns>
		public int DeleteByNews_ID(long news_ID, bool news_IDNull)
		{
			return CreateDeleteByNews_IDCommand(news_ID, news_IDNull).ExecuteNonQuery();
		}

		/// <summary>
		/// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
		/// delete records using the <c>FK_News_Media_News</c> foreign key.
		/// </summary>
		/// <param name="news_ID">The <c>News_ID</c> column value.</param>
		/// <param name="news_IDNull">true if the method ignores the news_ID
		/// parameter value and uses DbNull instead of it; otherwise, false.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteByNews_IDCommand(long news_ID, bool news_IDNull)
		{
			string whereSql = "";
			if(news_IDNull)
				whereSql += "[News_ID] IS NULL";
			else
			whereSql += "[News_ID]=" + _db.CreateSqlParameterName("@News_ID");

			IDbCommand cmd = CreateDeleteCommand(whereSql);
			if(!news_IDNull)
			AddParameter(cmd, "@News_ID", news_ID);
			return cmd;
		}

		/// <summary>
		/// Deletes <c>News_Media</c> records that match the specified criteria.
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
		/// to delete <c>News_Media</c> records that match the specified criteria.
		/// </summary>
		/// <param name="whereSql">The SQL search condition. 
		/// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
		/// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
		protected virtual IDbCommand CreateDeleteCommand(string whereSql)
		{
			string sql = "DELETE FROM [dbo].[News_Media]";
			if(null != whereSql && 0 < whereSql.Length)
				sql += " WHERE " + whereSql;
			return _db.CreateCommand(sql);
		}

		/// <summary>
		/// Deletes all records from the <c>News_Media</c> table.
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
		/// <returns>An array of <see cref="News_MediaRow"/> objects.</returns>
		protected News_MediaRow[] MapRecords(IDbCommand command)
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
		/// <returns>An array of <see cref="News_MediaRow"/> objects.</returns>
		protected News_MediaRow[] MapRecords(IDataReader reader)
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
		/// <returns>An array of <see cref="News_MediaRow"/> objects.</returns>
		protected virtual News_MediaRow[] MapRecords(IDataReader reader, 
										int startIndex, int length, ref int totalRecordCount)
		{
			if(0 > startIndex)
				throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
			if(0 > length)
				throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

			int nm_idColumnIndex = reader.GetOrdinal("NM_ID");
			int news_IDColumnIndex = reader.GetOrdinal("News_ID");
			int object_IDColumnIndex = reader.GetOrdinal("Object_ID");
			int useAvatarColumnIndex = reader.GetOrdinal("UseAvatar");
			int use_NoteColumnIndex = reader.GetOrdinal("Use_Note");
			int film_IDColumnIndex = reader.GetOrdinal("Film_ID");

			System.Collections.ArrayList recordList = new System.Collections.ArrayList();
			int ri = -startIndex;
			while(reader.Read())
			{
				ri++;
				if(ri > 0 && ri <= length)
				{
					News_MediaRow record = new News_MediaRow();
					recordList.Add(record);

					record.NM_ID = Convert.ToInt64(reader.GetValue(nm_idColumnIndex));
					if(!reader.IsDBNull(news_IDColumnIndex))
					record.News_ID = Convert.ToInt64(reader.GetValue(news_IDColumnIndex));
					if(!reader.IsDBNull(object_IDColumnIndex))
					record.Object_ID = Convert.ToInt32(reader.GetValue(object_IDColumnIndex));
					if(!reader.IsDBNull(useAvatarColumnIndex))
					record.UseAvatar = Convert.ToString(reader.GetValue(useAvatarColumnIndex));
					if(!reader.IsDBNull(use_NoteColumnIndex))
					record.Use_Note = Convert.ToString(reader.GetValue(use_NoteColumnIndex));
					if(!reader.IsDBNull(film_IDColumnIndex))
                        record.Film_ID = Convert.ToString(reader.GetValue(film_IDColumnIndex));

					if(ri == length && 0 != totalRecordCount)
						break;
				}
			}

			totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
			return (News_MediaRow[])(recordList.ToArray(typeof(News_MediaRow)));
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
		/// Converts <see cref="System.Data.DataRow"/> to <see cref="News_MediaRow"/>.
		/// </summary>
		/// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
		/// <returns>A reference to the <see cref="News_MediaRow"/> object.</returns>
		protected virtual News_MediaRow MapRow(DataRow row)
		{
			News_MediaRow mappedObject = new News_MediaRow();
			DataTable dataTable = row.Table;
			DataColumn dataColumn;
			// Column "NM_ID"
			dataColumn = dataTable.Columns["NM_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.NM_ID = (long)row[dataColumn];
			// Column "News_ID"
			dataColumn = dataTable.Columns["News_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.News_ID = (long)row[dataColumn];
			// Column "Object_ID"
			dataColumn = dataTable.Columns["Object_ID"];
			if(!row.IsNull(dataColumn))
				mappedObject.Object_ID = (int)row[dataColumn];
			// Column "UseAvatar"
			dataColumn = dataTable.Columns["UseAvatar"];
			if(!row.IsNull(dataColumn))
				mappedObject.UseAvatar = (string)row[dataColumn];
			// Column "Use_Note"
			dataColumn = dataTable.Columns["Use_Note"];
			if(!row.IsNull(dataColumn))
				mappedObject.Use_Note = (string)row[dataColumn];
			// Column "Film_ID"
			dataColumn = dataTable.Columns["Film_ID"];
			if(!row.IsNull(dataColumn))
                mappedObject.Film_ID = (string)row[dataColumn];
			return mappedObject;
		}
		
		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>News_Media</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable(string[] listFields)
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "News_Media";			
			foreach(string column in listFields)
			{
			dataTable.Columns.Add(column, typeof(string));
			}
			return dataTable;
		}

		/// <summary>
		/// Creates a <see cref="System.Data.DataTable"/> object for 
		/// the <c>News_Media</c> table.
		/// </summary>
		/// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
		protected virtual DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.TableName = "News_Media";
			DataColumn dataColumn;
				dataColumn = dataTable.Columns.Add("NM_ID", typeof(long));
				dataColumn.AllowDBNull = false;
				dataColumn.ReadOnly = true;
				dataColumn.Unique = true;
				dataColumn.AutoIncrement = true;
				dataColumn = dataTable.Columns.Add("News_ID", typeof(long));
				dataColumn = dataTable.Columns.Add("Object_ID", typeof(int));
				dataColumn = dataTable.Columns.Add("UseAvatar", typeof(string));
				dataColumn.MaxLength = 200;
				dataColumn = dataTable.Columns.Add("Use_Note", typeof(string));
				dataColumn = dataTable.Columns.Add("Film_ID", typeof(int));
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
				case "@NM_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int64, value);
					break;

				case "@News_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int64, value);
					break;

				case "@Object_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
					break;

				case "@UseAvatar":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@Use_Note":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				case "@Film_ID":
					parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
					break;

				default:
					throw new ArgumentException("Unknown parameter name (" + paramName + ").");
			}
			return parameter;
		}
	} // End of News_MediaCollection_Base class
}  // End of namespace

