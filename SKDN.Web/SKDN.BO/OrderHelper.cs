using System;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DAL;

namespace BO
{
    public class OrderHelper
    {
        public OrderHelper()
        {}
         //<summary>
         //Created By DungTT
         //</summary>
         //<param name="OID">Xac nhan don hang da thanh toan</param>
        public void XacNhanThanhToan(Int64 OID)
        {
            using (MainDB db = new MainDB())
            {
                db.StoredProcedures.XacNhanThanhToan(OID);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OID"></param>
        /// <returns></returns>
        public DataTable GetInfoOfOrderByOrderID(Int64 OID)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.SelecttblOrder(OID);
            }
            //if (dt != null && dt.Rows.Count > 0)
            //{

            //    // dt.Columns.Add("OrderDate");
            //    //dt.Columns.Add("gia");
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //      //  dt.Rows[i]["gia"] = String.Format("{0:n}", Convert.ToInt64(dt.Rows[i]["P_Price"]));
            //        //dt.Rows[i]["OrderDate"] = Convert.ToDateTime(dt.Rows[i]["O_Date"]).ToString("dd/MM/yyyy HH:mm:ss");
            //    }
            //    dt.AcceptChanges();
            //}
            return dt;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="OID"></param>
        /// <returns>Noi dung chi tiet don hang</returns>
        public DataTable GetOrderDetailByOrderID(Int64 OID)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.GetOrderDetailByOrderID(OID);
            }
            if (dt != null && dt.Rows.Count > 0)
            {

               // dt.Columns.Add("OrderDate");
                dt.Columns.Add("gia");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["gia"] = String.Format("{0:n}", Convert.ToInt64(dt.Rows[i]["P_Price"]));
                    //dt.Rows[i]["OrderDate"] = Convert.ToDateTime(dt.Rows[i]["O_Date"]).ToString("dd/MM/yyyy HH:mm:ss");
                }
                dt.AcceptChanges();
            }
            return dt;
        }
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable GetOrderIsPaid()
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.SelectOrderIsPaid();
            }
            if (dt != null && dt.Rows.Count > 0)
            {

                dt.Columns.Add("OrderDate");
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    dt.Rows[i]["OrderDate"] = Convert.ToDateTime(dt.Rows[i]["O_Date"]).ToString("dd/MM/yyyy HH:mm:ss");
                }
                dt.AcceptChanges();
            }
            return dt;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <returns>Danh sach cac don han het han thanh toan</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable GetOrderOverRequiredDate()
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.SelectOrderOverReQuiredDate();
            }
            if (dt != null && dt.Rows.Count > 0)
            {

                dt.Columns.Add("OrderDate");
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    dt.Rows[i]["OrderDate"] = Convert.ToDateTime(dt.Rows[i]["O_Date"]).ToString("dd/MM/yyyy HH:mm:ss");
                }
                dt.AcceptChanges();
            }
            return dt;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <returns>Danh sach cac don hang trong han thanh toan</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable GetOrderInRequiredDate()
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.SelectOrderInReQuiredDate();
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                
                dt.Columns.Add("OrderDate");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                   
                    dt.Rows[i]["OrderDate"] = Convert.ToDateTime(dt.Rows[i]["O_Date"]).ToString("dd/MM/yyyy HH:mm:ss");
                }
                dt.AcceptChanges();
            }
            return dt;
        }

        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <returns>Danh sach cac don hang vua dc gui vao dang cho duyet</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable GetNewOrder()
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.SelectNewOrder();
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                //dt.Columns.Add("CurrencyValue");
                dt.Columns.Add("OrderDate");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //dt.Rows[i]["TongTien"] = String.Format("{0:n}", Convert.ToInt64(dt.Rows[i]["O_Total"]));
                    dt.Rows[i]["OrderDate"] = Convert.ToDateTime(dt.Rows[i]["O_Date"]).ToString("dd/MM/yyyy HH:mm:ss");
                }
                dt.AcceptChanges();
            }
            return dt;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="O_ID">Xoa don hang</param>
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteOrderByID(string O_ID)
        {
            
            using (MainDB db = new MainDB())
            {
                db.StoredProcedures.DeletetblOrder(O_ID);
            }
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="O_ID">Chuyen don hang sang trang thay cho thanh toan</param>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateIsRemoveeOrderByID(string O_ID)
        {

            using (MainDB db = new MainDB())
            {
                db.StoredProcedures.UpdateIsRemovetblOrder(O_ID);
            }
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="pass"></param>
        /// <param name="tel"></param>
        /// <param name="address"></param>
        /// <param name="job"></param>
        /// <param name="name"></param>
        /// <param name="mail"></param>
        /// <returns>Insert thong tin cua khach hang va tra ra ID cua khach hang day</returns>
        public DataTable InsertCustomer(string pass,string tel,string address,string job,string name,string mail)
        {
            DataTable dt;
            using(MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.InserttblCustomer(pass, tel, address, job, name, mail);
            }
            return dt;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="O_ID"></param>
        /// <param name="suggest"></param>
        /// <param name="C_ID"></param>
        /// <param name="total"></param>
        /// <returns>Insert don hang cua 1 khach hang va tra ra ID cua don hang day</returns>
        public DataTable InsertOrder(Int64 O_ID,string suggest,int C_ID,string total)
        {
            DataTable dt;
            DateTime o_RequiredDate = DateTime.Now.AddDays(4);
            using(MainDB db=new MainDB())
            {
                dt = db.StoredProcedures.InserttblOrder(O_ID, DateTime.Now, false, suggest, C_ID, false, total, o_RequiredDate);
            }
            return dt;
        }
        /// <summary>
        /// Created By DungTT
        /// </summary>
        /// <param name="O_ID"></param>
        /// <param name="P_Price"></param>
        /// <param name="P_quantity"></param>
        /// <param name="P_ID"></param>
        /// <returns>Insert chi tiet cua don hang</returns>
        public DataTable InsertOrderDetail(Int64 O_ID, string P_Price,int P_quantity, int P_ID)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.InserttblOrderDetail(P_Price, P_quantity, P_ID, O_ID);
            }
            return dt;
        }
    }
}
