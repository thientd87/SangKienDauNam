using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using System.Collections;
using Dundas.Charting.WebControl;
using DFISYS.Core.DAL;
using DFISYS.User.Db;


namespace DFISYS.GUI.EditoralOffice.MainOffce.OnLoad.UserControl
{
    public partial class statistic : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strStartDate = DateTime.Today.AddDays(-7).ToString("yyyyMMdd");
                string strEndDate = DateTime.Today.ToString("yyyyMMdd");
                //string strStartDate = "20071201";
                //string strEndDate = "20071207";

                // - 9: la kieu thong ke theo so nguoi va may truy cap
                string seriesName = "Thống kê số người truy cập";
                DrawChart("line", "", "9",seriesName, strStartDate, strEndDate, "", "");

                // - 10: la kieu Thống kê theo số IP truy  cập
                //seriesName = "Thống kê theo số IP truy cập";
                //DrawChart("line", "", "10",seriesName, strStartDate, strEndDate, "", "");
            }
        }

        private void DrawChart(string chartType, string strID, string strCountKey, string seriesName, string strStartDate, string strEndDate, string strMinValue, string strMaxValue)
        {

            //// Bieu do Line
            //string[] aId = strID.Split(',');
            //for (int i = 0; i < aId.Length; i++)
            //{
            //    ArrayList visualReport = new VisualReport().GetReport(strCountKey, aId[i], strStartDate, strEndDate, strMinValue, strMaxValue);
            //    SqlDataReader dr = null;

            //    dr = new VisualReport().GetReportAsDataReader(strCountKey, aId[i], strStartDate, strEndDate, strMinValue, strMaxValue);
                
            //    if (dr.HasRows == true)
            //    {
            //        Chart1.Series.Add(seriesName);
            //        if (chartType == "line")
            //        {

            //            Chart1.Series[seriesName].Type = SeriesChartType.Line;
            //            Chart1.Series[seriesName].Points.DataBind(dr, "xValues", "yValues", "Label=yValues");
            //            Chart1.Series[seriesName].ShowLabelAsValue = true;
            //            Chart1.Series[seriesName]["LabelStyle"] = "Top";
            //            Chart1.Series[seriesName].Type = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Spline", true); ;
            //            Chart1.Series[seriesName].BorderWidth = 2;
            //            Chart1.Series[seriesName].MarkerStyle = MarkerStyle.Circle;

            //        }
            //    }

            //}

            //Chart1.ChartAreas["Default"].AxisX.Margin = true;
            //Chart1.CssClass = "DisplayInline";
        }
    }
}