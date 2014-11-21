using System;
using System.IO;
using DFISYS.API;
namespace DFISYS
{
	/// <summary>
	/// Startup Page. Redirects in dependents of the PortalType to the Pages "RenderTable.aspx" or "FrameSet.htm"
	/// </summary>
	public partial class StartPage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{			
                //PortalDefinition _objPD = PortalDefinition.Load();
                //if (_objPD != null && _objPD.tabs != null && _objPD.tabs.Count > 0)
                //{
                //    ChannelUsers objUser = new ChannelUsers();
                //    foreach (PortalDefinition.Tab _objTab in _objPD.tabs)
                //    {
                //        if (!_objTab.IsHidden && objUser.HasViewRights(Page.User, _objTab.roles))
                //        {
                //            string _strMainPage = Config.GetTabURL(_objTab.reference);
                //            Response.Redirect(_strMainPage, false);
                //            return;						
                //        }
                //    }
                //    Response.Redirect("/login.aspx");
                //}
                //else
                //{
                //    Response.Redirect( Config.GetMainPage(Page.User.Identity.Name));
                //}            
            Response.Redirect("/office.aspx");
		}

		override protected void OnInit(EventArgs e)
		{
			
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
	}
}
