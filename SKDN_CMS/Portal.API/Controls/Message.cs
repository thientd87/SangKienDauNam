using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace DFISYS.API.Controls
{
	/// <summary>
	/// Summary description for Message.
	/// </summary>
	[DefaultProperty("Error"), 
		ToolboxData("<{0}:Message runat=server></{0}:Message>")]
	public class Message : System.Web.UI.WebControls.WebControl
	{
		private string error = "";
		public string Error
		{
			get
			{
				return error;
			}

			set
			{
				error = value;
			}
		}
		private string success = "";
		public string Success
		{
			get
			{
				return success;
			}

			set
			{
				success = value;
			}
		}
		private string info = "";
		public string Info
		{
			get
			{
				return Info;
			}

			set
			{
				Info = value;
			}
		}

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			if(error != "")
			{
				output.Write("<pre class=\"Error\">{0}</pre>", error);
			}
			else if(success != "")
			{
				output.Write("<pre class=\"Success\">{0}</pre>", success);
			}
			else if(info != "")
			{
				output.Write("<pre class=\"Info\">{0}</pre>", info);
			}
		}
	}
}
