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
using System.Text;
using System.IO;
using System.Threading;
using DFISYS.BO;

namespace DFISYS.GUI.EditoralOffice.MainOffce.FileManager
{
	public partial class Default : System.Web.UI.Page
	{
		private int PageSize = 20;

		protected string HostName = ConfigurationManager.AppSettings["ImageUrl"];
        protected int CountFile = 0;

		protected void Page_Init(object sender, EventArgs e)
		{
			if (HostName.EndsWith("/")) HostName = HostName.Substring(0, HostName.Length - 1);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string userName = ChannelUsers.GetUserName();

				if (!string.IsNullOrEmpty(userName))
				{
					string toDayFolder = userName + string.Format("{0:/yyyy/MM}", DateTime.Now);

					if (!Directory.Exists(Server.MapPath("~/Images/Uploaded/" + toDayFolder))) Directory.CreateDirectory(Server.MapPath("~/Images/Uploaded/" + toDayFolder));

					if (!Directory.Exists(Server.MapPath("~/Images/Uploaded/Common"))) Directory.CreateDirectory(Server.MapPath("~/Images/Uploaded/Common"));
                    
					// bind treeview
					ltrFolder.Text = getFolder();
                    ltrFolder.Text += getFolder(Server.MapPath("~/Images/Uploaded/Common"));//string.Format("<li>{0}</li>", buildNode("Common", "Common"));

					// bind gridview
					string startup = string.Empty;
					DataTable tbl = null;
					if (Request.QueryString["fl"] != null)
					{
						try
						{
							startup = Crypto.DecryptFromHTML(Request.QueryString["fl"]);
							if (startup.ToLower().Equals("common"))
								tbl = getTableData(Request.QueryString["fl"], string.Empty, 0);
							else if (Directory.Exists(Server.MapPath("~/Images/Uploaded/" + userName + startup)))
								tbl = getTableData(Crypto.EncryptForHTML(userName + startup), string.Empty, 0);
						}
						catch
						{
							tbl = getTableData(Crypto.EncryptForHTML(toDayFolder), string.Empty, 0);
						}

					}
					else if (Request.Cookies["fl"] != null)
					{
						try
						{
							startup = Crypto.DecryptFromHTML(Request.Cookies["fl"].Value);
							if (startup.ToLower().Equals("common"))
								tbl = getTableData(Request.Cookies["fl"].Value, string.Empty, 0);
							else if (Directory.Exists(Server.MapPath("~/Images/Uploaded/" + userName + startup)))
								tbl = getTableData(Crypto.EncryptForHTML(userName + startup), string.Empty, 0);
						}
						catch
						{
							tbl = getTableData(Crypto.EncryptForHTML(toDayFolder), string.Empty, 0);
						}
					}
					else
						tbl = getTableData(Crypto.EncryptForHTML(toDayFolder), string.Empty, 0);


					Repeater1.DataSource = tbl;
					Repeater1.DataBind();
				}
			}
		}



		protected void rptFilesOfFolder_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			//if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
			//{
			//    Literal ltrItemSize = e.Item.FindControl("ltrItemSize") as Literal;

			//}
		}

		private void WriteCookie(string path)
		{
			HttpCookie myCookie = new HttpCookie("fl");
			DateTime now = DateTime.Now;

			string userName = ChannelUsers.GetUserName();

			if (path.Contains(userName))
				path = path.Substring(path.IndexOf(userName) + userName.Length);
			else
			{
				path = "Common";
			}

			// Set the cookie value.
			myCookie.Value = Crypto.EncryptForHTML(path);
			// Set the cookie expiration date.
			myCookie.Expires = now.AddMonths(1);

			// Add the cookie.
			Response.Cookies.Add(myCookie);
		}

		private string getFolder(string folderPath)
		{
			string folderName = folderPath.Substring(folderPath.LastIndexOf("\\") + 1);
			string virtualPath = folderPath.Substring(folderPath.IndexOf("\\Images\\Uploaded\\") + "\\Images\\Uploaded\\".Length).Replace("\\", "/");

			StringBuilder htmlCode = new StringBuilder();
			htmlCode.Append(string.Format("<li>{0}<br style=\"clear: both;\" /><ul class=\"treeview\">", buildNode(folderName, virtualPath)));

			DirectoryInfo folder = new DirectoryInfo(folderPath);
			DirectoryInfo[] subFolders = folder.GetDirectories();
			foreach (DirectoryInfo subFolder in subFolders)
				if (subFolder.Name.ToLower() != "thumbnails")
					htmlCode.Append(getFolder(subFolder.FullName));

			htmlCode.Append("</ul></li>");
			return htmlCode.ToString();
		}
		private string getFolder()
		{
			string userName = ChannelUsers.GetUserName();
			return getFolder(Server.MapPath("~/Images/Uploaded/" + userName));
		}

		private string buildNode(string folderName, string folderPath)
		{
			return string.Format("<a href=\"#\" class=\"tong_icon\" onclick=\"tonggleColspan(this)\"><img src=\"/images/lines/minus.gif\" alt=\"Thu nhỏ\" /></a> <a  class=\"folderTreeView\" _path=\"{0}\" href=\"javascript:browseFolder('{0}')\">{1}</a>", Crypto.EncryptForHTML(folderPath), folderName);
		}

		protected void btnBrowseFolder_Click(object sender, EventArgs e)
		{
			Repeater1.DataSource = getTableData(postBackArg.Value, txtKeyword.Text.Trim(), 0);
			Repeater1.DataBind();
			UpdatePanel1.Update();
		}

		DataTable getTableData(string path, string searchPattern, int pageIndex)
		{
			int startIndex = pageIndex * PageSize;
			int endIndex = (pageIndex + 1) * PageSize - 1;
			int count = 0;

			postBackArg2.Value = path;
			postBackArg.Value = path;
			js.Text = "<script>var treeview_CurrentNodePath = '" + path + "';</script>";

			path = Crypto.DecryptFromHTML(path);
			WriteCookie(path);

			DataTable table = new DataTable();
			DataRow row;


			table.Columns.Add("Name");
			table.Columns.Add("FullPath");
			table.Columns.Add("Type");
			table.Columns.Add("FileSize");

			path = Server.MapPath("~/Images/Uploaded/" + path);
			DirectoryInfo folder = new DirectoryInfo(path);

			DirectoryInfo[] subFolders = folder.GetDirectories("*" + searchPattern + "*", SearchOption.TopDirectoryOnly);
			foreach (DirectoryInfo subFolder in subFolders)
			{
				if (subFolder.Name.ToLower() != "thumbnails" && count >= startIndex && count <= endIndex)
				{
					row = table.NewRow();
					row[0] = subFolder.Name;
					row[1] = Crypto.EncryptForHTML(subFolder.FullName.Substring(subFolder.FullName.IndexOf("\\Images\\Uploaded\\") + "\\Images\\Uploaded\\".Length).Replace("\\", "/"));
					row[2] = "folder";
					row[3] = string.Empty;
					table.Rows.Add(row);
				}
				count++;
			}

			FileInfo[] files = folder.GetFiles("*" + searchPattern + "*", SearchOption.TopDirectoryOnly);

			Array.Sort(files, 0, files.Length, new FileSort());
			Array.Reverse(files);

			foreach (FileInfo file in files)
			{
				if (count >= startIndex && count <= endIndex)
				{
					row = table.NewRow();
					row[0] = file.Name;
					row[1] = file.FullName.Substring(file.FullName.IndexOf("\\Images\\Uploaded\\") + "\\Images\\Uploaded\\".Length).Replace("\\", "/");
					row[2] = file.Extension.Substring(1).ToLower();
					row[3] = string.Format("{0:0.00}", (file.Length / (double)1024)) + " KB";
					table.Rows.Add(row);
				}
				count++;
			}

			bindPaging(subFolders.Length + files.Length, pageIndex);

            CountFile = count;
            //Request.Form["hidCountFile"] = count.ToString();
			return table;
		}

		private void bindPaging(int itemCount, int pageIndex)
		{
			ltrPaging.Text = string.Empty;
			if (itemCount == 0) return;

			int pageCount = (int)Math.Floor((double)((itemCount - 1) / PageSize)) + 1;
			if (pageCount == 1) return;

			StringBuilder strPage = new StringBuilder();
			strPage.Append("Trang ");
			for (int i = 1; i <= pageCount; i++)
			{
				if (i == (pageIndex + 1))
					strPage.Append(string.Format("<span>{0}</span>", i));
				else
					strPage.Append(string.Format("<a href=\"javascript:grid_paging({0})\">{1}</a>", i - 1, i));
			}
			ltrPaging.Text = strPage.ToString();
		}

		protected void btnNewFolder_Click(object sender, EventArgs e)
		{
			string path = Server.MapPath("~/Images/Uploaded/" + Crypto.DecryptFromHTML(postBackArg.Value));
			string newFolderName = UnicodeUtility.UnicodeToKoDauAndGach(HttpUtility.HtmlDecode(postBackArg2.Value));
			if (!Directory.Exists(path + "\\" + newFolderName))
			{
				Directory.CreateDirectory(path + "\\" + newFolderName);
				// bind treeview
				ltrFolder.Text = getFolder();
				ltrFolder.Text += string.Format("<li>{0}</li>", buildNode("Common", "Common"));
				// bind gridview
				Repeater1.DataSource = getTableData(postBackArg.Value, txtKeyword.Text.Trim(), 0);
				Repeater1.DataBind();
			}
			else
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('Thư mục đã tồn tại');</script>");
		}

		protected void btnUploadFile_Click(object sender, EventArgs e)
		{
            //if (string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name)) return;

            //string targetFolder = Server.MapPath("~/Images/Uploaded/" + Crypto.DecryptFromHTML(postBackArg.Value));

            //if (fu1.HasFile) UploadFile(fu1, targetFolder);
            //if (fu2.HasFile) UploadFile(fu2, targetFolder);
            //if (fu3.HasFile) UploadFile(fu3, targetFolder);
            //if (fu4.HasFile) UploadFile(fu4, targetFolder);
            //if (fu5.HasFile) UploadFile(fu5, targetFolder);

            //if (fu6.HasFile) UploadFile(fu6, targetFolder);
            //if (fu7.HasFile) UploadFile(fu7, targetFolder);
            //if (fu8.HasFile) UploadFile(fu8, targetFolder);
            //if (fu9.HasFile) UploadFile(fu9, targetFolder);
            //if (fu10.HasFile) UploadFile(fu10, targetFolder);

            //Repeater1.DataSource = getTableData(postBackArg.Value, txtKeyword.Text.Trim(), 0);
            //Repeater1.DataBind();
		}

		private void UploadFile(FileUpload fu, string targetFolder)
		{
			if (!targetFolder.EndsWith("\\")) targetFolder += "\\";

			string contentType = fu.PostedFile.ContentType;
			if (contentType == "application/octet-stream")
			{
				if (fu.FileName.IndexOf(".flv") == -1)
				{
					ClientScript.RegisterClientScriptBlock(GetType(), "alert", "<script>alert('" + fu.FileName + " không hợp lệ');</script>");
					return;
				}
			}
			else
				if (contentType.IndexOf("image") == -1 && contentType.IndexOf("audio") == -1 && contentType.IndexOf("video") == -1 && contentType.IndexOf("flash") == -1)
				{
					ClientScript.RegisterClientScriptBlock(GetType(), "alert", "<script>alert('" + fu.FileName + " không hợp lệ');</script>");
					return;
				}

			if (fu.FileName.IndexOf(".") == -1) return;
			string extension = fu.FileName.Substring(fu.FileName.IndexOf(".") + 1);
			string name = UnicodeUtility.UnicodeToKoDauAndGach(HttpUtility.HtmlDecode(fu.FileName.Substring(0, fu.FileName.IndexOf("."))));

			int i = 2;
			if (File.Exists(targetFolder + fu.FileName))
			{
				while (File.Exists(targetFolder + name + i + "." + extension)) i++;
				fu.SaveAs(targetFolder + name + i + "." + extension);
			}
			else
				fu.SaveAs(targetFolder + fu.FileName);

		}

		protected void btnGrid_DeleteItem_Click(object sender, EventArgs e)
		{
			if (postBackArg2.Value.ToLower() != "folder")
			{
				string path = Server.MapPath("~/Images/Uploaded/" + postBackArg.Value);
				if (File.Exists(path))
				{
					File.Delete(path);
					Repeater1.DataSource = getTableData(Crypto.EncryptForHTML(postBackArg.Value.Substring(0, postBackArg.Value.LastIndexOf("/"))), txtKeyword.Text.Trim(), 0);
					Repeater1.DataBind();
					UpdatePanel1.Update();
				}
			}
		}

		protected void btnPaste_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(postBackArg3.Value))
			{
				string[] files = postBackArg3.Value.Split("\t".ToCharArray());
				int i = 0, count = files.Length;
				string fileName, currentFolder = Server.MapPath("~/Images/Uploaded/" + Crypto.DecryptFromHTML(postBackArg.Value));
				for (i = 0; i < count; i++)
				{
					fileName = files.GetValue(i).ToString();
					fileName = fileName.Substring(fileName.LastIndexOf("/") + 1);
					if (!File.Exists(currentFolder + "\\" + fileName)) File.Copy(Server.MapPath("~/Images/Uploaded/" + files.GetValue(i)), currentFolder + "\\" + fileName);
				}
				if (count >= 1)
				{
					Repeater1.DataSource = getTableData(postBackArg.Value, string.Empty, 0);
					Repeater1.DataBind();
					UpdatePanel1.Update();
				}
			}
		}

		protected void btnDeleteFolder_Click(object sender, EventArgs e)
		{
			postBackArg.Value = Crypto.DecryptFromHTML(postBackArg.Value);
			if (postBackArg.Value != HttpContext.Current.User.Identity.Name && postBackArg.Value != "Common")
			{
				string path = Server.MapPath("~/Images/Uploaded/" + HttpUtility.HtmlDecode(postBackArg.Value));
				if (Directory.Exists(path))
				{
					DirectoryInfo di = new DirectoryInfo(path);
					string parent = di.Parent.FullName;
					Directory.Delete(path, true);

					// bind treeview
					ltrFolder.Text = getFolder();
					ltrFolder.Text += string.Format("<li>{0}</li>", buildNode("Common", "Common"));
					// bind gridview
					Repeater1.DataSource = getTableData(Crypto.EncryptForHTML(postBackArg.Value.Substring(0, postBackArg.Value.LastIndexOf("/"))), txtKeyword.Text.Trim(), 0);
					Repeater1.DataBind();
				}
			}
			else
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "<script>alert('Thư mục này không xóa được');</script>");
		}

		protected void btnSearch_Click(object sender, ImageClickEventArgs e)
		{
			Repeater1.DataSource = getTableData(postBackArg.Value, txtKeyword.Text.Trim(), 0);
			Repeater1.DataBind();
			UpdatePanel1.Update();
		}

		protected void btnDeleteMultiItem_Click(object sender, EventArgs e)
		{
			string[] args = postBackArg.Value.Split(",".ToCharArray());
			string arg = string.Empty;
			for (int i = 0; i < args.Length; i++)
			{
				arg = Server.MapPath("~/Images/Uploaded/" + args.GetValue(i));
				if (Directory.Exists(arg)) Directory.Delete(arg, true);
				else if (File.Exists(arg)) File.Delete(arg);

			}
			postBackArg.Value = postBackArg2.Value;
			Repeater1.DataSource = getTableData(postBackArg2.Value, txtKeyword.Text.Trim(), 0);
			Repeater1.DataBind();
			UpdatePanel1.Update();
		}

		protected void btnSaveToShareFolder_Click(object sender, EventArgs e)
		{
			string[] paths = postBackArg.Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

			string path = string.Empty, sharePath = string.Empty, js_arrPath = string.Empty, shareURL = string.Empty;

			js_arrPath = "var arrImagesURL = new Array();" + Environment.NewLine;

			for (int i = 0; i < paths.Length; i++)
			{
				path = Server.MapPath("~/Images/Uploaded/" + paths.GetValue(i));
				if (File.Exists(path))
				{
					string newsId = Session["newsid"] == null ? string.Empty : Session["newsid"].ToString();
					if (string.IsNullOrEmpty(newsId))
						newsId = DateTime.Now.Year + "" + DateTime.Now.Month + DateTime.Now.Day + "" + DateTime.Now.Hour + "" + DateTime.Now.Minute + "" + DateTime.Now.Second + DateTime.Now.Millisecond;
					Session["newsid"] = newsId;

					if (!string.IsNullOrEmpty(Request.QueryString["df"]))
					{
						sharePath = Server.MapPath(Request.QueryString["df"]);
					}
					else
					{
						sharePath = Server.MapPath("~/Images/Uploaded/Share/" + DateTime.Now.ToString("yyyy/MM/dd") );
					}
					if (!Directory.Exists(sharePath)) Directory.CreateDirectory(sharePath);
					sharePath += "\\" + path.Substring(path.LastIndexOf("\\") + 1);
					shareURL = HostName + sharePath.Substring(sharePath.IndexOf("Images\\Uploaded") - 1).Replace("\\", "/");

					if (!File.Exists(sharePath)) 
                        File.Copy(path, sharePath);
                    else
                    {
                        sharePath = Server.MapPath("~/Images/Uploaded/Share/" + DateTime.Now.ToString("yyyy/MM/dd"));
                        if (!Directory.Exists(sharePath)) Directory.CreateDirectory(sharePath);
                        sharePath += "\\" + Guid.NewGuid().ToString("N").Substring(0, 3) +path.Substring(path.LastIndexOf("\\") + 1);
                        shareURL = HostName + sharePath.Substring(sharePath.IndexOf("Images\\Uploaded") - 1).Replace("\\", "/");
                        File.Copy(path, sharePath);
                    }
					js_arrPath += " arrImagesURL.push('" + shareURL + "');" + System.Environment.NewLine;
				}
			}
			js.Text = "<script>" + js_arrPath + Environment.NewLine + " if (opener) opener." + Request.QueryString["function"] + "(arrImagesURL); window.close();</script>";
		}

		protected void btnReloadUpdatePanel_Click(object sender, EventArgs e)
		{
			Repeater1.DataSource = getTableData(postBackArg.Value, txtKeyword.Text.Trim(), 0);
			Repeater1.DataBind();
			UpdatePanel1.Update();
		}

		protected void btnPaging_Click(object sender, EventArgs e)
		{
			Repeater1.DataSource = getTableData(postBackArg.Value, txtKeyword.Text.Trim(), int.Parse(postBackArg2.Value));
			Repeater1.DataBind();
			UpdatePanel1.Update();
		}
	}
	class FileSort : IComparer
	{

		public int Compare(object x, object y)
		{
			FileInfo f1 = x as FileInfo;
			FileInfo f2 = y as FileInfo;
			return f1.CreationTime.CompareTo(f2.CreationTime);
		}

	}
}
