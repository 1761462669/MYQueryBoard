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
    public class MDFProgressBar:Control
    {
        public MDFProgressBar()
        {
            this.DefaultStyleKey = typeof(MDFProgressBar);

            this.SizeChanged += MDFProgressBar_SizeChanged;
        }

        void MDFProgressBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.ProcessComplate();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ControlSetting();           
            
        }

        

        Border maxrec, valuerec, valueBlock, block;

        /// <summary>
        /// 控件基本设置
        /// </summary>
        private void ControlSetting()
        {
            maxrec = GetTemplateChild("maxrec") as Border;
            valuerec = GetTemplateChild("valuerec") as Border ;
            valueBlock = GetTemplateChild("valueBlock") as Border;
            block = GetTemplateChild("block") as Border;

            if (maxrec == null || valuerec == null || valueBlock == null || block == null)
                return;

            if (Orientaion == Orientation.Horizontal)
            {
                valuerec.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                valuerec.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                valuerec.Width = 0;

                valueBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                valueBlock.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                valueBlock.MinWidth = BlockMinSize;
                valueBlock.Width = 0;

                block.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                block.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                block.MinWidth = BlockMinSize;
                block.Width = 0;
            }
            else
            {
                valuerec.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                valuerec.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                valuerec.Height = 0;

                valueBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                valueBlock.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                valueBlock.MinHeight = BlockMinSize;
                valueBlock.Height = 0;

                block.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                block.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                block.MinHeight = BlockMinSize;
                block.Height = 0;
            }

            ProcessComplate();
        }

        /// <summary>
        /// 值计算
        /// </summary>
        private void ProcessComplate()
        {
            if (maxrec == null || valuerec == null || valueBlock == null || block == null)
                return;

            if (double.IsNaN(MaxValue) || MaxValue < 1)
                return;

            Percent = Math.Round(Value / MaxValue * 100, 2);

            if (Orientaion == Orientation.Horizontal)
                ProcessComplateHorizontal();
            else
                processComplateVertical();
        }

        private void ProcessComplateHorizontal()
        {
            double maxwidth = maxrec.ActualWidth;
            if (double.IsNaN(maxwidth))
                maxwidth = 0;

            double tmp = maxwidth / MaxValue; //每一个单位值的宽度
            double valuewidth = Value * tmp; //值区域宽度

            double currwidth = valuerec.ActualWidth; //当前值区域宽度
            if (double.IsNaN(currwidth))
                currwidth = 0;

            if (valuewidth > maxwidth)
                valuewidth = maxwidth;

            if (valuewidth < 0)
                valuewidth = 0;

            double runwidth = Math.Abs(valuewidth - currwidth);//运行宽度

            //valuerec.Width = valuewidth; //设置值区域宽度
            //valueBlock.Width = valuewidth;//值滑块区域宽度

            CreateAnimation(valuewidth, runwidth, "Width");

            
        }

        private void CreateAnimation(double tovalue, double runwidth, string path)
        {
            //计算时间 
            double runtime = runwidth / Speed;

            DoubleAnimation davaluerec = new DoubleAnimation();
            davaluerec.To = tovalue;
            davaluerec.Duration = TimeSpan.FromSeconds(runtime);
            davaluerec.EasingFunction = Easing;
            Storyboard.SetTarget(davaluerec,valuerec);
            Storyboard.SetTargetProperty(davaluerec, new PropertyPath(path));

            DoubleAnimation davalueBlock = new DoubleAnimation();
            davalueBlock.To = tovalue;
            davalueBlock.Duration = TimeSpan.FromSeconds(runtime);
            davalueBlock.EasingFunction = Easing;
            Storyboard.SetTarget(davalueBlock, valueBlock);
            Storyboard.SetTargetProperty(davalueBlock, new PropertyPath(path));

            DoubleAnimation dafactvalue = new DoubleAnimation();
            dafactvalue.To = Value;
            dafactvalue.Duration = TimeSpan.FromSeconds(runtime);
            dafactvalue.EasingFunction = Easing;
            Storyboard.SetTarget(dafactvalue, this);
            Storyboard.SetTargetProperty(dafactvalue, new PropertyPath("FactValue"));

            new Storyboard() { Children = { davaluerec, davalueBlock, dafactvalue } }.Begin();

        }

        private void processComplateVertical()
        {
            double maxheight = maxrec.ActualHeight;
            if (double.IsNaN(maxheight))
                maxheight = 0;

            double tmp = maxheight / MaxValue;//每一个单位值高度
            double valueheight = Value * tmp;//值区域高度

            //valuerec.Height = valueheight;
            //valueBlock.Height = valueheight;

            double currheight = valuerec.ActualHeight; //当前值区域宽度
            if (double.IsNaN(currheight))
                currheight = 0;

            if (valueheight > maxheight)
                valueheight = maxheight;

            if (valueheight < 0)
                valueheight = 0;

            double runheight = Math.Abs(valueheight - currheight);//运行宽度            

            CreateAnimation(valueheight, runheight, "Height");
        }

        #region 值属性
        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(MDFProgressBar), new PropertyMetadata(0d, (sender, arg) => {
                (sender as MDFProgressBar).ProcessComplate();
            }));


        /// <summary>
        /// 当前值
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(MDFProgressBar), new PropertyMetadata(0d, (sender, arg) => {
                (sender as MDFProgressBar).ProcessComplate();
            }));


        /// <summary>
        /// 百分比值
        /// </summary>
        public double Percent
        {
            get { return (double)GetValue(PercentProperty); }
            set { SetValue(PercentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Percent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PercentProperty =
            DependencyProperty.Register("Percent", typeof(double), typeof(MDFProgressBar), new PropertyMetadata(0d));        


        #endregion

        #region 动画属性
        /// <summary>
        /// 动画行进速度
        /// </summary>
        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Speed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(double), typeof(MDFProgressBar), new PropertyMetadata(500d));


        /// <summary>
        /// 行进动画
        /// </summary>
        public IEasingFunction Easing
        {
            get { return (IEasingFunction)GetValue(EasingProperty); }
            set { SetValue(EasingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Easing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EasingProperty =
            DependencyProperty.Register("Easing", typeof(IEasingFunction), typeof(MDFProgressBar), new PropertyMetadata(new QuinticEase() { EasingMode= EasingMode.EaseOut }));


        #endregion

        #region 行径方向
        /// <summary>
        /// 方向 
        /// </summary>
        public Orientation Orientaion
        {
            get { return (Orientation)GetValue(OrientaionProperty); }
            set { SetValue(OrientaionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientaion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientaionProperty =
            DependencyProperty.Register("Orientaion", typeof(Orientation), typeof(MDFProgressBar), new PropertyMetadata(Orientation.Horizontal, (sender, arg) => {
                (sender as MDFProgressBar).ControlSetting();
            }));

        #endregion

        #region 滑块
        public Visibility ShowBlock
        {
            get { return (Visibility)GetValue(ShowBlockProperty); }
            set { SetValue(ShowBlockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowBlock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowBlockProperty =
            DependencyProperty.Register("ShowBlock", typeof(Visibility), typeof(MDFProgressBar), new PropertyMetadata(Visibility.Visible));

        #endregion

        #region 笔刷

        public Brush ValueBrush
        {
            get { return (Brush)GetValue(ValueBrushProperty); }
            set { SetValue(ValueBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueBrushProperty =
            DependencyProperty.Register("ValueBrush", typeof(Brush), typeof(MDFProgressBar), new PropertyMetadata(default(Brush)));


        #endregion

        #region 滑块最小宽度（或高度）

        private double _blockMinSize = 30;
        public double BlockMinSize
        {
            get {
                return _blockMinSize;
            }
            set {
                _blockMinSize = value;
            }
        }
        #endregion



        public double FactValue
        {
            get { return (double)GetValue(FactValueProperty); }
            set { SetValue(FactValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FactValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FactValueProperty =
            DependencyProperty.Register("FactValue", typeof(double), typeof(MDFProgressBar), new PropertyMetadata(0d));


    }
}
