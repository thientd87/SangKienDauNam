using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;
using System.Text;
using Portal.API;

using System.Text.RegularExpressions;
using Portal.BO.Editoral.EditNews;
using Portal.Core.DAL;
using Portal.BO.Editoral.Newsedit;
using System.Collections.Generic;
using System.Threading;

namespace Portal.GUI.EditoralOffice.MainOffce.editnews
{
	public partial class _default : System.Web.UI.Page
	{

		protected long newsId = 0;

		protected void Page_Init(object sender, EventArgs e)
		{
			string strNewsId = Request.QueryString["newsId"];
			if (string.IsNullOrEmpty(strNewsId)) strNewsId = "0";
			newsId = Int64.Parse(strNewsId);

		}

		protected void Page_Load(object sender, EventArgs e)
		{

			if (!IsPostBack)
			{
				// list of object that can be add to editor
				bindDropdownlistModules();
				//js.Text = "<script>window.addEventListener?window.addEventListener('onload',loadContent,false):window.attachevent('onload',loadContent);</script>";
				//js.Text = "<script>addLoadEvent(loadContent); saveViewstate(); //__doPostBack('LinkButton1', '');</script>";
				NewsContent.AssetManagerWidth = "750";
				NewsContent.AssetManagerHeight = "530";

				NewsContent.AssetManager = "/AssetManager/assetmanager.asp?user=" + Page.User.Identity.Name;
			}

			if (!IsPostBack)
			{
				MainDB db = new MainDB();

				if (newsId != 0)
				{
					// title news
					object objTitleNews = db.SelectScalar("Select News_Title From News Where News_Id=" + newsId.ToString());
					if (objTitleNews != null) lblTitleNews.Text = objTitleNews.ToString();
				}
			}
		}

		protected void dtlListOfModules_ItemDataBound(object sender, DataListItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				LinkButton btnAddNewModule = e.Item.FindControl("btnAddNewModule") as LinkButton;
				if (btnAddNewModule != null)
				{
					//btnAddNewModule.Style.Add("background-image", "url(" + ((DataRowView)e.Item.DataItem)["Icon"].ToString() + ")");
					btnAddNewModule.Attributes["onclick"] = string.Format("editmodule_show('{0}', ''); document.getElementById('listOfModules').style.display='none'; return false;", ((DataRowView)e.Item.DataItem)["Path"].ToString());
				}
			}
		}

		private void bindDropdownlistModules()
		{
			string moduleconfig = "BlockList.xml";
			XmlDocument doc = new XmlDocument();
			doc.Load(Server.MapPath(moduleconfig));

			// load folders
			XmlNodeList folders = doc.SelectNodes("modules/folder/@path");
			string folderPath = string.Empty, folderFullpath = string.Empty;
			DirectoryInfo di;
			DirectoryInfo[] dis;

			DataTable tbl = new DataTable();
			tbl.Columns.Add(new DataColumn("Name"));
			tbl.Columns.Add(new DataColumn("Path"));
			tbl.Columns.Add(new DataColumn("Icon"));
			DataRow row;
			ltrStyleSheet.Text = string.Empty;
			foreach (XmlNode folder in folders)
			{
				folderPath = folder.InnerText;
				if (folderPath.EndsWith("/")) folderPath.Remove(folderPath.Length - 1);

				folderFullpath = Server.MapPath(folderPath);
				di = new DirectoryInfo(folderFullpath);
				dis = di.GetDirectories();
				for (int i = 0; i < dis.Length; i++)
				{
					row = tbl.NewRow();
					row[0] = getVietnameseName(dis[i].FullName);
					row[1] = folderPath.Substring(folderPath.IndexOf("/GUI/") + 5) + "/" + dis[i].Name;
					addStyleSheetOfModule(row[1].ToString());
					row[2] = getIconURL(dis[i].FullName);
					tbl.Rows.Add(row);
				}
			}
			// load files
			XmlNodeList files = doc.SelectNodes("modules/file/@path");
			for (int i = 0; i < files.Count; i++)
			{
				row = tbl.NewRow();
				//row[0] = files[i].InnerText.Substring(files[i].InnerText.LastIndexOf("/") + 1);
				row[0] = getVietnameseName(Server.MapPath(files[i].InnerText));
				row[1] = files[i].InnerText.Substring(files[i].InnerText.IndexOf("~/GUI/") + 6);
				addStyleSheetOfModule(row[1].ToString());
				row[2] = getIconURL(Server.MapPath(files[i].InnerText));
				tbl.Rows.Add(row);
			}
			dtlListOfModules.DataSource = tbl;
			dtlListOfModules.DataBind();
		}

		private string getIconURL(string fullPath)
		{
			if (!fullPath.EndsWith("\\")) fullPath += "\\";
			string url = "/GUI/EditoralOffice/MainOffce/editnews/images/Pie Chart.png";
			if (File.Exists(fullPath + "icon.jpg"))
			{
				url = fullPath + "icon.jpg";
				url = url.Substring(url.IndexOf("\\GUI\\")).Replace("\\", "/");
			}
			return url;
		}

		private string getVietnameseName(string fullPath)
		{
			if (fullPath.EndsWith("\\")) fullPath = fullPath.Substring(0, fullPath.Length - 1);
			string name = fullPath.Substring(fullPath.LastIndexOf("\\") + 1);
			if (File.Exists(fullPath + "\\ModuleSettings.config"))
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(fullPath + "\\ModuleSettings.config");
				XmlNode nameNode = doc.SelectSingleNode("module/name");
				if (nameNode != null) name = nameNode.InnerText;
			}
			return name;
		}

		protected void btnReload_Click(object sender, EventArgs e)
		{
			string viewstate = customViewstate.Value, innerHTML = "@_@_@";

			Literal ltr1 = new Literal(), ltr2 = new Literal();

			ltr1.Text = viewstate.Substring(0, viewstate.IndexOf(innerHTML));
			ltr2.Text = viewstate.Substring(viewstate.IndexOf(innerHTML) + innerHTML.Length);

			panel.Controls.Add(ltr1);
			panel.Controls.Add(genModule(customArg.Value, customArg2.Value));
			panel.Controls.Add(ltr2);

			// load css

			Hashtable cssDirectory = new Hashtable();

			MatchCollection matchs = Regex.Matches(customViewstate.Value, "_type=\"(.*?)\"");

			ltrStyleSheet.Text = string.Empty;
			foreach (Match match in matchs)
			{
				if (!cssDirectory.Contains(match.Groups[1].Value))
				{
					cssDirectory.Add(match.Groups[1].Value, match.Groups[1].Value);

					addStyleSheetOfModule(match.Groups[1].Value);
				}
			}
		}

		private void addStyleSheetOfModule(string moduleType)
		{
			DirectoryInfo d = new DirectoryInfo(Server.MapPath("~/GUI/" + moduleType));

			// style
			FileInfo[] files = d.GetFiles("*.css");
			foreach (FileInfo file in files)
			{
				ltrStyleSheet.Text += string.Format("<link rel=\"stylesheet\" type=\"text/css\" href=\"/GUI/{0}\" />", moduleType + "/" + file.Name) + Environment.NewLine;
			}

			// script
			files = d.GetFiles("*.js");
			foreach (FileInfo file in files)
			{
				ltrStyleSheet.Text += string.Format("<script type=\"text/javascript\" src=\"/GUI/{0}\"></script>", moduleType + "/" + file.Name) + Environment.NewLine;
			}
		}

		protected void btnReload_Click2(object sender, EventArgs e)
		{
			string viewstate = customViewstate.Value;

			Literal ltr1 = new Literal();

			ltr1.Text = viewstate;

			panel.Controls.Add(ltr1);

			// load css

			Hashtable cssDirectory = new Hashtable();

			MatchCollection matchs = Regex.Matches(viewstate, "_type=\"(.*?)\"");

			ltrStyleSheet.Text = string.Empty;
			foreach (Match match in matchs)
			{
				if (!cssDirectory.Contains(match.Groups[1].Value))
				{
					cssDirectory.Add(match.Groups[1].Value, match.Groups[1].Value);

					DirectoryInfo d = new DirectoryInfo(Server.MapPath("~/GUI/" + match.Groups[1].Value));
					FileInfo[] files = d.GetFiles("*.css");
					foreach (FileInfo file in files)
					{
						ltrStyleSheet.Text += string.Format("<link rel=\"stylesheet\" type=\"text/css\" href=\"/GUI/{0}\" />", match.Groups[1].Value + "/" + file.Name) + Environment.NewLine;
					}
				}
			}

			js.Text = "<script>onunload = function() { if (opener) { opener.hideBG(); } }</script>";
		}

		private Control genModule(string type, string reference)
		{
			Block block = new Block(reference, type);
			// presentation module
			Module moduleControl2 = (Module)LoadControl(block.ObjectVirtualPath);
			// Khởi tạo nội dung Module
			moduleControl2.InitModule(null,
										 block.ObjectReference,
										 block.ObjectType,
										 block.ObjectVirtualPath,
										 true);

			return moduleControl2;
		}

	}
}
