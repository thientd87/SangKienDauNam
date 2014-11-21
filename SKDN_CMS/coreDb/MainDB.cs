using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace DFISYS.Core.DAL {

    public class MainDB : MainDB_Base {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainDB"/> class.
        /// </summary>
        public MainDB() {
            // EMPTY
        }

        public DataTable SelectQuery(string sql) {
            IDbCommand cmd = this.CreateCommand(sql, false);
            return this.CreateDataTable(cmd);
        }

        /// <summary>
        /// Execute Normal Nonquery
        /// </summary>
        /// <param name="sql">Query String</param>
        public void AnotherNonQuery(string sql) {
            IDbCommand cmd = this.CreateCommand(sql, false);
            cmd.ExecuteNonQuery();
        }


        public object SelectScalar(string sql, object[] parValues, string[] parNames) {
            IDbCommand cmd = this.CreateCommand(sql, false);
            cmd.CommandText = sql;
            for (int i = 0; i < parValues.Length; i++) {
                cmd.Parameters.Add(new SqlParameter(parNames.GetValue(i).ToString(), parValues.GetValue(i)));
            }
            return cmd.ExecuteScalar();
        }


        public DataTable Select(string sql, object[] parValues, string[] parNames) {
            IDbCommand cmd = this.CreateCommand(sql, false);
            cmd.CommandText = sql;
            for (int i = 0; i < parValues.Length; i++) {
                cmd.Parameters.Add(new SqlParameter(parNames.GetValue(i).ToString(), parValues.GetValue(i)));
            }
            return CreateDataTable(cmd);
        }


        public DataTable CallStoredProcedure(string nameOfStored, bool returnDataTable) {
            IDbCommand cmd = this.CreateCommand(nameOfStored, true);
            if (returnDataTable) {
                return this.CreateDataTable(cmd);
            }
            else {
                cmd.ExecuteNonQuery();
                return null;
            }
        }

        public DataTable CallStoredProcedure(ArrayList listOfPara, string nameOfStored, bool returnDataTable) {
            IDbCommand cmd = this.CreateCommand(nameOfStored, true);
            for (int i = 0; i < listOfPara.Count; i++) {
                SqlParameter sp = (SqlParameter)listOfPara[i];
                AddParameter(cmd, sp.ParameterName, sp.DbType, sp.Value);
            }
            if (returnDataTable) {
                return this.CreateDataTable(cmd);
            }
            else {
                cmd.ExecuteNonQuery();
                return null;
            }
        }

        protected override IDbConnection CreateConnection() {
            return new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["cms_coreConnectionString"].ToString());
        }

        protected internal override string CreateSqlParameterName(string paramName) {
            return "@" + paramName;
        }

        protected override string CreateCollectionParameterName(string baseParamName) {
            return "@" + baseParamName;
        }
    } // End of MainDB class
} // End of namespace

