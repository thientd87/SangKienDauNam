using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BO;

namespace SKDN.Web.UserControls
{
    public partial class FilterBar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProductCategoryHelper pch=  new ProductCategoryHelper();
                DataTable dtSubject = pch.GetCatParent();
                if (dtSubject != null && dtSubject.Rows.Count > 0)
                {
                    rptSubject.DataSource = dtSubject;
                    rptSubject.DataBind();
                }
            }
        }
    }
}