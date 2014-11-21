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

namespace Portal.GUI.EditoralOffice.MainOffce.editnews.controls.slideshow
{
	public partial class slideshow : Portal.API.Module
	{
		protected string PrefixURL = "/GUI/EditoralOffice/MainOffce/editnews/controls/Image 2/fileupload/";
		private string seprator = "$_$";
		protected void Page_Load(object sender, EventArgs e)
		{
			LoadRuntimeSettings();
			object runtimesetting = ReadRuntimeConfig(true, "pictureurl");
			if (runtimesetting != null && !string.IsNullOrEmpty(runtimesetting.ToString()))
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