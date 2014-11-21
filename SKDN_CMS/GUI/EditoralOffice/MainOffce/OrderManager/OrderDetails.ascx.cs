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
using DFISYS.CoreBO.Common;

namespace MobileShop.GUI.Back_End.Order
{
    public partial class OrderDetails : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            switch(Request.QueryString["type"])
            {
                case "indate":
                    btnThanhToan.Visible = true;
                    btnUpdate.Visible = true;
                    btnDel.Visible = true;
                    break;
                case "overdate":
                    btnUpdate.Visible = true;
                    btnDel.Visible = true;
                    break;
                case "list":
                    btnDuyet.Visible = true;
                    break;
                default :
                    break;
            }
            ViewState["Sum"] = "0";
            if(!IsPostBack)
            {
                if(Request.QueryString["oid"]!=null&&Request.QueryString["oid"]!="")
                {
                    BindOrderDetail(Request.QueryString["oid"]);
                }
            }

        }

        protected  void BindOrderDetail(string OID)
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
                rdpRequiredDate.Value = Convert.ToDateTime(dtInfo.Rows[0]["O_RequiredDate"]).ToString("dd/MM/yyyy");
                ViewState["Sum"] = dtInfo.Rows[0]["O_total"].ToString();
                valueTotal.Value = dtInfo.Rows[0]["O_total"].ToString();
            }
            if(dt!=null&& dt.Rows.Count>0)
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
                    lblSum.Text = string.Format(Const.CurrentcyFormat, Convert.ToInt64(ViewState["Sum"]));
            }

        }

        protected void btnThanhToan_Click(object sender, EventArgs e)
        {
            OrderHelper oh = new OrderHelper();
            oh.XacNhanThanhToan(Convert.ToInt64(Request.QueryString["oid"]));
            Response.Redirect("/office/listorderispaid.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            OrderHelper oh = new OrderHelper();
            oh.DeleteOrderByID(Request.QueryString["oid"]);
            Response.Redirect("/office/listorder.aspx");
            //this.Page.ClientScript.RegisterClientScriptBlock();
        }

        protected void btnDuyet_Click(object sender, EventArgs e)
        {
            OrderHelper oh = new OrderHelper();
            oh.UpdateIsRemoveeOrderByID(Request.QueryString["oid"]);
            Response.Redirect("/office/listorder.aspx");
        }
        protected void gvCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "del")
            {
                string sql = "select * from tblOrderDetail where O_ID=" + Request.QueryString["oid"] + " and P_ID = " +
                             e.CommandArgument;
                DataTable dt = OrderHelper.SelectQuery(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    OrderHelper.DeleteOrderDetailItem(Convert.ToInt64(Request.QueryString["oid"]), Convert.ToInt32(e.CommandArgument));
                    string giaTriTru =
                         (Convert.ToInt64(valueTotal.Value) - (Convert.ToInt32(dt.Rows[0]["P_quantity"]) * Convert.ToInt64(dt.Rows[0]["P_price"]))).ToString();

                    OrderHelper.UpdateOrder(Convert.ToInt64(Request.QueryString["oid"]), giaTriTru);
                    BindOrderDetail(Request.QueryString["oid"]);

                }


            }
        }

        protected void gvCart_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }

        protected void gvCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BindOrderDetail(Request.QueryString["newsref"]);
        }
    }
}