using MES.Framework.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.Model
{
    /// <summary>
    /// 具有层级结构的实体
    /// </summary>
    public class HierarchyModel : BaseModel, IHierarchyModel
    {
        /// <summary>
        /// 父对象
        /// </summary>
        public IHierarchyModel Parent { get; set; }

        /// <summary>
        /// 子对象
        /// </summary>
        public IList<IHierarchyModel> Childs { get; set; }


        /// <summary>
        /// 获取根节点
        /// </summary>
        /// <returns></returns>
        public IHierarchyModel GetRoot()
        {
            var temp = this as IHierarchyModel;
            while (temp.Parent != null)
            {
                temp = temp.Parent;
            }
            return temp;
        }

        /// <summary>
        /// 获取所有叶子节点，如果本身是叶子节点，返回本身
        /// </summary>
        /// <returns></returns>
        public IList<IHierarchyModel> GetLeafs()
        {

            if (this.IsLeaf)
                return new List<IHierarchyModel>() { this };
            var list = new List<IHierarchyModel>();
            foreach (var child in this.Childs)
            {
                var leafs= child.GetLeafs();
                list = list.Union(leafs).ToList();
            }
            return list;



        }


        /// <summary>
        /// 是否是叶子节点
        /// </summary>
        public bool IsLeaf
        {
            get { return this.Childs == null || this.Childs.Count == 0; }
        }
    }
}
