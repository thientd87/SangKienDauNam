using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.Caching;
using System.Web;
using System.Globalization;

namespace DFISYS.CoreBO.Common {
    public class Const {
        //News_Mode
        public const int TIN_THUONG = 0;
        public const int TIN_NOI_BAT_TRANG_CHU = 2;
        public const int TIN_NOI_BAT_TRONG_MUC = 1;
        public const int TIN_TIEU_DIEM = 1;

        //News_Status
        public const int TRANG_THAI_TIN_DUOC_DANG = 3;
        public const int TRANG_THAI_TIN_BI_GO = 5; //TIN BỊ GỠ

        //Header
        public const string MENU_HOME = "home";
        public const string MENU_LOGIN = "login";

        //Content
        public const string CHI_TIET_TIN = "newsdetail";

        public const double VOTE_PROGRESS_BAR_LENGTH = 100;

        public const string FILM_ID = "FILM_ID";
        public const string table_NewsPublished = "NewsPublished";
        public const string table_Category = "Category";
        public const string table_Cinema = "Cinema";
        public const string table_City = "City";
        public const string table_Comment = "Comment";
        public const string table_MediaObject = "MediaObject";
        public const string table_VoteItem = "VoteItem";
        public const string table_Vote_Assign = "Vote_Assign";
        public const string table_Vote = "Vote";

        public string DATABASE_NAME = "ijCmsDb";

        public const string CurrentcyFormat = "{0:#,###0}";
    }
    public class Utils {
        public static string DATABASE_NAME = System.Configuration.ConfigurationSettings.AppSettings["CoreDb"].ToString();
        public Utils() {
        }
        public static string THUMBNAIL_LINK(String ImagePath, int ImageWidthSize) {
            string _path = "";
            _path = @"Thumbnail.Ashx?ImgFilePath=/";
            _path += ImagePath;
            _path += "&width=" + ImageWidthSize;
            return _path;
        }
        /// <summary>
        /// Đưa dữ liệu vào cache
        /// </summary>
        /// <param name="dataTableToCache">dữ liệu cần đưa</param>
        /// <param name="cacheName">tên cache</param>
        /// <param name="tableNameInDatabase">tên bảng trong DB</param>
        public static void SetDataToCache(DataTable dataTableToCache, string cacheName, string tableNameInDatabase) {
            SqlCacheDependency sqlDependency = new SqlCacheDependency(DATABASE_NAME, tableNameInDatabase);
            HttpContext.Current.Cache.Insert(cacheName, dataTableToCache, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }
        public static void SetDataToCache(int dataToCache, string cacheName, string tableNameInDatabase) {
            SqlCacheDependency sqlDependency = new SqlCacheDependency(DATABASE_NAME, tableNameInDatabase);
            HttpContext.Current.Cache.Insert(cacheName, dataToCache, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }

        public static void SetDataToCache(string dataToCache, string cacheName, string tableNameInDatabase) {
            SqlCacheDependency sqlDependency = new SqlCacheDependency(DATABASE_NAME, tableNameInDatabase);
            HttpContext.Current.Cache.Insert(cacheName, dataToCache, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }
        public static void SetDataToCache(DataTable[] dataToCache, string cacheName, string tableNameInDatabase) {
            SqlCacheDependency sqlDependency = new SqlCacheDependency(DATABASE_NAME, tableNameInDatabase);
            HttpContext.Current.Cache.Insert(cacheName, dataToCache, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }
        public static void SetDataToCache(DataSet dataToCache, string cacheName, string tableNameInDatabase) {
            SqlCacheDependency sqlDependency = new SqlCacheDependency(DATABASE_NAME, tableNameInDatabase);
            HttpContext.Current.Cache.Insert(cacheName, dataToCache, sqlDependency, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }

        public static void SetCache(DataTable dataCache, string cacheName, string[] tableNameInDatabase) {
            //System.Web.Caching.SqlCacheDependency sqlDep1 = new System.Web.Caching.SqlCacheDependency(Const.DATABASE_NAME, "tblTradeTransaction");
            //System.Web.Caching.SqlCacheDependency sqlDep2 = new System.Web.Caching.SqlCacheDependency(Const.DATABASE_NAME, "tblRemainTransaction");
            System.Web.Caching.SqlCacheDependency[] sqlDep = new SqlCacheDependency[tableNameInDatabase.Length];
            for (int i = 0; i < tableNameInDatabase.Length; i++) {
                sqlDep[i] = new System.Web.Caching.SqlCacheDependency(DATABASE_NAME, tableNameInDatabase[i]);
            }
            System.Web.Caching.AggregateCacheDependency agg = new System.Web.Caching.AggregateCacheDependency();
            //agg.Add(sqlDep1, sqlDep2);
            agg.Add(sqlDep);
            HttpContext.Current.Cache.Insert(cacheName, dataCache, agg, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }

        public static void SetToCache(object dataCache, string cacheName, string[] tableNameInDatabase) {
            System.Web.Caching.SqlCacheDependency[] sqlDep = new SqlCacheDependency[tableNameInDatabase.Length];
            for (int i = 0; i < tableNameInDatabase.Length; i++) {
                sqlDep[i] = new System.Web.Caching.SqlCacheDependency(DATABASE_NAME, tableNameInDatabase[i]);
            }
            System.Web.Caching.AggregateCacheDependency agg = new System.Web.Caching.AggregateCacheDependency();
            
            agg.Add(sqlDep);
            HttpContext.Current.Cache.Insert(cacheName, dataCache, agg, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration);
        }
        
        /// <summary>
        /// lấy dữ liệu từ cache ra
        /// </summary>
        /// <param name="cacheName">tên cache</param>
        /// <returns></returns>
        public static DataTable GetFromCache(string cacheName) {
            return HttpContext.Current.Cache[cacheName] as DataTable;
        }

        public static T GetFromCache<T>(string cacheName) {

            object obj = HttpContext.Current.Cache[cacheName];
            if (obj != null && obj != DBNull.Value)
                return (T)obj;
            return default(T);
        }

        public static int GetInt32FromCache(string cacheName) {
            return (int)HttpContext.Current.Cache[cacheName];
        }
        public static string GetStringFromCache(string cacheName) {
            return (string)HttpContext.Current.Cache[cacheName];
        }
        public static DataTable[] GetFromCacheAsTableArray(string cacheName) {
            return (DataTable[])HttpContext.Current.Cache[cacheName];
        }
        public static DataSet GetFromCacheAsDataSet(string cacheName) {
            return (DataSet)HttpContext.Current.Cache[cacheName];
        }
    }
    public static class function
    {
        public static int Obj2Int(object obj)
        {
            if (obj == null)
                return 0;
            try
            {
                return Convert.ToInt32(obj.ToString().Replace(",", string.Empty), CultureInfo.InvariantCulture);
            }
            catch
            {
                return 0;
            }
        }

        public static double Obj2Double(object value)
        {
            if (value == null || value.ToString().Trim() == "")
                return 0;
            try
            {
                return Convert.ToDouble(value, CultureInfo.InvariantCulture);
            }
            catch
            {
                return 0;
            }
        }
        public static bool Obj2Boolean(object value)
        {
            if (value == null || value.ToString().Trim() == "")
                return false;
            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return false;
            }
        }

        public static decimal Obj2Decimal(object value)
        {
            if (value == null || value.ToString().Trim() == "")
                return 0;
            try
            {
                return Convert.ToDecimal(value, CultureInfo.InvariantCulture);
            }
            catch
            {
                return 0;
            }
        }

        public static long Obj2Int64(object obj)
        {
            try
            {
                return Convert.ToInt64(obj);
            }
            catch
            {
                return 0;
            }
        }

        public static DateTime Obj2DateVN(object value)
        {
            try
            {
                return Convert.ToDateTime(value, new CultureInfo(1066));
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
            
        }
    }
}


