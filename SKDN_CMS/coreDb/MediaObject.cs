using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DFISYS.Core.DAL
{
    public class MediaObject
    {
        public double News_ID { get; set; }
        public int Object_ID { get; set; }
        public int Object_Type { get; set; }
        public string Object_Url { get; set; }
        public string Object_Note { get; set; }
        public string UserID { get; set; }
        public int STT { get; set; }
        public int galleryID { get; set; }
        public string News_Title { get; set; }

        public string Object_TypeName
        {
            get
            {
                string Name = string.Empty;
                switch (Object_Type)
                {
                    case 1:
                        Name = _Object_Type.Image.ToString();
                        break;
                    case 2:
                        Name = _Object_Type.Video.ToString();
                        break;
                }
                return Name;
            }
        }
        public enum _Object_Type
        {
            Image = 1,
            Video = 2,
        }
        public static List<MediaObject> DataTableTpObj(DataTable dtAllData)
        {
            List<MediaObject> lstAllData = new List<MediaObject>();
            if (dtAllData != null && dtAllData.Rows.Count > 0)
            {
                var allRow = dtAllData.Rows;
                foreach (DataRow row in allRow)
                {
                    MediaObject objData = new MediaObject();
                    objData.News_ID = row["News_ID"] == DBNull.Value ? 0 : Convert.ToDouble(row["News_ID"]);
                    objData.Object_ID = row["Object_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["Object_ID"]);
                    objData.Object_Type = row["Object_Type"] == DBNull.Value ? 0 : Convert.ToInt32(row["Object_Type"]);
                    objData.Object_Url = row["Object_Url"] == DBNull.Value ? string.Empty : row["Object_Url"].ToString();
                    objData.Object_Note = row["Object_Note"] == DBNull.Value ? string.Empty : row["Object_Note"].ToString();
                    objData.UserID = row["UserID"] == DBNull.Value ? string.Empty : row["UserID"].ToString();
                    objData.STT = row["STT"] == DBNull.Value ? 0 : Convert.ToInt32(row["STT"]);
                  //  objData.News_Title = row["News_Title"] == DBNull.Value ? string.Empty : row["News_Title"].ToString();
                    objData.galleryID = (int) (row["GalleryID"] == DBNull.Value ? 0 : row["GalleryID"]);
                    lstAllData.Add(objData);
                }
            }
            return lstAllData;
        }
        public List<MediaObject> MediaObject_GetAllItem()
        {
            DATABASE db = new DATABASE();
            DataTable dtAllData = db.EXECUTE_PROC("MediaObject_GetAllItem");
            List<MediaObject> lstAllData = new List<MediaObject>();
            lstAllData = DataTableTpObj(dtAllData);
            return lstAllData;
        }

        public List<MediaObject> MediaObject_getbyGallleryId(int Id)
        {
            DATABASE db = new DATABASE();
            DataTable dtAllData = db.EXECUTE_PROC("CMS_MediaObject_GetAllItem_By_GalleryId", new object[] { "@GalleryId", Id });
            var lstAllData = new List<MediaObject>();
            lstAllData = DataTableTpObj(dtAllData);
            return lstAllData;
        } 

        public List<MediaObject> MediaObject_GetAllItemByNews_ID(decimal News_ID)
        {
            DATABASE db = new DATABASE();
            DataTable dtAllData = db.EXECUTE_PROC("CMS_MediaObject_GetAllItem_By_News_ID", new object[] { "@News_ID", News_ID });
            List<MediaObject> lstAllData = new List<MediaObject>();
            lstAllData = DataTableTpObj(dtAllData);
            return lstAllData;
        }
        public string MediaObject_Add(int Object_Type, string Object_Url, string Object_Note, int STT, string UserID, decimal News_ID,int galleryId)
        {
            string strReturn = string.Empty; ;
            DATABASE db = new DATABASE();
            DataTable dtAllData = db.EXECUTE_PROC("CMS_MediaObject_GetAllItem_By_Object_Url", new object[] { "@Object_Url", Object_Url });
            if (dtAllData != null)
            {
                if (dtAllData.Rows.Count > 0)
                {
                    strReturn = Object_Url + "Ảnh này đã tồn tại. ";
                    return strReturn;
                }
            }
            strReturn = db.EXECUTE_PROC("CMS_MediaObject_Insert_Item", "", new object[] { "@Object_Type",Object_Type,
                "@Object_Url",Object_Url,
                "@Object_Note",Object_Note,
                "@UserID",UserID,
                "@STT",STT,"@News_ID",News_ID,"@GalleryId",galleryId });
            return strReturn;
        }
        public string MediaObject_Update(MediaObject objData)
        {
            string strReturn = string.Empty;
            DATABASE db = new DATABASE();
            strReturn = db.EXECUTE_PROC("MediaObject_Update_Item", "", new object[] { "@Object_ID", objData.Object_ID, "@Object_Type", objData.Object_Type, "@Object_Url", objData.Object_Url, "@Object_Note", objData.Object_Note, "@STT", objData.STT });
            return strReturn;
        }
        public string MediaObject_Delete(int Object_ID)
        {
            string strReturn = string.Empty;
            DATABASE db = new DATABASE();
            strReturn = db.EXECUTE_PROC("MediaObject_Delete_By_Object_ID", "", new object[] { "@Object_ID", Object_ID });
            return strReturn;
        }
    }
}
