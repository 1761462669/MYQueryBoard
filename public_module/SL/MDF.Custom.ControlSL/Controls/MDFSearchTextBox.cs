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
  
        public class MDFSearchTextBox : TextBox
        {
            public MDFSearchTextBox()
            {
                this.DefaultStyleKey = typeof(MDFSearchTextBox);
                this.DefaultString = "";
                this.Loaded += MDFSearchTextBox_Loaded;
            }

            void MDFSearchTextBox_Loaded(object sender, RoutedEventArgs e)
            {
                //************增加此代码是为了触发属性通知功能(不能删除)************
                this.Text = "触发通知";
                //************增加此代码是为了触发属性通知功能(不能删除)************

                this.Text = "";
            }

            #region 事件定义
            public event RoutedEventHandler SearchButtonClick;
            #endregion

            #region 成员变量
            Button searchbnt;

            #endregion


            public override void OnApplyTemplate()
            {
                base.OnApplyTemplate();
                searchbnt = this.GetTemplateChild("bnt") as Button;
                if (searchbnt != null)
                    searchbnt.Click += searchbnt_Click;

                if (string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(DefaultString))
                {
                    Text = DefaultString;
                    this.Foreground = DefaultTextBrush;
                    this.FontStyle = FontStyles.Normal;
                }
            }

            void searchbnt_Click(object sender, RoutedEventArgs e)
            {
                if (SearchButtonClick != null)
                    SearchButtonClick(this, e);
            }

            protected override void OnGotFocus(RoutedEventArgs e)
            {
                base.OnGotFocus(e);
                if (Text == DefaultString)
                {
                    this.Foreground = TextBrush;
                    Text = "";
                    this.FontStyle = FontStyles.Normal;
                }
            }

            protected override void OnLostFocus(RoutedEventArgs e)
            {
                base.OnLostFocus(e);
                if (Text == "" && !string.IsNullOrEmpty(DefaultString))
                {
                    Text = DefaultString;
                    this.Foreground = DefaultTextBrush;
                    this.FontStyle = FontStyles.Italic;
                }

            }

            #region 属性定义
            public readonly static DependencyProperty SearchButtonStyleProperty = DependencyProperty.Register("SearchButtonStyle",
                typeof(Style), typeof(MDFSearchTextBox), new PropertyMetadata(default(Style)));
            public Style SearchButtonStyle
            {
                get
                {
                    return (Style)GetValue(SearchButtonStyleProperty);
                }
                set
                {
                    SetValue(SearchButtonStyleProperty, value);
                }
            }


            public readonly static DependencyProperty DefaultStringProperty = DependencyProperty.Register("DefaultString",
                typeof(string), typeof(MDFSearchTextBox), new PropertyMetadata(default(string)));
            public string DefaultString
            {
                get
                {
                    return (string)GetValue(DefaultStringProperty);
                }
                set
                {
                    SetValue(DefaultStringProperty, value);
                }
            }

            public readonly static DependencyProperty DefaultTextBrushProperty = DependencyProperty.Register("DefaultTextBrush", typeof(Brush), typeof(MDFSearchTextBox),
                new PropertyMetadata(new SolidColorBrush(new Color() { R = 95, G = 95, B = 95, A = 255 })));

            public Brush DefaultTextBrush
            {
                get
                {
                    return (Brush)GetValue(DefaultTextBrushProperty);
                }
                set
                {
                    SetValue(DefaultTextBrushProperty, value);
                }
            }

            public readonly static DependencyProperty TextBrushProperty = DependencyProperty.Register("TextBrush", typeof(Brush), typeof(MDFSearchTextBox),
               new PropertyMetadata(new SolidColorBrush(Colors.Black)));

            public Brush TextBrush
            {
                get
                {
                    return (Brush)GetValue(TextBrushProperty);
                }
                set
                {
                    SetValue(TextBrushProperty, value);
                }
            }
            #endregion
        }
    
}
