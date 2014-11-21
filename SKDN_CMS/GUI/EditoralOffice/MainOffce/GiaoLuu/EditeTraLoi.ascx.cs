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
    public partial class EditeTraLoi : System.Web.UI.UserControl
    {
        public int questionID = 0;
        private Question obj;
        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (!IsPostBack)
            {
                try
                {
                    questionID = Convert.ToInt32(Request.QueryString["questionID"]);
                }
                catch (Exception)
                {
                }
                if (questionID <= 0)
                {
                    // insert
                }
                else
                {
                    obj = new Question();
                    obj.Question_ID = questionID;
                    obj = obj.SelectOne();
                    this.txtCauHoi.Text = obj.Question_Content;
                    this.NewsContent.Text = obj.Question_Answer;
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
            obj.Question_Content = txtCauHoi.Text;
            obj.Question_Answer = NewsContent.Text;
            obj.Status = QuestionStatus.DA_TRA_LOI;
            obj.Update();
            Response.Redirect(string.Format("/office/giao_luu_traloi.aspx?courseID={0}", obj.Sourse_ID));
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
            obj.Question_Content = txtCauHoi.Text;
            //obj.Question_Answer = "";
            obj.Status = QuestionStatus.DA_DIEU_PHOI;
            obj.Update();
            Response.Redirect(string.Format("/office/giao_luu_traloi.aspx?courseID={0}", obj.Sourse_ID));
        }
    }
}