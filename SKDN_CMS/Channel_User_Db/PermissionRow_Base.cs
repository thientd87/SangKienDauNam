
using System;
namespace DFISYS.User.Db {
    public abstract class PermissionRow_Base {
        private int _permission_ID;
        private string _permission_Name;

        public PermissionRow_Base() {
            // EMPTY
        }

        public int Permission_ID {
            get { return _permission_ID; }
            set { _permission_ID = value; }
        }

        public string Permission_Name {
            get { return _permission_Name; }
            set { _permission_Name = value; }
        }

        public override string ToString() {
            System.Text.StringBuilder dynStr = new System.Text.StringBuilder(GetType().Name);
            dynStr.Append(':');
            dynStr.Append("  Permission_ID=");
            dynStr.Append(Permission_ID);
            dynStr.Append("  Permission_Name=");
            dynStr.Append(Permission_Name);
            return dynStr.ToString();
        }

        public RoleRow[] RoleRow {
            get {
                using (MainDB db = new MainDB()) {
                    Role_PermissionRow[] records = db.Role_PermissionCollection.GetByPermission_ID(_permission_ID);

                    System.Collections.ArrayList tempModels = new System.Collections.ArrayList();
                    foreach (Role_PermissionRow temp in records) {
                        tempModels.Add(temp.RoleRow);
                    }

                    return (RoleRow[])(tempModels.ToArray(typeof(RoleRow)));
                }
            }
        }



    }
}
