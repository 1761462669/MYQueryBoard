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
using MDF.Framework.Model;

namespace SMES.Framework.Controls
{
    /// <summary>
    /// added by changhl,2015.4.8
    /// </summary>
    public class ModelWrapper : NotifyObject, IModelWrapper
    {

        private int _Key = 1;
        public int Key
        {
            get { return this._Key; }
            set
            {
                if (this._Key != value)
                {
                    this._Key = value;
                    this.RaisePropertyChanged("Key");
                }
            }
        }

        private ModelState _State = ModelState.View;
        public ModelState State
        {
            get { return this._State; }
            set
            {
                if (this._State != value)
                {
                    this._State = value;
                    this.RaisePropertyChanged("State");
                }
            }
        }


        private object _Model;
        public object Model
        {
            get { return this._Model; }
            set
            {
                if (this._Model != value)
                {
                    this._Model = value;
                    this.RaisePropertyChanged("Model");
                }
            }
        }
    }
}
