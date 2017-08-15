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

namespace MDF.Custom.ControlSL.Layout
{
    public class MDF_Line : ContentControl
    {
        public MDF_Line()
        {
            this.DefaultStyleKey = typeof(MDF_Line);
        }

        public event RoutedEventHandler LineSelected, lineUnSelected;
        #region 属性定义

        #region 线条
        /// <summary>
        /// 线条高度
        /// </summary>
        public double LineHeight
        {
            get { return (double)GetValue(LineHeightProperty); }
            set { SetValue(LineHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineHeightProperty =
            DependencyProperty.Register("LineHeight", typeof(double), typeof(MDF_Line), new PropertyMetadata(2d));


        #endregion

        #region 箭头


        public double AllowHeight
        {
            get { return (double)GetValue(AllowHeightProperty); }
            set { SetValue(AllowHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowHeightProperty =
            DependencyProperty.Register("AllowHeight", typeof(double), typeof(MDF_Line), new PropertyMetadata(12d));



        public double AllowWidth
        {
            get { return (double)GetValue(AllowWidthProperty); }
            set { SetValue(AllowWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowWidthProperty =
            DependencyProperty.Register("AllowWidth", typeof(double), typeof(MDF_Line), new PropertyMetadata(8d));


        public double AllowStrokeThickness
        {
            get { return (double)GetValue(AllowStrokeThicknessProperty); }
            set { SetValue(AllowStrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowStrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowStrokeThicknessProperty =
            DependencyProperty.Register("AllowStrokeThickness", typeof(double), typeof(MDF_Line), new PropertyMetadata(2d));



        public Visibility ShowLeftAllow
        {
            get { return (Visibility)GetValue(ShowLeftAllowProperty); }
            set { SetValue(ShowLeftAllowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowLeftAllow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowLeftAllowProperty =
            DependencyProperty.Register("ShowLeftAllow", typeof(Visibility), typeof(MDF_Line), new PropertyMetadata(Visibility.Collapsed));



        public Visibility ShowRightAllow
        {
            get { return (Visibility)GetValue(ShowRightAllowProperty); }
            set { SetValue(ShowRightAllowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowRightAllow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowRightAllowProperty =
            DependencyProperty.Register("ShowRightAllow", typeof(Visibility), typeof(MDF_Line), new PropertyMetadata(Visibility.Visible));


        #endregion


        /// <summary>
        /// 线条颜色笔刷
        /// </summary>
        public Brush LineBrush
        {
            get { return (Brush)GetValue(LineBrushProperty); }
            set { SetValue(LineBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineBrushProperty =
            DependencyProperty.Register("LineBrush", typeof(Brush), typeof(MDF_Line), new PropertyMetadata(new SolidColorBrush(Colors.Black)));



        public Brush SelectedBrush
        {
            get { return (Brush)GetValue(SelectedBrushProperty); }
            set { SetValue(SelectedBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBrushProperty =
            DependencyProperty.Register("SelectedBrush", typeof(Brush), typeof(MDF_Line), new PropertyMetadata(new SolidColorBrush(Colors.Green)));



        public bool Selected
        {
            get { return (bool)GetValue(SelectedProperty); }
            set { SetValue(SelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Selected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedProperty =
            DependencyProperty.Register("Selected", typeof(bool), typeof(MDF_Line), new PropertyMetadata(false, new PropertyChangedCallback((obj, arg) =>
            {
                (obj as MDF_Line).SetCheckColor();
            })));

        Brush tmpBrush;
        private void SetCheckColor()
        {
            if (Selected)
            {
                tmpBrush = LineBrush;
                LineBrush = SelectedBrush;
                if (LineSelected != null)
                    LineSelected(this, new RoutedEventArgs());
            }
            else
            {
                LineBrush = tmpBrush;
                if (lineUnSelected != null)
                    lineUnSelected(this, new RoutedEventArgs());
            }
        }
        #endregion

        #region 事件
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Selected = Selected ? false : true;
        }
        #endregion


    }
}
