using System.Data;
using DFISYS.Core.DAL;

namespace DFISYS.BO.Editoral.FeedbackManagement
{
    public class FeedbackHelper
    {
        public static DataTable SelectFeedBack()
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.SelectQuery("Select * from FeedBack order by ID DESC");
            }
            return dt;
        }
        public static void DeleteFeedback(string ID)
        {
            using (MainDB db = new MainDB())
            {
                db.AnotherNonQuery("delete from FeedBack where ID in(" + ID + ")");
            }
        }
    }
}