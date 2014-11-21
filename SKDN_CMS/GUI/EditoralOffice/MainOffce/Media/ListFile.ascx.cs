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
    public partial class ListFile : System.Web.UI.UserControl
    {
        protected string _SetFolder = "";
        protected string _SetFullFolder = "";

        protected string _UploadDir = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            _SetFolder = new Portal.BO.Editoral.Gallery.AlbumImage().SetFolder();
            _SetFullFolder = _SetFolder;



            _UploadDir = Server.MapPath(@"\Images") + @"\Uploaded\" + _SetFolder;
            lblError.Text = "";

            if (!IsPostBack) {

                if (!Directory.Exists(_UploadDir))
                {
                    Directory.CreateDirectory(_UploadDir);
                    Directory.CreateDirectory(_UploadDir + @"\Thumbnails");
                    Directory.CreateDirectory(_UploadDir + @"\Thumbnails\ChildThumb");
                }
                    

                showFiles();
            }
                
        }

        private void showFiles()
        {

            if (ViewState["fpUploadFolderRelativePath"] == null)
            {
                ViewState["fpUploadFolderRelativePath"] = "";
            }

            string _pathFolder = _UploadDir + @"\" + ViewState["fpUploadFolderRelativePath"];

            //Sort DataView 
            DataView FileSystemTableView = new Portal.BO.Editoral.Gallery.AlbumImage().GetImage(_pathFolder, txtKeyword.Text.Trim()).DefaultView;
            //FileSystemTableView.Sort("  ");
            
            GridView1.DataSource = FileSystemTableView;
            //Ensure PageIndex is not out of range
            GridView1.DataBind();
        }

        private void ChangePath(String Direction)
        {
            if (Direction == "/")	// goto root
                ViewState["fpUploadFolderRelativePath"] = "";
            else if (Direction == "../") // go one level up in directory tree
                ViewState["fpUploadFolderRelativePath"] = getParentDirectory(ViewState["fpUploadFolderRelativePath"].ToString());
            else {
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
                else {
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
                else {
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
                else {
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

                        }else
                            if (albumImage.checkWindowMediaExtension(_fileName3)) {
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
                else {
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
            showFiles();
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
                showFiles();

                
            }
            if (e.CommandName.Equals("DeleteFile1") || e.CommandName.Equals("DeleteFile2") || e.CommandName.Equals("DeleteFile3") || e.CommandName.Equals("DeleteFile4"))
            {
                string _pathFile =  _folderName;
                string _pathFolder = _UploadDir + @"\" + ViewState["fpUploadFolderRelativePath"];
                new Portal.BO.Editoral.Gallery.AlbumImage().DeleteFile(_pathFolder, _pathFile);
                showFiles();
            }
            if (e.CommandName.Equals("DeleteFolder1") || e.CommandName.Equals("DeleteFolder2") || e.CommandName.Equals("DeleteFolder3") || e.CommandName.Equals("DeleteFolder4"))
            {
                string _pathFolder = _UploadDir + @"\" + ViewState["fpUploadFolderRelativePath"] + _folderName;
                new Portal.BO.Editoral.Gallery.AlbumImage().DeleteFolder(_pathFolder);
                showFiles();
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

        protected void CreateFolder(object sender, EventArgs e)
        {
            if (txtNewFolder.Text.Trim() != "")
            {

                if (ViewState["fpUploadFolderRelativePath"] == null)
                {
                    ViewState["fpUploadFolderRelativePath"] = "";
                }

                string _pathFolder = _UploadDir + @"\" + ViewState["fpUploadFolderRelativePath"];

                DirectoryInfo mainDir = new DirectoryInfo(_pathFolder);
                mainDir.CreateSubdirectory(txtNewFolder.Text.Trim());
                txtNewFolder.Text = "";
                showFiles();
            }
            else {
                lblError.Text = "Folder name is not empty";
            }
            
        }

        protected void UpBtn_Click(object sender, ImageClickEventArgs e)
        {
            ChangePath("../");
            showFiles();

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
            showFiles();

            if (ViewState["fpUploadFolderRelativePath"] != null)
            {
                if (ViewState["fpUploadFolderRelativePath"].ToString() != "")
                {

                    _SetFullFolder = _SetFolder + @"\" + ViewState["fpUploadFolderRelativePath"];
                    _SetFullFolder = _SetFullFolder.Replace(@"\", @"/");
                }
            }
        }

        protected void LinkSelect_Click(object sender, EventArgs e)
        {
            this.SelectCheck(true, "chkBox1", 0);
            this.SelectCheck(true, "chkBox2", 1);
            this.SelectCheck(true, "chkBox3", 2);
            this.SelectCheck(true, "chkBox4", 3);

            this.SelectCheck(true, "CheckBox1", 0);
            this.SelectCheck(true, "CheckBox2", 1);
            this.SelectCheck(true, "CheckBox3", 2);
            this.SelectCheck(true, "CheckBox4", 3);

            this.SelectCheck(true, "chxFlash1", 0);
            this.SelectCheck(true, "chxFlash2", 1);
            this.SelectCheck(true, "chxFlash3", 2);
            this.SelectCheck(true, "chxFlash4", 3);

            this.SelectCheck(true, "cbxMedia1", 0);
            this.SelectCheck(true, "cbxMedia2", 1);
            this.SelectCheck(true, "cbxMedia3", 2);
            this.SelectCheck(true, "cbxMedia4", 3);

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

        protected void LinkCancel_Click(object sender, EventArgs e)
        {
            this.SelectCheck(false, "chkBox1", 0);
            this.SelectCheck(false, "chkBox2", 1);
            this.SelectCheck(false, "chkBox3", 2);
            this.SelectCheck(false, "chkBox4", 3);

            this.SelectCheck(false, "CheckBox1", 0);
            this.SelectCheck(false, "CheckBox2", 1);
            this.SelectCheck(false, "CheckBox3", 2);
            this.SelectCheck(false, "CheckBox4", 3);

            this.SelectCheck(false, "chxFlash1", 0);
            this.SelectCheck(false, "chxFlash2", 1);
            this.SelectCheck(false, "chxFlash3", 2);
            this.SelectCheck(false, "chxFlash4", 3);

            this.SelectCheck(false, "cbxMedia1", 0);
            this.SelectCheck(false, "cbxMedia2", 1);
            this.SelectCheck(false, "cbxMedia3", 2);
            this.SelectCheck(false, "cbxMedia4", 3);

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

        protected void LinkDelete_Click(object sender, EventArgs e)
        {
            this.DeleteFile(0, "chkBox1");
            this.DeleteFile(1, "chkBox2");
            this.DeleteFile(2, "chkBox3");
            this.DeleteFile(3, "chkBox4");

            this.DeleteFile(0, "CheckBox1");
            this.DeleteFile(1, "CheckBox2");
            this.DeleteFile(2, "CheckBox3");
            this.DeleteFile(3, "CheckBox4");

            this.DeleteFile(0, "chxFlash1");
            this.DeleteFile(1, "chxFlash2");
            this.DeleteFile(2, "chxFlash3");
            this.DeleteFile(3, "chxFlash4");
                            
            this.DeleteFile(0, "cbxMedia1");
            this.DeleteFile(1, "cbxMedia2");
            this.DeleteFile(2, "cbxMedia3");
            this.DeleteFile(3, "cbxMedia4");

            showFiles();
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

        private void SelectCheck(bool check, string id, int cell)
        {
            foreach (GridViewRow grdRow in this.GridView1.Rows)
            {
                if (grdRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox _SelectAll = (CheckBox)grdRow.Cells[cell].FindControl(id);
                    _SelectAll.Checked = check;
                }
            }
        }

        private void DeleteFile(int cell, string id)
        {
            foreach (GridViewRow grdRow in this.GridView1.Rows)
            {
                if (grdRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox _SelectDel = (CheckBox)grdRow.Cells[cell].FindControl(id);
                    if (id.IndexOf("chkBox") >= 0 || id.IndexOf("cbxMedia") >= 0 || id.IndexOf("cbxFlash") >= 0)
                    {
                        // Delete File
                        if (_SelectDel.Checked == true)
                        {
                            string _pathFolder = _UploadDir + @"\" + ViewState["fpUploadFolderRelativePath"];
                            new Portal.BO.Editoral.Gallery.AlbumImage().DeleteFile(_pathFolder, _SelectDel.ToolTip);
                        }
                    }
                    else
                        // Delete Folder
                        if (id.IndexOf("CheckBox") >= 0)
                        {
                            if (_SelectDel.Checked == true) {
                                string _pathFolder = _UploadDir + @"\" + ViewState["fpUploadFolderRelativePath"] + _SelectDel.ToolTip;
                                new Portal.BO.Editoral.Gallery.AlbumImage().DeleteFolder(_pathFolder);
                            }
                            
                                                    
                        }
                }
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

            showFiles();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            showFiles();
        }

       

    }
}
