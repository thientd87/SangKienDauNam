using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BO;

namespace SKDN.Web.Pages
{
    public partial class ListProjectByTime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string OrderType = Lib.QueryString.OrderType;
                
                DataTable dt = ProductHelper.GetProductByTime(99, 1, OrderType);
                if (dt != null && dt.Rows.Count > 0)
                {
                    rptListProject.DataSource = dt;
                    rptListProject.DataBind();
                }
            }
        }
    }
}