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
using DFISYS.GUI.Users.Common;
using DFISYS.User.Db;
using DFISYS.User.Security;

namespace DFISYS.GUI.Users.User {
    public partial class ViewUser : System.Web.UI.UserControl {
        #region Members
        private string _sort = string.Empty;
        private string _sortDirection = string.Empty;
        #endregion

        #region Page Load event handler
        private void Page_Load(object sender, System.EventArgs e) {
            // Allow grid page size to be configured via web.config. 
            try { gridUser.PageSize = 20; }
            catch { }
            Page.RegisterHiddenField("hidIdPrefix", ClientID + "_");
            btnTimKiem.Attributes.Add("onclick", "return Validate();");
            Common.Helpers.TieButton(txtSearch, btnTimKiem);

            if (IsPostBack) {

                if (Session["ddlChannel"] != null)
                    ddlChannel.SelectedValue = Session["ddlChannel"].ToString();

                Session.Remove("ddlChannel_UserEdit");
            }

            if (!IsPostBack) {
                // Khong xoa session voi truong hop: Back lai tu trang Gan Quyen
                if (Request.UrlReferrer != null && Request.UrlReferrer.ToString().IndexOf("/userassign") >= 0) {
                    if (Session["ddlChannel"] != null)
                        ddlChannel.SelectedValue = Session["ddlChannel"].ToString();
                }
                else
                    Session.Remove("ddlChannel");

                ViewState["userid"] = Page.User.Identity.Name;
                CheckPemission(ViewState["userid"].ToString());
                // Try to recall last sort and filter clauses from session
                // If no session support that's OK, this is just icing on the cake
                this.Sort = Session["UserSort"] == null ? string.Empty : Session["UserSort"].ToString();
                this.SortDirection = Session["UserSortDirection"] == null ? string.Empty : Session["UserSortDirection"].ToString();
                LoadChannel(ViewState["userid"].ToString());

                if (Session["ddlChannel_UserEdit"] != null) {
                    ddlChannel.SelectedValue = Session["ddlChannel_UserEdit"].ToString();
                    Session.Remove("ddlChannel_UserEdit");
                }
                LoadRole();
                BindGrid(-1);
                 
            }
            else {
                // Sort and direction are in viewstate
                this.Sort = (string)ViewState["sort"];
                this.SortDirection = (string)ViewState["sortDirection"];
                SetErrorMessage(string.Empty);
            }

            Session.Remove("Edit_UserID");
        }
        #endregion
        #region Properties
        /// <summary>
        /// The sort clause
        /// </summary>
        private string Sort {
            get { return _sort; }
            set { _sort = value; ViewState["sort"] = _sort; Session["UserSort"] = _sort; }
        }

        /// <summary>
        /// The sort direction (asc or desc)
        /// </summary>
        private string SortDirection {
            get { return _sortDirection; }
            set { _sortDirection = value; ViewState["sortDirection"] = _sortDirection; Session["UserSortDirection"] = _sortDirection; }
        }

        /// <summary>
        /// The where clause
        /// </summary>
        #endregion

        #region Paging and binding

        // Invoked when one of the grid page selection elements is clicked.
        private void OnPageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e) {
            gridUser.CurrentPageIndex = e.NewPageIndex;
            BindGrid(-1);
        }

        // Loads data from the database and binds the UI controls.
        private void BindGrid(int editIndex) {
            MainSecurity objSercu = new MainSecurity();
            Role objrole = objSercu.GetRole(Page.User.Identity.Name);

            using (MainDB db = new MainDB()) {
                int totalRows = 0;
                int startIndex = gridUser.CurrentPageIndex * gridUser.PageSize;
                string sort = "";
                if (this.Sort.Length > 0) sort = this.Sort + " " + this.SortDirection;
                string where = "";

                string strSearchName = txtSearch.Text.Trim().ToLower();

                if (ddlChannel.SelectedIndex >= 0) {
                    //lấy danh sách user của 1 channel
                    Channel_UserRow[] row = db.Channel_UserCollection.GetByChannel_ID(Convert.ToInt32(ddlChannel.SelectedValue));
                    string inuser = "";
                    if (row != null && row.Length > 0) {

                        //duyệt qua danh sách user của 1 channel
                        foreach (Channel_UserRow userRow in row) {
                            // Xu ly viec tim kiem theo UserName
                            // -- Neu Username nay trung voi keyword thi moi dc xu ly tiep
                            // -- Con ko co keyword thi hien thi all
                            if (strSearchName != "" && userRow.User_ID.ToLower().IndexOf(strSearchName) == -1) {
                                continue;
                            }


                            // Neu la account Quan Tri kenh thi ko nhin thay account Channelvn va Admin
                            if (objrole.isQuanTriKenh && objrole.isAdministrator == false) {
                                if (userRow.User_ID == "channelvn" || userRow.User_ID == "admin")
                                    continue;
                            }

                            //lọc theo role
                            if (ddlRole.SelectedIndex > 0) {
                                Channel_User_RoleRow[] curr = db.Channel_User_RoleCollection.GetByCU_ID(userRow.CU_ID);
                                if (curr != null && curr.Length > 0) {
                                    //duyệt qua các role 1 user có
                                    foreach (Channel_User_RoleRow roleRow in curr) {
                                        //nếu có role nào trùng với role cần lọc
                                        if (roleRow.Role_ID == Int32.Parse(ddlRole.SelectedValue)) {
                                            //đưa vào danh sách cần select
                                            inuser += "'" + userRow.User_ID + "',";
                                            break;
                                        }
                                    }
                                }
                            }
                            else {
                                //đưa vào danh sách cần select
                                inuser += "'" + userRow.User_ID + "',";
                            }
                        }
                        inuser += "'" + Const.Global_Admin + "','" + Const.TONG_BIEN_TAP + "',";
                        Business.User User = new Business.User();
                        string inWhere = User.CheckDupplicate(inuser);
                        where = "User_ID in (" + inWhere.Remove(inWhere.Length - 1) + ")";
                    }
                    else {
                        where = "User_ID in('" + Const.Global_Admin + "','" + Const.TONG_BIEN_TAP + "')";
                    }
                }

                if (objrole.isQuanTriKenh && objrole.isAdministrator == false)
                    where += " AND User_ID NOT IN ('channelvn','admin') ";

                DataTable table = db.UserCollection.GetAsDataTable(where, sort, startIndex, gridUser.PageSize, ref totalRows);

                int viewStateFirst = gridUser.CurrentPageIndex * gridUser.PageSize;
                int viewStateLast;

                if (gridUser.AllowPaging)
                    viewStateLast = Math.Min(viewStateFirst + gridUser.PageSize, table.Rows.Count);
                else
                    viewStateLast = table.Rows.Count;

                gridUser.VirtualItemCount = totalRows;
                gridUser.DataSource = table;
                ViewState["dtuser"] = table;
                gridUser.EditItemIndex = editIndex;
                gridUser.DataBind();

                ShowHideButton();
            }
        }
        #endregion

        #region Sorting
        // Invoked when a column sort label is clicked.
        private void OnSortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e) {
            string newSortColumn = e.SortExpression;

            if (newSortColumn == this.Sort)
                SwapDirection();
            else
                this.SortDirection = "Asc";

            gridUser.CurrentPageIndex = 0;
            this.Sort = e.SortExpression;
            BindGrid(-1);
        }

        private void SwapDirection() {
            if (this.SortDirection.ToLower().StartsWith("desc"))
                this.SortDirection = "Asc";
            else
                this.SortDirection = "Desc";
        }
        #endregion

        #region Editing, Deleting, Inserting
        // Invoked when the Edit button is clicked.
        private void OnEditCommand(object source, DataGridCommandEventArgs e) {
            string id = (string)gridUser.DataKeys[e.Item.ItemIndex];
            Session["Edit_UserID"] = id;
            Response.Redirect("/users/" + Const.OBJECT_USEREDIT + ".aspx");
        }

        // Invoked when the Delete button is clicked.
        private void OnDeleteCommand(object source, DataGridCommandEventArgs e) {
            try {
                using (MainDB db = new MainDB()) {
                    int i = 0;
                    CheckBox cb;
                    string id;
                    foreach (DataGridItem dgi in gridUser.Items) {
                        cb = (CheckBox)dgi.Cells[0].Controls[1];
                        if (cb.Checked) {
                            // Determine the key of the selected record ... 
                            id = (string)gridUser.DataKeys[i];
                            // Cook up an object and delete it
                            db.UserCollection.DeleteByPrimaryKey(id);
                        }
                        i++;
                    }
                }
                BindGrid(-1);
            }
            catch (Exception ex) {
                SetErrorMessage(ex.Message);
            }
        }

        // Invoked when a command button is clicked.
        private void OnCommand(object source, DataGridCommandEventArgs e) {
            // Is this the Insert button?
            if (e.CommandName == "Insert") {
                Response.Redirect("users/" + Const.OBJECT_USEREDIT + ".aspx");
            }
            if (e.CommandName == "Assign") {
                string id = (string)gridUser.DataKeys[e.Item.ItemIndex];
                Session["id"] = id.Trim();
                Session["CatID"] = ddlChannel.SelectedValue;
                Response.Redirect("users/" + Const.OBJECT_USERASSIGN.ToLower() + ".aspx");
            }
        }
        #endregion

        #region OnItemCreated event handler (makes grid prettier)
        /// <summary>
        /// Event fires before each row in the grid is created.
        /// Allows customization of items.
        /// Used to make header and pagination footer links pretty
        /// and to add a client-side confirmation script to the server-side delete button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e) {
            // Get the newly created item
            ListItemType itemType = e.Item.ItemType;

            switch (itemType) {
                case ListItemType.Pager:
                    // Change footer pagination links to read Page 1 [2] [3] and make links pretty
                    TableCell pager = (TableCell)e.Item.Controls[0];

                    // Enumerates all the items in the pager...
                    for (int i = 0; i < pager.Controls.Count; i += 2) {
                        Object o = pager.Controls[i];
                        if (o is LinkButton) {
                            LinkButton h = (LinkButton)o;
                            h.ToolTip = "Chuyển đến trang " + h.Text;
                            h.Text = "[ " + h.Text + " ]";
                            h.CssClass = "dgPagerLinks";
                        } // is a page link 
                        else {
                            if (o is Label) {
                                Label l = (Label)o;
                                l.Text = "Trang: " + l.Text;
                                l.CssClass = "dgPagerText";
                            }
                        } // is the current page number
                    } // for 
                    break;
                case ListItemType.Header:
                    // Make sortable column headers look right and give them tool tips
                    for (int i = 0; i < gridUser.Columns.Count; i++) {
                        // Adds a tooltip with the sort expression
                        TableCell cell = e.Item.Cells[i];

                        if (gridUser.Columns[i].SortExpression != "") {
                            cell.ToolTip = "Sắp xếp theo: " + gridUser.Columns[i].HeaderText;

                            // Make the link columns (the sortable ones) the same color as the others
                            try {
                                Object o = cell.Controls[0];
                                if (o is LinkButton) { LinkButton h = (LinkButton)o; h.CssClass = "grdLinkHeader"; }
                            }
                            catch { }
                        } // if a sortable column					
                    } // for	
                    break;
                case ListItemType.Footer:
                    // Add confirmation dialog to delete button in footer
                    TableCell FooterCell = e.Item.Cells[0];
                    ImageButton DeleteButton = (ImageButton)FooterCell.Controls[1];
                    DeleteButton.Attributes.Add("onclick",
                        "return confirm('Are you sure you want to delete the selected row(s)?');");
                    break;
            } // switch		
        } // Item_Created
        #endregion

        #region Helper functions
        // Displays an error message
        private void SetErrorMessage(string text) {
            _errorLabel.Text = "ERROR: " + text;
            _errorLabel.Visible = null != text && 0 < text.Length;
        }
        #endregion

        #region Web Form Designer generated code
        protected System.Web.UI.WebControls.Label _errorLabel;
        protected System.Web.UI.WebControls.ImageButton btnShowFilter;
        protected System.Web.UI.WebControls.Label lblFilter;
        protected System.Web.UI.WebControls.Label lblTotalRows;
        protected System.Web.UI.HtmlControls.HtmlGenericControl divFilter;
        protected System.Web.UI.WebControls.Button btnFilter;
        protected System.Web.UI.WebControls.Button btnClear;
        protected System.Web.UI.WebControls.Label lblCritUser_ID;
        protected System.Web.UI.WebControls.TextBox txtCritUser_ID;
        protected System.Web.UI.WebControls.Label lblCritUser_Name;
        protected System.Web.UI.WebControls.TextBox txtCritUser_Name;
        protected System.Web.UI.WebControls.Label lblCritUser_Pwd;
        protected System.Web.UI.WebControls.TextBox txtCritUser_Pwd;
        protected System.Web.UI.WebControls.Label lblCritUser_Email;
        protected System.Web.UI.WebControls.TextBox txtCritUser_Email;
        protected System.Web.UI.WebControls.Label lblCritUser_Address;
        protected System.Web.UI.WebControls.TextBox txtCritUser_Address;
        protected System.Web.UI.WebControls.Label lblCritUser_PhoneNum;
        protected System.Web.UI.WebControls.TextBox txtCritUser_PhoneNum;
        protected System.Web.UI.WebControls.Label lblCritUser_Im;
        protected System.Web.UI.WebControls.TextBox txtCritUser_Im;
        protected System.Web.UI.WebControls.Label lblCritUser_Website;
        protected System.Web.UI.WebControls.TextBox txtCritUser_Website;

        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.gridUser.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.OnItemCreated);
            this.gridUser.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.OnPageIndexChanged);
            this.gridUser.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.OnSortCommand);
            gridUser.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(OnEditCommand);
            gridUser.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(OnDeleteCommand);
            gridUser.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(OnCommand);
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion

        protected void ddlChannel_SelectedIndexChanged(object sender, EventArgs e) {
            Session["ddlChannel"] = ddlChannel.SelectedValue;
            BindGrid(-1);
        }

        protected void btnTimKiem_Click(object sender, EventArgs e) {
            //DataTable dt = (DataTable)ViewState["dtuser"];
            //using (MainDB db = new MainDB())
            //{
            //    gridUser.DataSource = db.UserCollection.Searching("User_ID", "User_Name", txtSearch.Text.Trim(), dt);
            //    gridUser.DataBind();
            //}
            //ShowHideButton();
            BindGrid(-1);
        }
        private void LoadChannel(string userID) {
            ddlChannel.DataTextField = "Channel_Name";
            ddlChannel.DataValueField = "Channel_ID";
            //nếu là admin kênh thì load kênh của admin đó có
            if (userID == Const.Global_Admin || userID == Const.TONG_BIEN_TAP) {
                using (MainDB db = new MainDB()) {
                    ddlChannel.DataSource = db.ChannelCollection.GetAllAsDataTable();
                }
            }
            else // nếu không phải admin kênh , load toàn bộ kênh
            {
                MainSecurity ms = new MainSecurity();
                ddlChannel.DataSource = ms.GetChannelAsTable(userID);
            }
            ddlChannel.DataBind();
        }
        private void ShowHideButton() {
            //ImageButton delete = Utils.GetFooterImageButton(gridUser, 0, true);
            ImageButton edit;
            ImageButton assign;
            CheckBox checkbox;
            string userID = ViewState["userid"].ToString();
            foreach (DataGridItem item in gridUser.Items) {
                Label name = (Label)item.Cells[2].Controls[1];
                if (userID != Const.Global_Admin && userID != Const.TONG_BIEN_TAP) {
                    if (name.Text == Const.Global_Admin || name.Text == Const.TONG_BIEN_TAP) {
                        edit = (ImageButton)item.Cells[1].Controls[1];
                        edit.Enabled = false;
                        assign = (ImageButton)item.Cells[7].Controls[1];
                        assign.Enabled = false;
                        checkbox = (CheckBox)item.Cells[0].Controls[1];
                        checkbox.Enabled = false;
                    }
                }
                else if (userID == Const.Global_Admin) {
                    if (name.Text == Const.TONG_BIEN_TAP) {
                        edit = (ImageButton)item.Cells[1].Controls[1];
                        edit.Enabled = false;
                        assign = (ImageButton)item.Cells[7].Controls[1];
                        assign.Enabled = false;
                        checkbox = (CheckBox)item.Cells[0].Controls[1];
                        checkbox.Enabled = false;
                    }
                    if (name.Text == Const.Global_Admin) {
                        checkbox = (CheckBox)item.Cells[0].Controls[1];
                        checkbox.Enabled = false;
                    }
                }
                else {
                    if (name.Text == Const.Global_Admin) {
                        edit = (ImageButton)item.Cells[1].Controls[1];
                        edit.Enabled = false;
                        assign = (ImageButton)item.Cells[7].Controls[1];
                        assign.Enabled = false;
                        checkbox = (CheckBox)item.Cells[0].Controls[1];
                        checkbox.Enabled = false;
                    }
                    if (name.Text == Const.TONG_BIEN_TAP) {
                        checkbox = (CheckBox)item.Cells[0].Controls[1];
                        checkbox.Enabled = false;
                        assign = (ImageButton)item.Cells[7].Controls[1];
                        assign.Enabled = false;
                    }
                }
            }
        }


        private void CheckPemission(string userID) {
            MainSecurity ms = new MainSecurity();
            if (userID.Trim().ToLower() != Common.Const.Global_Admin && userID.Trim().ToLower() != Common.Const.TONG_BIEN_TAP && !ms.GetRole(userID).isQuanTriKenh && !ms.GetRole(userID).isPhuTrachKenh) {
                Response.Redirect("/users/" + Const.OBJECT_ERROR + ".aspx?message=" + Const.Message_BanKhongCoQuyen);
            }
        }

        protected void btnQuyenMacDinh_Click(object sender, EventArgs e) {
            Response.Redirect("/users/" + Const.OBJECT_QUANLY_DEFAULT + ".aspx");
        }
        private void LoadRole() {
            using (MainDB db = new MainDB()) {
                ddlRole.DataTextField = "Role_Name";
                ddlRole.DataValueField = "Role_ID";
                ddlRole.DataSource = db.RoleCollection.GetAllAsDataTable();
                ddlRole.DataBind();
                ddlRole.Items.Remove(ddlRole.Items.FindByValue(RoleConst.Administrator.ToString()));
                ddlRole.Items.Remove(ddlRole.Items.FindByValue(RoleConst.TongBienTap.ToString()));
                ddlRole.Items.Insert(0, "-- Tất cả --");
            }
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e) {
            BindGrid(-1);
        }
    }
}