using System;
using System.Web.UI.WebControls;
using DFISYS.BO.Editoral.Category;
using Portal.BO.Editoral.ThongKeBaiViet;
using System.Data;
using System.Globalization;
using System.Web.UI.HtmlControls;

namespace DFISYS.GUI.EditoralOffice.MainOffce.Thongke
{
    public partial class ThongKeBaiVietTheoChuyenMuc : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CategoryHelper.Treebuild(ddlChuyenmuc);
                txtfromDate.Text = DateTime.Now.AddDays(-30.0).ToString("dd/MM/yyyy");
                txttoDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ViewState["sortingOrder"] = string.Empty;
                BindData("", "", 0, -1);
            }
        }

        protected void btnXem_Click(object sender, EventArgs e)
        {
            BindData("", "", Convert.ToInt32(ddlChuyenmuc.SelectedValue), Convert.ToInt32(ddlChuyenmuc.SelectedValue));
        }

        private void BindData(string sortExpr, string sortOrder, int cateID, int parentID)
        {
            ThongKeBaiVietHelper viet = new ThongKeBaiVietHelper();
            DateTime fromdate = Convert.ToDateTime(this.txtfromDate.Text, new CultureInfo(1066));
            DateTime todate = Convert.ToDateTime(this.txttoDate.Text, new CultureInfo(1066));
            DataTable dtAll = viet.ThongKeBaiTheoChuyenMucDeQuy(fromdate.ToString("yyyy/MM/dd"), todate.ToString("yyyy/MM/dd"),
                                                         cateID, parentID);
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
            BindData(e.SortExpression, sortingOrder, Convert.ToInt32(ddlChuyenmuc.SelectedValue), Convert.ToInt32(ddlChuyenmuc.SelectedValue));
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
                DataRowView data = e.Row.DataItem as DataRowView;


                HtmlAnchor linkThongKeBaiXB = e.Row.FindControl("linkThongKeBaiXB") as HtmlAnchor;

                HtmlAnchor linkThongKeBieuDo = e.Row.FindControl("linkThongKeBieuDo") as HtmlAnchor;

                string item = data["Cate_ID"].ToString();

                linkThongKeBaiXB.Attributes.Add("dateFrom", Convert.ToDateTime(this.txtfromDate.Text, new CultureInfo(1066)).ToString());
                linkThongKeBaiXB.Attributes.Add("dateTo", Convert.ToDateTime(this.txttoDate.Text, new CultureInfo(1066)).ToString());
                linkThongKeBaiXB.Attributes.Add("sortOrder", data["SortOrder"].ToString());
                linkThongKeBaiXB.Attributes.Add("cateID", item.ToString());

                linkThongKeBieuDo.Attributes.Add("dateFrom", Convert.ToDateTime(this.txtfromDate.Text, new CultureInfo(1066)).ToString());
                linkThongKeBieuDo.Attributes.Add("dateTo", Convert.ToDateTime(this.txttoDate.Text, new CultureInfo(1066)).ToString());
                linkThongKeBieuDo.Attributes.Add("sortOrder", data["SortOrder"].ToString());
                linkThongKeBieuDo.Attributes.Add("cateID", item.ToString());

                ThongKeBaiVietHelper viet = new ThongKeBaiVietHelper();
                DateTime fromdate = Convert.ToDateTime(this.txtfromDate.Text, new CultureInfo(1066));
                DateTime todate = Convert.ToDateTime(this.txttoDate.Text, new CultureInfo(1066));
                DataTable dtAll = viet.ThongKeBaiTheoChuyenMucDeQuy(fromdate.ToString("yyyy/MM/dd"), todate.ToString("yyyy/MM/dd"),
                                                                Convert.ToInt32(ddlChuyenmuc.SelectedValue), -1);


                int sum = CountRecursive(dtAll, Convert.ToInt16(item), Convert.ToInt32(data["SoLuongBaiTao"].ToString()));

                linkThongKeBaiXB.InnerText = sum.ToString();
                
                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

            }
        }

        public int CountRecursive(DataTable dtData, int cateID, int sum) 
        {
            foreach (DataRow row in dtData.Rows)
            {
                if (Convert.ToInt32(row["ParentID"].ToString()) == cateID)
                {
                    sum += Convert.ToInt32(row["SoLuongBaiTao"].ToString());
                    
                    foreach (DataRow row2 in dtData.Rows)
                    {
                        if (Convert.ToInt32(row2["ParentID"]) == Convert.ToInt32(row["Cate_ID"].ToString()))
                        {
                            sum += Convert.ToInt32(row2["SoLuongBaiTao"]);
                        }
                    }
                }
            }
            return sum;
        }
    }
}