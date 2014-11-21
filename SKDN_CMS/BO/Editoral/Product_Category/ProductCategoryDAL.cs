using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using DFISYS.CoreBO.Common;
using Microsoft.ApplicationBlocks.Data;

namespace DFISYS.BO.Editoral.Product_Category
{
    public class ProductCategoryDAL
    {
        private readonly string _conn;
        public ProductCategoryDAL()
        {
            _conn = ConfigurationManager.ConnectionStrings["cms_coreConnectionString"].ToString();
        }
        public void DeletetblCategory(int ID)
        {
            SqlHelper.ExecuteNonQuery(_conn, "proc_DeleteProduct_Category", ID);
        }
        public void InsertUpdatetblCategory(ProductCategory pc)
        {
            SqlHelper.ExecuteNonQuery(_conn, "proc_InsertUpdateProduct_Category", pc.ID, pc.Product_Category_Name, pc.Product_Category_Name_En, pc.Product_Category_Desc, pc.Product_Category_Name_En, pc.Product_Category_CatParent_ID, pc.IsActive, pc.MenuType, pc.Product_Category_Image, pc.Product_Category_Order);
        }
        public IDataReader SelecttblCategory(int ID)
        {
            return SqlHelper.ExecuteReader(_conn, "proc_SelectProduct_Category", ID);
        }
        public IDataReader SelectCatParent()
        {
            return SqlHelper.ExecuteReader(_conn, "SelectCatParent");
        }
        public IDataReader SelectCatChildren(int CatID)
        {
            return SqlHelper.ExecuteReader(_conn, "SelectCatChildren", CatID);
        }
        
    }
}