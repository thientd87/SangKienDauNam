using System;
using System.Web.UI;
using System.ComponentModel;

namespace DFISYS.API.Controls
{
	/// <summary>
	/// Summary description for LanguageLinkButton.
	/// </summary>
	[DefaultProperty("LanguageRef"), 
		ToolboxData("<{0}:RequiredFieldValidator runat=server></{0}:RequiredFieldValidator>")]
	public class RequiredFieldValidator : System.Web.UI.WebControls.RequiredFieldValidator
	{
		public string LanguageRef = "";
		public string LanguageRefText = "";

		protected override void OnPreRender(EventArgs e)
		{
			if(LanguageRefText != "")
			{
				base.Text = DFISYS.API.Language.GetText(DFISYS.API.Module.GetModuleControl(this), LanguageRefText);
			}
			if(LanguageRef != "")
			{
				base.ErrorMessage = DFISYS.API.Language.GetText(DFISYS.API.Module.GetModuleControl(this), LanguageRef);
			}
			base.OnPreRender (e);
		}

	}
}
