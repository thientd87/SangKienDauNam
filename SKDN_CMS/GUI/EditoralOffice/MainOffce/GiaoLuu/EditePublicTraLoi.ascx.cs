using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.CoreBO;
using DFISYS.CoreBO.Common;
using System.Globalization;

namespace DFISYS.GUI.EditoralOffice.MainOffce.GiaoLuu
{
    public partial class EditePublicTraLoi : System.Web.UI.UserControl
    {
        public int questionID = 0;
        public int courseID = 0;
        public int courseLogID = 0;
        private Question obj;
        private Course objCource;
        private CourseLog objLog;
        public string chude = "";
        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (!IsPostBack)
            {
                questionID = DFISYS.CoreBO.Common.function.Obj2Int(Request.QueryString["questionID"]);
                courseID = DFISYS.CoreBO.Common.function.Obj2Int(Request.QueryString["courseID"]);
                courseLogID = DFISYS.CoreBO.Common.function.Obj2Int(Request.QueryString["courseLogID"]);
                if (questionID > 0)
                {
                    //// by nội dung cũ vào, nếu chưa có nội dung
                    #region questionID>0
                    objLog = new CourseLog();
                    objLog = objLog.SelectOneByQuestionID(questionID);

                    obj = new Question();
                    obj.Question_ID = questionID;
                    obj = obj.SelectOne();


                    // GET vị trí duyệt nội dung
                    objCource = new Course();
                    objCource.Course_ID = obj.Sourse_ID;
                    objCource = objCource.SelectOne();
                    chude = objCource.Course_Title;
                    if (objCource.PublicOrder == CourseOrder.LEN_DAU)
                    {
                        this.NewsContent.Text += string.Format("<b>{0}</b> - <i>{1} tuổi</i> <br/><br/>", obj.User_Name, obj.User_Age);
                        this.NewsContent.Text += obj.Question_Content + "<br/><br/>";
                        this.NewsContent.Text += obj.Question_Answer + "<br/><br/>";
                        if ((objLog != null) && (objLog.Course_Content.Length > 0))
                        {
                            this.NewsContent.Text += objLog.Course_Content;
                        }
                    }
                    else
                    {
                        if ((objLog != null) && (objLog.Course_Content.Length > 0))
                        {
                            this.NewsContent.Text += objLog.Course_Content;
                        }
                        this.NewsContent.Text += string.Format("<b>{0}</b> - <i>{1} tuổi</i> <br/><br/>", obj.User_Name, obj.User_Age);
                        this.NewsContent.Text += obj.Question_Content + "<br/><br/>";
                        this.NewsContent.Text += obj.Question_Answer + "<br/><br/>";
                    }
                    #endregion
                }
                else
                {
                    courseID = DFISYS.CoreBO.Common.function.Obj2Int(Request.QueryString["courseID"]);
                    if (courseID > 0)
                    {
                        //// by nội dung cũ vào, nếu chưa có nội dung
                        objLog = new CourseLog();
                        objLog = objLog.SelectOneByCourseID(courseID);

                        // GET vị trí duyệt nội dung
                        objCource = new Course();
                        objCource.Course_ID = courseID;
                        objCource = objCource.SelectOne();
                        if ((objLog != null) && (objLog.Course_Content.Length > 0))
                        {
                            this.NewsContent.Text += objLog.Course_Content;
                        }
                    }
                    else
                    {
                        courseLogID = DFISYS.CoreBO.Common.function.Obj2Int(Request.QueryString["courseLogID"]);
                        //// by nội dung cũ vào, nếu chưa có nội dung
                        objLog = new CourseLog();
                        objLog.CourseLog_ID = courseLogID;
                        objLog = objLog.SelectOne();

                        // GET vị trí duyệt nội dung
                        objCource = new Course();
                        objCource.Course_ID = courseID;
                        objCource = objCource.SelectOne();
                        if ((objLog != null) && (objLog.Course_Content.Length > 0))
                        {
                            this.NewsContent.Text += objLog.Course_Content;
                        }
                    }
                }
               
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                questionID = Convert.ToInt32(Request.QueryString["questionID"]);
            }
            catch (Exception)
            {
            }
            obj = new Question();
            obj.Question_ID = questionID;
            obj = obj.SelectOne();
            obj.Status = QuestionStatus.DA_DUYET;
            obj.Update();


            objLog = new CourseLog();
            objLog.Course_Content = NewsContent.Text;
            objLog.CourseLog_Date = DateTime.Now;
            objLog.Course_ID = obj.Sourse_ID;   
            objLog.Insert();

            Response.Redirect(string.Format("/office/giao_luu_duyet.aspx?courseID={0}", obj.Sourse_ID));
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                questionID = Convert.ToInt32(Request.QueryString["questionID"]);
            }
            catch (Exception)
            {
            }
            obj = new Question();
            obj.Question_ID = questionID;
            obj = obj.SelectOne();
            Response.Redirect(string.Format("/office/giao_luu_duyet.aspx?courseID={0}", obj.Sourse_ID));
        }
    }
}