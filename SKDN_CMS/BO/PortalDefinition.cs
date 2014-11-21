using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Xml;
using System.Xml.Serialization;
using DFISYS.API;
using DFISYS.Ultility;
using Cache = DFISYS.SiteSystem.Cache;

namespace DFISYS {

    #region Templates Definition

    /// <summary>
    /// Manages the PortalDefinition.
    /// </summary>
    [XmlRoot("portal"), Serializable]
    public class TemplateDefinition {
        private static XmlSerializer xmlCloneDef = new XmlSerializer(typeof(PortalDefinition.Tab));
        private static XmlSerializer xmlTemplateDef = new XmlSerializer(typeof(TemplateDefinition));

        public TemplateDefinition() {
        }

        #region Global functions

        #region Column Functions

        #region Find Column

        /// <summary>
        /// Hàm tìm kiếm một cột
        /// </summary>
        /// <param name="columns">Danh sách cột xuất phát</param>
        /// <param name="reference">Mã tham chiếu của cột cần tìm</param>
        /// <returns>Cột cần tìm</returns>
        internal PortalDefinition.Column InternalGetColumn(ArrayList columns, string reference) {
            reference = reference.ToLower();
            if (columns == null) return null;

            // Duyệt danh sách cột xuất phát
            foreach (PortalDefinition.Column _objColumn in columns) {
                // So sánh mã tham chiếu với cột đang xét
                if (_objColumn.ColumnReference.ToLower() == reference) {
                    // Nếu đúng thì trả về cột cần tìm và kết thúc hàm
                    return _objColumn;
                }
                // Nếu chưa tìm thấy thì tiếp tục tìm với danh sách cột con của cột đang xét
                PortalDefinition.Column _objSubColumn = InternalGetColumn(_objColumn.Columns, reference);
                if (_objSubColumn != null) return _objSubColumn;
            }

            // Không tìm thấy cột cần tìm, trả về Null
            return null;
        }


        /// <summary>
        /// Thủ tục tìm kiếm một cột với mã tham chiếu biết trước
        /// </summary>
        /// <param name="_arrTemplatesList">Mảng chứa danh sách Template xuất phát</param>
        /// <param name="_strColumnReference">Mã tham chiếu của cột cần tìm</param>
        /// <param name="_objColumn">Tham chiếu đến đối tượng sẽ lưu thông tin cột tìm được</param>
        private void InternalGetColumnFromTemplate(ArrayList _arrTemplatesList, string _strColumnReference,
                                                   ref PortalDefinition.Column _objColumn) {
            // Duyệt danh sách Template xuất phát
            foreach (Template _objTemplate in _arrTemplatesList) {
                // Nếu chưa tìm thấy cột (null)
                // Tìm cột trong danh sách cột của Template đang xét
                if (_objColumn == null) _objColumn = InternalGetColumn(_objTemplate.Columns, _strColumnReference);

                // Nếu tìm thấy cột cần tìm thì thoát khỏi thủ tục
                if (_objColumn != null) {
                    break;
                }
            }
        }

        #endregion

        #region GetColumn

        /// <summary>
        /// Returns a Column by a reference. 
        /// If not reference is provided it returns the default (first) Column of FirstTemplate.
        /// </summary>
        /// <param name="_strColumnReference">Column reference</param>
        /// <returns>null if Column not found or the default Column if no reference is provided</returns>
        public PortalDefinition.Column GetColumn(string _strColumnReference) {
            // Kiểm tra mã tham chiếu có rỗng hay không
            if (_strColumnReference == null || _strColumnReference == "") {
                // Nễu mã tham chiếu rỗng
                // Tìm cột mặc định (cột 0 thuộc Template 0)
                Template _objDefaultTemplate = (templates != null && templates.Count > 0)
                                                   ? (Template)templates[0]
                                                   : null;
                if (_objDefaultTemplate != null) {
                    try {
                        // Mặc định trả về cột đầu tiên của Template đầu tiên
                        PortalDefinition.Column _objDefaultColumn =
                            (PortalDefinition.Column)_objDefaultTemplate.Columns[0];
                        return _objDefaultColumn;
                    } catch {
                        return null;
                    }
                } else {
                    return null;
                }
            }

            // Tìm kiếm trong Cache
            PortalDefinition.Column _objCachedColumn = null;
            string mrs = Config.GetPortalUniqueCacheKey() + "Column_" + _strColumnReference; // Cache Key
            if (HttpContext.Current.Cache[mrs] != null)
                _objCachedColumn = (PortalDefinition.Column)HttpContext.Current.Cache[mrs];

            // Nếu trong Cache có cột này thì trả về cột
            if (_objCachedColumn != null) {
                return _objCachedColumn;
            } else {
                // Nếu không có cột này trong Cache thì tìm kiếm cột đó trong Portal
                // Khai báo biến chứa thông tin cột cần tìm
                PortalDefinition.Column _objResultColumn = null;

                // Gọi thủ tục tìm kiếm cột cần tìm
                InternalGetColumnFromTemplate(templates, _strColumnReference, ref _objResultColumn);

                // Lưu cột tìm được vào Cache
                if (_objResultColumn != null) {
                    HttpContext.Current.Cache.Insert(mrs, _objResultColumn,
                                                     new CacheDependency(Config.GetTemplateDefinitionPhysicalPath()));
                }

                // Trả về cột tìm được
                return _objResultColumn;
            }
        }

        #endregion

        #region CloneColumns

        /// <summary>
        /// Thủ tục tạo mọt bản sao của thẻ mẫu
        /// </summary>
        /// <param name="_arrColumns">Mảng chứa các cột của thẻ cần sao chép</param>
        /// <returns>Đối tượng mới là bản sao của nguyên bản</returns>
        public object CloneColumns(ArrayList _arrColumns) {
            // Tiến hành ghi thẻ mẫu ra tệp
            XmlTextWriter xmlWriter = null;
            try {
                xmlWriter = new XmlTextWriter(Config.GetCloneColumnsDefinitionPhysicalPath(), Encoding.UTF8);
                xmlWriter.Formatting = Formatting.Indented;
                Template _objTempTemplate = new Template();
                _objTempTemplate.Columns = _arrColumns;
                xmlCloneDef.Serialize(xmlWriter, _objTempTemplate);
            } finally {
                if (xmlWriter != null) {
                    xmlWriter.Close();
                }
            }

            // Đọc thẻ mẫu từ tệp
            Template _objClonedTemplate = null;
            XmlTextReader xmlReader = null;
            try {
                xmlReader = new XmlTextReader(Config.GetCloneColumnsDefinitionPhysicalPath());
                _objClonedTemplate = (Template)xmlCloneDef.Deserialize(xmlReader);
            } finally {
                if (xmlReader != null) {
                    xmlReader.Close();
                }
            }

            // Trả về danh sách cột đã được sao chép
            return _objClonedTemplate == null ? null : _objClonedTemplate.Columns;
        }

        #endregion

        #region UpdateColumnsParent

        /// <summary>
        /// Sets the parent Column object
        /// </summary>
        /// <param name="columns">Collection of child Columns</param>
        /// <param name="_objParentColumn">Parrent Column or null if there is no parrent Column (Tab Level)</param>
        /// <param name="_objTemplate"></param>
        internal static void UpdateColumnsParent(ArrayList columns, PortalDefinition.Column _objParentColumn,
                                                 TemplateDefinition.Template _objTemplate) {
            if (columns == null) return;

            foreach (PortalDefinition.Column _objColumn in columns) {
                _objColumn.ColumnParent = _objParentColumn;
                _objColumn.TemplateRef = _objTemplate;
                _objColumn.TabRef = null;
                UpdateColumnsParent(_objColumn.Columns, _objColumn, _objTemplate);
            }
        }

        #endregion

        #endregion

        #region Template Functions

        #region Find Template

        /// <summary>
        /// Hàm tìm kiếm một Template
        /// </summary>
        /// <param name="arrTemplates">Danh sách Template xuất phát</param>
        /// <param name="reference">Mã tham chiếu của Template cần tìm</param>
        /// <returns>Tab cần tìm</returns>
        private Template InternalGetTemplate(ArrayList arrTemplates, string reference) {
            reference = reference.ToLower();
            if (templates == null) return null;

            // Duyệt danh sách Tab xuất phát
            foreach (Template t in arrTemplates) {
                // So sánh mã tham chiếu với Template đang xét
                if (t.reference.ToLower() == reference) {
                    // Nếu đúng thì trả về Template cần tìm và kết thúc hàm
                    return t;
                }
            }

            // Không tìm thấy Template cần tìm, trả về Null
            return null;
        }

        #endregion

        #region GetTemplate

        /// <summary>
        /// Returns a Template by a reference. 
        /// If not reference is provided it returns the default (first) Template.
        /// </summary>
        /// <param name="reference">Tabs reference</param>
        /// <returns>null if Template not found or the default Template if no reference is provided</returns>
        public Template GetTemplate(string reference) {
            if (reference == null || reference == "") {
                return (Template)templates[0];
            }

            // Tìm kiếm trong Cache
            Template _objCachedTemplate = null;
            string mrs = Config.GetPortalUniqueCacheKey() + "Template_" + reference; // Cache Key
            if (HttpContext.Current.Cache[mrs] != null) _objCachedTemplate = (Template)HttpContext.Current.Cache[mrs];

            // Nếu trong Cache có Tab này thì trả về Tab
            if (_objCachedTemplate != null) {
                return _objCachedTemplate;
            } else {
                // Nếu không có Template này trong Cache thì tìm kiếm Template đó trong Portal
                Template _objNeededTemplate = InternalGetTemplate(templates, reference);
                // Lưu Template tìm được vào Cache nếu tìm đc
                if (_objNeededTemplate != null)
                    HttpContext.Current.Cache.Insert(mrs, _objNeededTemplate,
                                                     new CacheDependency(Config.GetTemplateDefinitionPhysicalPath()));
                // Trả về Tab tìm được
                return _objNeededTemplate;
            }
        }

        #endregion

        #region GetCurrentTemplate

        /// <summary>
        /// Returns the current Tab. 
        /// The current HTTPContext is used to determinate the current Template (TemplateRef=[ref])
        /// </summary>
        /// <returns>The current Tab or the default Tab</returns>
        public static Template GetCurrentTemplate() {
            TemplateDefinition td = Load();
            return td.GetTemplate((HttpContext.Current.Request["TemplateRef"]));
        }

        #endregion

        #region DeleteTemplate

        /// <summary>
        /// Thủ tục xóa một Template
        /// </summary>
        /// <param name="reference">Mã tham chiếu của Template cần xóa</param>
        public static void DeleteTemplate(string reference) {
            TemplateDefinition td = Load();
            Template t = td.GetTemplate((reference));


            for (int i = 0; i < td.templates.Count; i++) {
                if (((Template)td.templates[i]).reference == reference) {
                    td.templates.RemoveAt(i);
                    break;
                }
            }

            td.Save();
        }

        #endregion

        #region UpdateTemplateDefinitionProperties

        /// <summary>
        /// Sets the parent Tab object
        /// </summary>
        /// <param name="templates">Collection of child templates</param>
        /// <param name="parent">Parrent Tab or null if there is no parrent Tab (root collection)</param>
        internal static void UpdateTemplateDefinitionProperties(ArrayList templates, PortalDefinition.Tab parent) {
            if (templates == null) return;

            foreach (Template t in templates) {
                UpdateColumnsParent(t.Columns, null, t);
            }
        }

        #endregion

        #endregion

        #region Common Functions

        #region IsValid

        /// <summary>
        /// Throws an ApplicationException if the Data in not valid
        /// </summary>
        internal void IsValid() {
            try {
                Hashtable templateRefList = new Hashtable();
                foreach (Template t in templates) {
                    t.IsValid(templateRefList);
                }
            } catch {
                HttpContext.Current.Cache.Remove("TemplateSettings");
                throw;
            }
        }

        #endregion

        #region Check change between file system and cache

        /// <summary>
        /// Ham thuc hien kiem tra xem tinh trang giua file he thong va cache co su thay
        /// doi gi khong? Neu co su thay doi thi tra ve true, nguoc lai tra ve false
        /// </summary>
        /// <param name="fileName">Ten file - duong dan chi tiet den portal.config</param>
        /// <param name="cache">doi tuong cua DFISYS.SiteSystem.Cache</param>
        /// <returns>Gia tri bool tra loi co thay doi hay k?</returns>
        public static bool FileChanged(string fileName, Cache cache) {
            if (cache["changed_" + fileName] == null) {
                return true;
            }
            DateTime cacheChanged = (DateTime)cache["changed_" + fileName];
            if (cacheChanged != File.GetLastWriteTime(fileName)) {
                return true;
            }
            return false;
        }

        #endregion

        #region LoadTemplateDefinition

        /// <summary>
        /// Loads the TemplateDefinition - Thuc hien kiem tra cache xem co hay chua, neu co roi thi tra ve tu cache
        /// Neu khong thi load tu file ra roi insert vao cache
        /// Extend 
        /// </summary>
        /// <returns>TemplateDefinition</returns>
        public static TemplateDefinition Load() {
            Cache cache = new Cache(HttpContext.Current.Application);

            //thuc hien load TemplateDefinition 
            string settingsPath = HttpContext.Current.Server.MapPath("~/settings/Templates.config");
            TemplateDefinition td;
            if (FileChanged(settingsPath, cache)) {
                XmlTextReader xmlReader = null;
                try {
                    xmlReader = new XmlTextReader(settingsPath);
                    td = (TemplateDefinition)xmlTemplateDef.Deserialize(xmlReader);
                    UpdateTemplateDefinitionProperties(td.templates, null);

                    //cache.Clean();
                    //luu tru thong tin cache noi dung file vao thoi diem cache va noi dung cache
                    cache["changed_" + settingsPath] = File.GetLastWriteTime(settingsPath);
                    cache["TemplateSettings"] = td;
                } finally {
                    if (xmlReader != null) {
                        xmlReader.Close();
                    }
                }
            }
            // Lookup in Cache
            td = (TemplateDefinition)cache["TemplateSettings"];
            if (td != null) return td;

            return td;
        }

        #endregion

        #region SaveTemplateDefinition

        /// <summary>
        /// Thủ tục lưu cấu truc templates
        /// </summary>
        public void Save() {
            IsValid();
            XmlTextWriter xmlWriter = null;
            try {
                xmlWriter = new XmlTextWriter(Config.GetTemplateDefinitionPhysicalPath(), Encoding.UTF8);
                xmlWriter.Formatting = Formatting.Indented;
                xmlTemplateDef.Serialize(xmlWriter, this);

                // Cập nhật lại liên kết giữa các phần tử
                UpdateTemplateDefinitionProperties(templates, null);

                // Add to Cache
                //System.Web.HttpContext.Current.Cache.Insert("TemplateSettings", this, 
                //    new System.Web.Caching.CacheDependency(Config.GetTemplateDefinitionPhysicalPath()));
            } finally {
                if (xmlWriter != null) {
                    xmlWriter.Close();
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region Template Definition

        /// <summary>
        /// Array of root Templates
        /// </summary>
        [XmlArray("templates"), XmlArrayItem("template", typeof(Template))]
        public ArrayList templates =
new ArrayList();

        /// <summary>
        /// Template Definition Object
        /// </summary>
        [Serializable]
        public class Template {
            /// <summary>
            /// Template's column collection
            /// </summary>
            [XmlArray("columns"), XmlArrayItem("column", typeof(PortalDefinition.Column))]
            public ArrayList Columns =
new ArrayList();

            /// <summary>
            /// Template reference.
            /// </summary>
            [XmlAttribute("ref")]
            public string reference = "";

            /// <summary>
            /// Type of this Template. There are three types: CT, CM, DT
            /// </summary>
            [XmlElement("type")]
            public string type = "";

            #region Module Functions

            #region GetModule

            /// <summary>
            /// Returns a Tabs Module by reference - Khong nen luu module vao cache
            /// </summary>
            /// <param name="ModuleRef">Module reference</param>
            /// <returns>Module or null</returns>
            public PortalDefinition.Module GetModule(string ModuleRef) {
                // Tìm kiếm trong Cache
                PortalDefinition.Module _objCachedModule = null;
                string mrs = Config.GetPortalUniqueCacheKey() + "Module_" + ModuleRef; // Cache Key
                if (HttpContext.Current.Cache[mrs] != null)
                    _objCachedModule = (PortalDefinition.Module)HttpContext.Current.Cache[mrs];

                // Nếu trong Cache có Module này thì trả về Module
                if (_objCachedModule != null) {
                    return _objCachedModule;
                } else {
                    // Nếu không có Module này trong Cache thì tìm kiếm Module đó trong Tab con
                    PortalDefinition.Module _objNeededModule = InternalGetModule(ModuleRef, Columns);
                    // Lưu Module tìm được vào Cache
                    HttpContext.Current.Cache.Insert(mrs, _objNeededModule,
                                                     new CacheDependency(Config.GetPortalDefinitionPhysicalPath()));
                    // Trả về Module tìm được
                    return _objNeededModule;
                }
            }

            /// <summary>
            /// Hàm tìm kiếm Module trong Template (được sử dụng nội bộ)
            /// </summary>
            /// <param name="_strModuleRef">Mã tham chiếu của Module</param>
            /// <param name="_arrColumns">Danh sách các cột có thể chứa Module</param>
            /// <returns>Module cần tìm</returns>
            private static PortalDefinition.Module InternalGetModule(string _strModuleRef, ArrayList _arrColumns) {
                // Nếu danh sách cột là rỗng thì trả về null (không tìm thấy Module)
                if (_arrColumns == null) return null;

                // Duyệt danh sách cột xuất phát
                foreach (PortalDefinition.Column _objColumn in _arrColumns) {
                    // Tìm kiếm Module trong danh sách Module của cột đang xét
                    foreach (PortalDefinition.Module _objModule in _objColumn.ModuleList) {
                        if (String.Compare(_objModule.reference, _strModuleRef, true) == 0) {
                            return _objModule;
                        }
                    }

                    // Nếu không tìm thấy Module thì tiếp tục tìm kiếm với danh sách cột con của cột đang xét
                    PortalDefinition.Module _objNestedModule = InternalGetModule(_strModuleRef, _objColumn.Columns);

                    // Nếu tìm thấy thì trả về Module cần tìm
                    if (_objNestedModule != null) return _objNestedModule;
                }

                // Nếu không tìm thấy Module thì trả về null
                return null;
            }

            #endregion

            #region DeleteModule

            /// <summary>
            /// Hàm xóa một Module trong Tab hiện thời
            /// </summary>
            /// <param name="ModuleRef">Mã tham chiếu của Module</param>
            /// <returns>True: Xóa thành công, False: Có lỗi xảy ra</returns>
            public bool DeleteModule(string ModuleRef) {
                try {
                    // Gọi hàm tìm kiếm và xóa module khỏi danh sách cột
                    InternalDeleteModule(ModuleRef, Columns);

                    return true;
                } catch {
                    return false;
                }
            }

            /// <summary>
            /// Hàm xóa một Module đã được gán cho một ví trí trong trong Template (được sử dụng nội bộ)
            /// </summary>
            /// <param name="_strModuleRef">Mã tham chiếu của Module</param>
            /// <param name="_arrColumns">Danh sách các cột có thể chứa Module</param>
            private static void InternalDeleteModule(string _strModuleRef, ArrayList _arrColumns) {
                // Nếu danh sách cột là rỗng thì kết thúc hàm (Module cần xóa không tồn tại)
                if (_arrColumns == null) return;

                // Duyệt danh sách cột xuất phát
                foreach (PortalDefinition.Column _objColumn in _arrColumns) {
                    if (_objColumn.ModuleList == null) return;

                    // Tìm kiếm Module trong danh sách Module của cột đang xét
                    for (int _intModuleCount = 0; _intModuleCount < _objColumn.ModuleList.Count; _intModuleCount++) {
                        if (
                            String.Compare(
                                ((PortalDefinition.Module)_objColumn.ModuleList[_intModuleCount]).reference,
                                _strModuleRef,
                                true) == 0) {
                            // Xóa Module khỏi cột đang xét
                            _objColumn.ModuleList.RemoveAt(_intModuleCount);
                            return;
                        }
                    }

                    // Nếu không tìm thấy Module thì tiếp tục tìm kiếm với danh sách cột con của cột đang xét
                    InternalDeleteModule(_strModuleRef, _objColumn.Columns);
                }
            }

            #endregion

            #region ArrangePositionChanges

            /// <summary>
            /// Thủ tục lưu trữ thứ tự sắp xếp mới của các Module trên Template hiện thời
            /// </summary>
            /// <param name="_strModulesPosition">Chuỗi chứa dữ liệu về vị trí phân bổ Module</param>
            /// <param name="_strColumnsLevel">Chuỗi chứa dữ liệu về mức của các cột</param>
            /// <param name="_objPortal">Đối tượng lưu trữ cấu trúc Portal</param>
            public void ArrangePositionChanges(string _strModulesPosition, string _strColumnsLevel,
                                               PortalDefinition _objPortal) {
                if (_strModulesPosition == null && _strModulesPosition.Trim().Length == 0) return;

                // Mảng lưu thông tin của các cột
                string[] _arrColumnDefinition = _strModulesPosition.Split("@".ToCharArray());

                // Mảng lưu thông tin về mức của các cột
                string[] _arrColumnLevelDefinition = _strColumnsLevel.Split("@".ToCharArray());

                if (_arrColumnDefinition != null && _arrColumnDefinition.Length > 0) {
                    // Do cần vị trí cũ của các Module để tham chiếu tìm thông tin Module
                    // --> Sao lưu danh sách cột hiện thời ra mảng mới
                    ArrayList _arrColumnList = _objPortal.CloneColumns(Columns) as ArrayList;

                    // Xóa danh sách các Module trong các cột
                    ClearAllColumn(_arrColumnList);

                    // Duyệt danh sách cột của Template
                    for (int _intColumnCount = 0; _intColumnCount < _arrColumnDefinition.Length; _intColumnCount++) {
                        // Chuỗi lưu thông tin về cột
                        string _strColumnDefinition = _arrColumnDefinition[_intColumnCount];

                        // Tách mã tham chiếu của cột
                        string _strColumnReference =
                            _strColumnDefinition.Substring(0, _strColumnDefinition.IndexOf('$'));

                        // Loại bỏ mã tham chiếu của cột khỏi chuỗi thông tin cột 
                        // Chỉ còn lại thông tin module trong chuỗi
                        _strColumnDefinition = _strColumnDefinition.Substring(_strColumnDefinition.IndexOf('$') + 1);

                        // Nếu cột có module thì duyệt tiếp
                        if (_strColumnDefinition != null && _strColumnDefinition.Trim().Length > 0) {
                            // Mảng lưu tên tham chiếu của Module
                            string[] _arrModuleRefList = _strColumnDefinition.Split("~".ToCharArray());

                            // Tiếp tục nếu danh sách Module của cột là không rỗng
                            if (_arrModuleRefList != null && _arrModuleRefList.Length > 0) {
                                // Duyệt danh sách Module 
                                for (int _intModuleCount = 0;
                                     _intModuleCount < _arrModuleRefList.Length;
                                     _intModuleCount++) {
                                    // Lấy thông tin Module từ danh sách cũ
                                    PortalDefinition.Module _objModule = GetModule(_arrModuleRefList[_intModuleCount]);

                                    if (_objModule != null) {
                                        // Nếu Module tồn tại thì thêm Module vào vị trí mới
                                        // Tìm kiếm cột tương ứng trong mảng đã sao chép (cột đích)
                                        PortalDefinition.Column _objDestinationColumn =
                                            _objPortal.InternalGetColumn(_arrColumnList, _strColumnReference);

                                        // Duyệt danh sách cột có thay đổi về mức độ
                                        foreach (string _strColumnLevel in _arrColumnLevelDefinition) {
                                            // Nếu cột đích có trùng mã tham chiếu với cột đang xét
                                            if (_strColumnLevel.IndexOf(_objDestinationColumn.ColumnReference) >= 0) {
                                                // Cập nhật lại mức độ của cột đích
                                                _objDestinationColumn.ColumnLevel =
                                                    Convert.ToInt32(
                                                        _strColumnLevel.Substring(_strColumnLevel.IndexOf('#') + 1));
                                                break;
                                            }
                                        }

                                        // Thêm Module vào cột đích
                                        if (_objDestinationColumn != null)
                                            _objDestinationColumn.ModuleList.Add(_objModule);
                                    }
                                }
                            }
                        }
                    }

                    // Cập nhật lại danh sách cột từ mảng mới
                    Columns = _arrColumnList;
                }
            }

            #endregion

            #endregion

            #region Column Functions

            #region DeleteColumn

            /// <summary>
            /// Xóa một cột thuộc 1 Tab
            /// </summary>
            /// <param name="_strColumnReference">Mã tham chiếu đến cột cần xóa</param>
            /// <returns>True: Xóa thành công, False: Xóa không thành công</returns>
            public bool DeleteColumn(string _strColumnReference) {
                try {
                    // Nạp cấu trúc Portal
                    PortalDefinition pd = PortalDefinition.Load();

                    // Tìm thông tin về cột cần xóa
                    PortalDefinition.Column _objColumn = pd.GetColumn(_strColumnReference);

                    if (_objColumn != null) {
                        // Nếu không có cột nào chứa cột cần xóa
                        // tức là cấp trên của cột cần xóa là cấp Tab
                        if (_objColumn.ColumnParent == null) // First Column Level
						{
                            // Duyệt danh sách cột của Tab hiện thời
                            for (int i = 0; i < Columns.Count; i++) {
                                // So sánh mã tham chiếu với cột đang xét
                                if (((PortalDefinition.Column)Columns[i]).ColumnReference == _strColumnReference) {
                                    // Xóa cột
                                    Columns.RemoveAt(i);
                                    break;
                                }
                            }
                        } else {
                            // Nếu có cột chứa cột hiện thời
                            // Lấy thông tin cột Cha
                            PortalDefinition.Column _objParentColumn = _objColumn.ColumnParent;

                            // Duyệt danh sách cột con của cột Cha
                            for (int i = 0; i < _objParentColumn.Columns.Count; i++) {
                                // So sánh mã tham chiếu với cột đang xét
                                if (((PortalDefinition.Column)_objParentColumn.Columns[i]).ColumnReference ==
                                    _strColumnReference) {
                                    // Xóa cột
                                    _objParentColumn.Columns.RemoveAt(i);
                                    break;
                                }
                            }
                        }

                        // Lưu cấu trúc Portal
                        pd.Save();

                        // Xóa cột thành công
                        return true;
                    } else {
                        return false;
                    }
                } catch {
                    return false;
                }
            }

            #endregion

            #region CloneColumns

            /// <summary>
            /// Thủ tục tạo một bản sao danh sách cột của Template hiện thời
            /// </summary>
            /// <returns>ArrayList chứa Danh sách cột được lưu trong mảng mới</returns>
            public ArrayList CloneColumns() {
                // Để tạo được bản sao của danh sách cột
                // Sử dụng Serialize để thực hiện sao chép
                MemoryStream mem = new MemoryStream();
                BinaryFormatter bf = new BinaryFormatter();

                // Ghi dữ liệu ra bộ nhớ đệm
                bf.Serialize(mem, Columns);

                // Đưa trỏ về đầu vùng nhớ
                mem.Seek(0, SeekOrigin.Begin);

                // Ghi dữ liệu từ bộ nhớ đệm ra mảng mới
                ArrayList _arrClonedColumns = bf.Deserialize(mem) as ArrayList;

                // Đóng vùng nhớ đệm
                mem.Close();

                return _arrClonedColumns;
            }

            #endregion

            #region ClearAllColumn

            /// <summary>
            /// Thủ tục xóa danh sách các Module trong các cột của 1 template
            /// </summary>
            /// <param name="_arrColumnsList">Danh sách cột cần xóa</param>
            private void ClearAllColumn(ArrayList _arrColumnsList) {
                // Nếu không có cột nào trong danh sách thì kết thúc thủ tục
                if (_arrColumnsList == null || _arrColumnsList.Count == 0) return;

                // Xóa thông tin về các module ở mảng mới
                // Chỉ lưu trữ thông tin về cột
                foreach (PortalDefinition.Column _objClearColumn in _arrColumnsList) {
                    _objClearColumn.ModuleList.Clear();

                    // Thực hiện tiếp với các cột con của cột hiện thời
                    ClearAllColumn(_objClearColumn.Columns);
                }
            }

            #endregion

            /// <summary>
            /// Hàm tìm kiếm cột với level cho trước
            /// </summary>
            /// <param name="columns">Danh sách cột xuất phát</param>
            /// <param name="level">Level của các cột cần tìm</param>
            /// <returns>Cột cần tìm</returns>
            public ArrayList GetArrayColumnWithLevel(int level) {
                if (Columns == null) return null;

                ArrayList _arrayColumnsList = new ArrayList();
                // Duyệt danh sách cột xuất phát
                foreach (PortalDefinition.Column _objColumn in Columns) {
                    // So sánh mã tham chiếu với cột đang xét
                    if (_objColumn.ColumnLevel == level) {
                        // Nếu đúng thì trả về cột cần tìm và kết thúc hàm
                        _arrayColumnsList.Add(_objColumn);
                    }
                }

                return _arrayColumnsList;
            }

            public int GetMaxLevelOfColumnsInTemplate() {
                int _iMaxColumnLevel = -1;
                if (Columns.Count > 0) {
                    foreach (PortalDefinition.Column column in Columns) {
                        if (column.ColumnLevel > _iMaxColumnLevel) {
                            _iMaxColumnLevel = column.ColumnLevel;
                        }
                    }
                }

                return _iMaxColumnLevel;
            }

            public int GetMinLevelOfColumnsInTemplate() {
                int _iMinColumnLevel = 100;
                if (Columns.Count > 0) {
                    foreach (PortalDefinition.Column column in Columns) {
                        if (column.ColumnLevel < _iMinColumnLevel) {
                            _iMinColumnLevel = column.ColumnLevel;
                        }
                    }
                }

                return _iMinColumnLevel;
            }

            #endregion

            #region IsValid

            internal void IsValid(Hashtable templateRefList) {
                if (templateRefList.ContainsKey(reference.ToLower())) {
                    throw new ApplicationException(Language.GetText("exception_DuplicateTemplateReferenceFound"));
                }
                templateRefList.Add(reference.ToLower(), reference.ToLower());

                Hashtable moduleRefList = new Hashtable();

                // New Code
                // Check modules list of each Column
                foreach (PortalDefinition.Column _objColumn in Columns) {
                    foreach (PortalDefinition.Module _objModule in _objColumn.ModuleList) {
                        _objModule.IsValid(moduleRefList);
                    }
                }
            }

            #endregion

            #region CreateNewTemplate

            /// <summary>
            /// Tạo một Template mới
            /// </summary>
            /// <returns></returns>
            public static Template Create() {
                Template t = new Template();

                // Mã tham chiếu đến Template
                t.reference = Guid.NewGuid().ToString();

                // Type của Template
                t.type = Definition.CT_TYPE_TEMPLATE;

                return t;
            }

            /// <summary>
            /// Tạo một Template mới với mã tham chiếu cho trước
            /// </summary>
            /// <param name="_strTabRef">Mã tham chiếu gán cho Template mới</param>
            /// <returns>Đối tượng lưu thông tin về Template vừa tạo</returns>
            public static Template Create(string _strTemplateRef) {
                Template t = new Template();

                // Mã tham chiếu đến Template
                if (_strTemplateRef != null && _strTemplateRef.Trim().Length > 0) {
                    t.reference = _strTemplateRef;
                } else {
                    t.reference = Guid.NewGuid().ToString();
                }

                // Type của Template
                t.type = Definition.CT_TYPE_TEMPLATE;

                return t;
            }

            #endregion
        }

        #endregion
    }

    #endregion

    #region PortalDefinition

    /// <summary>
    /// Manages the PortalDefinition.
    /// </summary>
    [XmlRoot("portal"), Serializable]
    public class PortalDefinition {
        private static XmlSerializer xmlCloneDef = new XmlSerializer(typeof(Tab));
        private static XmlSerializer xmlPortalDef = new XmlSerializer(typeof(PortalDefinition));
        private static XmlSerializer xmlTemplateDef = new XmlSerializer(typeof(TemplateDefinition.Template));

        /// <summary>
        /// Hàm khởi tạo mặc định, phục vụ cho Serialize
        /// </summary>
        public PortalDefinition() {
        }

        #region Global functions

        #region Column Functions

        #region Find Column

        /// <summary>
        /// Hàm tìm kiếm một cột
        /// </summary>
        /// <param name="columns">Danh sách cột xuất phát</param>
        /// <param name="reference">Mã tham chiếu của cột cần tìm</param>
        /// <returns>Cột cần tìm</returns>
        internal Column InternalGetColumn(ArrayList columns, string reference) {
            reference = reference.ToLower();
            if (columns == null) return null;

            // Duyệt danh sách cột xuất phát
            foreach (Column _objColumn in columns) {
                // So sánh mã tham chiếu với cột đang xét
                if (_objColumn.ColumnReference.ToLower() == reference) {
                    // Nếu đúng thì trả về cột cần tìm và kết thúc hàm
                    return _objColumn;
                }
                // Nếu chưa tìm thấy thì tiếp tục tìm với danh sách cột con của cột đang xét
                Column _objSubColumn = InternalGetColumn(_objColumn.Columns, reference);
                if (_objSubColumn != null) return _objSubColumn;
            }

            // Không tìm thấy cột cần tìm, trả về Null
            return null;
        }


        /// <summary>
        /// Thủ tục tìm kiếm một cột với mã tham chiếu biết trước
        /// </summary>
        /// <param name="_arrTabsList">Mảng chứa danh sách Tab xuất phát</param>
        /// <param name="_strColumnReference">Mã tham chiếu của cột cần tìm</param>
        /// <param name="_objColumn">Tham chiếu đến đối tượng sẽ lưu thông tin cột tìm được</param>
        private void InternalGetColumnFromTab(ArrayList _arrTabsList, string _strColumnReference, ref Column _objColumn) {
            // Duyệt danh sách Tab xuất phát
            foreach (Tab _objTab in _arrTabsList) {
                // Nếu chưa tìm thấy cột (null)
                // Tìm cột trong danh sách cột của Tab đang xét
                if (_objColumn == null) _objColumn = InternalGetColumn(_objTab.Columns, _strColumnReference);

                // Nếu chưa tìm thấy cột (null)
                // Tìm cột trong các Tab con của tab hiện thời
                if (_objColumn == null) InternalGetColumnFromTab(_objTab.tabs, _strColumnReference, ref _objColumn);

                // Nếu tìm thấy cột cần tìm thì thoát khỏi thủ tục
                if (_objColumn != null) {
                    break;
                }
            }
        }

        #endregion

        #region GetColumn

        /// <summary>
        /// Returns a Column by a reference. 
        /// If not reference is provided it returns the default (first) Column of FirstTab.
        /// </summary>
        /// <param name="_strColumnReference">Column reference</param>
        /// <returns>null if Column not found or the default Column if no reference is provided</returns>
        public Column GetColumn(string _strColumnReference) {
            // Kiểm tra mã tham chiếu có rỗng hay không
            if (_strColumnReference == null || _strColumnReference == "") {
                // Nễu mã tham chiếu rỗng
                // Tìm cột mặc định (cột 0 thuộc Tab 0)
                Tab _objDefaultTab = (tabs != null && tabs.Count > 0) ? (Tab)tabs[0] : null;
                if (_objDefaultTab != null) {
                    try {
                        // Mặc định trả về cột đầu tiên của Tab đầu tiên
                        Column _objDefaultColumn = (Column)_objDefaultTab.Columns[0];
                        return _objDefaultColumn;
                    } catch {
                        return null;
                    }
                } else {
                    return null;
                }
            }

            // Tìm kiếm trong Cache
            Column _objCachedColumn = null;
            string mrs = Config.GetPortalUniqueCacheKey() + "Column_" + _strColumnReference; // Cache Key
            if (HttpContext.Current.Cache[mrs] != null) _objCachedColumn = (Column)HttpContext.Current.Cache[mrs];

            // Nếu trong Cache có cột này thì trả về cột
            if (_objCachedColumn != null) {
                return _objCachedColumn;
            } else {
                // Nếu không có cột này trong Cache thì tìm kiếm cột đó trong Portal
                // Khai báo biến chứa thông tin cột cần tìm
                Column _objResultColumn = null;

                // Gọi thủ tục tìm kiếm cột cần tìm
                InternalGetColumnFromTab(tabs, _strColumnReference, ref _objResultColumn);

                // Lưu cột tìm được vào Cache
                if (_objResultColumn != null) {
                    HttpContext.Current.Cache.Insert(mrs, _objResultColumn,
                                                     new CacheDependency(Config.GetPortalDefinitionPhysicalPath()));
                }

                // Trả về cột tìm được
                return _objResultColumn;
            }
        }

        #endregion

        #region CloneColumns

        /// <summary>
        /// Thủ tục tạo mọt bản sao của thẻ mẫu
        /// </summary>
        /// <param name="_arrColumns">Mảng chứa các cột của thẻ cần sao chép</param>
        /// <returns>Đối tượng mới là bản sao của nguyên bản</returns>
        public object CloneColumns(ArrayList _arrColumns) {
            // Tiến hành ghi thẻ mẫu ra tệp
            XmlTextWriter xmlWriter = null;
            try {
                xmlWriter = new XmlTextWriter(Config.GetCloneColumnsDefinitionPhysicalPath(), Encoding.UTF8);
                xmlWriter.Formatting = Formatting.Indented;
                Tab _objTempTab = new Tab();
                _objTempTab.Columns = _arrColumns;
                xmlCloneDef.Serialize(xmlWriter, _objTempTab);
            } finally {
                if (xmlWriter != null) {
                    xmlWriter.Close();
                }
            }

            // Đọc thẻ mẫu từ tệp
            Tab _objClonedTab = null;
            XmlTextReader xmlReader = null;
            try {
                xmlReader = new XmlTextReader(Config.GetCloneColumnsDefinitionPhysicalPath());
                _objClonedTab = (Tab)xmlCloneDef.Deserialize(xmlReader);
            } finally {
                if (xmlReader != null) {
                    xmlReader.Close();
                }
            }

            // Trả về danh sách cột đã được sao chép
            return _objClonedTab == null ? null : _objClonedTab.Columns;
        }

        #endregion

        #region UpdateColumnsParent

        /// <summary>
        /// Sets the parent Column object
        /// </summary>
        /// <param name="_objTab"></param>
        /// <param name="columns">Collection of child Columns</param>
        /// <param name="_objParentColumn">Parrent Column or null if there is no parrent Column (Tab Level)</param>
        internal static void UpdateColumnsParent(ArrayList columns, Column _objParentColumn, Tab _objTab) {
            if (columns == null) return;

            foreach (Column _objColumn in columns) {
                _objColumn.ColumnParent = _objParentColumn;
                _objColumn.TabRef = _objTab;
                _objColumn.TemplateRef = null;
                UpdateColumnsParent(_objColumn.Columns, _objColumn, _objTab);
            }
        }

        #endregion

        #endregion

        #region Tab Functions

        #region Find Tab

        /// <summary>
        /// Hàm tìm kiếm một Tab
        /// </summary>
        /// <param name="tabs">Danh sách Tab xuất phát</param>
        /// <param name="reference">Mã tham chiếu của Tab cần tìm</param>
        /// <returns>Tab cần tìm</returns>
        private Tab InternalGetTab(ArrayList tabs, string reference) {
            reference = reference.ToLower();
            if (tabs == null) return null;

            // Duyệt danh sách Tab xuất phát
            foreach (Tab t in tabs) {
                // So sánh mã tham chiếu với Tab đang xét
                if (t.reference.ToLower() == reference) {
                    // Nếu đúng thì trả về Tab cần tìm và kết thúc hàm
                    return t;
                }
                // Nếu chưa tìm thấy thì tiếp tục tìm với danh sách Tab con của Tab đang xét
                Tab tb = InternalGetTab(t.tabs, reference);
                if (tb != null) return tb;
            }

            // Không tìm thấy Tab cần tìm, trả về Null
            return null;
        }

        #endregion

        #region GetTab

        /// <summary>
        /// Returns a Tab by a reference. 
        /// If not reference is provided it returns the default (first) Tab.
        /// </summary>
        /// <param name="reference">Tabs reference</param>
        /// <returns>null if Tab not found or the default Tab if no reference is provided</returns>
        public Tab GetTab(string reference) {
            if (reference == null || reference == "") {
                return (Tab)tabs[0];
            }

            // Tìm kiếm trong Cache
            Tab _objCachedTab = null;
            string mrs = Config.GetPortalUniqueCacheKey() + "Tab_" + reference; // Cache Key
            if (HttpContext.Current.Cache[mrs] != null) _objCachedTab = (Tab)HttpContext.Current.Cache[mrs];

            // Nếu trong Cache có Tab này thì trả về Tab
            if (_objCachedTab != null) {
                return _objCachedTab;
            } else {
                // Nếu không có Tab này trong Cache thì tìm kiếm Tab đó trong Portal
                Tab _objNeededTab = InternalGetTab(tabs, reference);
                // Lưu Tab tìm được vào Cache nếu tìm đc
                if (_objNeededTab != null)
                    HttpContext.Current.Cache.Insert(mrs, _objNeededTab, new CacheDependency(Config.GetPortalDefinitionPhysicalPath()));
                // Trả về Tab tìm được
                return _objNeededTab;
            }
        }

        #endregion

        #region CloneToTemplateTab

        /// <summary>
        /// Thủ tục tạo mọt bản sao của thẻ mẫu
        /// </summary>
        /// <param name="_arrColumns">Mảng chứa các cột của thẻ cần sao chép</param>
        /// <returns>Đối tượng mới là bản sao của nguyên bản</returns>
        public object CloneToTemplateTab(ArrayList _arrColumns) {
            /*// Để tạo được bản sao của một đối tượng
            // Sử dụng Serialize để thực hiện sao chép
            MemoryStream mem = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
				
            // Ghi dữ liệu ra bộ nhớ đệm
            bf.Serialize(mem, _objTarget);

            // Đưa trỏ về đầu vùng nhớ
            mem.Seek(0, SeekOrigin.Begin);

            // Ghi dữ liệu từ bộ nhớ đệm ra biến kết quả
            object _objResult = bf.Deserialize(mem);

            // Đóng vùng nhớ đệm
            mem.Close();

            // Trả về bản sao của đối tượng cần sao chép
            return _objResult;*/


            // Tiến hành ghi thẻ mẫu ra tệp
            XmlTextWriter xmlWriter = null;
            try {
                xmlWriter = new XmlTextWriter(Config.GetTemplateTabDefinitionPhysicalPath(), Encoding.UTF8);
                xmlWriter.Formatting = Formatting.Indented;
                TemplateDefinition.Template _objTemplate = new TemplateDefinition.Template();
                _objTemplate.Columns = _arrColumns;
                xmlTemplateDef.Serialize(xmlWriter, _objTemplate);
            } finally {
                if (xmlWriter != null) {
                    xmlWriter.Close();
                }
            }

            // Đọc thẻ mẫu từ tệp
            TemplateDefinition.Template _objClonedTemplate = null;
            XmlTextReader xmlReader = null;
            try {
                xmlReader = new XmlTextReader(Config.GetTemplateTabDefinitionPhysicalPath());
                _objClonedTemplate = (TemplateDefinition.Template)xmlTemplateDef.Deserialize(xmlReader);
            } finally {
                if (xmlReader != null) {
                    xmlReader.Close();
                }
            }

            // Trả về danh sách cột đã được sao chép
            return _objClonedTemplate == null ? null : _objClonedTemplate.Columns;
        }

        #endregion

        #region GetCurrentTab

        /// <summary>
        /// Returns the current Tab. 
        /// The current HTTPContext is used to determinate the current Tab (TabRef=[ref])
        /// </summary>
        /// <returns>The current Tab or the default Tab</returns>
        public static Tab GetCurrentTab() {
            PortalDefinition pd = Load();
            return pd.GetTab(HttpContext.Current.Request["TabRef"]);
        }
        public static Tab getTabByRef(string _ref) {
            PortalDefinition pd = Load();
            return pd.GetTab(_ref);
        }
        #endregion

        #region DeleteTab

        /// <summary>
        /// Thủ tục xóa một Tab
        /// </summary>
        /// <param name="reference">Mã tham chiếu của Tab cần xóa</param>
        public static void DeleteTab(string reference) {
            PortalDefinition pd = Load();
            Tab t = pd.GetTab(reference);

            if (t.parent == null) // Root Tab
			{
                for (int i = 0; i < pd.tabs.Count; i++) {
                    if (((Tab)pd.tabs[i]).reference == reference) {
                        pd.tabs.RemoveAt(i);
                        break;
                    }
                }
            } else {
                Tab pt = t.parent;

                for (int i = 0; i < pt.tabs.Count; i++) {
                    if (((Tab)pt.tabs[i]).reference == reference) {
                        pt.tabs.RemoveAt(i);
                        break;
                    }
                }
            }

            pd.Save();
        }

        #endregion

        #region UpdatePortalDefinitionProperties

        /// <summary>
        /// Sets the parent Tab object
        /// </summary>
        /// <param name="tabs">Collection of child tabs</param>
        /// <param name="parent">Parrent Tab or null if there is no parrent Tab (root collection)</param>
        internal static void UpdatePortalDefinitionProperties(ArrayList tabs, Tab parent) {
            if (tabs == null) return;

            foreach (Tab t in tabs) {
                t.parent = parent;
                UpdatePortalDefinitionProperties(t.tabs, t);
                UpdateColumnsParent(t.Columns, null, t);
            }
        }

        #endregion

        #endregion

        #region Common Functions

        #region IsValid

        /// <summary>
        /// Throws an ApplicationException if the Data in not valid
        /// </summary>
        internal void IsValid() {
            try {
                Hashtable tabRefList = new Hashtable();
                foreach (Tab t in tabs) {
                    t.IsValid(tabRefList);
                }
            } catch {
                HttpContext.Current.Cache.Remove("PortalSettings");
                throw;
            }
        }

        #endregion

        #region Check change between file system and cache

        /// <summary>
        /// Ham thuc hien kiem tra xem tinh trang giua file he thong va cache co su thay
        /// doi gi khong? Neu co su thay doi thi tra ve true, nguoc lai tra ve false
        /// </summary>
        /// <param name="fileName">Ten file - duong dan chi tiet den portal.config</param>
        /// <param name="cache">doi tuong cua DFISYS.SiteSystem.Cache</param>
        /// <returns>Gia tri bool tra loi co thay doi hay k?</returns>
        public static bool FileChanged(string fileName, Cache cache) {
            if (cache["changed_" + fileName] == null) {
                return true;
            }
            DateTime cacheChanged = (DateTime)cache["changed_" + fileName];
            if (cacheChanged != File.GetLastWriteTime(fileName)) {
                return true;
            }
            return false;
        }

        #endregion

        #region LoadPortalDefinition

        /// <summary>
        /// Loads the Portal Definition - Thuc hien kiem tra cache xem co hay chua, neu co roi thi tra ve tu cach
        /// Neu khong thi load tu file ra roi insert vao cache
        /// Extend 
        /// </summary>
        /// <returns>Portal Definition</returns>
        public static PortalDefinition Load() {
            Cache cache = new Cache(HttpContext.Current.Application);

            //thuc hien load Portaldefinition ngay 01/08  chuyen luu vao application cache
            string settingsPath = HttpContext.Current.Server.MapPath("~/settings/Portal.config");
            PortalDefinition pd;
            if (FileChanged(settingsPath, cache)) {
                XmlTextReader xmlReader = null;
                try {
                    xmlReader = new XmlTextReader(settingsPath);
                    pd = (PortalDefinition)xmlPortalDef.Deserialize(xmlReader);
                    UpdatePortalDefinitionProperties(pd.tabs, null);

                    //cache.Clean();
                    //luu tru thong tin cache noi dung file vao thoi diem cache va noi dung cache
                    cache["changed_" + settingsPath] = File.GetLastWriteTime(settingsPath);
                    cache["PortalSettings"] = pd;
                } finally {
                    if (xmlReader != null) {
                        xmlReader.Close();
                    }
                }
            }
            // Lookup in Cache
            pd = (PortalDefinition)cache["PortalSettings"]; //System.Web.HttpContext.Current.Cache
            if (pd != null) return pd;

            #region Ma load portaldefinition cu

            // Load Portaldefinition
            //XmlTextReader xmlReader = null;
            //try
            //{
            //    xmlReader = new XmlTextReader(Config.GetPortalDefinitionPhysicalPath());
            //    pd = (PortalDefinition)xmlPortalDef.Deserialize(xmlReader);

            //    UpdatePortalDefinitionProperties(pd.tabs, null);

            //    // Add to Cache - chua su dung expire - luu dang file dependency
            //    System.Web.HttpContext.Current.Cache.Insert("PortalSettings", pd, 
            //        new System.Web.Caching.CacheDependency(Config.GetPortalDefinitionPhysicalPath()));
            //}
            //finally
            //{
            //    if(xmlReader != null)
            //    {
            //        xmlReader.Close();
            //    }
            //}

            #endregion

            return pd;
        }

        #endregion

        #region SavePortalDefinition

        /// <summary>
        /// Thủ tục lưu cấu truc portal
        /// -Nen dua them dang - save change xong thi luu change vao cache
        /// - Tot nhat nen luu dang Tu dien hoac hashtable de co the truy xuat den cac site khac nhau
        /// -Tien toi luu vao DB cho do phai doc file
        /// </summary>
        public void Save() {
            IsValid();
            XmlTextWriter xmlWriter = null;
            try {
                xmlWriter = new XmlTextWriter(Config.GetPortalDefinitionPhysicalPath(), Encoding.UTF8);
                xmlWriter.Formatting = Formatting.Indented;
                xmlPortalDef.Serialize(xmlWriter, this);

                // Cập nhật lại liên kết giữa các phần tử
                UpdatePortalDefinitionProperties(tabs, null);
                // Add to Cache
                //System.Web.HttpContext.Current.Cache.Insert("PortalSettings", this, 
                //	new System.Web.Caching.CacheDependency(Config.GetPortalDefinitionPhysicalPath()));
            } finally {
                if (xmlWriter != null) {
                    xmlWriter.Close();
                }
            }
        }

        // bacth [10:31 AM 5/26/2008]
        public void SaveWithoutValidate() {

            XmlTextWriter xmlWriter = null;
            try {
                xmlWriter = new XmlTextWriter(Config.GetPortalDefinitionPhysicalPath(), Encoding.UTF8);
                xmlWriter.Formatting = Formatting.Indented;
                xmlPortalDef.Serialize(xmlWriter, this);

                // Cập nhật lại liên kết giữa các phần tử
                UpdatePortalDefinitionProperties(tabs, null);
                // Add to Cache
                //System.Web.HttpContext.Current.Cache.Insert("PortalSettings", this, 
                //	new System.Web.Caching.CacheDependency(Config.GetPortalDefinitionPhysicalPath()));
            } finally {
                if (xmlWriter != null) {
                    xmlWriter.Close();
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region Tab Definition

        /// <summary>
        /// Array of root Tabs
        /// </summary>
        [XmlArray("tabs"), XmlArrayItem("tab", typeof(Tab))]
        public ArrayList tabs = new ArrayList();

        /// <summary>
        /// Template for dynamic Tab Generation
        /// </summary>
        [XmlArray("template"), XmlArrayItem("column", typeof(Column))]
        public ArrayList TemplateColumns;

        /// <summary>
        /// Refer to a Tab Reference used as Template
        /// </summary>
        [XmlElement("templateref")]
        public string TemplateReference = "";

        /// <summary>
        /// Tab Definition Object
        /// </summary>
        [Serializable]
        public class Tab {
            /// <summary>
            /// Tab's column collection
            /// </summary>
            [XmlArray("columns"), XmlArrayItem("column", typeof(Column))]
            public ArrayList Columns = new ArrayList();

            /// <summary>
            /// Indicate that Tab is hidden or not
            /// </summary>
            [XmlAttribute("hidden")]
            public bool IsHidden = false;

            /// <summary>
            /// Parent Tab. null if it is a root Tab
            /// </summary>
            [XmlIgnore]
            public Tab parent = null;

            /// <summary>
            /// Tabs reference. Must be unique!
            /// </summary>
            [XmlAttribute("ref")]
            public string reference = "";

            /// <summary>
            /// Collection of view and edit roles.
            /// A View Role is represented by a 'ViewRole' class, a Edit Role by a 'EditRole' class
            /// </summary>
            [XmlArray("roles"),
             XmlArrayItem("view", typeof(ViewRole)),
             XmlArrayItem("edit", typeof(EditRole))]
            public ArrayList roles = new ArrayList();

            /// <summary>
            /// Skin Name Applied for this Tab
            /// </summary>
            [XmlAttribute("skin")]
            public string SkinName = "";

            /// <summary>
            /// Sub Tab collection.
            /// </summary>
            [XmlArray("tabs"), XmlArrayItem("tab", typeof(Tab))]
            public ArrayList tabs = new ArrayList();

            /// <summary>
            /// Tabs title.
            /// </summary>
            [XmlElement("title")]
            public string title = "";

            /// <summary>
            /// Returns the Tabs root Tab or this if it is a root Tab
            /// </summary>
            /// <returns>Root Tab or this</returns>
            public Tab GetRootTab() {
                if (parent == null) return this;
                return parent.GetRootTab();
            }

            #region Module Functions

            #region GetModule

            /// <summary>
            /// Returns a Tabs Module by reference - Khong nen luu module vao cache
            /// </summary>
            /// <param name="ModuleRef">Module reference</param>
            /// <returns>Module or null</returns>
            public Module GetModule(string ModuleRef) {
                // Tìm kiếm trong Cache
                Module _objCachedModule = null;
                string mrs = Config.GetPortalUniqueCacheKey() + "Module_" + ModuleRef; // Cache Key
                if (HttpContext.Current.Cache[mrs] != null) _objCachedModule = (Module)HttpContext.Current.Cache[mrs];

                // Nếu trong Cache có Module này thì trả về Module
                if (_objCachedModule != null) {
                    return _objCachedModule;
                } else {
                    // Nếu không có Module này trong Cache thì tìm kiếm Module đó trong Tab con
                    Module _objNeededModule = InternalGetModule(ModuleRef, Columns);
                    // Lưu Module tìm được vào Cache
                    //HttpContext.Current.Cache.Insert(mrs, _objNeededModule,
                    //                                 new CacheDependency(Config.GetPortalDefinitionPhysicalPath()));
                    // Trả về Module tìm được
                    return _objNeededModule;
                }
            }

            /// <summary>
            /// Hàm tìm kiếm Module trong Portal (được sử dụng nội bộ)
            /// </summary>
            /// <param name="_strModuleRef">Mã tham chiếu của Module</param>
            /// <param name="_arrColumns">Danh sách các cột có thể chứa Module</param>
            /// <returns>Module cần tìm</returns>
            private static Module InternalGetModule(string _strModuleRef, ArrayList _arrColumns) {
                // Nếu danh sách cột là rỗng thì trả về null (không tìm thấy Module)
                if (_arrColumns == null) return null;

                // Duyệt danh sách cột xuất phát
                foreach (Column _objColumn in _arrColumns) {
                    // Tìm kiếm Module trong danh sách Module của cột đang xét
                    foreach (Module _objModule in _objColumn.ModuleList) {
                        if (String.Compare(_objModule.reference, _strModuleRef, true) == 0) {
                            return _objModule;
                        }
                    }

                    // Nếu không tìm thấy Module thì tiếp tục tìm kiếm với danh sách cột con của cột đang xét
                    Module _objNestedModule = InternalGetModule(_strModuleRef, _objColumn.Columns);

                    // Nếu tìm thấy thì trả về Module cần tìm
                    if (_objNestedModule != null) return _objNestedModule;
                }

                // Nếu không tìm thấy Module thì trả về null
                return null;
            }

            #endregion

            #region DeleteModule

            /// <summary>
            /// Hàm xóa một Module trong Tab hiện thời
            /// </summary>
            /// <param name="ModuleRef">Mã tham chiếu của Module</param>
            /// <returns>True: Xóa thành công, False: Có lỗi xảy ra</returns>
            public bool DeleteModule(string ModuleRef) {
                try {
                    // Gọi hàm tìm kiếm và xóa module khỏi danh sách cột
                    InternalDeleteModule(ModuleRef, Columns);

                    return true;
                } catch {
                    return false;
                }
            }

            /// <summary>
            /// Hàm xóa một Module đã được gán cho một ví trí trong trong Portal (được sử dụng nội bộ)
            /// </summary>
            /// <param name="_strModuleRef">Mã tham chiếu của Module</param>
            /// <param name="_arrColumns">Danh sách các cột có thể chứa Module</param>
            private static void InternalDeleteModule(string _strModuleRef, ArrayList _arrColumns) {
                // Nếu danh sách cột là rỗng thì kết thúc hàm (Module cần xóa không tồn tại)
                if (_arrColumns == null) return;

                // Duyệt danh sách cột xuất phát
                foreach (Column _objColumn in _arrColumns) {
                    if (_objColumn.ModuleList == null) return;

                    // Tìm kiếm Module trong danh sách Module của cột đang xét
                    for (int _intModuleCount = 0; _intModuleCount < _objColumn.ModuleList.Count; _intModuleCount++) {
                        if (
                            String.Compare(((Module)_objColumn.ModuleList[_intModuleCount]).reference, _strModuleRef,
                                           true) == 0) {
                            // Xóa Module khỏi cột đang xét
                            _objColumn.ModuleList.RemoveAt(_intModuleCount);
                            return;
                        }
                    }

                    // Nếu không tìm thấy Module thì tiếp tục tìm kiếm với danh sách cột con của cột đang xét
                    InternalDeleteModule(_strModuleRef, _objColumn.Columns);
                }
            }

            #endregion

            #region ArrangePositionChanges

            /// <summary>
            /// Thủ tục lưu trữ thứ tự sắp xếp mới của các Module trên Tab hiện thời
            /// </summary>
            /// <param name="_strModulesPosition">Chuỗi chứa dữ liệu về vị trí phân bổ Module</param>
            /// <param name="_strColumnsLevel">Chuỗi chứa dữ liệu về mức của các cột</param>
            /// <param name="_objPortal">Đối tượng lưu trữ cấu trúc Portal</param>
            public void ArrangePositionChanges(string _strModulesPosition, string _strColumnsLevel,
                                               PortalDefinition _objPortal) {
                if (_strModulesPosition == null && _strModulesPosition.Trim().Length == 0) return;

                // Mảng lưu thông tin của các cột
                string[] _arrColumnDefinition = _strModulesPosition.Split("@".ToCharArray());

                // Mảng lưu thông tin về mức của các cột
                string[] _arrColumnLevelDefinition = _strColumnsLevel.Split("@".ToCharArray());

                if (_arrColumnDefinition != null && _arrColumnDefinition.Length > 0) {
                    // Do cần vị trí cũ của các Module để tham chiếu tìm thông tin Module
                    // --> Sao lưu danh sách cột hiện thời ra mảng mới
                    // Hàm CloneColumns sử dụng BinaryFormatter có tốc độ xử lý chậm
                    //ArrayList _arrColumnList = CloneColumns();
                    // Thay bằng hàm Clone của PortalDefinition
                    ArrayList _arrColumnList = _objPortal.CloneColumns(Columns) as ArrayList;

                    // Xóa danh sách các Module trong các cột
                    ClearAllColumn(_arrColumnList);

                    // Duyệt danh sách cột của Tab
                    for (int _intColumnCount = 0; _intColumnCount < _arrColumnDefinition.Length; _intColumnCount++) {
                        // Chuỗi lưu thông tin về cột
                        string _strColumnDefinition = _arrColumnDefinition[_intColumnCount];

                        // Tách mã tham chiếu của cột
                        string _strColumnReference =
                            _strColumnDefinition.Substring(0, _strColumnDefinition.IndexOf('$'));

                        // Loại bỏ mã tham chiếu của cột khỏi chuỗi thông tin cột 
                        // Chỉ còn lại thông tin module trong chuỗi
                        _strColumnDefinition = _strColumnDefinition.Substring(_strColumnDefinition.IndexOf('$') + 1);

                        // Nếu cột có module thì duyệt tiếp
                        if (_strColumnDefinition != null && _strColumnDefinition.Trim().Length > 0) {
                            // Mảng lưu tên tham chiếu của Module
                            string[] _arrModuleRefList = _strColumnDefinition.Split("~".ToCharArray());

                            // Tiếp tục nếu danh sách Module của cột là không rỗng
                            if (_arrModuleRefList != null && _arrModuleRefList.Length > 0) {
                                // Duyệt danh sách Module 
                                for (int _intModuleCount = 0;
                                     _intModuleCount < _arrModuleRefList.Length;
                                     _intModuleCount++) {
                                    // Lấy thông tin Module từ danh sách cũ
                                    Module _objModule = GetModule(_arrModuleRefList[_intModuleCount]);

                                    if (_objModule != null) {
                                        // Nếu Module tồn tại thì thêm Module vào vị trí mới
                                        // Tìm kiếm cột tương ứng trong mảng đã sao chép (cột đích)
                                        Column _objDestinationColumn =
                                            _objPortal.InternalGetColumn(_arrColumnList, _strColumnReference);

                                        // Duyệt danh sách cột có thay đổi về mức độ
                                        foreach (string _strColumnLevel in _arrColumnLevelDefinition) {
                                            // Nếu cột đích có trùng mã tham chiếu với cột đang xét
                                            if (_strColumnLevel.IndexOf(_objDestinationColumn.ColumnReference) >= 0) {
                                                // Cập nhật lại mức độ của cột đích
                                                _objDestinationColumn.ColumnLevel =
                                                    Convert.ToInt32(
                                                        _strColumnLevel.Substring(_strColumnLevel.IndexOf('#') + 1));
                                                break;
                                            }
                                        }

                                        // Thêm Module vào cột đích
                                        if (_objDestinationColumn != null)
                                            _objDestinationColumn.ModuleList.Add(_objModule);
                                    }
                                }
                            }
                        }
                    }

                    // Cập nhật lại danh sách cột từ mảng mới
                    Columns = _arrColumnList;
                }
            }

            #endregion

            #endregion

            #region Column Functions

            #region DeleteColumn

            /// <summary>
            /// Xóa một cột thuộc 1 Tab
            /// </summary>
            /// <param name="_strColumnReference">Mã tham chiếu đến cột cần xóa</param>
            /// <returns>True: Xóa thành công, False: Xóa không thành công</returns>
            public bool DeleteColumn(string _strColumnReference) {
                try {
                    // Nạp cấu trúc Portal
                    PortalDefinition pd = Load();

                    // Tìm thông tin về cột cần xóa
                    Column _objColumn = pd.GetColumn(_strColumnReference);

                    if (_objColumn != null) {
                        // Nếu không có cột nào chứa cột cần xóa
                        // tức là cấp trên của cột cần xóa là cấp Tab
                        if (_objColumn.ColumnParent == null) // First Column Level
						{
                            // Duyệt danh sách cột của Tab hiện thời
                            for (int i = 0; i < Columns.Count; i++) {
                                // So sánh mã tham chiếu với cột đang xét
                                if (((Column)Columns[i]).ColumnReference == _strColumnReference) {
                                    // Xóa cột
                                    Columns.RemoveAt(i);
                                    break;
                                }
                            }
                        } else {
                            // Nếu có cột chứa cột hiện thời
                            // Lấy thông tin cột Cha
                            Column _objParentColumn = _objColumn.ColumnParent;

                            // Duyệt danh sách cột con của cột Cha
                            for (int i = 0; i < _objParentColumn.Columns.Count; i++) {
                                // So sánh mã tham chiếu với cột đang xét
                                if (((Column)_objParentColumn.Columns[i]).ColumnReference == _strColumnReference) {
                                    // Xóa cột
                                    _objParentColumn.Columns.RemoveAt(i);
                                    break;
                                }
                            }
                        }

                        // Lưu cấu trúc Portal
                        pd.Save();

                        // Xóa cột thành công
                        return true;
                    } else {
                        return false;
                    }
                } catch {
                    return false;
                }
            }

            #endregion

            #region CloneColumns

            /// <summary>
            /// Thủ tục tạo một bản sao danh sách cột của Tab hiện thời
            /// </summary>
            /// <returns>ArrayList chứa Danh sách cột được lưu trong mảng mới</returns>
            public ArrayList CloneColumns() {
                // Để tạo được bản sao của danh sách cột
                // Sử dụng Serialize để thực hiện sao chép
                MemoryStream mem = new MemoryStream();
                BinaryFormatter bf = new BinaryFormatter();

                // Ghi dữ liệu ra bộ nhớ đệm
                bf.Serialize(mem, Columns);

                // Đưa trỏ về đầu vùng nhớ
                mem.Seek(0, SeekOrigin.Begin);

                // Ghi dữ liệu từ bộ nhớ đệm ra mảng mới
                ArrayList _arrClonedColumns = bf.Deserialize(mem) as ArrayList;

                // Đóng vùng nhớ đệm
                mem.Close();

                return _arrClonedColumns;
            }

            #endregion

            #region ClearAllColumn

            /// <summary>
            /// Thủ tục xóa danh sách các Module trong các cột của 1 Tab
            /// </summary>
            /// <param name="_arrColumnsList">Danh sách cột cần xóa</param>
            private void ClearAllColumn(ArrayList _arrColumnsList) {
                // Nếu không có cột nào trong danh sách thì kết thúc thủ tục
                if (_arrColumnsList == null || _arrColumnsList.Count == 0) return;

                // Xóa thông tin về các module ở mảng mới
                // Chỉ lưu trữ thông tin về cột
                foreach (Column _objClearColumn in _arrColumnsList) {
                    _objClearColumn.ModuleList.Clear();

                    // Thực hiện tiếp với các cột con của cột hiện thời
                    ClearAllColumn(_objClearColumn.Columns);
                }
            }

            #endregion

            #endregion

            #region Common Functions

            #region IsValid

            internal void IsValid(Hashtable tabRefList) {
                if (tabRefList.ContainsKey(reference.ToLower())) {
                    throw new ApplicationException(Language.GetText("exception_DuplicateTabReferenceFound"));
                }
                tabRefList.Add(reference.ToLower(), reference.ToLower());

                foreach (Tab t in tabs) {
                    t.IsValid(tabRefList);
                }

                Hashtable moduleRefList = new Hashtable();

                // New Code
                // Check modules list of each Column
                foreach (Column _objColumn in Columns) {
                    foreach (Module _objModule in _objColumn.ModuleList) {
                        _objModule.IsValid(moduleRefList);
                    }
                }
            }

            #endregion

            #region Create New Tab

            /// <summary>
            /// Tạo một Tab mới
            /// </summary>
            /// <returns></returns>
            public static Tab Create() {
                Tab t = new Tab();

                // Mã tham chiếu đến Tab
                t.reference = Guid.NewGuid().ToString();

                // Tiêu đề của Tab
                t.title = Language.GetText("NewTabTitle");

                return t;
            }

            /// <summary>
            /// Tạo một Tab mới với mã tham chiếu cho trước
            /// </summary>
            /// <param name="_strTabRef">Mã tham chiếu gán cho Tab mới</param>
            /// <returns>Đối tượng lưu thông tin về Tab vừa tạo</returns>
            public static Tab Create(string _strTabRef) {
                Tab t = new Tab();

                // Mã tham chiếu đến Tab
                if (_strTabRef != null && _strTabRef.Trim().Length > 0) {
                    t.reference = _strTabRef;
                } else {
                    t.reference = Guid.NewGuid().ToString();
                }

                // Tiêu đề của Tab
                t.title = Language.GetText("NewTabTitle");

                return t;
            }

            #endregion

            #endregion
        }

        #endregion

        #region Column Definition

        /// <summary>
        /// Portal Column Definition Object
        /// </summary>
        [Serializable]
        public class Column {
            [XmlAttribute("stylecontent")]
            public string ColumnCustomStyle = "";

            /// <summary>
            /// Column Level. (Các cột có Level bằng nhau thì nằm trên cùng một hàng)
            /// </summary>
            [XmlAttribute("level")]
            public int ColumnLevel = -1;

            /// <summary>
            /// Column Reference
            /// </summary>
            [XmlAttribute("name")]
            public string ColumnName;

            /// <summary>
            /// Column Parent Object
            /// </summary>
            [XmlIgnore]
            public Column ColumnParent = null;

            /// <summary>
            /// Column Reference
            /// </summary>
            [XmlAttribute("ref")]
            public string ColumnReference = "";

            /// <summary>
            /// Sub Columns Collection
            /// </summary>
            [XmlArray("columns"), XmlArrayItem("column", typeof(Column))]
            public ArrayList Columns = new ArrayList();

            /// <summary>
            /// Column Width
            /// </summary>
            [XmlAttribute("width")]
            public string ColumnWidth = "";

            /// <summary>
            /// is this column dragable?
            /// </summary>
            [XmlAttribute("isDragable")]
            public bool IsDragable = false;

            /// <summary>
            /// allow content manager edit column width?
            /// </summary>
            [XmlAttribute("editablecolumnwidth")]
            public bool EditableColumnWidth = false;

            /// <summary>
            /// An Array store Modules applied for this column
            /// </summary>
            [XmlArray("modules"), XmlArrayItem("module", typeof(Module))]
            public ArrayList ModuleList = new ArrayList();

            /// <summary>
            /// Hold Tab Reference
            /// </summary>
            [XmlIgnore]
            public Tab TabRef = null;

            /// <summary>
            /// Hold Template Reference
            /// </summary>
            [XmlIgnore]
            public TemplateDefinition.Template TemplateRef = null;

            /// <summary>
            /// Khởi tạo một cột mới trong một tab
            /// </summary>
            /// <param name="_objTab">Tab chứa cột mới</param>
            /// <returns>Đối tượng Column tham chiếu đến cột vừa tạo</returns>
            public static Column Create(Tab _objTab) {
                Column _objNewColumn = new Column();
                _objNewColumn.ColumnName = Language.GetText("NewColumnName");
                _objNewColumn.ColumnReference = Guid.NewGuid().ToString();
                _objNewColumn.TabRef = _objTab;
                return _objNewColumn;
            }

            /// <summary>
            /// Khởi tạo một cột mới trong một Template
            /// </summary>
            /// <param name="_objTemplate">Tab chứa cột mới</param>
            /// <returns>Đối tượng Column tham chiếu đến cột vừa tạo</returns>
            public static Column Create(TemplateDefinition.Template _objTemplate) {
                Column _objNewColumn = new Column();
                _objNewColumn.ColumnName = Language.GetText("NewColumnName");
                _objNewColumn.ColumnReference = Guid.NewGuid().ToString();
                _objNewColumn.TemplateRef = _objTemplate;
                return _objNewColumn;
            }

            /// <summary>
            /// Khởi tạo một cột mới trong cột đã chọn
            /// </summary>
            /// <param name="_objParentColumn">Cột đã chọn</param>
            /// <returns>Đối tượng Column tham chiếu đến cột vừa tạo</returns>
            public static Column Create(Column _objParentColumn) {
                Column _objNewColumn = new Column();
                _objNewColumn.ColumnName = Language.GetText("NewColumnName");
                _objNewColumn.ColumnReference = Guid.NewGuid().ToString();
                _objNewColumn.ColumnParent = _objParentColumn;
                return _objNewColumn;
            }

            public int GetMaxLevelOfChildrenColumns() {
                int _iMaxColumnLevel = -1;
                if (Columns.Count > 0) {
                    foreach (PortalDefinition.Column column in Columns) {
                        if (column.ColumnLevel > _iMaxColumnLevel) {
                            _iMaxColumnLevel = column.ColumnLevel;
                        }
                    }
                }

                return _iMaxColumnLevel;
            }

            public int GetMinLevelOfChildrenColumns() {
                int _iMinColumnLevel = 100;
                if (Columns.Count > 0) {
                    foreach (PortalDefinition.Column column in Columns) {
                        if (column.ColumnLevel < _iMinColumnLevel) {
                            _iMinColumnLevel = column.ColumnLevel;
                        }
                    }
                }

                return _iMinColumnLevel;
            }

            /// <summary>
            /// Hàm tìm kiếm cột với level cho trước
            /// </summary>
            /// <param name="columns">Danh sách cột xuất phát</param>
            /// <param name="level">Level của các cột cần tìm</param>
            /// <returns>Cột cần tìm</returns>
            public ArrayList GetArrayChildrenColumnWithLevel(int level) {
                if (Columns == null) return null;

                ArrayList _arrayColumnsList = new ArrayList();
                // Duyệt danh sách cột xuất phát
                foreach (PortalDefinition.Column _objColumn in Columns) {
                    // So sánh mã tham chiếu với cột đang xét
                    if (_objColumn.ColumnLevel == level) {
                        // Nếu đúng thì trả về cột cần tìm và kết thúc hàm
                        _arrayColumnsList.Add(_objColumn);
                    }
                }

                return _arrayColumnsList;
            }
        }

        #endregion

        #region Module Definition

        /// <summary>
        /// Module Definition Object
        /// </summary>
        [Serializable]
        public class Module {
            /// <summary>
            /// Cache Duration (seconds)
            /// </summary>
            [XmlAttribute("cacheduration")]
            public int CacheTime = 0;

            /// <summary>
            /// Module Runtime Settíng Object. Loaded by the internal Method 'LoadRuntimeProperties'
            /// </summary>
            [XmlIgnore]
            public ModuleRuntimeSettings moduleRuntimeSettings = null;

            /// <summary>
            /// Module Settings Object. Loaded by the internal Method 'LoadModuleSettings'
            /// </summary>
            [XmlIgnore]
            public ModuleSettings moduleSettings = null;

            /// <summary>
            /// Modules reference
            /// </summary>
            [XmlAttribute("ref")]
            public string reference = "";

            /// <summary>
            /// Collection of view and edit roles.
            /// A View Role is represented by a 'ViewRole' class, a Edit Role by a 'EditRole' class
            /// </summary>
            [XmlArray("roles"),
             XmlArrayItem("view", typeof(ViewRole)),
             XmlArrayItem("edit", typeof(EditRole))]
            public ArrayList roles = new ArrayList();

            /// <summary>
            /// Modules Title
            /// </summary>
            [XmlElement("title")]
            public string title = "";

            /// <summary>
            /// Modules Ctrl Type
            /// </summary>
            [XmlElement("type")]
            public string type = "";

            /// <summary>
            /// Loads the Modules Settings represented by the 'ModuleSettings.config' File.
            /// Called by the Methods 'Helper.GetEditControl()' and 'PortalTab.RenderModules()'
            /// Nen check exits phan ModuleSetting.config tai day - luu thong tin doc duoc vao cache de lan sau chi doc tu cache
            /// 
            /// </summary>
            internal void LoadModuleSettings() {
                string path = Config.GetModulePhysicalPath(type) + "ModuleSettings.config";
                if (File.Exists(path)) {
                    // Lookup in Cache
                    string msk = "ModuleSettings_" + path;
                    moduleSettings = (ModuleSettings)HttpContext.Current.Cache[msk];
                    if (moduleSettings != null) return;

                    XmlTextReader xmlReader = new XmlTextReader(path);
                    moduleSettings = (ModuleSettings)ModuleSettings.xmlModuleSettings.Deserialize(xmlReader);
                    xmlReader.Close();

                    // Add to Cache
                    HttpContext.Current.Cache.Insert(msk, moduleSettings, new CacheDependency(path));
                } else {
                    moduleSettings = null;
                }
            }

            /// <summary>
            /// Loads the Modules Settings represented by the 'Module_ModuleReference.config' File.
            /// Called by the Methods 'ModuleEdit.LoadData()'
            /// Tuong tu cung luu mot file ModuleRuntimeSettings vao cache - cai nay chua nhieu ModuleRuntimeSetting - Kien truc mau
            /// thi nam o thu muc ConFig.
            /// </summary>
            internal void LoadRuntimeProperties() {
                string path = Config.GetModulePhysicalPath(type) + "Module_" + reference + ".config";
                string mrs = "ModuleRuntimeSettings_" + path;

                if (File.Exists(path)) {
                    // Lookup in Cache
                    moduleRuntimeSettings = (ModuleRuntimeSettings)HttpContext.Current.Cache[mrs];
                    if (moduleRuntimeSettings != null) return;

                    XmlTextReader xmlReader = new XmlTextReader(path);
                    try {
                        moduleRuntimeSettings =
                            (ModuleRuntimeSettings)ModuleRuntimeSettings.xmlModuleSettings.Deserialize(xmlReader);
                    } finally {
                        xmlReader.Close();
                    }

                    // Add to Cache
                    HttpContext.Current.Cache.Insert(mrs, moduleRuntimeSettings,
                                                     new CacheDependency(path));
                } else {
                    // Config file is not existed. Create new config File
                    // Attemp to load Module Settings
                    LoadModuleSettings();
                    moduleRuntimeSettings = new ModuleRuntimeSettings();
                    if (moduleSettings != null) {
                        // Read Declared Properties
                        foreach (
                            ModulePropertyDeclaration _objPropertyDeclaration in
                                moduleSettings.ClientRuntimeDefinedProperties) {
                            moduleRuntimeSettings.ClientRuntimeProperties.Add(
                                new ModuleProperty(_objPropertyDeclaration.Name, ""));
                        }
                        foreach (
                            ModulePropertyDeclaration _objPropertyDeclaration in
                                moduleSettings.AdminRuntimeDefinedProperties) {
                            moduleRuntimeSettings.AdminRuntimeProperties.Add(
                                new ModuleProperty(_objPropertyDeclaration.Name, ""));
                        }

                        // Save Module Runtime Settings
                        XmlTextWriter _objXmlWriter = new XmlTextWriter(path, Encoding.UTF8);
                        _objXmlWriter.Formatting = Formatting.Indented;
                        try {
                            ModuleRuntimeSettings.xmlModuleSettings.Serialize(_objXmlWriter, moduleRuntimeSettings);

                            // Add to Cache
                            HttpContext.Current.Cache.Insert(mrs, moduleRuntimeSettings,
                                                             new CacheDependency(path));
                        } finally {
                            _objXmlWriter.Close();
                        }
                    }
                }
            }

            /// <summary>
            /// Hàm lấy tham chiếu đến đối tượng xử lý việc đọc các biến thực thi
            /// </summary>
            /// <returns></returns>
            public ArrayList GetAvaiblePropertyValues(bool _blnIsClientControl, string _strPropertyName) {
                if (moduleSettings != null) {
                    if (_blnIsClientControl) {
                        // Read Declared Properties
                        foreach (
                            ModulePropertyDeclaration _objPropertyDeclaration in
                                moduleSettings.ClientRuntimeDefinedProperties) {
                            if (_objPropertyDeclaration.Name == _strPropertyName) {
                                return _objPropertyDeclaration.AvaiableValues;
                            }
                        }
                    } else {
                        foreach (
                            ModulePropertyDeclaration _objPropertyDeclaration in
                                moduleSettings.AdminRuntimeDefinedProperties) {
                            if (_objPropertyDeclaration.Name == _strPropertyName) {
                                return _objPropertyDeclaration.AvaiableValues;
                            }
                        }
                    }
                }

                return null;
            }

            /// <summary>
            /// Thủ tục xóa tệp Config động của Module khi xóa Module
            /// </summary>
            internal void DeleteRuntimeProperties() {
                // Đường dẫn đến tệp Config động
                string path = Config.GetModulePhysicalPath(type) + "Module_" + reference + ".config";
                // Tên khóa trong Cache
                string mrs = "ModuleRuntimeSettings_" + path;

                if (File.Exists(path)) {
                    // Nếu tệp Config tồn tại thì xóa tệp đó đi
                    File.Delete(path);

                    // Xóa Cache của tệp Config
                    ModuleRuntimeSettings moduleRuntimeSettings = (ModuleRuntimeSettings)HttpContext.Current.Cache[mrs];
                    if (moduleRuntimeSettings != null) {
                        HttpContext.Current.Cache.Remove(mrs);
                    }
                }
            }

            /// <summary>
            /// Save Runtime Settings Value into Module_ModuleRef.config
            /// Nen thuc hien va dua vao cache
            /// </summary>
            internal void SaveRuntimeSettings() {
                // Full Physical path of config file
                string path = Config.GetModulePhysicalPath(type) + "Module_" + reference + ".config";

                // Save Module Runtime Settings
                XmlTextWriter _objXmlWriter = new XmlTextWriter(path, Encoding.UTF8);
                _objXmlWriter.Formatting = Formatting.Indented;
                try {
                    ModuleRuntimeSettings.xmlModuleSettings.Serialize(_objXmlWriter, moduleRuntimeSettings);
                } finally {
                    _objXmlWriter.Close();
                }
            }

            /// <summary>
            /// Lấy danh sách các tham số của Module, các thông tin về tham số gồm có tên, giải thích và nội dung tham số
            /// </summary>
            /// <param name="_blnIsClientControl"></param>
            /// <returns></returns>
            internal DataTable GetRuntimePropertiesSource(bool _blnIsClientControl) {
                if (moduleSettings != null && moduleRuntimeSettings != null) {
                    // Tạo bảng có 3 cột Name, Caption và Value
                    DataTable _dtbAllDefinedProperties = new DataTable();
                    DataColumn _dtcPropertyName = new DataColumn("Name", typeof(string));
                    DataColumn _dtcPropertyCaption = new DataColumn("Caption", typeof(string));
                    DataColumn _dtcPropertyValue = new DataColumn("Value", typeof(string));
                    _dtbAllDefinedProperties.Columns.Add(_dtcPropertyName);
                    _dtbAllDefinedProperties.Columns.Add(_dtcPropertyCaption);
                    _dtbAllDefinedProperties.Columns.Add(_dtcPropertyValue);

                    // Duyệt danh sách các property đã được định nghĩa
                    foreach (
                        ModulePropertyDeclaration _objDeclaredProperty in
                            moduleSettings.GetRuntimeProperties(_blnIsClientControl)) {
                        // Nạp dữ liệu cho các cột tương ứng của bảng
                        DataRow _dtrPropertyInfo = _dtbAllDefinedProperties.NewRow();
                        _dtrPropertyInfo["Name"] = _objDeclaredProperty.Name;
                        _dtrPropertyInfo["Caption"] = _objDeclaredProperty.Caption;
                        _dtrPropertyInfo["Value"] =
                            moduleRuntimeSettings.GetRuntimePropertyValue(_blnIsClientControl, _objDeclaredProperty.Name);
                        _dtbAllDefinedProperties.Rows.Add(_dtrPropertyInfo);
                    }

                    return _dtbAllDefinedProperties;
                }

                return null;
            }

            internal void IsValid(Hashtable moduleRefList) {
                if (moduleRefList.ContainsKey(reference.ToLower())) {
                    throw new ApplicationException(Language.GetText("exception_DuplicateModuleReferenceFound"));
                }
                moduleRefList.Add(reference.ToLower(), reference.ToLower());
            }

            public static bool IsExist(string moduleRef) {
                string settingsPath = HttpContext.Current.Server.MapPath("~/settings/Portal.config");
                XmlDocument doc = new XmlDocument();
                doc.Load(settingsPath);
                return doc.SelectSingleNode("//module[@ref='" + moduleRef + "']") != null;
            }

            public static Module Create() {
                Module m = new Module();
                m.reference = Guid.NewGuid().ToString();
                m.type = "Contact_Info";
                m.title = Language.GetText("NewModuleTitle");

                return m;
            }
        }

        #endregion

        #region Role definition

        #region Nested type: EditRole

        /// <summary>
        /// Edit Role. Derived from Role.
        /// </summary>
        [Serializable]
        public class EditRole : Role {
        }

        #endregion

        #region Nested type: Role

        /// <summary>
        /// Base class of a Role. Abstract.
        /// </summary>
        [Serializable]
        public abstract class Role {
            /// <summary>
            /// Name of the Role
            /// </summary>
            [XmlText]
            public string name = "";
        }

        #endregion

        #region Nested type: ViewRole

        /// <summary>
        /// View Role. Derived from Role.
        /// </summary>
        [Serializable]
        public class ViewRole : Role {
        }

        #endregion

        #endregion
    }

    #endregion

    #region ModuleSetting Definition

    /// <summary>
    /// Module Settings Object. Loaded from the Modules 'ModulesSettings.xml' File.
    /// Day la lop base cua cau hinh module
    /// </summary>
    [XmlRoot("module"), Serializable]
    public class ModuleSettings {
        internal static XmlSerializer xmlModuleSettings = new XmlSerializer(typeof(ModuleSettings));

        [XmlArray("editCtrlSettings"), XmlArrayItem("property", typeof(ModulePropertyDeclaration))]
        public ArrayList
AdminRuntimeDefinedProperties = new ArrayList();

        [XmlArray("ctrlSettings"), XmlArrayItem("property", typeof(ModulePropertyDeclaration))]
        public ArrayList
ClientRuntimeDefinedProperties = new ArrayList();

        /// <summary>
        /// Modules View .ascx Control.
        /// </summary>
        [XmlElement("ctrl")]
        public string ctrl = "";

        /// <summary>
        /// Modules Edit .ascx Control. 'none' if the Module has no Edit Control.
        /// </summary>
        [XmlElement("editCtrl")]
        public string editCtrl = "";

        /// <summary>
        /// True if the Module has no Edit Control. Property editCtrl mus be set to 'none' (case sensitive!)
        /// </summary>
        [XmlIgnore]
        public bool HasEditCtrl {
            get { return editCtrl != "none"; }
        }


        /// <summary>
        /// Hàm lấy danh sách các tham số cần thiết khi thực thi Module
        /// </summary>
        /// <param name="_blnIsClientControl">True: Module dành cho người dùng, False: Module cho người quản trị</param>
        /// <returns>Danh sách các tham số cần thiết khi thực thi module</returns>
        public ArrayList GetRuntimeProperties(bool _blnIsClientControl) {
            return _blnIsClientControl ? ClientRuntimeDefinedProperties : AdminRuntimeDefinedProperties;
        }


        /// <summary>
        /// Hàm lấy thông tin về một tham số cần thiết khi thực thi Module
        /// </summary>
        /// <param name="_blnIsClientControl">True: Module dành cho người dùng, False: Module cho người quản trị</param>
        /// <param name="_intPropertyIndex">Chỉ số của tham số</param>
        /// <returns>ModulePropertyDeclaration chứa Thông tin về tham số</returns>
        public ModulePropertyDeclaration GetPropertyDeclaration(bool _blnIsClientControl, int _intPropertyIndex) {
            return
                _blnIsClientControl
                    ? (ClientRuntimeDefinedProperties[_intPropertyIndex] as ModulePropertyDeclaration)
                    : (AdminRuntimeDefinedProperties[_intPropertyIndex] as ModulePropertyDeclaration);
        }
    }

    #endregion
}