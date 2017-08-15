using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetFramework.Model
{
    public class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Id
        { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName
        { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public string DeptId
        { get; set; }
    }
}
