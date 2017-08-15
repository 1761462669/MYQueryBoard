using MDF.Service.Host.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MDF.Service.Host
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var ex = e.Exception;
            string error = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
            ViewModelServiceError.Instance().Errors.Add(error);
        }
    }



}
