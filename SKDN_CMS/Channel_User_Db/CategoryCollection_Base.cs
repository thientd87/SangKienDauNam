


using System;
using System.Data;
using System.Collections;

namespace DFISYS.User.Db {
    /// <summary>
    /// The base class for <see cref="CategoryCollection"/>. Provides methods 
    /// for common database table operations. 
    /// </summary>
    /// <remarks>
    /// Do not change this source code. Update the <see cref="CategoryCollection"/>
    /// class if you need to add or change some functionality.
    /// </remarks>
    public abstract class CategoryCollection_Base
    {
        // Constants
        public const string Cat_IDColumnName = "Cat_ID";
        public const string Cat_NameColumnName = "Cat_Name";
        public const string Cat_DescriptionColumnName = "Cat_Description";
        public const string Cat_DisplayURLColumnName = "Cat_DisplayURL";
        public const string Cat_ParentIDColumnName = "Cat_ParentID";
        public const string Cat_isColumnColumnName = "Cat_isColumn";
        public const string Cat_isHiddenColumnName = "Cat_isHidden";
        public const string Cat_ViewNumColumnName = "Cat_ViewNum";
        public const string EditionType_IDColumnName = "EditionType_ID";
        public const string Cat_IconColumnName = "Cat_Icon";
        public const string Cat_OrderColumnName = "Cat_Order";
        public const string Channel_IDColumnName = "Channel_ID";
        public const string Cat_KeyWordsColumnName = "Cat_KeyWords";
        public const string Cat_Name_EnColumnName = "Cat_Name_En";
        public const string Cat_Description_EnColumnName = "Cat_Description_En";
        public const string IsActiveColumnName = "IsActive";

        // Instance fields
        protected MainDB _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryCollection_Base"/> 
        /// class with the specified <see cref="MainDB"/>.
        /// </summary>
        /// <param name="db">The <see cref="MainDB"/> object.</param>
        public CategoryCollection_Base(MainDB db)
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
        /// Gets an array of all records from the <c>Category</c> table.
        /// </summary>
        /// <returns>An array of <see cref="CategoryRow"/> objects.</returns>
        public virtual CategoryRow[] GetAll()
        {
            return MapRecords(CreateGetAllCommand());
        }

        /// <summary>
        /// Gets a <see cref="System.Data.DataTable"/> object that 
        /// includes all records from the <c>Category</c> table.
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
                    if (ObjStr.IsSubString(ObjStr.StandNonUnicode(Dr[StrCol].ToString()), ObjStr.StandNonUnicode(StrCmp)))
                    { DTable.ImportRow(Dr); }
                }
            }
            else DTable = null;
            return DTable;
        }
        public DataTable Searching(string StrColumnName, int IntValue)
        {
            DataTable Dt = GetAllAsDataTable();
            if (Dt.Columns.Contains(StrColumnName))
            {
                return GetAsDataTable("" + StrColumnName + "=" + IntValue, "");
            }
            else return null;
        }
        public DataTable Searching(string StrColumnName, int IntValue, bool Above)
        {
            DataTable Dt = GetAllAsDataTable();
            if (Dt.Columns.Contains(StrColumnName))
            {
                if (Above)
                    return GetAsDataTable("" + StrColumnName + ">=" + IntValue, "");
                else
                    return GetAsDataTable("" + StrColumnName + "<=" + IntValue, "");
            }
            else return null;
        }
        public DataTable Searching(string StrColumnName, int IntValueStart, int IntValueEnd)
        {
            DataTable Dt = GetAllAsDataTable();
            if (Dt.Columns.Contains(StrColumnName))
            {
                return GetAsDataTable("" + StrColumnName + ">=" + IntValueStart + " AND " + StrColumnName + "<=" + IntValueEnd, "");
            }
            else return null;
        }

        public DataTable Searching(string StrColumnName, DateTime ObjDate)
        {
            DataTable Dt = GetAllAsDataTable();
            if (Dt.Columns.Contains(StrColumnName))
                return GetAsDataTable("" + StrColumnName + "=" + GetDateFormat(ObjDate), "");
            else return null;
        }
        public DataTable Searching(string StrColumnName, DateTime ObjDate, bool Before)
        {
            DataTable Dt = GetAllAsDataTable();
            if (Dt.Columns.Contains(StrColumnName))
                if (Before)
                    return GetAsDataTable("" + StrColumnName + "<=" + GetDateFormat(ObjDate), "");
                else
                    return GetAsDataTable("" + StrColumnName + ">=" + GetDateFormat(ObjDate), "");
            else return null;
        }
        public DataTable Searching(string StrColumnName, DateTime ObjDateStart, DateTime ObjDateEnd)
        {
            DataTable Dt = GetAllAsDataTable();
            if (Dt.Columns.Contains(StrColumnName))
                return GetAsDataTable("" + StrColumnName + ">=" + GetDateFormat(ObjDateStart) + " AND " + StrColumnName + "<=" + GetDateFormat(ObjDateEnd), "");
            else return null;
        }
        public DataTable Searching(string StrColumnName, bool Status)
        {
            DataTable Dt = GetAllAsDataTable();
            if (Dt.Columns.Contains(StrColumnName))
                return GetAsDataTable("" + StrColumnName + "=" + Status, "");
            else return null;
        }
        /// <summary>
        /// Creates and returns an <see cref="System.Data.IDbCommand"/> object that is used
        /// to retrieve all records from the <c>Category</c> table.
        /// </summary>
        /// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
        protected virtual IDbCommand CreateGetAllCommand()
        {
            return CreateGetCommand(null, null);
        }

        /// <summary>
        /// Gets the first <see cref="CategoryRow"/> objects that 
        /// match the search condition.
        /// </summary>
        /// <param name="whereSql">The SQL search condition. For example: 
        /// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
        /// <returns>An instance of <see cref="CategoryRow"/> or null reference 
        /// (Nothing in Visual Basic) if the object was not found.</returns>
        public CategoryRow GetRow(string whereSql)
        {
            int totalRecordCount = -1;
            CategoryRow[] rows = GetAsArray(whereSql, null, 0, 1, ref totalRecordCount);
            return 0 == rows.Length ? null : rows[0];
        }

        /// <summary>
        /// Gets an array of <see cref="CategoryRow"/> objects that 
        /// match the search condition, in the the specified sort order.
        /// </summary>
        /// <param name="whereSql">The SQL search condition. For example: 
        /// <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
        /// <param name="orderBySql">The column name(s) followed by "ASC" (ascending) or "DESC" (descending).
        /// Columns are sorted in ascending order by default. For example: <c>"LastName ASC, FirstName ASC"</c>.</param>
        /// <returns>An array of <see cref="CategoryRow"/> objects.</returns>
        public CategoryRow[] GetAsArray(string whereSql, string orderBySql)
        {
            int totalRecordCount = -1;
            return GetAsArray(whereSql, orderBySql, 0, int.MaxValue, ref totalRecordCount);
        }

        /// <summary>
        /// Gets an array of <see cref="CategoryRow"/> objects that 
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
        /// <returns>An array of <see cref="CategoryRow"/> objects.</returns>
        public virtual CategoryRow[] GetAsArray(string whereSql, string orderBySql,
                            int startIndex, int length, ref int totalRecordCount)
        {
            using (IDataReader reader = _db.ExecuteReader(CreateGetCommand(whereSql, orderBySql)))
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
            using (IDataReader reader = _db.ExecuteReader(CreateGetCommand(whereSql, orderBySql)))
            {
                return MapRecordsToDataTable(reader, startIndex, length, ref totalRecordCount);
            }
        }

        public virtual CategoryRow[] GetTopAsArray(int topNum, string whereSql, string orderBySql)
        {
            int totalRecordCount = -1;
            using (IDataReader reader = _db.ExecuteReader(CreateGetTopCommand(topNum, whereSql, orderBySql)))
            {
                return MapRecords(reader, 0, int.MaxValue, ref totalRecordCount);
            }
        }

        public virtual DataTable GetTopAsDataTable(int topNum, string whereSql, string orderBySql)
        {
            int totalRecordCount = -1;
            using (IDataReader reader = _db.ExecuteReader(CreateGetTopCommand(topNum, whereSql, orderBySql)))
            {
                return MapRecordsToDataTable(reader, 0, int.MaxValue, ref totalRecordCount);
            }
        }

        public virtual int GetCount(string sqlWhere)
        {
            string sqlStr = "Select Count(*) From [dbo].[Category]" + ((sqlWhere == "") ? "" : (" Where " + sqlWhere));
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
        /// Gets Custom Page of CategoryRow
        /// </summary>
        /// <param name="pageNumber">Selected Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="whereSql">Where Clause</param>
        /// <param name="orderBySql">Order By Clause</param>
        /// <returns>An Array of CategoryRow</returns>
        public CategoryRow[] GetPageAsArray(int pageNumber, int pageSize, string whereSql, string orderBySql)
        {
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
        public DataTable GetPageAsDataTable(int pageNumber, int pageSize, string whereSql, string orderBySql)
        {
            int startIndex = (pageNumber - 1) * pageSize;
            int totalRecordCount = -1;
            return GetAsDataTable(whereSql, orderBySql, startIndex, pageSize, ref totalRecordCount);
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
            using (IDataReader reader = _db.ExecuteReader(CreateGetCommand(listFields, whereSql, orderBySql)))
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
            for (int _fieldCount = 0; _fieldCount < listFields.Length; _fieldCount++)
            {
                sql += ((_fieldCount > 0) ? ", " : "") + listFields[_fieldCount];
            }
            sql += " FROM [dbo].[Category]";
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
        protected virtual IDbCommand CreateGetCommand(string whereSql, string orderBySql)
        {
            string sql = "SELECT * FROM [dbo].[Category] ";
            if (null != whereSql && 0 < whereSql.Length)
                sql += " WHERE " + whereSql;
            if (null != orderBySql && 0 < orderBySql.Length)
                sql += " ORDER BY " + orderBySql;
            return _db.CreateCommand(sql);

            //return _db.FrontEndStoreProcedure.Category_GetListByWhere(whereSql, orderBySql);

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
            string sql = "SELECT TOP " + topNum.ToString() + " * FROM [dbo].[Category]";
            if (null != whereSql && 0 < whereSql.Length)
                sql += " WHERE " + whereSql;
            if (null != orderBySql && 0 < orderBySql.Length)
                sql += " ORDER BY " + orderBySql;
            return _db.CreateCommand(sql);
        }

        /// <summary>
        /// Gets <see cref="CategoryRow"/> by the primary key.
        /// </summary>
        /// <param name="cat_ID">The <c>Cat_ID</c> column value.</param>
        /// <returns>An instance of <see cref="CategoryRow"/> or null reference 
        /// (Nothing in Visual Basic) if the object was not found.</returns>
        public virtual CategoryRow GetByPrimaryKey(int cat_ID)
        {
            string whereSql = "[Cat_ID]=" + _db.CreateSqlParameterName("@Cat_ID");
            IDbCommand cmd = CreateGetCommand(whereSql, null);
            AddParameter(cmd, "@Cat_ID", cat_ID);
            CategoryRow[] tempArray = MapRecords(cmd);
            return 0 == tempArray.Length ? null : tempArray[0];
        }

        /// <summary>
        /// Gets an array of <see cref="CategoryRow"/> objects 
        /// by the <c>FK_Category_EditionType</c> foreign key.
        /// </summary>
        /// <param name="editionType_ID">The <c>EditionType_ID</c> column value.</param>
        /// <returns>An array of <see cref="CategoryRow"/> objects.</returns>
        public CategoryRow[] GetByEditionType_ID(int editionType_ID)
        {
            return GetByEditionType_ID(editionType_ID, false);
        }

        /// <summary>
        /// Gets an array of <see cref="CategoryRow"/> objects 
        /// by the <c>FK_Category_EditionType</c> foreign key.
        /// </summary>
        /// <param name="editionType_ID">The <c>EditionType_ID</c> column value.</param>
        /// <param name="editionType_IDNull">true if the method ignores the editionType_ID
        /// parameter value and uses DbNull instead of it; otherwise, false.</param>
        /// <returns>An array of <see cref="CategoryRow"/> objects.</returns>
        public virtual CategoryRow[] GetByEditionType_ID(int editionType_ID, bool editionType_IDNull)
        {
            return MapRecords(CreateGetByEditionType_IDCommand(editionType_ID, editionType_IDNull));
        }

        /// <summary>
        /// Gets a <see cref="System.Data.DataTable"/> object 
        /// by the <c>FK_Category_EditionType</c> foreign key.
        /// </summary>
        /// <param name="editionType_ID">The <c>EditionType_ID</c> column value.</param>
        /// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
        public DataTable GetByEditionType_IDAsDataTable(int editionType_ID)
        {
            return GetByEditionType_IDAsDataTable(editionType_ID, false);
        }

        /// <summary>
        /// Gets a <see cref="System.Data.DataTable"/> object 
        /// by the <c>FK_Category_EditionType</c> foreign key.
        /// </summary>
        /// <param name="editionType_ID">The <c>EditionType_ID</c> column value.</param>
        /// <param name="editionType_IDNull">true if the method ignores the editionType_ID
        /// parameter value and uses DbNull instead of it; otherwise, false.</param>
        /// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
        public virtual DataTable GetByEditionType_IDAsDataTable(int editionType_ID, bool editionType_IDNull)
        {
            return MapRecordsToDataTable(CreateGetByEditionType_IDCommand(editionType_ID, editionType_IDNull));
        }

        /// <summary>
        /// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to 
        /// return records by the <c>FK_Category_EditionType</c> foreign key.
        /// </summary>
        /// <param name="editionType_ID">The <c>EditionType_ID</c> column value.</param>
        /// <param name="editionType_IDNull">true if the method ignores the editionType_ID
        /// parameter value and uses DbNull instead of it; otherwise, false.</param>
        /// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
        protected virtual IDbCommand CreateGetByEditionType_IDCommand(int editionType_ID, bool editionType_IDNull)
        {
            string whereSql = "";
            if (editionType_IDNull)
                whereSql += "[EditionType_ID] IS NULL";
            else
                whereSql += "[EditionType_ID]=" + _db.CreateSqlParameterName("@EditionType_ID");

            IDbCommand cmd = CreateGetCommand(whereSql, null);
            if (!editionType_IDNull)
                AddParameter(cmd, "@EditionType_ID", editionType_ID);
            return cmd;
        }

        /// <summary>
        /// Adds a new record into the <c>Category</c> table.
        /// </summary>
        /// <param name="value">The <see cref="CategoryRow"/> object to be inserted.</param>
        public virtual void Insert(CategoryRow value)
        {
            string sqlStr = "INSERT INTO [dbo].[Category] (" +
                "[Cat_Name], " +
                "[Cat_Description], " +
                "[Cat_DisplayURL], " +
                "[Cat_ParentID], " +
                "[Cat_isColumn], " +
                "[Cat_isHidden], " +
                "[Cat_ViewNum], " +
                "[EditionType_ID], " +
                "[Cat_Icon], " +
                "[Cat_Order]," +
                "[Cat_KeyWords]" +
                ") VALUES (" +
                _db.CreateSqlParameterName("@Cat_Name") + ", " +
                _db.CreateSqlParameterName("@Cat_Description") + ", " +
                _db.CreateSqlParameterName("@Cat_DisplayURL") + ", " +
                _db.CreateSqlParameterName("@Cat_ParentID") + ", " +
                _db.CreateSqlParameterName("@Cat_isColumn") + ", " +
                _db.CreateSqlParameterName("@Cat_isHidden") + ", " +
                _db.CreateSqlParameterName("@Cat_ViewNum") + ", " +
                _db.CreateSqlParameterName("@EditionType_ID") + ", " +
                _db.CreateSqlParameterName("@Cat_Icon") + ", " +
                _db.CreateSqlParameterName("@Cat_Order") + ", " +
                _db.CreateSqlParameterName("@Cat_KeyWords") + ");SELECT @@IDENTITY";
            IDbCommand cmd = _db.CreateCommand(sqlStr);
            AddParameter(cmd, "@Cat_Name", value.Cat_Name);
            AddParameter(cmd, "@Cat_Description", value.Cat_Description);
            AddParameter(cmd, "@Cat_DisplayURL", value.Cat_DisplayURL);
            AddParameter(cmd, "@Cat_ParentID",
                value.IsCat_ParentIDNull ? DBNull.Value : (object)value.Cat_ParentID);
            AddParameter(cmd, "@Cat_isColumn",
                value.IsCat_isColumnNull ? DBNull.Value : (object)value.Cat_isColumn);
            AddParameter(cmd, "@Cat_isHidden",
                value.IsCat_isHiddenNull ? DBNull.Value : (object)value.Cat_isHidden);
            AddParameter(cmd, "@Cat_ViewNum",
                value.IsCat_ViewNumNull ? DBNull.Value : (object)value.Cat_ViewNum);
            AddParameter(cmd, "@EditionType_ID",
                value.IsEditionType_IDNull ? DBNull.Value : (object)value.EditionType_ID);
            AddParameter(cmd, "@Cat_Icon", value.Cat_Icon);
            AddParameter(cmd, "@Cat_Order",
                value.IsCat_OrderNull ? DBNull.Value : (object)value.Cat_Order);
            AddParameter(cmd, "@Cat_KeyWords", value.Cat_KeyWords);
            value.Cat_ID = Convert.ToInt32(cmd.ExecuteScalar());
        }

        /// <summary>
        /// Updates a record in the <c>Category</c> table.
        /// </summary>
        /// <param name="value">The <see cref="CategoryRow"/>
        /// object used to update the table record.</param>
        /// <returns>true if the record was updated; otherwise, false.</returns>
        public virtual bool Update(CategoryRow value)
        {
            string sqlStr = "UPDATE [dbo].[Category] SET " +
                "[Cat_Name]=" + _db.CreateSqlParameterName("@Cat_Name") + ", " +
                "[Cat_KeyWords]=" + _db.CreateSqlParameterName("@Cat_KeyWords") + ", " +
                "[Cat_Description]=" + _db.CreateSqlParameterName("@Cat_Description") + ", " +
                "[Cat_DisplayURL]=" + _db.CreateSqlParameterName("@Cat_DisplayURL") + ", " +
                "[Cat_ParentID]=" + _db.CreateSqlParameterName("@Cat_ParentID") + ", " +
                "[Cat_isColumn]=" + _db.CreateSqlParameterName("@Cat_isColumn") + ", " +
                "[Cat_isHidden]=" + _db.CreateSqlParameterName("@Cat_isHidden") + ", " +
                "[Cat_ViewNum]=" + _db.CreateSqlParameterName("@Cat_ViewNum") + ", " +
                "[EditionType_ID]=" + _db.CreateSqlParameterName("@EditionType_ID") + ", " +
                "[Cat_Icon]=" + _db.CreateSqlParameterName("@Cat_Icon") + ", " +
                "[Cat_Order]=" + _db.CreateSqlParameterName("@Cat_Order") + ", " +
                "[Cat_Name_En]=" + _db.CreateSqlParameterName("@Cat_Name_En") + ", " +
                "[Cat_Description_En]=" + _db.CreateSqlParameterName("@Cat_Description_En") + ", " +
                "[IsActive]=" + _db.CreateSqlParameterName("@IsActive") +
                " WHERE " +
                "[Cat_ID]=" + _db.CreateSqlParameterName("@Cat_ID");
            IDbCommand cmd = _db.CreateCommand(sqlStr);
            AddParameter(cmd, "@Cat_Name", value.Cat_Name);
            AddParameter(cmd, "@Cat_KeyWords", value.Cat_KeyWords);
            AddParameter(cmd, "@Cat_Description", value.Cat_Description);
            AddParameter(cmd, "@Cat_DisplayURL", value.Cat_DisplayURL);
            AddParameter(cmd, "@Cat_ParentID",
                value.IsCat_ParentIDNull ? DBNull.Value : (object)value.Cat_ParentID);
            AddParameter(cmd, "@Cat_isColumn", value.IsCat_isColumnNull ? DBNull.Value : (object)value.Cat_isColumn);
            AddParameter(cmd, "@Cat_isHidden", value.IsCat_isHiddenNull ? DBNull.Value : (object)value.Cat_isHidden);
            AddParameter(cmd, "@Cat_ViewNum", value.IsCat_ViewNumNull ? DBNull.Value : (object)value.Cat_ViewNum);
            AddParameter(cmd, "@EditionType_ID", value.IsEditionType_IDNull ? DBNull.Value : (object)value.EditionType_ID);
            AddParameter(cmd, "@Cat_Icon", value.Cat_Icon);
            AddParameter(cmd, "@Cat_Order", value.IsCat_OrderNull ? DBNull.Value : (object)value.Cat_Order);
            AddParameter(cmd, "@Cat_Name_En", value.Cat_Name_En);
            AddParameter(cmd, "@Cat_Description_En", value.Cat_Description_En);
            AddParameter(cmd, "@IsActive", value.IsActive);
            AddParameter(cmd, "@Cat_ID", value.Cat_ID);
            return 0 != cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Updates the <c>Category</c> table and calls the <c>AcceptChanges</c> method
        /// on the changed DataRow objects.
        /// </summary>
        /// <param name="table">The <see cref="System.Data.DataTable"/> used to update the data source.</param>
        public void Update(DataTable table)
        {
            Update(table, true);
        }

        /// <summary>
        /// Updates the <c>Category</c> table. Pass <c>false</c> as the <c>acceptChanges</c> 
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
            for (int i = rows.Count - 1; i >= 0; i--)
            {
                DataRow row = rows[i];
                switch (row.RowState)
                {
                    case DataRowState.Added:
                        Insert(MapRow(row));
                        if (acceptChanges)
                            row.AcceptChanges();
                        break;

                    case DataRowState.Deleted:
                        // Temporary reject changes to be able to access to the PK column(s)
                        row.RejectChanges();
                        try
                        {
                            DeleteByPrimaryKey((int)row["Cat_ID"]);
                        }
                        finally
                        {
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

        /// <summary>
        /// Deletes the specified object from the <c>Category</c> table.
        /// </summary>
        /// <param name="value">The <see cref="CategoryRow"/> object to delete.</param>
        /// <returns>true if the record was deleted; otherwise, false.</returns>
        public bool Delete(CategoryRow value)
        {
            return DeleteByPrimaryKey(value.Cat_ID);
        }

        /// <summary>
        /// Deletes a record from the <c>Category</c> table using
        /// the specified primary key.
        /// </summary>
        /// <param name="cat_ID">The <c>Cat_ID</c> column value.</param>
        /// <returns>true if the record was deleted; otherwise, false.</returns>
        public virtual bool DeleteByPrimaryKey(int cat_ID)
        {
            string whereSql = "[Cat_ID]=" + _db.CreateSqlParameterName("@Cat_ID");
            IDbCommand cmd = CreateDeleteCommand(whereSql);
            AddParameter(cmd, "@Cat_ID", cat_ID);
            return 0 < cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Deletes records from the <c>Category</c> table using the 
        /// <c>FK_Category_EditionType</c> foreign key.
        /// </summary>
        /// <param name="editionType_ID">The <c>EditionType_ID</c> column value.</param>
        /// <returns>The number of records deleted from the table.</returns>
        public int DeleteByEditionType_ID(int editionType_ID)
        {
            return DeleteByEditionType_ID(editionType_ID, false);
        }

        /// <summary>
        /// Deletes records from the <c>Category</c> table using the 
        /// <c>FK_Category_EditionType</c> foreign key.
        /// </summary>
        /// <param name="editionType_ID">The <c>EditionType_ID</c> column value.</param>
        /// <param name="editionType_IDNull">true if the method ignores the editionType_ID
        /// parameter value and uses DbNull instead of it; otherwise, false.</param>
        /// <returns>The number of records deleted from the table.</returns>
        public int DeleteByEditionType_ID(int editionType_ID, bool editionType_IDNull)
        {
            return CreateDeleteByEditionType_IDCommand(editionType_ID, editionType_IDNull).ExecuteNonQuery();
        }

        /// <summary>
        /// Creates an <see cref="System.Data.IDbCommand"/> object that can be used to
        /// delete records using the <c>FK_Category_EditionType</c> foreign key.
        /// </summary>
        /// <param name="editionType_ID">The <c>EditionType_ID</c> column value.</param>
        /// <param name="editionType_IDNull">true if the method ignores the editionType_ID
        /// parameter value and uses DbNull instead of it; otherwise, false.</param>
        /// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
        protected virtual IDbCommand CreateDeleteByEditionType_IDCommand(int editionType_ID, bool editionType_IDNull)
        {
            string whereSql = "";
            if (editionType_IDNull)
                whereSql += "[EditionType_ID] IS NULL";
            else
                whereSql += "[EditionType_ID]=" + _db.CreateSqlParameterName("@EditionType_ID");

            IDbCommand cmd = CreateDeleteCommand(whereSql);
            if (!editionType_IDNull)
                AddParameter(cmd, "@EditionType_ID", editionType_ID);
            return cmd;
        }

        /// <summary>
        /// Deletes <c>Category</c> records that match the specified criteria.
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
        /// to delete <c>Category</c> records that match the specified criteria.
        /// </summary>
        /// <param name="whereSql">The SQL search condition. 
        /// For example: <c>"FirstName='Smith' AND Zip=75038"</c>.</param>
        /// <returns>A reference to the <see cref="System.Data.IDbCommand"/> object.</returns>
        protected virtual IDbCommand CreateDeleteCommand(string whereSql)
        {
            string sql = "DELETE FROM [dbo].[Category]";
            if (null != whereSql && 0 < whereSql.Length)
                sql += " WHERE " + whereSql;
            return _db.CreateCommand(sql);
        }

        /// <summary>
        /// Deletes all records from the <c>Category</c> table.
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
        /// <returns>An array of <see cref="CategoryRow"/> objects.</returns>
        protected CategoryRow[] MapRecords(IDbCommand command)
        {
            using (IDataReader reader = _db.ExecuteReader(command))
            {
                return MapRecords(reader);
            }
        }

        /// <summary>
        /// Reads data from the provided data reader and returns 
        /// an array of mapped objects.
        /// </summary>
        /// <param name="reader">The <see cref="System.Data.IDataReader"/> object to read data from the table.</param>
        /// <returns>An array of <see cref="CategoryRow"/> objects.</returns>
        protected CategoryRow[] MapRecords(IDataReader reader)
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
        /// <returns>An array of <see cref="CategoryRow"/> objects.</returns>
        protected virtual CategoryRow[] MapRecords(IDataReader reader,
                                        int startIndex, int length, ref int totalRecordCount)
        {
            if (0 > startIndex)
                throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
            if (0 > length)
                throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

            int cat_IDColumnIndex = reader.GetOrdinal("Cat_ID");
            int channel_IDColumnIndex = 1;
            int cat_NameColumnIndex = reader.GetOrdinal("Cat_Name");
            int cat_DescriptionColumnIndex = reader.GetOrdinal("Cat_Description");
            int cat_DisplayURLColumnIndex = reader.GetOrdinal("Cat_DisplayURL");
            int cat_ParentIDColumnIndex = reader.GetOrdinal("Cat_ParentID");
            int cat_isColumnColumnIndex = reader.GetOrdinal("Cat_isColumn");
            int cat_isHiddenColumnIndex = reader.GetOrdinal("Cat_isHidden");
            int cat_ViewNumColumnIndex = reader.GetOrdinal("Cat_ViewNum");
            int editionType_IDColumnIndex = reader.GetOrdinal("EditionType_ID");
            int cat_IconColumnIndex = reader.GetOrdinal("Cat_Icon");
            int cat_OrderColumnIndex = reader.GetOrdinal("Cat_Order");
            int Cat_KeyWordsColumnIndex = reader.GetOrdinal("Cat_KeyWords");

            System.Collections.ArrayList recordList = new System.Collections.ArrayList();
            int ri = -startIndex;
            while (reader.Read())
            {
                ri++;
                if (ri > 0 && ri <= length)
                {
                    CategoryRow record = new CategoryRow();
                    recordList.Add(record);

                    record.Cat_ID = Convert.ToInt32(reader.GetValue(cat_IDColumnIndex));
                    record.Cat_Name = Convert.ToString(reader.GetValue(cat_NameColumnIndex));
                    record.Cat_KeyWords = Convert.ToString(reader.GetValue(Cat_KeyWordsColumnIndex));

                    if (!reader.IsDBNull(cat_DescriptionColumnIndex))
                        record.Cat_Description = Convert.ToString(reader.GetValue(cat_DescriptionColumnIndex));
                    if (!reader.IsDBNull(cat_DisplayURLColumnIndex))
                        record.Cat_DisplayURL = Convert.ToString(reader.GetValue(cat_DisplayURLColumnIndex));
                    if (!reader.IsDBNull(cat_ParentIDColumnIndex))
                        record.Cat_ParentID = Convert.ToInt32(reader.GetValue(cat_ParentIDColumnIndex));
                    if (!reader.IsDBNull(cat_isColumnColumnIndex))
                        record.Cat_isColumn = Convert.ToBoolean(reader.GetValue(cat_isColumnColumnIndex));
                    if (!reader.IsDBNull(cat_isHiddenColumnIndex))
                        record.Cat_isHidden = Convert.ToBoolean(reader.GetValue(cat_isHiddenColumnIndex));
                    if (!reader.IsDBNull(cat_ViewNumColumnIndex))
                        record.Cat_ViewNum = Convert.ToInt32(reader.GetValue(cat_ViewNumColumnIndex));
                    if (!reader.IsDBNull(editionType_IDColumnIndex))
                        record.EditionType_ID = Convert.ToInt32(reader.GetValue(editionType_IDColumnIndex));
                    if (!reader.IsDBNull(cat_IconColumnIndex))
                        record.Cat_Icon = Convert.ToString(reader.GetValue(cat_IconColumnIndex));
                    if (!reader.IsDBNull(cat_OrderColumnIndex))
                        record.Cat_Order = Convert.ToInt32(reader.GetValue(cat_OrderColumnIndex));
                    record.Channel_ID = 1;
                    if (ri == length && 0 != totalRecordCount)
                        break;
                }
            }

            totalRecordCount = 0 == totalRecordCount ? ri + startIndex : -1;
            return (CategoryRow[])(recordList.ToArray(typeof(CategoryRow)));
        }

        /// <summary>
        /// Reads data using the specified command and returns 
        /// a filled <see cref="System.Data.DataTable"/> object.
        /// </summary>
        /// <param name="command">The <see cref="System.Data.IDbCommand"/> object.</param>
        /// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
        protected DataTable MapRecordsToDataTable(IDbCommand command)
        {
            using (IDataReader reader = _db.ExecuteReader(command))
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
            if (0 > startIndex)
                throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
            if (0 > length)
                throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

            int columnCount = reader.FieldCount;
            int ri = -startIndex;

            DataTable dataTable = CreateDataTable();
            dataTable.BeginLoadData();
            object[] values = new object[columnCount];

            while (reader.Read())
            {
                ri++;
                if (ri > 0 && ri <= length)
                {
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
            if (0 > startIndex)
                throw new ArgumentOutOfRangeException("startIndex", startIndex, "StartIndex cannot be less than zero.");
            if (0 > length)
                throw new ArgumentOutOfRangeException("length", length, "Length cannot be less than zero.");

            int columnCount = reader.FieldCount;
            int ri = -startIndex;

            DataTable dataTable = CreateDataTable(listFields);
            dataTable.BeginLoadData();
            object[] values = new object[columnCount];

            while (reader.Read())
            {
                ri++;
                if (ri > 0 && ri <= length)
                {
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

        /// <summary>
        /// Converts <see cref="System.Data.DataRow"/> to <see cref="CategoryRow"/>.
        /// </summary>
        /// <param name="row">The <see cref="System.Data.DataRow"/> object to be mapped.</param>
        /// <returns>A reference to the <see cref="CategoryRow"/> object.</returns>
        protected virtual CategoryRow MapRow(DataRow row)
        {
            CategoryRow mappedObject = new CategoryRow();
            DataTable dataTable = row.Table;
            DataColumn dataColumn;
            // Column "Cat_ID"
            dataColumn = dataTable.Columns["Cat_ID"];
            if (!row.IsNull(dataColumn))
                mappedObject.Cat_ID = (int)row[dataColumn];
            // Column "Cat_Name"
            dataColumn = dataTable.Columns["Cat_Name"];
            if (!row.IsNull(dataColumn))
                mappedObject.Cat_Name = (string)row[dataColumn];
            // Column "Cat_Description"
            dataColumn = dataTable.Columns["Cat_Description"];
            if (!row.IsNull(dataColumn))
                mappedObject.Cat_Description = (string)row[dataColumn];
            // Column "Cat_DisplayURL"
            dataColumn = dataTable.Columns["Cat_DisplayURL"];
            if (!row.IsNull(dataColumn))
                mappedObject.Cat_DisplayURL = (string)row[dataColumn];
            // Column "Cat_ParentID"
            dataColumn = dataTable.Columns["Cat_ParentID"];
            if (!row.IsNull(dataColumn))
                mappedObject.Cat_ParentID = (int)row[dataColumn];
            // Column "Cat_isColumn"
            dataColumn = dataTable.Columns["Cat_isColumn"];
            if (!row.IsNull(dataColumn))
                mappedObject.Cat_isColumn = (bool)row[dataColumn];
            // Column "Cat_isHidden"
            dataColumn = dataTable.Columns["Cat_isHidden"];
            if (!row.IsNull(dataColumn))
                mappedObject.Cat_isHidden = (bool)row[dataColumn];
            // Column "Cat_ViewNum"
            dataColumn = dataTable.Columns["Cat_ViewNum"];
            if (!row.IsNull(dataColumn))
                mappedObject.Cat_ViewNum = (int)row[dataColumn];
            // Column "EditionType_ID"
            dataColumn = dataTable.Columns["EditionType_ID"];
            if (!row.IsNull(dataColumn))
                mappedObject.EditionType_ID = (int)row[dataColumn];
            // Column "Cat_Icon"
            dataColumn = dataTable.Columns["Cat_Icon"];
            if (!row.IsNull(dataColumn))
                mappedObject.Cat_Icon = (string)row[dataColumn];
            // Column "Cat_Order"
            dataColumn = dataTable.Columns["Cat_Order"];
            if (!row.IsNull(dataColumn))
                mappedObject.Cat_Order = (int)row[dataColumn];
            mappedObject.Channel_ID = 1;
            // Column "Cat_KeyWords"
            dataColumn = dataTable.Columns["Cat_KeyWords"];
            if (!row.IsNull(dataColumn))
                mappedObject.Cat_KeyWords = (string)row[dataColumn];
            return mappedObject;
        }

        /// <summary>
        /// Creates a <see cref="System.Data.DataTable"/> object for 
        /// the <c>Category</c> table.
        /// </summary>
        /// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
        protected virtual DataTable CreateDataTable(string[] listFields)
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Category";
            foreach (string column in listFields)
            {
                dataTable.Columns.Add(column, typeof(string));
            }
            return dataTable;
        }

        /// <summary>
        /// Creates a <see cref="System.Data.DataTable"/> object for 
        /// the <c>Category</c> table.
        /// </summary>
        /// <returns>A reference to the <see cref="System.Data.DataTable"/> object.</returns>
        protected virtual DataTable CreateDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Category";
            DataColumn dataColumn;
            dataColumn = dataTable.Columns.Add("Cat_ID", typeof(int));
            dataColumn.AllowDBNull = false;
            dataColumn.ReadOnly = true;
            dataColumn.Unique = true;
            dataColumn.AutoIncrement = true;
            dataColumn = dataTable.Columns.Add("Cat_Name", typeof(string));
            dataColumn.MaxLength = 500;
            dataColumn.AllowDBNull = false;
            dataColumn = dataTable.Columns.Add("Cat_Description", typeof(string));
            dataColumn = dataTable.Columns.Add("Cat_DisplayURL", typeof(string));
            dataColumn.MaxLength = 500;
            dataColumn = dataTable.Columns.Add("Cat_ParentID", typeof(int));
            dataColumn = dataTable.Columns.Add("Cat_isColumn", typeof(bool));
            dataColumn = dataTable.Columns.Add("Cat_isHidden", typeof(bool));
            dataColumn = dataTable.Columns.Add("Cat_ViewNum", typeof(int));
            dataColumn = dataTable.Columns.Add("EditionType_ID", typeof(int));
            dataColumn = dataTable.Columns.Add("Cat_Icon", typeof(string));
            dataColumn.MaxLength = 100;
            dataColumn = dataTable.Columns.Add("Cat_Order", typeof(int));
            dataColumn = dataTable.Columns.Add("Channel_ID", typeof(int));
            dataColumn = dataTable.Columns.Add("Cat_KeyWords", typeof(string));
            dataColumn.MaxLength = 500;
            dataColumn = dataTable.Columns.Add("Cat_Name_En", typeof(string));
            dataColumn.MaxLength = 500;
            dataColumn = dataTable.Columns.Add("Cat_Description_En", typeof(string));
            dataColumn = dataTable.Columns.Add("IsActive", typeof(bool));

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
            switch (paramName)
            {
                case "@Cat_ID":
                    parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
                    break;

                case "@Cat_Name":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                case "@Cat_Description":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                case "@Cat_DisplayURL":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                case "@Cat_ParentID":
                    parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
                    break;

                case "@Cat_isColumn":
                    parameter = _db.AddParameter(cmd, paramName, DbType.Boolean, value);
                    break;

                case "@Cat_isHidden":
                    parameter = _db.AddParameter(cmd, paramName, DbType.Boolean, value);
                    break;

                case "@Cat_ViewNum":
                    parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
                    break;

                case "@EditionType_ID":
                    parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
                    break;

                case "@Cat_Icon":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;

                case "@Cat_Order":
                    parameter = _db.AddParameter(cmd, paramName, DbType.Int32, value);
                    break;

                case "@Cat_KeyWords":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;
                case "@Cat_Name_En":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;
                case "@Cat_Description_En":
                    parameter = _db.AddParameter(cmd, paramName, DbType.String, value);
                    break;
                case "@IsActive":
                    parameter = _db.AddParameter(cmd, paramName, DbType.Boolean, value);
                    break;
                default:
                    throw new ArgumentException("Unknown parameter name (" + paramName + ").");
            }
            return parameter;
        }
    } // End of CategoryCollection_Base class
}  // End of namespace

