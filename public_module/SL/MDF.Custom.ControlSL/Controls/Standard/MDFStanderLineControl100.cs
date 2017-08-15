using MDF.Custom.ControlSL.Tools;
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

namespace MDF.Custom.ControlSL.Controls.Standard
{
    public class MDFStanderLineControl100 : ContentControl
    {
        public MDFStanderLineControl100()
        {
            this.DefaultStyleKey = typeof(MDFStanderLineControl100);

            ColorUnity.HtmlToColor("#FFFFFFF");
        }

        #region 背景颜色
        /// <summary>
        /// #FF4488DA
        /// </summary>
        public SolidColorBrush OutLeftBrush
        {
            get { return (SolidColorBrush)GetValue(OutLeftBrushProperty); }
            set { SetValue(OutLeftBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OutLeftBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutLeftBrushProperty =
            DependencyProperty.Register("OutLeftBrush", typeof(SolidColorBrush), typeof(MDFStanderLineControl100), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));

        /// <summary>
        /// #FF4488DA
        /// </summary>
        public SolidColorBrush OutRightBrush
        {
            get { return (SolidColorBrush)GetValue(OutRightBrushProperty); }
            set { SetValue(OutRightBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OutRightBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutRightBrushProperty =
            DependencyProperty.Register("OutRightBrush", typeof(SolidColorBrush), typeof(MDFStanderLineControl100), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));

        

        /// <summary>
        /// #FFB16AD3
        /// </summary>
        public SolidColorBrush InLeftBrush
        {
            get { return (SolidColorBrush)GetValue(InLeftBrushProperty); }
            set { SetValue(InLeftBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InLeftBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InLeftBrushProperty =
            DependencyProperty.Register("InLeftBrush", typeof(SolidColorBrush), typeof(MDFStanderLineControl100), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));


        /// <summary>
        /// #FFB16AD3
        /// </summary>
        public SolidColorBrush InRightBrush
        {
            get { return (SolidColorBrush)GetValue(InRightBrushProperty); }
            set { SetValue(InRightBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InRightBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InRightBrushProperty =
            DependencyProperty.Register("InRightBrush", typeof(SolidColorBrush), typeof(MDFStanderLineControl100), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));

        
        /// <summary>
        /// #FF7CB1F1
        /// </summary>
        public SolidColorBrush CenterBrush
        {
            get { return (SolidColorBrush)GetValue(CenterBrushProperty); }
            set { SetValue(CenterBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CenterBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CenterBrushProperty =
            DependencyProperty.Register("CenterBrush", typeof(SolidColorBrush), typeof(MDFStanderLineControl100), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));
         
        #endregion 

        #region Margin设置
        /// <summary>
        /// 下下限和下限之间的Margin
        /// </summary>
        public Thickness MarginLowerLeft
        {
            get { return (Thickness)GetValue(MarginLowerLeftProperty); }
            set { SetValue(MarginLowerLeftProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MarginLowerLeft.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarginLowerLeftProperty =
            DependencyProperty.Register("MarginLowerLeft", typeof(Thickness), typeof(MDFStanderLineControl100), new PropertyMetadata(new Thickness(0, 0, 5, 0)));


        /// <summary>
        ///  下限和设定值之间的Margin
        /// </summary>
        public Thickness MarginLeft
        {
            get { return (Thickness)GetValue(MarginLeftProperty); }
            set { SetValue(MarginLeftProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MarginLeft.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarginLeftProperty =
            DependencyProperty.Register("MarginLeft", typeof(Thickness), typeof(MDFStanderLineControl100), new PropertyMetadata(new Thickness(0, 0, 5, 0)));

        /// <summary>
        /// 设定值和上限之间的Margin
        /// </summary>

        public Thickness CenterMargin
        {
            get { return (Thickness)GetValue(CenterMarginProperty); }
            set { SetValue(CenterMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CenterMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CenterMarginProperty =
            DependencyProperty.Register("CenterMargin", typeof(Thickness), typeof(MDFStanderLineControl100), new PropertyMetadata(new Thickness(0, 0, 5, 0)));


        /// <summary>
        ///上限和上上限之间的Margin
        /// </summary>
        public Thickness MarginRight
        {
            get { return (Thickness)GetValue(MarginRightProperty); }
            set { SetValue(MarginRightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MarginRight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarginRightProperty =
            DependencyProperty.Register("MarginRight", typeof(Thickness), typeof(MDFStanderLineControl100), new PropertyMetadata(new Thickness(0, 0, 5, 0)));


        #endregion

        #region 是否显示该区块

        /// <summary>
        /// 是否显示下下限块
        /// </summary>  
        public Visibility ULowerLimtVisibility
        {
            get { return (Visibility)GetValue(ULowerLimtVisibilityProperty); }
            set { SetValue(ULowerLimtVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ULowerLimtVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ULowerLimtVisibilityProperty =
            DependencyProperty.Register("ULowerLimtVisibility", typeof(Visibility), typeof(MDFStanderLineControl100), new PropertyMetadata(Visibility.Visible));


        /// <summary>
        /// 是否显示下限快
        /// </summary>
        public Visibility LowerLimtVisibility
        {
            get { return (Visibility)GetValue(LowerLimtVisibilityProperty); }
            set { SetValue(LowerLimtVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LowerLimtVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LowerLimtVisibilityProperty =
            DependencyProperty.Register("LowerLimtVisibility", typeof(Visibility), typeof(MDFStanderLineControl100), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// 是否显示设定值块
        /// </summary>
        public Visibility SetValueVisibility
        {
            get { return (Visibility)GetValue(SetValueVisibilityProperty); }
            set { SetValue(SetValueVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SetValueVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SetValueVisibilityProperty =
            DependencyProperty.Register("SetValueVisibility", typeof(Visibility), typeof(MDFStanderLineControl100), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// 是否显示上限块
        /// </summary>
        public Visibility UpperLimtVisibility
        {
            get { return (Visibility)GetValue(UpperLimtVisibilityProperty); }
            set { SetValue(UpperLimtVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UpperLimtVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpperLimtVisibilityProperty =
            DependencyProperty.Register("UpperLimtVisibility", typeof(Visibility), typeof(MDFStanderLineControl100), new PropertyMetadata(Visibility.Visible));


        /// <summary>
        /// 是否显示上上限块
        /// </summary>
        public Visibility UUpperLimtVisibility
        {
            get { return (Visibility)GetValue(UUpperLimtVisibilityProperty); }
            set { SetValue(UUpperLimtVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UUpperLimtVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UUpperLimtVisibilityProperty =
            DependencyProperty.Register("UUpperLimtVisibility", typeof(Visibility), typeof(MDFStanderLineControl100), new PropertyMetadata(Visibility.Visible));

        #endregion

        #region 值设定
        /// <summary>
        /// 设定值
        /// </summary>
        public new string SetValue
        {
            get { return (string)GetValue(SetValueProperty); }
            set { SetValue(SetValueProperty, value); }
        }
        // Using a DependencyProperty as the backing store for SetValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SetValueProperty =
            DependencyProperty.Register("SetValue", typeof(string), typeof(MDFStanderLineControl100), new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
            {
                var value = (string)e.NewValue;
                if (value == "")
                {
                    (sender as MDFStanderLineControl100).CenterBrush = new SolidColorBrush(Colors.LightGray);
                }
                else
                {
                    (sender as MDFStanderLineControl100).CenterBrush = new SolidColorBrush(ColorUnity.HtmlToColor("#FF58d88d"));
                }
                if(e.NewValue==null)
                    (sender as MDFStanderLineControl100).CenterBrush = new SolidColorBrush(Colors.LightGray);

            })));

        /// <summary>
        /// 上限
        /// </summary> 
        public string UpperLimt
        {
            get { return (string)GetValue(UpperLimtProperty); }
            set { SetValue(UpperLimtProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UpperLimt.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpperLimtProperty =
            DependencyProperty.Register("UpperLimt", typeof(string), typeof(MDFStanderLineControl100), new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
                {
                    var value = (string)e.NewValue;
                    if (value == "")
                    {
                        (sender as MDFStanderLineControl100).InRightBrush = new SolidColorBrush(Colors.LightGray);
                    }
                    else
                    {
                        (sender as MDFStanderLineControl100).InRightBrush = new SolidColorBrush(ColorUnity.HtmlToColor("#FFffb118"));
                    }
                    if (e.NewValue == null)
                        (sender as MDFStanderLineControl100).InRightBrush = new SolidColorBrush(Colors.LightGray);

                })));

        /// <summary>
        /// 下限
        /// </summary>
        public string LowerLimt
        {
            get { return (string)GetValue(LowerLimtProperty); }
            set { SetValue(LowerLimtProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LowerLimt.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LowerLimtProperty =
            DependencyProperty.Register("LowerLimt", typeof(string), typeof(MDFStanderLineControl100), new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
                {
                    var value = (string)e.NewValue;
                    if (value == "")
                    {
                        (sender as MDFStanderLineControl100).InLeftBrush = new SolidColorBrush(Colors.LightGray);
                    }
                    else
                    {
                        (sender as MDFStanderLineControl100).InLeftBrush = new SolidColorBrush(ColorUnity.HtmlToColor("#FFffb118"));
                    }
                    if (e.NewValue == null)
                        (sender as MDFStanderLineControl100).InLeftBrush = new SolidColorBrush(Colors.LightGray);
                })));

        /// <summary>
        /// 上上限
        /// </summary> 
        public string UltimateUpperLlimit
        {
            get { return (string)GetValue(UltimateUpperLlimitProperty); }
            set { SetValue(UltimateUpperLlimitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UltimateUpperLlimit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UltimateUpperLlimitProperty =
            DependencyProperty.Register("UltimateUpperLlimit", typeof(string), typeof(MDFStanderLineControl100), new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
                {

                    //var value = (string)e.NewValue;
                    //if (value == "")
                    //{
                    //    (sender as MDFStanderLineControl100).UUpperLimtVisibility = Visibility.Collapsed;
                    //}
                    //else
                    //{
                    //    (sender as MDFStanderLineControl100).UUpperLimtVisibility = Visibility.Visible;
                    //}
                    var value = (string)e.NewValue;
                    if (value == "")
                    {
                        (sender as MDFStanderLineControl100).OutRightBrush = new SolidColorBrush(Colors.LightGray);
                    }
                    else
                    {
                        (sender as MDFStanderLineControl100).OutRightBrush = new SolidColorBrush(ColorUnity.HtmlToColor("#FFf76532"));
                    }
                    if (e.NewValue == null)
                        (sender as MDFStanderLineControl100).OutRightBrush = new SolidColorBrush(Colors.LightGray);
                })));

        /// <summary>
        /// 下下限
        /// </summary>
        public string UltimateLowerLimt
        {
            get { return (string)GetValue(UltimateLowerLimtProperty); }
            set { SetValue(UltimateLowerLimtProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UltimateLowerLimt.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UltimateLowerLimtProperty =
            DependencyProperty.Register("UltimateLowerLimt", typeof(string), typeof(MDFStanderLineControl100), new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
                {
                    var value = (string)e.NewValue;
                    if (value == "")
                    {
                        (sender as MDFStanderLineControl100).OutLeftBrush = new SolidColorBrush(Colors.LightGray);
                    }
                    else
                    {
                        (sender as MDFStanderLineControl100).OutLeftBrush = new SolidColorBrush(ColorUnity.HtmlToColor("#FFf76532"));
                    }
                    if (e.NewValue == null)
                        (sender as MDFStanderLineControl100).OutLeftBrush = new SolidColorBrush(Colors.LightGray);
                })));


        #endregion

        #region 是否包含

        /// <summary>
        /// 包含上限
        /// </summary>
        public bool IncludeUpperLimt
        {
            get { return (bool)GetValue(IncludeUpperLimtProperty); }
            set { SetValue(IncludeUpperLimtProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IncludeUpperLimt.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IncludeUpperLimtProperty =
            DependencyProperty.Register("IncludeUpperLimt", typeof(bool), typeof(MDFStanderLineControl100), new PropertyMetadata(false, new PropertyChangedCallback((sender, e) =>
            {
                var value = (bool)e.NewValue;
                if (value)
                {
                    (sender as MDFStanderLineControl100).CenterMargin = new Thickness(0, 0, 0, 0);
                    (sender as MDFStanderLineControl100).RightMargin = 55;
                }
                else
                {
                    (sender as MDFStanderLineControl100).CenterMargin = new Thickness(0, 0, 5, 0);
                    (sender as MDFStanderLineControl100).RightMargin = 60;
                }
            })));


        /// <summary>
        /// 包含下限
        /// </summary>
        public bool IncludeLowerLimt
        {
            get { return (bool)GetValue(IncludeLowerLimtProperty); }
            set { SetValue(IncludeLowerLimtProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IncludeLowerLimt.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IncludeLowerLimtProperty =
            DependencyProperty.Register("IncludeLowerLimt", typeof(bool), typeof(MDFStanderLineControl100), new PropertyMetadata(false, new PropertyChangedCallback((sender, e) =>
            {
                var value = (bool)e.NewValue;
                if (value)
                {
                    (sender as MDFStanderLineControl100).MarginLeft = new Thickness(0, 0, 0, 0);
                    (sender as MDFStanderLineControl100).LeftMargin = 55;
                }
                else
                {
                    (sender as MDFStanderLineControl100).MarginLeft = new Thickness(0, 0, 5, 0);
                    (sender as MDFStanderLineControl100).LeftMargin = 60;
                }
            }
                )));


        /// <summary>
        /// 包含上上限
        /// </summary>
        public bool IncludeUUpperLimt
        {
            get { return (bool)GetValue(IncludeUUpperLimtProperty); }
            set { SetValue(IncludeUUpperLimtProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IncludeUUpperLimt.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IncludeUUpperLimtProperty =
            DependencyProperty.Register("IncludeUUpperLimt", typeof(bool), typeof(MDFStanderLineControl100), new PropertyMetadata(false, new PropertyChangedCallback((sender, e) =>
            {
                var value = (bool)e.NewValue;
                if (value)
                {
                    (sender as MDFStanderLineControl100).MarginRight = new Thickness(0, 0, 0, 0);
                    (sender as MDFStanderLineControl100).RightUMargin = 55;
                }
                else
                {
                    (sender as MDFStanderLineControl100).MarginRight = new Thickness(0, 0, 5, 0);
                    (sender as MDFStanderLineControl100).RightUMargin = 60;
                }
            }
                )));


        /// <summary>
        /// 包含下下限
        /// </summary> 
        public bool IncludeULowerLimt
        {
            get { return (bool)GetValue(IncludeULowerLimtProperty); }
            set { SetValue(IncludeULowerLimtProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IncludeULowerLimt.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IncludeULowerLimtProperty =
            DependencyProperty.Register("IncludeULowerLimt", typeof(bool), typeof(MDFStanderLineControl100), new PropertyMetadata(false, new PropertyChangedCallback((sender, e) =>
            {
                var value = (bool)e.NewValue;
                if (value)
                {
                    (sender as MDFStanderLineControl100).MarginLowerLeft = new Thickness(0, 0, 0, 0);
                    (sender as MDFStanderLineControl100).LeftUMargin = 55;
                }
                else
                {
                    (sender as MDFStanderLineControl100).MarginLowerLeft = new Thickness(0, 0, 5, 0);
                    (sender as MDFStanderLineControl100).LeftUMargin = 60;
                }

            }
                )));

        #endregion

        #region 坐标轴长度 

        public int LeftUMargin
        {
            get { return (int)GetValue(LeftUMarginProperty); }
            set { SetValue(LeftUMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftUMarginProperty =
            DependencyProperty.Register("LeftUMargin", typeof(int), typeof(MDFStanderLineControl100), new PropertyMetadata(0));


        public int LeftMargin
        {
            get { return (int)GetValue(LeftMarginProperty); }
            set { SetValue(LeftMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftMarginProperty =
            DependencyProperty.Register("LeftMargin", typeof(int), typeof(MDFStanderLineControl100), new PropertyMetadata(0));


        public int RightMargin
        {
            get { return (int)GetValue(RightMarginProperty); }
            set { SetValue(RightMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightMarginProperty =
            DependencyProperty.Register("RightMargin", typeof(int), typeof(MDFStanderLineControl100), new PropertyMetadata(0));

        public int RightUMargin
        {
            get { return (int)GetValue(RightUMarginProperty); }
            set { SetValue(RightUMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightUMarginProperty =
            DependencyProperty.Register("RightUMargin", typeof(int), typeof(MDFStanderLineControl100), new PropertyMetadata(0));
        #endregion
    }
}
