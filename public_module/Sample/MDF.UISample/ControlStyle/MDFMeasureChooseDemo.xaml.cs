using MDF.Custom.ControlSL;
using SMES.Bussiness.ControlSL.MaterialChoose;
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
    public partial class MDFMeasureChooseDemo : PageBase
    {
        public MDFMeasureChooseDemo()
        {
            InitializeComponent();
            cbx.QueryMaterialTypes = new List<SMES.Bussiness.ControlSL.MaterialChoose.QueryMaterialType>();
            cbx.QueryMaterialTypes.Add(new QueryMaterialType() { Value = "1001" });

        }
    }
}
