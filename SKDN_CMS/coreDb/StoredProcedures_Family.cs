using System;
using System.Data;

namespace DFISYS.Core.DAL
{
    public class StoredProcedures_Family
    {
        private readonly MainDB _db;

        public StoredProcedures_Family(MainDB db)
        {
            _db = db;
        }

        public MainDB Database
        {
            get { return _db; }
        }

        protected DataTable CreateDataTable(IDbCommand command)
        {
            return _db.CreateDataTable(command);
        }

        //public DataTable vc_News_SelectList_By_Cat_And_Top(Int32 Cat_ID, Int32 Top)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_News_SelectList_By_Cat_And_Top", true);
        //    _db.AddParameter(cmd, "iCat_ID", DbType.Int32, Cat_ID);
        //    _db.AddParameter(cmd, "iTop", DbType.Int32, Top);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public DataTable vc_News_SelectList_By_Cat_Top_Focus_Mode(Int32 Cat_ID, Int32 Top, Boolean News_isFocus,
        //                                                          Int32 News_Mode, Int64 News_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_News_SelectList_By_Cat_Top_Focus_Mode", true);
        //    _db.AddParameter(cmd, "iCat_ID", DbType.Int32, Cat_ID);
        //    _db.AddParameter(cmd, "iTop", DbType.Int32, Top);
        //    if (News_isFocus)
        //        _db.AddParameter(cmd, "iNews_isFocus", DbType.Int32, 1);
        //    else
        //        _db.AddParameter(cmd, "iNews_isFocus", DbType.Int32, 0);
        //    _db.AddParameter(cmd, "iNews_Mode", DbType.Int32, News_Mode);
        //    _db.AddParameter(cmd, "iNews_ID", DbType.Int64, News_ID);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public DataTable vc_News_SelectList_ByCategory(Int32 PageIndex, Int32 PageSize, Int32 Cat_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_News_SelectList_ByCategory", true);
        //    _db.AddParameter(cmd, "iCat_ID", DbType.Int32, Cat_ID);
        //    _db.AddParameter(cmd, "iPageIndex", DbType.Int32, PageIndex);
        //    _db.AddParameter(cmd, "iPageSize", DbType.Int32, PageSize);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public DataTable vc_News_SelectListCount_ByCategory(Int32 Cat_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_News_SelectListCount_ByCategory", true);
        //    _db.AddParameter(cmd, "iCat_ID", DbType.Int32, Cat_ID);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public DataTable vc_Category_SelectBy_CatParent(Int32 Cat_ParentID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_Category_SelectBy_CatParent", true);
        //    _db.AddParameter(cmd, "iCat_ParentID", DbType.Int32, Cat_ParentID);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public DataTable vc_News_SelectList_ByCategoryString(Int32 PageSize, Int32 PageIndex, String sCat_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_News_SelectList_ByCategoryString", true);
        //    _db.AddParameter(cmd, "iPageSize", DbType.Int32, PageSize);
        //    _db.AddParameter(cmd, "iPageIndex", DbType.Int32, PageIndex);
        //    _db.AddParameter(cmd, "sCat_ID", DbType.String, sCat_ID);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public DataTable vc_News_SelectListCount_ByCategoryString(String sCat_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_News_SelectListCount_ByCategoryString", true);
        //    _db.AddParameter(cmd, "sCat_ID", DbType.String, sCat_ID);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public DataTable vc_Category_SelectOne_ByID(Int32 Cat_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_Category_SelectOne_ByID", true);
        //    _db.AddParameter(cmd, "iCat_ID", DbType.Int32, Cat_ID);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public DataTable vc_News_Select_By_ListNewsID(String sNews_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_News_Select_By_ListNewsID", true);
        //    _db.AddParameter(cmd, "sNews_ID", DbType.String, sNews_ID);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public DataTable vc_Comment_SelectCount_Approved_News_ID(Int64 News_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_Comment_SelectCount_Approved_News_ID", true);
        //    _db.AddParameter(cmd, "iNews_ID", DbType.Int64, News_ID);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public DataTable vc_Comment_Select_Approved_News_ID(Int64 News_ID, Int32 PageIndex, Int32 PageSize)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_Comment_Select_Approved_News_ID", true);
        //    _db.AddParameter(cmd, "iNews_ID", DbType.Int64, News_ID);
        //    _db.AddParameter(cmd, "iPageIndex", DbType.Int32, PageIndex);
        //    _db.AddParameter(cmd, "iPageSize", DbType.Int32, PageSize);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public DataTable vc_NewsThread_Select_ByListCategory(Int32 Cat_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_NewsThread_Select_ByListCategory", true);
        //    _db.AddParameter(cmd, "iCat_ID", DbType.Int32, Cat_ID);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public DataTable vc_News_SelectList_ByThread_Count(Int32 Thread_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_News_SelectList_ByThread_Count", true);
        //    _db.AddParameter(cmd, "iThread_ID", DbType.Int32, Thread_ID);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public DataTable vc_News_SelectList_ByThread(Int32 Thread_ID, Int32 PageIndex, Int32 PageSize)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_News_SelectList_ByThread", true);
        //    _db.AddParameter(cmd, "iThread_ID", DbType.Int32, Thread_ID);
        //    _db.AddParameter(cmd, "iPageIndex", DbType.Int32, PageIndex);
        //    _db.AddParameter(cmd, "iPageSize", DbType.Int32, PageSize);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public DataTable vc_ThreadDetail_GetCatIDByThread_ID(Int32 Thread_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_ThreadDetail_GetCatIDByThread_ID", true);
        //    _db.AddParameter(cmd, "iThread_ID", DbType.Int32, Thread_ID);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public DataTable vc_News_SelectCat_ID(Int64 News_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_News_SelectCat_ID", true);
        //    _db.AddParameter(cmd, "iNews_ID", DbType.Int64, News_ID);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public DataTable VC_SelectTinChuyenMucPaging_Count(Int32 Cat_ID, Int64 News_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("VC_SelectTinChuyenMucPaging_Count", true);
        //    _db.AddParameter(cmd, "iCat_ID", DbType.Int32, Cat_ID);
        //    _db.AddParameter(cmd, "iNews_ID", DbType.Int64, News_ID);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public DataTable vc_NewsThread_SelectOne(Int32 Thread_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_NewsThread_SelectOne", true);
        //    _db.AddParameter(cmd, "iThread_ID", DbType.Int32, Thread_ID);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public DataTable vc_NewsThread_CountNews(Int32 Thread_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_NewsThread_CountNews", true);
        //    _db.AddParameter(cmd, "iThread_ID", DbType.Int32, Thread_ID);
        //    DataTable dataTable = CreateDataTable(cmd);
        //    return dataTable;
        //}

        //public void vc_NewsThread_Update(String Title, Boolean Thread_isForcus, String Thread_Logo, String Thread_RT,
        //                                 String Thread_RC, Int32 Thread_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_NewsThread_Update", true);
        //    _db.AddParameter(cmd, "iThread_ID", DbType.Int32, Thread_ID);
        //    _db.AddParameter(cmd, "sTitle", DbType.String, Title);
        //    _db.AddParameter(cmd, "bThread_isForcus", DbType.Boolean, Thread_isForcus);
        //    _db.AddParameter(cmd, "sThread_Logo", DbType.String, Thread_Logo);
        //    _db.AddParameter(cmd, "sThread_RC", DbType.String, Thread_RC);
        //    _db.AddParameter(cmd, "sThread_RT", DbType.String, Thread_RT);
        //    CreateDataTable(cmd);
        //}

        //public void vc_NewsThread_Insert(String Title, Boolean Thread_isForcus, String Thread_Logo, String Thread_RT,
        //                                 String Thread_RC)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_NewsThread_Insert", true);
        //    _db.AddParameter(cmd, "sTitle", DbType.String, Title);
        //    _db.AddParameter(cmd, "bThread_isForcus", DbType.Boolean, Thread_isForcus);
        //    _db.AddParameter(cmd, "sThread_Logo", DbType.String, Thread_Logo);
        //    _db.AddParameter(cmd, "sThread_RC", DbType.String, Thread_RC);
        //    _db.AddParameter(cmd, "sThread_RT", DbType.String, Thread_RT);
        //    CreateDataTable(cmd);
        //}

        //public DataTable vc_Category_SelectByListID(String Cat_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_Category_SelectByListID", true);
        //    _db.AddParameter(cmd, "sCat_ID", DbType.String, Cat_ID);
        //    DataTable table = CreateDataTable(cmd);
        //    return table;
        //}

        //public DataTable vc_NewsThread_SelectByListID(String Thread_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_NewsThread_SelectByListID", true);
        //    _db.AddParameter(cmd, "sThread_ID", DbType.String, Thread_ID);
        //    DataTable table = CreateDataTable(cmd);
        //    return table;
        //}

        //public DataTable vc_Sql_Run(String sql)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_Sql_Run", true);
        //    _db.AddParameter(cmd, "sSql", DbType.String, sql);
        //    DataTable table = CreateDataTable(cmd);
        //    return table;
        //}

        //public DataTable vc_NewsThread_GetCategory(String sNew_ID)
        //{
        //    IDbCommand cmd = _db.CreateCommand("vc_NewsThread_GetCategory", true);
        //    _db.AddParameter(cmd, "sNews_ID", DbType.String, sNew_ID);
        //    DataTable table = CreateDataTable(cmd);
        //    return table;
        //}
    }
}