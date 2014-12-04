using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO.Compression;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace BO
{

    public class TableName
    {

        public static string DATABASE_NAME = "KinhPhuot";


        public static string NEWSPUBLISHED = "NewsPublished";
        public static string CATEGORY = "Category";
        public static string COMMENT = "Comment";
        public static string THREAD = "NewsThread";
        public static string META = "Meta";
        public static string BONBAINOIBAT = "BonBaiNoiBat";
        public static string Tags { get { return "Tags"; } }
        public static string NewsThread { get { return "NewsThread"; } }
        public static string ThreadDetail { get { return "ThreadDetail"; } }
        public static string Advertisments { get { return "Advertisments"; } }
        public static string ThongTinDoanhNghiep { get { return "ThongTinDoanhNghiep"; } }
        public static string VOTE = "Vote";
        public static string Seo_Category = "Seo_Category";
        public static string MediaObject = "MediaObject";
    }
    public class Const
    {




        public const string FormatNumber = "{0:#,##0.0}";
        public const string FormatNumber1 = "{0:#,###}";
        public const string FormatNumber2 = "{0:#,###.##}";
        public const string FormatNumber3 = "{0:#,##0.0#}";
        public const string FormatNumber4 = "#,##0";
        public const string FormatNumber5 = "{0:#,##0.#0}";
        public const string FormatNumber6 = "{0:#,##0}";
        public const string FormatNumber7 = "{0:#,##0.000}";

        public const string CurrentcyFormat = "{0:#,###0}";
        public const int NOI_BAT_TRANG_CHU = 2, NOI_BAT_MUC = 1, TIN_THONG_THUONG = 0, TIN_HOT = 3, TIN_TIEU_DIEM_TRANG_CHU = 4, KO_RA_TRANG_CHU = 6;
        public const int TIN_TIEU_DIEM = 5;
        public const int CHANNEL_All = 0;
        public const int CHANNEL_NEWS = 1;
        public const int CHANNEL_PRODUCT = 2;
        public const int LANG_TV = 1;
        public const int LANG_EN = 2;

    }

    public class Utils
    {
        public static void SetPageHeader(Page p, string strtitle, string description, string keywords, bool isIndex)
        {
            string FormatString = "{0} :: {1}";
            string SEO = ConfigurationManager.AppSettings["SEO"] != null ? ConfigurationManager.AppSettings["SEO"].ToString() : "Báo Doanh nhân Việt Nam toàn cầu";
            
            HtmlTitle title = (HtmlTitle)p.Master.FindControl("title");
            HtmlMeta metaDesc = (HtmlMeta)p.Master.FindControl("description");
            HtmlMeta keyword = (HtmlMeta)p.Master.FindControl("keywords");
            HtmlMeta robot = (HtmlMeta)p.Master.FindControl("robot");
            if (!isIndex) robot.Content = "NOINDEX,FOLLOW";
            if (strtitle != "" && title != null) title.Text = String.Format(FormatString, strtitle, SEO);
            if (description != "" && (metaDesc != null)) metaDesc.Content = String.Format(FormatString, description, SEO);
            if (keywords != "" && (keyword != null)) keyword.Content = keywords;
        }

      
        public static string BuildCatURL(string CatID,string CatParentID,string DisplayURL)
        {
            return string.Format("/c{0}p{1}/{2}.htm",CatID,CatParentID,DisplayURL);
        }
        public static string NewsDetailLink(string news_title, string catId, string catParentId, string newsId)
        {
            return String.Format("/{0}p{1}c{2}/{3}.htm", newsId, catParentId, catId, UnicodeToKoDauAndGach(HttpUtility.HtmlDecode(news_title)));
        }
        public const string uniChars = "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";
        public const string KoDauChars = "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";
        public static string UnicodeToKoDauAndGach(string s)
        {
            string retVal = String.Empty;
            int pos;
            for (int i = 0; i < s.Length; i++)
            {
                pos = uniChars.IndexOf(s[i].ToString());
                if (pos >= 0)
                    retVal += KoDauChars[pos];
                else
                    retVal += s[i];
            }
            retVal = retVal.Replace("–", "");
            retVal = retVal.Replace("-", "");
            retVal = retVal.Replace("  ", "");
            retVal = retVal.Replace(" ", "-");
            retVal = retVal.Replace("--", "-");
            retVal = retVal.Replace(":", "");
            retVal = retVal.Replace(";", "");
            retVal = retVal.Replace("+", "");
            retVal = retVal.Replace("@", "");
            retVal = retVal.Replace(">", "");
            retVal = retVal.Replace("<", "");
            retVal = retVal.Replace("*", "");
            retVal = retVal.Replace("{", "");
            retVal = retVal.Replace("}", "");
            retVal = retVal.Replace("|", "");
            retVal = retVal.Replace("^", "");
            retVal = retVal.Replace("~", "");
            retVal = retVal.Replace("]", "");
            retVal = retVal.Replace("[", "");
            retVal = retVal.Replace("`", "");
            retVal = retVal.Replace(".", "");
            retVal = retVal.Replace("'", "");
            retVal = retVal.Replace("(", "");
            retVal = retVal.Replace(")", "");
            retVal = retVal.Replace(",", "");
            retVal = retVal.Replace("”", "");
            retVal = retVal.Replace("“", "");
            retVal = retVal.Replace("?", "");
            retVal = retVal.Replace("\"", "");
            retVal = retVal.Replace("&", "");
            retVal = retVal.Replace("$", "");
            retVal = retVal.Replace("#", "");
            retVal = retVal.Replace("_", "");
            retVal = retVal.Replace("=", "");
            retVal = retVal.Replace("%", "");
            retVal = retVal.Replace("…", "");
            retVal = retVal.Replace("/", "");
            retVal = retVal.Replace("\\", "");
            retVal = retVal.Replace(" ", "-");
            retVal = retVal.Replace("--", "-");
            retVal = retVal.Replace("---", "-");
            retVal = retVal.Replace("----", "-");
            retVal = retVal.Replace("-----", "-");
            return retVal.ToLower().TrimEnd('-').TrimStart('-');
        }
    }
    public class Utility
    {

        public static void CreateDirectory(string path)
        {
            try
            {
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public static bool isMobileBrowser(HttpContext context)
        {
            //GETS THE CURRENT USER CONTEXT
            // HttpContext context = HttpContext.Current;

            //FIRST TRY BUILT IN ASP.NT CHECK
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["isWeb"]))
            {
                //if (context.Request.RawUrl.IndexOf("?ismobile=true")>0)
                //{
                //    context.Session.Add("ViewInWeb", true);
                //    context.Session.Timeout = 30;
                //}



                //if (context.Session["ViewInWeb"] != null && Convert.ToBoolean(context.Session["ViewInWeb"].ToString()))
                //    return false;

                if (context.Request.Browser.IsMobileDevice)
                {
                    return true;
                }
                //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
                if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
                {
                    return true;
                }
                //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
                if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
                    context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
                {
                    return true;
                }
                //AND FINALLY CHECK THE HTTP_USER_AGENT 
                //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
                if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
                {
                    //Create a list of all mobile types
                    string userAgent = context.Request.ServerVariables["HTTP_USER_AGENT"].ToLower();

                    if (userAgent.Contains("ipad"))
                        return false;

                    string[] mobiles =
                       new string[]
                {
                    "midp", "j2me", "avant", "docomo", 
                    "novarra", "palmos", "palmsource", 
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/", 
                    "blackberry", "mib/", "symbian", 
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio", 
                    "SIE-", "SEC-", "samsung", "HTC", 
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx", 
                    "NEC-", "philips", "mmm", "xx", 
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java", 
                    "pt", "pg", "vox", "amoi", 
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo", 
                    "sgh", "gradi", "jb", "dddi", 
                    "moto", "iphone", "android"
                    // "iphone", "opera mini", "sony", "HTC", "eric", "moto", "panasonic", "sharp", "philips", "samsung", "erics", "ericsson", "SonyEricsson" , "lg"
                };


                    //Loop through each item in the list created above 
                    //and check if the header contains that text
                    foreach (string s in mobiles)
                    {
                        if (userAgent.Contains(s.ToLower()))
                        {
                            return true;
                        }
                    }
                }

            }


            return false;
        }

        public static void createSiteMap(DataTable tblResult)
        {
            string SiteMapFile = @"~/sitemap.xml";
            string xmlFile = HttpContext.Current.Request.MapPath(SiteMapFile);

            XmlTextWriter writer = new XmlTextWriter(xmlFile, System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("urlset");
            writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            writer.WriteAttributeString("xsi:schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd");
            writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");

            // Add Home Page
            writer.WriteStartElement("url");
            writer.WriteElementString("loc", "http://gafin.vn");
            writer.WriteElementString("changefreq", "daily");
            writer.WriteEndElement(); // url

            // Add Sections and Articles

            foreach (DataRow row in tblResult.Rows)
            {
                string loc = "http://gafin.vn" + row["URL"].ToString();
                string changefreq = row["changefreq"].ToString();
                writer.WriteStartElement("url");
                writer.WriteElementString("loc", loc);
                writer.WriteElementString("changefreq", changefreq);
                writer.WriteEndElement(); // url
            }

            writer.WriteEndElement();// urlset        
            writer.Close();

        }

        public static string GetDiv(object value)
        {
            //string strResult = string.Empty;
            string strResult = "N/A";
            if (value != null && value != DBNull.Value && ConvertToDouble(value) != 0)
                strResult = string.Format(Const.FormatNumber, ConvertToDouble(value) / 1000000000);

            return strResult;
        }
        public static string GetDiv2(object value)
        {
            //string strResult = string.Empty;
            string strResult = "N/A";
            if (value != null && value != DBNull.Value)
                strResult = string.Format(Const.FormatNumber, ConvertToDouble(value) * 100);

            return strResult;
        }
        public static string GetDiv3(object value)
        {
            //string strResult = string.Empty;
            string strResult = "N/A";
            if (value != null && value != DBNull.Value)
                strResult = string.Format(Const.FormatNumber7, ConvertToDouble(value));

            return strResult;
        }
        public static void PageComppressed(HttpContext c)
        {
            if (!c.Request.UserAgent.ToLower().Contains("konqueror"))
            {
                if (c.Request.Headers["Accept-encoding"] != null && c.Request.Headers["Accept-encoding"].Contains("gzip"))
                {
                    c.Response.Filter = new GZipStream(c.Response.Filter, CompressionMode.Compress, true);

                    c.Response.AppendHeader("Content-encoding", "gzip");
                }

                else if (c.Request.Headers["Accept-encoding"] != null &&
                         c.Request.Headers["Accept-encoding"].Contains("deflate"))
                {
                    c.Response.Filter = new DeflateStream(c.Response.Filter, CompressionMode.Compress, true);

                    c.Response.AppendHeader("Content-encoding", "deflate");
                }
            }
        }
        public static void GetMinMax(object value1, object value2, object value3, object value4, ref double minValue, ref double maxValue)
        {
            double col1 = ConvertToDouble(value1);
            double col2 = ConvertToDouble(value2);
            double col3 = ConvertToDouble(value3);
            double col4 = ConvertToDouble(value4);

            maxValue = col1;
            if (col2 > maxValue) maxValue = col2;
            if (col3 > maxValue) maxValue = col3;
            if (col4 > maxValue) maxValue = col4;

            minValue = col1;
            if (col2 < minValue) minValue = col2;
            if (col3 < minValue) minValue = col3;
            if (col4 < minValue) minValue = col4;
        }





        public static string ConvertNumber1(object value)
        {
            string strResult = "&nbsp;";
            try
            {
                strResult = Convert.ToInt64(value).ToString("0,0", CultureInfo.CreateSpecificCulture("el-GR"));
                //  strResult = (strResult.Trim() == "" || strResult.Trim() == "0") ? "&nbsp;" : strResult;
            }
            catch { }
            return strResult;
        }

        public static string ConvertNumber2(object value)
        {
            string strResult = "&nbsp;";
            try
            {
                strResult = String.Format("{0:#,##0.00}", value);
                strResult = (strResult.Trim() == "" || strResult.Trim() == "0") ? "&nbsp;" : strResult;

            }
            catch { }
            return strResult;
        }
        public static string ConvertFormatNumber(object value, string isNull, long div)
        {
            string strResult = "&nbsp;";
            if (value != null && value != DBNull.Value && value.ToString() != "")
            {
                double dcValue = ConvertToDouble(value);
                strResult = dcValue == 0 ? "" : string.Format("{0:#,##0.00}", dcValue != 0 ? dcValue / div : dcValue);
            }
            else
            {
                strResult = isNull;
            }
            return strResult;
        }
        public static string ConvertFormatNumberPEPB(object value, string isNull, long div)
        {
            string strResult = "&nbsp;";
            if (value != null && value != DBNull.Value && value.ToString() != "")
            {
                double dcValue = ConvertToDouble(value);
                strResult = dcValue <= 0 ? isNull : string.Format("{0:#,##0.00}", dcValue != 0 ? dcValue / div : dcValue);
            }
            else
            {
                strResult = isNull;
            }
            return strResult;
        }
        public static DateTime c_ConvertToDateTime(string d)
        {
            DateTime __resDate = DateTime.MaxValue;
            try
            {
                string[] __str = d.Split(new char[] { '/' });
                int __dd = 0;
                int __mm = 0;
                int __yy = 0;
                if (__str.Length >= 1) __dd = int.Parse(__str[0]);
                if (__str.Length >= 2) __mm = int.Parse(__str[1]);
                if (__str.Length >= 3) __yy = int.Parse(__str[2]);
                __resDate = new DateTime(__yy, __mm, __dd);
            }
            catch (Exception ex)
            {
            }

            return __resDate;
        }
        public static string GetDateVN1(DateTime _Date)
        {
            if (_Date == null) return "";
            string[] ArrayDay = new string[] { "Chủ nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
            int currDay = (int)_Date.DayOfWeek;

            string CurrDate = String.Format("{0:dd/MM/yyyy}", _Date);
            int currMonth = _Date.Month;
            return ArrayDay[currDay] + ", " + CurrDate;
        }
        public static string ConvertQuy(object value)
        {
            string strResult = string.Empty;
            int intQuy = 0;
            int.TryParse(value.ToString(), out intQuy);
            switch (intQuy)
            {
                case 1:
                    strResult = " Quý I";
                    break;
                case 2:
                    strResult = " Quý II";
                    break;
                case 3:
                    strResult = " Quý III";
                    break;
                case 4:
                    strResult = " Quý IV";
                    break;
            }
            return strResult;
        }

        public static int GetQuy()
        {
            return 3;
        }

        public static double GetPointY(DataTable tblChart, int i)
        {
            double value = (tblChart != null && tblChart.Rows.Count > i) ? Utility.convert2DoubleFC(tblChart.Rows[i]["FValue"]) : 0;
            return value;
        }
        public static void AddTable(ref DataTable tbl, DataTable tabAdd)
        {
            if (tbl != null && tabAdd != null)
            {
                int count = tbl.Rows.Count;
                int countAdd = tabAdd.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    if (i < countAdd)
                        tbl.Rows[i]["Fvalue"] = ConvertToLong(tbl.Rows[i]["Fvalue"]) + ConvertToLong(tabAdd.Rows[i]["Fvalue"]);
                }
            }
        }

        public static double GetValueMaxFC(DataTable tblChart)
        {
            double maxValue;
            DataView dvChart = tblChart.DefaultView;
            dvChart.Sort = "Fvalue DESC";
            maxValue = Utility.convert2DoubleFC(dvChart[0]["Fvalue"]);
            maxValue = maxValue + maxValue * 0.1;
            return maxValue;
        }
        public static int ConvertToInt(string Value)
        {
            try
            {
                if (Value == string.Empty) return 0;
                return Convert.ToInt32(Value);
            }
            catch
            {
                return 0;
            }
        }
        public static string FormatFC(object value)
        {
            double dbResult = 0;
            if (value == null || value.ToString().Trim() == "" || value.ToString().Trim() == "0")
                return "";
            double.TryParse(value.ToString(), out dbResult);
            dbResult = dbResult / 1000000000;
            return string.Format("{0:#,###}", dbResult);
        }
        public static double convert2DoubleFC(object value)
        {
            double dbResult = 0;
            if (value == null || value.ToString().Trim() == "" || value.ToString().Trim() == "0")
                return 0;
            double.TryParse(value.ToString(), out dbResult);
            dbResult = dbResult / 1000000000;
            dbResult = Math.Round(dbResult, 0);
            return dbResult;
        }
        public static DateTime GetDateValue(object d)
        {
            try
            {
                return Convert.ToDateTime(d.ToString());
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        public static DateTime ObjectToDataTime(object value)
        {
            DateTime dt = DateTime.MinValue;
            try
            {
                dt = Convert.ToDateTime(value);
            }
            catch { dt = DateTime.MinValue; }
            return dt;
        }
        public static DateTime ConvertToDateTime(string d)
        {
            DateTime __resDate = DateTime.MaxValue;
            try
            {
                string[] __str = d.Split(new char[] { '/' });
                int __dd = 0;
                int __mm = 0;
                int __yy = 0;
                if (__str.Length >= 1) __dd = int.Parse(__str[0]);
                if (__str.Length >= 2) __mm = int.Parse(__str[1]);
                if (__str.Length >= 3) __yy = int.Parse(__str[2]);
                __resDate = new DateTime(__yy, __mm, __dd);
            }
            catch (Exception ex)
            {
            }

            return __resDate;
        }


        public static string ConvertToTags(object Value)
        {
            string strResult = string.Empty;
            if (Value != null)
            {
                List<string> tags = new List<string>();

                string _tmp = Value.ToString();
                if (_tmp.IndexOf(",") != -1)
                {
                    string[] arr = _tmp.Split(',');

                    if (arr != null && arr.Length > 0)
                    {
                        string _link = "<a href=\"/tag/{0}.htm\" title=\"{0}\"><b>{0}</b></a>";
                        string _tmp1 = string.Empty;

                        for (int i = 0; i < arr.Length; i++)
                        {
                            if (arr[i] != null && arr[i].ToString() != "")
                            {
                                _tmp1 = string.Format(_link, arr[i].Trim().Replace(" ", "-").Replace("@", "-").Replace("/", "-").Replace("\\", "-").Replace("!", ""));

                                if (!tags.Contains(_tmp1))
                                    tags.Add(_tmp1);
                            }
                        }
                    }
                }
                else
                    tags.Add(_tmp);
                foreach (string tag in tags)
                {
                    strResult += tag;
                }
                strResult = "<div class=\"tags\">" + strResult + "</div>";
            }
            return strResult;
        }
        public static string ConvertToString(object Value)
        {
            try
            {
                if (Value == null) return string.Empty;
                return Value.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string UppercaseFirst(object value)
        {
            string s = ConvertToString(value).ToLower(); ;
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static int ConvertToInt(object Value)
        {
            int _result = 0;

            if (Value == null)
                return _result;

            Int32.TryParse(Value.ToString(), out _result);

            return _result;
        }

        public static long ConvertToLong(object Value)
        {
            long _result = 0;

            if (Value == null)
                return _result;

            Int64.TryParse(Value.ToString(), out _result);

            return _result;

        }

        public static double ConvertToDouble(object Value)
        {
            try
            {

                return double.Parse(Value.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public static string RenderControlToString(Control ctr)
        {
            StringBuilder sb = new StringBuilder();
            System.IO.StringWriter sw = new System.IO.StringWriter(sb);
            HtmlTextWriter htmlWriter = new HtmlTextWriter(sw);
            ctr.RenderControl(htmlWriter);
            return sb.ToString();
        }

        public static string RenderControlToStringEscape(Control ctr)
        {
            StringBuilder sb = new StringBuilder();
            System.IO.StringWriter sw = new System.IO.StringWriter(sb);
            HtmlTextWriter htmlWriter = new HtmlTextWriter(sw);
            ctr.RenderControl(htmlWriter);
            return sb.ToString().Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("\r\n", "").Replace("  ", "");
        }
        public static void setPermanentlyUrl(HttpContext context)
        {
            rewriteUrl(context, "http://tripcare4u.com/home.htm");
        }
        public static void rewriteUrl(HttpContext context, string url)
        {
            context.Response.Clear();
            context.Response.Status = "301 Moved Permanently";
            context.Response.AddHeader("Location", url);
        }

        public static void Redirect301(HttpContext context, string url)
        {
            context.Response.Clear();
            context.Response.Status = "301 Moved Permanently";
            context.Response.AddHeader("Location", url);
        }
        public static void SetPageHeader(Page p, string Title, string Description, string Keywords, bool isIndex)
        {
            string _tmp = "{0} - {1}";
            string _pageTitle = ConfigurationManager.AppSettings["PageTitle"];

            HtmlTitle title = (HtmlTitle)p.Master.FindControl("title");
            HtmlMeta metaDesc = (HtmlMeta)p.Master.FindControl("description");
            HtmlMeta keyword = (HtmlMeta)p.Master.FindControl("keywords");
            HtmlMeta robot = (HtmlMeta)p.Master.FindControl("robot");

            if (robot != null && isIndex)
                robot.Content = "NOINDEX, FOLLOW";
            if (String.IsNullOrEmpty(Keywords))
                Keywords = ConfigurationManager.AppSettings["KeywordHome"];

            if (Title != "" && title != null) title.Text = String.Format(_tmp, Title, _pageTitle);
            if (Description != "" && (metaDesc != null)) metaDesc.Content = String.Format(_tmp, Description, _pageTitle);
            if (Keywords != "" && (keyword != null)) keyword.Content = String.Format(_tmp, Keywords, string.Empty);
        }

        public static void SetPageHeader(Page p, string Title, string Description, string Keywords)
        {
            string _tmp = "{0} - {1}";
            string _pageTitle = ConfigurationManager.AppSettings["PageTitle"];

            HtmlTitle title = (HtmlTitle)p.Master.FindControl("title");
            HtmlMeta metaDesc = (HtmlMeta)p.Master.FindControl("description");
            HtmlMeta keyword = (HtmlMeta)p.Master.FindControl("keywords");

            if (Title != "" && title != null) title.Text = String.Format(_tmp, Title, _pageTitle);
            if (Description != "" && (metaDesc != null)) metaDesc.Content = String.Format(_tmp, Description, _pageTitle);
            if (Keywords != "" && (keyword != null)) keyword.Content = String.Format(_tmp, Keywords, string.Empty);
        }
        public static void SetPageHome(Page p, string Description, string Keywords)
        {
            string _tmp = "{0} - {1}";
            string _pageTitle = ConfigurationManager.AppSettings["PageTitle"];

            HtmlTitle title = (HtmlTitle)p.Master.FindControl("title");
            HtmlMeta metaDesc = (HtmlMeta)p.Master.FindControl("description");
            HtmlMeta keyword = (HtmlMeta)p.Master.FindControl("keywords");

            if (title != null) title.Text = _pageTitle;
            if (Description != "" && (metaDesc != null)) metaDesc.Content = String.Format(_tmp, Description, _pageTitle);
            if (Keywords != "" && (keyword != null)) keyword.Content = String.Format(_tmp, Keywords, string.Empty);
        }
        public static void SetPageHeaderDetail(Page p, string Title, string CatName, string Description, string Keywords)
        {
            string _tmp = "{0} - {1} - " + ConfigurationManager.AppSettings["PageTitle"];

            HtmlTitle title = (HtmlTitle)p.Master.FindControl("title");
            HtmlMeta metaDesc = (HtmlMeta)p.Master.FindControl("description");
            HtmlMeta keyword = (HtmlMeta)p.Master.FindControl("keywords");

            if (Title != "" && title != null) title.Text = RemoveRedundanceChar(String.Format(_tmp, Title, CatName), 100);
            if (Description != "" && (metaDesc != null)) metaDesc.Content = RemoveRedundanceChar(String.Format(_tmp, Description, string.Empty), 160);
            if (Keywords != "" && (keyword != null)) keyword.Content = String.Format(_tmp, Keywords, string.Empty);
        }

        public static string RemoveRedundanceChar(string input, int numOfChar)
        {
            if (input == null || input.Length == 0) return String.Empty;
            else if (input.Length <= numOfChar) return input;
            input = input.Substring(0, numOfChar);
            if (input.IndexOf(" ") != -1) return input.Substring(0, input.LastIndexOf(" "));
            return input;
        }



        public static string GetFormatImage(object FileName)
        {
            string result = string.Empty;
            if (FileName == null || FileName.ToString().Trim() == "") return "";
            string strFileName = HttpUtility.HtmlEncode(FileName.ToString());
            if (strFileName.ToLower().Contains(".pdf"))
                result = "iconAdobe.jpg";
            if (strFileName.ToLower().Contains(".doc"))
                result = "iconWord.jpg";
            if (strFileName.ToLower().Contains(".xls"))
                result = "iconExel.jpg";
            if (strFileName.ToLower().Contains(".ppt"))
                result = "iconPP.jpg";
            if (strFileName.ToLower().Contains(".rar") || strFileName.ToLower().Contains(".rip"))
                result = "iconWinzar.jpg";
            result = "<img alt=\"\" border=\"0\" width=\"13\" src=\"/Images/" + result + "\" />";
            return result;
        }
        public static bool isNewsByTime(DateTime date)
        {
            bool blResult = false;
            DateTime dtPublish = date.AddMinutes(10);
            blResult = dtPublish > DateTime.Now;
            return blResult;
        }
        public static string GetToolTip(object title, object img, object sapo)
        {
            string stitle = title.ToString().Replace("'", "\\\'");
            string ssapo = sapo.ToString().Replace("'", "\\\'");
            //string temp = "<a onmouseout=\"hideddrivetip()\" onmouseover=\"ddrivetip('{0}','{1}','{2}')\" href=\"{3}\">{0}</a>";
            string temp = " onmouseout=\"hideddrivetip()\" onmouseover=\"ddrivetip('{0}','{1}','{2}',this)\" ";
            return string.Format(temp, stitle, img, ssapo);
        }
        public static string RemoveHTMLTag(string htmlString)
        {
            if (htmlString == null) return String.Empty;
            string pattern = @"(<[^>]+>)";
            string text = System.Text.RegularExpressions.Regex.Replace(htmlString, pattern, string.Empty);
            return text;
        }

        public static string RemoveHTMLCommentTag(string htmlString)
        {
            if (htmlString == null) return String.Empty;
            string pattern = @"<!--(.*?)-->";
            string text = System.Text.RegularExpressions.Regex.Replace(htmlString, pattern, " ");
            return text;
        }

        /// <summary>
        /// 
        /// </summary>
        public static int News_ID
        {
            get
            {
                try
                {
                    return Convert.ToInt32(HttpContext.Current.Request.QueryString["News_ID"]);
                }
                catch
                {
                    return 0;
                }
            }
        }


        public static int Obj2Int(object obj)
        {
            if (obj == null)
                return 0;
            try
            {
                return Convert.ToInt32(obj);
            }
            catch { return 0; }
        }
        public static double Obj2Double(object value)
        {
            if (value == null || value.ToString().Trim() == "")
                return 0;
            try
            {
                return Convert.ToDouble(value, CultureInfo.InvariantCulture);
            }
            catch { return 0; }
        }
        public static long Obj2Int64(object obj)
        {
            try
            {
                return Convert.ToInt64(obj);
            }
            catch { return 0; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="tableName"></param>
        /// <param name="cacheName"></param>
        /// <param name="data"></param>
        public static void SaveToCacheDependency(string database, string tableName, string cacheName, object data)
        {

            //SqlCacheDependency sqlDep = new SqlCacheDependency(database, tableName);
            //if (data != null)
            //{
            //    HttpContext.Current.Cache.Insert(cacheName, data, sqlDep);
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="tableName"></param>
        /// <param name="cacheName"></param>
        /// <param name="data"></param>
        public static void SaveToCacheV2(string database, string tableName, string cacheName, object data)
        {

            SqlCacheDependency sqlDep = new SqlCacheDependency(database, tableName);
            if (data != null)
            {
                HttpContext.Current.Cache.Insert(cacheName, data, sqlDep);
            }

            // CacheController.Add(cacheName, data,0);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheName"></param>
        public static void RemoveFromCache(string cacheName)
        {
            HttpContext.Current.Cache.Remove(cacheName);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheName"></param>
        /// <returns></returns>
        public static T GetFromCache<T>(string cacheName)
        {

            object obj = HttpContext.Current.Cache[cacheName];
            if (obj != null && obj != DBNull.Value)
                return (T)obj;
            return default(T);
        }


        public static T GetFinnanceChannelvnFromCache<T>(string cacheName)
        {

            object obj = HttpContext.Current.Cache[cacheName];
            if (obj != null && obj != DBNull.Value)
                return (T)obj;
            return default(T);
        }
        public static string PortData = "0";

        //public static T GetFromMemCache<T>(string cacheName)
        //{
        //    if (CacheController.IsCacheExists(PortData, cacheName))
        //        return CacheController.Get<T>(PortData, cacheName);
        //    return default(T);
        //}
        public static string GetFromCache(string cacheName)
        {


            object obj = HttpContext.Current.Cache[cacheName];
            if (obj != null && obj != DBNull.Value)
                return obj.ToString();
            return null;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="tableName"></param>
        /// <param name="cacheName"></param>
        /// <param name="data"></param>
        public static void SaveToCacheDependency(string database, string[] tableName, string cacheName, object data)
        {

            AggregateCacheDependency aggCacheDep = new AggregateCacheDependency();
            SqlCacheDependency[] sqlDepGroup = new SqlCacheDependency[tableName.Length];
            for (int i = 0; i < tableName.Length; i++)
            {
                sqlDepGroup[i] = new SqlCacheDependency(database, tableName[i]);

            }
            aggCacheDep.Add(sqlDepGroup);
            if (data != null)
                HttpContext.Current.Cache.Insert(cacheName, data, aggCacheDep);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumber(string s)
        {
            try
            {
                int i = Convert.ToInt32(s);
                return true;
            }
            catch { return false; }

        }
        public static T GetObj<T>(object key)
        {
            if (key != null && key != DBNull.Value)
            {
                try
                {
                    return ((T)key);
                }
                catch
                {
                    return default(T);
                }
            }
            else
            {
                return default(T);
            }
        }

        /* Rewrite*/
        public static string GenerateURL(object Title, object strId)
        {
            string strTitle = Title.ToString();
            strTitle = strTitle.Trim();
            strTitle = strTitle.ToLower();
            char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            strTitle = strTitle.Replace(".", "-");
            for (int i = 0; i < chars.Length; i++)
            {
                string strChar = chars.GetValue(i).ToString();
                if (strTitle.Contains(strChar)) { strTitle = strTitle.Replace(strChar, string.Empty); }
            }
            strTitle = strTitle.Replace(" ", "-");
            strTitle = strTitle.Replace("--", "-");
            strTitle = strTitle.Replace("---", "-");
            strTitle = strTitle.Replace("----", "-");
            strTitle = strTitle.Replace("-----", "-");
            strTitle = strTitle.Replace("----", "-");
            strTitle = strTitle.Replace("---", "-");
            strTitle = strTitle.Replace("--", "-");
            strTitle = strTitle.Trim();
            strTitle = strTitle.Trim('-');
            strTitle = "~/Article/" + strTitle + "-" + strId + ".aspx";
            return strTitle;
        }
        public const string uniChars = "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";
        public const string KoDauChars = "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";
        public static string UnicodeToKoDauAndGach(string s)
        {
            string retVal = String.Empty;
            int pos;
            for (int i = 0; i < s.Length; i++)
            {
                pos = uniChars.IndexOf(s[i].ToString());
                if (pos >= 0)
                    retVal += KoDauChars[pos];
                else
                    retVal += s[i];
            }
            retVal = retVal.Replace("–", "");
            retVal = retVal.Replace("-", "");
            retVal = retVal.Replace("  ", "");
            retVal = retVal.Replace(" ", "-");
            retVal = retVal.Replace("--", "-");
            retVal = retVal.Replace(":", "");
            retVal = retVal.Replace(";", "");
            retVal = retVal.Replace("+", "");
            retVal = retVal.Replace("@", "");
            retVal = retVal.Replace(">", "");
            retVal = retVal.Replace("<", "");
            retVal = retVal.Replace("*", "");
            retVal = retVal.Replace("{", "");
            retVal = retVal.Replace("}", "");
            retVal = retVal.Replace("|", "");
            retVal = retVal.Replace("^", "");
            retVal = retVal.Replace("~", "");
            retVal = retVal.Replace("]", "");
            retVal = retVal.Replace("[", "");
            retVal = retVal.Replace("`", "");
            retVal = retVal.Replace(".", "");
            retVal = retVal.Replace("'", "");
            retVal = retVal.Replace("(", "");
            retVal = retVal.Replace(")", "");
            retVal = retVal.Replace(",", "");
            retVal = retVal.Replace("”", "");
            retVal = retVal.Replace("“", "");
            retVal = retVal.Replace("?", "");
            retVal = retVal.Replace("\"", "");
            retVal = retVal.Replace("&", "");
            retVal = retVal.Replace("$", "");
            retVal = retVal.Replace("#", "");
            retVal = retVal.Replace("_", "");
            retVal = retVal.Replace("=", "");
            retVal = retVal.Replace("%", "");
            retVal = retVal.Replace("…", "");
            retVal = retVal.Replace("/", "");
            retVal = retVal.Replace("\\", "");
            retVal = retVal.Replace(" ", "-");
            retVal = retVal.Replace("--", "-");
            retVal = retVal.Replace("---", "-");
            retVal = retVal.Replace("----", "-");
            retVal = retVal.Replace("-----", "-");
            return retVal.ToLower().TrimEnd('-').TrimStart('-');
        }
        /* End Rewrite*/
        public static void SetFocusDropdown(DropDownList ddl, string value)
        {
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                if (ddl.Items[i].Value == value)
                {
                    ddl.SelectedIndex = i;
                    return;
                }
            }
            return;
        }

        public static string CatSapo(string input, int sotu)
        {
            if (input == null) return "";
            string[] arr = input.Split(' ');
            if (arr.Length <= sotu) return input;
            else return String.Join(" ", arr, 0, sotu - 1) + " ...";
        }
        public static string ColorLink(string ID, string colorName)
        {
           return String.Format("/productcolor/{0}-{1}.htm", ID, colorName);
        }
        public static string CatLink(string catId, string catParentId, string CatDisplayURL,string Channel_ID)
        {
            string channelName = Channel_ID == "1" ? "/news" : Channel_ID == "2" ? "/projects" : "";
            return String.Format(channelName + "/{2}-p{1}c{0}.htm", catId, catParentId, CatDisplayURL);
        }
        public static string NewsDetailLinkV2(string news_title, string catId, string catParentId, string newsId, string Channel_ID)
        {
            string channelName = Channel_ID == "1" ? "/news" : Channel_ID == "2" ? "/products" : "";
            return String.Format(channelName + "/{0}p{1}c{2}/{3}.htm", newsId, catParentId, catId, UnicodeToKoDauAndGach(HttpUtility.HtmlDecode(news_title)));
        }
        public static string NewsDetailLink(string news_title, int catId, int catParentId, long newsId)
        {
            return String.Format("/news/{0}p{1}c{2}/{3}.htm", newsId, catParentId, catId, UnicodeToKoDauAndGach(HttpUtility.HtmlDecode(news_title))).ToLower();
        }

        public static string ImagesThumbUrl = System.Configuration.ConfigurationSettings.AppSettings["ImageUrl"].ToString().TrimEnd('/');
        public static string ImagesStorageUrl = System.Configuration.ConfigurationSettings.AppSettings["ImagesStorageUrl"].ToString().TrimEnd('/');

        public static string GetImageLink(string title, string url, string img)
        {
            if (img == null || String.IsNullOrEmpty(img) || img.IndexOf(".") == -1) return String.Empty;
            if (img.IndexOf("http:") != -1 || img.IndexOf("https:") != -1)
            {
                return String.Format("<a title=\"{2}\" href=\"{1}\"><img src=\"{0}\" alt=\"{2}\" border=\"0\"/></a>", img, url, HttpUtility.HtmlEncode(title));
            }
           
            img = img.Replace(ImagesStorageUrl, "").TrimStart('/');
            return String.Format("<a title=\"{3}\" href=\"{2}\"><img src=\"{0}/{1}\" alt=\"{3}\" border=\"0\"/></a>", ImagesStorageUrl, img, url, HttpUtility.HtmlEncode(title));
        }
        public static string GetImageLink(string title, string url, string img,string cssClass)
        {
            if (img == null || String.IsNullOrEmpty(img) || img.IndexOf(".") == -1) return String.Empty;
            if (img.IndexOf("http:") != -1 || img.IndexOf("https:") != -1)
            {
                return String.Format("<a title=\"{2}\" href=\"{1}\"><img src=\"{0}\" alt=\"{2}\" border=\"0\"/></a>", img, url, HttpUtility.HtmlEncode(title));
            }

            img = img.Replace(ImagesStorageUrl, "").TrimStart('/');
            return String.Format("<a title=\"{3}\" href=\"{2}\"><img src=\"{0}/{1}\" alt=\"{3}\" border=\"0\" class=\"{4}\"/></a>", ImagesStorageUrl, img, url, HttpUtility.HtmlEncode(title), cssClass);
        }
        public static string GetThumbNail(string title, string url, string img, int width,bool targetBlank = false)
        {
            if (img == null || String.IsNullOrEmpty(img) || img.IndexOf(".") == -1) return String.Empty;
            string error;
            string thumb;
            if (img.IndexOf("http:") != -1 || img.IndexOf("https:") != -1)
            {
                error = String.Format("{0}/GetThumbNail.ashx?ImgFilePath={1}&width={2}", ImagesThumbUrl, HttpUtility.UrlEncode(img), width);

                img = img.Replace("http://", "").Replace("https://", "");
                img = img.Substring(img.IndexOf("/") + 1);

                thumb = String.Format("{0}/ThumbImages/{1}", ImagesThumbUrl, img.Insert(img.LastIndexOf("."), "_" + width));
                return String.Format("<a title=\"{2}\" href=\"{0}\"><img src=\"{1}\" onerror=\"LoadImage(this,'{3}');\" title=\"{2}\" alt=\"{4}\" border=\"0\"/></a>", url, thumb, HttpUtility.HtmlEncode(title), error, UnicodeToKoDau(title));
            }
            error = String.Format("{0}/GetThumbNail.ashx?ImgFilePath={1}/{2}&width={3}", ImagesThumbUrl, ImagesStorageUrl, HttpUtility.UrlEncode(img), width);
            if (!string.IsNullOrEmpty(ImagesStorageUrl))
                img = img.Replace(ImagesStorageUrl, "").TrimStart('/');

            thumb = String.Format("{0}/ThumbImages/{1}", ImagesThumbUrl, img.Insert(img.LastIndexOf("."), "_" + width));
            return String.Format("<a title=\"{2}\" href=\"{0}\" target=\"{5}\"><img src=\"{1}\" onerror=\"LoadImage(this,'{3}');\" title=\"{2}\" alt=\"{4}\" border=\"0\"/></a>", url, thumb, HttpUtility.HtmlEncode(title), error, UnicodeToKoDau(title),targetBlank?"_blank":string.Empty);
        }
        public static string GetThumbNail(string title, string url, string img, int width,string rel)
        {
            if (img == null || String.IsNullOrEmpty(img) || img.IndexOf(".") == -1) return String.Empty;
            string error;
            string thumb;
            if (img.IndexOf("http:") != -1 || img.IndexOf("https:") != -1)
            {
                error = String.Format("{0}/GetThumbNail.ashx?ImgFilePath={1}&width={2}", ImagesThumbUrl, HttpUtility.UrlEncode(img), width);

                img = img.Replace("http://", "").Replace("https://", "");
                img = img.Substring(img.IndexOf("/") + 1);

                thumb = String.Format("{0}/ThumbImages/{1}", ImagesThumbUrl, img.Insert(img.LastIndexOf("."), "_" + width));
                return String.Format("<a title=\"{2}\" href=\"{0}\"><img src=\"{1}\" onerror=\"LoadImage(this,'{3}');\" title=\"{2}\" alt=\"{4}\" border=\"0\"/></a>", url, thumb, HttpUtility.HtmlEncode(title), error, UnicodeToKoDau(title));
            }
            error = String.Format("{0}/GetThumbNail.ashx?ImgFilePath={1}/{2}&width={3}", ImagesThumbUrl, ImagesStorageUrl, HttpUtility.UrlEncode(img), width);
            if (!string.IsNullOrEmpty(ImagesStorageUrl))
                img = img.Replace(ImagesStorageUrl, "").TrimStart('/');

            thumb = String.Format("{0}/ThumbImages/{1}", ImagesThumbUrl, img.Insert(img.LastIndexOf("."), "_" + width));
            return String.Format("<a rel=\"{5}\" title=\"{2}\" href=\"{0}\"><img src=\"{1}\" onerror=\"LoadImage(this,'{3}');\" title=\"{2}\" alt=\"{4}\" border=\"0\"/></a>", url, thumb, HttpUtility.HtmlEncode(title), error, UnicodeToKoDau(title),rel);
        }
        public static string GetThumbNailWithPlayIcon(string title, string url, string img, int width)
        {
            if (img == null || String.IsNullOrEmpty(img) || img.IndexOf(".") == -1) return String.Empty;
            string error;
            string thumb;
            if (img.IndexOf("http:") != -1 || img.IndexOf("https:") != -1)
            {
                error = String.Format("{0}/GetThumbNail.ashx?ImgFilePath={1}&width={2}", ImagesThumbUrl, HttpUtility.UrlEncode(img), width);

                img = img.Replace("http://", "").Replace("https://", "");
                img = img.Substring(img.IndexOf("/") + 1);

                thumb = String.Format("{0}/ThumbImages/{1}", ImagesThumbUrl, img.Insert(img.LastIndexOf("."), "_" + width));
                return String.Format("<a title=\"{2}\" href=\"{0}\"><img src=\"{1}\" onerror=\"LoadImage(this,'{3}');\" title=\"{2}\" alt=\"{4}\" border=\"0\"/></a>", url, thumb, HttpUtility.HtmlEncode(title), error, UnicodeToKoDau(title));
            }
            error = String.Format("{0}/GetThumbNail.ashx?ImgFilePath={1}/{2}&width={3}", ImagesThumbUrl, ImagesStorageUrl, HttpUtility.UrlEncode(img), width);
            if (!string.IsNullOrEmpty(ImagesStorageUrl))
                img = img.Replace(ImagesStorageUrl, "").TrimStart('/');

            thumb = String.Format("{0}/ThumbImages/{1}", ImagesThumbUrl, img.Insert(img.LastIndexOf("."), "_" + width));
            return String.Format("<a title=\"{2}\" href=\"{0}\"><img src=\"{1}\" onerror=\"LoadImage(this,'{3}');\" title=\"{2}\" alt=\"{4}\" border=\"0\"/><img src=\"/images/play_ico.png\" class=\"play\" alt=\"\">    </a>", url, thumb, HttpUtility.HtmlEncode(title), error, UnicodeToKoDau(title));
        }
        //public static string GetThumbNail(string title, string url, string img, int width)
        //{
        //    if (img == null || String.IsNullOrEmpty(img) || img.IndexOf(".") == -1) return String.Empty;
        //    string error = String.Format("{0}/GetThumbNail.ashx?ImgFilePath={1}/{2}&width={3}", ImagesThumbUrl, ImagesStorageUrl, HttpUtility.UrlEncode(img), width);
        //    img = img.Replace(ImagesStorageUrl, "").TrimStart('/');
        //    string strImagesStorageUrl = ImagesStorageUrl;
        //    if (img.ToLower().Contains("http://"))
        //        img = img.Replace();
        //    string thumb = String.Format("{0}/ThumbImages/{1}", strImagesStorageUrl, img.Insert(img.LastIndexOf("."), "_" + width));
        //    return String.Format("<a title=\"{2}\" href=\"{0}\"><img src=\"{1}\" onerror=\"LoadImage(this,'{3}');\" title=\"{2}\" border=\"0\"/></a>", url, thumb, HttpUtility.HtmlEncode(title), error);
        //}
        public static string GetThumbNailNoLink(string title,  string img, int width)
        {
            

            if (img == null || String.IsNullOrEmpty(img) || img.IndexOf(".") == -1) return String.Empty;
            string error;
            string thumb;
            if (img.IndexOf("http:") != -1 || img.IndexOf("https:") != -1)
            {
                error = String.Format("{0}/GetThumbNail.ashx?ImgFilePath={1}&width={2}", ImagesThumbUrl, HttpUtility.UrlEncode(img), width);

                img = img.Replace("http://", "").Replace("https://", "");
                img = img.Substring(img.IndexOf("/") + 1);

                thumb = String.Format("{0}/ThumbImages/{1}", ImagesThumbUrl, img.Insert(img.LastIndexOf("."), "_" + width));
                return String.Format("<img src=\"{0}\" onerror=\"LoadImage(this,'{2}');\" title=\"{1}\" alt=\"{3}\" border=\"0\"/>",  thumb, HttpUtility.HtmlEncode(title), error, UnicodeToKoDau(title));
            }
            error = String.Format("{0}/GetThumbNail.ashx?ImgFilePath={1}/{2}&width={3}", ImagesThumbUrl, ImagesStorageUrl, HttpUtility.UrlEncode(img), width);
            img = img.Replace(ImagesStorageUrl, "").TrimStart('/');

            thumb = String.Format("{0}/ThumbImages/{1}", ImagesThumbUrl, img.Insert(img.LastIndexOf("."), "_" + width));
            return String.Format("<img src=\"{0}\" onerror=\"LoadImage(this,'{2}');\" title=\"{1}\" alt=\"{3}\" border=\"0\"/>",  thumb, HttpUtility.HtmlEncode(title), error, UnicodeToKoDau(title));


        }
        public static string ChangeToVietNamDate(DateTime dt)
        {
            string strVietNameDate = "";
            try
            {
                string t = dt.DayOfWeek.ToString();
                string ngay = "";
                switch (t.ToLower())
                {
                    case "monday":
                        ngay = "Thứ hai";
                        break;
                    case "tuesday":
                        ngay = "Thứ ba";
                        break;
                    case "wednesday":
                        ngay = "Thứ tư";
                        break;
                    case "thursday":
                        ngay = "Thứ năm";
                        break;
                    case "friday":
                        ngay = "Thứ sáu";
                        break;
                    case "saturday":
                        ngay = "Thứ bảy";
                        break;
                    case "sunday":
                        ngay = "Chủ nhật";
                        break;
                }
                strVietNameDate = ngay + ", " + dt.ToString("dd/MM/yyyy HH:mm");
            }
            catch { }
            return strVietNameDate;
        }
        public static string ChangeToVietNamDate2(DateTime dt)
        {
            string strVietNameDate = "";
            try
            {
                string t = dt.DayOfWeek.ToString();
                string ngay = "";
                switch (t.ToLower())
                {
                    case "monday":
                        ngay = "Thứ hai";
                        break;
                    case "tuesday":
                        ngay = "Thứ ba";
                        break;
                    case "wednesday":
                        ngay = "Thứ tư";
                        break;
                    case "thursday":
                        ngay = "Thứ năm";
                        break;
                    case "friday":
                        ngay = "Thứ sáu";
                        break;
                    case "saturday":
                        ngay = "Thứ bảy";
                        break;
                    case "sunday":
                        ngay = "Chủ nhật";
                        break;
                }
                strVietNameDate = ngay + ", " + dt.ToString("dd/MM/yyyy HH:mm");
            }
            catch { }
            return strVietNameDate;
        }


        public static string ChangeToVietNamDate3(DateTime dt)
        {
            string strVietNameDate = "";
            try
            {
                string t = dt.DayOfWeek.ToString();
                string ngay = "";
                switch (t.ToLower())
                {
                    case "monday":
                        ngay = "Thứ hai";
                        break;
                    case "tuesday":
                        ngay = "Thứ ba";
                        break;
                    case "wednesday":
                        ngay = "Thứ tư";
                        break;
                    case "thursday":
                        ngay = "Thứ năm";
                        break;
                    case "friday":
                        ngay = "Thứ sáu";
                        break;
                    case "saturday":
                        ngay = "Thứ bảy";
                        break;
                    case "sunday":
                        ngay = "Chủ nhật";
                        break;
                }
                strVietNameDate = dt.ToString("HH:mm") + " " + ngay + ", " + dt.ToString("dd/MM/yyyy");
            }
            catch { }
            return strVietNameDate;
        }
        public static DateTime ObjectToDateTime(object value)
        {
            DateTime dtResult = DateTime.MinValue;
            try
            {
                dtResult = Convert.ToDateTime(value);
            }
            catch { }
            return dtResult;
        }
        public static string ChangeToVietNamDate4(object value)
        {
            string strVietNameDate = "";
            DateTime dt = ObjectToDateTime(value);
            try
            {
                int day = (int)dt.DayOfWeek + 1;
                string ngay = day == 8 ? " chủ nhật" : " thứ " + day;

                strVietNameDate = "Cập nhật lúc " + dt.ToString("HH:mm") + ngay + ", " + dt.ToString("dd/MM/yyyy");
            }
            catch { }
            return strVietNameDate;
        }
        public static string RemoveHTMLWord(string p)
        {
            if (p == null) return String.Empty;
            p = p.Replace(" class=\"MsoNormal\" style=\"margin: 0in 0in 0pt;\"", " ");
            p = p.Replace("<span style=\"color: black;\"><o:p>&nbsp;</o:p></span></p>", " ");
            p = p.Replace("<span style=\"\"><o:p>&nbsp;</o:p></span></p>", " ");
            p = p.Replace("<span style=\"color: black;\"><o:p></o:p></span>", " ");

            p = p.Replace("style=\"color: black;\" lang=\"VI\"", " ");
            p = p.Replace("<span style=\"\"><o:p></o:p></span>", " ");
            p = p.Replace("<p></p>", " ");
            p = p.Replace("</o:p>", " ");
            p = p.Replace("<p><p>", "<p>");
            p = p.Replace("<o:p>", " ");
            p = p.Replace("class=\"MsoNormal\"", " ");
            p = RemoveHTMLCommentTag(p);
            return p;
        }

        #region "SEO"

        public const string __PAGE_KEYWORD =
               "cafef,tài chính,chứng khoán,vn-index,hastc-index,P/E,EPS,bất động sản,nhà đất,địa ốc,ngân hàng,đầu tư,cổ phiếu,cổ phần, trái phiếu, cổ tức,danh mục đầu tư,quản lý lãi lỗ,thị trường niêm yết,otc, blog tài chính, chính sách, tiền tệ, lạm phát, dữ liệu doanh nghiệp, blue-chips, vàng, Đô la, USD, IPO, đấu giá, bán khống, phái sinh,vốn điều lệ, lợi nhuận, tỷ giá, kinh tế, chuyên gia kinh tế, khủng hoảng kinh tế, suy thoái kinh tế, tài chính quốc tế, kinh doanh, tín dụng, quỹ đầu tư,  Việt Nam";

        public const string __PAGE_TITLE = "  CafeF.vn - Thông tin và dữ liệu tài chính, chứng khoán Việt Nam";
        public const string __PAGE_TITLE1 = "  Thông tin và dữ liệu tài chính, chứng khoán Việt Nam";
        public static string UnicodeToKoDau(string s)
        {
            //return s;
            string retVal = String.Empty;
            s = s.Trim();
            int pos;
            for (int i = 0; i < s.Length; i++)
            {
                pos = uniChars.IndexOf(s[i].ToString());
                if (pos >= 0)
                    retVal += KoDauChars[pos];
                else
                    retVal += s[i];
            }
            retVal = retVal.Replace("-", "");
            //retVal = retVal.Replace(" ", "-");
            retVal = retVal.Replace(":", "");
            retVal = retVal.Replace(";", "");
            retVal = retVal.Replace("+", "");
            retVal = retVal.Replace("@", "");
            retVal = retVal.Replace(">", "");
            retVal = retVal.Replace("<", "");
            retVal = retVal.Replace("*", "");
            retVal = retVal.Replace("{", "");
            retVal = retVal.Replace("}", "");
            retVal = retVal.Replace("|", "");
            retVal = retVal.Replace("^", "");
            retVal = retVal.Replace("~", "");
            retVal = retVal.Replace("]", "");
            retVal = retVal.Replace("[", "");
            retVal = retVal.Replace("`", "");
            retVal = retVal.Replace(".", "");
            retVal = retVal.Replace("'", "");
            retVal = retVal.Replace("(", "");
            retVal = retVal.Replace(")", "");
            retVal = retVal.Replace(",", "");
            retVal = retVal.Replace("”", "");
            retVal = retVal.Replace("“", "");
            retVal = retVal.Replace("?", "");
            retVal = retVal.Replace("\"", "");
            retVal = retVal.Replace("&", "");
            retVal = retVal.Replace("$", "");
            retVal = retVal.Replace("#", "");
            retVal = retVal.Replace("_", "");
            retVal = retVal.Replace("=", "");
            retVal = retVal.Replace("%", "");
            retVal = retVal.Replace("…", "");
            retVal = retVal.Replace("/", "-");
            retVal = retVal.Replace("\\", "");
            retVal = retVal.Replace("''", "");
            return retVal;
        }
        public static string ReplaceEmpty(string retVal)
        {
            retVal = retVal.Replace("''", "");
            return retVal;
        }
        //public static string CatSapo(string input, int sotu)
        //{
        //    if (input == null) return "";
        //    string[] arr = input.Split(' ');
        //    if (arr.Length <= sotu) return input;
        //    else return String.Join(" ", arr, 0, sotu - 1) + " ...";
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="__Page"></param>
        /// <param name="addTitle1">Tieu de bai viet</param>
        /// <param name="addTitle2">Tieu de bai viet khong dau</param>
        /// <param name="addChapo">Chapo cua bai de dua vao the meta des</param>
        /// 
        //public static void Set_Page_Header(Page __Page, string addTitle1, string addTitle2, string addChapo, int Cat_ID) {
        //    Control __ctlTitle = __Page.Header.FindControl("Title");
        //    string __keyword = __PAGE_KEYWORD;
        //    if (__ctlTitle != null) {
        //        //string __newTitle = addTitle1 != ""
        //        //                        ? ( addTitle1 +
        //        //                           (addTitle2 != "" ? " - " + addTitle2 + " - " : " - ") + "  CafeF.vn - " + __PAGE_TITLE1)
        //        //                        : __PAGE_TITLE;
        //        string __newTitle = addTitle1 != ""
        //                                ? (addTitle1 +
        //                                   (addTitle2 != "" ? " | " + addTitle2 : "") + " | CafeF.vn")
        //                                : __PAGE_TITLE;
        //        __ctlTitle.Controls.Add(new LiteralControl(__newTitle));
        //    }
        //    HtmlMeta __ctlMetaKeyword = __Page.Header.FindControl("KEYWORDS") as HtmlMeta;
        //    if (Cat_ID > 0) {
        //        if (__ctlMetaKeyword != null) {
        //            if (Cat_ID == 31) __keyword = Const.TCCK_KEYWORD; //chung khoan
        //            if (Cat_ID == 35) __keyword = Const.BDS_KEYWORD;
        //            if (Cat_ID == 34) __keyword = Const.TCNH_KEYWORD;
        //            if (Cat_ID == 32) __keyword = Const.TCQT_KEYWORD;
        //            if (Cat_ID == 36) __keyword = Const.DN_KEYWORD;
        //            if (Cat_ID == 33) __keyword = Const.KTDT_KEYWORD;
        //            if (__keyword == "") __keyword = __PAGE_KEYWORD;
        //            __ctlMetaKeyword.Attributes["content"] = __keyword;
        //        }
        //    }
        //    else {
        //        __ctlMetaKeyword.Visible = false;
        //    }
        //    HtmlMeta __ctlMetaDesc = __Page.Header.FindControl("description") as HtmlMeta;
        //    //if (__ctlMetaDesc != null)
        //    //{
        //    //    __ctlMetaDesc.Attributes["content"] = addChapo != "" ? addChapo + "," + __keyword : __keyword;
        //    //}

        //    //description mà có keywords nữa là thừa, vì trên thẻ meta keywords đã có rồi mà lại để ở description nữa là trùng lặp
        //    //SonPC Modified
        //    if (__ctlMetaDesc != null) {
        //        __ctlMetaDesc.Attributes["content"] = addChapo != "" ? addChapo : "";
        //    }
        //    //SonPC Modifed

        //}

        //public static void Set_Page_Header(Page __Page, string addTitle1, int Cat_ID) {
        //    Control __ctlTitle = __Page.Header.FindControl("Title");
        //    if (__ctlTitle != null) {
        //        string _title_des = "";

        //        switch (Cat_ID) {
        //            case 31:
        //                _title_des = Const.TCCK_TITLE_DES;
        //                break;
        //            case 35:
        //                _title_des = Const.BDS_TITLE_DES;
        //                break;
        //            case 34:
        //                _title_des = Const.TCNH_TITLE_DES;
        //                break;
        //            case 32:
        //                _title_des = Const.TCQT_TITLE_DES;
        //                break;
        //            case 36:
        //                _title_des = Const.DN_TITLE_DES;
        //                break;
        //            case 33:
        //                _title_des = Const.KTDT_TITLE_DES;
        //                break;
        //        }

        //        //if (Cat_ID == 31) _title_des = Const.TCCK_TITLE_DES;
        //        //else
        //        //    if (Cat_ID == 35) _title_des = Const.BDS_TITLE_DES;
        //        //    else
        //        //        if (Cat_ID == 34) _title_des = Const.TCNH_TITLE_DES;
        //        //        else
        //        //            if (Cat_ID == 32) _title_des = Const.TCQT_TITLE_DES;
        //        //            else
        //        //                if (Cat_ID == 36) _title_des = Const.DN_TITLE_DES;
        //        //                else
        //        //                    if (Cat_ID == 33) _title_des = Const.KTDT_TITLE_DES;

        //        string __newTitle = _title_des;
        //        //if (_title_des != "")
        //        //    __newTitle =  addTitle1 + " - CafeF.vn - "  + _title_des;
        //        //else
        //        //    __newTitle = addTitle1 + " - CafeF.vn ";

        //        __ctlTitle.Controls.Add(new LiteralControl(__newTitle));
        //    }
        //    HtmlMeta __ctlMetaDesc = __Page.Header.FindControl("description") as HtmlMeta;
        //    if (__ctlMetaDesc != null) {
        //        string __description = "";

        //        switch (Cat_ID) {
        //            case 31:
        //                __description = Const.TCCK_DESCRIPTION;
        //                break;
        //            case 35:
        //                __description = Const.BDS_DESCRIPTION;
        //                break;
        //            case 34:
        //                __description = Const.TCNH_DESCRIPTION;
        //                break;
        //            case 32:
        //                __description = Const.TCQT_DESCRIPTION;
        //                break;
        //            case 36:
        //                __description = Const.DN_DESCRIPTION;
        //                break;
        //            case 33:
        //                __description = Const.KTDT_DESCRIPTION;
        //                break;
        //        }

        //        if (__description == "") __description = __PAGE_KEYWORD;
        //        __ctlMetaDesc.Attributes["content"] = __description;
        //    }
        //    HtmlMeta __ctlMetaKeyword = __Page.Header.FindControl("KEYWORDS") as HtmlMeta;
        //    if (__ctlMetaKeyword != null) {
        //        string __keyword = "";

        //        switch (Cat_ID) {
        //            case 31:
        //                __keyword = Const.TCCK_KEYWORD;
        //                break;
        //            case 35:
        //                __keyword = Const.BDS_KEYWORD;
        //                break;
        //            case 34:
        //                __keyword = Const.TCNH_KEYWORD;
        //                break;
        //            case 32:
        //                __keyword = Const.TCQT_KEYWORD;
        //                break;
        //            case 36:
        //                __keyword = Const.DN_KEYWORD;
        //                break;
        //            case 33:
        //                __keyword = Const.KTDT_KEYWORD;
        //                break;
        //        }


        //        if (__keyword == "") __keyword = __PAGE_KEYWORD;
        //        __ctlMetaKeyword.Attributes["content"] = __keyword;
        //    }
        //}

        //public static void Set_Page_Header(Page __Page, int pageIndex, int Cat_ID, DateTime startDate, DateTime endDate, bool showKeyword) {
        //    string viewDate = "";
        //    if (startDate != DateTime.MaxValue && endDate != DateTime.MaxValue) {
        //        if (startDate != endDate) {
        //            viewDate = " | " + startDate.ToString("dd/MM/yyyy") + " - " + endDate.ToString("dd/MM/yyyy");
        //        }
        //        else {
        //            viewDate = " | " + startDate.ToString("dd/MM/yyyy");
        //        }
        //    }

        //    Control __ctlTitle = __Page.Header.FindControl("Title");
        //    if (__ctlTitle != null) {
        //        string _title_des = "";

        //        switch (Cat_ID) {
        //            case 31:
        //                _title_des = string.Format(Const.TCCK_TITLE_DES, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
        //                break;
        //            case 35:
        //                _title_des = string.Format(Const.BDS_TITLE_DES, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
        //                break;
        //            case 34:
        //                _title_des = string.Format(Const.TCNH_TITLE_DES, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
        //                break;
        //            case 32:
        //                _title_des = string.Format(Const.TCQT_TITLE_DES, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
        //                break;
        //            case 36:
        //                _title_des = string.Format(Const.DN_TITLE_DES, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
        //                break;
        //            case 33:
        //                _title_des = string.Format(Const.KTDT_TITLE_DES, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
        //                break;
        //            case 39:
        //                _title_des = string.Format(Const.TTHH_TITLE_DES, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
        //                break;
        //        }

        //        string __newTitle = _title_des;

        //        __ctlTitle.Controls.Add(new LiteralControl(__newTitle));
        //    }

        //    HtmlMeta __ctlMetaDesc = __Page.Header.FindControl("description") as HtmlMeta;
        //    if (__ctlMetaDesc != null) {
        //        string __description = "";

        //        switch (Cat_ID) {
        //            case 31:
        //                __description = string.Format(Const.TCCK_DESCRIPTION, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
        //                break;
        //            case 35:
        //                __description = string.Format(Const.BDS_DESCRIPTION, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
        //                break;
        //            case 34:
        //                __description = string.Format(Const.TCNH_DESCRIPTION, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
        //                break;
        //            case 32:
        //                __description = string.Format(Const.TCQT_DESCRIPTION, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
        //                break;
        //            case 36:
        //                __description = string.Format(Const.DN_DESCRIPTION, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
        //                break;
        //            case 33:
        //                __description = string.Format(Const.KTDT_DESCRIPTION, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
        //                break;
        //            case 39:
        //                __description = string.Format(Const.TTHH_DESCRIPTION, viewDate + (pageIndex > 1 ? " | Trang " + pageIndex.ToString() : ""));
        //                break;
        //        }

        //        if (__description == "") __description = __PAGE_KEYWORD;
        //        __ctlMetaDesc.Attributes["content"] = __description;
        //    }
        //    HtmlMeta __ctlMetaKeyword = __Page.Header.FindControl("KEYWORDS") as HtmlMeta;
        //    if (showKeyword && __ctlMetaKeyword != null) {
        //        string __keyword = "";

        //        switch (Cat_ID) {
        //            case 31:
        //                __keyword = Const.TCCK_KEYWORD;
        //                break;
        //            case 35:
        //                __keyword = Const.BDS_KEYWORD;
        //                break;
        //            case 34:
        //                __keyword = Const.TCNH_KEYWORD;
        //                break;
        //            case 32:
        //                __keyword = Const.TCQT_KEYWORD;
        //                break;
        //            case 36:
        //                __keyword = Const.DN_KEYWORD;
        //                break;
        //            case 33:
        //                __keyword = Const.KTDT_KEYWORD;
        //                break;
        //            case 39:
        //                __keyword = Const.TTHH_KEYWORD;
        //                break;
        //        }

        //        //if (Cat_ID == 31) __keyword = Const.TCCK_KEYWORD;
        //        //else
        //        //    if (Cat_ID == 35) __keyword = Const.BDS_KEYWORD;
        //        //    else
        //        //        if (Cat_ID == 34) __keyword = Const.TCNH_KEYWORD;
        //        //        else
        //        //            if (Cat_ID == 32) __keyword = Const.TCQT_KEYWORD;
        //        //            else
        //        //                if (Cat_ID == 36) __keyword = Const.DN_KEYWORD;
        //        //                else
        //        //                    if (Cat_ID == 33) __keyword = Const.KTDT_KEYWORD;
        //        if (__keyword == "") __keyword = __PAGE_KEYWORD;
        //        __ctlMetaKeyword.Attributes["content"] = __keyword;
        //    }
        //    __ctlMetaKeyword.Visible = showKeyword;
        //}

        public static void Set_Page_Header(Page __Page, string addTitle1, string des, string key)
        {
            Control __ctlTitle = __Page.Header.FindControl("Title");
            if (__ctlTitle != null)
            {
                __ctlTitle.Controls.Add(new LiteralControl(addTitle1));
            }
            HtmlMeta __ctlMetaDesc = __Page.Header.FindControl("description") as HtmlMeta;
            if (__ctlMetaDesc != null)
            {
                __ctlMetaDesc.Attributes["content"] = des;
            }
            HtmlMeta __ctlMetaKeyword = __Page.Header.FindControl("KEYWORDS") as HtmlMeta;
            if (__ctlMetaKeyword != null)
            {
                if (key == "")
                {
                    __ctlMetaKeyword.Visible = false;
                }
                else
                {
                    __ctlMetaKeyword.Visible = true;
                    __ctlMetaKeyword.Attributes["content"] = key;
                }
            }
        }

        public static void Set_Page_Header(Page __Page, string addTitle1, string des, string key, bool showKeyword)
        {
            Control __ctlTitle = __Page.Header.FindControl("Title");
            if (__ctlTitle != null)
            {
                __ctlTitle.Controls.Add(new LiteralControl(addTitle1));
            }
            HtmlMeta __ctlMetaDesc = __Page.Header.FindControl("description") as HtmlMeta;
            if (__ctlMetaDesc != null)
            {
                __ctlMetaDesc.Attributes["content"] = des;
            }
            HtmlMeta __ctlMetaKeyword = __Page.Header.FindControl("KEYWORDS") as HtmlMeta;
            if (showKeyword && __ctlMetaKeyword != null)
            {
                __ctlMetaKeyword.Attributes["content"] = key;
            }
            __ctlMetaKeyword.Visible = showKeyword;
        }
        #endregion


        /// <summary>
        /// Thông báo cho Search Engine biết Link đã bị xóa
        /// </summary>
        public static void DeletedLinkNotify()
        {
            HttpContext.Current.Response.Status = "410 Gone";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p">Trang hiện tại</param>
        /// <param name="url">Đường dẫn mới nhất của bài viết</param>
        public static void SetCanonicalLink(Page p, string url)
        {
            HtmlLink link = new HtmlLink();
            link.Href = url;
            link.Attributes.Add("rel", "canonical");
            p.Header.Controls.Add(link);
        }

        /// <summary>
        /// Thiết đặt SEO cho Facebook
        /// </summary>
        /// <param name="p">Page cần Set trạng thái</param>
        /// <param name="title">Tiêu đề bài viết</param>
        /// <param name="sapo">Tóm tắt bài viết</param>
        /// <param name="images">Dường dẫn ảnh nếu có</param>
        /// <param name="url">Đường dẫn đến bài viết mới nhất</param>
        public static void SetFaceBookSEO(Page p, string title, string sapo, string images, string url)
        {
            #region Facebook
            HtmlMeta m = new HtmlMeta();
            if (!string.IsNullOrEmpty(title))
            {
                m.Attributes.Add("property", "og:title");
                m.Content = HttpUtility.HtmlDecode(title);
                p.Header.Controls.Add(m);
            }

            m = new HtmlMeta();
            m.Attributes.Add("property", "og:type");
            m.Attributes.Add("Content", "article");
            p.Header.Controls.Add(m);

            if (!string.IsNullOrEmpty(url))
            {
                m = new HtmlMeta();
                m.Attributes.Add("property", "og:url");
                m.Attributes.Add("Content", url);
                p.Header.Controls.Add(m);
            }
            if (!string.IsNullOrEmpty(images))
            {
                m = new HtmlMeta();
                m.Attributes.Add("property", "og:image");
                m.Attributes.Add("Content", images);
                p.Header.Controls.Add(m);
            }
            m = new HtmlMeta();
            m.Attributes.Add("property", "og:site_name");
            m.Attributes.Add("Content", System.Configuration.ConfigurationManager.AppSettings["WebDomain"]);
            p.Header.Controls.Add(m);

            if (!string.IsNullOrEmpty(sapo))
            {
                m = new HtmlMeta();
                m.Attributes.Add("property", "og:description");
                m.Content = HttpUtility.HtmlDecode(sapo);
                p.Header.Controls.Add(m);
            }
            #endregion
        }
    }
    public static class Lib
    {





        /// <summary>
        /// Chuyển đổi 1 giá trị sang kiểu Integer
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>Số kiểu Integer, nếu lỗi return int.MinValue</returns>
        public static int Object2Integer(object value)
        {
            int _result = 0;
            if (value == null) return _result;

            Int32.TryParse(value.ToString(), out _result);

            return _result;
        }

        /// <summary>
        /// Chuyển đổi 1 giá trị sang kiểu Long
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>Số kiểu Long, nếu lỗi return long.MinValue</returns>
        public static long Object2Long(object value)
        {
            long _result = 0;
            if (value == null)
                return _result;

            Int64.TryParse(value.ToString(), out _result);

            return _result;
        }

        /// <summary>
        /// Chuyển đổi 1 giá trị sang kiểu Double
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>Số kiểu Double, nếu lỗi return double.NaN</returns>
        public static double Object2Double(object value)
        {
            double _result = 0;

            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return _result;

            Double.TryParse(value.ToString(), out _result);

            return _result;
        }

        /// <summary>
        /// Chuyển đổi 1 giá trị sang kiểu float
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>Số kiểu float, nếu lỗi return float.NaN</returns>
        public static float Object2Float(object value)
        {
            if (null == value) return 0;
            try
            {
                return float.Parse(value.ToString());
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// Chuyển đổi 1 giá trị sang kiểu boolean
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>giá trị kiểu boolean, nếu lỗi return false</returns>
        public static bool Object2Boolean(object value)
        {
            if (null == value) return false;
            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Chuyển đổi 1 giá trị sang kiểu DateTime
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>Số kiểu DateTime, nếu lỗi return DateTime.MinValue</returns>
        public static DateTime Object2DateTime(object value)
        {
            if (null == value) return DateTime.MinValue;
            try
            {
                return Convert.ToDateTime(value);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        /// <summary>
        /// Chuyển đổi 1 xâu ngày tháng dạng dd/MM/yyyy sang ngày tháng
        /// </summary>
        /// <param name="value">Xâu nhập</param>
        /// <returns>Trả về kiểu DateTime cua ngày cần chuyển đổi (Nếu lỗi thì trả về DateTime.MinValue)</returns>
        public static DateTime String2Date(string value)
        {
            string temp = value;

            string date = temp.Substring(0, temp.IndexOf("/"));
            temp = temp.Substring(temp.IndexOf("/") + 1);
            string month = temp.Substring(0, temp.IndexOf("/"));
            string year = temp.Substring(temp.IndexOf("/") + 1);

            string[] months = new string[] { "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec" };
            try
            {
                return Convert.ToDateTime(date + " " + months[Convert.ToInt32(month) - 1] + " " + year);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        /// <summary>
        /// Lấy 1 xâu ngẫu nhiên
        /// </summary>
        /// <param name="length">Số lượng ký tự</param>
        /// <returns>Xâu ngẫu nhiên</returns>
        public static string GetRamdomString(int length)
        {
            string temp = "";
            string[] myAlphabet = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

            Random Rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                temp += myAlphabet[Rnd.Next(0, myAlphabet.Length - 1)];
            }
            return temp;
        }

        #region Chuyen doi xau dang unicode co dau sang dang khong dau
        private const string KoDauChars =
            "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";

        private const string uniChars =
            "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";

        /// <summary>
        /// Chuyển đổi 1 xâu từ dạng unicode có dấu sang dạng unicode không dấu
        /// </summary>
        /// <param name="s">xâu unicode có dấu</param>
        /// <returns>xâu unicode không dấu đã convert</returns>
        public static string UnicodeToKoDau(string s)
        {
            string retVal = String.Empty;
            s = s.Trim();
            int pos;
            for (int i = 0; i < s.Length; i++)
            {
                pos = uniChars.IndexOf(s[i].ToString());
                if (pos >= 0)
                    retVal += KoDauChars[pos];
                else
                    retVal += s[i];
            }
            return retVal;
        }

        /// <summary>
        /// Chuyển đổi 1 xâu từ dạng unicode có dấu sang dạng unicode không dấu và có gạch ngăn cách giữa mỗi từ
        /// </summary>
        /// <param name="s">xâu unicode có dấu</param>
        /// <returns>xâu unicode không dấu và có gạch ngăn cách giữa mỗi từ</returns>
        public static string UnicodeToKoDauAndGach(string s)
        {
            string strChar = "abcdefghiklmnopqrstxyzuvxw0123456789 ";
            //string retVal = UnicodeToKoDau(s);
            //s = s.Replace("-", " ");
            s = s.Replace("–", "");
            s = s.Replace("  ", " ");
            s = UnicodeToKoDau(s.ToLower().Trim());
            string sReturn = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (strChar.IndexOf(s[i]) > -1)
                {
                    if (s[i] != ' ')
                        sReturn += s[i];
                    else if (i > 0 && s[i - 1] != ' ' && s[i - 1] != '-')
                        sReturn += "-";
                }
            }

            return sReturn;
        }
        #endregion

        #region QueryStrings
        public static class QueryString
        {
            public static string PageName
            {
                get
                {
                    string filePath = HttpContext.Current.Request.FilePath;
                    string url = (filePath.IndexOf("?") > 0 ? filePath.Substring(0, filePath.IndexOf("?")) : filePath);
                    url = url.ToLower();
                    url = url.Replace("//", "/");
                    url = url.Replace(".aspx", "");
                    url = url.Replace("/", "");
                    return url;
                }
            }
            public static int Tab
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["tab"])) return 0;

                    return Lib.Object2Integer(HttpContext.Current.Request.QueryString["tab"]);
                }
            }
            public static string San
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["san"])) return "";

                    return HttpContext.Current.Request.QueryString["san"];
                }
            }
            public static string Date
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["date"])) return "";

                    return HttpContext.Current.Request.QueryString["date"];
                }
            }
            public static int CategoryID
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["Cat_ID"])) return 0;

                    return Lib.Object2Integer(HttpContext.Current.Request.QueryString["Cat_ID"]);
                }
            }
            public static int ColorID
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["Color_ID"])) return 0;

                    return Lib.Object2Integer(HttpContext.Current.Request.QueryString["Color_ID"]);
                }
            }
            public static int ParentCategory
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["Cat_ParentID"])) return 0;

                    return Lib.Object2Integer(HttpContext.Current.Request.QueryString["Cat_ParentID"]);
                }
            }
            public static string CategoryName
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["Cat_Name"])) return "-";

                    return HttpContext.Current.Request.QueryString["Cat_Name"];
                }
            }
            public static int PageIndex
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["PageIndex"])) return 0;

                    return Lib.Object2Integer(HttpContext.Current.Request.QueryString["PageIndex"]);
                }
            }
            public static long NewsID
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["News_ID"])) return 0;

                    return Lib.Object2Long(HttpContext.Current.Request.QueryString["News_ID"]);
                }
            }
            public static int AlbumID
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["AlbumID"])) return 0;

                    return Lib.Object2Integer(HttpContext.Current.Request.QueryString["AlbumID"]);
                }
            }
            public static int ProductID
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["News_ID"])) return 0;

                    return Lib.Object2Integer(HttpContext.Current.Request.QueryString["News_ID"]);
                }
            }
            public static int ViewByDate_Day
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["day"])) return 0;

                    return Lib.Object2Integer(HttpContext.Current.Request.QueryString["day"]);
                }
            }
            public static int ViewByDate_Month
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["month"])) return 0;

                    return Lib.Object2Integer(HttpContext.Current.Request.QueryString["month"]);
                }
            }
            public static int ViewByDate_Year
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["year"])) return 0;

                    return Lib.Object2Integer(HttpContext.Current.Request.QueryString["year"]);
                }
            }


            public static int EventID
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["EventID"])) return 0;

                    return Lib.Object2Integer(HttpContext.Current.Request.QueryString["EventID"]);
                }
            }
            public static string Event_Name
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["Event_Name"])) return "";

                    return HttpContext.Current.Request.QueryString["Event_Name"].ToString();
                }
            }
            public static string Symbol
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["Symbol"])) return "";

                    return HttpContext.Current.Request.QueryString["Symbol"].ToString();
                }
            }

            public static int ThreadID
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["ThreadID"])) return 0;

                    return Lib.Object2Integer(HttpContext.Current.Request.QueryString["ThreadID"]);
                }
            }
            public static int PageType
            {
                get
                {
                    if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["PageType"])) return 0;

                    return Lib.Object2Integer(HttpContext.Current.Request.QueryString["PageType"]);
                }
            }

        }
        #endregion






    }

}
