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
using DFISYS.BO.Editoral.NewsMedia;
using System.IO;

namespace DFISYS.GUI.EditoralOffice.MainOffce.NewsMedia
{
	public partial class MediaObjectList : System.Web.UI.UserControl
	{
		public string isCapNhap = "";
		public string strMediaID = "";
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["media"] != null)
			{
				strMediaID = Request.QueryString["media"].ToString();
			}

			ShowSelectedMedia();

			if (!IsPostBack)
			{
				lnkShowAllMedia.Visible = false;
				lnkSelectedMedia.Visible = false;
				ltrSelectedMedia.Visible = false;
				ltrShowAllMedia.Visible = false;
			}
		}

		protected void grdMedia_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName.ToLower() == "NewMediaObject".ToLower())
			{
				GridViewRow grdrow = this.grdMedia.FooterRow;
				if (grdrow != null)
				{
					TextBox txtINote = grdrow.FindControl("txtINote") as TextBox;
					DropDownList cboType = grdrow.FindControl("cboIType") as DropDownList;
					FileUpload flObject = grdrow.FindControl("flObject") as FileUpload;
					if (cboType != null)
					{
						if (flObject.FileName != "")
						{
							objNewsMediaSource.InsertParameters[0].DefaultValue = flObject.FileName;
							objNewsMediaSource.InsertParameters[1].DefaultValue = cboType.SelectedValue;
							objNewsMediaSource.InsertParameters[2].DefaultValue = txtINote.Text;
							objNewsMediaSource.InsertParameters[3].DefaultValue = ChannelUsers.GetUserName();
							objNewsMediaSource.Insert();
							//thuc hien upload anh vao thu muc Share

							if (DFISYS.FileHelper.isFileMediaObject(flObject.FileName))
							{
								//duong dan den thu muc upload1
								string strFolder = "images/uploaded/";
								string strType = "Picture";
								if (cboType.SelectedValue != "1")
									strType = "Video";
								string strFileUploaded = DFISYS.FileHelper.UploadMediaObject(strFolder, "Share/Media/" + strType, flObject.PostedFile, 125);
							}
						}

						//ShowSelectedMedia();
						//grdMedia.DataBind();
					}
				}

			}

			if (e.CommandName.ToLower() == "DeleteMedia".ToLower())
			{
				string str_MediaId = e.CommandArgument.ToString().Trim();
				string str_Newsid = Request.QueryString["newsid"] != null ? Request.QueryString["newsid"].ToString().Trim() : "";
				string str_FilmId = Request.QueryString["filmid"] != null ? Request.QueryString["filmid"].ToString().Trim() : "";

				/*
				if (str_Newsid.Trim() != "")
				{
					NewsMediaHelper.Delete_News_Media_ByNewsIdAndObjectId(Convert.ToInt64(str_Newsid), str_MediaId);
                    
				}
				else
				if (str_FilmId.Trim() != "")
				{
					NewsMediaHelper.DeleteNews_Media_Film_Object_By_FilmIdAndObjectId(Convert.ToInt32(str_FilmId), Convert.ToInt32(str_MediaId));
                    
                    
				}
				 */

				if (NewsMediaHelper.Check_Exist_News_Media_ByObjectId(str_MediaId))
				{
					Page.RegisterClientScriptBlock("CannotDeleteImage", "<script language='javascript'>alert('Có một số image bạn không thể xóa được !');</script>");
				}
				else
				{
					// Neu khong dc su dung thi co the xoa di duoc
					objNewsMediaSource.DeleteParameters[0].DefaultValue = str_MediaId;
					objNewsMediaSource.Delete();
				}

				grdMedia.DataBind();
			}
		}

		protected void cboPage_SelectedIndexChanged(object sender, EventArgs e)
		{
			ViewState["media_id_checked" + grdMedia.PageIndex] = getCheckedRow();
			grdMedia.PageIndex = Convert.ToInt32(cboPage.SelectedValue);
		}

		protected void grdMedia_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType != DataControlRowType.Header && e.Row.RowType != DataControlRowType.Footer)
			{

				//if (DataBinder.Eval(e.Row.DataItem, "Object_ID").ToString() == "0")
				//{
				//    e.Row.Cells.RemoveAt(4);
				//    e.Row.Cells.RemoveAt(3);
				//    e.Row.Cells.RemoveAt(2);
				//    e.Row.Cells[1].Attributes.Add("colspan", "4");
				//    e.Row.Cells[1].Attributes.Add("align", "center");
				//}
				//else
				//{
				if (e.Row.Cells.Count > 2)
				{
					// Selected Value cho combobox Kieu
					Literal ltrType = e.Row.Cells[2].FindControl("ltrType") as Literal;
					if (ltrType != null)
					{
						if (ltrType.Text == "1")
							ltrType.Text = "Hình ảnh";
						else
							ltrType.Text = "Video";
					}

					//// Cat chuoi ten file
					//if (DataBinder.Eval(e.Row.DataItem, "Object_Url").ToString().IndexOf("/") > 0)
					//{ 
					//    int pos = DataBinder.Eval(e.Row.DataItem, "Object_Url").ToString().LastIndexOf("/");
					//    e.Row.Cells[1].Text = DataBinder.Eval(e.Row.DataItem, "Object_Url").ToString().Substring(pos+1);
					//}

				}
				if (e.Row.DataItem != null)
				{
					if ((DataBinder.Eval(e.Row.DataItem, "News_ID").ToString() == "" && DataBinder.Eval(e.Row.DataItem, "Film_ID").ToString() == ""))
					{
						//e.Row.Cells[7].Text = "";
					}
				}

				// }

			}


		}

		protected string IsCheck(string strMediaId)
		{
			//ViewState["media_id_checked" + cboPage.SelectedValue] = getCheckedRow();
			if (ViewState["media_id_checked" + cboPage.SelectedValue] != null && ViewState["media_id_checked" + cboPage.SelectedValue].ToString() != "")
			{
				string[] strArMediaId = ViewState["media_id_checked" + cboPage.SelectedValue].ToString().Split(',');
				foreach (string str in strArMediaId)
				{
					if (str.Trim() == strMediaId)
					{
						return "checked";
					}
				}
			}

			return "";


		}

		protected void grdMedia_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			this.grdMedia.EditIndex = -1;
		}

		protected void grdMedia_RowEditing(object sender, GridViewEditEventArgs e)
		{
			this.grdMedia.EditIndex = e.NewEditIndex;
		}
		protected void grdMedia_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			GridViewRow grdrow = this.grdMedia.Rows[e.RowIndex];
			if (grdrow != null)
			{
				ImageButton btnSave = grdrow.FindControl("imgSave") as ImageButton;
				if (btnSave != null)
				{
					// Check xem image này mà đang được chọn trong bài viết thì không được xóa
					string object_id = btnSave.CommandArgument;
					if (NewsMediaHelper.Check_Exist_News_Media_ByObjectId(object_id))
					{
						Page.RegisterClientScriptBlock("CannotDeleteImage", "<script language='javascript'>alert('Bạn không thể xóa image này được!');</script>");
					}
					else
					{
						objNewsMediaSource.DeleteParameters[0].DefaultValue = btnSave.CommandArgument;
						objNewsMediaSource.Delete();
					}

				}
			}
			this.grdMedia.EditIndex = -1;
		}

		// edit by bacth: do not allow modified FILE [10:06 AM 6/6/2008]
		protected void grdMedia_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			GridViewRow grdrow = this.grdMedia.Rows[e.RowIndex];
			if (grdrow != null)
			{
				// Thực hiện cập nhật lại gridview
				TextBox txtENote = grdrow.FindControl("txtENote") as TextBox;
				DropDownList cboType = grdrow.FindControl("cboType") as DropDownList;
				//FileUpload flEObject = grdrow.FindControl("flEObject") as FileUpload;
				ImageButton btnSave = grdrow.FindControl("imgSave") as ImageButton;
				if (btnSave != null)
				{
					objNewsMediaSource.UpdateParameters["_obj_id"].DefaultValue = btnSave.CommandArgument;
					//objNewsMediaSource.UpdateParameters[1].DefaultValue = flEObject.FileName;
					objNewsMediaSource.UpdateParameters["_obj_type"].DefaultValue = cboType.SelectedValue;
					objNewsMediaSource.UpdateParameters["_obj_note"].DefaultValue = txtENote.Text;
					objNewsMediaSource.Update();
					//thuc hien xoa file cu va cap nhat file moi
					//if (flEObject.FileName != "")
					//{
					//    if (Portal.FileHelper.isPicture(flEObject.FileName))
					//    {
					//        //duong dan den thu muc upload
					//        string strFolder = "images/uploaded/";
					//        string strType = "Picture";
					//        if (cboType.SelectedValue != "1")
					//            strType = "Video";
					//        Portal.FileHelper.UploadPicture(strFolder, "Share/Media/" + strType, flEObject.PostedFile, 125);
					//    }
					//}

					string oldPath = string.Empty, newPath = string.Empty;
					HiddenField hdfObject_Url = grdrow.FindControl("hdfObject_Url") as HiddenField;
					if (cboType.SelectedIndex == 1) // chuyển từ ảnh sang video
					{
						oldPath = @"/Images/Uploaded/Share/Media/picture/" + hdfObject_Url.Value;
						newPath = @"/Images/Uploaded/Share/Media/video/" + hdfObject_Url.Value;
					}
					else // chuyển từ video sang ảnh
					{
						oldPath = @"/Uploaded/Share/Media/video/" + hdfObject_Url.Value;
						newPath = @"/Uploaded/Share/Media/picture/" + hdfObject_Url.Value;
					}

					// không xóa ảnh cũ!
					if (File.Exists(Server.MapPath(oldPath)) && !File.Exists(Server.MapPath(newPath)))
						File.Copy(Server.MapPath(oldPath), Server.MapPath(newPath));

				}
			}
			//this.grdListNewsThread.AutoGenerateEditButton = false;
			grdMedia.DataBind();
			this.grdMedia.EditIndex = -1;
		}

		protected void lnkRealDel_Click(object sender, EventArgs e)
		{

			string strCheckedRow = getCheckedRow();
			if (strCheckedRow != null && strCheckedRow != "")
			{
				string[] strArCheckedRow = strCheckedRow.Split(',');
				foreach (string strObjectId in strArCheckedRow)
				{
					// Check xem image này mà đang được chọn trong bài viết thì không được xóa
					if (NewsMediaHelper.Check_Exist_News_Media_ByObjectId(strObjectId))
					{
						Page.RegisterClientScriptBlock("CannotDeleteImage", "<script language='javascript'>alert('Có một số image bạn không thể xóa được !');</script>");
					}
					else
					{
						// Neu khong dc su dung thi co the xoa di duoc
						objNewsMediaSource.DeleteParameters[0].DefaultValue = strObjectId;
						objNewsMediaSource.Delete();
					}
				}

			}


		}

		private void ShowSelectedMedia()
		{
			string strNewsId = Request.QueryString["newsid"] != null ? Request.QueryString["newsid"] : "";
			string str_FilmId = Request.QueryString["filmid"] != null ? Request.QueryString["filmid"].ToString().Trim() : "";
			string str_Type = Request.QueryString["type"] != null ? Request.QueryString["type"].ToString().Trim() : "";
			string strWhere = "";
			if (str_Type.ToLower() == "ShowAll".ToLower())
			{
				strWhere = " AND 1=1";
				lnkAddMedia.Visible = false;
				ltrAddMedia.Visible = false;
			}
			else if (strNewsId.Trim() != "")
			{
				string strObject_Id = NewsMediaHelper.Get_ObjectId_By_NewsId(Convert.ToInt64(strNewsId));
				if (strObject_Id.Trim() != "")
					strWhere = " AND Object_ID IN (" + strObject_Id + ")";

				ShowGridCol(5);
			}
			else if (str_FilmId.Trim() != "")
			{
				string strObject_Id = NewsMediaHelper.Get_ObjectId_By_FilmId(str_FilmId);
				if (strObject_Id.Trim() != "")
					strWhere = " AND Object_ID IN (" + strObject_Id + ")";

				ShowGridCol(5);

			}
			else if (Session["Object_Id"] != null && Session["Object_Id"].ToString() != "")
			{
				// Neu Tao Bai viet moi hay tao film moi
				strWhere = " AND Object_ID IN (" + Session["Object_Id"].ToString().Substring(1) + ")";
			}
			else
			{
				// Neu Tao moi Film va News Thi khong hien thi Media nao ca
				strWhere = " And Object_ID = -2 ";
			}

			if (strWhere.Trim() != "")
				objNewsMediaSource.SelectParameters[0].DefaultValue = "UserID='" + ChannelUsers.GetUserName() + "'" + strWhere;
			else
				objNewsMediaSource.SelectParameters[0].DefaultValue = "UserID='" + ChannelUsers.GetUserName() + "'" + " AND Object_ID = -1 ";



		}

		protected void lnkSelectedMedia_Click(object sender, EventArgs e)
		{
			ShowSelectedMedia();
		}

		protected void lnkShowAllMedia_Click(object sender, EventArgs e)
		{
			objNewsMediaSource.SelectParameters[0].DefaultValue = "UserID='" + ChannelUsers.GetUserName() + "' ";
		}



		private string getCheckedRow()
		{
			string strDel = Request.Form["chkSelect"];
			/*foreach (GridViewRow grdRow in this.grdMedia.Rows)
			{
				if (grdRow.RowType == DataControlRowType.DataRow)
				{
					CheckBox chkSel = (CheckBox)grdRow.Cells[0].FindControl("chkSelect");
					if (chkSel.Checked)
					{
						ImageButton btnItem = (ImageButton)grdRow.Cells[4].FindControl("imgEdit");
						if (strDel != "")
							strDel += ",";
						strDel += btnItem.CommandArgument;
					}
				}
			}*/
			if (strDel == null)
				return "";

			return strDel;
		}

		protected void lnkAddMedia_Click(object sender, EventArgs e)
		{
			string strMediaID = "";
			if (Request.QueryString["MediaFilm"] != null || Request.QueryString["media"] != null)
			{
				if (Request.QueryString["MediaFilm"] != null)
					strMediaID = Request.QueryString["MediaFilm"].ToString();
				else
					strMediaID = Request.QueryString["media"].ToString();

				// Luu lai nhung MediaObject da check tren trang nay
				ViewState["media_id_checked" + cboPage.SelectedValue] = getCheckedRow();
				string strMediaNews = "";

				// Dua tat ca MediaObject da check vao 1 bien String
				for (int i = 0; i < cboPage.Items.Count; i++)
				{
					if (ViewState["media_id_checked" + i].ToString().Trim() != "")
					{
						strMediaNews += "," + ViewState["media_id_checked" + i].ToString();
					}

				}
				if (strMediaNews.Trim() != "")
					strMediaNews = strMediaNews.Substring(1);
				else
					strMediaNews = getCheckedRow();


				Session["Object_Id"] = "";
				#region regist client script
				if (strMediaNews != "0")
				{
					Page.RegisterStartupScript("medialist", "<script>AssignRelatedNews('" + strMediaID + "','" + strMediaNews + "','" + txt_news_title_checked.ClientID + "',true);window.opener.BindDataForDropdown('hdMedia','hdMediaTitle','cboMedia','hdMedia');this.close();</script>");
				}
				#endregion
			}
		}

		protected void btnSearch_OnClick(object sender, EventArgs e)
		{
			//objNewsMediaSource.SelectParameters[0].DefaultValue = "UserID='" + Page.User.Identity.Name + "' AND ( Object_Url like '%" + txtKeyword.Text.Trim() + "%' OR Object_Note like '%" + txtKeyword.Text.Trim() + "%' )";
		}

		private void HideGridCol(int _i)
		{
			grdMedia.Columns[_i].Visible = false;
		}

		private void ShowGridCol(int _i)
		{
			grdMedia.Columns[_i].Visible = true;
		}

		private void UploadMediaObject(string fileName, string type, string title)
		{
			// Copy file sang thu muc Media Object
			fileName = fileName.Replace("/", "\\");
			if (fileName.StartsWith("\\")) fileName = fileName.Substring(1);

			if (fileName.ToLower().IndexOf("common") == 0 || fileName.ToLower().IndexOf(HttpContext.Current.User.Identity.Name.ToLower()) == 0)
			{

				int pos = fileName.LastIndexOf(@"\");
				string name = "";

				if (pos > 0)
					name = fileName.Substring(pos + 1);
				else
					name = fileName;

				string destinationFile = Server.MapPath(@"\Images");
				if (type == "1")
					destinationFile += @"\Uploaded\Share\Media\picture\" + name;
				else
					destinationFile += @"\Uploaded\Share\Media\video\" + name;

				if (!Directory.Exists(Server.MapPath(@"\Images") + @"\Uploaded\Share\Media\picture\"))
				{
					Directory.CreateDirectory(Server.MapPath(@"\Images") + @"\Uploaded\Share\Media\picture\");
				}

				string sourceFile = Server.MapPath(@"\Images") + @"\Uploaded\" + fileName;

				if (File.Exists(sourceFile) && !File.Exists(destinationFile))
					File.Copy(sourceFile, destinationFile);

				// Insert vao DB
				objNewsMediaSource.InsertParameters[0].DefaultValue = name;
				objNewsMediaSource.InsertParameters[1].DefaultValue = type;
				objNewsMediaSource.InsertParameters[2].DefaultValue = title;
				objNewsMediaSource.InsertParameters[3].DefaultValue = ChannelUsers.GetUserName();
				objNewsMediaSource.Insert();

				isCapNhap = "ok";
			}
		}

		protected void btnAddMediaObject_Click(object sender, EventArgs e)
		{
			string strObjectId = "";
			if (txtFileName1.Text.Trim() != "")
			{
				UploadMediaObject(txtFileName1.Text.Trim(), cboType1.SelectedValue, txtTitle1.Text.Trim());
			}

			if (txtFileName2.Text.Trim() != "")
				UploadMediaObject(txtFileName2.Text.Trim(), cboType2.SelectedValue, txtTitle2.Text.Trim());

			if (txtFileName3.Text.Trim() != "")
				UploadMediaObject(txtFileName3.Text.Trim(), cboType3.SelectedValue, txtTitle3.Text.Trim());

			if (txtFileName4.Text.Trim() != "")
				UploadMediaObject(txtFileName4.Text.Trim(), cboType4.SelectedValue, txtTitle4.Text.Trim());

			if (txtFileName5.Text.Trim() != "")
				UploadMediaObject(txtFileName5.Text.Trim(), cboType5.SelectedValue, txtTitle5.Text.Trim());

			txtTitle1.Text = ""; txtTitle2.Text = ""; txtTitle3.Text = ""; txtTitle4.Text = ""; txtTitle5.Text = "";
			cboType1.SelectedValue = "1"; cboType2.SelectedValue = "1"; cboType3.SelectedValue = "1"; cboType4.SelectedValue = "1"; cboType5.SelectedValue = "1";
			txtFileName1.Text = ""; txtFileName2.Text = ""; txtFileName3.Text = ""; txtFileName4.Text = ""; txtFileName5.Text = "";

			//Page.RegisterClientScriptBlock("aaaa", "<Script language='javascript'> window.attachEvent('onload',isContinue); </script>");


			ShowSelectedMedia();
		}
	}
}