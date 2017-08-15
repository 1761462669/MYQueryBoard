using MDF.Framework.Model;
using MES.Framework.IModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Framework.Model
{
    /// <summary>
    /// 实体对象
    /// added by changhl,2015.7.5
    /// </summary>
    public class BaseModel : IBaseModel
    {
        #region feilds && Properties
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Cd { get; set; }

        /// <summary>
        /// 控制码
        /// </summary>
        public string Ctrl { get; set; }

        /// <summary>
        /// 是否再用
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Demo { get; set; }

        /// <summary>
        /// 顺序号
        /// </summary>
        public int Seq { get; set; }

        #endregion

        #region Constructs
        public BaseModel()
        {
            this.IsUsed = true;
        }
        #endregion

        #region Methods

        /// <summary>
        /// 重写Equals方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {

            var castObj = obj as BaseModel;
            if (castObj != null && castObj.Id == this.Id)
            {
                if (this.GetType() == castObj.GetType())
                {
                    return true;

                }
            }


            return false;
        }

        /// <summary>
        /// 重写获取哈希Code方法
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return 7 ^ this.Id.GetHashCode();
        }
        #endregion
    }
}
