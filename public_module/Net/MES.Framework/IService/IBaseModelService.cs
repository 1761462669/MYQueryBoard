using MDF.Framework.Db;
using MES.Framework.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.IService
{
    /// <summary>
    /// 服务基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseModelService<T> where T:IBaseModel
    {
        /// <summary>
        /// 保存对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        T SaveOrUpdate(T model);
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="Id"></param>
        void Delete(int Id);
        /// <summary>
        /// 检查对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //bool Check(T obj, IDbSession session);

        /// <summary>
        /// 获取所有对象
        /// </summary>
        /// <returns></returns>
        IList<T> GetModels();

        /// <summary>
        /// 获取个数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        int GetCounts();

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="model"></param>
        void MoveUp(T model);

        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="model"></param>
        void MoveDown(T model);


        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="pre"></param>
        /// <param name="insert"></param>
        void Insert(int preModelId, T insert);

    }
}
