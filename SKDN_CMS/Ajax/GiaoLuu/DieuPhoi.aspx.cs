using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.CoreBO;

namespace DFISYS.Ajax.GiaoLuu
{
    public partial class DieuPhoi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.Page.User.Identity.Name)) return;
            if (Request.QueryString["action"] == "updateDiephoi")
            {
                int id = DFISYS.CoreBO.Common.function.Obj2Int(Request.QueryString["id"]);
                int ur = DFISYS.CoreBO.Common.function.Obj2Int(Request.QueryString["userResponse"]);
                BO.CoreBO.Question obj = new BO.CoreBO.Question();
                obj.Question_ID = id;
                obj = obj.SelectOne();
                obj.Channel_ID = ur;
                obj.Update();
            }
        }
    }
}