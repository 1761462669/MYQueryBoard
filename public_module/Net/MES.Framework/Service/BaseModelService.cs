using MDF.Framework.Db;
using MES.Framework.IModel;
using MES.Framework.IService;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Framework.Utility;

namespace MES.Framework.Service
{
    /// <summary>
    /// 基础实体服务
    /// </summary>
    
    public class BaseModelService<T> : IBaseModelService<T> where T:IBaseModel
    {
        /// <summary>
        /// 数据库
        /// </summary>
        [Import]
        public IDataBase Db { get; set; }
        /// <summary>
        /// 保存修改对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual T SaveOrUpdate(T model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            T newModel;
            using (var session = this.Db.OpenSession())
            {
                try
                {
                    session.BeginTransAction();
                    this.Check(model, session);
                    if (model.Id > 0)
                    {
                        newModel = this.Update(model,session);
                    }
                    else
                    {
                        newModel = this.Save(model, session);
                    }
                    session.Commit();
                    return newModel;
                }
                catch (Exception)
                {
                    session.Rollback();
                    throw;
                }

            }
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id"></param>
        public virtual void Delete(int Id)
        {
            using (var session = this.Db.OpenSession())
            {
                var obj = session.GetObject<T>(Id);
                session.Delete(obj);
            }
        }
        /// <summary>
        /// 检查对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        protected virtual bool Check(T obj, IDbSession session)
        {
            obj.CheckModel<T>(session);
            return true;
        }
        /// <summary>
        /// 获取所有对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual IList<T> GetModels() 
        {
            using (var session = this.Db.OpenSession())
            {
                return session.Query<T>()
                    .OrderBy(c=>c.Seq)
                    .ToList();
            }
        }
        /// <summary>
        /// 获取对象个数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual int GetCounts() 
        {
            using (var session = this.Db.OpenSession())
            {
                return session.Query<T>().Count();
            }
        }

        /// <summary>
        /// 获取最大顺序号
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <returns></returns>
        protected virtual int GetMaxSeq(T obj, IDbSession session)
        {
            return SequenceHelper.MaxSeq<T>(session);
        }
     

        /// <summary>
        /// 保存对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        protected  virtual T Save(T obj,IDbSession session) 
        {
            obj.Seq = GetMaxSeq(obj, session) + 1;
            int id= (int)session.Save(obj);
            return session.GetObject<T>(id);

        }
        /// <summary>
        ///  更新对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        protected virtual T Update(T obj,IDbSession session)
        {
            session.Update(obj);
            return session.GetObject<T>(obj.Id);

        }


        public virtual void MoveUp(T model)
        {
            using (var session = this.Db.OpenSession())
            {
                try
                {
                    session.BeginTransAction();
                    var query = this.GetQuerySequenceCondition(model, session);
                    T preModel;
                    model.SwapPreModelSeq(query, out preModel);
                    this.Update(preModel, session);
                    this.Update(model, session);
                    session.Commit();
                }
                catch (Exception)
                {
                    session.Rollback();
                    throw;
                }
                
            }
        }

        public virtual void MoveDown(T model)
        {
            using (var session = this.Db.OpenSession())
            {
                try
                {
                    session.BeginTransAction();
                    var query = this.GetQuerySequenceCondition(model, session);
                    T nextModel;
                    model.SwapNextModelSeq(query, out nextModel);
                    this.Update(nextModel, session);
                    this.Update(model, session);
                    session.Commit();
                }
                catch (Exception)
                {
                    session.Rollback();
                    throw;
                }

            }
        }

        public virtual void Insert(int preModelId, T insert)
        {
            using (var session = this.Db.OpenSession())
            {
                try
                {
                    session.BeginTransAction();
                    var pos= session.GetObject<T>(preModelId).Seq;
                    var query = this.GetQuerySequenceCondition(insert, session);
                    IList<T> affects;
                    insert.InsertPreCheck(query, pos, out affects);
                    this.Check(insert, session);
                    this.Save(insert,session);
                    if(affects!=null && affects.Count>0)
                        foreach (var item in affects)
                        {
                            this.Update(item,session);
                        }
                    session.Commit();

                }
                catch (Exception)
                {
                    session.Rollback();
                    throw;
                }
 
            }
        }


        protected virtual IQueryable<T> GetQuerySequenceCondition(T modle, IDbSession session)
        {

            return session.Query<T>();
        }



    }
}
