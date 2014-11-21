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

namespace MobileShop.GUI.Back_End.Order
{
    public partial class ListOrderOverDate : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void gvListProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.ToString().ToLower() == "delete")
            {
                objListOrder.DeleteParameters["O_ID"].DefaultValue = e.CommandArgument.ToString();
                objListOrder.Delete();
            }
        }


        protected void LinkDelete_Click(object sender, EventArgs e)
        {
            string strIDs = hCheckedIDs.Value.ToString();
            objListOrder.DeleteParameters["O_ID"].DefaultValue = strIDs;
            objListOrder.Delete();
        }
    }
}