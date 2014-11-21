using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DFISYS.User.Db;


namespace DFISYS.BO.CoreBO
{
    public class Vote
    {
        public int Vote_ID { get; set; }
        public string Vote_Title { get; set; }
        public DateTime Vote_StartDate { get; set; }
        public DateTime Vote_EndDate { get; set; }
        public string Vote_InitContent { get; set; }
        public int Cat_ID { get; set; }
        public bool isActive { get; set; }
        public Vote(int vote_id, string vote_title, DateTime vote_startdate, DateTime vote_enddate, string vote_initcontent, int cat_id, bool isactive)
        {
            this.Vote_ID = vote_id;
            this.Vote_Title = vote_title;
            this.Vote_StartDate = vote_startdate;
            this.Vote_EndDate = vote_enddate;
            this.Vote_InitContent = vote_initcontent;
            this.Cat_ID = cat_id;
            this.isActive = isactive;
        }
        public Vote()
        { }
        ///  MapObject Vote-------------------------------------------------------
        private Vote MapObject(DataRow row)
        {
            return new Vote()
            {
                Vote_ID = row["Vote_ID"] != null ? Convert.ToInt32(row["Vote_ID"]) : 0,
                Vote_Title = row["Vote_Title"] != null ? row["Vote_Title"].ToString() : string.Empty,
                Vote_StartDate = row["Vote_StartDate"] != null ? Convert.ToDateTime(row["Vote_StartDate"]) : DateTime.Now,
                Vote_EndDate = row["Vote_EndDate"] != null ? Convert.ToDateTime(row["Vote_EndDate"]) : DateTime.Now,
                Vote_InitContent = row["Vote_InitContent"] != null ? row["Vote_InitContent"].ToString() : string.Empty,
                Cat_ID = row["Cat_ID"] != null ? Convert.ToInt32(row["Cat_ID"]) : 0,
                isActive = row["isActive"] != null ? Convert.ToBoolean(row["isActive"]) : false
            };
        }
        ///  SelectAllSearch Vote-------------------------------------------------------
        public List<Vote> SelectAllSearch(int pageSize, int pageIndex, string keyword)
        {
            List<Vote> ls = new List<Vote>();
            keyword = keyword.Replace(" ", "+");
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_Vote_Search", new object[] { pageIndex, pageSize, keyword }, new string[] { "@pageIndex", "@pageSize", "@keyword" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        ///  Insert Vote-------------------------------------------------------
        public void Insert()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_Vote_Insert", new object[] { Vote_Title, Vote_StartDate, Vote_EndDate, Vote_InitContent, Cat_ID, isActive }, new string[] { "@Vote_Title", "@Vote_StartDate", "@Vote_EndDate", "@Vote_InitContent", "@Cat_ID", "@isActive" }, false);
            }
        }
        ///  Update Vote-------------------------------------------------------
        public void Update()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_Vote_Update", new object[] { Vote_ID, Vote_Title, Vote_StartDate, Vote_EndDate, Vote_InitContent, Cat_ID, isActive }, new string[] { "@Vote_ID", "@Vote_Title", "@Vote_StartDate", "@Vote_EndDate", "@Vote_InitContent", "@Cat_ID", "@isActive" }, false);
            }
        }
        ///  Delete Vote-------------------------------------------------------
        public void Delete()
        {
            if (Vote_ID > 0)
            {
                using (MainDB db = new MainDB())
                {
                    db.CallStoredProcedure("CMS_Vote_Delete", new object[] { Vote_ID }, new string[] { "@Vote_ID" }, false);
                }
            }
        }
        ///  SelectOne Vote-------------------------------------------------------
        public Vote SelectOne()
        {
            DataTable tbl;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_Vote_SelectOne", new object[] { Vote_ID }, new string[] { "@Vote_ID" }, true);
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    return MapObject(tbl.Rows[0]);
                }
            }
            return null;
        }
    }
    public class Vote_Item
    {
        public int VoteIt_ID { get; set; }
        public string VoteIt_Content { get; set; }
        public int Vote_ID { get; set; }
        public decimal VoteIt_Rate { get; set; }
        public int VoteIt_STT { get; set; }
        public Vote_Item(int voteit_id, string voteit_content, int vote_id, decimal voteit_rate, int voteit_stt)
        {
            this.VoteIt_ID = voteit_id;
            this.VoteIt_Content = voteit_content;
            this.Vote_ID = vote_id;
            this.VoteIt_Rate = voteit_rate;
            this.VoteIt_STT = voteit_stt;
        }
        public Vote_Item()
        { }
        ///  MapObject Vote_Item-------------------------------------------------------
        private Vote_Item MapObject(DataRow row)
        {
            return new Vote_Item()
            {
                VoteIt_ID = row["VoteIt_ID"] != null ? Convert.ToInt32(row["VoteIt_ID"]) : 0,
                VoteIt_Content = row["VoteIt_Content"] != null ? row["VoteIt_Content"].ToString() : string.Empty,
                Vote_ID = row["Vote_ID"] != null ? Convert.ToInt32(row["Vote_ID"]) : 0,
                VoteIt_Rate = row["VoteIt_Rate"] != null ? Convert.ToDecimal(row["VoteIt_Rate"]) : 0,
                VoteIt_STT = row["VoteIt_STT"] != null ? Convert.ToInt32(row["VoteIt_STT"]) : 0
            };
        }
        ///  SelectAllSearch Vote_Item-------------------------------------------------------
        public List<Vote_Item> SelectAllSearch(int pageSize, int pageIndex, string keyword)
        {
            List<Vote_Item> ls = new List<Vote_Item>();
            keyword = keyword.Replace(" ", "+");
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_Vote_Item_Search", new object[] { pageIndex, pageSize, keyword }, new string[] { "@pageIndex", "@pageSize", "@keyword" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        ///  Insert Vote_Item-------------------------------------------------------
        public void Insert()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_Vote_Item_Insert", new object[] { VoteIt_Content, Vote_ID, VoteIt_Rate, VoteIt_STT }, new string[] { "@VoteIt_Content", "@Vote_ID", "@VoteIt_Rate", "@VoteIt_STT" }, false);
            }
        }
        ///  Update Vote_Item-------------------------------------------------------
        public void Update()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_Vote_Item_Update", new object[] { VoteIt_ID, VoteIt_Content, Vote_ID, VoteIt_Rate, VoteIt_STT }, new string[] { "@VoteIt_ID", "@VoteIt_Content", "@Vote_ID", "@VoteIt_Rate", "@VoteIt_STT" }, false);
            }
        }
        ///  Delete Vote_Item-------------------------------------------------------
        public void Delete()
        {
            if (VoteIt_ID > 0)
            {
                using (MainDB db = new MainDB())
                {
                    db.CallStoredProcedure("CMS_Vote_Item_Delete", new object[] { VoteIt_ID }, new string[] { "@VoteIt_ID" }, false);
                }
            }
        }
        ///  SelectOne Vote_Item-------------------------------------------------------
        public Vote_Item SelectOne()
        {
            DataTable tbl;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_Vote_Item_SelectOne", new object[] { VoteIt_ID }, new string[] { "@VoteIt_ID" }, true);
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    return MapObject(tbl.Rows[0]);
                }
            }
            return null;
        }

        public List<Vote_Item> SelectAllByVoteID(int voteID)
        {
            List<Vote_Item> ls = new List<Vote_Item>();
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_Vote_Item_SelectByVoteID", new object[] { voteID }, new string[] { "@Vote_ID" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
    }

}