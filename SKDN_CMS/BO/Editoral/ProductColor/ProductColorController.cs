using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nextcom.Citinews.Core.Library;

namespace DFISYS.BO.Editoral.ProductColor
{
    public class ProductColorController
    {
        public List<ProductColorObject> SellectAllProductColor()
        {
            return ObjectHelper.FillCollection<ProductColorObject>(new ProductColorDAL().proc_SelectProductColorsAll());
        }
        public void DeleteProductColor(ProductColorObject obj)
        {
            new ProductColorDAL().proc_DeleteProductColor(obj);
        }
        public void UpdateProductColor(ProductColorObject obj)
        {
            new ProductColorDAL().proc_InsertUpdateProductColor(obj);
        }
        public ProductColorObject SelectProductColor(int ID)
        {
            return ObjectHelper.FillObject<ProductColorObject>(new ProductColorDAL().proc_SelectProductColor(ID));
        }
    }
}