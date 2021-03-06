using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using DFISYS.BO.Editoral.Product;
using DFISYS.BO.Editoral.Product_Category;
using DFISYS.CoreBO.Threads;
using DFISYS.BO.Editoral.Newsedit;
using DFISYS.BO.Editoral.Category;
using DFISYS.User.Security;
using DFISYS.BO.Editoral.Newslist;
using DFISYS.Core.DAL;
using System.IO;
using System.Xml;
using System.Security;
using System.Web.UI.WebControls;
using System.Collections.Generic;




namespace DFISYS.GUI.EditoralOffice.MainOffce.editnews {
    public partial class editnews3 : UserControl {
        protected string strNewsID = "";
        protected string strTempNewsID = "";
        private string referUrl =  string.Empty;
        ProductCategoryController proCatController = new ProductCategoryController();
        #region page load

        private void InitEditor()
        {
            NewsContent.config.toolbar = new object[]
			{
				new object[] { "Source", "-", "Save", "NewPage", "Preview", "-", "Templates" },
				new object[] { "Cut", "Copy", "Paste", "PasteText", "PasteFromWord", "-", "Print", "SpellChecker", "Scayt" },
				new object[] { "Undo", "Redo", "-", "Find", "Replace", "-", "SelectAll", "RemoveFormat" },
				new object[] { "Form", "Checkbox", "Radio", "TextField", "Textarea", "Select", "Button", "ImageButton", "HiddenField" },
				"/",
				new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript" },
				new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote", "CreateDiv" },
				new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
				new object[] { "BidiLtr", "BidiRtl" },
				new object[] { "Link", "Unlink", "Anchor" },
				new object[] { "Image", "Flash", "Table", "HorizontalRule", "Smiley", "SpecialChar", "PageBreak", "Iframe" },
				"/",
				new object[] { "Styles", "Format", "Font", "FontSize" },
				new object[] { "TextColor", "BGColor" },
				new object[] { "Maximize", "ShowBlocks", "-", "About" }
			};
        }
        protected void Page_Load(object source, EventArgs e) {
            Page.ClientScript.RegisterHiddenField("hidEmoticon", txtInit.ClientID.ToString());
            string strcpmode = Request.QueryString["cpmode"].ToString().Replace("add,", "").Replace("#", "");
            string strJSNumberWordSapo = CategoryXMLHepler.GennerateScriptToCheckSapo();
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "sapo", strJSNumberWordSapo);
            referUrl = Request.QueryString["source"];
            InitEditor();
            if (!IsPostBack) {
                if (Request.QueryString["cpmode"] == "add")
                    CreateTempNews();
                else if (Request.QueryString["cpmode"].IndexOf("templist") != -1)
                    Page.RegisterClientScriptBlock("autosave", "<script>$('.form').autosave({ 'interval': 60000 });</script>");

                

                if (Session["newsid"] != null) Session.Remove("newsid");

                //if (Session["EditionType"] != null && Session["EditionType"] != "")
                //{
                //    CategoryHelper.BindListCatDropDownWithEdition(lstCat, Session["EditionType"].ToString());
                //    CategoryHelper.BindCheckBoxListCat(lstOtherCat, Convert.ToInt32(Session["EditionType"].ToString()));
                //}
                //else
                //{
                //    Response.Redirect("/login.aspx");
                //}

                CategoryHelper.BindListCatDropDown(lstCat);
               // proCatController.BuildTreeCat(ddlCatProduct);
                
                //DataTable dtTags = ThreadManagement.BO.ThreadHelper.GetThreadlist(" Thread_RT = 'true'", 10, 0);
                //if(dtTags!=null&&dtTags.Rows.Count>0)
                //{
                //    cblTags.DataSource = dtTags;
                //    cblTags.DataTextField = "Title";
                //    cblTags.DataValueField = "Thread_ID";
                //    cblTags.DataBind();
                //}

                lstCat.Items.RemoveAt(0);

                #region BindTime
                BindYear();
                BindMonth();
                BindDay();
                BindHour();
                BindMinute();
                BindSercond();
                #endregion
                //lay newsref

                //GetAllEditionType();
                LoadProvinces();
                GetAllTacGia();
                //GetNewsLetterCategories();

                if (!string.IsNullOrEmpty(Request.QueryString["NewsRef"])) {
                    ltrEdit.Text = "Chỉnh sửa nội dung";
                    strNewsID = Request.QueryString["NewsRef"];
                    Session["newsid"] = strNewsID;
                    BindNewsEdit(Convert.ToInt64(strNewsID));
                    btnUpdate.Visible = false;

                    hidNewsID.Value = strNewsID;

                    //CheckNewsInNewsLetter();
                }
                else {
                    Session["newsid"] = NewsHelper.GenNewsID();
                    strNewsID = Session["newsid"].ToString();
                    ltrEdit.Text = "Viết mới nội dung";
                    btnUpdated.Visible = false;

                    hidNewsID.Value = strNewsID;

                    // Lấy dữu liệu từ bảng NewsTemp
                    //if (Session["temp_id"] != null && Session["temp_id"].ToString() != "") {
                    //    strTempNewsID = Session["temp_id"].ToString();
                    //    BindNewTemp(strTempNewsID);
                    //    Session.Remove("temp_id");
                    //}
                    //else {
                    //    strTempNewsID = NewsHelper.GenNewsID();
                    //}
                    //Session.Remove("strTempNewsID");
                    //Session["strTempNewsID"] = strTempNewsID;
                }

                //cboMedia.Attributes.Add("onclick", "Preview(this)");

                //kiem tra quyens 
                MainSecurity objSercu = new MainSecurity();
                Permission objPer = objSercu.GetPermission(Page.User.Identity.Name);
                if (objPer.isXuat_Ban_Bai)
                    btnPublish.Visible = true;
                else
                    btnPublish.Visible = false;

                switch (strcpmode) {
                    case "editwaitlist":
                        btnUpdated.Text = "Nhận biên tập";
                        break;
                    case "approvalwaitlist":
                        btnUpdated.Text = "Nhận duyệt";
                        btnSend.Visible = false;
                        break;
                    case "approvinglist":
                        btnSend.Visible = false;
                        break;
                    case "publishedlist":
                        btnPublish.Visible = false;
                        btnSend.Visible = false;
                        btnUpdate.Visible = false;
                        break;
                }
            }
            try
            {
                MediaObject objMediaObject = new MediaObject();
                var Data = objMediaObject.MediaObject_GetAllItemByNews_ID(Convert.ToDecimal(strNewsID));
                if (Data != null && Data.Count > 0)
                {
                    //ltrMedia.Text = Data.Count.ToString();
                }    
            }
            catch(Exception ex)
            {
                
            }
       }

        private void LoadProvinces() {
            DataTable provinces = NewsEditHelper.GetAllProvinces();
            if (provinces != null && provinces.Rows.Count > 0) {
                ddlProvinces.DataSource = provinces;
                ddlProvinces.DataBind();
            }
            ddlProvinces.Items.Insert(0, new ListItem("-- Chọn tỉnh thành --", "0"));
        }

        private void Page_Init(object sender, EventArgs e) {
            // Check security
            if (!IsPostBack && !NewslistHelper.isHasPermission(HttpContext.Current))
                throw new SecurityException("Bạn không có quyền truy cập vào trang này");
        }

        private void BindToDropdown(ListBox cbo, string text) {
            string[] str = HttpUtility.HtmlEncode(text).Replace("#;#", ">").Replace(";#", "<").Split('>');
            string[] lItem;
            foreach (string s in str) {
                if (s == "") continue;
                lItem = s.Split('<');
                lItem[1] = HttpUtility.HtmlDecode(lItem[1]);
                //  if (lItem[1].Replace("\\", "/").IndexOf("/") != -1) lItem[1] = lItem[1].Substring(lItem[1].LastIndexOf("/") + 1, lItem[1].Length - lItem[1].LastIndexOf("/") - 1);
                cbo.Items.Add(new ListItem(lItem[1], lItem[0]));
            }
        }
        #endregion

        /// <summary>
        /// Tạo mới tin trắng và redirect sang trang edit luôn
        /// </summary>
        private void CreateTempNews() {
            string strNewsID = "";
            if (Session["newsid"] == null || Session["newsid"].ToString() == string.Empty)
                strNewsID = NewsHelper.GenNewsID();
            else
                strNewsID = Session["newsid"].ToString();

            long NewsID = 0;
            Int64.TryParse(strNewsID, out NewsID);

            NewsRow objNewsRow = NewsEditHelper.GetNewsInfo_News(NewsID, false);
            if (objNewsRow == null || objNewsRow == default(NewsRow)) {
                objsoure.InsertParameters[4].DefaultValue = txtSelectedFile.Text.Trim();

                objsoure.InsertParameters[0].DefaultValue = strNewsID;
                objsoure.InsertParameters[8].DefaultValue = Page.User.Identity.Name;

                //Trang thai tao moi danh cho tin luu lam
                objsoure.InsertParameters[10].DefaultValue = "0";

                objsoure.InsertParameters[14].DefaultValue = "";
                objsoure.InsertParameters["_str_Extension3"].DefaultValue = txtSourceLink.Text.Trim();

                //template (khong biet de lam j)
                objsoure.InsertParameters[18].DefaultValue = "0";
                objsoure.InsertParameters["_str_Extension3"].DefaultValue = txtSourceLink.Text.Trim();

                objsoure.Insert();
            }
        }

        #region luu lai du lieu vao list tam
        protected void btnUpdate_Click(object sender, EventArgs e) {
            if (Validate()) {
                string strNewsID = "";
                if (Session["newsid"] == null || Session["newsid"].ToString() == string.Empty)
                    strNewsID = NewsHelper.GenNewsID();
                else
                    strNewsID = Session["newsid"].ToString();

                objsoure.InsertParameters[4].DefaultValue = txtSelectedFile.Text.Trim();

                objsoure.InsertParameters[0].DefaultValue = strNewsID;
                objsoure.InsertParameters[8].DefaultValue = Page.User.Identity.Name;
                //Trang thai tao moi danh cho tin luu lam
                objsoure.InsertParameters[10].DefaultValue = "0";
              //  string strOtherCat = ddlCatProduct.SelectedValue;
                //foreach (ListItem item in lstOtherCat.Items) {
                //    if (item.Selected)
                //        strOtherCat += item.Value + ",";
                //}

                //if (strOtherCat != "")
                //    strOtherCat = strOtherCat.Substring(0, strOtherCat.Length - 1);
              //  objsoure.InsertParameters[14].DefaultValue = strOtherCat;
                objsoure.InsertParameters["_str_Extension3"].DefaultValue = txtSourceLink.Text.Trim();

                // Get ViewNum cua Category
                int template = CategoryHelper.getCatInfoAsCategoryRow(Convert.ToInt32(lstCat.SelectedValue)).Cat_ViewNum;
                objsoure.InsertParameters[18].DefaultValue = template.ToString();
                objsoure.InsertParameters["_str_Extension3"].DefaultValue = txtSourceLink.Text.Trim();

                objsoure.Insert();
            }
            if(!String.IsNullOrEmpty(referUrl))
                Response.Redirect(referUrl);
        }

        protected void objsoure_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
            string strcpmode = Request.QueryString["cpmode"];
            //if ((bool)e.ReturnValue) 
            {

               // SaveWapContent(Request.QueryString["NewsRef"], Convert.ToInt32(objsoure.UpdateParameters["_news_status"].DefaultValue));

                //if (lstCat.SelectedValue != "")
                //SaveNewsLetter(Convert.ToInt64(Request.QueryString["NewsRef"]), Convert.ToInt32(lstCat.SelectedValue));

                UpdateNewsFileType(Convert.ToInt64(Request.QueryString["NewsRef"]));

                if (objsoure.UpdateParameters["_news_status"].DefaultValue == "3")
                    Response.Redirect("/office/publishedlist.aspx");
                else if (strcpmode.IndexOf("dellist") >= 0)
                    Response.Redirect("/office/templist.aspx");
                else
                    Response.Redirect("/office/" + strcpmode.Substring(strcpmode.IndexOf(",") + 1) + ".aspx");
            }
            //else
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "UpdatedFailed", "<script>alert('Đã có lỗi xảy ra, thao tác thực hiện không thành công');</script>");
        }

        protected void objsoure_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
            if (Session["newsid"] != null) Session.Remove("newsid");
            string strcpmode = Request.QueryString["cpmode"];

            if ((bool)e.ReturnValue) {

                SaveWapContent(objsoure.InsertParameters[0].DefaultValue, Convert.ToInt32(objsoure.InsertParameters["_news_status"].DefaultValue));

                //if (lstCat.SelectedValue != "")
                //SaveNewsLetter(Convert.ToInt64(objsoure.InsertParameters[0].DefaultValue), Convert.ToInt32(lstCat.SelectedValue));

                UpdateNewsFileType(Convert.ToInt64(objsoure.InsertParameters[0].DefaultValue));

                if (objsoure.InsertParameters["_news_status"].DefaultValue == "3")
                    Response.Redirect("/office/publishedlist.aspx");
                else if (strcpmode == "add" && objsoure.InsertParameters["_news_status"].DefaultValue == "0") {
                    if (Session["newsid"] != null)
                        Session.Remove("newsid");
                    Response.Redirect("/office/add,templist/" + objsoure.InsertParameters[0].DefaultValue + ".aspx");
                }
                else
                    Response.Redirect("/office/templist.aspx");
            }
            else
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "InsertedFailed", "<script>alert('Đã có lỗi xảy ra, thao tác thực hiện không thành công');</script>");
        }

        #endregion

        #region bind du lieu vao form khi edit

        #region WAP

        /// <summary>
        /// Insert sang bảng news wap
        /// </summary>
        /// <param name="NewsID"></param>
        private void SaveWapContent(string NewsID, int NewsStatus) {
            if (chkCopyToWap.Checked) {
                NewsEditHelper.SaveWapContent(Convert.ToInt64(NewsID), NewsContent.Text, NewsStatus);
            }
        }

        #endregion WAP

        private void SetValueForCombo(DropDownList ddl, string i) {
            ddl.SelectedValue = i.ToString();
        }

        private void BindNewsEdit(long _news_id) {
            NewsRow objNewsRow = NewsEditHelper.GetNewsInfo_News(_news_id, false);

            if (objNewsRow != null) {
                lstCat.SelectedValue = objNewsRow.Cat_ID.ToString();
                txtTitle.Text = objNewsRow.News_Title != null ? objNewsRow.News_Title : "";
                txtSubTitle.Text = objNewsRow.News_Subtitle != null ? objNewsRow.News_Subtitle : "";
                txtSource.Text = objNewsRow.News_Source != null ? objNewsRow.News_Source : "";
                txtInit.Text = objNewsRow.News_InitialContent != null ? NewsEditHelper.ReplaceImageSrcToEmoticon(objNewsRow.News_InitialContent) : "";
                txtInit.Text = txtInit.Text.Replace("<br/>", System.Environment.NewLine);
                if (Request.QueryString["redirect"] == null) {
                    NewsContent.Text = objNewsRow.News_Content != null ? objNewsRow.News_Content : "";
                    Session["NewsContent"] = NewsContent.Text;
                }
                else {
                    NewsContent.Text = Session["NewsContent"].ToString();
                }
                chkIsFocus.Checked = objNewsRow.IsNews_isFocusNull != true ? objNewsRow.News_isFocus : false;
                cboIsHot.SelectedValue = objNewsRow.IsNews_ModeNull != true ? objNewsRow.News_Mode.ToString() : "0";

                hdRelatNews.Value = objNewsRow.News_Relation != null ? objNewsRow.News_Relation : "";
               // chkShowComment.Checked = objNewsRow.IsisCommentNull != true ? objNewsRow.isComment : false; //Cho phép hiện ảnh hay ko?
                chkShowRate.Checked = objNewsRow.IsisUserRateNull != true ? objNewsRow.isUserRate : false;
                txtSelectedFile.Text = objNewsRow.News_Image != null ? objNewsRow.News_Image : "";
                txtImageTitle.Text = objNewsRow.News_ImageNote != null ? objNewsRow.News_ImageNote : "";
                txtIcon.Text = objNewsRow.Icon != null ? objNewsRow.Icon : "";
                txtMaCP.Text = objNewsRow.Extension1 != null ? objNewsRow.Extension1 : "";
                txtExtension2.Text = objNewsRow.Extension2 != null ? objNewsRow.Extension2 : "";
                txtSourceLink.Text = objNewsRow.Extension3 != null ? objNewsRow.Extension3 : "";

                //txtExtension4.Text = objNewsRow.IsExtension4Null != true ? objNewsRow.Extension4.ToString() : "";
                ddlAuthor.SelectedValue = objNewsRow.IsExtension4Null != true ? objNewsRow.Extension4.ToString() : "0";

                if (objNewsRow.Template != 0)
                    ddlProvinces.SelectedValue = objNewsRow.Template.ToString();

                if (objNewsRow.News_OtherCat != null) {
                    //string[] strOthers = objNewsRow.News_OtherCat.Split(",".ToCharArray());
                    //for (int i = 0; i < lstOtherCat.Items.Count; i++) {
                    //    foreach (string strItem in strOthers)
                    //        if (strItem == lstOtherCat.Items[i].Value) {
                    //            lstOtherCat.Items[i].Selected = true;
                    //            break;
                    //        }
                    //}
                    //ddlCatProduct.SelectedValue = objNewsRow.News_OtherCat;
                }

                if (!objNewsRow.IsNews_PublishDateNull) {
                    SetValueForCombo(cboMonth, objNewsRow.News_PublishDate.Month.ToString());
                    SetValueForCombo(cboDay, objNewsRow.News_PublishDate.Day.ToString());
                    SetValueForCombo(cboYear, objNewsRow.News_PublishDate.Year.ToString());
                    SetValueForCombo(cboSercond, objNewsRow.News_PublishDate.Second.ToString());
                    SetValueForCombo(cboMinute, objNewsRow.News_PublishDate.Minute.ToString());
                    SetValueForCombo(cboHour, objNewsRow.News_PublishDate.Hour.ToString());
                }
                else {
                    SetValueForCombo(cboMonth, "0");
                    SetValueForCombo(cboDay, "0");
                    SetValueForCombo(cboYear, "2000");
                    SetValueForCombo(cboSercond, "-1");
                    SetValueForCombo(cboMinute, "-1");
                    SetValueForCombo(cboHour, "-1");
                }

                if (!IsPostBack) {
                    hdMedia.Value = DFISYS.BO.Editoral.NewsMedia.NewsMediaHelper.Get_ObjectId_By_NewsId(_news_id);
                    DataTable dtThread = ThreadHelper.SelectThreadByNewsID(_news_id);
                    if (dtThread != null && dtThread.Rows.Count > 0)
                        hidLuongSuKien.Value = dtThread.Rows[0]["Thread_ID"].ToString();
                }


                //Load data to Combobox Tin lien quan;
                string str;
                if (hdRelatNews.Value.TrimEnd(',') != "") {
                    str = NewsEditHelper.Get_Media_By_ListsId("News_ID", "News_Title", "News", hdRelatNews.Value);
                    BindToDropdown(cboNews, str);
                }


                string strThread;
                if (hidLuongSuKien.Value.TrimEnd(',') != "") {
                    //strThread = NewsEditHelper.Get_Media_By_ListsId("Thread_ID", "Title", "NewsThread", hidLuongSuKien.Value);
                    strThread = NewsEditHelper.Get_AllThread_By_NewsID(_news_id.ToString());
                    BindToDropdown(lstThread, strThread);

                    string[] arrThread = hidLuongSuKien.Value.Split(',');
                    if(arrThread!=null&&arrThread.Length>0)
                    {
                        for (int i = 0; i < cblTags.Items.Count; i++)
                        {
                            foreach (string strItem in arrThread)
                                if (strItem == cblTags.Items[i].Value)
                                {
                                    cblTags.Items[i].Selected = true;
                                    break;
                                }
                        }
                    }

                }
                if (hdMedia.Value.TrimEnd(',').Length > 0) {
                    str = NewsEditHelper.Get_Media_By_ListsId("Object_ID", "Object_Url", "MediaObject", hdMedia.Value);
                    BindToDropdown(cboMedia, str);
                }

                LoadAttachmentsType(_news_id);
            }
        }

        private void LoadAttachmentsType(long _news_id) {
            DataTable types = NewsEditHelper.GetAttachmentsType(_news_id);

            if (types != null && types.Rows.Count > 0) {
                DataRow row = null;
                foreach (ListItem item in cblFileType.Items) {
                    for (int i = 0; i < types.Rows.Count; i++) {
                        row = types.Rows[i];
                        if (item.Value == row["Type"].ToString())
                            item.Selected = true;
                    }
                }
            }
        }

        #endregion

        #region Cap nhat lai khi edit
        protected void btnUpdated_Click(object sender, EventArgs e) {
            if (Validate()) {
                MainSecurity objSercu = new MainSecurity();
                Role objrole = objSercu.GetRole(Page.User.Identity.Name);
                string strcpmode = Request.QueryString["cpmode"].ToString().Replace("add,", "").Replace("#", "");
                string strNewsID = Request.QueryString["NewsRef"].ToString();

                objsoure.UpdateParameters[4].DefaultValue = txtSelectedFile.Text.Trim();

                //string strOtherCat = ddlCatProduct.SelectedValue;
                //foreach (ListItem item in lstOtherCat.Items) {
                //    if (item.Selected)
                //        strOtherCat += item.Value + ",";
                //}

                //foreach (ListItem item in cblTags.Items)
                //{
                //    if (item.Selected)
                //        hidLuongSuKien.Value += item.Value + ",";
                //}
                //hidLuongSuKien.Value = hidLuongSuKien.Value.TrimEnd(',');
                
                //UpdateLuongSuKienTin(strNewsID, hidLuongSuKien.Value);

                //// Get cac chuyen muc khac
                //if (strOtherCat != "")
                //    strOtherCat = strOtherCat.Substring(0, strOtherCat.Length - 1);
                //objsoure.UpdateParameters[12].DefaultValue = strOtherCat;

                // Get ViewNum cua Category
                int template = CategoryHelper.getCatInfoAsCategoryRow(Convert.ToInt32(lstCat.SelectedValue)).Cat_ViewNum;
                objsoure.UpdateParameters[17].DefaultValue = template.ToString();
                
                if (objrole.isBienTapVien) {
                    objsoure.UpdateParameters[9].DefaultValue = "0";
                }
                if (strcpmode.IndexOf("editwaitlist") >= 0 || strcpmode.IndexOf("editinglist") >= 0) {
                    objsoure.UpdateParameters[9].DefaultValue = "1";
                }
                if (strcpmode.IndexOf("approvalwaitlist") >= 0 || strcpmode.IndexOf("approvinglist") >= 0) {
                    objsoure.UpdateParameters[9].DefaultValue = "2";
                }

                //tu danh sach bi tra lai - hoac da xuat ban
                if (strcpmode.IndexOf("backlist") >= 0)
                    objsoure.UpdateParameters[9].DefaultValue = "-1";
                //cap nhat tin da xuat ban
                if (strcpmode.IndexOf("publishedlist") >= 0) {
                    objsoure.UpdateParameters[9].DefaultValue = "3";

                    // Neu chon ngay xuat ban voi truong hop Published thi se update lai truong PublishedDate
                    // Con khong chon thi ko update lai
                    //if (getPublishTime().ToString("yyyy-MM-dd") == "2000-01-01")
                    //    objsoure.UpdateParameters[13].DefaultValue = DateTime.MaxValue.ToString();
                    //else
                    objsoure.UpdateParameters[13].DefaultValue = getPublishTime().ToString();
                    objsoure.UpdateParameters["_thread_id"].DefaultValue = hidLuongSuKien.Value.Trim();
                }
                if (strcpmode.IndexOf("dellist") >= 0) {
                    objsoure.UpdateParameters[9].DefaultValue = "0";
                }
                if (strcpmode.IndexOf("removedlist") >= 0) {
                    objsoure.UpdateParameters[9].DefaultValue = "7";
                }

                objsoure.UpdateParameters["_str_Extension3"].DefaultValue = txtSourceLink.Text.Trim();

                objsoure.Update();
            }
            if (!String.IsNullOrEmpty(referUrl))
                Response.Redirect(referUrl);
        }
        #endregion

        #region Gui thang len trong 2 truong hop :Them moi va edit
        protected void btnSend_Click(object sender, EventArgs e) {
            if (Validate()) {
                string strcpmode = Request.QueryString["cpmode"].ToString().Replace("add,", "").Replace("#", "");
                //thuc thi voi cong viec gui thang len. Co 2 truong hop la viet - gui thang, edit - gui thang
                //lay quyen han cua nguoi dung
                MainSecurity objsecu = new MainSecurity();
                Role objrole = objsecu.GetRole(Page.User.Identity.Name);
                //string strNewsID = DateTime.Now.Year + "" + DateTime.Now.Month + DateTime.Now.Day + "" + DateTime.Now.Hour + "" + DateTime.Now.Minute + "" + DateTime.Now.Second + DateTime.Now.Millisecond;

                // Get NewsId
                string strNewsID = "";
                if (Session["newsid"] == null || Session["newsid"].ToString() == "")
                    strNewsID = NewsHelper.GenNewsID();
                else
                    strNewsID = Session["newsid"].ToString();

                Session["newsid"] = "";

                if (Request.QueryString["NewsRef"] != null && Request.QueryString["NewsRef"] != "")
                    objsoure.UpdateParameters[4].DefaultValue = txtSelectedFile.Text.Trim();
                else
                    objsoure.InsertParameters[4].DefaultValue = txtSelectedFile.Text.Trim();

                objsoure.InsertParameters[0].DefaultValue = strNewsID;
                objsoure.InsertParameters[8].DefaultValue = Page.User.Identity.Name;


                //string strOtherCat = ddlCatProduct.SelectedValue;
                //foreach (ListItem item in lstOtherCat.Items) {
                //    if (item.Selected)
                //        strOtherCat += item.Value + ",";
                //}
                //if (strOtherCat != "")
                //    strOtherCat = strOtherCat.Substring(0, strOtherCat.Length - 1);
              //  objsoure.InsertParameters[14].DefaultValue = strOtherCat;
                //cap nhat trang thai update khi send thang isSend=true
                objsoure.UpdateParameters["_isSend"].DefaultValue = "true";


                //Sua va gui thang chua co Other cat
                if (Request.QueryString["NewsRef"] != null && Request.QueryString["NewsRef"] != "") {
                    strNewsID = Request.QueryString["NewsRef"].ToString();

                    //Trang thai tao moi danh cho tin 
                    if (objrole.isThuKyToaSoan || objrole.isThuKyChuyenMuc || objrole.isBienTapVien)
                        objsoure.UpdateParameters[9].DefaultValue = "2";
                    else if (objrole.isPhongVien)
                        objsoure.UpdateParameters[9].DefaultValue = "1";

                    objsoure.Update();

                }
                else//viet va gui thang
				{
                    //Trang thai tao moi danh cho tin luu lam
                    if (objrole.isThuKyToaSoan || objrole.isThuKyChuyenMuc || objrole.isBienTapVien)
                        objsoure.InsertParameters[10].DefaultValue = "2";
                    else if (objrole.isPhongVien)
                        objsoure.InsertParameters[10].DefaultValue = "1";

                    objsoure.InsertParameters[15].DefaultValue = getPublishTime().ToString();
                    objsoure.Insert();
                }

            }
            if (!String.IsNullOrEmpty(referUrl))
                Response.Redirect(referUrl);
        }
        #endregion

        #region comeback
        protected void btnCancel_Click(object sender, EventArgs e) {
            string[] strcpmode = Request.QueryString["cpmode"].ToString().Split(",".ToCharArray());
            if (strcpmode.Length > 1)
                Response.Redirect("/office/" + strcpmode[1].Replace("#", "") + ".aspx");
            else
                Response.Redirect("/office/templist.aspx");
        }
        #endregion



        private bool IsHasPermissionPublicInCat()
        {
            bool IsOk = false;
            if (Page.User.Identity.Name.Trim() == "admin")
                return true;
             MainSecurity objSercu = new MainSecurity();
             DataTable objPer = objSercu.GetPermissionAsTable(Page.User.Identity.Name, 1, CategoryHelper.GetCatParent(Convert.ToInt32(lstCat.SelectedValue)));
            
               if (objPer!=null&&objPer.Rows.Count>0)
               {
                   for(int i = 0;i<objPer.Rows.Count;i++)
                   {
                       if (objPer.Rows[i]["Permission_name"].ToString().Trim().ToLower() == "xuất bản bài")
                       {
                           IsOk = true;
                           break;
                       }
                           
                   }
               }
               if (!IsOk )
               {
                   Page.ClientScript.RegisterClientScriptBlock(GetType(), "alert", "<script>alert('" + "Bạn không có quyền xuất bản bài trong chuyên mục này " + "')</script>");
               }
                
            return IsOk;
        }

        #region xuat ban trong 2 t.h: them moi va edit
        protected void btnPublish_Click(object sender, EventArgs e) {
            if (Validate() && IsHasPermissionPublicInCat())
            {
                // Get CatID cac chuyen muc khac
               // string strOtherCat = ddlCatProduct.SelectedValue;
                //foreach (ListItem item in lstOtherCat.Items) {
                //    if (item.Selected)
                //        strOtherCat += item.Value + ",";
                //}
                //string Tags = "";

                //foreach (ListItem item in cblTags.Items)
                //{
                //    if (item.Selected)
                //        hidLuongSuKien.Value += item.Value + ",";
                //}
                //hidLuongSuKien.Value = hidLuongSuKien.Value.TrimEnd(',');
                //if (strOtherCat != "")
                //    strOtherCat = strOtherCat.Substring(0, strOtherCat.Length - 1);

                string strNewsID = "";
                string strcpmode = Request.QueryString["cpmode"].ToString().Replace("add,", "").Replace("#", "");
                //xet 2 truong hop: 1 la tao va xuat ban, 2 sua va xuất bản
                if (!string.IsNullOrEmpty(Request.QueryString["NewsRef"])) {
                    strNewsID = Request.QueryString["NewsRef"].ToString();

                    objsoure.UpdateParameters[9].DefaultValue = "3";

                    // Neu chon ngay xuat ban voi truong hop chua Published 
                    // thi check xem da chon PublishedDate chua
                    //if (getPublishTime().Year == 2000)
                    //    objsoure.UpdateParameters[13].DefaultValue = DateTime.Now.ToString();
                    //else
                    objsoure.UpdateParameters[13].DefaultValue = getPublishTime().ToString();

                  //  objsoure.UpdateParameters[12].DefaultValue = strOtherCat;
                    objsoure.UpdateParameters["_str_Extension3"].DefaultValue = txtSourceLink.Text.Trim();
                    objsoure.UpdateParameters["_thread_id"].DefaultValue = hidLuongSuKien.Value.Trim();
                    // Get ViewNum cua Category
                    int template = CategoryHelper.getCatInfoAsCategoryRow(Convert.ToInt32(lstCat.SelectedValue)).Cat_ViewNum;
                    objsoure.UpdateParameters[17].DefaultValue = template.ToString();
                }
                else {
                    if (Session["newsid"] == null || Session["newsid"].ToString() == "")
                        strNewsID = NewsHelper.GenNewsID();
                    else
                        strNewsID = Session["newsid"].ToString();

                    objsoure.InsertParameters[0].DefaultValue = strNewsID;
                    objsoure.InsertParameters[8].DefaultValue = Page.User.Identity.Name;
                    //trang thai tin la xuat ban
                    objsoure.InsertParameters[10].DefaultValue = "3";
                    objsoure.InsertParameters["_str_Extension3"].DefaultValue = txtSourceLink.Text.Trim();
                    objsoure.InsertParameters["_thread_id"].DefaultValue = hidLuongSuKien.Value.Trim();
                   // objsoure.InsertParameters[14].DefaultValue = strOtherCat;

                    //if (getPublishTime().Year == 2000)
                    //objsoure.InsertParameters[15].DefaultValue = DateTime.Now.ToString();
                    //else
                    objsoure.InsertParameters[15].DefaultValue = getPublishTime().ToString();

                    // Get ViewNum cua Category
                    int template = CategoryHelper.getCatInfoAsCategoryRow(Convert.ToInt32(lstCat.SelectedValue)).Cat_ViewNum;
                    objsoure.InsertParameters[18].DefaultValue = template.ToString();
                }

                if (Request.QueryString["NewsRef"] != null && Request.QueryString["NewsRef"] != "")
                    objsoure.UpdateParameters[4].DefaultValue = txtSelectedFile.Text.Trim();
                else
                    objsoure.InsertParameters[4].DefaultValue = txtSelectedFile.Text.Trim();


                if (!string.IsNullOrEmpty(Request.QueryString["NewsRef"])) {
                    objsoure.UpdateParameters["_isSend"].DefaultValue = "true";
                    objsoure.Update();
                }
                else
                    objsoure.Insert();
            }
            if (!String.IsNullOrEmpty(referUrl))
                Response.Redirect(referUrl);
        }
        #endregion

        #region Thoi gian
        private DateTime getPublishTime() {
            int intHour, intMinute, intSercond, intYear, intMont, intDay;

            intHour = (cboHour.SelectedValue != "-1" ? Convert.ToInt32(cboHour.SelectedValue) : DateTime.Now.Hour);
            intMinute = (cboMinute.SelectedValue != "-1" ? Convert.ToInt32(cboMinute.SelectedValue) : DateTime.Now.Minute);
            intSercond = (cboSercond.SelectedValue != "-1" ? Convert.ToInt32(cboSercond.SelectedValue) : DateTime.Now.Second);
            intYear = cboYear.SelectedValue != "2000" ? Convert.ToInt32(cboYear.SelectedValue) : DateTime.Now.Year;
            intMont = (cboMonth.SelectedValue != "0" ? Convert.ToInt32(cboMonth.SelectedValue) : DateTime.Now.Month);
            intDay = (cboDay.SelectedValue != "0" ? Convert.ToInt32(cboDay.SelectedValue) : DateTime.Now.Day);
            DateTime dtPublishDate = new DateTime(intYear, intMont, intDay, intHour, intMinute, intSercond);
            return dtPublishDate;
        }
        private void BindYear() {
            DataTable objYearTbl = createData("YAER");
            cboYear.DataSource = objYearTbl;
            cboYear.DataMember = objYearTbl.TableName;
            cboYear.DataValueField = objYearTbl.Columns[0].ColumnName;
            cboYear.DataTextField = objYearTbl.Columns[1].ColumnName;
            cboYear.DataBind();
        }
        private void BindMonth() {
            DataTable objMontTbl = createData("MONTH");
            cboMonth.DataSource = objMontTbl;
            cboMonth.DataMember = objMontTbl.TableName;
            cboMonth.DataValueField = objMontTbl.Columns[0].ColumnName;
            cboMonth.DataTextField = objMontTbl.Columns[1].ColumnName;
            cboMonth.DataBind();
        }
        private void BindDay() {
            DataTable objDayTbl = createData("DAY");
            cboDay.DataSource = objDayTbl;
            cboDay.DataMember = objDayTbl.TableName;
            cboDay.DataValueField = objDayTbl.Columns[0].ColumnName;
            cboDay.DataTextField = objDayTbl.Columns[1].ColumnName;
            cboDay.DataBind();
        }
        private void BindHour() {
            DataTable objHourTbl = createData("HOUR");
            cboHour.DataSource = objHourTbl;
            cboHour.DataMember = objHourTbl.TableName;
            cboHour.DataValueField = objHourTbl.Columns[0].ColumnName;
            cboHour.DataTextField = objHourTbl.Columns[1].ColumnName;
            cboHour.DataBind();
        }
        private void BindMinute() {
            DataTable objMinuteTbl = createData("MINUTE");
            cboMinute.DataSource = objMinuteTbl;
            cboMinute.DataMember = objMinuteTbl.TableName;
            cboMinute.DataValueField = objMinuteTbl.Columns[0].ColumnName;
            cboMinute.DataTextField = objMinuteTbl.Columns[1].ColumnName;
            cboMinute.DataBind();
        }
        private void BindSercond() {
            DataTable objSercondTbl = createData("SERCOND");
            cboSercond.DataSource = objSercondTbl;
            cboSercond.DataMember = objSercondTbl.TableName;
            cboSercond.DataValueField = objSercondTbl.Columns[0].ColumnName;
            cboSercond.DataTextField = objSercondTbl.Columns[1].ColumnName;
            cboSercond.DataBind();
        }
        private DataTable createData(string mode) {
            DataTable objTable = new DataTable(mode);
            objTable.Columns.Add("Key");
            objTable.Columns.Add("Value");
            switch (mode) {
                case "YAER":
                    object[] objArr = new object[2];
                    int intCurYear = DateTime.Now.Year - 1;
                    objArr[0] = 2000;
                    objArr[1] = "Chọn năm";
                    objTable.Rows.Add(objArr);
                    int intMaxYear = intCurYear + 10;
                    for (int i = intCurYear; i <= intMaxYear; i++) {
                        object[] objCurr = new object[2];
                        objCurr[0] = i;
                        objCurr[1] = i;
                        objTable.Rows.Add(objCurr);
                    }
                    break;
                case "MONTH":
                    object[] objMnt = new object[2];
                    objMnt[0] = 0;
                    objMnt[1] = "Chọn tháng";
                    objTable.Rows.Add(objMnt);
                    int intMaxMonth = 12;
                    for (int i = 1; i <= intMaxMonth; i++) {
                        object[] objCurr = new object[2];
                        objCurr[0] = i;
                        objCurr[1] = i;
                        objTable.Rows.Add(objCurr);
                    }
                    break;
                case "DAY":
                    object[] objDay = new object[2];
                    objDay[0] = 0;
                    objDay[1] = "Chọn ngày";
                    objTable.Rows.Add(objDay);
                    int intMaxDay = 31;
                    for (int i = 1; i <= intMaxDay; i++) {
                        object[] objCurr = new object[2];
                        objCurr[0] = i;
                        objCurr[1] = i;
                        objTable.Rows.Add(objCurr);
                    }
                    break;
                case "HOUR":
                    object[] objHOUR = new object[2];
                    objHOUR[0] = -1;
                    objHOUR[1] = "Chọn giờ";
                    objTable.Rows.Add(objHOUR);
                    int intMaxHOUR = 24;
                    for (int i = 0; i < intMaxHOUR; i++) {
                        object[] objCurr = new object[2];
                        objCurr[0] = i;
                        objCurr[1] = i;
                        objTable.Rows.Add(objCurr);
                    }
                    break;
                case "MINUTE":
                    object[] objMINUTE = new object[2];
                    objMINUTE[0] = -1;
                    objMINUTE[1] = "Chọn phút";
                    objTable.Rows.Add(objMINUTE);
                    int intMaxobjMINUTE = 60;
                    for (int i = 0; i <= intMaxobjMINUTE; i++) {
                        object[] objCurr = new object[2];
                        objCurr[0] = i;
                        objCurr[1] = i;
                        objTable.Rows.Add(objCurr);
                    }
                    break;
                case "SERCOND":
                    object[] objSERCOND = new object[2];
                    objSERCOND[0] = -1;
                    objSERCOND[1] = "Chọn giây";
                    objTable.Rows.Add(objSERCOND);
                    int intMaxSERCOND = 60;
                    for (int i = 0; i <= intMaxSERCOND; i++) {
                        object[] objCurr = new object[2];
                        objCurr[0] = i;
                        objCurr[1] = i;
                        objTable.Rows.Add(objCurr);
                    }
                    break;
            }
            return objTable;
        }
        #endregion

        #region validate
        private bool Validate() {
            string message = string.Empty;

            // ok khi không đụng độ và Cat khác rỗng
            bool isOK = !isConcurrency(ref message) && !string.IsNullOrEmpty(lstCat.SelectedValue.Trim());



            if (!isOK && !string.IsNullOrEmpty(message)) {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alert", "<script>alert('" + message + "')</script>");
            }

            return isOK;
        }
        #endregion

        //private void CheckNewsInNewsLetter() {
        //    long newsid = Convert.ToInt64(Request.QueryString["NewsRef"]);

        //    DataTable result = NewsletterHelper.CheckNewsInNewsLetter(newsid);
        //    if (result != null && result.Rows.Count > 0) {
        //        for (int i = 0; i < result.Rows.Count; i++) {
        //            foreach (ListItem item in lstNewsLetter.Items) {
        //                if (item.Value == result.Rows[i]["CatID"].ToString())
        //                    item.Selected = true;
        //            }
        //        }
        //    }
        //}

        //private void GetNewsLetterCategories() {
        //    DataTable cats = UserStatisticHelper.GetAllEmailCategory();
        //    if (cats != null && cats.Rows.Count > 0) {
        //        lstNewsLetter.DataSource = cats;
        //        lstNewsLetter.DataBind();
        //    }
        //}

        //private void SaveNewsLetter(long NewsID, int CatID) {
        //    List<ListItem> selected = new List<ListItem>();

        //    foreach (ListItem item in lstNewsLetter.Items) {
        //        if (item.Selected)
        //            selected.Add(item);
        //    }

        //    DateTime today = DateTime.Now;

        //    if (selected.Count > 0) {
        //        DataTable newsletterbydate = null;

        //        int NewsLetterID = 0;

        //        foreach (ListItem cat in selected) {
        //            newsletterbydate = NewsletterHelper.CheckNewsLetterByDate(Convert.ToInt32(cat.Value), today);

        //            // Đã có newsletter theo ngày rồi
        //            if (newsletterbydate != null && newsletterbydate.Rows.Count > 0) {
        //                //insert news vao newsletter
        //                NewsLetterID = Convert.ToInt32(newsletterbydate.Rows[0]["ID"]);
        //                NewsletterHelper.InsertNewsLetterDetails(NewsLetterID, NewsID, CatID, 0);
        //            }
        //            //chưa có thì tạo mới & insert news vào
        //            else {
        //                DataTable newsletter = NewsletterHelper.InsertNewsLetter(cat.Text + today.ToString(" dd - MM - yyyy"), Convert.ToInt32(cat.Value), today);

        //                if (newsletter != null && newsletter.Rows.Count > 0) {
        //                    NewsLetterID = Convert.ToInt32(newsletter.Rows[0][0].ToString());

        //                    NewsletterHelper.InsertNewsLetterDetails(NewsLetterID, NewsID, CatID, 0);
        //                }
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Kiểm tra xem bài viết này có đồng thời 2 người can thiệp không [bacth, 12:29 PM 5/31/2008]
        /// </summary>
        /// <param name="message">Thông báo trong trường hợp có sự động độ</param>
        /// <returns></returns>
        private bool isConcurrency(ref string message) {
            if (string.IsNullOrEmpty(Request.QueryString["NewsRef"])) return false; // bài viết mới - không đụng độ
            string cpMode = Request.QueryString["cpmode"].Replace("add,", string.Empty);

            long newsId = 0;
            long.TryParse(Request.QueryString["NewsRef"], out newsId);
            NewsRow news = NewsEditHelper.GetNewsInfo(newsId);
            if (news == null) {
                message = "Bài viết không tồn tại hoặc đã bị xóa";
                return true;
            }

            if (cpMode.Equals("editwaitlist")) {
                if (news.News_Status != 1) {
                    message = "Bài viết đã có người khác cập nhật";
                    return true;
                }
                else {
                    // kiểm tra xem có ai nhận biên tập không?
                    string otherUser = NewsEditHelper.getReceiver(newsId);
                    if (!string.IsNullOrEmpty(otherUser) && otherUser != Page.User.Identity.Name) {
                        message = otherUser + " đã nhận biên tập bài này trong khi bạn đang xem bài viết";
                        return true;
                    }
                }
            }
            else if (cpMode.Equals("approvalwaitlist")) {
                if (news.News_Status != 2) {
                    message = "Bài viết đã có người khác cập nhật";
                    return true;
                }
                else {
                    // kiểm tra xem có ai nhận duyệt không?
                    string otherUser = NewsEditHelper.getReceiver(newsId);
                    if (!string.IsNullOrEmpty(otherUser) && otherUser != Page.User.Identity.Name) {
                        message = otherUser + " đã nhận duyệt bài này trong khi bạn đang xem bài viết";
                        return true;
                    }
                }
            }
            return false;
        }

        private void GetAllTacGia() {
            DataTable authors = NewsEditHelper.GetAllTacGia();
            if (authors != null && authors.Rows.Count > 0) {
                ddlAuthor.DataSource = authors;
                ddlAuthor.DataBind();
            }

            ddlAuthor.Items.Insert(0, new ListItem("-- Chọn tác giả --", "0"));
        }

        //private void GetAllEditionType()
        //{
        //    DataTable editonType = NewsEditHelper.GetAllEditionType();
        //    if (editonType != null && editonType.Rows.Count > 0)
        //    {
        //        ddlLanguage.DataSource = editonType;
        //        ddlLanguage.DataBind();
        //    }

        //    ddlLanguage.Items.Insert(0, new ListItem("-- Chọn loại tin --", "0"));
        //}


        private void UpdateLuongSuKienTin(string strNewsID, string luongSuKien)
        {
            if (!string.IsNullOrEmpty(luongSuKien))
            {
                DFISYS.CoreBO.Threads.Threaddetails thread = new DFISYS.CoreBO.Threads.Threaddetails();
                thread.AddThreadToNews(strNewsID, luongSuKien);  
            }
         
        }

        private void UpdateNewsFileType(Int64 NewsID) {
            string selectedItems = string.Empty;
            foreach (ListItem item in cblFileType.Items) {
                if (item.Selected) {
                    selectedItems += item.Value + ",";
                }
            }

            //if (!String.IsNullOrEmpty(selectedItems)) {
            NewsEditHelper.UpdateNewsAttachmentFileType(NewsID, selectedItems);
            //}
        }

    }
}