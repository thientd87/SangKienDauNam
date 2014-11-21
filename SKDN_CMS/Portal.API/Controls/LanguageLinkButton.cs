using System;
using System.Web.UI;
using System.ComponentModel;

namespace DFISYS.API.Controls
{
	/// <summary>
	/// Summary description for LanguageLinkButton.
	/// </summary>
	[DefaultProperty("LanguageRef"), 
		ToolboxData("<{0}:LinkButton runat=server></{0}:LinkButton>")]
	public class LinkButton : System.Web.UI.WebControls.LinkButton
	{
		public string LanguageRef = "";

		protected override void OnPreRender(EventArgs e)
		{
			base.Text = DFISYS.API.Language.GetText(DFISYS.API.Module.GetModuleControl(this), LanguageRef);
			base.OnPreRender (e);
		}

	}
}
