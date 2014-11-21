using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DFISYS.BO.Editoral.SiteInformation
{
    public class SiteInformation
    {
        public SiteInformation()
        {

        }
        private string _siteName;

        public string SiteName
        {
            get { return _siteName; }
            set { _siteName = value; }
        }

        private string _siteDescription;

        public string SiteDescription
        {
            get { return _siteDescription; }
            set { _siteDescription = value; }
        }

        private string _siteKeyword;

        public string SiteKeyword
        {
            get { return _siteKeyword; }
            set { _siteKeyword = value; }
        }

        private string _siteAddress;

        public string SiteAddress
        {
            get { return _siteAddress; }
            set { _siteAddress = value; }
        }

        private string _siteFooter;

        public string SiteFooter
        {
            get { return _siteFooter; }
            set { _siteFooter = value; }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

    }
}