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
using SMES.PortalCommonality.UserInfo;

namespace SMES.PortalCommonality
{
    public interface IPortal
    {
        /// <summary>
        /// 打开URL
        /// </summary>
        /// <param name="url"></param>
        void Open(string url);

        void ShowMsg(string msg,MsgStateEnum state);

        /// <summary>
        /// Portal顶层展示消息（可控制是否自动关闭）
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="state"></param>
        void ShowMsg(string msg, MsgStateEnum state,bool isAutoClose);

        PortalUser User { get; set; }

        bool IsBusy { get; set; }
    }

    public enum MsgStateEnum
    {
        Success = 1,
        Faild = 2,
        Info = 3
    }
}
