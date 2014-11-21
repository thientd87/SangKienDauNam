using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;

namespace DFISYS.BO.Editoral.Product
{
    public class ProductsDAL
    {
        private readonly string _conn;
        public ProductsDAL()
        {
            _conn = ConfigurationManager.ConnectionStrings["cms_coreConnectionString"].ToString();
        }


        public void proc_ProductDelete(int ID)
        {
            SqlHelper.ExecuteNonQuery(_conn, "proc_ProductDelete", ID);
        }


        public IDataReader proc_ProductInsertUpdate(Product obj)
        {
            return SqlHelper.ExecuteReader(_conn, "proc_ProductInsertUpdate", obj.Id, obj.ProductName, obj.ProductName_En, obj.ProductSumary, obj.ProductSumary_En, obj.ProductDescription, obj.ProductDescription_En, obj.ProductCategory, obj.ProductColor, obj.ProductCost, obj.ProductType, obj.ProductAvatar, obj.ProductLayout, obj.IsActive,obj.ProductVideo,obj.ProductTag,obj.ProductOtherCat);
        }


        public IDataReader proc_ProductSelect(int  Id)
        {
            return SqlHelper.ExecuteReader(_conn, "proc_ProductSelect", Id);
        }


        public IDataReader proc_ProductsSelectAll()
        {
            return SqlHelper.ExecuteReader(_conn, "proc_ProductsSelectAll");
        }


        public IDataReader proc_ProductsSelectDynamic(string whereCondition, string orderByExpression)
        {
            return SqlHelper.ExecuteReader(_conn, "proc_ProductsSelectDynamic", whereCondition, orderByExpression);
        }


        public IDataReader proc_ProductsSelectPaged()
        {
            return SqlHelper.ExecuteReader(_conn, "proc_ProductsSelectPaged");
        }
    }
}