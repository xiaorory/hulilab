using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hulilab.Models.Repository;
using hulilab.Models.DAL;
using System.Linq.Expressions;
using System.Data;

namespace hulilab.Models.BLL
{
    interface IService<T> where T:class
    {
        bool Add(T obj);
        bool Delete(T obj);
        bool Edit(T obj);
        bool Find(T obj);
        bool Load(Func<DataRow, bool> condition,out List<T> results);
        bool LoadAll(out List<T> results);
    }
}
