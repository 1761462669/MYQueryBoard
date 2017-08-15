using MDF.Framework.Commands;
using MDF.Framework.ViewModel;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SMES.Framework.Command
{
    public class GoForwardCommand : BaseViewModel, ICommand, IRaiseCanExecute
    {
        public string RegionName
        {
            get { return (string)GetValue(RegionNameProperty); }
            set { SetValue(RegionNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AreaName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegionNameProperty =
            DependencyProperty.Register("RegionName", typeof(string), typeof(GoBackCommand), new PropertyMetadata(null));

        public event EventHandler CanExecuteChanged;
        public void RaisedCanExecute()
        {
            if (this.CanExecuteChanged != null)
                this.CanExecuteChanged(this, new EventArgs());
        }



        public bool CanExecute(object parameter)
        {
            return this.RegionName != null;
        }

        public void Execute(object parameter)
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            if (regionManager.Regions.ContainsRegionWithName(this.RegionName))
            {
                regionManager.Regions[this.RegionName].NavigationService.Journal.GoForward();
            }
        }
    }
}
