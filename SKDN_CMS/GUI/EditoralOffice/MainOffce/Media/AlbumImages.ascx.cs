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

namespace Portal.GUI.EditoralOffice.MainOffce.Media
{
    public partial class AlbumImages : System.Web.UI.UserControl
    {
        protected string _SetFolder = "";
        protected string _SetFullFolder = "";

        protected string _UploadDir = "";

        public String GetPathToUploadFile()
        {
            string _SetFolder = new Portal.BO.Editoral.Gallery.AlbumImage().SetFolder();
            string a=  @"Images\Uploaded\" + _SetFolder + @"\" + ViewState["fpUploadFolderRelativePath"] + @"";
            return a.Replace(@"\","/");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _SetFolder = new Portal.BO.Editoral.Gallery.AlbumImage().SetFolder();
            _SetFullFolder = _SetFolder;

            

            _UploadDir = Server.MapPath(@"\Images") + @"\Uploaded\" + _SetFolder;
            //lblError.Text = "";

            if (!IsPostBack)
            {
                if (!Directory.Exists(_UploadDir))
                {
                    Directory.CreateDirectory(_UploadDir);
                    Directory.CreateDirectory(_UploadDir + @"\Thumbnails");
                    Directory.CreateDirectory(_UploadDir + @"\Thumbnails\ChildThumb");
                }

                ShowFile(txtKeyword.Text.Trim());
                
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string _SetFolder = new Portal.BO.Editoral.Gallery.AlbumImage().SetFolder();

            for (int i = 0; i < Request.Files.Count; i++)
            {
                if (Request.Files[i].FileName.Trim().Length > 0)
                {

                    HttpPostedFile file = Request.Files[i];
                    string _path = @"Images\Uploaded\" + _SetFolder + @"\" + ViewState["fpUploadFolderRelativePath"] + @"\";
                    new Portal.BO.Editoral.Gallery.AlbumImage().UploadImage(_path, file);
                }
            }

            ShowFile(txtKeyword.Text.Trim());
            if (ViewState["fpUploadFolderRelativePath"] != null)
            {
                if (ViewState["fpUploadFolderRelativePath"].ToString() != "")
                {

                    _SetFullFolder = _SetFolder + @"\" + ViewState["fpUploadFolderRelativePath"];
                    _SetFullFolder = _SetFullFolder.Substring(0, _SetFullFolder.Length - 1);
                    _SetFullFolder = _SetFullFolder.Replace(@"\", @"/");
                }
            }


        }

        private void showThumnailFiles(string _searchPattern)
        {

            if (ViewState["fpUploadFolderRelativePath"] == null)
            {
                ViewState["fpUploadFolderRelativePath"] = "";
            }

            string _pathFolder = _UploadDir + @"\" + ViewState["fpUploadFolderRelativePath"];

            //Sort DataView 
            DataView FileSystemTableView = new Portal.BO.Editoral.Gallery.AlbumImage().GetImage(_pathFolder, _searchPattern).DefaultView;
            //FileSystemTableView.Sort = " Modified1 DESC, Modified2 DESC, Modified3 DESC, Modified4 DESC ";

            GridView1.DataSource = FileSystemTableView;
            //Ensure PageIndex is not out of range
            GridView1.DataBind();
        }

        private void showListFiles(string _searchPattern)
        {
            if (HttpContext.Current.User.Identity.Name == "")
                return;

            if (ViewState["fpUploadFolderRelativePath"] == null)
            {
                ViewState["fpUploadFolderRelativePath"] = "";
            }

            string _pathFolder = _UploadDir + @"\" + ViewState["fpUploadFolderRelativePath"];

            //Sort DataView 
            DataView FileSystemTableView = new Portal.BO.Editoral.Gallery.AlbumImage().GetListImage(_pathFolder, _searchPattern).DefaultView;
            FileSystemTableView.Sort =" IsFile,Modified DESC";
            grdListImage.DataSource = FileSystemTableView;
            //Ensure PageIndex is not out of range
            grdListImage.DataBind();
        }

        private void ChangePath(String Direction)
        {
            if (Direction == "/")	// goto root
                ViewState["fpUploadFolderRelativePath"] = "";
            else if (Direction == "../") // go one level up in directory tree
                ViewState["fpUploadFolderRelativePath"] = getParentDirectory(ViewState["fpUploadFolderRelativePath"].ToString());
            else
            {
                // add the directory name to the end of the current path
                if (ViewState["fpUploadFolderRelativePath"].ToString().LastIndexOf("\\") != ViewState["fpUploadFolderRelativePath"].ToString().Length - 1)
                    ViewState["fpUploadFolderRelativePath"] = ViewState["fpUploadFolderRelativePath"] + @"\";
                ViewState["fpUploadFolderRelativePath"] = ViewState["fpUploadFolderRelativePath"].ToString() + Direction + @"\";
            }


        }

        private string getParentDirectory(String RelativePath)
        {
            // this function works for /main/db/test/ as well as /main/db/test.aspx
            if (RelativePath == "./")
                return ("../");	// trivial, no string manipulation required
            else if (RelativePath == "")
                return ("");	// can't go higher than root
            else
            {
                // remove trailing "/" at end of path
                if (RelativePath.LastIndexOf("\\") == RelativePath.Length - 1)
                {
                    RelativePath = RelativePath.Remove(RelativePath.LastIndexOf("\\"), (RelativePath.Length - RelativePath.LastIndexOf("\\")));
                }
                try
                {
                    // remove the characters after the last occurence of / in the string. => parent directory
                    RelativePath = RelativePath.Remove(RelativePath.LastIndexOf("\\"), (RelativePath.Length - RelativePath.LastIndexOf("\\")));
                    return (RelativePath);
                }
                catch
                {
                    return ("");	// default to root;
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlTable _folder1 = (HtmlTable)e.Row.Cells[0].FindControl("folder1");
                HtmlTable _folder2 = (HtmlTable)e.Row.Cells[1].FindControl("folder2");
                HtmlTable _folder3 = (HtmlTable)e.Row.Cells[2].FindControl("folder3");
                HtmlTable _folder4 = (HtmlTable)e.Row.Cells[3].FindControl("folder4");

                _folder1.Visible = false;
                _folder2.Visible = false;
                _folder3.Visible = false;
                _folder4.Visible = false;

                HtmlTable _flash1 = (HtmlTable)e.Row.Cells[0].FindControl("flash1");
                HtmlTable _flash2 = (HtmlTable)e.Row.Cells[1].FindControl("flash2");
                HtmlTable _flash3 = (HtmlTable)e.Row.Cells[2].FindControl("flash3");
                HtmlTable _flash4 = (HtmlTable)e.Row.Cells[3].FindControl("flash4");

                _flash1.Visible = false;
                _flash2.Visible = false;
                _flash3.Visible = false;
                _flash4.Visible = false;

                HtmlTable _media1 = (HtmlTable)e.Row.Cells[0].FindControl("media1");
                HtmlTable _media2 = (HtmlTable)e.Row.Cells[1].FindControl("media2");
                HtmlTable _media3 = (HtmlTable)e.Row.Cells[2].FindControl("media3");
                HtmlTable _media4 = (HtmlTable)e.Row.Cells[3].FindControl("media4");

                _media1.Visible = false;
                _media2.Visible = false;
                _media3.Visible = false;
                _media4.Visible = false;

                HtmlTable _tb1 = (HtmlTable)e.Row.Cells[0].FindControl("tb1");
                HtmlTable _tb2 = (HtmlTable)e.Row.Cells[1].FindControl("tb2");
                HtmlTable _tb3 = (HtmlTable)e.Row.Cells[2].FindControl("tb3");
                HtmlTable _tb4 = (HtmlTable)e.Row.Cells[3].FindControl("tb4");

                _tb1.Visible = false;
                _tb2.Visible = false;
                _tb3.Visible = false;
                _tb4.Visible = false;

                Portal.BO.Editoral.Gallery.AlbumImage albumImage = new Portal.BO.Editoral.Gallery.AlbumImage();

                if (string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FileName1"))))
                {
                    _tb1.Visible = false;
                    _folder1.Visible = false;
                }
                else
                {
                    string _fileName1 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FileName1"));
                    if (albumImage.checkImageExtension(_fileName1))
                    {
                        _tb1.Visible = true;
                        _folder1.Visible = false;
                    }
                    else
                        if (albumImage.checkFlashExtension(_fileName1))
                        {
                            _flash1.Visible = true;
                            _tb1.Visible = false;

                        }
                        else
                            if (albumImage.checkWindowMediaExtension(_fileName1))
                            {
                                _media1.Visible = true;
                            }
                            else
                            {
                                _folder1.Visible = true;
                            }
                }



                if (string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FileName2"))))
                {
                    _tb2.Visible = false;
                    _folder2.Visible = false;
                }
                else
                {
                    string _fileName2 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FileName2"));
                    if (albumImage.checkImageExtension(_fileName2))
                    {
                        _tb2.Visible = true;
                        _folder2.Visible = false;
                    }
                    else
                        if (albumImage.checkFlashExtension(_fileName2))
                        {
                            _flash2.Visible = true;
                            _tb2.Visible = false;

                        }
                        else
                            if (albumImage.checkWindowMediaExtension(_fileName2))
                            {
                                _media2.Visible = true;
                            }
                            else
                            {
                                _folder2.Visible = true;
                            }
                }


                if (string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FileName3"))))
                {
                    _tb3.Visible = false;
                    _folder3.Visible = false;
                }
                else
                {
                    string _fileName3 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FileName3"));
                    if (albumImage.checkImageExtension(_fileName3))
                    {
                        _tb3.Visible = true;
                        _folder3.Visible = false;
                    }
                    else
                        if (albumImage.checkFlashExtension(_fileName3))
                        {
                            _flash3.Visible = true;
                            _tb3.Visible = false;

                        }
                        else
                            if (albumImage.checkWindowMediaExtension(_fileName3))
                            {
                                _media3.Visible = true;
                            }
                            else
                            {
                                _folder3.Visible = true;
                            }
                }



                if (string.IsNullOrEmpty(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FileName4"))))
                {
                    _tb4.Visible = false;
                    _folder4.Visible = false;
                }
                else
                {
                    string _fileName4 = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FileName4"));
                    if (albumImage.checkImageExtension(_fileName4))
                    {
                        _tb4.Visible = true;
                        _folder4.Visible = false;
                    }
                    else
                        if (albumImage.checkFlashExtension(_fileName4))
                        {
                            _flash4.Visible = true;
                            _tb4.Visible = false;
                        }
                        else
                            if (albumImage.checkWindowMediaExtension(_fileName4))
                            {
                                _media4.Visible = true;
                            }
                            else
                            {
                                _folder4.Visible = true;
                            }
                }

            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            ShowFile(txtKeyword.Text.Trim());
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string _folderName = "";
            if (e.CommandArgument is string)
            {
                _folderName = Convert.ToString(e.CommandArgument);
            }
            if (e.CommandName.Equals("SelectFolder"))
            {
                ChangePath(_folderName);
                ShowFile(txtKeyword.Text.Trim());


            }
            if (e.CommandName.Equals("DeleteFile1") || e.CommandName.Equals("DeleteFile2") || e.CommandName.Equals("DeleteFile3") || e.CommandName.Equals("DeleteFile4"))
            {
                string _pathFile = _folderName;
                string _pathFolder = _UploadDir + @"\" + ViewState["fpUploadFolderRelativePath"];
                new Portal.BO.Editoral.Gallery.AlbumImage().DeleteFile(_pathFolder, _pathFile);
                ShowFile(txtKeyword.Text.Trim());
            }
            if (e.CommandName.Equals("DeleteFolder1") || e.CommandName.Equals("DeleteFolder2") || e.CommandName.Equals("DeleteFolder3") || e.CommandName.Equals("DeleteFolder4"))
            {
                string _pathFolder = _UploadDir + @"\" + ViewState["fpUploadFolderRelativePath"] + _folderName;
                new Portal.BO.Editoral.Gallery.AlbumImage().DeleteFolder(_pathFolder);
                ShowFile(txtKeyword.Text.Trim());
            }

            if (ViewState["fpUploadFolderRelativePath"] != null)
            {
                if (ViewState["fpUploadFolderRelativePath"].ToString() != "")
                {

                    _SetFullFolder = _SetFolder + @"\" + ViewState["fpUploadFolderRelativePath"];
                    _SetFullFolder = _SetFullFolder.Substring(0, _SetFullFolder.Length - 1);
                    _SetFullFolder = _SetFullFolder.Replace(@"\", @"/");
                }
            }

        }

        protected void UpBtn_Click(object sender, ImageClickEventArgs e)
        {
            ChangePath("../");
            ShowFile(txtKeyword.Text.Trim());

            if (ViewState["fpUploadFolderRelativePath"] != null)
            {
                if (ViewState["fpUploadFolderRelativePath"].ToString() != "")
                {

                    _SetFullFolder = _SetFolder + @"\" + ViewState["fpUploadFolderRelativePath"];
                    _SetFullFolder = _SetFullFolder.Replace(@"\", @"/");
                }
            }

        }

        protected void GoRoot_Click(object sender, ImageClickEventArgs e)
        {
            ChangePath("/");
            ShowFile(txtKeyword.Text.Trim());

            if (ViewState["fpUploadFolderRelativePath"] != null)
            {
                if (ViewState["fpUploadFolderRelativePath"].ToString() != "")
                {

                    _SetFullFolder = _SetFolder + @"\" + ViewState["fpUploadFolderRelativePath"];
                    _SetFullFolder = _SetFullFolder.Replace(@"\", @"/");
                }
            }
        }

        protected void LinkDelete_Click(object sender, EventArgs e)
        {
            
            // Get cac file can delete
            string strFileName = Request.Form["chkSelect"];
            if(strFileName != null)
            {
                Portal.BO.Editoral.Gallery.AlbumImage objAlbumImage = new Portal.BO.Editoral.Gallery.AlbumImage();
                string[] strArFileName = strFileName.Split(',');
                for (int i = 0; i < strArFileName.Length; i++)
                {
                    if (objAlbumImage.checkFlashExtension(strArFileName[i]) || objAlbumImage.checkImageExtension(strArFileName[i]) || objAlbumImage.checkWindowMediaExtension(strArFileName[i]))
                    {
                        // Delete File
                        string _pathFolder = _UploadDir + @"\" + ViewState["fpUploadFolderRelativePath"];
                        objAlbumImage.DeleteFile(_pathFolder, strArFileName[i]);
                    }
                    else
                    {
                        // Delete Folder
                        string _pathFolder = _UploadDir + @"\" + ViewState["fpUploadFolderRelativePath"] + strArFileName[i];
                        objAlbumImage.DeleteFolder(_pathFolder);
                    }
                }
            }

            ShowFile(txtKeyword.Text.Trim());
            if (ViewState["fpUploadFolderRelativePath"] != null)
            {
                if (ViewState["fpUploadFolderRelativePath"].ToString() != "")
                {

                    _SetFullFolder = _SetFolder + @"\" + ViewState["fpUploadFolderRelativePath"];
                    _SetFullFolder = _SetFullFolder.Substring(0, _SetFullFolder.Length - 1);
                    _SetFullFolder = _SetFullFolder.Replace(@"\", @"/");
                }
            }
            
           
        }

        
        

        

        protected void btnView_Click(object sender, EventArgs e)
        {
            
        }

        protected void cbxViewBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            ShowFile(txtKeyword.Text.Trim());
        }

        private void ShowFile(string _searchPattern)
        {
            if (cbxViewBy.SelectedValue.Trim().ToLower() == "thumbnails")
            {
                grdListImage.Visible = false;
                GridView1.Visible = true;
                showThumnailFiles(_searchPattern);
            }
            else
            {
                grdListImage.Visible = true;
                GridView1.Visible = false;
                showListFiles(_searchPattern); 
            }
            
        }

        protected void grdListFile_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("SelectFile"))
            {
                string _name = "";
                string _isFile = "";
                if (e.CommandArgument is string)
                {
                    string[] arStr = e.CommandArgument.ToString().Split(',');
                    _name = arStr[0];
                    _isFile = arStr[1];
                }     

                if (_isFile.ToLower() == "false")
                {
                    // Neu bam vao Folder thi vao folder do
                    ChangePath(_name);
                    ShowFile(txtKeyword.Text.Trim());
                }
                else
                { 
                    // Lua chon 1 image   
                    SelectFile(_name);
                }
            }

            if (e.CommandName.Equals("DeleteFile"))
            {
                string _name = "";
                string _isFile = "";
                if (e.CommandArgument is string)
                {
                    string[] arStr = e.CommandArgument.ToString().Split(',');
                    _name = arStr[0];
                    _isFile = arStr[1];
                }


                if (_isFile.ToLower() == "false")
                {
                    // Delete Folder
                    string _pathFolder = _UploadDir + @"\" + ViewState["fpUploadFolderRelativePath"] + _name;
                    new Portal.BO.Editoral.Gallery.AlbumImage().DeleteFolder(_pathFolder);
                    ShowFile(txtKeyword.Text.Trim());
                }
                else
                { 
                    // Delete File
                    string _pathFile = _name;
                    string _pathFolder = _UploadDir + @"\" + ViewState["fpUploadFolderRelativePath"];
                    new Portal.BO.Editoral.Gallery.AlbumImage().DeleteFile(_pathFolder, _pathFile);
                    ShowFile(txtKeyword.Text.Trim());
                }

                if (ViewState["fpUploadFolderRelativePath"] != null)
                {
                    if (ViewState["fpUploadFolderRelativePath"].ToString() != "")
                    {

                        _SetFullFolder = _SetFolder + @"\" + ViewState["fpUploadFolderRelativePath"];
                        _SetFullFolder = _SetFullFolder.Substring(0, _SetFullFolder.Length - 1);
                        _SetFullFolder = _SetFullFolder.Replace(@"\", @"/");
                    }
                }

                
            }
        }

        public void SelectFile(String _strFileName)
        {
            
            string _strNewsID = Request.QueryString["newsid"] != null ? Request.QueryString["newsid"].ToString() : "";
            string _strType = Request.QueryString["type"] != null ? Request.QueryString["type"].ToString() : "";
            string _strControlID = Request.QueryString["controlid"] != null ? Request.QueryString["controlid"].ToString() : "";
            string _pathImage = _UploadDir + @"\" + ViewState["fpUploadFolderRelativePath"] + _strFileName;
            string _DesDir = Server.MapPath(@"\Images") + @"\Uploaded\Share\";
            string _strUrlImage = "";


            String ServerImagePath = Request.QueryString["ServerImagePath"] != null ? Request.QueryString["ServerImagePath"].ToString() : "";
            //SonPC 14/02/2008
            string _strUrlImageNews = String.Empty;
            string editor = Request.QueryString["editor"] != null ? Request.QueryString["editor"].ToString() : "";
            //SonPC 12/02/2008

            // === Begin  Copy Image vao thu muc Share
            if (_strType == "news")
            {
                if (_strNewsID != "")
                {
                    _DesDir += _strNewsID + @"\";
                    if (!Directory.Exists(_DesDir))
                        Directory.CreateDirectory(_DesDir);
                }
                else
                {
                    //Session["newsid"] = DateTime.Now.Year + "" + DateTime.Now.Month + DateTime.Now.Day + "" + DateTime.Now.Hour + "" + DateTime.Now.Minute + "" + DateTime.Now.Second + DateTime.Now.Millisecond;
                    _strNewsID = Session["newsid"].ToString();
                    _DesDir += Session["newsid"].ToString() + @"\";
                    if (!Directory.Exists(_DesDir))
                        Directory.CreateDirectory(_DesDir);
                }

                _strUrlImage = "Images/Uploaded/Share/" + _strNewsID + "/" + _strFileName;
                _strUrlImageNews = "Images/Uploaded/"+HttpContext.Current.User.Identity.Name+ "/" + _strFileName;

                //_strUrlImage = System.Configuration.ConfigurationSettings.AppSettings["ImageUrl"] + "/" + _strUrlImage;
                //_strUrlImageNews = System.Configuration.ConfigurationSettings.AppSettings["ImageUrl"] + "/" + _strUrlImageNews;
            }
            else if (_strType == "mediaobject")
            {
                _strUrlImage = _strFileName;

                if (ViewState["fpUploadFolderRelativePath"].ToString().Trim() != "")
                    _strUrlImage = ViewState["fpUploadFolderRelativePath"] + @"\" + _strFileName; ;
            }
            
            
            string _desImage = _DesDir + _strFileName;
            // Neu la kieu MediaObject thi khong copy file o day, ma copy o trang MediaObjectList.ascx
            if (!File.Exists(_desImage) && _strType != "mediaobject")
                    File.Copy(_pathImage, _desImage);

            // *** END Copy vao thu muc Share
             
            // Bind duong dan imnet vao thu textbox

                

            if (!Page.IsStartupScriptRegistered("UpdateCaller"))
            {
                string strjscript = "<script language='javascript'>\n";
                //strjscript.Append("function StartupScript() {\n");
                strjscript += "if (window.opener == null) \n";
                strjscript += "  { window.close(); }\n";
                strjscript += "else \n";
                strjscript += "{\n";
                strjscript += "  var vCallerDocument = window.opener.document;\n  var vItem;\n";
                strjscript += "  vItem = vCallerDocument.getElementById('" + _strControlID + "');\n";

                //SonPC 14/02/2008
                strjscript += "var editor = '" + editor + "'; \n";
                strjscript += "var sFile_Extension = '" + _strUrlImage + "';";

                strjscript += "if (editor == '1') \n";
                strjscript += "{ \n";
                strjscript += "     if(sFile_Extension.toUpperCase().indexOf('.GIF') >=0 || sFile_Extension.toUpperCase().indexOf('.JPG') >=0 || sFile_Extension.toUpperCase().indexOf('.PNG') >=0) \n";
                strjscript += "     { \n";
                //strjscript += "        window.opener.theEditor.PasteHtml('<img src=\"/" + _strUrlImage + "\" border=\"0\">'); \n";
                strjscript += "       window.opener.document.getElementById('inpFlvURL').value = '" + ServerImagePath + "/" + _strUrlImageNews + "' ;\n ";
                strjscript += "     } \n";
                strjscript += "     else if(sFile_Extension.toUpperCase().indexOf('.SWF') >=0 || sFile_Extension.toUpperCase().indexOf('.FLV') >= 0)\n";
                strjscript += "     { \n";
                strjscript += "       window.opener.document.getElementById('inpFlvURL').value = '" + ServerImagePath + "/" + _strUrlImageNews + "' ;\n ";
                strjscript += "     }\n";
                strjscript += "} \n";
                //SonPC 14/02/2008

                //SonPC 05/05/2008
                strjscript += "else if(editor == '2') \n";
                strjscript += "{";
                //strjscript += " if(navigator.appName.indexOf('Microsoft')!=-1) \n";
                //strjscript += " {   var obj=dialogArguments.oUtil.obj; } \n";
                //strjscript += " else \n";
                strjscript += "    var obj=window.opener.oUtil.obj;  \n";
                strjscript += " obj.insertHTML(\"<img src='" + System.Configuration.ConfigurationSettings.AppSettings["ImageUrl"] + "/" + _strUrlImage + "'>\"); \n";
                strjscript += "}";
                //SonPC 05/05/2008

                strjscript += "else \n";
                strjscript += "{ \n";
                strjscript += "  vItem.value = '" + _strUrlImage + "';\n";
                strjscript += "} \n";

                strjscript += "}\n";
                strjscript += "window.close();\n";
                strjscript += "\n</script>";

                Page.RegisterStartupScript("UpdateCaller", strjscript);
            }
        }

        protected void grdListImage_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow objrow = grdListImage.Rows[e.NewEditIndex];

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ShowFile(txtKeyword.Text.Trim());
        }

        protected void grdListImage_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grdListImage.PageIndex = e.NewPageIndex;
            ShowFile(txtKeyword.Text.Trim());
        }

        protected void grdListImage_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnPreview = (ImageButton)e.Row.Cells[5].FindControl("btnPreview");
                LinkButton lnbSelectFile = (LinkButton)e.Row.Cells[1].FindControl("lnbSelectFile");

                Portal.BO.Editoral.Gallery.AlbumImage albumImage = new Portal.BO.Editoral.Gallery.AlbumImage();

                if (DataBinder.Eval(e.Row.DataItem, "FileName_IsFile").ToString().ToLower().IndexOf("true") > 0)
                {
                    lnbSelectFile.OnClientClick = "showImage('" + DataBinder.Eval(e.Row.DataItem, "FullName") + "');return false;";

                    string _fileName = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "FileName"));
                    if (albumImage.checkImageExtension(_fileName))
                    {
                        //btnPreview.Attributes.Add("onclick", "ImgResizeViewByList('/images/Uploaded/" + _SetFolder + "/" + _fileName + "',500);return false;");
                    }
                    else if (albumImage.checkFlashExtension(_fileName))
                    {
                        //btnPreview.Attributes.Add("onclick", "playFlashByViewList('/images/Uploaded/" + _SetFolder + "/" + _fileName + "');return false;");

                    } else if (albumImage.checkWindowMediaExtension(_fileName))
                        {
                            //btnPreview.Attributes.Add("onclick", "playMediaByViewList('/images/Uploaded/" + _SetFolder + "/" + _fileName + "');return false;");
                           // playMedia('/images/Uploaded/<%=_SetFullFolder%>/<%#DataBinder.Eval(Container.DataItem,"FileName1")%>')            
                        }
                       
                    
                }
                else
                {
                    // Neu la thu muc thi Invisible Link Preview
                    btnPreview.Visible = false;
                }

                
            }
        }

        #region Create New Folder
        protected void btnCreateNewFolder_Click(object sender, EventArgs e)
        {
            if (!txtNewFolder.Text.Trim().Equals(""))
            {
                if (ViewState["fpUploadFolderRelativePath"] == null)
                {
                    ViewState["fpUploadFolderRelativePath"] = "";
                }
                string _pathFolder = _UploadDir + @"\" + ViewState["fpUploadFolderRelativePath"];
                DirectoryInfo mainDir = new DirectoryInfo(_pathFolder);
                mainDir.CreateSubdirectory(txtNewFolder.Text.Trim());
                txtNewFolder.Text = String.Empty;
                showListFiles(txtKeyword.Text.Trim());
            }
        }
        #endregion
    }
}