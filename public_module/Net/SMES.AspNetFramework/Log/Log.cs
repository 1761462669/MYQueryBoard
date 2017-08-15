using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMES.AspNetFramework.Log
{
    public class Log
    {
        private static object lockobj = new object();

        public string Msg
        { get; set; }

        //public string FileName
        //{ get; set; }

        public Exception Error
        { get; set; }


        //public void Write(string msg, string filename)
        //{
        //    WriteLogFileByMsg(msg, filename);
        //}

        public void Write(string msg)
        {
            Msg = msg;
            ThreadPool.QueueUserWorkItem(new WaitCallback(WriteLogFileByMsg));
            //WriteLogFileByMsg(msg);
        }

        public void Write(Exception ex)
        {
            Error = ex;
            WriteLogFileByException(ex);
        }

        private void WriteLogFileByException(Exception ee)
        {
            //StringBuilder substr = new StringBuilder();
            //substr.Append(DateTime.Now.ToString());
            //substr.Append("\r\n");
            //substr.Append(GetExceptionMsg(ee));
            //substr.Append("\r\n");
            //substr.Append("**********************************************");
            //substr.Append("\r\n");

            Write(GetExceptionMsg(ee));
        }

        private string GetExceptionMsg(Exception ex)
        {
            StringBuilder substr = new StringBuilder();
            if (ex == null)
                return "";

            substr.Append("异常信息：" + ex.Message);
            substr.Append("\r\n");
            substr.Append("异常位置：" + ex.Source);
            substr.Append("\r\n");
            substr.Append("异常方法:" + ex.TargetSite);

            if (ex.InnerException != null)
            {
                substr.Append("\r\n");
                substr.Append("**");
                substr.Append("\r\n");
                substr.Append(GetExceptionMsg(ex.InnerException));
            }

            return substr.ToString();
            
        }

        private void WriteLogFileByMsg(object state)
        {            

            StringBuilder substr = new StringBuilder();
            substr.Append(DateTime.Now.ToString());
            substr.Append("\r\n");
            substr.Append(Msg);
            substr.Append("\r\n");
            substr.Append("**********************************************");
            substr.Append("\r\n");
            
            lock (lockobj)
            {
                WriteFile(substr.ToString(), "");
            }
        }

        

        private void WriteFile(string msg, string filename)
        {
            string rfilename = GetLogFileName(filename);
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(rfilename, true))
            {
                sw.Write(msg);
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }

        private string GetLogFileName(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                filename = "log";

            DateTime dtime = DateTime.Now;
            string dir = AppDomain.CurrentDomain.BaseDirectory + "Log\\" + dtime.ToString("yyyyMMdd") + "\\";

            if (!System.IO.Directory.Exists(dir))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                catch
                { }
            }

            return dir + dtime.ToString("yyyyMMddHH") + "_" + filename + ".log";
        }
    }
    
}
