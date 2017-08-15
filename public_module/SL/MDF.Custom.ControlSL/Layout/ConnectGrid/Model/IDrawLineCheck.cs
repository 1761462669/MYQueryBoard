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

namespace MDF.Custom.ControlSL.Layout.ConnectGrid.Model
{
    public interface IDrawLineCheck
    {
        bool AddLineCheck(MDF_ConnectItemModel from, MDF_ConnectItemModel to);

        bool DeleckLine(MDF_ConnectLineModel model);
    }
}
