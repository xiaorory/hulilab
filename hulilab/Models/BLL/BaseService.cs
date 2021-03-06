﻿using System;
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
        protected string errorMsg="";
        public string ErrorMsg { get { return errorMsg; } }

        private IRepository<T> GetCurrentRepository()
        {
            if (typeof(T) == typeof(Member))
            {
                return (IRepository<T>)new MemberRepository();
            }
            else if (typeof(T) == typeof(Project))
            {
                return (IRepository<T>)new ProjectRepository();
            }
            else if (typeof(T) == typeof(Collaboration))
            {
                return (IRepository<T>)new CollaborationRepository();
            }
            else if (typeof(T) == typeof(Publication))
            {
                return (IRepository<T>)new PublicationRepository();
            }
            else if (typeof(T) == typeof(Share))
            {
                return (IRepository<T>)new ShareRepository();
            }
            else if (typeof(T) == typeof(Comment))
            {
                return (IRepository<T>)new CommentRepository();
            }
            else if (typeof(T) == typeof(TextSettings))
            {
                return (IRepository<T>)new TextSettingsRepository();
            }
            else
            {
                return null;
            }
        }

        public bool Add(T obj)
        {
            bool isSuccess = false;
            IRepository<T> ir = GetCurrentRepository();
            if (ir != null)
            {
                if (ir.Create(obj))
                {
                    isSuccess = true;
                }
                else
                {
                    errorMsg = ir.ErrorMsg;
                }
            }
            else
            {
                errorMsg = "当前的对象的数据库操作方法尚未实现";
            }
            return isSuccess;
        }

        public bool Delete(T obj)
        {
            bool isSuccess = false;
            IRepository<T> ir = GetCurrentRepository();
            if (ir != null)
            {
                if(ir.Delete(obj))
                {
                    isSuccess = true;
                }
                else
                {
                    errorMsg = ir.ErrorMsg;
                }
            }
            else
            {
                errorMsg = "当前的对象的数据库操作方法尚未实现";
            }
            return isSuccess;
        }

        /// <summary>
        /// 清空某个数据库表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Clear(T obj)
        {
            bool isSuccess = false;
            IRepository<T> ir = GetCurrentRepository();
            if (ir != null)
            {
                if (ir.Clear(obj))
                {
                    isSuccess = true;
                }
                else
                {
                    errorMsg = ir.ErrorMsg;
                }
            }
            else
            {
                errorMsg = "当前的对象的数据库操作方法尚未实现";
            }
            return isSuccess;
        }

        /// <summary>
        /// 按照obj中的值来清空某个表内的符合这些值的内容
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ClearUnderCondition(T obj)
        {
            bool isSuccess = false;
            IRepository<T> ir = GetCurrentRepository();
            if (ir != null)
            {
                if (ir.ClearUnderCondition(obj))
                {
                    isSuccess = true;
                }
                else
                {
                    errorMsg = ir.ErrorMsg;
                }
            }
            else
            {
                errorMsg = "当前的对象的数据库操作方法尚未实现";
            }
            return isSuccess;
        }


        public bool Edit(T obj)
        {
            bool isSuccess = false;
            IRepository<T> ir = GetCurrentRepository();
            if (ir != null)
            {
                if(ir.Edit(obj))
                {
                    isSuccess = true;
                }
                else
                {
                    errorMsg = ir.ErrorMsg;
                }
            }
            else
            {
                errorMsg = "当前的对象的数据库操作方法尚未实现";
            }
            return isSuccess;
        }

        public bool Find(T obj)
        {
            bool isSuccess = false;
            IRepository<T> ir = GetCurrentRepository();
            if (ir != null)
            {
                if(ir.Find(obj))
                {
                    isSuccess = true;
                }
                else
                {
                    errorMsg = ir.ErrorMsg;
                }
            }
            else
            {
                errorMsg = "当前的对象的数据库操作方法尚未实现";
            }
            return isSuccess;
        }

        public bool Load(Func<DataRow, bool> condition,out List<T> results)
        {
            bool isSuccess = false;
            IRepository<T> ir = GetCurrentRepository();
            if (ir != null)
            {
                if(ir.Load(condition, out results))
                {
                    isSuccess = true;
                }
                else
                {
                    errorMsg = ir.ErrorMsg;
                }
            }
            else
            {
                results = null;
                errorMsg = "当前的对象的数据库操作方法尚未实现";
            }
            return isSuccess;
        }

        public bool LoadAll(out List<T> results)
        {
            return Load(p => true, out results);
        }

        public bool First(out T result)
        {
            bool isSuccess = false;
            result = null;
            List<T> results;
            isSuccess = LoadAll(out results);
            if (isSuccess)
            {
                if (null != results && results.Count > 0)
                {
                    result = results[0];
                }
                else
                {
                    isSuccess = false;
                    errorMsg = "集合为空，找不到第一个成员";
                }
            }
            return isSuccess;
        }
    }
}