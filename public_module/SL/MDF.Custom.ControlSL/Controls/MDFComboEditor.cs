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

namespace MDF.Custom.ControlSL.Controls
{
    public class MDFComboEditor : ComboBox,System.ComponentModel.INotifyPropertyChanged
    {
        public MDFComboEditor()
        {
            this.DefaultStyleKey = typeof(MDFComboEditor);
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        private void RaisedPropertyChanged(string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(name));
            }
        }
    }
}
