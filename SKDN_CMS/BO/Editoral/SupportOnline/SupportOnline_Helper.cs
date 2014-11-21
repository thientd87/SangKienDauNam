using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DFISYS.Core.DAL;

namespace DFISYS.BO
{
    public class SupportOnline_Helper
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
        public static DataTable SelectSupportOnline()
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.SelectQuery("Select * from SupportOnline");
            }
            return dt;
        }

        public static DataTable SelectOneSupportOnline(string ID)
        {
            DataTable dt;
            using (MainDB db = new MainDB())
            {
                dt = db.SelectQuery("Select * from SupportOnline where ID ='"+ID+"'");
            }
            return dt;
        }
        public static void InsertSupportOnline(string FullName,string Yahoo,string Skype, string Mobile, string GroupName, int STT)
        {
            using(MainDB db = new MainDB())
            {
                db.StoredProcedures.InsertSupportOnline(FullName, Yahoo, Skype,Mobile,GroupName,STT);
            }
        }
        public static void UpdateSupportOnline(int ID, string FullName, string Yahoo, string Skype, string Mobile, string GroupName, int STT)
        {
            using (MainDB db = new MainDB())
            {
                db.StoredProcedures.UpdateSupportOnline(ID,FullName, Yahoo, Skype,Mobile,GroupName,STT);
            }
        }
        public static void DeleteSupportOnline(string ID)
        {
            using (MainDB db = new MainDB())
            {
                db.StoredProcedures.DeleteSupportOnline(ID);
            }
        }
        public static void ActiveSupportOnline(string ID)
        {
            using (MainDB db = new MainDB())
            {
                db.StoredProcedures.SetActiveSupportOnline(ID);
            }
        }
        public static void UnActiveSupportOnline(string ID)
        {
            using (MainDB db = new MainDB())
            {
                db.StoredProcedures.SetUnActiveSupportOnline(ID);
            }
        }
    }
}