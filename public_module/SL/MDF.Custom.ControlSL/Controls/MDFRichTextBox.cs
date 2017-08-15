using SMES.Framework.Utility;
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
    public class MDFRichTextBox:RichTextBox
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            RightContextMenuUnity unity = new RightContextMenuUnity();
            unity.creatMenu(this);
        }
    }
}
