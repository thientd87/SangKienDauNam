using System;
using System.Web;
using System.Web.UI;
using System.Data;
using DFISYS.BO.Editoral.Category;
using DFISYS.BO.Editoral.Newslist;

namespace DFISYS.GUI.EditoralOffice.MainOffce.PublishedList {
    public partial class PublishedList : UserControl {
        private int pageSize = 400;

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack)
            {
                CategoryHelper.Treebuild(ddlChuyenmuc);
                BindData();
            }
                
        }

        private void BindData() {
            DataTable list = NewslistHelper.SelectNewsPublishedForView(0, pageSize);
            grdListNews.DataSource = list;
            grdListNews.DataBind();
        }

        protected void grdListNews_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e) {
            grdListNews.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtKey.Text.Trim()) || ddlChuyenmuc.SelectedValue!="0")
            {
                DataTable list = NewslistHelper.SelectNewsPublishedForViewSearch("0", pageSize.ToString(), txtKey.Text, ddlChuyenmuc.SelectedValue);
                grdListNews.AllowPaging = false;
                grdListNews.DataSource = list;
                grdListNews.DataBind();

            }
           
        }
    }
}