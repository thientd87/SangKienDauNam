using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using DFISYS.BO.Editoral.ProductColor;
using Microsoft.ApplicationBlocks.Data;

namespace DFISYS.BO.Editoral.ProductGift
{
    public class ProductGiftDAL
    {
        private readonly string _conn;
        public ProductGiftDAL()
        {
            _conn = ConfigurationManager.ConnectionStrings["cms_coreConnectionString"].ToString();
        }


        public void proc_DeleteProductGift(ProductGiftObject obj)
        {
            SqlHelper.ExecuteNonQuery(_conn, "proc_DeleteProductGift", obj.Id);
        }

        public void proc_InsertUpdateProductGift(ProductGiftObject obj)
        {
            SqlHelper.ExecuteNonQuery(_conn, "proc_InsertUpdateProductGift", obj.Id, obj.ProductGift, obj.Order, obj.IsActive);
        }

        public IDataReader proc_SelectProductGift(int ID)
        {
            return SqlHelper.ExecuteReader(_conn, "proc_SelectProductGift", ID);
        }


        public IDataReader proc_SelectProductGiftAll()
        {
            return SqlHelper.ExecuteReader(_conn, "proc_SelectProductGiftAll");
        }
        
    }
}