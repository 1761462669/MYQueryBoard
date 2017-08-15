using System;
using System.ComponentModel;
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
    public class CircularPanel : Panel
    {
        #region 属性定义
        public enum AlignmentOptions { Left, Center, Right };

        public static readonly DependencyProperty RadiusXProperty = DependencyProperty.Register("RadiusX", typeof(double), typeof(CircularPanel), new PropertyMetadata(CircularPanel.RadiusChanged));
        public static readonly DependencyProperty RadiusYProperty = DependencyProperty.Register("RadiusY", typeof(double), typeof(CircularPanel), new PropertyMetadata(CircularPanel.RadiusChanged));
        public static readonly DependencyProperty AngleItemProperty = DependencyProperty.Register("AngleItem", typeof(double), typeof(CircularPanel), new PropertyMetadata(double.NaN, CircularPanel.AngleItemChanged));
        public static readonly DependencyProperty IsAnimatedProperty = DependencyProperty.Register("IsAnimated", typeof(bool), typeof(CircularPanel), new PropertyMetadata(CircularPanel.IsAnimatedChanged));
        public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.Register("AnimationDuration", typeof(int), typeof(CircularPanel), new PropertyMetadata(CircularPanel.AnimationDurationChanged));
        public static readonly DependencyProperty InitialAngleProperty = DependencyProperty.Register("InitialAngle", typeof(double), typeof(CircularPanel), new PropertyMetadata(CircularPanel.InitialAngleChanged));
        public static readonly DependencyProperty AlignProperty = DependencyProperty.Register("Align", typeof(AlignmentOptions), typeof(CircularPanel), new PropertyMetadata(CircularPanel.AlignChanged));

        private static void RadiusChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { ((CircularPanel)sender).Refresh(); }
        private static void AngleItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { ((CircularPanel)sender).Refresh(); }
        private static void IsAnimatedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { ((CircularPanel)sender).OnIsAnimatedChanged(e); }
        private static void AnimationDurationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { ((CircularPanel)sender).Refresh(); }
        private static void InitialAngleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { ((CircularPanel)sender).Refresh(); }
        private static void AlignChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { ((CircularPanel)sender).Refresh(); }
        private static void TransformItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { ((CircularPanel)sender).Refresh(); }



        /// <summary>
        /// 是否滚轮滚动
        /// </summary>
        [Category("Circular Panel")]
        public bool AllowMouseWheel
        {
            get { return (bool)GetValue(AllowMouseWheelProperty); }
            set { SetValue(AllowMouseWheelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowMouseWheel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowMouseWheelProperty =
            DependencyProperty.Register("AllowMouseWheel", typeof(bool), typeof(CircularPanel), new PropertyMetadata(true));


        /// <summary>
        /// 滚轮动画时长
        /// </summary>
        [Category("Circular Panel")]
        public int WheelDuration
        {
            get { return (int)GetValue(WheelDurationProperty); }
            set { SetValue(WheelDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WheelDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WheelDurationProperty =
            DependencyProperty.Register("WheelDuration", typeof(int), typeof(CircularPanel), new PropertyMetadata(1000));


        /// <summary>
        /// Item是否旋转
        /// </summary>
        [Category("Circular Panel")]
        public bool TransformItem
        {
            get { return (bool)GetValue(TransformItemProperty); }
            set { SetValue(TransformItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TransformItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TransformItemProperty =
            DependencyProperty.Register("TransformItem", typeof(bool), typeof(CircularPanel), new PropertyMetadata(false, TransformItemChanged));

        /// <summary>
        /// Item是否缩放
        /// </summary>
        [Category("Circular Panel")]
        public bool ZooItem
        {
            get { return (bool)GetValue(ZooItemProperty); }
            set { SetValue(ZooItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ZooItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ZooItemProperty =
            DependencyProperty.Register("ZooItem", typeof(bool), typeof(CircularPanel), new PropertyMetadata(false, TransformItemChanged));


        /// <summary>
        /// 半径X
        /// </summary>
        [Category("Circular Panel")]
        public double RadiusX
        {
            get { return (double)this.GetValue(CircularPanel.RadiusXProperty); }
            set { this.SetValue(CircularPanel.RadiusXProperty, value); }
        }

        /// <summary>
        /// 半径Y
        /// </summary>
        [Category("Circular Panel")]
        public double RadiusY
        {
            get { return (double)this.GetValue(CircularPanel.RadiusYProperty); }
            set { this.SetValue(CircularPanel.RadiusYProperty, value); }
        }

        /// <summary>
        /// Item间隔角度
        /// </summary>
        [Category("Circular Panel")]
        public double AngleItem
        {
            get { return (double)this.GetValue(CircularPanel.AngleItemProperty); }
            set { this.SetValue(CircularPanel.AngleItemProperty, value); }
        }

        /// <summary>
        /// 显示时是否显示动画
        /// </summary>
        [Category("Circular Panel")]
        public bool IsAnimated
        {
            get { return (bool)this.GetValue(CircularPanel.IsAnimatedProperty); }
            set { this.SetValue(CircularPanel.IsAnimatedProperty, value); }
        }

        /// <summary>
        /// 显示动画时长
        /// </summary>
        [Category("Circular Panel")]
        public int AnimationDuration
        {
            get { return (int)this.GetValue(CircularPanel.AnimationDurationProperty); }
            set { this.SetValue(CircularPanel.AnimationDurationProperty, value); }
        }

        /// <summary>
        /// 初始角度
        /// </summary>
        [Category("Circular Panel")]
        public double InitialAngle
        {
            get { return (double)this.GetValue(CircularPanel.InitialAngleProperty); }
            set { this.SetValue(CircularPanel.InitialAngleProperty, value); }
        }

        /// <summary>
        /// 角度对其方式
        /// </summary>
        [Category("Circular Panel")]
        public AlignmentOptions Align
        {
            get { return (AlignmentOptions)this.GetValue(CircularPanel.AlignProperty); }
            set { this.SetValue(CircularPanel.AlignProperty, value); }
        }

        /// <summary>
        /// 自动滚动时长
        /// </summary>
        [Category("Circular Panel")]
        public int AutoTrundleDuration
        {
            get { return (int)GetValue(AutoTrundleDurationProperty); }
            set { SetValue(AutoTrundleDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoTrundleDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoTrundleDurationProperty =
            DependencyProperty.Register("AutoTrundleDuration", typeof(int), typeof(CircularPanel), new PropertyMetadata(5000));

        /// <summary>
        /// 是否允许自动滚动
        /// </summary>
        [Category("Circular Panel")]
        public bool AllowAutoTrundle
        {
            get { return (bool)GetValue(AllowAutoTrundleProperty); }
            set { SetValue(AllowAutoTrundleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowAutoTrundle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowAutoTrundleProperty =
            DependencyProperty.Register("AllowAutoTrundle", typeof(bool), typeof(CircularPanel), new PropertyMetadata(false, AllowAutoTrundleChanged));


        private static void AllowAutoTrundleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { ((CircularPanel)sender).AutoTrundleAnimate(); }


        /// <summary>
        /// 自动滚动角度
        /// </summary>
        [Category("Circular Panel")]
        public double AutoTrundleAngle
        {
            get { return (double)GetValue(AutoTrundleAngleProperty); }
            set { SetValue(AutoTrundleAngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoTrundleAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoTrundleAngleProperty =
            DependencyProperty.Register("AutoTrundleAngle", typeof(double), typeof(CircularPanel), new PropertyMetadata(360d, AllowAutoTrundleChanged));

        /// <summary>
        /// 自动滚动开始时间
        /// </summary>
        [Category("Circular Panel")]
        public int AutoTrundleBeginTime
        {
            get { return (int)GetValue(AutoTrundleBeginTimeProperty); }
            set { SetValue(AutoTrundleBeginTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoTrundleBeginTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoTrundleBeginTimeProperty =
            DependencyProperty.Register("AutoTrundleBeginTime", typeof(int), typeof(CircularPanel), new PropertyMetadata(0, AllowAutoTrundleChanged));



        /// <summary>
        /// 自动滚动状态
        /// </summary>
        [Category("Circular Panel")]
        public bool AutoTrundleState
        {
            get { return (bool)GetValue(AutoTrundleStateProperty); }
            set { SetValue(AutoTrundleStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoTrundleState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoTrundleStateProperty =
            DependencyProperty.Register("AutoTrundleState", typeof(bool), typeof(CircularPanel), new PropertyMetadata(true, AutoTrundleStateChanged));

        private static void AutoTrundleStateChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((CircularPanel)sender).AutoTrundleStateChange();
        }

        /// <summary>
        /// 自动滚动动画
        /// </summary>
        [Category("Circular Panel")]
        public IEasingFunction AutoTrundleEase
        {
            get { return (IEasingFunction)GetValue(AutoTrundleEaseProperty); }
            set { SetValue(AutoTrundleEaseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AutoTrundleEase.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AutoTrundleEaseProperty =
            DependencyProperty.Register("AutoTrundleEase", typeof(IEasingFunction), typeof(CircularPanel), new PropertyMetadata(default(IEasingFunction), AutoTrundleStateChanged));


        /// <summary>
        /// 滚轮滚动角度
        /// </summary>
        [Category("Circular Panel")]
        public double WheelAngle
        {
            get { return (double)GetValue(WheelAngleProperty); }
            set { SetValue(WheelAngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WheelAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WheelAngleProperty =
            DependencyProperty.Register("WheelAngle", typeof(double), typeof(CircularPanel), new PropertyMetadata(60d));


        #endregion

        bool isload = false;

        public CircularPanel()
        {
            this.MouseWheel += CircularPanel_MouseWheel;
            this.Loaded += CircularPanel_Loaded;
        }

        void CircularPanel_Loaded(object sender, RoutedEventArgs e)
        {
            isload = true;
            AutoTrundleAnimate();
        }

        //鼠标滚轮事件
        void CircularPanel_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!AllowMouseWheel)
                return;

            int data = e.Delta;
            if (data == 0)
                return;

            if (data > 0)
                WheelAnimate(WheelAngle);
            else
                WheelAnimate(0 - WheelAngle);
        }

        private void OnIsAnimatedChanged(DependencyPropertyChangedEventArgs e)
        {
            this.Animate();
            this.Refresh();
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size resultSize = new Size(0, 0);

            foreach (UIElement child in this.Children)
            {
                child.Measure(availableSize);
                resultSize.Width = Math.Max(resultSize.Width, child.DesiredSize.Width);
                resultSize.Height = Math.Max(resultSize.Height, child.DesiredSize.Height);
            }

            resultSize.Width =
                double.IsPositiveInfinity(availableSize.Width) ?
                resultSize.Width : availableSize.Width;

            resultSize.Height =
                double.IsPositiveInfinity(availableSize.Height) ?
                resultSize.Height : availableSize.Height;

            this.Animate();

            return resultSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            this.Refresh();
            return base.ArrangeOverride(finalSize);
        }

        private void Animate()
        {
            if (this.IsAnimated)
            {
                int index = 0;
                foreach (FrameworkElement element in this.Children)
                {
                    element.Opacity = 0;
                    Storyboard loadStoryboard = new Storyboard();
                    int time = this.AnimationDuration * (index + 1);

                    DoubleAnimation opacityAnimation = new DoubleAnimation();
                    opacityAnimation.From = 0;
                    opacityAnimation.To = 1;
                    opacityAnimation.BeginTime = new TimeSpan(0, 0, 0, 0, time);
                    opacityAnimation.Duration = new Duration(new TimeSpan(0, 0, 0, 0, this.AnimationDuration));
                    Storyboard.SetTarget(opacityAnimation, element);
                    Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("(Opacity)"));
                    loadStoryboard.Children.Add(opacityAnimation);

                    loadStoryboard.Begin();
                    index++;
                }
            }
        }

        Storyboard wheelStoryboard;
        private void WheelAnimate(double data)
        {
            if (AllowAutoTrundle)
                return;

            AutoTrundleStop();

            //if (wheelStoryboard != null)
            //{
            //    wheelStoryboard.SkipToFill();
            //    wheelStoryboard = null;
            //}

            wheelStoryboard = new Storyboard();
            DoubleAnimation wheelAnimation = new DoubleAnimation();
            wheelAnimation.From = this.InitialAngle;
            double tovalue = (this.InitialAngle + data);
            wheelAnimation.To = tovalue;
            wheelAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(WheelDuration));
            wheelAnimation.EasingFunction = new QuarticEase() { EasingMode = EasingMode.EaseOut };
            //opacityAnimation.Duration = new Duration(new TimeSpan(0, 0, 0, 0, this.AnimationDuration));
            Storyboard.SetTarget(wheelAnimation, this);
            Storyboard.SetTargetProperty(wheelAnimation, new PropertyPath("(InitialAngle)"));
            wheelStoryboard.Children.Add(wheelAnimation);
            wheelStoryboard.Completed += loadStoryboard_Completed;
            wheelStoryboard.Begin();
        }

        void loadStoryboard_Completed(object sender, EventArgs e)
        {
            AutoTrundleAnimate();
        }

        Storyboard autoTrundStoryborad;
        private void AutoTrundleAnimate()
        {

            if (!AllowAutoTrundle)
            {
                AutoTrundleStop();
                return;
            }

            if (!isload)
            {
                return;
            }

            if (System.ComponentModel.DesignerProperties.IsInDesignTool)
                return;

            autoTrundStoryborad = new Storyboard();
            DoubleAnimation autoanimation = new DoubleAnimation();
            autoanimation.EasingFunction = AutoTrundleEase;
            autoanimation.BeginTime = TimeSpan.FromMilliseconds(AutoTrundleBeginTime);
            autoanimation.Duration = TimeSpan.FromMilliseconds(AutoTrundleDuration);
            autoanimation.From = this.InitialAngle;
            autoanimation.To = +this.InitialAngle + AutoTrundleAngle;
            Storyboard.SetTarget(autoanimation, this);
            Storyboard.SetTargetProperty(autoanimation, new PropertyPath("(InitialAngle)"));
            autoTrundStoryborad.Children.Add(autoanimation);
            //autoTrundStoryborad.RepeatBehavior = RepeatBehavior.Forever;
            this.AutoTrundleState = true;
            autoTrundStoryborad.Completed += autoTrundStoryborad_Completed;
            autoTrundStoryborad.Begin();
        }

        void autoTrundStoryborad_Completed(object sender, EventArgs e)
        {
            AutoTrundleAnimate();
        }

        private void AutoTrundleStop()
        {
            if (autoTrundStoryborad != null)
            {
                autoTrundStoryborad.Pause();
                autoTrundStoryborad = null;
            }
        }

        private void AutoTrundleStateChange()
        {
            if (autoTrundStoryborad == null)
                return;

            if (AutoTrundleState)
                autoTrundStoryborad.Resume();
            else
                autoTrundStoryborad.Pause();
        }


        private void Refresh()
        {
            int count = 0;
            if (double.IsNaN(this.Width))
            {
                this.Width = 200;
            }
            if (double.IsNaN(this.Height))
            {
                this.Height = 200;
            }

            if (double.IsNaN(this.AngleItem) && this.Children.Count != 0)
            {
                this.AngleItem = 360 / this.Children.Count;
            }

            foreach (FrameworkElement element in this.Children)
            {

                double alignX = 0;
                double alignY = 0;
                double angle = 0;
                switch (this.Align)
                {
                    case AlignmentOptions.Left:
                        alignX = 0;
                        alignY = 0;
                        break;
                    case AlignmentOptions.Center:
                        alignX = element.DesiredSize.Width / 2;
                        alignY = element.DesiredSize.Height / 2;
                        break;
                    case AlignmentOptions.Right:
                        alignX = element.DesiredSize.Width;
                        alignY = element.DesiredSize.Height;
                        break;
                }

                angle = ((this.AngleItem * count++) + this.InitialAngle) % 360;
                CompositeTransform r2 = new CompositeTransform();
                r2.CenterX = alignX;
                r2.CenterY = alignY;
                if (TransformItem)
                    r2.Rotation = angle;
                else
                    r2.Rotation = 0;

                if (ZooItem)
                {
                    //90度时 是在最底部. 此时此时应为1
                    //270度时，在最顶部.此时应为0
                    double tmp = 90;//最大显示位 90度
                    if (angle < 0)  //如果反转 转换成正转
                        angle = angle + 360;

                    double tmp2 = angle % 360;
                    tmp2 = tmp2 - tmp;
                    if (tmp2 < 0)
                        tmp2 = tmp2 + 360;
                    double tmp3 = Math.Abs(tmp2 - 180) / 180;
                    r2.ScaleX = tmp3;
                    r2.ScaleY = tmp3;
                }
                else
                {
                    r2.ScaleX = 1;
                    r2.ScaleY = 1;
                }

                element.RenderTransform = r2;

                double x = this.RadiusX * Math.Cos(Math.PI * angle / 180);
                double y = this.RadiusY * Math.Sin(Math.PI * angle / 180);

                if (!(double.IsNaN(this.Width)) && !(double.IsNaN(this.Height)) && !(double.IsNaN(alignX)) && !(double.IsNaN(alignY)) && !(double.IsNaN(element.DesiredSize.Width)) && !(double.IsNaN(element.DesiredSize.Height)))
                {
                    element.Arrange(new Rect(x + this.Width / 2 - alignX, y + this.Height / 2 - alignY, element.DesiredSize.Width, element.DesiredSize.Height));
                }
            }
        }
    }
}
