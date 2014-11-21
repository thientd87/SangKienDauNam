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
using DFISYS.BO.Editoral.Profile;
using DFISYS.User.Db;

namespace DFISYS.GUI.EditoralOffice.MainOffce.AccountProfile {
    public partial class profile : System.Web.UI.UserControl {
        string user_id;
        protected void Page_Load(object sender, EventArgs e) {
            user_id = HttpContext.Current.User.Identity.Name;

            if (!IsPostBack) {
                txtUserName.ReadOnly = true;

                if (user_id != "")
                    BindDataToForm(user_id);
            }
            objsoure.UpdateParameters["_user_id"].DefaultValue = user_id;
        }

        private void BindDataToForm(string _user_id) {
            UserRow objUser = Profile.GetUserInfo(_user_id);
            if (objUser != null) {
                txtFullName.Text = objUser.User_Name != null ? objUser.User_Name.ToString().Trim() : "";
                txtAddress.Text = objUser.User_Address != null ? objUser.User_Address.ToString().Trim() : "";
                txtEmail.Text = objUser.User_Email != null ? objUser.User_Email.ToString().Trim() : "";
                txtPhone.Text = objUser.User_PhoneNum != null ? objUser.User_PhoneNum.ToString().Trim() : "";
                txtUserName.Text = objUser.User_ID != null ? objUser.User_ID : "";
                txtWebsite.Text = objUser.User_Website != null ? objUser.User_Website.Trim() : "";
                txtYM.Text = objUser.User_Im != null ? objUser.User_Im.Trim() : "";
            }

        }

        protected void btnSave_Click(object sender, EventArgs e) {
            UserRow objUser = Profile.GetUserInfo(user_id);

            if (txtCurrentPassword.Text.Trim() != "") {
                string md5_pass = FormsAuthentication.HashPasswordForStoringInConfigFile(txtCurrentPassword.Text.Trim(), "MD5");
                if (txtPassword.Text.Trim() != txtPasswordAgian.Text.Trim()) {
                    ltrMessage.Text = "<font color=red>Gõ lại mật khẩu !</font>";
                    return;
                }

                if (md5_pass != objUser.User_Pwd) {
                    ltrMessage.Text = "<font color=red>Mật khẩu hiện thời không đúng !</font>";
                    return;
                }

                if (FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "MD5") == objUser.User_Pwd) {
                    ltrMessage.Text = "<font color=red>Mật khẩu mới không được trùng với mật khẩu cũ!</font>";
                    return;
                }

            }
            
            objsoure.Update();
            ltrMessage.Text = "<font color=red>Lưu lại thành công</font>";
        }


    }
}