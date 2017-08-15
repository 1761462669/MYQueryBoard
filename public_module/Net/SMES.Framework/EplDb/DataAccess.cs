using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework.EplDb
{
    public static class DataAccess
    {      

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="configName">连接字符串配置节点名称</param>
        /// <returns>数据库对象</returns>
        public static Database CreateDb(string configName)
        {
            return Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase(configName);
        }

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <returns>数据库对象</returns>
        public static Database CreateDb()
        {
            return Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase();
        }


        #region 执行sql查询语句 返回Dataset

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="sql">SQL 语句</param>
        /// <param name="pars">参数</param>
        /// <returns>数据集</returns>
        public static DataSet Query(this Database db, string sql, SQLParaCollection pars)
        {
            DbCommand command = db.GetSqlStringCommand(sql);

            if (pars != null && pars.Count > 0)
            {
                foreach (var par in pars)
                {
                    db.AddInParameter(command, par.Name, par.DbType, par.Value);
                }
            }

            DataSet ds = db.ExecuteDataSet(command);

            return ds;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="sql">SQL 语句</param>
        /// <returns>数据集</returns>
        public static DataSet Query(this Database db, string sql)
        {            
            return Query(db, sql, null);
        }

        /// <summary>
        /// 查询单值
        /// </summary>
        /// <typeparam name="T">单值类型</typeparam>
        /// <param name="db">数据库对象</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="pars">参数</param>
        /// <returns>值</returns>
        public static T QuerySingleValue<T>(this Database db, string sql,SQLParaCollection pars)
        {
            return (T)QuerySingleValue(db, sql, pars);
        }

        /// <summary>
        /// 查询单值
        /// </summary>
        /// <typeparam name="T">单值类型</typeparam>
        /// <param name="db">数据库对象</param>
        /// <param name="sql">SQL语句</param>
        /// <returns>值</returns>
        public static T QuerySingleValue<T>(this Database db, string sql)
        {
            return QuerySingleValue<T>(db,sql,null);
        }

        /// <summary>
        /// 查询单值
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="pars">参数</param>
        /// <returns>结果</returns>
        public static object QuerySingleValue(this Database db, string sql, SQLParaCollection pars)
        {
            DataSet ds = Query(db, sql, pars);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Columns.Count == 0)
                return null;

            return ds.Tables[0].Rows[0][0];
        }

        /// <summary>
        /// 查询单值
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="sql">SQL语句</param>
        /// <returns>结果</returns>
        public static object QuerySingleValue(this Database db, string sql)
        {
            return QuerySingleValue(db,sql,null);
        }
        #endregion

        #region  执行update/delete/insert 语句

        /// <summary>
        /// 执行insert/delete/update
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="sql">sql语句</param>
        /// <param name="pars">参数</param>
        /// <returns>影响行数</returns>
        public static int Execute(this Database db, string sql, SQLParaCollection pars)
        {
            DbCommand command = db.GetSqlStringCommand(sql);

            if (pars != null && pars.Count > 0)
            {
                foreach (var par in pars)
                {
                    db.AddInParameter(command, par.Name, par.DbType, par.Value);
                }
            }

            int rlt = db.ExecuteNonQuery(command);

            return rlt;
        }

        /// <summary>
        /// 执行insert/delete/update
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="sql">sql语句</param>
        /// <returns>影响行数</returns>
        public static int Execute(this Database db,string sql)
        {
            return Execute(db, sql, null);
        }
        #endregion

        #region 执行存储过程
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="procename">存储过程名称</param>
        /// <param name="inpars">输入参数</param>
        /// <param name="outpars">输出参数</param>
        public static void ExecuteProce(this Database db, string procename, SQLParaCollection inpars, SQLParaCollection outpars)
        {            
            DbCommand command = db.GetStoredProcCommand(procename);

            if (inpars != null)
            {
                foreach (var par in inpars)
                {
                    db.AddInParameter(command, par.Name, par.DbType, par.Value);
                }
            }

            if (outpars != null)
            {
                foreach (var par in outpars)
                {
                    db.AddOutParameter(command, par.Name, par.DbType, 100);
                }
            }

            db.ExecuteNonQuery(command);

            if (outpars != null && outpars.Count > 0)
            {
                foreach (var item in outpars)
                {
                    item.Value = command.Parameters[item.Name].Value;
                }
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="procename">存储过程名称</param>
        /// <param name="inpars">输入参数</param>
        public static void ExecuteProce(this Database db, string procename, SQLParaCollection inpars)
        {
            ExecuteProce(db, procename, inpars, null);
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="procename">存储过程名称</param>
        public static void ExecuteProce(this Database db, string procename)
        {
            ExecuteProce(db, procename);
        }
        #endregion
    }
}
