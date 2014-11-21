using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BaseModel;
using DFISYS.Core.DAL;
using DFISYS.GUI.EditoralOffice.MainOffce.Tool;

namespace DFISYS.BO
{
    public class Gallery : Model_Base
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Gallery(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
        public Gallery()
        { }

        ///  MapObject Gallery-------------------------------------------------------
        private Gallery MapObject(DataRow row)
        {
            return new Gallery()
            {
                ID = row["ID"] != DBNull.Value ? Convert.ToInt32(row["ID"]) : 0,
                Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : string.Empty
            };
        }

        /// SelectAllCount Gallery------------------------------------------------------- 
        public int SelectAllCount(string keyword)
        {
            keyword = keyword.Replace(" ", "+");
            int r = 0;
            using (MainDB db = new MainDB())
            {
                DataTable dt = (DataTable) db.CallStoredProcedure("CMS_Gallery_SearchCount", new object[] { keyword }, new string[] { "@keyword" }, true);
                r = dt != null ? Convert.ToInt32(dt.Rows[0][0]) : 0;
            }
            return r;
        }
        ///  SelectAllSearch Gallery-------------------------------------------------------
        public List<Gallery> SelectAllSearch(int pageSize, int pageIndex, string keyword)
        {
            List<Gallery> ls = new List<Gallery>();
            keyword = keyword.Replace(" ", "+");
            using (MainDB db = new MainDB())
            {
                DataTable dt = (DataTable)db.CallStoredProcedure("CMS_Gallery_Search", new object[] { pageIndex, pageSize, keyword }, new string[] { "@pageIndex", "@pageSize", "@keyword" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        //select mediaobj by GalleryID
        public List<MediaObject> selectbyGalleryID(int ID)
        {
            List<MediaObject> result;
            using (var db = new MainDB())
            {
                var dt = (DataTable)db.CallStoredProcedure("CMS_Gallery_GetbyID", new object[] { ID }, new string[] { "@ID" }, true);

                result= MediaObject.DataTableTpObj(dt);
            }          
            return result;
        }

        /// SelectAll Gallery------------------------------------------------------- 
        public List<Gallery> SelectAllLike(string keyword)
        {
            List<Gallery> ls = new List<Gallery>();
            keyword = keyword.Replace('"', ' ');
            using (MainDB db = new MainDB())
            {
                DataTable dt = (DataTable)db.CallStoredProcedure("CMS_Gallery_SelectAllLike", new object[] { keyword }, new string[] { "@keyword" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        ///  Insert Gallery-------------------------------------------------------
        public int Insert()
        {
            int result = 0;
            using (MainDB db = new MainDB())
            {
                DataTable tbl = db.CallStoredProcedure("CMS_Gallery_Insert", new object[] { Name }, new string[] { "@Name" }, true) as DataTable;
                if (tbl != null && tbl.Rows.Count>0)
                {
                    result = (int) tbl.Rows[0]["ID"];
                }
            }
            return result;
        }
        ///  Update Gallery-------------------------------------------------------
        public void Update()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_Gallery_Update", new object[] { ID, Name }, new string[] { "@ID", "@Name" }, false);
            }
        }
        ///  Delete Gallery-------------------------------------------------------
        public void Delete()
        {
            if (ID > 0)
            {
                using (MainDB db = new MainDB())
                {
                    db.CallStoredProcedure("CMS_Gallery_Delete", new object[] { ID }, new string[] { "@ID" }, false);
                }
            }
        }
        ///  SelectOne Gallery-------------------------------------------------------
        public Gallery SelectOne()
        {
            DataTable tbl;
            using (MainDB db = new MainDB())
            {
                tbl = (DataTable) db.CallStoredProcedure("CMS_Gallery_SelectOne", new object[] { ID }, new[] { "@ID" }, true);
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    return MapObject(tbl.Rows[0]);
                }
            }
            return null;
        }
    }

}
