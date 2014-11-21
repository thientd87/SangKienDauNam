using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

using System.ComponentModel;

namespace Portal.BO.Editoral.Gallery
{
    public class AlbumImage
    {
        public AlbumImage() { }

        /*public string UploadImage(string _sFolder, System.Web.HttpPostedFile postFile)
        {
            string _fileName = "";
            if (checkImageExtension(postFile.FileName, postFile.ContentType))
            {
                string _path = _sFolder;
                ThumbnailUtilities _upload = new ThumbnailUtilities();
                _fileName = _upload.UploadPicture(_path, postFile);
                _upload.CreateThumbnail(150, _upload.CurrentPath, _fileName, _path);
                _upload.CreateChildThumbnail(90, _upload.CurrentPath, _fileName, _path);

            }
            else {
                if (checkExtension(postFile.FileName, postFile.ContentType))
                {
                    UploadFile(_sFolder, postFile);
                }
            }
                

            return _fileName;
            
        }*/

        public string UploadImage(string _sFolder, HttpPostedFile _File)
        {
            string _sResult = "";
            string _sFileName = System.IO.Path.GetFileName(_File.FileName);
            _sFolder = HttpContext.Current.Request.PhysicalApplicationPath.Replace(@"\", "/") + _sFolder;
            //Nếu đường dẫn chưa tồn tại thì tạo mới
            if (!System.IO.Directory.Exists(_sFolder))
                System.IO.Directory.CreateDirectory(_sFolder);
            //lấy tên file cần upload
            try
            {
                //upload
                _File.SaveAs(_sFolder + _sFileName);
                _sResult = _sFileName;

            }
            catch { }
            //Trả lại tên file cho hàm
            return _sResult;
        }


        public string UploadFile(string _sFolder, HttpPostedFile _File)
        {
            string _sResult = "";
            string _sFileName = System.IO.Path.GetFileName(_File.FileName);
            _sFolder = HttpContext.Current.Request.PhysicalApplicationPath.Replace(@"\", "/") + _sFolder;
            //Nếu đường dẫn chưa tồn tại thì tạo mới
            if (!System.IO.Directory.Exists(_sFolder))
                System.IO.Directory.CreateDirectory(_sFolder);
            //lấy tên file cần upload
            try
            {
                //upload
                _File.SaveAs(_sFolder + _sFileName);
                _sResult = _sFileName;
                
            }
            catch { }
            //Trả lại tên file cho hàm
            return _sResult;
        }
        
        
        public string SetFolder() {
            return HttpContext.Current.User.Identity.Name;
        }

        private String GetFileSize(long Lenght)
        {
            if (Lenght > 1000000) return ((Lenght / 1000000).ToString() + " Mb");
            else if (Lenght > 1000) return ((Lenght / 1000).ToString() + " Kb");
            else return (Lenght.ToString() + " b");

        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public DataTable GetListImage(string _pathFolder, string searchPattern)
        {
            DirectoryInfo mainDir = new DirectoryInfo(_pathFolder);
            FileSystemInfo[] FSInfos;
            if(searchPattern == null || searchPattern == "")
                FSInfos = mainDir.GetFileSystemInfos() ;
            else
                FSInfos = mainDir.GetFileSystemInfos("*"+searchPattern+"*");

            DataTable FileSystemInfosTable = new DataTable();
            DataRow myDataRow;
            FileSystemInfosTable.Columns.Add(new DataColumn("FileName", Type.GetType("System.String"))); //FileName
            FileSystemInfosTable.Columns.Add(new DataColumn("FileShortName", Type.GetType("System.String"))); //FileName
            FileSystemInfosTable.Columns.Add(new DataColumn("IsFile", Type.GetType("System.Boolean"))); //FileName
            FileSystemInfosTable.Columns.Add(new DataColumn("Modified", Type.GetType("System.DateTime"))); //Last modified date and time
            FileSystemInfosTable.Columns.Add(new DataColumn("Avatar", Type.GetType("System.String"))); //Last modified date and time
            FileSystemInfosTable.Columns.Add(new DataColumn("Size", Type.GetType("System.String"))); //Size
            FileSystemInfosTable.Columns.Add(new DataColumn("FileName_IsFile", Type.GetType("System.String"))); //Size
            
            //Modified 23012008
            FileSystemInfosTable.Columns.Add(new DataColumn("FullName", Type.GetType("System.String"))); //FullName
            //Modified 23012008

            string filename;
            myDataRow = FileSystemInfosTable.NewRow();
            foreach (FileSystemInfo FSInfo in FSInfos)
            {
                filename = FSInfo.ToString();
                if (filename == "Thumbnails")
                    continue;


                if (FSInfo.ToString().Length < 50)
                {
                    myDataRow["FileName"] = FSInfo.ToString();
                    myDataRow["FileShortName"] = FSInfo.ToString();
                }
                else
                {
                    myDataRow["FileShortName"] = FSInfo.ToString().Substring(0, 50) + "...";
                    myDataRow["FileName"] = FSInfo.ToString();
                }

                // Kiem tra xem Image nay da Thumbnails chua thi Create Image trong Thumbnails
                /*if (!File.Exists(_pathFolder + @"/Thumbnails/" + FSInfo.ToString()) && checkImageExtension(FSInfo.ToString()))
                {
                    ThumbnailUtilities _upload = new ThumbnailUtilities();
                    _upload.CreateThumbnail(150, _upload.CurrentPath, FSInfo.ToString(), @"Images/Uploaded/" + SetFolder());
                    _upload.CreateChildThumbnail(90, _upload.CurrentPath, FSInfo.ToString(), @"Images/Uploaded/" + SetFolder());
                }*/

                myDataRow["Modified"] = FSInfo.LastWriteTime;


                switch (FSInfo.GetType().ToString())
                {
                    case "System.IO.DirectoryInfo":
                        {

                            myDataRow["IsFile"] = false;
                            myDataRow["Avatar"] = @"/images/icons/folder.gif";
                            myDataRow["Size"] = "Dir";
                            myDataRow["FileName_IsFile"] = myDataRow["FileName"] + "," + myDataRow["IsFile"];
                            myDataRow["FullName"] = "";
                            FileSystemInfosTable.Rows.Add(myDataRow);
                            myDataRow = FileSystemInfosTable.NewRow();
                            break;
                        }
                    case "System.IO.FileInfo":
                        {
                            if (checkExtension(FSInfo.Name))
                            {
                                myDataRow["IsFile"] = true;
                                
                                if (checkImageExtension(FSInfo.ToString()))
                                    myDataRow["Avatar"] = @"/images/icons/document_add.png";
                                else
                                    if (checkFlashExtension(FSInfo.ToString()))
                                        myDataRow["Avatar"] = @"/images/icons/flash.jpg";
                                    else
                                        if (checkWindowMediaExtension(FSInfo.ToString()))
                                            myDataRow["Avatar"] = @"/images/icons/windowmedia.jpg";

                                myDataRow["FileName_IsFile"] = myDataRow["FileName"] + "," + myDataRow["IsFile"];
                                myDataRow["Size"] = GetFileSize(((FileInfo)FSInfo).Length);

                                string strolder = HttpContext.Current.Request.PhysicalApplicationPath.Replace(@"\", "/");
                                myDataRow["FullName"] = FSInfo.FullName.Replace("\\", "/").Replace(strolder, System.Configuration.ConfigurationSettings.AppSettings["ImageUrl"]);


                                FileSystemInfosTable.Rows.Add(myDataRow);
                                myDataRow = FileSystemInfosTable.NewRow();
                            }

                            break;
                        }
                }
            }

            
            return FileSystemInfosTable;

        }

        public DataTable GetImage(string _pathFolder, string searchPattern)
        {
            
            DirectoryInfo mainDir = new DirectoryInfo(_pathFolder);
            //Get a list of files and folders in this directory
            //FileSystemInfo[] FSInfos = mainDir.GetFileSystemInfos();
            FileSystemInfo[] FSInfos;
            if (searchPattern == null || searchPattern == "")
                FSInfos = mainDir.GetFileSystemInfos();
            else
                FSInfos = mainDir.GetFileSystemInfos("*" + searchPattern + "*");
            
            DataTable FileSystemInfosTable = new DataTable(); 
            DataRow myDataRow;

            FileSystemInfosTable.Columns.Add(new DataColumn("FileName1", Type.GetType("System.String"))); //FileName
            FileSystemInfosTable.Columns.Add(new DataColumn("FileName2", Type.GetType("System.String"))); //FileName
            FileSystemInfosTable.Columns.Add(new DataColumn("FileName3", Type.GetType("System.String"))); //FileName
            FileSystemInfosTable.Columns.Add(new DataColumn("FileName4", Type.GetType("System.String"))); //FileName

            FileSystemInfosTable.Columns.Add(new DataColumn("FileShortName1", Type.GetType("System.String"))); //FileName
            FileSystemInfosTable.Columns.Add(new DataColumn("FileShortName2", Type.GetType("System.String"))); //FileName
            FileSystemInfosTable.Columns.Add(new DataColumn("FileShortName3", Type.GetType("System.String"))); //FileName
            FileSystemInfosTable.Columns.Add(new DataColumn("FileShortName4", Type.GetType("System.String"))); //FileName


            FileSystemInfosTable.Columns.Add(new DataColumn("IsFile1", Type.GetType("System.Boolean"))); //FileName
            FileSystemInfosTable.Columns.Add(new DataColumn("IsFile2", Type.GetType("System.Boolean"))); //FileName
            FileSystemInfosTable.Columns.Add(new DataColumn("IsFile3", Type.GetType("System.Boolean"))); //FileName
            FileSystemInfosTable.Columns.Add(new DataColumn("IsFile4", Type.GetType("System.Boolean"))); //FileName

            FileSystemInfosTable.Columns.Add(new DataColumn("Modified1", Type.GetType("System.DateTime"))); //Last modified date and time
            FileSystemInfosTable.Columns.Add(new DataColumn("Modified2", Type.GetType("System.DateTime"))); //Last modified date and time
            FileSystemInfosTable.Columns.Add(new DataColumn("Modified3", Type.GetType("System.DateTime"))); //Last modified date and time
            FileSystemInfosTable.Columns.Add(new DataColumn("Modified4", Type.GetType("System.DateTime"))); //Last modified date and time

            string filename;
            int i = 1;
            myDataRow = FileSystemInfosTable.NewRow();
            foreach (FileSystemInfo FSInfo in FSInfos)
            {
                filename = FSInfo.ToString();
                if (filename == "Thumbnails")
                    continue;


                if (FSInfo.ToString().Length < 15)
                {
                    myDataRow["FileName" + i.ToString()] = FSInfo.ToString();
                    myDataRow["FileShortName" + i.ToString()] = FSInfo.ToString();
                }
                else {
                    myDataRow["FileShortName" + i.ToString()] = FSInfo.ToString().Substring(0, 15) + "...";
                    myDataRow["FileName" + i.ToString()] = FSInfo.ToString();
                }

                // Kiem tra xem Image nay da Thumbnails chua thi Create Image trong Thumbnails
                /*if (!File.Exists(_pathFolder + @"/Thumbnails/" + FSInfo.ToString()) && checkImageExtension(FSInfo.ToString()))
                {
                    ThumbnailUtilities _upload = new ThumbnailUtilities();
                    _upload.CreateThumbnail(150, _upload.CurrentPath, FSInfo.ToString(), @"Images/Uploaded/" + SetFolder());
                    _upload.CreateChildThumbnail(90, _upload.CurrentPath, FSInfo.ToString(), @"Images/Uploaded/" + SetFolder());
                }*/

                myDataRow["Modified" + i.ToString()] = FSInfo.LastWriteTime;
                   

                switch (FSInfo.GetType().ToString())
                {
                    case "System.IO.DirectoryInfo":
                        {
                            
                            myDataRow["IsFile" + i.ToString()] = false;
                            i += 1;
                            if (i % 5 != 0)
                            {
                                //Tiep tuc them anh vao cot tiep theo:
                                continue;
                            }
                            FileSystemInfosTable.Rows.Add(myDataRow);
                            i = 1;
                            myDataRow = FileSystemInfosTable.NewRow();
                            break;
                        }
                    case "System.IO.FileInfo":
                        {
                            if (checkExtension(FSInfo.Name)) {

                                myDataRow["IsFile" + i.ToString()] = true;
                                i += 1;
                                if (i % 5 != 0)
                                {
                                    //Tiep tuc them anh vao cot tiep theo:
                                    continue;
                                }
                                FileSystemInfosTable.Rows.Add(myDataRow);
                                i = 1;
                                myDataRow = FileSystemInfosTable.NewRow();
                            }
                            
                            break;
                        }
                }
            }

            if ((i % 5 != 0) && myDataRow["FileName1"].ToString() != "")
            {
                FileSystemInfosTable.Rows.Add(myDataRow);
            }

            return FileSystemInfosTable;
        }

        public bool checkExtension(string fileName, string contentType)
        {
            char[] _char = fileName.ToUpper().ToCharArray();
            string[] _extension ={ "JPG", "BMP", "PNG", "GIF", "SWF", "AVI", "WMV", "MP3","WMA","FLV" };
            bool _check = false;
            int _index = 0;
            for (int i = 0; i <= _extension.Length - 1; i++)
            {
                _index = fileName.LastIndexOf(".");
                string strkt = fileName.ToUpper().Substring(_index + 1);
                if (strkt == _extension[i] && ( contentType.ToLower().IndexOf("image") >= 0 || contentType.ToLower() == "application/x-shockwave-flash"
                    || contentType.ToLower().IndexOf("audio") >= 0 || contentType.ToLower().IndexOf("video") >= 0 || contentType.ToLower().IndexOf("application/octet-stream") >= 0)
                    )
                {
                    _check = true;
                    break;
                }
            }
            return _check;
        }

        public bool checkExtension(string fileName)
        {
            char[] _char = fileName.ToUpper().ToCharArray();
            string[] _extension ={ "JPG", "BMP", "PNG", "GIF", "SWF", "AVI", "WMV", "MP3", "WMA", "FLV" };
            bool _check = false;
            int _index = 0;
            for (int i = 0; i <= _extension.Length - 1; i++)
            {
                _index = fileName.LastIndexOf(".");
                string strkt = fileName.ToUpper().Substring(_index + 1);
                if (strkt == _extension[i])
                {
                    _check = true;
                    break;
                }
            }
            return _check;
        }

        public bool checkFlVExtension(string fileName)
        {
            char[] _char = fileName.ToUpper().ToCharArray();
            string[] _extension ={  "FLV" };
            bool _check = false;
            int _index = 0;
            for (int i = 0; i <= _extension.Length - 1; i++)
            {
                _index = fileName.LastIndexOf(".");
                string strkt = fileName.ToUpper().Substring(_index + 1);
                if (strkt == _extension[i])
                {
                    _check = true;
                    break;
                }
            }
            return _check;
        }

        public bool checkFlVExtension(string fileName, string contentType)
        {
            char[] _char = fileName.ToUpper().ToCharArray();
            string[] _extension ={ "FLV" };
            bool _check = false;
            int _index = 0;
            for (int i = 0; i <= _extension.Length - 1; i++)
            {
                _index = fileName.LastIndexOf(".");
                string strkt = fileName.ToUpper().Substring(_index + 1);
                if (strkt == _extension[i] && (contentType.ToLower().IndexOf("video") >= 0 || contentType.ToLower().IndexOf("application/octet-stream") >= 0))
                {
                    _check = true;
                    break;
                }
            }
            return _check;
        }

        public bool checkWindowMediaExtension(string fileName)
        {
            char[] _char = fileName.ToUpper().ToCharArray();
            string[] _extension ={ "AVI", "WMV", "MP3", "WMA" };
            bool _check = false;
            int _index = 0;
            for (int i = 0; i <= _extension.Length - 1; i++)
            {
                _index = fileName.LastIndexOf(".");
                string strkt = fileName.ToUpper().Substring(_index + 1);
                if (strkt == _extension[i] )
                {
                    _check = true;
                    break;
                }
            }
            return _check;
        }

        public bool checkWindowMediaExtension(string fileName, string contentType)
        {
            char[] _char = fileName.ToUpper().ToCharArray();
            string[] _extension ={ "AVI", "WMV", "MP3", "WMA" };
            bool _check = false;
            int _index = 0;
            for (int i = 0; i <= _extension.Length - 1; i++)
            {
                _index = fileName.LastIndexOf(".");
                string strkt = fileName.ToUpper().Substring(_index + 1);
                if (strkt == _extension[i] && contentType.ToLower().IndexOf("video") >= 0)
                {
                    _check = true;
                    break;
                }
            }
            return _check;
        }


        public bool checkImageExtension(string fileName, string contentType)
        {
            char[] _char = fileName.ToUpper().ToCharArray();
            string[] _extension ={ "JPG", "BMP", "PNG", "GIF" };
            bool _check = false;
            int _index = 0;
            for (int i = 0; i <= _extension.Length - 1; i++)
            {
                _index = fileName.LastIndexOf(".");
                string strkt = fileName.ToUpper().Substring(_index + 1);
                if (strkt == _extension[i] && contentType.ToLower().IndexOf("image") >= 0)
                {
                    _check = true;
                    break;
                }
            }
            return _check;
        }

        public bool checkImageExtension(string fileName)
        {
            char[] _char = fileName.ToUpper().ToCharArray();
            string[] _extension ={ "JPG", "BMP", "PNG", "GIF" };
            bool _check = false;
            int _index = 0;
            for (int i = 0; i <= _extension.Length - 1; i++)
            {
                _index = fileName.LastIndexOf(".");
                string strkt = fileName.ToUpper().Substring(_index + 1);
                if (strkt == _extension[i])
                {
                    _check = true;
                    break;
                }
            }
            return _check;
        }

        public bool checkFlashExtension(string fileName)
        {
            char[] _char = fileName.ToUpper().ToCharArray();
            string[] _extension ={ "SWF","FLV" };
            bool _check = false;
            int _index = 0;
            for (int i = 0; i <= _extension.Length - 1; i++)
            {
                _index = fileName.LastIndexOf(".");
                string strkt = fileName.ToUpper().Substring(_index + 1);
                if (strkt == _extension[i])
                {
                    _check = true;
                    break;
                }
            }
            return _check;
        }

        public bool checkFlashExtension(string fileName, string contentType)
        {
            char[] _char = fileName.ToUpper().ToCharArray();
            string[] _extension ={ "SWF", "FLV" };
            bool _check = false;
            int _index = 0;
            for (int i = 0; i <= _extension.Length - 1; i++)
            {
                _index = fileName.LastIndexOf(".");
                string strkt = fileName.ToUpper().Substring(_index + 1);
                if (strkt == _extension[i] && contentType.ToLower().IndexOf("application/x-shockwave-flash") >= 0)
                {
                    _check = true;
                    break;
                }
            }
            return _check;
        }

        public void DeleteFile(string _pathFolder, string _pathFile)
        {
            try
            {
                FileInfo file = new FileInfo(_pathFolder + _pathFile);
                file.Delete();

                file = new FileInfo(_pathFolder + @"Thumbnails\" + _pathFile);
                file.Delete();

                file = new FileInfo(_pathFolder + @"Thumbnails\ChildThumb\" + _pathFile);
                file.Delete();
            }
            catch{}

            
        }

        public void DeleteFolder(string _pathFolder)
        {
            try {
                if (Directory.Exists(_pathFolder)) Directory.Delete(_pathFolder, true);
            }
            catch { }
        }
        
    }

    
}
