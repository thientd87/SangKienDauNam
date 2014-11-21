using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.BO.Editoral.Newsedit;
using System.Data;

namespace Portal.GUI.EditoralOffice.MainOffce.NewsCrawler {
    public partial class CrawlerList : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack)
                BindData();
        }

        private void BindData() {
            DataTable _list = NewsEditHelper.GetListCrawlerNews();

            grdList.DataSource = _list;
            grdList.DataBind();
        }

        protected void grdList_RowCommand(object sender, GridViewCommandEventArgs e) {
            if (e.CommandName.Equals("Copy")) {
                int _id = Convert.ToInt32(e.CommandArgument);

                DataTable _obj = NewsEditHelper.GetCrawlerNews(_id);

                if (_obj != null && _obj.Rows.Count > 0) {
                    DataRow row = _obj.Rows[0];
                    long NewsID = Convert.ToInt64(NewsHelper.GenNewsID());
                    NewsEditHelper.CreateNews(NewsID, 0, string.Empty, row["News_Title"].ToString(), string.Empty, "Theo " + row["SourceName"], row["News_InitContent"].ToString(), row["News_Content"].ToString(), HttpContext.Current.User.Identity.Name, false, 0, 0, string.Empty, string.Empty, string.Empty, DateTime.Now, false, false, 0, string.Empty, string.Empty, string.Empty, false, string.Empty, string.Empty, row["News_Source"].ToString(), 0, string.Empty, string.Empty);

                    NewsEditHelper.UpdateCrawlerStatus(_id);

                    Response.Redirect("/office/add,templist/" + NewsID + ".aspx");
                }
                else
                    Response.Redirect("/office/crawler.aspx");
            }
        }

        protected void grdList_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            grdList.PageIndex = e.NewPageIndex;

            BindData();
        }


    }
}