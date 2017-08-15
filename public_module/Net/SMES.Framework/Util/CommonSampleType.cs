using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework.Util
{
    /// <summary>
    /// 取样类型常量
    /// </summary>
    public class CommonSampleType
    {
        /// <summary>
        /// 按批
        /// </summary>
        public static readonly string Batch = "批";
        /// <summary>
        /// 按牌号
        /// </summary>
        public static readonly string Material = "牌号";
        public static IList<Tuple<string, string>> values = new List<Tuple<string, string>>();

        public static IList<Tuple<string, string>> Values
        {
            get
            {
                if (values.Count == 0)
                {
                    values.Add(new Tuple<string, string>("批", "批"));
                    foreach (var item in CommonTimeType.Items)
                    {
                        values.Add(new Tuple<string, string>("牌号/" + item.Item2, "牌号/" + item.Item2));
                    } 
                }
                return values;

            }
        }
        public IList<Tuple<string,string>> Items
        {
            get {return Values; }
        }
    }
}
