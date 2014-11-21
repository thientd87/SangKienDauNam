using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Portal.BO;
using System.Web.UI.DataVisualization.Charting;

namespace Portal.GUI.EditoralOffice.MainOffce.Newslist
{
    public partial class History : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindData();
        }

        private void BindData()
        {
            long NewsID = 0;
            Int64.TryParse(Request.QueryString["News_ID"].ToString(), out NewsID);

            DataTable history = Portal.BO.Action.GetActionHistory(NewsID);
            if (history != null && history.Rows.Count > 0)
            {
                grdList.DataSource = history;
                grdList.DataBind();
            }

            //Title t = new Title(Portal.BO.Action.GetNewsTitle(NewsID), Docking.Top,
            //                    new System.Drawing.Font("Trebuchet MS", 14, System.Drawing.FontStyle.Bold),
            //                    System.Drawing.Color.FromArgb(26, 59, 105));
            //Chart1.Titles.Add(t);
            
            DataTable visit = Portal.BO.Action.VisitByHourV2(NewsID);
            int maxValue = 0, minValue = 0;
            string labelX = string.Empty,  strData = string.Empty;
            if (visit != null && visit.Rows.Count > 0)
            {
                
                for (int i = 0; i < visit.Rows.Count; i++)
                {
                    if (Convert.ToInt32(visit.Rows[i]["Visit"]) > maxValue)
                        maxValue = Convert.ToInt32(visit.Rows[i]["Visit"]);
                    //Chart1.Series["Series1"].Points.AddXY(Convert.ToDateTime(visit.Rows[i]["VisitHour"]).ToString("HH:ss"), visit.Rows[i]["Visit"]);
                    labelX += Convert.ToDateTime(visit.Rows[i]["VisitHour"]).ToString("HH") + "|";
                    strData += visit.Rows[i]["Visit"] + ",";
                }
            
            }
            StringBuilder strhtml = new StringBuilder();
            strhtml.Append("http://chart.apis.google.com/chart?");
            // mau nen cho bieu do
            strhtml.Append("chf=c,s,FFFFFF");
            //set ox, oy
           // Change = (maxValue - minValue) * 5 / 100;
           // List<double> colOy = Common.GetMaxMinOY(Convert.ToDouble(maxValue), Convert.ToDouble(minValue), Convert.ToDouble(Endpoint), Convert.ToDouble(Change));
            

            double MaxOy =maxValue;
            double MinOy = 0;
            double index1Oy = Math.Round((MaxOy - MinOy) / 4 + MinOy);
            double index2Oy = Math.Round((MaxOy - MinOy) / 2 + MinOy);
            double index3Oy = Math.Round(MaxOy - (MaxOy - MinOy) / 4);
            strhtml.AppendFormat("&chxl=0:|{0}|{1}|{2}|{3}|{4}|1:|{5}", MinOy, index1Oy, index2Oy, index3Oy, string.Format("{0:0.00}", MaxOy), labelX.TrimEnd('|'));
            // vi  tri ox va oy             
            //chua bit
            strhtml.AppendFormat("&chds={0},{1}", MinOy, MaxOy);
            // mau chu cho ox va oy
           // strhtml.AppendFormat("&chxs=0,{0},10,0,l,FFFFFF|1,{1},10,0,l,FFFFFF", "333333", "333333");
            // truc/
            strhtml.Append("&chxt=y,x&chbh=a&chs=550x250&cht=bvg&chco=3399CC,3D7930");
            // kick thuoc
            //strhtml.Append("&chs=800x250");
            // chua bit
           // strhtml.Append(" &chp=0.05");
            // mau cho duong ke tuong ung cac bieu do
            //StringBuilder strcolor = new StringBuilder();
            //strcolor.Append("&chco=");
            //strcolor.AppendFormat("{0}", "3D79E3C5");
            //// max min. co bao nhiu bieu do tuong ung bay nhiu cap max min

            strhtml.Append("&chco=A6C0F3,3D7930");
            strhtml.AppendFormat("&chd=t:{0}", strData.TrimEnd(','));
            //// chia do thi ra bao nhiu phan
            //strhtml.Append("&chg=0,25,2,2");
            //// boi mau
            //// chls duong net dut
            //strhtml.AppendFormat("&chls=1&chm=B,{0},0,0,0", "3399CC44"); ;
            //int it = 0;

            imgChart.ImageUrl = strhtml.ToString();

        }
    }
}
