using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Web;
using DFISYS.Core.DAL;

namespace DFISYS.BO.Editoral.Vote {
    public class VoteHelper {
        public VoteHelper() { }

        #region getChildVote Dung Store
        private DataTable getChildVote(int _vote_parent) {
            DataTable objResult;
            using (MainDB objDb = new MainDB()) {
                //objResult = objDb.VoteCollection.GetAsDataTable("Vote_Parent=" + _vote_parent, "Vote_EndDate");
                objResult = objDb.StoredProcedures.vc_Execute_Sql("SELECT * FROM Vote WHERE Vote_Parent = " + _vote_parent + " ORDER BY Vote_EndDate ");

            }
            return objResult;
        }
        #endregion

        public void CreateVote(string _vote, string _start_date, string _end_date, int _parent, string _avatar, string _note, string _user, int _cat_id) {
            VoteRow objrow = new VoteRow();
            objrow.Vote_Title = _vote;
            if (_start_date != "" && _start_date != null)
                objrow.Vote_StartDate = Convert.ToDateTime(_start_date, new CultureInfo(1066));
            if (_end_date != "" && _end_date != null)
                objrow.Vote_EndDate = Convert.ToDateTime(_end_date, new CultureInfo(1066));
            objrow.Vote_Parent = _parent;
            //objrow.isShow = _is_show;
            objrow.Vote_Parent_Image = _avatar;
            objrow.Vote_InitContent = _note;
            objrow.UserID = _user;
            objrow.Cat_ID = _cat_id;
            using (MainDB objDb = new MainDB()) {
                objDb.VoteCollection.Insert(objrow);
            }
        }

        #region getVoteParent dung Store
        //Da chuyen sang store
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable getVoteParent() {
            DataTable objresult;
            string strSQL = "Select Vote_ID, Vote_Title from Vote WHERE (Vote_Parent is null OR Vote_Parent=0) AND UserID='" + HttpContext.Current.User.Identity.Name + "' ORDER BY Vote_EndDate DESC";
            using (MainDB objDb = new MainDB()) {
                //objresult = objDb.SelectQuery(strSQL);
                objresult = objDb.StoredProcedures.vc_Execute_Sql(strSQL);
            }
            DataRow objFirstRow = objresult.NewRow();
            objFirstRow["Vote_ID"] = 0;
            objFirstRow["Vote_Title"] = "------Chọn vote đại diện ---";
            objresult.Rows.InsertAt(objFirstRow, 0);
            return objresult;
        }


        //Da chuyen sang store
        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable getVoteParent(string Vote_ID) {
            DataTable objresult;
            string strSQL = "Select Vote_ID, Vote_Title from Vote ";
            if (Vote_ID == null || Vote_ID.Equals("")) {
                strSQL += "WHERE (Vote_Parent is null OR Vote_Parent=0) AND UserID = '" + HttpContext.Current.User.Identity.Name + "' ORDER BY Vote_EndDate DESC";
            }
            else {
                strSQL += " WHERE Vote_ID <> " + Vote_ID + " AND (Vote_Parent is null OR Vote_Parent=0) AND UserID = '" + HttpContext.Current.User.Identity.Name + "' ORDER BY Vote_EndDate DESC";
            }
            using (MainDB objDb = new MainDB()) {
                //objresult = objDb.SelectQuery(strSQL);
                objresult = objDb.StoredProcedures.vc_Execute_Sql(strSQL);
            }
            DataRow objFirstRow = objresult.NewRow();
            objFirstRow["Vote_ID"] = 0;
            objFirstRow["Vote_Title"] = "------Chọn vote đại diện ---";
            objresult.Rows.InsertAt(objFirstRow, 0);
            return objresult;
        }
        #endregion

        #region Chuyen getVoteRow dung Store
        public DataRow getVoteRow_Old(string _vote_id) {
            DataTable objTable;
            using (MainDB objDb = new MainDB()) {
                objTable = objDb.VoteCollection.GetAsDataTable("Vote_ID=" + _vote_id, "");
            }
            return objTable.Rows[0];
        }

        #endregion

        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateVote(int _vote_id, string _vote_title, string _vote_start, string _vote_end, int _parent, string _img, string _note, int _cat_id) {
            VoteRow objrow;
            using (MainDB objDb = new MainDB()) {
                objrow = objDb.VoteCollection.GetByPrimaryKey(_vote_id);
            }
            if (objrow != null) {
                objrow.Vote_Title = _vote_title;
                try {
                    objrow.Vote_StartDate = Convert.ToDateTime(_vote_start, new CultureInfo(1066));
                }
                catch { objrow.Vote_StartDate = DateTime.Now; }
                try {
                    objrow.Vote_EndDate = Convert.ToDateTime(_vote_end, new CultureInfo(1066));
                }
                catch { objrow.Vote_EndDate = DateTime.Now; }
                objrow.Vote_Parent = _parent;

                if (_img != null && _img != "") objrow.Vote_Parent_Image = _img;

                objrow.Vote_InitContent = _note;
                objrow.Cat_ID = _cat_id;

                using (MainDB objDb = new MainDB()) {
                    objDb.VoteCollection.Update(objrow);
                }
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DelVote(string _selected_id) {
            if (_selected_id.IndexOf(",") <= 0) {
                try {
                    int intid = Convert.ToInt32(_selected_id);
                    using (MainDB objDB = new MainDB()) {

                        objDB.VoteCollection.DeleteByPrimaryKey(intid);

                    }
                }
                catch { }
            }
            else {
                string[] objTempSel = _selected_id.Split(',');
                try {
                    using (MainDB objDB = new MainDB()) {
                        objDB.VoteCollection.Delete("Vote_ID in (" + _selected_id + ")");
                    }
                }
                catch { }
            }
        }

        public static void AssignNewsToVote(string _strNewsId, string _strVoteId) {
            using (MainDB objDB = new MainDB()) {
                objDB.StoredProcedures.vc_DeleteVote_Assigns_News_ByVote_ID(Convert.ToInt32(_strVoteId));
                objDB.StoredProcedures.vc_InsertVote_Assign(_strNewsId, Convert.ToInt32(_strVoteId), "", -1);
            }
        }

        public static DataTable GetVote_Assign__NewsId_ByVoteId(string _strVoteId) {
            DataTable dtResult;
            using (MainDB objDB = new MainDB()) {
                dtResult = objDB.StoredProcedures.vc_SelectVote_Assign__NewsId_ByVoteId(Convert.ToInt32(_strVoteId));
            }
            return dtResult;
        }

        public static void DeleteVote_Assigns_News_ByVote_ID(string _strVoteId) {
            using (MainDB objDB = new MainDB()) {
                objDB.StoredProcedures.vc_DeleteVote_Assigns_News_ByVote_ID(Convert.ToInt32(_strVoteId));
            }
        }

        public DataRow getVoteRow(string _vote_id) {
            DataTable objTable;
            using (MainDB objDb = new MainDB()) {
                objTable = objDb.StoredProcedures.vc_Vote_SelectList_Where("Vote_ID=" + _vote_id);
            }
            if (objTable.Rows.Count != 0)
                return objTable.Rows[0];
            else
                return objTable.NewRow();
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable GetVoteList(string strWhere, int PageSize, int StartRow) {
            using (MainDB db = new MainDB()) {
                return db.StoredProcedures.vc_Vote_SelectList(strWhere, StartRow, PageSize);
            }
        }

        public int GetVoteRowsCount(string strWhere) {
            using (MainDB db = new MainDB()) {
                DataTable table = db.StoredProcedures.vc_Vote_SelectList_Count(strWhere);
                if (table.Rows.Count == 0) { return 0; }
                else return Convert.ToInt32(table.Rows[0][0]);
            }
        }
    }
}
