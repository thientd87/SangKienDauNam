using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BO;

namespace MobileShop.GUI.Back_End.Order
{
    public partial class OrderDetailIsPaid : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            ViewState["Sum"] = "0";
            if (!IsPostBack)
            {
                if (Request.QueryString["oid"] != null && Request.QueryString["oid"] != "")
                {
                    BindOrderDetail(Request.QueryString["oid"]);
                }
            }

        }
        protected void BindOrderDetail(string OID)
        {
            BO.OrderHelper oh = new OrderHelper();
            DataTable dt = oh.GetOrderDetailByOrderID(Convert.ToInt64(OID));
            DataTable dtInfo = oh.GetInfoOfOrderByOrderID(Convert.ToInt64(OID));
            if (dtInfo != null && dtInfo.Rows.Count > 0)
            {
                lblCusName.Text = dtInfo.Rows[0]["C_fullname"].ToString();
                lblEmail.Text = dtInfo.Rows[0]["C_Email"].ToString();
                lblAddress.Text = dtInfo.Rows[0]["C_Address"].ToString();
                lblTel.Text = dtInfo.Rows[0]["C_Phone"].ToString();
                lblOrderDate.Text = Convert.ToDateTime(dtInfo.Rows[0]["O_date"]).ToString("dd/MM/yyyy HH:mm:ss");
                lblSuggess.Text = dtInfo.Rows[0]["O_suggest"].ToString();
                rdpRequiredDate.Text = Convert.ToDateTime(dtInfo.Rows[0]["O_RequiredDate"]).ToString("dd/MM/yyyy");
                ViewState["Sum"] = string.Format("{0:n}", dtInfo.Rows[0]["O_total"]);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                gvCart.DataSource = dt;
                gvCart.DataBind();
            }
        }
        protected void gvCart_DataBound(object sender, EventArgs e)
        {
            if (gvCart.Rows.Count > 0)
            {
                Label lblSum = (Label)gvCart.FooterRow.FindControl("lblThanhTien");
                if (lblSum != null)
                    lblSum.Text = ViewState["Sum"].ToString();
            }

        }

    }
}