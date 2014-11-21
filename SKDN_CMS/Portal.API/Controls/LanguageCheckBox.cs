using System;
using System.Web.UI;
using System.ComponentModel;

namespace DFISYS.API.Controls
{
	/// <summary>
	/// Summary description for LanguageButton.
	/// </summary>
	[DefaultProperty("LanguageRef"), 
	ToolboxData("<{0}:CheckBox runat=server></{0}:CheckBox>")]
	public class CheckBox : System.Web.UI.WebControls.CheckBox
	{
		public string LanguageRef = "";

		protected override void OnPreRender(EventArgs e)
		{
			base.Text = DFISYS.API.Language.GetText(DFISYS.API.Module.GetModuleControl(this), LanguageRef);
			base.OnPreRender (e);
		}

	}
}