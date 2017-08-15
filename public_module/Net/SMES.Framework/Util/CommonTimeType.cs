using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework.Util
{
    /// <summary>
    /// 时间类型常量
    /// </summary>
    public static class CommonTimeType
    {
        public static readonly string Year = "Year";
        public static readonly string Month = "Month";
        public static readonly string Week = "Week";
        public static readonly string Day = "Day";
        public static IList<Tuple<string,string>> values=new List<Tuple<string,string>>();

        public static  IList<Tuple<string,string>> Values
        {
            get
            {
                if(values.Count==0)
                {
                    values.Add(new Tuple<string,string>("Year","年"));
                    values.Add(new Tuple<string,string>("Month","月"));
                    values.Add(new Tuple<string,string>("Week","周"));
                    values.Add(new Tuple<string,string>("Day","天"));

                }
                return values;

            }
        }
        public static IList<Tuple<string, string>> Items
        {
            get { return Values; }
        }
            
    }
}
