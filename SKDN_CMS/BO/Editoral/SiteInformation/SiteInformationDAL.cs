using System.Configuration;
using System.Data;
using Microsoft.ApplicationBlocks.Data;


namespace DFISYS.BO.Editoral.SiteInformation
{
    public class SiteInformationDAL
    {
        private readonly string _conn;
        public SiteInformationDAL()
        {
            _conn = ConfigurationManager.ConnectionStrings["cms_coreConnectionString"].ToString();
        }


        public void proc_SiteInformationDelete(SiteInformation obj)
        {
            SqlHelper.ExecuteNonQuery(_conn, "proc_SiteInformationDelete", obj.Id);
        }


        public void proc_SiteInformationInsert(SiteInformation obj)
        {
            SqlHelper.ExecuteNonQuery(_conn, "proc_SiteInformationInsert", obj.SiteName, obj.SiteDescription, obj.SiteKeyword, obj.SiteAddress, obj.SiteFooter);
        }


        public void proc_SiteInformationInsertUpdate(SiteInformation obj)
        {
            SqlHelper.ExecuteNonQuery(_conn, "proc_SiteInformationInsertUpdate", obj.SiteName, obj.SiteDescription, obj.SiteKeyword, obj.SiteAddress, obj.SiteFooter, obj.Id);
        }


        public IDataReader proc_SiteInformationSelect(int Id)
        {
            return SqlHelper.ExecuteReader(_conn, "proc_SiteInformationSelect", Id);
        }
    }
}