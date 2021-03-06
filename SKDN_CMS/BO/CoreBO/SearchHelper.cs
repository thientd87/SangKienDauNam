using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Web;
using System.Web.Security;

namespace ChannelVN.CoreBO
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
			for (int i = 0; i < _keys.Length; i++)
			{
				strResult += " AND " + _colum + " like N'%" + _keys[i] + "%'";
			}
			strResult = strResult.Substring(5, strResult.Length - 5);
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
