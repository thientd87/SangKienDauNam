using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DFISYS.User.Db;


namespace DFISYS.BO.CoreBO
{
    public class WebLink
    {
        public int WebLink_ID{ get; set; }
        public string WebLink_URL{ get; set; }
        public string WebLink_Name{ get; set; }
        public string WebLink_Description{ get; set; }
        public int WebLink_Order{ get; set; }
        public WebLink(int weblink_id, string weblink_url, string weblink_name, string weblink_description, int weblink_order)
        {
            this.WebLink_ID = weblink_id;
            this.WebLink_URL = weblink_url;
            this.WebLink_Name = weblink_name; 
            this.WebLink_Description = weblink_description; 
            this.WebLink_Order = weblink_order;
        }
        
        public WebLink()
        {
            this.WebLink_ID = 0;
            this.WebLink_URL = "";
            this.WebLink_Name = "";
            this.WebLink_Description = "";
            this.WebLink_Order = 0;
        }

        ///  MapObject WebLink-------------------------------------------------------
        private WebLink MapObject(DataRow row)
        {
            return new WebLink()
            {
                WebLink_ID = row["WebLink_ID"] != null ? Convert.ToInt32(row["WebLink_ID"]) : 0,
                WebLink_URL = row["WebLink_URL"] != null ? row["WebLink_URL"].ToString() : string.Empty,
                WebLink_Name = row["WebLink_Name"] != null ? row["WebLink_Name"].ToString() : string.Empty,
                WebLink_Description = row["WebLink_Description"] != null ? row["WebLink_Description"].ToString() : string.Empty,
                WebLink_Order = row["WebLink_Order"] != null ? Convert.ToInt32(row["WebLink_Order"]) : 0
            };
        }
        ///  SelectAllSearch WebLink-------------------------------------------------------
        public List<WebLink> SelectAllSearch(int pageSize, int pageIndex, string keyword)
        {
            List<WebLink> ls = new List<WebLink>();
            keyword = "%" + keyword.Trim() + "%";
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_WebLink_Search", new object[] { pageIndex, pageSize, keyword }, new string[] { "@pageIndex", "@pageSize", "@keyword" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        ///  Insert WebLink-------------------------------------------------------
        public void Insert()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_WebLink_Insert", new object[] { WebLink_URL, WebLink_Name, WebLink_Description, WebLink_Order }, new string[] { "@WebLink_URL", "@WebLink_Name", "@WebLink_Description", "@WebLink_Order" }, false);
            }
        }
        ///  Update WebLink-------------------------------------------------------
        public void Update()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_WebLink_Update", new object[] { WebLink_ID, WebLink_URL, WebLink_Name, WebLink_Description, WebLink_Order }, new string[] { "@WebLink_ID", "@WebLink_URL", "@WebLink_Name", "@WebLink_Description", "@WebLink_Order" }, false);
            }
        }
        ///  Delete WebLink-------------------------------------------------------
        public void Delete()
        {
            if (WebLink_ID > 0)
            {
                using (MainDB db = new MainDB())
                {
                    db.CallStoredProcedure("CMS_WebLink_Delete", new object[] { WebLink_ID }, new string[] { "@WebLink_ID" }, false);
                }
            }
        }
        ///  SelectOne WebLink-------------------------------------------------------
        public WebLink SelectOne()
        {
            DataTable tbl;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_WebLink_SelectOne", new object[] { WebLink_ID }, new string[] { "@WebLink_ID" }, true);
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    return MapObject(tbl.Rows[0]);
                }
            }
            return null;
        }
    }
}