using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Portal.BO.Editoral.Newsedit;


namespace Portal.GUI.EditoralOffice.MainOffce.NewsCrawler {
    public partial class ImagesSuggestion : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            //Response.Write(Request.QueryString["source"]);

            if (!IsPostBack)
                BindData();
        }

        private void BindData() {
            int _id = 0;

            if (Int32.TryParse(Request.QueryString["ID"], out _id)) {
                DataTable _obj = NewsEditHelper.GetCrawlerNews(_id);

                if (_obj != null && _obj.Rows.Count > 0) {
                    DataRow row = _obj.Rows[0];

                    ltrTitle.Text = row["News_Title"].ToString();
                    ltrSapo.Text = row["News_InitContent"].ToString();

                    long NewsID = Convert.ToInt64(NewsHelper.GenNewsID());

                    //NewsEditHelper.CreateNews(NewsID, 0, string.Empty, row["News_Title"].ToString(), string.Empty, "Theo " + row["SourceName"], row["News_InitContent"].ToString(), row["News_Content"].ToString(), string.Empty, false, 0, 0, string.Empty, string.Empty, string.Empty, DateTime.Now, false, false, 0, string.Empty, string.Empty, string.Empty, false, string.Empty, string.Empty, row["News_Source"].ToString(), 0, string.Empty, string.Empty);

                    //NewsEditHelper.UpdateCrawlerStatus(_id);

                    //Response.Redirect("/office/add,templist/" + NewsID + ".aspx");
                }
            }
        }

    }
}