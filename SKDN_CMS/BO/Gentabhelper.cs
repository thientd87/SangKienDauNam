using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DFISYS.Core.DAL;
using System.Collections;
using System.IO;
using System.Xml;
namespace DFISYS.BO
{
	public class Gentabhelper
	{
		public Gentabhelper()
		{
		}
		/// <summary>
		/// Thủ tục cập nhật lại mã tham chiếu của thẻ tương ứng với đề mục đã sửa
		/// Thủ tục này được gọi khi người sử dụng thay đổi đường dẫn hiển thị trên URL của một đề mục nào đó.
		/// </summary>
		/// <param name="_strOldTabRef">Mã tham chiếu cũ, dùng để tìm ra thẻ hiện thời ứng với đề mục cần sửa</param>
		/// <param name="_strNewTabRef">Mã</param>
		/// <param name="_strNewTabTitle"></param>
		public void SyncCategoryTab(string _strOldTabRef, string _strNewTabRef, string _strNewTabTitle, string _EditionRef, string _ParentRef, string _NewRef, string _NewTabTitle)
		{
			PortalDefinition _objPortal = PortalDefinition.Load();
			PortalDefinition.Tab _objCurrentCategoryTab = _objPortal.GetTab(_strOldTabRef);

			if (_objCurrentCategoryTab != null)
			{
				_objCurrentCategoryTab.title = _strNewTabTitle;
				_objCurrentCategoryTab.reference = _strNewTabRef;
				_objPortal.Save();
			}
			else
			{
				//neu truong hop khong ton tai tab thi tao tab do ra
				AddCategoryTab(_EditionRef, _ParentRef, _NewRef, _NewTabTitle);
			}

		}
		/// <summary>
		/// Ham thuc hien Them mot tab moi khi them mot category. Tabref duoc tinh va chuyen theo dang
		/// Edition.CatParent.Subcat
		/// </summary>
		/// <param name="_EditionRef">Thong tin ve edition ref</param>
		/// <param name="_ParentRef">Thong tin ve catparent reFt</param>
		/// <param name="_NewRef">Thong tin ve ref cua cat moi</param>
		/// <param name="_NewTabTitle">Tieu de cua tab</param>
		public void AddCategoryTab(string _EditionRef, string _ParentRef, string _NewRef, string _NewTabTitle)
		{
			PortalDefinition _objPortal = PortalDefinition.Load();
			string strCurrTabRef = _EditionRef;
			if (_ParentRef != "")
				strCurrTabRef += "." + _ParentRef;
			PortalDefinition.Tab _objCurrentCategoryTab = _objPortal.GetTab(strCurrTabRef);

			if (_objCurrentCategoryTab == null)
			{

				PortalDefinition.Tab t = PortalDefinition.Tab.Create();
				t.reference = strCurrTabRef;
				t.title = strCurrTabRef;

				_objPortal.tabs.Add(t);
				_objCurrentCategoryTab = _objPortal.GetTab(strCurrTabRef);

			}

			if (_objCurrentCategoryTab != null)
			{
				PortalDefinition.Tab _newtab = PortalDefinition.Tab.Create(_NewRef);
				PortalDefinition.ViewRole _objViewRole = new PortalDefinition.ViewRole();
				_objViewRole.name = DFISYS.API.Config.EveryoneRoles;
				_newtab.roles.Add(_objViewRole);
				_newtab.title = _NewTabTitle;
				_newtab.reference = strCurrTabRef + "." + _NewRef;//EditionRef+"."+_ParentRef 
				_objCurrentCategoryTab.tabs.Add(_newtab);
				_objPortal.Save();
			}
		}

		/// <summary>
		/// Add new tab to Portal.config [bacth, 10:11 AM 5/26/2008]
		/// </summary>
		/// <param name="tabRef">new tab reference</param>
		/// <param name="parentTabRef">parent tab reference</param>
		/// <param name="title">new tab title</param>
		/// <param name="cloneTabRef">layout tab copied</param>
		/// <returns></returns>
		public static bool AddNewTab(string tabRef, string parentTabRef, string title, string cloneTabRef)
		{
			//string config1 = HttpContext.Current.Server.MapPath("~/settings/Portal.config"); // current application
			//string config2 = @"D:\shared\Portal.config"; // synchronous other file

			PortalDefinition portal = PortalDefinition.Load();
			PortalDefinition.Tab newTab = portal.GetTab(tabRef);
			PortalDefinition.Tab parentTab = portal.GetTab(parentTabRef);
			if (newTab == null)
			{
				// add new tab
				newTab = PortalDefinition.Tab.Create();
				newTab.reference = tabRef;
				newTab.title = title;
				parentTab.tabs.Add(newTab);
				// tab roles
				PortalDefinition.ViewRole role = new PortalDefinition.ViewRole();
				role.name = DFISYS.API.Config.EveryoneRoles;
				newTab.roles.Add(role);
				// tab layout
				if (!string.IsNullOrEmpty(cloneTabRef))
				{
					PortalDefinition.Tab cloneTab = portal.GetTab(cloneTabRef);
					if (cloneTab != null)
					{
						newTab.Columns = cloneTab.CloneColumns();
						foreach (PortalDefinition.Column c in newTab.Columns) getNewRef(c);
					}
				}
			}
			portal.Save();
			return true;
		}
		private static void getNewRef(PortalDefinition.Column column)
		{
			PortalDefinition portal = PortalDefinition.Load();
			string newRef = string.Empty;
			foreach (PortalDefinition.Module m in column.ModuleList)
			{
				newRef = Guid.NewGuid().ToString();
				while (PortalDefinition.Module.IsExist(newRef)) newRef = Guid.NewGuid().ToString();
				m.reference = newRef;
			}
			foreach (PortalDefinition.Column c in column.Columns) getNewRef(c);


		}
	}
}
