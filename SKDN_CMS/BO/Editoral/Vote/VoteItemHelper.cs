using System;
using System.Data;
using DFISYS.Core.DAL;

namespace DFISYS.BO.Editoral.Vote
{
    public class VoteItemHelper
    {
        public VoteItemHelper() { }
        #region getVoteItem dung Store
        public DataTable  getVoteItem(int _vote_id)
        {
            DataTable objResult;
            using (MainDB objDb = new MainDB())
            {
                //objResult = objDb.VoteItemCollection.GetByVote_IDAsDataTable(_vote_id);
                String sql = "SELECT * FROM VoteItem WHERE Vote_ID = " + _vote_id;
                objResult = objDb.StoredProcedures.vc_Execute_Sql(sql);
            }
            DataTable objTemp = objResult.Clone();
            if (objResult.Rows.Count == 0)
            {
                DataRow objrow = objTemp.NewRow();
                objrow["VoteIt_ID"] = 0;
                objrow["VoteIt_Content"] = "Hiện mục không có dữ liệu, bạn phải thêm dữ liệu vào";
                objTemp.Rows.Add(objrow);
                objResult.Dispose();
                return objTemp;
            }
            return objResult;
        }
        public DataTable getVoteItem_Old(int _vote_id)
        {
            DataTable objResult;
            using (MainDB objDb = new MainDB())
            {
                objResult = objDb.VoteItemCollection.GetByVote_IDAsDataTable(_vote_id);
            }
            DataTable objTemp = objResult.Clone();
            if (objResult.Rows.Count == 0)
            {
                DataRow objrow = objTemp.NewRow();
                objrow["VoteIt_ID"] = 0;
                objrow["VoteIt_Content"] = "Hiện mục không có dữ liệu, bạn phải thêm dữ liệu vào";
                objTemp.Rows.Add(objrow);
                objResult.Dispose();
                return objTemp;
            }
            return objResult;
        }
        #endregion

        public void CreateVoteItem(int _vote_id, string _vote_item, Int32 _VotNum)
        {
            VoteItemRow objrow = new VoteItemRow();
            //objrow.VoteIt_ID = DateTime.Now.Millisecond;
            objrow.Vote_ID = _vote_id;
            objrow.VoteIt_Content = _vote_item;
            objrow.VoteIt_Rate = _VotNum;
            using (MainDB objDb = new MainDB())
            {
                objDb.VoteItemCollection.Insert(objrow);
            }
        }
        public void UpdateVoteItem(int _item_id, string _vote_item, Int32 _VotNum)
        {
            VoteItemRow objrow;
            using (MainDB objDb = new MainDB())
            {
                objrow = objDb.VoteItemCollection.GetByPrimaryKey(_item_id);
            }
            if (objrow != null)
            {
                objrow.VoteIt_Content = _vote_item;
                objrow.VoteIt_Rate = _VotNum;
                using (MainDB objDb = new MainDB())
                {
                    objDb.VoteItemCollection.Update(objrow);
                }
            }
        }
        public void DelItem(int _item_id)
        {
            using (MainDB objDb = new MainDB())
            {
                objDb.VoteItemCollection.DeleteByPrimaryKey(_item_id);
            }
        }
    }
}
