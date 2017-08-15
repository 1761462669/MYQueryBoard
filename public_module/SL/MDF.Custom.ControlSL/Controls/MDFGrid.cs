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
    public class MDFGrid :DataGrid
    {
        public MDFGrid()
        {
            this.DefaultStyleKey = typeof(DataGrid);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //base.RowSelectorSettings = new Infragistics.Controls.Grids.RowSelectorSettings { EnableRowNumbering = true, Visibility = System.Windows.Visibility.Visible };

            //<ig:XamGrid.FilteringSettings>
            //    <ig:FilteringSettings AllowFiltering="FilterMenu"></ig:FilteringSettings>
            //</ig:XamGrid.FilteringSettings>
            //base.FilteringSettings = new Infragistics.Controls.Grids.FilteringSettings { AllowFiltering = Infragistics.Controls.Grids.FilterUIType.FilterMenu };
        }
    }
}
