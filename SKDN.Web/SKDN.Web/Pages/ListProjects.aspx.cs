﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BO;

namespace SKDN.Web.Pages
{
    public partial class ListProjects : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int CatID = Lib.QueryString.CategoryID;
                ProductHelper ph = new ProductHelper();
                DataTable dt = ph.GetProductByCatID_Paged(99, 1, CatID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    rptListProject.DataSource = dt;
                    rptListProject.DataBind();
                }
            }
        }
    }
}