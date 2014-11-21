using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DFISYS.User.Db;

namespace DFISYS.BO.CoreBO
{
    public class Comment
    {
        public Int64 Comment_ID { get; set; }
        public Int64 News_ID { get; set; }
        public string Avartar { get; set; }
        public string Comment_User { get; set; }
        public string Comment_Content { get; set; }
        public DateTime Comment_Date { get; set; }
        public bool Comment_Approved { get; set; }
        public string Comment_Email { get; set; }
        public string Approver { get; set; }
        public int Comment_Type { get; set; }
        public DateTime Comment_Approved_Date { get; set; }

        public Comment()
        {
            this.Comment_Approved = false;
            this.Approver = string.Empty;
            this.Avartar = string.Empty;
            this.Comment_Content = string.Empty;
            this.Comment_Date = DateTime.Now;
            this.Comment_Email = string.Empty;
            this.Comment_ID = 0;
            this.Comment_Type = 0;
            this.Comment_User = string.Empty;
            this.Comment_Approved_Date = DateTime.Now;
        }

        public Comment(Int64 Comment_ID)
        {
            this.Comment_Approved = false;
            this.Approver = string.Empty;
            this.Avartar = string.Empty;
            this.Comment_Content = string.Empty;
            this.Comment_Date = DateTime.Now;
            this.Comment_Email = string.Empty;
            this.Comment_ID = Comment_ID;
            this.Comment_Type = 0;
            this.Comment_User = string.Empty;
            this.Comment_Approved_Date = DateTime.Now;
        }
        

        public DataTable GetAll(int pageSize, int pageIndex, int commentType, int commentApproved, string keyword)
        {
            keyword = keyword.Replace(" ", "+");
            using (MainDB db = new MainDB())
            {
                return db.CallStoredProcedure("CMS_Comment_Search",
                     new object[] { commentType, pageIndex, pageSize, commentApproved, keyword },
                     new string[] { "@Comment_Type", "@pageIndex", "@pageSize", "@Comment_Approved", "@keyword" },
                     true);
            }
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commentType"></param>
        /// <param name="commentApproved"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public int Count(int commentType, int commentApproved, string keyword)
        {
            keyword = keyword.Replace(" ", "+");
            DataTable tbl = null;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_Comment_Search_Count",
                     new object[] { commentType, commentApproved, keyword },
                     new string[] { "@Comment_Type", "@Comment_Approved", "@keyword" },
                     true);
            }

            return tbl != null ? Convert.ToInt32(tbl.Rows[0][0]) : 0;
        }

        //CMS_Comment_GetOne

        public Comment GetOne()
        {

            DataTable tbl;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_Comment_GetOne",
                     new object[] { this.Comment_ID },
                     new string[] { "@Comment_ID" },
                     true);
            }

            if (tbl != null && tbl.Rows.Count > 0)
            {
                return MapObject(tbl.Rows[0]);
            }

            return null;

        }

        private Comment MapObject(DataRow row)
        {
            return new Comment()
                       {
                           Comment_Approved = row["Comment_Approved"] != DBNull.Value && Convert.ToBoolean(row["Comment_Approved"]),
                           Comment_Content = row["Comment_Content"] != null ? row["Comment_Content"].ToString() : string.Empty,
                           Comment_Date = row["Comment_Date"] != null ? Convert.ToDateTime(row["Comment_Date"]) : DateTime.Now,
                           Comment_Email = row["Comment_Email"] != null ? row["Comment_Email"].ToString() : string.Empty,
                           Comment_ID = row["Comment_ID"] != null ? Convert.ToInt64(row["Comment_ID"]) : 0,
                           Comment_Type = row["Comment_Type"] != null ? Convert.ToInt32(row["Comment_Type"]) : 0,
                           Approver = row["Approver"] != null ? row["Approver"].ToString() : string.Empty,
                           Avartar = row["Avartar"] != null ? row["Avartar"].ToString() : string.Empty,
                           Comment_User = row["Comment_User"] != null ? row["Comment_User"].ToString() : string.Empty,
                           Comment_Approved_Date = row["Comment_Approved_Date"] != null ? Convert.ToDateTime(row["Comment_Approved_Date"]) : DateTime.Now
                       };
        }

        public void Insert()
        {

        }

        public void Update()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_Comment_Update",
                     new object[] { Comment_Approved, Comment_Content, Comment_Date, Comment_Email, Comment_ID, Comment_Type, Approver, Avartar, Comment_User, Comment_Approved_Date },
                     new string[] { "@Comment_Approved", "@Comment_Content", "@Comment_Date", "@Comment_Email", "@Comment_ID", "@Comment_Type", "@Approver", "@Avartar", "@Comment_User", "@Comment_Approved_Date" },
                     true);
            }
        }

        public void Delete()
        {
            if (this.Comment_ID > 0)
            {
                using (MainDB db = new MainDB())
                {
                    db.CallStoredProcedure("CMS_Comment_Delete", new object[] { this.Comment_ID }, new string[] { "@Comment_ID" }, false);
                }
            }
        }
    }
}