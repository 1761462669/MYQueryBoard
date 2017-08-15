using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.Utility
{
    /// <summary>
    /// 责任链的上下文
    /// addedy by changhl,2013-12-30
    /// </summary>
    public class ChainContext : Dictionary<string, object>
    {

    }

    public interface IHandleNode
    {

        ChainContext Context { get; set; }
        /// <summary>
        /// 是否处理
        /// </summary>
        /// <returns></returns>
        bool IsHandle();
        /// <summary>
        /// 处理
        /// </summary>
        void Handle();
    }


    public class ResponsibilityChain : IHandleNode
    {
        public List<IHandleNode> Nodes { get; set; }

        public ChainContext Context { get; set; }

        public virtual bool IsHandle()
        {
            return true;
        }

        public void Handle()
        {
            foreach (var node in Nodes)
            {
                node.Context = Context;
                
                if(node.IsHandle())
                {
                    node.Handle();
                }
            }
        }
    }
}
