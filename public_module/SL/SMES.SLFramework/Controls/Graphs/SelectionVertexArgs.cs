using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework.Controls.Graphs
{
    public class SelectionVertexArgs : EventArgs
    {
        public Vertex Selected { get; private set; }
        public SelectionVertexArgs(Vertex node)
        {
            this.Selected = node;
        }

    }
}
