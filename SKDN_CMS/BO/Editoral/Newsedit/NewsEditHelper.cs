using System;
using System.Data;
using System.Web;
using DFISYS.Core.DAL;
using System.ComponentModel;
using System.IO;


namespace DFISYS.BO.Editoral.Newsedit {
    public static class NewsEditHelper {

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static bool CreateNews_Extension(long _news_id, int _cat_id, string _news_subtitle, string _news_title, string _news_image, string _news_source, string _news_init, string _news_content, string _poster, bool _news_isfocus, int _news_status, int _news_type, string _related_news, string _obj_media, string _other_cat, DateTime _switchtime, bool _isShowComment, bool _isShowRate, int _template, string _news_title_image, string _news_icon, string _thread_id, bool _isNoiBatNhat, string _str_Extension1, string _str_Extension2, string _str_Extension3, int _str_Extension4, string _tag_id, string _newsTemp_id)
        {
            bool toReturn = false;
            if (_news_init != null)
                _news_init = _news_init.Replace(System.Environment.NewLine, "");

            _news_title = _news_title == null ? string.Empty : _news_title;

            //_news_title = _news_title.IndexOf("'") != -1 ? _news_title.Replace("'", "") : _news_title;

            // Neu bai nay dat la Noi Bat Nhat Trang Chu thi News_Mode = 2
            if (_isNoiBatNhat)
            {
                _news_type = 2;
            }

            MainDB objDb = new MainDB();
            objDb.BeginTransaction();
            string strAction = "";

            try
            {
                #region Gán giá trị cho bảng News
                NewsRow objrow = new NewsRow();
                //strResult = _news_id;
                objrow.News_ID = _news_id;
                objrow.Cat_ID = _cat_id;
                objrow.News_Subtitle = _news_subtitle;
                objrow.News_Title = _news_title;
                objrow.News_Image = _news_image;
                objrow.News_Source = _news_source;
                objrow.News_InitialContent = _news_init;
                objrow.News_Content = _news_content;
                objrow.News_Author = _poster;
                //objrow.News_Author = _news_author;
                objrow.News_isFocus = _news_isfocus;
                objrow.News_Status = _news_status;
                objrow.News_Mode = _news_type;
                //Danh sach nhung tin lien quan den tin nay
                objrow.News_Relation = _related_news;
                //objrow.News_FocusImage = _news_imgfocus;
                objrow.News_CreateDate = DateTime.Now;
                objrow.News_ModifiedDate = DateTime.Now;
                objrow.isComment = _isShowComment;
                objrow.isUserRate = _isShowRate;
                objrow.Template = _template;
                objrow.News_ImageNote = _news_title_image;
                objrow.Icon = _news_icon;
                objrow.WordCount = NewsHelper.WordCount(objrow.News_Content);

                objrow.Extension1 = _str_Extension1;
                objrow.Extension2 = _str_Extension2;
                objrow.Extension3 = _str_Extension3;
                objrow.Extension4 = _str_Extension4;
                objrow.News_PublishDate = _switchtime;
                //objrow.news_ = HttpContext.Current.User.Identity.Name;
                //lay other cat neu co
                if (_other_cat != null && _other_cat != "" && _other_cat != "0")
                {
                    objrow.News_OtherCat = _other_cat;
                }

                #endregion

                

                
                // thu insert 2 lan
                if (!objDb.NewsCollection.InsertNews_Extesion(objrow))
                {

                    if (!objDb.NewsCollection.InsertNews_Extesion(objrow)) throw new Exception("Insert khong thanh cong");
                }

                objDb.CommitTransaction();

                toReturn = true;
                
            }
            catch (Exception ex)
            {
                objDb.RollbackTransaction();
                toReturn = false;
                throw ex;
            }
            finally
            {
                if (HttpContext.Current.Session["newsid"] != null) HttpContext.Current.Session.Remove("newsid");
                objDb.Close();
            }

            return toReturn;
        }




        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static bool CreateNews(long _news_id, int _cat_id, string _news_subtitle, string _news_title, string _news_image, string _news_source, string _news_init, string _news_content, string _poster, bool _news_isfocus, int _news_status, int _news_type, string _related_news, string _obj_media, string _other_cat, DateTime _switchtime, bool _isShowComment, bool _isShowRate, int _template, string _news_title_image, string _news_icon, string _thread_id, bool _isNoiBatNhat, string _str_Extension1, string _str_Extension2, string _str_Extension3, int _str_Extension4, string _tag_id, string _newsTemp_id) {
            bool toReturn = false;
            if (_news_init != null)
                _news_init = _news_init.Replace(System.Environment.NewLine, "");

            _news_title = _news_title == null ? string.Empty : _news_title;

            //_news_title = _news_title.IndexOf("'") != -1 ? _news_title.Replace("'", "") : _news_title;

            // Neu bai nay dat la Noi Bat Nhat Trang Chu thi News_Mode = 2
            if (_isNoiBatNhat) {
                _news_type = 2;
            }

            MainDB objDb = new MainDB();
            objDb.BeginTransaction();
            string strAction = "";

            try {
                #region Gán giá trị cho bảng News
                NewsRow objrow = new NewsRow();
                //strResult = _news_id;
                objrow.News_ID = _news_id;
                objrow.Cat_ID = _cat_id;
                objrow.News_Subtitle = _news_subtitle;
                objrow.News_Title = _news_title;
                objrow.News_Image = _news_image;
                objrow.News_Source = _news_source;
                objrow.News_InitialContent = _news_init;
                objrow.News_Content = _news_content;
                objrow.News_Author = _poster;
                //objrow.News_Author = _news_author;
                objrow.News_isFocus = _news_isfocus;
                objrow.News_Status = _news_status;
                objrow.News_Mode = _news_type;
                //Danh sach nhung tin lien quan den tin nay
                objrow.News_Relation = _related_news;
                //objrow.News_FocusImage = _news_imgfocus;
                objrow.News_CreateDate = DateTime.Now;
                objrow.News_ModifiedDate = DateTime.Now;
                objrow.isComment = _isShowComment;
                objrow.isUserRate = _isShowRate;
                objrow.Template = _template;
                objrow.News_ImageNote = _news_title_image;
                objrow.Icon = _news_icon;
                objrow.WordCount = NewsHelper.WordCount(objrow.News_Content);

                objrow.Extension1 = _str_Extension1;
                objrow.Extension2 = _str_Extension2;
                objrow.Extension3 = _str_Extension3;
                objrow.Extension4 = _str_Extension4;

                if (_news_status == 3) {
                    if (_switchtime.Year != 2000)
                        objrow.News_PublishDate = _switchtime;
                    else
                        objrow.News_PublishDate = DateTime.Now;

                    objrow.News_Approver = HttpContext.Current.User.Identity.Name;
                }

                //lay other cat neu co
                if (_other_cat != null && _other_cat != "" && _other_cat != "0") {
                    objrow.News_OtherCat = _other_cat;
                }

                #endregion

                #region Gán và Insert giá trị cho bảng Action và NewsPublished
                //Thuc hien cap nhat thong tin vao Action
                ActionRow objArow = new ActionRow();
                objArow.News_ID = objrow.News_ID;
                objArow.Sender_ID = HttpContext.Current.User.Identity.Name;
                objArow.ActionType = _news_status;
                objArow.CreateDate = DateTime.Now;

                if (_news_status == 0) {
                    objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Tạo bài viết";
                    objArow.Reciver_ID = HttpContext.Current.User.Identity.Name;
                    strAction = "<b>" + _news_title + "</b> " + LogAction.LogAction_Tao + " bởi " + HttpContext.Current.User.Identity.Name;
                }
                if (_news_status == 1) {
                    objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Tạo bài viết và gửi lên chờ biên tập";
                    objrow.News_SwitchTime = DateTime.Now;
                    strAction = "<b>" + _news_title + "</b> " + LogAction.LogAction_Tao_Gui_ChoBT + " bởi " + HttpContext.Current.User.Identity.Name;
                }
                if (_news_status == 2) {
                    objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Tạo bài viết và gửi lên chờ duyệt";
                    objrow.News_SwitchTime = DateTime.Now;
                    strAction = "<b>" + _news_title + "</b> " + LogAction.LogAction_Tao_Gui_ChoDuyet + " bởi " + HttpContext.Current.User.Identity.Name;
                }

                NewsPublishedRow objpublishRow = new NewsPublishedRow();
                if (_news_status == 3) {
                    // Chỉ có những tin nào xuất bản thì mới Insert vào bảng News Published
                    objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Tạo bài viết và xuất bản";
                    strAction = "<b>" + _news_title + "</b> " + LogAction.LogAction_TaoXB + " bởi " + HttpContext.Current.User.Identity.Name;
                    //type_log_action = LogAction.LogType_BaiViet_XB;

                    //thuc hien xuat ban bai viet

                    objpublishRow.News_ID = _news_id;
                    objpublishRow.Cat_ID = _cat_id;
                    objpublishRow.News_Subtitle = _news_subtitle;
                    objpublishRow.News_Title = _news_title;
                    objpublishRow.News_Image = _news_image;
                    objpublishRow.News_Source = _news_source;
                    objpublishRow.News_InitContent = objrow.News_InitialContent;
                    objpublishRow.News_Content = _news_content;
                    objpublishRow.News_Athor = _poster;
                    objpublishRow.News_Approver = HttpContext.Current.User.Identity.Name;
                    objpublishRow.News_Status = 3;
                    if (_switchtime.Year != 2000)
                        objpublishRow.News_PublishDate = _switchtime;
                    else
                        objpublishRow.News_PublishDate = DateTime.Now;
                    objpublishRow.News_isFocus = _news_isfocus;
                    objpublishRow.News_Mode = _news_type;
                    objpublishRow.News_Relation = _related_news;
                    objpublishRow.News_OtherCat = _other_cat;
                    objpublishRow.Template = _template;
                    objpublishRow.isComment = _isShowComment;
                    objpublishRow.isUserRate = _isShowRate;
                    objpublishRow.News_ImageNote = _news_title_image;
                    objpublishRow.Icon = _news_icon;
                    objpublishRow.WordCount = objrow.WordCount;

                    objpublishRow.Extension1 = _str_Extension1;
                    objpublishRow.Extension2 = _str_Extension2;
                    objpublishRow.Extension3 = _str_Extension3; objpublishRow.Extension4 = _str_Extension4;
                }
                #region Insert vào bảng MediaObject
                //truong hop co media object
                if (_obj_media != null && _obj_media != "")
                {
                    News_MediaRow objMrow = null;
                    string[] mediaIds = _obj_media.Split(",".ToCharArray());
                    foreach (string strMediaId in mediaIds)
                    {
                        objMrow = new News_MediaRow();
                        objMrow.News_ID = _news_id;
                        objMrow.Object_ID = Convert.ToInt32(strMediaId);
                        objDb.News_MediaCollection.Insert(objMrow);
                    }
                }
                #endregion
                // thu insert 2 lan
                if (!objDb.NewsCollection.Insert(objrow)) {
                    objrow.News_ID = long.Parse(NewsHelper.GenNewsID());
                    objpublishRow.News_ID = objrow.News_ID;
                    objArow.News_ID = objrow.News_ID;
                    if (!objDb.NewsCollection.Insert(objrow)) throw new Exception("Insert khong thanh cong");
                }

                objDb.ActionCollection.Insert(objArow);
                if (objrow.News_Status == 3) {
                    objDb.NewsPublishedCollection.Insert(objpublishRow);

                    // Neu la bai noi bat nhat Trang chu thi insert vao vi tri dau tien cua cua bang BonBaiNoiBat
                    if (_isNoiBatNhat) {
                        //string sql = " Update BonBaiNoiBat Set Thutu = Thutu + 1; " + Environment.NewLine;
                        //sql += " Insert Into BonBaiNoiBat (News_Id, isNoiBat, Thutu) Values (" + objpublishRow.News_ID + ", 0, 1)" + Environment.NewLine;
                        //objDb.SelectQuery(sql);
                        objDb.CallStoredProcedure("NoiBatNhatTrangChu", new object[] { objpublishRow.News_ID }, new string[] { "@news_id" }, false);

                    }
                    // *** End vi tri noi bat nhat
                }

                #endregion

                #region Gan Thread vao bai
                // Gan Thread vao bai

                if (_thread_id != null && _thread_id != "") {
                    DFISYS.CoreBO.Threads.Threaddetails objThread = new DFISYS.CoreBO.Threads.Threaddetails();
                    string[] strArThread = _thread_id.Split(',');
                    foreach (string str in strArThread) {
                        objThread.AddnewsThread(objrow.News_ID.ToString(), Convert.ToInt32(str), objDb);
                    }
                }
                #endregion

                objDb.CommitTransaction();

                //LogAction.InsertMemCache(HttpContext.Current.User.Identity.Name, DateTime.Now, strAction, type_log_action, objrow.News_ID);


                

                toReturn = true;

                #region Delete NewsTemp
                DFISYS.BO.Editoral.Draft.DraftHelper.DeleteNewsTempByTempID(_newsTemp_id);
                #endregion

                 
            }
            catch (Exception ex) {
                objDb.RollbackTransaction();
                toReturn = false;
                throw ex;
            }
            finally {
                if (HttpContext.Current.Session["newsid"] != null) HttpContext.Current.Session.Remove("newsid");
                objDb.Close();
            }

            return toReturn;
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public static bool UpdateNews(long _news_id, int _cat_id, string _news_subtitle, string _news_title, string _news_image, string _news_source, string _news_init, string _news_content, bool _news_isfocus, int _news_status, int _news_type, string _related_news, string _other_cat, DateTime _switchtime, bool _isSend, bool _isShowComment, bool _isShowRate, int _template, string _obj_media, string _news_title_image, string _news_icon, string _thread_id, bool _isNoiBatNhat, string _str_Extension1, string _str_Extension2, string _str_Extension3, int _str_Extension4, string _tag_id, string _newsTemp_id) {
            bool toReturn = false;
            int old_news_status = -1;
            string strAction = "";
            string strActionEdit = "";
            //int type_log_action = LogAction.LogType_BaiViet;

            //_news_title = _news_title.IndexOf("'") != -1 ? _news_title.Replace("'", "") : _news_title;

            // Neu bai nay dat la Noi Bat Nhat Trang Chu thi News_Mode = 2
            if (_isNoiBatNhat) {
                _news_type = 2;
            }

            MainDB objDb = new MainDB();
            objDb.BeginTransaction();
            NewsRow objrow;

            if (_news_init != null)
                _news_init = _news_init.Replace(System.Environment.NewLine, "<br/>");

            try {
                #region Gan Thread vao bai
                // Gan Thread vao bai

                DFISYS.CoreBO.Threads.Threaddetails objThread = new DFISYS.CoreBO.Threads.Threaddetails();
                objDb.SelectScalar("Delete ThreadDetail Where News_ID = '" + _news_id + "'");

                if (_thread_id != null && _thread_id != string.Empty) {
                    string[] strArThread = _thread_id.Split(',');
                    foreach (string str in strArThread)
                        objThread.AddnewsThread(_news_id.ToString(), int.Parse(str), objDb);
                }
                #endregion

                objrow = objDb.NewsCollection.GetByPrimaryKey(_news_id);
                old_news_status = objrow.News_Status;

                if (objrow != null) {
                    #region Kiem tra xem, luc sua bai, user nay co sua title, avatar, sapo, content ko?
                    // Kiem tra xem, luc sua bai, user nay co sua title, avatar, sapo, content ko?
                    strAction = "<b>" + _news_title + "</b>" + LogAction.LogAction_SuaBai + " bởi " + HttpContext.Current.User.Identity.Name;
                    if (objrow.News_Title != null && objrow.News_Title.Trim() != _news_title.Trim())
                        strActionEdit = ", " + LogAction.LogAction_Edit_Title;

                    _news_init = _news_init != null ? _news_init : string.Empty;

                    if (objrow.News_InitialContent != null && objrow.News_InitialContent.Trim() != _news_init.Trim())
                        strActionEdit = ", " + LogAction.LogAction_Edit_Sapo;
                    //if (objrow.News_Image != null && objrow.News_Image.Trim() != _news_image.Trim())
                    if (_news_image != null && objrow.News_Image != null && objrow.News_Image.Trim() != _news_image.Trim() || (_news_image == null && objrow.News_Image != null) || (_news_image != null && objrow.News_Image == null))
                        strActionEdit = ", " + LogAction.LogAction_Edit_Image;
                    if (objrow.News_Content != null && objrow.News_Content.Trim() != _news_content.Trim())
                        strActionEdit = ", " + LogAction.LogAction_Edit_Content;
                    if (strActionEdit.Trim() != "")
                        strActionEdit = strActionEdit.Substring(1);
                    #endregion

                    #region Gan cac gia tri cho bang News
                    objrow.Cat_ID = _cat_id;
                    objrow.News_Subtitle = _news_subtitle;
                    objrow.News_Title = _news_title;
                    objrow.News_Image = _news_image;
                    objrow.News_Source = _news_source;
                    objrow.News_InitialContent = _news_init;
                    objrow.News_Content = _news_content;
                    objrow.WordCount = NewsHelper.WordCount(objrow.News_Content);
                    //objrow.User_ID = _poster;
                    //objrow.News_Author = _news_author;
                    objrow.News_isFocus = _news_isfocus;
                    //Trang thai cua tin tuy thuoc vao nguoi bien tap.
                    if (_news_status != -1)
                        objrow.News_Status = _news_status;
                    objrow.News_Mode = _news_type;
                    //tin lien quan
                    objrow.News_Relation = _related_news;
                    //if (_news_imgfocus != "" && _news_imgfocus!=null)
                    //objrow.News_FocusImage = _news_imgfocus;
                    if (_other_cat != "")
                        objrow.News_OtherCat = _other_cat;

                    DateTime dtPublish = _switchtime;
                    if (objrow.News_Status == 3) {
                        if (dtPublish.Year != 9999) {
                            // Neu khong phai la news da published ma khong chon ngay Xuat ban thi update lai ngay gio
                            if (dtPublish.Year != 2000)
                                objrow.News_PublishDate = dtPublish;
                            else
                                objrow.News_PublishDate = DateTime.Now;
                        }
                    }

                    objrow.Template = _template;
                    objrow.News_ImageNote = _news_title_image;

                    objrow.isComment = _isShowComment;
                    objrow.isUserRate = _isShowRate;
                    objrow.Icon = _news_icon;

                    objrow.Extension1 = _str_Extension1;
                    objrow.Extension2 = _str_Extension2;
                    objrow.Extension3 = _str_Extension3;
                    objrow.Extension4 = _str_Extension4;

                    objrow.News_ModifiedDate = DateTime.Now;
                    #endregion

                    #region Gan cac gia tri cho bang Action
                    //thuc hien luu thong tin vao action.
                    ActionRow objArow = new ActionRow();
                    objArow.Sender_ID = HttpContext.Current.User.Identity.Name;
                    //Gui len. Neu la xoa tam thi khong luu action - Cho nay can phai xem la gui len hay luu lai?
                    string cpMode = string.Empty;
                    if (HttpContext.Current.Request.QueryString["cpmode"] != null) {
                        cpMode = HttpContext.Current.Request.QueryString["cpmode"].Replace("add,", string.Empty);
                        switch (objrow.News_Status) {
                            case 1:
                                if (old_news_status != 1) {
                                    objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " gửi bài chờ biên tập";
                                    strAction = "<b>" + _news_title + "</b> " + LogAction.LogAction_SuaGuiChoBT + " bởi " + HttpContext.Current.User.Identity.Name;
                                }
                                else {
                                    objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " nhận biên tập";
                                    objArow.Reciver_ID = HttpContext.Current.User.Identity.Name;
                                    ActionRow tem = new ActionRow();
                                    tem = getLastestAction(_news_id);
                                    objArow.Sender_ID = tem.Sender_ID;
                                    strAction = "<b>" + _news_title + "</b> " + LogAction.LogAction_NhanBaiBT + " bởi " + HttpContext.Current.User.Identity.Name;
                                }
                                break;
                            case 2:
                                if (old_news_status != 2) {
                                    objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " gửi bài chờ duyệt";
                                    strAction = "<b>" + _news_title + "</b> " + LogAction.LogAction_SuaGuiChoDuyet + " bởi " + HttpContext.Current.User.Identity.Name;
                                }
                                else {
                                    objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " nhận duyệt";
                                    objArow.Reciver_ID = HttpContext.Current.User.Identity.Name;
                                    ActionRow tem = new ActionRow();
                                    tem = getLastestAction(_news_id);
                                    objArow.Sender_ID = tem.Sender_ID;
                                    strAction = "<b>" + _news_title + "</b> " + LogAction.LogAction_NhanBaiBT + " bởi " + HttpContext.Current.User.Identity.Name;
                                }
                                break;
                            case 3: {
                                    if (old_news_status != 3)
                                        strAction = "<b>" + _news_title + "</b> " + LogAction.LogAction_SuaXB + " bởi " + HttpContext.Current.User.Identity.Name;
                                    else
                                        strAction = "<b>" + _news_title + "</b> " + LogAction.LogAction_SuaBaiXB + " bởi " + HttpContext.Current.User.Identity.Name;

                                    //type_log_action = LogAction.LogType_BaiViet_XB;
                                    objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " xuất bản bài";
                                }
                                break;
                        }
                    }


                    objArow.ActionType = objrow.News_Status;
                    objArow.News_ID = _news_id;

                    objArow.CreateDate = DateTime.Now;
                    // chỉ khi tin đổi trạng thái
                    // hoặc
                    // không phải tin ở trong danh sách nhận duyệt, nhận biên tập
                    // thì mới lưu action
                    // [bacth, 11:00 AM 5/31/2008]
                    if (old_news_status != objrow.News_Status || (cpMode.ToLower().Equals("approvalwaitlist") || cpMode.ToLower().Equals("editwaitlist"))) {
                        if (objrow.News_Status == 1 || objrow.News_Status == 2 || objrow.News_Status == 3) objDb.ActionCollection.Insert(objArow);
                    }

                    #endregion

                    #region Gan cac gia tri cho bang NewsPublished
                    NewsPublishedRow objpublishRow;

                    // Get lai nhung gia tri cua cua News Published
                    objpublishRow = objDb.NewsPublishedCollection.GetByPrimaryKey(_news_id);

                    // Neu khong ton tai news_id trong NewsPUblished thi khoi tao lai
                    if (objpublishRow == null)
                        objpublishRow = new NewsPublishedRow();

                    // Gan cac gia tri cho News Published
                    if (_news_status == 3) {

                        objrow.News_Approver = HttpContext.Current.User.Identity.Name;

                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " xuất bản bài";
                        objArow.ActionType = 3;

                        //thuc hien chuyen du lieu qua bang newspublished
                        objpublishRow.Icon = _news_icon;
                        objpublishRow.News_ID = _news_id;
                        objpublishRow.Cat_ID = _cat_id;
                        objpublishRow.News_Subtitle = _news_subtitle;
                        objpublishRow.News_Title = _news_title;
                        if (_news_image != "" && _news_image != null)
                            objpublishRow.News_Image = _news_image;
                        else
                            objpublishRow.News_Image = objrow.News_Image;
                        objpublishRow.News_Source = _news_source;
                        objpublishRow.News_InitContent = objrow.News_InitialContent;
                        objpublishRow.News_Content = _news_content;
                        objpublishRow.News_Athor = objrow.News_Author;
                        objpublishRow.News_Approver = HttpContext.Current.User.Identity.Name;
                        objpublishRow.News_Status = 3;
                        objpublishRow.Extension1 = _str_Extension1;
                        objpublishRow.Extension2 = _str_Extension2;
                        objpublishRow.Extension3 = _str_Extension3;
                        objpublishRow.Extension4 = _str_Extension4;

                        if (dtPublish.Year != 9999) {
                            // Neu khong phai la news da published ma khong chon ngay Xuat ban thi update lai ngay gio
                            if (dtPublish.Year != 2000)
                                objpublishRow.News_PublishDate = dtPublish;
                            else
                                objpublishRow.News_PublishDate = DateTime.Now;
                        }

                        objpublishRow.News_isFocus = _news_isfocus;
                        objpublishRow.News_Mode = _news_type;
                        if (_related_news != "" && _related_news != null)
                            objpublishRow.News_Relation = objrow.News_Relation;
                        else
                            objpublishRow.News_Relation = _related_news;
                        if (_other_cat != "" && _other_cat != null)
                            objpublishRow.News_OtherCat = objrow.News_OtherCat;
                        else
                            objpublishRow.News_OtherCat = _other_cat;

                        objpublishRow.Template = _template;
                        objpublishRow.isComment = _isShowComment;
                        objpublishRow.isUserRate = _isShowRate;
                        objpublishRow.News_ImageNote = _news_title_image;
                    }

                    #endregion
                    //truong hop co media object
                    objDb.News_MediaCollection.DeleteByNews_ID(_news_id);
                    if (_obj_media != null && _obj_media != "")
                    {
                        News_MediaRow objMrow = null;
                        string[] mediaIds = _obj_media.Split(",".ToCharArray());
                        foreach (string strMediaId in mediaIds)
                        {
                            objMrow = new News_MediaRow();
                            objMrow.News_ID = _news_id;
                            objMrow.Object_ID = Convert.ToInt32(strMediaId);
                            objDb.News_MediaCollection.Insert(objMrow);
                        }
                    }
                    if (!objDb.NewsCollection.Update(objrow)) throw new Exception("Không cập nhật được bản tin");

                    #region Cap nhap vao bang News, Action va  NewsPublished
                    NewsPublishedRow objCurrRow;
                    //neu la send thi moi cap nhat Action

                    if (objrow.News_Status == 3) {
                        //thuc hien xoa neu ton tai tin do va chen moi vao bang xuat ban
                        objCurrRow = objDb.NewsPublishedCollection.GetByPrimaryKey(_news_id);
                        if (objCurrRow != null) {
                            objDb.NewsPublishedCollection.DeleteByPrimaryKey(_news_id);
                        }
                        objDb.NewsPublishedCollection.Insert(objpublishRow);

                        //#region Xu ly bai noi bat
                        //if (_isNoiBatNhat) {
                        //    // Neu la bai noi bat nhat Trang chu thi insert vao vi tri dau tien cua cua bang BonBaiNoiBat

                        //    objDb.CallStoredProcedure("NoiBatNhatTrangChu", new object[] { objArow.News_ID }, new string[] { "@news_id" }, false);
                        //} else {
                        //    // Neu bai nay dg la BaiNoiBatNhat thi khi uncheck di, se remove ra khoi BonBaiNoiBat
                        //    //objDb.SelectQuery(" Delete BonBaiNoiBat Where News_Id = " + objpublishRow.News_ID);
                        //    objDb.CallStoredProcedure("DeleteNoiBatNhatTrangChu", new object[] { objArow.News_ID }, new string[] { "@news_id" }, false);
                        //}

                        //#endregion
                    }
                    #endregion

                    if (strActionEdit.Trim() != "")
                        strAction += "<br/>" + strActionEdit;
                    //LogAction.InsertMemCache(HttpContext.Current.User.Identity.Name, DateTime.Now, strAction, type_log_action, objrow.News_ID);

                    objDb.CommitTransaction();

                    
                    toReturn = true;

                    #region Delete NewsTemp
                    DFISYS.BO.Editoral.Draft.DraftHelper.DeleteNewsTempByNewsID(_news_id.ToString());
                    DFISYS.BO.Editoral.Draft.DraftHelper.DeleteNewsTempByTempID(_newsTemp_id);
                    #endregion
                }
            }
            catch (Exception ex) {
                objDb.RollbackTransaction();
                toReturn = false;

                throw ex;
            }
            finally {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["newsid"] != null)
                    HttpContext.Current.Session.Remove("newsid");
                objDb.Close();
            }

            return toReturn;
        }

        public static bool IsNoiBatNhatTrangChu(long news_id) {
            bool isReturn = false;
            DataTable dt = null;
            using (MainDB objDb = new MainDB()) {
                dt = (DataTable)objDb.CallStoredProcedure("IsNoiBatNhatTrangChu", new object[] { news_id }, new string[] { "@news_id" }, true);
            }
            if (dt != null && dt.Rows.Count > 0) {
                if (dt.Rows[0][0].ToString() == "0")
                    isReturn = false;
                else
                    isReturn = true;
            }

            return isReturn;
        }
         
        public static NewsRow GetNewsInfo_NewsExtension(long _news_id, bool isUpdateAction)
        {
            NewsRow _newsrow = null;
            using (MainDB objDb = new MainDB())
            {
                //_newsrow = objDb.NewsCollection.GetByPrimaryKey(_news_id);
                #region Get thong tin chi tiet cua bang News
                DataTable table = objDb.StoredProcedures.vc_Execute_Sql("SELECT * FROM News_Extension WHERE News_ID = " + _news_id);
                if (table != null && table.Rows.Count != 0)
                {
                    _newsrow = new NewsRow();
                    _newsrow.Cat_ID = table.Rows[0]["Cat_ID"] == System.DBNull.Value ? 0 : Convert.ToInt32(table.Rows[0]["Cat_ID"].ToString());
                    _newsrow.IsCat_IDNull = table.Rows[0]["Cat_ID"] == System.DBNull.Value ? true : false;

                    _newsrow.isComment = table.Rows[0]["isComment"] == System.DBNull.Value ? false : Convert.ToBoolean(table.Rows[0]["isComment"]);
                    _newsrow.IsisCommentNull = table.Rows[0]["isComment"] == System.DBNull.Value ? true : false;

                    _newsrow.isUserRate = table.Rows[0]["isUserRate"] == System.DBNull.Value ? true : Convert.ToBoolean(table.Rows[0]["isUserRate"]);
                    _newsrow.IsisUserRateNull = table.Rows[0]["isUserRate"] == System.DBNull.Value ? true : false;

                    _newsrow.News_Approver = table.Rows[0]["News_Approver"] == System.DBNull.Value ? "" : table.Rows[0]["News_Approver"].ToString();
                    _newsrow.News_Author = table.Rows[0]["News_Author"] == System.DBNull.Value ? "" : table.Rows[0]["News_Author"].ToString();
                    _newsrow.News_Content = table.Rows[0]["News_Content"] == System.DBNull.Value ? "" : table.Rows[0]["News_Content"].ToString();

                    _newsrow.News_CreateDate = table.Rows[0]["News_CreateDate"] == System.DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(table.Rows[0]["News_CreateDate"]);
                    _newsrow.IsNews_CreateDateNull = table.Rows[0]["News_CreateDate"] == System.DBNull.Value ? true : false;

                    _newsrow.News_CurrEditor = table.Rows[0]["News_CurrEditor"] == System.DBNull.Value ? "" : table.Rows[0]["News_CurrEditor"].ToString();

                    _newsrow.News_ID = table.Rows[0]["News_ID"] == System.DBNull.Value ? -1 : Convert.ToInt64(table.Rows[0]["News_ID"]);
                    _newsrow.News_Image = table.Rows[0]["News_Image"] == System.DBNull.Value ? "" : table.Rows[0]["News_Image"].ToString();
                    _newsrow.News_ImageNote = table.Rows[0]["News_ImageNote"] == System.DBNull.Value ? "" : table.Rows[0]["News_ImageNote"].ToString();
                    _newsrow.News_InitialContent = table.Rows[0]["News_InitialContent"] == System.DBNull.Value ? "" : table.Rows[0]["News_InitialContent"].ToString();

                    _newsrow.News_isFocus = table.Rows[0]["News_isFocus"] == System.DBNull.Value ? true : (bool)table.Rows[0]["News_isFocus"];
                    _newsrow.IsNews_isFocusNull = table.Rows[0]["News_isFocus"] == System.DBNull.Value ? true : false;

                    _newsrow.News_Mode = table.Rows[0]["News_Mode"] == System.DBNull.Value ? -1 : Convert.ToInt32(table.Rows[0]["News_Mode"]);
                    _newsrow.IsNews_ModeNull = table.Rows[0]["News_Mode"] == System.DBNull.Value ? true : false;

                    _newsrow.News_ModifiedDate = table.Rows[0]["News_ModifiedDate"] == System.DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(table.Rows[0]["News_ModifiedDate"]);
                    _newsrow.IsNews_ModifiedDateNull = table.Rows[0]["News_ModifiedDate"] == System.DBNull.Value ? true : false;

                    _newsrow.News_OtherCat = table.Rows[0]["News_OtherCat"] == System.DBNull.Value ? "" : table.Rows[0]["News_OtherCat"].ToString();

                    _newsrow.News_PublishDate = table.Rows[0]["News_PublishDate"] == System.DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(table.Rows[0]["News_PublishDate"]);
                    _newsrow.IsNews_PublishDateNull = table.Rows[0]["News_PublishDate"] == System.DBNull.Value ? true : false;

                    _newsrow.News_Rate = table.Rows[0]["News_Rate"] == System.DBNull.Value ? -1 : Convert.ToDecimal(table.Rows[0]["News_Rate"]);
                    _newsrow.IsNews_RateNull = table.Rows[0]["News_Rate"] == System.DBNull.Value ? true : false;

                    _newsrow.News_Relation = table.Rows[0]["News_Relation"] == System.DBNull.Value ? "" : table.Rows[0]["News_Relation"].ToString();
                    _newsrow.News_Source = table.Rows[0]["News_Source"] == System.DBNull.Value ? "" : table.Rows[0]["News_Source"].ToString();

                    _newsrow.News_Status = table.Rows[0]["News_Status"] == System.DBNull.Value ? -1 : Convert.ToInt32(table.Rows[0]["News_Status"]);
                    _newsrow.IsNews_StatusNull = table.Rows[0]["News_Status"] == System.DBNull.Value ? true : false;

                    _newsrow.Template = table.Rows[0]["Template"] == System.DBNull.Value ? 0 : Convert.ToInt32(table.Rows[0]["Template"]);

                    _newsrow.News_Subtitle = table.Rows[0]["News_Subtitle"] == System.DBNull.Value ? "" : table.Rows[0]["News_Subtitle"].ToString();

                    _newsrow.News_SwitchTime = table.Rows[0]["News_SwitchTime"] == System.DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(table.Rows[0]["News_SwitchTime"]);
                    _newsrow.IsNews_SwitchTimeNull = table.Rows[0]["News_SwitchTime"] == System.DBNull.Value ? true : false;

                    _newsrow.News_Title = table.Rows[0]["News_Title"] == System.DBNull.Value ? "" : table.Rows[0]["News_Title"].ToString();

                    _newsrow.News_ViewNum = table.Rows[0]["News_ViewNum"] == System.DBNull.Value ? -1 : Convert.ToInt32(table.Rows[0]["News_ViewNum"]);
                    _newsrow.IsNews_ViewNumNull = table.Rows[0]["News_ViewNum"] == System.DBNull.Value ? true : false;

                    _newsrow.Icon = table.Rows[0]["Icon"] == System.DBNull.Value ? "" : table.Rows[0]["Icon"].ToString();
                    _newsrow.Extension1 = table.Rows[0]["Extension1"] == System.DBNull.Value ? "" : table.Rows[0]["Extension1"].ToString();
                    _newsrow.Extension2 = table.Rows[0]["Extension2"] == System.DBNull.Value ? "" : table.Rows[0]["Extension2"].ToString();
                    _newsrow.Extension3 = table.Rows[0]["Extension3"] == System.DBNull.Value ? "" : table.Rows[0]["Extension3"].ToString();
                    _newsrow.Extension4 = table.Rows[0]["Extension4"] == System.DBNull.Value ? -1 : Convert.ToInt32(table.Rows[0]["Extension4"].ToString());
                    _newsrow.IsExtension4Null = table.Rows[0]["Extension4"] == System.DBNull.Value ? true : false;

                    #region Cập nhập vào bảng Action
                    if (isUpdateAction == true)
                    {
                        string strcpmode = HttpContext.Current.Request.QueryString["cpmode"].ToString();
                        if (strcpmode.ToLower().IndexOf("editwaitlist") >= 0 || strcpmode.ToLower().IndexOf("approvalwaitlis") >= 0)
                        {
                            ActionRow objArow = new ActionRow();
                            ActionRow objcurrAction = getLastestAction(_news_id);
                            objArow.CreateDate = DateTime.Now;
                            objArow.News_ID = _newsrow.News_ID;
                            //sender chinh la nguoi cu - tot nhat k nen cho null - lay last action de tim sender
                            objArow.Sender_ID = objcurrAction.Sender_ID;
                            //dat lai receiver chinh la nguoi hien tai thuc hien thao tac voi tin
                            objArow.Reciver_ID = HttpContext.Current.User.Identity.Name;
                            //Co nghia news_status =0,1 hoac 2
                            if (_newsrow.News_Status == 0)
                            {
                                objArow.ActionType = 0;
                                objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Khôi phục từ tin bị xoá";
                            }
                            if (_newsrow.News_Status == 1)
                            {
                                objArow.ActionType = 1;
                                objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Nhận biên tập";
                            }
                            if (_newsrow.News_Status == 2)
                            {
                                objArow.ActionType = 2;
                                objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Nhận duyệt";
                            }
                            if (_newsrow.News_Status == 3)
                            {
                                objArow.ActionType = 3;
                                objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Cập nhật tin đã xuất bản";
                            }
                            
                                // co the con trang thai tiep theo
                                objDb.ActionCollection.Insert(objArow);
                            
                        }
                    }

                    #endregion

                }
                else
                    _newsrow = GetNewsInfo_News(_news_id, isUpdateAction);
                #endregion
            }

            return _newsrow;
        }

        public static NewsRow GetNewsInfo_News(long _news_id, bool isUpdateAction) {
            NewsRow _newsrow = null;
            using (MainDB objDb = new MainDB()) {
                //_newsrow = objDb.NewsCollection.GetByPrimaryKey(_news_id);
                #region Get thong tin chi tiet cua bang News
                DataTable table = objDb.StoredProcedures.vc_Execute_Sql("SELECT * FROM News WHERE News_ID = " + _news_id);
                if (table.Rows.Count != 0) {
                    _newsrow = new NewsRow();
                    _newsrow.Cat_ID = table.Rows[0]["Cat_ID"] == System.DBNull.Value ? 0 : Convert.ToInt32(table.Rows[0]["Cat_ID"].ToString());
                    _newsrow.IsCat_IDNull = table.Rows[0]["Cat_ID"] == System.DBNull.Value ? true : false;

                    _newsrow.isComment = table.Rows[0]["isComment"] == System.DBNull.Value ? false : Convert.ToBoolean(table.Rows[0]["isComment"]);
                    _newsrow.IsisCommentNull = table.Rows[0]["isComment"] == System.DBNull.Value ? true : false;

                    _newsrow.isUserRate = table.Rows[0]["isUserRate"] == System.DBNull.Value ? true : Convert.ToBoolean(table.Rows[0]["isUserRate"]);
                    _newsrow.IsisUserRateNull = table.Rows[0]["isUserRate"] == System.DBNull.Value ? true : false;

                    _newsrow.News_Approver = table.Rows[0]["News_Approver"] == System.DBNull.Value ? "" : table.Rows[0]["News_Approver"].ToString();
                    _newsrow.News_Author = table.Rows[0]["News_Author"] == System.DBNull.Value ? "" : table.Rows[0]["News_Author"].ToString();
                    _newsrow.News_Content = table.Rows[0]["News_Content"] == System.DBNull.Value ? "" : table.Rows[0]["News_Content"].ToString();

                    _newsrow.News_CreateDate = table.Rows[0]["News_CreateDate"] == System.DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(table.Rows[0]["News_CreateDate"]);
                    _newsrow.IsNews_CreateDateNull = table.Rows[0]["News_CreateDate"] == System.DBNull.Value ? true : false;

                    _newsrow.News_CurrEditor = table.Rows[0]["News_CurrEditor"] == System.DBNull.Value ? "" : table.Rows[0]["News_CurrEditor"].ToString();

                    _newsrow.News_ID = table.Rows[0]["News_ID"] == System.DBNull.Value ? -1 : Convert.ToInt64(table.Rows[0]["News_ID"]);
                    _newsrow.News_Image = table.Rows[0]["News_Image"] == System.DBNull.Value ? "" : table.Rows[0]["News_Image"].ToString();
                    _newsrow.News_ImageNote = table.Rows[0]["News_ImageNote"] == System.DBNull.Value ? "" : table.Rows[0]["News_ImageNote"].ToString();
                    _newsrow.News_InitialContent = table.Rows[0]["News_InitialContent"] == System.DBNull.Value ? "" : table.Rows[0]["News_InitialContent"].ToString();

                    _newsrow.News_isFocus = table.Rows[0]["News_isFocus"] == System.DBNull.Value ? true : (bool)table.Rows[0]["News_isFocus"];
                    _newsrow.IsNews_isFocusNull = table.Rows[0]["News_isFocus"] == System.DBNull.Value ? true : false;

                    _newsrow.News_Mode = table.Rows[0]["News_Mode"] == System.DBNull.Value ? -1 : Convert.ToInt32(table.Rows[0]["News_Mode"]);
                    _newsrow.IsNews_ModeNull = table.Rows[0]["News_Mode"] == System.DBNull.Value ? true : false;

                    _newsrow.News_ModifiedDate = table.Rows[0]["News_ModifiedDate"] == System.DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(table.Rows[0]["News_ModifiedDate"]);
                    _newsrow.IsNews_ModifiedDateNull = table.Rows[0]["News_ModifiedDate"] == System.DBNull.Value ? true : false;

                    _newsrow.News_OtherCat = table.Rows[0]["News_OtherCat"] == System.DBNull.Value ? "" : table.Rows[0]["News_OtherCat"].ToString();

                    _newsrow.News_PublishDate = table.Rows[0]["News_PublishDate"] == System.DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(table.Rows[0]["News_PublishDate"]);
                    _newsrow.IsNews_PublishDateNull = table.Rows[0]["News_PublishDate"] == System.DBNull.Value ? true : false;

                    _newsrow.News_Rate = table.Rows[0]["News_Rate"] == System.DBNull.Value ? -1 : Convert.ToDecimal(table.Rows[0]["News_Rate"]);
                    _newsrow.IsNews_RateNull = table.Rows[0]["News_Rate"] == System.DBNull.Value ? true : false;

                    _newsrow.News_Relation = table.Rows[0]["News_Relation"] == System.DBNull.Value ? "" : table.Rows[0]["News_Relation"].ToString();
                    _newsrow.News_Source = table.Rows[0]["News_Source"] == System.DBNull.Value ? "" : table.Rows[0]["News_Source"].ToString();

                    _newsrow.News_Status = table.Rows[0]["News_Status"] == System.DBNull.Value ? -1 : Convert.ToInt32(table.Rows[0]["News_Status"]);
                    _newsrow.IsNews_StatusNull = table.Rows[0]["News_Status"] == System.DBNull.Value ? true : false;

                    _newsrow.Template = table.Rows[0]["Template"] == System.DBNull.Value ? 0 : Convert.ToInt32(table.Rows[0]["Template"]);

                    _newsrow.News_Subtitle = table.Rows[0]["News_Subtitle"] == System.DBNull.Value ? "" : table.Rows[0]["News_Subtitle"].ToString();

                    _newsrow.News_SwitchTime = table.Rows[0]["News_SwitchTime"] == System.DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(table.Rows[0]["News_SwitchTime"]);
                    _newsrow.IsNews_SwitchTimeNull = table.Rows[0]["News_SwitchTime"] == System.DBNull.Value ? true : false;

                    _newsrow.News_Title = table.Rows[0]["News_Title"] == System.DBNull.Value ? "" : table.Rows[0]["News_Title"].ToString();

                    _newsrow.News_ViewNum = table.Rows[0]["News_ViewNum"] == System.DBNull.Value ? -1 : Convert.ToInt32(table.Rows[0]["News_ViewNum"]);
                    _newsrow.IsNews_ViewNumNull = table.Rows[0]["News_ViewNum"] == System.DBNull.Value ? true : false;

                    _newsrow.Icon = table.Rows[0]["Icon"] == System.DBNull.Value ? "" : table.Rows[0]["Icon"].ToString();
                    _newsrow.Extension1 = table.Rows[0]["Extension1"] == System.DBNull.Value ? "" : table.Rows[0]["Extension1"].ToString();
                    _newsrow.Extension2 = table.Rows[0]["Extension2"] == System.DBNull.Value ? "" : table.Rows[0]["Extension2"].ToString();
                    _newsrow.Extension3 = table.Rows[0]["Extension3"] == System.DBNull.Value ? "" : table.Rows[0]["Extension3"].ToString();
                    _newsrow.Extension4 = table.Rows[0]["Extension4"] == System.DBNull.Value ? -1 : Convert.ToInt32(table.Rows[0]["Extension4"].ToString());
                    _newsrow.IsExtension4Null = table.Rows[0]["Extension4"] == System.DBNull.Value ? true : false;


                }
                #endregion
            }

            #region Cập nhập vào bảng Action
            if (isUpdateAction == true) {
                string strcpmode = HttpContext.Current.Request.QueryString["cpmode"].ToString();
                if (strcpmode.ToLower().IndexOf("editwaitlist") >= 0 || strcpmode.ToLower().IndexOf("approvalwaitlis") >= 0) {
                    ActionRow objArow = new ActionRow();
                    ActionRow objcurrAction = getLastestAction(_news_id);
                    objArow.CreateDate = DateTime.Now;
                    objArow.News_ID = _newsrow.News_ID;
                    //sender chinh la nguoi cu - tot nhat k nen cho null - lay last action de tim sender
                    objArow.Sender_ID = objcurrAction.Sender_ID;
                    //dat lai receiver chinh la nguoi hien tai thuc hien thao tac voi tin
                    objArow.Reciver_ID = HttpContext.Current.User.Identity.Name;
                    //Co nghia news_status =0,1 hoac 2
                    if (_newsrow.News_Status == 0) {
                        objArow.ActionType = 0;
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Khôi phục từ tin bị xoá";
                    }
                    if (_newsrow.News_Status == 1) {
                        objArow.ActionType = 1;
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Nhận biên tập";
                    }
                    if (_newsrow.News_Status == 2) {
                        objArow.ActionType = 2;
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Nhận duyệt";
                    }
                    if (_newsrow.News_Status == 3)
                    {
                        objArow.ActionType = 3;
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Cập nhật tin đã xuất bản";
                    }
                    using (MainDB objDb = new MainDB()) {
                        // co the con trang thai tiep theo
                        objDb.ActionCollection.Insert(objArow);
                    }
                }
            }

            #endregion

            return _newsrow;
        }

        public static NewsRow GetNewsInfo(long _news_id) {
            NewsRow _newsrow;
            using (MainDB objDb = new MainDB()) {
                _newsrow = objDb.NewsCollection.GetByPrimaryKey(_news_id);
            }

            return _newsrow;
        }

        public static NewsRow GetNewsByPrimaryKey(long _news_id) {
            string strcpmode = HttpContext.Current.Request.QueryString["cpmode"].ToString();
            NewsRow _newsrow;
            using (MainDB objDb = new MainDB()) {
                _newsrow = objDb.NewsCollection.GetByPrimaryKey(_news_id);

                #region Cập nhập vào bảng Action
                if (strcpmode.ToLower().IndexOf("editwaitlist") >= 0 || strcpmode.ToLower().IndexOf("approvalwaitlis") >= 0) {
                    ActionRow objArow = new ActionRow();
                    ActionRow objcurrAction = getLastestAction(_news_id);
                    objArow.CreateDate = DateTime.Now;
                    objArow.News_ID = _newsrow.News_ID;
                    //sender chinh la nguoi cu - tot nhat k nen cho null - lay last action de tim sender
                    objArow.Sender_ID = objcurrAction.Sender_ID;
                    //dat lai receiver chinh la nguoi hien tai thuc hien thao tac voi tin
                    objArow.Reciver_ID = HttpContext.Current.User.Identity.Name;
                    //Co nghia news_status =0,1 hoac 2
                    if (_newsrow.News_Status == 0) {
                        objArow.ActionType = 0;
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Khôi phục từ tin bị xoá";
                    }
                    if (_newsrow.News_Status == 1) {
                        objArow.ActionType = 1;
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Nhận biên tập";
                    }
                    if (_newsrow.News_Status == 2) {
                        objArow.ActionType = 2;
                        objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Nhận duyệt";
                    }


                    // co the con trang thai tiep theo
                    objDb.ActionCollection.Insert(objArow);
                }
                #endregion
            }

            return _newsrow;

        }

        private static ActionRow getLastestAction(long _news_id) {
            ActionRow[] _arows;
            using (MainDB objDb = new MainDB()) {
                _arows = objDb.ActionCollection.GetTopAsArray(1, "News_ID='" + _news_id + "'", "CreateDate DESC");
            }

            return _arows.Length > 0 ? _arows[0] : null;
        }

        public static string getReceiver(long _news_id) {
            return getLastestAction(_news_id).Reciver_ID;
        }

        public static string getSender(long _news_id) {
            ActionRow _arows = getLastestAction(_news_id);
            return _arows != null ? _arows.Sender_ID : "";

        }

        public static void DeleteNewsRelation(string _news_id, string _news_id_relation_delete) {

            NewsRow objNewsRow = NewsEditHelper.GetNewsInfo(Convert.ToInt64(_news_id.Trim()));
            string strNew_NewsId_Relation = objNewsRow.News_Relation.Replace("," + _news_id_relation_delete, "");
            strNew_NewsId_Relation = strNew_NewsId_Relation.Replace(_news_id_relation_delete, "");


            // Xoa dau , o dau chuoi string
            if (strNew_NewsId_Relation.Trim() != "") {
                if (strNew_NewsId_Relation[0] == ',')
                    strNew_NewsId_Relation = strNew_NewsId_Relation.Remove(0, 1);
            }

            using (MainDB objDb = new MainDB()) {

                // Update vao bang news
                objDb.SelectQuery(" Update  News Set News_Relation = '" + strNew_NewsId_Relation + "'" +
                                    " Where News_Id = " + _news_id
                                 );
                //update vao bang news Published
                objDb.SelectQuery(" Update  NewsPublished Set News_Relation = '" + strNew_NewsId_Relation + "'" +
                                    " Where News_Id = " + _news_id
                                 );
            }
        }

        public static string ReplaceEmoticonToImageSrc(string strSapo) {
            try {
                if (strSapo != null) {
                    StreamReader SR;
                    string S;
                    string filename = HttpContext.Current.Server.MapPath(@"\Images\EmoticonOng\EmoticonOng.txt");
                    SR = File.OpenText(filename);
                    S = SR.ReadLine();
                    string[] arIcon;
                    while (S != null) {

                        arIcon = S.Split(' ');
                        strSapo = strSapo.Replace(arIcon[1], "<img src='/Images/EmoticonOng/" + arIcon[0] + ".png'>");
                        S = SR.ReadLine();
                    }
                    SR.Close();
                }
            }
            catch (Exception ex) { throw ex; }

            return strSapo;
        }

        public static string ReplaceImageSrcToEmoticon(string strSapo) {
            try {
                if (strSapo != null) {
                    StreamReader SR;
                    string S;
                    string filename = HttpContext.Current.Server.MapPath(@"\Images\EmoticonOng\EmoticonOng.txt");
                    SR = File.OpenText(filename);
                    S = SR.ReadLine();
                    string[] arIcon;
                    while (S != null) {

                        arIcon = S.Split(' ');
                        strSapo = strSapo.Replace("<img src='/Images/EmoticonOng/" + arIcon[0] + ".png'>", arIcon[1]);
                        S = SR.ReadLine();
                    }
                    SR.Close();

                }
            }
            catch (Exception ex) { }

            return strSapo;

        }

        public static string Get_AllThread_By_NewsID(string id)
        {
            DataTable result;
            string listThread = string.Empty;

            using (MainDB objDb = new MainDB())
            {
                result = objDb.StoredProcedures.vc_Execute_Sql(" select ThreadDetail.Thread_ID as Thread_ID1, Title from ThreadDetail Full Join NewsThread On ThreadDetail.Thread_ID = NewsThread.Thread_ID Where News_ID like '" + id + "'");
            }

            for (int i = 0; i < result.Rows.Count; i++)
            {
                listThread += result.Rows[i]["Thread_ID1"] + ",";
            }
            listThread.TrimEnd(',');

            int length = result.Rows.Count;
            if (length > 0)
            {
                //string[] ids = lst.Split(",".ToCharArray());
                string[] ids = listThread.Split(",".ToCharArray());

                DataRow[] rows = new DataRow[length];
                DataRow r;
                result.Rows.CopyTo(rows, 0);

                string sReturn = "";
                int i = 0, j = 0;
                for (i = 0; i < length; i++)
                {
                    for (j = 0; j < length; j++)
                    {
                        r = (DataRow)rows.GetValue(j);
                        if (((string)ids.GetValue(i)).Equals(r[0].ToString()))
                        {
                            sReturn += r[0] + ";#" + r[1] + "#;#";
                            break;
                        }
                    }
                }
                return sReturn.Substring(0, sReturn.Length - 3);
            }
            return "";
        }

        public static string Get_Media_By_ListsId(string id, string text, string table, string lst) {
            DataTable result;
            lst = lst.TrimEnd(',');
            using (MainDB objDb = new MainDB()) {
                result = objDb.StoredProcedures.vc_Execute_Sql(" select " + id + "," + text + " from " + table + " Where " + id + " in (" + lst + ") ");
            }

            int length = result.Rows.Count;
            if (length > 0) {
                string[] ids = lst.Split(",".ToCharArray());
                DataRow[] rows = new DataRow[length];
                DataRow r;
                result.Rows.CopyTo(rows, 0);

                string sReturn = "";
                int i = 0, j = 0;
                for (i = 0; i < length; i++) {
                    for (j = 0; j < length; j++) {
                        r = (DataRow)rows.GetValue(j);
                        if (((string)ids.GetValue(i)).Equals(r[0].ToString())) {
                            sReturn += r[0] + ";#" + r[1] + "#;#";
                            break;
                        }
                    }
                }
                return sReturn.Substring(0, sReturn.Length - 3);
            }
            return "";
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void CreateNewsPublishedOrFeedBackNews(long _news_id, int _cat_id, string _news_subtitle, string _news_title, string _news_image, string _news_source, string _news_init, string _news_content, string _poster, bool _news_isfocus, int _news_status, int _news_type, string _related_news, string _obj_media, string _other_cat, DateTime _switchtime, bool _isShowComment, bool _isShowRate, int _template, string _news_title_image, string _news_icon, string _thread_id, bool _isFeedBackNews, string _news_CreateDate) {
            if (_news_init != null)
                _news_init = _news_init.Replace(System.Environment.NewLine, "");

            if (_isFeedBackNews == false) {

                #region Gan Thread vao bai
                // Gan Thread vao bai

                if (_thread_id != null) {
                    DFISYS.CoreBO.Threads.Threaddetails objThread = new DFISYS.CoreBO.Threads.Threaddetails();
                    string[] strArThread = _thread_id.Split(',');
                    foreach (string str in strArThread) {
                        objThread.AddnewsThread(_news_id.ToString(), Convert.ToInt32(str));
                    }
                }
                #endregion

                #region Gán giá trị cho bảng NewsCreateNews
                NewsRow objrow = new NewsRow();
                //strResult = _news_id;
                objrow.News_ID = _news_id;
                objrow.Cat_ID = _cat_id;
                objrow.News_Subtitle = _news_subtitle;
                objrow.News_Title = _news_title;
                objrow.News_Image = _news_image;
                objrow.News_Source = _news_source;
                objrow.News_InitialContent = _news_init;
                objrow.News_Content = _news_content;
                objrow.News_Author = _poster;
                //objrow.News_Author = _news_author;
                objrow.News_isFocus = _news_isfocus;
                objrow.News_Status = _news_status;
                objrow.News_Mode = _news_type;
                //Danh sach nhung tin lien quan den tin nay
                objrow.News_Relation = _related_news;
                //objrow.News_FocusImage = _news_imgfocus;
                objrow.News_CreateDate = DateTime.Now;
                objrow.News_ModifiedDate = DateTime.Now;
                objrow.isComment = _isShowComment;
                objrow.isUserRate = _isShowRate;
                objrow.Template = _template;
                objrow.News_ImageNote = _news_title_image;
                objrow.Icon = _news_icon;
                if (_news_status == 3) {
                    if (_switchtime.Year != 2000)
                        objrow.News_PublishDate = _switchtime;
                    else
                        objrow.News_PublishDate = DateTime.Now;

                    objrow.News_Approver = HttpContext.Current.User.Identity.Name;
                }

                //lay other cat neu co
                if (_other_cat != null && _other_cat != "" && _other_cat != "0") {
                    objrow.News_OtherCat = _other_cat;
                }

                #endregion

                #region Gán và Insert giá trị cho bảng Action và NewsPublished
                //Thuc hien cap nhat thong tin vao Action
                ActionRow objArow = new ActionRow();
                objArow.News_ID = objrow.News_ID;
                objArow.Sender_ID = HttpContext.Current.User.Identity.Name;
                objArow.ActionType = _news_status;
                objArow.CreateDate = DateTime.Now;
                if (_news_status == 0) {
                    objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Tạo bài viết";
                    objArow.Reciver_ID = HttpContext.Current.User.Identity.Name;
                }
                if (_news_status == 1) {
                    objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Tạo bài viết và gửi lên chờ biên tập";
                    objrow.News_SwitchTime = DateTime.Now;
                }
                if (_news_status == 2) {
                    objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Tạo bài viết và gửi lên chờ duyệt";
                    objrow.News_SwitchTime = DateTime.Now;
                }
                if (_news_status == 3) {
                    // Chỉ có những tin nào xuất bản thì mới Insert vào bảng News Published
                    objArow.Comment_Title = HttpContext.Current.User.Identity.Name + " Tạo bài viết và xuất bản";


                    //thuc hien xuat ban bai viet
                    NewsPublishedRow objpublishRow = new NewsPublishedRow();
                    objpublishRow.News_ID = _news_id;
                    objpublishRow.Cat_ID = _cat_id;
                    objpublishRow.News_Subtitle = _news_subtitle;
                    objpublishRow.News_Title = _news_title;
                    objpublishRow.News_Image = _news_image;
                    objpublishRow.News_Source = _news_source;
                    objpublishRow.News_InitContent = objrow.News_InitialContent;
                    objpublishRow.News_Content = _news_content;
                    objpublishRow.News_Athor = _poster;
                    objpublishRow.News_Approver = HttpContext.Current.User.Identity.Name;
                    objpublishRow.News_Status = 3;
                    if (_switchtime.Year != 2000)
                        objpublishRow.News_PublishDate = _switchtime;
                    else
                        objpublishRow.News_PublishDate = DateTime.Now;
                    objpublishRow.News_isFocus = _news_isfocus;
                    objpublishRow.News_Mode = _news_type;
                    objpublishRow.News_Relation = _related_news;
                    objpublishRow.News_OtherCat = _other_cat;
                    objpublishRow.Template = _template;
                    objpublishRow.isComment = _isShowComment;
                    objpublishRow.isUserRate = _isShowRate;
                    objpublishRow.News_ImageNote = _news_title_image;
                    objpublishRow.Icon = _news_icon;

                    using (MainDB objDb = new MainDB()) {
                        objDb.NewsPublishedCollection.Insert(objpublishRow);
                    }
                }

                using (MainDB objDb = new MainDB()) {
                    objDb.NewsCollection.Insert(objrow);
                    objDb.ActionCollection.Insert(objArow);
                }

                #endregion

                #region Insert vào bảng MediaObject
                //truong hop co media object
                if (_obj_media != null && _obj_media != "") {
                    News_MediaRow objMrow = null;
                    string[] mediaIds = _obj_media.Split(",".ToCharArray());
                    using (MainDB objDb = new MainDB()) {
                        foreach (string strMediaId in mediaIds) {
                            objMrow = new News_MediaRow();
                            objMrow.News_ID = _news_id;
                            objMrow.Object_ID = Convert.ToInt32(strMediaId);
                            objDb.News_MediaCollection.Insert(objMrow);
                        }
                    }
                }
                #endregion

                #region Insert vao bang LogInfo để thống kê
                //Log objLog = new Log();
                //string strUser_Id = HttpContext.Current.User.Identity.Name;
                //if (_news_status == 1)
                //{
                //    // Đối với bài chờ biên tập
                //    objLog.UpdateLogInfo((int)CountKey.Category_Wait_Edit_Content, _cat_id.ToString());
                //    objLog.UpdateLogInfo((int)CountKey.User_Wait_Edit_Content, strUser_Id);
                //}
                //else if (_news_status == 2)
                //{
                //    // Đối với bài chờ duyet
                //    objLog.UpdateLogInfo((int)CountKey.Category_Wait_Approve_Content, _cat_id.ToString());
                //    objLog.UpdateLogInfo((int)CountKey.User_Wait_Approve_Content, strUser_Id);
                //}
                //else if (_news_status == 3)
                //{
                //    // Đối với bài đã xuất bản
                //    objLog.UpdateLogInfo((int)CountKey.Category_Approved_Content, _cat_id.ToString());
                //    objLog.UpdateLogInfo((int)CountKey.User_Approved_Content, strUser_Id);
                //}


                #endregion
            }
            else {
                // Ghi vao table FeedBackNews
                #region Gán giá trị cho bảng FeedBackNewsRow
                FeedBackNewsRow objrow = new FeedBackNewsRow();
                objrow.News_ID = _news_id;
                objrow.Cat_ID = _cat_id;
                objrow.News_Subtitle = _news_subtitle;
                objrow.News_Title = _news_title;
                objrow.News_Image = _news_image;
                objrow.News_Source = _news_source;
                objrow.News_InitContent = _news_init;
                objrow.News_Content = _news_content;
                objrow.News_Athor = _poster;
                objrow.News_Approver = HttpContext.Current.User.Identity.Name;
                objrow.News_Status = 3;
                if (_switchtime.Year != 2000)
                    objrow.News_PublishDate = _switchtime;
                else
                    objrow.News_PublishDate = DateTime.Now;
                objrow.News_isFocus = _news_isfocus;
                objrow.News_Mode = _news_type;
                objrow.News_Relation = _related_news;
                objrow.News_OtherCat = _other_cat;
                objrow.Template = _template;
                objrow.isComment = _isShowComment;
                objrow.isUserRate = _isShowRate;
                objrow.News_ImageNote = _news_title_image;
                objrow.Icon = _news_icon;
                objrow.News_ModifedDate = Convert.ToDateTime(_news_CreateDate);
                using (MainDB objDb = new MainDB()) {
                    objDb.FeedBackNewsCollection.Insert(objrow);
                }
                #endregion
            }
        }

        public static DataTable GetNewsById(string _news_id) {
            DataTable dt = new DataTable();
            if (_news_id.Trim() != "") {
                using (MainDB objDb = new MainDB()) {
                    dt = objDb.SelectQuery("Select * From News Where News_id = " + _news_id);
                }
            }
            return dt;
        }

        public static string GetExtension1ByNewsID(string news_id) {
            string sReturn = "";
            DataTable dt;
            using (MainDB objDb = new MainDB()) {
                dt = objDb.StoredProcedures.vc_Execute_Sql("Select  Extension1 From News Where News_ID = " + news_id);
            }
            if (dt.Rows.Count > 0 && dt.Rows[0]["Extension1"] != DBNull.Value)
                sReturn = dt.Rows[0]["Extension1"].ToString();

            if (sReturn.Trim() == "")
                sReturn = "-1";
            return sReturn;
        }

        public static string GetNewsTitleByNewsID(string news_id) {
            string sReturn = "";
            DataTable dt;
            using (MainDB objDb = new MainDB()) {
                dt = objDb.StoredProcedures.vc_Execute_Sql("Select News_Title From News Where News_ID = " + news_id);
            }
            if (dt.Rows.Count > 0 && dt.Rows[0]["News_Title"] != DBNull.Value)
                sReturn = dt.Rows[0]["News_Title"].ToString();

            return sReturn;
        }

        public static DataTable GetAllProvinces() {
            DataTable dt;
            using (MainDB objDb = new MainDB()) {
                dt = objDb.StoredProcedures.vc_Execute_Sql("SELECT * FROM Provinces");
            }

            return dt;
        }

        
        public static DataTable GetAllTacGia() {
            DataTable dt;
            using (MainDB objDb = new MainDB()) {
                dt = objDb.StoredProcedures.GetAllTacGia();
            }

            return dt;
        }

        public static DataTable GetAllEditionType()
        {
            DataTable dt;
            using (MainDB objDb = new MainDB())
            {
                dt = objDb.StoredProcedures.GetAllEditionType();
            }

            return dt;
        }

        public static DataTable AutoSave_Insert(long NewsID, int CatID, string NewsTitle, string NewsImage, string NewsInitContent, string NewsContent, DateTime CreateDate) {
            DataTable dt;
            using (MainDB objDb = new MainDB()) {
                dt = objDb.StoredProcedures.AutoSave_Insert(NewsID, CatID, NewsTitle, NewsImage, NewsInitContent, NewsContent, CreateDate);
            }

            return dt;
        }

        

        public static DataTable GetWapContent(long NewsID) {
            DataTable _result = null;
            using (MainDB _db = new MainDB()) {
                _result = _db.SelectQuery("SELECT * FROM News_Wap WHERE NewsID = " + NewsID);
            }

            return _result;
        }

        public static DataTable GetListCrawlerNews() {
            DataTable _result = null;
            using (MainDB _db = new MainDB()) {
                _result = _db.SelectQuery("SELECT * FROM News_Crawler WHERE Status = 0 ORDER BY CrawledDate DESC");
            }

            return _result;
        }

        public static DataTable GetListCrawlerNews(int Top) {
            DataTable _result = null;
            using (MainDB _db = new MainDB()) {
                _result = _db.StoredProcedures.cms_GetListCrawlerNews(Top);
            }

            return _result;
        }

        public static DataTable GetCrawlerNews(int ID) {
            DataTable _result = null;
            using (MainDB _db = new MainDB()) {
                _result = _db.SelectQuery("SELECT * FROM News_Crawler WHERE ID = " + ID + " AND Status = 0");
            }

            return _result;
        }

        public static void UpdateCrawlerStatus(int ID) {
            using (MainDB _db = new MainDB()) {
                _db.SelectQuery("UPDATE News_Crawler SET Status = 1 WHERE ID = " + ID);
            }
        }

        public static void DeleteNewsCrawler(string IDs) {
            using (MainDB _db = new MainDB()) {
                _db.SelectQuery("UPDATE News_Crawler SET Status = 1 WHERE ID IN ( " + IDs + ")");
            }
        }

        

        

         

         
        
        public static void UpdateNewsAttachmentFileType(Int64 NewsID, string Types) {
            using (MainDB __db = new MainDB()) {
                __db.StoredProcedures.cms_UpdateNewsAttachmentFileType(NewsID, Types);
            }
        }

        public static void DeleteNews_Extension(string NewsID)
        {
            using (MainDB __db = new MainDB())
            {
                __db.AnotherNonQuery("delete from News_Extension where news_id=" + NewsID);
            }  
        }

        public static DataTable GetAttachmentsType(Int64 NewsID) {
            DataTable __result = null;

            using (MainDB __db = new MainDB()) {
                __result = __db.StoredProcedures.cms_GetAttachmentsType(NewsID);
            }

            return __result;
        }


         

        public static void SaveWapContent(long NewsID, string Content, int NewsStatus)
        {
            using (MainDB _db = new MainDB())
            {
                _db.StoredProcedures.SaveWapContent(NewsID, Content, NewsStatus);
            }
        }

        public static void UpdateWapContent(long NewsID, string Content)
        {
            using (MainDB _db = new MainDB())
            {
                _db.StoredProcedures.UpdateWapContent(NewsID, Content);
            }
        }
    }
}
