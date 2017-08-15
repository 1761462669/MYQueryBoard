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
    public class MDFTextBox:TextBox
    {
        public MDFTextBox()
        {
            this.DefaultStyleKey = typeof(MDFTextBox);

            this.LostFocus += MDFTextBox_LostFocus;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        void MDFTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TextMode == TextModeEnum.Num)
                {
                    SetTextValueNum(this.Text);
                    if (!string.IsNullOrEmpty(this.Text))
                    {
                        if (this.IsRound&&this.DecimalPlaces!=0)
                        {
                            //this.Text = this.Round(double.Parse(this.Text), DecimalPlaces).ToString();
                            this.Text = this.DynamicRound(double.Parse(this.Text), DecimalPlaces,this.CutoffValue).ToString();
                        }
                    }
                }
                if (TextMode == TextModeEnum.Int)
                {
                    SetTextValueInt(this.Text);
                }
            }
            catch
            {
                this.Text = "0";
            }
        }


        private void SetTextValueNum(string text)
        {
            decimal result = 0;
            bool isSuccess = decimal.TryParse(text, out result);
            if (isSuccess)
            {
                this.Text = decimal.Parse(text).ToString();
                this.SelectionStart = this.Text.Length;
            }
            else
            {
                if (!string.IsNullOrEmpty(text))
                {
                    text = text.Substring(0, text.Length - 1);
                    this.SelectionStart = text.Length;
                    SetTextValueNum(text);
                }
                else
                {
                    this.Text = "0";
                }
            }
        }

        private void SetTextValueInt(string text)
        {
            int result = 0;
            bool isSuccess = int.TryParse(text, out result);
            if (isSuccess)
            {
                this.Text = int.Parse(text).ToString();
                this.SelectionStart = this.Text.Length;
            }
            else
            {
                if (!string.IsNullOrEmpty(text))
                {
                    text = text.Substring(0, text.Length - 1);
                    this.SelectionStart = text.Length;

                    SetTextValueInt(text);
                }
                else
                {
                    this.Text = "0";
                }
            }
        }


        TextBlock tbnull;

        public string NullText
        {
            get { return (string)GetValue(NullTextProperty); }
            set { SetValue(NullTextProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            tbnull = this.GetTemplateChild("tbnull") as TextBlock;
        }

        // Using a DependencyProperty as the backing store for NullText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NullTextProperty =
            DependencyProperty.Register("NullText", typeof(string), typeof(MDFTextBox), new PropertyMetadata(default(string)));



        public TextModeEnum TextMode
        {
            get { return (TextModeEnum)GetValue(TextModeProperty); }
            set { SetValue(TextModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextModeProperty =
            DependencyProperty.Register("TextMode", typeof(TextModeEnum), typeof(MDFTextBox), new PropertyMetadata(TextModeEnum.All));


        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);

            if(tbnull == null)
                return;

            tbnull.Visibility = System.Windows.Visibility.Collapsed;                
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            if (tbnull == null)
                return;

            if(string.IsNullOrEmpty(Text))
                tbnull.Visibility = System.Windows.Visibility.Visible;
            else
                tbnull.Visibility = System.Windows.Visibility.Collapsed;  

        }
        
        /// <summary>
        /// 实现数据的四舍五入法 
        /// </summary>
        /// <param name="Data">要进行处理的数据</param>
        /// <param name="x">保留的小数位数</param>
        /// <returns>四舍五入后的结果</returns>
        private double Round(double Data, int x)
        {
            bool isNegative = false;
            //如果是负数 
            if (Data < 0)
            {
                isNegative = true;
                Data = -Data;
            }

            //int IValue = 1;
            //for (int i = 1; i <= x; i++)
            //{
            //    IValue = IValue * 10;
            //}

            int IValue = 10^x;

            double Int = Math.Round(Data * IValue + 0.5, 0);
            Data = Int / IValue;

            if (isNegative)
            {
                Data = -Data;
            }

            return Data;
        }
        /// <summary>
        /// 动态修正数值
        /// </summary>
        /// <param name="value">要进行处理的数据</param>
        /// <param name="pos">保留的小数位数</param>
        /// <param name="x">边界值</param>
        /// <returns>结果值</returns>
        public double DynamicRound(double value, int pos, int x)
        {
            var mm =Math.Pow(10,pos);
            var a = value * mm;
            var b = Math.Floor(a);
            if (x == 0)
            {
                if ((a - b) * 10 >= 5)
                {
                    b = b + 1;
                }
            }
            else
            {
                if ((a - b) * 10 >= x)
                {
                    b = b + 1;
                }
            }

            return b / mm;
        }
        /// <summary>
        /// 是否修正数值
        /// </summary>
        public bool IsRound
        {
            get { return (bool)GetValue(IsRoundProperty); }
            set { SetValue(IsRoundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsRound.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsRoundProperty =
            DependencyProperty.Register("IsRound", typeof(bool), typeof(MDFTextBox), new PropertyMetadata(false));

        /// <summary>
        /// 保留小数位数
        /// </summary>
        public int DecimalPlaces
        {
            get { return (int)GetValue(DecimalPlacesProperty); }
            set { SetValue(DecimalPlacesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DecimalPlaces.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DecimalPlacesProperty =
            DependencyProperty.Register("DecimalPlaces", typeof(int), typeof(MDFTextBox), new PropertyMetadata(0));


        /// <summary>
        /// 界定舍取值边界
        /// </summary>
        public int CutoffValue
        {
            get { return (int)GetValue(CutoffValueProperty); }
            set { SetValue(CutoffValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CutoffValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CutoffValueProperty =
            DependencyProperty.Register("CutoffValue", typeof(int), typeof(MDFTextBox), new PropertyMetadata(0));

        
        
    }

    public enum TextModeEnum
    {
        All = 0,
        Num = 1,
        Int = 2
    }
}
