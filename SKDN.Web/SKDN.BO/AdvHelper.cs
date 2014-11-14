using System;
using System.Collections.Generic;
using DAL;

using System.Data;

namespace BO
{
    public class AdvHelper
    {
        public List<AdvDoc> GetAdvByPosition(int catId, int posId)
        {
            List<AdvDoc> lst = new List<AdvDoc>();
            string key = String.Format("GetAdvByPosition-{0}-{1}", catId, posId);
            lst = Utility.GetFromCache<List<AdvDoc>>(key);
            DataTable tbl = new DataTable();
            if (lst != null) return lst;
            using (MainDB db = new MainDB())
            {
                tbl = db.StoredProcedures.Advertisement_GetById(catId, posId);
            }
            if (tbl == null) return new List<AdvDoc>();
            lst = new List<AdvDoc>();
            DataRow row;
            AdvDoc item;
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                row = tbl.Rows[i];
                item = new AdvDoc();
                item.FilePath = row["FilePath"] != null ? row["FilePath"].ToString() : string.Empty;
                item.Embed = row["Embed"] != null ? row["Embed"].ToString() : string.Empty;
                item.Link = row["Link"] != null ? row["Link"].ToString() : string.Empty;
                item.Name = row["Name"] != null ? row["Name"].ToString() : string.Empty;
                item.Type = row["Type"] != null ? Convert.ToInt32(row["Type"])  : 0;
               
                lst.Add(item);
            }
          //  Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.Advertisments, key, lst);
            return lst;
            
        }

        public List<AdvDoc> GetAdvByPositionById(int AdvID)
        {
            List<AdvDoc> lst = new List<AdvDoc>();
            string key = String.Format("GetAdvByPositionById-{0}", AdvID);
            lst = Utility.GetFromCache<List<AdvDoc>>(key);
            DataTable tbl = new DataTable();
            if (lst != null) return lst;
            using (MainDB db = new MainDB())
            {
                tbl = db.StoredProcedures.CMS_Advertisments_SelectOne(AdvID);
            }
            if (tbl == null) return new List<AdvDoc>();
            lst = new List<AdvDoc>();
            DataRow row;
            AdvDoc item;
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                row = tbl.Rows[i];
                item = new AdvDoc();
                item.FilePath = row["FilePath"] != null ? row["FilePath"].ToString() : string.Empty;
                item.Embed = row["Embed"] != null ? row["Embed"].ToString() : string.Empty;
                item.Link = row["Link"] != null ? row["Link"].ToString() : string.Empty;
                item.Name = row["Name"] != null ? row["Name"].ToString() : string.Empty;
                item.Type = row["Type"] != null ? Convert.ToInt32(row["Type"]) : 0;

                lst.Add(item);
            }
            //  Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.Advertisments, key, lst);
            return lst;

        }
        public List<AdvDoc> GetAllAdv(string keyword)
        {
            List<AdvDoc> lst = new List<AdvDoc>();
            string key = String.Format("GetAllAdv{0}", keyword);
            lst = Utility.GetFromCache<List<AdvDoc>>(key);
            DataTable tbl = new DataTable();
            if (lst != null) return lst;
            using (MainDB db = new MainDB())
            {
                tbl = db.StoredProcedures.CMS_Advertisments_SelectAllLike(keyword);
            }
            if (tbl == null) return new List<AdvDoc>();
            lst = new List<AdvDoc>();
            DataRow row;
            AdvDoc item;
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                row = tbl.Rows[i];
                item = new AdvDoc();
                item.FilePath = row["FilePath"] != null ? Utility.ImagesStorageUrl + "/"+ row["FilePath"].ToString() : string.Empty;
                item.Embed = row["Embed"] != null ? row["Embed"].ToString() : string.Empty;
                item.Link = row["Link"] != null ? row["Link"].ToString() : string.Empty;
                item.Name = row["Name"] != null ? row["Name"].ToString() : string.Empty;
                item.Type = row["Type"] != null ? Convert.ToInt32(row["Type"]) : 0;
                item.Description = row["Description"] != null ? row["Description"].ToString() : string.Empty;

                lst.Add(item);
            }
            //  Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.Advertisments, key, lst);
            return lst;

        }
    }
    public class AdvDoc
    {
        public AdvDoc()
        {
            this.Name = string.Empty;
            this.FilePath = string.Empty;
            this.Embed = string.Empty;
            this.Description = string.Empty;
            this.Link = string.Empty;
            this.Type = 0;
            this.Width = 0;
            this.Height = 0;
        }

        public string Name { get; set; }
        public string FilePath { get; set; }
        public string Embed { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public int Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
