using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework
{
    public interface IEntityServiceBase<T> where T:class
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        T Save(T model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        void Delete(T model);

        /// <summary>
        /// 得到对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(object id);

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="obj"></param>
        void Update(T obj);
       
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        IList<T> Query();


    }
}
