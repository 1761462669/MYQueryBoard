using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.IModel
{
    /// <summary>
    /// 能力接口
    /// </summary>
    public interface ICapability
    {
        decimal Rate { get; set; }

        /// <summary>
        /// 获取能力
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        decimal GetCapability(DateTime startTime, DateTime endTime);

        /// <summary>
        /// 获取能力
        /// </summary>
        /// <returns></returns>
        decimal GetCapability();

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        IResourceCapability Copy();




       


    }
    /// <summary>
    /// 单资源能力
    /// </summary>
    public interface IResourceCapability : ITimeSegment,ICapability
    {
        /// <summary>
        /// 资源Id
        /// </summary>
        string ResourceId { get; set; }

        string ResourceName { get; set; }

        string EquShortName { get; set; }

        string ShiftId { get; set; }

        string ShiftName { get; set; }

        string TeamId { get; set; }

        string TeamName { get; set; }

    }

    /// <summary>
    /// 组合资源能力
    /// </summary>
    public interface ICompositeCapability:IList<IResourceCapability>, ICapability
    {

    }



    //public interface IComposit
}
