



using System;
using System.Data;
using System.Collections;

namespace DFISYS.Core.DAL {
    /// <summary>
    /// The base class for <see cref="NewsCollection"/>. Provides methods 
    /// for common database table operations. 
    /// </summary>
    /// <remarks>
    /// Do not change this source code. Update the <see cref="NewsCollection"/>
    /// class if you need to add or change some functionality.
    /// </remarks>
    public abstract class NewsCollection_Base {
        // Constants
        public const string News_IDColumnName = "News_ID";
        public const string Cat_IDColumnName = "Cat_ID";
        public const string News_TitleColumnName = "News_Title";
        public const string News_SubtitleColumnName = "News_Subtitle";
        public const string News_ImageColumnName = "News_Image";
        public const string News_ImageNoteColumnName = "News_ImageNote";
        public const string News_SourceColumnName = "News_Source";
        public const string News_InitialContentColumnName = "News_InitialContent";
        public const string News_ContentColumnName = "News_Content";
        public const string News_AuthorColumnName = "News_Author";
        public const string News_CurrEditorColumnName = "News_CurrEditor";
        public const string News_ApproverColumnName = "News_Approver";
        public const string News_StatusColumnName = "News_Status";
        public const string News_SwitchTimeColumnName = "News_SwitchTime";
        public const string News_PublishDateColumnName = "News_PublishDate";
        public const string News_isFocusColumnName = "News_isFocus";
        public const string News_ModeColumnName = "News_Mode";
        public const string News_ViewNumColumnName = "News_ViewNum";
        public const string News_CreateDateColumnName = "News_CreateDate";
        public const string News_ModifiedDateColumnName = "News_ModifiedDate";
        public const string News_RelationColumnName = "News_Relation";
        public const string News_RateColumnName = "News_Rate";
        public const string News_OtherCatColumnName = "News_OtherCat";
        public const string isCommentColumnName = "isComment";
        public const string isUserRateColumnName = "isUserRate";
        public const string TemplateColumnName = "Template";
        public const string IconColumnName = "Icon";
        public const string Extension1ColumnName = "Extension1";
        public const string Extension2ColumnName = "Extension2";
        public const string Extension3ColumnName = "Extension3";
        public const string Extension4ColumnName = "Extension4";


        // Instance fields
        protected MainDB _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsCollection_Base"/> 
        /// class with the specified <see cref="MainDB"/>.
        /// </summary>
        /// <param name="db">The <see cref="MainDB"/> object.</param>
        public NewsCollection_Base(MainDB db) {
            _db = db;
        }

        /// <summary>
        /// Gets the database object that this table belongs to.
        ///	</summary>
        ///	<value>The <see cref="MainDB"/> object.</value>
        protected MainDB Database {
            get { return _db; }
        }

        /// <summary>
        /// Gets an array of all records from the <c>News</c> table.
        /// </summary>
        /// <returns>An array of <see cref="NewsRow"/> objects.</returns>
        public virtual NewsRow[] GetAll() {
            return MapRecords(CreateGetAllCommand());
        }

        /// <summary>
        /// Gets a <see cref="System.Data.DataTable"/> object that 
        /// includes all records from the <c>News</c> table.
        /// </summary>
        /// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
        public virtual DataTable GetAllAsDataTable() {
            return MapRecordsToDataTable(CreateGetAllCommand());
        }

        public string GetDateFormat(DateTime ObjDate) {
            string Dateformat = "#";
            Dateformat += ObjDate.Month.ToString();
            Dateformat += "-";
            Dateformat += ObjDate.Day.ToString();
            Dateformat += "-";
            Dateformat += ObjDate.Year.ToString();
            Dateformat += "#";
            return Dateformat;
        }

        public DataTable Searching(string StrCol, string StrCmp) {
            StrProcess ObjStr = new StrProcess();
            DataTable DTable = new DataTable("Unknown");
            DataTable Dt = GetAllAsDataTable();
            DTable = Dt.Clone();
            if (Dt.Columns.Contains(StrCol)) {
                foreach (DataRow Dr in Dt.Rows) {
                    if (ObjStr.IsSubString(ObjStr.StandNonUnicode(Dr[StrCol].ToString()), ObjStr.StandNonUnicode(StrCmp))) { DTable.ImportRow(Dr); }
                }
            } else DTable = null;
            return DTable;
        }
        public DataTable Searching(string StrColumnName, int IntValue) {
            DataTable Dt = GetAllAsDataTable();
            if (Dt.Columns.Contains(StrColumnName)) {
                return GetAsDataTable("" + StrColumnName + "=" + IntValue, "");
            } else return null;
        }
        public DataTable Searching(string StrColumnName, int IntValue, bool Above) {
            DataTable Dt = GetAllAsDataTable();
            if (Dt.Columns.Contains(StrColumnName)) {
                if (Above)
                    return GetAsDataTable("" + StrColumnName + ">=" + IntValue, "");
                else
                    return GetAsDataTable("" + StrColumnName + "<=" + IntValue, "");
            } else return null;
        }
        public DataTable Searching(string StrColumnName, int IntValueStart, int IntValueEnd) {
            DataTable Dt = GetAllAsDataTable();
            if (Dt.Columns.Contains(StrColumnName)) {
                return GetAsDataTable("" + StrColumnName + ">=" + IntValueStart + " AND " + StrColumnName + "<=" + IntValueEnd, "");
            } else return null;
        }

        public DataTable Searching(string StrColumnName, DateTime ObjDate) {
            DataTable Dt = GetAllAsDataTable();
            if (Dt.Columns.Contains(StrColumnName))
                return GetAsDataTable("" + StrColumnName + "=" + GetDateFormat(ObjDate), "");
            else return null;
        }
        public DataTable Searching(string StrColumnName, DateTime ObjDate, bool Before) {
            DataTable Dt = GetAllAsDataTable();
            if (Dt.Columns.Contains(StrColumnName))
                if (Before)
                    return GetAsDataTable("" + StrColumnName + "<=" + GetDateFormat(ObjDate), "");
                else
                    return GetAsDataTable("" + StrColumnName + ">=" + GetDateFormat(ObjDate), "");
            else return null;
        }
        public DataTable Searching(string StrColumnName, DateTime ObjDateStart, DateTime ObjDateEnd) {
            DataTable Dt = GetAllAsDataTable();
            if (Dt.Columns.Contains(StrColumnName))
                return GetAsDataTable("" + StrColumnName + ">=" + GetDateFormat(ObjDateStart) + " AND " + StrColumnName + "<=" + GetDateFormat(ObjDateEnd), "");
            else return null;
        }
        public DataTable Searching(string StrColumnName, bool Status) {
            DataTable Dt = GetAllAsDataTable();
            if (Dt.Columns.Contains(StrColumnName))
                return GetAsDataTable("" + StrColumnName + "=" + Status, "");
            else return null;
        }
        /// <summary>
        /// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
        /// to retrieve all records from the <c>News</c> table.
        /// </summary>
        /// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
        protected virtual IDbCommand CreateGetAllCommand() {
            return CreateGetCommand(null, null);
        }

        /// <summary>
        /// Gets the first <see cref="NewsRow"/> objects that 
        /// match the search condition.
        /// </summary>
        /// <param name="whereSql">The SQL search condition. For example: 
        /// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
        /// <returns>An instance of <see cref="NewsRow"/> or null reference 
        /// (Nothing in Visual Basic) if the object was not found.</returns>
        public NewsRow GetRow(string whereSql) {
            int totalRecordCount = -1;
            NewsRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
            return 0 == rows.Length ? null : rows[0];
        }

        /// <summary>
        /// Gets an array of <see cref="NewsRow"/> objects that 
        /// match the search condition, in the the specified sort order.
        /// </summary>
        /// <param name="whereSql">The SQL search condition. For example: 
        /// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
        /// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
        /// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
        /// <returns>An array of <see cref="NewsRow"/> objects.</returns>
        public NewsRow[] GetAsArray(string whereSql, string orderBySql) {
            int totalRecordCount = -1;
            return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
        }

        /// <summary>
        /// Gets an array of <see cref="NewsRow"/> objects that 
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
        /// <returns>An array of <see cref="NewsRow"/> objects.</returns>
        public virtual NewsRow[] GetAsArray(string whereSql, string orderBySql,
                            int startIndex, int length, ref int totalRecordCount) {
            using (IDataReader reader = _db.ExecuteReader(CreateGetCommand(whereSql, orderBySql))) {
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
        public DataTable GetAsDataTable(string whereSql, string orderBySql) {
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
                            int startIndex, int length, ref int totalRecordCount) {
            using (IDataReader reader = _db.ExecuteReader(CreateGetCommand(whereSql, orderBySql))) {
                return MapRecordsToDataTable(reader, startIndex, length, ref totalRecordCount);
            }
        }

        public virtual NewsRow[] GetTopAsArray(int topNum, string whereSql, string orderBySql) {
            int totalRecordCount = -1;
            using (IDataReader reader = _db.ExecuteReader(CreateGetTopCommand(topNum, whereSql, orderBySql))) {
                return MapRecords(reader, 0, int.MaxValue, ref totalRecordCount);
            }
        }

        public virtual DataTable GetTopAsDataTable(int topNum, string whereSql, string orderBySql) {
            int totalRecordCount = -1;
            using (IDataReader reader = _db.ExecuteReader(CreateGetTopCommand(topNum, whereSql, orderBySql))) {
                return MapRecordsToDataTable(reader, 0, int.MaxValue, ref totalRecordCount);
            }
        }

        public virtual int GetCount(string sqlWhere) {
            string sqlStr = "Select Count(*) From [dbo].[News]" + ((sqlWhere == "") ? "" : (" Where " + sqlWhere));
            IDbCommand cmd = _db.CreateCommand(sqlStr);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public int GetPagesNum(int pageSize, string sqlWhere) {
            if (pageSize > 0) {
                int totalRows = GetCount(sqlWhere);
                int divPages = Convert.ToInt32(totalRows / pageSize);
                if (totalRows % pageSize > 0) {
                    divPages++;
                }
                return divPages;
            } else {
                return 0;
            }
        }

        /// <summary>
        /// Gets Custom Page of NewsRow
        /// </summary>
        /// <param name="pageNumber">Selected Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="whereSql">Where Clause</param>
        /// <param name="orderBySql">Order By Clause</param>
        /// <returns>An Array of NewsRow</returns>
        public NewsRow[] GetPageAsArray(int pageNumber, int pageSize, string whereSql, string orderBySql) {
            int startIndex = (pageNumber - 1) * pageSize;
            int totalRecordCount = -1;
            return GetAsArray(whereSql, orderBySql, startIndex, pageSize, ref totalRecordCount);
        }

        /// <summary>
        /// Gets Custom Page As DataTable
        /// </summary>
        /// <param name="pageNumber">Selected Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="whereSql">Where Clause</param>
        /// <param name="orderBySql">Order By Clause</param>
        /// <returns>A reference of DataTable</returns>
        public DataTable GetPageAsDataTable(int pageNumber, int pageSize, string whereSql, string orderBySql) {
            int startIndex = (pageNumber - 1) * pageSize;
            int totalRecordCount = -1;
            return GetAsDataTable(whereSql, orderBySql, startIndex, pageSize, ref totalRecordCount);
        }

        /// <summary>
        /// Make Dynamic Fields List
        /// </summary>
        /// <param name="fields">Field separate by ','</param>
        /// <returns></returns>
        public string[] MakeListFields(params string[] fields) {
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
        public DataTable GetAsDataTable(string[] listFields, string whereSql, string orderBySql) {
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
            int startIndex, int length, ref int totalRecordCount) {
            using (IDataReader reader = _db.ExecuteReader(CreateGetCommand(listFields, whereSql, orderBySql))) {
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
        protected virtual IDbCommand CreateGetCommand(string[] listFields, string whereSql, string orderBySql) {
            string sql = "SELECT ";
            for (int _fieldCount = 0; _fieldCount < listFields.Length; _fieldCount++) {
                sql += ((_fieldCount > 0) ? ", " : "") + listFields[_fieldCount];
            }
            sql += " FROM [dbo].[News]";
            if (null != whereSql && 0 < whereSql.Length)
                sql += " WHERE " + whereSql;
            if (null != orderBySql && 0 < orderBySql.Length)
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
        protected virtual IDbCommand CreateGetCommand(string whereSql, string orderBySql) {
            string sql = "SELECT * FROM [dbo].[News]";
            if (null != whereSql && 0 < whereSql.Length)
                sql += " WHERE " + whereSql;
            if (null != orderBySql && 0 < orderBySql.Length)
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
        protected virtual IDbCommand CreateGetTopCommand(int topNum, string whereSql, string orderBySql) {
            string sql = "SELECT TOP " + topNum.ToString() + " * FROM [dbo].[News]";
            if (null != whereSql && 0 < whereSql.Length)
                sql += " WHERE " + whereSql;
            if (null != orderBySql && 0 < orderBySql.Length)
                sql += " ORDER BY " + orderBySql;
            return _db.CreateCommand(sql);
        }

        /// <summary>
        /// Gets <see cref="NewsRow"/> by the primary key.
        /// </summary>
        /// <param name="news_ID">The <c>News_ID</c> column value.</param>
        /// <returns>An instance of <see cref="NewsRow"/> or null reference 
        /// (Nothing in Visual Basic) if the object was not found.</returns>
        public virtual NewsRow GetByPrimaryKey(long news_ID) {
            string whereSql = "[News_ID]=" + _db.CreateSqlParameterName("@News_ID");
            IDbCommand cmd = CreateGetCommand(whereSql, null);
            AddParameter(cmd, "@News_ID", news_ID);
            NewsRow[] tempArray = MapRecords(cmd);
            return 0 == tempArray.Length ? null : tempArray[0];
        }



        public virtual bool InsertNews_Extesion(NewsRow value)
        {
            if(!Update_Extension(value))
            {
                string sqlStr = "INSERT INTO [dbo].[News_Extension] (" +
                "[News_ID], " +
                "[Cat_ID], " +
                "[News_Title], " +
                "[News_Subtitle], " +
                "[News_Image], " +
                "[News_ImageNote], " +
                "[News_Source], " +
                "[News_InitialContent], " +
                "[News_Content], " +
                "[News_Author], " +
                "[News_CurrEditor], " +
                "[News_Approver], " +
                "[News_Status], " +
                "[News_SwitchTime], " +
                "[News_PublishDate], " +
                "[News_isFocus], " +
                "[News_Mode], " +
                "[News_ViewNum], " +
                "[News_CreateDate], " +
                "[News_ModifiedDate], " +
                "[News_Relation], " +
                "[News_Rate], " +
                "[News_OtherCat], " +
                "[isComment], " +
                "[isUserRate], " +
                "[Template], " +
                "[WordCount], " +
                "[Icon], " +
                "[Extension1], " +
                "[Extension2], " +
                "[Extension3], " +
                "[Extension4]" +
                ") VALUES (" +
                _db.CreateSqlParameterName("@News_ID") + ", " +
                _db.CreateSqlParameterName("@Cat_ID") + ", " +
                _db.CreateSqlParameterName("@News_Title") + ", " +
                _db.CreateSqlParameterName("@News_Subtitle") + ", " +
                _db.CreateSqlParameterName("@News_Image") + ", " +
                _db.CreateSqlParameterName("@News_ImageNote") + ", " +
                _db.CreateSqlParameterName("@News_Source") + ", " +
                _db.CreateSqlParameterName("@News_InitialContent") + ", " +
                _db.CreateSqlParameterName("@News_Content") + ", " +
                _db.CreateSqlParameterName("@News_Author") + ", " +
                _db.CreateSqlParameterName("@News_CurrEditor") + ", " +
                _db.CreateSqlParameterName("@News_Approver") + ", " +
                _db.CreateSqlParameterName("@News_Status") + ", " +
                _db.CreateSqlParameterName("@News_SwitchTime") + ", " +
                _db.CreateSqlParameterName("@News_PublishDate") + ", " +
                _db.CreateSqlParameterName("@News_isFocus") + ", " +
                _db.CreateSqlParameterName("@News_Mode") + ", " +
                _db.CreateSqlParameterName("@News_ViewNum") + ", " +
                _db.CreateSqlParameterName("@News_CreateDate") + ", " +
                _db.CreateSqlParameterName("@News_ModifiedDate") + ", " +
                _db.CreateSqlParameterName("@News_Relation") + ", " +
                _db.CreateSqlParameterName("@News_Rate") + ", " +
                _db.CreateSqlParameterName("@News_OtherCat") + ", " +
                _db.CreateSqlParameterName("@isComment") + ", " +
                _db.CreateSqlParameterName("@isUserRate") + ", " +
                _db.CreateSqlParameterName("@Template") + ", " +
                _db.CreateSqlParameterName("@WordCount") + ", " +
                _db.CreateSqlParameterName("@Icon") + ", " +
                _db.CreateSqlParameterName("@Extension1") + ", " +
                _db.CreateSqlParameterName("@Extension2") + ", " +
                _db.CreateSqlParameterName("@Extension3") + ", " +
                _db.CreateSqlParameterName("@Extension4") + ");";

                IDbCommand cmd = _db.CreateCommand(sqlStr);
                AddParameter(cmd, "@News_ID", value.News_ID);
                AddParameter(cmd, "@Cat_ID",
                    value.IsCat_IDNull ? DBNull.Value : (object)value.Cat_ID);
                AddParameter(cmd, "@News_Title", value.News_Title);
                AddParameter(cmd, "@News_Subtitle", value.News_Subtitle);
                AddParameter(cmd, "@News_Image", value.News_Image);
                AddParameter(cmd, "@News_ImageNote", value.News_ImageNote);
                AddParameter(cmd, "@News_Source", value.News_Source);
                AddParameter(cmd, "@News_InitialContent", value.News_InitialContent);
                AddParameter(cmd, "@News_Content", value.News_Content);
                AddParameter(cmd, "@News_Author", value.News_Author);
                AddParameter(cmd, "@News_CurrEditor", value.News_CurrEditor);
                AddParameter(cmd, "@News_Approver", value.News_Approver);
                AddParameter(cmd, "@News_Status",
                    value.IsNews_StatusNull ? DBNull.Value : (object)value.News_Status);
                AddParameter(cmd, "@News_SwitchTime",
                    value.IsNews_SwitchTimeNull ? DBNull.Value : (object)value.News_SwitchTime);
                AddParameter(cmd, "@News_PublishDate",
                    value.IsNews_PublishDateNull ? DBNull.Value : (object)value.News_PublishDate);
                AddParameter(cmd, "@News_isFocus",
                    value.IsNews_isFocusNull ? DBNull.Value : (object)value.News_isFocus);
                AddParameter(cmd, "@News_Mode",
                    value.IsNews_ModeNull ? DBNull.Value : (object)value.News_Mode);
                AddParameter(cmd, "@News_ViewNum",
                    value.IsNews_ViewNumNull ? DBNull.Value : (object)value.News_ViewNum);
                AddParameter(cmd, "@News_CreateDate",
                    value.IsNews_CreateDateNull ? DBNull.Value : (object)value.News_CreateDate);
                AddParameter(cmd, "@News_ModifiedDate",
                    value.IsNews_ModifiedDateNull ? DBNull.Value : (object)value.News_ModifiedDate);
                AddParameter(cmd, "@News_Relation", value.News_Relation);
                AddParameter(cmd, "@News_Rate",
                    value.IsNews_RateNull ? DBNull.Value : (object)value.News_Rate);
                AddParameter(cmd, "@News_OtherCat", value.News_OtherCat);
                AddParameter(cmd, "@isComment",
                    value.IsisCommentNull ? DBNull.Value : (object)value.isComment);
                AddParameter(cmd, "@isUserRate",
                    value.IsisUserRateNull ? DBNull.Value : (object)value.isUserRate);
                AddParameter(cmd, "@Template",
                    value.IsTemplateNull ? DBNull.Value : (object)value.Template);
                AddParameter(cmd, "@WordCount", value.WordCount);
                AddParameter(cmd, "@Icon", value.Icon);
                AddParameter(cmd, "@Extension1", value.Extension1);
                AddParameter(cmd, "@Extension2", value.Extension2);
                AddParameter(cmd, "@Extension3", value.Extension3);
                AddParameter(cmd, "@Extension4", value.Extension4);

                try
                {
                    return cmd.ExecuteNonQuery() == 1;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return true;
        }

        /// <summary>
        /// Adds a new record into the <c>News</c> table.
        /// </summary>
        /// <param name="value">The <see cref="NewsRow"/> object to be inserted.</param>
        public virtual bool Insert(NewsRow value) {
            string sqlStr = "INSERT INTO [dbo].[News] (" +
                "[News_ID], " +
                "[Cat_ID], " +
                "[News_Title], " +
                "[News_Subtitle], " +
                "[News_Image], " +
                "[News_ImageNote], " +
                "[News_Source], " +
                "[News_InitialContent], " +
                "[News_Content], " +
                "[News_Author], " +
                "[News_CurrEditor], " +
                "[News_Approver], " +
                "[News_Status], " +
                "[News_SwitchTime], " +
                "[News_PublishDate], " +
                "[News_isFocus], " +
                "[News_Mode], " +
                "[News_ViewNum], " +
                "[News_CreateDate], " +
                "[News_ModifiedDate], " +
                "[News_Relation], " +
                "[News_Rate], " +
                "[News_OtherCat], " +
                "[isComment], " +
                "[isUserRate], " +
                "[Template], " +
                "[WordCount], " +
                "[Icon], " +
                "[Extension1], " +
                "[Extension2], " +
                "[Extension3], " +
                "[Extension4]" +
                ") VALUES (" +
                _db.CreateSqlParameterName("@News_ID") + ", " +
                _db.CreateSqlParameterName("@Cat_ID") + ", " +
                _db.CreateSqlParameterName("@News_Title") + ", " +
                _db.CreateSqlParameterName("@News_Subtitle") + ", " +
                _db.CreateSqlParameterName("@News_Image") + ", " +
                _db.CreateSqlParameterName("@News_ImageNote") + ", " +
                _db.CreateSqlParameterName("@News_Source") + ", " +
                _db.CreateSqlParameterName("@News_InitialContent") + ", " +
                _db.CreateSqlParameterName("@News_Content") + ", " +
                _db.CreateSqlParameterName("@News_Author") + ", " +
                _db.CreateSqlParameterName("@News_CurrEditor") + ", " +
                _db.CreateSqlParameterName("@News_Approver") + ", " +
                _db.CreateSqlParameterName("@News_Status") + ", " +
                _db.CreateSqlParameterName("@News_SwitchTime") + ", " +
                _db.CreateSqlParameterName("@News_PublishDate") + ", " +
                _db.CreateSqlParameterName("@News_isFocus") + ", " +
                _db.CreateSqlParameterName("@News_Mode") + ", " +
                _db.CreateSqlParameterName("@News_ViewNum") + ", " +
                _db.CreateSqlParameterName("@News_CreateDate") + ", " +
                _db.CreateSqlParameterName("@News_ModifiedDate") + ", " +
                _db.CreateSqlParameterName("@News_Relation") + ", " +
                _db.CreateSqlParameterName("@News_Rate") + ", " +
                _db.CreateSqlParameterName("@News_OtherCat") + ", " +
                _db.CreateSqlParameterName("@isComment") + ", " +
                _db.CreateSqlParameterName("@isUserRate") + ", " +
                _db.CreateSqlParameterName("@Template") + ", " +
                _db.CreateSqlParameterName("@WordCount") + ", " +
                _db.CreateSqlParameterName("@Icon") + ", " +
                _db.CreateSqlParameterName("@Extension1") + ", " +
                _db.CreateSqlParameterName("@Extension2") + ", " +
                _db.CreateSqlParameterName("@Extension3") + ", " +
                _db.CreateSqlParameterName("@Extension4") + ");";

            IDbCommand cmd = _db.CreateCommand(sqlStr);
            AddParameter(cmd, "@News_ID", value.News_ID);
            AddParameter(cmd, "@Cat_ID",
                value.IsCat_IDNull ? DBNull.Value : (object)value.Cat_ID);
            AddParameter(cmd, "@News_Title", value.News_Title);
            AddParameter(cmd, "@News_Subtitle", value.News_Subtitle);
            AddParameter(cmd, "@News_Image", value.News_Image);
            AddParameter(cmd, "@News_ImageNote", value.News_ImageNote);
            AddParameter(cmd, "@News_Source", value.News_Source);
            AddParameter(cmd, "@News_InitialContent", value.News_InitialContent);
            AddParameter(cmd, "@News_Content", value.News_Content);
            AddParameter(cmd, "@News_Author", value.News_Author);
            AddParameter(cmd, "@News_CurrEditor", value.News_CurrEditor);
            AddParameter(cmd, "@News_Approver", value.News_Approver);
            AddParameter(cmd, "@News_Status",
                value.IsNews_StatusNull ? DBNull.Value : (object)value.News_Status);
            AddParameter(cmd, "@News_SwitchTime",
                value.IsNews_SwitchTimeNull ? DBNull.Value : (object)value.News_SwitchTime);
            AddParameter(cmd, "@News_PublishDate",
                value.IsNews_PublishDateNull ? DBNull.Value : (object)value.News_PublishDate);
            AddParameter(cmd, "@News_isFocus",
                value.IsNews_isFocusNull ? DBNull.Value : (object)value.News_isFocus);
            AddParameter(cmd, "@News_Mode",
                value.IsNews_ModeNull ? DBNull.Value : (object)value.News_Mode);
            AddParameter(cmd, "@News_ViewNum",
                value.IsNews_ViewNumNull ? DBNull.Value : (object)value.News_ViewNum);
            AddParameter(cmd, "@News_CreateDate",
                value.IsNews_CreateDateNull ? DBNull.Value : (object)value.News_CreateDate);
            AddParameter(cmd, "@News_ModifiedDate",
                value.IsNews_ModifiedDateNull ? DBNull.Value : (object)value.News_ModifiedDate);
            AddParameter(cmd, "@News_Relation", value.News_Relation);
            AddParameter(cmd, "@News_Rate",
                value.IsNews_RateNull ? DBNull.Value : (object)value.News_Rate);
            AddParameter(cmd, "@News_OtherCat", value.News_OtherCat);
            AddParameter(cmd, "@isComment",
                value.IsisCommentNull ? DBNull.Value : (object)value.isComment);
            AddParameter(cmd, "@isUserRate",
                value.IsisUserRateNull ? DBNull.Value : (object)value.isUserRate);
            AddParameter(cmd, "@Template",
                value.IsTemplateNull ? DBNull.Value : (object)value.Template);
            AddParameter(cmd, "@WordCount", value.WordCount);
            AddParameter(cmd, "@Icon", value.Icon);
            AddParameter(cmd, "@Extension1", value.Extension1);
            AddParameter(cmd, "@Extension2", value.Extension2);
            AddParameter(cmd, "@Extension3", value.Extension3);
            AddParameter(cmd, "@Extension4", value.Extension4);

            try {
                return cmd.ExecuteNonQuery() == 1;
            } catch (Exception ex) {
                throw ex;
            }
        }



        public virtual bool Update_Extension(NewsRow value)
        {
            string sqlStr = "UPDATE [dbo].[News_Extension] SET " +
                "[Cat_ID]=" + _db.CreateSqlParameterName("@Cat_ID") + ", " +
                "[News_Title]=" + _db.CreateSqlParameterName("@News_Title") + ", " +
                "[News_Subtitle]=" + _db.CreateSqlParameterName("@News_Subtitle") + ", " +
                "[News_Image]=" + _db.CreateSqlParameterName("@News_Image") + ", " +
                "[News_ImageNote]=" + _db.CreateSqlParameterName("@News_ImageNote") + ", " +
                "[News_Source]=" + _db.CreateSqlParameterName("@News_Source") + ", " +
                "[News_InitialContent]=" + _db.CreateSqlParameterName("@News_InitialContent") + ", " +
                "[News_Content]=" + _db.CreateSqlParameterName("@News_Content") + ", " +
                "[News_Author]=" + _db.CreateSqlParameterName("@News_Author") + ", " +
                "[News_CurrEditor]=" + _db.CreateSqlParameterName("@News_CurrEditor") + ", " +
                "[News_Approver]=" + _db.CreateSqlParameterName("@News_Approver") + ", " +
                "[News_Status]=" + _db.CreateSqlParameterName("@News_Status") + ", " +
                "[News_SwitchTime]=" + _db.CreateSqlParameterName("@News_SwitchTime") + ", " +
                "[News_PublishDate]=" + _db.CreateSqlParameterName("@News_PublishDate") + ", " +
                "[News_isFocus]=" + _db.CreateSqlParameterName("@News_isFocus") + ", " +
                "[News_Mode]=" + _db.CreateSqlParameterName("@News_Mode") + ", " +
                "[News_ViewNum]=" + _db.CreateSqlParameterName("@News_ViewNum") + ", " +
                "[News_CreateDate]=" + _db.CreateSqlParameterName("@News_CreateDate") + ", " +
                "[News_ModifiedDate]=" + _db.CreateSqlParameterName("@News_ModifiedDate") + ", " +
                "[News_Relation]=" + _db.CreateSqlParameterName("@News_Relation") + ", " +
                "[News_Rate]=" + _db.CreateSqlParameterName("@News_Rate") + ", " +
                "[News_OtherCat]=" + _db.CreateSqlParameterName("@News_OtherCat") + ", " +
                "[isComment]=" + _db.CreateSqlParameterName("@isComment") + ", " +
                "[isUserRate]=" + _db.CreateSqlParameterName("@isUserRate") + ", " +
                "[Template]=" + _db.CreateSqlParameterName("@Template") + ", " +
                "[Icon]=" + _db.CreateSqlParameterName("@Icon") + ", " +
                "[WordCount]=" + _db.CreateSqlParameterName("@WordCount") + ", " +
                "[Extension1]=" + _db.CreateSqlParameterName("@Extension1") + ", " +
                "[Extension2]=" + _db.CreateSqlParameterName("@Extension2") + ", " +
                "[Extension3]=" + _db.CreateSqlParameterName("@Extension3") + ", " +
                "[Extension4]=" + _db.CreateSqlParameterName("@Extension4") +

                " WHERE " +
                "[News_ID]=" + _db.CreateSqlParameterName("@News_ID");
            IDbCommand cmd = _db.CreateCommand(sqlStr);
            AddParameter(cmd, "@Cat_ID",
                value.IsCat_IDNull ? DBNull.Value : (object)value.Cat_ID);
            AddParameter(cmd, "@News_Title", value.News_Title);
            AddParameter(cmd, "@News_Subtitle", value.News_Subtitle);
            AddParameter(cmd, "@News_Image", value.News_Image);
            AddParameter(cmd, "@News_ImageNote", value.News_ImageNote);
            AddParameter(cmd, "@News_Source", value.News_Source);
            AddParameter(cmd, "@News_InitialContent", value.News_InitialContent);
            AddParameter(cmd, "@News_Content", value.News_Content);
            AddParameter(cmd, "@News_Author", value.News_Author);
            AddParameter(cmd, "@News_CurrEditor", value.News_CurrEditor);
            AddParameter(cmd, "@News_Approver", value.News_Approver);
            AddParameter(cmd, "@News_Status",
                value.IsNews_StatusNull ? DBNull.Value : (object)value.News_Status);
            AddParameter(cmd, "@News_SwitchTime",
                value.IsNews_SwitchTimeNull ? DBNull.Value : (object)value.News_SwitchTime);
            AddParameter(cmd, "@News_PublishDate",
                value.IsNews_PublishDateNull ? DBNull.Value : (object)value.News_PublishDate);
            AddParameter(cmd, "@News_isFocus",
                value.IsNews_isFocusNull ? DBNull.Value : (object)value.News_isFocus);
            AddParameter(cmd, "@News_Mode",
                value.IsNews_ModeNull ? DBNull.Value : (object)value.News_Mode);
            AddParameter(cmd, "@News_ViewNum",
                value.IsNews_ViewNumNull ? DBNull.Value : (object)value.News_ViewNum);
            AddParameter(cmd, "@News_CreateDate",
                value.IsNews_CreateDateNull ? DBNull.Value : (object)value.News_CreateDate);
            AddParameter(cmd, "@News_ModifiedDate",
                value.IsNews_ModifiedDateNull ? DBNull.Value : (object)value.News_ModifiedDate);
            AddParameter(cmd, "@News_Relation", value.News_Relation);
            AddParameter(cmd, "@News_Rate",
                value.IsNews_RateNull ? DBNull.Value : (object)value.News_Rate);
            AddParameter(cmd, "@News_OtherCat", value.News_OtherCat);
            AddParameter(cmd, "@isComment",
                value.IsisCommentNull ? DBNull.Value : (object)value.isComment);
            AddParameter(cmd, "@isUserRate",
                value.IsisUserRateNull ? DBNull.Value : (object)value.isUserRate);
            AddParameter(cmd, "@Template",
                value.IsTemplateNull ? DBNull.Value : (object)value.Template);
            AddParameter(cmd, "@Icon", value.Icon);
            AddParameter(cmd, "@WordCount", value.WordCount);
            AddParameter(cmd, "@News_ID", value.News_ID);

            AddParameter(cmd, "@Extension1", value.Extension1);
            AddParameter(cmd, "@Extension2", value.Extension2);
            AddParameter(cmd, "@Extension3", value.Extension3);
            AddParameter(cmd, "@Extension4", value.Extension4);

            return 0 != cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Updates a record in the <c>News</c> table.
        /// </summary>
        /// <param name="value">The <see cref="NewsRow"/>
        /// object used to update the table record.</param>
        /// <returns>true if the record was updated; otherwise, false.</returns>
        public virtual bool Update(NewsRow value) {
            string sqlStr = "UPDATE [dbo].[News] SET " +
                "[Cat_ID]=" + _db.CreateSqlParameterName("@Cat_ID") + ", " +
                "[News_Title]=" + _db.CreateSqlParameterName("@News_Title") + ", " +
                "[News_Subtitle]=" + _db.CreateSqlParameterName("@News_Subtitle") + ", " +
                "[News_Image]=" + _db.CreateSqlParameterName("@News_Image") + ", " +
                "[News_ImageNote]=" + _db.CreateSqlParameterName("@News_ImageNote") + ", " +
                "[News_Source]=" + _db.CreateSqlParameterName("@News_Source") + ", " +
                "[News_InitialContent]=" + _db.CreateSqlParameterName("@News_InitialContent") + ", " +
                "[News_Content]=" + _db.CreateSqlParameterName("@News_Content") + ", " +
                "[News_Author]=" + _db.CreateSqlParameterName("@News_Author") + ", " +
                "[News_CurrEditor]=" + _db.CreateSqlParameterName("@News_CurrEditor") + ", " +
                "[News_Approver]=" + _db.CreateSqlParameterName("@News_Approver") + ", " +
                "[News_Status]=" + _db.CreateSqlParameterName("@News_Status") + ", " +
                "[News_SwitchTime]=" + _db.CreateSqlParameterName("@News_SwitchTime") + ", " +
                "[News_PublishDate]=" + _db.CreateSqlParameterName("@News_PublishDate") + ", " +
                "[News_isFocus]=" + _db.CreateSqlParameterName("@News_isFocus") + ", " +
                "[News_Mode]=" + _db.CreateSqlParameterName("@News_Mode") + ", " +
                "[News_ViewNum]=" + _db.CreateSqlParameterName("@News_ViewNum") + ", " +
                "[News_CreateDate]=" + _db.CreateSqlParameterName("@News_CreateDate") + ", " +
                "[News_ModifiedDate]=" + _db.CreateSqlParameterName("@News_ModifiedDate") + ", " +
                "[News_Relation]=" + _db.CreateSqlParameterName("@News_Relation") + ", " +
                "[News_Rate]=" + _db.CreateSqlParameterName("@News_Rate") + ", " +
                "[News_OtherCat]=" + _db.CreateSqlParameterName("@News_OtherCat") + ", " +
                "[isComment]=" + _db.CreateSqlParameterName("@isComment") + ", " +
                "[isUserRate]=" + _db.CreateSqlParameterName("@isUserRate") + ", " +
                "[Template]=" + _db.CreateSqlParameterName("@Template") + ", " +
                "[Icon]=" + _db.CreateSqlParameterName("@Icon") + ", " +
                "[WordCount]=" + _db.CreateSqlParameterName("@WordCount") + ", " +
                "[Extension1]=" + _db.CreateSqlParameterName("@Extension1") + ", " +
                "[Extension2]=" + _db.CreateSqlParameterName("@Extension2") + ", " +
                "[Extension3]=" + _db.CreateSqlParameterName("@Extension3") + ", " +
                "[Extension4]=" + _db.CreateSqlParameterName("@Extension4") +

                " WHERE " +
                "[News_ID]=" + _db.CreateSqlParameterName("@News_ID");
            IDbCommand cmd = _db.CreateCommand(sqlStr);
            AddParameter(cmd, "@Cat_ID",
                value.IsCat_IDNull ? DBNull.Value : (object)value.Cat_ID);
            AddParameter(cmd, "@News_Title", value.News_Title);
            AddParameter(cmd, "@News_Subtitle", value.News_Subtitle);
            AddParameter(cmd, "@News_Image", value.News_Image);
            AddParameter(cmd, "@News_ImageNote", value.News_ImageNote);
            AddParameter(cmd, "@News_Source", value.News_Source);
            AddParameter(cmd, "@News_InitialContent", value.News_InitialContent);
            AddParameter(cmd, "@News_Content", value.News_Content);
            AddParameter(cmd, "@News_Author", value.News_Author);
            AddParameter(cmd, "@News_CurrEditor", value.News_CurrEditor);
            AddParameter(cmd, "@News_Approver", value.News_Approver);
            AddParameter(cmd, "@News_Status",
                value.IsNews_StatusNull ? DBNull.Value : (object)value.News_Status);
            AddParameter(cmd, "@News_SwitchTime",
                value.IsNews_SwitchTimeNull ? DBNull.Value : (object)value.News_SwitchTime);
            AddParameter(cmd, "@News_PublishDate",
                value.IsNews_PublishDateNull ? DBNull.Value : (object)value.News_PublishDate);
            AddParameter(cmd, "@News_isFocus",
                value.IsNews_isFocusNull ? DBNull.Value : (object)value.News_isFocus);
            AddParameter(cmd, "@News_Mode",
                value.IsNews_ModeNull ? DBNull.Value : (object)value.News_Mode);
            AddParameter(cmd, "@News_ViewNum",
                value.IsNews_ViewNumNull ? DBNull.Value : (object)value.News_ViewNum);
            AddParameter(cmd, "@News_CreateDate",
                value.IsNews_CreateDateNull ? DBNull.Value : (object)value.News_CreateDate);
            AddParameter(cmd, "@News_ModifiedDate",
                value.IsNews_ModifiedDateNull ? DBNull.Value : (object)value.News_ModifiedDate);
            AddParameter(cmd, "@News_Relation", value.News_Relation);
            AddParameter(cmd, "@News_Rate",
                value.IsNews_RateNull ? DBNull.Value : (object)value.News_Rate);
            AddParameter(cmd, "@News_OtherCat", value.News_OtherCat);
            AddParameter(cmd, "@isComment",
                value.IsisCommentNull ? DBNull.Value : (object)value.isComment);
            AddParameter(cmd, "@isUserRate",
                value.IsisUserRateNull ? DBNull.Value : (object)value.isUserRate);
            AddParameter(cmd, "@Template",
                value.IsTemplateNull ? DBNull.Value : (object)value.Template);
            AddParameter(cmd, "@Icon", value.Icon);
            AddParameter(cmd, "@WordCount", value.WordCount);
            AddParameter(cmd, "@News_ID", value.News_ID);

            AddParameter(cmd, "@Extension1", value.Extension1);
            AddParameter(cmd, "@Extension2", value.Extension2);
            AddParameter(cmd, "@Extension3", value.Extension3);
            AddParameter(cmd, "@Extension4", value.Extension4);

            return 0 != cmd.ExecuteNonQuery();
        }

        public void Update(DataTable table) {
            Update(table, true);
        }

        public virtual void Update(DataTable table, bool acceptChanges) {
            DataRowCollection rows = table.Rows;
            for (int i = rows.Count - 1; i >= 0; i--) {
                DataRow row = rows[i];
                switch (row.RowState) {
                    case DataRowState.Added:
                        Insert(MapRow(row));
                        if (acceptChanges)
                            row.AcceptChanges();
                        break;

                    case DataRowState.Deleted:
                        // Temporary reject changes to be able to access to the PK column(s)
                        row.RejectChanges();
                        try {
                            DeleteByPrimaryKey((long)row["News_ID"]);
                        } finally {
                            row.Delete();
                        }
                        if (acceptChanges)
                            row.AcceptChanges();
                        break;

                    case DataRowState.Modified:
                        Update(MapRow(row));
                        if (acceptChanges)
                            row.AcceptChanges();
                        break;
                }
            }
        }

        public bool Delete(NewsRow value) {
            return DeleteByPrimaryKey(value.News_ID);
        }

        public virtual bool DeleteByPrimaryKey(long news_ID) {
            string whereSql = "[News_ID]=" + _db.CreateSqlParameterName("@News_ID");
            IDbCommand cmd = CreateDeleteCommand(whereSql);
            AddParameter(cmd, "@News_ID", news_ID);
            return 0 < cmd.ExecuteNonQuery();
        }

        public int Delete(string whereSql) {
            return CreateDeleteCommand(whereSql).ExecuteNonQuery();
        }

        protected virtual IDbCommand CreateDeleteCommand(string whereSql) {
            string sql = "DELETE FROM [dbo].[News]";
            if (null != whereSql && 0 < whereSql.Length)
                sql += " WHERE " + whereSql;
            return _db.CreateCommand(sql);
        }

        public int DeleteAll() {
            return Delete("");
        }

        protected NewsRow[] MapRecords(IDbCommand command) {
            using (IDataReader reader = _db.ExecuteReader(command)) {
                return MapRecords(reader);
            }
        }


        protected NewsRow[] MapRecords(IDataReader reader) {
            int totalRecordCount = -1;
            return MapRecords(reader, 0, int.MaxValue, ref totalRecordCount);
        }

        protected virtual NewsRow[] MapRecords(IDataReader reader,
                                        int startIndex, int length, ref int totalRecordCount) {
            if (0 > startIndex)
                throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
            if (0 > length)
                throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

            int news_IDColumnIndex = reader.GetOrdinal("News_ID");
            int cat_IDColumnIndex = reader.GetOrdinal("Cat_ID");
            int news_TitleColumnIndex = reader.GetOrdinal("News_Title");
            int news_SubtitleColumnIndex = reader.GetOrdinal("News_Subtitle");
            int news_ImageColumnIndex = reader.GetOrdinal("News_Image");
            int news_ImageNoteColumnIndex = reader.GetOrdinal("News_ImageNote");
            int news_SourceColumnIndex = reader.GetOrdinal("News_Source");
            int news_InitialContentColumnIndex = reader.GetOrdinal("News_InitialContent");
            int news_ContentColumnIndex = reader.GetOrdinal("News_Content");
            int news_AuthorColumnIndex = reader.GetOrdinal("News_Author");
            int news_CurrEditorColumnIndex = reader.GetOrdinal("News_CurrEditor");
            int news_ApproverColumnIndex = reader.GetOrdinal("News_Approver");
            int news_StatusColumnIndex = reader.GetOrdinal("News_Status");
            int news_SwitchTimeColumnIndex = reader.GetOrdinal("News_SwitchTime");
            int news_PublishDateColumnIndex = reader.GetOrdinal("News_PublishDate");
            int news_isFocusColumnIndex = reader.GetOrdinal("News_isFocus");
            int news_ModeColumnIndex = reader.GetOrdinal("News_Mode");
            int news_ViewNumColumnIndex = reader.GetOrdinal("News_ViewNum");
            int news_CreateDateColumnIndex = reader.GetOrdinal("News_CreateDate");
            int news_ModifiedDateColumnIndex = reader.GetOrdinal("News_ModifiedDate");
            int news_RelationColumnIndex = reader.GetOrdinal("News_Relation");
            int news_RateColumnIndex = reader.GetOrdinal("News_Rate");
            int news_OtherCatColumnIndex = reader.GetOrdinal("News_OtherCat");
            int isCommentColumnIndex = reader.GetOrdinal("isComment");
            int isUserRateColumnIndex = reader.GetOrdinal("isUserRate");
            int templateColumnIndex = reader.GetOrdinal("Template");
            int iconColumnIndex = reader.GetOrdinal("Icon");
            int wordCountColumnIndex = reader.GetOrdinal("WordCount");

            int extension1ColumnIndex = reader.GetOrdinal("Extension1");
            int extension2ColumnIndex = reader.GetOrdinal("Extension2");
            int extension3ColumnIndex = reader.GetOrdinal("Extension3");
            int extension4ColumnIndex = reader.GetOrdinal("Extension4");


            System.Collections.ArrayList recordList = new System.Collections.ArrayList();
            int ri = -startIndex;
            while (reader.Read()) {
                ri++;
                if (ri > 0 && ri <= length) {
                    NewsRow record = new NewsRow();
                    recordList.Add(record);

                    record.News_ID = Convert.ToInt64(reader.GetValue(news_IDColumnIndex));
                    if (!reader.IsDBNull(cat_IDColumnIndex))
                        record.Cat_ID = Convert.ToInt32(reader.GetValue(cat_IDColumnIndex));
                    if (!reader.IsDBNull(news_TitleColumnIndex))
                        record.News_Title = Convert.ToString(reader.GetValue(news_TitleColumnIndex));
                    if (!reader.IsDBNull(news_SubtitleColumnIndex))
                        record.News_Subtitle = Convert.ToString(reader.GetValue(news_SubtitleColumnIndex));
                    if (!reader.IsDBNull(news_ImageColumnIndex))
                        record.News_Image = Convert.ToString(reader.GetValue(news_ImageColumnIndex));
                    if (!reader.IsDBNull(news_ImageNoteColumnIndex))
                        record.News_ImageNote = Convert.ToString(reader.GetValue(news_ImageNoteColumnIndex));
                    if (!reader.IsDBNull(news_SourceColumnIndex))
                        record.News_Source = Convert.ToString(reader.GetValue(news_SourceColumnIndex));
                    if (!reader.IsDBNull(news_InitialContentColumnIndex))
                        record.News_InitialContent = Convert.ToString(reader.GetValue(news_InitialContentColumnIndex));
                    if (!reader.IsDBNull(news_ContentColumnIndex))
                        record.News_Content = Convert.ToString(reader.GetValue(news_ContentColumnIndex));
                    if (!reader.IsDBNull(news_AuthorColumnIndex))
                        record.News_Author = Convert.ToString(reader.GetValue(news_AuthorColumnIndex));
                    if (!reader.IsDBNull(news_CurrEditorColumnIndex))
                        record.News_CurrEditor = Convert.ToString(reader.GetValue(news_CurrEditorColumnIndex));
                    if (!reader.IsDBNull(news_ApproverColumnIndex))
                        record.News_Approver = Convert.ToString(reader.GetValue(news_ApproverColumnIndex));
                    if (!reader.IsDBNull(news_StatusColumnIndex))
                        record.News_Status = Convert.ToInt32(reader.GetValue(news_StatusColumnIndex));
                    if (!reader.IsDBNull(news_SwitchTimeColumnIndex))
                        record.News_SwitchTime = Convert.ToDateTime(reader.GetValue(news_SwitchTimeColumnIndex));
                    if (!reader.IsDBNull(news_PublishDateColumnIndex))
                        record.News_PublishDate = Convert.ToDateTime(reader.GetValue(news_PublishDateColumnIndex));
                    if (!reader.IsDBNull(news_isFocusColumnIndex))
                        record.News_isFocus = Convert.ToBoolean(reader.GetValue(news_isFocusColumnIndex));
                    if (!reader.IsDBNull(news_ModeColumnIndex))
                        record.News_Mode = Convert.ToInt32(reader.GetValue(news_ModeColumnIndex));
                    if (!reader.IsDBNull(news_ViewNumColumnIndex))
                        record.News_ViewNum = Convert.ToInt32(reader.GetValue(news_ViewNumColumnIndex));
                    if (!reader.IsDBNull(news_CreateDateColumnIndex))
                        record.News_CreateDate = Convert.ToDateTime(reader.GetValue(news_CreateDateColumnIndex));
                    if (!reader.IsDBNull(news_ModifiedDateColumnIndex))
                        record.News_ModifiedDate = Convert.ToDateTime(reader.GetValue(news_ModifiedDateColumnIndex));
                    if (!reader.IsDBNull(news_RelationColumnIndex))
                        record.News_Relation = Convert.ToString(reader.GetValue(news_RelationColumnIndex));
                    if (!reader.IsDBNull(news_RateColumnIndex))
                        record.News_Rate = Convert.ToDecimal(reader.GetValue(news_RateColumnIndex));
                    if (!reader.IsDBNull(news_OtherCatColumnIndex))
                        record.News_OtherCat = Convert.ToString(reader.GetValue(news_OtherCatColumnIndex));
                    if (!reader.IsDBNull(isCommentColumnIndex))
                        record.isComment = Convert.ToBoolean(reader.GetValue(isCommentColumnIndex));
                    if (!reader.IsDBNull(isUserRateColumnIndex))
                        record.isUserRate = Convert.ToBoolean(reader.GetValue(isUserRateColumnIndex));
                    if (!reader.IsDBNull(templateColumnIndex))
                        record.Template = Convert.ToInt32(reader.GetValue(templateColumnIndex));
                    if (!reader.IsDBNull(iconColumnIndex))
                        record.Icon = Convert.ToString(reader.GetValue(iconColumnIndex));
                    if (!reader.IsDBNull(wordCountColumnIndex))
                        record.WordCount = Convert.ToInt32(reader.GetValue(wordCountColumnIndex));

                    if (!reader.IsDBNull(extension1ColumnIndex))
                        record.Extension1 = Convert.ToString(reader.GetValue(extension1ColumnIndex));
                    if (!reader.IsDBNull(extension2ColumnIndex))
                        record.Extension2 = Convert.ToString(reader.GetValue(extension2ColumnIndex));
                    if (!reader.IsDBNull(extension3ColumnIndex))
                        record.Extension3 = Convert.ToString(reader.GetValue(extension3ColumnIndex));
                    if (!reader.IsDBNull(extension4ColumnIndex))
                        record.Extension4 = Convert.ToInt32(reader.GetValue(extension4ColumnIndex));


                    if (ri == length && 0 != totalRecordCount)
                        break;
                }
            }

            totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
            return (NewsRow[])(recordList.ToArray(typeof(NewsRow)));
        }

        /// <summary>
        /// Reads data using the specified command and returns 
        /// a filled <see cref="System.Data.DataTable"/> object.
        /// </summary>
        /// <param name="command">The <see cref="System.Data.IDbCommand"/> object.</param>
        /// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
        protected DataTable MapRecordsToDataTable(IDbCommand command) {
            using (IDataReader reader = _db.ExecuteReader(command)) {
                return MapRecordsToDataTable(reader);
            }
        }

        protected DataTable MapRecordsToDataTable(IDataReader reader) {
            int totalRecordCount = 0;
            return MapRecordsToDataTable(reader, 0, int.MaxValue, ref totalRecordCount);
        }


        protected virtual DataTable MapRecordsToDataTable(IDataReader reader,
                                        int startIndex, int length, ref int totalRecordCount) {
            if (0 > startIndex)
                throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
            if (0 > length)
                throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

            int columnCount = reader.FieldCount;
            int ri = -startIndex;

            DataTable dataTable = CreateDataTable();
            dataTable.BeginLoadData();
            object[] values = new object[columnCount];

            while (reader.Read()) {
                ri++;
                if (ri > 0 && ri <= length) {
                    reader.GetValues(values);
                    dataTable.LoadDataRow(values, true);

                    if (ri == length && 0 != totalRecordCount)
                        break;
                }
            }
            dataTable.EndLoadData();

            totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
            return dataTable;
        }


        protected virtual DataTable MapRecordsToDataTable(string[] listFields, IDataReader reader,
                                        int startIndex, int length, ref int totalRecordCount) {
            if (0 > startIndex)
                throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
            if (0 > length)
                throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

            int columnCount = reader.FieldCount;
            int ri = -startIndex;

            DataTable dataTable = CreateDataTable(listFields);
            dataTable.BeginLoadData();
            object[] values = new object[columnCount];

            while (reader.Read()) {
                ri++;
                if (ri > 0 && ri <= length) {
                    reader.GetValues(values);
                    dataTable.LoadDataRow(values, true);

                    if (ri == length && 0 != totalRecordCount)
                        break;
                }
            }
            dataTable.EndLoadData();

            totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
            return dataTable;
        }

        protected virtual NewsRow MapRow(DataRow row) {
            NewsRow mappedObject = new NewsRow();
            DataTable dataTable = row.Table;
            DataColumn dataColumn;
            // Column "News_ID"
            dataColumn = dataTable.Columns["News_ID"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_ID = (long)row[dataColumn];
            // Column "Cat_ID"
            dataColumn = dataTable.Columns["Cat_ID"];
            if (!row.IsNull(dataColumn))
                mappedObject.Cat_ID = (int)row[dataColumn];
            // Column "News_Title"
            dataColumn = dataTable.Columns["News_Title"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_Title = (string)row[dataColumn];
            // Column "News_Subtitle"
            dataColumn = dataTable.Columns["News_Subtitle"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_Subtitle = (string)row[dataColumn];
            // Column "News_Image"
            dataColumn = dataTable.Columns["News_Image"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_Image = (string)row[dataColumn];
            // Column "News_ImageNote"
            dataColumn = dataTable.Columns["News_ImageNote"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_ImageNote = (string)row[dataColumn];
            // Column "News_Source"
            dataColumn = dataTable.Columns["News_Source"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_Source = (string)row[dataColumn];
            // Column "News_InitialContent"
            dataColumn = dataTable.Columns["News_InitialContent"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_InitialContent = (string)row[dataColumn];
            // Column "News_Content"
            dataColumn = dataTable.Columns["News_Content"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_Content = (string)row[dataColumn];
            // Column "News_Author"
            dataColumn = dataTable.Columns["News_Author"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_Author = (string)row[dataColumn];
            // Column "News_CurrEditor"
            dataColumn = dataTable.Columns["News_CurrEditor"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_CurrEditor = (string)row[dataColumn];
            // Column "News_Approver"
            dataColumn = dataTable.Columns["News_Approver"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_Approver = (string)row[dataColumn];
            // Column "News_Status"
            dataColumn = dataTable.Columns["News_Status"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_Status = (int)row[dataColumn];
            // Column "News_SwitchTime"
            dataColumn = dataTable.Columns["News_SwitchTime"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_SwitchTime = (DateTime)row[dataColumn];
            // Column "News_PublishDate"
            dataColumn = dataTable.Columns["News_PublishDate"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_PublishDate = (DateTime)row[dataColumn];
            // Column "News_isFocus"
            dataColumn = dataTable.Columns["News_isFocus"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_isFocus = (bool)row[dataColumn];
            // Column "News_Mode"
            dataColumn = dataTable.Columns["News_Mode"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_Mode = (int)row[dataColumn];
            // Column "News_ViewNum"
            dataColumn = dataTable.Columns["News_ViewNum"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_ViewNum = (int)row[dataColumn];
            // Column "News_CreateDate"
            dataColumn = dataTable.Columns["News_CreateDate"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_CreateDate = (DateTime)row[dataColumn];
            // Column "News_ModifiedDate"
            dataColumn = dataTable.Columns["News_ModifiedDate"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_ModifiedDate = (DateTime)row[dataColumn];
            // Column "News_Relation"
            dataColumn = dataTable.Columns["News_Relation"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_Relation = (string)row[dataColumn];
            // Column "News_Rate"
            dataColumn = dataTable.Columns["News_Rate"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_Rate = (decimal)row[dataColumn];
            // Column "News_OtherCat"
            dataColumn = dataTable.Columns["News_OtherCat"];
            if (!row.IsNull(dataColumn))
                mappedObject.News_OtherCat = (string)row[dataColumn];
            // Column "isComment"
            dataColumn = dataTable.Columns["isComment"];
            if (!row.IsNull(dataColumn))
                mappedObject.isComment = (bool)row[dataColumn];
            // Column "isUserRate"
            dataColumn = dataTable.Columns["isUserRate"];
            if (!row.IsNull(dataColumn))
                mappedObject.isUserRate = (bool)row[dataColumn];
            // Column "Template"
            dataColumn = dataTable.Columns["Template"];
            if (!row.IsNull(dataColumn))
                mappedObject.Template = (int)row[dataColumn];
            // Column "Icon"
            dataColumn = dataTable.Columns["Icon"];
            if (!row.IsNull(dataColumn))
                mappedObject.Icon = (string)row[dataColumn];

            dataColumn = dataTable.Columns["Extension1"];
            if (!row.IsNull(dataColumn))
                mappedObject.Extension1 = (string)row[dataColumn];
            dataColumn = dataTable.Columns["Extension2"];
            if (!row.IsNull(dataColumn))
                mappedObject.Extension2 = (string)row[dataColumn];
            dataColumn = dataTable.Columns["Extension3"];
            if (!row.IsNull(dataColumn))
                mappedObject.Extension3 = (string)row[dataColumn];
            dataColumn = dataTable.Columns["Extension4"];
            if (!row.IsNull(dataColumn))
                mappedObject.Extension4 = (int)row[dataColumn];

            return mappedObject;
        }

        /// <summary>
        /// Creates a <see cref="System.Data.DataTable"/> object for 
        /// the <c>News</c> table.
        /// </summary>
        /// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
        protected virtual DataTable CreateDataTable(string[] listFields) {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "News";
            foreach (string column in listFields) {
                dataTable.Columns.Add(column, typeof(string));
            }
            return dataTable;
        }

        /// <summary>
        /// Creates a <see cref="System.Data.DataTable"/> object for 
        /// the <c>News</c> table.
        /// </summary>
        /// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
        protected virtual DataTable CreateDataTable() {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "News";
            DataColumn dataColumn;
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
            dataColumn = dataTable.Columns.Add("News_InitialContent", typeof(string));
            dataColumn = dataTable.Columns.Add("News_Content", typeof(string));
            dataColumn = dataTable.Columns.Add("News_Author", typeof(string));
            dataColumn.MaxLength = 200;
            dataColumn = dataTable.Columns.Add("News_CurrEditor", typeof(string));
            dataColumn.MaxLength = 200;
            dataColumn = dataTable.Columns.Add("News_Approver", typeof(string));
            dataColumn.MaxLength = 200;
            dataColumn = dataTable.Columns.Add("News_Status", typeof(int));
            dataColumn = dataTable.Columns.Add("News_SwitchTime", typeof(DateTime));
            dataColumn = dataTable.Columns.Add("News_PublishDate", typeof(DateTime));
            dataColumn = dataTable.Columns.Add("News_isFocus", typeof(bool));
            dataColumn = dataTable.Columns.Add("News_Mode", typeof(int));
            dataColumn = dataTable.Columns.Add("News_ViewNum", typeof(int));
            dataColumn = dataTable.Columns.Add("News_CreateDate", typeof(DateTime));
            dataColumn = dataTable.Columns.Add("News_ModifiedDate", typeof(DateTime));
            dataColumn = dataTable.Columns.Add("News_Relation", typeof(string));
            dataColumn.MaxLength = 1000;
            dataColumn = dataTable.Columns.Add("News_Rate", typeof(decimal));
            dataColumn = dataTable.Columns.Add("News_OtherCat", typeof(string));
            dataColumn.MaxLength = 50;
            dataColumn = dataTable.Columns.Add("isComment", typeof(bool));
            dataColumn = dataTable.Columns.Add("isUserRate", typeof(bool));
            dataColumn = dataTable.Columns.Add("Template", typeof(int));
            dataColumn = dataTable.Columns.Add("Icon", typeof(string));
            dataColumn = dataTable.Columns.Add("Extension1", typeof(string));
            dataColumn = dataTable.Columns.Add("Extension2", typeof(string));
            dataColumn = dataTable.Columns.Add("Extension3", typeof(string));
            dataColumn = dataTable.Columns.Add("Extension4", typeof(int));
            dataColumn.MaxLength = 200;
            return dataTable;
        }

        protected virtual IDbDataParameter AddParameter(IDbCommand cmd, string paramName, object value) {
            IDbDataParameter parameter;
            switch (paramName) {
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

                case "@News_InitialContent":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                case "@News_Content":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                case "@News_Author":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                case "@News_CurrEditor":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                case "@News_Approver":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                case "@News_Status":
                    parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
                    break;

                case "@News_SwitchTime":
                    parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
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

                case "@News_ViewNum":
                    parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
                    break;

                case "@News_CreateDate":
                    parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
                    break;

                case "@News_ModifiedDate":
                    parameter = _db.AddParameter(cmd, paramName, DbType.DateTime, value);
                    break;

                case "@News_Relation":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                case "@News_Rate":
                    parameter = _db.AddParameter(cmd, paramName, DbType.Decimal, value);
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

                case "@WordCount":
                    parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
                    break;

                case "@Extension1":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;
                case "@Extension2":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;
                case "@Extension3":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;
                case "@Extension4":
                    parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
                    break;

                default:
                    throw new ArgumentException("Unknown parameter name (" + paramName + ").");
            }
            return parameter;
        }
    } // End of NewsCollection_Base class
}  // End of namespace

