using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using AddIns.BO;
using System.Text;

namespace AddIns.ajax {
    public partial class ListAdv : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack)
                BindData();
        }

        private void BindData() {
            //DataTable _ads = AdvHelper.GetAdvByPageAndPositions(Convert.ToInt32(Request.QueryString["page"]), Convert.ToInt32(Request.QueryString["pos"]));

            //grdList.DataSource = _ads;
            //grdList.DataBind();
        }

        protected override void Render(HtmlTextWriter output) {
            output.Write(RenderControlToString(divContainer));
        }

        public string RenderControlToString(Control ctr) {
            StringBuilder sb = new StringBuilder();
            System.IO.StringWriter sw = new System.IO.StringWriter(sb);
            HtmlTextWriter htmlWriter = new HtmlTextWriter(sw);
            ctr.RenderControl(htmlWriter);
            return sb.ToString();
        }

        public override void VerifyRenderingInServerForm(Control control) {
            return;
        }
    }
}