namespace Portal.GUI.Administrator.AdminPortal
{
	using System;
	using System.Collections;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for ModuleList.
	/// </summary>
	public partial  class ModuleList : System.Web.UI.UserControl
	{
		/// <summary>
		/// Wrapper Class for the Tab Object.
		/// </summary>
		public class DisplayModuleItem
		{
			/// <summary>
			/// Tabs Text
			/// </summary>
			public string Title
			{
				get { return m_Title; }
			}
			/// <summary>
			/// Tabs Reference
			/// </summary>
			public string Reference
			{
				get { return m_Reference; }
			}
			/// <summary>
			/// Modules Type
			/// </summary>
			public string ModuleType
			{
				get { return m_ModuleType; }
			}

			/// <summary>
			/// Column Reference
			/// </summary>
			public string ColumnReference
			{
				get { return m_ColumnReference; }
			}

			internal string m_ColumnReference = "";
			internal string m_Title = "";
			internal string m_Reference = "";
			internal string m_ModuleType = "";
		}

		protected Portal.API.Controls.LinkButton lnkEditColumn;
		public string TitleLanguageRef = "";
		private ArrayList moduleList = null;

		public string ContainerColumnReference
		{
			get{return (lblColumnReference.Value != null && lblColumnReference.Value != "") ? lblColumnReference.Value : "";}
			set{lblColumnReference.Value = value;}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			divTitle.InnerText = Portal.API.Language.GetText(
				Portal.API.Module.GetModuleControl(this), TitleLanguageRef);
		}

		protected override void LoadViewState(object bag)
		{
			base.LoadViewState(bag);
			moduleList = (ArrayList)ViewState["moduleList"];
		}
		protected override object SaveViewState()
		{
			ViewState["moduleList"] = moduleList;
			return base.SaveViewState();
		}

		public void LoadData(ArrayList modules)
		{
			// Init Data
			moduleList = modules;
			Bind();
		}

		public void LoadData(string _strColumnRef)
		{
			PortalDefinition _objPortal = PortalDefinition.Load();
			PortalDefinition.Column _objColumn = _objPortal.GetColumn(_strColumnRef);

			if (_objColumn != null)
			{
				moduleList = _objColumn.ModuleList;
				Bind();
			}
		}

		private void Bind()
		{
			ArrayList bindList = new ArrayList();
			foreach(PortalDefinition.Module m in moduleList)
			{
				DisplayModuleItem dt = new DisplayModuleItem();
				bindList.Add(dt);

				dt.m_Title = m.title;
				dt.m_Reference = m.reference;
				dt.m_ModuleType = m.type;
				dt.m_ColumnReference = (lblColumnReference.Value != null && lblColumnReference.Value != "") ? lblColumnReference.Value : "";
			}
			gridModules.DataSource = bindList;
			gridModules.DataKeyField = "ColumnReference";
			gridModules.DataBind();
		}

		protected void OnEditModule(object sender, CommandEventArgs args)
		{
			((Tab)Parent).EditModule((string)args.CommandArgument);
			//ParentReference.EditModule((string)args.CommandArgument);
		}

		protected void OnAddModule(object sender, CommandEventArgs e)
		{
			((Tab)Parent).AddModule(e.CommandArgument.ToString());
			//ParentReference.AddModule(this);
		}

		protected void OnModuleUp(object sender, CommandEventArgs args)
		{
			int idx = Int32.Parse((string)args.CommandArgument);
			((Tab)Parent).MoveModuleUp(idx, this);
			//ParentReference.MoveModuleUp(idx, this);
		}
		protected void OnModuleDown(object sender, CommandEventArgs args)
		{
			int idx = Int32.Parse((string)args.CommandArgument);
			((Tab)Parent).MoveModuleDown(idx, this);
			//ParentReference.MoveModuleDown(idx, this);
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
}
