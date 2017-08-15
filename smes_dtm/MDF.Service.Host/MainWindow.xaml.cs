using MDF.Service.Host.Rmt;
using MDF.Service.Host.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MDF.Service.Host
{
    public delegate void StartSevice();
    /// <summary>
    /// 服务管理主窗口
    /// </summary>
    public partial class MainWindow : Window
    {
        #region feilds && Propertys

        StartSevice startDelegate;
        /// <summary>
        /// 业务服务宿主
        /// </summary>
        ServiceHost businessServiceHost = null;
        /// <summary>
        /// 跨域访问宿主
        /// </summary>
        ServiceHost crossDomainServiceHost = null;


        RmtServer rmtserver;
        //ViewModleMessage vmMessages = new ViewModleMessage();
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            var vm = this.Resources["ViewModleMessage"] as ViewModleMessage;
            if (vm != null)
            {
                vm.CompletedConfig -= vm_CompletedConfig;
            }
            vm.CompletedConfig += vm_CompletedConfig;

            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Button_Click(null, null);
            this.tabMonitor.DataContext = MDF.Framework.Bus.ServiceMonitor.Instance();
            this.errorListBox.ItemsSource = ViewModelServiceError.Instance().Errors;
        }

        void vm_CompletedConfig(object sender, EventArgs e)
        {
            if (this.startDelegate == null)

                this.startDelegate = () =>
                    {
                        //获取
                        var vm = sender as ViewModleMessage;
                        if (this.businessServiceHost != null)
                        {
                            this.businessServiceHost.Opened -= this.businessServiceHost_Opened;
                            this.businessServiceHost.Close();

                        }
                        if (this.crossDomainServiceHost != null)
                        {
                            this.crossDomainServiceHost.Close();
                            this.crossDomainServiceHost.Opened -= this.crossDomainServiceHost_Opened;
                        }

                        this.crossDomainServiceHost = new ServiceHost(typeof(MDF.Service.CrossDomainService));
                        this.businessServiceHost = new ServiceHost(typeof(MDF.Service.BusinessService));
                        this.crossDomainServiceHost.Opened += crossDomainServiceHost_Opened;
                        this.businessServiceHost.Opened += businessServiceHost_Opened;
                        vm.DataSource[4].Message = "正在启动跨域访问服务....";
                        vm.DataSource[4].State = EMessageState.Run;
                        vm.DataSource[4].StartTime = DateTime.Now;
                        this.crossDomainServiceHost.Open();
                        vm.DataSource[5].Message = "正在启动业务服务....";
                        vm.DataSource[5].StartTime = DateTime.Now;
                        this.businessServiceHost.Open();
                        vm.DataSource[5].State = EMessageState.Run;

                        vm.DataSource[6].Message = "正在启动远程访问服务...";
                        vm.DataSource[6].State = EMessageState.Run;
                        vm.DataSource[6].StartTime = DateTime.Now;
                        rmtserver = new RmtServer();
                        rmtserver.Start();
                        vm.DataSource[6].State = EMessageState.Finish;
                        vm.DataSource[6].Message = "远程服务启动完成[tcp://127.0.0.1:"+rmtserver.Port+"/"+rmtserver.SericeName+"]";
                    };
            this.Dispatcher.BeginInvoke(this.startDelegate);

        }

        void host_Opened(object sender, EventArgs e)
        {

        }

        private void ConfigLog()
        {
            var vm = this.Resources["ViewModleMessage"] as ViewModleMessage;
            var messageLog = vm.DataSource[0];
            messageLog.State = EMessageState.Run;
            MDF.Framework.Log.LogBase.Config();
            messageLog.Message = "日志配置结束！";
            messageLog.State = EMessageState.Finish;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.Resources["ViewModleMessage"] as ViewModleMessage;

            try
            {

                vm.IntialMessages();
                Thread thread = new Thread(() =>
                {

                    vm.Config();
                });
                thread.Start();

            }
            catch (Exception ex)
            {

                vm.ExcptionMessage += ex.Message;
            }
        }

        private void businessServiceHost_Opened(object sender, EventArgs e)
        {
            var vm = this.Resources["ViewModleMessage"] as ViewModleMessage;
            vm.DataSource[5].Message = "业务服务已启动！";
            vm.DataSource[5].State = EMessageState.Finish;
            vm.DataSource[5].EndTime = DateTime.Now;
            vm.BusinessServiceAdress = new ObservableCollection<string>(this.businessServiceHost.Description.Endpoints.Select(c => c.Address.Uri.AbsoluteUri).ToList());
        }

        private void crossDomainServiceHost_Opened(object sender, EventArgs e)
        {
            var vm = this.Resources["ViewModleMessage"] as ViewModleMessage;
            vm.DataSource[4].Message = "跨域访问服务已启动！";
            vm.DataSource[4].State = EMessageState.Finish;
            vm.DataSource[4].EndTime = DateTime.Now;
            vm.CrossDomainServiceAdress = new ObservableCollection<string>(this.crossDomainServiceHost.Description.Endpoints.Select(c => c.Address.Uri.AbsoluteUri).ToList());
        }

        private void TabControl_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void btClearMonitorInfos_Click(object sender, RoutedEventArgs e)
        {
            MDF.Framework.Bus.ServiceMonitor.Instance().Infos.Clear();
        }

    }
}
