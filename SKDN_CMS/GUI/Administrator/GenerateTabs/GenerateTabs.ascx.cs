using System.Collections;
using System.Web.UI;
using Portal.API;
using Portal.Core.DAL;
namespace Portal.GUI.Administrator.GenerateTabs
{
	using System;
	using System.Data;
	using System.Web.UI.WebControls;

	/// <summary>
	///		Summary description for GenerateTabs.
	/// </summary>
	public partial class GenerateTabs : System.Web.UI.UserControl
	{
		protected Portal.API.Controls.Label Label1;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!IsPostBack)
			{
				LoadEditionTypesList();
				LoadCategory();
			}
		}

		#region Bind Data
		/// <summary>
		/// Thủ tục nạp danh sách chuyên san
		/// </summary>
		private void LoadEditionTypesList()
		{
			// Lấy danh sách chuyên san
			DataTable _objAllEditionTypes = null;
			using (MainDB _objDB = new MainDB())
			{
				_objAllEditionTypes = _objDB.EditionTypeCollection.GetAllAsDataTable();
			}

			// Hiển thị danh sách
			if (_objAllEditionTypes != null)
			{
				dgrEditionTypes.DataSource = _objAllEditionTypes;
				dgrEditionTypes.DataKeyField = EditionTypeCollection.EditionType_IDColumnName;
				dgrEditionTypes.DataBind();
			}
		}

		/// <summary>
		/// Thủ tục nạp danh sách các Categories cha
		/// </summary>
		/// <param name="_intEditionTypes">Mã tham chiếu đến chuyên san chứa Category cha</param>
		/// <param name="dgrCatList">Điều khiển hiện thị danh sách Categories</param>
		private void LoadCategoriesList(int _intEditionTypes, DataGrid dgrCatList)
		{
			// Lấy danh sách Category cha
			DataTable _objEditionCategories = null;
			using (MainDB _objDB = new MainDB())
			{
                _objEditionCategories = _objDB.CategoryCollection.GetAsDataTable("EditionType_ID = " + _intEditionTypes.ToString() + " AND ( Cat_ParentID=0 OR Cat_ParentID IS NULL)", "Cat_Order");
				/*_objEditionCategories = _objDB.SelectQuery(
					"SELECT     TOP 100 PERCENT b.Cat_Name AS ParentName, b.Cat_DisplayURL AS ParentDisplayURL, a.Cat_ID, a.Cat_Name, a.Cat_DisplayURL, a.Cat_ParentID, " +
                    "b.Cat_Name + '_' + a.Cat_Name AS Cat_FullName " + 
					"FROM         dbo.Category a INNER JOIN " + 
                    "dbo.Category b ON a.Cat_ParentID = b.Cat_ID " +
                    "ORDER BY a.Cat_ParentID");*/
			}

			// Hiển thị danh sách
			if (_objEditionCategories != null)
			{
				dgrCatList.DataSource = _objEditionCategories;
				dgrCatList.DataKeyField = CategoryCollection.Cat_IDColumnName;
				dgrCatList.DataBind();
			}
		}

		/// <summary>
		/// Thủ tục hiển thị danh sách Categories con
		/// </summary>
		/// <param name="_intParentCategoryID">Mã tham chiếu đến Category cha</param>
		/// <param name="dgrCatList"></param>
		private void LoadSubCategories(int _intParentCategoryID, DataGrid dgrCatList)
		{
			DataTable _objEditionCategories = null;
			using (MainDB _objDB = new MainDB())
			{
				//_objEditionCategories = _objDB.CategoryCollection.GetAsDataTable("Cat_ParentID = " + _intParentCategoryID, "");
				_objEditionCategories = _objDB.SelectQuery(
                    "SELECT     a.Cat_ID AS ParentCat_ID, a.Cat_Name AS Parent_CatName, a.Cat_DisplayURL as ParentCat_DisplayURL, " +
					"a.Cat_DisplayURL + '.' + b.Cat_DisplayURL as SubCat_DisplayURL, b.Cat_ID AS SubCat_ID, b.Cat_Name AS SubCat_Name, b.EditionType_ID " +
					"FROM         dbo.Category a INNER JOIN " +
                    "dbo.Category b ON a.Cat_ID = b.Cat_ParentID " +
					"Where a.Cat_ID = " + _intParentCategoryID.ToString() + " Order By b.Cat_Order"
				);
			}

			if (_objEditionCategories != null)
			{
				dgrCatList.DataSource = _objEditionCategories;
				dgrCatList.DataKeyField = "SubCat_ID";
				dgrCatList.DataBind();
			}
		}

		private void LoadCategory()
		{
			PortalDefinition _objPortal = PortalDefinition.Load();
			ltrCurrentTemplateTabReference.Text = _objPortal.TemplateReference;
			foreach(PortalDefinition.Tab _objTab in _objPortal.tabs)
			{
				ShowTabItem(_objTab, drdTabsList);
			}
		}

		private void ShowTabItem(PortalDefinition.Tab _objTab, System.Web.UI.WebControls.DropDownList _ddrTabsList)
		{
			ListItem _objNewTab = new ListItem((_objTab.parent == null ? "" : "----") + _objTab.title, _objTab.reference);
			_ddrTabsList.Items.Add(_objNewTab);

			foreach(PortalDefinition.Tab _objSubTab in _objTab.tabs)
			{
				ShowTabItem(_objSubTab, _ddrTabsList);
			}
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
			this.dgrEditionTypes.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgrEditionTypes_ItemDataBound);

		}
		#endregion

		private void dgrEditionTypes_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				DataGrid _dgrCategories = (DataGrid) e.Item.FindControl("dgrCatList");
				_dgrCategories.ItemDataBound += new DataGridItemEventHandler(_dgrCategories_ItemDataBound);
				if (_dgrCategories != null)
				{
					LoadCategoriesList(Convert.ToInt32(dgrEditionTypes.DataKeys[e.Item.ItemIndex]), _dgrCategories);
				}
			}
		}

		private void _dgrCategories_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				DataGrid _dgrSubCategories = (DataGrid) e.Item.FindControl("dgrSubCategories");
				_dgrSubCategories.ItemDataBound += new DataGridItemEventHandler(_dgrSubCategories_ItemDataBound);
				
				if (_dgrSubCategories != null)
				{
					LoadSubCategories(Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Cat_ID")), _dgrSubCategories);
				}

				Literal _ltrURL = e.Item.FindControl("ltrTopCategoryURL") as Literal;
				if (_ltrURL	!= null)
				{
                    _ltrURL.Text += Config.CustomExtension;
				}

				System.Web.UI.HtmlControls.HtmlImage _imgShowSubCats = e.Item.FindControl("imgShowHideSubCatList") as System.Web.UI.HtmlControls.HtmlImage;
				if (_imgShowSubCats != null)
				{
					System.Web.UI.HtmlControls.HtmlTableCell _htcSubCats = e.Item.FindControl("htcSubCatList") as System.Web.UI.HtmlControls.HtmlTableCell;
					_imgShowSubCats.Attributes.Add("onClick", "ShowHide(" + _htcSubCats.ClientID + ");");
				}
				
			}
		}

		private void _dgrSubCategories_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				Literal _ltrURL = e.Item.FindControl("ltrCategoryURL") as Literal;
				if (_ltrURL	!= null)
				{
                    _ltrURL.Text += Config.CustomExtension;
				}
			}
		}
		#endregion

		#region Generation Methods
		private void UpdateTabs(TemplateApplyingMode _objGenerateMode)
		{
			PortalDefinition _objPortal = PortalDefinition.Load();
			EditionTypeRow[] _arrAllEditionTypes = null;
			
			using (MainDB _objDB = new MainDB())
			{
				_arrAllEditionTypes = _objDB.EditionTypeCollection.GetAll();
			}

			if (_arrAllEditionTypes != null)
			{
				foreach(EditionTypeRow _objEdition in _arrAllEditionTypes)
				{
					PortalDefinition.Tab _objCurrentEdition = _objPortal.GetTab(_objEdition.EditionDisplayURL);
					ArrayList _arrTemplateColumns = _objPortal.CloneToTemplateTab(_objPortal.TemplateColumns) as ArrayList;
					ReGenModuleID(ref _arrTemplateColumns);

					if (_objCurrentEdition != null)
					{
						// Nếu cho phép cập nhật chuyên san thì cần xét thêm chuyên san phải khác với chuyên san mẫu
						if (_objGenerateMode.EditionType && _objCurrentEdition.reference != _objPortal.TemplateReference)
						{
							_objCurrentEdition.Columns = _arrTemplateColumns;
							_objCurrentEdition.title = _objEdition.EditionName;
						}
						AddCategoryTab(_objPortal, _objCurrentEdition.tabs, _objEdition.EditionType_ID.ToString(), null, _objEdition.EditionDisplayURL, _objGenerateMode);
					}
					else
					{
						PortalDefinition.Tab _objNewEditionTab = PortalDefinition.Tab.Create(_objEdition.EditionDisplayURL);
						PortalDefinition.ViewRole _objViewRole = new PortalDefinition.ViewRole();
						_objViewRole.name = Config.EveryoneRoles;
						_objNewEditionTab.roles.Add(_objViewRole);
						_objNewEditionTab.Columns = _arrTemplateColumns;
						_objPortal.tabs.Add(_objNewEditionTab);

						AddCategoryTab(_objPortal, _objNewEditionTab.tabs, _objEdition.EditionType_ID.ToString(), null, _objEdition.EditionDisplayURL, _objGenerateMode);
					}

					
				}
			}

			_objPortal.Save();
            Response.Redirect(Request.Url.PathAndQuery);
		}

		/// <summary>
		/// Thủ tục khởi tạo hoặc cập nhật một Tab chứa thông tin về một Edition, Category cha hoặc một Category con
		/// </summary>
		/// <param name="_objPortal">Đối tượng lưu cấu trúc Portal</param>
		/// <param name="_arrTabsCollection">Mảng chứa danh sách Tab ở mức đang xét</param>
		/// <param name="_strEditionTypeID">Mã tham chiếu đến chuyên san</param>
		/// <param name="_strParentCategoryID">Mã tham chiếu đến Category cha</param>
		/// <param name="_strParentCategoryDisplayURL">Chuỗi tham chiếu đến Tab của Category cha</param>
		private void AddCategoryTab(PortalDefinition _objPortal, ArrayList _arrTabsCollection, string _strEditionTypeID, string _strParentCategoryID, string _strParentCategoryDisplayURL, TemplateApplyingMode _objGenerateMode)
		{
			// Khai báo mảng lưu danh sách Category cần duyệt
			CategoryRow[] _arrCategories = null;
			using (MainDB _objDb = new MainDB())
			{
				if (_strEditionTypeID == null)
				{
					// Nếu không có mã chuyên san thì lấy danh sách các Category cha
					_arrCategories = _objDb.CategoryCollection.GetAsArray("Cat_ParentID " + (_strParentCategoryID == null ? " IS NULL" : (" = " + _strParentCategoryID)), "");
				}
				else
				{
					// Nếu có mã chuyên san thì lấy danh sách các Category con
					_arrCategories = _objDb.CategoryCollection.GetAsArray("EditionType_ID = " + _strEditionTypeID + " AND Cat_ParentID " + (_strParentCategoryID == null ? " IS NULL" : (" = " + _strParentCategoryID)), "");
				}
			}

			if (_arrCategories != null && _arrCategories.Length > 0)
			{
				// Duyệt danh sách Category có cần tạo Tab
				foreach(CategoryRow _objCategory in _arrCategories)
				{
					// Khai báo chuỗi lưu mã tham chiếu đến Tab của Category đang xét
					string _strNewCategoryTabURl = _strParentCategoryDisplayURL + "." + _objCategory.Cat_DisplayURL;

					// Lấy thông tin về Tab cần cập nhật
					PortalDefinition.Tab _objCurrentCategoryTab = _objPortal.GetTab(_strNewCategoryTabURl);

					// Lấy mẫu Tab đã được lưu
					ArrayList _arrTemplateColumns = _objPortal.CloneToTemplateTab(_objPortal.TemplateColumns) as ArrayList;

					// Làm mới các mã tham chiếu trong mẫu
					ReGenModuleID(ref _arrTemplateColumns);
					
					// Nếu Tab cần cập nhật không tồn tại
					if (_objCurrentCategoryTab == null)
					{
						// Tạo Tab mới
						PortalDefinition.Tab _objNewCategoryTab = PortalDefinition.Tab.Create(_strNewCategoryTabURl);
						PortalDefinition.ViewRole _objViewRole = new PortalDefinition.ViewRole();
						_objViewRole.name = Config.EveryoneRoles;
						_objNewCategoryTab.roles.Add(_objViewRole);
						_objNewCategoryTab.Columns = _arrTemplateColumns;
						_objNewCategoryTab.title = _objCategory.Cat_Name;
						_arrTabsCollection.Add(_objNewCategoryTab);

						AddCategoryTab(_objPortal, _objNewCategoryTab.tabs, null, _objCategory.Cat_ID.ToString(), _strNewCategoryTabURl, _objGenerateMode);
					}	
					else
					{
						bool _blnAllowGeneration = _objCategory.IsCat_ParentIDNull ? _objGenerateMode.ParentCategory : _objGenerateMode.SubCategory;

						// Nếu cho phép cập nhật thì cần xét them là tab cần cập nhật phải khác với tab mẫu
						if (_blnAllowGeneration && _objCurrentCategoryTab.reference != _objPortal.TemplateReference)
						{
							_objCurrentCategoryTab.Columns = _arrTemplateColumns;
							_objCurrentCategoryTab.title = _objCategory.Cat_Name;
						}

						AddCategoryTab(_objPortal, _objCurrentCategoryTab.tabs, null, _objCategory.Cat_ID.ToString(), _strNewCategoryTabURl, _objGenerateMode);
					}

					
				}
			}
		}

		/// <summary>
		/// Thủ tục sinh mã tham chiếu cho các Module và Column trong thẻ sử dụng thẻ mẫu
		/// </summary>
		/// <param name="_arrColumns">Mảng danh sách các cột của thẻ mẫu</param>
		private void ReGenModuleID(ref ArrayList _arrColumns)
		{
			foreach (PortalDefinition.Column _objColumn in _arrColumns )
			{
				foreach (PortalDefinition.Module _objModule in _objColumn.ModuleList )
				{
					_objModule.reference = System.Guid.NewGuid().ToString();
				}

				_objColumn.ColumnReference = System.Guid.NewGuid().ToString();
				ReGenModuleID(ref _objColumn.Columns);
			}
		}

		/// <summary>
		/// Thủ tục cập nhật lại mã tham chiếu của thẻ tương ứng với đề mục đã sửa
		/// Thủ tục này được gọi khi người sử dụng thay đổi đường dẫn hiển thị trên URL của một đề mục nào đó.
		/// </summary>
		/// <param name="_strOldTabRef">Mã tham chiếu cũ, dùng để tìm ra thẻ hiện thời ứng với đề mục cần sửa</param>
		/// <param name="_strNewTabRef">Mã</param>
		/// <param name="_strNewTabTitle"></param>
		private void SyncCategoryTab(string _strOldTabRef, string _strNewTabRef, string _strNewTabTitle)
		{
			PortalDefinition _objPortal = PortalDefinition.Load();
			PortalDefinition.Tab _objCurrentCategoryTab = _objPortal.GetTab(_strOldTabRef);

			if (_objCurrentCategoryTab != null)
			{
				_objCurrentCategoryTab.title = _strNewTabTitle;
				_objCurrentCategoryTab.reference = _strNewTabRef;
				_objPortal.Save();
			}
		}
        /// <summary>
		/// Thủ tục thay đổi thứ tự của Category đã chọn
		/// </summary>
		/// <param name="_intCategoryID">Mã của Category cần thay đổi thứ tự</param>
		/// <param name="_intOrderChangeValue">Giá trị thay đổi 1 : Lên, -1 : Xuống</param>
		private void ChangeCatOrder(int _intCategoryID, int _intOrderChangeValue)
		{
			CategoryRow _objCurrentCategory = null;
			CategoryRow _objBesideCategory = null;

			using (MainDB _objDB = new MainDB())
			{
				_objCurrentCategory = _objDB.CategoryCollection.GetByPrimaryKey(_intCategoryID);
				if (_objCurrentCategory != null)
				{
					// Lấy thông tin về Category nằm trên hoặc dưới Category hiện thời
					_objBesideCategory = _objDB.CategoryCollection.GetRow("EditionType_ID = " + _objCurrentCategory.EditionType_ID + (_objCurrentCategory.IsCat_ParentIDNull ? "" : " AND Cat_ParentID = " + _objCurrentCategory.Cat_ParentID) + " And Cat_Order = " + (_objCurrentCategory.Cat_Order + _intOrderChangeValue).ToString());
				}
			}

			if (_objCurrentCategory != null && _objBesideCategory != null)
			{
				_objCurrentCategory.Cat_Order = _objCurrentCategory.Cat_Order + _intOrderChangeValue;
				_objBesideCategory.Cat_Order = _objBesideCategory.Cat_Order - _intOrderChangeValue;

				using (MainDB _objDB = new MainDB())
				{
					// Cập nhật lại thứ tự mới
					_objDB.CategoryCollection.Update(_objCurrentCategory);
					_objDB.CategoryCollection.Update(_objBesideCategory);
				}
			}
		}

		/// <summary>
		/// Thủ tục áp nội dung thẻ mẫu vào nội dung thẻ đã chọn
		/// </summary>
		/// <param name="_strTabRef"></param>
		private void ApplyCustomTab(string _strTabRef)
		{
			PortalDefinition _objPortal = PortalDefinition.Load();
			PortalDefinition.Tab _objSelectedTab = _objPortal.GetTab(_strTabRef);
			ArrayList _arrTemplateContent = _objPortal.CloneToTemplateTab(_objPortal.TemplateColumns) as ArrayList;

			if (_objSelectedTab != null && _objSelectedTab.reference != _objPortal.TemplateReference && _arrTemplateContent != null)
			{
				ReGenModuleID(ref _arrTemplateContent);

				_objSelectedTab.Columns = _arrTemplateContent;

				_objPortal.Save();
			}
		}
		#endregion

		#region Handler All Item Command
		protected void MoveCategoryUp(object sender, CommandEventArgs e)
		{
			ChangeCatOrder(Convert.ToInt32(e.CommandArgument), -1);
			LoadEditionTypesList();
		}

		protected void MoveCategoryDown(object sender, CommandEventArgs e)
		{
			ChangeCatOrder(Convert.ToInt32(e.CommandArgument), 1);
			LoadEditionTypesList();
		}
		#endregion

		#region Handler Edit Category Event
		protected void OnCancelEditCategory()
		{
			LoadEditionTypesList();
		}
		#endregion

		#region Handler Generate Tabs
		protected void btnApplyTemplateToAllTab_Click(object sender, EventArgs e)
		{
			TemplateApplyingMode _objGenerateMode = new TemplateApplyingMode(true, true, true);
			UpdateTabs(_objGenerateMode);
		}

		protected void btnApplyTemplateToAllEditionTab_Click(object sender, EventArgs e)
		{
			TemplateApplyingMode _objGenerateMode = new TemplateApplyingMode(true, false, false);
			UpdateTabs(_objGenerateMode);
		}

		protected void btnApplyTemplateToAllParentCategoryTab_Click(object sender, EventArgs e)
		{
			TemplateApplyingMode _objGenerateMode = new TemplateApplyingMode(false, true, false);
			UpdateTabs(_objGenerateMode);
		}

		protected void btnApplyTemplateToAllSubCategoryTab_Click(object sender, EventArgs e)
		{
			TemplateApplyingMode _objGenerateMode = new TemplateApplyingMode(false, false, true);
			UpdateTabs(_objGenerateMode);
		}

		protected void btnApplyTemplateToCustomTab_Click(object sender, EventArgs e)
		{
			ApplyCustomTab(drdTabsList.SelectedValue);
		}
		#endregion
	}

	/// <summary>
	/// Lớp lưu chế độ thực hiện áp dụng mẫu cho các Tab
	/// </summary>
	public class TemplateApplyingMode
	{
		// Áp mẫu cho chuyên san
		public bool EditionType = true;

		// Áp mẫu cho danh mục cha
		public bool ParentCategory = true;

		// Áp mẫu cho danh mục con
		public bool SubCategory = true;

		public TemplateApplyingMode(bool _blnEditionType, bool _blnParentCategory, bool _blnSubCategory)
		{
			EditionType = _blnEditionType;
			ParentCategory = _blnParentCategory;
			SubCategory = _blnSubCategory;
		}
	}
}
