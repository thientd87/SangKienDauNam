using System;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.UI;
using Portal.API;
using Portal.API.Controls;
using Portal.Ultility;

namespace Portal.GUI.Administrator.AdminPortal
{
    /// <summary>
    ///		Summary description for Tab.
    /// </summary>
    public partial class Tab : UserControl
    {
        #region Delegates

        public delegate void ColumnEventHandler(object sender, PortalDefinition.Column column);

        public delegate void TabEventHandler(object sender, PortalDefinition.Tab tab);

        #endregion

        protected ColumnEdit ColumnEditCtrl;
        private string CurrentColumnReference = "";
        private string CurrentReference = "";
        protected ColumnList lstColumns;
        protected ModuleList lstModules;
        protected ModuleEdit ModuleEditCtrl;
        protected Roles RolesCtrl;

        // Lưu mã tham chiếu của Tab đang chọn

        // Khai báo các sự kiện sẽ có thể phát sinh khi quản lý một Tab
        public event TabEventHandler Save = null;
        public event TabEventHandler Cancel = null;
        public event TabEventHandler Delete = null;
        public event TabEventHandler SaveTemplate = null;

        #region PageLoad & Read/Write ViewState

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override void LoadViewState(object bag)
        {
            base.LoadViewState(bag);
            CurrentReference = (string) ViewState["CurrentReference"];
            CurrentColumnReference = (string) ViewState["CurrentColumnReference"];
        }

        protected override object SaveViewState()
        {
            ViewState["CurrentReference"] = CurrentReference;
            ViewState["CurrentColumnReference"] = CurrentColumnReference;
            return base.SaveViewState();
        }

        #endregion

        #region Edit UserControl Visible

        private void ShowEditModules()
        {
            ModuleEditCtrl.Visible = true;
            ColumnEditCtrl.Visible = false;
            lstColumns.Visible = false;
            lstModules.Visible = false;
        }

        private void ShowModulesList()
        {
            ModuleEditCtrl.Visible = false;
            ColumnEditCtrl.Visible = false;
            lstModules.Visible = true;
        }

        private void ShowEditColumns()
        {
            ModuleEditCtrl.Visible = false;
            ColumnEditCtrl.Visible = true;
            lstModules.Visible = true;
            lstColumns.Visible = true;
        }

        private void ShowColumnsList()
        {
            ModuleEditCtrl.Visible = false;
            ColumnEditCtrl.Visible = false;
            lstModules.Visible = false;
            lstColumns.Visible = true;
        }

        private void ShowCurrentEditingColumn()
        {
            PortalDefinition pd = PortalDefinition.Load();
            PortalDefinition.Column _objCurrentColumn = pd.GetColumn(CurrentColumnReference);

            if (_objCurrentColumn != null)
            {
                EditColumn(_objCurrentColumn.ColumnReference);
            }
        }

        private void ShowCurrentEditingParentColumn()
        {
            PortalDefinition pd = PortalDefinition.Load();
            PortalDefinition.Column _objCurrentColumn = pd.GetColumn(CurrentColumnReference);

            if (_objCurrentColumn != null && _objCurrentColumn.ColumnParent != null)
            {
                EditColumn(_objCurrentColumn.ColumnParent.ColumnReference);
            }
        }

        private void ShowCurrentEditingParentColumn(string ParentColumnReference)
        {
            PortalDefinition pd = PortalDefinition.Load();
            PortalDefinition.Column _objCurrentColumn = pd.GetColumn(ParentColumnReference);

            if (_objCurrentColumn != null)
            {
                EditColumn(_objCurrentColumn.ColumnReference);
            }
        }

        #endregion

        #region Load Data Methods

        /// <summary>
        /// Thủ tục nạp dữ liệu về Tab đang sửa
        /// </summary>
        /// <param name="tabRef">Mã tham chiếu đến Tab đang sửa</param>
        public void LoadData(string tabRef)
        {
            CurrentReference = tabRef;

            PortalDefinition pd = PortalDefinition.Load();
            PortalDefinition.Tab t = pd.GetTab(CurrentReference);

            txtTitle.Text = HttpUtility.HtmlDecode(t.title);
            txtReference.Text = CurrentReference;
            chkIsHidden.Checked = t.IsHidden;

            TemplateDefinition td = TemplateDefinition.Load();
            ArrayList _arrTemplateList = new ArrayList();
            _arrTemplateList.Add(string.Empty);
            foreach(TemplateDefinition.Template template in td.templates)
            {
                _arrTemplateList.Add(template.reference);
            }
            ddrTemplateList.DataSource = _arrTemplateList;
            ddrTemplateList.DataBind();

            //string _strSkinFolder = Portal.Ultility.ConfigurationSetting.GetConfigPhysicalPath(t.reference, Config.GetAppConfigValue("SkinPrefix"), Config.GetAppConfigValue("SkinFolder"));
            string _strSkinFolder = Server.MapPath("");
            ArrayList _arrConfigFileList = new ArrayList();
            _arrConfigFileList.Add("Skin.Default.config");
            int _intSelectedIndex = 0;
            if (Directory.Exists(_strSkinFolder))
            {
                DirectoryInfo _difConfig = new DirectoryInfo(_strSkinFolder);
                foreach (FileInfo _objConfigFile in _difConfig.GetFiles())
                {
                    string _strFileNamePart = "" + "." + t.reference + ".";
                    if (_objConfigFile.Name.IndexOf(_strFileNamePart) >= 0)
                    {
                        if (_objConfigFile.Name == t.SkinName) _intSelectedIndex = _arrConfigFileList.Count;
                        _arrConfigFileList.Add(_objConfigFile.Name);
                    }
                }
            }
            ddrSkins.DataSource = _arrConfigFileList;
            ddrSkins.DataBind();
            if (_arrConfigFileList.Count > 0)
            {
                ddrSkins.SelectedIndex = _intSelectedIndex;
            }

            RolesCtrl.LoadData(t.roles);

            // Đọc danh sách Columns của Tab
            // Hiển thị danh sách các Module được gắn vào từng Column
            lstColumns.LoadColumnList(t);

            ShowColumnsList();
        }

        internal void EditModule(string reference)
        {
            ShowEditModules();
            ModuleEditCtrl.LoadData(CurrentReference, reference);
        }

        /// <summary>
        /// Thủ tục hiển thị form chỉnh sửa thông tin cột
        /// </summary>
        /// <param name="_strColumnRef"></param>
        internal void EditColumn(string _strColumnRef)
        {
            // Lưu lại mã tham chiếu của cột đang sửa
            CurrentColumnReference = _strColumnRef;

            // Thiết lập trạng thái các Control
            ShowEditColumns();

            // Nạp form chỉnh sửa thông tin cột
            ColumnEditCtrl.LoadData(_strColumnRef, CurrentReference);

            // Nạp danh sách Module của cột cần sửa
            (lstModules.FindControl("lnkAddModule") as LinkButton).CommandArgument = _strColumnRef;
            lstModules.LoadData(_strColumnRef);
            lstModules.ContainerColumnReference = _strColumnRef;

            // Nạp danh sách cột con của cột cần sửa
            lstColumns.LoadColumnList(_strColumnRef);
        }

        #endregion

        #region ModuleEditCtrl Event Handler

        protected void OnCancelEditModule(object sender, EventArgs args)
        {
            ShowCurrentEditingColumn();
        }

        protected void OnSaveModule(object sender, EventArgs args)
        {
            // Rebind
            LoadData(CurrentReference);

            ShowCurrentEditingColumn();
        }

        protected void OnDeleteModule(object sender, EventArgs args)
        {
            // Rebind
            LoadData(CurrentReference);

            ShowCurrentEditingColumn();
        }

        #endregion

        #region ColumnEditCtrl Event Handler

        protected void OnCancelEditColumn(object sender, EventArgs args)
        {
            LoadData(CurrentReference);

            ShowCurrentEditingParentColumn();
        }

        protected void OnSaveColumn(object sender, EventArgs args)
        {
            // Rebind
            LoadData(CurrentReference);

            ShowCurrentEditingParentColumn();
        }

        protected void OnDeleteColumn(string DeletedColumnReference, string ParentColumnReference)
        {
            // Rebind
            LoadData(CurrentReference);

            ShowCurrentEditingParentColumn(ParentColumnReference);
        }

        #endregion

        #region ModuleList Event Handler

        /// <summary>
        /// Thủ tục thực hiện chuyển vị trí của Module đã chọn lên 1 mức
        /// </summary>
        /// <param name="idx">Vị trí hiện thời</param>
        /// <param name="list">Danh sách Module</param>
        internal void MoveModuleUp(int idx, ModuleList list)
        {
            // Nếu Module đang ở mức đầu tiên thì kết thúc thủ tục
            if (idx <= 0) return;

            // Nạp cấu trúc Portal
            PortalDefinition pd = PortalDefinition.Load();

            // Lấy thông tin cột chứa Module hiện thời
            PortalDefinition.Column _objColumnContainer = pd.GetColumn(list.ContainerColumnReference);

            // Lấy danh sách Module của cột
            ArrayList _arrModuleList = _objColumnContainer.ModuleList;

            // Lấy thông tin Module hiện thời từ danh sách Module
            PortalDefinition.Module m = (PortalDefinition.Module) _arrModuleList[idx];

            // Gỡ Module ra khỏi vị trí hiện thời
            _arrModuleList.RemoveAt(idx);

            // Chèn Module vào vị trí mới
            _arrModuleList.Insert(idx - 1, m);

            // Lưu cấu trúc Portal
            pd.Save();

            // Rebind
            LoadData(CurrentReference);
            ShowCurrentEditingColumn();
        }

        /// <summary>
        /// Thủ tục thực hiện chuyển vị trí của Module đã chọn xuống 1 mức
        /// </summary>
        /// <param name="idx">Vị trí hiện thời</param>
        /// <param name="list">Danh sách Module</param>
        internal void MoveModuleDown(int idx, ModuleList list)
        {
            // Nạp cấu trúc Portal
            PortalDefinition pd = PortalDefinition.Load();

            // Lấy thông tin cột chứa Module hiện thời
            PortalDefinition.Column _objColumnContainer = pd.GetColumn(list.ContainerColumnReference);

            // Lấy danh sách Module của cột
            ArrayList _arrModuleList = _objColumnContainer.ModuleList;

            // Nếu Module đang ở mức cuối cùng thì kết thúc thủ tục
            if (idx >= _arrModuleList.Count - 1) return;

            // Lấy thông tin Module hiện thời từ danh sách Module
            PortalDefinition.Module m = (PortalDefinition.Module) _arrModuleList[idx];

            // Gỡ Module ra khỏi vị trí hiện thời
            _arrModuleList.RemoveAt(idx);

            // Chèn Module vào vị trí mới
            _arrModuleList.Insert(idx + 1, m);

            // Lưu cấu trúc Portal
            pd.Save();

            // Rebind
            LoadData(CurrentReference);
            ShowCurrentEditingColumn();
        }

        /// <summary>
        /// Thủ tục thêm một Module mới
        /// </summary>
        /// <param name="_strColumnRef">Mã tham chiếu đến cột sẽ chứa Module</param>
        internal void AddModule(string _strColumnRef)
        {
            // Nạp cấu trúc portal
            PortalDefinition pd = PortalDefinition.Load();

            // Tìm cột sẽ chứa Module mới
            PortalDefinition.Column _objColumnContainer = pd.GetColumn(_strColumnRef);

            if (_objColumnContainer != null)
            {
                // Tạo Module mới
                PortalDefinition.Module _objNewModule = PortalDefinition.Module.Create();

                // Thêm Module mới vào danh sách Module của cột
                _objColumnContainer.ModuleList.Add(_objNewModule);

                // Lưu cấu trúc Portal
                pd.Save();

                // Nạp lại thông tin Tab hiện thời
                LoadData(CurrentReference);

                // Hiển thị form sửa thông tin Module
                EditModule(_objNewModule.reference);
            }
        }

        #endregion

        #region TabEditCtrl Event Handler

        protected void OnCancel(object sender, EventArgs args)
        {
            PortalDefinition pd = PortalDefinition.Load();
            PortalDefinition.Tab t = pd.GetTab(CurrentReference);

            if (Cancel != null)
            {
                Cancel(this, t);
            }

            LoadData(CurrentReference);
            ShowModulesList();
        }


        protected void OnSave(object sender, EventArgs args)
        {
            try
            {
                if (!Page.IsValid) return;

                PortalDefinition pd = PortalDefinition.Load();
                PortalDefinition.Tab t = pd.GetTab(CurrentReference);

                t.title = HttpUtility.HtmlEncode(txtTitle.Text);
                t.reference = txtReference.Text;
                t.SkinName = ddrSkins.SelectedValue;
                t.IsHidden = chkIsHidden.Checked;
                t.roles = RolesCtrl.GetData();

                if(!string.Empty.Equals(ddrTemplateList.SelectedValue))
                {
                    //get information about selected template
                    TemplateDefinition td = TemplateDefinition.Load();
                    TemplateDefinition.Template template = td.GetTemplate(ddrTemplateList.SelectedValue);

                    //apply template to tab by replace columns's tab by columns'template
                    t.Columns = (ArrayList)template.Columns.Clone();
                }

                pd.Save();

                CurrentReference = t.reference;

                if (Save != null)
                {
                    Save(this, t);
                }

                ShowModulesList();
                LoadData(CurrentReference);
            }
            catch (Exception e)
            {
                lbError.Text = e.Message;
            }
        }

        protected void OnSaveTemplate(object sender, EventArgs args)
        {
            //Load PortalDefinition to get current Tab and Load TemplateDefinition to Save template
            PortalDefinition _objPortal = PortalDefinition.Load();
            PortalDefinition.Tab _objCurrentTab = _objPortal.GetTab(CurrentReference);

            if (_objCurrentTab != null)
            {
                try
                {
                    //Save to PortalDefinition
                    _objPortal.TemplateColumns = _objCurrentTab.Columns;
                    _objPortal.TemplateReference = _objCurrentTab.reference;
                    _objPortal.Save();

                    //Save to TemplateDefinition
                    TemplateDefinition _objTemplate = TemplateDefinition.Load();
                    TemplateDefinition.Template t = new TemplateDefinition.Template();

                    t.reference = txtReference.Text;
                    t.type = Definition.CT_TYPE_TEMPLATE;
                    t.Columns = _objCurrentTab.Columns;
                    _objTemplate.templates.Add(t);

                    _objTemplate.Save();
                }
                catch (Exception e)
                {
                    lbError.Text = e.Message;
                }
            }
        }

        protected void OnDelete(object sender, EventArgs args)
        {
            PortalDefinition pd = PortalDefinition.Load();
            PortalDefinition.Tab t = pd.GetTab(CurrentReference);
            PortalDefinition.DeleteTab(CurrentReference);

            if (Delete != null)
            {
                Delete(this, t);
            }
        }

        #endregion

        #region ColumnList Event Handler

        protected void OnAddColumn(object sender, PortalDefinition.Column _objNewColumn)
        {
            EditColumn(_objNewColumn.ColumnReference);
        }

        /// <summary>
        /// Thủ tục dịch chuyển một cột sang trái một vị trí
        /// </summary>
        /// <param name="_strColumnReference">Mã tham chiếu của cột cần dịch chuyển</param>
        internal void MoveColumnLeft(string _strColumnReference)
        {
            // Nạp cấu trúc Portal
            PortalDefinition _objPortal = PortalDefinition.Load();
            // Lấy thông tin về cột cần dịch chuyển
            PortalDefinition.Column _objSelectedColumn = _objPortal.GetColumn(_strColumnReference);

            // Nếu cột cần dịch chuyển không tồn tại thì kết thúc hàm
            if (_objSelectedColumn != null)
            {
                // Tìm kiếm danh sách cột trong đó có cột đang xét
                PortalDefinition.Column _objParentColumn = _objSelectedColumn.ColumnParent;
                ArrayList _arrColumnsList = null;

                // Nếu cột cha rỗng thì cột đang xét trực thuộc Tab
                if (_objParentColumn == null)
                {
                    // Lấy thông tin Tab hiện thời
                    PortalDefinition.Tab _objCurrentTab = _objPortal.GetTab(CurrentReference);
                    if (_objCurrentTab != null)
                    {
                        // Lấy danh sách cột của Tab
                        _arrColumnsList = _objCurrentTab.Columns;
                    }
                }
                else
                {
                    // Nếu cột đang xét là cột con của 1 cột khác, thì trả về danh sách các cột đồng cấp
                    _arrColumnsList = _objParentColumn.Columns;
                }

                // Biến lưu vị trí của cột đã chọn trong danh sách
                int _intCurrentColumnIndex = 0;

                // Biến lưu trữ số cột đồng mức với cột đã chọn
                int _intSameLevelColumnsCount = 0;

                // Kiểm duyệt danh sách cột đồng cấp
                // Đếm cột có cùng Level
                for (int _intColumnCount = 0; _intColumnCount < _arrColumnsList.Count; _intColumnCount++)
                {
                    PortalDefinition.Column _objColumn = _arrColumnsList[_intColumnCount] as PortalDefinition.Column;
                    if (_objColumn.ColumnLevel == _objSelectedColumn.ColumnLevel)
                    {
                        if (_objColumn.ColumnReference == _objSelectedColumn.ColumnReference)
                        {
                            _intCurrentColumnIndex = _intSameLevelColumnsCount;
                        }
                        _intSameLevelColumnsCount++;
                    }
                }

                // Duyệt danh sách các cột cùng cấp
                if (_arrColumnsList != null && _intSameLevelColumnsCount > 0)
                {
                    // Để di chuyển sang trái --> không thể là ở vị trí đầu tiên
                    if (_intCurrentColumnIndex == 0)
                    {
                        return;
                    }
                    else
                    {
                        // Di chuyển cột đã chọn sang trái
                        _arrColumnsList.RemoveAt(_intCurrentColumnIndex);
                        _arrColumnsList.Insert(_intCurrentColumnIndex - 1, _objSelectedColumn);
                    }
                }
            }

            // Lưu cấu trúc Portal
            _objPortal.Save();

            // Nạp dữ liệu Tab
            LoadData(CurrentReference);

            // Nạp dữ liệu cột
            if (CurrentColumnReference != "")
            {
                EditColumn(CurrentColumnReference);
            }
        }

        internal void MoveColumnRight(string _strColumnReference)
        {
            // Nạp cấu trúc Portal
            PortalDefinition _objPortal = PortalDefinition.Load();
            // Lấy thông tin về cột cần dịch chuyển
            PortalDefinition.Column _objSelectedColumn = _objPortal.GetColumn(_strColumnReference);

            // Nếu cột cần dịch chuyển không tồn tại thì kết thúc hàm
            if (_objSelectedColumn != null)
            {
                // Tìm kiếm danh sách cột trong đó có cột đang xét
                PortalDefinition.Column _objParentColumn = _objSelectedColumn.ColumnParent;
                ArrayList _arrColumnsList = null;

                // Nếu cột cha rỗng thì cột đang xét trực thuộc Tab
                if (_objParentColumn == null)
                {
                    // Lấy thông tin Tab hiện thời
                    PortalDefinition.Tab _objCurrentTab = _objPortal.GetTab(CurrentReference);
                    if (_objCurrentTab != null)
                    {
                        // Lấy danh sách cột của Tab
                        _arrColumnsList = _objCurrentTab.Columns;
                    }
                }
                else
                {
                    // Nếu cột đang xét là cột con của 1 cột khác, thì trả về danh sách các cột đồng cấp
                    _arrColumnsList = _objParentColumn.Columns;
                }

                // Biến lưu vị trí của cột đã chọn trong danh sách
                int _intCurrentColumnIndex = 0;

                // Biến lưu trữ số cột đồng mức với cột đã chọn
                int _intSameLevelColumnsCount = 0;

                // Kiểm duyệt danh sách cột đồng cấp
                // Đếm cột có cùng Level
                for (int _intColumnCount = 0; _intColumnCount < _arrColumnsList.Count; _intColumnCount++)
                {
                    PortalDefinition.Column _objColumn = _arrColumnsList[_intColumnCount] as PortalDefinition.Column;
                    if (_objColumn.ColumnLevel == _objSelectedColumn.ColumnLevel)
                    {
                        if (_objColumn.ColumnReference == _objSelectedColumn.ColumnReference)
                        {
                            _intCurrentColumnIndex = _intSameLevelColumnsCount;
                        }
                        _intSameLevelColumnsCount++;
                    }
                }

                // Duyệt danh sách các cột cùng cấp
                if (_arrColumnsList != null && _intSameLevelColumnsCount > 0)
                {
                    // Để di chuyển sang phải --> không thể đang là vị trí cuối cùng
                    if (_intCurrentColumnIndex >= (_intSameLevelColumnsCount - 1))
                    {
                        return;
                    }
                    else
                    {
                        // Di chuyển cột đã chọn sang trái
                        _arrColumnsList.RemoveAt(_intCurrentColumnIndex);
                        _arrColumnsList.Insert(_intCurrentColumnIndex + 1, _objSelectedColumn);
                    }
                }
            }

            // Lưu cấu trúc Portal
            _objPortal.Save();

            // Nạp dữ liệu Tab
            LoadData(CurrentReference);

            // Nạp dữ liệu cột
            if (CurrentColumnReference != "")
            {
                EditColumn(CurrentColumnReference);
            }
        }

        #endregion

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
        {
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