using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.GUI.EditoralOffice.MainOffce.Feedback
{
    public partial class FeedbackManger : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LinkDelete_Click(object sender, EventArgs e)
        {
            string strUnIDs = hCheckedIDs.Value;
            objSupport.DeleteParameters["ID"].DefaultValue = strUnIDs;
            objSupport.Delete();
            grdListNews.DataBind();
        }
    }
}