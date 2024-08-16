using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer
{
    public class DataBaseAccess
    {

        private SqlConnection DbConn = new SqlConnection();
        private SqlDataAdapter DbAdapter = new SqlDataAdapter();
        private SqlCommand DbCommand = new SqlCommand();
        private SqlTransaction DbTran;
        
        private string strConnString = "";

        public DataBaseAccess(IConfiguration configuration)
        {
            strConnString = configuration.GetConnectionString("DefaultConnection");
        }

        public DataBaseAccess(string connection)
        {
        }


        public void setConnString(string strConn)
        {
            try
            {
                strConnString = strConn;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public string getConnString()
        {
            try
            {
                return strConnString;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        private void createConn()
        {
            try
            {

                DbConn.ConnectionString = strConnString;
                DbConn.Open();

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception exp)
            {
                // Response.write(exp.Message);

                throw exp;
            }
        }
        public void closeConnection()
        {
            try
            {
                if (DbConn.State != 0)
                    DbConn.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void beginTrans()
        {
            try
            {
                if (DbTran == null)
                {
                    if (DbConn.State == 0)
                    {
                        createConn();
                    }

                    DbTran = DbConn.BeginTransaction();
                    DbCommand.Transaction = DbTran;
                    DbAdapter.SelectCommand.Transaction = DbTran;
                    DbAdapter.InsertCommand.Transaction = DbTran;
                    DbAdapter.UpdateCommand.Transaction = DbTran;
                    DbAdapter.DeleteCommand.Transaction = DbTran;

                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public void commitTrans()
        {
            try
            {
                if (DbTran != null)
                {
                    DbTran.Commit();
                    DbTran = null;
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public void rollbackTrans()
        {
            try
            {
                if (DbTran != null)
                {
                    DbTran.Rollback();
                    DbTran = null;
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// Fills the Dataset dset and its Table tblname via stored procedure provided as spName arguement.Takes Parameters as param
        /// </summary>
        /// <param name="dSet"></param>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <param name="tblName"></param>
        /// 
        public void selectStoredProcedure(DataSet dSet, string spName, Hashtable param, string tblName, string commandtimeout)
        {

            DbCommand = new SqlCommand();
            try
            {
                if (DbConn.State == 0)
                {
                    createConn();
                }
                DbCommand.Connection = DbConn;
                DbCommand.CommandText = spName;
                DbCommand.CommandTimeout = 540;
                DbCommand.CommandType = CommandType.StoredProcedure;
                foreach (string para in param.Keys)
                {
                    DbCommand.Parameters.AddWithValue(para, param[para]);

                }

                DbAdapter.SelectCommand = (DbCommand);
                DbAdapter.Fill(dSet, tblName);
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            catch (Exception exp)
            {

                throw exp;
            }
            finally
            {
                DbAdapter.Dispose();
                DbConn.Close();
            }
        }
        public void selectStoredProcedure(DataSet dSet, string spName, Hashtable param, string tblName)
        {

            DbCommand = new SqlCommand();
            try
            {
                if (DbConn.State == 0)
                {
                    createConn();
                }
                DbCommand.Connection = DbConn;
                DbCommand.CommandText = spName;
                DbCommand.CommandTimeout = 600;//240;
                DbCommand.CommandType = CommandType.StoredProcedure;
                foreach (string para in param.Keys)
                {
                    DbCommand.Parameters.AddWithValue(para, param[para]);

                }

                DbAdapter.SelectCommand = (DbCommand);
                DbAdapter.Fill(dSet, tblName);
                DbConn.Close();
            }
            catch (SqlException ex)
            {
                DbConn.Close();
                throw ex;
            }
            catch (Exception exp)
            {
                DbConn.Close();
                throw exp;
            }
            finally
            {
                DbAdapter.Dispose();
                DbConn.Close();
            }
        }

        public void selectStoredProcedure(DataSet dSet, string spName, SqlCommand comm, string tblName)
        {
            try
            {
                if (DbConn.State == 0)
                {
                    createConn();
                }
                DbCommand.CommandTimeout = 0;
                comm.Connection = DbConn;
                comm.CommandText = spName;
                comm.CommandTimeout = 0;
                comm.CommandType = CommandType.StoredProcedure;
                DbAdapter.SelectCommand = comm;
                DbAdapter.Fill(dSet, tblName);
                comm.Parameters.Clear();
                DbAdapter.Dispose();
                DbConn.Close();
            }
            catch (SqlException ex)
            {
                comm.Parameters.Clear();
                DbAdapter.Dispose();
                DbConn.Close();
                throw ex;
            }
            catch (Exception e)
            {
                comm.Parameters.Clear();
                DbAdapter.Dispose();
                DbConn.Close();
                throw e;
            }
            finally
            {
                DbAdapter.Dispose();
                DbConn.Close();
            }
        }
        public void selectStoredProcedure(DataSet dSet, string spName, string tblName)
        {
            try
            {
                if (DbConn.State == 0)
                {
                    createConn();
                }
                DbCommand.Connection = DbConn;
                DbCommand.CommandText = spName;
                DbCommand.CommandTimeout = 540;
                DbCommand.CommandType = CommandType.StoredProcedure;
                DbAdapter.SelectCommand = DbCommand;
                DbAdapter.Fill(dSet, tblName);
            }
            catch (SqlException ex)
            {
                DbAdapter.Dispose();
                DbConn.Close();
                throw ex;
            }

            catch (Exception exp)
            {
                DbAdapter.Dispose();
                DbConn.Close();
                throw exp;
            }
            finally
            {
                DbAdapter.Dispose();
                DbConn.Close();
            }
        }

        public void selectQuery(DataSet dSet, string query, string tblName)
        {
            try
            {
                if (DbConn.State == 0)
                {
                    createConn();
                }
                DbCommand.CommandTimeout = 600;
                DbCommand.Connection = DbConn;
                DbCommand.CommandText = query;
                DbCommand.CommandType = CommandType.Text;
                DbAdapter = new SqlDataAdapter(DbCommand);
                DbAdapter.Fill(dSet, tblName);
            }
            catch (SqlException ex)
            {
                DbAdapter.Dispose();
                DbConn.Close();
                DbAdapter.Dispose();
                DbConn.Close();
                throw ex;
            }
            catch (Exception exp)
            {
                //Arfan 
                //Console.Write(exp);
                DbAdapter.Dispose();
                DbConn.Close();
                DbAdapter.Dispose();
                DbConn.Close();
                throw exp;
            }
            finally
            {
                DbAdapter.Dispose();
                DbConn.Close();
            }
        }

        //public  SqlDataReader getReader(string query)
        //{
        //    SqlDataReader dr;
        //    try
        //    {
        //        if (DbConn.State == 0)
        //        {
        //            createConn();
        //        }
        //        DbCommand.Connection = DbConn;
        //        DbCommand.CommandText = query;
        //        DbCommand.CommandTimeout = 120;
        //        DbCommand.CommandType = CommandType.Text;
        //        dr = DbCommand.ExecuteReader();
        //        return dr;
        //    }
        //    catch (Exception exp)
        //    {
        //        //Arfan 
        //        //Console.Write(exp);
        //        throw exp;
        //    }
        //    finally
        //    {
        //        DbAdapter.Dispose();
        //        DbConn.Close();
        //    }
        //}

        public int executeQuery(string query)
        {
            try
            {

                if (DbConn.State == 0)
                {
                    createConn();
                }
                DbCommand.Connection = DbConn;
                DbCommand.CommandText = query;
                DbCommand.CommandType = CommandType.Text;
                return DbCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                DbAdapter.Dispose();
                DbConn.Close();
            }
        }
        public int executeStoredProcedure(string spName, Hashtable param)
        {
            try
            {
                if (DbConn.State == 0)
                {
                    createConn();
                }
                DbCommand.Connection = DbConn;
                DbCommand.CommandTimeout = 0;
                DbCommand.CommandText = spName;
                DbCommand.CommandType = CommandType.StoredProcedure;
                foreach (string para in param.Keys)
                {
                    DbCommand.Parameters.AddWithValue(para, param[para]);
                }
                return DbCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception exp)
            {
                throw exp;
            }

           
                  finally
            {
                DbAdapter.Dispose();
                DbConn.Close();
            
            }

 
        }
        public int returnint32(string strSql)
        {
            try
            {
                if (DbConn.State == 0)
                {

                    createConn();
                }
                else
                {
                    DbConn.Close();
                    createConn();
                }
                DbCommand.Connection = DbConn;
                DbCommand.CommandText = strSql;
                DbCommand.CommandType = CommandType.Text;
                return (int)DbCommand.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                DbAdapter.Dispose();
                DbConn.Close();

            }
        }
        public Int64 returnint64(string strSql)
        {
            try
            {
                if (DbConn.State == 0)
                {
                    createConn();
                }
                DbCommand.Connection = DbConn;
                DbCommand.CommandText = strSql;
                DbCommand.CommandType = CommandType.Text;
                return (Int64)DbCommand.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception exp)
            {
                throw exp;
            }

            finally
            {
                DbAdapter.Dispose();
                DbConn.Close();

            }
        }
        public int executeDataSet(DataSet dSet, string tblName, string strSql)
        {
            try
            {
                if (DbConn.State == 0)
                {
                    createConn();
                }

                DbAdapter.SelectCommand.CommandText = strSql;
                DbAdapter.SelectCommand.CommandType = CommandType.Text;
                SqlCommandBuilder DbCommandBuilder = new SqlCommandBuilder(DbAdapter);

                return DbAdapter.Update(dSet, tblName);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public bool checkDbConnection()
        {
            int _flag = 0;
            try
            {
                //setConnString(ConfigurationManager.ConnectionStrings["EmpWebConnectionString"].ToString());
                if (DbConn.State == ConnectionState.Open)
                {
                    DbConn.Close();
                }

                DbConn.ConnectionString = getConnString();
                DbConn.Open();
                _flag = 1;

                //if (DbConn.State != ConnectionState.Closed || DbConn.State != ConnectionState.Broken)
                //{
                //    DbConn.ConnectionString = strConnString;
                //    DbConn.Open();
                //    _flag = 1;
                //}
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _flag = 0;
            }
            if (_flag == 1)
            {
                DbConn.Close();
                _flag = 0;
                return true;
            }
            else
            {
                return false;
            }

        }

        public string executeScaler2(string query)
        {
            try
            {

                if (DbConn.State == 0)
                {
                    createConn();
                }
                DbCommand.Connection = DbConn;
                DbCommand.CommandText = query;
                DbCommand.CommandType = CommandType.Text;
                string data = DbCommand.ExecuteScalar().ToString();
                return DbCommand.ExecuteScalar().ToString();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception exp)
            {
                return "No Record Found";
                throw exp;
            }
            finally
            {
                DbAdapter.Dispose();
                DbConn.Close();
            }
        }

        public int returnIdentityStoredProcedure(string spName, Hashtable param)
        {
            try
            {
                if (DbConn.State == 0)
                {
                    createConn();
                }
                DbCommand.Connection = DbConn;
                DbCommand.CommandTimeout = 0;
                DbCommand.CommandText = spName;
                DbCommand.CommandType = CommandType.StoredProcedure;
                foreach (string para in param.Keys)
                {
                    DbCommand.Parameters.AddWithValue(para, param[para]);
                }
                DbCommand.Parameters.Add("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;

                DbCommand.ExecuteNonQuery();
                int id = Convert.ToInt32(DbCommand.Parameters["@ID"].Value.ToString());
                return id;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        #region General Functions
        public List<Dictionary<string, object>> DT_to_DictionaryList(DataTable dt)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return rows;
        }

        #endregion

        //public static bool checkDbConnectivity()
        //{
        //    DataAccess Da = new DataAccess();
        //    Da.setConnString(ConfigurationManager.ConnectionStrings["EmpWebConnectionString"].ToString());
        //    return Da.checkDbConnection();
        //}
    }
}

