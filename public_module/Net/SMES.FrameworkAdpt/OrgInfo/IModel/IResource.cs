using SMES.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.OrgInfo.IModel
{
    public interface IResource : IHierarchyModel
    {
        /// <summary>
        /// 序号
        /// </summary>
        int SequenceNumber { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        string Place { get; set; }

        /// <summary>
        /// 设备简称
        /// </summary>
        string ShortName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        string Remark { get; set; }
    }
}
