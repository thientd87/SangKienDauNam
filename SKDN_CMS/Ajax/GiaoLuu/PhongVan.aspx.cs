using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.CoreBO;

namespace DFISYS.Ajax.GiaoLuu
{
    public partial class PhongVan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.Page.User.Identity.Name)) return;
            if (Request.QueryString["action"] == "updateStatus")
            {
                int id = DFISYS.CoreBO.Common.function.Obj2Int(Request.QueryString["id"]);
                int status = DFISYS.CoreBO.Common.function.Obj2Int(Request.QueryString["status"]);
                Course obj = new Course();
                obj.Course_ID = id;
                obj = obj.SelectOne();
                obj.Status = status;
                obj.Update();
            }
        }
    }
}