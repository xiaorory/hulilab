using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace hulilab.Models.DAL
{
    public class AccessDbService : IDbService
    {
        private OleDbConnection conn = null;
        private string connectionString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}\Content\db\db.mdb",System.Web.HttpContext.Current.Server.MapPath("~"));
        private string errorMsg = string.Empty;
        public string ErrorMsg
        {
            get { return errorMsg; }
        }

        public OleDbConnection GetConnection()
        {
            try
            {
                if (conn == null)
                {
                    conn = new OleDbConnection(connectionString);
                }

                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return null;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                if (conn != null)
                {
                    conn.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 运行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool RunTextCommand(string sql)
        {
            bool isSuccess = false;
            try
            {
                OleDbConnection dbConn = GetConnection();
                if (dbConn != null)
                {
                    OleDbCommand cmd = new OleDbCommand(sql, dbConn);
                    cmd.ExecuteNonQuery();
                    CloseConnection();
                    isSuccess = true;
                }
                else
                {
                    errorMsg = "数据库连接为空";
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            return isSuccess;
        }

        /// <summary>
        /// 运行sql语句，并返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="infectedRows"></param>
        /// <returns></returns>
        public bool RunTextCommand(string sql, out int infectedRows)
        {
            infectedRows = 0;
            bool isSuccess = false;
            try
            {
                OleDbConnection dbConn = GetConnection();
                if (dbConn != null)
                {
                    OleDbCommand cmd = new OleDbCommand(sql, dbConn);
                    infectedRows = cmd.ExecuteNonQuery();
                    CloseConnection();
                    isSuccess = true;
                }
                else
                {
                    errorMsg = "数据库连接为空";
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            return isSuccess;
        }

        /// <summary>
        /// 运行sql语句，并返回受影响的行数和主键id
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="infectedRows"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RunTextCommand(string sql, out int infectedRows, out int id)
        {
            id = -1;
            infectedRows = 0;
            bool isSuccess = false;
            try
            {
                OleDbConnection dbConn = GetConnection();
                if (dbConn != null)
                {
                    OleDbTransaction tran = dbConn.BeginTransaction();
                    OleDbCommand cmd = new OleDbCommand(sql, dbConn, tran);
                    infectedRows = cmd.ExecuteNonQuery();
                    if (infectedRows == 1)
                    {
                        tran.Commit();
                        cmd = new OleDbCommand("select @@IDENTITY", dbConn);
                        object obj = cmd.ExecuteScalar();
                        if (null != obj && int.TryParse(obj.ToString(), out id))
                        {
                            isSuccess = true;
                        }
                    }
                    else
                    {
                        tran.Rollback();
                    }
                    isSuccess = CloseConnection() && isSuccess;
                }
                else
                {
                    errorMsg = "数据库连接为空";
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
           
            return isSuccess;
        }

        public bool RunFunction(string functionName, List<OleDbParameter> parms)
        {
            bool isSuccess = false;
            try
            {
                OleDbConnection dbConn = GetConnection();
                if (dbConn != null)
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = functionName;
                    foreach (OleDbParameter parm in parms)
                    {
                        cmd.Parameters.Add(parm);
                    }
                    cmd.ExecuteNonQuery();
                    CloseConnection();
                    isSuccess = true;
                }
                else
                {
                    errorMsg = "数据库连接为空";
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            return isSuccess;
        }

        public bool Query(string sql,out OleDbDataReader dr)
        {
            bool isSuccess = false;
            dr = null;
            try
            {
                OleDbConnection dbConn = GetConnection();
                if (dbConn != null)
                {
                    OleDbCommand cmd = new OleDbCommand(sql, dbConn);
                    dr = cmd.ExecuteReader();
                    isSuccess = true;
                }
                else
                {
                    errorMsg = "数据库连接为空";
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }
            return isSuccess;
        }

        public bool Query(string sql, out DataSet ds)
        {
            bool isSuccess = false;
            ds = new DataSet();
            try
            {
                OleDbConnection dbConn = GetConnection();
                if (dbConn != null)
                {
                    OleDbCommand cmd = new OleDbCommand(sql, dbConn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(ds);
                    isSuccess = true;
                }
                else
                {
                    errorMsg = "数据库连接为空";
                }
            }
            catch (Exception ex)
            {
                ds = null;
                errorMsg = ex.Message;
            }
            return isSuccess;
        }
    }
}