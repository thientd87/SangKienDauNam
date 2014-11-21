using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DFISYS.BO.Editoral.ProductColor;
using Nextcom.Citinews.Core.Library;

namespace DFISYS.BO.Editoral.ProductGift
{
    public class ProductGiftController
    {
        public List<ProductGiftObject> SellectAllProductGift()
        {
            return ObjectHelper.FillCollection<ProductGiftObject>(new ProductGiftDAL().proc_SelectProductGiftAll());
        }
        public void DeleteProductGift(ProductGiftObject obj)
        {
            new ProductGiftDAL().proc_DeleteProductGift(obj);
        }
        public void UpdateProductGift(ProductGiftObject obj)
        {
            new ProductGiftDAL().proc_InsertUpdateProductGift(obj);
        }
        public ProductGiftObject SelectProductGift(int ID)
        {
            return ObjectHelper.FillObject<ProductGiftObject>(new ProductGiftDAL().proc_SelectProductGift(ID));
        }
    }
}