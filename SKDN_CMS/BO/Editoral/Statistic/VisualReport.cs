using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Xml;
using System.IO;
using System.Data.SqlClient;
using Portal.Core.DAL;

namespace Portal.BO.Editoral.Statistic {

    public enum CountKey : byte {
        All_View = 1,
        Home_View = 2,
        Category_View = 3,
        Content_View = 4,
        Error_500 = 5,
        Category_Wait_Edit_Content = 6,
        Category_Wait_Approve_Content = 7,
        Category_Approved_Content = 8,
        UNIQUE_USER_ACCESS = 9,
        UNIQUE_IP = 10,
        User_Wait_Edit_Content = 11,
        User_Wait_Approve_Content = 12,
        User_Approved_Content = 13,
        Custom_User_View = 14
    }

    public class VisualReport : ArrayList {
        private string _XValue;
        private string _ZValue;
        private string _CountKeyText;
        private string _CountKeyValue;
        private string _ImageUrl;
        private decimal _Value;
        private decimal _MaxValue;

        public VisualReport() { }

        public string ImageUrl {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }

        public string CountKeyText {
            get { return _CountKeyText; }
            set { _CountKeyText = value; }
        }

        public string CountKeyValue {
            get { return _CountKeyValue; }
            set { _CountKeyValue = value; }
        }

        public string XValue {
            get { return _XValue; }
            set { _XValue = value; }
        }

        public string ZValue {
            get { return _ZValue; }
            set { _ZValue = value; }
        }
        public decimal Value {
            get { return _Value; }
            set { _Value = value; }
        }

        public decimal MaxValue {
            get { return _MaxValue; }
            set { _MaxValue = value; }
        }

        public ArrayList GenerateCountKeyDesc(Int64 _contentId, string _catId, string _userId) {
            //VisualReportCollection items = new VisualReportCollection();
            ArrayList items = new ArrayList();

            HttpContext context = HttpContext.Current;
            XmlReader reader = new XmlTextReader(File.OpenRead(context.Server.MapPath("~/GUI/EditoralOffice/MainOffce/Statistic/XMLFiles/StatDef.xml")));
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();

            XmlElement root = doc.DocumentElement;

            if (_contentId == -1 && _catId == "" && _userId == "") {

                VisualReport item = new VisualReport();
                item.CountKeyText = root.GetElementsByTagName("S_" + ((int)CountKey.All_View)).Item(0).InnerText;
                if (item.CountKeyText != null) {
                    item.CountKeyValue = ((int)CountKey.All_View).ToString();
                    items.Add(item);
                }

                item = new VisualReport();
                item.CountKeyText = root.GetElementsByTagName("S_" + ((int)CountKey.Home_View)).Item(0).InnerText;
                if (item.CountKeyText != null) {
                    item.CountKeyValue = ((int)CountKey.Home_View).ToString();
                    items.Add(item);
                }

                item = new VisualReport();
                item.CountKeyText = root.GetElementsByTagName("S_" + ((int)CountKey.Error_500)).Item(0).InnerText;
                if (item.CountKeyText != null) {
                    item.CountKeyValue = ((int)CountKey.Error_500).ToString();
                    items.Add(item);
                }

                item = new VisualReport();
                item.CountKeyText = root.GetElementsByTagName("S_" + ((int)CountKey.UNIQUE_IP)).Item(0).InnerText;
                if (item.CountKeyText != null) {
                    item.CountKeyValue = ((int)CountKey.UNIQUE_IP).ToString();
                    items.Add(item);
                }

                item = new VisualReport();
                item.CountKeyText = root.GetElementsByTagName("S_" + ((int)CountKey.UNIQUE_USER_ACCESS)).Item(0).InnerText;
                if (item.CountKeyText != null) {
                    item.CountKeyValue = ((int)CountKey.UNIQUE_USER_ACCESS).ToString();
                    items.Add(item);
                }
            }
            else
                if (_contentId >= 0 && _catId == "" && _userId == "") {

                    // Voi truong hop thong ke theo content
                    VisualReport item = new VisualReport();
                    item.CountKeyText = root.GetElementsByTagName("S_" + ((int)CountKey.Content_View).ToString()).Item(0).InnerText;
                    item.CountKeyValue = ((int)CountKey.Content_View).ToString();
                    items.Add(item);
                }
                else
                    if (_contentId == -1 && _catId != "" && _userId == "") {

                        // Voi truong hop thong ke theo Category
                        VisualReport item = new VisualReport();
                        item.CountKeyText = root.GetElementsByTagName("S_" + ((int)CountKey.Category_View).ToString()).Item(0).InnerText;
                        item.CountKeyValue = ((int)CountKey.Category_View).ToString();
                        items.Add(item);
                        item = new VisualReport();
                        item.CountKeyText = root.GetElementsByTagName("S_" + ((int)CountKey.Category_Wait_Approve_Content).ToString()).Item(0).InnerText;
                        item.CountKeyValue = ((int)CountKey.Category_Wait_Approve_Content).ToString();
                        items.Add(item);
                        item = new VisualReport();
                        item.CountKeyText = root.GetElementsByTagName("S_" + ((int)CountKey.Category_Wait_Edit_Content).ToString()).Item(0).InnerText;
                        item.CountKeyValue = ((int)CountKey.Category_Wait_Edit_Content).ToString();
                        items.Add(item);
                        item = new VisualReport();
                        item.CountKeyText = root.GetElementsByTagName("S_" + ((int)CountKey.Category_Approved_Content).ToString()).Item(0).InnerText;
                        item.CountKeyValue = ((int)CountKey.Category_Approved_Content).ToString();
                        items.Add(item);
                    }
                    else
                        if (_contentId == -1 && _catId == "" && _userId != "") {
                            // Voi truong hop thong ke theo User
                            VisualReport item = new VisualReport();
                            item = new VisualReport();
                            item.CountKeyText = root.GetElementsByTagName("S_" + ((int)CountKey.User_Wait_Edit_Content).ToString()).Item(0).InnerText;
                            item.CountKeyValue = ((int)CountKey.User_Wait_Edit_Content).ToString();
                            items.Add(item);
                            item = new VisualReport();
                            item.CountKeyText = root.GetElementsByTagName("S_" + ((int)CountKey.User_Wait_Approve_Content).ToString()).Item(0).InnerText;
                            item.CountKeyValue = ((int)CountKey.User_Wait_Approve_Content).ToString();
                            items.Add(item);
                            item = new VisualReport();
                            item.CountKeyText = root.GetElementsByTagName("S_" + ((int)CountKey.User_Approved_Content).ToString()).Item(0).InnerText;
                            item.CountKeyValue = ((int)CountKey.User_Approved_Content).ToString();
                            items.Add(item);
                        }

            return items;
        }

        //public VisualReport GetReport(string CountKey, string strID, string StartDay, string EndDay, string MinVal, string MaxVal) {
        //    DataTable dt;
        //    using (MainDB db = new MainDB()) {
        //        dt = db.StoredProcedures.Stats_GetReport(EndDay, MaxVal, CountKey, StartDay, strID, MinVal);
        //    }
        //    if (dt.Rows.Count == 0) return null;

        //    VisualReport items = new VisualReport();
        //    foreach (DataRow row in dt.Rows) {
        //        VisualReport item = new VisualReport();
        //        item.XValue = row["xValues"].ToString();
        //        item.ZValue = row["zValues"].ToString();
        //        if (row["yValues"].ToString() == "") return null;
        //        item.Value = Convert.ToDecimal(row["yValues"]);
        //        items.Add(item);
        //    }
        //    return items;
        //}

        //public VisualReport GetReportByMonth(string CountKey, string strID, string StartDay, string EndDay, string MinVal, string MaxVal) {
        //    //DataSet dsData = SqlHelper.ExecuteDataset(_SqlConnString, "Stats_GetReportByMonth", CountKey, strID, StartDay, EndDay, MinVal, MaxVal);
        //    DataTable dt;
        //    using (MainDB db = new MainDB()) {
        //        dt = db.StoredProcedures.Stats_GetReportByMonth(EndDay, MaxVal, CountKey, StartDay, strID, MinVal);
        //    }
        //    if (dt.Rows.Count == 0) return null;

        //    VisualReport items = new VisualReport();

        //    foreach (DataRow row in dt.Rows) {
        //        VisualReport item = new VisualReport();
        //        item.XValue = row["xValues"].ToString();
        //        item.ZValue = row["zValues"].ToString();
        //        if (row["yValues"].ToString() == "") return null;
        //        item.Value = Convert.ToDecimal(row["yValues"]);
        //        items.Add(item);
        //    }
        //    return items;
        //}

        //public VisualReport GetReportByYear(string CountKey, string strID, string StartDay, string EndDay, string MinVal, string MaxVal) {
        //    //DataSet dsData = SqlHelper.ExecuteDataset(_SqlConnString, "Stats_GetReportByYear", CountKey, strID, StartDay, EndDay, MinVal, MaxVal);
        //    DataTable dt;
        //    using (MainDB db = new MainDB()) {
        //        dt = db.StoredProcedures.Stats_GetReportByYear(EndDay, MaxVal, CountKey, StartDay, strID, MinVal);
        //    }
        //    if (dt.Rows.Count == 0) return null;
        //    VisualReport items = new VisualReport();

        //    foreach (DataRow row in dt.Rows) {
        //        VisualReport item = new VisualReport();
        //        item.XValue = row["xValues"].ToString();
        //        item.ZValue = row["zValues"].ToString();
        //        if (row["yValues"].ToString() == "") return null;
        //        item.Value = Convert.ToDecimal(row["yValues"]);
        //        items.Add(item);
        //    }
        //    return items;
        //}

        //public VisualReport GetReportByWeek(string CountKey, string strID, string StartDay, string EndDay, string MinVal, string MaxVal) {
        //    //DataSet dsData = SqlHelper.ExecuteDataset(_SqlConnString, "Stats_GetReportByWeek", CountKey, strID, StartDay, EndDay, MinVal, MaxVal);
        //    DataTable dt;
        //    using (MainDB db = new MainDB()) {
        //        dt = db.StoredProcedures.Stats_GetReportByWeek(EndDay, MaxVal, CountKey, StartDay, strID, MinVal);
        //    }
        //    if (dt.Rows.Count == 0) return null;

        //    VisualReport items = new VisualReport();
        //    foreach (DataRow row in dt.Rows) {
        //        VisualReport item = new VisualReport();
        //        item.XValue = row["xValues"].ToString();
        //        item.ZValue = row["zValues"].ToString();
        //        if (row["yValues"].ToString() == "") return null;
        //        item.Value = Convert.ToDecimal(row["yValues"]);
        //        items.Add(item);
        //    }
        //    return items;
        //}

        //public VisualReport GetReportByQuater(string CountKey, string strID, string StartDay, string EndDay, string MinVal, string MaxVal) {
        //    //DataSet dsData = SqlHelper.ExecuteDataset(_SqlConnString, "Stats_GetReportByQuater", CountKey, strID, StartDay, EndDay, MinVal, MaxVal);
        //    DataTable dt;
        //    using (MainDB db = new MainDB()) {
        //        dt = db.StoredProcedures.Stats_GetReportByQuater(EndDay, MaxVal, CountKey, StartDay, strID, MinVal);
        //    }
        //    if (dt.Rows.Count == 0) return null;

        //    VisualReport items = new VisualReport();

        //    foreach (DataRow row in dt.Rows) {
        //        VisualReport item = new VisualReport();
        //        item.XValue = row["xValues"].ToString();
        //        item.ZValue = row["zValues"].ToString();
        //        if (row["yValues"].ToString() == "") return null;
        //        item.Value = Convert.ToDecimal(row["yValues"]);
        //        items.Add(item);
        //    }
        //    return items;
        //}

        //public SqlDataReader GetReportAsDataReader(string CountKey, string strID, string StartDay, string EndDay, string MinVal, string MaxVal) {
        //    SqlDataReader dsData;//= SqlHelper.ExecuteReader(_SqlConnString, "Stats_GetReport", CountKey, strID, StartDay, EndDay, MinVal, MaxVal);
        //    using (MainDB db = new MainDB()) {
        //        dsData = db.StoredProcedures.GetReportAsDataReader(CountKey, strID, StartDay, EndDay, MinVal, MaxVal);
        //    }

        //    return dsData;
        //}

        //public SqlDataReader GetReporByWeekAsDataReader(string CountKey, string strID, string StartDay, string EndDay, string MinVal, string MaxVal) {
        //    //SqlDataReader dsData = SqlHelper.ExecuteReader(_SqlConnString, "Stats_GetReportByWeek", CountKey, strID, StartDay, EndDay, MinVal, MaxVal);
        //    SqlDataReader dsData;
        //    using (MainDB db = new MainDB()) {
        //        dsData = db.StoredProcedures.GetReporByWeekAsDataReader(CountKey, strID, StartDay, EndDay, MinVal, MaxVal);
        //    }
        //    return dsData;
        //}

        //public SqlDataReader GetReporByMonthAsDataReader(string CountKey, string strID, string StartDay, string EndDay, string MinVal, string MaxVal) {
        //    // SqlDataReader dsData = SqlHelper.ExecuteReader(_SqlConnString, "Stats_GetReportByMonth", CountKey, strID, StartDay, EndDay, MinVal, MaxVal);
        //    SqlDataReader dsData;
        //    using (MainDB db = new MainDB()) {
        //        dsData = db.StoredProcedures.GetReporByMonthAsDataReader(CountKey, strID, StartDay, EndDay, MinVal, MaxVal);
        //    }
        //    return dsData;
        //}

        //public SqlDataReader GetReporByYearAsDataReader(string CountKey, string strID, string StartDay, string EndDay, string MinVal, string MaxVal) {
        //    //SqlDataReader dsData = SqlHelper.ExecuteReader(_SqlConnString, "Stats_GetReportByYear", CountKey, strID, StartDay, EndDay, MinVal, MaxVal);
        //    SqlDataReader dsData;
        //    using (MainDB db = new MainDB()) {
        //        dsData = db.StoredProcedures.GetReporByYearAsDataReader(CountKey, strID, StartDay, EndDay, MinVal, MaxVal);
        //    }

        //    return dsData;
        //}

        //public SqlDataReader GetReporByQuaterAsDataReader(string CountKey, string strID, string StartDay, string EndDay, string MinVal, string MaxVal) {
        //    //SqlDataReader dsData = SqlHelper.ExecuteReader(_SqlConnString, "Stats_GetReportByQuater", CountKey, strID, StartDay, EndDay, MinVal, MaxVal);
        //    SqlDataReader dsData;
        //    using (MainDB db = new MainDB()) {
        //        dsData = db.StoredProcedures.GetReporByQuaterAsDataReader(CountKey, strID, StartDay, EndDay, MinVal, MaxVal);
        //    }

        //    return dsData;
        //}

    }
}
