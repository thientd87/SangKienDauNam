using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
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


namespace DFISYS.GUI.EditoralOffice.MainOffce.editnews
{
    public partial class EditPublishedNews : UserControl
    {
        protected string strNewsID = "";
        protected string strTempNewsID = "";
        private string referUrl = string.Empty;

        #region page load
        protected void Page_Load(object source, EventArgs e)
        {
            Page.ClientScript.RegisterHiddenField("hidEmoticon", txtInit.ClientID.ToString());
            string strcpmode = Request.QueryString["cpmode"].ToString().Replace("add,", "").Replace("#", "");
            string strJSNumberWordSapo = CategoryXMLHepler.GennerateScriptToCheckSapo();
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "sapo", strJSNumberWordSapo);
            referUrl = Request.QueryString["source"];
            if (!IsPostBack)
            {
                //if (Request.QueryString["cpmode"] == "add")
                //    CreateTempNews();
                //else if (Request.QueryString["cpmode"].IndexOf("templist") != -1)
                //    Page.RegisterClientScriptBlock("autosave", "<script>$('.form').autosave({ 'interval': 60000 });</script>");

                if (Session["newsid"] != null) Session.Remove("newsid");

                CategoryHelper.BindListCatDropDown(lstCat);
                CategoryHelper.BindCheckBoxListCat(lstOtherCat);
                DataTable dtTags = ThreadManagement.BO.ThreadHelper.GetThreadlist(" Thread_RT = 'true'", 10, 0);
                if (dtTags != null && dtTags.Rows.Count > 0)
                {
                    cblTags.DataSource = dtTags;
                    cblTags.DataTextField = "Title";
                    cblTags.DataValueField = "Thread_ID";
                    cblTags.DataBind();
                }

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

                LoadProvinces();
                GetAllTacGia();
                //GetNewsLetterCategories();

                if (!string.IsNullOrEmpty(Request.QueryString["NewsRef"]))
                {
                    ltrEdit.Text = "CHỈNH SỬA NỘI DUNG BÀI ĐÃ XUẤT BẢN";
                    strNewsID = Request.QueryString["NewsRef"];
                    Session["newsid"] = strNewsID;
                    BindNewsEdit(Convert.ToInt64(strNewsID));
                    hidNewsID.Value = strNewsID;

                    //CheckNewsInNewsLetter();
                }
               

                //cboMedia.Attributes.Add("onclick", "Preview(this)");

                //kiem tra quyens 
                

               
            }
        }

        private void LoadProvinces()
        {
            DataTable provinces = NewsEditHelper.GetAllProvinces();
            if (provinces != null && provinces.Rows.Count > 0)
            {
                ddlProvinces.DataSource = provinces;
                ddlProvinces.DataBind();
            }
            ddlProvinces.Items.Insert(0, new ListItem("-- Chọn tỉnh thành --", "0"));
        }

        private void Page_Init(object sender, EventArgs e)
        {
            // Check security
            //if (!IsPostBack && !NewslistHelper.isHasPermission(HttpContext.Current))
            //    throw new SecurityException("Bạn không có quyền truy cập vào trang này");
        }

        private void BindToDropdown(ListBox cbo, string text)
        {
            string[] str = HttpUtility.HtmlEncode(text).Replace("#;#", ">").Replace(";#", "<").Split('>');
            string[] lItem;
            foreach (string s in str)
            {
                if (s == "") continue;
                lItem = s.Split('<');
                lItem[1] = HttpUtility.HtmlDecode(lItem[1]);
                //  if (lItem[1].Replace("\\", "/").IndexOf("/") != -1) lItem[1] = lItem[1].Substring(lItem[1].LastIndexOf("/") + 1, lItem[1].Length - lItem[1].LastIndexOf("/") - 1);
                cbo.Items.Add(new ListItem(lItem[1], lItem[0]));
            }
        }
        #endregion

        

        #region luu lai du lieu vao list tam
       

        protected void objsoure_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            string strcpmode = Request.QueryString["cpmode"];
            if ((bool)e.ReturnValue)
            {

                SaveWapContent(Request.QueryString["NewsRef"], Convert.ToInt32(objsoure.UpdateParameters["_news_status"].DefaultValue));

                //if (lstCat.SelectedValue != "")
                //SaveNewsLetter(Convert.ToInt64(Request.QueryString["NewsRef"]), Convert.ToInt32(lstCat.SelectedValue));

                UpdateNewsFileType(Convert.ToInt64(Request.QueryString["NewsRef"]));

                //if (objsoure.UpdateParameters["_news_status"].DefaultValue == "3")
                //    Response.Redirect("/office/publishedlist.aspx");
                //else if (strcpmode.IndexOf("dellist") >= 0)
                //    Response.Redirect("/office/templist.aspx");
                //else
                //    Response.Redirect("/office/" + strcpmode.Substring(strcpmode.IndexOf(",") + 1) + ".aspx");
            }
            //else
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "UpdatedFailed", "<script>alert('Đã có lỗi xảy ra, thao tác thực hiện không thành công');</script>");
        }

        protected void objsoure_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (Session["newsid"] != null) Session.Remove("newsid");
            string strcpmode = Request.QueryString["cpmode"];

            if ((bool)e.ReturnValue)
            {

                SaveWapContent(objsoure.InsertParameters[0].DefaultValue, Convert.ToInt32(objsoure.InsertParameters["_news_status"].DefaultValue));

                //if (lstCat.SelectedValue != "")
                //SaveNewsLetter(Convert.ToInt64(objsoure.InsertParameters[0].DefaultValue), Convert.ToInt32(lstCat.SelectedValue));

                UpdateNewsFileType(Convert.ToInt64(objsoure.InsertParameters[0].DefaultValue));

                
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
        private void SaveWapContent(string NewsID, int NewsStatus)
        {
            if (chkCopyToWap.Checked)
            {
                NewsEditHelper.SaveWapContent(Convert.ToInt64(NewsID), NewsContent.Text, NewsStatus);
            }
        }

        #endregion WAP

        private void SetValueForCombo(DropDownList ddl, string i)
        {
            ddl.SelectedValue = i.ToString();
        }

        private void BindNewsEdit(long _news_id)
        {
            NewsRow objNewsRow = NewsEditHelper.GetNewsInfo_NewsExtension(_news_id, false);
            MainSecurity objSercu = new MainSecurity();
            Permission objPer = objSercu.GetPermission(Page.User.Identity.Name);
            if (objPer.isXuat_Ban_Bai)
            {
                //Kiểm tra nếu có quyền xuất bản thì sẽ hiện dầy đủ thông tin
                btnPublish.Visible = true;
               // pnControl.Visible = true;
            }
            else
            {
                btnPublish.Visible = false;
            }
            if (objNewsRow != null)
            {

                if (!objPer.isXuat_Ban_Bai && Page.User.Identity.Name.Trim().ToLower() != objNewsRow.News_Author.Trim().ToLower())
                {
                    throw new SecurityException("Bạn không có quyền truy cập vào trang này");
                    return;
                }

               

                 

                ltrXuatBan.Text = "Bài viết do \""+objNewsRow.News_Approver.ToUpper()+"\" xuất bản lúc "+ objNewsRow.News_PublishDate.ToString("dd/MM/yyyy hh:mm");
                lstCat.SelectedValue = objNewsRow.Cat_ID.ToString();
                txtTitle.Text = objNewsRow.News_Title != null ? objNewsRow.News_Title : "";
                txtSubTitle.Text = objNewsRow.News_Subtitle != null ? objNewsRow.News_Subtitle : "";
                txtSource.Text = objNewsRow.News_Source != null ? objNewsRow.News_Source : "";
                txtInit.Text = objNewsRow.News_InitialContent != null ? NewsEditHelper.ReplaceImageSrcToEmoticon(objNewsRow.News_InitialContent) : "";
                txtInit.Text = txtInit.Text.Replace("<br/>", System.Environment.NewLine);
                if (Request.QueryString["redirect"] == null)
                {
                    NewsContent.Text = objNewsRow.News_Content != null ? objNewsRow.News_Content : "";
                    Session["NewsContent"] = NewsContent.Text;
                }
                else
                {
                    NewsContent.Text = Session["NewsContent"].ToString();
                }
                chkIsFocus.Checked = objNewsRow.IsNews_isFocusNull != true ? objNewsRow.News_isFocus : false;
                cboIsHot.SelectedValue = objNewsRow.IsNews_ModeNull != true ? objNewsRow.News_Mode.ToString() : "0";

                hdRelatNews.Value = objNewsRow.News_Relation != null ? objNewsRow.News_Relation : "";
                chkShowComment.Checked = objNewsRow.IsisCommentNull != true ? objNewsRow.isComment : false; //Cho phép hiện ảnh hay ko?
                chkShowRate.Checked = objNewsRow.IsisUserRateNull != true ? objNewsRow.isUserRate : false;
                txtSelectedFile.Text = objNewsRow.News_Image != null ? objNewsRow.News_Image : "";
                txtImageTitle.Text = objNewsRow.News_ImageNote != null ? objNewsRow.News_ImageNote : "";
                txtIcon.Text = objNewsRow.Icon != null ? objNewsRow.Icon : "";
                txtMaCP.Text = objNewsRow.Extension1 != null ? objNewsRow.Extension1 : "";
                txtExtension2.Text = objNewsRow.Extension2 != null ? objNewsRow.Extension2 : "";
                txtSourceLink.Text = objNewsRow.Extension3 != null ? objNewsRow.Extension3 : "";

            
                ddlAuthor.SelectedValue = objNewsRow.IsExtension4Null != true ? objNewsRow.Extension4.ToString() : "0";

                if (objNewsRow.Template != 0)
                    ddlProvinces.SelectedValue = objNewsRow.Template.ToString();

                if (objNewsRow.News_OtherCat != null)
                {
                    string[] strOthers = objNewsRow.News_OtherCat.Split(",".ToCharArray());
                    for (int i = 0; i < lstOtherCat.Items.Count; i++)
                    {
                        foreach (string strItem in strOthers)
                            if (strItem == lstOtherCat.Items[i].Value)
                            {
                                lstOtherCat.Items[i].Selected = true;
                                break;
                            }
                    }
                }

                if (!objNewsRow.IsNews_PublishDateNull)
                {
                    SetValueForCombo(cboMonth, objNewsRow.News_PublishDate.Month.ToString());
                    SetValueForCombo(cboDay, objNewsRow.News_PublishDate.Day.ToString());
                    SetValueForCombo(cboYear, objNewsRow.News_PublishDate.Year.ToString());
                    SetValueForCombo(cboSercond, objNewsRow.News_PublishDate.Second.ToString());
                    SetValueForCombo(cboMinute, objNewsRow.News_PublishDate.Minute.ToString());
                    SetValueForCombo(cboHour, objNewsRow.News_PublishDate.Hour.ToString());
                }
                else
                {
                    SetValueForCombo(cboMonth, "0");
                    SetValueForCombo(cboDay, "0");
                    SetValueForCombo(cboYear, "2000");
                    SetValueForCombo(cboSercond, "-1");
                    SetValueForCombo(cboMinute, "-1");
                    SetValueForCombo(cboHour, "-1");
                }

                if (!IsPostBack)
                {
                    hdMedia.Value = DFISYS.BO.Editoral.NewsMedia.NewsMediaHelper.Get_ObjectId_By_NewsId(_news_id);
                    DataTable dtThread = ThreadHelper.SelectThreadByNewsID(_news_id);
                    if (dtThread != null && dtThread.Rows.Count > 0)
                        hidLuongSuKien.Value = dtThread.Rows[0]["Thread_ID"].ToString();
                }


                //Load data to Combobox Tin lien quan;
                string str;
                if (hdRelatNews.Value.TrimEnd(',') != "")
                {
                    str = NewsEditHelper.Get_Media_By_ListsId("News_ID", "News_Title", "News", hdRelatNews.Value);
                    BindToDropdown(cboNews, str);
                }


                string strThread;
                if (hidLuongSuKien.Value.TrimEnd(',') != "")
                {
                    strThread = NewsEditHelper.Get_Media_By_ListsId("Thread_ID", "Title", "NewsThread", hidLuongSuKien.Value);
                    BindToDropdown(lstThread, strThread);

                    string[] arrThread = hidLuongSuKien.Value.Split(',');
                    if (arrThread != null && arrThread.Length > 0)
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
                if (hdMedia.Value.TrimEnd(',').Length > 0)
                {
                    str = NewsEditHelper.Get_Media_By_ListsId("Object_ID", "Object_Url", "MediaObject", hdMedia.Value);
                    //BindToDropdown(cboMedia, str);
                }

                LoadAttachmentsType(_news_id);
            }
        }

        private void LoadAttachmentsType(long _news_id)
        {
            DataTable types = NewsEditHelper.GetAttachmentsType(_news_id);

            if (types != null && types.Rows.Count > 0)
            {
                DataRow row = null;
                foreach (ListItem item in cblFileType.Items)
                {
                    for (int i = 0; i < types.Rows.Count; i++)
                    {
                        row = types.Rows[i];
                        if (item.Value == row["Type"].ToString())
                            item.Selected = true;
                    }
                }
            }
        }

        #endregion

        #region Cập nhật tin đã xuất bản
        /// <summary>
        /// Insert sang bảng News_Extension
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdated_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                // Get CatID cac chuyen muc khac
                string strOtherCat = "";
                foreach (ListItem item in lstOtherCat.Items)
                {
                    if (item.Selected)
                        strOtherCat += item.Value + ",";
                }
                //string Tags = "";

                foreach (ListItem item in cblTags.Items)
                {
                    if (item.Selected)
                        hidLuongSuKien.Value += item.Value + ",";
                }
                hidLuongSuKien.Value = hidLuongSuKien.Value.TrimEnd(',');
                if (strOtherCat != "")
                    strOtherCat = strOtherCat.Substring(0, strOtherCat.Length - 1);
                objsoure.InsertParameters[10].DefaultValue = "3";
                objsoure.InsertParameters["_str_Extension3"].DefaultValue = txtSourceLink.Text.Trim();
                objsoure.InsertParameters["_thread_id"].DefaultValue = hidLuongSuKien.Value.Trim();
                objsoure.InsertParameters[14].DefaultValue = strOtherCat;

                //if (getPublishTime().Year == 2000)
                //objsoure.InsertParameters[15].DefaultValue = DateTime.Now.ToString();
                //else
                objsoure.InsertParameters[15].DefaultValue = getPublishTime().ToString();

                // Get ViewNum cua Category
                int template = CategoryHelper.getCatInfoAsCategoryRow(Convert.ToInt32(lstCat.SelectedValue)).Cat_ViewNum;
                objsoure.InsertParameters[18].DefaultValue = template.ToString();
                objsoure.InsertParameters[4].DefaultValue = txtSelectedFile.Text.Trim();

                objsoure.Insert();
                if (!String.IsNullOrEmpty(referUrl))
                    Response.Redirect(referUrl);
            }
           
        }
        #endregion

        #region Xóa dữ liệu bảng news_extension
        //ThienTD EDIT: xóa dữ liệu trong bảng News_Extesion
        protected void btnSend_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(Request.QueryString["NewsRef"]))
            {
                NewsEditHelper.DeleteNews_Extension(Request.QueryString["NewsRef"]);

            }
            if (!String.IsNullOrEmpty(referUrl))
                Response.Redirect(referUrl);
        }
        #endregion

      

        #region xuat ban trong 2 t.h: them moi va edit
        protected void btnPublish_Click(object sender, EventArgs e)
        {
            if (Validate())
            {
                // Get CatID cac chuyen muc khac
                string strOtherCat = "";
                foreach (ListItem item in lstOtherCat.Items)
                {
                    if (item.Selected)
                        strOtherCat += item.Value + ",";
                }
                //string Tags = "";

                foreach (ListItem item in cblTags.Items)
                {
                    if (item.Selected)
                        hidLuongSuKien.Value += item.Value + ",";
                }
                hidLuongSuKien.Value = hidLuongSuKien.Value.TrimEnd(',');
                if (strOtherCat != "")
                    strOtherCat = strOtherCat.Substring(0, strOtherCat.Length - 1);

                string strNewsID = "";
                string strcpmode = Request.QueryString["cpmode"].ToString().Replace("add,", "").Replace("#", "");
                //xet 2 truong hop: 1 la tao va xuat ban, 2 sua va xuất bản
                if (!string.IsNullOrEmpty(Request.QueryString["NewsRef"]))
                {
                    strNewsID = Request.QueryString["NewsRef"].ToString();

                    objsoure.UpdateParameters[9].DefaultValue = "3";

                    // Neu chon ngay xuat ban voi truong hop chua Published 
                    // thi check xem da chon PublishedDate chua
                    //if (getPublishTime().Year == 2000)
                    //    objsoure.UpdateParameters[13].DefaultValue = DateTime.Now.ToString();
                    //else
                    objsoure.UpdateParameters[13].DefaultValue = getPublishTime().ToString();

                    objsoure.UpdateParameters[12].DefaultValue = strOtherCat;
                    objsoure.UpdateParameters["_str_Extension3"].DefaultValue = txtSourceLink.Text.Trim();
                    objsoure.UpdateParameters["_thread_id"].DefaultValue = hidLuongSuKien.Value.Trim();
                    // Get ViewNum cua Category
                    int template = CategoryHelper.getCatInfoAsCategoryRow(Convert.ToInt32(lstCat.SelectedValue)).Cat_ViewNum;
                    objsoure.UpdateParameters[17].DefaultValue = template.ToString();
                     objsoure.UpdateParameters[4].DefaultValue = txtSelectedFile.Text.Trim();
                    objsoure.UpdateParameters["_isSend"].DefaultValue = "true";
                    objsoure.Update();
                   
                    NewsEditHelper.DeleteNews_Extension(Request.QueryString["NewsRef"]);

                    
                    if (!String.IsNullOrEmpty(referUrl))
                        Response.Redirect(referUrl);
                }
            }
            
        }
        #endregion

        #region Thoi gian
        private DateTime getPublishTime()
        {
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
        private void BindYear()
        {
            DataTable objYearTbl = createData("YAER");
            cboYear.DataSource = objYearTbl;
            cboYear.DataMember = objYearTbl.TableName;
            cboYear.DataValueField = objYearTbl.Columns[0].ColumnName;
            cboYear.DataTextField = objYearTbl.Columns[1].ColumnName;
            cboYear.DataBind();
        }
        private void BindMonth()
        {
            DataTable objMontTbl = createData("MONTH");
            cboMonth.DataSource = objMontTbl;
            cboMonth.DataMember = objMontTbl.TableName;
            cboMonth.DataValueField = objMontTbl.Columns[0].ColumnName;
            cboMonth.DataTextField = objMontTbl.Columns[1].ColumnName;
            cboMonth.DataBind();
        }
        private void BindDay()
        {
            DataTable objDayTbl = createData("DAY");
            cboDay.DataSource = objDayTbl;
            cboDay.DataMember = objDayTbl.TableName;
            cboDay.DataValueField = objDayTbl.Columns[0].ColumnName;
            cboDay.DataTextField = objDayTbl.Columns[1].ColumnName;
            cboDay.DataBind();
        }
        private void BindHour()
        {
            DataTable objHourTbl = createData("HOUR");
            cboHour.DataSource = objHourTbl;
            cboHour.DataMember = objHourTbl.TableName;
            cboHour.DataValueField = objHourTbl.Columns[0].ColumnName;
            cboHour.DataTextField = objHourTbl.Columns[1].ColumnName;
            cboHour.DataBind();
        }
        private void BindMinute()
        {
            DataTable objMinuteTbl = createData("MINUTE");
            cboMinute.DataSource = objMinuteTbl;
            cboMinute.DataMember = objMinuteTbl.TableName;
            cboMinute.DataValueField = objMinuteTbl.Columns[0].ColumnName;
            cboMinute.DataTextField = objMinuteTbl.Columns[1].ColumnName;
            cboMinute.DataBind();
        }
        private void BindSercond()
        {
            DataTable objSercondTbl = createData("SERCOND");
            cboSercond.DataSource = objSercondTbl;
            cboSercond.DataMember = objSercondTbl.TableName;
            cboSercond.DataValueField = objSercondTbl.Columns[0].ColumnName;
            cboSercond.DataTextField = objSercondTbl.Columns[1].ColumnName;
            cboSercond.DataBind();
        }
        private DataTable createData(string mode)
        {
            DataTable objTable = new DataTable(mode);
            objTable.Columns.Add("Key");
            objTable.Columns.Add("Value");
            switch (mode)
            {
                case "YAER":
                    object[] objArr = new object[2];
                    int intCurYear = DateTime.Now.Year - 1;
                    objArr[0] = 2000;
                    objArr[1] = "Chọn năm";
                    objTable.Rows.Add(objArr);
                    int intMaxYear = intCurYear + 10;
                    for (int i = intCurYear; i <= intMaxYear; i++)
                    {
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
                    for (int i = 1; i <= intMaxMonth; i++)
                    {
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
                    for (int i = 1; i <= intMaxDay; i++)
                    {
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
                    for (int i = 0; i < intMaxHOUR; i++)
                    {
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
                    for (int i = 0; i <= intMaxobjMINUTE; i++)
                    {
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
                    for (int i = 0; i <= intMaxSERCOND; i++)
                    {
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
        private bool Validate()
        {
            string message = string.Empty;

            // ok khi không đụng độ và Cat khác rỗng
            bool isOK = !isConcurrency(ref message) && !string.IsNullOrEmpty(lstCat.SelectedValue.Trim());

            if (!isOK && !string.IsNullOrEmpty(message))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "alert", "<script>alert('" + message + "')</script>");
            }

            return isOK;
        }
        #endregion

        

        /// <summary>
        /// Kiểm tra xem bài viết này có đồng thời 2 người can thiệp không [bacth, 12:29 PM 5/31/2008]
        /// </summary>
        /// <param name="message">Thông báo trong trường hợp có sự động độ</param>
        /// <returns></returns>
        private bool isConcurrency(ref string message)
        {
            if (string.IsNullOrEmpty(Request.QueryString["NewsRef"])) return false; // bài viết mới - không đụng độ
            string cpMode = Request.QueryString["cpmode"].Replace("add,", string.Empty);

            long newsId = 0;
            long.TryParse(Request.QueryString["NewsRef"], out newsId);
            NewsRow news = NewsEditHelper.GetNewsInfo(newsId);
            if (news == null)
            {
                message = "Bài viết không tồn tại hoặc đã bị xóa";
                return true;
            }

            if (cpMode.Equals("editwaitlist"))
            {
                if (news.News_Status != 1)
                {
                    message = "Bài viết đã có người khác cập nhật";
                    return true;
                }
                else
                {
                    // kiểm tra xem có ai nhận biên tập không?
                    string otherUser = NewsEditHelper.getReceiver(newsId);
                    if (!string.IsNullOrEmpty(otherUser) && otherUser != Page.User.Identity.Name)
                    {
                        message = otherUser + " đã nhận biên tập bài này trong khi bạn đang xem bài viết";
                        return true;
                    }
                }
            }
            else if (cpMode.Equals("approvalwaitlist"))
            {
                if (news.News_Status != 2)
                {
                    message = "Bài viết đã có người khác cập nhật";
                    return true;
                }
                else
                {
                    // kiểm tra xem có ai nhận duyệt không?
                    string otherUser = NewsEditHelper.getReceiver(newsId);
                    if (!string.IsNullOrEmpty(otherUser) && otherUser != Page.User.Identity.Name)
                    {
                        message = otherUser + " đã nhận duyệt bài này trong khi bạn đang xem bài viết";
                        return true;
                    }
                }
            }
            return false;
        }

        private void GetAllTacGia()
        {
            DataTable authors = NewsEditHelper.GetAllTacGia();
            if (authors != null && authors.Rows.Count > 0)
            {
                ddlAuthor.DataSource = authors;
                ddlAuthor.DataBind();
            }

            ddlAuthor.Items.Insert(0, new ListItem("-- Chọn tác giả --", "0"));
        }

        private void UpdateNewsFileType(Int64 NewsID)
        {
            string selectedItems = string.Empty;
            foreach (ListItem item in cblFileType.Items)
            {
                if (item.Selected)
                {
                    selectedItems += item.Value + ",";
                }
            }

            //if (!String.IsNullOrEmpty(selectedItems)) {
            NewsEditHelper.UpdateNewsAttachmentFileType(NewsID, selectedItems);
            //}
        }
    }
}