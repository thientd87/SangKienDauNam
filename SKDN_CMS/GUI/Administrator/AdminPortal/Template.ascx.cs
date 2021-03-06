using System;
using System.Collections;
using System.Web.UI;
using Portal.API;
using Portal.API.Controls;

namespace Portal.GUI.Administrator.AdminPortal
{
    public partial class Template : UserControl
    {
        // Lưu mã tham chiếu của Template đang chọn

        #region Delegates

        public delegate void ColumnEventHandler(object sender, PortalDefinition.Column column);

        public delegate void TemplateEventHandler(object sender, TemplateDefinition.Template template);

        #endregion

        private string CurrentColumnReference = "";
        private string CurrentTemplateReference = "";

        // Khai báo các sự kiện sẽ có thể phát sinh khi quản lý một Tab
        public event TemplateEventHandler Save = null;
        public event TemplateEventHandler Cancel = null;
        public event TemplateEventHandler Delete = null;

        #region PageLoad & Read/Write ViewState

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void LoadViewState(object bag)
        {
            base.LoadViewState(bag);
            CurrentTemplateReference = (string) ViewState["CurrentTemplateReference"];
            CurrentColumnReference = (string) ViewState["CurrentColumnReference"];
        }

        protected override object SaveViewState()
        {
            ViewState["CurrentTemplateReference"] = CurrentTemplateReference;
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
            TemplateDefinition td = TemplateDefinition.Load();
            PortalDefinition.Column _objCurrentColumn = td.GetColumn(CurrentColumnReference);

            if (_objCurrentColumn != null)
            {
                EditColumn(_objCurrentColumn.ColumnReference);
            }
        }

        private void ShowCurrentEditingParentColumn()
        {
            TemplateDefinition td = TemplateDefinition.Load();
            PortalDefinition.Column _objCurrentColumn = td.GetColumn(CurrentColumnReference);

            if (_objCurrentColumn != null && _objCurrentColumn.ColumnParent != null)
            {
                EditColumn(_objCurrentColumn.ColumnParent.ColumnReference);
            }
        }

        private void ShowCurrentEditingParentColumn(string ParentColumnReference)
        {
            TemplateDefinition td = TemplateDefinition.Load();
            PortalDefinition.Column _objCurrentColumn = td.GetColumn(ParentColumnReference);

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
        public void LoadData(string templateRef)
        {
            CurrentTemplateReference = templateRef;

            TemplateDefinition td = TemplateDefinition.Load();
            TemplateDefinition.Template t = td.GetTemplate(CurrentTemplateReference);

            txtReference.Text = CurrentTemplateReference;


            string[] _arrTemplateType = new string[1] { ""};
            ArrayList _arrTemplateTypeList = new ArrayList();
            int _intSelectedIndex = 0;
            for (int i = 0; i < _arrTemplateType.Length; i++)
            {
                _arrTemplateTypeList.Add(_arrTemplateType[i]);
                if (t.type.Equals(_arrTemplateType[i]))
                {
                    _intSelectedIndex = i;
                }
            }
            ddrTemplateType.DataSource = _arrTemplateTypeList;
            ddrTemplateType.DataBind();


            if (_arrTemplateTypeList.Count > 0)
            {
                ddrTemplateType.SelectedIndex = _intSelectedIndex;
            }

            // Đọc danh sách Columns của Template
            // Hiển thị danh sách các Module được gắn vào từng Column
            lstColumns.LoadColumnList(t);

            ShowColumnsList();
        }

        internal void EditModule(string reference)
        {
            ShowEditModules();
            ModuleEditCtrl.LoadData(CurrentTemplateReference, reference);
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
            ColumnEditCtrl.LoadData(_strColumnRef, CurrentTemplateReference);

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
            LoadData(CurrentTemplateReference);

            ShowCurrentEditingColumn();
        }

        protected void OnDeleteModule(object sender, EventArgs args)
        {
            // Rebind
            LoadData(CurrentTemplateReference);

            ShowCurrentEditingColumn();
        }

        #endregion

        #region ColumnEditCtrl Event Handler

        protected void OnCancelEditColumn(object sender, EventArgs args)
        {
            LoadData(CurrentTemplateReference);

            ShowCurrentEditingParentColumn();
        }

        protected void OnSaveColumn(object sender, EventArgs args)
        {
            // Rebind
            LoadData(CurrentTemplateReference);

            ShowCurrentEditingParentColumn();
        }

        protected void OnDeleteColumn(string DeletedColumnReference, string ParentColumnReference)
        {
            // Rebind
            LoadData(CurrentTemplateReference);

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
            TemplateDefinition td = TemplateDefinition.Load();

            // Lấy thông tin cột chứa Module hiện thời
            PortalDefinition.Column _objColumnContainer = td.GetColumn(list.ContainerColumnReference);

            // Lấy danh sách Module của cột
            ArrayList _arrModuleList = _objColumnContainer.ModuleList;

            // Lấy thông tin Module hiện thời từ danh sách Module
            PortalDefinition.Module m = (PortalDefinition.Module) _arrModuleList[idx];

            // Gỡ Module ra khỏi vị trí hiện thời
            _arrModuleList.RemoveAt(idx);

            // Chèn Module vào vị trí mới
            _arrModuleList.Insert(idx - 1, m);

            // Lưu cấu trúc Portal
            td.Save();

            // Rebind
            LoadData(CurrentTemplateReference);
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
            TemplateDefinition td = TemplateDefinition.Load();

            // Lấy thông tin cột chứa Module hiện thời
            PortalDefinition.Column _objColumnContainer = td.GetColumn(list.ContainerColumnReference);

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
            td.Save();

            // Rebind
            LoadData(CurrentTemplateReference);
            ShowCurrentEditingColumn();
        }

        /// <summary>
        /// Thủ tục thêm một Module mới
        /// </summary>
        /// <param name="_strColumnRef">Mã tham chiếu đến cột sẽ chứa Module</param>
        internal void AddModule(string _strColumnRef)
        {
            // Nạp cấu trúc portal
            TemplateDefinition td = TemplateDefinition.Load();

            // Tìm cột sẽ chứa Module mới
            PortalDefinition.Column _objColumnContainer = td.GetColumn(_strColumnRef);

            if (_objColumnContainer != null)
            {
                // Tạo Module mới
                PortalDefinition.Module _objNewModule = PortalDefinition.Module.Create();

                // Thêm Module mới vào danh sách Module của cột
                _objColumnContainer.ModuleList.Add(_objNewModule);

                // Lưu cấu trúc Portal
                td.Save();

                // Nạp lại thông tin Tab hiện thời
                LoadData(CurrentTemplateReference);

                // Hiển thị form sửa thông tin Module
                EditModule(_objNewModule.reference);
            }
        }

        #endregion

        #region TemplateEditCtrl Event Handler

        protected void OnCancel(object sender, EventArgs args)
        {
            TemplateDefinition td = TemplateDefinition.Load();
            TemplateDefinition.Template t = td.GetTemplate(CurrentTemplateReference);

            if (Cancel != null)
            {
                Cancel(this, t);
            }

            LoadData(CurrentTemplateReference);
            ShowModulesList();
        }


        protected void OnSave(object sender, EventArgs args)
        {
            try
            {
                if (!Page.IsValid) return;

                TemplateDefinition td = TemplateDefinition.Load();
                TemplateDefinition.Template t = td.GetTemplate(CurrentTemplateReference);

                t.reference = txtReference.Text;
                t.type = ddrTemplateType.SelectedValue;

                td.Save();

                CurrentTemplateReference = t.reference;

                if (Save != null)
                {
                    Save(this, t);
                }

                ShowModulesList();
                LoadData(CurrentTemplateReference);
            }
            catch (Exception e)
            {
                lbError.Text = e.Message;
            }
        }

        protected void OnDelete(object sender, EventArgs args)
        {
            TemplateDefinition td = TemplateDefinition.Load();
            TemplateDefinition.Template t = td.GetTemplate(CurrentTemplateReference);
            TemplateDefinition.DeleteTemplate(CurrentTemplateReference);

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
            TemplateDefinition _objPortal = TemplateDefinition.Load();
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
                    TemplateDefinition.Template _objCurrentTemplate = _objPortal.GetTemplate(CurrentTemplateReference);
                    if (_objCurrentTemplate != null)
                    {
                        // Lấy danh sách cột của Tab
                        _arrColumnsList = _objCurrentTemplate.Columns;
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
            LoadData(CurrentTemplateReference);

            // Nạp dữ liệu cột
            if (CurrentColumnReference != "")
            {
                EditColumn(CurrentColumnReference);
            }
        }

        internal void MoveColumnRight(string _strColumnReference)
        {
            // Nạp cấu trúc Portal
            TemplateDefinition _objPortal = TemplateDefinition.Load();
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
                    TemplateDefinition.Template _objCurrentTemplate = _objPortal.GetTemplate(CurrentTemplateReference);
                    if (_objCurrentTemplate != null)
                    {
                        // Lấy danh sách cột của Tab
                        _arrColumnsList = _objCurrentTemplate.Columns;
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

            // Nạp dữ liệu Template
            LoadData(CurrentTemplateReference);

            // Nạp dữ liệu cột
            if (CurrentColumnReference != "")
            {
                EditColumn(CurrentColumnReference);
            }
        }

        #endregion
    }
}