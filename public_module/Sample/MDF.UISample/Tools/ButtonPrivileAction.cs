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
using System.Collections.Generic;
using MDF.Custom.ControlSL.Controls;

namespace MDF.UISample.Tools
{
    public class ButtonPrivileAction
    {
        #region 设置按钮权限
        private Control ctl;
        private Dictionary<string, bool> commandPrivates;
        private ListBox listBox;
        private bool IsItemsChanged = true;

        public ButtonPrivileAction(Control ctl, Dictionary<string, bool> commandPrivates)
        {
            ctl.Loaded += ctl_Loaded;

            this.ctl = ctl;
            this.commandPrivates = commandPrivates;

            this.SetButtonPrivileVisble(ctl);
        }

        void ctl_Loaded(object sender, RoutedEventArgs e)
        {
            listBox = SMES.Framework.Utility.VisualTreeHelper.GetChildObject<ListBox>(sender as DependencyObject);
            if (listBox != null)
            {
                listBox.LayoutUpdated += listBox_LayoutUpdated;
            }
        }

        void listBox_LayoutUpdated(object sender, EventArgs e)
        {
            PrivileVisble();
        }

        private void PrivileVisble()
        {
            if (listBox != null)
            {
                foreach (var item in listBox.Items)
                {
                    ListBoxItem listBoxItem = listBox.ItemContainerGenerator.ContainerFromIndex(listBox.Items.IndexOf(item)) as ListBoxItem;

                    if (listBoxItem == null) continue;

                    SetButtonPrivileVisble(listBoxItem);
                }
            }
        }

        /// <summary>
        /// 设置按钮权限
        /// </summary>
        /// <param name="element"></param>
        private void SetButtonPrivileVisble(FrameworkElement element)
        {
            if (ctl == null || commandPrivates == null)
                return;

            foreach (var item in commandPrivates)
            {
                UIElement obj = SMES.Framework.Utility.VisualTreeHelper.SearchVisualTree<Button>(element, item.Key);

                if (obj != null)
                {
                    obj.Visibility = item.Value ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
                }
            }
        }

        #endregion
    }
}
