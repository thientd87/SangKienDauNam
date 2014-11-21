using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DFISYS.Core.DAL
{
    public class DATABASE
    {
        //Khai báo biến thành viên
        private SqlConnection CN;
        
        public static string StrCN = ConfigurationManager.ConnectionStrings["cms_coreConnectionString"].ConnectionString;
        //Phương thức lấy chuỗi kết nối

        //Phương thức lấy biết kết nối
        public SqlConnection GET_CONNECT()
        {
            try
            {
                CN = new SqlConnection(StrCN);
                CN.Open();
                return CN;
            }
            catch (Exception)
            {
                return null;
            }

        }

        //Phương thức đóng kết nối
        public void CLOSE_CN()
        {
            if (CN.State == ConnectionState.Open) CN.Close();
        }
        //Phương thức thực hiện một câu lệnh sql
        public string EXECUTE_SQL(string StrSQL)
        {
            string status = "";
            SqlCommand cmdexec = null;
            try
            {
                cmdexec = new SqlCommand(StrSQL, GET_CONNECT());
                cmdexec.ExecuteNonQuery();
            }
            catch (Exception ex)
            { status = ex.Message; }
            finally
            {
                cmdexec.Dispose();
                CLOSE_CN();
            }
            return status;
        }

        //Thực thi một câu sql có tham số
        public string EXECUTE_SQL(string StrSQL, params object[] pars)
        {
            SqlCommand cmd = null;
            string err = "";
            try
            {

                cmd = new SqlCommand(StrSQL, GET_CONNECT());
                cmd.CommandType = CommandType.Text;
                for (int i = 0; i < pars.Length; i += 2)
                {
                    SqlParameter par = new SqlParameter(pars[i].ToString(), pars[i + 1]);
                    cmd.Parameters.Add(par);
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                err = "Lỗi khi thực thi câu sql: " + ex.Message;
            }
            finally
            {
                cmd.Dispose();
                CLOSE_CN();
            }
            return err;

        }
        /////////////////////////////////
        public DataTable Get_DataTable(string StrSQL, params object[] pars)
        {
            SqlCommand cmd = null; SqlDataAdapter da = null; DataTable dt = null;

            try
            {

                cmd = new SqlCommand(StrSQL, GET_CONNECT());
                cmd.CommandType = CommandType.Text;
                for (int i = 0; i < pars.Length; i += 2)
                {
                    SqlParameter par = new SqlParameter(pars[i].ToString(), pars[i + 1]);
                    cmd.Parameters.Add(par);
                }
                da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (SqlException ex)
            {
                dt = null;
            }
            finally
            {
                cmd.Dispose();
                CLOSE_CN();
            }
            return dt;

        }

        //Phương thức thực thi một câu sql trả về datatable
        public DataTable Get_DataTable(string sql)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, GET_CONNECT());
            DataTable dt = new DataTable();
            try
            {
                //Dien du lieu vao dataTable
                da.Fill(dt);

            }
            catch (Exception)
            {
                dt = null;
            }

            return dt;
        }
        public DataSet Dataset(string SQL)
        {
            SqlDataAdapter da = null;
            DataSet ds = new DataSet();
            try
            {
                da = new SqlDataAdapter(SQL, (new DATABASE()).GET_CONNECT());
                da.Fill(ds, "temp");

            }
            catch (Exception)
            {
                ds = null;
            }

            return ds;
        }
        //Phương thức thực hiện một câu sql trả về giá trị của hàng cột đầu tiên
        public object EXECUTESQL_SCALAR(string StrSQL)
        {
            object result = null;
            SqlCommand cmdexec = null;
            try
            {
                cmdexec = new SqlCommand(StrSQL, GET_CONNECT());
                result = cmdexec.ExecuteScalar();
                if (result == null) result = 0;
            }
            catch (Exception)
            {
                result = null;
            }
            finally
            {
                cmdexec.Dispose();
                CLOSE_CN();
            }
            return result;
        }

        //Tạo phương thức thực thi một thủ tục có n tham số đầu vào và 1 đầu ra
        public object EXECUTE_PROC_INPUT_OUTPUT(string name_proc, string pars_output, SqlDbType SQLDbType_output, int size_output, params object[] pars)
        {
            object result = null; SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(name_proc, GET_CONNECT());
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par = new SqlParameter(pars_output, SQLDbType_output, size_output);
                cmd.Parameters.Add(par);
                cmd.Parameters[pars_output].Direction = ParameterDirection.Output;
                for (int i = 0; i < pars.Length; i += 2)
                {
                    SqlParameter par1 = new SqlParameter(pars[i].ToString(), pars[i + 1]);
                    cmd.Parameters.Add(par1);
                }
                cmd.ExecuteNonQuery();
                result = cmd.Parameters[pars_output].Value;
            }
            catch (SqlException)
            {
                result = null;
            }
            finally
            {
                cmd.Dispose();
                CLOSE_CN();
            }
            return result;
        }
        //Phương thức lấy giá trị của một trường trong bảng
        public object GET_VALUE_FIELD(string name_field, string name_table, string condition)
        {
            string STR_SQL;
            object result = null;
            STR_SQL = "select " + name_field + " from " + name_table + " where " + condition;
            SqlCommand cmd = new SqlCommand(STR_SQL, GET_CONNECT());
            SqlDataReader dr = null;
            try
            {
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result = dr.GetValue(0);
                    break;
                }

            }
            catch (SqlException)
            {
                result = null;
            }
            finally
            {
                cmd.Dispose();
                CLOSE_CN();
            }
            return result;
        }

        public string EXECUTE_PROC(string name_proc, string thongbao, params object[] pars)
        {
            SqlCommand cmd = null; string err = "";
            try
            {

                cmd = new SqlCommand(name_proc, GET_CONNECT());
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < pars.Length; i += 2)
                {
                    SqlParameter par = new SqlParameter(pars[i].ToString(), pars[i + 1]);
                    cmd.Parameters.Add(par);
                }
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {

                err = ex.ToString();
            }
            finally
            {

                cmd.Dispose();
                CLOSE_CN();
            }
            return err;

        }
        ////Tạo phương thức thực thi một thủ tục có n tham số và trả về một datatable
        public DataTable EXECUTE_PROC(string name_proc, params object[] pars)
        {
            SqlCommand cmd = null; SqlDataAdapter da = null; DataTable dt = null;
            try
            {

                cmd = new SqlCommand(name_proc, GET_CONNECT());
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < pars.Length; i += 2)
                {
                    SqlParameter par = new SqlParameter(pars[i].ToString(), pars[i + 1]);
                    cmd.Parameters.Add(par);
                }
                da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                //ds.Clear();
                da.Fill(ds);
                dt = ds.Tables[0];


            }
            catch (SqlException)
            {
                dt = null;
            }
            finally
            {
                da.Dispose();
                cmd.Dispose();
                CLOSE_CN();
            }
            return dt;

        }


        //Thực thi một thủ tục trả về một datatable
        public DataTable EXECUTE_PROC(string name_proc)
        {
            SqlCommand cmd = null; SqlDataAdapter da = null; DataTable dt = null;
            try
            {
                cmd = new SqlCommand();
                cmd.Connection = GET_CONNECT();
                cmd.CommandText = name_proc;
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (SqlException)
            {
                dt = null;
            }
            finally
            {
                da.Dispose();
                cmd.Dispose();
                CLOSE_CN();
            }
            return dt;
        }

    }
}
