using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace DFISYS.Core.DAL {
    public class StoredProcedures {
        private MainDB _db;

        public StoredProcedures(MainDB db) {
            _db = db;
        }

        public MainDB Database {
            get { return _db; }
        }
         public DataTable proc_CategoryLayout_Insert(int Cat_ID, int CellIndex, int ProductID)
        {
            IDbCommand cmd = _db.CreateCommand("proc_CategoryLayout_Insert", true);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            _db.AddParameter(cmd, "CellIndex", DbType.Int32, CellIndex);
            _db.AddParameter(cmd, "ProductID", DbType.Int32, ProductID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable proc_CategoryLayout_Select(int Cat_ID)
        {
            IDbCommand cmd = _db.CreateCommand("proc_CategoryLayout_Select", true);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable proc_CategoryLayout_Delete(int Cat_ID)
        {
            IDbCommand cmd = _db.CreateCommand("proc_CategoryLayout_Delete", true);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable adv_GetPositionByPage(int PageId)
        {
            IDbCommand cmd = _db.CreateCommand("adv_GetPositionByPage", true);
            _db.AddParameter(cmd, "PageId", DbType.Int32, PageId);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable adv_GetAdsByPageAndPos(int PPId)
        {
            IDbCommand cmd = _db.CreateCommand("adv_GetAdsByPageAndPos", true);
            _db.AddParameter(cmd, "PPId", DbType.Int32, PPId);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable adv_InsertNewAdv(int PPID, string Name, DateTime StartDate, DateTime EndDate, string Embed, string Description, bool isActive, bool isRotate, int Order, int Type, string Link)
        {
            IDbCommand cmd = _db.CreateCommand("adv_InsertNewAdv", true);
            _db.AddParameter(cmd, "PPID", DbType.Int32, PPID);
            _db.AddParameter(cmd, "StartDate", DbType.DateTime, StartDate);
            _db.AddParameter(cmd, "EndDate", DbType.DateTime, EndDate);
            _db.AddParameter(cmd, "Embed", DbType.String, Embed);
            _db.AddParameter(cmd, "Description", DbType.String, Description);
            _db.AddParameter(cmd, "IsActive", DbType.Boolean, isActive);
            _db.AddParameter(cmd, "IsRotate", DbType.Boolean, isRotate);
            _db.AddParameter(cmd, "Order", DbType.Int32, Order);
            _db.AddParameter(cmd, "Type", DbType.Int32, Type);
            _db.AddParameter(cmd, "Link", DbType.String, Link);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable adv_InsertNewAdv(string Name, DateTime StartDate, DateTime EndDate, string Embed, string Description, bool isActive, bool isRotate, int Order, int Type, string Link, string FilePath,int Width,int Height)
        {
            IDbCommand cmd = _db.CreateCommand("adv_InsertNewAdv", true);
            _db.AddParameter(cmd, "StartDate", DbType.DateTime, StartDate);
            _db.AddParameter(cmd, "EndDate", DbType.DateTime, EndDate);
            _db.AddParameter(cmd, "Embed", DbType.String, Embed);
            _db.AddParameter(cmd, "FilePath", DbType.String, FilePath);
            _db.AddParameter(cmd, "Description", DbType.String, Description);
            _db.AddParameter(cmd, "IsActive", DbType.Boolean, isActive);
            _db.AddParameter(cmd, "IsRotate", DbType.Boolean, isRotate);
            _db.AddParameter(cmd, "Order", DbType.Int32, Order);
            _db.AddParameter(cmd, "Type", DbType.Int32, Type);
            _db.AddParameter(cmd, "Link", DbType.String, Link);
            _db.AddParameter(cmd, "Name", DbType.String, Name);
            _db.AddParameter(cmd, "Width", DbType.Int32, Width);
            _db.AddParameter(cmd, "Height", DbType.Int32, Height);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable adv_Update(int AdvID, string Name, DateTime StartDate, DateTime EndDate, string Embed, string Description, bool isActive, bool isRotate, int Order, int Type, string Link, string FilePath,int Width,int Height)
        {
            IDbCommand cmd = _db.CreateCommand("adv_Update", true);
            _db.AddParameter(cmd, "AdvID", DbType.Int32, AdvID);
            _db.AddParameter(cmd, "StartDate", DbType.DateTime, StartDate);
            _db.AddParameter(cmd, "EndDate", DbType.DateTime, EndDate);
            _db.AddParameter(cmd, "Embed", DbType.String, Embed);
            _db.AddParameter(cmd, "FilePath", DbType.String, FilePath);
            _db.AddParameter(cmd, "Description", DbType.String, Description);
            _db.AddParameter(cmd, "IsActive", DbType.Boolean, isActive);
            _db.AddParameter(cmd, "IsRotate", DbType.Boolean, isRotate);
            _db.AddParameter(cmd, "Order", DbType.Int32, Order);
            _db.AddParameter(cmd, "Type", DbType.Int32, Type);
            _db.AddParameter(cmd, "Link", DbType.String, Link);
            _db.AddParameter(cmd, "Name", DbType.String, Name);
            _db.AddParameter(cmd, "Width", DbType.Int32, Width);
            _db.AddParameter(cmd, "Height", DbType.Int32, Height);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable adv_Details(int AdvID)
        {
            IDbCommand cmd = _db.CreateCommand("adv_Details", true);
            _db.AddParameter(cmd, "AdvID", DbType.Int32, AdvID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable adv_GetAllCategories()
        {
            IDbCommand cmd = _db.CreateCommand("adv_GetAllCategories", true);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable adv_GetAllPositions()
        {
            IDbCommand cmd = _db.CreateCommand("adv_GetAllPositions", true);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public void adv_InsertAdvPositionDetails(int Position, int page, int AdvID)
        {
            IDbCommand cmd = _db.CreateCommand("adv_InsertAdvPositionDetails", true);
            _db.AddParameter(cmd, "PosID", DbType.Int32, Position);
            _db.AddParameter(cmd, "PageID", DbType.Int32, page);
            _db.AddParameter(cmd, "AdvID", DbType.Int32, AdvID);
            _db.CreateDataTable(cmd);
        }

        public DataTable adv_GetAdvByPageAndPositions(int Category, int Position)
        {
            IDbCommand cmd = _db.CreateCommand("adv_GetAdvByPageAndPositions", true);
            _db.AddParameter(cmd, "PosID", DbType.Int32, Position);
            _db.AddParameter(cmd, "CatID", DbType.Int32, Category);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable adv_GetAllPosByAd(int AdvID)
        {
            IDbCommand cmd = _db.CreateCommand("adv_GetAllPosByAd", true);
            _db.AddParameter(cmd, "AdvID", DbType.Int32, AdvID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable adv_UpdateAdvPositionDetails(int _AdvID, int Position, string adv_pages)
        {
            IDbCommand cmd = _db.CreateCommand("adv_UpdateAdvPositionDetails", true);
            _db.AddParameter(cmd, "AdvID", DbType.Int32, _AdvID);
            _db.AddParameter(cmd, "PosID", DbType.Int32, Position);
            _db.AddParameter(cmd, "Pages", DbType.String, adv_pages);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
       

        public DataTable Nc_UpdateOrderThread(int Thread_ID, int Order,int Cate_ID, string Logo)
        {
            IDbCommand cmd = _db.CreateCommand("Nc_UpdateOrderThread", true);
            _db.AddParameter(cmd, "Thread_ID", DbType.Int32, Thread_ID);
            _db.AddParameter(cmd, "Order", DbType.Int32, Order);
            _db.AddParameter(cmd, "Cate_ID", DbType.Int32, Cate_ID);
            _db.AddParameter(cmd, "Logo", DbType.String, Logo);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable nc_GetLatestThreads(int Top)
        {
            IDbCommand cmd = _db.CreateCommand("CMS_GetLatestThreads", true);
            _db.AddParameter(cmd, "Top", DbType.Int32, Top);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
         

        protected DataTable CreateDataTable(IDbCommand command) {
            return _db.CreateDataTable(command);
        }

         
        public DataTable News_GetListNewSpecial(string Where, string StartIndex, string PageSize, string SortExpression)
        {
            IDbCommand cmd = _db.CreateCommand("CMS_GetListNewSpecial", true);
            _db.AddParameter(cmd, "Where", DbType.String, Where);
            _db.AddParameter(cmd, "StartIndex", DbType.String, StartIndex);
            _db.AddParameter(cmd, "PageSize", DbType.String, PageSize);
            _db.AddParameter(cmd, "SortExpression", DbType.String, SortExpression);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable News_GetListNewSpecialNumRows(string Where)
        {
            IDbCommand cmd = _db.CreateCommand("CMS_GetListNewSpecialNumRows", true);
            _db.AddParameter(cmd, "Where", DbType.String, Where);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public bool News_isLastAccessUser(long newsID, string userName) {
            IDbCommand cmd = _db.CreateCommand("CMS_isLastAccessUser", true);
            _db.AddParameter(cmd, "newsID", DbType.Int64, newsID);
            _db.AddParameter(cmd, "userName", DbType.String, userName);
            return (int)cmd.ExecuteScalar() == 1;
        }

        public bool News_isHasPermissionEdit(long newsID, string inCats) {
            IDbCommand cmd = _db.CreateCommand("CMS_isHasPermissionEdit", true);
            _db.AddParameter(cmd, "newsID", DbType.Int64, newsID);
            _db.AddParameter(cmd, "inCats", DbType.String, inCats);
            return (int)cmd.ExecuteScalar() == 1;
        }

             
        public DataTable vc_NewsThread_SelectList(String sWhere, Int32 iStartIndex, Int32 iPageSize) {
            IDbCommand cmd = _db.CreateCommand("CMS_NewsThread_SelectList", true);
            _db.AddParameter(cmd, "sWhere", DbType.String, sWhere);
            _db.AddParameter(cmd, "iStartIndex", DbType.Int32, iStartIndex);
            _db.AddParameter(cmd, "iPageSize", DbType.Int32, iPageSize);
            DataTable table = CreateDataTable(cmd);
            return table;
        }

        public DataTable vc_NewsThread_SelectList_Count(String sWhere) {
            IDbCommand cmd = _db.CreateCommand("CMS_NewsThread_SelectList_Count", true);
            _db.AddParameter(cmd, "sWhere", DbType.String, sWhere);
            DataTable table = CreateDataTable(cmd);
            return table;
        }

        public DataTable vc_ThreadDetails_SelectList(String sWhere, Int32 iStartIndex, Int32 iPageSize) {
            IDbCommand cmd = _db.CreateCommand("CMS_ThreadDetails_SelectList", true);
            _db.AddParameter(cmd, "sWhere", DbType.String, sWhere);
            _db.AddParameter(cmd, "iStartIndex", DbType.Int32, iStartIndex);
            _db.AddParameter(cmd, "iPageSize", DbType.Int32, iPageSize);
            DataTable table = CreateDataTable(cmd);
            return table;
        }

          
        public DataTable CountAllComment() {
            IDbCommand cmd = _db.CreateCommand("CMS_CountAllComment", true);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable CountComment() {
            IDbCommand cmd = _db.CreateCommand("CountComment", true);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

             
        public DataTable vc_NewsThread_CheckExistingTitle(String Title, Int32 Thread_ID) {
            IDbCommand cmd = _db.CreateCommand("CMS_NewsThread_CheckExistingTitle", true);
            _db.AddParameter(cmd, "sTitle", DbType.String, Title);
            _db.AddParameter(cmd, "iThread_ID", DbType.Int32, Thread_ID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

         

        public DataTable AutoSave_Insert(long NewsID, int CatID, string NewsTitle, string NewsImage, string NewsInitContent, string NewsContent, DateTime CreateDate) {
            IDbCommand cmd = _db.CreateCommand("AutoSave_Insert", true);
            _db.AddParameter(cmd, "NewsID", DbType.Int64, NewsID);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, CatID);
            _db.AddParameter(cmd, "News_Title", DbType.String, NewsTitle);
            _db.AddParameter(cmd, "News_Image", DbType.String, NewsImage);
            _db.AddParameter(cmd, "News_InitContent", DbType.String, NewsInitContent);
            _db.AddParameter(cmd, "News_Content", DbType.String, NewsContent);
            _db.AddParameter(cmd, "CreateDate", DbType.DateTime, CreateDate);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        
        public DataTable GetAllTacGia() {
            IDbCommand cmd = _db.CreateCommand("CMS_GetAllTacGia", true);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable GetAllEditionType()
        {
            IDbCommand cmd = _db.CreateCommand("CMS_GetAllEditionType", true);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

          
        public DataTable vf_SelectThreadByNewsID(Int64 NewsID) {
            IDbCommand cmd = _db.CreateCommand("CMS_SelectThreadByNewsID", true);
            _db.AddParameter(cmd, "NewsID", DbType.Int64, NewsID);
            DataTable dt = CreateDataTable(cmd);
            return dt;
        }
         
   
        public DataTable cms_GetListCrawlerNews(int Top) {
            IDbCommand cmd = _db.CreateCommand("cms_GetListCrawlerNews", true);
            _db.AddParameter(cmd, "Top", DbType.Int32, Top);
            DataTable table = CreateDataTable(cmd);
            return table;
        }

        public DataTable cms_UpdateNewsAttachmentFileType(Int64 NewsID, string Types) {
            IDbCommand cmd = _db.CreateCommand("cms_UpdateNewsAttachmentFileType", true);
            _db.AddParameter(cmd, "NewsID", DbType.Int64, NewsID);
            _db.AddParameter(cmd, "Types", DbType.String, Types);
            DataTable table = CreateDataTable(cmd);
            return table;
        }

        public DataTable cms_GetAttachmentsType(Int64 NewsID) {
            IDbCommand cmd = _db.CreateCommand("cms_GetAttachmentsType", true);
            _db.AddParameter(cmd, "NewsID", DbType.Int64, NewsID);
            DataTable table = CreateDataTable(cmd);
            return table;
        }

        public  DataTable SelectAllNewsPublished(int StartIndex,int PageSize)
        {
            IDbCommand cmd = _db.CreateCommand("CMS_NewsPublished_SelectAll", true);
            _db.AddParameter(cmd, "StartIndex", DbType.Int64, StartIndex);
            _db.AddParameter(cmd, "PageSize", DbType.Int64, PageSize);
            DataTable table = CreateDataTable(cmd);
            return table;
        }

        public DataTable vc_ThreadDetail_CheckExistingNews_ID_Thread_ID(String News_ID, Int32 Thread_ID)
        {
            IDbCommand cmd = _db.CreateCommand("CMS_ThreadDetail_CheckExistingNews_ID_Thread_ID", true);
            _db.AddParameter(cmd, "sNews_ID", DbType.String, News_ID);
            _db.AddParameter(cmd, "iThread_ID", DbType.Int32, Thread_ID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable vc_News_SelectListNewsByRangeNewsId(String sWhere)
        {
            IDbCommand cmd = _db.CreateCommand("CMS_News_SelectListNewsByRangeNewsId", true);
            _db.AddParameter(cmd, "sWhere", DbType.String, sWhere);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable vc_Execute_Sql(String sSql)
        {
            IDbCommand cmd = _db.CreateCommand("vc_Execute_Sql", true);
            _db.AddParameter(cmd, "sSql", DbType.String, sSql);
            DataTable table = CreateDataTable(cmd);
            return table;
        }

        
        public DataTable SelectAllNewsPublishedV2(string StartIndex, string PageSize, string Key, string Cat_ID)
        {
            IDbCommand cmd = _db.CreateCommand("SelectAllNewsPublishedV2", true);
            _db.AddParameter(cmd, "StartIndex", DbType.String, StartIndex);
            _db.AddParameter(cmd, "PageSize", DbType.String, PageSize);
            _db.AddParameter(cmd, "Key", DbType.String, Key);
            _db.AddParameter(cmd, "Cat_ID", DbType.String, Cat_ID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable vc_GetCatPath(Int32 CatID)
        {
            IDbCommand cmd = _db.CreateCommand("GetCatPath", true);
            _db.AddParameter(cmd, "cat_id", DbType.Int32, CatID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }


        public bool Category_Delete(int catId)
        {
            IDbCommand cmd = _db.CreateCommand("CMS_Category_Delete", true);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, catId);
            return cmd.ExecuteNonQuery() == 1;
        }


        public DataTable vc_MediaObject_SelectList(String sWhere, Int32 iStartIndex, Int32 iPageSize)
        {
            IDbCommand cmd = _db.CreateCommand("vc_MediaObject_SelectList", true);
            _db.AddParameter(cmd, "sWhere", DbType.String, sWhere);
            _db.AddParameter(cmd, "iStartIndex", DbType.Int32, iStartIndex);
            _db.AddParameter(cmd, "iPageSize", DbType.Int32, iPageSize);
            DataTable table = CreateDataTable(cmd);
            return table;
        }

        public DataTable vc_MediaObject_SelectList_Count(String sWhere)
        {
            IDbCommand cmd = _db.CreateCommand("vc_MediaObject_SelectList_Count", true);
            _db.AddParameter(cmd, "sWhere", DbType.String, sWhere);
            DataTable table = CreateDataTable(cmd);
            return table;
        }

        public DataTable vc_EditionType_SelectOne(Int32 EditionType_ID)
        {
            IDbCommand cmd = _db.CreateCommand("CMS_EditionType_SelectOne", true);
            _db.AddParameter(cmd, "iEditionType_ID", DbType.Int32, EditionType_ID);
            DataTable table = CreateDataTable(cmd);

            return table;
        }

        public DataTable vc_EditionType_SelectAll()
        {
            IDbCommand cmd = _db.CreateCommand("CMS_GetAllEditionType", true);
            DataTable table = CreateDataTable(cmd);

            return table;
        }

        public DataTable News_GetListNewNumRow(string Where, string cpmode, string Reciver_ID, string Sender_ID)
        {
            IDbCommand cmd = _db.CreateCommand("CMS_News_GetListNewNumRows", true);
            _db.AddParameter(cmd, "Where", DbType.String, Where);
            _db.AddParameter(cmd, "cpmode", DbType.String, cpmode);
            _db.AddParameter(cmd, "Reciver_ID", DbType.String, Reciver_ID);
            _db.AddParameter(cmd, "Sender_ID", DbType.String, Sender_ID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable News_GetListNew(string Where, string cpmode, string Reciver_ID, string Sender_ID, string StartIndex, string PageSize, string SortExpression)
        {
            IDbCommand cmd = _db.CreateCommand("CMS_News_GetListNew", true);
            _db.AddParameter(cmd, "Where", DbType.String, Where);
            _db.AddParameter(cmd, "cpmode", DbType.String, cpmode);
            _db.AddParameter(cmd, "Reciver_ID", DbType.String, Reciver_ID);
            _db.AddParameter(cmd, "Sender_ID", DbType.String, Sender_ID);
            _db.AddParameter(cmd, "StartIndex", DbType.String, StartIndex);
            _db.AddParameter(cmd, "PageSize", DbType.String, PageSize);
            _db.AddParameter(cmd, "SortExpression", DbType.String, SortExpression);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable News_GetListNewMyPublished(string Where, string cpmode, string Reciver_ID, string Sender_ID, string StartIndex, string PageSize, string News_Approver, string SortExpression)
        {
            IDbCommand cmd = _db.CreateCommand("News_GetListNewMyPublished", true);
            _db.AddParameter(cmd, "Where", DbType.String, Where);
            _db.AddParameter(cmd, "cpmode", DbType.String, cpmode);
            _db.AddParameter(cmd, "Reciver_ID", DbType.String, Reciver_ID);
            _db.AddParameter(cmd, "Sender_ID", DbType.String, Sender_ID);
            _db.AddParameter(cmd, "StartIndex", DbType.String, StartIndex);
            _db.AddParameter(cmd, "PageSize", DbType.String, PageSize);
            _db.AddParameter(cmd, "News_Approver", DbType.String, News_Approver);
            _db.AddParameter(cmd, "SortExpression", DbType.String, SortExpression);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable News_GetAllNewsTemplistCount(string strWhere)
        {
            IDbCommand cmd = _db.CreateCommand("News_GetAllNewsTemplistCount", true);
            _db.AddParameter(cmd, "strWhere", DbType.String, strWhere);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable News_GetAllNewsTemplist(string strWhere, string StartIndex, string EndIndex)
        {
            IDbCommand cmd = _db.CreateCommand("News_GetAllNewsTemplist", true);
            _db.AddParameter(cmd, "strWhere", DbType.String, strWhere);
            _db.AddParameter(cmd, "StartIndex", DbType.String, StartIndex);
            _db.AddParameter(cmd, "EndIndex", DbType.String, EndIndex);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable CMS_GetListWapNews(int Top)
        {
            IDbCommand cmd = _db.CreateCommand("CMS_GetListWapNews", true);
            _db.AddParameter(cmd, "Top", DbType.Int32, Top);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable SaveWapContent(long NewsID, string NewsContent, int NewsStatus)
        {
            IDbCommand cmd = _db.CreateCommand("CMS_SaveWapContent", true);
            _db.AddParameter(cmd, "NewsID", DbType.Int64, NewsID);
            _db.AddParameter(cmd, "NewsContent", DbType.String, NewsContent);
            _db.AddParameter(cmd, "NewsStatus", DbType.Int32, NewsStatus);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable UpdateWapContent(long NewsID, string NewsContent)
        {
            IDbCommand cmd = _db.CreateCommand("CMS_UpdateWapContent", true);
            _db.AddParameter(cmd, "NewsID", DbType.Int64, NewsID);
            _db.AddParameter(cmd, "NewsContent", DbType.String, NewsContent);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }


        //ho tro truc tuyen
        public DataTable InsertSupportOnline(string FullName, string Yahoo, string Skype, string Mobile, string GroupName, int STT)
        {
            IDbCommand cmd = _db.CreateCommand("InsertSupportOnline", true);
            _db.AddParameter(cmd, "FullName", DbType.String, FullName);
            _db.AddParameter(cmd, "Yahoo", DbType.String, Yahoo);
            _db.AddParameter(cmd, "Skype", DbType.String, Skype);
            _db.AddParameter(cmd, "Mobile", DbType.String, Mobile);
            _db.AddParameter(cmd, "GroupName", DbType.String, GroupName);
            _db.AddParameter(cmd, "STT", DbType.Int32, STT);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable UpdateSupportOnline(int ID, string FullName, string Yahoo, string Skype, string Mobile, string GroupName, int STT)
        {
            IDbCommand cmd = _db.CreateCommand("UpdateSupportOnline", true);
            _db.AddParameter(cmd, "ID", DbType.Int32, ID);
            _db.AddParameter(cmd, "FullName", DbType.String, FullName);
            _db.AddParameter(cmd, "Yahoo", DbType.String, Yahoo);
            _db.AddParameter(cmd, "Skype", DbType.String, Skype);
            _db.AddParameter(cmd, "Mobile", DbType.String, Mobile);
            _db.AddParameter(cmd, "GroupName", DbType.String, GroupName);
            _db.AddParameter(cmd, "STT", DbType.Int32, STT);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable DeleteSupportOnline(string ID)
        {
            IDbCommand cmd = _db.CreateCommand("DeleteSupportOnline", true);
            _db.AddParameter(cmd, "ID", DbType.String, ID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable SetActiveSupportOnline(string ID)
        {
            IDbCommand cmd = _db.CreateCommand("SetActiveSupportOnline", true);
            _db.AddParameter(cmd, "ID", DbType.String, ID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable SetUnActiveSupportOnline(string ID)
        {
            IDbCommand cmd = _db.CreateCommand("SetUnActiveSupportOnline", true);
            _db.AddParameter(cmd, "ID", DbType.String, ID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public void tdt_DeleteOrderDetailItem(long O_id, int P_ID)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_DeleteOrderDetailItem", true);
            _db.AddParameter(cmd, "O_id", DbType.Int64, O_id);
            _db.AddParameter(cmd, "P_ID", DbType.Int32, P_ID);
            CreateDataTable(cmd);
        }
        public void tdt_updateTotalOrder(long O_id, string o_Total)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_updateTotalOrder", true);
            _db.AddParameter(cmd, "O_id", DbType.Int64, O_id);
            _db.AddParameter(cmd, "o_Total", DbType.String, o_Total);
            CreateDataTable(cmd);
        }
        /// <summary>

        /// </summary>
        /// <param name="O_id"></param>
        /// <returns>Xac nhan don hang dc thanh toan</returns>
        public DataTable XacNhanThanhToan(Int64 O_id)
        {
            IDbCommand cmd = _db.CreateCommand("XacNhanThanhToan", true);
            _db.AddParameter(cmd, "O_id", DbType.Int64, O_id);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>

        /// </summary>
        /// <param name="O_id"></param>
        /// <returns>Thong tin co ban cua 1 don hang</returns>
        public DataTable SelecttblOrder(Int64 O_id)
        {
            IDbCommand cmd = _db.CreateCommand("SelecttblOrder", true);
            _db.AddParameter(cmd, "O_id", DbType.Int64, O_id);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Danh sach cac don hang da thanh toan
        /// </summary>
        /// <returns></returns>
        public DataTable SelectOrderIsPaid()
        {
            IDbCommand cmd = _db.CreateCommand("SelectOrderIsPaid", true);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <returns>Danh sach nhung don hang trong han thanh toan</returns>
        public DataTable SelectOrderInReQuiredDate()
        {
            IDbCommand cmd = _db.CreateCommand("SelectOrderInReQuiredDate", true);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <returns>Danh sach nhung don hang qua han thanh toan</returns>
        public DataTable SelectOrderOverReQuiredDate()
        {
            IDbCommand cmd = _db.CreateCommand("SelectOrderOverReQuiredDate", true);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="O_id"></param>
        /// <returns>Chuyen don hang sang trang thai cho thanh toan</returns>
        public DataTable UpdateIsRemovetblOrder(String O_id)
        {
            IDbCommand cmd = _db.CreateCommand("UpdateIsRemovetblOrder", true);
            _db.AddParameter(cmd, "O_id", DbType.String, O_id);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="O_id"></param>
        /// <returns>Xoa don hang</returns>
        public DataTable DeletetblOrder(String O_id)
        {
            IDbCommand cmd = _db.CreateCommand("DeletetblOrder", true);
            _db.AddParameter(cmd, "O_id", DbType.String, O_id);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns>Chi Tiet 1 don hang</returns>
        public DataTable GetOrderDetailByOrderID(Int64 OrderID)
        {
            IDbCommand cmd = _db.CreateCommand("GetOrderDetailByOrderID", true);
            _db.AddParameter(cmd, "OrderID", DbType.Int64, OrderID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <returns>Danh sach cac don hang moi dc gui</returns>
        public DataTable SelectNewOrder()
        {
            IDbCommand cmd = _db.CreateCommand("SelectNewOrder", true);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="P_Price"></param>
        /// <param name="P_quantity"></param>
        /// <param name="P_id"></param>
        /// <param name="O_ID"></param>
        /// <returns></returns>
        public DataTable InserttblOrderDetail(String P_Price, Int32 P_quantity, Int32 P_id, Int64 O_ID)
        {
            IDbCommand cmd = _db.CreateCommand("InserttblOrderDetail", true);
            _db.AddParameter(cmd, "P_Price", DbType.String, P_Price);
            _db.AddParameter(cmd, "P_quantity", DbType.Int32, P_quantity);
            _db.AddParameter(cmd, "P_id", DbType.Int32, P_id);
            _db.AddParameter(cmd, "O_ID", DbType.Int64, O_ID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="C_password"></param>
        /// <param name="C_phone"></param>
        /// <param name="C_address"></param>
        /// <param name="C_job"></param>
        /// <param name="C_fullname"></param>
        /// <param name="C_email"></param>
        /// <returns>Tra ra ID cua khach hang vua dc insert vao`</returns>
        public DataTable InserttblCustomer(String C_password, String C_phone, String C_address, String C_job, String C_fullname, String C_email)
        {
            IDbCommand cmd = _db.CreateCommand("InserttblCustomer", true);
            _db.AddParameter(cmd, "C_password", DbType.String, C_password);
            _db.AddParameter(cmd, "C_phone", DbType.String, C_phone);
            _db.AddParameter(cmd, "C_address", DbType.String, C_address);
            _db.AddParameter(cmd, "C_job", DbType.String, C_job);
            _db.AddParameter(cmd, "C_fullname", DbType.String, C_fullname);
            _db.AddParameter(cmd, "C_email", DbType.String, C_email);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="O_ID"></param>
        /// <param name="O_date"></param>
        /// <param name="O_status"></param>
        /// <param name="O_suggest"></param>
        /// <param name="C_id"></param>
        /// <param name="O_Isremove"></param>
        /// <param name="O_total"></param>
        /// <param name="O_RequiredDate"></param>
        /// <returns>tra ra ma so don hang vua dc insert vao</returns>
        public DataTable InserttblOrder(Int64 O_ID, DateTime O_date, Boolean O_status, String O_suggest, Int32 C_id, Boolean O_Isremove, String O_total, DateTime O_RequiredDate)
        {
            IDbCommand cmd = _db.CreateCommand("InserttblOrder", true);
            _db.AddParameter(cmd, "O_ID", DbType.Int64, O_ID);
            _db.AddParameter(cmd, "O_date", DbType.DateTime, O_date);
            _db.AddParameter(cmd, "O_status", DbType.Boolean, O_status);
            _db.AddParameter(cmd, "O_suggest", DbType.String, O_suggest);
            _db.AddParameter(cmd, "C_id", DbType.Int32, C_id);
            _db.AddParameter(cmd, "O_Isremove", DbType.Boolean, O_Isremove);
            _db.AddParameter(cmd, "O_total", DbType.String, O_total);
            _db.AddParameter(cmd, "O_RequiredDate", DbType.DateTime, O_RequiredDate);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

#region Thống kê
        public DataTable tdt_ReportNewsByComment(DateTime fromdate, DateTime todate)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_ReportNewsByComment", true);
            _db.AddParameter(cmd, "fromdate", DbType.DateTime, fromdate);
            _db.AddParameter(cmd, "todate", DbType.DateTime, todate);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable tdt_ReportNewsByCat(string fromdate, string todate)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_ReportNewsByCat", true);
            _db.AddParameter(cmd, "fromdate", DbType.DateTime, fromdate);
            _db.AddParameter(cmd, "todate", DbType.DateTime, todate);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable tdt_ReportPageViewByCat(DateTime fromdate, DateTime todate)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_ReportPageViewByCat", true);
            _db.AddParameter(cmd, "fromdate", DbType.DateTime, fromdate);
            _db.AddParameter(cmd, "todate", DbType.DateTime, todate);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable tdt_ReportBaiXemNhieuNhat(string fromdate, string todate, int Cat_ID, int Top)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_ReportBaiXemNhieuNhat", true);
            _db.AddParameter(cmd, "fromdate", DbType.String, fromdate);
            _db.AddParameter(cmd, "todate", DbType.String, todate);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            _db.AddParameter(cmd, "Top", DbType.Int32, Top);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable tdt_ReportBaiTheoTacGia(string fromdate, string todate, int Cat_ID, int Top)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_ReportBaiTheoTacGia", true);
            _db.AddParameter(cmd, "fromdate", DbType.String, fromdate);
            _db.AddParameter(cmd, "todate", DbType.String, todate);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            _db.AddParameter(cmd, "Top", DbType.Int32, Top);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable tdt_ReportTheoTungBai(string fromdate, string todate, string userName)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_ReportChiTietTungBai", true);
            _db.AddParameter(cmd, "fromdate", DbType.String, fromdate);
            _db.AddParameter(cmd, "todate", DbType.String, todate);
            _db.AddParameter(cmd, "User", DbType.String, userName);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable tdt_ReportTheoTungBaiTheoChuyenMuc(string fromdate, string todate, int cateID, int sortOrder)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_ReportChiTietBaiTheoChuyenMuc", true);
            _db.AddParameter(cmd, "fromdate", DbType.String, fromdate);
            _db.AddParameter(cmd, "todate", DbType.String, todate);
            _db.AddParameter(cmd, "Cat_ID ", DbType.Int16, cateID);
            _db.AddParameter(cmd, "SortOrder", DbType.Int16, sortOrder);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable tdt_ReportBaiTheoChiTietTacGia(string fromdate, string todate, int Cat_ID, int Top, string user, int check, int dateCount)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_ReportBaiTheoChiTietTacGia", true);
            _db.AddParameter(cmd, "fromdate", DbType.String, fromdate);
            _db.AddParameter(cmd, "todate", DbType.String, todate);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            _db.AddParameter(cmd, "Top", DbType.Int32, Top);
            _db.AddParameter(cmd, "User", DbType.String, user);
            _db.AddParameter(cmd, "date", DbType.Int32, check);
            _db.AddParameter(cmd, "dateCount", DbType.Int32, dateCount);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable tdt_ReportBaiTheoChuyenMuc(string fromdate, string todate, int Cat_ID, int Top)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_ReportBaiTheoChuyenMucPhanCap", true);
            _db.AddParameter(cmd, "fromdate", DbType.String, fromdate);
            _db.AddParameter(cmd, "todate", DbType.String, todate);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable tdt_ReportBaiTheoChuyenMucDeQuy(string fromdate, string todate, int Cat_ID, int ParentID)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_ReportBaiTheoChuyenMucDeQuy", true);
            _db.AddParameter(cmd, "fromdate", DbType.String, fromdate);
            _db.AddParameter(cmd, "todate", DbType.String, todate);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            _db.AddParameter(cmd, "parentID", DbType.Int32, ParentID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable tdt_ReportBaiTheoGioXuatBan(string fromdate, string todate, string user, int dateCount, int cateID)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_ReportBaiTheoGioXuatBan", true);
            _db.AddParameter(cmd, "fromdate", DbType.String, fromdate);
            _db.AddParameter(cmd, "todate", DbType.String, todate);
            _db.AddParameter(cmd, "User", DbType.String, user);
            _db.AddParameter(cmd, "dateCount", DbType.Int32, dateCount);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, cateID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable tdt_ReportBaiTheoGioTao(string fromdate, string todate, string user, int dateCount, int cateID)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_ReportBaiTheoGioTao", true);
            _db.AddParameter(cmd, "fromdate", DbType.String, fromdate);
            _db.AddParameter(cmd, "todate", DbType.String, todate);
            _db.AddParameter(cmd, "User", DbType.String, user);
            _db.AddParameter(cmd, "dateCount", DbType.Int32, dateCount);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, cateID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable tdt_ReportBaiTheoGioXuatBanTheoChuyeMuc(string fromdate, string todate, int Cat_ID, int SortOrder, int dateCount)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_ReportBaiTheoGioXuatBanTheoChuyenMuc", true);
            _db.AddParameter(cmd, "fromdate", DbType.String, fromdate);
            _db.AddParameter(cmd, "todate", DbType.String, todate);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            _db.AddParameter(cmd, "SortOrder", DbType.Int32, SortOrder);
            _db.AddParameter(cmd, "dateCount", DbType.Int32, dateCount);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable tdt_ReportBaiTheoGioXuatBanTheoChuyeMucTongThe(string fromdate, string todate, int Cat_ID, int SortOrder, int dateCount)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_ReportBaiTheoGioXuatBanTheoChuyenMucTongThe", true);
            _db.AddParameter(cmd, "fromdate", DbType.String, fromdate);
            _db.AddParameter(cmd, "todate", DbType.String, todate);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            _db.AddParameter(cmd, "SortOrder", DbType.Int32, SortOrder);
            _db.AddParameter(cmd, "dateCount", DbType.Int32, dateCount);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable tdt_ReportNewsPageView(string fromdate, string todate, int Cat_ID, int Extension)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_ReportNewsPageView", true);
            _db.AddParameter(cmd, "fromdate", DbType.String, fromdate);
            _db.AddParameter(cmd, "todate", DbType.String, todate);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            _db.AddParameter(cmd, "Extension", DbType.Int32, Extension);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable tdt_ReportNews(string fromdate, string todate, int Cat_ID, int Extension)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_ReportNews", true);
            _db.AddParameter(cmd, "fromdate", DbType.String, fromdate);
            _db.AddParameter(cmd, "todate", DbType.String, todate);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            _db.AddParameter(cmd, "Extension", DbType.Int32, Extension);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable ThongKeLuongBaiViet(DateTime fromdate, DateTime todate)
        {
            IDbCommand cmd = this._db.CreateCommand("ThongKeLuongBaiTheoUser", true);
            this._db.AddParameter(cmd, "fromdate", DbType.DateTime, fromdate);
            this._db.AddParameter(cmd, "todate", DbType.DateTime, todate);
            return this._db.CreateDataTable(cmd);
        }
#endregion

        public DataTable vc_SelectVote_Assign__NewsId_ByVoteId(int Vote_ID)
        {
            IDbCommand cmd = _db.CreateCommand("vc_SelectVote_Assign__NewsId_ByVoteId", true);
            _db.AddParameter(cmd, "Vote_ID", DbType.Int32, Vote_ID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable vc_DeleteVote_Assigns_News_ByVote_ID(Int32 Vote_ID)
        {
            IDbCommand cmd = _db.CreateCommand("vc_DeleteVote_Assigns_News_ByVote_ID", true);
            _db.AddParameter(cmd, "Vote_ID", DbType.Int32, Vote_ID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable vc_InsertVote_Assign(String News_ID, Int32 Vote_ID, String Vote_GenTag, Int32 Cat_ID)
        {
            IDbCommand cmd = _db.CreateCommand("vc_InsertVote_Assign", true);
            _db.AddParameter(cmd, "News_ID", DbType.String, News_ID);
            _db.AddParameter(cmd, "Vote_ID", DbType.Int32, Vote_ID);
            _db.AddParameter(cmd, "Vote_GenTag", DbType.String, Vote_GenTag);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable vc_Vote_SelectList_Where(String sWhere)
        {
            IDbCommand cmd = _db.CreateCommand("vc_Vote_SelectList_Where", true);
            _db.AddParameter(cmd, "sWhere", DbType.String, sWhere);
            DataTable table = CreateDataTable(cmd);
            return table;
        }

        public DataTable SetupVote_GetSetupVoteById(int voteid)
        {
            IDbCommand cmd = _db.CreateCommand("Vote_GetSetupVoteById", true);
            _db.AddParameter(cmd, "Vote_ID", DbType.Int32, voteid);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable SetupVote_Insert(int vote_id, string SetupVote_TitleColor, string SetupVote_FontSize, string SetupVote_BorderColor, string SetupVote_BackgroundColor, string GenCode, string Width, string Height)
        {
            IDbCommand cmd = _db.CreateCommand("SetupVote_Insert", true);
            _db.AddParameter(cmd, "Vote_ID", DbType.Int32, vote_id);
            _db.AddParameter(cmd, "SetupVote_TitleColor", DbType.String, SetupVote_TitleColor);
            _db.AddParameter(cmd, "SetupVote_FontSize", DbType.String, SetupVote_FontSize);
            _db.AddParameter(cmd, "SetupVote_BackgroundColor", DbType.String, SetupVote_BackgroundColor);
            _db.AddParameter(cmd, "SetupVote_GenCode", DbType.String, GenCode);
            _db.AddParameter(cmd, "SetupVote_Width", DbType.String, Width);
            _db.AddParameter(cmd, "SetupVote_Height", DbType.String, Height);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable SetupVote_Update(int vote_id, string SetupVote_TitleColor, string SetupVote_FontSize, string SetupVote_BorderColor, string SetupVote_BackgroundColor, string GenCode, string Width, string Height)
        {
            IDbCommand cmd = _db.CreateCommand("SetupVote_Update", true);
            _db.AddParameter(cmd, "Vote_ID", DbType.Int32, vote_id);
            _db.AddParameter(cmd, "SetupVote_TitleColor", DbType.String, SetupVote_TitleColor);
            _db.AddParameter(cmd, "SetupVote_FontSize", DbType.String, SetupVote_FontSize);
            _db.AddParameter(cmd, "SetupVote_BackgroundColor", DbType.String, SetupVote_BackgroundColor);
            _db.AddParameter(cmd, "SetupVote_GenCode", DbType.String, GenCode);
            _db.AddParameter(cmd, "SetupVote_Width", DbType.String, Width);
            _db.AddParameter(cmd, "SetupVote_Height", DbType.String, Height);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable vc_Vote_SelectList(String sWhere, Int32 iStartIndex, Int32 iPageSize)
        {
            IDbCommand cmd = _db.CreateCommand("vc_Vote_SelectList", true);
            _db.AddParameter(cmd, "sWhere", DbType.String, sWhere);
            _db.AddParameter(cmd, "iStartIndex", DbType.Int32, iStartIndex);
            _db.AddParameter(cmd, "iPageSize", DbType.Int32, iPageSize);
            DataTable table = CreateDataTable(cmd);
            return table;
        }

        public DataTable vc_Vote_SelectList_Count(String sWhere)
        {
            IDbCommand cmd = _db.CreateCommand("vc_Vote_SelectList_Count", true);
            _db.AddParameter(cmd, "sWhere", DbType.String, sWhere);
            DataTable table = CreateDataTable(cmd);
            return table;
        }
    }
}