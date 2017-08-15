using Microsoft.Expression.Shapes;
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

    [TemplateVisualState(Name = "MouseOver", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
    public class MDFRoundProcess : ContentControl
    {
        
        public MDFRoundProcess()
        {
            this.DefaultStyleKey = typeof(MDFRoundProcess);
        }

        #region 行为
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            VisualStateManager.GoToState(this, "MouseOver", false);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            VisualStateManager.GoToState(this, "Normal", false);
        }
        #endregion

        Arc processArc;

        #region 属性

        /// <summary>
        /// 区域笔刷
        /// </summary>       
        public Brush ArcFill
        {
            get { return (Brush)GetValue(ArcFillProperty); }
            set { SetValue(ArcFillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ArcBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArcFillProperty =
            DependencyProperty.Register("ArcFill", typeof(Brush), typeof(MDFRoundProcess), new PropertyMetadata(new SolidColorBrush(Colors.Green)));




        public Brush BackArcFill
        {
            get { return (Brush)GetValue(BackArcFillProperty); }
            set { SetValue(BackArcFillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackArcFill.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackArcFillProperty =
            DependencyProperty.Register("BackArcFill", typeof(Brush), typeof(MDFRoundProcess), new PropertyMetadata(default(Brush)));


        

        public double ArcThickness
        {
            get { return (double)GetValue(ArcThicknessProperty); }
            set { SetValue(ArcThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ArcThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArcThicknessProperty =
            DependencyProperty.Register("ArcThickness", typeof(double), typeof(MDFRoundProcess), new PropertyMetadata(4d));



        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(MDFRoundProcess), new PropertyMetadata(0d));



        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stroke.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(MDFRoundProcess), new PropertyMetadata(default(Brush)));


        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(MDFRoundProcess), new PropertyMetadata(100d, (sender, arg) =>
            {
                (sender as MDFRoundProcess).CreateProcess();
            }));


        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(MDFRoundProcess), new PropertyMetadata(0d, (sender, arg) =>
            {
                (sender as MDFRoundProcess).CreateProcess();
            }));



        public double StratAngle
        {
            get { return (double)GetValue(StratAngleProperty); }
            set { SetValue(StratAngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StratAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StratAngleProperty =
            DependencyProperty.Register("StratAngle", typeof(double), typeof(MDFRoundProcess), new PropertyMetadata(-90d));




        /// <summary>
        /// 动画速度 度/秒
        /// </summary>
        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Speed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(double), typeof(MDFRoundProcess), new PropertyMetadata(70d));




        public IEasingFunction Easing
        {
            get { return (IEasingFunction)GetValue(EasingProperty); }
            set { SetValue(EasingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Easing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EasingProperty =
            DependencyProperty.Register("Easing", typeof(IEasingFunction), typeof(MDFRoundProcess), new PropertyMetadata(new QuarticEase() { EasingMode = EasingMode.EaseInOut }));


        public object PopupContent
        {
            get { return (object)GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PopupContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupContentProperty =
            DependencyProperty.Register("PopupContent", typeof(object), typeof(MDFRoundProcess), new PropertyMetadata(null));

        
        
        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            processArc = this.GetTemplateChild("processArc") as Arc;
            CreateProcess();
        }

        private void CreateProcess()
        {
            if (processArc == null)
                return;

            if (MaxValue < 0)
                return;

            double schedule = MaxValue / 360; //得到每一个角度的值;

            double anagle = (Value / schedule) + StratAngle; //得到旋转角度

            //计算动画时间
            double time = Math.Abs(anagle - processArc.EndAngle) / Speed;

            DoubleAnimation da = new DoubleAnimation();
            da.To = anagle;
            da.Duration = new Duration(TimeSpan.FromSeconds(time));
            da.EasingFunction = Easing;

            Storyboard.SetTarget(da, processArc);
            Storyboard.SetTargetProperty(da, new PropertyPath("EndAngle"));

            new Storyboard() { Children = { da } }.Begin();
        }

    }
}
