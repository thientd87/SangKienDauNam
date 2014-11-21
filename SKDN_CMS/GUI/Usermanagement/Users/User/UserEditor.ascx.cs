using System;
using System.Web.Security;
using System.Web.UI;
using DFISYS.GUI.Users.Common;
using DFISYS.User.Db;
using DFISYS.User.Security;

namespace DFISYS.GUI.Users.User
{
    public partial class UserEditor : UserControl
    {
        #region Members
        const string listPage = "/"+Const.OBJECT_USER+".aspx";
        #endregion

        #region Page Load event handler
        private void Page_Load(object sender, EventArgs e)
        {
            Page.RegisterHiddenField("hidIdPrefix", ClientID + "_");
            cmdSave.Attributes.Add("onclick", "return Validate();");
            if (!Page.IsPostBack)
            {
                //string id = Request["id"];
                string id = Session["Edit_UserID"] != null ? Session["Edit_UserID"].ToString() : "";
                ViewState["originalId"] = id;
                ViewState["userid"] = Page.User.Identity.Name;//"admin";
                CheckPemission(ViewState["userid"].ToString());
                if (id != null && id.Length > 0)
                {
                    BindForm(id);
                    txtUser_ID.Enabled = false;
                    cbxPassword.Visible = true;
                    txtUser_Pwd.Enabled = false;
                    lblTitle.Text = "Sửa thông tin người dùng";
                }
                else
                {
                    cbxPassword.Visible = false;
                    //nếu thêm mới người dùng không cho kích hoạt trạng thái
                    chkUser_isActive.Enabled = false;
                    lblTitle.Text = "Thêm mới thông tin người dùng";
                }
                LoadChannel();

                if (Session["ddlChannel"] != null)
                {
                    ddlChannel.SelectedValue = Session["ddlChannel"].ToString();
                }

                Session["ddlChannel_UserEdit"] = ddlChannel.SelectedValue;
            } // not post back
            else
            {
                SetErrorMessage(string.Empty);
            } // post back

            

        } // page load	
        #endregion

        #region Page binding
        private void CheckPemission(string userID)
        {
            MainSecurity ms = new MainSecurity();
            if (userID.Trim().ToLower() != Common.Const.Global_Admin && userID.Trim().ToLower() != Common.Const.TONG_BIEN_TAP && !ms.GetRole(userID).isQuanTriKenh && !ms.GetRole(userID).isPhuTrachKenh)
            {
                Response.Redirect("/users/" + Const.OBJECT_ERROR + ".aspx?message=" + Const.Message_BanKhongCoQuyen);
            }
        }

        private void BindForm(string id)
        {
            try
            {
                // load up the specified row						
                using (MainDB db = new MainDB())
                {
                    UserRow row = db.UserCollection.GetByPrimaryKey(id);
                    this.txtUser_ID.Text = SafeString(row.User_ID);
                    this.txtUser_Name.Text = SafeString(row.User_Name);
                    //this.txtUser_Pwd.Text = SafeString(row.User_Pwd);
                    this.txtUser_Email.Text = SafeString(row.User_Email);
                    this.txtUser_Address.Text = SafeString(row.User_Address);
                    this.txtUser_PhoneNum.Text = SafeString(row.User_PhoneNum);
                    this.txtUser_Im.Text = SafeString(row.User_Im);
                    this.txtUser_Website.Text = SafeString(row.User_Website);
                    this.chkUser_isActive.Checked = row.User_isActive;
                } // using
            } // try															
            catch (Exception ex)
            {
                //SetErrorMessage(ex.Message);
            } // try/catch
        } // BindForm
        #endregion

        #region Save and Cancel button event handlers
        private void OnSaveClick(object sender, EventArgs e)
        {
            try
            {
                Business.User user = new Business.User();
                UserRow row = new UserRow();
                // Neu ma truong hop Update thi Get cac thong tin cua user len
                if (txtUser_ID.Enabled == false)
                {
                    using (MainDB db = new MainDB())
                    {
                        row = db.UserCollection.GetAsArray(" User_Id = '" + txtUser_ID.Text + "'", "")[0];
                    }
                }

                if (this.txtUser_ID.Text.Length > 0) row.User_ID = this.txtUser_ID.Text;
                if (this.txtUser_Name.Text.Length > 0) row.User_Name = this.txtUser_Name.Text;
                if ((this.txtUser_Pwd.Text.Length > 0 && !cbxPassword.Visible) || this.txtUser_Pwd.Text.Length > 0 && cbxPassword.Checked)
                {
                    string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(txtUser_Pwd.Text, "MD5");
                    row.User_Pwd = pass;
                }
                if (this.txtUser_Email.Text.Length > 0) row.User_Email = this.txtUser_Email.Text;
                if (this.txtUser_Address.Text.Length > 0) row.User_Address = this.txtUser_Address.Text;
                if (this.txtUser_PhoneNum.Text.Length > 0) row.User_PhoneNum = this.txtUser_PhoneNum.Text;
                if (this.txtUser_Im.Text.Length > 0) row.User_Im = this.txtUser_Im.Text;
                if (this.txtUser_Website.Text.Length > 0) row.User_Website = this.txtUser_Website.Text;
                row.User_isActive = this.chkUser_isActive.Checked;

                using (MainDB db = new MainDB())
                {
                    string originalId = (string)ViewState["originalId"];
                    if (originalId != null && originalId.Length > 0)
                    {
                        row.User_ID = originalId;
                        row.User_ModifiedDate = DateTime.Now;
                        db.UserCollection.Update(row);
                        //kiểm tra xem người dùng đã được gán vào kênh này chưa.
                        Channel_UserRow[] curCheck =  db.Channel_UserCollection.GetAsArray("User_ID='" + (string)ViewState["originalId"] + "' and Channel_ID=" + ddlChannel.SelectedValue, "");
                        if (curCheck == null)
                        {
                            //gán người dùng vào kênh
                            Channel_UserRow cur = new Channel_UserRow();
                            cur.User_ID = txtUser_ID.Text;
                            cur.Channel_ID = Int32.Parse(ddlChannel.SelectedValue);
                            db.Channel_UserCollection.Insert(cur);
                        }
                    }
                    else
                    {
                        if (user.isUserExited(txtUser_ID.Text))
                        {
                            SetErrorMessage("Đã tồn tại User này");
                            return;
                        }
                        row.User_CreatedDate = DateTime.Now;
                        db.UserCollection.Insert(row);
                        //gán người dùng vào kênh
                        Channel_UserRow cur = new Channel_UserRow();
                        cur.User_ID = txtUser_ID.Text;
                        cur.Channel_ID = Int32.Parse(ddlChannel.SelectedValue);
                        db.Channel_UserCollection.Insert(cur);
                    }
                }
                Session["Edit_UserID"] = "";
                Response.Redirect(listPage);
            } // try															
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message);
            } // try/catch	
        }
        
        private void OnCancelClick(object sender, EventArgs e)
        {
            Session["Edit_UserID"] = "";
            Response.Redirect(listPage);
        }
        #endregion

        #region Helper functions
        // Displays an error message
        private void SetErrorMessage(string text)
        {
            _errorLabel.Text = "ERROR: " + text;
            _errorLabel.Visible = null != text && 0 < text.Length;
        }

        /// <summary>
        /// Prevents errors when strings (or other types) are null and we just want the
        /// closest string approximation we can get.
        /// </summary>
        /// <param name="obj">the object to check</param>
        /// <returns>the object if it is a non-null string, or the ToString of the object if it is a non-null non-string, or string.empty if it is null</returns>
        private string SafeString(object obj)
        {
            if (obj == null)
                return string.Empty;
            else
            {
                if (obj.GetType() == typeof(string))
                    return (string)obj;
                else
                    return obj.ToString();
            }
        }
        #endregion

        #region Web Form Designer generated code        

        override protected void OnInit(EventArgs e)
        {
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
        private void InitializeComponent()
        {
            this.cmdSave.Click += new System.EventHandler(OnSaveClick);
            this.btnCancel.Click += new System.EventHandler(OnCancelClick);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion			

        protected void cbxPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxPassword.Checked)
            {
                txtUser_Pwd.Enabled = true;
                txtUser_Pwd.Focus();
            }
            else
            {
                txtUser_Pwd.Enabled = false;
            }
        }
        private void LoadChannel()
        {
            ddlChannel.DataTextField = "Channel_Name";
            ddlChannel.DataValueField = "Channel_ID";
            MainSecurity ms = new MainSecurity();
            string userID = ViewState["userid"].ToString();
            //nếu là admin global hoặc tổng biên tập
            if (userID==Const.Global_Admin || userID == Const.TONG_BIEN_TAP)
            {
                using (MainDB db = new MainDB())
                {
                    ddlChannel.DataSource = db.ChannelCollection.GetAllAsDataTable();
                }
            }
            else
            {
                ddlChannel.DataSource = ms.GetChannelAsTable(ViewState["userid"].ToString());
            }
            ddlChannel.DataBind();
        }
    }
}