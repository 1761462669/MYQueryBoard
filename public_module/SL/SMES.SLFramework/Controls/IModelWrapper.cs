using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SMES.Framework.Controls
{
    /// <summary>
    /// 对Model的封装，添加与展示相关的属性
    /// </summary>
    public interface IModelWrapper
    {
        ModelState State { get; set; }
        object Model { get; set; }
    }
}
