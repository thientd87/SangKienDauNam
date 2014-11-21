using System;
using System.Web.UI;
using System.ComponentModel;

namespace DFISYS.API.Controls
{
	/// <summary>
	/// Summary description for LanguageButton.
	/// </summary>
	[DefaultProperty("LanguageRef"), 
		ToolboxData("<{0}:Button runat=server></{0}:Button>")]
	public class Button : System.Web.UI.WebControls.Button
	{
		public string LanguageRef = "";

		protected override void OnPreRender(EventArgs e)
		{
			base.Text = DFISYS.API.Language.GetText(DFISYS.API.Module.GetModuleControl(this), LanguageRef);
			base.OnPreRender (e);
		}

	}
}
