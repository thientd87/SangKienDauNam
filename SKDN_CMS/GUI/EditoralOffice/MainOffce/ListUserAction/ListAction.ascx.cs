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

namespace Portal.GUI.EditoralOffice.MainOffce.ListUserAction
{
    public partial class ListAction : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (txtKeyword.Text.Trim() != "")
                objListNewsSource.SelectParameters["strWhere"].DefaultValue = " AND Action like N'%"+ txtKeyword.Text.Trim() +"%' ";
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            objListNewsSource.SelectParameters["strWhere"].DefaultValue = " AND Action like N'%" + txtKeyword.Text.Trim() + "%' ";
        }
    }
}