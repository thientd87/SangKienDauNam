using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.CoreBO;

namespace DFISYS.Ajax.GiaoLuu
{
    public partial class LichSuDuyet : System.Web.UI.Page
    {
        public int courseID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.Page.User.Identity.Name)) return;

            if (Request.QueryString["courseID"] != null )
            {
                courseID = DFISYS.CoreBO.Common.function.Obj2Int(Request.QueryString["courseID"]);
                CourseLog obj = new CourseLog();
                this.grdListPhongVan.DataSource = obj.SelectAllByCourseID(courseID);
                this.grdListPhongVan.DataBind();
            }
        }
    }
}