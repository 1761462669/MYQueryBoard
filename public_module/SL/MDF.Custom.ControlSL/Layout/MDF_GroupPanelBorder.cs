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

namespace MDF.Custom.ControlSL.Layout
{
    [TemplateVisualState(Name = "View", GroupName = "DataState")]
    [TemplateVisualState(Name = "Edit", GroupName = "DataState")]
    public class MDF_GroupPanelBorder : ContentControl
    {
        private Image imgLine;

        public MDF_GroupPanelBorder()
        {
            this.DefaultStyleKey = typeof(MDF_GroupPanelBorder);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            imgLine = this.GetTemplateChild("imgLine") as Image;

            if (IsShowLine)
            {
                imgLine.Height = 1;
            }
            else
            {
                imgLine.Height = 0;
            }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MDF_GroupPanelBorder), new PropertyMetadata(""));



        public HeaderTypeEnum HeaderType
        {
            get { return (HeaderTypeEnum)GetValue(HeaderTypeProperty); }
            set { SetValue(HeaderTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderTypeProperty =
            DependencyProperty.Register("HeaderType", typeof(HeaderTypeEnum), typeof(MDF_GroupPanelBorder), new PropertyMetadata(HeaderTypeEnum.请选择, new PropertyChangedCallback((sender, arg) =>
            {
                if (arg.NewValue.ToString() != "")
                {
                    (sender as MDF_GroupPanelBorder).Title = arg.NewValue.ToString();
                }
            })));



        public bool BeginStroy
        {
            get { return (bool)GetValue(BeginStroyProperty); }
            set { SetValue(BeginStroyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BeginStroy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BeginStroyProperty =
            DependencyProperty.Register("BeginStroy", typeof(bool), typeof(MDF_GroupPanelBorder), new PropertyMetadata(false, new PropertyChangedCallback((sender, e) =>
                {
                    bool newValue = (bool)e.NewValue;

                    //if (e.NewValue.ToString().ToLower()== e.OldValue.ToString().ToLower()) return;

                    MDF_GroupPanelBorder obj = sender as MDF_GroupPanelBorder;

                    if (newValue)
                    {
                        VisualStateManager.GoToState(obj, "Edit", true);
                    }
                    else
                    {
                        VisualStateManager.GoToState(obj, "View", true);
                    }

                })));



        public bool IsShowLine
        {
            get { return (bool)GetValue(IsShowLineProperty); }
            set { SetValue(IsShowLineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowLine.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowLineProperty =
            DependencyProperty.Register("IsShowLine", typeof(bool), typeof(MDF_GroupPanelBorder), new PropertyMetadata(true, new PropertyChangedCallback((sender, e) =>
                {

                })));

    }

    public enum HeaderTypeEnum
    {
        请选择 = 0,
        操作区域 = 1,
        查询区域 = 2,
        数据区域 = 3
    }
}
