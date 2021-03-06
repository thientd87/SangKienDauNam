using System;
using System.Collections.Generic;
using System.Text;
using DFISYS.Core.DAL;
using System.ComponentModel;
using System.Data;
using System.Web;

namespace DFISYS.CoreBO.Threads {
    public class ThreadHelper {
        public ThreadHelper() { }

        #region GetThreadlist dung store
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable GetThreadlist(string strWhere, int PageSize, int StartRow) {
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

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DelThread(string _selected_id) {
            if (_selected_id.IndexOf(",") <= 0) {
                //DeleteFile(_selected_id);
                try {
                    int intid = Convert.ToInt32(_selected_id);
                    using (MainDB objDB = new MainDB()) {

                        objDB.NewsThreadCollection.DeleteByPrimaryKey(intid);

                    }
                }
                catch { }
            }
            else {
                string[] objTempSel = _selected_id.Split(',');
                //foreach (string temp in objTempSel)
                //{
                //    DeleteFile(temp.Trim());
                //}
                try {
                    using (MainDB objDB = new MainDB()) {
                        objDB.NewsThreadCollection.Delete("Thread_ID in (" + _selected_id + ")");
                    }
                }
                catch { }
            }
        }

        //[DataObjectMethod(DataObjectMethodType.Insert)]
        //public void createThread(string _thread_title, bool _thread_isfocus, string _thread_logo) {
        //    NewsThreadRow objrow = new NewsThreadRow();
        //    objrow.Title = _thread_title;
        //    objrow.Thread_isForcus = _thread_isfocus;
        //    if (_thread_logo != "" && _thread_logo != null)
        //        objrow.Thread_Logo = _thread_logo;
        //    using (MainDB objdb = new MainDB()) {
        //        DataTable table = objdb.StoredProcedures.vc_NewsThread_CheckExistingTitle(_thread_title, 0);
        //        if (table.Rows.Count == 0) {
        //            objdb.NewsThreadCollection.Insert(objrow);
        //        }
        //    }
        //}
        public static DataTable SelectThreadByNewsID(Int64 NewsID)
        {
            DataTable result = null;

            using (MainDB db = new MainDB())
            {
                result = db.StoredProcedures.vf_SelectThreadByNewsID(NewsID);
            }
            return result;
        }
       
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

        //public DataTable ThreadSelectOne(Int32 Thread_ID) {
        //    using (MainDB db = new MainDB())
        //        return db.StoredProcedures_Family.vc_NewsThread_SelectOne(Thread_ID);
        //}

        //public void ThreadInsert(String Title, Boolean IsFocus, HttpPostedFile _File, String RC, String RT) {

        //    string _sFileName = "";
        //    if (_File.FileName != "") {
        //        Random r = new Random();

        //        _sFileName = r.Next().ToString() + System.IO.Path.GetFileName(_File.FileName);
        //        string strolder = HttpContext.Current.Request.PhysicalApplicationPath.Replace(@"\", "/") + "Images/Uploaded/Share/thread/";

        //        if (!System.IO.Directory.Exists(strolder))
        //            System.IO.Directory.CreateDirectory(strolder);

        //        try {
        //            if (FileHelper.isPicture(_sFileName))
        //                _File.SaveAs(strolder + _sFileName);

        //        }
        //        catch { }
        //    }

        //    if (_sFileName != "")
        //        _sFileName = "Images/Uploaded/Share/thread/" + _sFileName;

        //    using (MainDB db = new MainDB())
        //        db.StoredProcedures_Family.vc_NewsThread_Insert(Title, IsFocus, _sFileName, RT, RC);
        //}

        //public void ThreadUpdate(String Title, Boolean IsFocus, HttpPostedFile _File, String RC, String RT, Int32 Thread_ID) {
        //    string _sFileName = "";
        //    if (_File.FileName != "") {
        //        Random r = new Random();

        //        _sFileName = r.Next().ToString() + System.IO.Path.GetFileName(_File.FileName);
        //        string strolder = HttpContext.Current.Request.PhysicalApplicationPath.Replace(@"\", "/") + "Images/Uploaded/Share/thread/";

        //        if (!System.IO.Directory.Exists(strolder))
        //            System.IO.Directory.CreateDirectory(strolder);

        //        try {
        //            if (FileHelper.isPicture(_sFileName))
        //                _File.SaveAs(strolder + _sFileName);

        //        }
        //        catch { }
        //    }

        //    if (_sFileName != "")
        //        _sFileName = "Images/Uploaded/Share/thread/" + _sFileName;

        //    using (MainDB db = new MainDB())
        //        db.StoredProcedures_Family.vc_NewsThread_Update(Title, IsFocus, _sFileName, RT, RC, Thread_ID);
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

        //public DataTable GetNewsListForThread(Int32 CatID, string condition) {
        //    if (CatID != 0)
        //        condition += " AND Cat_ID = " + CatID;

        //    using (MainDB db = new MainDB()) {
        //        return db.StoredProcedures.vc_Execute_Sql("SELECT TOP (1000) * FROM News WHERE News_Status = 3 AND News_PublishDate < getdate() " + condition + " ORDER BY News_PublishDate DESC");
        //    }
        //}

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
    }
}
