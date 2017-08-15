using MDF.Framework.Model;
using SMES.Framework;
using SMES.FrameworkAdpt.OrgInfo.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.OrgInfo
{
    /// <summary>
    /// 部门的model  主要属性都是继承IDataModel中的
    /// id，cd.name,等基本属性
    /// add by wuyun 2015-04-02
    /// </summary>
    public class AreaModel : BaseHierarchyModel,IResource
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
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
       public IResource Resource { get; set; }
    }
}
