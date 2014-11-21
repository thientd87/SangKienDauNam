using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Portal.Core.DAL;
using System.Configuration;

namespace Portal.BO {

    public class LogMainDB : MainDB_Base {

        public LogMainDB() {

        }
        public LogMainDB(bool init)
            : base(init) {

        }

        protected override IDbConnection CreateConnection() {
            return new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["LogDB_ConnectionString"].ToString());
        }

        protected  override string CreateSqlParameterName(string paramName)
        {
            return "@" + paramName;
        }

        protected override string CreateCollectionParameterName(string baseParamName) {
            return "@" + baseParamName;
        }

        public DataTable SelectQuery(string sql) {
            IDbCommand cmd = this.CreateCommand(sql, false);
            return this.CreateDataTable(cmd);
        }
    }

    public class Action {

        public static DataTable GetActionHistory(long NewsID) {
            DataTable _result = null;

            using (MainDB db = new MainDB()) {
                _result = db.SelectQuery("SELECT * FROM Action WHERE News_ID = " + NewsID);
            }

            return _result;
        }

        public static string GetNewsTitle(long NewsID) {
            string _result = string.Empty;
            DataTable dt = null;

            using (MainDB db = new MainDB()) {
                dt = db.SelectQuery("SELECT News_Title FROM News WHERE News_ID = " + NewsID);
            }

            if (dt != null && dt.Rows.Count > 0)
                _result = dt.Rows[0][0].ToString();

            return _result;
        }

        public static DataTable VisitByHour(long NewsID) {
            DataTable _result = null;

            using (LogMainDB db = new LogMainDB()) {
                _result = db.SelectQuery(string.Format("select count(VisitHour) AS Visit, VisitHour FROM(select DATEPART(hour, VisitDate) AS VisitHour from VisitorInfor where NewsID = {0}) AS A GROUP BY VisitHour ORDER BY VisitHour ", NewsID));
            }

            return _result;

        }

        public static DataTable VisitByHourV2(long NewsID)
        {
            DataTable _result = null;

            using (LogMainDB db = new LogMainDB())
            {
                _result = db.SelectQuery(string.Format("select sum(PageView) Visit, dateadd(hour,[hour],[date]) [VisitHour] from Article_Hourly where Article_ID = {0} and dateadd(hour,[hour],[date]) > DATEADD(hour, -24, getdate()) group by dateadd(hour,[hour],[date])", NewsID));
            }

            return _result;

        }
    }
}