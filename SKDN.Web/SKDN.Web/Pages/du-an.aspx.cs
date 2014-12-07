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
    public partial class du_an : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtHotSubject = ProductHelper.SelectProductByProductTypePaged(1, 1, 1);
                if (dtHotSubject != null && dtHotSubject.Rows.Count > 0)
                {
                    ltrImage.Text = dtHotSubject.Rows[0]["Image"] != null && !string.IsNullOrEmpty(dtHotSubject.Rows[0]["Image"].ToString()) ? dtHotSubject.Rows[0]["Image"].ToString() : string.Empty;
                    ltrContentProject.Text = dtHotSubject.Rows[0]["ProductDescription"].ToString();

                    DataTable dtData = ProductHelper.GetProductByTime(18, 1, "DESC");
                    for (int i = 1; i < dtHotSubject.Rows.Count; i++)
                    {
                        dtData.ImportRow(dtHotSubject.Rows[i]);
                    }

                    rptListProject.DataSource = dtData;
                    rptListProject.DataBind();
                
                }
            }
        }
    }
}