using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.Editoral.Category;
using Portal.BO.Editoral.ThongKeBaiViet;
using System.Data;
using System.Globalization;
using System.Text;

namespace DFISYS.GUI.EditoralOffice.MainOffce.Thongke
{
    public partial class ThongKeBaiVietTheoBieuDo : System.Web.UI.UserControl
    {
       // static string dateFromStatic;
       // static string dateToStatic;
        static string userNameStatic;
        private int dateCount = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                userNameStatic = this.Page.User.Identity.Name;//Convert.ToString(Request.QueryString["userName"]);
               // dateFromStatic = Request.QueryString["fromDate"]??DateTime.Now.ToString("MM/dd/yyyy");
              //  dateToStatic = Request.QueryString["toDate"] ?? DateTime.Now.ToString("MM/dd/yyyy"); ;


                Literal1.Text = "Thống kê bài viết theo tác giả " + userNameStatic;
                CategoryHelper.Treebuild(ddlChuyenmuc);

                ViewState["sortingOrder"] = string.Empty;
                int check = 0;
                if (checkDateStat.Checked)
                {
                    check = 1;
                    rptListnew.Visible = false;
                    GridView1.Visible = true;
                    BindData("", "", userNameStatic, check);
                }
                else
                {
                    rptListnew.Visible = true;
                    GridView1.Visible = false;
                    check = 0;
                    BindData("", "", userNameStatic, check);
                }

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "123", GetChartData(0, 0).ToString());
            }
        }

        protected void btnXem_Click(object sender, EventArgs e)
        {
            int check = 0;
            if (checkDateStat.Checked)
            {
                dateCount = Convert.ToInt32(dllNumberDay.SelectedValue);
                int cateID = Convert.ToInt32(ddlChuyenmuc.SelectedValue);
                check = 1;
                rptListnew.Visible = false;
                GridView1.Visible = true;
                BindData("", "", userNameStatic, check);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "123", GetChartData(dateCount, cateID).ToString());
            }
            else
            {
                int cateID = Convert.ToInt32(ddlChuyenmuc.SelectedValue);
                rptListnew.Visible = true;
                GridView1.Visible = false;
                check = 0;
                BindData("", "", userNameStatic, check);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "123", GetChartData(0, cateID).ToString());
            }
        }

        protected void rptListnew_Sorting(object sender, GridViewSortEventArgs e)
        {
            int check = 0;
            if (checkDateStat.Checked)
            {
                check = 1;
                rptListnew.Visible = false;
                GridView1.Visible = true;
                BindData(e.SortExpression, sortingOrder, userNameStatic, check);
            }
            else
            {
                rptListnew.Visible = true;
                GridView1.Visible = false;
                check = 0;
                BindData(e.SortExpression, sortingOrder, userNameStatic, check);
            }
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            int check = 0;
            if (checkDateStat.Checked)
            {
                check = 1;
                rptListnew.Visible = false;
                GridView1.Visible = true;
                BindData(e.SortExpression, sortingOrder, userNameStatic, check);
            }
            else
            {
                rptListnew.Visible = true;
                GridView1.Visible = false;
                check = 0;
                BindData(e.SortExpression, sortingOrder, userNameStatic, check);
            }
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

        private void BindData(string sortExpr, string sortOrder, string user, int check)
        {
            ThongKeBaiVietHelper viet = new ThongKeBaiVietHelper();
            dateCount = Convert.ToInt32(dllNumberDay.SelectedValue);
            if (dateCount == 0) dateCount = 10000;
            DateTime fromdate = DateTime.Now.AddDays(-dateCount);
            DateTime todate = DateTime.Now;
            int Top = Convert.ToInt32(dllPageCount.SelectedValue);
            
            DataTable dtAll = viet.ThongKeBaiTheoChiTietTacGia(fromdate.ToString("yyyy/MM/dd"), todate.ToString("yyyy/MM/dd"),
                                                         Convert.ToInt32(ddlChuyenmuc.SelectedValue), Top, user, check, dateCount);
            if (dtAll != null)
            {
                DataView dv = dtAll.DefaultView;
                if (sortExpr != string.Empty)
                    dv.Sort = sortExpr + " " + sortOrder;
                if (checkDateStat.Checked)
                {
                    GridView1.DataSource = dv;
                    GridView1.DataBind();
                }
                else
                {
                    rptListnew.DataSource = dv;
                    rptListnew.DataBind();
                }
            }
        }

        protected StringBuilder GetChartData(int dateCount, int cateID)
        {
            ThongKeBaiVietHelper viet = new ThongKeBaiVietHelper();
            dateCount = Convert.ToInt32(dllNumberDay.SelectedValue);
            if (dateCount == 0) dateCount = 10000;
            DateTime fromdate = DateTime.Now.AddDays(-dateCount);
            DateTime todate = DateTime.Now;
            DataTable dtAll = viet.ThongKeBaiTheoGioXuatBan(fromdate.ToString("yyyy/MM/dd"), todate.ToString("yyyy/MM/dd"), userNameStatic, dateCount, cateID);
            DataTable dtAllTao = viet.ThongKeBaiTheoGioTao(fromdate.ToString("yyyy/MM/dd"), todate.ToString("yyyy/MM/dd"), userNameStatic, dateCount, cateID);
            StringBuilder scriptChart = new StringBuilder();
            StringBuilder dataBaitao = new StringBuilder();
            StringBuilder dataBaiXB = new StringBuilder();
            int[] dateTao = new int[24];
            int[] dateXB = new int[24];
            int i = 0;
            foreach (DataRow row in dtAllTao.Rows)
            {
                if (row["GioTao"].ToString().TrimEnd().Length == 0)
                {
                    dateTao[i] = 0;
                }
                else
                {
                    dateTao[i] = Convert.ToInt16(row["GioTao"].ToString());
                }

                if (dateTao[i] == i)
                {
                    if (row["SoLuongBaiTao"].ToString().Length == 0)
                    {
                        dataBaitao.Append("0,");
                    }
                    else
                    {
                        dataBaitao.Append(row["SoLuongBaiTao"].ToString() + ",");
                    }
                }
                else
                {
                    int k = i;
                    for (int j = 0; j < dateTao[k] - k; j++)
                    {
                        dataBaitao.Append("0,".ToString());
                        i++;
                    }
                    if (row["SoLuongBaiTao"].ToString().Length == 0)
                    {
                        dataBaitao.Append("0,");
                    }
                    else
                    {
                        dataBaitao.Append(row["SoLuongBaiTao"].ToString() + ",");
                    }
                }
                i++;
            }

            if (i < 24)
            {
                for (int j = i; j < 24; j++)
                {
                    dataBaitao.Append("0,");
                }
            }

            i = 0;
            foreach (DataRow row in dtAll.Rows)
            {
                if (row["GioXB"].ToString().TrimEnd().Length == 0)
                {
                    dateXB[i] = 0;
                }
                else
                {
                    dateXB[i] = Convert.ToInt16(row["GioXB"].ToString());
                }

                if (dateXB[i] == i)
                {
                    if (row["SoLuongBaiXB"].ToString().Length == 0)
                    {
                        dataBaiXB.Append("0,");
                    }
                    else
                    {
                        dataBaiXB.Append(row["SoLuongBaiXB"].ToString() + ",");
                    }
                }
                else
                {
                    int k = i;
                    for (int j = 0; j < dateXB[k] - k; j++)
                    {
                        dataBaiXB.Append("0,".ToString());
                        i++;
                    }
                    if (row["SoLuongBaiXB"].ToString().Length == 0)
                    {
                        dataBaiXB.Append("0,");
                    }
                    else
                    {
                        dataBaiXB.Append(row["SoLuongBaiXB"].ToString() + ",");
                    }
                }
                i++;
            }

            if (i < 24)
            {
                for (int j = i; j < 24; j++)
                {
                    dataBaiXB.Append("0,");
                }
            }

            dataBaitao.Remove(dataBaitao.Length - 1, 1);
            dataBaiXB.Remove(dataBaiXB.Length - 1, 1);

            scriptChart.Append("<script language=\"javascript\" src=\"/Scripts/jquery-1.4.1.js\" type=\"text/javascript\"><script language=\"javascript\" src=\"/Scripts/highcharts.js\" type=\"text/javascript\"></script></script><script type=\"text/javascript\"> var chart; $(document).ready(function () {chart = new Highcharts.Chart({chart: { renderTo: 'container',type: 'line',marginRight: 130,marginBottom: 25},title: {text: 'Thống kê bài của biên tập viên " + userNameStatic + "',x: -20},subtitle: {text: '',x: -20},xAxis: {categories: ['0h', '1h',  '2h', '3h', '4h', '5h', '6h','7h', '8h', '9h', '10h', '11h', '12h', '13h', '14h', '15h', '16h', '17h', '18h', '19h', '20h', '21h', '22h', '23h']},yAxis: {title: {text: 'Số lượng bài'},plotLines: [{value: 0,width: 1,color: '#808080'}]},tooltip: {formatter: function () {return '<b>' + this.series.name + '</b><br/>' +this.x + ': ' + this.y + ' bài';}},legend: {layout: 'vertical',align: 'right',verticalAlign: 'top',x: -10,y: 100,borderWidth: 0},series: [{name: 'Bài tạo',data: [" + dataBaitao.ToString() + "]}, {name: 'Bài xuất bản',data: [" + dataBaiXB.ToString() + "]}]});});</script>");


            return scriptChart;
        }


        protected void rptListnew_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView data = e.Row.DataItem as DataRowView;

                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

                if (data["SoLuongBaiTao"].ToString().Length == 0)
                {
                    e.Row.Cells[1].Text = "0";
                }
                if (data["SoLuongBaiXB"].ToString().Length == 0)
                {
                    e.Row.Cells[2].Text = "0";
                }
            }

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView data = e.Row.DataItem as DataRowView;
                Literal listeral3 = e.Row.FindControl("Literal3") as Literal;
                var item = data["SoLuongBaiXB"];

                if (item == null || item.ToString().Trim().Length == 0)
                {
                    listeral3.Text = data["DateXuatban2"].ToString();
                }
                else
                {
                    listeral3.Text = data["SoLuongBaiXB"].ToString();
                }

                if (data["SoLuongBaiTao"].ToString().Length == 0)
                {
                    e.Row.Cells[2].Text = "0";
                }
                if (data["SoLuongBaiXB"].ToString().Length == 0)
                {
                    e.Row.Cells[3].Text = "0";
                }

                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
            }
        }
    }
}