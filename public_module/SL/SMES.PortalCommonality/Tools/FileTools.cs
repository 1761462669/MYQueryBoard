using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices.Automation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SMES.PortalCommonality.Tools
{
    public class FileTools
    {
        string downDirString = @"C:\Smes\Downloads";

        /// <summary>
        /// 是否需要下载
        /// </summary>
        /// <param name="srcUrl"></param>
        /// <param name="targetUrl"></param>
        /// <returns></returns>
        public bool IsNeedDownLoads(string srcUrl)
        {
            CheckedDownLoadsFile();

            bool isNeedDownLoad = true;
            string path = downDirString + "\\" + System.IO.Path.GetFileName(srcUrl);
            
            if (File.Exists(path))
            {
                DateTime oldTime = File.GetCreationTime(path);
                DateTime newTime = File.GetCreationTime(srcUrl);

                if (newTime == oldTime)
                {
                    isNeedDownLoad = false;
                }

                isNeedDownLoad = true;
            }

            return isNeedDownLoad;
        }

        public void CheckedDownLoadsFile()
        {
            string path = downDirString;

            // Determine whether the directory exists.
            if (!Directory.Exists(path))
            {
                // Create the directory it does not exist.
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 通过Sream下载文件
        /// </summary>
        /// <param name="str"></param>
        public void DownFromSteam(Stream str,string url)
        {
            CheckedDownLoadsFile();

            byte[] mbyte = new byte[100000000];
            int startmbyte = 0;

            using (StreamReader reader = new StreamReader(str))
            {
                int allmybyte = (int)mbyte.Length;
                while (allmybyte > 0)
                {
                    int m = str.Read(mbyte, startmbyte, allmybyte);
                    if (m == 0)
                        break;

                    startmbyte += m;
                    allmybyte -= m;
                }

                reader.Dispose();
                str.Dispose();
            }

            string path = downDirString + System.IO.Path.GetFileName(url);
            using (FileStream fstr = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fstr.Write(mbyte, 0, startmbyte);
                fstr.Flush();
                fstr.Close();
            }

            using (dynamic cmd = AutomationFactory.CreateObject("WScript.Shell"))
            {
                cmd.Run(path, 1, true);
            }
        }
    }
}
