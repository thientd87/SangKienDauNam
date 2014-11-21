using System;

using System.Data;
using DFISYS.Core.DAL;


namespace Portal.BO.Editoral.ThongKeBaiViet
{
    public class ThongKeBaiVietHelper
    {
        public DataTable GetReportNewsByCat(string fromdate, string todate)
        {
            DataTable dt;
            using (MainDB ndb = new MainDB())
            {
                dt = ndb.StoredProcedures.tdt_ReportNewsByCat(fromdate, todate);
                if(dt!=null&&dt.Rows.Count>0)
                {
                    if(!dt.Columns.Contains("ValuePercent")) dt.Columns.Add("ValuePercent");
                    for(int i=0;i<dt.Rows.Count;i++)
                    {
                        dt.Rows[i]["ValuePercent"] =
                            Math.Round(
                                Convert.ToDouble(dt.Rows[i]["CountID"]) /
                                Convert.ToDouble(dt.Rows[i]["Sum_Cat"]) * 100, 2);
                    }
                    dt.AcceptChanges();
                }
            }
            return dt;
        }
        public DataTable GetReportPageViewByCat(DateTime fromdate, DateTime todate)
        {
            using (MainDB ndb = new MainDB())
            {
                return ndb.StoredProcedures.tdt_ReportPageViewByCat(fromdate, todate);
            }
        }
     
        public DataTable ThongKeLuongBaiViet(DateTime fromdate, DateTime todate)
        {
            using (MainDB ndb = new MainDB())
            {
                return ndb.StoredProcedures.ThongKeLuongBaiViet(fromdate, todate);
            }
        }

        public DataTable ThongKeBaiVietV2(string fromdate, string todate, int Cat_ID, int Extension)
        {
            using (MainDB ndb = new MainDB())
            {
                return ndb.StoredProcedures.tdt_ReportNews(fromdate, todate,Cat_ID,Extension);
            }
        }

        public DataTable ThongKeBaiXemNhieuNhat(string fromdate, string todate, int Cat_ID, int Top)
        {
            using (MainDB ndb = new MainDB())
            {
                return ndb.StoredProcedures.tdt_ReportBaiXemNhieuNhat(fromdate, todate, Cat_ID, Top);
            }
        }

        public DataTable ThongKeBaiTheoTacGia(string fromdate, string todate, int Cat_ID, int Top)
        {
            using (MainDB ndb = new MainDB())
            {
                return ndb.StoredProcedures.tdt_ReportBaiTheoTacGia(fromdate, todate, Cat_ID, Top);
            }
        }

        public DataTable ThongKeTheoTungBai(string fromdate, string todate, string username)
        {
            using (MainDB ndb = new MainDB())
            {
                return ndb.StoredProcedures.tdt_ReportTheoTungBai(fromdate, todate, username);
            }
        }

        public DataTable ThongKeTheoTungBaiTheoChuyenMuc(string fromdate, string todate, int cateID, int sortOrder)
        {
            using (MainDB ndb = new MainDB())
            {
                return ndb.StoredProcedures.tdt_ReportTheoTungBaiTheoChuyenMuc(fromdate, todate, cateID, sortOrder);
            }
        }

        public DataTable ThongKeBaiTheoChiTietTacGia(string fromdate, string todate, int Cat_ID, int Top, string user, int check, int dateCount)
        {
            using (MainDB ndb = new MainDB())
            {
                return ndb.StoredProcedures.tdt_ReportBaiTheoChiTietTacGia(fromdate, todate, Cat_ID, Top, user, check, dateCount);
            }
        }

        public DataTable ThongKeBaiTheoChuyenMuc(string fromdate, string todate, int Cat_ID, int Top)
        {
            using (MainDB ndb = new MainDB())
            {
                return ndb.StoredProcedures.tdt_ReportBaiTheoChuyenMuc(fromdate, todate, Cat_ID, Top);
            }
        }

        public DataTable ThongKeBaiTheoChuyenMucDeQuy(string fromdate, string todate, int Cat_ID, int ParentID)
        {
            using (MainDB ndb = new MainDB())
            {
                return ndb.StoredProcedures.tdt_ReportBaiTheoChuyenMucDeQuy(fromdate, todate, Cat_ID, ParentID);
            }
        }

        public DataTable ThongKeBaiTheoGioXuatBanTheoChuyenMuc(string fromdate, string todate, int Cat_ID, int SortOrder, int dateCount)
        {
            using (MainDB ndb = new MainDB())
            {
                return ndb.StoredProcedures.tdt_ReportBaiTheoGioXuatBanTheoChuyeMuc(fromdate, todate, Cat_ID, SortOrder, dateCount);
            }
        }

        public DataTable ThongKeBaiTheoGioXuatBanTheoChuyenMucTongThe(string fromdate, string todate, int Cat_ID, int SortOrder, int dateCount)
        {
            using (MainDB ndb = new MainDB())
            {
                return ndb.StoredProcedures.tdt_ReportBaiTheoGioXuatBanTheoChuyeMucTongThe(fromdate, todate, Cat_ID, SortOrder, dateCount);
            }
        }

        public DataTable ThongKeBaiTheoGioXuatBan(string fromdate, string todate, string user, int dateCount, int cateID)
        {
            using (MainDB ndb = new MainDB())
            {
                return ndb.StoredProcedures.tdt_ReportBaiTheoGioXuatBan(fromdate, todate, user, dateCount, cateID);
            }
        }

        public DataTable ThongKeBaiTheoGioTao(string fromdate, string todate, string user, int dateCount, int cateID)
        {
            using (MainDB ndb = new MainDB())
            {
                return ndb.StoredProcedures.tdt_ReportBaiTheoGioTao(fromdate, todate, user, dateCount, cateID);
            }
        }

        public DataTable ThongKeBaiVietPageView(string fromdate, string todate, int Cat_ID, int Extension)
        {
            using (MainDB ndb = new MainDB())
            {
                return ndb.StoredProcedures.tdt_ReportNewsPageView(fromdate, todate, Cat_ID, Extension);
            }
        }
        //public DataTable ThongKeComment(DateTime fromdate, DateTime todate)
        //{
        //    DataTable dt;
        //    using (MainDB ndb = new MainDB())
        //    {
        //        dt= ndb.StoredProcedures.tdt_ReportNewsByComment(fromdate, todate);
        //    }
        //    if(dt!=null&&dt.Rows.Count>0)
        //    {
        //        if(!dt.Columns.Contains("newsURL")) dt.Columns.Add("newsURL");
        //        for(int i=0;i<dt.Rows.Count;i++)
        //        {

        //            dt.Rows[i]["newsURL"] = String.Format("http://vietpress.vn/{0}p{1}c{2}/{3}.htm",
        //                                                  dt.Rows[i]["News_ID"], dt.Rows[i]["Cat_ParentID"],
        //                                                  dt.Rows[i]["Cat_ID"],
        //                                                  BO.UnicodeUtility.UnicodeToKoDauAndGach(
        //                                                      dt.Rows[i]["News_Title"].ToString()));
        //        }
        //    }
        //    dt.AcceptChanges();
        //    return dt;
        //}
    }
}