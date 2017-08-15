using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework.Controls.Graphs
{
    /// <summary>
    /// 添加边事件参数
    /// </summary>
    public class AddEdgeEventArgs : EventArgs
    {
        public object PreData { get; private set; }
        public AddEdgeEventArgs(object preData)
        {
            this.PreData = preData;
        }
    }
}
