using MDF.Framework.Model;
using SMES.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.OrgInfo
{
    /// <summary>
    /// 工作岗位，包含基本的id，name属性和部门属性
    /// add by  wuyun  2015-04-03
    /// </summary>
    public class PostModel : DataModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int SequenceNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
       public AreaModel Area { get; set; }
    }
}
