using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDF.Framework.Db
{
    /// <summary>
    /// 数据库工厂扩展类
    /// </summary>
    public static class DbSessionExtension
    {
        /// <summary>
        /// 数据库事务
        /// </summary>
        static IDbTransaction DbSessionTransaction = null;

        /// <summary>
        /// 根据SQL查询返回泛型集合。
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="DbSession">数据库工厂</param>
        /// <param name="Sql">SQL语句</param>
        /// <returns></returns>
        public static List<T> Query<T>(this IDbSession DbSession, string Sql) where T : new()
        {
            try
            {
                List<T> EntityList = new List<T>();
                using (IDbCommand Command = DbSession.Connection.CreateCommand())
                {
                    Command.CommandText = Sql;
                    using (IDataReader Reader = Command.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            T Entity = new T();
                            var EntityProperties = Entity.GetType().GetProperties();
                            if (EntityProperties.Length == 0 && Reader.FieldCount == 1)
                            {
                                try
                                {
                                    object DataValue = Reader.GetValue(0);
                                    Entity = Reader.IsDBNull(0) ? default(T) : (T)DataValue;
                                    EntityList.Add(Entity);
                                }
                                catch
                                {
                                    Entity = default(T);
                                    EntityList.Add(Entity);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < Reader.FieldCount; i++)
                                {
                                    if (Reader.IsDBNull(i))
                                    {
                                        continue;
                                    }
                                    var EntityPropertie = EntityProperties.FirstOrDefault(Propertie => Propertie.Name.ToUpper() == Reader.GetName(i).ToUpper());
                                    if (EntityPropertie != null && EntityPropertie.CanWrite && EntityPropertie.PropertyType.FullName == Reader.GetValue(i).GetType().FullName)
                                    {
                                        EntityPropertie.SetValue(Entity, Reader.GetValue(i), null);
                                    }
                                }
                                EntityList.Add(Entity);
                            }
                        }
                    }
                }
                return EntityList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 开启数据库事务。
        /// </summary>
        /// <param name="DbSession"></param>
        public static void BeginTransaction(this IDbSession DbSession)
        {
            try
            {
                DbSessionTransaction = DbSession.Connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 提交数据库事务。
        /// </summary>
        /// <param name="DbSession"></param>
        public static void CommitTransaction(this IDbSession DbSession)
        {
            try
            {
                DbSessionTransaction.Commit();
                DbSessionTransaction.Dispose();
                DbSessionTransaction = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 回滚数据库事务。
        /// </summary>
        /// <param name="DbSession"></param>
        public static void RollbackTransaction(this IDbSession DbSession)
        {
            try
            {
                DbSessionTransaction.Rollback();
                DbSessionTransaction.Dispose();
                DbSessionTransaction = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行SQL语句，并返回受影响的行数。
        /// </summary>
        /// <param name="DbSession"></param>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public static int ExecuteSql(this IDbSession DbSession, string Sql)
        {
            try
            {
                using (IDbCommand Command = DbSession.Connection.CreateCommand())
                {
                    Command.Transaction = DbSessionTransaction;
                    Command.CommandText = Sql;
                    int Result = Command.ExecuteNonQuery();
                    return Result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}