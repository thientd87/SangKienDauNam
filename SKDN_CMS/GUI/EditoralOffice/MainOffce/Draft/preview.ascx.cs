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

namespace Portal.GUI.EditoralOffice.MainOffce.Draft
{
    public partial class preview : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string temp_id = Request.QueryString["temp_id"] != null ? Request.QueryString["temp_id"].ToString() : "-1";
            if (temp_id != "-1")
            {
                DataTable dt = Portal.BO.Editoral.Draft.DraftHelper.GetNewsTempDetailByTempID(temp_id);
                if(dt != null && dt.Rows.Count > 0)
                {
                    ltrContent.Text = dt.Rows[0]["news_content"].ToString();
                    ltrSapo.Text = dt.Rows[0]["news_sapo"].ToString();
                    ltrTitle.Text = dt.Rows[0]["news_title"].ToString();
                    ltrImage.Text = "<img src='/" + dt.Rows[0]["news_image"] + "' width='130px'>";
                }
                
            }
        }
    }
}