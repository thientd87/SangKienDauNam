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
    public partial class EditeCourse : System.Web.UI.UserControl
    {
        public int courseID = 0;
        private Course objCourse;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                try
                {
                    courseID = Convert.ToInt32(Request.QueryString["courseId"]);
                }
                catch (Exception)
                {
                }
                if (courseID <= 0)
                {
                    // insert
                }
                else
                {
                    objCourse = new Course();
                    objCourse.Course_ID = courseID;
                    objCourse = objCourse.SelectOne();

                    this.txtFromDate.Text = objCourse.Course_Date.ToString("dd/MM/yyyy");
                    this.txtTitle.Text = objCourse.Course_Title;
                    this.txtSubTitle.Text = objCourse.Course_SubTitle;
                    this.txtInit.Text = objCourse.Course_InitContent;
                    this.txtSelectedFile.Text = objCourse.Course_Image;
                    this.txtImageTitle.Text = objCourse.Course_ImageNote;
                    this.NewsContent.Text = objCourse.Course_Content;
                    this.chkActive.Checked = objCourse.isActive;
                    this.txtNewID.Text = objCourse.News_ID.ToString();
                    this.ddlOrder.SelectedValue = objCourse.PublicOrder.ToString();
                    this.ddlStatus.SelectedValue = objCourse.Status.ToString();                    
                }
            }
        }

        

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                courseID = Convert.ToInt32(Request.QueryString["courseId"]);
            }
            catch (Exception)
            {
            }
            var o = new Course();
            o.Course_Date = Convert.ToDateTime(txtFromDate.Text.Trim(), new CultureInfo(1066));
            o.Course_Title = txtTitle.Text;
            o.Course_InitContent = txtInit.Text;
            o.Course_SubTitle = txtSubTitle.Text;
            o.Course_Content = NewsContent.Text;
            o.Course_Image = txtSelectedFile.Text;
            o.Course_ImageNote = txtImageTitle.Text;
            o.isActive = this.chkActive.Checked;
            o.News_ID = function.Obj2Int64(txtNewID.Text);
            o.PublicOrder = function.Obj2Int(ddlOrder.SelectedValue);
            o.Status = function.Obj2Int(ddlStatus.SelectedValue);
            if (courseID <= 0)
            {
                o.Course_ID = 0;
                o.Insert();
            }
            else
            {
                o.Course_ID = courseID;
                o.Update();
            }
            Response.Redirect("/office/giao_luu_list.aspx");
        }

    }
}