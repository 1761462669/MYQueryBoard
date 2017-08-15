using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.IModel
{
    /// <summary>
    /// 基础实体基类
    /// </summary>
    public interface IBaseModel:ISequence
    {
        #region feilds && Properties
        /// <summary>
        /// 主键
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        string Cd { get; set; }

        /// <summary>
        /// 控制码
        /// </summary>
        string Ctrl { get; set; }

        /// <summary>
        /// 是否再用
        /// </summary>
        bool IsUsed { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        string Demo { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        int Seq { get; set; }

        #endregion
    }
}
