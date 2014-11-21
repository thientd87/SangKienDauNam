namespace DFISYS.GUI.Share.SearchInputSmall
{
	using System;
	/// <summary>
	///		Summary description for SeachSmall.
	/// </summary>
	public delegate void SearchedEventHandle(object sender, EventArgs e);
	public partial class SearchSmall : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Button btnSearch;
		protected string mTextSearch;
		protected string mTypeSearch;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			searchBtn.Attributes.Add("Onclick","goSearch();");
			txtSearch.Attributes.Add("Onkeydown","goKeySearch();") ;
			if(!IsPostBack)
				txtSearch.Text ="";
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
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
