using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DFISYS.BO.Editoral.Category;
using Portal.BO.Editoral.ThongKeBaiViet;

namespace Portal.GUI.EditoralOffice.MainOffce.Thongke
{
    public partial class ThongKeBaiXemNhieuNhat : System.Web.UI.UserControl
    {
        private void BindData()
        {
            ThongKeBaiVietHelper viet = new ThongKeBaiVietHelper();
            DateTime fromdate = Convert.ToDateTime(this.txtfromDate.Text, new CultureInfo(1066));
            DateTime todate = Convert.ToDateTime(this.txttoDate.Text, new CultureInfo(1066));
            int Top = Convert.ToInt32(dllPageCount.SelectedValue);
            DataTable dtAll = viet.ThongKeBaiXemNhieuNhat(fromdate.ToString("yyyy/MM/dd"), todate.ToString("yyyy/MM/dd"),
                                                         Convert.ToInt32(ddlChuyenmuc.SelectedValue), Top);
            if(dtAll!=null)
            {
                rptListnew.DataSource = dtAll;
                rptListnew.DataBind();
            }

        }

        protected void btnXem_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CategoryHelper.Treebuild(ddlChuyenmuc);
                txtfromDate.Text = DateTime.Now.AddDays(-30.0).ToString("dd/MM/yyyy");
                txttoDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                BindData();
            }
        }
    }
}