using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using Portal.Core.DAL;

namespace Portal.BO.Editoral.TopClickAndComment {
    public class BaiThaoLuanTrangChu {
        //[DataObjectMethod(DataObjectMethodType.Select)]
        //public static DataTable SelectTopNew()
        //{
        //    DataTable dt = new DataTable();
        //    using (MainDB db = new MainDB())
        //    {
        //        dt = db.StoredProcedures.TopFeedBackSelect();

        //    }
        //    return dt;
        //}


        //[DataObjectMethod(DataObjectMethodType.Select)]
        //public static DataTable FeedBackNews_Select()
        //{
        //    DataTable dt = new DataTable();
        //    using (MainDB db = new MainDB())
        //    {
        //        dt = db.StoredProcedures.FeedBackNews_Select();
        //    }
        //    return dt;
        //}

        public static void Update(string strFeedBackId) {
            if (strFeedBackId.Trim() != "") {
                string[] arr = strFeedBackId.Split(',');
                if (arr.Length == 2) {
                    using (MainDB db = new MainDB()) {
                        db.StoredProcedures.vc_Execute_Sql(" Delete BaiThaoLuanNoiBat ");
                        for (int i = 0; i < arr.Length; i++) {
                            db.StoredProcedures.vc_Execute_Sql(" Insert Into BaiThaoLuanNoiBat(FeedBack_ID,[Index]) values(" + arr[i] + ", " + (i + 1) + ") ");
                        }

                        // Update nhung bai tin noi bat khac ve tin thong tuong

                        db.StoredProcedures.vc_Execute_Sql(" Update FeedBackNews Set News_Mode = 0 Where News_Status = 3 And News_Mode = 2 And FeedBack_ID Not IN (" + strFeedBackId + ") ");
                    }
                }
            }
        }

    }
}
