using SMES.PortalCommonality.UserInfo;
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

namespace SMES.PortalCommonality
{
    public class PortalHelper
    {
        public static IPortal Portal
        { get; set; }

        #region 消息

        /// <summary>
        /// 显示Portal消息
        /// added by zongyh
        /// </summary>
        /// <param name="msg">消息内容</param>
        /// <param name="type">消息类型</param>
        public static void ShowMsg(string msg, MsgStateEnum type)
        {
            if (Portal == null)
            {
                MessageBox.Show(msg);
            }
            else
            {
                Portal.ShowMsg(msg, type);
            }
        }

        #endregion

        #region 用户信息

        /// <summary>
        /// 获取当前登录用户信息
        /// added by zongyh
        /// </summary>
        public static PortalUser CurrentUser
        {
            get
            {
                if (Portal == null)
                {
                    return new PortalUser() { UserId = "68392", UserName = "admin", PersonId = "68392" };
                }
                else
                {
                    if (Portal.User == null)
                    {
                        throw new Exception("无法获取当前登录用户信息");
                    }

                    return Portal.User;
                }
            }
        }

        #endregion

        #region 等待

        private bool _IsBusy;

        /// <summary>
        /// 是否显示等待
        /// </summary>
        public bool IsBusy
        {
            get { return this._IsBusy; }
            set
            {
                this._IsBusy = value;

                if(Portal != null)
                {
                    Portal.IsBusy = value;
                }
            }
        }



        #endregion
    }
}
