using System;
using System.Data;
using DAL;

namespace DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class StoredProcedures
    {
        private MainDB _db;

        public StoredProcedures(MainDB db)
        {
            _db = db;
        }

        public MainDB Database
        {
            get { return _db; }
        }

        public DataTable Web_SelectSiteInformation(int ID)
        {
            IDbCommand cmd = _db.CreateCommand("proc_SiteInformationSelect", true);
            _db.AddParameter(cmd, "Id", DbType.Int32, ID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable Web_SelectSupportOnline()
        {
            IDbCommand cmd = _db.CreateCommand("Web_SelectSupportOnline", true);
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
        #region Advertisement
        public DataTable Advertisement_GetById(int catId, int posId)
        {
            IDbCommand cmd = _db.CreateCommand("adv_Get_AdvByPageAndPositionsV2", true);
            _db.AddParameter(cmd, "CatId", DbType.Int32, catId);
            _db.AddParameter(cmd, "PosId", DbType.Int32, posId);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable CMS_Advertisments_SelectAllLike(string keyword)
        {
            IDbCommand cmd = _db.CreateCommand("CMS_Advertisments_SelectAllLike", true);
            _db.AddParameter(cmd, "keyword", DbType.String, keyword);
            
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable CMS_Advertisments_SelectOne(int AdvID)
        {
            IDbCommand cmd = _db.CreateCommand("CMS_Advertisments_SelectOne", true);
            _db.AddParameter(cmd, "AdvID", DbType.String, AdvID);

            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        #endregion

        public DataTable DangKyMuaHang_Insert(string CusName, string CusAddress, string CusMobile, string CusEmail, int ProductId)
        {
            IDbCommand cmd = _db.CreateCommand("DangKyMuaHang_Insert", true);
            _db.AddParameter(cmd, "CusName", DbType.String, CusName);
            _db.AddParameter(cmd, "CusAddress", DbType.String, CusAddress);
            _db.AddParameter(cmd, "CusMobile", DbType.String, CusMobile);
            _db.AddParameter(cmd, "CusEmail", DbType.String, CusEmail);
            _db.AddParameter(cmd, "ProductId", DbType.Int32, ProductId);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable Web_NewsPublished_GetFocus(int iTop)
        {
            IDbCommand cmd = _db.CreateCommand("Web_NewsPublished_GetFocus", true);
            _db.AddParameter(cmd, "iTop", DbType.Int32, iTop);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable Web_MediaObject_GetAllItem_By_News_ID(long News_ID)
        {
            IDbCommand cmd = _db.CreateCommand("Web_MediaObject_GetAllItem_By_News_ID", true);
            _db.AddParameter(cmd, "News_ID", DbType.Int64, News_ID);

            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataSet Web_GetDetail(long News_Id)
        {

            IDbCommand cmd = _db.CreateCommand("Web_GetDetail", true);
            _db.AddParameter(cmd, "News_Id", DbType.Int64, News_Id);
            DataSet table = _db.CreateDataSet(cmd);
            return table;
        }
        public DataTable SEO_SelectByCatID(int Cat_ID)
        {
            IDbCommand cmd = _db.CreateCommand("SEO_SelectByCatID", true);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);

            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable SEO_Update(Int32 ID, int Cat_ID, string SEO_Title, string SEO_Desscription, string SEO_Keyword)
        {
            IDbCommand cmd = _db.CreateCommand("SEO_Update", true);
            _db.AddParameter(cmd, "ID", DbType.Int32, ID);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            _db.AddParameter(cmd, "SEO_Title", DbType.String, SEO_Title);
            _db.AddParameter(cmd, "SEO_Desscription", DbType.String, SEO_Desscription);
            _db.AddParameter(cmd, "SEO_Keyword", DbType.String, SEO_Keyword);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable Microf_DanhSachTin_Count(int catId)
        {
            IDbCommand cmd = _db.CreateCommand("Microf_DanhSachTin_Count", true);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int64, catId);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable Web_DanhSachTin_Count(int catId)
        {
            IDbCommand cmd = _db.CreateCommand("Web_DanhSachTin_Count", true);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int64, catId);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable Web_SearchNews(string Key, int pageSize, int pageIndex)
        {
            IDbCommand cmd = _db.CreateCommand("Web_SearchNews", true);
            _db.AddParameter(cmd, "Key", DbType.String, Key);
            _db.AddParameter(cmd, "pageSize", DbType.Int32, pageSize);
            _db.AddParameter(cmd, "pageIndex", DbType.Int32, pageIndex);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable Web_SearchNews_Count(string Key)
        {
            IDbCommand cmd = _db.CreateCommand("Web_SearchNews", true);
            _db.AddParameter(cmd, "Key", DbType.String, Key);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable Web_DanhSachTin(int catId, int pageSize, int pageIndex)
        {
            IDbCommand cmd = _db.CreateCommand("Web_DanhSachTin", true);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, catId);
            _db.AddParameter(cmd, "pageSize", DbType.Int32, pageSize);
            _db.AddParameter(cmd, "pageIndex", DbType.Int32, pageIndex);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable Microf_DanhSachTin(int catId, int pageSize, int pageIndex)
        {
            IDbCommand cmd = _db.CreateCommand("Microf_DanhSachTin", true);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, catId);
            _db.AddParameter(cmd, "pageSize", DbType.Int32, pageSize);
            _db.AddParameter(cmd, "pageIndex", DbType.Int32, pageIndex);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable Microf_LatestNewsByCat(int Top, int Mode,string Cat_ID)
        {
            IDbCommand cmd = _db.CreateCommand("Microf_LatestNewsByCat", true);
            _db.AddParameter(cmd, "Top", DbType.Int32, Top);
            _db.AddParameter(cmd, "Mode", DbType.Int32, Mode);
            _db.AddParameter(cmd, "Cat_ID", DbType.String, Cat_ID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable Web_GetLastestGallery()
        {
            IDbCommand cmd = _db.CreateCommand("Web_GetLastestGallery", true);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable Web_GetTopLastestGallery(int Top)
        {
            IDbCommand cmd = _db.CreateCommand("Web_GetTopLastestGallery", true);
            _db.AddParameter(cmd, "Top", DbType.Int32, Top);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable Web_GetImageByGalleryID(int GalleryId, int Top)
        {
            IDbCommand cmd = _db.CreateCommand("Web_GetImageByGalleryID", true);
            _db.AddParameter(cmd, "GalleryId", DbType.Int32, GalleryId);
            _db.AddParameter(cmd, "Top", DbType.Int32, Top);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable Web_ListProductCatCount()
        {
            IDbCommand cmd = _db.CreateCommand("Web_ListProductCatCount", true);
           
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable Web_ListProductCatPagging(int pageIndex, int pageSize)
        {
            IDbCommand cmd = _db.CreateCommand("Web_ListProductCatPagging", true);
           
            _db.AddParameter(cmd, "pageIndex", DbType.Int32, pageIndex);
            _db.AddParameter(cmd, "pageSize", DbType.Int32, pageSize);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable Web_SearchProducts(string key, int pageIndex, int pageSize)
        {
            IDbCommand cmd = _db.CreateCommand("Web_SearchProducts", true);
            _db.AddParameter(cmd, "key", DbType.String, key);
            _db.AddParameter(cmd, "pageIndex", DbType.Int32, pageIndex);
            _db.AddParameter(cmd, "pageSize", DbType.Int32, pageSize);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable tdt_SearchFullText(string key, int pageIndex, int pageSize)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_SearchFullText", true);
            _db.AddParameter(cmd, "key", DbType.String, key);
            _db.AddParameter(cmd, "pageIndex", DbType.Int32, pageIndex);
            _db.AddParameter(cmd, "pageSize", DbType.Int32, pageSize);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable HG_insertFeedBack(string Name, string GioiTinh, string Tel, string Email, string Address, string Title, string Content)
        {
            IDbCommand cmd = _db.CreateCommand("HG_insertFeedBack", true);
            _db.AddParameter(cmd, "Name", DbType.String, Name);
            _db.AddParameter(cmd, "GioiTinh", DbType.String, GioiTinh);
            _db.AddParameter(cmd, "Tel", DbType.String, Tel);
            _db.AddParameter(cmd, "Email", DbType.String, Email);
            _db.AddParameter(cmd, "Address", DbType.String, Address);
            _db.AddParameter(cmd, "Title", DbType.String, Title);
            _db.AddParameter(cmd, "Content", DbType.String, Content);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable InsertDangKyQuangTang(string fullname, string email, string address, string phone, string gift)
        {
            IDbCommand cmd = _db.CreateCommand("InsertDangKyQuangTang", true);
            _db.AddParameter(cmd, "fullname", DbType.String, fullname);
            _db.AddParameter(cmd, "email", DbType.String, email);
            _db.AddParameter(cmd, "address", DbType.String, address);
            _db.AddParameter(cmd, "phone", DbType.String, phone);
            _db.AddParameter(cmd, "gift", DbType.String, gift);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        
        public DataTable Web_GetCategoryByParent(int parentID, bool Cat_isColumn, int Edition_type,int Channel_ID)
        {
            IDbCommand cmd = _db.CreateCommand("Web_GetCategoryByParent", true);
            _db.AddParameter(cmd, "ParentID", DbType.Int32, parentID);
            _db.AddParameter(cmd, "Cat_isColumn", DbType.Boolean, Cat_isColumn);
            _db.AddParameter(cmd, "Edition_type", DbType.Int32, Edition_type);
           // _db.AddParameter(cmd, "Channel_ID", DbType.Int32, Channel_ID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable SelectAllNewsPublished(int StartIndex, int PageSize)
        {
            IDbCommand cmd = _db.CreateCommand("SelectAllNewsPublished", true);
            _db.AddParameter(cmd, "StartIndex", DbType.Int32, StartIndex);
            _db.AddParameter(cmd, "PageSize", DbType.Int32, PageSize);
            
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable Microf_LatestNews(int Top, int Mode, int Channel_ID)
        {
            IDbCommand cmd = _db.CreateCommand("Microf_LatestNews", true);

            _db.AddParameter(cmd, "Top", DbType.Int32, Top);
            _db.AddParameter(cmd, "Mode", DbType.Int32, Mode);
            _db.AddParameter(cmd, "Channel_ID", DbType.Int32, Channel_ID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable Microf_BonBanNoiBat(int Top)
        {
            IDbCommand cmd = _db.CreateCommand("Microf_BonBanNoiBat", true);
            _db.AddParameter(cmd, "Top", DbType.Int32, Top);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        protected DataTable CreateDataTable(IDbCommand command)
        {
            return _db.CreateDataTable(command);
        }
        public DataTable fn_GetItemByCategorySameDataSet(int catId, int top)
        {

            IDbCommand cmd = _db.CreateCommand("fn_GetItemByCategorySameDataSet", true);
            _db.AddParameter(cmd, "catId", DbType.Int32, catId);
            _db.AddParameter(cmd, "Top", DbType.Int32, top);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable Select_DanhSachTin(int catId, int pageSize, int pageIndex)
        {
            IDbCommand cmd = _db.CreateCommand("Select_DanhSachTin", true);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, catId);
            _db.AddParameter(cmd, "pageSize", DbType.Int32, pageSize);
            _db.AddParameter(cmd, "pageIndex", DbType.Int32, pageIndex);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable Web_Get_TinKhac(int Cat_ID, int Top, long News_Id)
        {

            IDbCommand cmd = _db.CreateCommand("Web_Get_TinKhac", true);
            _db.AddParameter(cmd, "News_Id", DbType.Int64, News_Id);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            _db.AddParameter(cmd, "Top", DbType.Int32, Top);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable Web_Get_TinMoiCapNhat(int Cat_ID, int Top, long News_Id)
        {

            IDbCommand cmd = _db.CreateCommand("Web_Get_TinMoiCapNhat", true);
            _db.AddParameter(cmd, "News_Id", DbType.Int64, News_Id);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            _db.AddParameter(cmd, "Top", DbType.Int32, Top);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable Web_BonBanNoiBat(int Top, int EditionType =1)
        {
            IDbCommand cmd = _db.CreateCommand("Web_BonBanNoiBat", true);
            _db.AddParameter(cmd, "Top", DbType.Int32, Top);
            _db.AddParameter(cmd, "EditionType", DbType.Int32, EditionType);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        

        public DataTable Microf_Detail(long News_Id)
        {
            IDbCommand cmd = _db.CreateCommand("Microf_Detail", true);
            _db.AddParameter(cmd, "News_Id", DbType.Int64, News_Id);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable Microf_SelectCategory(int Cat_ID)
        {
            IDbCommand cmd = _db.CreateCommand("Microf_SelectCategory", true);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable Select_DanhSachTin_Count(int Cat_ID)
        {
            IDbCommand cmd = _db.CreateCommand("Select_DanhSachTin_Count", true);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable Web_SelectNewsByOtherCat(string OtherCat)
        {
            IDbCommand cmd = _db.CreateCommand("Web_SelectNewsByOtherCat", true);
            _db.AddParameter(cmd, "OtherCat", DbType.String, OtherCat);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable tdt_SelectLastestNewByCatID(int Cat_ID)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_SelectLastestNewByCatID", true);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable NewsPublished_GetSubCatByCatParent(string Cat_ParentID)
        {
            IDbCommand cmd = _db.CreateCommand("NewsPublished_GetSubCatByCatParent", true);
            _db.AddParameter(cmd, "Cat_ID", DbType.String, Cat_ParentID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable Category_GetListByWhere(string whereSql, string orderBySql)
        {
            IDbCommand cmd = _db.CreateCommand("Category_GetListByWhere", true);
            _db.AddParameter(cmd, "whereSql", DbType.String, whereSql);
            _db.AddParameter(cmd, "orderBySql", DbType.String, orderBySql);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }
        public DataTable tdt_SelectCatProParent()
        {
            IDbCommand cmd = _db.CreateCommand("tdt_SelectCatProParent", true);
            DataTable table = CreateDataTable(cmd);
            return table;
        }
        public DataTable tdt_SelectTopNew(Int32 Top)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_SelectTopNew", true);
            _db.AddParameter(cmd, "Top", DbType.Int32, Top);

            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable tdt_NewsList(int CatID,Int32 PageIndex, Int32 PageSize, Int64 NewsID)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_NewsList", true);
            _db.AddParameter(cmd, "CatID", DbType.Int32, CatID);
            _db.AddParameter(cmd, "PageIndex", DbType.Int32, PageIndex);
            _db.AddParameter(cmd, "PageSize", DbType.Int32, PageSize);
            _db.AddParameter(cmd, "NewsID", DbType.Int64, NewsID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable Web_NoiBatMuc(int CatID, Int32 Top,bool IsHidden = true)
        {
            IDbCommand cmd = _db.CreateCommand("Web_NoiBatMuc", true);
            _db.AddParameter(cmd, "CatID", DbType.Int32, CatID);
            _db.AddParameter(cmd, "Top", DbType.Int32, Top);
            _db.AddParameter(cmd, "IsHidden", DbType.Boolean, IsHidden);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable tdt_NoiBatMuc(int CatID,Int32 Top)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_NoiBatMuc", true);
            _db.AddParameter(cmd, "CatID", DbType.Int32, CatID);
            _db.AddParameter(cmd, "Top", DbType.Int32, Top);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable tdt_SelectOtherNews(Int64 NewsID, int Top)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_SelectOtherNews", true);
            _db.AddParameter(cmd, "NewsID", DbType.Int64, NewsID);
            _db.AddParameter(cmd, "Top", DbType.Int32, Top);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable DetailsNews(Int64 NewsID)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_DetailsNews", true);
            _db.AddParameter(cmd, "NewsID", DbType.Int64, NewsID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable NewsListCount(int CatID,long NewsID)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_NewsListCount", true);
            _db.AddParameter(cmd, "CatID", DbType.Int32, CatID);
            _db.AddParameter(cmd, "NewsID", DbType.Int64, NewsID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable tdt_UpdateStatusActiveNews(string News_ID)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_UpdateStatusActiveNews", true);
            _db.AddParameter(cmd, "News_ID", DbType.String, News_ID);

            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable tdt_SelectAllNewsIsNotActive()
        {
            IDbCommand cmd = _db.CreateCommand("tdt_SelectAllNewsIsNotActive", true);


            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        
        public DataTable tdt_SelectAllNews()
        {
            IDbCommand cmd = _db.CreateCommand("tdt_SelectAllNews", true);


            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable tdt_UpdateStatusNewsPublished(string News_ID)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_UpdateStatusNewsPublished", true);
            _db.AddParameter(cmd, "News_ID", DbType.String, News_ID);
          
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable tdt_SelectNewsByNewsID(long News_ID)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_SelectNewsByNewsID", true);
            _db.AddParameter(cmd, "News_ID", DbType.Int64, News_ID);
          
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable tdt_InsertNewspublished(long News_ID, int Cat_ID, string News_Title, string News_Subtitle, string News_Image, string News_Content, string News_InitContent, string News_Athor, int News_Status, DateTime News_PublishDate, bool News_isFocus, int News_Mode)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_InsertNewspublished", true);
            _db.AddParameter(cmd, "News_ID", DbType.Int64, News_ID);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            _db.AddParameter(cmd, "News_Title", DbType.String, News_Title);
            _db.AddParameter(cmd, "News_Subtitle", DbType.String, News_Subtitle);
            _db.AddParameter(cmd, "News_Image", DbType.String, News_Image);
            _db.AddParameter(cmd, "News_Content", DbType.String, News_Content);
            _db.AddParameter(cmd, "News_Athor", DbType.String, News_Athor);
            _db.AddParameter(cmd, "News_Status", DbType.Int32, News_Status);
            _db.AddParameter(cmd, "News_PublishDate", DbType.DateTime, News_PublishDate);
            _db.AddParameter(cmd, "News_isFocus", DbType.Boolean, News_isFocus);
            _db.AddParameter(cmd, "News_Mode", DbType.Int32, News_Mode);
            _db.AddParameter(cmd, "News_InitContent", DbType.String, News_InitContent);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable tdt_UpdateNewspublished(long News_ID, int Cat_ID, string News_Title, string News_Subtitle, string News_Image, string News_Content, string News_InitContent, string News_Athor, int News_Status, DateTime News_PublishDate, bool News_isFocus, int News_Mode)
        {
            IDbCommand cmd = _db.CreateCommand("tdt_UpdateNewspublished", true);
            _db.AddParameter(cmd, "News_ID", DbType.Int64, News_ID);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, Cat_ID);
            _db.AddParameter(cmd, "News_Title", DbType.String, News_Title);
            _db.AddParameter(cmd, "News_Subtitle", DbType.String, News_Subtitle);
            _db.AddParameter(cmd, "News_Image", DbType.String, News_Image);
            _db.AddParameter(cmd, "News_Content", DbType.String, News_Content);
            _db.AddParameter(cmd, "News_Athor", DbType.String, News_Athor);
            _db.AddParameter(cmd, "News_Status", DbType.Int32, News_Status);
            _db.AddParameter(cmd, "News_PublishDate", DbType.DateTime, News_PublishDate);
            _db.AddParameter(cmd, "News_isFocus", DbType.Boolean, News_isFocus);
            _db.AddParameter(cmd, "News_Mode", DbType.Int32, News_Mode);
            _db.AddParameter(cmd, "News_InitContent", DbType.String, News_InitContent);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable SelectAccountLogin(String Acc, string Pass)
        {
            IDbCommand cmd = _db.CreateCommand("SelectAccountLogin", true);
            _db.AddParameter(cmd, "Acc", DbType.String, Acc);
            _db.AddParameter(cmd, "Pass", DbType.String, Pass);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }


        public DataTable proc_SelectProductColor(int ID)
        {
            IDbCommand cmd = _db.CreateCommand("proc_SelectProductColor", true);
            _db.AddParameter(cmd, "ID", DbType.Int32, ID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="Row"></param>
        /// <returns>Danh sach cac san pham hot</returns>
        public DataTable SelectProductHot(Int32 Row)
        {
            IDbCommand cmd = _db.CreateCommand("SelectProductHot", true);
            _db.AddParameter(cmd, "Row", DbType.Int32, Row);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable proc_SelectProductColorsAll()
        {
            IDbCommand cmd = _db.CreateCommand("proc_SelectProductColorsAll", true);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="Row"></param>
        /// <returns>Danh sach cac san pham moi</returns>
        public DataTable SelectProductNew(Int32 Row)
        {
            IDbCommand cmd = _db.CreateCommand("SelectProductNew", true);
            _db.AddParameter(cmd, "Row", DbType.Int32, Row);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="WhereCondition">DK tim kiem</param>
        /// <returns></returns>
        public DataTable SelecttblProductsDynamic(String WhereCondition)
        {
            IDbCommand cmd = _db.CreateCommand("SelecttblProductsDynamic", true);
            _db.AddParameter(cmd, "WhereCondition", DbType.String, WhereCondition);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="Cat_id"></param>
        /// <returns></returns>
        public DataTable DeletetblCategory(String Cat_id)
        {
            IDbCommand cmd = _db.CreateCommand("DeletetblCategory", true);
            _db.AddParameter(cmd, "Cat_id", DbType.String, Cat_id);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="table"></param>
        /// <param name="item_per_page"></param>
        /// <param name="page_number"></param>
        /// <param name="fields"></param>
        /// <param name="key"></param>
        /// <param name="order"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable DoSearch(String table, Int32 item_per_page, Int32 page_number, String fields, String key, String order, String where)
        {
            IDbCommand cmd = _db.CreateCommand("DoSearch", true);
            _db.AddParameter(cmd, "table", DbType.String, table);
            _db.AddParameter(cmd, "item_per_page", DbType.Int32, item_per_page);
            _db.AddParameter(cmd, "page_number", DbType.Int32, page_number);
            _db.AddParameter(cmd, "fields", DbType.String, fields);
            _db.AddParameter(cmd, "key", DbType.String, key);
            _db.AddParameter(cmd, "order", DbType.String, order);
            _db.AddParameter(cmd, "where", DbType.String, where);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="Cat_ParentID"></param>
        /// <param name="Cat_Log"></param>
        /// <param name="Cat_name"></param>
        /// <returns></returns>
        public DataTable InserttblCategory(Int32 Cat_ParentID, String Cat_Log, String Cat_name)
        {
            IDbCommand cmd = _db.CreateCommand("InserttblCategory", true);
            _db.AddParameter(cmd, "Cat_ParentID", DbType.Int32, Cat_ParentID);
            _db.AddParameter(cmd, "Cat_Log", DbType.String, Cat_Log);
            _db.AddParameter(cmd, "Cat_name", DbType.String, Cat_name);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="Cat_ParentID"></param>
        /// <param name="Cat_Log"></param>
        /// <param name="Cat_id"></param>
        /// <param name="Cat_name"></param>
        /// <returns></returns>
        public DataTable InsertUpdatetblCategory(Int32 Cat_ParentID, String Cat_Log, Int32 Cat_id, String Cat_name)
        {
            IDbCommand cmd = _db.CreateCommand("InsertUpdatetblCategory", true);
            _db.AddParameter(cmd, "Cat_ParentID", DbType.Int32, Cat_ParentID);
            _db.AddParameter(cmd, "Cat_Log", DbType.String, Cat_Log);
            _db.AddParameter(cmd, "Cat_id", DbType.Int32, Cat_id);
            _db.AddParameter(cmd, "Cat_name", DbType.String, Cat_name);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="CatID"></param>
        /// <returns></returns>
        public DataTable SelectCatChildren(Int32 CatID)
        {
            IDbCommand cmd = _db.CreateCommand("Web_SelectCatChildren", true);
            _db.AddParameter(cmd, "CatID", DbType.Int32, CatID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCatParent()
        {
            IDbCommand cmd = _db.CreateCommand("Web_SelectCatParent", true);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="Cat_ID"></param>
        /// <returns></returns>
        public DataTable SelectCountProductByCatID(String Cat_ID)
        {
            IDbCommand cmd = _db.CreateCommand("SelectCountProductByCatID", true);
            _db.AddParameter(cmd, "Cat_ID", DbType.String, Cat_ID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable proc_ProductsSelectOrderTime(Int32 PageSize, Int32 PageNum, string OrderType)
        {
            IDbCommand cmd = _db.CreateCommand("proc_ProductsSelectOrderTime", true);
            _db.AddParameter(cmd, "pageSize", DbType.Int32, PageSize);
            _db.AddParameter(cmd, "pageIndex", DbType.Int32, PageNum);
            _db.AddParameter(cmd, "OrderType", DbType.String, OrderType);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable proc_ProductsSelectByProductType(Int32 PageSize, Int32 PageNum, int ProductType)
        {
            IDbCommand cmd = _db.CreateCommand("proc_ProductsSelectByProductType", true);
            _db.AddParameter(cmd, "pageSize", DbType.Int32, PageSize);
            _db.AddParameter(cmd, "pageIndex", DbType.Int32, PageNum);
            _db.AddParameter(cmd, "ProductType", DbType.Int32, ProductType);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageNum"></param>
        /// <param name="CatID"></param>
        /// <returns></returns>
        public DataTable proc_ProductsSelectByColorPaged(Int32 PageSize, Int32 PageNum, int ColorID)
        {
            IDbCommand cmd = _db.CreateCommand("proc_ProductsSelectByColorPaged", true);
            _db.AddParameter(cmd, "pageSize", DbType.Int32, PageSize);
            _db.AddParameter(cmd, "pageIndex", DbType.Int32, PageNum);
            _db.AddParameter(cmd, "ColorID", DbType.Int32, ColorID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable SelectProductsByCatID(Int32 PageSize, Int32 PageNum, int CatID)
        {
            IDbCommand cmd = _db.CreateCommand("proc_ProductsSelectPaged", true);
            _db.AddParameter(cmd, "pageSize", DbType.Int32, PageSize);
            _db.AddParameter(cmd, "pageIndex", DbType.Int32, PageNum);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, CatID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable SelectProductsByCatIDCount(int CatID)
        {
            IDbCommand cmd = _db.CreateCommand("proc_ProductsSelectCount", true);
      
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, CatID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }


        public DataTable proc_SelectProductCategory_Paged(Int32 PageSize, Int32 PageNum, int CatID)
        {
            IDbCommand cmd = _db.CreateCommand("proc_SelectProductCategory_Paged", true);
            _db.AddParameter(cmd, "pageSize", DbType.Int32, PageSize);
            _db.AddParameter(cmd, "pageIndex", DbType.Int32, PageNum);
            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, CatID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }

        public DataTable proc_SelectProductCategory_Count(int CatID)
        {
            IDbCommand cmd = _db.CreateCommand("proc_SelectProductCategory_Count", true);

            _db.AddParameter(cmd, "Cat_ID", DbType.Int32, CatID);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
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
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <returns></returns>
        public DataTable SelecttblCategoriesAll()
        {
            IDbCommand cmd = _db.CreateCommand("SelecttblCategoriesAll", true);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="Cat_id"></param>
        /// <returns></returns>
        public DataTable SelecttblCategory(Int32 Cat_id)
        {
            IDbCommand cmd = _db.CreateCommand("proc_SelectProduct_Category", true);
            _db.AddParameter(cmd, "ID", DbType.Int32, Cat_id);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="P_id"></param>
        /// <returns></returns>
        public DataTable DeletetblProduct(String P_id)
        {
            IDbCommand cmd = _db.CreateCommand("DeletetblProduct", true);
            _db.AddParameter(cmd, "P_id", DbType.String, P_id);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
        public DataTable InsertUpdatetblProduct(String P_kichthuoc, String P_tgthoai,  String P_trongluong, Boolean P_IsNew, String P_tgcho, String P_name, Boolean P_IsFocus, String P_camera, Int32 P_catID, String P_giachinhhang, String P_daitan, String P_chuthich, Boolean P_gprs, String P_pin, Int32 P_sold, String P_dophangiai, Int32 P_id, Boolean P_hongngoai, String P_baohanh, String P_amthanh, Boolean P_bluetooth, Boolean P_trangthai, Boolean P_usb, String P_tinnhan, String P_image, String P_memory, String P_manhinh)
        {
            IDbCommand cmd = _db.CreateCommand("InsertUpdatetblProduct", true);
            _db.AddParameter(cmd, "P_kichthuoc", DbType.String, P_kichthuoc);
            _db.AddParameter(cmd, "P_tgthoai", DbType.String, P_tgthoai);
            _db.AddParameter(cmd, "P_trongluong", DbType.String, P_trongluong);
            _db.AddParameter(cmd, "P_IsNew", DbType.Boolean, P_IsNew);
            _db.AddParameter(cmd, "P_tgcho", DbType.String, P_tgcho);
            _db.AddParameter(cmd, "P_name", DbType.String, P_name);
            _db.AddParameter(cmd, "P_IsFocus", DbType.Boolean, P_IsFocus);
            _db.AddParameter(cmd, "P_camera", DbType.String, P_camera);
            _db.AddParameter(cmd, "P_catID", DbType.Int32, P_catID);
            _db.AddParameter(cmd, "P_giachinhhang", DbType.String, P_giachinhhang);
            _db.AddParameter(cmd, "P_daitan", DbType.String, P_daitan);
            _db.AddParameter(cmd, "P_chuthich", DbType.String, P_chuthich);
            _db.AddParameter(cmd, "P_gprs", DbType.Boolean, P_gprs);
            _db.AddParameter(cmd, "P_pin", DbType.String, P_pin);
            _db.AddParameter(cmd, "P_sold", DbType.Int32, P_sold);
            _db.AddParameter(cmd, "P_dophangiai", DbType.String, P_dophangiai);
            _db.AddParameter(cmd, "P_id", DbType.Int32, P_id);
            _db.AddParameter(cmd, "P_hongngoai", DbType.Boolean, P_hongngoai);
            _db.AddParameter(cmd, "P_baohanh", DbType.String, P_baohanh);
            _db.AddParameter(cmd, "P_amthanh", DbType.String, P_amthanh);
            _db.AddParameter(cmd, "P_bluetooth", DbType.Boolean, P_bluetooth);
            _db.AddParameter(cmd, "P_trangthai", DbType.Boolean, P_trangthai);
            _db.AddParameter(cmd, "P_usb", DbType.Boolean, P_usb);
            _db.AddParameter(cmd, "P_tinnhan", DbType.String, P_tinnhan);
            _db.AddParameter(cmd, "P_image", DbType.String, P_image);
            _db.AddParameter(cmd, "P_memory", DbType.String, P_memory);
            _db.AddParameter(cmd, "P_manhinh", DbType.String, P_manhinh);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }
       
        public DataTable InserttblProduct(String P_kichthuoc, String P_tgthoai, String P_trongluong, Boolean P_IsNew, String P_tgcho, String P_name, Boolean P_IsFocus, String P_camera, Int32 P_catID, String P_giachinhhang, String P_daitan, String P_chuthich, Boolean P_gprs, String P_pin, Int32 P_sold, String P_dophangiai, Boolean P_trangthai, Boolean P_hongngoai, String P_baohanh, String P_amthanh, Boolean P_bluetooth, Boolean P_usb, String P_tinnhan, String P_image, String P_memory, String P_manhinh)
        {
            IDbCommand cmd = _db.CreateCommand("InserttblProduct", true);
            _db.AddParameter(cmd, "P_kichthuoc", DbType.String, P_kichthuoc);
            _db.AddParameter(cmd, "P_tgthoai", DbType.String, P_tgthoai);
            _db.AddParameter(cmd, "P_trongluong", DbType.String, P_trongluong);
            _db.AddParameter(cmd, "P_IsNew", DbType.Boolean, P_IsNew);
            _db.AddParameter(cmd, "P_tgcho", DbType.String, P_tgcho);
            _db.AddParameter(cmd, "P_name", DbType.String, P_name);
            _db.AddParameter(cmd, "P_IsFocus", DbType.Boolean, P_IsFocus);
            _db.AddParameter(cmd, "P_camera", DbType.String, P_camera);
            _db.AddParameter(cmd, "P_catID", DbType.Int32, P_catID);
            _db.AddParameter(cmd, "P_giachinhhang", DbType.String, P_giachinhhang);
            _db.AddParameter(cmd, "P_daitan", DbType.String, P_daitan);
            _db.AddParameter(cmd, "P_chuthich", DbType.String, P_chuthich);
            _db.AddParameter(cmd, "P_gprs", DbType.Boolean, P_gprs);
            _db.AddParameter(cmd, "P_pin", DbType.String, P_pin);
            _db.AddParameter(cmd, "P_sold", DbType.Int32, P_sold);
            _db.AddParameter(cmd, "P_dophangiai", DbType.String, P_dophangiai);
            _db.AddParameter(cmd, "P_trangthai", DbType.Boolean, P_trangthai);
            _db.AddParameter(cmd, "P_hongngoai", DbType.Boolean, P_hongngoai);
            _db.AddParameter(cmd, "P_baohanh", DbType.String, P_baohanh);
            _db.AddParameter(cmd, "P_amthanh", DbType.String, P_amthanh);
            _db.AddParameter(cmd, "P_bluetooth", DbType.Boolean, P_bluetooth);
            _db.AddParameter(cmd, "P_usb", DbType.Boolean, P_usb);
            _db.AddParameter(cmd, "P_tinnhan", DbType.String, P_tinnhan);
            _db.AddParameter(cmd, "P_image", DbType.String, P_image);
            _db.AddParameter(cmd, "P_memory", DbType.String, P_memory);
            _db.AddParameter(cmd, "P_manhinh", DbType.String, P_manhinh);
            DataTable dataTable = CreateDataTable(cmd);
            return dataTable;
        }


        public DataTable SelecttblProduct(Int32 P_id)
        {
            IDbCommand cmd = _db.CreateCommand("proc_ProductSelect", true);
            _db.AddParameter(cmd, "Id", DbType.Int32, P_id);
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

        public DataTable proc_SelectProductGiftAll()
        {
            IDbCommand cmd = _db.CreateCommand("proc_SelectProductGiftAll", true);
            DataTable table = CreateDataTable(cmd);
            return table;
        }
    }
} 

