using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using MDF.Framework.Db;
using MDF.Framework.Bus;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SMES.Util
{
    /// <summary>
    /// 辅助adpt用
    /// sql语句查出数据 返回 dataset 类型
    /// 利用json序列化、反序列化转换为对应的对象
    /// </summary>
    [InheritedExport]
    public static class SqlQueryHelper
    {
        [Import]
        public static IDataBase Db { get; set; }

        /// <summary>
        /// 根据sql语句返回dataset
        /// 写adpt用
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static System.Data.DataSet ExecuteSql(string sql)
        {
            if (Db == null)
                Db = ObjectFactory.GetObject<IDataBase>();

            using (var session = Db.OpenSession())
            {
                return session.CreateSQLQuery(sql).DataSet();
            }
        }

        /// <summary>
        /// sql语句获取单个对象
        /// 写adpt用
        /// </summary>
        /// <typeparam name="T">反序列化时的对象类型</typeparam>
        /// <param name="sql">查询的sql语句</param>
        /// <returns></returns>
        public static T GetFirstObject<T>(string sql)
        {
            var ds = ExecuteSql(sql);

            if (ds == null || ds.Tables == null
                || ds.Tables.Count == 0)
                return default(T);

            var json = Newtonsoft.Json.Linq.JObject.FromObject(ds)["Table"][0].ToString();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json, new DecimalToIntConvert());
        }

        /// <summary>
        /// 获取对象集合
        /// 写adpt用
        /// </summary>
        /// <typeparam name="T">反序列化时的对象类型</typeparam>
        /// <param name="sql">查询的sql语句</param>
        /// <returns></returns>
        public static List<T> GetObjects<T>(string sql)
        {
            var ds = ExecuteSql(sql);

            if (ds == null || ds.Tables == null
                || ds.Tables.Count == 0)
                return null;

            var json = Newtonsoft.Json.Linq.JObject.FromObject(ds)["Table"].ToString();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(json,new DecimalToIntConvert());
        }

        public class DecimalToIntConvert : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                if (objectType == typeof(Int32))
                    return true;

                return false;
            }

            public override bool CanRead
            {
                get
                {
                    return true;
                }
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken j = JToken.Load(reader);

                if (j.Type == JTokenType.Float)
                    return (int)Convert.ToDecimal(j.ToString());

                throw new NotImplementedException();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
