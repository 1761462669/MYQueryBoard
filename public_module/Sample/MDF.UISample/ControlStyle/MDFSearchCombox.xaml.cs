using SMES.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MDF.UISample.ControlStyle
{
    public partial class MDFSearchCombox : UserControl
    {
        List<SMES.Framework.DataModel> Datas = new List<SMES.Framework.DataModel>();

        public MDFSearchCombox()
        {
            InitializeComponent();

            this.Loaded += MDFSearchCombox_Loaded;
        }

        void MDFSearchCombox_Loaded(object sender, RoutedEventArgs e)
        {
            Datas.Add(new SMES.Framework.DataModel() { Id = 1, Name = "aa1" });
            Datas.Add(new SMES.Framework.DataModel() { Id = 2, Name = "aa2" });
            Datas.Add(new SMES.Framework.DataModel() { Id = 3, Name = "aa3" });
            Datas.Add(new SMES.Framework.DataModel() { Id = 4, Name = "aa4" });

            this.combox.ItemSource = Datas;
        }
    }
}
