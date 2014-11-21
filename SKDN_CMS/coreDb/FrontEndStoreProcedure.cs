using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace DFISYS.Core.DAL
{
   public class FrontEndStoreProcedure
    {
        private MainDB _db;

        public FrontEndStoreProcedure(MainDB db)
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

        public DataTable NewsPublished_GetNewsModeByCat(string Cat_ID)
        {
            IDbCommand cmd = _db.CreateCommand("NewsPublished_GetNewsModeByCat", true);
            _db.AddParameter(cmd, "Cat_ID", DbType.String, Cat_ID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

        public DataTable NewsPublished_GetTop10News(string Cat_ID)
        {
            IDbCommand cmd = _db.CreateCommand("NewsPublished_GetTop10News", true);
            _db.AddParameter(cmd, "Cat_ID", DbType.String, Cat_ID);
            DataTable table = _db.CreateDataTable(cmd);
            return table;
        }

       public DataTable NewsPublished_GetTopNews(string Cat_ID, string Top)
       {
           IDbCommand cmd = _db.CreateCommand("NewsPublished_GetTopNews", true);
           _db.AddParameter(cmd, "Cat_ID", DbType.String, Cat_ID);
           _db.AddParameter(cmd, "Number", DbType.String, Top);
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
           IDbCommand cmd = _db.CreateCommand("CMS_Category_GetListByWhere", true);
           _db.AddParameter(cmd, "whereSql", DbType.String, whereSql);
           _db.AddParameter(cmd, "orderBySql", DbType.String, orderBySql);
           DataTable table = _db.CreateDataTable(cmd);
           return table;
       }


       #region SonPC
       public DataTable vc_SelectCategoryByParent(Int32 Cat_ParentID, Int32 EditionTypeID)
       {
           IDbCommand cmd = _db.CreateCommand("vc_SelectCategoryByParent", true);
           _db.AddParameter(cmd, "iCat_ParentID", DbType.Int32, Cat_ParentID);
           _db.AddParameter(cmd, "iEditionTypeID", DbType.Int32, EditionTypeID);
           DataTable table = CreateDataTable(cmd);
           return table;
       }

       public DataTable vc_News_Select_NewsFocus_ByCat_ID(Int32 Cat_ID)
       {
           IDbCommand cmd = _db.CreateCommand("vc_News_Select_NewsFocus_ByCat_ID", true);
           _db.AddParameter(cmd, "iCat_ID", DbType.Int32, Cat_ID);
           DataTable dataTable = CreateDataTable(cmd);
           return dataTable;
       }

       public Int32 vc_SelectParentIDByCategoryID(Int32 Cat_ID)
       {
           IDbCommand cmd = _db.CreateCommand("vc_SelectCategoryByID", true);
           _db.AddParameter(cmd, "iCat_ID", DbType.Int32, Cat_ID);
           DataTable table = CreateDataTable(cmd);

           if (table.Rows.Count == 0)
           {
               return 0;
           }
           else
           {
               if (table.Rows[0]["Cat_ParentID"].Equals(0))
               {
                   return Cat_ID;
               }
               else
               {
                   return vc_SelectParentIDByCategoryID(Convert.ToInt32(table.Rows[0]["Cat_ParentID"]));
               }
               //return Convert.ToInt32(table.Rows[0]["Cat_ParentID"]);
           }
       }
       #endregion
   }
}
