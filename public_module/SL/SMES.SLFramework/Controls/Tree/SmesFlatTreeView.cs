using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SMES.Framework.Controls.Tree
{
    public class SmesFlatTreeView : TreeView
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return base.GetContainerForItemOverride();
        }
    }
}
