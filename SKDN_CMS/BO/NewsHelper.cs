using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using DFISYS.Core.DAL;
using DFISYS.API;
using Cache = DFISYS.SiteSystem.Cache;
using System.Web;
using DFISYS.BO.Editoral.Newsedit;
using System;
using DFISYS.BO;
using System.Collections;

namespace DFISYS {
    /// <summary>
    /// Summary description for NewsHelper.
    /// </summary>
    public class NewsHelper {
        public NewsHelper() {
            //
            // TODO: Add constructor logic here
            //

        }
        /// <summary>
        /// Hàm trợ giúp lấy mã của Category trong Tab hiện thời khi người sử dụng chọn xem một Category
        /// </summary>
        /// <returns>Mã tham chiếu đến Category hiện thời</returns>
        public static string GetCurrentCategoryID() {
            Cache cache = new Cache(HttpContext.Current.Application);
            // Biến lưu trữ mã của chuyên san hiện thời
            int _intCurrentCategoryID = 0, _intCurrParent = 0;
            string _strCurrentTabRef = HttpContext.Current.Request.QueryString["TabRef"];
            if (HttpContext.Current.Request.QueryString["RealRef"] != null) {
                _strCurrentTabRef = HttpContext.Current.Request.QueryString["RealRef"];
            }
            string mrs = Config.GetPortalUniqueCacheKey() + "CurrentCategoryID_" + (_strCurrentTabRef == null ? "" : _strCurrentTabRef);
            string nrs = Config.GetPortalUniqueCacheKey() + "CurrentParentID_" + (_strCurrentTabRef == null ? "" : _strCurrentTabRef);
            // Tìm kiếm trong Cache
            if (cache[mrs] != null && cache[nrs] != null) {
                _intCurrentCategoryID = (int)cache[mrs];
                _intCurrParent = (int)cache[nrs];
            }
            if (_intCurrentCategoryID > 0) return _intCurrParent + "," + _intCurrentCategoryID;

            // Lấy thông tin về Tab hiện thời
            PortalDefinition.Tab _objCurrentTab = PortalDefinition.getTabByRef(_strCurrentTabRef);//PortalDefinition.GetCurrentTab();

            if (_objCurrentTab != null) {
                // Lấy mã tham chiếu của Tab
                string _strTabRef = _objCurrentTab.reference;
                string _strCategoryRef = "", _strParentRef = "";

                // Tìm vị trí dấu chấm cuối
                int _intLastDotPos = _strTabRef.LastIndexOf('.');

                if (_intLastDotPos > 0) {
                    // Cắt lấy phần tên đại diện trên URL của Category
                    _strCategoryRef = _objCurrentTab.reference.Substring(_intLastDotPos + 1);
                    _strParentRef = _objCurrentTab.reference.Substring(0, _intLastDotPos);
                    // Lấy thông tin về Category đã chọn
                    CategoryRow _objCurrentCategory = null;
                    using (MainDB _objDB = new MainDB()) {
                        _objCurrentCategory = _objDB.CategoryCollection.GetRow("Cat_DisplayURL = '" + _strCategoryRef + "'");
                    }

                    if (_objCurrentCategory != null) {
                        _intCurrentCategoryID = _objCurrentCategory.Cat_ID;
                        _intCurrParent = _objCurrentCategory.Cat_ParentID;
                    }
                    else {
                        _intLastDotPos = _strParentRef.LastIndexOf('.');
                        _strParentRef = _strParentRef.Substring(_intLastDotPos + 1);
                        using (MainDB _objDB = new MainDB()) {
                            _objCurrentCategory = _objDB.CategoryCollection.GetRow("Cat_DisplayURL = '" + _strParentRef + "'");
                        }
                        if (_objCurrentCategory != null) {
                            _intCurrentCategoryID = _objCurrentCategory.Cat_ID;
                            _intCurrParent = _objCurrentCategory.Cat_ParentID;
                        }
                    }
                }
            }

            // Lưu mã của chuyên san hiện thời vào Cache
            cache[mrs] = _intCurrentCategoryID;
            cache[nrs] = _intCurrParent;
            // Trả về 0 nếu không tìm thấy Category
            return _intCurrParent + "," + _intCurrentCategoryID;//0
        }

        /// <summary>
        /// Hàm trợ giúp tìm kiếm mã tham chiếu đến chuyên san hiện thời từ mã tham chiếu của Tab đang đượ hiển thị
        /// </summary>
        /// <returns>Mã tham chiếu đến chuyên san hiện thời</returns>
        public static int GetCurrentEditionTypeID() {
            // Biến lưu trữ mã của chuyên san hiện thời
            int _intCurrentEditionTypeID = 0;

            string _strCurrentTabRef = System.Web.HttpContext.Current.Request.QueryString["TabRef"];
            //string mrs = Config.GetPortalUniqueCacheKey() + "CurrentEditionTypeID_" + (_strCurrentTabRef == null ? "" : _strCurrentTabRef);
            // Tìm kiếm trong Cache
            //if (System.Web.HttpContext.Current.Cache[mrs] != null) _intCurrentEditionTypeID = (int)System.Web.HttpContext.Current.Cache[mrs];
            //if(_intCurrentEditionTypeID > 0) return _intCurrentEditionTypeID;

            // Nạp thông tin Tab hiện thời
            PortalDefinition.Tab _objCurrentTab = PortalDefinition.GetCurrentTab();

            if (_objCurrentTab != null) {
                // Lấy mã tham chiếu của Tab đang dc hiển thị
                string _strTabRef = _objCurrentTab.reference;
                string _strEditionRef = "";

                // Tìm vị trí dấu chấm đầu tiên
                int _intLastDotPos = _strTabRef.IndexOf('.');

                if (_intLastDotPos > 0) {
                    // Tách lấy phần tên đại diện trên URL của chuyên san
                    _strEditionRef = _objCurrentTab.reference.Substring(0, _intLastDotPos);
                }
                else {
                    // Nếu không có dấu chấm thì lấy toàn bộ mã tham chiếu của Tab
                    _strEditionRef = _objCurrentTab.reference;
                }

                // Lấy thông tin về chuyên san
                EditionTypeRow _objCurrentEditionType = null;
                using (MainDB _objDB = new MainDB()) {
                    _objCurrentEditionType = _objDB.EditionTypeCollection.GetRow("EditionDisplayURL = '" + _strEditionRef + "'");
                }

                if (_objCurrentEditionType != null) {
                    // Tìm thấy chuyên san thì trả về mã tham chiếu của chuyên san
                    _intCurrentEditionTypeID = _objCurrentEditionType.EditionType_ID;
                }
                else {
                    // Nếu không tìm thấy chuyên san thì trả về mã tham chiếu của chuyên san đầu tiên trong danh sách chuyên san
                    EditionTypeRow[] _arrCurrentEditionTypes = null;
                    using (MainDB _objDB = new MainDB()) {
                        _arrCurrentEditionTypes = _objDB.EditionTypeCollection.GetTopAsArray(1, "", "EditionType_ID");
                    }
                    if (_arrCurrentEditionTypes != null && _arrCurrentEditionTypes.Length > 0) {
                        _intCurrentEditionTypeID = _arrCurrentEditionTypes[0].EditionType_ID;
                    }
                }
            }

            // Lưu mã của chuyên san hiện thời vào Cache
            //System.Web.HttpContext.Current.Cache.Insert(mrs, _intCurrentEditionTypeID);

            // Trả về 1 nếu không tìm thấy bất kỳ chuyên san nào trong danh sách -- Đã chỉnh sửa để loại bỏ cache vào ngày 05-08
            //edited by trangnva
            return _intCurrentEditionTypeID;//1
        }

        /// <summary>
        /// Hàm trợ giúp lấy danh sách các mã tham chiếu của các Category trong chuyên san hiện thời
        /// </summary>
        /// <param name="_intCurrentEditionID">Mã tham chiếu đến chuyên san đang dc hiển thị</param>
        /// <returns>Mã tham chiếu của các Categories được phân tách bằng dấu ','</returns>
        public static string GetCurrentCategoriesListID(int _intCurrentEditionID) {
            string _strCheckInCache = null;
            string mrs = Config.GetPortalUniqueCacheKey() + "CategoriesIDList_" + (_intCurrentEditionID > 0 ? _intCurrentEditionID.ToString() : "");
            // Tìm kiếm trong Cache
            if (System.Web.HttpContext.Current.Cache[mrs] != null) _strCheckInCache = (string)System.Web.HttpContext.Current.Cache[mrs];
            if (_strCheckInCache != null && _strCheckInCache != "") return _strCheckInCache;


            // Lấy danh sách các Categories của chuyên san hiện thời
            StringBuilder _strCategoriesIDList = new StringBuilder();
            DataTable _dtbCategories = null;
            using (MainDB _objDB = new MainDB()) {
                _dtbCategories = _objDB.CategoryCollection.GetAsDataTable("EditionType_ID=" + _intCurrentEditionID + " AND Cat_isColumn=0", ""); //.GetByEditionType_IDAsDataTable(_intCurrentEditionID);
            }

            // Duyệt danh sách Cateogries
            if (_dtbCategories != null) {
                for (int _intCategoryCount = 0; _intCategoryCount < _dtbCategories.Rows.Count; _intCategoryCount++) {
                    // Ghép chuỗi kq
                    _strCategoriesIDList.Append(_intCategoryCount > 0 ? "," : "");
                    _strCategoriesIDList.Append(_dtbCategories.Rows[_intCategoryCount]["Cat_ID"]);
                }
            }

            // Lưu mã của chuyên san hiện thời vào Cache
            System.Web.HttpContext.Current.Cache.Insert(mrs, _strCategoriesIDList.ToString());

            return _strCategoriesIDList.ToString();
        }

        /// <summary>
        /// Hàm loại bỏ các thẻ HTML trong các đoạn Text nhỏ
        /// </summary>
        /// <param name="_strHTML">Chuỗi HTML đầu vào</param>
        /// <returns>Chuỗi Text đã loại bỏ các thẻ HTML định trước</returns>
        public static string StripHTMLTags(string _strHTML) {
            if (_strHTML == null) return null;
            return Regex.Replace(_strHTML, @"</?(?i:span|b|font|strong|br|p|table|tr|td|script|tbody|div)(.|\n)*?>", string.Empty);
        }
        public static int WordCount(string htmlContent) {
            if (string.IsNullOrEmpty(htmlContent))
                return 0;

            string plainText = Strip(htmlContent);
            return plainText.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
        public static void UpdateCounter() {
            //// get news' that view count has change from log tem table
            //DataTable logInfo_Tem = null;
            //using (LogCMS.DAL.MainDB logDB = new LogCMS.DAL.MainDB())
            //{
            //    logInfo_Tem = logDB.StoredProcedures.LogInfo_Tem_SelectAll();
            //    // refresh log tem table
            //    logDB.StoredProcedures.LogInfo_Tem_DeleteAll();
            //}

            //// update into news table
            //DFISYS.Core.DAL.MainDB coreDB = new MainDB();
            //foreach (DataRow row in logInfo_Tem.Rows)
            //    coreDB.StoredProcedures.News_UpdateViewCount(Int64.Parse(row[1].ToString()), Int64.Parse(row[0].ToString()));
        }

        public static string Strip(string htmlContent) {
            if (string.IsNullOrEmpty(htmlContent))
                return "";
            try {
                // because browsers inserts space
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                        @"(<!--)(.*?)(-->)", string.Empty,
                        RegexOptions.Singleline);
                htmlContent = Regex.Replace(htmlContent, @"<[^>]*>", String.Empty);
                Regex objRegExp = new Regex("<(.|\n)+?>");
                htmlContent = objRegExp.Replace(htmlContent, String.Empty);
                htmlContent = htmlContent.Replace(Environment.NewLine, " ");
                htmlContent = Regex.Replace(htmlContent, "&nbsp;", " ");

            }
            catch { }

            return htmlContent;
        }

        public static string GenNewsID() {
            return string.Format("{0}{1}{2}", DateTime.Now.ToString("yyyMMddhhmm"), DateTime.Now.Second, DateTime.Now.Millisecond);
        }
    }
}
