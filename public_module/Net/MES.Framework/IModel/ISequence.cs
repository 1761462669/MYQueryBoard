using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.IModel
{
    /// <summary>
    /// 具有顺序的Model
    /// </summary>
    public interface ISequence
    {
        /// <summary>
        /// 顺序
        /// </summary>
        int Seq { get; set; }
       
    }
}
