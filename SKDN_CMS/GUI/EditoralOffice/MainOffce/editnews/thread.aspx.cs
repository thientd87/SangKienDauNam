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
using DFISYS.BO.Editoral.Category;
namespace DFISYS.GUI.EditoralOffice.MainOffce.editnews
{
	public partial class thread : System.Web.UI.Page
	{
		private DFISYS.CoreBO.Threads.ThreadHelper t = new DFISYS.CoreBO.Threads.ThreadHelper();
		private void BindData()
		{
            //DataTable dt = t.GetListThread(cboCategory.SelectedValue);
            //gvData.DataSource = dt;
            //gvData.DataBind();


		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				CategoryHelper.TreebuildAllCat(cboCategory);
				string strCatId = Request.QueryString["CatID"] != null ? Request.QueryString["CatID"].ToString() : "";
				cboCategory.SelectedValue = strCatId;

				BindData();
			}

		}

		protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
		{ }



		protected void cboCategory_SelectedIndexChanged1(object sender, EventArgs e)
		{
			BindData();
		}
        protected void cboPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdThreadList.PageIndex = Convert.ToInt32(cboPage.SelectedValue);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            objThreadlistSource.SelectParameters[0].DefaultValue = " Title like N'%" + txtKeyword.Text.Trim() + "%' ";
        }
	}
}
