using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MDF.Custom.ControlSL.Layout
{
    public class MDFGroupPanel : ContentControl
    {
        public MDFGroupPanel()
        {
            base.DefaultStyleKey = typeof(MDFGroupPanel);
            this.Loaded += OnGroupPanelLoaded;
        }
        ScrollViewer _contentViewer;
        ToggleButton _contentToggleButton;
        private bool _recalculateMargins = true;

        #region Events Handlers
        void OnParentLayoutUpdated(object sender, System.EventArgs e)
        {
            CalculateMargins();
            if (tTranslate.X > _maxMarginLeft)
                tTranslate.X = _maxMarginLeft;
            if (tTranslate.Y > _maxMarginTop)
                tTranslate.Y = _maxMarginTop;
        }

        void OnGroupPanelLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var parent = this.Parent as FrameworkElement;
                parent.LayoutUpdated += OnParentLayoutUpdated;

                this.UpdateLayout();
                if (VisualTreeHelper.GetChildrenCount(this) > 0)
                {
                    var rootElement = VisualTreeHelper.GetChild(this, 0) as Border;
                    if (rootElement != null)
                    {
                        var winHandle = rootElement.FindName("headerPanel") as Border;
                        _contentToggleButton = rootElement.FindName("contentToggleButton") as ToggleButton;
                        _contentViewer = rootElement.FindName("contentViewer") as ScrollViewer;

                        if (winHandle != null)
                        {
                            winHandle.MouseMove += OnWinHandleMouseMove;
                            winHandle.MouseLeftButtonDown += OnWinHandleMouseLeftButtonDown;
                            winHandle.MouseLeftButtonUp += OnWinHandleMouseLeftButtonUp;
                            winHandle.MouseRightButtonDown += OnWinHandleMouseRightButtonDown;
                            winHandle.MouseRightButtonUp += OnWinHandleMouseRightButtonUp;
                            winHandle.MouseLeave += OnWinHandleMouseLeave;
                        }
                    }
                }

                this.LayoutUpdated += OnGroupPanelLayoutUpdated;

                if (this.IsMovable)
                    SetTransform();
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine("GroupPanel Loading Error: " + ex.Message);
            }
        }
        void OnGroupPanelLayoutUpdated(object sender, System.EventArgs e)
        {
            if (_recalculateMargins)
            {
                CalculateMargins();
                Move(this.tTranslate.X, this.tTranslate.Y);
                _recalculateMargins = false;
            }
        }
        #endregion

        private void SetTransform()
        {
            if (this.Parent as FrameworkElement != null)
            {
                GeneralTransform objGeneralTransform = this.TransformToVisual(this.Parent as FrameworkElement);
                Point point = objGeneralTransform.Transform(new Point(0, 0));
                tTranslate.X = point.X;
                tTranslate.Y = point.Y;
                this.Margin = new Thickness(0);
                this.VerticalAlignment = VerticalAlignment.Top;
                this.HorizontalAlignment = HorizontalAlignment.Left;
                this.RenderTransform = tTranslate;
            }
        }

        #region Moving Logic
        TranslateTransform tTranslate = new TranslateTransform();
        Point _borderPosition;
        Point _currentPosition;
        double _maxMarginLeft;
        double _maxMarginTop;
        bool _dragOn = false;
        private void OnWinHandleMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsMovable)
            {
                e.Handled = true;
                this.Opacity = 0.0;
            }
        }
        private void OnWinHandleMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            this.Opacity = 1.0;
        }
        private void OnWinHandleMouseLeave(object sender, MouseEventArgs e)
        {
            this.Opacity = 1.0;
        }
        private void OnWinHandleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.ToggleButtonVisibility == System.Windows.Visibility.Visible && e.ClickCount == 2)
            {
                this.IsExpanded = !this.IsExpanded;
            }

            if (!this.IsMovable) return;

            var c = sender as FrameworkElement;
            _dragOn = true;
            this.Opacity *= 0.5;
            _borderPosition = e.GetPosition(sender as Border);

            CalculateMargins();

            if (c != null) c.CaptureMouse();
        }
        private void OnWinHandleMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_dragOn)
            {
                var c = sender as FrameworkElement;
                this.Opacity = 1;
                if (c != null) c.ReleaseMouseCapture();
                _dragOn = false;
            }
        }

        private void OnWinHandleMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragOn)
            {
                _currentPosition = e.GetPosition(sender as Border);
                var x = tTranslate.X + _currentPosition.X - _borderPosition.X;
                var y = tTranslate.Y + _currentPosition.Y - _borderPosition.Y;

                Move(x, y);
            }
        }

        private void CalculateMargins()
        {
            var parent = (this.Parent as FrameworkElement);
            if (parent != null)
            {
                //TODO: offset by control's Margin
                _maxMarginLeft = parent.ActualWidth - this.ActualWidth;
                _maxMarginTop = parent.ActualHeight - this.ActualHeight;
            }
        }

        private void Move(double x, double y)
        {
            if (x < 0)
                x = 0;
            if (y < 0)
                y = 0;
            if (x > _maxMarginLeft)
                x = _maxMarginLeft;
            if (y > _maxMarginTop)
                y = _maxMarginTop;
            tTranslate.X = x;
            tTranslate.Y = y;
        }
        #endregion

        private void ToggleExpandedState(bool isExpanded)
        {

        }

        #region Dependency Properties

        /// <summary>
        /// Gets or sets Header Text of the GroupPanel
        /// </summary>
        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        public static DependencyProperty HeaderTextProperty = DependencyProperty.Register(
         "HeaderText", typeof(string), typeof(MDFGroupPanel), null);

        public const string IsMovablePropertyName = "IsMovable";
        public static DependencyProperty IsMovableProperty = DependencyProperty.Register(
         IsMovablePropertyName, typeof(bool), typeof(MDFGroupPanel), new PropertyMetadata(false, (sender, e) =>
         {
             (sender as MDFGroupPanel).OnPropertyChanged(new PropertyChangedEventArgs(IsMovablePropertyName));
         }));

        /// <summary>
        /// Gets or sets whether the GroupPanel can be movable within its parent control's boundaries.  
        /// </summary>
        public bool IsMovable
        {
            get { return (bool)GetValue(IsMovableProperty); }
            set { SetValue(IsMovableProperty, value); }
        }
        public const string IsExpandablePropertyName = "IsExpandable";
        public static DependencyProperty IsExpandableProperty = DependencyProperty.Register(
            IsExpandablePropertyName, typeof(bool), typeof(MDFGroupPanel), new PropertyMetadata(true, (sender, e) =>
            {
                (sender as MDFGroupPanel).OnPropertyChanged(new PropertyChangedEventArgs(IsExpandablePropertyName));
            }));

        /// <summary>
        /// Gets or sets whether the GroupPanel can be movable within its parent control's boundaries.  
        /// </summary>
        public bool IsExpandable
        {
            get { return (bool)GetValue(IsExpandableProperty); }
            set { SetValue(IsExpandableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsExpanded.  This enables animation, styling, binding, etc...
        public const string IsExpandedPropertyName = "IsExpanded";
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register(IsExpandedPropertyName, typeof(bool), typeof(MDFGroupPanel), new PropertyMetadata(true, (sender, e) =>
            {
                (sender as MDFGroupPanel).OnPropertyChanged(new PropertyChangedEventArgs(IsExpandedPropertyName));
            }));

        /// <summary>
        /// Gets or sets whether the GroupPanel is expanded
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        private void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            switch (ea.PropertyName)
            {
                case IsMovablePropertyName:
                    {
                        if (this.IsMovable)
                            SetTransform();
                        break;
                    }
                case IsExpandablePropertyName:
                    {
                        // behavior implemented using binding to this property in the generic style
                        break;
                    }
                case IsExpandedPropertyName:
                    {
                        _recalculateMargins = true;
                        break;
                    }
                case ToggleButtonVisibilityName:
                    {
                        break;
                    }
                case HeaderIconName:
                    {
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// 状态按钮可视状态
        /// </summary>
        public Visibility ToggleButtonVisibility
        {
            get { return (Visibility)GetValue(ToggleButtonVisibilityProperty); }
            set { SetValue(ToggleButtonVisibilityProperty, value); }
        }

        public const string ToggleButtonVisibilityName = "ToggleButtonVisibility";
        public static readonly DependencyProperty ToggleButtonVisibilityProperty =
            DependencyProperty.Register(ToggleButtonVisibilityName, typeof(Visibility), typeof(MDFGroupPanel), new PropertyMetadata(Visibility.Collapsed, (sender, e) =>
            {
                (sender as MDFGroupPanel).OnPropertyChanged(new PropertyChangedEventArgs(ToggleButtonVisibilityName));
            }));

        public ImageSource HeaderIcon
        {
            get { return (ImageSource)GetValue(HeaderIconProperty); }
            set { SetValue(HeaderIconProperty, value); }
        }

        /// <summary>
        /// 图标
        /// </summary>
        public const string HeaderIconName = "HeaderIcon";
        public static readonly DependencyProperty HeaderIconProperty =
            DependencyProperty.Register(HeaderIconName, typeof(ImageSource), typeof(MDFGroupPanel), new PropertyMetadata(default(ImageSource), (sender, e) =>
            {
                (sender as MDFGroupPanel).OnPropertyChanged(new PropertyChangedEventArgs(HeaderIconName));
            }));

        /// <summary>
        /// 内容区域外边距
        /// </summary>
        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentMarginProperty =
            DependencyProperty.Register("ContentMargin", typeof(Thickness), typeof(MDFGroupPanel), new PropertyMetadata(new Thickness(0)));

        /// <summary>
        /// 内容区域边框厚度
        /// </summary>
        public Thickness ContentBorderThickness
        {
            get { return (Thickness)GetValue(ContentBorderThicknessProperty); }
            set { SetValue(ContentBorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentBorderThicknessProperty =
            DependencyProperty.Register("ContentBorderThickness", typeof(Thickness), typeof(MDFGroupPanel), new PropertyMetadata(new Thickness(0)));

        /// <summary>
        /// 内容区域内边距
        /// </summary>
        public Thickness ContentPadding
        {
            get { return (Thickness)GetValue(ContentPaddingProperty); }
            set { SetValue(ContentPaddingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ContentPadding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentPaddingProperty =
            DependencyProperty.Register("ContentPadding", typeof(Thickness), typeof(MDFGroupPanel), new PropertyMetadata(new Thickness(5)));

        /// <summary>
        /// 水平滚动条可视状态
        /// </summary>
        public ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(HorizontalScrollBarVisibilityProperty); }
            set { SetValue(HorizontalScrollBarVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalScrollBarVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalScrollBarVisibilityProperty =
            DependencyProperty.Register("HorizontalScrollBarVisibility", typeof(ScrollBarVisibility), typeof(MDFGroupPanel), new PropertyMetadata(ScrollBarVisibility.Disabled));

        /// <summary>
        /// 垂直滚动条可视状态
        /// </summary>
        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(VerticalScrollBarVisibilityProperty); }
            set { SetValue(VerticalScrollBarVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalScrollBarVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalScrollBarVisibilityProperty =
            DependencyProperty.Register("VerticalScrollBarVisibility", typeof(ScrollBarVisibility), typeof(MDFGroupPanel), new PropertyMetadata(ScrollBarVisibility.Disabled));

        #endregion

        public UIElement HeardContent
        {
            get { return (UIElement)GetValue(HeardContentProperty); }
            set { SetValue(HeardContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeardContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeardContentProperty =
            DependencyProperty.Register("HeardContent", typeof(UIElement), typeof(MDFGroupPanel), new PropertyMetadata(default(UIElement)));

    }
}
