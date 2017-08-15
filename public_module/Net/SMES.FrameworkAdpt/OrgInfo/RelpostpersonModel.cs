using SMES.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.OrgInfo
{
    public class RelpostpersonModel :RootModel
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 岗位
        /// </summary>
        public PostModel PostModel { get; set; }
        /// <summary>
        /// 人员
        /// </summary>
        public PersonModel PersonModel { get; set; }

        /// <summary>
        /// 顺序码
        /// </summary>
        public int SequenceNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
