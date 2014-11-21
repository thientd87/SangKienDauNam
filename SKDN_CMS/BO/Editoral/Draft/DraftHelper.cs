using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DFISYS.Core.DAL;

namespace DFISYS.BO.Editoral.Draft
{
    public static class DraftHelper
    {
        public static void InsertNewsTemp(Int64 temp_id, Int64 news_id,string cat_id, string news_title, string news_image, string news_sapo, string news_content)
        {
            using (MainDB objDb = new MainDB())
            {
                objDb.CallStoredProcedure("be_InsertNewsTemp", new object[] {temp_id, news_id, cat_id, news_title, news_image, news_sapo, news_content, HttpContext.Current.User.Identity.Name }, new string[] { "@news_temp", "@news_id", "@cat_id", "@news_title", "@news_image", "@news_sapo", "@news_content", "@username" }, false);
            }
        }


        public static void UpdateNewsTemp(Int64 temp_id, Int64 news_id, string cat_id, string news_title, string news_image, string news_sapo, string news_content)
        {
            using (MainDB objDb = new MainDB())
            {
                objDb.CallStoredProcedure("be_UpdateNewsTemp", new object[] { temp_id, news_id, cat_id, news_title, news_image, news_sapo, news_content, HttpContext.Current.User.Identity.Name }, new string[] { "@news_temp", "@news_id", "@cat_id", "@news_title", "@news_image", "@news_sapo", "@news_content", "@username" }, false);
            }
        }

        public static bool CheckExistTempID(Int64 temp_id)
        {
            int k;
            using (MainDB objDb = new MainDB())
            {
                k = Convert.ToInt32(((DataTable)objDb.CallStoredProcedure("[be_CheckExistTempID]", new object[] { temp_id }, new string[] { "temp_id" }, true)).Rows[0][0]);
            }
            if (k > 0)
                return true;
            else
                return false;

            return false;
        }

        public static string GetTempIDByNewsID(Int64 news_id)
        {
            string strReturn = "";
            DataTable dt;
            using (MainDB objDb = new MainDB())
            {
                dt = (DataTable)objDb.CallStoredProcedure("be_GetTempIDByNewsID", new object[] { news_id, HttpContext.Current.User.Identity.Name }, new string[] { "@news_id", "@username" }, true);
            }
            if (dt != null && dt.Rows.Count > 0)
            { 
                strReturn = dt.Rows[0]["temp_id"].ToString();
            }

            return strReturn;
        }

        public static void DeleteNewsTempByTempID(string temp_id)
        {
            try
            {
                using (MainDB objDb = new MainDB())
                {
                    objDb.CallStoredProcedure("DeleteNewsTempByTempID", new object[] { temp_id, HttpContext.Current.User.Identity.Name }, new string[] { "@temp_id", "@username" } , false);
                }
            }
            catch { }
        }

        public static void DeleteNewsTempByNewsID(string news_id)
        {
            try
            {
                using (MainDB objDb = new MainDB())
                {
                    objDb.CallStoredProcedure("DeleteNewsTempByNewsID", new object[] { news_id, HttpContext.Current.User.Identity.Name }, new string[] { "@news_id", "@username" }, false);
                }
            }
            catch { }
        }

        public static DataTable GetList(string fromDate, string toDate, string news_title, string cat_id, int PageSize, int StartRow)
        {
            string strWhereSQL = getSQLWhere(fromDate, toDate,  news_title, cat_id);
            DataTable dt;
            using (MainDB objDb = new MainDB())
            {
                dt = (DataTable)objDb.CallStoredProcedure("be_GetListNewsTemp", new object[] { strWhereSQL, StartRow, PageSize }, new string[] { "@strWhere", "@StartIndex", "@PageSize" }, true);
            }
            return dt;
        }

        public static int GetNumberRecord(string fromDate, string toDate, string news_title, string cat_id)
        {
            DataTable dt;
            string strWhereSQL = getSQLWhere(fromDate, toDate, news_title, cat_id);
            using (MainDB objDb = new MainDB())
            {
                dt = (DataTable)objDb.CallStoredProcedure("be_GetListNewsTemp_NumRow", new object[] { strWhereSQL}, new string[] { "@strWhere" }, true);
            }
            int iReturn = Convert.ToInt32(dt.Rows[0][0]);
            return iReturn;
        }


        private static string getSQLWhere(string fromDate, string toDate, string news_title, string cat_id)
        {
            string strWhereSQL = "";
            if (!string.IsNullOrEmpty(fromDate))
            {
                if (fromDate.Trim() != "")
                    fromDate = fromDate.Substring(3, 2) + "/" + fromDate.Substring(0, 2) + "/" + fromDate.Substring(6, 4);
                strWhereSQL += " AND ModifyDate >= '" + fromDate + "'";
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                if (toDate.Trim() != "")
                    toDate = toDate.Substring(3, 2) + "/" + toDate.Substring(0, 2) + "/" + toDate.Substring(6, 4) + " 23:59.999";
                strWhereSQL += " AND ModifyDate <= '" + toDate + "'";
            }

            if (!string.IsNullOrEmpty(news_title))
                strWhereSQL += " AND News_Title like N'%" + news_title + "%'";
            if (!string.IsNullOrEmpty(cat_id) && cat_id != "-1" && cat_id != "0")
                strWhereSQL += " AND cat_id = " + cat_id + "";

            strWhereSQL += " AND username = '" + HttpContext.Current.User.Identity.Name + "' ";

            return strWhereSQL;
        }

        public static DataTable GetNewsTempDetailByTempID(string temp_id)
        {
            using (MainDB objDb = new MainDB())
            {
                return (DataTable)objDb.CallStoredProcedure("be_GetNewsTempDetailByTempID", new object[] { temp_id }, new string[] { "@temp_id" }, true);
            }
        }

        public static void DeleteNewsTemp(string temp_id)
        {
            using (MainDB objDb = new MainDB())
            {
                objDb.CallStoredProcedure("be_DeleteNewsTemp", new object[] { temp_id }, new string[] { "@temp_id" }, false);
            }
        }
    }
}
