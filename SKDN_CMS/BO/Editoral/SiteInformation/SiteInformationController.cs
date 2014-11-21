using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DFISYS.BO.Editoral.ProductColor;
using Nextcom.Citinews.Core.Library;

namespace DFISYS.BO.Editoral.SiteInformation
{
    public class SiteInformationController
    {
        public static void InsertUpdateSIteInformation(SiteInformation site)
        {
            new SiteInformationDAL().proc_SiteInformationInsertUpdate(site);
        }
        public static SiteInformation SelectSiteInformation(int site)
        {
            return ObjectHelper.FillObject<SiteInformation>(new SiteInformationDAL().proc_SiteInformationSelect(site));
        }
    }
}