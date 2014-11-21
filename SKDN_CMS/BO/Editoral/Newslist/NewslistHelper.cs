using System;
using System.Data;
using System.Web;
using DFISYS.CoreBO.Common;
using DFISYS.Core.DAL;
using System.ComponentModel;
using DFISYS.BO.Editoral.Category;
using DFISYS.User.Security;
using DFISYS.BO.Editoral.Newsedit;
namespace DFISYS.BO.Editoral.Newslist {
    public static class NewslistHelper {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static DataTable GetNewslist(string strWhere, int PageSize, int StartRow, bool isFistPage) {
            //Lay gia tri mode list de xu ly
            if (strWhere == null)
                strWhere = "";
            if (isFistPage) {
                StartRow = 0;
            }

            if (PageSize == 0) PageSize = 40;

            string cpmode = "";
            if (HttpContext.Current.Request.QueryString["cpmode"] != null)
                cpmode = HttpContext.Current.Request.QueryString["cpmode"].ToString();
            DataTable objresult;
            int intPageNum = StartRow / PageSize + 1;
            //lay duoc du lieu cua tat ca nhung thang co trang thai la status can lay
            using (MainDB objdb = new MainDB()) {
                //objresult = objdb.NewsCollection.GetPageAsDataTable(intPageNum, PageSize, strWhere, "News_ModifiedDate DESC"); ;
                objresult = objdb.StoredProcedures.News_GetListNew(strWhere, cpmode, HttpContext.Current.User.Identity.Name, HttpContext.Current.User.Identity.Name, StartRow.ToString(), PageSize.ToString(), "[News].News_PublishDate DESC");
            }
            return objresult;
        }

        /// <summary>
        /// Check whether current user can edit the news
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool isHasPermission(HttpContext context) {
            if (context.Request.QueryString["NewsRef"] == null || context.Request.QueryString["NewsRef"] == string.Empty)
                return true;
            else {
                string cpMode = context.Request.QueryString["cpmode"];
                if (!string.IsNullOrEmpty(cpMode)) {
                    if (cpMode.Contains(",")) cpMode = cpMode.Substring(cpMode.IndexOf(",") + 1);
                    cpMode = cpMode.ToLower();

                    long newsId = long.Parse(context.Request.QueryString["NewsRef"]);
                    string userName = context.User.Identity.Name;
                    if (!string.IsNullOrEmpty(userName) && newsId != 0) {
                        MainSecurity objsecu = new MainSecurity();
                        //Role objrole = objsecu.GetRole(context.User.Identity.Name);

                        NewsRow newsRow = NewsEditHelper.GetNewsInfo_News(newsId, false);
                        if (newsRow != null) {
                            Permission permission = objsecu.GetPermission(userName);
                            MainDB db = new MainDB();
                            bool toReturn = false;
                            switch (cpMode) {
                                case "templist":
                                    toReturn = newsRow.News_Status == (byte)NewsStatus.LuuTam
                                        && db.StoredProcedures.News_isLastAccessUser(newsId, userName);
                                    break;
                                case "editwaitlist":
                                    toReturn = newsRow.News_Status == (byte)NewsStatus.ChoBienTap
                                        && permission.isBien_Tap_Bai
                                        && db.StoredProcedures.News_isHasPermissionEdit(newsId, CategoryHelper.GetCatIDByUser());
                                    break;
                                case "editinglist":
                                    toReturn = newsRow.News_Status == (byte)NewsStatus.NhanBienTap
                                        && permission.isBien_Tap_Bai
                                        && db.StoredProcedures.News_isLastAccessUser(newsId, userName);
                                    break;
                                case "approvalwaitlist":
                                    toReturn = newsRow.News_Status == (byte)NewsStatus.ChoDuyet && permission.isDuyet_Bai
                                        && db.StoredProcedures.News_isHasPermissionEdit(newsId, CategoryHelper.GetCatIDByUser());
                                    break;
                                case "approvalwaitspeciallist":
                                    toReturn = newsRow.News_Status == (byte)NewsStatus.ChoDuyet && permission.isDuyet_Bai
                                        && db.StoredProcedures.News_isHasPermissionEdit(newsId, CategoryHelper.GetCatIDByUser());
                                    break;
                                case "approvinglist":
                                    toReturn = newsRow.News_Status == (byte)NewsStatus.NhanDuyet && permission.isDuyet_Bai
                                        && db.StoredProcedures.News_isLastAccessUser(newsId, userName);
                                    break;
                                case "publishedlist":
                                    toReturn = newsRow.News_Status == (byte)NewsStatus.XuatBan && permission.isXuat_Ban_Bai
                                        && db.StoredProcedures.News_isHasPermissionEdit(newsId, CategoryHelper.GetCatIDByUser());
                                    break;
                                case "removedlist":
                                    toReturn = newsRow.News_Status == (byte)NewsStatus.GoBo && permission.isXuat_Ban_Bai
                                        && db.StoredProcedures.News_isHasPermissionEdit(newsId, CategoryHelper.GetCatIDByUser());
                                    break;
                                case "backlist":
                                    toReturn = newsRow.News_Status == (byte)NewsStatus.TraLai
                                        && db.StoredProcedures.News_isLastAccessUser(newsId, userName);
                                    break;
                                case "dellist":
                                    toReturn = newsRow.News_Status == (byte)NewsStatus.XoaTam
                                        && db.StoredProcedures.News_isLastAccessUser(newsId, userName);
                                    break;
                            }
                            return toReturn;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Ham lay Action hien thoi cua tin
        /// </summary>
        /// <param name="_news_id">ID cua tin</param>
        /// <returns></returns>
        public static ActionRow getLastestAction(long _news_id) {
            ActionRow[] _arows;
            using (MainDB objDb = new MainDB()) {
                _arows = objDb.ActionCollection.GetTopAsArray(1, "News_ID=" + _news_id, "CreateDate DESC");
            }
            if (_arows.Length > 0)
                return _arows[0];
            else
                return null;
        }
        public static int getLastestStatus(long _news_id) {
            ActionRow ac = getLastestAction(_news_id);
            if (ac != null && !ac.IsActionTypeNull)
                return getLastestAction(_news_id).ActionType;

            return 0;
        }
        private static string getReceiver(long _news_id) {
            return getLastestAction(_news_id).Reciver_ID;
        }
        private static string getSender(long _news_id) {
            return getLastestAction(_news_id).Sender_ID;
        }
        public static int GetRowsCount(string strWhere, bool isFistPage) {
            if (strWhere == null)
                strWhere = "";
            string cpmode = HttpContext.Current.Request.QueryString["cpmode"].ToString();
            DataTable objresult;
            using (MainDB objdb = new MainDB()) {
                objresult = objdb.StoredProcedures.News_GetListNewNumRow(strWhere, cpmode, HttpContext.Current.User.Identity.Name, HttpContext.Current.User.Identity.Name);
            }
            return (int)objresult.Rows[0][0];
        }
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static DataTable getPage(int numPage) {
            int intPagenum = numPage;
            DataTable objTb = new DataTable();
            objTb.Columns.Add(new DataColumn("Text", typeof(string)));
            objTb.Columns.Add(new DataColumn("Value", typeof(string)));
            for (int i = 1; i <= intPagenum; i++) {
                DataRow myRow = objTb.NewRow();
                myRow["Text"] = i.ToString();
                myRow["Value"] = Convert.ToString(i - 1);
                objTb.Rows.Add(myRow);
            }
            if (intPagenum == 0) {
                DataRow myRow = objTb.NewRow();
                myRow["Text"] = "1";
                myRow["Value"] = "0";
                objTb.Rows.Add(myRow);
            }
            return objTb;
        }
        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void UpdateNewsRow(long _news_id, int _news_status, string _selected_id) {
            if (_selected_id == "" || _selected_id == null)
                newsupdate(_news_id, _news_status);
            else {
                //truong hop chuyen trang thai cho tat ca cac tin duoc select
                string[] strnews_ids = _selected_id.Split(",".ToCharArray());
                foreach (string strid in strnews_ids) {
                    long intNid = Convert.ToInt64(strid);
                    newsupdate(intNid, _news_status);
                }
            }
        }

        


        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void UpdateNewsRow(long _news_id, int _news_status, string _selected_id, bool blnIsFocus, int intNewMode, bool isUpdate) {
            if (isUpdate == false)//Truong hop gui len
			{
                if (_selected_id == "" || _selected_id == null)
                    newsupdate(_news_id, _news_status);
                else {
                    //truong hop chuyen trang thai cho tat ca cac tin duoc select
                    string[] strnews_ids = _selected_id.Split(",".ToCharArray());
                    foreach (string strid in strnews_ids) {
                        long intNid = Convert.ToInt64(strid);
                        newsupdate(intNid, _news_status);
                    }
                }
            }
            else //Truong hop edit truc tiep tren row
			{
                newsupdate(_news_id, blnIsFocus, intNewMode);
            }
        }


        private static void newsupdate(long _news_id, bool blnIsFocus, int intNewMode) {
            MainDB objDb = new MainDB();
            objDb.BeginTransaction();
            try {
                NewsRow objRow;
                NewsPublishedRow publishedRow = null;
                bool IsPublished = false;
                String cpmode = HttpContext.Current.Request.QueryString["cpmode"];
                if (cpmode.IndexOf("publishedlist") >= 0 || cpmode.IndexOf("removedlist") >= 0)
                    IsPublished = true;

                //using (MainDB objDb = new MainDB())
                //{
                objRow = objDb.NewsCollection.GetByPrimaryKey(_news_id);

                if (IsPublished)
                    publishedRow = objDb.NewsPublishedCollection.GetByPrimaryKey(_news_id);
                //}
                if (objRow != null) {
                    //thuc hien doi trang thai cua tin - luu thong tin modified thanh ngay gio hien tai.
                    //objRow.News_Status = _news_status;
                    objRow.News_isFocus = blnIsFocus;
                    objRow.News_Mode = intNewMode;
                    objRow.News_ModifiedDate = DateTime.Now;

                    if (IsPublished && publishedRow != null) {
                        publishedRow.News_isFocus = blnIsFocus;
                        publishedRow.News_Mode = intNewMode;
                        publishedRow.News_ModifedDate = DateTime.Now;
                    }

                    //using (MainDB objDb = new MainDB())
                    //{
                    objDb.NewsCollection.Update(objRow);

                    if (IsPublished && publishedRow != null)
                        objDb.NewsPublishedCollection.Update(publishedRow);
                    //}
                }

                // Commit Transaction
                objDb.CommitTransaction();
            }
            catch (Exception ex) {
                objDb.RollbackTransaction();
            }
            finally {
                objDb.Close();
            }



        }

        private static void newsupdate(long _news_id, int _news_status) {
            NewsRow objRow;
            int old_news_status = -1;
            int _cat_id = -1;

            MainDB objDb = new MainDB();
            objDb.BeginTransaction();
            string strAction = "";
            try {
                objRow = objDb.NewsCollection.GetByPrimaryKey(_news_id);
                string _news_title = objRow.News_Title.Trim();
                old_news_status = objRow.News_Status;
                
                _cat_id = objRow.Cat_ID;
                int type_log_action = LogAction.LogType_BaiViet;

                if (objRow != null) {
                    //thuc hien doi trang thai cua tin - luu thong tin modified thanh ngay gio hien tai.
                    objRow.News_Status = _news_status;
                    objRow.News_ModifiedDate = DateTime.Now;
                    objRow.News_Approver = HttpContext.Current.User.Identity.Name;
                    //thuc hien luu thong tin vao action.
                    ActionRow objArow = new ActionRow();
                    //Gui len. Neu la xoa tam thi khong luu action
                    if (_news_status == 1) {
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " gửi bài chờ biên tập";
                        objArow.ActionType = 1;
                        strAction = "<b>" + _news_title.Replace("'", "") + "</b> " + LogAction.LogAction_GuiChoBT + " bởi " + HttpContext.Current.User.Identity.Name;
                    }
                    if (_news_status == 2) {
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " gửi bài chờ duyệt";
                        objArow.ActionType = 2;
                        strAction = "<b>" + _news_title.Replace("'", "") + "</b> " + LogAction.LogAction_GuiChoBT + " bởi " + HttpContext.Current.User.Identity.Name;
                    }

                    NewsPublishedRow objpublishRow = new NewsPublishedRow();
                    if (_news_status == 3) {
                        objRow.News_Approver = HttpContext.Current.User.Identity.Name;
                        if (objRow.IsNews_PublishDateNull == false && objRow.News_PublishDate.Year != 9999 && objRow.News_PublishDate.Year != 2000)
                            objRow.News_PublishDate = DateTime.Now;

                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " xuất bản bài";
                        objArow.ActionType = 3;
                        strAction = "<b>" + _news_title.Replace("'", "") + "</b> " + LogAction.LogAction_XB + " bởi " + HttpContext.Current.User.Identity.Name;
                        type_log_action = LogAction.LogType_BaiViet_XB;

                        //thuc hien chuyen du lieu qua bang newspublished

                        objpublishRow.News_ID = objRow.News_ID;
                        objpublishRow.Cat_ID = objRow.Cat_ID;
                        objpublishRow.News_Subtitle = objRow.News_Subtitle;
                        objpublishRow.News_Title = objRow.News_Title;
                        objpublishRow.News_Image = objRow.News_Image;
                        objpublishRow.News_Source = objRow.News_Source;
                        objpublishRow.News_InitContent = objRow.News_InitialContent;
                        objpublishRow.News_Content = objRow.News_Content;
                        objpublishRow.News_Athor = objRow.News_Author;
                        objpublishRow.News_Approver = objRow.News_Approver;
                        objpublishRow.News_Status = 3;

                        if (objRow.IsNews_PublishDateNull == true || objRow.News_PublishDate.Year == 9999 || objRow.News_PublishDate.Year == 2000) {
                            objRow.News_PublishDate = DateTime.Now;
                        }
                        objpublishRow.News_PublishDate = objRow.News_PublishDate;

                        objpublishRow.News_isFocus = objRow.News_isFocus;
                        objpublishRow.News_Mode = objRow.News_Mode;
                        objpublishRow.isComment = objRow.isComment;
                        objpublishRow.isUserRate = objRow.isUserRate;
                        objpublishRow.Template = objRow.Template;
                        objpublishRow.Icon = objRow.Icon;
                        //objpublishRow.News_Relation = objRow.News_Relation;

                    }
                    if (_news_status == 5) {
                        ActionRow objLastestArow = NewslistHelper.getLastestAction(Convert.ToInt64(_news_id));
                        objArow.Reciver_ID = objLastestArow.Sender_ID;
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " trả lại bài";
                        strAction = "<b>" + _news_title.Replace("'", "") + "</b> " + LogAction.LogAction_TraLai + " bởi " + HttpContext.Current.User.Identity.Name;
                        objArow.ActionType = getLastestStatus(_news_id);

                        if (old_news_status == 3) type_log_action = LogAction.LogType_BaiViet_XB;
                    }
                    //xoa tam
                    if (_news_status == 6) {
                        objArow.Reciver_ID = HttpContext.Current.User.Identity.Name;
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " xoá tạm bài";
                        strAction = "<b>" + _news_title.Replace("'", "") + "</b> " + LogAction.LogAction_XoaTam + " bởi " + HttpContext.Current.User.Identity.Name;
                        objArow.ActionType = getLastestStatus(_news_id);
                    }
                    //gui bai tu backlist len
                    if (_news_status == -1) {
                        int intLastStaus = getLastestStatus(_news_id);
                        objArow.Reciver_ID = HttpContext.Current.User.Identity.Name;
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " gửi bài";
                        strAction = "<b>" + _news_title.Replace("'", "") + "</b> " + LogAction.LogAction_GuiBai + " bởi " + HttpContext.Current.User.Identity.Name;
                        objRow.News_Status = intLastStaus;
                        objArow.ActionType = intLastStaus;
                    }
                    if (_news_status == 7) {
                        int intLastStaus = getLastestStatus(_news_id);
                        objArow.ActionType = intLastStaus;
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " gỡ bỏ bài";
                        strAction = "<b>" + _news_title.Replace("'", "") + "</b> " + LogAction.LogAction_GoBai + " bởi " + HttpContext.Current.User.Identity.Name;
                        type_log_action = LogAction.LogType_BaiViet_XB;

                        
                        objpublishRow = objDb.NewsPublishedCollection.GetByPrimaryKey(_news_id);
                        if (objpublishRow != null) {
                            objDb.NewsPublishedCollection.DeleteByPrimaryKey(_news_id);
                        }
                        
                    }

                    objArow.News_ID = _news_id;
                    objArow.Sender_ID = HttpContext.Current.User.Identity.Name;

                    objArow.CreateDate = DateTime.Now;

                     
                    objDb.ActionCollection.Insert(objArow);
                    objDb.NewsCollection.Update(objRow);
                    if (_news_status == 3) {
                        objDb.NewsPublishedCollection.Insert(objpublishRow);
                    }

                    // Commit Transaction
                    objDb.CommitTransaction();

                   
                }

            }
            catch (Exception ex) {
                objDb.RollbackTransaction();
            }
            finally {
                objDb.Close();
            }

        }
        #region Delete method
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static void DelNews(string _selected_id) {
            if (_selected_id.IndexOf(",") <= 0) {
                try {
                    long intid = Convert.ToInt64(_selected_id);
                    using (MainDB objDB = new MainDB()) {

                        objDB.NewsCollection.DeleteByPrimaryKey(intid);

                    }
                }
                catch { }
            }
            else {
                try {
                    using (MainDB objDB = new MainDB()) {
                        objDB.NewsCollection.Delete("News_ID in (" + _selected_id + ")");
                    }
                }
                catch { }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_sWhere"></param>
        private static void DeleteFile(string _news_id) {
            string strFolder = "images/uploaded/";
            FileHelper.delImgFolder(strFolder, "Share/" + _news_id);
        }



        #endregion
         
   

        public static DataTable GetNewsListAction(string _news_id, int PageSize, int StartRow) {
            int k = 0;
            int intPageNum = StartRow / PageSize + 1;
            using (MainDB objDb = new MainDB()) {
                return objDb.ActionCollection.GetAsDataTable(" news_id = '" + _news_id + "'", " createdate desc ", StartRow, PageSize, ref k);
            }
        }

        public static int GetCountAction(string _news_id) {
            using (MainDB objDb = new MainDB()) {
                return Convert.ToInt32(objDb.ActionCollection.GetCount(" news_id = '" + _news_id + "'"));
            }
        }

        public static void RemoveCountCache(string cpMode)
        {
            string userName = HttpContext.Current.User.Identity.Name;
            string[] arrCacheFormat = {
                                          String.Format("GetRowsNews_{0}_{1}", cpMode, userName),
                                          String.Format("comment_{0}", userName)
                                          , "allnewstemplist"
                                      };
            for(int i=0;i<arrCacheFormat.Length;i++)
            {
                
            }

        }

        public static int GetCountNews(string strcpmod, bool isClear) {
            string strCats = CategoryHelper.GetCatIDByUser();
            if (strCats == "")
                return 0;

            string strWhere = "";

            switch (strcpmod.ToLower()) {
                case "templist":
                    strWhere = " News_Status=0 AND Category.Cat_ID in (" + strCats + ")";
                    break;
                case "sendlist":
                case "editwaitlist":
                case "editinglist":
                    strWhere = " News_Status=1 AND Category.Cat_ID in (" + strCats + ")";
                    break;
                case "sendapprovallist":
                case "approvinglist":
                case "approvalwaitlist":
                    strWhere = " News_Status=2 AND Category.Cat_ID in (" + strCats + ")";
                    break;
                case "approvalwaitspeciallist":
                    strWhere = " News_Status=2 and isUserRate = 1 AND Category.Cat_ID in (" + strCats + ")";
                    break;
                case "dellist":
                    strWhere = " News_Status=6 AND Category.Cat_ID in (" + strCats + ")";
                    break;
                case "publishedlist":
                    strWhere = " News_Status=3 AND Category.Cat_ID in (" + strCats + ")";
                    break;
                case "backlist":
                    strWhere = " News_Status=5 AND Category.Cat_ID in (" + strCats + ")";
                    break;
                case "removedlist":
                    strWhere = " News_Status=7 AND Category.Cat_ID in (" + strCats + ")";
                    break;

            }

            return GetRowsNews(strWhere, strcpmod, isClear);
        }

        public static int GetRowsNews(string strWhere, string cpmode, bool isClear) {
            if (strWhere == null)
                strWhere = "";

            string strCacheName = "GetRowsNews_" + cpmode + "_" + HttpContext.Current.User.Identity.Name;
            if (isClear) HttpContext.Current.Cache.Remove(strCacheName);
            DataTable objresult = HttpContext.Current.Cache[strCacheName] as DataTable;
            if (objresult == null) {
                using (MainDB objdb = new MainDB()) {
                    objresult = objdb.StoredProcedures.News_GetListNewNumRow(strWhere, cpmode, HttpContext.Current.User.Identity.Name, HttpContext.Current.User.Identity.Name);
                }

                //DFISYS.BO.Common.Utils.SetDataToCache(objresult, strCacheName, "News",);
                //HttpContext.Current.Cache.Insert(strCacheName, objresult, null, DateTime.Now.AddMinutes(60), TimeSpan.Zero);
            }
            return (int)objresult.Rows[0][0];
        }
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static DataTable GetNewslistOfNewsSpecialListControl(string strWhere, int PageSize, int StartRow,string SortExpression)
        {
            string cpmode = "";
            if (HttpContext.Current.Request.QueryString["cpmode"] != null)
                cpmode = HttpContext.Current.Request.QueryString["cpmode"].ToString();

            if (string.IsNullOrEmpty(SortExpression)) SortExpression = "[News].News_ModifiedDate DESC";
      

            //Lay gia tri mode list de xu ly
            if (strWhere == null)
                strWhere = "";

            DataTable objresult;
            int intPageNum = StartRow / PageSize + 1;
            //lay duoc du lieu cua tat ca nhung thang co trang thai la status can lay
            using (MainDB objdb = new MainDB())
            {
                objresult = objdb.StoredProcedures.News_GetListNewSpecial(strWhere, StartRow.ToString(), PageSize.ToString(), SortExpression);
            }
            return objresult;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static DataTable GetNewslistOfNewsListControl(string strWhere, int PageSize, int StartRow, string News_Approver, string SortExpression) {
            string cpmode = "";
            if (HttpContext.Current.Request.QueryString["cpmode"] != null)
                cpmode = HttpContext.Current.Request.QueryString["cpmode"].ToString();

            if (string.IsNullOrEmpty(SortExpression)) SortExpression = "[News].News_ModifiedDate DESC";
            if (cpmode.IndexOf("publishedlist") > -1) SortExpression = SortExpression.Replace("[News].News_ModifiedDate", "News.News_publishDate");

            //Lay gia tri mode list de xu ly
            if (strWhere == null)
                strWhere = "";

            DataTable objresult;

            if (PageSize == 0) PageSize = 40;

            int intPageNum = StartRow / PageSize + 1;
            //lay duoc du lieu cua tat ca nhung thang co trang thai la status can lay
            using (MainDB objdb = new MainDB()) {
                objresult = objdb.StoredProcedures.News_GetListNew(strWhere, cpmode, HttpContext.Current.User.Identity.Name, HttpContext.Current.User.Identity.Name, StartRow.ToString(), PageSize.ToString(), SortExpression);
            }
            return objresult;
        }

        public static int GetRowsCountOfNewsListControl(string strWhere, string News_Approver) {
            if (strWhere == null)
                strWhere = "";
            string cpmode = HttpContext.Current.Request.QueryString["cpmode"].ToString();

            string key = "GetRowsCountOfNewsListControl" + strWhere;


            DataTable objresult = HttpContext.Current.Cache[key] as DataTable;
            if (objresult != null) return (int)objresult.Rows[0][0]; 
            using (MainDB objdb = new MainDB()) {
                objresult = objdb.StoredProcedures.News_GetListNewNumRow(strWhere, cpmode, HttpContext.Current.User.Identity.Name, HttpContext.Current.User.Identity.Name);
            }
            Utils.SetCache(objresult, key, new string[] {"NewsPublished" });
            return (int)objresult.Rows[0][0];
        }

        public static int GetRowsCountOfNewsSpecialListControl(string strWhere)
        {
            if (strWhere == null)
                strWhere = "";
            string cpmode = HttpContext.Current.Request.QueryString["cpmode"].ToString();

            string key = "GetRowsCountOfNewsSpecialListControl" + strWhere;


            DataTable objresult =  Utils.GetFromCache(key);
            if (objresult != null) return (int)objresult.Rows[0][0];
            using (MainDB objdb = new MainDB())
            {
                objresult = objdb.StoredProcedures.News_GetListNewSpecialNumRows(strWhere);
            }
             Utils.SetCache(objresult, key, new string[] { "NewsPublished" });
            return (int)objresult.Rows[0][0];
        }
        public static DataTable SelectNewsByRangeId(String RangeNewsId) {
            using (MainDB db = new MainDB()) {
                DataTable table = db.StoredProcedures.vc_News_SelectListNewsByRangeNewsId(RangeNewsId);
                return table;
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static DataTable News_GetAllNewsTemplist(string strWhere, int PageSize, int StartRow) {
            //Lay gia tri mode list de xu ly
            if (strWhere == null)
                strWhere = "";

            int EndIndex = PageSize + StartRow;

            DataTable objresult;

            using (MainDB objdb = new MainDB()) {
                objresult = objdb.StoredProcedures.News_GetAllNewsTemplist(strWhere, StartRow.ToString(), EndIndex.ToString());
            }
            return objresult;
        }


        public static int News_GetAllNewsTemplistCount(string strWhere) {
            //Lay gia tri mode list de xu ly
            if (strWhere == null)
                strWhere = "";

            //lay duoc du lieu cua tat ca nhung thang co trang thai la status can lay
            using (MainDB objdb = new MainDB()) {
                return Convert.ToInt32(objdb.StoredProcedures.News_GetAllNewsTemplistCount(strWhere).Rows[0][0]);
            }
        }
        public static bool IsHaveNewUpdateNewsPublished(string News_ID)
        {
            bool IsUpdate = false;

            using (MainDB __db = new MainDB())
            {
                DataTable __result = __db.SelectQuery("Select news_ID from news_extension where news_ID = " + News_ID);
                if (__result != null && __result.Rows.Count > 0)
                    IsUpdate = true;
            }

            return IsUpdate;
        }
        public static DataTable SelectNewsPublishedForView(int StartIndex, int PageSize) {
            DataTable __result = null;

            using (MainDB __db = new MainDB()) {
                __result = __db.StoredProcedures.SelectAllNewsPublished(StartIndex, PageSize);
            }

            return __result;
        }
        
       

        public static DataTable GetListWapNews(int Top) {
            DataTable __result = null;

            using (MainDB __db = new MainDB()) {
                __result = __db.StoredProcedures.CMS_GetListWapNews(Top);
            }

            return __result;
        }




        
        public static DataTable SelectNewsPublishedForViewSearch(string StartIndex, string PageSize, string Key, string Cat_ID)
        {
            DataTable __result = null;

            using (MainDB __db = new MainDB())
            {
                __result = __db.StoredProcedures.SelectAllNewsPublishedV2(StartIndex, PageSize, Key, Cat_ID);
            }

            return __result;
        }

        public static DataTable BaiNoiBat_BaiNoiBat_Select(string EditionType)
        {
            using (MainDB db = new MainDB())
            {
                return db.SelectQuery("Select * From v_BaiNoiBatTrangChu_Select vbn Join NewsPublished np On vbn.News_ID = np.News_ID Join Category c On np.Cat_ID = c.Cat_ID Where c.EditionType_ID = " + EditionType + " Order By Thutu");
            }
        }

        public static void BaiNoiBat_BaiNoiBat_Update(long[] newsIds, int[] thutu, string newsIdNotSelected, string editionType)
        {
            using (MainDB db = new MainDB())
            {
                // xóa hết các bài trong bảng BonBaiNoiBat
                string sql = "Delete From BonBaiNoiBat" + Environment.NewLine;

                // insert từng bài đã chọn vào bảng BonBaiNoiBat
                for (int i = 0; i < newsIds.Length; i++)
                    sql += "Insert Into BonBaiNoiBat (News_Id, isNoiBat, Thutu) Values (" + newsIds.GetValue(i) + ", 0, " + thutu[i] + ")" + Environment.NewLine;

                // cập nhật lại những tin không được chọn thành tin bình thường
                if (!string.IsNullOrEmpty(newsIdNotSelected))
                {
                    sql += "Update News Set News_Mode = 0 From News Join Category On News.Cat_ID = Category.Cat_ID " +
                           "Where Category.EditionType_ID = " + editionType + "AND News_ID In (" + newsIdNotSelected + ") AND (News_PublishDate < DATEADD(HOUR,-48,GETDATE())) " + Environment.NewLine;
                    sql += "Update newspublished Set News_Mode = 0 From NewsPublished Join Category On NewsPublished.Cat_ID = Category.Cat_ID" +
                           " Where Category.EditionType_ID = " + editionType + " AND News_ID In (" + newsIdNotSelected + ") AND (News_PublishDate < DATEADD(HOUR,-48,GETDATE())) " + Environment.NewLine;
                }

                db.AnotherNonQuery(sql);
            }
        }

        public static string GetSenderIDByNewsID(string news_id)
        {
            string sReturn = "";
            using (MainDB objDb = new MainDB())
            {
                DataTable dt = objDb.SelectQuery(" select top 1 sender_id from action where news_id = " + news_id + " order by createdate desc ");
                if (dt.Rows.Count > 0)
                    sReturn = dt.Rows[0][0].ToString();
                else
                    sReturn = "";
            }

            return sReturn;
        }
    }

    public enum NewsStatus : byte {
        LuuTam = 0,
        ChoBienTap = 1, NhanBienTap = 1,
        ChoDuyet = 2, NhanDuyet = 2,
        XuatBan = 3,
        TraLai = 5,
        XoaTam = 6,
        GoBo = 7,
        None = 255
    }
}
