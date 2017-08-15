using MDF.Framework.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.CommonPara.Service
{
    public class CommonParaUnity
    {
        /// <summary>
        /// 根据KeyCode获取参数值
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        public static string GetParaValueByKeyCode(string keyCode)
        {
            if (CommonParaService.CommonParas.Count == 0)
            {
                var list = ObjectFactory.GetObject<ICommonParaService>().GetAllParas();

                if(list.Count == 0)
                {
                    throw new Exception("参数集合为空");
                }
            }

            if (!CommonParaService.CommonParas.Keys.Contains(keyCode))
                throw new Exception("集合中找不到" + keyCode);

            return CommonParaService.CommonParas.FirstOrDefault(c => c.Key == keyCode).Value;
        }
    }
}
