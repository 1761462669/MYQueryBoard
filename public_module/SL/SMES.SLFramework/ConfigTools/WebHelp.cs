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

namespace SMES.Framework.ConfigTools
{
    public class WebHelp
    {
        static string _webUrl;
        /// <summary>
        /// 得到当前Siliverlight运行的WEB站点地址
        /// 如:http://192.168.0.1/web
        /// </summary>
        public static string WebUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_webUrl))
                {
                    _webUrl = Application.Current.Host.Source.AbsoluteUri;
                    _webUrl = _webUrl.Replace(_webUrl.Substring(_webUrl.IndexOf("/ClientBin")), "");
                    return _webUrl;
                }
                else
                    return _webUrl;
            }
        }

        /// <summary>
        /// 获取公告文件服务URL
        /// </summary>
        public static string GetFilePublicInfoUrl
        {
            get
            {
                string url = "";
                if (!string.IsNullOrEmpty(WebUrl))
                {
                    url = WebUrl + "/Upload/System/PublicInfo";
                }

                return url;
            }
            
        }
    }
}
