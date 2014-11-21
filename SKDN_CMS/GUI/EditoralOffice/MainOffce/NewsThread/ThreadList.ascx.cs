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

using Portal.Core.DAL;

namespace Portal.GUI.EditoralOffice.MainOffce.NewsThread
{
    public partial class ThreadList : System.Web.UI.UserControl
    {
        private ChannelVN.CoreBO.Threads.ThreadHelper t = new ChannelVN.CoreBO.Threads.ThreadHelper();
        private void BindData()
        {
            DataTable table = new DataTable();
            using (MainDB db = new MainDB())
                table = db.StoredProcedures_Family.vc_Sql_Run("SElECT Thread_ID,Title FROM NewsThread");
            gvData.DataSource = table;
            gvData.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                BindData();

        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String Thread_ID = e.CommandArgument.ToString();
            if (e.CommandName.Equals("editCat"))
                Response.Redirect("/office/newsthread/" + Thread_ID + ".aspx");
            else if (e.CommandName.Equals("del"))
            {
                t.DelThread(Thread_ID);
                BindData();
            }
        }

        protected void cmdThemMoi_Click(object sender, EventArgs e)
        {
            Response.Redirect("/office/newsthread.aspx");
        }
    }
}