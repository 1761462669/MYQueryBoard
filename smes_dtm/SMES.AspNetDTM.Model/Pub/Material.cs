using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetDTM.Model.Pub
{
    [Serializable]
    /// <summary>
    /// 物料模型
    /// </summary>
    public class Material
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id
        { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        { get; set; }

        /// <summary>
        /// 物料类型
        /// </summary>
        public int MaterialTypeID
        { get; set; }

        /// <summary>
        /// CD
        /// </summary>
        public string Cd { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed { get; set; }

        
    }
}
