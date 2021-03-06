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
using Portal.API;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using Portal.BO.Editoral.Category;
using Sgml;

namespace Portal.GUI.Administrator.CategoryManager
{
	public partial class SetupLayout : System.Web.UI.Page
	{
		string GUIDirectory = Config.GetModulePhysicalPath(); //GUIDirectory = @"D:\Task\Copy of portal\GUI";

		string DragContainerIdFormat = "DragAndDropContainer_{0}"; // <ul id=DragAndDropContainer_1>
		string DragModuleIdFormat = "DragableModule_{0}${1}"; // <li id=DragableModule_1$frontend/tin tieu diem
		string VirtualSpace = "<li class=\"virtualElement\"><div>Bạn có thể kéo thả module vào đây</div></li>";
		int CountDragContainer = 0;
		int CountDragModule = 0;

		ModuleSettings moduleSettings;
		ModuleRuntimeSettings moduleRuntimeSettings;
		PortalDefinition.Module module;

		/// <summary>
		/// Check permission for objRole.isTongBienTap || objRole.isThuKyToaSoan
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Page_Init(object sender, EventArgs e)
		{
			// fix user right
			Portal.User.Security.MainSecurity objsecu = new Portal.User.Security.MainSecurity();
			Portal.User.Security.Role objRole = objsecu.GetRole(this.User.Identity.Name);
			if (!objRole.isTongBienTap && !objRole.isThuKyToaSoan)
			{
				this.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('Bạn không có quyền hoặc chưa đăng nhập'); window.location = '/login.aspx';</script>");
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				// bind layout for active tab
				if (!string.IsNullOrEmpty(Request.QueryString["tabref"]))
				{
					int catId = 0;
					int.TryParse(Request.QueryString["tabref"], out catId);
					if (catId == 0) { Visible = false; return; }

					string tab = CategoryHelper.GetTabReferenceByCatId(catId);
					if (string.IsNullOrEmpty(tab)) { Visible = false; return; }

					if (!string.IsNullOrEmpty(tab))
					{
						ViewState["TabReference"] = tab;
						PortalDefinition.Tab t = getTab((string)ViewState["TabReference"]);
						if (t != null) RenderTables(t.Columns, workarea);
					}

					// bind module drop down list
					LoadModule();
				}
			}
		}

		private void RenderTables(ArrayList columns, HtmlGenericControl workarea)
		{
			workarea.Controls.Clear();
			HtmlTable table = null;
			foreach (PortalDefinition.Column column in columns)
			{
				table = genTable(column);
				workarea.Controls.Add(table);
			}
		}

		HtmlTable genTable(PortalDefinition.Column column)
		{
			HtmlTable table = new HtmlTable();
			table.Style["width"] = "100%";

			HtmlTableRow trHeader = genHeaderTable(column);
			table.Controls.Add(trHeader);

			HtmlTableRow trContent = new HtmlTableRow();


			// render column inside table
			if (column.Columns.Count == 0)
			{
				trContent.Controls.Add(genColumn(column));
			}
			else
			{
				foreach (PortalDefinition.Column childColumn in column.Columns)
				{
					trContent.Controls.Add(genColumn(childColumn));
				}
			}
			table.Controls.Add(trContent);
			return table;
		}

		HtmlTableCell genColumn(PortalDefinition.Column column)
		{
			HtmlTableCell td = new HtmlTableCell();
			bindColumnProperties(td, column);
			td.Attributes["class"] = "content";

			// bind column control
			StringBuilder html = new StringBuilder();
			html.Append("<div style=\"clear:both; margin:5px\">");
			html.Append("<div style=\"float:left;\"><span>" + column.ColumnName + "</span></div>");
			html.Append("<div style=\"float:right\"><a href=\"#\" class=\"arrow addtable\" title=\"Thêm bảng vào trong cột\" onclick=\"addTable(this); return false;\"></a>");
			html.Append("<a href=\"#\" class=\"arrow edit\" title=\"Sửa thông tin cột\" onclick=\"ChangeColumnToEditMode(this); return false;\"></a>");
			html.Append("<a href=\"#\" class=\"arrow moveleft\" title=\"Chuyển cột sáng trái\" onclick=\"moveColumnLeft(this); return false;\"></a>");
			html.Append("<a href=\"#\" class=\"arrow moveright\" title=\"Chuyển cột sang phải\" onclick=\"moveColumnRight(this); return false;\"></a>");
			html.Append("<a href=\"#\" class=\"arrow remove\" title=\"Xóa cột này\" onclick=\"removeColumn(this); return false;\"></a>");
			html.Append("</div></div><br style=\"clear:both;\" />");
			td.InnerHtml = html.ToString();

			// render modules inside column
			HtmlGenericControl ul;
			if (column.ModuleList.Count == 0)
				ul = genDragContainer(CountDragContainer++, VirtualSpace);
			else
				ul = genDragContainer(CountDragContainer++, string.Empty);
			//
			td.Controls.Add(ul);
			foreach (PortalDefinition.Module module in column.ModuleList)
			{
				ul.Controls.Add(createModule(module));
			}

			// render table inside column
			foreach (PortalDefinition.Column childColumn in column.Columns)
			{
				td.Controls.Add(genTable(childColumn));
			}
			//
			return td;
		}


		void bindColumnProperties(HtmlTableCell cell, PortalDefinition.Column column)
		{
			cell.Attributes["_style"] = column.ColumnCustomStyle;
			cell.Attributes["level"] = column.ColumnLevel.ToString();
			cell.Attributes["name"] = column.ColumnName;
			cell.Attributes["editablecolumnwidth"] = column.EditableColumnWidth.ToString();
			cell.Attributes["ref"] = column.ColumnReference;
			cell.Style["width"] = getWidthForStyle(column.ColumnWidth);
			cell.Attributes["isdragable"] = column.IsDragable.ToString();
			cell.Attributes["valign"] = "top";

		}

		private HtmlGenericControl createModule(PortalDefinition.Module module)
		{
			HtmlGenericControl dragModule = new HtmlGenericControl("li");
			string moduleId = string.Format(DragModuleIdFormat, (CountDragModule++), module.type.Replace("/", "$"));
			dragModule.Attributes["id"] = moduleId;
			dragModule.Attributes["cacheduration"] = module.CacheTime.ToString();
			dragModule.Attributes["ref"] = module.reference;
			dragModule.Attributes["title"] = module.title;
			dragModule.Attributes["type"] = module.type;

			StringBuilder html = new StringBuilder();
			html.Append("<span>" + module.title + "</span>");
			html.Append(" | <a href=\"#\" onclick=\"ChangeModuleToEditMode(this); return false;\">Edit</a>");
			html.Append(" | <a href=\"#\" onclick=\"removeModule(this); return false;\">Remove</a>");

			dragModule.InnerHtml = html.ToString();

			return dragModule;
		}

		HtmlGenericControl genDragContainer(int containerIndex, string innerHTML)
		{
			HtmlGenericControl ul = new HtmlGenericControl("ul");
			ul.Attributes["id"] = string.Format(DragContainerIdFormat, containerIndex);
			ul.Attributes["class"] = "draglist";
			ul.InnerHtml = innerHTML;
			return ul;
		}

		string getWidthForStyle(string width)
		{
			if (string.IsNullOrEmpty(width)) return "auto";
			if (width.EndsWith("px") || width.EndsWith("%") || width.ToLower() == "auto")
				return width;
			else
				return width + "px";
		}

		HtmlTableRow genHeaderTable(PortalDefinition.Column column) // columns inside
		{
			// header row
			HtmlTableRow trHeader = new HtmlTableRow();
			HtmlTableCell td = new HtmlTableCell();
			td.Style["color"] = "white";
			td.Style["background-color"] = "#303030";
			if (column.Columns.Count > 1) td.Attributes["colspan"] = column.Columns.Count.ToString();

			string html = "<div style=\"clear:both; margin:5px;\"><div style=\"float:left;\"><span>" + column.ColumnName + "</span></div>";
			html += "<div style=\"float:right;\">";

			if (column.EditableColumnWidth)
				html += "<a class=\"arrow editablecolumnwidth_active\" href=\"#\" title=\"Bảng này cho phép sửa độ rộng cột, bấm vào để thay đổi\" onclick=\"tonggle_editablecolumnwidth(this); return false;\"></a>";
			else
				html += "<a class=\"arrow editablecolumnwidth_inactive\" href=\"#\" title=\"Bảng này không cho phép sửa độ rộng cột, bấm vào để thay đổi\" onclick=\"tonggle_editablecolumnwidth(this); return false;\"></a>";

			html += "<a class=\"arrow addcolumn\" href=\"#\" title=\"Thêm cột vào trong bảng\" onclick=\"addColumn(this); return false;\"></a>";
			html += "<a class=\"arrow edit\" href=\"#\" title=\"Sửa tên bảng\" onclick=\"editTableName(this); return false;\"></a>";
			html += "<a class=\"arrow moveup\" href=\"#\" title=\"Chuyển bảng lên trên\" onclick=\"moveTableUp(this); return false;\"></a>";
			html += "<a class=\"arrow movedown\" href=\"#\" title=\"Chuyển bảng xuống dưới\" onclick=\"moveTableDown(this); return false;\"></a>";
			html += "<a class=\"arrow moveleft\" href=\"#\" title=\"Chuyển bảng sang trái\" onclick=\"moveTableLeft(this); return false;\"></a>";
			html += "<a class=\"arrow moveright\" href=\"#\" title=\"Chuyển bảng sang phải\" onclick=\"moveTableRight(this); return false;\"></a>";
			html += "<a class=\"arrow remove\" href=\"#\" title=\"Xóa bảng này\" onclick=\"removeTable(this); return false;\"></a>";
			html += "</div></div><br style=\"clear:both;\" />";
			td.InnerHtml = html;

			bindColumnProperties(td, column);

			trHeader.Controls.Add(td);
			trHeader.Attributes["class"] = "header";
			return trHeader;
		}

		protected void btnBindModuleEditForm_Click(object sender, EventArgs e)
		{
			string tab = (string)ViewState["TabReference"];

			PortalDefinition pd = PortalDefinition.Load();
			PortalDefinition.Tab t = pd.GetTab(tab);

			string arg = Request.Form["pageArg"];
			string moduleRef = string.Empty;
			string moduleType = string.Empty;
			string columnRef = string.Empty;
			string title = string.Empty;

			if (arg.Split("$".ToCharArray()).Length == 4)
			{
				moduleRef = arg.Split("$".ToCharArray())[0];
				moduleType = arg.Split("$".ToCharArray())[1];
				title = arg.Split("$".ToCharArray())[2];
				columnRef = arg.Split("$".ToCharArray())[3];
			}
			else
				return;

			PortalDefinition.Column column = pd.GetColumn(columnRef);

			if (moduleRef == null || moduleRef == string.Empty || moduleRef.ToLower() == "null")
			{
				module = PortalDefinition.Module.Create();
				module.type = moduleType;
				module.title = title;
				moduleRef = module.reference;
			}
			else
			{
				module = new PortalDefinition.Module();
				module.type = moduleType;
				module.title = title;
				module.reference = moduleRef;
			}

			if (module != null)
			{
				module.LoadModuleSettings();
				module.LoadRuntimeProperties();

				moduleSettings = module.moduleSettings;
				moduleRuntimeSettings = module.moduleRuntimeSettings;

				lblModuleType.Text = moduleType;
				txtReference.Text = module.reference;


				rptRuntimeProperties.DataSource = module.GetRuntimePropertiesSource(true);
				rptRuntimeProperties.DataBind();
				upEditModuleForm.Update();
			}
		}

		protected void rptRuntimeProperties_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			{
				DropDownList drdAvaiableValues = e.Item.FindControl("drdAvaiableValues") as DropDownList;
				TextBox txtPropertyValue = e.Item.FindControl("txtPropertyValue") as TextBox;

				if (drdAvaiableValues != null && txtPropertyValue != null)
				{
					ArrayList properties = module.GetAvaiblePropertyValues(true, ((DataRowView)e.Item.DataItem)["Name"].ToString());
					ModulePropertyValue property;
					if (properties.Count > 0)
					{
						for (int i = 0; i < properties.Count; i++)
						{
							property = (ModulePropertyValue)properties[i];
							drdAvaiableValues.Items.Add(new ListItem(property.AvaiableKey, property.AvaiableValue));
						}
						try
						{
							drdAvaiableValues.SelectedValue = txtPropertyValue.Text;
						}
						catch { }
						txtPropertyValue.Visible = false;
						drdAvaiableValues.Visible = true;
					}
					else
					{
						txtPropertyValue.Visible = true;
						drdAvaiableValues.Visible = false;
					}

				}
			}
		}

		protected void btnSaveModule_Click(object sender, EventArgs e)
		{
			string tab = (string)ViewState["TabReference"];

			PortalDefinition.Module module;

			string arg = Request.Form["pageArg"];
			string moduleRef = string.Empty;
			string moduleType = string.Empty;
			string moduleTitle = string.Empty;
			string columnRef = string.Empty;

			if (arg.Split("$".ToCharArray()).Length == 4)
			{
				moduleRef = arg.Split("$".ToCharArray())[0];
				moduleType = arg.Split("$".ToCharArray())[1];
				moduleTitle = arg.Split("$".ToCharArray())[2];
				columnRef = arg.Split("$".ToCharArray())[3];
			}
			else
				return;

			if (string.IsNullOrEmpty(moduleRef)) return;

			module = new PortalDefinition.Module();
			module.reference = moduleRef;
			module.type = moduleType;

			if (module != null)
			{
				// Lưu các thông số khi thực thi của module
				module.LoadModuleSettings();
				module.LoadRuntimeProperties();
				for (int _intPropertyCount = 0; _intPropertyCount < rptRuntimeProperties.Items.Count; _intPropertyCount++)
				{
					HtmlInputHidden _hihPropertyName = rptRuntimeProperties.Items[_intPropertyCount].FindControl("lblPropertyName") as HtmlInputHidden;
					TextBox _txtPropertyValue = rptRuntimeProperties.Items[_intPropertyCount].FindControl("txtPropertyValue") as TextBox;
					DropDownList _drdAvaiableValues = rptRuntimeProperties.Items[_intPropertyCount].FindControl("drdAvaiableValues") as DropDownList;

					if (_hihPropertyName != null && _txtPropertyValue != null)
					{
						string _strPropertyValue = _txtPropertyValue.Visible ? _txtPropertyValue.Text : _drdAvaiableValues.SelectedValue;
						module.moduleRuntimeSettings.SetRuntimePropertyValue(true, _hihPropertyName.Value, _strPropertyValue);
					}
				}
				module.SaveRuntimeSettings();
			}
		}

		public PortalDefinition.Tab getTab(string tabRef)
		{
			PortalDefinition pd = PortalDefinition.Load();
			PortalDefinition.Tab t = pd.GetTab(tabRef);
			return t;
		}

		private void LoadModule()
		{
			// Get Module List
			string[] dirs = System.IO.Directory.GetDirectories(GUIDirectory);
			int idx = GUIDirectory.Length;
			ArrayList arr = new ArrayList();
			foreach (string dir in dirs)
				arr.Add(dir.Substring(idx));

			cboModuleFolder.DataSource = arr;
			cboModuleFolder.DataBind();
			cboModuleFolder.Items.Insert(0, new ListItem());

		}

		protected void cboModuleFolder_SelectedIndexChanged(object sender, EventArgs e)
		{
			string[] dirs = System.IO.Directory.GetDirectories(GUIDirectory + cboModuleFolder.SelectedValue);

			int idx = GUIDirectory.Length + cboModuleFolder.SelectedValue.Length + 1;
			ArrayList arr = new ArrayList();
			foreach (string dir in dirs)
				arr.Add(dir.Substring(idx));

			DropDownList2.DataSource = arr;
			DropDownList2.DataBind();
			DropDownList2.Items.Insert(0, new ListItem());

			UpdatePanel1.Update();
		}

		protected void btnSaveLayout_Click(object sender, EventArgs e)
		{
			string tab = (string)ViewState["TabReference"];

			string html = "<tables>" + HiddenField1.Value + "</tables>";
			// hotfix
			html = html.Replace("<br style=\"clear: both;\">", string.Empty);
			html = html.Replace("<BR style=\"CLEAR: both\">", string.Empty);


			XmlDocument layout = convertHTMLtoXML(html);
			if (layout != null)
			{
				PortalDefinition pd = PortalDefinition.Load();
				PortalDefinition.Tab t = pd.GetTab(tab);
				// remove all old column
				while (t.Columns.Count > 0)
					t.DeleteColumn(((PortalDefinition.Column)t.Columns[0]).ColumnReference);

				// add new column
				XmlNodeList htmlTables = layout.SelectNodes("tables/table");
				if (htmlTables.Count == 0) htmlTables = layout.SelectNodes("tables/TABLE"); // fix ie: UPPERCASE all tag
				PortalDefinition.Column newColumn;

				int countTable = 0;
				foreach (XmlNode htmlTable in htmlTables)
				{
					newColumn = PortalDefinition.Column.Create(t);
					saveTable(ref newColumn, htmlTable, countTable++);
					t.Columns.Add(newColumn);
				}
				pd.Save();
				RenderTables(t.Columns, workarea);
			}
		}

		void saveTable(ref PortalDefinition.Column column, XmlNode htmlTable, int level)
		{
			XmlNodeList xmlHeaderCells = htmlTable.SelectNodes("tbody/tr/td");
			if (xmlHeaderCells.Count == 0) xmlHeaderCells = htmlTable.SelectNodes("tbody/tr/td".ToUpper()); // fix ie: UPPERCASE all tag
			XmlNode xmlHeaderCell = xmlHeaderCells[0];

			column.ColumnLevel = level;

			string reference = xmlNodeToText(xmlHeaderCell.SelectSingleNode("@ref"));
			if (!string.IsNullOrEmpty(reference)) column.ColumnReference = reference;

			column.ColumnName = xmlNodeToText(xmlHeaderCell.SelectSingleNode("@name"));
			column.ColumnWidth = "100%";
			column.EditableColumnWidth = xmlNodeToText(xmlHeaderCell.SelectSingleNode("@editablecolumnwidth")).ToLower() == "true";
			column.IsDragable = xmlNodeToText(xmlHeaderCell.SelectSingleNode("@isdragable")).ToLower() == "true";

			XmlNodeList htmlColumns = htmlTable.SelectNodes("tbody/tr[position()=2]/td");
			if (htmlColumns.Count == 0) htmlColumns = htmlTable.SelectNodes("TBODY/TR[position()=2]/TD"); // fix ie: UPPERCASE all tag
			PortalDefinition.Column childColumn;
			foreach (XmlNode htmlColumn in htmlColumns)
			{
				childColumn = PortalDefinition.Column.Create(column);
				saveColumn(ref childColumn, htmlColumn, 0);
				column.Columns.Add(childColumn);
			}
		}
		void saveColumn(ref PortalDefinition.Column column, XmlNode htmlColumn, int level)
		{
			column.ColumnLevel = level;

			string reference = xmlNodeToText(htmlColumn.SelectSingleNode("@ref"));
			if (!string.IsNullOrEmpty(reference)) column.ColumnReference = reference;

			column.ColumnName = xmlNodeToText(htmlColumn.SelectSingleNode("@name"));
			column.ColumnCustomStyle = xmlNodeToText(htmlColumn.SelectSingleNode("@_style"));
			column.ColumnWidth = getWidthFromStyle(xmlNodeToText(htmlColumn.SelectSingleNode("@style")));
			column.IsDragable = xmlNodeToText(htmlColumn.SelectSingleNode("@isdragable")).ToLower() == "true";

			XmlNodeList htmlModules = htmlColumn.SelectNodes("ul/li");
			if (htmlModules.Count == 0) htmlModules = htmlColumn.SelectNodes("ul/li".ToUpper()); // fix ie: UPPERCASE all tag
			PortalDefinition.Module newModule;
			foreach (XmlNode htmlModule in htmlModules)
			{
				if (xmlNodeToText(htmlModule.SelectSingleNode("@class")) != "virtualElement")
				{
					newModule = PortalDefinition.Module.Create();
					saveModule(ref newModule, htmlModule);
					column.ModuleList.Add(newModule);
				}
			}

			// save tables
			XmlNodeList htmlTables = htmlColumn.SelectNodes("table");
			if (htmlTables.Count == 0) htmlTables = htmlColumn.SelectNodes("TABLE");
			int countTable = 0;
			PortalDefinition.Column childColumn;
			foreach (XmlNode htmlTable in htmlTables)
			{
				childColumn = PortalDefinition.Column.Create(column);
				saveTable(ref childColumn, htmlTable, countTable++);
				column.Columns.Add(childColumn);
			}
		}
		void saveModule(ref PortalDefinition.Module module, XmlNode htmlModule)
		{
			string reference = xmlNodeToText(htmlModule.SelectSingleNode("@ref"));
			if (!string.IsNullOrEmpty(reference)) module.reference = reference;

			module.title = xmlNodeToText(htmlModule.SelectSingleNode("@title"));
			int.TryParse(xmlNodeToText(htmlModule.SelectSingleNode("@cacheduration")), out module.CacheTime);
			module.type = xmlNodeToText(htmlModule.SelectSingleNode("@type"));

			PortalDefinition.ViewRole viewrole = new PortalDefinition.ViewRole();
			viewrole.name = "TATCA";
			module.roles.Add(viewrole);
		}
		string getWidthFromStyle(string style)
		{
			if (Regex.IsMatch(style, "width:(.*?);"))
			{
				return Regex.Match(style, "width:(.*?);").Groups[1].Value.Trim();
			}
			return string.Empty;
		}
		string xmlNodeToText(XmlNode node)
		{
			return node == null ? string.Empty : node.InnerXml;
		}


		// using Sgml, convert html document to xml document
		XmlDocument convertHTMLtoXML(string html)
		{
			try
			{
				//Console.Write("Converting HTML to XML...");
				XmlDocument docSites = new XmlDocument();
				try
				{
					SgmlReader xhtmlConverter = new SgmlReader();
					xhtmlConverter.InputStream = new System.IO.StringReader(html);
					xhtmlConverter.DocType = "text/html";
					docSites.Load(xhtmlConverter);
				}
				catch (Exception)
				{
					docSites.LoadXml(html);
				}
				//Console.WriteLine("OK");
				return docSites;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		protected void btnSignmeout_Click(object sender, EventArgs e)
		{
			HttpCookie cookie = Request.Cookies["PortalUser"];
			if (cookie != null)
			{
				cookie.Values["AC"] = "";
				cookie.Values["PW"] = "";
				DateTime dt = DateTime.Now;
				dt.AddDays(-1);
				cookie.Expires = dt;
				Response.Cookies.Add(cookie);
			}
			FormsAuthentication.SignOut();
            Response.Redirect("~/Login" + Config.CustomExtension);
		}
	}
}
