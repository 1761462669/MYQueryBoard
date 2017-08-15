using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework.Controls.Graphs
{
    /// <summary>
    /// 画边事件参数
    /// </summary>
    public class DrawLineEventArgs : EventArgs
    {
        public object PreData { get; set; }
        public object NextData { get; set; }
    }

}
