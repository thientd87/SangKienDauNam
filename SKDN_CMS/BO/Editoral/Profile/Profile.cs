using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DFISYS.User.Db;
using System.ComponentModel;

namespace DFISYS.BO.Editoral.Profile {
    public static class Profile {

        public static UserRow GetUserInfo(string _user_id) {
            using (DFISYS.User.Db.MainDB objUserDB = new DFISYS.User.Db.MainDB()) {
                return objUserDB.UserCollection.GetByPrimaryKey(_user_id);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public static void UpdateUserInfo(string _user_id, string _current_password, string _new_password, string _user_name, string _email, string _address, string _phone, string _ym, string _website) {
            UserRow objUser;

            using (DFISYS.User.Db.MainDB objUserDB = new DFISYS.User.Db.MainDB()) {
                objUser = objUserDB.UserCollection.GetByPrimaryKey(_user_id);
            }

            if (objUser != null) {
                objUser.User_Address = _address != null ? _address : "";
                objUser.User_Email = _email != null ? _email : "";
                objUser.User_Im = _ym;
                objUser.User_Name = _user_name;
                objUser.User_PhoneNum = _phone;
                objUser.User_Website = _website;

                if (_current_password != null && _new_password != null) {
                    if (objUser.User_Pwd.Trim() == FormsAuthentication.HashPasswordForStoringInConfigFile(_current_password.Trim(), "MD5") && _new_password.Trim() != "") {
                        objUser.User_Pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(_new_password.Trim(), "MD5");
                    }
                }

                using (DFISYS.User.Db.MainDB objUserDB = new DFISYS.User.Db.MainDB()) {
                    objUserDB.UserCollection.Update(objUser);
                }

                //Cập nhật thời gian thay đổi pass lần cuối cùng
                ChannelUsers.UpdateLastChanged(_user_id, FormsAuthentication.HashPasswordForStoringInConfigFile(_new_password.Trim(), "MD5"));
            }
        }
    }
}
