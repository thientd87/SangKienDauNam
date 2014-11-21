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
using System.Xml;
using DFISYS.BO.Editoral.Newsedit;

namespace DFISYS.Ajax {
    public partial class ajax : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

            if (!IsPostBack) {
                var selectedItems = Request.Form["SelectedItems"];
                if (selectedItems != null && selectedItems.ToString() != "") {
                    NewsEditHelper.DeleteNewsCrawler(selectedItems.ToString());
                }
            }

        }
    }
}
