using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DFISYS.BO.AboutUs
{
    public class AboutUsObject
    {
        private int _id;
        private string _aboutUsImage;
        private string _aboutUs;
        private string _sponsorImage;
        private string _sponsor;
        private string _missionImage;
        private string _mission;
        private bool _isActive;
        private DateTime _createdDate;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string AboutUsImage
        {
            get { return _aboutUsImage; }
            set { _aboutUsImage = value; }
        }

        public string AboutUs
        {
            get { return _aboutUs; }
            set { _aboutUs = value; }
        }

        public string SponsorImage
        {
            get { return _sponsorImage; }
            set { _sponsorImage = value; }
        }

        public string Sponsor
        {
            get { return _sponsor; }
            set { _sponsor = value; }
        }

        public string MissionImage
        {
            get { return _missionImage; }
            set { _missionImage = value; }
        }

        public string Mission
        {
            get { return _mission; }
            set { _mission = value; }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }
    }
}