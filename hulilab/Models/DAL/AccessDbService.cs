using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Threading;

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
                    conn.Open();
                }
                else
                {
                    while (conn.State == ConnectionState.Connecting || conn.State == ConnectionState.Executing ||
                        conn.State == ConnectionState.Fetching)
                    {
                        Thread.Sleep(500);
                        conn.ResetState();
                    }
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                }
                return conn;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 关闭数据库链接
        /// </summary>
        /// <returns></returns>
        public bool CloseConnection()
        {
            try
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
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
                    isSuccess = CloseConnection();
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
                    isSuccess = CloseConnection();
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
        /// 运行insert sql语句，并返回插入行的主键id
        /// </summary>
        /// <param name="sql">insert sql 语句</param>
        /// <param name="id">插入行的主键id</param>
        /// <returns>成功为true，否则为false</returns>
        public bool RunInsertCommand(string sql, out int id)
        {
            id = -1;
            int infectedRows;
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
                        else
                        {
                            errorMsg = "获取@@IDENTITY失败。";
                        }
                    }
                    else
                    {
                        errorMsg = "受影响行超过一行，回滚操作。";
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