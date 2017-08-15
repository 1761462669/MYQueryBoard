using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetFramework
{
    public static class DataConvert
    {
        /// <summary>
        /// string转换 Int
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static int ToInt(this string value)
        {
            return ToInt(value, 0);
        }

        /// <summary>
        /// string转换 Int
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="defvalue">默认值</param>
        /// <returns>结果</returns>
        public static int ToInt(this string value, int defvalue)
        {
            if (string.IsNullOrEmpty(value))
                return defvalue;

            int result = defvalue;

            if (int.TryParse(value, out result))
                return result;
            else
                return defvalue;
        }

        /// <summary>
        /// string转换 double
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="defvalue">默认值</param>
        /// <returns>结果</returns>
        public static double ToDouble(this string value, double defvalue)
        {
            if (string.IsNullOrEmpty(value))
                return defvalue;

            double rlt = defvalue;

            if (double.TryParse(value, out rlt))
                return rlt;
            else
                return defvalue;
        }

        /// <summary>
        /// string转换 double
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static double ToDouble(this string value)
        {
            return ToDouble(value, 0d);
        }

        /// <summary>
        /// string转换 DateTime
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static DateTime ToDateTime(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return new DateTime();

            DateTime rlt = new DateTime();

            if (DateTime.TryParse(value, out rlt))
                return rlt;
            else
                return new DateTime();
        }

        /// <summary>
        /// 星期转换为周
        /// </summary>
        /// <param name="week">星期</param>
        /// <param name="mode">模式</param>
        /// <returns>结果</returns>
        public static string Change(this DayOfWeek week, WeekMode mode)
        {
           
            string[][] language ={
                                new string[]{"星期日","星期一","星期二","星期三","星期四","星期五","星期六" },
                                new string[]{ "日", "一", "二", "三", "四", "五", "六" },
                                new string[]{ "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"},
                                new string[]{ "Sun", "Mon", "Tue", "Wed", "Thursday", "Fri", "Sat" }
                                };

            return language[(int)mode][(int)week];
        }

        /// <summary>
        /// 星期转换为周
        /// </summary>
        /// <param name="week">星期</param>
        /// <returns>结果</returns>
        public static string Change(this DayOfWeek week)
        {
            return week.Change(WeekMode.CHS);
        }
       
    }

    /// <summary>
    /// 星期显示模式
    /// </summary>
    public enum WeekMode
    { 
        /// <summary>
        /// 中文全称
        /// </summary>
        CHS=0,

        /// <summary>
        /// 中文简称
        /// </summary>
        CHSSimple=1,

        /// <summary>
        /// 英文全称
        /// </summary>
        EN=2,

        /// <summary>
        /// 英文简称
        /// </summary>
        ENSimple=3
        
    }
}
