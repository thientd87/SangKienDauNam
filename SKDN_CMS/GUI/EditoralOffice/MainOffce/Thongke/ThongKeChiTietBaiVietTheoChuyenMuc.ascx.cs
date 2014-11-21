using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.BO.Editoral.ThongKeBaiViet;
using System.Data;

namespace Portal.GUI.EditoralOffice.MainOffce.Thongke
{
    public partial class ThongKeChiTietBaiVietTheoChuyenMuc : System.Web.UI.UserControl
    {
        static string dateFromStatic;
        static string dateToStatic;
        static string cateID;
        static string sortOrderFromRequest;

        protected void Page_Load(object sender, EventArgs e)
        {
           // dateFromStatic = Request.QueryString["fromDate"];
           // dateToStatic = Request.QueryString["toDate"];
            cateID = Request.QueryString["cateID"];
            sortOrderFromRequest = Request.QueryString["sortOrder"];

            ViewState["sortingOrder"] = string.Empty;
            BindData("", "", dateFromStatic, dateToStatic, cateID, sortOrderFromRequest);
        }

        private void BindData(string sortExpr, string sortOrder, string dateFrom, string toDate, string cateID, string sortOrderFromRequest)
        {
            ThongKeBaiVietHelper viet = new ThongKeBaiVietHelper();
            DateTime fromdate = Convert.ToDateTime(dateFrom);
            DateTime todate = Convert.ToDateTime(toDate);
            DataTable dtAll = viet.ThongKeTheoTungBaiTheoChuyenMuc(fromdate.ToString("yyyy/MM/dd"), todate.ToString("yyyy/MM/dd"), Convert.ToInt16(cateID), Convert.ToInt16(sortOrderFromRequest));
            if (dtAll != null)
            {
                DataView dv = dtAll.DefaultView;
                if (sortExpr != string.Empty)
                    dv.Sort = sortExpr + " " + sortOrder;
                rptListnew.DataSource = dv;
                rptListnew.DataBind();
            }

        }

        protected void rptListnew_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindData(e.SortExpression, sortingOrder, dateFromStatic, dateToStatic, cateID, sortOrderFromRequest);
        }

        public string sortingOrder
        {
            get
            {
                if (ViewState["sortingOrder"].ToString() == "desc")
                    ViewState["sortingOrder"] = "asc";
                else
                    ViewState["sortingOrder"] = "desc";

                return ViewState["sortingOrder"].ToString();
            }
            set
            {
                ViewState["sortingOrder"] = value;
            }
        }

        protected void rptListnew_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            }
        }
    }
}