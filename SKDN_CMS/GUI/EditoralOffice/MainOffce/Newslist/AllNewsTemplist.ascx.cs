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
using Portal.BO.Editoral.Category;
using Portal.User.Security;
using Portal.BO.Editoral.Newslist;
using Portal.Core.DAL;
using System.IO;


namespace Portal.GUI.EditoralOffice.MainOffce.Newslist
{
    public partial class AllNewsTemplist : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CategoryHelper.Treebuild(ddlChuyenmuc);
                CategoryHelper.Treebuild(cboCategory);
            }
        }


        private void Filter()
        {
            // Reset cac gia tri cua hop TimKiem
            txtKeyword.Text = "";
            cboCategory.SelectedValue = "0";
            txtNguoiTao.Text = "";
            // End Reset

            string strWhere = "";
            string strCat = ddlChuyenmuc.SelectedValue;

            if (ddlChuyenmuc.SelectedValue != "0" && ddlChuyenmuc.SelectedValue != "")
            {
                strWhere = " AND Cat_ID = " + strCat;
            }

            if (txtCalendar.Text.Trim() != "" && txtToDate.Text.Trim() != "")
            {
                string sFromDate = txtCalendar.Text;
                if (sFromDate.Trim() != "")
                    sFromDate = sFromDate.Substring(3, 2) + "/" + sFromDate.Substring(0, 2) + "/" + sFromDate.Substring(6, 4);

                string sToDate = txtToDate.Text;
                if (sToDate.Trim() != "")
                    sToDate = sToDate.Substring(3, 2) + "/" + sToDate.Substring(0, 2) + "/" + sToDate.Substring(6, 4) + " 23:59.999";

                strWhere += " AND News_CreateDate Between '" + sFromDate + "' AND '" + sToDate + "'";
            }

            objListNewsSource.SelectParameters["strWhere"].DefaultValue = strWhere;
        }

        protected void ddlChuyenmuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filter();
        }

        protected void btnFilter_Click(object sender, ImageClickEventArgs e)
        {
            Filter();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ddlChuyenmuc.SelectedValue = "0"; 
            txtToDate.Text = ""; txtCalendar.Text = "";

            string strWhere = "";
            if (cboCategory.SelectedValue != "0" && cboCategory.SelectedValue != "")
            {
                strWhere = " AND Cat_ID = " + cboCategory.SelectedValue;
            }

            if (txtKeyword.Text.Trim() != "")
            {
                strWhere += " AND News_Title like N'%" + txtKeyword.Text.Trim() + "%'";
            }

            if (txtNguoiTao.Text.Trim() != "")
            {
                strWhere += " AND News_Author like N'%" + txtNguoiTao.Text.Trim() + "%'";
            }
            objListNewsSource.SelectParameters["strWhere"].DefaultValue = strWhere;
        }

        protected void grdListNews_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            grdListNews.PageIndex = e.NewSelectedIndex;
        }

        protected void grdListNews_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header && e.Row.RowType != DataControlRowType.Footer)
            {
                try
                {
                    DropDownList cboIsHot = (DropDownList)e.Row.FindControl("cboIsHot");
                    cboIsHot.SelectedValue = DataBinder.Eval(e.Row.DataItem, "News_Mode").ToString();
                }
                catch { }
            }
        }
    }
}