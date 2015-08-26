using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nextcom.Citinews.Core.Library;

namespace DFISYS.BO.AboutUs
{
    public class AboutUsController
    {
        public static void InsertUpdateAboutUs(AboutUsObject site)
        {
            new AboutUsDAL().proc_AboutUsInsertUpdate(site);
        }
        public static AboutUsObject SelectSiteAboutUs(int site)
        {
            return ObjectHelper.FillObject<AboutUsObject>(new AboutUsDAL().proc_AboutUsSelect(site));
        }
    }
}