using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BO;

namespace SKDN.Web
{
    public partial class ProjectDetailAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtHotSubject = ProductHelper.GetProductByID(Lib.QueryString.ProductID);
                if (dtHotSubject != null && dtHotSubject.Rows.Count > 0)
                {
                    ltrImage.Text = dtHotSubject.Rows[0]["Image"] != null && !string.IsNullOrEmpty(dtHotSubject.Rows[0]["Image"].ToString()) ? dtHotSubject.Rows[0]["Image"].ToString() : string.Empty;
                    ltrContentProject.Text = dtHotSubject.Rows[0]["ProductDescription"].ToString();
                }
            }
        }
    }
}