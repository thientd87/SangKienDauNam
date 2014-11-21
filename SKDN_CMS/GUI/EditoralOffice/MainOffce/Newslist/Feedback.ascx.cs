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

namespace Portal.GUI.EditoralOffice.MainOffce.Newslist
{
	public partial class Feedback : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		protected void cmdYes_Click(object sender, EventArgs e)
		{
			string strcpmode = Request.QueryString["cpmode"].ToString();
			string _news_id = hdID.Value;
			long intNID = Convert.ToInt64(_news_id);
			objListActionSource.UpdateParameters[0].DefaultValue = _news_id;
			objListActionSource.UpdateParameters[2].DefaultValue = "" + Portal.BO.Editoral.Newslist.NewslistHelper.getLastestStatus(intNID);
			objListActionSource.Update();
		}
	}
}