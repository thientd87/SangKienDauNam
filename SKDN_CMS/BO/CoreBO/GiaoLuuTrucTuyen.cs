using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DFISYS.User.Db;

namespace DFISYS.BO.CoreBO
{
    public class Course
    {
        public int Course_ID { get; set; }
        public DateTime Course_Date { get; set; }
        public string Course_Title { get; set; }
        public string Course_InitContent { get; set; }
        public string Course_SubTitle { get; set; }
        public string Course_Content { get; set; }
        public string Course_Image { get; set; }
        public string Course_ImageNote { get; set; }
        public bool isActive { get; set; }
        public Int64 News_ID { get; set; }
        public int Status { get; set; }
        public int CountQuestion { get; set; }
        public int PublicOrder { get; set; }
        public Course(int course_id, DateTime course_date, string course_title, string course_initcontent, string course_subtitle, string course_content, string course_image, string course_imagenote, bool isactive, Int64 news_id, int status, int publicorder)
        {
            this.Course_ID = course_id;
            this.Course_Date = course_date;
            this.Course_Title = course_title;
            this.Course_InitContent = course_initcontent;
            this.Course_SubTitle = course_subtitle;
            this.Course_Content = course_content;
            this.Course_Image = course_image;
            this.Course_ImageNote = course_imagenote;
            this.isActive = isactive;
            this.News_ID = news_id;
            this.Status = status;
            this.PublicOrder = publicorder;
        }
        public Course()
        { }

        ///  MapObject Course-------------------------------------------------------
        private Course MapObject(DataRow row)
        {
            return new Course()
            {
                Course_ID = row["Course_ID"] != DBNull.Value ? Convert.ToInt32(row["Course_ID"]) : 0,
                Course_Date = row["Course_Date"] != DBNull.Value ? Convert.ToDateTime(row["Course_Date"]) : DateTime.Now,
                Course_Title = row["Course_Title"] != DBNull.Value ? row["Course_Title"].ToString() : string.Empty,
                Course_InitContent = row["Course_InitContent"] != DBNull.Value ? row["Course_InitContent"].ToString() : string.Empty,
                Course_SubTitle = row["Course_SubTitle"] != DBNull.Value ? row["Course_SubTitle"].ToString() : string.Empty,
                Course_Content = row["Course_Content"] != DBNull.Value ? row["Course_Content"].ToString() : string.Empty,
                Course_Image = row["Course_Image"] != DBNull.Value ? row["Course_Image"].ToString() : string.Empty,
                Course_ImageNote = row["Course_ImageNote"] != DBNull.Value ? row["Course_ImageNote"].ToString() : string.Empty,
                isActive = row["isActive"] != DBNull.Value && Convert.ToBoolean(row["isActive"]),
                News_ID = row["News_ID"] != DBNull.Value ? Convert.ToInt64(row["News_ID"]) : 0,
                Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0,
                PublicOrder = row["PublicOrder"] != DBNull.Value ? Convert.ToInt32(row["PublicOrder"]) : 0
            };
        }
        ///  SelectAllSearch Course-------------------------------------------------------
        public List<Course> SelectAllSearch(int pageSize, int pageIndex, string keyword)
        {
            List<Course> ls = new List<Course>();
            keyword = keyword.Replace(" ", "+");
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_Course_Search", new object[] { pageIndex, pageSize, keyword }, new string[] { "@pageIndex", "@pageSize", "@keyword" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        public int SelectAllCount(string keyword)
        {
            keyword = keyword.Replace(" ", "+");
            DataTable tbl = null;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_Course_SearchCount",
                     new object[] { keyword },
                     new string[] { "@keyword" },
                     true);
            }
            return tbl != null ? Convert.ToInt32(tbl.Rows[0][0]) : 0;
        }
        ///  Insert Course-------------------------------------------------------
        public void Insert()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_Course_Insert", new object[] { Course_Date, Course_Title, Course_InitContent, Course_SubTitle, Course_Content, Course_Image, Course_ImageNote, isActive, News_ID, Status, PublicOrder }, new string[] { "@Course_Date", "@Course_Title", "@Course_InitContent", "@Course_SubTitle", "@Course_Content", "@Course_Image", "@Course_ImageNote", "@isActive", "@News_ID", "@Status", "@PublicOrder" }, false);
            }
        }
        ///  Update Course-------------------------------------------------------
        public void Update()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_Course_Update", new object[] { Course_ID, Course_Date, Course_Title, Course_InitContent, Course_SubTitle, Course_Content, Course_Image, Course_ImageNote, isActive, News_ID, Status, PublicOrder }, new string[] { "@Course_ID", "@Course_Date", "@Course_Title", "@Course_InitContent", "@Course_SubTitle", "@Course_Content", "@Course_Image", "@Course_ImageNote", "@isActive", "@News_ID", "@Status", "@PublicOrder" }, false);
            }
        }
        ///  Delete Course-------------------------------------------------------
        public void Delete()
        {
            if (Course_ID > 0)
            {
                using (MainDB db = new MainDB())
                {
                    db.CallStoredProcedure("CMS_Course_Delete", new object[] { Course_ID }, new string[] { "@Course_ID" }, false);
                }
            }
        }
        ///  SelectOne Course-------------------------------------------------------
        public Course SelectOne()
        {
            DataTable tbl;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_Course_SelectOne", new object[] { Course_ID }, new string[] { "@Course_ID" }, true);
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    return MapObject(tbl.Rows[0]);
                }
            }
            return null;
        }
    }
    public static class CourseOrder
    {
        public const int LEN_DAU = 0;
        public const int XUONG_CUOI = 1;
    }
 
    public class CourseLog
    {
        public int CourseLog_ID { get; set; }
        public DateTime CourseLog_Date { get; set; }
        public int Course_ID { get; set; }
        public string Course_Content { get; set; }
        public CourseLog(int courselog_id, DateTime courselog_date, int course_id, string course_content)
        {
            this.CourseLog_ID = courselog_id;
            this.CourseLog_Date = courselog_date;
            this.Course_ID = course_id;
            this.Course_Content = course_content;
        }
        public CourseLog()
        { }
        ///  MapObject CourseLog-------------------------------------------------------
        private CourseLog MapObject(DataRow row)
        {
            return new CourseLog()
            {
                CourseLog_ID = row["CourseLog_ID"] != DBNull.Value ? Convert.ToInt32(row["CourseLog_ID"]) : 0,
                CourseLog_Date = row["CourseLog_Date"] != DBNull.Value ? Convert.ToDateTime(row["CourseLog_Date"]) : DateTime.Now,
                Course_ID = row["Course_ID"] != DBNull.Value ? Convert.ToInt32(row["Course_ID"]) : 0,
                Course_Content = row["Course_Content"] != DBNull.Value ? row["Course_Content"].ToString() : string.Empty
            };
        }
        ///  SelectAllSearch CourseLog-------------------------------------------------------
        public List<CourseLog> SelectAllSearch(int pageSize, int pageIndex, string keyword)
        {
            List<CourseLog> ls = new List<CourseLog>();
            keyword = keyword.Replace(" ", "+");
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_CourseLog_Search", new object[] { pageIndex, pageSize, keyword }, new string[] { "@pageIndex", "@pageSize", "@keyword" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        public List<CourseLog> SelectAllByCourseID(int courseID)
        {
            List<CourseLog> ls = new List<CourseLog>();
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_CourseLog_SelectAllByCourseID", new object[] { courseID }, new string[] { "@Course_ID" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        ///  Insert CourseLog-------------------------------------------------------
        public void Insert()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_CourseLog_Insert", new object[] { CourseLog_Date, Course_ID, Course_Content }, new string[] { "@CourseLog_Date", "@Course_ID", "@Course_Content" }, false);
            }
        }
        ///  Update CourseLog-------------------------------------------------------
        public void Update()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_CourseLog_Update", new object[] { CourseLog_ID, CourseLog_Date, Course_ID, Course_Content }, new string[] { "@CourseLog_ID", "@CourseLog_Date", "@Course_ID", "@Course_Content" }, false);
            }
        }
        ///  Delete CourseLog-------------------------------------------------------
        public void Delete()
        {
            if (CourseLog_ID > 0)
            {
                using (MainDB db = new MainDB())
                {
                    db.CallStoredProcedure("CMS_CourseLog_Delete", new object[] { CourseLog_ID }, new string[] { "@CourseLog_ID" }, false);
                }
            }
        }
        ///  SelectOne CourseLog-------------------------------------------------------
        public CourseLog SelectOne()
        {
            DataTable tbl;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_CourseLog_SelectOne", new object[] { CourseLog_ID }, new string[] { "@CourseLog_ID" }, true);
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    return MapObject(tbl.Rows[0]);
                }
            }
            return null;
        }
        //[CMS_CourseLog_SelectOneByQuestionID]
        public CourseLog SelectOneByQuestionID(int questionID)
        {
            DataTable tbl;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_CourseLog_SelectOneByQuestionID", new object[] { questionID }, new string[] { "@Question_ID" }, true);
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    return MapObject(tbl.Rows[0]);
                }
            }
            return null;
        }
        public CourseLog SelectOneByCourseID(int courseID)
        {
            DataTable tbl;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_CourseLog_SelectOneByCourseID", new object[] { courseID }, new string[] { "@Course_ID" }, true);
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    return MapObject(tbl.Rows[0]);
                }
            }
            return null;
        }
    }
    public class ChannelResponse
    {
        public int ChannelResponse_ID { get; set; }
        public int Sourse_ID { get; set; }
        public string UserID { get; set; }
        public string ChannelResponse_NameManager { get; set; }
        public bool IsActive { get; set; }
        public ChannelResponse(int channelresponse_id, int sourse_id, string userid, string channelresponse_namemanager, bool isactive)
        {
            this.ChannelResponse_ID = channelresponse_id;
            this.Sourse_ID = sourse_id;
            this.UserID = userid;
            this.ChannelResponse_NameManager = channelresponse_namemanager;
            this.IsActive = isactive;
        }
        public ChannelResponse()
        { }
        ///  MapObject ChannelResponse-------------------------------------------------------
        private ChannelResponse MapObject(DataRow row)
        {
            return new ChannelResponse()
            {
                ChannelResponse_ID = row["ChannelResponse_ID"] != DBNull.Value ? Convert.ToInt32(row["ChannelResponse_ID"]) : 0,
                Sourse_ID = row["Sourse_ID"] != DBNull.Value ? Convert.ToInt32(row["Sourse_ID"]) : 0,
                UserID = row["UserID"] != DBNull.Value ? row["UserID"].ToString() : string.Empty,
                ChannelResponse_NameManager = row["ChannelResponse_NameManager"] != DBNull.Value ? row["ChannelResponse_NameManager"].ToString() : string.Empty,
                IsActive = row["IsActive"] != DBNull.Value && Convert.ToBoolean(row["IsActive"])
            };
        }
        ///  SelectAllSearch ChannelResponse-------------------------------------------------------
        public List<ChannelResponse> SelectAllSearch(int pageSize, int pageIndex, string keyword)
        {
            List<ChannelResponse> ls = new List<ChannelResponse>();
            keyword = keyword.Replace(" ", "+");
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_ChannelResponse_Search", new object[] { pageIndex, pageSize, keyword }, new string[] { "@pageIndex", "@pageSize", "@keyword" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        ///  Insert ChannelResponse-------------------------------------------------------
        public void Insert()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_ChannelResponse_Insert", new object[] { Sourse_ID, UserID, ChannelResponse_NameManager, IsActive }, new string[] { "@Sourse_ID", "@UserID", "@ChannelResponse_NameManager", "@IsActive" }, false);
            }
        }
        ///  Update ChannelResponse-------------------------------------------------------
        public void Update()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_ChannelResponse_Update", new object[] { ChannelResponse_ID, Sourse_ID, UserID, ChannelResponse_NameManager, IsActive }, new string[] { "@ChannelResponse_ID", "@Sourse_ID", "@UserID", "@ChannelResponse_NameManager", "@IsActive" }, false);
            }
        }
        ///  Delete ChannelResponse-------------------------------------------------------
        public void Delete()
        {
            if (ChannelResponse_ID > 0)
            {
                using (MainDB db = new MainDB())
                {
                    db.CallStoredProcedure("CMS_ChannelResponse_Delete", new object[] { ChannelResponse_ID }, new string[] { "@ChannelResponse_ID" }, false);
                }
            }
        }
        ///  SelectOne ChannelResponse-------------------------------------------------------
        public ChannelResponse SelectOne()
        {
            DataTable tbl;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_ChannelResponse_SelectOne", new object[] { ChannelResponse_ID }, new string[] { "@ChannelResponse_ID" }, true);
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    return MapObject(tbl.Rows[0]);
                }
            }
            return null;
        }

        ///  SelectAllSearch ChannelResponse-------------------------------------------------------
        public List<ChannelResponse> SelectAllBySourseID(int sourseID)
        {
            List<ChannelResponse> ls = new List<ChannelResponse>();
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_ChannelResponse_SelectAllBySourseID", new object[] { sourseID }, new string[] { "@sourse_ID" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        ///  SelectAllSearch ChannelResponse-------------------------------------------------------
        public List<ChannelResponse> SelectAllBySourseIDActive(int sourseID)
        {
            List<ChannelResponse> ls = new List<ChannelResponse>();
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_ChannelResponse_SelectAllBySourseIDActive", new object[] { sourseID }, new string[] { "@sourse_ID" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }

    }
    public class Question
    {
        public int Question_ID { get; set; }
        public int Sourse_ID { get; set; }
        public int Channel_ID { get; set; }
        public string Question_Content { get; set; }
        public string Question_Answer { get; set; }
        public DateTime Question_Time { get; set; }
        public string User_Name { get; set; }
        public int User_Age { get; set; }
        public int User_Sex { get; set; }
        public string User_Address { get; set; }
        public string User_Mobile { get; set; }
        public string User_Email { get; set; }
        public string User_Job { get; set; }
        public int Status { get; set; }
        public Question(int question_id, int sourse_id, int channel_id, string question_content, string question_answer, DateTime question_time, string user_name, int user_age, int user_sex, string user_address, string user_mobile, string user_email, string user_job, int status)
        {
            this.Question_ID = question_id;
            this.Sourse_ID = sourse_id;
            this.Channel_ID = channel_id;
            this.Question_Content = question_content;
            this.Question_Answer = question_answer;
            this.Question_Time = question_time;
            this.User_Name = user_name;
            this.User_Age = user_age;
            this.User_Sex = user_sex;
            this.User_Address = user_address;
            this.User_Mobile = user_mobile;
            this.User_Email = user_email;
            this.User_Job = user_job;
            this.Status = status;
        }
        public Question()
        { }
        ///  MapObject Question-------------------------------------------------------
        private Question MapObject(DataRow row)
        {
            return new Question()
            {
                Question_ID = row["Question_ID"] != DBNull.Value ? Convert.ToInt32(row["Question_ID"]) : 0,
                Sourse_ID = row["Sourse_ID"] != DBNull.Value ? Convert.ToInt32(row["Sourse_ID"]) : 0,
                Channel_ID = row["Channel_ID"] != DBNull.Value ? Convert.ToInt32(row["Channel_ID"]) : 0,
                Question_Content = row["Question_Content"] != DBNull.Value ? row["Question_Content"].ToString() : string.Empty,
                Question_Answer = row["Question_Answer"] != DBNull.Value ? row["Question_Answer"].ToString() : string.Empty,
                Question_Time = row["Question_Time"] != DBNull.Value ? Convert.ToDateTime(row["Question_Time"]) : DateTime.Now,
                User_Name = row["User_Name"] != DBNull.Value ? row["User_Name"].ToString() : string.Empty,
                User_Age = row["User_Age"] != DBNull.Value ? Convert.ToInt32(row["User_Age"]) : 0,
                User_Sex = row["User_Sex"] != DBNull.Value ? Convert.ToInt32(row["User_Sex"]) : 0,
                User_Address = row["User_Address"] != DBNull.Value ? row["User_Address"].ToString() : string.Empty,
                User_Mobile = row["User_Mobile"] != DBNull.Value ? row["User_Mobile"].ToString() : string.Empty,
                User_Email = row["User_Email"] != DBNull.Value ? row["User_Email"].ToString() : string.Empty,
                User_Job = row["User_Job"] != DBNull.Value ? row["User_Job"].ToString() : string.Empty,
                Status = row["Status"] != DBNull.Value ? Convert.ToInt32(row["Status"]) : 0
            };
        }
        ///  SelectAllSearch Question-------------------------------------------------------
        public List<Question> SelectAllSearch(int pageSize, int pageIndex, string keyword, int idCourse)
        {
            List<Question> ls = new List<Question>();
            keyword = keyword.Replace(" ", "+");
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_Question_Search", new object[] { pageIndex, pageSize, keyword, idCourse }, new string[] { "@pageIndex", "@pageSize", "@keyword", "@courseID" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        
        public int SelectAllCount(string keyword, int courseID)
        {
            keyword = keyword.Replace(" ", "+");
            DataTable tbl = null;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_Question_SearchCount",
                     new object[] { keyword, courseID },
                     new string[] { "@keyword", "@courseID"},
                     true);
            }
            return tbl != null ? Convert.ToInt32(tbl.Rows[0][0]) : 0;
        }



        public List<Question> SelectAllStatusSearch(int pageSize, int pageIndex, string keyword, int idCourse, int status)
        {
            List<Question> ls = new List<Question>();
            keyword = keyword.Replace(" ", "+");
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_Question_Search_Status", new object[] { pageIndex, pageSize, keyword, idCourse, status }, new string[] { "@pageIndex", "@pageSize", "@keyword", "@courseID", "@status" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }

        public int SelectAllStatusCount(string keyword, int courseID, int status)
        {
            keyword = keyword.Replace(" ", "+");
            DataTable tbl = null;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_Question_SearchStatusCount",
                     new object[] { keyword, courseID, status },
                     new string[] { "@keyword", "@courseID", "@status" },
                     true);
            }
            return tbl != null ? Convert.ToInt32(tbl.Rows[0][0]) : 0;
        }


        public List<Question> SelectAllSearchByUser(int pageSize, int pageIndex, string keyword, int idCourse, string user)
        {
            List<Question> ls = new List<Question>();
            using (MainDB db = new MainDB())
            {
                DataTable dt = db.CallStoredProcedure("CMS_Question_SearchByUser", new object[] { pageIndex, pageSize, keyword, idCourse, user }, new string[] { "@pageIndex", "@pageSize", "@keyword", "@courseID", "@user" }, true);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(MapObject(dt.Rows[i]));
                }
            }
            return ls;
        }
        public int SelectAllCountByUser(string keyword, int courseID, string user)
        {
            DataTable tbl = null;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_Question_SearchByUserCount",
                     new object[] { keyword, courseID, user },
                     new string[] { "@keyword", "@courseID", "@user" },
                     true);
            }
            return tbl != null ? Convert.ToInt32(tbl.Rows[0][0]) : 0;
        }

        ///  Insert Question-------------------------------------------------------
        public void Insert()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_Question_Insert", new object[] { Sourse_ID, Channel_ID, Question_Content, Question_Answer, Question_Time, User_Name, User_Age, User_Sex, User_Address, User_Mobile, User_Email, User_Job, Status }, new string[] { "@Sourse_ID", "@Channel_ID", "@Question_Content", "@Question_Answer", "@Question_Time", "@User_Name", "@User_Age", "@User_Sex", "@User_Address", "@User_Mobile", "@User_Email", "@User_Job", "@Status" }, false);
            }
        }
        ///  Update Question-------------------------------------------------------
        public void Update()
        {
            using (MainDB db = new MainDB())
            {
                db.CallStoredProcedure("CMS_Question_Update", new object[] { Question_ID, Sourse_ID, Channel_ID, Question_Content, Question_Answer, Question_Time, User_Name, User_Age, User_Sex, User_Address, User_Mobile, User_Email, User_Job, Status }, new string[] { "@Question_ID", "@Sourse_ID", "@Channel_ID", "@Question_Content", "@Question_Answer", "@Question_Time", "@User_Name", "@User_Age", "@User_Sex", "@User_Address", "@User_Mobile", "@User_Email", "@User_Job", "@Status" }, false);
            }
        }
        ///  Delete Question-------------------------------------------------------
        public void Delete()
        {
            if (Question_ID > 0)
            {
                using (MainDB db = new MainDB())
                {
                    db.CallStoredProcedure("CMS_Question_Delete", new object[] { Question_ID }, new string[] { "@Question_ID" }, false);
                }
            }
        }
        ///  SelectOne Question-------------------------------------------------------
        public Question SelectOne()
        {
            DataTable tbl;
            using (MainDB db = new MainDB())
            {
                tbl = db.CallStoredProcedure("CMS_Question_SelectOne", new object[] { Question_ID }, new string[] { "@Question_ID" }, true);
                if (tbl != null && tbl.Rows.Count > 0)
                {
                    return MapObject(tbl.Rows[0]);
                }
            }
            return null;
        }
    }
    
    
    
    
    
    public static class QuestionStatus
    {
        public const int CHUA_DIEU_PHOI = 0;
        public const int DA_DIEU_PHOI = 1;
        public const int DA_TRA_LOI = 2;
        public const int DA_DUYET = 3;
        public const int XOA = 4;
    }
    public static class Sex
    {
        public const int NU = 0;
        public const int NAM = 1;
    }
}