using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using Portal.Core.DAL;

namespace Portal.BO.Editoral.TinDoanhNghiep
{
    public class TinDoanhNghiep
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static DataTable GetListThongTinDoanhNghiep()
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.NC_SelectThongTinDoanhNghiep();
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("STT");
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    dt.Rows[i - 1]["STT"] = i;
                }
                dt.AcceptChanges();
            }
            return dt;
        }

        public static void InsertThongTinDoanhNghiep(long News_ID, bool isActive)
        {
            using (MainDB db = new MainDB())
            {
                 db.StoredProcedures.NC_InsertThongTinDoanhNghiep(News_ID,isActive);
            }
        }
        public static void UpdateThongTinDoanhNghiep(int ID, bool isActive, int Order)
        {

            using (MainDB db = new MainDB())
            {
                db.StoredProcedures.NC_UpdateThongTinDoanhNghiep(ID, isActive, Order);
            }
        }

        public static void DelteThongTinDoanhNghiep(int ID)
        {
            using (MainDB db = new MainDB())
            {
                db.StoredProcedures.NC_DeleteThongTinDoanhNghiep(ID);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable GetNewsList(Int32 CatID, string condition)
        {
            if (CatID != 0)
                condition += " and (N.Cat_ID in(select Cat_ID from Category where Cat_ParentID = " + CatID + ") or N.Cat_ID =  " + CatID + ")";

            using (MainDB db = new MainDB())
            {
                return db.StoredProcedures.vc_Execute_Sql("SELECT TOP (1000) N.*,C.Cat_Name FROM News as N inner join Category as C on C.Cat_ID = N.Cat_ID WHERE News_Status = 3 AND News_PublishDate < getdate() " + condition + " ORDER BY News_PublishDate DESC");
            }
        } 
    }
}