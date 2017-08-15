using SMES.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.OrgInfo
{
    /// <summary>
    /// 企业的model
    /// 
    /// add by wangbin 2015-4-2
    /// </summary>
    public class EnterpriseModel : BaseHierarchyModel
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int SequenceNumber { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        ///岗位Code
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }


    }
}
