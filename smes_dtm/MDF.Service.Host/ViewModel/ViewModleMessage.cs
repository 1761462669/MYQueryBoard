using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDF.Framework.Bus;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Media;
using System.ServiceModel;

namespace MDF.Service.Host.ViewModel
{
    public class ViewModleMessage : NotifyObject
    {

        public event EventHandler CompletedConfig;

        private ObservableCollection<ServiceMessage> _DataSource = new ObservableCollection<ServiceMessage>();
        public ObservableCollection<ServiceMessage> DataSource
        {
            get { return this._DataSource; }
            set
            {
                if (this._DataSource != value)
                {
                    this._DataSource = value;
                    this.RaisedPropertyChanged("DataSource");
                }
            }
        }


        private ObservableCollection<string> _BusinessServiceAdress = new ObservableCollection<string>();
        public ObservableCollection<string> BusinessServiceAdress
        {
            get { return this._BusinessServiceAdress; }
            set
            {
                if (this._BusinessServiceAdress != value)
                {
                    this._BusinessServiceAdress = value;
                    this.RaisedPropertyChanged("BusinessServiceAdress");
                }
            }
        }


        private ObservableCollection<string> _CrossDomainServiceAdress = new ObservableCollection<string>();
        public ObservableCollection<string> CrossDomainServiceAdress
        {
            get { return this._CrossDomainServiceAdress; }
            set
            {
                if (this._CrossDomainServiceAdress != value)
                {
                    this._CrossDomainServiceAdress = value;
                    this.RaisedPropertyChanged("CrossDomainServiceAdress");
                }
            }
        }


        private string _ExcptionMessage;
        public string ExcptionMessage
        {
            get { return this._ExcptionMessage; }
            set
            {
                if (this._ExcptionMessage != value)
                {
                    this._ExcptionMessage = value;
                    this.RaisedPropertyChanged("ExcptionMessage");
                }
            }
        }

        public ViewModleMessage()
        {
            IntialMessages();
        }

        public void IntialMessages()
        {
            this.DataSource.Clear();
            this.DataSource.Add(new ServiceMessage() { Seq = 1, Message = "配置日志", State = EMessageState.New });
            this.DataSource.Add(new ServiceMessage() { Seq = 2, Message = "配置Ioc容器", State = EMessageState.New });
            this.DataSource.Add(new ServiceMessage() { Seq = 3, Message = "配置熟知类型", State = EMessageState.New });
            this.DataSource.Add(new ServiceMessage() { Seq = 4, Message = "配置服务代理缓存", State = EMessageState.New });
            this.DataSource.Add(new ServiceMessage() { Seq = 5, Message = "启动跨域访问服务", State = EMessageState.New });
            this.DataSource.Add(new ServiceMessage() { Seq = 6, Message = "启动业务服务", State = EMessageState.New });

            this.DataSource.Add(new ServiceMessage() { Seq = 7, Message = "启动Remoting服务", State = EMessageState.New });

            this.ExcptionMessage = null;
            this.CrossDomainServiceAdress.Clear();
            this.BusinessServiceAdress.Clear();
        }

        public void Config()
        {

            try
            {
                var messageLog = this.DataSource[0];
                messageLog.Message = "开始配置日志.....";
                messageLog.State = EMessageState.Run;
                messageLog.StartTime = DateTime.Now;
                MDF.Framework.Log.LogBase.Config();
                messageLog.EndTime = DateTime.Now;
                messageLog.Message = "日志配置结束！";
                messageLog.State = EMessageState.Finish;

                this.DataSource[1].Message = "开始配置Ioc容器.....";
                this.DataSource[1].State = EMessageState.Run;
                this.DataSource[1].StartTime = DateTime.Now;
                MDF.Framework.Bus.ObjectFactory.Config();
                this.DataSource[1].EndTime = DateTime.Now;
                this.DataSource[1].State = EMessageState.Finish;
                this.DataSource[1].Message = "Ioc容器配置结束";

                this.DataSource[2].Message = "开始配置熟知类型.....";
                this.DataSource[2].State = EMessageState.Run;
                this.DataSource[2].StartTime = DateTime.Now;
                MDF.Framework.Bus.ObjectFactory.ParseAssemblys();
                this.DataSource[2].EndTime = DateTime.Now;
                this.DataSource[2].State = EMessageState.Finish;
                this.DataSource[2].Message = "熟知类型结束";

                this.DataSource[3].Message = "开始配置服务代理缓存.....";
                this.DataSource[3].State = EMessageState.Run;
                this.DataSource[3].StartTime = DateTime.Now;
                MDF.Framework.Bus.ObjectFactory.CreateTypeProxy();
                this.DataSource[3].EndTime = DateTime.Now;
                this.DataSource[3].State = EMessageState.Finish;
                this.DataSource[3].Message = "服务代理缓存配置结束";
                if (this.CompletedConfig != null)
                    this.CompletedConfig(this, new EventArgs());

            }
            catch (Exception ex)
            {
                this.ExcptionMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message; ;
            }


        }

        void businessServiceHost_Opened(object sender, EventArgs e)
        {

        }

        void crossDomainServiceHost_Opened(object sender, EventArgs e)
        {
            this.DataSource[4].Message = "跨域访问服务已启动！";
            this.DataSource[4].State = EMessageState.Finish;
        }
    }

    public class MessageStateConverter : IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var state = (EMessageState)value;
            switch (state)
            {
                case EMessageState.New:
                    return new SolidColorBrush(Colors.Red);
                case EMessageState.Run:
                    return new SolidColorBrush(Colors.Yellow);
                case EMessageState.Finish:
                    return new SolidColorBrush(Colors.Green);
                default:
                    break;
            };
            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class ColorSucessConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var flag = (bool)value;
            if (flag)
                return Colors.Green;
            else
                return Colors.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
