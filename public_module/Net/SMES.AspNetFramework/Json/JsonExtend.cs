using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetFramework.Json
{
    public static class JsonExtend
    {
        public static JObject ToJObject(this DataRow row)
        {
            JObject obj = new JObject();

            if (row == null)
                return obj;


            DataTable dt = row.Table;

            foreach (DataColumn column in dt.Columns)
            {
                obj.Add(column.ColumnName, row[column].ToString());
            }

            return obj;
        }

        public static JArray ToJArray(this DataTable table)
        {
            JArray array = new JArray();

            if (table == null || table.Rows.Count == 0)
                return array;

            JObject obj = null;
            foreach (DataRow row in table.Rows)
            {
                obj = new JObject();
                foreach (DataColumn column in table.Columns)
                {
                    obj.Add(column.ColumnName, row[column].ToString());
                }
                array.Add(obj);
            }

            return array;
        }

        public static string ToJson(object obj)
        {
            return MDF.Framework.Bus.InfoExchange.ConvertToJson(obj);
        }

        public static string ToKonwnTypeJson(object obj)
        {
            return MDF.Framework.Bus.InfoExchange.ConvertToJson(obj, SetingsAspNetKonwnTypesBinder); 
        }


        public static JsonSerializerSettings SetingsAspNetKonwnTypesBinder = new JsonSerializerSettings()
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Local,
            DateParseHandling = DateParseHandling.DateTime,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            //PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            TypeNameHandling = TypeNameHandling.Objects,
            Binder = MDF.Framework.Bus.InfoExchange.KnownTypesBinder
        };
    }
}
