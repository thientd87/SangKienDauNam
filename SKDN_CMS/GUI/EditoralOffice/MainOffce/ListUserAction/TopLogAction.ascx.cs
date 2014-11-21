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
using System.Web.Caching;

namespace Portal.GUI.EditoralOffice.MainOffce.ListUserAction
{
    public partial class TopLogAction : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            objListNewsSource.SelectParameters["strWhere"].DefaultValue = " AND Type = " + Portal.LogAction.LogType_BaiViet_XB;

            if (!IsPostBack)
            {
                string cacheName = "GetListLogActionCount";
                object strCount =  HttpContext.Current.Cache.Get(cacheName);
                if (strCount == null)
                {
                    strCount = Portal.BO.Editoral.UserActionHelper.ActionHelper.GetListLogActionCount(" AND LogDate >= '" + DateTime.Today.ToString("yyyy/MM/dd") + "' AND  LogDate <= getdate() AND Type = " + Portal.LogAction.LogType_BaiViet_XB ).ToString();
                    HttpContext.Current.Cache.Insert(cacheName, strCount, null, DateTime.Now.AddMinutes(10), Cache.NoSlidingExpiration);
                }
                ltrCount.Text = strCount.ToString();
            }
            
        }
    }
}