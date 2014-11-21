using System;
using System.Web.UI;
using System.ComponentModel;

namespace DFISYS.API.Controls
{
	/// <summary>
	/// Summary description for LanguageLinkButton.
	/// </summary>
	[DefaultProperty("LanguageRef"), 
		ToolboxData("<{0}:CustomValidator runat=server></{0}:CustomValidator>")]
	public class CustomValidator : System.Web.UI.WebControls.CustomValidator
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
