using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Com.IExternalData
{
    /// <summary>
    /// 获取实时数据接口
    /// </summary>
    public interface IHistorian
    {
        /// <summary>
        /// 获取数据点当前值
        /// </summary>
        /// <param name="tag">Tag点名称</param>
        /// <returns>值</returns>
        object GetValue(string tag);

        /// <summary>
        /// 获取Tag点当前值
        /// </summary>
        /// <param name="tags">Tag点列表</param>
        /// <returns>返回值</returns>
        Dictionary<string, object> GetValues(params string[] tags);

        /// <summary>
        /// 根据DataTable获取Tag点值(替换原列数据)
        /// </summary>
        /// <param name="dt">配置表</param>
        /// <param name="cols">配置列</param>
        /// <returns>数据</returns>
        DataTable ReplaceTableCellValue(DataTable dt, params string[] cols);

        /// <summary>
        /// 根据DataTable获取Tag点值(添加新列)
        /// </summary>
        /// <param name="dt">配置表</param>
        /// <param name="cols">配置列</param>
        /// <returns>数据</returns>
        DataTable AddTableColumnValue(DataTable dt, params string[] cols);
    }
}
