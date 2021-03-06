using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using DFISYS.Core.DAL;
using System.Web;

namespace DFISYS.CoreBO {
    public class NewsList {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static DataTable List(string catIDs, string fromDate, string toDate, string newsTitle, int focus, int type, int pageSize, int startRow, string sortExpression) {
            newsTitle = CoreBO.Utils.SQL.HandleParamFTS(newsTitle);

            string dateField = "News_ModifiedDate";
            if (HttpContext.Current.Request["cpmode"] == "publishedlist") dateField = "News_PublishDate";

            using (MainDB objdb = new MainDB()) {
                return (DataTable)objdb.CallStoredProcedure("s_ListNews",
                    new object[] { catIDs, fromDate, toDate, newsTitle, focus.ToString(), type.ToString(), sortExpression, dateField, (startRow + 1).ToString(), (startRow + pageSize).ToString() },
                    new string[] { "@catIDs", "@fromDate", "@toDate", "@newsTitle", "@focus", "@type", "@sortExpression", "@dateField", "@startRow", "@endRow" }, true);
            }
        }

        public static int Count(string catIDs, string fromDate, string toDate, string newsTitle, int focus, int type) {
            newsTitle = CoreBO.Utils.SQL.HandleParamFTS(newsTitle);

            using (MainDB objdb = new MainDB()) {
                return (int)objdb.CallStoredProcedure("s_CountNews",
                    new object[] { catIDs, fromDate, toDate, newsTitle, focus.ToString(), type.ToString() },
                    new string[] { "@catIDs", "@fromDate", "@toDate", "@newsTitle", "@focus", "@type" }, false);
            }
        }
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static bool Delete(string newsID) {
            using (MainDB objdb = new MainDB()) {
                return (int)objdb.CallStoredProcedure("CMS_News_DeleteItem",
                    new object[] { newsID },
                    new string[] { "@newsID" }, false) == 1;
            }
        }
        public static void Delete(long[] newsIDs) {
            using (MainDB objdb = new MainDB()) {
                for (int i = 0; i < newsIDs.Length; i++)
                    objdb.CallStoredProcedure("CMS_News_DeleteItem",
                        new object[] { newsIDs.GetValue(i) },
                        new string[] { "@newsID" }, false);
            }
        }
        public static void UpdateStatus(long[] newsIDs, int newsStatus) {
            if (newsIDs.Length > 0)
                for (int i = 0; i < newsIDs.Length; i++)
                    UpdateStatus((long)newsIDs.GetValue(i), newsStatus);
        }
        public static string GetPageTitle(string cpmode) {
            if (string.IsNullOrEmpty(cpmode)) return string.Empty;
            cpmode = cpmode.ToLower();
            string title = string.Empty;
            switch (cpmode) {
                case "templist":
                    title = "Danh sách bài viết lưu tạm";
                    break;
                case "sendlist":
                    title = "Danh sách bài viết đã gửi chờ biên tập";
                    break;
                case "sendapprovallist":
                    title = "Danh sách bài viết đã gửi chờ duyệt";
                    break;
                case "dellist":
                    title = "Danh sách bài viết xóa tạm";
                    break;
                case "editwaitlist":
                    title = "Danh sách bài viết chờ biên tập";
                    break;
                case "editinglist":
                    title = "Danh sách bài viết nhận biên tập";
                    break;
                case "approvinglist":
                    title = "Danh sách bài viết nhận duyệt";
                    break;
                case "approvalwaitlist":
                    title = "Danh sách bài viết chờ duyệt";
                    break;
                case "publishedlist":
                    title = "Danh sách bài viết đã xuất bản";
                    break;
                case "backlist":
                    title = "Danh sách bài viết trả lại";
                    break;
                case "removedlist":
                    title = "Danh sách bài viết đã gỡ bỏ";
                    break;
                case "mypublished":
                    break;
                default:
                    break;
            }
            return title;
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
            int intPageNum = StartRow / PageSize + 1;
            //lay duoc du lieu cua tat ca nhung thang co trang thai la status can lay
            using (MainDB objdb = new MainDB()) {
                if (News_Approver == "" || News_Approver == null) {
                    objresult = objdb.StoredProcedures.News_GetListNew(strWhere, cpmode, HttpContext.Current.User.Identity.Name, HttpContext.Current.User.Identity.Name, StartRow.ToString(), PageSize.ToString(), SortExpression);
                }
                else {
                    objresult = objdb.StoredProcedures.News_GetListNewMyPublished(strWhere, cpmode, HttpContext.Current.User.Identity.Name, HttpContext.Current.User.Identity.Name, StartRow.ToString(), PageSize.ToString(), News_Approver, SortExpression);
                }
            }
            return objresult;
        }

        private static void UpdateStatus(long newsID, int newsStatus) {
            NewsRow objRow;
            int old_news_status = -1;
            int _cat_id = -1;

            MainDB objDb = new MainDB();
            objDb.BeginTransaction();

            try {
                objRow = objDb.NewsCollection.GetByPrimaryKey(newsID);
                old_news_status = objRow.News_Status;
                _cat_id = objRow.Cat_ID;

                if (objRow != null) {
                    //thuc hien doi trang thai cua tin - luu thong tin modified thanh ngay gio hien tai.
                    objRow.News_Status = newsStatus;
                    objRow.News_ModifiedDate = DateTime.Now;
                    //thuc hien luu thong tin vao action.
                    ActionRow objArow = new ActionRow();
                    //Gui len. Neu la xoa tam thi khong luu action
                    if (newsStatus == 1) {
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " gửi bài chờ biên tập";
                        objArow.ActionType = 1;
                    }
                    if (newsStatus == 2) {
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " gửi bài chờ duyệt";
                        objArow.ActionType = 2;
                    }

                    NewsPublishedRow objpublishRow = new NewsPublishedRow();
                    if (newsStatus == 3) {
                        objRow.News_Approver = HttpContext.Current.User.Identity.Name;
                        if (objRow.IsNews_PublishDateNull == false && objRow.News_PublishDate.Year != 9999 && objRow.News_PublishDate.Year != 2000)
                            objRow.News_PublishDate = DateTime.Now;

                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " xuất bản bài";
                        objArow.ActionType = 3;

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
                        objpublishRow.News_Relation = objRow.News_Relation;

                    }
                    if (newsStatus == 5) {
                        ActionRow objLastestArow = getLastestAction(Convert.ToInt64(newsID));
                        objArow.Reciver_ID = objLastestArow.Sender_ID;
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " trả lại bài";

                        objArow.ActionType = getLastestStatus(newsID);
                    }
                    //xoa tam
                    if (newsStatus == 6) {
                        objArow.Reciver_ID = HttpContext.Current.User.Identity.Name;
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " xoá tạm bài";
                        objArow.ActionType = getLastestStatus(newsID);
                    }
                    //gui bai tu backlist len
                    if (newsStatus == -1) {
                        int intLastStaus = getLastestStatus(newsID);
                        objArow.Reciver_ID = HttpContext.Current.User.Identity.Name;
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " gửi bài";
                        objRow.News_Status = intLastStaus;
                        objArow.ActionType = intLastStaus;
                    }
                    if (newsStatus == 7) {
                        int intLastStaus = getLastestStatus(newsID);
                        objArow.ActionType = intLastStaus;
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " gỡ bỏ bài";

                        // Neu la go bo thi go luon o bang NewsPublished
                        //using (MainDB objDb = new MainDB())
                        //{
                        objpublishRow = objDb.NewsPublishedCollection.GetByPrimaryKey(newsID);
                        if (objpublishRow != null) {
                            objDb.NewsPublishedCollection.DeleteByPrimaryKey(newsID);
                        }
                        //}
                    }

                    objArow.News_ID = newsID;
                    objArow.Sender_ID = HttpContext.Current.User.Identity.Name;

                    objArow.CreateDate = DateTime.Now;

                    //if (_news_status != 6)
                    objDb.ActionCollection.Insert(objArow);
                    objDb.NewsCollection.Update(objRow);
                    if (newsStatus == 3) {
                        objDb.NewsPublishedCollection.Insert(objpublishRow);
                    }

                    // Commit Transaction
                    objDb.CommitTransaction();


                    //#region Insert vao bang LogInfo để thống kê
                    //Log objLog = new Log();

                    //if (old_news_status != -1 && old_news_status != newsStatus && _cat_id != -1)
                    //{
                    //    string strUser_Id = HttpContext.Current.User.Identity.Name;
                    //    if (newsStatus == 1)
                    //    {
                    //        // Đối với bài chờ biên tập
                    //        objLog.UpdateLogInfo((int)CountKey.Category_Wait_Edit_Content, _cat_id.ToString());
                    //        objLog.UpdateLogInfo((int)CountKey.User_Wait_Edit_Content, strUser_Id);
                    //    }
                    //    else if (newsStatus == 2)
                    //    {
                    //        // Đối với bài chờ duyet
                    //        objLog.UpdateLogInfo((int)CountKey.Category_Wait_Approve_Content, _cat_id.ToString());
                    //        objLog.UpdateLogInfo((int)CountKey.User_Wait_Approve_Content, strUser_Id);
                    //    }
                    //    else if (newsStatus == 3)
                    //    {
                    //        // Đối với bài đã xuất bản
                    //        objLog.UpdateLogInfo((int)CountKey.Category_Approved_Content, _cat_id.ToString());
                    //        objLog.UpdateLogInfo((int)CountKey.User_Approved_Content, strUser_Id);
                    //    }
                    //}
                    //#endregion
                }

            }
            catch (Exception ex) {
                objDb.RollbackTransaction();
            }
            finally {
                objDb.Close();
            }

        }
        public static int getLastestStatus(long newsID) {
            ActionRow ac = getLastestAction(newsID);
            if (!ac.IsActionTypeNull)
                return ac.ActionType;

            return 0;
        }
        private static string getReceiver(long newsID) {
            return getLastestAction(newsID).Reciver_ID;
        }
        public static ActionRow getLastestAction(long newsID) {
            ActionRow[] acs;
            using (MainDB objDb = new MainDB()) {
                acs = objDb.ActionCollection.GetTopAsArray(1, "News_ID=" + newsID, "CreateDate DESC");
            }
            if (acs.Length > 0)
                return acs[0];
            else
                return null;
        }
    }
}
