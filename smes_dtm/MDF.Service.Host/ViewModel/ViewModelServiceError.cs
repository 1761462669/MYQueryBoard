using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MDF.Service.Host.ViewModel
{
    public class ViewModelServiceError : NotifyObject
    {
        private static ViewModelServiceError _instance=new ViewModelServiceError();
        public static ViewModelServiceError Instance()
        {
            return _instance;
        }
        

        private ObservableCollection<string> _Errors = new ObservableCollection<string>();
        public ObservableCollection<string> Errors
        {
            get { return this._Errors; }
            set
            {
                if (this._Errors != value)
                {
                    this._Errors = value;
                    this.RaisedPropertyChanged("Errors");
                }
            }
        }
        private ViewModelServiceError()
        {

        }

        
    }
}
