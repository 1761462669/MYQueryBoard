using SMES.AspNetFramework.Excpt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetFramework.Web
{
    public class RpsData
    {
        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool IsSucceed
        { get; set; }


        /// <summary>
        /// 错误信息
        /// </summary>
        public string Error
        { get; set; }

        public int ErrorCode
        { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data
        { get; set; }

        public RpsData()
        {
            IsSucceed = true;
            Error = "";
        }

        #region 错误请求
        public static RpsData Fail(string message)
        {
            return Fail(0, message);
        }

        public static RpsData Fail(int errorcode, string message)
        {
            return new RpsData() { Error = message, ErrorCode = errorcode, IsSucceed = false };
        }

        public static RpsData Fail(CustomException ex)
        {
            return Fail(ex.Code, ex.Message);
        }
        #endregion

        #region 成功请求
        public static RpsData Succee(object data)
        {
            return new RpsData() { Data = data };
        }
        #endregion
    }
}
