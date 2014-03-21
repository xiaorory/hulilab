using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace hulilab.Models.DAL
{
    interface IDbService
    {
        OleDbConnection GetConnection();
        bool CloseConnection();
        bool RunTextCommand(string sql);
        bool RunTextCommand(string sql,out int infectedRows);
        bool RunTextCommand(string sql, out int infectedRows, out int id);
        bool RunFunction(string functionName, List<OleDbParameter> parms);
        bool Query(string sql, out OleDbDataReader dr);
        bool Query(string sql, out DataSet ds);
        string ErrorMsg { get; }
    }
}
