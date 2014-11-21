using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DFISYS.Core.DAL;

namespace DFISYS.BO.Editoral.UserActionHelper
{
    public static class ActionHelper
    {
        public static DataTable GetListLogAction(string strWhere, int PageSize, int StartRow)
        {
            if (strWhere == null)
                strWhere = "";
            string channel = System.Configuration.ConfigurationManager.AppSettings["channel"];
            DataTable dt;
            using (Channel_LogDB objDB = new Channel_LogDB())
            {
                dt = (DataTable)objDB.CallStoredProcedure("GetListLogAction", new object[] { channel, strWhere, StartRow, PageSize }, new string[] { "@channel", "@strWhere", "@StartIndex", "@PageSize" }, true);
            }
            return dt;
        }

        public static int GetListLogActionCount(string strWhere)
        {
            if (strWhere == null)
                strWhere = "";

            string channel = System.Configuration.ConfigurationManager.AppSettings["channel"];
            DataTable dt;
            using (Channel_LogDB objDB = new Channel_LogDB())
            {
                dt = (DataTable)objDB.CallStoredProcedure("GetListLogActionCount", new object[] { channel, strWhere }, new string[] { "@channel", "@strWhere" }, true);
            }

            return Convert.ToInt32(dt.Rows[0][0]);
        }
    }
    public class Channel_LogDB : MainDB_Base
    {

        public Channel_LogDB()
        {

        }
        public Channel_LogDB(bool init)
            : base(init)
        {

        }
        /// <summary>
        /// Creates a new connection to the database.
        /// </summary>
        /// <returns>An <see cref="System.Data.IDbConnection"/> object.</returns>
        protected override IDbConnection CreateConnection()
        {
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["channel_log"].ToString());
            return con;
        }
        /// <summary>
        /// Returns a SQL statement parameter name that is specific for the data provider.
        /// For example it returns ? for OleDb provider, or @paramName for MS SQL provider.
        /// </summary>
        /// <param name="paramName">The data provider neutral SQL parameter name.</param>
        /// <returns>The SQL statement parameter name.</returns>
        protected  override string CreateSqlParameterName(string paramName)
        {
            return "@" + paramName;
        }

        /// <summary>
        /// Creates a .Net data provider specific parameter name that is used to
        /// create a parameter object and add it to the parameter collection of
        /// <see cref="System.Data.IDbCommand"/>.
        /// </summary>
        /// <param name="baseParamName">The base name of the parameter.</param>
        /// <returns>The full data provider specific parameter name.</returns>
        protected override string CreateCollectionParameterName(string baseParamName)
        {
            return "@" + baseParamName;
        }

        public DataTable vc_Execute_Sql(String sSql)
        {
            IDbCommand cmd = CreateCommand("vc_Execute_Sql", true);
            AddParameter(cmd, "sSql", DbType.String, sSql);
            DataTable table = CreateDataTable(cmd);
            return table;
        }
    }
}
