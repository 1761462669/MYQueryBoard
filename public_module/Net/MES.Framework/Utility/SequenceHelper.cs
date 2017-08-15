using MDF.Framework.Db;
using MES.Framework.IModel;
using MES.Framework.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.Utility
{
    /// <summary>
    /// 具有顺序的帮助类
    /// added by changhl,2015-7-16
    /// </summary>
    public static class SequenceHelper
    {
        /// <summary>
        /// 对象上移
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="session"></param>
        public static void MoveUp<T>(this T model, IDbSession session) where T : ISequence
        {
            if (session == null)
                throw new ArgumentNullException("session");
            if (model == null)
                throw new ArgumentNullException("model");
            var queryble = session.Query<T>();
            T PreModel;
            SwapPreModelSeq(model, queryble, out PreModel);
            session.Update(PreModel);
            session.Update(model);


        }

        public static void MoveDown<T>(this T model, IDbSession session) where T : ISequence
        {
            if (session == null)
                throw new ArgumentNullException("session");
            if (model == null)
                throw new ArgumentNullException("model");
            var queryble = session.Query<T>();
            T PreModel;
            SwapNextModelSeq(model, queryble, out PreModel);
            session.Update(PreModel);
            session.Update(model);


        }


        /// <summary>
        /// 交换前序Model顺序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="queryable"></param>
        /// <param name="preModel"></param>
        public static void SwapPreModelSeq<T>(this T model, IQueryable<T> queryable, out T preModel) where T : ISequence
        {
            if (queryable == null)
                throw new ArgumentNullException("queryable");
            if (model == null)
                throw new ArgumentNullException("model");
            preModel = queryable.Where(c => c.Seq < model.Seq)
                 .OrderByDescending(c => c.Seq).FirstOrDefault();
            if (preModel == null)
                throw new Exception(string.Format(Resources.exception_NotFondObject, "顺序小于【" + model.Seq + "】"));
            var seq = model.Seq;
            model.Seq = preModel.Seq;
            preModel.Seq = seq;

        }

        public static void SwapNextModelSeq<T>(this T model, IQueryable<T> queryable, out T nextModel) where T : ISequence
        {
            if (queryable == null)
                throw new ArgumentNullException("queryable");
            if (model == null)
                throw new ArgumentNullException("model");
            nextModel = queryable.Where(c => c.Seq > model.Seq)
                 .OrderBy(c => c.Seq).FirstOrDefault();
            if (nextModel == null)
                throw new Exception(string.Format(Resources.exception_NotFondObject, "顺序大于【" + model.Seq + "】"));
            var seq = model.Seq;
            model.Seq = nextModel.Seq;
            nextModel.Seq = seq;
        }





        /// <summary>
        /// 获取最大顺序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <returns></returns>
        public static int MaxSeq<T>(IDbSession session) where T : ISequence
        {
            var queryable = session.Query<T>();
            return MaxSeq<T>(queryable, session);
        }

        /// <summary>
        /// 获取最大顺序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static int MaxSeq<T>(this IQueryable<T> queryable, IDbSession session) where T : ISequence
        {
            if (session == null)
                throw new ArgumentNullException("session");
            if (queryable == null)
                throw new ArgumentNullException("queryable");
            try
            {
                return queryable.Max(c => c.Seq);
            }
            catch (ArgumentNullException)
            {
                return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static void Insert<T>(this T insert, int position, IDbSession session) where T : ISequence
        {
            if (insert == null)
                throw new ArgumentNullException("insert");
            if (session == null)
                throw new ArgumentNullException("session");
            var query = session.Query<T>();
            IList<T> affectedObjs;
            InsertPreCheck(insert, query, position, out affectedObjs);
            session.Save(insert);
            if(affectedObjs!=null)
                foreach (var item in affectedObjs)
                {
                    session.Update(item);
                }
        }



        public static void InsertPreCheck<T>(this T insert, IQueryable<T> queryable, int position, out IList<T> affectedObjs) where T : ISequence
        {
            insert.Seq = position + 1;
            affectedObjs = queryable.Where(c => c.Seq > position)
                .OrderBy(c => c.Seq).ToList();
            if (affectedObjs.Count > 0)
            {
                var temp = insert;
                foreach (var item in affectedObjs)
                {
                    item.Seq = temp.Seq + 1;
                    temp = item;
                }
            }


        }
    }
}
