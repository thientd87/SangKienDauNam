using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DFISYS.API;
using DFISYS.Core.DAL;

namespace DFISYS.BO.Editoral.EditionType {
    public class EditionTypeHelper {
        public static DataTable SelectAllForDropdownlist() {
            using (MainDB db = new MainDB()) {
                DataTable table = db.StoredProcedures.vc_EditionType_SelectAll();
                DataRow dr = table.NewRow();
                dr["EditionType_ID"] = 0;
                dr["EditionName"] = "-- Chọn loại --";
                table.Rows.InsertAt(dr, 0);

                return table;
            }
        }

        public static DataTable SelectAllForDropdownlist(DropDownList dl) {
            using (MainDB db = new MainDB()) {
                DataTable table = db.StoredProcedures.vc_EditionType_SelectAll();
                //DataRow dr = table.NewRow();
                //dr["EditionType_ID"] = 0;
                //dr["EditionName"] = "-- Chọn loại --";
                //table.Rows.InsertAt(dr, 0);

                int RowCount = table.Rows.Count;
                for (int i = 0; i < RowCount; i++) {
                    dl.Items.Add(new ListItem(table.Rows[i]["EditionName"].ToString(), table.Rows[i]["EditionType_ID"].ToString()));
                }

                return table;
            }
        }

        public static DataTable GetEditionTypeDetail(int editionType_ID)
        {
            using (MainDB db = new MainDB())
            {
                DataTable table = db.StoredProcedures.vc_EditionType_SelectOne(editionType_ID);
                
                return table;
            }
        }

    }
}
