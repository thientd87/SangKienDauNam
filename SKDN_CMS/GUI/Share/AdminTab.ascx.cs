using System;
using System.Collections;
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
using DFISYS.User.Security;
using Cache = DFISYS.SiteSystem.Cache;

namespace DFISYS.GUI.Share
{
    public partial class AdminTab : BaseModule, INamingContainer
    {
        #region Variable
        private StringBuilder _strBuilderContainerList;
        //private StringBuilder _strExpandSnapScripts = new StringBuilder();
        private string _strContainerList;
        //private string _strFirstSendQueryCall = string.Empty;
        //private string _strLastSendQueryCall = string.Empty;

        protected ArrayList _arrAllSnap = new ArrayList();
        protected ArrayList _arrAllSnapHeaderDrag = new ArrayList();
        protected ArrayList _arrAllSnapHeaderCollapsedDrag = new ArrayList();
        protected ArrayList _arrAllSnapHeaderExpand = new ArrayList();
        protected ArrayList _arrAllEmptyColumn = new ArrayList();
        protected ArrayList _arrAllSnapHeaderCollapsedExpand = new ArrayList();

        protected API.Controls.LinkButton Linkbutton1;
        private bool IsAdmin = false;
        private bool IsAdminRole = false;
        #endregion

        #region Render Functions

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
            #region Khi phuong thuc lay la get
            // Kiểm tra nếu không có Module nào trong cột thì chèn khoảng trắng để lưu vị trí cột
            if (modules.Count == 0)
            {
                _arrAllEmptyColumn.Add(td);
            }
            // Duyệt danh sách Module
            for (int _intModuleCount = 0; _intModuleCount < modules.Count; _intModuleCount++)
            {
                PortalDefinition.Module _objModuleDefinition = modules[_intModuleCount] as PortalDefinition.Module;
                ChannelUsers objUser = new ChannelUsers();

                // Kiểm tra người dùng có quyền hiển thị Module hay không
                if (objUser.HasViewRights(Page.User, _objModuleDefinition.roles))
                {
                    // Nạp các thông số về Module
                    _objModuleDefinition.LoadModuleSettings();

                    // Khởi tạo Module
                    Module _objModuleControl = null;
                    CachedModule _objCachedModuleControl = null;
                    //try
                    //{
                    // Kiểm tra xem có thông số nào được xác lập không
                    // Xây dựng chuỗi đường dẫn đến tệp ASCX
                    string _strModuleSrc = Config.GetModuleVirtualPath(_objModuleDefinition.type);
                    if (_objModuleDefinition.moduleSettings == null)
                    {
                        //_objModuleDefinition.LoadRuntimeProperties();
                        //string strAjax=_objModuleDefinition.moduleRuntimeSettings.GetRuntimePropertyValue(true, "isAjax");
                        ////Xu ly truong hop co ajax request den ca trang. Neu Module k co Ajax thi k load
                        //if ((strAjax == "false" || strAjax =="")&& Request.HttpMethod =="POST")
                        //    continue;
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
                    #region phan bo di

                    // Kiểm tra xem Module có được phép hiển thị hay không
                    //if((_objModuleControl != null && _objModuleControl.IsVisible() || (_objCachedModuleControl != null && _objCachedModuleControl.IsVisible())))
                    {
                    #endregion
                        // Kiểm tra xem trang hiện thời có nằm trong trạng thái cho phép kéo thả hay không
                        // Nếu ở trạng thái hỗ trợ kéo thả thì tạo các khung chứa (Snap) để chứa nội dung của các Module 
                        // (nội dung này sẽ được nạp sử dụng Ajax khi người sử dụng mở rộng khung hoặc chế độ tự động mở rộng khung dc bật)
                        if (IsAdmin)
                        {
                            #region Xu ly voi cac module quan tri
                            // Khởi tạo phần tiêu đề của khung chứa Module (Khởi tạo 2 Control để tránh trùng lặp ID)
                            // Header khi chưa thu gọn
                            ModuleHeader _objModuleHeaderExpanded = (ModuleHeader)LoadControl("ModuleHeader.ascx");
                            _objModuleHeaderExpanded.SetModuleConfig(_objModuleDefinition);
                            // Header khi đã thu gọn
                            ModuleHeader _objModuleHeaderCollapsed = (ModuleHeader)LoadControl("ModuleHeader.ascx");
                            _objModuleHeaderCollapsed.SetModuleConfig(_objModuleDefinition);

                            // Sử dụng Snap Component để chứa Module và hỗ trợ khả năng kéo thả
                            // Khởi tạo Khung hiển thị của Module (Make Snap Instance)
                            Snap _objSnap = new Snap();
                            _objSnap.ClientSideCookieEnabled = false;								// Không lưu trữ Cookie khi thay đổi Snap
                            _objSnap.DraggingStyle = SnapDraggingStyleType.TransparentRectangle;	// Kiểu kéo khung, hiển thị 1 hình chữ nhật xám khi kéo khung
                            _objSnap.DockingStyle = SnapDockingStyleType.TransparentRectangle;		// Kiểu thả khung, hiển thị 1 hình chữ nhật xám tại nơi sẽ đặt
                            _objSnap.ResizingMode = SnapResizingType.Horizontal;					// Thay đổi kích thước tùy vào vùng đặt khung (đổi theo chiều ngang)
                            _objSnap.MustBeDocked = true;											// Khung chứa Module phải được gắn vào 1 vị trí cụ thể
                            _objSnap.ID = _objModuleDefinition.reference.Replace('-', '_');							// Thiết lập mã tham chiếu cho khung (cần đổi - thành _)
                            _objSnap.CurrentDockingIndex = _intModuleCount;							// Thứ tự đặt khung hiển thị
                            _objSnap.CollapseDuration = 0;
                            _objSnap.CollapseSlide = SlideType.None;
                            _objSnap.CollapseTransition = TransitionType.None;
                            _objSnap.ExpandDuration = 0;
                            _objSnap.ExpandSlide = SlideType.None;
                            _objSnap.ExpandTransition = TransitionType.None;
                            _objSnap.IsCollapsed = true;
                            _objSnap.Width = Unit.Percentage(100);

                            // Khai báo mã tham chiếu của các vùng có thể đặt Module
                            _objSnap.DockingContainers = _strContainerList;

                            // Xác lập mã tham thiếu của vùng đặt hiện thời
                            _objSnap.CurrentDockingContainer = td.Attributes["ID"].ToString();

                            string _strNewsRef = Request.QueryString["NewsRef"] == null ? "" : Request.QueryString["NewsRef"];

                            // Khai báo mẫu sử dụng của khung chứa Module
                            // Mẫu phần trên của khung
                            ModuleContainerTemplate _objHeaderTemplate = new ModuleContainerTemplate(ListItemType.Header, tab.reference, _objModuleDefinition.reference, _strNewsRef);
                            _objHeaderTemplate.HeaderContent = _objModuleDefinition.title;
                            _objHeaderTemplate.EditModuleControl = _objModuleHeaderExpanded;
                            // Lưu tham chiếu đến điều khiển chứa mã tham chiếu của Khung tại máy trạm
                            _arrAllSnapHeaderDrag.Add(_objHeaderTemplate._renderClientIDOfControlForDrag);
                            _arrAllSnapHeaderExpand.Add(_objHeaderTemplate._renderClientIDOfControlForExpand);

                            // Mẫu chứa phần trên của khung khi thu gọn
                            ModuleContainerTemplate _objCollapsedHeaderTemplate = new ModuleContainerTemplate(ListItemType.EditItem, tab.reference, _objModuleDefinition.reference, _strNewsRef);
                            _objCollapsedHeaderTemplate.HeaderContent = _objModuleDefinition.title;
                            _objCollapsedHeaderTemplate.EditModuleControl = _objModuleHeaderCollapsed;
                            // Lưu tham chiếu đến điều khiển chứa mã tham chiếu của Khung tại máy trạm
                            _arrAllSnapHeaderCollapsedDrag.Add(_objCollapsedHeaderTemplate._renderClientIDOfCollapsedControlForDrag);
                            _arrAllSnapHeaderCollapsedExpand.Add(_objCollapsedHeaderTemplate._renderClientIDOfCollapsedControlForExpand);

                            // Mẫu của phần giữa khung
                            ModuleContainerTemplate _objContentTemplate = new ModuleContainerTemplate(ListItemType.Item, tab.reference, _objModuleDefinition.reference, _strNewsRef);
                            Literal lh = new Literal();
                            lh.Text = "";
                            lh.EnableViewState = false;
                            _objContentTemplate.ChildControl = lh;

                            // Mẫu phần dưới của khung
                            ModuleContainerTemplate _objFooterTemplate = new ModuleContainerTemplate(ListItemType.Footer);

                            // Đặt mẫu vào các vị trí tương ứng của khung
                            _objSnap.HeaderTemplate = _objHeaderTemplate;
                            _objSnap.CollapsedHeaderTemplate = _objCollapsedHeaderTemplate;
                            _objSnap.ContentTemplate = _objContentTemplate;
                            _objSnap.FooterTemplate = _objFooterTemplate;

                            // Display Module
                            td.Controls.Add(_objSnap);

                            // Lưu tham chiếu đến khung để lấy mã tham chiếu tại máy trạm
                            _arrAllSnap.Add(_objSnap);
                            #endregion
                        }
                        else
                        {
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
                              //  _objCellContainer.Attributes.Add("class", "Module_Simple_Block");
                                if (_objModuleControl != null)
                                {
                                    _objCellContainer.Controls.Add(_objModuleControl);
                                }
                                else
                                {
                                    _objCellContainer.Controls.Add(_objCachedModuleControl);
                                }
                                //cách giữa header và body
                                _objCellContainer.Attributes.Add("style", "padding-bottom:3px;");
                                td.Controls.Add(_objSimpleModuleContainer);
                            }

                        }
                    }
                    
                }

            }
            #endregion
        #endregion
        }

        /// <summary>
        /// Thủ tục lấy danh sách mã tham chiếu của tất cả các vùng có thể đặt khung (bên trong chứa Module)
        /// </summary>
        /// <param name="_arrColumns">Mảng chứa danh sách cột xuất phát</param>
        /// <param name="_strColumnContainerList">
        /// Tham chiếu đến chuỗi chứa danh sách các mã tham chiếu cần lấy.
        /// Sử dụng StringBuilder để tiết kiệm bộ nhớ
        /// </param>
        private void GetContainerList(ArrayList _arrColumns, ref StringBuilder _strColumnContainerList)
        {
            // Nếu danh sách cột rỗng thì kết thúc hàm
            if (_arrColumns == null || _arrColumns.Count == 0) return;

            // Duyệt danh sách cột xuất phát
            for (int _intColumnCount = 0; _intColumnCount < _arrColumns.Count; _intColumnCount++)
            {
                // Tạo đối tượng đại diện cho cột đang xét
                PortalDefinition.Column _objColumn = (PortalDefinition.Column)_arrColumns[_intColumnCount];

                if (_objColumn != null)
                {
                    // Thêm mã tham chiếu của cột vào danh sách mã tham chiếu của các vùng có thể đặt khung
                    // Thêm vào đầu danh sách để không phải đảo ngược lại danh sách
                    // (Cần danh sách ngược do Snap Component bắt Docking theo cây Controls,
                    // chỉ Dock được các điều khiển con sau đó mới đến điều khiển cha
                    if (_strColumnContainerList.Length > 0) _strColumnContainerList.Insert(0, ',');
                    _strColumnContainerList.Insert(0, _objColumn.ColumnReference.Replace('-', '_'));
                    _strColumnContainerList.Insert(0, "Cell");

                    // Tiếp tục gọi đệ quy với các cột con của cột đang xét
                    GetContainerList(_objColumn.Columns, ref _strColumnContainerList);
                }
            }
        }

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

            // Khai báo mảng chứa các điều khiển bảng dùng để chứa cột
            ArrayList _arrTableIndexes = new ArrayList();

            // Duyệt danh sách các cột xuất phát
            for (int _intColumnCount = 0; _intColumnCount < _arrColumns.Count; _intColumnCount++)
            {
                // Lấy thông tin về cột đang xét
                PortalDefinition.Column _objColumn = _arrColumns[_intColumnCount] as PortalDefinition.Column;
                SortableHtmlTable _objTable = null;
                HtmlTableRow _objTableRow = null;
                HtmlTableRow _objTableHeaderRow = null;
                HtmlTableRow _objTableFooterRow;

                // Duyệt danh sách các bảng đã tạo
                // Các cột có cùng cấp độ sẽ nằm cùng một bảng
                foreach (SortableHtmlTable _tblContainer in _arrTableIndexes)
                {
                    // Kiểm tra cấp độ của cột đang xét
                    if (_tblContainer.Attributes["level"] == _objColumn.ColumnLevel.ToString())
                    {
                        // Nếu bảng đang xét có cùng cấp độ với cột đang xét
                        // thì lấy tham chiếu đến bảng và dòng đầu tiên của bảng
                        _objTable = _tblContainer;
                        _objTableHeaderRow = _tblContainer.Rows[0];
                        _objTableRow = _tblContainer.Rows[1];
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
                    _objTableHeaderRow = new HtmlTableRow();
                    _objTable.Rows.Add(_objTableHeaderRow);

                    // Tạo dòng chứa các cột
                    _objTableRow = new HtmlTableRow();
                    _objTable.Rows.Add(_objTableRow);
                    _arrTableIndexes.Add(_objTable);
                }

                // Khởi tạo điều khiển đại diện cho 1 cột
                HtmlTableCell _objCellContainer = new HtmlTableCell();
                _objCellContainer.VAlign = "top";
               // _objCellContainer.Attributes.Add("id", "Cell" + _objColumn.ColumnReference.Replace('-', '_'));
               // _objCellContainer.Attributes.Add("class", IsAdmin ? "Portal_Column" : "Portal_NormalColumn");

                if (_objColumn.ColumnCustomStyle != null && _objColumn.ColumnCustomStyle != "") _objCellContainer.Attributes.Add("style", _objColumn.ColumnCustomStyle);

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
                // Thêm cột vừa tạo vào bảng
                _objTableRow.Cells.Add(_objCellContainer);


                // Sắp xếp các Module đã được thiết lập cho cột đang xét
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
                foreach (HtmlTable _objIndexedTable in _arrTableIndexes)
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

                //intFixedColumns = _objTable.Rows[0].Cells.Count - _intFixedColumns;

                // Duyệt danh sách các cột của bảng (thuộc dòng đầu tiên)
                for (int _intCellCount = 0; _intCellCount < _objTable.Rows[0].Cells.Count; _intCellCount++)
                {
                    HtmlTableCell _objColumnHeaderCell = _objTable.Rows[0].Cells[_intCellCount];
                    HtmlTableCell _objColumnContentCell = _objTable.Rows[1].Cells[_intCellCount];
                    // Chia đều độ rộng cho các cột
                    //_objCell.Width = _objCell.Width != "" ? _objCell.Width : ((IsAdmin() ? 200 : 100) / (_intFixedColumns > 0 ? (IsAdmin() ? (_intFixedColumns * 2) : _intFixedColumns) : _objTable.Rows[0].Cells.Count)).ToString() + "%";
                    _objColumnHeaderCell.Width = _objColumnHeaderCell.Width != "" ? _objColumnHeaderCell.Width : "*";
                    _objColumnContentCell.Width = _objColumnContentCell.Width != "" ? _objColumnContentCell.Width : "*";
                }

                 
            }
        }

        #region Permission Check
        /// <summary>
        /// Hàm xác định đang ở chế độ cho phép di chuyển Module hay không
        /// </summary>
        /// <returns></returns>
        private bool IsAllowArrangeModules()
        {
            return Session["AllowArrangeModules"] == null ? false : Convert.ToBoolean(Session["AllowArrangeModules"]);
        }

        /// <summary>
        /// Hàm kiểm tra người dùng hiện thời có quyền cao nhất hay không 
        /// và có thể thực hiện kéo thả module hay không
        /// </summary>
        /// <returns>True: Đúng, False: Sai</returns>
        private bool IsAdminMode()
        {
            return IsInAdminRole() && IsAllowArrangeModules();//Page.User.IsInRole(DFISYS.API.Config.AdminRole)
        }

        /// <summary>
        /// Hàm kiểm tra người dùng hiện thời có quyền cao nhất hay không
        /// </summary>
        /// <returns>True: Đúng, False: Sai</returns>
        private bool IsInAdminRole()
        {
            string strCacheName = "IsInAdminRole_" + Page.User.Identity.Name;
            if (HttpContext.Current.Cache[strCacheName] == null)
            {
                string strAdminRole = Config.AdminRoles;
                if (Page.User.IsInRole(strAdminRole))
                {
                    SaveCache(strCacheName, "true");
                    return true;
                }

                MainSecurity objSecu = new MainSecurity();
                Role objrole = objSecu.GetRole(Page.User.Identity.Name);

                if (objrole.isTongBienTap || objrole.isQuanTriKenh)
                {
                    SaveCache(strCacheName, "true");
                    return true;
                }
                
                SaveCache(strCacheName, "false");
                return false;
            }
            else
            {
                //try
                //{
                bool isReturn = Convert.ToBoolean(HttpContext.Current.Cache[strCacheName].ToString());
                return isReturn;
                //}
                //catch { }
            }
            //return false;

        }

        private void SaveCache(string strCacheName, string value)
        {
            HttpContext.Current.Cache.Add(strCacheName, value, null, DateTime.Now.AddHours(24), TimeSpan.Zero, CacheItemPriority.High, null);
        }
        #endregion

        #region Override Functions
         

        /// <summary>
        /// Nạp chồng Thủ tục khởi tạo các điều khiển con
        /// </summary>
        override protected void CreateChildControls()
        {
            // Lấy thông tin Tab hiện thời
            PortalDefinition.Tab tab = PortalDefinition.GetCurrentTab();
            if (tab == null) return;

            // Kiểm tra quyền của người sử dụng
            ChannelUsers objuser = new ChannelUsers();
            

            if (objuser.HasViewRights(Page.User, tab.roles))
            {
                

                // Tiến hành sinh mã cho các cột của Tab
                // Vùng bắt đầu là DisplayRegion.
                RenderColumns(tab, tab.Columns, DisplayRegion);

                 
            }
            else
            {
                // Add by tqdat
                Cache cache = new Cache(HttpContext.Current.Application);
                string path = HttpContext.Current.Request.Url.AbsolutePath.ToLower();
                string _strCacheKey = Config.GetPortalUniqueCacheKey() + path + "_" + HttpContext.Current.User.Identity.Name;
                string _strCacheRawKey = Config.GetPortalUniqueCacheKey() + "_Raw" + HttpContext.Current.Request.RawUrl + "_" + HttpContext.Current.User.Identity.Name;
                cache[_strCacheKey] = null;
                cache[_strCacheRawKey] = null;
                Session["lastPath"] = HttpContext.Current.Request.RawUrl;


                // Neu khong duoc fep access vao hoac Mat Session thi redirect ve trang Login

                Response.Redirect("/login.aspx");
            }
        }
        #endregion

        protected void OnSignOut(object sender, EventArgs args)
        {
            HttpCookie cookie = Request.Cookies["PortalUser"];
            if (cookie != null)
            {
                cookie.Values["AC"] = "";
                cookie.Values["PW"] = "";
                DateTime dt = DateTime.Now;
                dt.AddDays(-1);
                cookie.Expires = dt;
                Response.Cookies.Add(cookie);
            }
            FormsAuthentication.SignOut();
            Context.User = null;
            Response.Redirect("/login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
 
        }
        /// <summary>
        /// Ham thuc hien luu cac response xuong html cache file
        /// </summary

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

    }
}