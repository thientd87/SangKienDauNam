using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using DFISYS.API;
using Module = DFISYS.API.Module;
using Cache = DFISYS.SiteSystem.Cache;
namespace DFISYS.GUI.Share
{
	/// <summary>
	///	Renders a Tab.
	/// </summary>
	public partial  class PortalTab : BaseModule, INamingContainer
	{
		#region Variables
		protected OverlayMenu ovm;
	    protected ArrayList _arrAllSnap = new ArrayList();
		protected ArrayList _arrAllSnapHeaderDrag = new ArrayList();
		protected ArrayList _arrAllSnapHeaderCollapsedDrag = new ArrayList();
		protected ArrayList _arrAllSnapHeaderExpand = new ArrayList();
		protected ArrayList _arrAllEmptyColumn = new ArrayList();
		protected ArrayList _arrAllSnapHeaderCollapsedExpand = new ArrayList();
        private System.Collections.Generic.Dictionary<string, ArrayList> _tdList = new Dictionary<string, ArrayList>();

		protected API.Controls.LinkButton Linkbutton1;
		private bool IsAdmin = false;
		#endregion

		#region Render module and column
	    #region render module

	    /// <summary>
	    /// Thủ tục khởi tạo Module và chèn Module vào cột đã định.
	    /// </summary>
	    /// <param name="td">Ô (hay cột) chứa Module</param>
	    /// <param name="tab">Tab chứa Module (cần để lấy thêm thông tin về Tab)</param>
	    /// <param name="modules">Danh sách Module đã được định nghĩa thuộc cột này</param>
	    private void RenderModules(HtmlTableCell td, PortalDefinition.Tab tab, ArrayList modules)
	    {
	        //if (Request.HttpMethod == "GET")
	        //{
            
	        // Kiểm tra nếu không có Module nào trong cột thì chèn khoảng trắng để lưu vị trí cột
	        if (modules.Count == 0)
	        {
	            _arrAllEmptyColumn.Add(td);
	        }
	        // Duyệt danh sách Module
	        PortalDefinition.Module _objModuleDefinition;
	        ChannelUsers objUser = new ChannelUsers();
	        // Khởi tạo Module
	        Module _objModuleControl;
	        CachedModule _objCachedModuleControl;
	        for (int _intModuleCount = 0; _intModuleCount < modules.Count; _intModuleCount++)
	        {
	            _objModuleDefinition = modules[_intModuleCount] as PortalDefinition.Module;
	            //DuongNA tạm thời bỏ Kiểm tra người dùng có quyền hiển thị Module hay không
	            //if (objUser.HasViewRights(Page.User, _objModuleDefinition.roles))
	            {
	                _objModuleControl = null;
	                _objCachedModuleControl = null;
	                // Nạp các thông số về Module
	                _objModuleDefinition.LoadModuleSettings();
	                //DuongNA tạm thời bỏ trycatch
	                //try
	                {
	                    // Kiểm tra xem có thông số nào được xác lập không
	                    // Xây dựng chuỗi đường dẫn đến tệp ASCX
	                    string _strModuleSrc = Config.GetModuleVirtualPath(_objModuleDefinition.type);
	                    if (_objModuleDefinition.moduleSettings == null)
	                    {
	                        string[] strModulePath = _objModuleDefinition.type.Split("/".ToCharArray());
	                        string strModule;
	                        if (strModulePath.Length == 2)
	                            strModule = strModulePath[1];
	                        else
	                            strModule = strModulePath[0];
	                        _strModuleSrc += strModule + ".ascx";
	                    }
	                    else
	                        _strModuleSrc += _objModuleDefinition.moduleSettings.ctrl;
                        
                       
	                    //DuongNA tạm thời bỏ Kiểm tra xem Module có được phép hiển thị hay không
	                    //if((_objModuleControl != null && _objModuleControl.IsVisible() || (_objCachedModuleControl != null && _objCachedModuleControl.IsVisible())))
	                    {
	                        // Kiểm tra xem trang hiện thời có nằm trong trạng thái cho phép kéo thả hay không
	                        // Nếu ở trạng thái hỗ trợ kéo thả thì tạo các khung chứa (Snap) để chứa nội dung của các Module 
	                        // (nội dung này sẽ được nạp sử dụng Ajax khi người sử dụng mở rộng khung hoặc chế độ tự động mở rộng khung dc bật)
                            
	                        if (_objModuleDefinition.CacheTime <= 0)
	                        {
	                            // Nếu Module không sử dụng Cache
	                            _objModuleControl = (Module)LoadModule(_strModuleSrc);

	                            // Khởi tạo nội dung Module
	                            _objModuleControl.InitModule(tab.reference,
	                                                         _objModuleDefinition.reference,
	                                                         _objModuleDefinition.type,
	                                                         Config.GetModuleVirtualPath(_objModuleDefinition.type),
	                                                         true);
	                        }
	                        else
	                        {
	                            // Nếu Module có sử dụng Cache
	                            _objCachedModuleControl = new CachedModule();
	                            _objCachedModuleControl.ModuleCacheTime = _objModuleDefinition.CacheTime;

	                            // Khởi tạo nội dung Module
	                            _objCachedModuleControl.InitModule(tab.reference,
	                                                               _objModuleDefinition.reference,
	                                                               _objModuleDefinition.type,
	                                                               Config.GetModuleVirtualPath(_objModuleDefinition.type),
	                                                               true,
	                                                               _strModuleSrc);
	                        }

	                        if ((_objModuleControl != null && _objModuleControl.IsVisible() || (_objCachedModuleControl != null && _objCachedModuleControl.IsVisible())))
	                        {
	                            // Hiển thị nội dung Module theo cách trình bày bình thường không hỗ trợ kéo thả
	                            HtmlTable _objSimpleModuleContainer = new HtmlTable();
	                            _objSimpleModuleContainer.Width = "100%";
	                            _objSimpleModuleContainer.CellPadding = 0;
	                            _objSimpleModuleContainer.CellSpacing = 0;
	                            _objSimpleModuleContainer.Rows.Add(new HtmlTableRow());

	                            // Nạp dữ liệu của Module vào ô chứa Module
	                            HtmlTableCell _objCellContainer = new HtmlTableCell();
	                            _objSimpleModuleContainer.Rows[0].Cells.Add(_objCellContainer);
	                            _objCellContainer.Attributes.Add("class", "Module_Simple_Block");
	                            _objCellContainer.VAlign = "top";
	                            if (_objModuleControl != null)
	                            {
	                                _objCellContainer.Controls.Add(_objModuleControl);
	                            }
	                            else
	                            {
	                                _objCellContainer.Controls.Add(_objCachedModuleControl);
	                            }
	                            //cách giữa header và body
	                            _objCellContainer.Attributes.Add("style", "padding-bottom:0px;");
	                            td.Controls.Add(_objSimpleModuleContainer);
	                        }

	                        //}
	                    }
	                }
	                //catch (Exception e)
	                //{
	                //    Console.WriteLine(e.Message + e.StackTrace);
	                //}
	            }
                
	        }
	    }

	    #endregion
         /// <summary>
        /// Thủ tục tạo và sắp xếp các cột của một Tab
        /// </summary>
        /// <param name="_objCurrentTab">Đối tượng chứa thông tin về Tab hiện thời</param>
        /// <param name="_arrColumns">Mảng chứa danh sách cột xuất phát</param>
        /// <param name="_htcContainer">Điều khiển sẽ chứa các cột trong danh sách cột xuất phát</param>
        private void RenderColumns(PortalDefinition.Tab _objCurrentTab, ArrayList _arrColumns, HtmlTableCell _htcContainer)
		{
			// Nếu không có cột nào trong danh sách xuất phát thì kết thúc thủ tục
			if (_arrColumns == null || _arrColumns.Count == 0) return;
			// Khai báo mảng chứa các bảng dùng để chứa cột
			ArrayList _arrTableIndexes = new ArrayList();
            PortalDefinition.Column _objColumn;
            SortableHtmlTable _objTable;
            HtmlTableRow _objTableRow;
            HtmlTableCell _objCellContainer;
			// Duyệt danh sách các cột xuất phát
			for(int _intColumnCount = 0; _intColumnCount < _arrColumns.Count; _intColumnCount++)
			{
				// Lấy thông tin về cột đang xét
				_objColumn = _arrColumns[_intColumnCount] as PortalDefinition.Column;
				_objTable = null;
				_objTableRow = null;
				// Duyệt danh sách các bảng đã tạo
				// Các cột có cùng cấp độ sẽ nằm cùng một bảng
				foreach(SortableHtmlTable _tblContainer in _arrTableIndexes)
				{
					// Kiểm tra cấp độ của cột đang xét
					if (_tblContainer.Attributes["level"] == _objColumn.ColumnLevel.ToString())
					{
						// Nếu bảng đang xét có cùng cấp độ với cột đang xét
						// thì lấy tham chiếu đến bảng và dòng đầu tiên của bảng
						_objTable = _tblContainer;
						//_objTableHeaderRow = _tblContainer.Rows[0];
						_objTableRow = _tblContainer.Rows[0];
						break;
					}
				}
				
				// Nếu bảng chưa có tham chiếu (cấp độ hiện thời của cột là cấp độ mới (chưa được xét)
				if (_objTable == null)
				{
					// Khởi tạo bảng
					_objTable = new SortableHtmlTable();
					_objTable.CellPadding = 0;
					_objTable.CellSpacing = 0;
					_objTable.Border = 0;

					// Lưu trữ cấp độ của bảng (chứa các cột có cùng cấp độ)
					_objTable.Attributes.Add("level", _objColumn.ColumnLevel.ToString());

					// Tạo dòng chứa tiêu đề các cột
                    //_objTableHeaderRow = new HtmlTableRow();
                    //_objTable.Rows.Add(_objTableHeaderRow);
                    
					// Tạo dòng chứa các cột
					_objTableRow = new HtmlTableRow();
					_objTable.Rows.Add(_objTableRow);
                    
				    // Lưu lại bảng đại diện cho cột vào mảng các bảng
					_arrTableIndexes.Add(_objTable);
				}

				// Khởi tạo điều khiển đại diện cho 1 cột
				_objCellContainer = new HtmlTableCell();
				_objCellContainer.VAlign = "top";
				_objCellContainer.Attributes.Add("id", "Cell" + _objColumn.ColumnReference.Replace('-', '_'));
				_objCellContainer.Attributes.Add("class", IsAdmin ? "Portal_Column" : "Portal_NormalColumn");

				if (_objColumn.ColumnCustomStyle != null && _objColumn.ColumnCustomStyle != "") 
				    _objCellContainer.Attributes.Add("style", _objColumn.ColumnCustomStyle);

				// Kiểm tra xem độ rộng cột có được thiết lập hay không
				if (_objColumn.ColumnWidth != "")
				{
					// Nếu được thiết lập thì xác định thông số width của cột
					_objCellContainer.Attributes.Add("width", _objColumn.ColumnWidth.ToString());

					// Nếu thiết lập là 100%, có nghĩa là chỉ có 1 cột --> bảng chứa cột này cần được để lên 100%
					if (_objColumn.ColumnWidth == "100%")
					{
						_objTable.Attributes.Add("width", "100%");
					}
				}
				// Thêm TD vừa tạo vào TR của bảng trong danh sách
				_objTableRow.Cells.Add(_objCellContainer);

                // Sắp xếp các Module đã được thiết lập cho cột (TD) đang xét
                RenderModules(_objCellContainer, _objCurrentTab, _objColumn.ModuleList);

                // Thực hiện sinh mã cho các cột con
				RenderColumns(_objCurrentTab, _objColumn.Columns, _objCellContainer);
			}

			// Thêm danh sách bảng có được vào vùng chứa hiện thời
			if (_arrTableIndexes != null && _arrTableIndexes.Count > 0)
			{
                //try
                //{
					// Sắp xếp lại các cột theo đúng thứ tự Level đã định
					_arrTableIndexes.Sort();

					// Duyệt danh sách các bảng đã tạo ra
					foreach(HtmlTable _objIndexedTable in _arrTableIndexes)
					{
						// Tính lại độ rộng của các cột trong bảng
						ReCalculateCellsWidth(_objIndexedTable);
						
						// Kiểm tra cấp độ của bảng
						if (Convert.ToInt32(_objIndexedTable.Attributes["level"]) == -1)
						{
							// Nếu là cấp độ mặc định (-1)
							// tức là bảng nằm ở cuối danh sách các điều khiển của cột
							_htcContainer.Controls.Add(_objIndexedTable);
						}
						else
						{
							
							int _intColumnLevel = Convert.ToInt32(_objIndexedTable.Attributes["level"]);

							// Chèn khung hiển thị vào cột cha đang xét
							// Kiểm tra vị trí chèn có hợp lệ không (phải chèn vào trong danh sách, không được chèn ra ngoài)
							if (_htcContainer.Controls.Count > _intColumnLevel)
							{
								// Đặt khung vào vị trí đã định
								_htcContainer.Controls.AddAt(_intColumnLevel, _objIndexedTable);
							}
							else
							{
								// Nếu vị trí chèn nằm ngoài danh sách thì sử dụng lệnh thêm vào cuối danh sách thay vì chèn
								_htcContainer.Controls.Add(_objIndexedTable);
							}
						}
						
					}
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //    // Nếu có lỗi thì kết thúc thủ tục
                //    return;
                //}
			}
		}

		/// <summary>
		/// Thủ tục điều chỉnh lại độ rộng của các cột trong bảng
		/// </summary>
		/// <param name="_objTable">Bảng cần điều chỉnh</param>
		private void ReCalculateCellsWidth(HtmlTable _objTable)
		{
			if (_objTable.Rows[0].Cells.Count > 0)
			{
				int _intFixedColumns = 0;

				// Duyệt danh sách các cột của bảng (thuộc dòng đầu tiên)
				foreach (HtmlTableCell _objCell in _objTable.Rows[0].Cells)
				{
					// Tìm số lượng cột đã được xác định độ rộng
					if (_objCell.Width != "")
					{
						_intFixedColumns++;
					}
				}

				_intFixedColumns = _objTable.Rows[0].Cells.Count - _intFixedColumns;

				// Duyệt danh sách các cột của bảng (thuộc dòng đầu tiên)
				for (int _intCellCount = 0; _intCellCount < _objTable.Rows[0].Cells.Count ; _intCellCount ++)
				{
					//HtmlTableCell _objColumnHeaderCell = _objTable.Rows[0].Cells[_intCellCount];
					HtmlTableCell _objColumnContentCell = _objTable.Rows[0].Cells[_intCellCount];
					// Chia đều độ rộng cho các cột
					//_objCell.Width = _objCell.Width != "" ? _objCell.Width : ((IsAdmin() ? 200 : 100) / (_intFixedColumns > 0 ? (IsAdmin() ? (_intFixedColumns * 2) : _intFixedColumns) : _objTable.Rows[0].Cells.Count)).ToString() + "%";
					//_objColumnHeaderCell.Width = _objColumnHeaderCell.Width != "" ? _objColumnHeaderCell.Width : "*";
					_objColumnContentCell.Width = _objColumnContentCell.Width != "" ? _objColumnContentCell.Width : "*";
				}

			}
		}

		#endregion
        #region Override Functions
		protected override object SaveViewState()
		{
			if (IsAdmin)
			{
				// Duyệt danh sách các khung đã tạo
				for(int _intSnapCount = 0; _intSnapCount < _arrAllSnap.Count; _intSnapCount++)
				{
					// Xác lập mã tham chiếu của khung cho các điều khiển còn thiếu
					((Literal) _arrAllSnapHeaderDrag[_intSnapCount]).Text = ((Snap)_arrAllSnap[_intSnapCount]).ClientID;
					((Literal) _arrAllSnapHeaderExpand[_intSnapCount]).Text = ((Snap)_arrAllSnap[_intSnapCount]).ClientID;
					((Literal) _arrAllSnapHeaderCollapsedDrag[_intSnapCount]).Text = ((Snap)_arrAllSnap[_intSnapCount]).ClientID;
					((Literal) _arrAllSnapHeaderCollapsedExpand[_intSnapCount]).Text = ((Snap)_arrAllSnap[_intSnapCount]).ClientID;
				}
			}

			return base.SaveViewState();
		}

		/// <summary>
		/// Nạp chồng Thủ tục khởi tạo các điều khiển con
		/// </summary>
		override protected void CreateChildControls()
		{
			// Lấy thông tin Tab hiện thời
			PortalDefinition.Tab tab = PortalDefinition.GetCurrentTab();
			if(tab == null) return;
            
			// Kiểm tra quyền của người sử dụng
            ChannelUsers objuser = new ChannelUsers();
            // Edit by Tqdat
            // Xu ly viec khong de Session Timeout la BienTapvien out ra khoi fan Admin
            //  - Neu bi Session TimeOut thi kiem tra xem Cookie co ton tai User va Pass hay ko
            //      + Neu Co thi Login lai de lay lai session
            //      + Neu Khong thi Redirect ra trang login.aspx

            //bool isHasViewRights = objuser.HasViewRights(Page.User, tab.roles);
            /*if (isHasViewRights == false)
            {
                // Kiem tra xem co Cookie hay ko
                HttpCookie cookie = Request.Cookies["PortalUser"];
                if (cookie != null)
                {
                    objuser.Login(cookie.Values["AC"].Trim(), cookie.Values["PW"].Trim());
                    isHasViewRights = true;
                }
            }*/

            if (objuser.HasViewRights(Page.User, tab.roles))
			{
                
                RenderColumns(tab, tab.Columns, DisplayRegion);
				if (IsAdmin)
				{
					// Hiển thị vùng trắng của các cột rỗng
					foreach(HtmlTableCell _objEmptyColumn in _arrAllEmptyColumn)
					{
						LiteralControl _ltrSpace = new LiteralControl();
						_ltrSpace.Text = "&nbsp;";
						_objEmptyColumn.Controls.Add(_ltrSpace); 
					}
				}

			}
            else
            {
               
                Cache cache = new Cache(HttpContext.Current.Application);
                string path = HttpContext.Current.Request.Url.AbsolutePath.ToLower();
                string _strCacheKey = Config.GetPortalUniqueCacheKey() + path + "_" + HttpContext.Current.User.Identity.Name;
                string _strCacheRawKey = Config.GetPortalUniqueCacheKey() + "_Raw" + HttpContext.Current.Request.RawUrl + "_" + HttpContext.Current.User.Identity.Name;
                cache[_strCacheKey] = null;
                cache[_strCacheRawKey] = null;

                Session["NotPermission"] = "True";
                Session["lastPath"] = HttpContext.Current.Request.RawUrl;
                Response.Redirect("/login.aspx");
            }
		}
	  
	    private string RenderHtmlTableToString(HtmlTable table)
	    {
            using (MemoryStream dataStream = new MemoryStream())
            {
                using (StreamWriter textWriter = new StreamWriter(dataStream, Encoding.UTF8))
                {
                    using (HtmlTextWriter htmlWriter = new HtmlTextWriter(textWriter))
                    {
                        table.RenderControl(htmlWriter);
                        textWriter.Flush();
                        dataStream.Seek(0, SeekOrigin.Begin);

                        using (StreamReader dataReader = new StreamReader(dataStream))
                        {
                            string htmlContent = dataReader.ReadToEnd();
                            return htmlContent;
                        }
                    }
                }
            }

	    }
	    
        private object Clone(object source)
        {
            MethodInfo mi = typeof(object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);
            return mi.Invoke(source, null);
        }
	    /// <summary>
	    
	    /// xóa các module nội dung ra khỏi bảng nội dung mẫu
	    /// </summary>
	    /// <param name="tdDisplay"></param>
	    /// <param name="td_id_module"></param>
        private void RemoveModuleFromColumn(HtmlTableCell tdDisplay, Dictionary<string, ArrayList> td_id_module)
        {
            HtmlTableCell td;
            ArrayList modules;
            //duyệt qua mảng các ID của TD cần đưa module vào
            foreach (KeyValuePair<string, ArrayList> td_id in td_id_module)
            {

                //tìm ra các TD cần đưa module vào
                td = tdDisplay.FindControl(td_id.Key) as HtmlTableCell;
                //lấy ra module tương ứng với ID của TD đó
                modules = td_id.Value;
                //nếu TD đó có chứ module , tức là không phải để chứa cột khác , thì xóa module khỏi column đó
                if (modules != null && modules.Count > 0)
                {
                    try
                    {
                        td.InnerHtml = "";
                    }
                    catch
                    {
                    }
                }
            }
        }
	    /// <summary>
	    
	    /// Hàm đưa module vào TD theo ID của TD
	    /// </summary>
	    /// <param name="tdDisplay">Table tổng chứa các TD có các ID cần đưa</param>
	    /// <param name="td_id_module">Các module tương úng với các TD</param>
	    /// <param name="tab">Tab hiện thời</param>
	    private void GenerateModuleToColumn(HtmlTableCell tdDisplay , Dictionary<string ,ArrayList> td_id_module,PortalDefinition.Tab tab)
	    {
            HtmlTableCell td;
            ArrayList modules;
	        //duyệt qua mảng các ID của TD cần đưa module vào
            foreach (KeyValuePair<string, ArrayList> td_id in td_id_module)
            {
                
                //tìm ra các TD cần đưa module vào
                td = tdDisplay.FindControl(td_id.Key) as HtmlTableCell;
                //lấy ra module tương ứng với ID của TD đó
                modules = td_id.Value;
                if (td != null)
                {
                    //nếu TD đó có chứ module , tức là không phải để chứa cột khác , thì đưa module vào
                    if (modules != null && modules.Count > 0)
                    {
                        RenderModules(td, tab, modules);
                    }
                }
            }
	    }
		#endregion

		#region Event Handler
		protected void OnSignOut(object sender, EventArgs args)
		{
			HttpCookie cookie = Request.Cookies["PortalUser"];
			if (cookie != null)
			{				
				cookie.Values["PW"] = "";
				DateTime dt = DateTime.Now;
				dt.AddDays(-1);
				cookie.Expires = dt;
				Response.Cookies.Add(cookie);
			}
			FormsAuthentication.SignOut();
            Response.Redirect("Login" + Config.CustomExtension);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			
		}
		/// <summary>
		/// Ham thuc hien luu cac response xuong html cache file
		/// </summary>
	    protected void OnAddTab(object sender, EventArgs args)
		{
			PortalDefinition pd = PortalDefinition.Load();
			PortalDefinition.Tab t = PortalDefinition.Tab.Create();

			pd.GetTab(Request["TabRef"]).tabs.Add(t);

			pd.Save();

			Response.Redirect(Helper.GetEditTabLink(t.reference));
		}
		protected void OnEditTab(object sender, EventArgs args)
		{
			Response.Redirect(Helper.GetEditTabLink());
		}
		protected void OnDeleteTab(object sender, EventArgs args)
		{
			PortalDefinition.Tab t = PortalDefinition.GetCurrentTab();
			PortalDefinition.DeleteTab(t.reference);

			if(t.parent == null)
			{
				Response.Redirect(Helper.GetTabLink(""));
			}
			else
			{
				Response.Redirect(Helper.GetTabLink(t.parent.reference));
			}
		}
		
		override protected void OnInit(EventArgs e)
		{
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			
		}
		#endregion
		
	}

	#region SortableHtmlTable class
	/// <summary>
	/// Lớp thừa kế từ Control Table
	/// Lớp này thực thi giao diện IComparable hỗ trợ cho việc Sort
	/// </summary>
	public class SortableHtmlTable : HtmlTable, IComparable
	{
		#region IComparable Members

		/// <summary>
		/// Hàm thực hiện so sánh khi sắp xếp các bảng
		/// </summary>
		/// <param name="obj">Bảng cần so sánh</param>
		/// <returns>Kết quả so sánh</returns>
		public int CompareTo(object obj)
		{
			// TODO:  Add SortableHtmlTable.CompareTo implementation
			HtmlTable _objTable = (HtmlTable) obj;
			
			// Trả về kết quả so sánh giữa thuộc tính level của bảng hiện thời với thuộc tính level của bảng cần so sánh
			return Attributes["level"].CompareTo(_objTable.Attributes["level"]);
		}

		#endregion
	}
	#endregion

	#region Snap Template
	public class ModuleContainerTemplate : ITemplate
	{
		ListItemType templateType;

		public Control ChildControl = null;
		public string SnapID="";
		public string HeaderContent="";
		public Control EditModuleControl = null;
		public Literal _renderClientIDOfControlForDrag = new Literal();
		public Literal _renderClientIDOfCollapsedControlForDrag = new Literal();
		public Literal _renderClientIDOfControlForExpand = new Literal();
		public Literal _renderClientIDOfCollapsedControlForExpand = new Literal();

		private string TabRef = "";
		private string ModuleRef = "";
		private string NewsRef = "";

		public ModuleContainerTemplate(ListItemType type)
		{
			templateType = type;
		}

		public ModuleContainerTemplate(ListItemType type, string _strTabRef, string _strModuleRef, string _strNewsRef)
		{
			templateType = type;
			TabRef = _strTabRef;
			ModuleRef = _strModuleRef;
			NewsRef = _strNewsRef;
		}

		public void InstantiateIn(Control container)
		{
			Literal _renderStartHTMLControl1 = new Literal();
			Literal _renderStartHTMLControl2 = new Literal();
			Literal _renderStartTextHTMLControl = new Literal();
			Literal _renderEndHTMLControl = new Literal();
			StringBuilder _strTypeContentMixed = new StringBuilder();

			_renderClientIDOfControlForDrag.EnableViewState = false;
			_renderClientIDOfControlForExpand.EnableViewState = false;
			_renderStartHTMLControl1.EnableViewState = false;
			_renderStartHTMLControl2.EnableViewState = false;
			_renderStartTextHTMLControl.EnableViewState = false;
			_renderEndHTMLControl.EnableViewState = false;


			switch( templateType )
			{
					// Hiển thị Header Module header dạng đầy đủ (Expanded)
				case ListItemType.Header:
					_strTypeContentMixed.Append("<table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");
					_strTypeContentMixed.Append("<tr>");
					_strTypeContentMixed.Append("<td width=\"100%\" class=\"Module_Header\" onmousedown=\"");
					_renderStartHTMLControl1.Text = _strTypeContentMixed.ToString();

					_strTypeContentMixed = new StringBuilder();
					_strTypeContentMixed.Append(".StartDragging(event);\">");
					_strTypeContentMixed.Append("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"border-collapse:collapse;\"><tr><td class=\"Module_Header_Title\">");
					_strTypeContentMixed.Append("<img style=\"cursor:hand\" onclick=\"");
					_renderStartHTMLControl2.Text = _strTypeContentMixed.ToString();

					_strTypeContentMixed = new StringBuilder();
					_strTypeContentMixed.Append(".ToggleExpand();\" ");
					_strTypeContentMixed.Append("src=\"images/Snap/collapse.gif\" width=\"9\" height=\"9\" border=\"0\">&nbsp;");
					_strTypeContentMixed.Append(HeaderContent);
					_strTypeContentMixed.Append("</td>");
					//_strTypeContentMixed.Append("<td width=\"*\">");
					_renderStartTextHTMLControl.Text = _strTypeContentMixed.ToString();

					_strTypeContentMixed = new StringBuilder();
					//_strTypeContentMixed.Append("</td>");
					_strTypeContentMixed.Append("</tr></table></td>");
					_strTypeContentMixed.Append("</tr>");
					_strTypeContentMixed.Append("</table>");
					_renderEndHTMLControl.Text = _strTypeContentMixed.ToString();

					_renderClientIDOfControlForDrag.Text = "";
					_renderClientIDOfControlForExpand.Text = "";

					container.Controls.Add(_renderStartHTMLControl1);
					container.Controls.Add(_renderClientIDOfControlForDrag);
					container.Controls.Add(_renderStartHTMLControl2);
					container.Controls.Add(_renderClientIDOfControlForExpand);
					container.Controls.Add(_renderStartTextHTMLControl);
					//container.Controls.Add(EditModuleControl);	( Tạm thời không sử dụng Edit Link)
					container.Controls.Add(_renderEndHTMLControl);

					break;

					// Hiển thị Header Module dạng thu gọn (Collapsed)
				case ListItemType.EditItem:
					_strTypeContentMixed.Append("<table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");
					_strTypeContentMixed.Append("<tr>");
					_strTypeContentMixed.Append("<td width=\"100%\" class=\"Module_Header_Collapsed\" onmousedown=\"");
					_renderStartHTMLControl1.Text = _strTypeContentMixed.ToString();

					_strTypeContentMixed = new StringBuilder();
					_strTypeContentMixed.Append(".StartDragging(event);\"><table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"border-collapse:collapse;\"><tr><td class=\"Module_Header_Title\">");
					_strTypeContentMixed.Append("<img style=\"cursor:hand\" onclick=\"");
					_renderStartHTMLControl2.Text = _strTypeContentMixed.ToString();

					_strTypeContentMixed = new StringBuilder();
					//_strTypeContentMixed.Append(".ToggleExpand();\" src=\"images/Snap/expand.gif\" width=\"9\" height=\"9\" border=\"0\">&nbsp;");
					_strTypeContentMixed.Append(".ToggleExpand();SendQuery('" + TabRef + "','" + ModuleRef + "','" + NewsRef + "')\" ");
					//_strTypeContentMixed.Append(".ToggleExpand();\" ");
					_strTypeContentMixed.Append("src=\"images/Snap/expand.gif\" width=\"9\" height=\"9\" border=\"0\">&nbsp;");
					_strTypeContentMixed.Append(HeaderContent);
					_strTypeContentMixed.Append("</td>");
					//_strTypeContentMixed.Append("<td width=\"*\">");
					_renderStartTextHTMLControl.Text = _strTypeContentMixed.ToString();

					_renderClientIDOfCollapsedControlForDrag.Text = "";
					_renderClientIDOfCollapsedControlForExpand.Text = "";

					_strTypeContentMixed = new StringBuilder();
					//_strTypeContentMixed.Append("</td>");
					_strTypeContentMixed.Append("</tr></table></td>");
					_strTypeContentMixed.Append("</tr>");
					_strTypeContentMixed.Append("</table>");
					_renderEndHTMLControl.Text = _strTypeContentMixed.ToString();

					container.Controls.Add(_renderStartHTMLControl1);
					container.Controls.Add(_renderClientIDOfCollapsedControlForDrag);
					container.Controls.Add(_renderStartHTMLControl2);
					container.Controls.Add(_renderClientIDOfCollapsedControlForExpand);
					container.Controls.Add(_renderStartTextHTMLControl);
					//container.Controls.Add(EditModuleControl);	Tạm thời không sử dụng Edit Link
					container.Controls.Add(_renderEndHTMLControl);

					break;

					// Hiển thị nội dung Module
				case ListItemType.Item:
					HtmlTable _objModuleContent = new HtmlTable();
					_objModuleContent.CellPadding = 0;
					_objModuleContent.CellSpacing = 0;
					_objModuleContent.Width = "100%";
					_objModuleContent.Rows.Add(new HtmlTableRow());
					_objModuleContent.Rows[0].Cells.Add(new HtmlTableCell());
					_objModuleContent.Rows[0].Cells[0].Attributes.Add("class", "Module_Block");
					_objModuleContent.Rows[0].Cells[0].Attributes.Add("id", "MB-" + ModuleRef);
					_objModuleContent.Rows[0].Cells[0].Controls.Add(ChildControl);

					/*HtmlGenericControl _objDivContainerForChildControl = new HtmlGenericControl("div");
					_objDivContainerForChildControl.Attributes.Add("width", "100%");
					_objDivContainerForChildControl.Attributes.Add("id", "MB-" + ModuleRef);
					_objModuleContent.Rows[0].Cells[0].Controls.Add(_objDivContainerForChildControl);
					_objDivContainerForChildControl.Controls.Add(ChildControl);*/

					container.Controls.Add(_objModuleContent);
					break;
				case ListItemType.AlternatingItem:
					HtmlTable _objAltContent = new HtmlTable();
					_objAltContent.CellPadding = 0;
					_objAltContent.CellSpacing = 0;
					_objAltContent.Width = "100%";
					_objAltContent.Rows.Add(new HtmlTableRow());
					_objAltContent.Rows[0].Cells.Add(new HtmlTableCell());
					_objAltContent.Rows[0].Cells[0].Attributes.Add("class", "Portal_SplitColumn_Table");
					_objAltContent.Rows[0].Cells[0].Controls.Add(ChildControl);
					container.Controls.Add(_objAltContent);
					break;

					// Hiển thị đoạn kết thúc Block
				case ListItemType.Footer:
					_renderStartHTMLControl1.Text = "<img height=\"2\" src=\"images/Snap/clear.gif\" border=\"0\">";
					container.Controls.Add(_renderStartHTMLControl1);
					break;
			}
		}
	}

	#endregion
}
