using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DFISYS.User.Db;

namespace DFISYS.BO.CoreBO
{
    public class DatMuaBanTinVang
    {
        public Int64 UserID { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int TimeType { get; set; }
        public int BuyType { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime ActiveDate { get; set; }
        public DatMuaBanTinVang(Int64 userid, string name, string company, string email, string address, int timetype, int buytype, DateTime registerdate, DateTime activedate)
        {
            this.UserID = userid;
            this.Name = name;
            this.Company = company;
            this.Email = email;
            this.Address = address;
            this.TimeType = timetype;
            this.BuyType = buytype;
            this.RegisterDate = registerdate;
            this.ActiveDate = activedate;
        }
        public DatMuaBanTinVang()
        { }
        ///  MapObject DatMuaBanTinVang-------------------------------------------------------
        private DatMuaBanTinVang MapObject(DataRow row)
        {
            return new DatMuaBanTinVang()
            {
                UserID = row["UserID"] != DBNull.Value ? Convert.ToInt64(row["UserID"]) : 0,
                Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : string.Empty,
                Company = row["Company"] != DBNull.Value ? row["Company"].ToString() : string.Empty,
                Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : string.Empty,
                Address = row["Address"] != DBNull.Value ? row["Address"].ToString() : string.Empty,
                TimeType = row["TimeType"] != DBNull.Value ? Convert.ToInt32(row["TimeType"]) : 0,
                BuyType = row["BuyType"] != DBNull.Value ? Convert.ToInt32(row["BuyType"]) : 0,
                RegisterDate = row["RegisterDate"] != DBNull.Value ? Convert.ToDateTime(row["RegisterDate"]) : DateTime.Now,
                ActiveDate = row["ActiveDate"] != DBNull.Value ? Convert.ToDateTime(row["ActiveDate"]) : DateTime.Now
            };
        }
        /// SelectAllCount DatMuaBanTinVang------------------------------------------------------- 
        public int SelectAllCount(string keyword)
        {
            keyword = keyword.Replace(" ", "+");
            int r = 0;
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_DatMuaBanTinVang_SelectAllCount", new object[] { keyword }, new string[] { "@keyword" }, true);
                r = dt != null ? Convert.ToInt32(dt.Rows[0][0]) : 0;
            }
            return r;
        }
        ///  SelectAllSearch DatMuaBanTinVang-------------------------------------------------------
        public List<DatMuaBanTinVang> SelectAllSearch(int pageSize, int pageIndex, string keyword)
        {
            List<DatMuaBanTinVang> ls = new List<DatMuaBanTinVang>();
            keyword = keyword.Replace(" ", "+");
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_DatMuaBanTinVang_Search", new object[] { pageIndex, pageSize, keyword }, new string[] { "@pageIndex", "@pageSize", "@keyword" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }

        /// SelectAll DatMuaBanTinVang------------------------------------------------------- 
        public List<DatMuaBanTinVang> SelectAllLike(string keyword)
        {
            List<DatMuaBanTinVang> ls = new List<DatMuaBanTinVang>();
            keyword = keyword.Replace('"', ' ');
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_DatMuaBanTinVang_SelectAllLike", new object[] { keyword }, new string[] { "@keyword" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        ///  Insert DatMuaBanTinVang-------------------------------------------------------
        public void Insert()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_DatMuaBanTinVang_Insert", new object[] { Name, Company, Email, Address, TimeType, BuyType, RegisterDate, ActiveDate }, new string[] { "@Name", "@Company", "@Email", "@Address", "@TimeType", "@BuyType", "@RegisterDate", "@ActiveDate" }, false);
            }
        }
        ///  Update DatMuaBanTinVang-------------------------------------------------------
        public void Update()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_DatMuaBanTinVang_Update", new object[] { UserID, Name, Company, Email, Address, TimeType, BuyType, RegisterDate, ActiveDate }, new string[] { "@UserID", "@Name", "@Company", "@Email", "@Address", "@TimeType", "@BuyType", "@RegisterDate", "@ActiveDate" }, false);
            }
        }
        ///  Delete DatMuaBanTinVang-------------------------------------------------------
        public void Delete()
        {
            if (UserID > 0)
            {
                using (MainDB db = new MainDB())
                {
                    db.CallStoredProcedure("CMS_DatMuaBanTinVang_Delete", new object[] { UserID }, new string[] { "@UserID" }, false);
                }
            }
        }
        ///  SelectOne DatMuaBanTinVang-------------------------------------------------------
        public DatMuaBanTinVang SelectOne()
        {
            DataTable tbl;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_DatMuaBanTinVang_SelectOne", new object[] { UserID }, new string[] { "@UserID" }, true);
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    return MapObject(tbl.Rows[0]);
                }
            }
            return null;
        }
    }

}