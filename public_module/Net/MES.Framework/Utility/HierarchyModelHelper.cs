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
    /// 树结构帮助类
    /// </summary>
    public static class HierarchyModelHelper
    {
        /// <summary>
        /// 设置层级结构的树形结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="session"></param>
        public static void SetChilds<T>(this T model, IDbSession session) where T : IHierarchyModel
        {
            if (session == null)
                throw new ArgumentNullException("session");
            if (model == null)
                return;
            model.Childs = session.Query<T>()
                .Where(c => c.Parent.Id == model.Id && c.IsUsed)
                .OrderBy(c => c.Seq)
                .ToList().OfType<IHierarchyModel>().ToList();
            foreach (T item in model.Childs)
            {
                item.SetChilds<T>(session);
            }
        }







    }
}
