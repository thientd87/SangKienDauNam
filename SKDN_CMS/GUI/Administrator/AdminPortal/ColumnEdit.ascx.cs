using System;
using System.Collections;
using System.Web.UI;

namespace Portal.GUI.Administrator.AdminPortal
{
    /// <summary>
    ///		Summary description for ColumnEdit.
    /// </summary>
    public partial class ColumnEdit : UserControl
    {
        #region Delegates

        public delegate void DeleteColumnEventHandler(string DeletedColumnReference, string ParentColumnReference);

        #endregion

        private string CurrentColumnReference = "";
        private string CurrentTabReference = "";
        private string CurrentTemplateReference = "";

        public string ColumnReference
        {
            get { return CurrentColumnReference; }
        }

        public string TabReference
        {
            get { return CurrentTabReference; }
        }

        public string TemplateReference
        {
            get { return CurrentTemplateReference; }
        }

        public event EventHandler Save = null;
        public event EventHandler Cancel = null;
        public event DeleteColumnEventHandler Delete = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Put user code to initialize the page here
        }

        /// <summary>
        /// Hàm trả về danh sách tất cả các cấp độ có thể của một cột con
        /// </summary>
        /// <param name="_objEditingColumn">Đối tượng chứa thông tin về cột con cần lấy danh sách cấp độ</param>
        /// <returns>Mảng chứa danh sách cấp độ có thể của cột con</returns>
        private ArrayList LoadAvaiableColumnLevels(PortalDefinition.Column _objEditingColumn)
        {
            if (_objEditingColumn == null) return null;

            ArrayList _objPossibleLevels = new ArrayList();

            // Lưu cấp độ mặc định là -1 (cấp độ thấp nhất)
            _objPossibleLevels.Add(-1);

            if (_objEditingColumn.ColumnParent == null)
            {
                // Nếu không có cột cha thì tính số lượng cấp độ có thể dựa vào số lượng cấp độ của các cột cha
                if (_objEditingColumn.TabRef != null || _objEditingColumn.TemplateRef != null)
                {
                    int _intMaxLevel = -1;
                    
                    if(Parent is Tab)
                    {
                        foreach (PortalDefinition.Column _objParentColumn in _objEditingColumn.TabRef.Columns)
                        {
                            if (_objParentColumn != null && _objParentColumn.ColumnLevel > _intMaxLevel)
                            {
                                _intMaxLevel = _objParentColumn.ColumnLevel;
                            }
                        } 
                    }
                    else if(Parent is Template)
                    {
                        foreach (PortalDefinition.Column _objParentColumn in _objEditingColumn.TemplateRef.Columns)
                        {
                            if (_objParentColumn != null && _objParentColumn.ColumnLevel > _intMaxLevel)
                            {
                                _intMaxLevel = _objParentColumn.ColumnLevel;
                            }
                        }
                    }

                    // Trả về danh sách cấp độ
                    for (int _intLevelCount = 0; _intLevelCount <= _intMaxLevel + 1; _intLevelCount++)
                    {
                        _objPossibleLevels.Add(_intLevelCount);
                    }
                }

                return _objPossibleLevels;
            }
            else
            {
                // Nếu có Module trong cột cha thì lấy danh sách cấp độ theo số lượng Module trong cột cha
                if (_objEditingColumn.ColumnParent.ModuleList != null &&
                    _objEditingColumn.ColumnParent.ModuleList.Count > 0)
                {
                    // Duyệt danh sách Module trong cột cha
                    for (int _intModuleCount = 0;
                         _intModuleCount < _objEditingColumn.ColumnParent.ModuleList.Count;
                         _intModuleCount++)
                    {
                        _objPossibleLevels.Add(_intModuleCount);
                    }

                    // Kiểm tra xem có cột con nào nữa hay không, nếu có cột con thì phải thêm vào số lượng mức của các cột con
                    // Duyệt danh sách cột trong cột cha để lấy ra cấp độ lớn nhất
                    int _intMaxLevel = -1;
                    foreach (PortalDefinition.Column _objCurrentColumn in _objEditingColumn.ColumnParent.Columns)
                    {
                        if (_objCurrentColumn != null && _objCurrentColumn.ColumnLevel > _intMaxLevel)
                        {
                            _intMaxLevel = _objCurrentColumn.ColumnLevel;
                            _objPossibleLevels.Add(_objPossibleLevels.Count - 1);
                        }
                    }

                    return _objPossibleLevels;
                }
                else
                {
                    // Cột cha không có Module nào
                    // Lấy danh sách cấp độ dựa theo số lượng cấp độ của Module

                    // Duyệt danh sách cột trong cột cha để lấy ra cấp độ lớn nhất
                    int _intMaxLevel = -1;
                    foreach (PortalDefinition.Column _objCurrentColumn in _objEditingColumn.ColumnParent.Columns)
                    {
                        if (_objCurrentColumn != null && _objCurrentColumn.ColumnLevel > _intMaxLevel)
                        {
                            _intMaxLevel = _objCurrentColumn.ColumnLevel;
                        }
                    }

                    // Trả về danh sách cấp độ
                    for (int _intLevelCount = 0; _intLevelCount <= _intMaxLevel + 1; _intLevelCount++)
                    {
                        _objPossibleLevels.Add(_intLevelCount);
                    }

                    return _objPossibleLevels;
                }
            }
        }

        /// <summary>
        /// Hàm nạp dữ liệu về một cột
        /// </summary>
        /// <param name="_strColumnRef">Mã tham chiếu của cột cần sửa</param>
        /// <param name="_strTabRef">Mã tham chiếu của Tab chứa cột</param>
        public void LoadData(string _strColumnRef, string _strTabRef)
        {
            PortalDefinition.Column _objColumn = null;

            if (Parent is Tab)
            {
                // Lấy thông tin về cột cần sửa
                PortalDefinition pd = PortalDefinition.Load();
                _objColumn = pd.GetColumn(_strColumnRef);
            }
            else if (Parent is Template)
            {
                TemplateDefinition td = TemplateDefinition.Load();
                _objColumn = td.GetColumn(_strColumnRef);
            }


            if (_objColumn != null)
            {
                // Hiển thị thông tin về cột
                txtTitle.Text = _objColumn.ColumnName;
                txtColWidth.Text = _objColumn.ColumnWidth.ToString();
                ltrColumnReference.Text = _objColumn.ColumnReference;
                chkDefaultColumnWidth.Checked = _objColumn.ColumnWidth == "";
                //txtColWidth.ReadOnly = _objColumn.ColumnWidth == "";
                txtCustomStyle.Text = _objColumn.ColumnCustomStyle;

                // Nạp danh sách các cấp độ có thể có cho cột hiện thời
                drdColumnLevel.DataSource = LoadAvaiableColumnLevels(_objColumn);
                drdColumnLevel.DataBind();
                drdColumnLevel.SelectedIndex = (drdColumnLevel.Items.FindByValue(_objColumn.ColumnLevel.ToString()) !=
                                                null)
                                                   ? drdColumnLevel.Items.IndexOf(
                                                         drdColumnLevel.Items.FindByValue(
                                                             _objColumn.ColumnLevel.ToString()))
                                                   : 0;

                CurrentColumnReference = _strColumnRef;
                CurrentTabReference = _strTabRef;
                CurrentTemplateReference = _strTabRef;

                chkDefaultColumnWidth.Attributes.Add("onclick", txtColWidth.ClientID + ".readOnly = this.checked;");
            }
        }

        protected override void LoadViewState(object bag)
        {
            base.LoadViewState(bag);
            CurrentColumnReference = (string) ViewState["CurrentColumnReference"];
            CurrentTabReference = (string) ViewState["CurrentTabReference"];
        }

        protected override object SaveViewState()
        {
            ViewState["CurrentColumnReference"] = CurrentColumnReference;
            ViewState["CurrentTabReference"] = CurrentTabReference;
            return base.SaveViewState();
        }

        protected void OnCancel(object sender, EventArgs args)
        {
            if (Cancel != null)
            {
                Cancel(this, new EventArgs());
            }
        }

        /// <summary>
        /// Thủ tục lưu các thông tin đã xác lập của 1 Cột
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        protected void OnSave(object sender, EventArgs args)
        {
            try
            {
                if (Page.IsValid)
                {
                    // Save
                    PortalDefinition.Column _objColumn = null;
                    PortalDefinition pd = null;
                    TemplateDefinition td = null;

                    if (Parent is Tab)
                    {
                        // Lấy thông tin về cột cần sửa
                        pd = PortalDefinition.Load();
                        _objColumn = pd.GetColumn(CurrentColumnReference);
                    }
                    else if (Parent is Template)
                    {
                        td = TemplateDefinition.Load();
                        _objColumn = td.GetColumn(CurrentColumnReference);
                    }

                    if (_objColumn != null)
                    {
                        // Cập nhật lại tên cột và độ rộng của cột
                        _objColumn.ColumnName = txtTitle.Text;

                        // Cập nhật màu nền của cột
                        _objColumn.ColumnCustomStyle = txtCustomStyle.Text;

                        // Cập nhật mức độ của cột
                        _objColumn.ColumnLevel = Convert.ToInt32(drdColumnLevel.SelectedValue);
                        //lay ve do rong cua cot
                        string strColWidth = txtColWidth.Text;
                        try
                        {
                            _objColumn.ColumnWidth = chkDefaultColumnWidth.Checked ? "" : strColWidth;
                        }
                        catch
                        {
                            // Nếu có lỗi, lấy độ rộng mặc định
                            _objColumn.ColumnWidth = "";
                        }
                    }

                    if (Parent is Tab)
                    {
                        pd.Save();
                    }
                    else if (Parent is Template)
                    {
                        td.Save();
                    }

                    CurrentColumnReference = _objColumn.ColumnReference;

                    if (Save != null)
                    {
                        Save(this, new EventArgs());
                    }
                }
            }
            catch (Exception e)
            {
                lbError.Text = e.Message;
            }
        }

        protected void OnDelete(object sender, EventArgs args)
        {
            if (Parent is Tab)
            {
                PortalDefinition pd = PortalDefinition.Load();
                PortalDefinition.Tab t = pd.GetTab(CurrentTabReference);
                PortalDefinition.Column _objColumnBeingDeleted = pd.GetColumn(CurrentColumnReference);

                if (_objColumnBeingDeleted != null)
                {
                    t.DeleteColumn(CurrentColumnReference);

                    pd.Save();

                    if (Delete != null)
                    {
                        Delete(CurrentColumnReference,
                               _objColumnBeingDeleted.ColumnParent == null
                                   ? Guid.NewGuid().ToString()
                                   : _objColumnBeingDeleted.ColumnReference);
                    }
                }
            }
            else if (Parent is Template)
            {
                TemplateDefinition td = TemplateDefinition.Load();
                TemplateDefinition.Template t = td.GetTemplate(CurrentTabReference);
                PortalDefinition.Column _objColumnBeingDeleted = td.GetColumn(CurrentColumnReference);

                if (_objColumnBeingDeleted != null)
                {
                    t.DeleteColumn(CurrentColumnReference);

                    td.Save();

                    if (Delete != null)
                    {
                        Delete(CurrentColumnReference,
                               _objColumnBeingDeleted.ColumnParent == null
                                   ? Guid.NewGuid().ToString()
                                   : _objColumnBeingDeleted.ColumnReference);
                    }
                }
            }


            // Hopefully we where redirected here!
        }

        #region Web Form Designer generated code

        protected override void OnInit(EventArgs e)
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