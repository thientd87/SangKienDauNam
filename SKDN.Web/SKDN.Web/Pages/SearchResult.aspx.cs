using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BO;

namespace SKDN.Web.Pages
{
    public partial class SearchResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string Key = Request.QueryString["key"];

                string strKeyword = Key.Replace('"', ' ');
                strKeyword = strKeyword.Replace("'", " ");
                string[] strKeys = strKeyword.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                string strWhere = getAndCond(strKeys);
                DataTable objTintuc = ProductHelper.SearchProductByNameAndCode(strWhere, 0, 30, 165);
                int Count = 0;
                if (objTintuc != null && objTintuc.Rows.Count > 0)
                {
                    Count = objTintuc.Rows.Count;

                    rptListProject.DataSource = objTintuc;
                    rptListProject.DataBind();
                }
               // ltrCatName.Text = "Có " + Count + " kết quả phù hợp với yêu cầu";
            }
        }
        public string getAndCond(string[] _keys)
        {
            if (_keys.Length == 0) return string.Empty;

            string strResult = "";
            for (int i = 0; i < _keys.Length; i++)
            {
                strResult += " and (P.ProductName  like N'%" + _keys[i] + "%'  or P.ProductName_En  like N'%" + _keys[i] + "%') ";
            }
            strResult = strResult.Substring(5, strResult.Length - 5);
            return strResult;
        }
    }
}