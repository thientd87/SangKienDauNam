
using System;
namespace DFISYS.User.Db {
    public abstract class UserRow_Base {
        private string _user_ID;
        private string _user_Name;
        private string _user_Pwd;
        private string _user_Email;
        private string _user_Address;
        private string _user_PhoneNum;
        private string _user_Im;
        private string _user_Website;
        private bool _user_isActive;
        private bool _user_isActiveNull = true;
        private System.DateTime _user_CreatedDate;
        private bool _user_CreatedDateNull = true;
        private System.DateTime _user_ModifiedDate;
        private bool _user_ModifiedDateNull = true;

        public UserRow_Base() {
            // EMPTY
        }

        public string User_ID {
            get { return _user_ID; }
            set { _user_ID = value; }
        }

        public string User_Name {
            get { return _user_Name; }
            set { _user_Name = value; }
        }

        public string User_Pwd {
            get { return _user_Pwd; }
            set { _user_Pwd = value; }
        }

        public string User_Email {
            get { return _user_Email; }
            set { _user_Email = value; }
        }

        public string User_Address {
            get { return _user_Address; }
            set { _user_Address = value; }
        }

        public string User_PhoneNum {
            get { return _user_PhoneNum; }
            set { _user_PhoneNum = value; }
        }

        public string User_Im {
            get { return _user_Im; }
            set { _user_Im = value; }
        }

        public string User_Website {
            get { return _user_Website; }
            set { _user_Website = value; }
        }

        public bool User_isActive {
            get {
                if (IsUser_isActiveNull)
                    throw new InvalidOperationException("Cannot get value because it is DBNull.");
                return _user_isActive;
            }
            set {
                _user_isActiveNull = false;
                _user_isActive = value;
            }
        }

        public bool IsUser_isActiveNull {
            get { return _user_isActiveNull; }
            set { _user_isActiveNull = value; }
        }

        public System.DateTime User_CreatedDate {
            get {
                if (IsUser_CreatedDateNull)
                    throw new InvalidOperationException("Cannot get value because it is DBNull.");
                return _user_CreatedDate;
            }
            set {
                _user_CreatedDateNull = false;
                _user_CreatedDate = value;
            }
        }

        public bool IsUser_CreatedDateNull {
            get { return _user_CreatedDateNull; }
            set { _user_CreatedDateNull = value; }
        }

        public System.DateTime User_ModifiedDate {
            get {
                if (IsUser_ModifiedDateNull)
                    throw new InvalidOperationException("Cannot get value because it is DBNull.");
                return _user_ModifiedDate;
            }
            set {
                _user_ModifiedDateNull = false;
                _user_ModifiedDate = value;
            }
        }

        public bool IsUser_ModifiedDateNull {
            get { return _user_ModifiedDateNull; }
            set { _user_ModifiedDateNull = value; }
        }

        public override string ToString() {
            System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
            dynStr.Append(':');
            dynStr.Append("  User_ID=");
            dynStr.Append(User_ID);
            dynStr.Append("  User_Name=");
            dynStr.Append(User_Name);
            dynStr.Append("  User_Pwd=");
            dynStr.Append(User_Pwd);
            dynStr.Append("  User_Email=");
            dynStr.Append(User_Email);
            dynStr.Append("  User_Address=");
            dynStr.Append(User_Address);
            dynStr.Append("  User_PhoneNum=");
            dynStr.Append(User_PhoneNum);
            dynStr.Append("  User_Im=");
            dynStr.Append(User_Im);
            dynStr.Append("  User_Website=");
            dynStr.Append(User_Website);
            dynStr.Append("  User_isActive=");
            dynStr.Append(IsUser_isActiveNull ? (object)"<NULL>" : User_isActive);
            dynStr.Append("  User_CreatedDate=");
            dynStr.Append(IsUser_CreatedDateNull ? (object)"<NULL>" : User_CreatedDate);
            dynStr.Append("  User_ModifiedDate=");
            dynStr.Append(IsUser_ModifiedDateNull ? (object)"<NULL>" : User_ModifiedDate);
            return dynStr.ToString();
        }


    }
}
