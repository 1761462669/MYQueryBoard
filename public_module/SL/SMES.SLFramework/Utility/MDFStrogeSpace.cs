using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SMES.Framework.Utility
{
    public class StrogeSpaceUnity
    {
        /// <summary> 
        /// 申请独立存储空间 
        /// </summary> 
        /// <param name="size"></param> 
        /// <returns></returns> 
        public static bool ApplayStrogeSpace(double size)
        {
            try
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    Int64 quotSize = Convert.ToInt64(size * 1024);
                    Int64 curSize = store.AvailableFreeSpace;
                    if (curSize < quotSize)
                    {
                        if (store.IncreaseQuotaTo(quotSize))
                        { return true; }
                        else
                        { return false; }
                    }
                    else
                    { return true; }
                }
            }
            catch (IsolatedStorageException ex)
            { throw new IsolatedStorageException("申请独立存储空间失败！" + ex.Message); }
        }

        /// <summary> 
        /// 保存字符串到文件 
        /// </summary> 
        /// <param name="data"></param> 
        /// <param name="fileName"></param> 
        public static void SaveString(string data, string fileName)
        {
            if (!IsolatedStorageFile.IsEnabled)
            {
                MessageBox.Show("请先启用应用程序存储，才可以记住密码。(开始->Microsoft Silverlight->Microsoft Silverlight)");
                return;
            }

            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isf.FileExists(fileName))
                { isf.DeleteFile(fileName); }

                using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream(fileName, FileMode.Create, isf))
                {
                    using (var sw = new StreamWriter(isfs))
                    {
                        sw.Write(data);
                        sw.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filename"></param>
        public static void DeleteFile(string filename)
        {
            if (!IsolatedStorageFile.IsEnabled)
            {
                //MessageBox.Show("请先启用应用程序存储，才可以记住密码。(开始->Microsoft Silverlight->Microsoft Silverlight)");
                return;
            }

            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isf.FileExists(filename))
                    isf.DeleteFile(filename);
            }
        }

        /// <summary> 
        /// 泛类型支持存储文件 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="Tdata"></param> 
        /// <param name="fileName"></param> 
        public static void SaveTData<T>(T Tdata, string fileName)
        {
            if (!IsolatedStorageFile.IsEnabled)
            {
                //MessageBox.Show("请先启用应用程序存储，才可以记住密码。(开始->Microsoft Silverlight->Microsoft Silverlight)");
                return;
            }

            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isf.FileExists(fileName))
                {
                    isf.DeleteFile(fileName);
                }
                IsolatedStorageFileStream isfs = isf.CreateFile(fileName);
                DataContractSerializer ser = new DataContractSerializer(typeof(T));
                ser.WriteObject(isfs, Tdata);
                isfs.Close();
            }
        }

        /// <summary> 
        /// 返回字符串 
        /// </summary> 
        /// <param name="fileName"></param> 
        /// <returns></returns> 
        public static string FindData(string fileName)
        {
            string data = string.Empty;
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isf.FileExists(fileName))
                {
                    using (var isfs = new IsolatedStorageFileStream(fileName, FileMode.Open, isf))
                    {
                        using (var sr = new StreamReader(isfs))
                        {
                            string lineData;
                            while ((lineData = sr.ReadLine()) != null) { data += lineData; }
                        }
                    }
                }

            }
            return data;
        }


        /// <summary> 
        /// 泛类型返回 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="fileName"></param> 
        /// <returns></returns> 
        public static T FindTData<T>(string fileName)
        {
            if(!IsolatedStorageFile.IsEnabled)
            {
                return default(T);
            }

            T data = default(T);
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isf.FileExists(fileName))
                {
                    IsolatedStorageFileStream isfs = isf.OpenFile(fileName, FileMode.Open);
                    var ser = new DataContractSerializer(typeof(T));
                    data = (T)ser.ReadObject(isfs);
                    isfs.Close();
                }
            }
            return data;
        } 

    }
}
