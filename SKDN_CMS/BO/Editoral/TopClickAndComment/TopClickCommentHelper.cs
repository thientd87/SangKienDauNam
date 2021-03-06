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

namespace QuanLyBaiNoiBat.TopClickAndComment {
    public class TopClickCommentHelper {

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static DataTable SelectTopNew() {
            DataTable dt = new DataTable();
            using (MainDB db = new MainDB()) {
                dt = db.StoredProcedures.vc_TopNewSelect();
            }
            return dt;
        }



        public static DataTable Get4BaiNoiBat() {
            using (MainDB db = new MainDB()) {
                return db.StoredProcedures.BonBaiNoiBat_Select();
            }
        }

        public static void Clear4BaiNoiBat() {
            using (MainDB db = new MainDB()) {
                db.AnotherNonQuery("DELETE FROM temp_click_comment WHERE [Type]=1");
            }
        }

        public static void Update(string strNewsId) {
            if (strNewsId.Trim() != "") {
                int sobainoibat = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["SoBaiNoiBatTrangChu"]);
                string[] arr = strNewsId.Split(',');
                if (arr.Length == sobainoibat) {
                    using (MainDB db = new MainDB()) {
                        db.StoredProcedures.BonBaiNoiBat_Delete();
                        for (int i = 0; i < arr.Length; i++) {
                            db.StoredProcedures.BonBaiNoiBat_Insert(Convert.ToInt64(arr[i]), false, i + 1);
                        }

                        // Update nhung bai tin noi bat khac ve tin thong tuong

                        db.SelectQuery(" Update News Set News_Mode = 0 Where News_Status = 3 And News_Mode = 2 And News_ID Not IN (" + strNewsId + ") ");
                        db.SelectQuery(" Update NewsPublished Set News_Mode = 0 Where  News_Mode = 2 And News_ID Not IN (" + strNewsId + ") ");
                    }
                }
            }
        }

        public static void Update(string strNewsId, string editionType)
        {
            if (strNewsId.Trim() != "")
            {
                int sobainoibat = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["SoBaiNoiBatTrangChu"]);
                string[] arr = strNewsId.Split(',');
                if (arr.Length == sobainoibat)
                {
                    using (MainDB db = new MainDB())
                    {
                        db.StoredProcedures.BonBaiNoiBat_Delete();
                        for (int i = 0; i < arr.Length; i++)
                        {
                            db.StoredProcedures.BonBaiNoiBat_Insert(Convert.ToInt64(arr[i]), false, i + 1);
                        }

                        // Update nhung bai tin noi bat khac ve tin thong tuong

                        db.SelectQuery(" Update News Set News_Mode = 0 Where News_Status = 3 And News_Mode = 2 And News_ID Not IN (" + strNewsId + ") ");
                        db.SelectQuery(" Update NewsPublished Set News_Mode = 0 From NewsPublished Join Category On NewsPublished.Cat_ID = Category.Cat_ID" +
                                       "Where  News_Mode = 2 And News_ID Not IN (" + strNewsId + ") And Category.EditionType_ID =" + editionType + "");
                    }
                }
            }
        }

        public static void BaiNoiBat_BaiNoiBat_Update(long[] newsIds, int[] thutu, string newsIdNotSelected, string editionType)
        {
            using (MainDB db = new MainDB())
            {
                // xóa hết các bài trong bảng BonBaiNoiBat
                string sql = "Delete bt From BonBaiNoiBat bt Join Category c On bt.Cat_ID = c.Cat_ID Where c.EditionType_ID = " + editionType + Environment.NewLine;

                // insert từng bài đã chọn vào bảng BonBaiNoiBat
                for (int i = 0; i < newsIds.Length; i++)
                    sql += "Insert Into BonBaiNoiBat (News_Id, isNoiBat, Thutu) Values (" + newsIds.GetValue(i) + ", 0, " + thutu[i] + ")" + Environment.NewLine;

                // cập nhật lại những tin không được chọn thành tin bình thường
                if (!string.IsNullOrEmpty(newsIdNotSelected))
                {
                    sql += "Update News Set News_Mode = 0 From News Join Category On News.Cat_ID = Category.Cat_ID " +
                           "Where Category.EditionType_ID = " + editionType + "AND News_ID In (" + newsIdNotSelected + ") AND (News_PublishDate < DATEADD(HOUR,-48,GETDATE())) " + Environment.NewLine;
                    sql += "Update newspublished Set News_Mode = 0 From NewsPublished Join Category On NewsPublished.Cat_ID = Category.Cat_ID" +
                           " Where Category.EditionType_ID = " + editionType + " AND News_ID In (" + newsIdNotSelected + ") AND (News_PublishDate < DATEADD(HOUR,-48,GETDATE())) " + Environment.NewLine;
                }

                db.AnotherNonQuery(sql);
            }
        }

        public static DataTable LoadBaiNoiBat() {
            using (MainDB db = new MainDB()) {
                return db.StoredProcedures.BonBaiNoiBat_BaiNoiBat_Select();
            }
        }


        public static DataTable BaiNoiBat_BaiNoiBat_Select() {
            using (MainDB db = new MainDB()) {
                return db.SelectQuery("Select * From v_BaiNoiBatTrangChu_Select Order By ThuTu DESC,News_PublishDate DESC ");
            }
        }


        public static DataTable BaiNoiBat_BaiNoiBat_Select(string EditionType)
        {
            using (MainDB db = new MainDB())
            {
                return db.SelectQuery("Select * From v_BaiNoiBatTrangChu_Select vbn Join NewsPublished np On vbn.News_ID = np.News_ID Join Category c On np.Cat_ID = c.Cat_ID Where c.EditionType_ID = " + EditionType + " Order By Thutu");
            }
        }

        


        public static void BaiNoiBat_BaiNoiBat_Update(long[] newsIds, int[] thutu, string newsIdNotSelected) {
            using (MainDB db = new MainDB()) {
                // xóa hết các bài trong bảng BonBaiNoiBat
                string sql = "Delete From BonBaiNoiBat" + Environment.NewLine;

                // insert từng bài đã chọn vào bảng BonBaiNoiBat
                for (int i = 0; i < newsIds.Length; i++)
                    sql += "Insert Into BonBaiNoiBat (News_Id, isNoiBat, Thutu) Values (" + newsIds.GetValue(i) + ", 0, " + thutu[i] + ")" + Environment.NewLine;

                // cập nhật lại những tin không được chọn thành tin bình thường
                if (!string.IsNullOrEmpty(newsIdNotSelected)) {
                    sql += "Update News Set News_Mode = 0 Where News_ID In (" + newsIdNotSelected + ") AND (News_PublishDate < DATEADD(HOUR,-48,GETDATE())) " + Environment.NewLine;
                    sql += "Update newspublished Set News_Mode = 0 Where News_ID In (" + newsIdNotSelected + ") AND (News_PublishDate < DATEADD(HOUR,-48,GETDATE())) " + Environment.NewLine;
                }

                db.AnotherNonQuery(sql);
            }
        }


        public static string GetSenderIDByNewsID(string news_id) {
            string sReturn = "";
            using (MainDB objDb = new MainDB()) {
                DataTable dt = objDb.SelectQuery(" select top 1 sender_id from action where news_id = " + news_id + " order by createdate desc ");
                if (dt.Rows.Count > 0)
                    sReturn = dt.Rows[0][0].ToString();
                else
                    sReturn = "";
            }

            return sReturn;
        }
    }
}
