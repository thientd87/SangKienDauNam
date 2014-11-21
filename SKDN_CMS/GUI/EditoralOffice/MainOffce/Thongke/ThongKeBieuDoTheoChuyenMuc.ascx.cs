using System;
using System.Web.UI.WebControls;
using System.Data;
using DFISYS.BO.Editoral.Category;
using Portal.BO.Editoral.ThongKeBaiViet;
using System.Text;

namespace DFISYS.GUI.EditoralOffice.MainOffce.Thongke
{
    public partial class ThongKeBieuDoTheoChuyenMuc : System.Web.UI.UserControl
    {
        static string dateFromStatic;
        static string dateToStatic;
        static string cateID;
        static string sortOrder;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cateID = Convert.ToString(Request.QueryString["cateID"]);
                dateFromStatic = DateTime.Now.AddDays(-1000).ToString("MM/dd/yyyy");//Request.QueryString["fromDate"];
                dateToStatic = DateTime.Now.ToString("MM/dd/yyyy");//Request.QueryString["toDate"];
                sortOrder = "1";//Request.QueryString["sortOrder"];

                CategoryHelper.Treebuild(ddlChuyenmuc);

                ViewState["sortingOrder"] = string.Empty;

                BindData("", "", Convert.ToInt16(cateID), Convert.ToInt16(cateID));

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "123", GetChartData(Convert.ToInt32(cateID), Convert.ToInt32(sortOrder), 0).ToString());
            }
        }

        protected void btnXem_Click(object sender, EventArgs e)
        {
            int dateCount = Convert.ToInt32(dllNumberDay.SelectedValue);
            int cateID = Convert.ToInt32(ddlChuyenmuc.SelectedValue);
            BindData("", "", Convert.ToInt16(cateID), Convert.ToInt16(cateID));
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "123", GetChartData(cateID, 1, dateCount).ToString());

        }

        protected void rptListnew_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindData(e.SortExpression, sortingOrder, Convert.ToInt16(cateID), -1);
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

        private void BindData(string sortExpr, string sortOrder, int cateID, int parentID)
        {
            ThongKeBaiVietHelper viet = new ThongKeBaiVietHelper();
            DateTime fromdate = DateTime.Now.AddDays(-1000);//Convert.ToDateTime(dateFromStatic);
            DateTime todate = DateTime.Now;//Convert.ToDateTime(dateToStatic);
            int dateCount = Convert.ToInt32(dllNumberDay.SelectedValue);
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

        protected StringBuilder GetChartData(int cateID, int sortOrder, int dateCount)
        {
            ThongKeBaiVietHelper viet = new ThongKeBaiVietHelper();
            if (dateCount == 0) dateCount = 1000;
            DateTime fromdate = DateTime.Now.AddDays(-dateCount);//Convert.ToDateTime(dateFromStatic);
            DateTime todate = DateTime.Now;//Convert.ToDateTime(dateToStatic);
            DataTable dtAll = viet.ThongKeBaiTheoGioXuatBanTheoChuyenMuc(fromdate.ToString("yyyy/MM/dd"), todate.ToString("yyyy/MM/dd"), cateID, sortOrder, dateCount);
            DataTable dtAllTongThe = viet.ThongKeBaiTheoGioXuatBanTheoChuyenMucTongThe(fromdate.ToString("yyyy/MM/dd"), todate.ToString("yyyy/MM/dd"), cateID, sortOrder, dateCount);
            StringBuilder scriptChart = new StringBuilder();
            StringBuilder nameSeri = new StringBuilder();
            scriptChart.Append("<script language=\"javascript\" src=\"/Scripts/jquery-1.4.1.js\" type=\"text/javascript\"><script language=\"javascript\" src=\"/Scripts/highcharts.js\" type=\"text/javascript\"></script></script><script type=\"text/javascript\"> var chart; $(document).ready(function () {chart = new Highcharts.Chart({chart: { renderTo: 'container',type: 'line',marginRight: 130,marginBottom: 25},title: {text: 'Thống kê bài xuất bản theo chuyên mục',x: -20},subtitle: {text: '',x: -20},xAxis: {categories: ['0h', '1h',  '2h', '3h', '4h', '5h', '6h','7h', '8h', '9h', '10h', '11h', '12h', '13h', '14h', '15h', '16h', '17h', '18h', '19h', '20h', '21h', '22h', '23h']},yAxis: {title: {text: 'Số lượng bài'},plotLines: [{value: 0,width: 1,color: '#808080'}]},tooltip: {formatter: function () {return '<b>' + this.series.name + '</b><br/>' +this.x + ': ' + this.y + ' bài';}},legend: {layout: 'vertical',align: 'right',verticalAlign: 'top',x: -10,y: 100,borderWidth: 0},series: [");
            StringBuilder dataBaiXB = new StringBuilder();
            int[] dateXB = new int[24];
            int i = 0;

            //{name: 'Bài tạo',data: [" + dataBaitao.ToString() + "]},
            nameSeri.Append("{name: 'Tổng các chuyên mục',data: [");

            foreach (DataRow row in dtAllTongThe.Rows)
            {
                if (row["GioXBTongThe"].ToString().TrimEnd().Length == 0)
                {
                    dateXB[i] = 0;
                }
                else
                {
                    dateXB[i] = Convert.ToInt16(row["GioXBTongThe"].ToString());
                }

                if (dateXB[i] == i)
                {
                    if (row["SoLuongBaiXBTongThe"].ToString().Length == 0)
                    {
                        dataBaiXB.Append("0,");
                    }
                    else
                    {
                        dataBaiXB.Append(row["SoLuongBaiXBTongThe"].ToString() + ",");
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
                    if (row["SoLuongBaiXBTongThe"].ToString().Length == 0)
                    {
                        dataBaiXB.Append("0,");
                    }
                    else
                    {
                        dataBaiXB.Append(row["SoLuongBaiXBTongThe"].ToString() + ",");
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

            dataBaiXB.Remove(dataBaiXB.Length - 1, 1);

            nameSeri.Append(dataBaiXB.ToString());

            scriptChart.Append(nameSeri + "]},");

            dataBaiXB.Remove(0, dataBaiXB.Length);
            nameSeri.Remove(0, nameSeri.Length);

            i = 0;
            for (int n = 0; n < dtAll.Rows.Count; n++)
            {
                if (n == (dtAll.Rows.Count - 1))
                {
                    if (dtAll.Rows[n]["GioXB"].ToString().TrimEnd().Length == 0)
                    {
                        dateXB[i] = 0;
                    }
                    else
                    {
                        dateXB[i] = Convert.ToInt16(dtAll.Rows[n]["GioXB"].ToString());
                    }

                    if (dateXB[i] == i)
                    {
                        if (dtAll.Rows[n]["SoLuongBaiXB"].ToString().Length == 0)
                        {
                            dataBaiXB.Append("0,");
                        }
                        else
                        {
                            dataBaiXB.Append(dtAll.Rows[n]["SoLuongBaiXB"].ToString() + ",");
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
                        if (dtAll.Rows[n]["SoLuongBaiXB"].ToString().Length == 0)
                        {
                            dataBaiXB.Append("0,");
                        }
                        else
                        {
                            dataBaiXB.Append(dtAll.Rows[n]["SoLuongBaiXB"].ToString() + ",");
                        }
                    }

                    if (i < 24)
                    {
                        for (int j = i; j < 24; j++)
                        {
                            dataBaiXB.Append("0,");
                        }
                    }
                    i = 0;
                    dataBaiXB.Remove(dataBaiXB.Length - 1, 1);

                    nameSeri.Append("{name: '" + dtAll.Rows[n]["Cate_Name"].ToString() + "',data: [" + dataBaiXB.ToString());

                    scriptChart.Append(nameSeri.ToString() + "]}");

                    dataBaiXB.Remove(0, dataBaiXB.Length);
                    nameSeri.Remove(0, nameSeri.Length);
                }
                else
                {
                    if (Convert.ToInt32(dtAll.Rows[n]["Cate_ID"].ToString()) != Convert.ToInt32(dtAll.Rows[n + 1]["Cate_ID"].ToString()))
                    {
                        if (dtAll.Rows[n]["GioXB"].ToString().TrimEnd().Length == 0)
                        {
                            dateXB[i] = 0;
                        }
                        else
                        {
                            dateXB[i] = Convert.ToInt16(dtAll.Rows[n]["GioXB"].ToString());
                        }

                        if (dateXB[i] == i)
                        {
                            if (dtAll.Rows[n]["SoLuongBaiXB"].ToString().Length == 0)
                            {
                                dataBaiXB.Append("0,");
                            }
                            else
                            {
                                dataBaiXB.Append(dtAll.Rows[n]["SoLuongBaiXB"].ToString() + ",");
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
                            if (dtAll.Rows[n]["SoLuongBaiXB"].ToString().Length == 0)
                            {
                                dataBaiXB.Append("0,");
                            }
                            else
                            {
                                dataBaiXB.Append(dtAll.Rows[n]["SoLuongBaiXB"].ToString() + ",");
                            }
                        }
                        if (i < 24)
                        {
                            for (int j = i; j < 24; j++)
                            {
                                dataBaiXB.Append("0,");
                            }
                        }
                        i = 0;
                        dataBaiXB.Remove(dataBaiXB.Length - 1, 1);

                        nameSeri.Append("{name: '" + dtAll.Rows[n]["Cate_Name"].ToString() + "',data: [" + dataBaiXB.ToString());

                        scriptChart.Append(nameSeri.ToString() + "]},");

                        dataBaiXB.Remove(0, dataBaiXB.Length);
                        nameSeri.Remove(0, nameSeri.Length);
                        continue;
                    }
                }

                if (dtAll.Rows[n]["GioXB"].ToString().TrimEnd().Length == 0)
                {
                    dateXB[i] = 0;
                }
                else
                {
                    dateXB[i] = Convert.ToInt16(dtAll.Rows[n]["GioXB"].ToString());
                }

                if (dateXB[i] == i)
                {
                    if (dtAll.Rows[n]["SoLuongBaiXB"].ToString().Length == 0)
                    {
                        dataBaiXB.Append("0,");
                    }
                    else
                    {
                        dataBaiXB.Append(dtAll.Rows[n]["SoLuongBaiXB"].ToString() + ",");
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
                    if (dtAll.Rows[n]["SoLuongBaiXB"].ToString().Length == 0)
                    {
                        dataBaiXB.Append("0,");
                    }
                    else
                    {
                        dataBaiXB.Append(dtAll.Rows[n]["SoLuongBaiXB"].ToString() + ",");
                    }
                }
                i++;
            }

            scriptChart.Append("]});});</script>");

            return scriptChart;
        }


        protected void rptListnew_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView data = e.Row.DataItem as DataRowView;

                e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();

                string item = data["Cate_ID"].ToString();

                ThongKeBaiVietHelper viet = new ThongKeBaiVietHelper();
                DateTime fromdate = Convert.ToDateTime(dateFromStatic);
                DateTime todate = Convert.ToDateTime(dateToStatic);
                int dateCount = Convert.ToInt32(dllNumberDay.SelectedValue);
                DataTable dtAll = viet.ThongKeBaiTheoChuyenMucDeQuy(fromdate.ToString("yyyy/MM/dd"), todate.ToString("yyyy/MM/dd"),
                                                             Convert.ToInt16(cateID), Convert.ToInt16(cateID));

                int sum = CountRecursive(dtAll, Convert.ToInt16(item), Convert.ToInt32(data["SoLuongBaiTao"].ToString()));


                if (data["SoLuongBaiTao"].ToString().Length == 0)
                {
                    e.Row.Cells[2].Text = "0";
                }
                else
                {
                    e.Row.Cells[2].Text = sum.ToString();
                }
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