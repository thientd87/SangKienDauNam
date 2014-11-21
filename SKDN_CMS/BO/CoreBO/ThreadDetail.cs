using System;
using System.Collections.Generic;
using System.Text;
using DFISYS.Core.DAL;
using System.Data;
using System.ComponentModel;

namespace DFISYS.CoreBO.Threads {
    public class Threaddetails {
        public Threaddetails() { }
        #region GetThreadDetails Store
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable GetThreadDetails(string strWhere, int PageSize, int StartRow) {
            DataTable objresult;
            //lay thong tin ve menh de where de loc thong tin
            if (strWhere == null)
                strWhere = "";
            int intPageNum = StartRow / PageSize + 1;
            //lay duoc du lieu cua tat ca nhung thang co trang thai la status can lay
            using (MainDB objdb = new MainDB()) {
                //objresult = objdb.ThreadDetailsCollection.GetPageAsDataTable(intPageNum, PageSize, strWhere, " Threaddetails_ID DESC"); ;
                objresult = objdb.StoredProcedures.vc_ThreadDetails_SelectList(strWhere, StartRow, PageSize);
            }
            return objresult;
        }
        public DataTable GetThreadDetails_Old(string strWhere, int PageSize, int StartRow) {
            DataTable objresult;
            //lay thong tin ve menh de where de loc thong tin
            if (strWhere == null)
                strWhere = "";
            int intPageNum = StartRow / PageSize + 1;
            //lay duoc du lieu cua tat ca nhung thang co trang thai la status can lay
            using (MainDB objdb = new MainDB()) {
                objresult = objdb.ThreadDetailsCollection.GetPageAsDataTable(intPageNum, PageSize, strWhere, " Threaddetails_ID DESC"); ;
            }
            return objresult;
        }
        #endregion
      
        #region GetThreadRowsCount
        //public int GetThreadRowsCount(string strWhere) {
        //    if (strWhere == null)
        //        strWhere = "";
        //    DataTable objresult;
        //    using (MainDB objdb = new MainDB()) {
        //        //objresult = objdb.ThreadDetailsCollection.GetAsDataTable(strWhere, ""); ;
        //        objresult = objdb.StoredProcedures.vc_ThreadDetails_SelectList_Count(strWhere);
        //    }
        //    if (objresult.Rows.Count == 0) return 0;
        //    else return Convert.ToInt32(objresult.Rows[0][0]);
        //    //return objresult.Rows.Count;
        //}
        public int GetThreadRowsCount_Old(string strWhere) {
            if (strWhere == null)
                strWhere = "";
            DataTable objresult;
            using (MainDB objdb = new MainDB()) {
                objresult = objdb.ThreadDetailsCollection.GetAsDataTable(strWhere, ""); ;
            }
            return objresult.Rows.Count;
        }
        #endregion

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void AddnewsThread(string _selected_id, int threadID)
        {
            if (_selected_id == null) { return; }
            if (_selected_id == "") { return; }
            string[] objTempSel = _selected_id.Split(',');
            MainDB objDB = new MainDB();
            ThreadDetailRow objrow = new ThreadDetailRow();
            DataTable checkExit;
            foreach (string temp in objTempSel)
            {
                checkExit = objDB.StoredProcedures.vc_ThreadDetail_CheckExistingNews_ID_Thread_ID(temp, threadID);
                if (checkExit.Rows.Count == 0)
                {
                    objrow.Thread_ID = threadID;
                    objrow.News_ID = temp.Trim();
                    objDB.ThreadDetailCollection.Insert(objrow);
                }
            }
        }

        //public void AddThreadToNews(string _selected_id, string threadIDs)
        //{
        //    if (_selected_id == null) { return; }
        //    if (_selected_id == "") { return; }
        //    string[] objTempSel = threadIDs.Split(',');
        //    int oneThread = 0; 
        //    MainDB objDB = new MainDB();
        //    ThreadDetailRow objrow = new ThreadDetailRow();
        //    DataTable checkExit;
        //    foreach (string temp in objTempSel)
        //    {
        //        oneThread = Convert.ToInt32(temp);
        //        checkExit = objDB.StoredProcedures.vc_ThreadDetail_CheckExistingNews_ID_Thread_ID(_selected_id, oneThread);
        //        if (checkExit.Rows.Count == 0)
        //        {
        //            objrow.Thread_ID = oneThread;
        //            objrow.News_ID = _selected_id.Trim();
        //            objDB.ThreadDetailCollection.Insert(objrow);
        //        }
        //    }
        //}

        public void AddnewsThread(string _selected_id, int threadID, MainDB objDB)
        {
            if (_selected_id == null) { return; }
            if (_selected_id == "") { return; }
            string[] objTempSel = _selected_id.Split(',');

            ThreadDetailRow objrow = new ThreadDetailRow();
            DataTable checkExit;
            foreach (string temp in objTempSel)
            {
                checkExit = objDB.StoredProcedures.vc_ThreadDetail_CheckExistingNews_ID_Thread_ID(temp, threadID);
                if (checkExit.Rows.Count == 0)
                {
                    objrow.Thread_ID = threadID;
                    objrow.News_ID = temp.Trim();
                    objDB.ThreadDetailCollection.Insert(objrow);
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DelNewsThread(string _selected_id) {
            if (_selected_id.IndexOf(",") <= 0) {
                int intid = Convert.ToInt32(_selected_id);
                using (MainDB objDB = new MainDB()) {
                    objDB.ThreadDetailCollection.DeleteByPrimaryKey(intid);
                }
            }
            else {
                string[] objTempSel = _selected_id.Split(',');
                try {
                    using (MainDB objDB = new MainDB()) {
                        objDB.ThreadDetailCollection.Delete(" Threaddetails_ID in (" + _selected_id + ")");
                    }
                }
                catch (Exception ex) { throw ex; }
            }
        }

        public void DeleteThreadByNewsId(string news_id) {
            using (MainDB objdb = new MainDB()) {
                objdb.SelectQuery("Delete ThreadDetail Where News_ID = '" + news_id + "'");
            }
        }

        //public DataTable GetThreadByNewsId(string strNews_Id) {
        //    using (MainDB objdb = new MainDB()) {
        //        return objdb.StoredProcedures.ThreadDetail_GetThreadDetailByNewsId(strNews_Id);
        //    }
        //}

        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateThread(int _thread_id, string _thread_title, bool _thread_isfocus, string _thread_logo) {
            NewsThreadRow objrow;
            using (MainDB objDb = new MainDB()) {
                objrow = objDb.NewsThreadCollection.GetByPrimaryKey(_thread_id);
            }
            if (objrow != null) {
                objrow.Title = _thread_title;
                objrow.Thread_isForcus = _thread_isfocus;
                if (_thread_logo != "" && _thread_logo != null)
                    objrow.Thread_Logo = _thread_logo;
                using (MainDB objDb = new MainDB()) {
                    DataTable table = objDb.StoredProcedures.vc_NewsThread_CheckExistingTitle(_thread_title, _thread_id);
                    if (table.Rows.Count == 0) {
                        objDb.NewsThreadCollection.Update(objrow);
                    }
                }
            }
        }


        public void AddThreadToNews(string _selected_id, string threadIDs)
        {
            if (_selected_id == null) { return; }
            if (_selected_id == "") { return; }
            string[] objTempSel = threadIDs.Split(',');
            int oneThread = 0;
            MainDB objDB = new MainDB();
            ThreadDetailRow objrow = new ThreadDetailRow();
            DataTable checkExit;
            foreach (string temp in objTempSel)
            {
                oneThread = Convert.ToInt32(temp);
                checkExit = objDB.StoredProcedures.vc_ThreadDetail_CheckExistingNews_ID_Thread_ID(_selected_id, oneThread);
                if (checkExit.Rows.Count == 0)
                {
                    objrow.Thread_ID = oneThread;
                    objrow.News_ID = _selected_id.Trim();
                    objDB.ThreadDetailCollection.Insert(objrow);
                }
            }
        }
    }
}
