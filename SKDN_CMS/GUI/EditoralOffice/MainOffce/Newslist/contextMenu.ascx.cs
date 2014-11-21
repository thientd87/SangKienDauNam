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
using DFISYS.User.Security;

namespace DFISYS.GUI.EditoralOffice.MainOffce.Newslist
{
	public partial class contextMenu : System.Web.UI.UserControl
	{
		protected bool isSendDirectly = false;
	    protected bool isPublished = false;
	    public Role objrole;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				MainSecurity objSecu = new MainSecurity();
                objrole = objSecu.GetRole(Page.User.Identity.Name);
				isSendDirectly = objrole.isBienTapVien || objrole.isThuKyToaSoan || objrole.isThuKyChuyenMuc;
			    isPublished = objrole.isThuKyToaSoan || objrole.isThuKyChuyenMuc;
			}

		}
	}
}