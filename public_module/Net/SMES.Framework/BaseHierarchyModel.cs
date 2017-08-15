using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework
{
    public class BaseHierarchyModel : DataModel, IHierarchyModel
    {
        public IHierarchyModel Parent { set; get; }

        public IList<IHierarchyModel> Childs { get; set; }

        public object Data { get; set; }
    }
}
