using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DFISYS.User.Db;


namespace DFISYS.BO.CoreBO
{
    public class ReportPageView
    {
        public int PageView_ID { get; set; }
        public int PageView_Cat { get; set; }
        public DateTime PageView_Date { get; set; }
        public int PageView_Hours { get; set; }
        public int PageView_Count { get; set; }
        public ReportPageView(int pageview_id, int pageview_cat, DateTime pageview_date, int pageview_hours, int pageview_count)
        {
            this.PageView_ID = pageview_id;
            this.PageView_Cat = pageview_cat;
            this.PageView_Date = pageview_date;
            this.PageView_Hours = pageview_hours;
            this.PageView_Count = pageview_count;
        }
        public ReportPageView()
        { }
        ///  MapObject ReportPageView-------------------------------------------------------
        private ReportPageView MapObject(DataRow row)
        {
            return new ReportPageView()
            {
                PageView_ID = row["PageView_ID"] != null ? Convert.ToInt32(row["PageView_ID"]) : 0,
                PageView_Cat = row["PageView_Cat"] != null ? Convert.ToInt32(row["PageView_Cat"]) : 0,
                PageView_Date = row["PageView_Date"] != null ? Convert.ToDateTime(row["PageView_Date"]) : DateTime.Now,
                PageView_Hours = row["PageView_Hours"] != null ? Convert.ToInt32(row["PageView_Hours"]) : 0,
                PageView_Count = row["PageView_Count"] != null ? Convert.ToInt32(row["PageView_Count"]) : 0
            };
        }
        ///  SelectAllSearch ReportPageView-------------------------------------------------------
        public List<ReportPageView> SelectAllSearch(int pageSize, int pageIndex, string keyword)
        {
            List<ReportPageView> ls = new List<ReportPageView>();
            keyword = keyword.Replace(" ", "+");
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_ReportPageView_Search", new object[] { pageIndex, pageSize, keyword }, new string[] { "@pageIndex", "@pageSize", "@keyword" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        ///  SelectAllSearch ReportPageView-------------------------------------------------------
        public List<ReportPageView> Select(DateTime firstDate, DateTime endDate, string listCat)
        {
            List<ReportPageView> ls = new List<ReportPageView>();
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_ReportPageView_Select", new object[] { firstDate, endDate, listCat }, new string[] { "@startDate", "@endDate", "@listCat" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        ///  Insert ReportPageView-------------------------------------------------------
        public void Insert()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_ReportPageView_Insert", new object[] { PageView_Cat, PageView_Date, PageView_Hours, PageView_Count }, new string[] { "@PageView_Cat", "@PageView_Date", "@PageView_Hours", "@PageView_Count" }, false);
            }
        }
        ///  Update ReportPageView-------------------------------------------------------
        public void Update()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_ReportPageView_Update", new object[] { PageView_ID, PageView_Cat, PageView_Date, PageView_Hours, PageView_Count }, new string[] { "@PageView_ID", "@PageView_Cat", "@PageView_Date", "@PageView_Hours", "@PageView_Count" }, false);
            }
        }
        ///  Delete ReportPageView-------------------------------------------------------
        public void Delete()
        {
            if (PageView_ID > 0)
            {
                using (MainDB db = new MainDB())
                {
                    db.CallStoredProcedure("CMS_ReportPageView_Delete", new object[] { PageView_ID }, new string[] { "@PageView_ID" }, false);
                }
            }
        }
        ///  SelectOne ReportPageView-------------------------------------------------------
        public ReportPageView SelectOne()
        {
            DataTable tbl;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_ReportPageView_SelectOne", new object[] { PageView_ID }, new string[] { "@PageView_ID" }, true);
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    return MapObject(tbl.Rows[0]);
                }
            }
            return null;
        }
    }

}