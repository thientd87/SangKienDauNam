using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DFISYS.BO.Editoral.Category;
using DFISYS.BO.Editoral.Newsedit;
using DFISYS.User.Security;
using DFISYS.BO.Editoral.Newslist;
using DFISYS.Core.DAL;
using System.IO;
using System.Threading;


namespace DFISYS.GUI.EditoralOffice.MainOffce.Newslist
{
    public partial class Newslist2 : System.Web.UI.UserControl
    {
        #region page load  thuc hien phan list cho tung doi tuong

        protected string isXuatBan = "true";

        private bool isAllowChamNhuanBut = false;

        protected string CpMode = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string strcpmode = Request.QueryString["cpmode"].ToString();
            CpMode = strcpmode;
            MainSecurity objSercu = new MainSecurity();
            Permission objPer = null;
            Role objrole = null;
            //DataTable tblPermissions = objSercu.GetPermissionAsTable(HttpContext.Current.User.Identity.Name);
            //isAllowChamNhuanBut = tblPermissions.Select("Permission_ID=" + PermissionConst.ChamNhuanBut).Length == 1;
            objPer = objSercu.GetPermission(Page.User.Identity.Name);
            isAllowChamNhuanBut = objPer.isChamNhuanBut;

            if (!Page.IsPostBack)
            {

                string strUrlRefer = Request.UrlReferrer != null ? Request.UrlReferrer.ToString().ToLower() : "";
                if (strUrlRefer == "" || strUrlRefer.IndexOf("add," + strcpmode) == -1)
                {
                    Session.Remove("ddlChuyenmuc");
                    Session.Remove("txtFromDate");
                    Session.Remove("txtToDate");
                    Session.Remove("txtKeyword");
                    Session.Remove("cboCategory");
                    Session.Remove("cboTieuDiem");
                    Session.Remove("cboIsHot");
                }

                txtFromDate.Attributes.Add("onkeypress", "SetReadOnly()");
                txtToDate.Attributes.Add("onkeypress", "SetReadOnly()");
                txtFromDate.Attributes.Add("onpaste", "return false");
                txtToDate.Attributes.Add("onpaste", "return false");
                txtFromDate.Attributes.Add("oncontextmenu", "return false");
                txtToDate.Attributes.Add("oncontextmenu", "return false");
                txtKeyword.Attributes.Add("onkeypress", "return trapEnterKey(event, '" + btnSearch.ClientID + "')");

                Session["cpmode"] = Request.QueryString["cpmode"];
                string strCats = CategoryHelper.Treebuild(ddlChuyenmuc);
                CategoryHelper.Treebuild(ddlChuyenmuc);

                // Init value
                ddlChuyenmuc.SelectedValue = Session["ddlChuyenmuc"] != null ? Session["ddlChuyenmuc"].ToString() : "0";
                txtToDate.Text = Session["txtToDate"] != null ? Session["txtToDate"].ToString() : "";
                txtFromDate.Text = Session["txtFromDate"] != null ? Session["txtFromDate"].ToString() : "";
                //cboCategory.SelectedValue = Session["cboCategory"] != null ? Session["cboCategory"].ToString() : "0";
                txtKeyword.Text = Session["txtKeyword"] != null ? Session["txtKeyword"].ToString() : "";

                //Helper.BindUser(cboApproverBy);
                //Helper.BindUser(cboCreatedBy);

                // End Init Value

                if (string.IsNullOrEmpty(strCats))
                {
                    objListNewsSource.SelectParameters[0].DefaultValue = "News_Status = -2 AND Cat_ID = -2";
                    return;
                }

                // Tu dong Search
                if (Session["ddlChuyenmuc"] != null || (Session["txtToDate"] != null && Session["txtFromDate"] != null))
                {
                    Filter();
                    //return;
                }
                else if (Session["cboCategory"] != null || Session["txtKeyword"] != null)
                {
                    btnSearch_Click(null, null);
                    //return;
                }
                else
                    excutive(strcpmode, strCats, "");



                // show command button, page title depending on cp mode
                switch (strcpmode)
                {
                    case "templist":
                        ltrLabel.Text = "Danh sách bài viết lưu tạm";
                        break;
                    case "sendlist":
                        ltrLabel.Text = "Danh sách bài viết đã gửi chờ biên tập";
                        LinkDelete.Visible = false;
                        break;
                    case "sendapprovallist":
                        ltrLabel.Text = "Danh sách bài viết đã gửi chờ duyệt";
                        LinkDelete.Visible = false;

                        break;
                    case "dellist":
                        ltrLabel.Text = "Danh sách bài viết xóa tạm";
                        LinkDelete.Visible = true;

                        break;
                    case "editwaitlist":
                        ltrLabel.Text = "Danh sách bài viết chờ biên tập";

                        break;
                    case "editinglist":
                        ltrLabel.Text = "Danh sách bài viết nhận biên tập";

                        break;
                    case "approvinglist":
                        ltrLabel.Text = "Danh sách bài viết nhận duyệt";


                        break;
                    case "approvalwaitlist":
                        ltrLabel.Text = "Danh sách bài viết chờ duyệt";



                        break;
                    case "publishedlist":
                        ltrLabel.Text = "Danh sách bài viết đã xuất bản";
                        grdListNews.Columns[2].Visible = true;

                       // LinkDelete.Visible = false;

                        break;
                    //voi danh sach bai tra lai.
                    case "backlist":
                        ltrLabel.Text = "Danh sách bài viết trả lại";
                        break;
                    //voi danh sach bai tra lai.
                    case "removedlist":
                        ltrLabel.Text = "Danh sách bài viết đã gỡ bỏ";
                        break;

                    case "mypublished":
                        LinkDelete.Visible = false;
                        break;
                    default:
                        //xem quyen cua thang dang set

                        break;
                }
            }
        }

        #endregion

        private void excutive(string strcpmode, string strCats, string strAndCat)
        {
            if (!string.IsNullOrEmpty(strCats))
            {
                strCats = " AND (Category.Cat_ID in (" + strCats + ") OR Category.Cat_ParentID IN (" + strCats + ") )";
                strAndCat = strCats + strAndCat;
            }
            switch (strcpmode)
            {
                case "templist":
                    objListNewsSource.SelectParameters[0].DefaultValue = "News_Status=0 " + strAndCat;
                    break;
                case "sendlist":
                    objListNewsSource.SelectParameters[0].DefaultValue = "News_Status=1 " + strAndCat;
                    break;
                case "sendapprovallist":
                    objListNewsSource.SelectParameters[0].DefaultValue = "News_Status=2 " + strAndCat;
                    break;
                case "dellist":
                    objListNewsSource.SelectParameters[0].DefaultValue = "News_Status=6 " + strAndCat;
                    break;
                //Cung la waitlist nhung voi quyen khac nhau thi status nhan dc la khac nhau:
                //Neu la BTV thi trang thai la 1 va mode la editwaitlist.
                case "editwaitlist":
                    objListNewsSource.SelectParameters[0].DefaultValue = "News_Status=1 " + strAndCat + " ";
                    break;
                case "editinglist":
                    objListNewsSource.SelectParameters[0].DefaultValue = "News_Status=1 " + strAndCat;
                    break;
                case "approvinglist":
                    objListNewsSource.SelectParameters[0].DefaultValue = "News_Status=2 " + strAndCat;
                    break;
                case "approvalwaitlist":
                    objListNewsSource.SelectParameters[0].DefaultValue = "News_Status=2 " + strAndCat;
                    break;
                case "publishedlist":
                    objListNewsSource.SelectParameters[0].DefaultValue = "News_Status=3 " + strAndCat;
                    break;
                //voi danh sach bai tra lai.
                case "backlist":
                    objListNewsSource.SelectParameters[0].DefaultValue = "News_Status=5 " + strAndCat;
                    break;
                //voi danh sach bai tra lai.
                case "removedlist":
                    objListNewsSource.SelectParameters[0].DefaultValue = "News_Status=7 " + strAndCat;
                    break;
                default:
                    //xem quyen cua thang dang set
                    MainSecurity objSecu = new MainSecurity();
                    Role objrole = objSecu.GetRole(Page.User.Identity.Name);
                    if (objrole.isBienTapVien)
                    {
                        objListNewsSource.SelectParameters[0].DefaultValue = "News_Status=1 " + strAndCat;
                    }
                    if (objrole.isPhongVien)
                    {
                        objListNewsSource.SelectParameters[0].DefaultValue = "News_Status=0 " + strAndCat;
                    }
                    if (objrole.isThuKyChuyenMuc || objrole.isThuKyToaSoan || objrole.isPhuTrachKenh || objrole.isTongBienTap)
                    {
                        objListNewsSource.SelectParameters[0].DefaultValue = "News_Status=2 " + strAndCat;
                    }
                    break;
                case "mypublished":
                    objListNewsSource.SelectParameters[0].DefaultValue = " News_Status=3 " + strAndCat;// +" order by news_createDate";
                    objListNewsSource.SelectParameters[1].DefaultValue = HttpContext.Current.User.Identity.Name;
                    break;
            }
            
            objListNewsSource.Select();
            grdListNews.DataBind();
        }

        protected void grdListNews_Sorted(object sender, EventArgs e)
        {
            string sortExpression = grdListNews.SortExpression.ToLower();

            switch (sortExpression)
            {
                case "news_title":
                    SortColumn(1);
                    break;
                case "news_isfocus":
                    SortColumn(grdListNews.Columns.Count - 2);
                    break;
                case "news_mode":
                    SortColumn(grdListNews.Columns.Count - 1);
                    break;
                case "viewcount":
                    SortColumn(grdListNews.Columns.Count - 3);
                    break;
            }
        }

        void SortColumn(int columnIndex)
        {

            string className = grdListNews.SortDirection == SortDirection.Ascending ? "asc" : "desc";
            string tooltip = grdListNews.SortDirection == SortDirection.Ascending ? "Xắp xếp theo thứ tự tăng dần" : "Xắp sếp theo thứ tự giảm dần";

            for (int i = 0; i < grdListNews.Columns.Count; i++)
            {
                grdListNews.Columns[i].HeaderStyle.CssClass = "";
                grdListNews.HeaderRow.Cells[i].ToolTip = "";
            }
            grdListNews.Columns[columnIndex].HeaderStyle.CssClass = className;
            grdListNews.HeaderRow.Cells[columnIndex].ToolTip = tooltip;
        }



        protected void grdListNews_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView data = e.Row.DataItem as DataRowView;

                Literal ltrInfo = e.Row.FindControl("ltrInfo") as Literal;
                CheckBox chkIsFocus = e.Row.FindControl("chkIsFocus") as CheckBox;
                DropDownList cboIsHot = e.Row.FindControl("cboIsHot") as DropDownList;
                HtmlAnchor aIconUpdate = (HtmlAnchor)e.Row.FindControl("aIconUpdate");

                if (aIconUpdate != null)
                {
                    aIconUpdate.HRef = "/ajax/DifferentOfContent.aspx?nid=" + DataBinder.Eval(e.Row.DataItem, "News_ID");
                    aIconUpdate.Visible = NewslistHelper.IsHaveNewUpdateNewsPublished(DataBinder.Eval(e.Row.DataItem, "News_ID").ToString());
                }


                string datetime = data["ModifiedDate"] == DBNull.Value ? string.Empty : ((DateTime)data["ModifiedDate"]).ToString("dd/MM/yyyy HH:mm");
                string approver = data["News_Approver"] == DBNull.Value ? string.Empty : (string)data["News_Approver"];
                string senderId = data["Sender_ID"] == DBNull.Value ? string.Empty : (string)data["Sender_ID"];

                switch (CpMode)
                {
                    case "templist":
                        ltrInfo.Text = "Lần sửa cuối: <b>{0}</b>";
                        ltrInfo.Text = string.Format(ltrInfo.Text, datetime);
                        break;
                    case "sendlist":
                        ltrInfo.Text = "Ngày gửi: <b>{0}</b>";
                        ltrInfo.Text = string.Format(ltrInfo.Text, datetime);
                        chkIsFocus.Enabled = false;
                        cboIsHot.Enabled = false;
                        break;
                    case "sendapprovallist":
                        ltrInfo.Text = "Ngày gửi: <b>{0}</b>";
                        ltrInfo.Text = string.Format(ltrInfo.Text, datetime);
                        chkIsFocus.Enabled = false;
                        cboIsHot.Enabled = false;
                        break;
                    case "dellist":
                        ltrInfo.Text = "Lần sửa cuối: <b>{0}</b>";
                        ltrInfo.Text = string.Format(ltrInfo.Text, datetime);
                        break;
                    //Cung la waitlist nhung voi quyen khac nhau thi status nhan dc la khac nhau:
                    //Neu la BTV thi trang thai la 1 va mode la editwaitlist.
                    case "editwaitlist":
                        ltrInfo.Text = "Người đưa: <b>{0}</b>, Lần sửa cuối: <b>{1}</b>";
                        ltrInfo.Text = string.Format(ltrInfo.Text, senderId, datetime);
                        break;
                    case "editinglist":
                        ltrInfo.Text = "Người đưa: <b>{0}</b>, Lần sửa cuối: <b>{1}</b>";
                        ltrInfo.Text = string.Format(ltrInfo.Text, senderId, datetime);
                        break;
                    case "approvinglist":
                        ltrInfo.Text = "Người đưa: <b>{0}</b>, Lần sửa cuối: <b>{1}</b>";
                        ltrInfo.Text = string.Format(ltrInfo.Text, senderId, datetime);
                        break;
                    case "approvalwaitlist":
                        ltrInfo.Text = "Người đưa: <b>{0}</b>, Lần sửa cuối: <b>{1}</b>";
                        ltrInfo.Text = string.Format(ltrInfo.Text, senderId, datetime);
                        break;
                    case "publishedlist":
                        if ((DateTime)data["ModifiedDate"] > DateTime.Now)
                            ltrInfo.Text = "Người viết: <b>{0}</b>, NXB: <span style=\"color:red; text-decoration:underline;\">{1}</span>, sửa cuối: <b>{2}</b>";
                        else
                            ltrInfo.Text = "Người viết: <b>{0}</b>, NXB: {1}, sửa cuối: <b>{2}</b>";
                        ltrInfo.Text = string.Format(ltrInfo.Text, senderId, datetime, approver);

                        if (isAllowChamNhuanBut)
                            grdListNews.Columns[2].Visible = false;

                        break;
                    case "backlist":
                        ltrInfo.Text = "Người trả: <b>{0}</b>, Lần sửa cuối: <b>{1}</b>";
                        ltrInfo.Text = string.Format(ltrInfo.Text, senderId, datetime);
                        break;
                    case "removedlist":
                        ltrInfo.Text = "Người gỡ: <b>{0}</b>, Lần sửa cuối: <b>{1}</b>";
                        ltrInfo.Text = string.Format(ltrInfo.Text, senderId, datetime);
                        break;
                }
            }
        }


        void grdListNews_Unload(object sender, EventArgs e)
        {
            Session.Remove("ddlChuyenmuc");
            Session.Remove("txtFromDate");
            Session.Remove("txtToDate");
            Session.Remove("txtKeyword");
            Session.Remove("cboCategory");
            Session.Remove("cboTieuDiem");
            Session.Remove("cboIsHot");
        }

        private void DisableAll()
        {

            this.LinkDelete.Visible = false;

            tblSearch.Visible = false;

        }

        protected void grdListNews_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow grdrow = this.grdListNews.Rows[e.RowIndex];
            String action = String.Empty;

            if (grdrow != null)
            {
                ImageButton btnUpdate = (ImageButton)grdrow.FindControl("btnUpdate");
                if (btnUpdate != null)
                    objListNewsSource.UpdateParameters[0].DefaultValue = btnUpdate.CommandArgument.ToString();
                CheckBox cbo = (CheckBox)grdrow.FindControl("chkIsFocus");
                if (cbo != null)
                    objListNewsSource.UpdateParameters[3].DefaultValue = cbo.Checked.ToString(); //is focus
                DropDownList cboIsHot = (DropDownList)grdrow.FindControl("cboIsHot");
                if (cboIsHot != null)
                    objListNewsSource.UpdateParameters[4].DefaultValue = cboIsHot.SelectedValue.ToString(); //new mode

                objListNewsSource.UpdateParameters[5].DefaultValue = "true";
            }
        }


        protected void ddlChuyenmuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter();
            grdListNews.SelectedIndex = -1;
            grdListNews.PageIndex = 0;
        }

        private void Filter()
        {
            // Reset cac gia tri cua hop TimKiem
            txtKeyword.Text = "";
            //cboCategory.SelectedValue = "0";
            Session.Remove("txtKeyword");
            Session.Remove("cboCategory");
            Session.Remove("cboTieuDiem");
            Session.Remove("cboIsHot");
            // End Reset


            //cboPage.SelectedValue = "0";

            string strCat = "";
            string strcpmode = Request.QueryString["cpmode"].ToString();
            string strWhere = "";

            if (ddlChuyenmuc.SelectedValue != "0" && ddlChuyenmuc.SelectedValue != "")
            {
                strCat = ddlChuyenmuc.SelectedValue;
            }
            else
                strCat = CategoryHelper.Treebuild(ddlChuyenmuc);

            //if (strCat == "" || strCat == "0") {
            //    strCat = CategoryHelper.Treebuild(cboCategory);
            //}

            if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "")
            {
                string sFromDate = txtFromDate.Text;
                if (sFromDate.Trim() != "")
                    sFromDate = sFromDate.Substring(3, 2) + "/" + sFromDate.Substring(0, 2) + "/" + sFromDate.Substring(6, 4);

                string sToDate = txtToDate.Text;
                if (sToDate.Trim() != "")
                    sToDate = sToDate.Substring(3, 2) + "/" + sToDate.Substring(0, 2) + "/" + sToDate.Substring(6, 4) + " 23:59.999";

                if (strcpmode.ToLower().IndexOf("publishedlist") >= 0)
                    strWhere += " AND News_PublishDate Between '" + sFromDate + "' AND '" + sToDate + "'";
                else
                    strWhere += " AND News_ModifiedDate Between '" + sFromDate + "' AND '" + sToDate + "'";
            }

            excutive(strcpmode, strCat, strWhere);
            //objdspage.Update();


            // Luu cac gia tri Filter
            Session["ddlChuyenmuc"] = ddlChuyenmuc.SelectedValue;
            Session["txtFromDate"] = txtFromDate.Text.Trim();
            Session["txtToDate"] = txtToDate.Text.Trim();
            // End Luu cac gia tri filter

            grdListNews.SelectedIndex = -1;
            grdListNews.PageIndex = 0;
        }

        #region Button handle

        protected void LinkDelete_Click(object sender, EventArgs e)
        {
            //string strcpmode = Request.QueryString["cpmode"].ToString();
            //if (strcpmode == "dellist")
            //{
            string[] Ids = hdNewsID.Value.Split(',');
                foreach (string id in Ids)
                {
                    if (id.Length > 0)
                        DFISYS.CoreBO.NewsList.Delete(new long[] { long.Parse(id) });
                }

            //}
            //else
            //{
                //objListNewsSource.UpdateParameters[1].DefaultValue = "6";
            //}
            //objListNewsSource.UpdateParameters[2].DefaultValue = hdNewsID.Value;
            //objListNewsSource.Update();

            //NewsEditHelper n = new NewsEditHelper();
           // n.

        }



        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Reset cac gia tri cua  Filter
            txtFromDate.Text = "";
            txtToDate.Text = "";
            // ddlChuyenmuc.SelectedValue = "0";
            // Session.Remove("ddlChuyenmuc");
            Session.Remove("txtFromDate");
            Session.Remove("txtToDate");
            // End Reset
            DFISYS.BO.SearchHelper objhelp = new DFISYS.BO.SearchHelper();
            string strcpmode = Request.QueryString["cpmode"].ToString();
            string strKeyword = txtKeyword.Text.Trim().Replace("'", " ");
            strKeyword = strKeyword.Replace('"', ' ');
            string[] strKeys = strKeyword.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            string strWhere = objhelp.getAndCond("News_Title", strKeys);
            if (strWhere.Trim() != "")
                strWhere = " And " + strWhere;

            string strCat = ddlChuyenmuc.SelectedValue;
            if (strCat == "0")
                strCat = CategoryHelper.Treebuild(ddlChuyenmuc);



            excutive(strcpmode, strCat, strWhere);
            // Luu cac gia tri TimKiem
            Session["txtKeyword"] = txtKeyword.Text.Trim();

            grdListNews.SelectedIndex = -1;
            grdListNews.PageIndex = 0;

        }

        private string RemoveQuote(string input)
        {
            return input.Replace("'", string.Empty);
        }

        protected void lnkRealDel_Click(object sender, EventArgs e)
        {
            string strcpmode = Request.QueryString["cpmode"].ToString();
            String value = objListNewsSource.DeleteParameters[0].DefaultValue = hdNewsID.Value;
            objListNewsSource.Delete();

        }




        protected void btnFilter_Click(object sender, EventArgs e)
        {
            Filter();
        }

        protected void btnSetTieuDiem_Click(object sender, EventArgs e)
        {
            string arg = hdArgs.Value;
            long newsID = long.Parse(arg.Split(",".ToCharArray())[0]);
            bool tieudiem = bool.Parse(arg.Split(",".ToCharArray())[1]);
            DFISYS.CoreBO.NewsEditHelper.SetTieuDiem(newsID, tieudiem);
        }

        protected void btnSetLoaiTin_Click(object sender, EventArgs e)
        {
            string arg = hdArgs.Value;
            long newsID = long.Parse(arg.Split(",".ToCharArray())[0]);
            int loaitin = int.Parse(arg.Split(",".ToCharArray())[1]);
            DFISYS.CoreBO.NewsEditHelper.SetLoaiTin(newsID, loaitin);
        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            string arg = hdArgs.Value;
            long newsID = long.Parse(arg.Split(",".ToCharArray())[0]);
            int status = int.Parse(arg.Split(",".ToCharArray())[1]);
            DFISYS.CoreBO.NewsList.UpdateStatus(new long[] { newsID }, status);
        }

        protected void btnDeletePermanently_Click(object sender, EventArgs e)
        {
            DFISYS.CoreBO.NewsList.Delete(new long[] { long.Parse(hdArgs.Value) });
            Response.Redirect(Request.RawUrl.ToString());
        }
        #endregion
    }
}