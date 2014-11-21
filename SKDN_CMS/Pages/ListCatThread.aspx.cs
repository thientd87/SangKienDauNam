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
namespace Portal
{
    public partial class ListCatThread : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable table = new DataTable();   
                String IsCat = Request.QueryString["IsCat"];
                if (IsCat == "true")
                    using (MainDB db = new MainDB())
                        //table = db.StoredProcedures_Family.vc_Sql_Run("SELECT Cat_ID ID,Cat_Name Title FROM Category WHERE Cat_ID NOT IN (" + Request.QueryString["ID"] + ")");
                        table = Portal.BO.Editoral.Category.CategoryHelper.GetListCategory(Request.QueryString["ID"].ToString());

                else
                    using (MainDB db = new MainDB())
                        table = db.StoredProcedures_Family.vc_Sql_Run("SELECT Thread_ID ID,Title FROM NewsThread WHERE Thread_ID NOT IN (" + Request.QueryString["ID"] + ")");

                gvData.DataSource = table;
                gvData.DataBind();
            }
        }

        protected void btnChon_Click(object sender, EventArgs e)
        {
            String id = Request.Form["id"] != null ? Request.Form["id"].ToString() : "";

            String js = String.Empty;
            js += "<script language='javascript'>";
            js += "window.opener.bindItem('" + Request.QueryString["Control"] + "','" + id + "','" + Request.QueryString["Hid"] + "'," + Request.QueryString["IsCat"] + ");self.close();";
            js += "</script>";

            Page.RegisterStartupScript("control", js);
        }


    }
}
