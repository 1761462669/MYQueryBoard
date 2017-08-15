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
using Infragistics.Controls.Grids;
using MDF.Framework.ViewModel;
using SMES.Framework.Utility;

namespace SMES.Framework.Command
{
    /// <summary>
    /// 导出ExcelCommand(目前只支持XamGrid控件)，需要绑定XamGridControl属性
    /// </summary>
    public class ExportExcelCommand : BaseViewModel, ICommand
    {
        public XamGrid XamGridControl
        {
            get { return (XamGrid)GetValue(XamGridControlProperty); }
            set { SetValue(XamGridControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XamGridControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XamGridControlProperty =
            DependencyProperty.Register("XamGridControl", typeof(XamGrid), typeof(ExportExcelCommand), new PropertyMetadata(null));

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if(XamGridControl != null)
            {
                ExcelHelper.ExportExcel(XamGridControl);
            }
        }
    }
}
