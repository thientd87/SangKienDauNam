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
using System.IO;

namespace Portal.GUI.EditoralOffice.MainOffce.editnews.controls.slideshow
{
	public partial class editform : Portal.API.Module
	{
		protected string PrefixURL = "/GUI/EditoralOffice/MainOffce/editnews/controls/Image 2/fileupload/";

		private string seprator = "$_$";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack) bindForm();
		}

		protected void btnAdd_Click(object sender, EventArgs e)
		{
			if (fUpload.HasFile && fUpload.PostedFile.ContentType.IndexOf("image") > -1)
			{
				string fileName = HttpUtility.UrlDecode(fUpload.FileName);
				string directory = HttpUtility.UrlDecode(Server.MapPath(PrefixURL + DateTime.Now.ToString("yyyy/MM/dd/")));
				if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
				fUpload.SaveAs(directory + fileName);
				
				PortalDefinition.Module module = new PortalDefinition.Module();
				module.reference=ModuleRef;
				module.type = ModuleType;
				module.LoadRuntimeProperties();
				string oldValue = module.moduleRuntimeSettings.GetRuntimePropertyValue(true, "pictureurl");
				if (string.IsNullOrEmpty(oldValue))
					module.moduleRuntimeSettings.SetRuntimePropertyValue(true, "pictureurl", DateTime.Now.ToString("yyyy/MM/dd/") + fileName);
				else
					module.moduleRuntimeSettings.SetRuntimePropertyValue(true, "pictureurl", oldValue + seprator + DateTime.Now.ToString("yyyy/MM/dd/") + fileName);
				module.SaveRuntimeSettings();
			}
			bindForm();
			((Portal.GUI.EditoralOffice.MainOffce.editnews.editModule)this.Page).loadPresentationModule(ModuleType, ModuleRef);
		}

		protected void btnRemove_Click(object sender, EventArgs e)
		{
			LoadRuntimeSettings();
			object runtimesetting = ReadRuntimeConfig(true, "pictureurl");
			if (runtimesetting != null && runtimesetting.ToString() != string.Empty)
			{
				string strRuntimeSetting = runtimesetting.ToString();
				string arg = ((Button)sender).CommandArgument;
				strRuntimeSetting = strRuntimeSetting.Replace(arg, string.Empty);
				if (strRuntimeSetting.StartsWith(seprator)) strRuntimeSetting = strRuntimeSetting.Substring(seprator.Length);
				if (strRuntimeSetting.EndsWith(seprator)) strRuntimeSetting = strRuntimeSetting.Substring(0, strRuntimeSetting.Length - seprator.Length);
				strRuntimeSetting = strRuntimeSetting.Replace(seprator + seprator, string.Empty);

				string[] pictureurls = strRuntimeSetting.Split(new string[] { seprator }, StringSplitOptions.RemoveEmptyEntries);
				DataTable tbl = new DataTable();
				DataRow row;
				tbl.Columns.Add("PictureURL");
				for (int i = 0; i < pictureurls.Length; i++)
				{
					row = tbl.NewRow();
					row[0] = pictureurls.GetValue(i);
					tbl.Rows.Add(row);
				}
				rptSlideshow.DataSource = tbl;
				rptSlideshow.DataBind();


				
				PortalDefinition.Module module = new PortalDefinition.Module();
				module.reference = ModuleRef;
				module.type = ModuleType;
				module.LoadRuntimeProperties();
				module.moduleRuntimeSettings.SetRuntimePropertyValue(true, "pictureurl", strRuntimeSetting);
				module.SaveRuntimeSettings();

				// delete file
				if (File.Exists(Server.MapPath(PrefixURL + arg)))
					try
					{
						File.Delete(Server.MapPath(PrefixURL + arg));
					}
					catch { }
			}
			((Portal.GUI.EditoralOffice.MainOffce.editnews.editModule)this.Page).loadPresentationModule(ModuleType, ModuleRef);
		}

		private void bindForm()
		{
			LoadRuntimeSettings();
			object runtimesetting = ReadRuntimeConfig(true, "pictureurl");
			if (runtimesetting != null && runtimesetting.ToString() != string.Empty)
			{
				string[] pictureurls = runtimesetting.ToString().Split(new string[] { seprator }, StringSplitOptions.RemoveEmptyEntries);
				DataTable tbl = new DataTable();
				DataRow row;
				tbl.Columns.Add("PictureURL");
				for (int i = 0; i < pictureurls.Length; i++)
				{
					row = tbl.NewRow();
					row[0] = pictureurls.GetValue(i);
					tbl.Rows.Add(row);
				}
				rptSlideshow.DataSource = tbl;
				rptSlideshow.DataBind();
			}
		}
	}
}