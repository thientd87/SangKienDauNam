using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using AddIns.BO;

namespace AddIns.ajax
{
    public partial class adv_delete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               //if(!String.IsNullOrEmpty(Request.QueryString["i"]))
               //    AdvHelper.AdvDelete(Request.QueryString["i"]);
            }
        }
    }
}