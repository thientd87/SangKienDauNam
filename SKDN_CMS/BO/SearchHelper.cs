using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DFISYS.GUI.Administrator.CategoryManager;

namespace DFISYS.BO
{
    public class SearchHelper
    {
        public SearchHelper() { }
        /// <summary>
        /// Hàm lấy phép toán And của một cột nào đó để tìm kiếm trong mảng keys
        /// </summary>
        /// <param name="_colum"></param>
        /// <param name="_keys"></param>
        /// <returns></returns>
        public string getAndCond(string _colum, string[] _keys)
        {
			if (_keys.Length == 0) return string.Empty;

            string strResult = "";
            List<string> columnArray = new List<string>();
            if (_colum.IndexOf(',') != -1)
                columnArray = _colum.Split(',').ToList();
            else
                columnArray.Add(_colum);

            foreach (var column in columnArray)
            {
                for (int i = 0; i < _keys.Length; i++)
                {
                    strResult +=  column + " like N'%" + _keys[i] + "%'" + " OR " ;
                }
            }
            strResult = " (" + strResult.Substring(0,strResult.Length-4) + ")";
            return strResult;
        }
        /// <summary>
        /// Hàm đưa ra kết quả phép toán or
        /// </summary>
        /// <param name="_colum"></param>
        /// <param name="_keys"></param>
        /// <returns></returns>
        public string getOrCond(string _colum, string[] _keys)
        {
            string strResult = "";
            for (int i = 0; i < _keys.Length; i++)
            {
                strResult += " OR " + _colum + " like N'%" + _keys[i] + "%'";
            }
            strResult = strResult.Substring(4, strResult.Length - 4);
            return strResult;
        }
    }
}
