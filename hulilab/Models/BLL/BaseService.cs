using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using hulilab.Models.Repository;
using hulilab.Models.DAL;
using System.Linq.Expressions;
using System.Data;

namespace hulilab.Models.BLL
{
    public class BaseService<T> :IService<T> where T:BaseObject,new()
    {
        private IRepository<T> GetCurrentRepository()
        {
            if (typeof(T) == typeof(Member))
            {
                return (IRepository<T>)new MemberRepository();
            }
            else
            {
                return null;
            }
        }

        public bool Add(T obj)
        {
            IRepository<T> ir = GetCurrentRepository();
            if (ir != null)
            {
                return ir.Create(obj);
            }
            else
            {
                return false;
            }
        }

        public bool Delete(T obj)
        {
            IRepository<T> ir = GetCurrentRepository();
            if (ir != null)
            {
                return ir.Delete(obj);
            }
            else
            {
                return false;
            }
        }

        public bool Edit(T obj)
        {
            IRepository<T> ir = GetCurrentRepository();
            if (ir != null)
            {
                return ir.Edit(obj);
            }
            else
            {
                return false;
            }
        }

        public bool Find(T obj)
        {
            IRepository<T> ir = GetCurrentRepository();
            if (ir != null)
            {
                return ir.Find(obj);
            }
            else
            {
                return false;
            }
        }

        public bool Load(Func<DataRow, bool> condition,out List<T> results)
        {
            IRepository<T> ir = GetCurrentRepository();
            if (ir != null)
            {
                return ir.Load(condition, out results);
            }
            else
            {
                results = new List<T>();
                return false;
            }
        }

        public bool LoadAll(out List<T> results)
        {
            return Load(p => true, out results);
        }
    }
}