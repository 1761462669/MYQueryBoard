using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MDF.Service.Host
{
    public class ServiceMessage : NotifyObject
    {
        private string _Message;
        /// <summary>
        /// 消息
        /// </summary>
        public string Message
        {
            get { return this._Message; }
            set
            {
                if (this._Message != value)
                {
                    this._Message = value;
                    this.RaisedPropertyChanged("Message");
                }
            }
        }


        private int _Seq;
        public int Seq
        {
            get { return this._Seq; }
            set
            {
                if (this._Seq != value)
                {
                    this._Seq = value;
                    this.RaisedPropertyChanged("Seq");
                }
            }
        }



        
        private DateTime _StartTime;
        public DateTime StartTime
        {
            get { return this._StartTime; }
            set
            {
                if (this._StartTime != value)
                {
                    this._StartTime = value;
                    this.RaisedPropertyChanged("StartTime");
                }
            }
        }


        
        private DateTime _EndTime;
        public DateTime EndTime
        {
            get { return this._EndTime; }
            set
            {
                if (this._EndTime != value)
                {
                    this._EndTime = value;
                    this.RaisedPropertyChanged("EndTime");
                    var span = this.EndTime - this.StartTime;
                    this.ConsumeSeconds = span.TotalSeconds;
                }
            }
        }


        
        private double _ConsumeSeconds;
        public double ConsumeSeconds
        {
            get { return this._ConsumeSeconds; }
            set
            {
                if (this._ConsumeSeconds != value)
                {
                    this._ConsumeSeconds = value;
                    this.RaisedPropertyChanged("ConsumeSeconds");
                }
            }
        }

        
        private EMessageState _State;
        public EMessageState State
        {
            get { return this._State; }
            set
            {
                if (this._State != value)
                {
                    this._State = value;
                    this.RaisedPropertyChanged("State");
                }
            }
        }

    }

    public enum EMessageState
    {
        New = 0,
        Run = 1,
        Finish = 2,
    }
}
