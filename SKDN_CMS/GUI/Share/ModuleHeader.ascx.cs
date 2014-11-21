
namespace DFISYS.GUI.Share
{
	using System;

	/// <summary>
	///	This is the Modules Header Control. Loaded by the RenderTab.ascx Control.
	///	Makes also the EditLink invisible if the user has no rights or there is no
	///	edit module.
	/// </summary>
	public abstract class ModuleHeader : System.Web.UI.UserControl
	{
		protected PortalDefinition.Module ModuleDef;
		protected EditLink lnkEditLink;

		/// <summary>
		/// Initializes the Control
		/// </summary>
		/// <param name="md"></param>
		internal void SetModuleConfig(PortalDefinition.Module md)
		{
			ModuleDef = md;
			lnkEditLink.ModuleRef = md.reference;
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Set the link per default invisible
			bool showedit = false;
            //if(UserManagement.HasEditRights(Page.User, ModuleDef.roles))
            //{
            //    // User has right, continue
            //    if(ModuleDef.moduleSettings == null)
            //    {
            //        // no Module Settings, set visible
            //        showedit = true;
            //    }
            //    else
            //    {
            //        // Module has module settings
            //        if(ModuleDef.moduleSettings.HasEditCtrl)
            //        {
            //            // Module has a edit control, set visible
            //            showedit = true;
            //        }
            //    }
            //}

			if(Page.User.IsInRole(DFISYS.API.Config.AdminRoles))
			{
				lnkEditLink.Visible = false;
			}
			else
			{
				lnkEditLink.Visible = showedit;
			}
		}
		

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
