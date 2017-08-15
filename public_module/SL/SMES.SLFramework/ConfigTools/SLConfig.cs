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
using System.Xml.Linq;

namespace SMES.Framework.ConfigTools
{
    public class SLConfig
    {
        /// <summary>
        /// 配置文件加载完成事件
        /// </summary>
        public event EventHandler LoadCompleta;

        /// <summary>
        /// 配置信息保存字典
        /// </summary>
        private static Dictionary<string, string> ConfigDic
        { get; set; }

        public static bool IsLoadCompleta
        { get; set; }

        /// <summary>
        /// 开始读取配置信息
        /// </summary>
        public void BeginReadWebConfig()
        {

            BeginReadWebConfig("/SLConfig/config.xml");

        }

        /// <summary>
        /// 开始读取配置信息
        /// </summary>
        public void BeginReadWebConfig(string configfilepath)
        {
            string weburl = WebHelp.WebUrl + configfilepath;
            WebRequest request = WebRequest.CreateHttp(weburl);
            AsyncCallback asyncrequest = new AsyncCallback(ReadServerConfig);
            try
            {
                request.BeginGetResponse(asyncrequest, request);
            }
            catch
            {
                throw new Exception("配置文件请求错误!");
            }
        }


        private void ReadServerConfig(IAsyncResult result)
        {
            WebRequest requerst = result.AsyncState as WebRequest;
            if (requerst == null)
                return;

            WebResponse response = requerst.EndGetResponse(result);
            System.IO.Stream sr = response.GetResponseStream();
            ReadXmlLinq(sr);
            sr.Close();
            requerst = null;

            if (LoadCompleta != null)
                LoadCompleta(this, new EventArgs());

            IsLoadCompleta = true;
        }

        private void ReadXmlLinq(System.IO.Stream stream)
        {
            XDocument document = XDocument.Load(stream);
            XElement pelement = document.Element("Config");

            foreach (XElement element in pelement.Elements("add"))
            {
                SetConfigDic(element.Attribute("key").Value, element.Attribute("value").Value);
            }
        }

        /// <summary>
        /// 设置配置信息
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetConfigDic(string key, string value)
        {
            if (ConfigDic == null)
                ConfigDic = new Dictionary<string, string>();

            if (ConfigDic.ContainsKey(key))
                ConfigDic[key] = value;
            else
                ConfigDic.Add(key, value);
        }

        /// <summary>
        /// 得到配置信息
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetConfigDic(string key)
        {
            if (ConfigDic == null)
                return "";

            if (ConfigDic.ContainsKey(key))
                return ConfigDic[key];
            else
                return "";
        }
    }
}
