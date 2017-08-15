using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework
{
    public interface IDataModel : IRootModel, IModelBasicProperties
    {
        /// <summary>
        /// 编码
        /// </summary>

        /// <summary>
        /// 主键
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// 是否在用
        /// </summary>

    }


    public interface IModelBasicProperties
    {
        string Cd { get; set; }
        /// <summary>
        /// 控制码
        /// </summary>
        string Ctrl { get; set; }

        bool IsUsed { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }

    }

    public interface IBusinessModel : IModelBasicProperties, IRootModel
    {


        string Id { get; set; }





    }


}
