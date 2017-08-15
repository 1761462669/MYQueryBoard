using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.Framework
{
    public class BaseHierarchyModel : DataModel, IHierarchyModel
    {

        private IHierarchyModel _Parent;
        public IHierarchyModel Parent
        {
            get { return this._Parent; }
            set
            {
                if (this._Parent != value)
                {
                    this._Parent = value;
                    this.RaisePropertyChanged("Parent");
                }
            }
        }


        private IList<IHierarchyModel> _Childs=new List<IHierarchyModel>();
        public IList<IHierarchyModel> Childs
        {
            get { return this._Childs; }
            set
            {
                if (this._Childs != value)
                {
                    this._Childs = value;
                    this.RaisePropertyChanged("Childs");
                }
            }
        }

        //public IList<IHierarchyModel> Childs { get; set; }


        private object _Data;
        public object Data
        {
            get { return this._Data; }
            set
            {
                if (this._Data != value)
                {
                    this._Data = value;
                    this.RaisePropertyChanged("Data");
                   
                }
            }
        }
    }
}
