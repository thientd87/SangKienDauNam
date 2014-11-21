using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.IO;
namespace DFISYS
{
    public static class FileHelper
    {
        // Define default min and max password lengths.
        private static int DEFAULT_MIN_PASSWORD_LENGTH = 8;
        private static int DEFAULT_MAX_PASSWORD_LENGTH = 10;

        // Define supported password characters divided into groups.
        // You can add (or remove) characters to (from) these groups.
        private static string PASSWORD_CHARS_LCASE = "abcdefgijkmnopqrstwxyz";
        private static string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";
        public static string getFileContent(string _path)
        {
            string strResult = "";
            try
            {
                System.IO.StreamReader objRead = System.IO.File.OpenText(_path);
                strResult = objRead.ReadToEnd();
                objRead.Close();
            }
            catch { }
            return strResult;
        }
        public static DateTime DateConvert(string _Date)
        {
            DateTime dtResult = DateTime.Now;
            string[] strDates = _Date.Split("/".ToCharArray());
            string strDateConvert = "";
            if (strDates.Length == 3)
            {
                strDateConvert = strDates[1] + "/" + strDates[0] + "/" + strDates[2];
                dtResult = Convert.ToDateTime(strDateConvert);
            }
            return dtResult;
        }
        public static void delImgFolder(string _sFolder, string _subFolder)
        {
            _sFolder = HttpContext.Current.Request.PhysicalApplicationPath.Replace(@"\", "/") + _sFolder;
            try
            {
                if (System.IO.Directory.Exists(_sFolder + _subFolder + "/"))
                    System.IO.File.Delete(_sFolder + _subFolder + "/");
            }
            catch { }
            try
            {
                if (System.IO.Directory.Exists(_sFolder + "Thumbnails/"))
                    System.IO.File.Delete(_sFolder + "Thumbnails/");
            }
            catch { }
            
        }

        public static void DeleteFolder(string _pathFolder)
        {
            try
            {
                Directory.Delete(_pathFolder, true);
            }
            catch(Exception ex) {  }
        }

        public static void DelImgFile(string _sFolder, string FileName)
        {
            _sFolder = HttpContext.Current.Request.PhysicalApplicationPath.Replace(@"\", "/") + _sFolder;
            try
            {
                System.IO.File.Delete(_sFolder + FileName);
            }
            catch { }
            try
            {
                System.IO.File.Delete(_sFolder + "Thumbnails/" + FileName);
            }
            catch { }
        }

        public static void DelImgFile(string _sFolder, string _subFolder, string FileName)
        {
            _sFolder = HttpContext.Current.Request.PhysicalApplicationPath.Replace(@"\", "/") + _sFolder;
            try
            {
                System.IO.File.Delete(_sFolder + _subFolder + FileName);
            }
            catch (Exception ex){ }
            try
            {
                System.IO.File.Delete(_sFolder + "Thumbnails/" + _subFolder + FileName);
            }
            catch { }
        }

        public static Boolean isPicture(string Filename)
        {
            string[] extImage ={ "gif", "jpg", "png", "bmp", "tif", "mpg", "swf", "dat", "wmv","flv" };
            string strKT = "";
            bool isImage = false;
            strKT = Filename.ToLower().Substring(Filename.LastIndexOf(".") + 1);
            for (int i = 0; i < 5; i++)
                if (strKT == extImage[i])
                {
                    isImage = true;
                    break;
                }
            return isImage;
        }

        public static Boolean isFileMediaObject(string Filename)
        {
            string[] extImage ={ "gif", "jpg", "png", "bmp", "tif", "mpg", "swf", "dat", "wmv", "flv", "mp3" };
            string strKT = "";
            bool isImage = false;
            strKT = Filename.ToLower().Substring(Filename.LastIndexOf(".") + 1);
            for (int i = 0; i< extImage.Length; i++)
                if (strKT == extImage[i])
                {
                    isImage = true;
                    break;
                }
            return isImage;
        }
        //public static void WriteToFile(string strPath,byte[] Buffer)
        //{
        //    // Create a file
        //    FileStream newFile = new FileStream(strPath, FileMode.Create);

        //    // Write data to the file
        //    newFile.Write(Buffer, 0, Buffer.Length);

        //    // Close file
        //    newFile.Close();
        //}
        /// <summary>
        /// Luu file va tao thumb theo dinh dang: Images/uploaded/share/NewsID
        /// va :Images/uploaded/Thumb/NewsID
        /// </summary>
        /// <param name="_sFolder">Thu muc Upload: Images/Uploaded/</param>
        /// <param name="_subFolder">Thu muc share: Share/</param>
        /// <param name="_File">Ten file</param>
        /// <param name="intSize">Chieu dai anh</param>
        /// <returns>Ten file da upload len duoc</returns>
        public static string UploadPicture(string _sFolder,string _subFolder, HttpPostedFile _File, int intSize)
        {
            string _sResult = "";
            string _sFileName = System.IO.Path.GetFileName(_File.FileName);
            string strolder = HttpContext.Current.Request.PhysicalApplicationPath.Replace(@"\", "/") + _sFolder;
            //Nếu đường dẫn chưa tồn tại thì tạo mới
            if (!System.IO.Directory.Exists(strolder + _subFolder + "/"))
                System.IO.Directory.CreateDirectory(strolder + _subFolder + "/");
            //lấy tên file cần upload
            try
            {
                if (isPicture(_sFileName))
                {
                    //upload file vao thu muc can luu
                    _File.SaveAs(strolder + _subFolder + "/" + _sFileName);
                    _sResult = _sFileName;
                    //neu chua co thu muc trong thumb thi tao ra
                    if (!System.IO.Directory.Exists(strolder + "Thumbnails/" + _subFolder.Replace("Share/", "") + "/"))
                        System.IO.Directory.CreateDirectory(strolder + "Thumbnails/" + _subFolder.Replace("Share/","") + "/");
                    //Neu trong truong hop da tao xong anh chinh thi check de tao anh thumb
                    if (System.IO.File.Exists(strolder + _subFolder + "/" + _sFileName))
                    {
                        //Khoi tao thong tin anh chinh
                        System.Drawing.Bitmap normalImg = new System.Drawing.Bitmap(strolder + _subFolder+"/" + _sFileName);
                        int xWidth = normalImg.Width, yHeight = normalImg.Height;
                        if (xWidth > intSize)
                        {
                            //Tinh ti le cua anh can thumb
                            double percent = (double)intSize / xWidth;
                            yHeight = (int)(yHeight * percent);
                            xWidth = intSize;
                        }
                        //Khoi tao thong tin anh thumb
                        System.Drawing.Bitmap pThumbnail = new System.Drawing.Bitmap(normalImg, new System.Drawing.Size(xWidth, yHeight));
                        //Luu anh thumb vao thu muc da dinh.
                        pThumbnail.Save(strolder + "Thumbnails/" + _subFolder.Replace("Share/", "")+"/" + _sFileName, System.Drawing.Imaging.ImageFormat.Bmp);
                    }
                }
            }
            catch { }
            //Trả lại tên file cho hàm
            return _sResult;
        }


        public static string UploadImage(string _sFolder, string _subFolder,string prefix, string _thumbnailsFolder, HttpPostedFile _File, int[] MaxW,int[] MaxH)
        {
            string _sResult = "";
            Random r = new Random();
            string random = r.Next().ToString();

            string _sFileName = random + "_" + System.IO.Path.GetFileName(_File.FileName);
            //string strolder = HttpContext.Current.Request.PhysicalApplicationPath.Replace(@"\", "/") + _sFolder;
            string strolder = HttpContext.Current.Server.MapPath(_sFolder);
            //string sthumbnailes = HttpContext.Current.Request.PhysicalApplicationPath.Replace(@"\", "/") + _thumbnailsFolder;
            string sthumbnailes = HttpContext.Current.Server.MapPath(_thumbnailsFolder);
            if (!System.IO.Directory.Exists(strolder + _subFolder + "/" + prefix + "/"))
                System.IO.Directory.CreateDirectory(strolder + _subFolder + "/" + prefix + "/");
            try
            {
                if (isPicture(_sFileName))
                {
                    _File.SaveAs(strolder + _subFolder + "/" + prefix + "/" + _sFileName);
                    _sResult = _sFileName;
                    //if (!System.IO.Directory.Exists(sthumbnailes + _subFolder + "/" + prefix + "/"))
                        //System.IO.Directory.CreateDirectory(sthumbnailes + _subFolder + "/" + prefix + "/");
                    if (System.IO.File.Exists(strolder + _subFolder + "/" + prefix + "/" + _sFileName))
                    {
                        System.Drawing.Bitmap normalImg = new System.Drawing.Bitmap(strolder + _subFolder + "/" + prefix + "/" + _sFileName);
                        int xWidth = normalImg.Width, yHeight = normalImg.Height;
                        int NewW = xWidth, NewH = yHeight;
                        if (xWidth >= yHeight)
                        {
                            for (int i = 0; i < MaxH.Length; i++)
                            {
                                if (xWidth > MaxW[i])
                                {
                                    double percen = (double)MaxW[i] / xWidth;
                                    NewW = MaxW[i];
                                    NewH = (int)(yHeight * percen);
                                }
                                //System.Drawing.Bitmap pThumbnail = new System.Drawing.Bitmap(normalImg, new System.Drawing.Size(NewW, NewH));
                                //pThumbnail.Save(sthumbnailes + _subFolder + "/" + prefix + "/" + i.ToString() + "_" + _sFileName, System.Drawing.Imaging.ImageFormat.Bmp);
                            }                            
                        }
                        else
                        {
                            for (int i = 0; i < MaxW.Length; i++)
                            {
                                if (yHeight > MaxH[i])
                                {
                                    double percen = (double)MaxH[i] / yHeight;
                                    NewH = MaxH[i];
                                    NewW = (int)(xWidth * percen);
                                }
                                //System.Drawing.Bitmap pThumbnail = new System.Drawing.Bitmap(normalImg, new System.Drawing.Size(NewW, NewH));
                                //pThumbnail.Save(sthumbnailes + _subFolder + "/" + prefix + "/" + i.ToString() + "_" + _sFileName, System.Drawing.Imaging.ImageFormat.Bmp);
                            }
                        }                       
                    }
                }
            }
            catch { }
            //Trả lại tên file cho hàm
            return _sResult;
        }

        public static string UploadMediaObject(string _sFolder, string _subFolder, HttpPostedFile _File, int intSize)
        {
            string _sResult = "";
            string _sFileName = System.IO.Path.GetFileName(_File.FileName);
            string strolder = HttpContext.Current.Request.PhysicalApplicationPath.Replace(@"\", "/") + _sFolder;
            //Nếu đường dẫn chưa tồn tại thì tạo mới
            if (!System.IO.Directory.Exists(strolder + _subFolder + "/"))
                System.IO.Directory.CreateDirectory(strolder + _subFolder + "/");
            //lấy tên file cần upload
            try
            {
               
                //upload file vao thu muc can luu
                _File.SaveAs(strolder + _subFolder + "/" + _sFileName);
                _sResult = _sFileName;
            }
            catch { }
            //Trả lại tên file cho hàm
            return _sResult;
        }
        public static string Generate()
        {
            return Generate(DEFAULT_MIN_PASSWORD_LENGTH,
                DEFAULT_MAX_PASSWORD_LENGTH);
        }
        public static string Generate(int length)
        {
            return Generate(length, length);
        }
        public static string Generate(int minLength, int maxLength)
        {
            // Make sure that input parameters are valid.
            if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
                return null;

            // Create a local array containing supported password characters
            // grouped by types. You can remove character groups from this
            // array, but doing so will weaken the password strength.
            char[][] charGroups = new char[][] 
		{
			PASSWORD_CHARS_LCASE.ToCharArray(),
			PASSWORD_CHARS_UCASE.ToCharArray(),
			//PASSWORD_CHARS_NUMERIC.ToCharArray(),
			//PASSWORD_CHARS_SPECIAL.ToCharArray()
		};

            // Use this array to track the number of unused characters in each
            // character group.
            int[] charsLeftInGroup = new int[charGroups.Length];

            // Initially, all characters in each group are not used.
            for (int i = 0; i < charsLeftInGroup.Length; i++)
                charsLeftInGroup[i] = charGroups[i].Length;

            // Use this array to track (iterate through) unused character groups.
            int[] leftGroupsOrder = new int[charGroups.Length];

            // Initially, all character groups are not used.
            for (int i = 0; i < leftGroupsOrder.Length; i++)
                leftGroupsOrder[i] = i;

            // Because we cannot use the default randomizer, which is based on the
            // current time (it will produce the same "random" number within a
            // second), we will use a random number generator to seed the
            // randomizer.

            // Use a 4-byte array to fill it with random bytes and convert it then
            // to an integer value.
            byte[] randomBytes = new byte[4];

            // Generate 4 random bytes.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            // Convert 4 bytes into a 32-bit integer value.
            int seed = (randomBytes[0] & 0x7f) << 24 |
                randomBytes[1] << 16 |
                randomBytes[2] << 8 |
                randomBytes[3];

            // Now, this is real randomization.
            Random random = new Random(seed);

            // This array will hold password characters.
            char[] password = null;

            // Allocate appropriate memory for the password.
            if (minLength < maxLength)
                password = new char[random.Next(minLength, maxLength + 1)];
            else
                password = new char[minLength];

            // Index of the next character to be added to password.
            int nextCharIdx;

            // Index of the next character group to be processed.
            int nextGroupIdx;

            // Index which will be used to track not processed character groups.
            int nextLeftGroupsOrderIdx;

            // Index of the last non-processed character in a group.
            int lastCharIdx;

            // Index of the last non-processed group.
            int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

            // Generate password characters one at a time.
            for (int i = 0; i < password.Length; i++)
            {
                // If only one character group remained unprocessed, process it;
                // otherwise, pick a random character group from the unprocessed
                // group list. To allow a special character to appear in the
                // first position, increment the second parameter of the Next
                // function call by one, i.e. lastLeftGroupsOrderIdx + 1.
                if (lastLeftGroupsOrderIdx == 0)
                    nextLeftGroupsOrderIdx = 0;
                else
                    nextLeftGroupsOrderIdx = random.Next(0,
                        lastLeftGroupsOrderIdx);

                // Get the actual index of the character group, from which we will
                // pick the next character.
                nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

                // Get the index of the last unprocessed characters in this group.
                lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                // If only one unprocessed character is left, pick it; otherwise,
                // get a random character from the unused character list.
                if (lastCharIdx == 0)
                    nextCharIdx = 0;
                else
                    nextCharIdx = random.Next(0, lastCharIdx + 1);

                // Add this character to the password.
                password[i] = charGroups[nextGroupIdx][nextCharIdx];

                // If we processed the last character in this group, start over.
                if (lastCharIdx == 0)
                    charsLeftInGroup[nextGroupIdx] =
                        charGroups[nextGroupIdx].Length;
                // There are more unprocessed characters left.
                else
                {
                    // Swap processed character with the last unprocessed character
                    // so that we don't pick it until we process all characters in
                    // this group.
                    if (lastCharIdx != nextCharIdx)
                    {
                        char temp = charGroups[nextGroupIdx][lastCharIdx];
                        charGroups[nextGroupIdx][lastCharIdx] =
                            charGroups[nextGroupIdx][nextCharIdx];
                        charGroups[nextGroupIdx][nextCharIdx] = temp;
                    }
                    // Decrement the number of unprocessed characters in
                    // this group.
                    charsLeftInGroup[nextGroupIdx]--;
                }

                // If we processed the last group, start all over.
                if (lastLeftGroupsOrderIdx == 0)
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                // There are more unprocessed groups left.
                else
                {
                    // Swap processed group with the last unprocessed group
                    // so that we don't pick it until we process all groups.
                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] =
                            leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }
                    // Decrement the number of unprocessed groups.
                    lastLeftGroupsOrderIdx--;
                }
            }

            // Convert password characters into a string and return the result.
            return new string(password);
        }

        
        public static Boolean isFileMedia(string Filename)
        {
            string[] extImage ={ "swf", "dat", "wmv", "flv", "mp3" };
            string strKT = "";
            bool isImage = false;
            strKT = Filename.ToLower().Substring(Filename.LastIndexOf(".") + 1);
            for (int i = 0; i < extImage.Length; i++)
                if (strKT == extImage[i])
                {
                    isImage = true;
                    break;
                }
            return isImage;
        }
    }
}
