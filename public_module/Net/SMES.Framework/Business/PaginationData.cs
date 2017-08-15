using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework.Business
{
    [Serializable]
    public class PaginationData
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPageNum
        { get; set; }

        /// <summary>
        /// 记录总条数
        /// </summary>
        public int TotalRecordNum
        { get; set; }

        /// <summary>
        /// 每页记录条数
        /// </summary>
        public int PageSize
        { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        { get; set; }

        /// <summary>
        /// 每页记录数据
        /// </summary>
        public object Data
        { get; set; }
    }

    [Serializable]
    public class PaginationData<T>:PaginationData where T:class
    {
        public new IList<T> Data
        {
            get;
            set;
        }
    }
}
