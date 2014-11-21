using System;
using System.Web.UI;
using System.ComponentModel;

namespace DFISYS.API.Controls
{
	/// <summary>
	/// Summary description for LanguageLinkButton.
	/// </summary>
	[DefaultProperty("LanguageRef"), 
		ToolboxData("<{0}:Label runat=server></{0}:Label>")]
	public class Label : System.Web.UI.WebControls.Label
	{
		public string LanguageRef = "";

		protected override void OnPreRender(EventArgs e)
		{
			base.Text = DFISYS.API.Language.GetText(DFISYS.API.Module.GetModuleControl(this), LanguageRef);
			base.OnPreRender (e);
		}

	}
}
