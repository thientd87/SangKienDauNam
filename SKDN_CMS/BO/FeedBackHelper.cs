using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Portal.Core.DAL;

namespace Portal.BO.Editoral.Feedback
{
    public class FeedBackHelper
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