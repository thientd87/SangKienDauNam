using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using DFISYS;
using DFISYS.BO.Editoral.Category;
using DFISYS.Core.DAL;
using DFISYS.API;
using Cache = DFISYS.SiteSystem.Cache;
using System.Web;
using System.Data;
using System.Web.Caching;
using System.IO;
using DFISYS.User.Security;
using System.Reflection;
using System.Configuration;
using DFISYS.CoreBO.Utils;
using System.Data.SqlClient;

namespace DFISYS.CoreBO {
    /// <summary>
    /// Summary description for NewsHelper.
    /// </summary>
    public class NewsHelper {
        public NewsHelper() {
            
        }

          /// <summary>
        /// Hàm xây dựng chuỗi đường dẫn đến tab sẽ hiện thị nội dung một category
        /// </summary>
        /// <param name="_intCategoryID">Mã tham chiếu đến category cần hiển thị</param>
        /// <returns>Chuỗi đường dẫn đến Category đã chọn</returns>
        public static string BuildCategoryURL(int _intCategoryID) {
            // Tìm kiếm trong Cache
            string _strCheckInCache = null;
            string mrs = Config.GetPortalUniqueCacheKey() + "CategoryURL_" + _intCategoryID.ToString();
            if (System.Web.HttpContext.Current.Cache[mrs] != null) _strCheckInCache = (string)System.Web.HttpContext.Current.Cache[mrs];
            if (_strCheckInCache != null && _strCheckInCache != "") return _strCheckInCache;
            // Không tìm thấy đường dẫn này trong Cache --> xây dựng lại chuỗi đường dẫn 
            // Đối tượng lưu chuỗi đường dẫn kq
            StringBuilder _strCategoryURL = new StringBuilder();
            // Thêm phần đường dẫn
            using (MainDB objDb = new MainDB()) {
                _strCategoryURL.Append(objDb.StoredProcedures.vc_GetCatPath(_intCategoryID).Rows[0][0]);
            }
            //_strCategoryURL.Append(GetCategoryFragmentURL(_intCategoryID));
            // Thêm phần mở rộng của đường dẫn vào chuỗi kq
            _strCategoryURL.Append(Config.CustomExtension);
            // Lưu đường dẫn vừa tạo vào Cache
            System.Web.HttpContext.Current.Cache.Insert(mrs, _strCategoryURL.ToString());
            // Trả về chuỗi kq
            return _strCategoryURL.ToString();
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
        public static string Strip(string htmlContent) {
            if (string.IsNullOrEmpty(htmlContent))
                return "";

            try {
                // Remove HTML Development formatting

                // Replace line breaks with space

                // because browsers inserts space
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                        @"(<!--)(.*?)(-->)", string.Empty,
                        RegexOptions.Singleline);


                htmlContent = Regex.Replace(htmlContent, @"[\n\t\s]+", " ", RegexOptions.Singleline);

                htmlContent = htmlContent.Replace("\r", " ");
                // Replace line breaks with space

                // because browsers inserts space

                htmlContent = htmlContent.Replace("\n", " ");
                // Remove step-formatting

                htmlContent = htmlContent.Replace("\t", string.Empty);
                // Remove repeating speces becuase browsers ignore them

                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                                                                      @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)

                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"<( )*head([^>])*>", "<head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"(<( )*(/)( )*head( )*>)", "</head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         "(<head>).*(</head>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)

                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"<( )*script([^>])*>", "<script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                //result = System.Text.RegularExpressions.Regex.Replace(result, 

                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",

                //         string.Empty, 

                //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"(<script>).*(</script>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);



                // remove all styles (prepare first by clearing attributes)

                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"<( )*style([^>])*>", "<style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"(<( )*(/)( )*style( )*>)", "</style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         "(<style>).*(</style>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags

                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"<( )*td([^>])*>", "\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags

                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"<( )*br( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"<( )*li( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place

                // if <P>, <DIV> and <TR> tags

                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"<( )*div([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"<( )*tr([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"<( )*p([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // Remove remaining tags like <a>, links, images,

                // comments etc - anything thats enclosed inside < >

                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"<[^>]*>", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // replace special characters:

                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @" ", " ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"&bull;", " * ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"&lsaquo;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"&rsaquo;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"&trade;", "(tm)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"&frasl;", "/",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"&lt;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"&gt;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"&copy;", "(c)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"&reg;", "(r)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove all others. More can be added, see

                // http://hotwired.lycos.com/webmonkey/reference/special_characters/

                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         @"&(.{2,6});", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // for testng

                //System.Text.RegularExpressions.Regex.Replace(result, 

                //       this.txtRegex.Text,string.Empty, 

                //       System.Text.RegularExpressions.RegexOptions.IgnoreCase);


                // make line breaking consistent

                htmlContent = htmlContent.Replace("\n", "\r");

                // Remove extra line breaks and tabs:

                // replace over 2 breaks with 2 and over 4 tabs with 4. 

                // Prepare first to remove any whitespaces inbetween

                // the escaped characters and remove redundant tabs inbetween linebreaks

                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         "(\r)( )+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         "(\t)( )+(\t)", "\t\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         "(\t)( )+(\r)", "\t\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         "(\r)( )+(\t)", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove redundant tabs

                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         "(\r)(\t)+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove multible tabs followind a linebreak with just one tab

                htmlContent = System.Text.RegularExpressions.Regex.Replace(htmlContent,
                         "(\r)(\t)+", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Initial replacement target string for linebreaks

                string breaks = "\r\r\r";
                // Initial replacement target string for tabs

                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < htmlContent.Length; index++) {
                    htmlContent = htmlContent.Replace(breaks, "\r\r");
                    htmlContent = htmlContent.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                htmlContent = Regex.Replace(htmlContent, "&nbsp;", " ");
                htmlContent = Regex.Replace(htmlContent, @"\s+", " ");



            }
            catch {
                // Remove HTML Tag
                htmlContent = Regex.Replace(htmlContent, @"<(.|\n)*?>", string.Empty);

                //Remove Script Tag and Form Blocks 
                htmlContent = Regex.Replace(htmlContent, @"@]*?.*?", string.Empty);
                htmlContent = Regex.Replace(htmlContent, "&nbsp;", " ");
                htmlContent = Regex.Replace(htmlContent, "  ", " ");
            }



            return htmlContent;
        }
        public static string GenNewsID() {
            return string.Format("{0}{1}{2}", DateTime.Now.ToString("yyyMMddhhmm"), DateTime.Now.Second, DateTime.Now.Millisecond);
        }

        public static void CopyToNhuanBut(long newsId) {
            using (MainDB db = new MainDB()) {
                db.CallStoredProcedure("s_CopyToChamNhuanBut",
                    new object[] { newsId },
                    new string[] { "@newsId" },
                    false);
            }
        }

        public static void SynchronizeNhuanBut() {
            using (MainDB db = new MainDB())
                db.CallStoredProcedure("s_SynchronizeNhuanBut", new object[] { }, new string[] { }, false);
        }

        public static bool UpdateSuggestionData() {
            int newsStatus = CpModeToNewsStatus();
            string cacheName = HttpContext.Current.User.Identity.Name + "_" + newsStatus + ".js";

            if (HttpContext.Current.Cache[cacheName] == null) {
                GenSuggestionData();
                DFISYS.CoreBO.Common.Utils.SetDataToCache(1, cacheName, "News");
                return true;
            }
            return false;
        }
        public static int CpModeToNewsStatus() {
            string strcpmode = HttpContext.Current.Request.QueryString["cpmode"].ToString();
            int newsStatus = -2;
            switch (strcpmode) {
                case "templist":
                    newsStatus = 0;
                    break;
                case "sendlist":
                    newsStatus = 1;
                    break;
                case "sendapprovallist":
                    newsStatus = 2;
                    break;
                case "dellist":
                    newsStatus = 6;
                    break;
                //Cung la waitlist nhung voi quyen khac nhau thi status nhan dc la khac nhau:
                //Neu la BTV thi trang thai la 1 va mode la editwaitlist.
                case "editwaitlist":
                    newsStatus = 1;
                    break;
                case "editinglist":
                    newsStatus = 1;
                    break;
                case "approvinglist":
                    newsStatus = 2;
                    break;
                case "approvalwaitlist":
                    newsStatus = 2;
                    break;
                case "publishedlist":
                    newsStatus = 3;
                    break;
                //voi danh sach bai tra lai.
                case "backlist":
                    newsStatus = 5;
                    break;
                //voi danh sach bai tra lai.
                case "removedlist":
                    newsStatus = 7;
                    break;
                default:
                    //xem quyen cua thang dang set
                    MainSecurity objSecu = new MainSecurity();
                    Role objrole = objSecu.GetRole(HttpContext.Current.User.Identity.Name);
                    if (objrole.isBienTapVien) {
                        newsStatus = 1;
                    }
                    if (objrole.isPhongVien) {
                        newsStatus = 0;
                    }
                    if (objrole.isThuKyChuyenMuc || objrole.isThuKyToaSoan || objrole.isPhuTrachKenh || objrole.isTongBienTap) {
                        newsStatus = 2;
                    }
                    break;
                case "mypublished":
                    newsStatus = 3;
                    //objListNewsSource.SelectParameters[1].DefaultValue = HttpContext.Current.User.Identity.Name;
                    break;
            }
            return newsStatus;
        }
        private static void GenSuggestionData() {
            int newsStatus = CpModeToNewsStatus();
            string fileName = HttpContext.Current.User.Identity.Name + "_" + newsStatus + ".js";
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("/scripts/suggestionData")))
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/scripts/suggestionData"));

            StringBuilder data = new StringBuilder();
            data.Append("oc=[");

            string catIDs = CategoryHelper.GetCatIDByUser();

            if (!string.IsNullOrEmpty(catIDs)) {
                DataTable tbl = NewsList.GetNewslistOfNewsListControl("[News].Cat_ID In (" + catIDs + ") And [News].News_Status = " + newsStatus, 200, 0, string.Empty, "[News].News_ID Desc");
                if (tbl != null && tbl.Rows.Count > 0) {
                    int i = 0;
                    data.Append("{i:" + i + ",");
                    data.Append("m:\"" + ((string)tbl.Rows[i][0]).Replace("\"", "\\\"") + "\",");
                    data.Append("o:\"" + DFISYS.CoreBO.Utils.Unicode.UnicodeToKoDau((string)tbl.Rows[i][0]).Replace("\"", "\\\"") + "\"}");

                    for (i = 1; i < tbl.Rows.Count; i++) {
                        data.Append(",{i:" + i + ",");
                        data.Append("m:\"" + ((string)tbl.Rows[i][0]).Replace("\"", "\\\"") + "\",");
                        data.Append("o:\"" + DFISYS.CoreBO.Utils.Unicode.UnicodeToKoDau((string)tbl.Rows[i][0]).Replace("\"", "\\\"") + "\"}");
                    }

                }
            }

            data.Append("];");

            TextWriter tw = new StreamWriter(HttpContext.Current.Server.MapPath("/scripts/suggestionData/" + fileName), false, Encoding.Unicode);
            tw.Write(data.ToString());
            tw.Close();

        }
    }
}
