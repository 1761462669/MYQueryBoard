using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MDF.UISample
{
    public partial class MDFComboTreeTest : UserControl
    {
        public MDFComboTreeTest()
        {
            InitializeComponent();

            this.Loaded += MDFComboTreeTest_Loaded;
        }

        void MDFComboTreeTest_Loaded(object sender, RoutedEventArgs e)
        {
            var source = new PagedCollectionView(
                new List<item>() { 
                    new item(){Id = "黄金叶",Name="产品路线1"},
                    new item(){Id = "黄金叶",Name="产品路线2"},
                    new item(){Id = "硬帝豪",Name="产品路线1"}
                });

            source.GroupDescriptions.Add(new PropertyGroupDescription("Id"));

            this.combo.GridItemSource = source;
                

            this.combo.SelectedIndex = 0;
        }

        public class item : System.ComponentModel.INotifyPropertyChanged
        {
            private string id;
            public string Id { get { return this.id; } set {
                this.id = value;

                if (this.PropertyChanged == null)
                    return;

                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Id"));
            } }
            private string name;
            public string Name
            {
                get { return this.name; }
                set
                {
                    this.name = value;

                    if (this.PropertyChanged == null)
                        return;
                    this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Name"));
                }
            }

            #region INotifyPropertyChanged 成员

            public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

            #endregion
        }
    }
}
