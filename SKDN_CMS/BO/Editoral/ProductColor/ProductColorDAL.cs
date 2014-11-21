using System.Configuration;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

namespace DFISYS.BO.Editoral.ProductColor
{
    public class ProductColorDAL
    {
        private readonly string _conn;
        public ProductColorDAL()
        {
            _conn = ConfigurationManager.ConnectionStrings["cms_coreConnectionString"].ToString();
        }


        public void proc_DeleteProductColor(ProductColorObject obj)
        {
            SqlHelper.ExecuteNonQuery(_conn, "proc_DeleteProductColor", obj.Id);
        }


        public void proc_InsertProductColor(ProductColorObject obj)
        {
            SqlHelper.ExecuteNonQuery(_conn, "proc_InsertProductColor", obj.ColorName, obj.ColorCode, obj.IsActive);
        }


        public void proc_InsertUpdateProductColor(ProductColorObject obj)
        {
            SqlHelper.ExecuteNonQuery(_conn, "proc_InsertUpdateProductColor", obj.Id, obj.ColorName, obj.ColorCode, obj.IsActive);
        }


        public IDataReader proc_SelectProductColor(int ID)
        {
            return SqlHelper.ExecuteReader(_conn, "proc_SelectProductColor", ID);
        }


        public IDataReader proc_SelectProductColorsAll()
        {
            return SqlHelper.ExecuteReader(_conn, "proc_SelectProductColorsAll");
        }


        public IDataReader proc_SelectProductColorsDynamic(string WhereCondition, string OrderByExpression)
        {
            return SqlHelper.ExecuteReader(_conn, "proc_SelectProductColorsDynamic", WhereCondition, OrderByExpression);
        }


        public IDataReader proc_SelectProductColorsPaged()
        {
            return SqlHelper.ExecuteReader(_conn, "proc_SelectProductColorsPaged");
        }


        public void proc_UpdateProductColor(ProductColorObject obj)
        {
            SqlHelper.ExecuteNonQuery(_conn, "proc_UpdateProductColor", obj.Id, obj.ColorName, obj.ColorCode, obj.IsActive);
        }
    }
}