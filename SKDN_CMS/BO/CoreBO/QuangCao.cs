using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DFISYS.User.Db;

namespace DFISYS.BO.CoreBO
{
    public class Adv_Position
    {
        public int PosID { get; set; }
        public string PosName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ClassName { get; set; }
        public Adv_Position(int posid, string posname, int width, int height, string classname)
        {
            this.PosID = posid;
            this.PosName = posname;
            this.Width = width;
            this.Height = height;
            this.ClassName = classname;
        }
        public Adv_Position()
        { }
        ///  MapObject Adv_Position-------------------------------------------------------
        private Adv_Position MapObject(DataRow row)
        {
            return new Adv_Position()
            {
                PosID = row["PosID"] != DBNull.Value ? Convert.ToInt32(row["PosID"]) : 0,
                PosName = row["PosName"] != DBNull.Value ? row["PosName"].ToString() : string.Empty,
                Width = row["Width"] != DBNull.Value ? Convert.ToInt32(row["Width"]) : 0,
                Height = row["Height"] != DBNull.Value ? Convert.ToInt32(row["Height"]) : 0,
                ClassName = row["ClassName"] != DBNull.Value ? row["ClassName"].ToString() : string.Empty
            };
        }
       
        /// SelectAll Adv_Position------------------------------------------------------- 
        public List<Adv_Position> SelectAllLike(string keyword)
        {
            List<Adv_Position> ls = new List<Adv_Position>();
            keyword = keyword.Replace('"', ' ');
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_Adv_Position_SelectAllLike", new object[] { keyword }, new string[] { "@keyword" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        ///  Insert Adv_Position-------------------------------------------------------
        public void Insert()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_Adv_Position_Insert", new object[] { PosName, Width, Height, ClassName }, new string[] { "@PosName", "@Width", "@Height", "@ClassName" }, false);
            }
        }
        ///  Update Adv_Position-------------------------------------------------------
        public void Update()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_Adv_Position_Update", new object[] { PosID, PosName, Width, Height, ClassName }, new string[] { "@PosID", "@PosName", "@Width", "@Height", "@ClassName" }, false);
            }
        }
        ///  Delete Adv_Position-------------------------------------------------------
        public void Delete()
        {
            if (PosID > 0)
            {
                using (MainDB db = new MainDB())
                {
                    db.CallStoredProcedure("CMS_Adv_Position_Delete", new object[] { PosID }, new string[] { "@PosID" }, false);
                }
            }
        }
        ///  SelectOne Adv_Position-------------------------------------------------------
        public Adv_Position SelectOne()
        {
            DataTable tbl;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_Adv_Position_SelectOne", new object[] { PosID }, new string[] { "@PosID" }, true);
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    return MapObject(tbl.Rows[0]);
                }
            }
            return null;
        }
    }
    public class Adv_Page_Position
    {
        public int AdvID { get; set; }
        public int CatID { get; set; }
        public int PosID { get; set; }
        public Adv_Page_Position(int advid, int catid, int posid)
        {
            this.AdvID = advid;
            this.CatID = catid;
            this.PosID = posid;
        }
        public Adv_Page_Position()
        { }
        ///  MapObject Adv_Page_Position-------------------------------------------------------
        private Adv_Page_Position MapObject(DataRow row)
        {
            return new Adv_Page_Position()
            {
                AdvID = row["AdvID"] != DBNull.Value ? Convert.ToInt32(row["AdvID"]) : 0,
                CatID = row["CatID"] != DBNull.Value ? Convert.ToInt32(row["CatID"]) : 0,
                PosID = row["PosID"] != DBNull.Value ? Convert.ToInt32(row["PosID"]) : 0
            };
        }

        /// SelectAll Adv_Page_Position------------------------------------------------------- 
        public List<Adv_Page_Position> SelectAllLike(string keyword)
        {
            List<Adv_Page_Position> ls = new List<Adv_Page_Position>();
            keyword = keyword.Replace('"', ' ');
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_Adv_Page_Position_SelectAllLike", new object[] { keyword }, new string[] { "@keyword" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        /// SelectAll Adv_Page_Position------------------------------------------------------- 
        public List<Adv_Page_Position> SelectAllByAdvID(int advID)
        {
            List<Adv_Page_Position> ls = new List<Adv_Page_Position>();
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_Adv_Page_Position_SelectAllByAdvID", new object[] { advID }, new string[] { "@advID" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        ///  Insert Adv_Page_Position-------------------------------------------------------
        public void Insert()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_Adv_Page_Position_Insert", new object[] { CatID, PosID }, new string[] { "@CatID", "@PosID" }, false);
            }
        }
        ///  Update Adv_Page_Position-------------------------------------------------------
        public void Update()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_Adv_Page_Position_Update", new object[] { AdvID, CatID, PosID }, new string[] { "@AdvID", "@CatID", "@PosID" }, false);
            }
        }
        ///  Delete Adv_Page_Position-------------------------------------------------------
        public void Delete()
        {
            if (AdvID > 0)
            {
                using (MainDB db = new MainDB())
                {
                    db.CallStoredProcedure("CMS_Adv_Page_Position_Delete", new object[] { AdvID }, new string[] { "@AdvID" }, false);
                }
            }
        }
        ///  SelectOne Adv_Page_Position-------------------------------------------------------
        public Adv_Page_Position SelectOne()
        {
            DataTable tbl;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_Adv_Page_Position_SelectOne", new object[] { AdvID }, new string[] { "@AdvID" }, true);
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    return MapObject(tbl.Rows[0]);
                }
            }
            return null;
        }
    }
    public class Advertisments
    {
        public int AdvID { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Embed { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsRotate { get; set; }
        public string Link { get; set; }
        public int Order { get; set; }
        public int Type { get; set; }
        public int ViewCount { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Advertisments(int advid, string name, string filepath, DateTime startdate, DateTime enddate, string embed, string description, bool isactive, bool isrotate, string link, int order, int type, int viewcount, int width, int height)
        {
            this.AdvID = advid;
            this.Name = name;
            this.FilePath = filepath;
            this.StartDate = startdate;
            this.EndDate = enddate;
            this.Embed = embed;
            this.Description = description;
            this.IsActive = isactive;
            this.IsRotate = isrotate;
            this.Link = link;
            this.Order = order;
            this.Type = type;
            this.ViewCount = viewcount;
            this.Width = width;
            this.Height = height;
        }
        public Advertisments()
        { }
        ///  MapObject Advertisments-------------------------------------------------------
        private static Advertisments MapObject(DataRow row)
        {
            return new Advertisments()
            {
                AdvID = row["AdvID"] != DBNull.Value ? Convert.ToInt32(row["AdvID"]) : 0,
                Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : string.Empty,
                FilePath = row["FilePath"] != DBNull.Value ? row["FilePath"].ToString() : string.Empty,
                StartDate = row["StartDate"] != DBNull.Value ? Convert.ToDateTime(row["StartDate"]) : DateTime.Now,
                EndDate = row["EndDate"] != DBNull.Value ? Convert.ToDateTime(row["EndDate"]) : DateTime.Now,
                Embed = row["Embed"] != DBNull.Value ? row["Embed"].ToString() : string.Empty,
                Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : string.Empty,
                IsActive = row["IsActive"] != DBNull.Value ? Convert.ToBoolean(row["IsActive"]) : false,
                IsRotate = row["IsRotate"] != DBNull.Value ? Convert.ToBoolean(row["IsRotate"]) : false,
                Link = row["Link"] != DBNull.Value ? row["Link"].ToString() : string.Empty,
                Order = row["Order"] != DBNull.Value ? Convert.ToInt32(row["Order"]) : 0,
                Type = row["Type"] != DBNull.Value ? Convert.ToInt32(row["Type"]) : 0,
                ViewCount = row["ViewCount"] != DBNull.Value ? Convert.ToInt32(row["ViewCount"]) : 0,
                Width = row["Width"] != DBNull.Value ? Convert.ToInt32(row["Width"]) : 0,
                Height = row["Height"] != DBNull.Value ? Convert.ToInt32(row["Height"]) : 0
            };
        }

        /// SelectAll Advertisments------------------------------------------------------- 
        public List<Advertisments> SelectAllLike(string keyword)
        {
            List<Advertisments> ls = new List<Advertisments>();
            keyword = keyword.Replace('"', ' ');
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_Advertisments_SelectAllLike", new object[] { keyword }, new string[] { "@keyword" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        /// SelectAll Advertisments------------------------------------------------------- 
        public List<Advertisments> SelectAllLikeByPagePosition(string keyword, int page, int position)
        {
            List<Advertisments> ls = new List<Advertisments>();
            keyword = keyword.Replace('"', ' ');
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_Advertisments_SelectAllLikeByPagePosition", new object[] { keyword, page, position }, new string[] { "@keyword", "@page", "@position" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        
        ///  Insert Advertisments-------------------------------------------------------
        public void Insert()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_Advertisments_Insert", new object[] { Name, FilePath, StartDate, EndDate, Embed, Description, IsActive, IsRotate, Link, Order, Type, ViewCount, Width, Height }, new string[] { "@Name", "@FilePath", "@StartDate", "@EndDate", "@Embed", "@Description", "@IsActive", "@IsRotate", "@Link", "@Order", "@Type", "@ViewCount", "@Width", "@Height" }, false);
            }
        }
        ///  Update Advertisments-------------------------------------------------------
        public void Update()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_Advertisments_Update", new object[] { AdvID, Name, FilePath, StartDate, EndDate, Embed, Description, IsActive, IsRotate, Link, Order, Type, ViewCount, Width, Height }, new string[] { "@AdvID", "@Name", "@FilePath", "@StartDate", "@EndDate", "@Embed", "@Description", "@IsActive", "@IsRotate", "@Link", "@Order", "@Type", "@ViewCount", "@Width", "@Height" }, false);
            }
        }
        ///  Delete Advertisments-------------------------------------------------------
        public void Delete()
        {
            if (AdvID > 0)
            {
                using (MainDB db = new MainDB())
                {
                    db.CallStoredProcedure("CMS_Advertisments_Delete", new object[] { AdvID }, new string[] { "@AdvID" }, false);
                }
            }
        }
        ///  SelectOne Advertisments-------------------------------------------------------
        public Advertisments SelectOne()
        {
            DataTable tbl;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_Advertisments_SelectOne", new object[] { AdvID }, new string[] { "@AdvID" }, true);
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    return MapObject(tbl.Rows[0]);
                }
            }
            return null;
        }
        public DataTable SelectOneDataTable()
        {
            DataTable tbl;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_Advertisments_SelectOne", new object[] { AdvID }, new string[] { "@AdvID" }, true);
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    return tbl;
                }
            }
            return null;
        }
    }

}