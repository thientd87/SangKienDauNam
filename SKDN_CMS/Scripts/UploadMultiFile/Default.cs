using DFISYS.BO;
using System;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DFISYS.Scripts.UploadMultiFile
{
    public class Default : Page
    {
        protected Button btnSave;
        protected HtmlInputFile file1;
        protected HtmlInputFile file2;
        protected HtmlInputFile file3;
        protected HtmlInputFile file4;
        protected HtmlInputFile file5;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected Label lblFileError;

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string str = (base.Request.QueryString["currentFolder"] != null) ? base.Request.QueryString["currentFolder"] : string.Empty;
            if (!string.IsNullOrEmpty(str))
            {
                try
                {
                    str = Crypto.DecryptFromHTML(str);
                    string str2 = base.Server.MapPath("~/Images/Uploaded/" + str);
                    if (!str2.EndsWith(@"\"))
                    {
                        str2 = str2 + @"\";
                    }
                    for (int i = 0; i < base.Request.Files.Count; i++)
                    {
                        if (base.Request.Files[i].FileName.Trim().Length > 0)
                        {
                            HttpPostedFile file = base.Request.Files[i];
                            this.UploadFile(str2, file);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Page.RegisterClientScriptBlock("aa", "<script>alert('"+exception.Message+"')</script>");
                    EventLog.WriteEntry("UploadFile", exception.ToString(), EventLogEntryType.Error);
                }
            }
            this.Page.RegisterClientScriptBlock("aa", "<script>file_uploadFinish();</script>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string UploadFile(string _sFolder, HttpPostedFile _File)
        {
            string str = "";
            string fileName = Path.GetFileName(_File.FileName);
            string str3 = fileName.Substring(fileName.LastIndexOf(".") + 1);
            string str4 = UnicodeUtility.UnicodeToKoDauAndGach(HttpUtility.HtmlDecode(fileName.Substring(0, fileName.LastIndexOf("."))));
            if (!Directory.Exists(_sFolder))
            {
                Directory.CreateDirectory(_sFolder);
            }
            try
            {
                int num = 2;
                if (!File.Exists(_sFolder + str4 + "." + str3))
                {
                    goto Label_012B;
                }
                goto Label_0084;
            Label_007C:
                num++;
            Label_0084:;
                if (File.Exists(string.Concat(new object[] { _sFolder, str4, num, ".", str3 })))
                {
                    goto Label_007C;
                }
                _File.SaveAs(string.Concat(new object[] { _sFolder, str4, num, ".", str3 }));
                return string.Concat(new object[] { str4, num, ".", str3 });
            Label_012B:

                if("jpg,gif,png,bmp".IndexOf(str3.ToLower())>0)
                {
                    int fileSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["fileSize"]);
                    int fileWidth = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["fileWidth"]);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(_File.InputStream);
                    if (_File.ContentLength < (fileSize * 1024) && img.Width < fileWidth)
                    {
                        _File.SaveAs(_sFolder + str4 + "." + str3);
                        str = str4 + "." + str3;
                    }
                    else
                    {
                        throw new Exception("Kích thước ảnh lớn hơn cho phép!");
                    }
                    
                }
                else
                {
                    _File.SaveAs(_sFolder + str4 + "." + str3);
                    str = str4 + "." + str3;
                }
                
            }
            catch (Exception exception)
            {
                EventLog.WriteEntry("UploadFile1", exception.ToString(), EventLogEntryType.Error);
            }
            return str;
        }
    }
}

