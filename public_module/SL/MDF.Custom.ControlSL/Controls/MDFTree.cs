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
    public class MDFTree:TreeView
    {
        public MDFTree()
        {
            this.DefaultStyleKey = typeof(MDFTree);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.SelectedItemChanged += MDFTree_SelectedItemChanged;
        }

        void MDFTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SelectdItem = this.SelectedItem;
        }


        public object SelectdItem
        {
            get { return (object)GetValue(SelectdItemProperty); }
            set { SetValue(SelectdItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectdItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectdItemProperty =
            DependencyProperty.Register("SelectdItem", typeof(object), typeof(MDFTree), new PropertyMetadata(null));


    }

    public class MDFTreeItem : TreeViewItem
    {
        public MDFTreeItem()
        {
            this.DefaultStyleKey = typeof(TreeViewItem);
        }
    }
}
