using MDF.Framework.Bus;
using SMES.FrameworkAdpt.CommonPara.Model;
using SMES.FrameworkAdpt.CommonPara.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SMES.Framework.Markup.Para
{
    public class CommonParaUnity
    {
        public event RoutedEventHandler DatasLoadedComplated;

        /// <summary>
        /// 加载参数字典
        /// </summary>
        public async void LoadParas()
        {
            SynInvokeWcfService wcf = new SynInvokeWcfService();
            var result = await wcf.Invoke<ICommonParaService>(c => c.GetAllParas());

            if (result.IsSuccess)
            {
                var list = InfoExchange.DeConvert<List<CommonParaModel>>(result.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);

                CommonPara.dic.Clear();
                foreach (var item in list)
                {
                    CommonPara.dic.Add(item.KeyCode, item.ParaValue);
                }

                if(DatasLoadedComplated != null)
                {
                    DatasLoadedComplated(this, new RoutedEventArgs());
                }
            }
        }
    }
}
