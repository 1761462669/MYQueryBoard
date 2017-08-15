using MDF.Framework.Db;
using MDF.Framework.Db.Imp;
using SMES.Framework.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework
{
    public class EntityServiceBase<T>:ServiceBase,IEntityServiceBase<T> where T:class
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual T Save(T model)
        {
            using (IDbSession session = Db.OpenSession())
            {
                var id=session.Save(model);
                return Get(id);
                //return model;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        public virtual void Delete(T model)
        {           
            using (IDbSession session = Db.OpenSession())
            {
                session.Delete(model);
            }
        }

        /// <summary>
        /// 得到对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(object id)
        {
            using (IDbSession session = Db.OpenSession())
            {
                return session.GetObject<T>(id);
            }
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="obj">对象</param>
        public virtual void Update(T obj)
        {
            using (IDbSession session = Db.OpenSession())
            {                
                session.Update(obj);
                //return obj;
            }
        }
        
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public virtual IList<T> Query()
        {
            using (IDbSession session = Db.OpenSession())
            {                
                return session.Query<T>().ToList();
            }
        }
        
    }
}
