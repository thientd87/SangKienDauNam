using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;

namespace DFISYS.BO.AboutUs
{
    public class AboutUsDAL
    {
        private readonly string _conn;
        public AboutUsDAL()
        {
            _conn = ConfigurationManager.ConnectionStrings["cms_coreConnectionString"].ToString();
        }

        public IDataReader proc_AboutUsSelect(int Id)
        {
            return SqlHelper.ExecuteReader(_conn, "proc_AboutUsSelect", Id);
        }

        public void proc_AboutUsInsertUpdate(AboutUsObject obj)
        {
            SqlHelper.ExecuteNonQuery(_conn, "proc_AboutUsInsertUpdate", obj.Id, obj.AboutUsImage, obj.AboutUs, obj.SponsorImage, obj.Sponsor, obj.MissionImage, obj.Mission,obj.IsActive);
        }
    }
}