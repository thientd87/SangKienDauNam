using System;
using System.Data;
using System.Web;
using DAL;


namespace BO
{
    public class SKDNHelper
    {
        public static DataTable GetAllAlbum(int imgWitdth)
        {
            DataTable dtAlbum = new DataTable();
            using (MainDB objDb = new MainDB())
            {
                dtAlbum = objDb.StoredProcedures.Web_GetTopLastestGallery(1000);
                if (dtAlbum != null && dtAlbum.Rows.Count > 0)
                {
                    if (!dtAlbum.Columns.Contains("Image")) dtAlbum.Columns.Add("Image");
                    if (!dtAlbum.Columns.Contains("URL")) dtAlbum.Columns.Add("URL");
                    
                    for (int i = 0; i < dtAlbum.Rows.Count; i++)
                    {
                        DataTable dtAlbumDetail = objDb.StoredProcedures.Web_GetImageByGalleryID(Convert.ToInt32(dtAlbum.Rows[i]["ID"]), 1);
                        if(dtAlbumDetail!=null && dtAlbumDetail.Rows.Count > 0)
                            dtAlbum.Rows[i]["Image"] = dtAlbumDetail.Rows[0]["Object_URL"] != null
                                ? Utility.GetThumbNail(dtAlbumDetail.Rows[0]["Object_Note"].ToString(),
                                   "/album/" + Utility.UnicodeToKoDauAndGach(dtAlbum.Rows[i]["Name"].ToString()) + "-" + dtAlbum.Rows[i]["ID"] +".htm", dtAlbumDetail.Rows[0]["Object_URL"].ToString(), imgWitdth)
                                : String.Empty;

                        dtAlbum.Rows[i]["URL"] = "/album/" +
                                                 Utility.UnicodeToKoDauAndGach(dtAlbum.Rows[i]["Name"].ToString()) + "-" +
                                                 dtAlbum.Rows[i]["ID"] + ".htm";

                    }
                    dtAlbum.AcceptChanges();
                }
            }
            return dtAlbum;
        }
        public static void DangKyQuaTang(string fullname, string email, string address, string phone, string gift)
        {
            using (MainDB objDb = new MainDB())
            {
                objDb.StoredProcedures.InsertDangKyQuangTang(fullname, email, address, phone, gift);
            }
        }
        public static void DangKyMuaHang(string CusName, string CusAddress, string CusMobile, string CusEmail, int ProductId)
        {
            using (MainDB objDb = new MainDB())
            {
                objDb.StoredProcedures.DangKyMuaHang_Insert(CusName, CusAddress, CusMobile, CusEmail, ProductId);
            }
        }
        public static DataTable GetTopLastestAlbum(int Top,int imgWitdth)
        {
            DataTable dtAlbum = new DataTable();
            using (MainDB objDb = new MainDB())
            {
                dtAlbum = objDb.StoredProcedures.Web_GetTopLastestGallery(Top);
                if (dtAlbum != null && dtAlbum.Rows.Count > 0)
                {
                    if (!dtAlbum.Columns.Contains("Image")) dtAlbum.Columns.Add("Image");

                    for (int i = 0; i < dtAlbum.Rows.Count; i++)
                    {
                        DataTable dtAlbumDetail = objDb.StoredProcedures.Web_GetImageByGalleryID(Convert.ToInt32(dtAlbum.Rows[i]["ID"]), 1);
                        if (dtAlbumDetail != null && dtAlbumDetail.Rows.Count > 0)
                            dtAlbum.Rows[i]["Image"] = dtAlbumDetail.Rows[0]["Object_URL"] != null
                                ? Utility.GetThumbNail(dtAlbumDetail.Rows[0]["Object_Note"].ToString(),
                                   "/album/" + Utility.UnicodeToKoDauAndGach(dtAlbum.Rows[i]["Name"].ToString()) + "-" + dtAlbum.Rows[i]["ID"] + ".htm", dtAlbumDetail.Rows[0]["Object_URL"].ToString(), imgWitdth,true)
                                : String.Empty;

                    }
                    dtAlbum.AcceptChanges();
                }
            }
            return dtAlbum;
        }
        public static DataTable GetAlbumDetail(int AlbumID,int imgWidth)
        {
            DataTable dtAlbumDetail = new DataTable();
            using (MainDB objDb = new MainDB())
            {

                dtAlbumDetail = objDb.StoredProcedures.Web_GetImageByGalleryID(AlbumID, 1000);
               
                if (dtAlbumDetail != null && dtAlbumDetail.Rows.Count > 0)
                {
                    if (!dtAlbumDetail.Columns.Contains("Image")) dtAlbumDetail.Columns.Add("Image");

                    for (int i = 0; i < dtAlbumDetail.Rows.Count; i++)
                    {
                        dtAlbumDetail.Rows[i]["Image"] = dtAlbumDetail.Rows[i]["Object_URL"] != null
                                ? Utility.GetThumbNail(dtAlbumDetail.Rows[i]["Object_Note"].ToString(),
                                   Utility.ImagesStorageUrl + dtAlbumDetail.Rows[i]["Object_URL"], dtAlbumDetail.Rows[i]["Object_URL"].ToString(), imgWidth,"gallery")
                                : String.Empty;

                    }
                    dtAlbumDetail.AcceptChanges();
                }
            }
            return dtAlbumDetail;
        }
        public static DataTable GetLastestAlbum(int imgWith,int Top)
        {
            
            DataTable dtAlbum, dtAlbumDetail =  new DataTable();
            using (MainDB objDb = new MainDB())
            {
               dtAlbum = objDb.StoredProcedures.Web_GetLastestGallery();
                if (dtAlbum != null && dtAlbum.Rows.Count > 0)
                {
                    dtAlbumDetail = objDb.StoredProcedures.Web_GetImageByGalleryID(Convert.ToInt32(dtAlbum.Rows[0]["ID"]), Top);
                }
                if (dtAlbumDetail != null && dtAlbumDetail.Rows.Count > 0)
                {
                    if (!dtAlbumDetail.Columns.Contains("Image")) dtAlbumDetail.Columns.Add("Image");

                    for (int i = 0; i < dtAlbumDetail.Rows.Count; i++)
                    {
                        dtAlbumDetail.Rows[i]["Image"] = dtAlbumDetail.Rows[i]["Object_URL"] != null
                                ? Utility.GetThumbNail(dtAlbumDetail.Rows[i]["Object_Note"].ToString(),
                                   "#", dtAlbumDetail.Rows[i]["Object_URL"].ToString(), imgWith)
                                : String.Empty;
                       
                    }
                    dtAlbumDetail.AcceptChanges();
                }
            }
            return dtAlbumDetail;
        }

        public static DataTable GetAllSupportOnline()
        {
            DataTable result;
            using (MainDB objDb = new MainDB())
            {
                //result = objDb.SelectQuery(" select Object_ID from news_media Where Film_ID = " + film_id + " And Object_ID > 0 And (News_ID IS NULL OR News_ID = -1) ");
                result = objDb.StoredProcedures.Web_SelectSupportOnline();
            }
            if (result!=null && result.Rows.Count > 0)
            {
                if (!result.Columns.Contains("URL")) result.Columns.Add("URL");
                if (!result.Columns.Contains("icon")) result.Columns.Add("icon");
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(result.Rows[i]["Yahoo"].ToString()))
                    {
                        result.Rows[i]["URL"] = "ymsgr:SendIM?" + result.Rows[i]["Yahoo"];
                        result.Rows[i]["icon"] = "http://opi.yahoo.com/online?u=" + result.Rows[i]["Yahoo"] + "&m=g&t=2";
                    }
                    else if (!string.IsNullOrEmpty(result.Rows[i]["Skype"].ToString()))
                    {
                        result.Rows[i]["URL"] = "skype:" + result.Rows[i]["Skype"] + "?call";
                        result.Rows[i]["icon"] = "http://mystatus.skype.com/bigclassic/" + result.Rows[i]["Skype"];
                    }

                }
                result.AcceptChanges();
                
            }
            return result;
        }

       

        public static DataTable GetSiteInformation(int id)
        {
            DataTable result;
            using (MainDB objDb = new MainDB())
            {

                result = objDb.StoredProcedures.Web_SelectSiteInformation(id);
            }
            return result;
        }
        public static string Get_ObjectId_By_NewsID(string news_id)
        {
            DataTable result;
            using (MainDB objDb = new MainDB())
            {
                //result = objDb.SelectQuery(" select Object_ID from news_media Where Film_ID = " + film_id + " And Object_ID > 0 And (News_ID IS NULL OR News_ID = -1) ");
                result = objDb.StoredProcedures.vc_Execute_Sql(" select Object_ID from news_media Where News_ID = '" + news_id + "' And Object_ID > 0 And (Film_ID IS NULL OR Film_ID = -1) ");
            }
            if (result.Rows.Count > 0)
            {
                string sReturn = "";
                foreach (DataRow dr in result.Rows)
                {
                    sReturn += dr["Object_ID"] + ",";
                }
                sReturn = sReturn.Remove(sReturn.Length - 1, 1);
                return sReturn;
            }
            return "";
        }
        public static DataTable GetAllMedia_By_News_ID(long News_ID)
        {
            string CacheName = "Web_MediaObject_GetAllItem_By_News_ID" + News_ID;
            DataTable tbl = Utility.GetFromCache<DataTable>(CacheName);
            if (tbl == null)
            {
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.Web_MediaObject_GetAllItem_By_News_ID(News_ID);

                }
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    if (!tbl.Columns.Contains("URL")) tbl.Columns.Add("URL");
                    if (!tbl.Columns.Contains("index")) tbl.Columns.Add("index");
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        tbl.Rows[i]["URL"] = Utility.ImagesStorageUrl + tbl.Rows[i]["Object_Url"];
                        tbl.Rows[i]["index"] = (i + 1) + "/" + tbl.Rows.Count;
                    }
                    tbl.AcceptChanges();
                }
                Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.MediaObject, CacheName, tbl);
            }
            return tbl;
        }
        public static DataTable GetAllMedia_By_News_ID(long News_ID, int ImagesWidth)
        {
            string CacheName = "Web_MediaObject_GetAllItem_By_News_ID" + News_ID;
            DataTable tbl = Utility.GetFromCache<DataTable>(CacheName);
            if (tbl == null)
            {
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.Web_MediaObject_GetAllItem_By_News_ID(News_ID);

                }
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    if (!tbl.Columns.Contains("URL")) tbl.Columns.Add("URL");
                    if (!tbl.Columns.Contains("OriginalUrl")) tbl.Columns.Add("OriginalUrl");
                    if (!tbl.Columns.Contains("index")) tbl.Columns.Add("index");
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        //tbl.Rows[i]["URL"] = Utils.ImagesStoreURL + tbl.Rows[i]["Object_Url"];
                        tbl.Rows[i]["URL"] = tbl.Rows[i]["Object_Url"] != null ? Utility.GetThumbNailNoLink(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["Object_Url"].ToString(), ImagesWidth) : String.Empty;
                        tbl.Rows[i]["OriginalUrl"] = tbl.Rows[i]["Object_Url"] != null ? Utility.ImagesStorageUrl + "" + tbl.Rows[i]["Object_Url"] : String.Empty;
                        tbl.Rows[i]["index"] = (i + 1) + "/" + tbl.Rows.Count;
                    }
                    tbl.AcceptChanges();
                }
                Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.MediaObject, CacheName, tbl);
            }
            return tbl;
        }
        public static DataTable displayMicrof_Detail(long news_id)
        {
            string CacheName = "st_GetDetail" + news_id;
            DataTable dt = Utility.GetFromCache<DataTable>(CacheName);
            if (dt == null)
            {
                DataSet ds;
                using (MainDB db = new MainDB())
                {
                    ds = db.StoredProcedures.Web_GetDetail(news_id);
                }
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {

                        if (!ds.Tables[0].Columns.Contains("URL")) ds.Tables[0].Columns.Add("URL");
                        if (!ds.Tables[0].Columns.Contains("NewsURL")) ds.Tables[0].Columns.Add("NewsURL");

                        if (!ds.Tables[0].Columns.Contains("PublishDate")) ds.Tables[0].Columns.Add("PublishDate");
                        if (!ds.Tables[0].Columns.Contains("NewsRelated")) ds.Tables[0].Columns.Add("NewsRelated");
                        

                        ds.Tables[0].Rows[0]["News_Subtitle"] = ds.Tables[0].Rows[0]["News_Subtitle"].ToString() != ""
                                                                    ? ds.Tables[0].Rows[0]["News_Subtitle"]
                                                                    : "";
                        ds.Tables[0].Rows[0]["URL"] = string.Format("/{0}c{1}/{2}.htm", ds.Tables[0].Rows[0]["News_ID"],
                                                                    10,
                                                                    Utility.UnicodeToKoDauAndGach(
                                                                        ds.Tables[0].Rows[0]["News_Title"].ToString()));
                        ds.Tables[0].Rows[0]["NewsURL"] = "" +
                                                            Utility.NewsDetailLink(
                                                                ds.Tables[0].Rows[0]["News_Title"].ToString(),
                                                                Convert.ToInt32(ds.Tables[0].Rows[0]["Cat_ID"].ToString()),
                                                                Convert.ToInt32(
                                                                    ds.Tables[0].Rows[0]["Cat_ParentID"].ToString()),
                                                                news_id);
                       

                        ds.Tables[0].Rows[0]["PublishDate"] =
                            Utility.ChangeToVietNamDate2(
                                Utility.ObjectToDataTime(ds.Tables[0].Rows[0]["News_PublishDate"]));
                       

                        if (ds.Tables.Count == 2 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        {
                            string strRelated = "";
                            strRelated = "<ul>";
                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {

                                strRelated += string.Format("<li><a title=\"{0}\" href=\"{1}\">{0}</a></li>",
                                                            ds.Tables[1].Rows[i]["News_Title"],
                                                            Utility.NewsDetailLink(
                                                                ds.Tables[1].Rows[i]["News_Title"].ToString(),
                                                                Utility.Obj2Int(ds.Tables[1].Rows[i]["Cat_ID"]),
                                                                Utility.Obj2Int(ds.Tables[1].Rows[i]["Cat_ParentID"]),
                                                                Utility.Obj2Int64(ds.Tables[1].Rows[i]["News_ID"])));
                            }
                            strRelated += "</ul>";
                            ds.Tables[0].Rows[0]["NewsRelated"] = strRelated;
                        }
                        ds.Tables[0].AcceptChanges();
                    }

                    dt = ds.Tables[0];
                }
                else
                {
                    dt = null;
                }
              //  Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.NEWSPUBLISHED, CacheName, dt);
            }


            return dt;
        }
        public static DataTable SelectCategory(int Cat_ID)
        {
            string CacheName = "Microf_SelectCategory" + Cat_ID;
            DataTable tbl = Utility.GetFromCache<DataTable>(CacheName);
            if (tbl == null)
            {
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.Microf_SelectCategory(Cat_ID);

                }
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    if (!tbl.Columns.Contains("Cat_URL")) tbl.Columns.Add("Cat_URL");
                    if (!tbl.Columns.Contains("RSS_URL")) tbl.Columns.Add("RSS_URL");
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        tbl.Rows[i]["Cat_URL"] = Utility.CatLink(tbl.Rows[i]["Cat_ID"].ToString(), tbl.Rows[i]["Cat_ParentID"].ToString(), Utility.UnicodeToKoDauAndGach(tbl.Rows[i]["Cat_Name"].ToString()), tbl.Rows[i]["Channel_ID"].ToString());
                        tbl.Rows[i]["RSS_URL"] = string.Format("/rss/c{1}p{0}/{2}.rss", tbl.Rows[i]["Cat_ParentID"], tbl.Rows[i]["Cat_ID"], tbl.Rows[i]["Cat_DisplayURL"]);
                    }


                    tbl.AcceptChanges();

                }

                Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.CATEGORY, CacheName, tbl);
            }
            return tbl;
        }
        public static int GetDanhSachTin_Count(int catID, int PageSize)
        {
            string CachName = "Microf_DanhSachTin_Count" + catID + PageSize;
            int totalPage = Utility.GetFromCache<int>(CachName);
            if (totalPage == 0)
            {
                DataTable tbl = null;
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.Web_DanhSachTin_Count(catID);
                    if (tbl != null)
                    {
                        totalPage = Utility.ConvertToInt(tbl.Rows[0][0]);
                        totalPage = (totalPage - 1) / PageSize + 1;
                    }
                    else
                    {
                        totalPage = 1;
                    }

                }
               // Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.NEWSPUBLISHED, CachName, totalPage);
            }
            return totalPage;
        }
        public static DataTable GetNewsNoiBatMuc(int CatID, Int32 Top,int imageWidth, bool IsHidden = true)
        {
            string CacheName = "GetNewsNoiBatMuc" + CatID + Top + IsHidden;
            DataTable tbl = Utility.GetFromCache<DataTable>(CacheName);
            if (tbl == null)
            {
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.Web_NoiBatMuc(CatID, Top, IsHidden);

                    if (tbl != null && tbl.Rows.Count > 0)
                    {
                        if (!tbl.Columns.Contains("URL")) tbl.Columns.Add("URL");
                        if (!tbl.Columns.Contains("Image")) tbl.Columns.Add("Image");
                        if (!tbl.Columns.Contains("PublishDate")) tbl.Columns.Add("PublishDate");
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            tbl.Rows[i]["URL"] = Utility.NewsDetailLinkV2(tbl.Rows[i]["News_Title"].ToString(),tbl.Rows[i]["Cat_ID"].ToString(), tbl.Rows[i]["Cat_ParentID"].ToString(),tbl.Rows[i]["News_ID"].ToString(), "1");
                            tbl.Rows[i]["Image"] = tbl.Rows[i]["News_Image"] != null ? Utility.GetThumbNail(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["URL"].ToString(), tbl.Rows[i]["News_Image"].ToString(), imageWidth) : String.Empty;
                            tbl.Rows[i]["PublishDate"] = Convert.ToDateTime(tbl.Rows[i]["News_PublishDate"]).ToString("dd/MM/yyyy | HH:mm");
                        }
                        tbl.AcceptChanges();
                      //  Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.NEWSPUBLISHED, CacheName, tbl);
                    }
                }
            }
            return tbl;
        }

        public static DataTable GetFocusNews(int iTop,int imgWith)
        {
            string CacheName = "Web_NewsPublished_GetFocus" + iTop;
            DataTable tbl = Utility.GetFromCache<DataTable>(CacheName);
            if (tbl == null)
            {
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.Web_NewsPublished_GetFocus(iTop);

                    if (tbl != null && tbl.Rows.Count > 0)
                    {
                        if (!tbl.Columns.Contains("URL")) tbl.Columns.Add("URL");
                        if (!tbl.Columns.Contains("Image")) tbl.Columns.Add("Image");
                      
                        if (!tbl.Columns.Contains("PublishDate")) tbl.Columns.Add("PublishDate");
                        for (int i = 0; i < tbl.Rows.Count; i++)
                        {
                            tbl.Rows[i]["URL"] = Utility.NewsDetailLinkV2(tbl.Rows[i]["News_Title"].ToString(),
                                tbl.Rows[i]["Cat_ID"].ToString(), tbl.Rows[i]["Cat_ParentID"].ToString(),
                                tbl.Rows[i]["News_ID"].ToString(), "1");
                            tbl.Rows[i]["Image"] = tbl.Rows[i]["News_Image"] != null
                                ? Utility.GetThumbNail(tbl.Rows[i]["News_Title"].ToString(),
                                    tbl.Rows[i]["URL"].ToString(), tbl.Rows[i]["News_Image"].ToString(), imgWith)
                                : String.Empty;
                            
                            tbl.Rows[i]["PublishDate"] =
                                Convert.ToDateTime(tbl.Rows[i]["News_PublishDate"]).ToString("dd/MM/yyyy | HH:mm");
                          

                        }
                        tbl.AcceptChanges();
                        Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.NEWSPUBLISHED, CacheName, tbl);
                    }
                }
            }
            return tbl;
        }
        public static DataTable SearchNews(string  Key, int PageIndex, int PageSize, int imgWidth)
        {
            string CacheName = "SearchNews" + Key + PageSize + PageIndex + imgWidth;
            DataTable tbl = Utility.GetFromCache<DataTable>(CacheName);
            if (tbl == null)
            {
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.Web_SearchNews(Key, PageSize, PageIndex);
                }
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    if (!tbl.Columns.Contains("URL")) tbl.Columns.Add("URL");
                    if (!tbl.Columns.Contains("Cat_URL")) tbl.Columns.Add("Cat_URL");
                    if (!tbl.Columns.Contains("Image")) tbl.Columns.Add("Image");
                    if (!tbl.Columns.Contains("ImageVideo")) tbl.Columns.Add("ImageVideo");
                    if (!tbl.Columns.Contains("OriginImage")) tbl.Columns.Add("OriginImage");
                    if (!tbl.Columns.Contains("PublishDate")) tbl.Columns.Add("PublishDate");
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        tbl.Rows[i]["URL"] = Utility.NewsDetailLinkV2(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["Cat_ID"].ToString(), tbl.Rows[i]["Cat_ParentID"].ToString(), tbl.Rows[i]["News_ID"].ToString(), "1");
                        tbl.Rows[i]["Image"] = tbl.Rows[i]["News_Image"] != null ? Utility.GetThumbNail(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["URL"].ToString(), tbl.Rows[i]["News_Image"].ToString(), imgWidth) : String.Empty;
                        tbl.Rows[i]["ImageVideo"] = tbl.Rows[i]["News_Image"] != null ? Utility.GetThumbNailWithPlayIcon(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["URL"].ToString(), tbl.Rows[i]["News_Image"].ToString(), imgWidth) : String.Empty;
                        tbl.Rows[i]["Cat_URL"] = Utility.CatLink(tbl.Rows[i]["Cat_ID"].ToString(), tbl.Rows[i]["Cat_ParentID"].ToString(), Utility.UnicodeToKoDauAndGach(tbl.Rows[i]["Cat_Name"].ToString()), "1");
                        tbl.Rows[i]["PublishDate"] = Convert.ToDateTime(tbl.Rows[i]["News_PublishDate"]).ToString("dd/MM/yyyy | HH:mm");
                        tbl.Rows[i]["OriginImage"] = tbl.Rows[i]["News_Image"] != null ? Utility.ImagesStorageUrl + tbl.Rows[i]["News_Image"] : String.Empty;

                    }
                    tbl.AcceptChanges();
                    // Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.NEWSPUBLISHED, CacheName, tbl);
                }
            }

            return tbl;
        }
        public static int SearchNewsCount(string Key)
        {
            int Count = 0;
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.StoredProcedures.Web_SearchNews_Count(Key);
                if (dt != null && dt.Rows.Count > 0)
                {
                    Count = Convert.ToInt32(dt.Rows[0][0]);
                }

            }
            return Count;
        }
        public static DataTable displayGetDanhSachTin(int catID, int PageIndex, int PageSize, int imgWidth)
        {
            string CacheName = "Microf_DanhSachTin" + catID + PageSize + PageIndex + imgWidth;
            DataTable tbl = Utility.GetFromCache<DataTable>(CacheName);
            if (tbl == null)
            {
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.Web_DanhSachTin(catID, PageSize, PageIndex);
                }
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    if (!tbl.Columns.Contains("URL")) tbl.Columns.Add("URL");
                    if (!tbl.Columns.Contains("Cat_URL")) tbl.Columns.Add("Cat_URL");
                    if (!tbl.Columns.Contains("Image")) tbl.Columns.Add("Image");
                    if (!tbl.Columns.Contains("ImageVideo")) tbl.Columns.Add("ImageVideo");
                    if (!tbl.Columns.Contains("OriginImage")) tbl.Columns.Add("OriginImage");
                    if (!tbl.Columns.Contains("PublishDate")) tbl.Columns.Add("PublishDate");
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        tbl.Rows[i]["URL"] = Utility.NewsDetailLinkV2(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["Cat_ID"].ToString(), tbl.Rows[i]["Cat_ParentID"].ToString(), tbl.Rows[i]["News_ID"].ToString(), "1");
                        tbl.Rows[i]["Image"] = tbl.Rows[i]["News_Image"] != null ? Utility.GetThumbNail(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["URL"].ToString(), tbl.Rows[i]["News_Image"].ToString(), imgWidth) : String.Empty;
                        tbl.Rows[i]["ImageVideo"] = tbl.Rows[i]["News_Image"] != null ? Utility.GetThumbNailWithPlayIcon(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["URL"].ToString(), tbl.Rows[i]["News_Image"].ToString(), imgWidth) : String.Empty;
                        tbl.Rows[i]["Cat_URL"] = Utility.CatLink(tbl.Rows[i]["Cat_ID"].ToString(), tbl.Rows[i]["Cat_ParentID"].ToString(), Utility.UnicodeToKoDauAndGach(tbl.Rows[i]["Cat_Name"].ToString()),"1");
                        tbl.Rows[i]["PublishDate"] = Convert.ToDateTime(tbl.Rows[i]["News_PublishDate"]).ToString("dd/MM/yyyy | HH:mm");
                        tbl.Rows[i]["OriginImage"] = tbl.Rows[i]["News_Image"] != null ? Utility.ImagesStorageUrl + tbl.Rows[i]["News_Image"] : String.Empty;

                    }
                    tbl.AcceptChanges();
                   // Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.NEWSPUBLISHED, CacheName, tbl);
                }
            }

            return tbl;
        }
        public static DataTable displayGetLatestNewsByMode(int Top, int Mode, int imgWidth,string Cat_ID)
        {
            string CacheName = "Microf_LatestNews" + Top + Mode + imgWidth + Cat_ID;
            DataTable tbl = Utility.GetFromCache<DataTable>(CacheName);
            if (tbl == null)
            {
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.Microf_LatestNewsByCat(Top, Mode, Cat_ID);
                }
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    if (!tbl.Columns.Contains("URL")) tbl.Columns.Add("URL");
                    if (!tbl.Columns.Contains("Image")) tbl.Columns.Add("Image");
                    if (!tbl.Columns.Contains("PublishDate")) tbl.Columns.Add("PublishDate");

                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        tbl.Rows[i]["URL"] = Utility.NewsDetailLinkV2(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["Cat_ID"].ToString(), tbl.Rows[i]["Cat_ParentID"].ToString(), tbl.Rows[i]["News_ID"].ToString(), tbl.Rows[i]["Channel_ID"].ToString());
                        tbl.Rows[i]["Image"] = tbl.Rows[i]["News_Image"] != null ? Utility.GetThumbNail(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["URL"].ToString(), tbl.Rows[i]["News_Image"].ToString(), imgWidth) : String.Empty;
                        tbl.Rows[i]["PublishDate"] = Convert.ToDateTime(tbl.Rows[i]["News_PublishDate"]).ToString("dd/MM");
                    }
                    tbl.AcceptChanges();
                    Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.NEWSPUBLISHED, CacheName, tbl);

                }
            }

            return tbl;
        }
        public static DataTable displayGetLatestNewsByMode(int Top, int Mode, int imgWidth, int Channel_ID)
        {
            string CacheName = "Microf_LatestNews" + Top + Mode + imgWidth + Channel_ID;
            DataTable tbl = Utility.GetFromCache<DataTable>(CacheName);
            if (tbl == null)
            {
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.Microf_LatestNews(Top, Mode, Channel_ID);
                }
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    if (!tbl.Columns.Contains("URL")) tbl.Columns.Add("URL");
                    if (!tbl.Columns.Contains("Cat_URL")) tbl.Columns.Add("Cat_URL");
                    if (!tbl.Columns.Contains("Image")) tbl.Columns.Add("Image");
                    if (!tbl.Columns.Contains("DirectLinkImage")) tbl.Columns.Add("DirectLinkImage");
                    if (!tbl.Columns.Contains("PublishDate")) tbl.Columns.Add("PublishDate");

                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        tbl.Rows[i]["URL"] = Utility.NewsDetailLinkV2(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["Cat_ID"].ToString(), tbl.Rows[i]["Cat_ParentID"].ToString(), tbl.Rows[i]["News_ID"].ToString(), tbl.Rows[i]["Channel_ID"].ToString());
                        tbl.Rows[i]["Cat_URL"] = Utility.CatLink(tbl.Rows[i]["Cat_ID"].ToString(), tbl.Rows[i]["Cat_ParentID"].ToString(), Utility.UnicodeToKoDauAndGach(tbl.Rows[i]["Cat_Name"].ToString()), tbl.Rows[i]["Channel_ID"].ToString());
                        tbl.Rows[i]["Image"] = tbl.Rows[i]["News_Image"] != null ? Utility.GetThumbNail(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["URL"].ToString(), tbl.Rows[i]["News_Image"].ToString(), imgWidth) : String.Empty;
                        tbl.Rows[i]["DirectLinkImage"] = tbl.Rows[i]["News_Image"] != null ? (tbl.Rows[i]["News_Image"].ToString().StartsWith("http://") ? tbl.Rows[i]["News_Image"].ToString() : Utility.ImagesStorageUrl + "/" + tbl.Rows[i]["News_Image"]) : String.Empty;
                        tbl.Rows[i]["PublishDate"] = Convert.ToDateTime(tbl.Rows[i]["News_PublishDate"]).ToString("dd/MM/yyyy HH:mm");
                    }
                    tbl.AcceptChanges();
                    Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.NEWSPUBLISHED, CacheName, tbl);

                }
            }

            return tbl;
        }
        public static DataTable displayGetBonBanNoiBat(int Top, int imgWidth)
        {
            string CacheName = "Microf_BonBanNoiBat" + Top;
            DataTable tbl = Utility.GetFromCache<DataTable>(CacheName);
            if (tbl == null)
            {
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.Microf_BonBanNoiBat(Top);
                }
                if (tbl != null && tbl.Rows.Count > 0)
                {

                    if (!tbl.Columns.Contains("URL")) tbl.Columns.Add("URL");
                    if (!tbl.Columns.Contains("Image")) tbl.Columns.Add("Image");
                    if (!tbl.Columns.Contains("PublishDate")) tbl.Columns.Add("PublishDate");
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {


                        tbl.Rows[i]["URL"] = Utility.NewsDetailLinkV2(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["Cat_ID"].ToString(), tbl.Rows[i]["Cat_ParentID"].ToString(), tbl.Rows[i]["News_ID"].ToString(), tbl.Rows[i]["Channel_ID"].ToString());
                        tbl.Rows[i]["Image"] = tbl.Rows[i]["News_Image"] != null ? Utility.GetThumbNail(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["URL"].ToString(), tbl.Rows[i]["News_Image"].ToString(), imgWidth) : String.Empty;
                        tbl.Rows[i]["PublishDate"] = Convert.ToDateTime(tbl.Rows[i]["News_PublishDate"]).ToString("dd/MM");

                    }
                    tbl.AcceptChanges();

                    Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.NEWSPUBLISHED, CacheName, tbl);
                }

            }

            return tbl;
        }



        public static DataTable GetBonBanNoiBat(int Top,int imgWidth)
        {
            string CacheName = "Web_BonBanNoiBat" + Top;
            DataTable tbl = Utility.GetFromCache<DataTable>(CacheName);
            if (tbl == null)
            {
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.Web_BonBanNoiBat(Top);
                }
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    if (!tbl.Columns.Contains("URL")) tbl.Columns.Add("URL");
                    if (!tbl.Columns.Contains("Image")) tbl.Columns.Add("Image");
                    if (!tbl.Columns.Contains("OriginImage")) tbl.Columns.Add("OriginImage");
                    if (!tbl.Columns.Contains("PublishDate")) tbl.Columns.Add("PublishDate");
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        tbl.Rows[i]["URL"] = Utility.NewsDetailLinkV2(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["Cat_ID"].ToString(), tbl.Rows[i]["Cat_ParentID"].ToString(), tbl.Rows[i]["News_ID"].ToString(), tbl.Rows[i]["Channel_ID"].ToString());
                        tbl.Rows[i]["Image"] = tbl.Rows[i]["News_Image"] != null ? Utility.GetThumbNail(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["URL"].ToString(), tbl.Rows[i]["News_Image"].ToString(), imgWidth) : String.Empty;
                        tbl.Rows[i]["OriginImage"] = tbl.Rows[i]["News_Image"] != null ? Utility.GetThumbNail(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["URL"].ToString(), tbl.Rows[i]["News_Image"].ToString(), 620) : String.Empty;
                        tbl.Rows[i]["PublishDate"] = Convert.ToDateTime(tbl.Rows[i]["News_PublishDate"]).ToString("dd/MM");
                    }
                    tbl.AcceptChanges();
                }
               // Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.NEWSPUBLISHED, CacheName, tbl);
            }

            return tbl;
        }

        public static DataTable displayGetItemByCategorySameDataSet_Data(int Top, int catId, int imgWidth)
        {
            string CacheName = "fn_GetItemByCategorySameDataSet" + Top + catId + imgWidth;
            DataTable tbl = Utility.GetFromCache<DataTable>(CacheName);
            if (tbl == null)
            {
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.fn_GetItemByCategorySameDataSet(catId, Top);
                }
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    if (!tbl.Columns.Contains("URL")) tbl.Columns.Add("URL");
                    if (!tbl.Columns.Contains("Image")) tbl.Columns.Add("Image");
                    if (!tbl.Columns.Contains("PublishDate")) tbl.Columns.Add("PublishDate");
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        tbl.Rows[i]["URL"] = Utility.NewsDetailLinkV2(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["Cat_ID"].ToString(), tbl.Rows[i]["Cat_ParentID"].ToString(), tbl.Rows[i]["News_ID"].ToString(), tbl.Rows[i]["Channel_ID"].ToString());
                        tbl.Rows[i]["Image"] = tbl.Rows[i]["News_Image"] != null ? Utility.GetThumbNail(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["URL"].ToString(), tbl.Rows[i]["News_Image"].ToString(), imgWidth) : String.Empty;
                        tbl.Rows[i]["PublishDate"] = Convert.ToDateTime(tbl.Rows[i]["News_PublishDate"]).ToString("dd/MM");
                    }
                    tbl.AcceptChanges();
                }
                Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.NEWSPUBLISHED, CacheName, tbl);
            }

            return tbl;
        }
        #region Phân trang
        public static String CreatePageNavigator(Int32 TotalPage, Int32 PageIndex, String aHref, String aOnclick)
        {
            if (TotalPage == 0 || TotalPage == 1)
                return String.Empty;
            return HandlerPage(TotalPage, PageIndex, aHref, aOnclick);
        }

        public static DataTable DetailsNews(Int64 NewsID)
        {
            DataTable __result = null;
            using (MainDB db = new MainDB())
            {
                __result = db.StoredProcedures.Microf_Detail(NewsID);
            }
            return __result;
        } 

        private static String CreateLink(Int32 Page, String Text, String aHref, String aOnclick, Int32 PageIndex)
        {
            String strTemp = "";
            aHref = aHref.Replace("{page}", Page.ToString());
            aOnclick = aOnclick.Replace("{page}", Page.ToString());

            if (Page == PageIndex)
                strTemp += "<a onclick=\"" + aOnclick + "\" class=\"pageSelected\" href=\"#\">" + Text + "</a>&nbsp;&nbsp;";
            else
                strTemp += "<a onclick=\"" + aOnclick + "\"  href=\"" + aHref + "\">" + Text + "</a>&nbsp;&nbsp;";


            return strTemp;

        }

        private static String HandlerPage(Int32 TotalPage, Int32 PageIndex, String aHref, String aOnclick)
        {
            String strReturn = "";

            if (PageIndex != 1)
                strReturn += CreateLink(1, "Trang đầu", aHref, aOnclick, PageIndex);

            if (TotalPage <= 5)
                for (int i = 1; i <= TotalPage; i++)
                    strReturn += CreateLink(i, i.ToString(), aHref, aOnclick, PageIndex);
            else
            {
                if (PageIndex == 1)
                {
                    for (int i = 1; i <= 5; i++)
                        strReturn += CreateLink(i, i.ToString(), aHref, aOnclick, PageIndex);

                    strReturn += CreateLink(6, "...", aHref, aOnclick, PageIndex);

                }
                else if (PageIndex == 2)
                {
                    strReturn += CreateLink(1, "1", aHref, aOnclick, PageIndex);
                    for (int i = 2; i <= 5; i++)
                        strReturn += CreateLink(i, i.ToString(), aHref, aOnclick, PageIndex);

                    strReturn += CreateLink(6, "...", aHref, aOnclick, PageIndex);
                }
                else if (PageIndex > 2 && PageIndex < TotalPage - 3)
                {
                    if (PageIndex > 3)
                        strReturn += CreateLink(PageIndex - 3, "...", aHref, aOnclick, PageIndex);

                    for (int i = PageIndex - 2; i <= PageIndex + 2; i++)
                        strReturn += CreateLink(i, i.ToString(), aHref, aOnclick, PageIndex);

                    if (PageIndex <= TotalPage - 3)
                        strReturn += CreateLink(PageIndex + 3, "...", aHref, aOnclick, PageIndex);
                }
                else if (PageIndex >= TotalPage - 3 && PageIndex <= TotalPage - 1)
                {

                    strReturn += CreateLink(PageIndex - 4, "...", aHref, aOnclick, PageIndex);
                    for (int i = PageIndex - 3; i <= TotalPage - 1; i++)
                        strReturn += CreateLink(i, i.ToString(), aHref, aOnclick, PageIndex);
                }
                else if (PageIndex == TotalPage)
                {
                    strReturn += CreateLink(PageIndex - 5, "...", aHref, aOnclick, PageIndex);
                    strReturn += CreateLink(PageIndex - 4, Convert.ToString(PageIndex - 5), aHref, aOnclick, PageIndex);
                    for (int i = TotalPage - 3; i <= TotalPage; i++)
                        strReturn += CreateLink(i, i.ToString(), aHref, aOnclick, PageIndex);
                }
            }
            if (TotalPage != PageIndex)
                strReturn += CreateLink(TotalPage, "Trang cuối", aHref, aOnclick, PageIndex);

            return strReturn;
        }


        #endregion Phân Trang
        public static  DataTable GetCategoryDetail(int Cat_ID)
        {
            DataTable tbl;
            using (MainDB db = new MainDB())
            {
                tbl = db.StoredProcedures.Microf_SelectCategory(Cat_ID);
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    if (!tbl.Columns.Contains("Cat_URL")) tbl.Columns.Add("Cat_URL");
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        tbl.Rows[i]["Cat_URL"] = Utility.CatLink(tbl.Rows[i]["Cat_ID"].ToString(),
                                                                 tbl.Rows[i]["Cat_ParentID"].ToString(),
                                                                 Utility.UnicodeToKoDauAndGach(tbl.Rows[i]["Cat_Name"].ToString()), "1");
                    }
                }
            }
            return tbl;
        }
        public static DataTable GetDanhSachTin(int catId, int pageSize, int pageIndex)
        {

            DataTable tblTop;
          
                using (MainDB db = new MainDB())
                {
                    tblTop = db.StoredProcedures.Select_DanhSachTin(catId, pageSize, pageIndex);
                }
                if (tblTop != null)
                {
                    int iCount = tblTop.Rows.Count;
                    if (!tblTop.Columns.Contains("News_Url")) tblTop.Columns.Add("News_Url");
                    if (!tblTop.Columns.Contains("Image_Link")) tblTop.Columns.Add("Image_Link");
                    if (!tblTop.Columns.Contains("VNDate")) tblTop.Columns.Add("VNDate");
                    string title;
                    DataRow row;
                    for (int i = 0; i < iCount; i++)
                    {
                        row = tblTop.Rows[i];

                        row["News_Url"] = Utils.NewsDetailLink(row["News_Title"].ToString(), row["Cat_ID"].ToString(), row["Cat_ParentID"].ToString(), row["News_ID"].ToString());
                        row["Image_Link"] = Utility.ImagesStorageUrl + row["News_Image"];
                        row["VNDate"] = Convert.ToDateTime(row["News_PublishDate"].ToString()).ToString("dd/MM/yyyy hh:mm");
                        
                        tblTop.AcceptChanges();
                    }
                }
                
               
           

            return tblTop;
        }
        public static DataTable displayGetTinKhac(int Top, int Cat_ID, long News_Id,int imgWidth)
        {
            string CacheName = "stGet_TinKhac" + Top + Cat_ID + News_Id;
            DataTable tbl = Utility.GetFromCache<DataTable>(CacheName);
            if (tbl == null)
            {
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.Web_Get_TinKhac(Cat_ID, Top, News_Id);
                }
                if (tbl != null && tbl.Rows.Count > 0)
                {

                    if (!tbl.Columns.Contains("URL")) tbl.Columns.Add("URL");
                    if (!tbl.Columns.Contains("Image")) tbl.Columns.Add("Image");
                    if (!tbl.Columns.Contains("PublishDate")) tbl.Columns.Add("PublishDate");
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {

                        tbl.Rows[i]["URL"] = Utility.NewsDetailLinkV2(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["Cat_ID"].ToString(), tbl.Rows[i]["Cat_ParentID"].ToString(), tbl.Rows[i]["News_ID"].ToString(),"1");
                        tbl.Rows[i]["Image"] = tbl.Rows[i]["News_Image"] != null ? Utility.GetThumbNail(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["URL"].ToString(), tbl.Rows[i]["News_Image"].ToString(), imgWidth) : String.Empty;
                        tbl.Rows[i]["PublishDate"] = Convert.ToDateTime(tbl.Rows[i]["News_PublishDate"]).ToString("dd/MM/yyyy | HH:mm");
                    }
                    tbl.AcceptChanges();
                   // Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.NEWSPUBLISHED, CacheName, tbl);
                }
            }
            return tbl;
        }
        public static DataTable displayGetTinMoiCapNhat(int Top, int Cat_ID, long News_Id)
        {
            string CacheName = "displayGetTinMoiCapNhat" + Top + Cat_ID + News_Id;
            DataTable tbl = Utility.GetFromCache<DataTable>(CacheName);
            if (tbl == null)
            {
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.Web_Get_TinMoiCapNhat(Cat_ID, Top, News_Id);
                }
                if (tbl != null && tbl.Rows.Count > 0)
                {

                    if (!tbl.Columns.Contains("URL")) tbl.Columns.Add("URL");
                    if (!tbl.Columns.Contains("Image")) tbl.Columns.Add("Image");
                    if (!tbl.Columns.Contains("PublishDate")) tbl.Columns.Add("PublishDate");
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {

                        tbl.Rows[i]["URL"] = Utility.NewsDetailLinkV2(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["Cat_ID"].ToString(), tbl.Rows[i]["Cat_ParentID"].ToString(), tbl.Rows[i]["News_ID"].ToString(), "1");
                        // tbl.Rows[i]["Image"] = tbl.Rows[i]["News_Image"] != null ? Utility.GetThumbNail(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["URL"].ToString(), tbl.Rows[i]["News_Image"].ToString(), imgWidth) : String.Empty;
                        tbl.Rows[i]["PublishDate"] = Convert.ToDateTime(tbl.Rows[i]["News_PublishDate"]).ToString("dd/MM/yyyy | HH:mm");
                    }
                    tbl.AcceptChanges();
                   // Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.NEWSPUBLISHED, CacheName, tbl);
                }
            }
            return tbl;
        }
        public static DataTable GetNewsByOtherCat(string OtherCat)
        {
            string CacheName = "Web_SelectNewsByOtherCat" + OtherCat;
            DataTable tbl = Utility.GetFromCache<DataTable>(CacheName);
            if (tbl == null)
            {
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.Web_SelectNewsByOtherCat(OtherCat);
                }
                if (tbl != null && tbl.Rows.Count > 0)
                {

                    if (!tbl.Columns.Contains("URL")) tbl.Columns.Add("URL");
                    if (!tbl.Columns.Contains("Image")) tbl.Columns.Add("Image");
                    if (!tbl.Columns.Contains("PublishDate")) tbl.Columns.Add("PublishDate");
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {

                        tbl.Rows[i]["URL"] = Utility.NewsDetailLinkV2(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["Cat_ID"].ToString(), tbl.Rows[i]["Cat_ParentID"].ToString(), tbl.Rows[i]["News_ID"].ToString(), "1");
                        // tbl.Rows[i]["Image"] = tbl.Rows[i]["News_Image"] != null ? Utility.GetThumbNail(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["URL"].ToString(), tbl.Rows[i]["News_Image"].ToString(), imgWidth) : String.Empty;
                        tbl.Rows[i]["PublishDate"] = Convert.ToDateTime(tbl.Rows[i]["News_PublishDate"]).ToString("dd/MM/yyyy | HH:mm");
                    }
                    tbl.AcceptChanges();
                    Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.NEWSPUBLISHED, CacheName, tbl);
                }
            }
            return tbl;
        }
        public static DataTable GetAllNews(int StartIndex, int PageSize, int imgWidth)
        {

            string CacheName = "SelectAllNewsPublished" + StartIndex + PageSize;
            DataTable tbl = Utility.GetFromCache<DataTable>(CacheName);
            if (tbl == null)
            {
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.SelectAllNewsPublished(StartIndex, PageSize);

                }
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    if (!tbl.Columns.Contains("URL")) tbl.Columns.Add("URL");
                    if (!tbl.Columns.Contains("Image")) tbl.Columns.Add("Image");
                    if (!tbl.Columns.Contains("TinhTrang")) tbl.Columns.Add("TinhTrang");
                    if (!tbl.Columns.Contains("PublishDate")) tbl.Columns.Add("PublishDate");
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        tbl.Rows[i]["URL"] = Utility.NewsDetailLinkV2(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["Cat_ID"].ToString(), tbl.Rows[i]["Cat_ParentID"].ToString(), tbl.Rows[i]["News_ID"].ToString(), tbl.Rows[i]["Channel_ID"].ToString());
                        tbl.Rows[i]["Image"] = tbl.Rows[i]["News_Image"] != null ? Utility.GetThumbNail(tbl.Rows[i]["News_Title"].ToString(), tbl.Rows[i]["URL"].ToString(), tbl.Rows[i]["News_Image"].ToString(), imgWidth) : String.Empty;

                        tbl.Rows[i]["PublishDate"] = Convert.ToDateTime(tbl.Rows[i]["News_PublishDate"]).ToString("dd/MM/yyyy | HH:mm");
                        tbl.Rows[i]["TinhTrang"] = tbl.Rows[i]["isComment"] != null ? Convert.ToBoolean(tbl.Rows[i]["isComment"]) ? "Còn hàng" : "Liên hệ đặt hàng" : "Liên hệ đặt hàng";

                    }
                    tbl.AcceptChanges();
                    Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.NEWSPUBLISHED, CacheName, tbl);
                }
            }

            return tbl;
        }

        public static int GetDanhSachTinCount(int CatID)
        {
            int Count = 0;
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.StoredProcedures.Web_DanhSachTin_Count(CatID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (CatID == 0)
                        Count = dt.Rows.Count;
                    else
                        Count = Convert.ToInt32(dt.Rows[0][0]);
                }

            }
            return Count;
        }
        public static DataTable GetLastestNewByCatID(int Cat_ID)
        {
            DataTable dt;
            using(MainDB db =  new MainDB())
            {
                dt = db.StoredProcedures.tdt_SelectLastestNewByCatID(Cat_ID);
            }
            if(dt!=null&&dt.Rows.Count>0)
            {
                if (!dt.Columns.Contains("Image_Link")) dt.Columns.Add("Image_Link");
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    dt.Rows[i]["Image_Link"] = Utility.ImagesStorageUrl + dt.Rows[i]["News_Image"];
                }
                dt.AcceptChanges();
            }
            return dt;
        }

        public static string BuidlMenuLeftMenuTripCare()
        {
            string xml = "";
           
            string ParentMenuFormat = "<li><a href=\"{0}\">{1}</a></li>";
            string SubMenuFormat = "<li class=\"sub\"><a href=\"{0}\">{1}</a></li>";
            using(MainDB  db = new MainDB())
            {
                DataTable dtParent = db.StoredProcedures.Category_GetListByWhere(" Where (Cat_ParentID =0 OR Cat_ParentID is null) and Channel_ID = 2", " Order By Cat_Order ");
                string xmlParent = "";
                if(dtParent!=null&&dtParent.Rows.Count>0)
                {
                    xmlParent = "";
                    DataTable dtChild;
                    string xmlChild="";
                    for (int i = 0; i < dtParent.Rows.Count; i++)
                    {
                        xmlParent += string.Format(ParentMenuFormat, Utils.BuildCatURL(dtParent.Rows[i]["Cat_ID"].ToString(), dtParent.Rows[i]["Cat_ParentID"].ToString(), dtParent.Rows[i]["Cat_DisplayURL"].ToString()), dtParent.Rows[i]["Cat_Name"]);
                        xmlChild = "";
                        dtChild = db.StoredProcedures.Category_GetListByWhere(" Where Cat_ParentID = " + dtParent.Rows[i]["Cat_ID"] + " ", " Order By Cat_Order ");    
                        if(dtChild!=null&&dtChild.Rows.Count>0)
                        {
                            for (int j = 0; j < dtChild.Rows.Count; j++)
                            {
                                xmlChild += string.Format(SubMenuFormat, Utils.BuildCatURL(dtChild.Rows[j]["Cat_ID"].ToString(),dtChild.Rows[j]["Cat_ParentID"].ToString(),dtChild.Rows[j]["Cat_DisplayURL"].ToString()),dtChild.Rows[j]["Cat_Name"]);

                            }
                            xmlParent += xmlChild;
                        }
                        


                    }

                    xml = xmlParent;
                }
            }
            return xml;
        }
        public static DataTable GetCategoryByParentV2(int parentID, bool Cat_isColumn, int editionType,int Channel_ID)
        {
            DataTable tbl = new DataTable();
            string key = String.Format("tdt_GetCategoryByParent_{0}_{1}_{2}_{3}", parentID, Cat_isColumn, editionType, Channel_ID);
            tbl = Utility.GetFromCache<DataTable>(key);
            if (tbl == null)
            {
                using (MainDB db = new MainDB())
                {
                    tbl = db.StoredProcedures.Web_GetCategoryByParent(parentID, Cat_isColumn, editionType, Channel_ID);
                }
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    if (!tbl.Columns.Contains("Cat_URL")) tbl.Columns.Add("Cat_URL");
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        tbl.Rows[i]["Cat_URL"] = Utility.CatLink(tbl.Rows[i]["Cat_ID"].ToString(),
                                                                 tbl.Rows[i]["Cat_ParentID"].ToString(),
                                                                 Utility.UnicodeToKoDauAndGach(tbl.Rows[i]["Cat_Name"].ToString()), "1");
                    }
                }
                Utility.SaveToCacheDependency(TableName.DATABASE_NAME, TableName.CATEGORY, key, tbl);
            }
            return tbl;
        }

        public static void InsertFeedBack(string Name, string GioiTinh, string Tel, string Email, string Address, string Title, string Content)
        {

            using (MainDB db = new MainDB())
            {
                db.StoredProcedures.HG_insertFeedBack(Name, GioiTinh, Tel, Email, Address, Title, HttpUtility.HtmlEncode(Content));
            }

        }


        
    }
}
