using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DFISYS.Core.DAL;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace ThreadManagement.BO {
    public class ThreadHelper {
        public ThreadHelper() { }

        
        public static  DataTable GetThreadFocus()
        {
            DataTable dt;
            using(MainDB db = new MainDB())
            {
                dt = db.StoredProcedures.nc_GetLatestThreads(0);
            }
            return dt;
        }

        public static void UpdateThreadOrder(int Thread_ID, int Order, int CateID, string Logo)
        {
            using(MainDB db = new MainDB())
            {
                db.StoredProcedures.Nc_UpdateOrderThread(Thread_ID, Order, CateID, Logo);
            }
        }

        #region GetThreadlist dung store
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static DataTable GetThreadlist(string strWhere, int PageSize, int StartRow) {
            //lay thong tin ve menh de where de loc thong tin
            if (strWhere == null)
                strWhere = "";
            DataTable table = new DataTable();
            using (MainDB objdb = new MainDB())
                table = objdb.StoredProcedures.vc_NewsThread_SelectList(strWhere, StartRow, PageSize);

            DataTable objTemp = table.Clone();
            if (table.Rows.Count == 0) {
                DataRow dr = objTemp.NewRow();
                dr["Thread_ID"] = 0;
                dr["Title"] = "Chưa có dữ liệu";
                dr["Thread_isForcus"] = false;
                dr["Thread_Logo"] = "";
                dr["Row"] = 0;
                objTemp.Rows.Add(dr);
                table.Dispose();
                return objTemp;
            }
            return table;
        }

        public DataTable GetThreadlist_Old(string strWhere, int PageSize, int StartRow) {
            //lay thong tin ve menh de where de loc thong tin
            if (strWhere == null)
                strWhere = "";
            DataTable objresult;
            int intPageNum = StartRow / PageSize + 1;
            //lay duoc du lieu cua tat ca nhung thang co trang thai la status can lay
            using (MainDB objdb = new MainDB()) {
                objresult = objdb.NewsThreadCollection.GetPageAsDataTable(intPageNum, PageSize, strWhere, "Thread_ID DESC"); ;
            }
            return objresult;
        }
        #endregion

        #region GetThreadRowsCount dung Store
        /// <summary>
        /// Ham thuc hien lay thong tin row count de phan trang
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        /// 
        public int GetThreadRowsCount(string strWhere) {
            if (strWhere == null)
                strWhere = "";
            DataTable objresult;
            using (MainDB objdb = new MainDB()) {
                //objresult = objdb.NewsThreadCollection.GetAsDataTable(strWhere, ""); ;
                objresult = objdb.StoredProcedures.vc_NewsThread_SelectList_Count(strWhere);
            }
            if (objresult.Rows.Count == 0) return 1;
            else return Convert.ToInt32(objresult.Rows[0][0]);
            //return objresult.Rows.Count;
        }
        public int GetThreadRowsCount_Old(string strWhere) {
            if (strWhere == null)
                strWhere = "";
            DataTable objresult;
            using (MainDB objdb = new MainDB()) {
                objresult = objdb.NewsThreadCollection.GetAsDataTable(strWhere, ""); ;
            }
            return objresult.Rows.Count;
        }
        #endregion

        //[DataObjectMethod(DataObjectMethodType.Delete)]
        //public string[] DelThread(string _selected_id) {

        //    string[] strImpact = new string[2];

        //    if (_selected_id.Trim() != "") {
        //        DataTable dt;
        //        DataTable dtCount;
        //        string[] objTempSel = _selected_id.Split(',');
        //        int count = 0;
        //        StringBuilder strThreadIDDel = new StringBuilder();
        //        for (int i = 0; i < objTempSel.Length; i++) {
        //            dt = ThreadSelectOne(Convert.ToInt32(objTempSel[i]));
        //            dtCount = ThreadCountNews(Convert.ToInt32(objTempSel[i]));
        //            if (dt.Rows.Count > 0) {
        //                if(dtCount.Rows.Count > 0)
        //                {
        //                    if (Convert.ToInt32(dtCount.Rows[0]["NewsCount"].ToString()) > 0)
        //                    {
        //                        strImpact[0] += dt.Rows[0]["Title"].ToString() + ",";
        //                    }
        //                }
        //                else if (dtCount.Rows.Count == 0)
        //                {
        //                    string strTitle = dt.Rows[0]["Title"].ToString();

        //                    using (MainDB objDB = new MainDB())
        //                    {
        //                        objDB.ThreadDetailCollection.DeleteByThread_ID(Convert.ToInt32(objTempSel[i]));
        //                        count++;
        //                    }

        //                    strThreadIDDel.Append(objTempSel[i] + ",");

        //                    string strAction = "<b>" + strTitle + "</b> " + Portal.LogAction.LogAction_Thread_DeleteThread + " bởi " + HttpContext.Current.User.Identity.Name;
        //                }
        //                //Portal.LogAction.InsertMemCache(HttpContext.Current.User.Identity.Name, DateTime.Now, strAction, Portal.LogAction.LogType_Thread, Convert.ToInt64(objTempSel[i]));
        //            }
        //        }

        //        strImpact[1] = count.ToString();

        //        if(strThreadIDDel.Length > 0 && strThreadIDDel != null)
        //        {
        //            strThreadIDDel.Remove(strThreadIDDel.Length - 1, 1);
        //            using (MainDB objDB = new MainDB()) {

        //            objDB.NewsThreadCollection.Delete("Thread_ID in (" + strThreadIDDel.ToString() + ")");
        //        }
        //        }
                
        //    }

        //    return strImpact;
        //}

        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable getPage(int numPage) {
            int intPagenum = numPage;
            DataTable objTb = new DataTable();
            objTb.Columns.Add(new DataColumn("Text", typeof(string)));
            objTb.Columns.Add(new DataColumn("Value", typeof(string)));
            for (int i = 1; i <= intPagenum; i++) {
                DataRow myRow = objTb.NewRow();
                myRow["Text"] = i.ToString();
                myRow["Value"] = Convert.ToString(i - 1);
                objTb.Rows.Add(myRow);
            }
            if (intPagenum == 0) {
                DataRow myRow = objTb.NewRow();
                myRow["Text"] = "1";
                myRow["Value"] = "0";
                objTb.Rows.Add(myRow);
            }
            return objTb;
        }

        #region GetAllThread Dung Store
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable getAllThread() {
            DataTable objResult;
            using (MainDB objDb = new MainDB()) {
                //objResult = objDb.NewsThreadCollection.GetAllAsDataTable();
                objResult = objDb.StoredProcedures.vc_Execute_Sql("SELECT * FROM NewsThread");
            }
            return objResult;
        }
        #endregion

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void createThread(string _thread_title, bool _thread_isfocus, string _thread_logo, string _thread_rt, int _thread_rc, int _status)
        {
            NewsThreadRow objrow = new NewsThreadRow();
            objrow.Title = _thread_title;
            objrow.Thread_isForcus = _thread_isfocus;
            objrow.Thread_RT = _thread_rt;
            objrow.Thread_RC = _thread_rc;
            objrow.Status = 1;
            if (_thread_logo != "" && _thread_logo != null)
                objrow.Thread_Logo = _thread_logo;
            using (MainDB objdb = new MainDB()) {
                DataTable table = objdb.StoredProcedures.vc_NewsThread_CheckExistingTitle(_thread_title, 0);
                if (table.Rows.Count == 0) {
                    objdb.NewsThreadCollection.Insert(objrow);
                }
            }
        }

        //public DataTable ThreadSelectOne(Int32 Thread_ID) {
        //    using (MainDB db = new MainDB())
        //        return db.StoredProcedures_Family.vc_NewsThread_SelectOne(Thread_ID);
        //}

        //public DataTable ThreadCountNews(Int32 Thread_ID)
        //{
        //    using (MainDB db = new MainDB())
        //        return db.StoredProcedures_Family.vc_NewsThread_CountNews(Thread_ID);
        //}

        //public void ThreadInsert(String Title, Boolean IsFocus, string _FileUrl, String RC, String RT) {
        //    using (MainDB db = new MainDB())
        //        db.StoredProcedures_Family.vc_NewsThread_Insert(Title, IsFocus, _FileUrl, RT, RC);

        //    string strAction = "<b>" + Title + "</b> " + Portal.LogAction.LogAction_Thread_CreateThread + " bởi " + HttpContext.Current.User.Identity.Name;
        //    //Portal.LogAction.InsertMemCache(HttpContext.Current.User.Identity.Name, DateTime.Now, strAction, Portal.LogAction.LogType_Thread, -1);
        //}

        //public void ThreadUpdate(String Title, Boolean IsFocus, string _FileUrl, String RC, String RT, Int32 Thread_ID) {
        //    using (MainDB db = new MainDB())
        //        db.StoredProcedures_Family.vc_NewsThread_Update(Title, IsFocus, _FileUrl, RT, RC, Thread_ID);

        //    string strAction = "<b>" + Title + "</b> " + Portal.LogAction.LogAction_Thread_UpdateThread + " bởi " + HttpContext.Current.User.Identity.Name;
        //    //Portal.LogAction.InsertMemCache(HttpContext.Current.User.Identity.Name, DateTime.Now, strAction, Portal.LogAction.LogType_Thread, Convert.ToInt64(Thread_ID));
        //}

        //public DataTable GetThreads(String Thread_ID) {
        //    if (Thread_ID == "") Thread_ID = "0";
        //    using (MainDB db = new MainDB())
        //        return db.StoredProcedures_Family.vc_NewsThread_SelectByListID(Thread_ID);
        //}
        //public DataTable GetCats(String Cat_ID) {
        //    if (Cat_ID == "") Cat_ID = "0";
        //    using (MainDB db = new MainDB())
        //        return db.StoredProcedures_Family.vc_Category_SelectByListID(Cat_ID);
        //}
        //public DataTable RunSql(String sql) {
        //    using (MainDB db = new MainDB())
        //        return db.StoredProcedures_Family.vc_Sql_Run(sql);
        //}

        //public DataTable GetListThread(string strCatID) {
        //    string strWhere = "";
        //    if (strCatID.Trim() != "" && strCatID.Trim() != "0")
        //        strWhere = " Where Thread_RC like '%" + strCatID + "%' ";

        //    using (MainDB db = new MainDB()) {
        //        return db.StoredProcedures_Family.vc_Sql_Run("SElECT Thread_ID,Title FROM NewsThread " + strWhere);
        //    }
        //}

        //public void UpdateThreadFocus(string thread_id, bool isActive) {
        //    using (MainDB db = new MainDB()) {
        //        db.StoredProcedures_Family.vc_Sql_Run(" Update NewsThread Set Thread_isForcus = '" + isActive + "' Where Thread_ID = " + thread_id);
        //    }
        //}

        //public void UpdateThreadOrder(List<int> thutu, List<long> thread_id) {
        //    using (MainDB db = new MainDB()) {
        //        for (int i = 0; i < thutu.Count; i++) {
        //            db.StoredProcedures_Family.vc_Sql_Run(" Update NewsThread Set Thread_Order = " + thutu[i].ToString() + " Where Thread_ID = " + thread_id[i].ToString());
        //        }
        //    }
        //}

        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateThread(int _thread_id, string _thread_title, bool _thread_isfocus, string _thread_logo, string _thread_rt, int _thread_rc, bool _status)
        {
            NewsThreadRow objrow;
            using (MainDB objDb = new MainDB()) {
                objrow = objDb.NewsThreadCollection.GetByPrimaryKey(_thread_id);
            }
            if (objrow != null) {
                objrow.Title = _thread_title;
                objrow.Thread_isForcus = _thread_isfocus;
                objrow.Thread_RT = _thread_rt;
                objrow.Thread_RC = _thread_rc;
                if (_thread_logo != "" && _thread_logo != null)
                    objrow.Thread_Logo = _thread_logo;
                if (_status)
                    objrow.Status = 0;
                else
                    objrow.Status = 1;
                using (MainDB objDb = new MainDB()) {
                    DataTable table = objDb.StoredProcedures.vc_NewsThread_CheckExistingTitle(_thread_title, _thread_id);
                    if (table.Rows.Count == 0) {
                        objDb.NewsThreadCollection.Update(objrow);
                    }
                }
            }
        }


    }
}
