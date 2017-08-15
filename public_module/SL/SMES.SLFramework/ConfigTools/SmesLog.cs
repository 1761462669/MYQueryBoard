using System;
using System.IO;
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
    public class SmesLog
    {
        public static void WriteErrLog(Exception ex)
        {
            StreamWriter sw;

            var c = Application.Current;

            string strErrLog =
                 string.Format(@"C:/MES/log/WebLog{0}.txt",DateTime.Now.Date.ToString("yyyyMMdd"));   //获取写日志的路径
            if (File.Exists(strErrLog))
            {
                FileInfo oFile = new FileInfo(strErrLog);
                if (oFile.Length > 1024000)
                {
                    oFile.Delete();
                }
            }
            if (File.Exists(strErrLog))
            {
                sw = File.AppendText(strErrLog);
            }
            else
            {
                sw = File.CreateText(strErrLog);
            }
            string strDate = "出错时间:" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            sw.WriteLine(strDate);
            sw.WriteLine(ex);
            sw.WriteLine("===================================================================");
            sw.Flush();
            sw.Close();
        }
    }
}
