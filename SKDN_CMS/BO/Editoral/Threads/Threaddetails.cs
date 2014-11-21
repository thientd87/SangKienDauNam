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
using DFISYS.Core.DAL;
namespace ThreadManagement.BO {

    public class Threaddetails {
        public Threaddetails() { }


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

        //public DataTable GetThreadDetails(int ThreadID) {
        //    DataTable __result;


        //    using (MainDB objdb = new MainDB()) {
        //        __result = objdb.StoredProcedures.GetThreadDetails(ThreadID);
        //    }
        //    return __result;
        //}

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

        //public void AddnewsThread(string _selected_id, int threadID, MainDB objDB) {
        //    if (_selected_id == null) { return; }
        //    if (_selected_id == "") { return; }
        //    string[] objTempSel = _selected_id.Split(',');

        //    ThreadDetailRow objrow = new ThreadDetailRow();
        //    DataTable checkExit;

        //    DataTable dtNewsInfo;
        //    DataTable dtThreadInfo;
        //    ThreadHelper objThread = new ThreadHelper();

        //    foreach (string temp in objTempSel) {
        //        checkExit = objDB.StoredProcedures.vc_ThreadDetail_CheckExistingNews_ID_Thread_ID(temp, threadID);
        //        if (checkExit.Rows.Count == 0) {
        //            objrow.Thread_ID = threadID;
        //            objrow.News_ID = temp.Trim();
        //            objDB.ThreadDetailCollection.Insert(objrow);

        //            dtThreadInfo = objThread.ThreadSelectOne(threadID);
        //            dtNewsInfo = DFISYS.BO.Editoral.Newsedit.NewsEditHelper.GetNewsById(temp.Trim());

        //            string strAction = "<b>" + dtNewsInfo.Rows[0]["News_Title"] + "</b> " + Portal.LogAction.LogAction_ThreadDetail_Add + "<b> " + dtThreadInfo.Rows[0]["Title"] + " </b>" + " bởi " + HttpContext.Current.User.Identity.Name;
        //            //Portal.LogAction.InsertMemCache(HttpContext.Current.User.Identity.Name, DateTime.Now, strAction, Portal.LogAction.LogType_Thread, Convert.ToInt64(threadID));
        //        }
        //    }
        //}

        //public void DelNewsThread(string _selected_id) {
        //    try {
        //        using (MainDB objdb = new MainDB()) {
        //            objdb.StoredProcedures.vc_Execute_Sql("Delete FROM ThreadDetail Where News_ID IN (" + _selected_id + ")");
        //        }
        //    }
        //    catch (Exception ex) {
        //        throw ex;
        //    }
        //}

        //public void DeleteThreadByNewsId(string news_id) {
        //    using (MainDB objdb = new MainDB()) {
        //        objdb.SelectQuery("Delete ThreadDetail Where News_ID = '" + news_id + "'");
        //    }
        //}

        //public DataTable GetThreadByNewsId(string strNews_Id) {
        //    using (MainDB objdb = new MainDB()) {
        //        return objdb.StoredProcedures.ThreadDetail_GetThreadDetailByNewsId(strNews_Id);
        //    }
        //}
        //public DataTable GetThreadDetailInfoByThreadDetailID(string ThreadDetailID) {
        //    using (MainDB objdb = new MainDB()) {
        //        return objdb.StoredProcedures.vc_Execute_Sql("Select * From ThreadDetail Where Threaddetails_ID = " + ThreadDetailID);
        //    }
        //}

    }
}
