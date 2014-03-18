using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using hulilab.Models.DAL;
using System.Reflection;
using System.Linq.Expressions;
using System.Data.Linq.Mapping;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace hulilab.Models.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseObject,new()
    {
        private string errorMsg = string.Empty;
        public string ErrorMsg { get { return errorMsg; } }
        private IDbService dbService = new AccessDbService();

        private IEnumerable<PropertyInfo> GetValidProperties(T obj)
        {
            //获得所有property的信息
            PropertyInfo[] properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            IEnumerable<PropertyInfo> validProperties =
                from property in properties
                where property != null && (property.PropertyType.IsValueType ||
                property.PropertyType == typeof(string) || property.PropertyType == typeof(bool) || property.PropertyType == typeof(StringBuilder))
                && property.GetValue(obj, null) != null
                select property;
            return validProperties;
        }

        private string GetValue(PropertyInfo property, T obj)
        {
            string value;
            if (property.PropertyType == typeof(bool))
            {
                if (bool.Parse(property.GetValue(obj, null).ToString()))
                {
                    value = "yes";
                }
                else
                {
                    value = "no";
                }
            }
            else if (property.PropertyType.IsValueType)
            {
                value = property.GetValue(obj, null).ToString();
            }
            else
            {
                value = "'" + property.GetValue(obj, null) + "'";
            }
            return value;
        }

        public bool Create(T obj)
        {
            bool isSuccess = false;
            string sql = "insert into " + typeof(T).Name + " (";
            string cols = "";
            string vals = "";

            foreach (PropertyInfo property in GetValidProperties(obj))
            {
                if (property.Name.ToLower() != "id")
                {
                    cols += "[" + property.Name + "],";
                    vals += GetValue(property, obj) + ",";
                }
            }
            sql += cols.Substring(0, cols.Length - 1) + ") values(" + vals.Substring(0, vals.Length - 1) + ")";
            int id, infectedRows;
            if (dbService.RunTextCommand(sql, out infectedRows, out id))
            {
                if (infectedRows == 1 && id > 0)
                {
                    isSuccess = true;
                    obj.ID = id;
                }
            }

            return isSuccess;
        }

        public bool Delete(T obj)
        {
            bool isSuccess = false;
            string sql = "delete * from [" + typeof(T).Name + "] where ";
            string conditions = "";

            foreach (PropertyInfo property in GetValidProperties(obj))
            {
                conditions += "[" + property.Name + "]=";
                conditions += GetValue(property, obj);
                conditions += " and ";
            }
            sql += conditions.Substring(0, conditions.Length - 5);
            int infectedRows;
            if (dbService.RunTextCommand(sql, out infectedRows))
            {
                isSuccess = true;
            }

            return isSuccess;
        }

        public bool Find(T obj)
        {
            bool isSuccess = false;
            string sql = "select * from [" + typeof(T).Name + "] where ";
            string conditions = "";

            foreach (PropertyInfo property in GetValidProperties(obj))
            {
                conditions += "[" + property.Name + "]=";
                conditions += GetValue(property, obj);
                conditions += " and ";
            }
            sql += conditions.Substring(0, conditions.Length - 5);
            OleDbDataReader dr;
            if (dbService.Query(sql, out dr))
            {
                if (null != dr && dr.Read())
                {
                    foreach (PropertyInfo property in typeof(T).GetProperties())
                    {
                        if (dr[property.Name].GetType() != typeof(System.DBNull))
                        {
                            if (property.PropertyType == typeof(System.Text.StringBuilder))
                            {
                                property.SetValue(obj, new StringBuilder(dr[property.Name].ToString()), null);
                            }
                            else
                            {
                                property.SetValue(obj, dr[property.Name], null);
                            }
                        }
                    }
                    isSuccess = true;
                }
            }
            isSuccess = dbService.CloseConnection() && isSuccess;

            return isSuccess;
        }

        public bool Edit(T obj)
        {
            bool isSuccess = false;
            string sql = "update  " + typeof(T).Name + " set";
            string cols = "";
            string conditions = " where ";

            foreach (PropertyInfo property in GetValidProperties(obj))
            {
                if (property.Name.ToLower() == "id")//id为主键
                {
                    conditions += "[" + property.Name + "]=" + property.GetValue(obj, null) + ",";
                }
                else
                {
                    cols += "[" + property.Name + "]=";
                    cols += GetValue(property, obj) + ",";
                }
            }
            sql += cols.Substring(0, cols.Length - 1) + conditions.Substring(0, conditions.Length - 1);
            int infectedRows;
            if (dbService.RunTextCommand(sql, out infectedRows))
            {
                if (infectedRows == 1)
                {
                    isSuccess = true;
                }
            }

            return isSuccess;
        }

        public bool Load(Func<DataRow, bool> condition,out List<T> results)
        {
            bool isSuccess = false;
            results = new List<T>();
            string sql = "select * from [" + typeof(T).Name + "]";
            DataSet ds;
            if (dbService.Query(sql, out ds))
            {
                if (null != ds)
                {
                    DataTable dt = ds.Tables[0];
                    IEnumerable<DataRow> rows = from row in dt.AsEnumerable() select row;
                    IEnumerable<DataRow> selectedRows = rows.Where(condition);
                    foreach (DataRow dr in selectedRows)
                    {
                        T obj = new T();

                        foreach (PropertyInfo property in typeof(T).GetProperties())
                        {
                            if (dr[property.Name].GetType() != typeof(System.DBNull))
                            {
                                if (property.PropertyType == typeof(System.Text.StringBuilder))
                                {
                                    property.SetValue(obj, new StringBuilder(dr[property.Name].ToString()), null);
                                }
                                else
                                {
                                    property.SetValue(obj, dr[property.Name], null);
                                }
                            }
                        }
                        results.Add(obj);
                    }
                    isSuccess = true;
                }
            }
            isSuccess = dbService.CloseConnection() && isSuccess;

            return isSuccess;
        }
    }
}