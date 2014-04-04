using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hulilab.Models.DAL;
using System.Linq.Expressions;
using System.Data;

namespace hulilab.Models.Repository
{
    interface IRepository<T> where T: BaseObject,new()
    {
        string ErrorMsg { get; }
        bool Find(T obj);
        bool Delete(T obj);
        bool Edit(T obj);
        bool Create(T obj);
        bool Clear(T obj);
        bool ClearUnderCondition(T obj);
        bool Load(Func<DataRow,bool> condition,out List<T> results);
    }
}
