using MDF.Framework.Db;
using MES.Framework.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.Utility
{
    /// <summary>
    /// 基础模型帮助类
    /// </summary>
    public static class BaseModelHelper
    {
        /// <summary>
        /// 验证模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="session"></param>
        public static void CheckModel<T>(this T model, IDbSession session) where T : IBaseModel
        {
            if (model == null)
                throw new ArgumentOutOfRangeException("model");
            if (session == null)
                throw new ArgumentNullException("session");
            model.ValidateNullPropertyValue("Name");
            if (model.CheckDuplicationName(session))
                throw new DuplicateWaitObjectException(string.Format("属性【{0}】的值【{1}】在数据库中有重复值！", "Name", model.Name));

        }


        public static int GetDuplicationNameCount<T>(this T model, IDbSession session) where T : IBaseModel
        {

            return session.Query<T>().Where(c => c.Name == model.Name).Count();

        }

        public static bool CheckDuplicationName<T>(this T model, IDbSession session) where T : IBaseModel
        {
            var count = model.GetDuplicationNameCount(session);
            if (model.Id > 0)
                return !(count <= 1);
            else
                return !(count == 0);


        }



    }
}
