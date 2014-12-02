using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DFISYS.BO.Editoral.NewsMedia;
using DFISYS.BO.Editoral.ProductColor;
using DFISYS.Core.DAL;
using Nextcom.Citinews.Core.Library;

namespace DFISYS.BO.Editoral.Product
{
    public class ProductController
    {
        public List<Product> ProductsSelectAll()
        {
            return ObjectHelper.FillCollection<Product>(new ProductsDAL().proc_ProductsSelectAll());
        }
        public List<Product> ProductsSelectDynamic(string whereCondition, string order)
        {
            return ObjectHelper.FillCollection<Product>(new ProductsDAL().proc_ProductsSelectDynamic(whereCondition,order));
        }
        public void DeleteProductColor(int ID)
        {
            new ProductsDAL().proc_ProductDelete(ID);
        }
        public Product ProductInsertUpdate(Product obj)
        {
            return ObjectHelper.FillObject<Product>(new ProductsDAL().proc_ProductInsertUpdate(obj));
            
        }
        public Product SelectProduct(int ID)
        {
            return ObjectHelper.FillObject<Product>(new ProductsDAL().proc_ProductSelect(ID));
        }

        public void DelteProduct(int ID)
        {
            new ProductsDAL().proc_ProductDelete(ID);
        }
    }
}