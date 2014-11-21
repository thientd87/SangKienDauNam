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
using DFISYS.Core.DAL;

namespace DFISYS.GUI.Front_End.bacth_test8
{
	public partial class _default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				string strnews = Request.QueryString["news"] != null ? Request.QueryString["news"].ToString() : "";
				if (strnews != "")
				{
					long lngNewsID = 0;
					try
					{
						lngNewsID = Convert.ToInt64(strnews);
					}
					catch
					{
						lngNewsID = 0;
					}

					NewsRow objNewRow;
					using (MainDB objDb = new MainDB())
					{
						objNewRow = objDb.NewsCollection.GetByPrimaryKey(lngNewsID);
					}
					if (objNewRow != null)
					{
						ltrNewsTitle.Text = objNewRow.News_Title;
						ltrImageNote.Text = objNewRow.News_ImageNote;
						if ((objNewRow.News_Image != "") && (objNewRow.News_Image != null))
						{
                            imgNewsAvatar.ImageUrl = System.Configuration.ConfigurationSettings.AppSettings["ImageUrl"] + objNewRow.News_Image;
							imgNewsAvatar.Width = 250;
						}
						
						ltrNewsInit.Text = objNewRow.News_InitialContent;
						ltrNewsDetail.Text = objNewRow.News_Content;
						
					}
				}


			}
		}
	}
}
