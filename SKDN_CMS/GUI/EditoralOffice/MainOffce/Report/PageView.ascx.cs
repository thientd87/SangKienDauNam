using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.CoreBO;
using DFISYS.BO.Editoral.Category;
using System.Data;
using System.Globalization;

namespace DFISYS.GUI.EditoralOffice.MainOffce.Report
{
    public partial class PageView : System.Web.UI.UserControl
    {
        ReportPageView objPageView = new ReportPageView();
        private int pageSize = 400;
        private string listCat = "";
        private DataTable dtCat = new DataTable();

        public string firstDate = "", endDate = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string keycache = "GetListCategory";
            dtCat = Cache[keycache] as DataTable;
            if (dtCat == null)
            {
                dtCat = CategoryHelper.GetListCategory("");
                Cache.Insert(keycache, dtCat, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
            }
            if (!IsPostBack)
            {
               
                for (int i = 0; i < dtCat.Rows.Count; i++)
                {
                    dtCat.Rows[i]["Cat_Name"] = dtCat.Rows[i]["Cat_Name"].ToString().Replace("&nbsp;", "");
                }
                dtCat.AcceptChanges();
                DateTime now = DateTime.Now;

                txtFromDate.Text = "01/" + now.Month.ToString() + "/" + now.Year.ToString();
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CategoryHelper.Treebuild(ddlChuyenmuc);
                initListCat();
                loadData();
            }
        }

        private void initListCat()
        {
            if (ddlChuyenmuc.SelectedValue != "")
            {
                listCat = ddlChuyenmuc.SelectedValue;
                listCat += ","+ CategoryHelper.GetChildCatIdByCatParentId(Convert.ToInt32(ddlChuyenmuc.SelectedValue));
            }
            else
            {
                listCat = "0";
            }
        }

        public String GetNameCat(int catID)
        {
            for (int i = 0; i < dtCat.Rows.Count; i++)
            {
                if (dtCat.Rows[i]["Cat_ID"].ToString() == catID.ToString())
                {
                    return dtCat.Rows[i]["Cat_Name"].ToString();
                }
            }
            return "";
        }

        protected void grdListNews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdListNews.Rows[index];
                grdListNews.EditIndex = index;
                var txtContent = row.FindControl("txtContent") as TextBox;
                if (txtContent != null) txtContent.Text = row.Cells[2].Text;
            }  
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            //grdListNews.DataSource = comment.GetAll(40, Convert.ToInt32(ddlPageUp.SelectedValue), 0, Convert.ToInt32(ddlChuyenmuc.SelectedValue), txtKeyword.Text);
            //grdListNews.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
        }

        private void loadData()
        {
            if (ddlChuyenmuc.SelectedValue != "")
            {
                listCat = ddlChuyenmuc.SelectedValue;

                DataTable dt = CategoryHelper.GetCategoriesByParent(Convert.ToInt32(ddlChuyenmuc.SelectedValue));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listCat += "," + Convert.ToUInt32(dt.Rows[i]["Cat_ID"]).ToString();
                }
            }
            else
            {
                listCat = "0";
            }





            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now;
            firstDate = txtFromDate.Text.Trim();
            endDate = txtToDate.Text.Trim();
            fromDate = Convert.ToDateTime(txtFromDate.Text.Trim(), new CultureInfo(1066));
            toDate = Convert.ToDateTime(txtToDate.Text.Trim(), new CultureInfo(1066));
            List<ReportPageView> lsPageView = objPageView.Select(fromDate, toDate, listCat);

            Int32 totalView = 0;
            if (totalView <= 0)
            {
                foreach (var item in lsPageView)
                {
                    totalView += item.PageView_Count;
                }
            }
            Session["totalView"] = totalView;

            grdListNews.DataSource = lsPageView;
            grdListNews.DataBind();
        }

        protected void ddlChuyenmuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            loadData();
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            loadData();
        }

        protected void grdListNews_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            int totalView = 0;
            try
            {
                totalView = Convert.ToInt32(Session["totalView"]);
            }
            catch (Exception)
            {
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal ltr = e.Row.FindControl("ltrName") as Literal;
                if (ltr != null)
                {
                    ReportPageView rowView = (ReportPageView)e.Row.DataItem;
                    ltr.Text = GetNameCat(rowView.PageView_Cat);
                }
                Literal ltrPT = e.Row.FindControl("ltrPhanTram") as Literal;
                if (ltrPT != null)
                {
                    ReportPageView rowView = (ReportPageView)e.Row.DataItem;
                    string f="<td style='width:{0}%; background:blue; height:10px;'>&nbsp;</td><td style='padding-left:5px'>{0}%</td>";
                    decimal pt = (decimal)rowView.PageView_Count / totalView * 100;
                    ltrPT.Text = string.Format(f, Math.Round(pt, 2));
                }
            }
        }

    }
}