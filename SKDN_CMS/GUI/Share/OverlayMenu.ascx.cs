namespace DFISYS.GUI.Share
{
	using System;
	using System.Collections;
	using System.Web.UI;
	using System.Web.UI.WebControls;

	/// <summary>
	///		Summary description for OverlayMenu.
	/// </summary>
	[ControlBuilder(typeof(OverlayMenuItemCtrlBuilder)), ParseChildren(false)]
	public partial  class OverlayMenu : System.Web.UI.UserControl, IPostBackEventHandler
	{
		public string RootText = "";

		private string languageRef = "";
		private string cssClass = "";

		public string LanguageRef
		{
			get { return languageRef; }
			set { languageRef = value; }
		}

		public string CssClass
		{
			get { return cssClass; }
			set { cssClass = value; }
		}

		ArrayList miList = new ArrayList();
	
		public ArrayList Items
		{
			get { return miList; }
		}


		protected override void AddParsedSubObject(object obj)
		{
			OverlayMenuItem m = obj as OverlayMenuItem;
			if(m != null)
			{
				m.MenuItemIndex = miList.Add(m);
			}
			else
			{
				base.AddParsedSubObject(obj);
			}
		}

		public void RaisePostBackEvent(string args)
		{
			int i = Int32.Parse(args);
			OverlayMenuItem mi = (OverlayMenuItem)miList[i];
			mi.InvokeClick();
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(languageRef != "")
				RootText = DFISYS.API.Language.GetText(languageRef);

			MenuRepeater.DataSource = miList;
			MenuRepeater.DataBind();

			ltrDisplayRootText.Visible = (RootText == "");
			
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

		}
		#endregion
	}

	public class OverlayMenuItem
	{
		private string text = "";
		public string Text
		{
			get 
			{
				if(languageRef == "")
					return text; 
				return DFISYS.API.Language.GetText(languageRef);
			}
			set { text = value; languageRef = ""; }
		}
		private string languageRef = "";
		public string LanguageRef
		{
			get { return languageRef; }
			set { languageRef = value; }
		}

		private string icon = "";
		public string Icon
		{
			get { return icon; }
			set { icon = value; }
		}

		private int menuItemIndex = -1;
		public int MenuItemIndex
		{
			get { return menuItemIndex; }
			set { menuItemIndex = value; }
		}

		public event EventHandler Click = null;
		public void InvokeClick()
		{
			if(Click != null)
			{
				Click(this, new EventArgs());
			}
		}

		private bool visible = true;
		public bool Visible
		{
			get { return visible; }
			set { visible = value; }
		}
	}

	public class OverlayMenuSeparatorItem : OverlayMenuItem
	{
	}

	public class OverlayMenuItemCtrlBuilder : System.Web.UI.ControlBuilder
	{
		public override Type GetChildControlType(String tagName,
			IDictionary attributes)
		{
			if (String.Compare(tagName, "MenuItem", true) == 0) 
			{
				return typeof(OverlayMenuItem);
			}
			if (String.Compare(tagName, "SeparatorItem", true) == 0) 
			{
				return typeof(OverlayMenuSeparatorItem);
			}
			return null;
		}
	}

}
