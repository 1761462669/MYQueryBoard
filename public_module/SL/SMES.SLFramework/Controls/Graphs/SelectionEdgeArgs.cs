using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework.Controls.Graphs
{
    public class SelectionEdgeArgs : EventArgs
    {
        public Edge Selected { get; private set; }
        public SelectionEdgeArgs(Edge node)
        {
            this.Selected = node;
        }
    }
}
