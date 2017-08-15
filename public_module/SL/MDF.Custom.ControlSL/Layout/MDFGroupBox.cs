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
    [TemplatePart(Name = "TitleRectangleGeometryPart", Type = typeof(RectangleGeometry)),
     TemplatePart(Name = "ContentRectangleGeometryPart", Type = typeof(RectangleGeometry)),
     TemplatePart(Name = "TitlePart", Type = typeof(ContentControl)),
     TemplateVisualState(Name = "Normal", GroupName = "CommonStates"),
     TemplateVisualState(Name = "Disabled", GroupName = "CommonStates")]
    public class MDFGroupBox : ContentControl
    {
        private RectangleGeometry _contentRectangleGeometryPart;
        private ContentControl _titlePart;
        private RectangleGeometry _titleRectangleGeometryPart;
        public static new readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register("IsEnabled", typeof(bool), typeof(MDFGroupBox), new PropertyMetadata(true, new PropertyChangedCallback(MDFGroupBox.IsEnabledPropertyChangedCallback)));
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(object), typeof(MDFGroupBox), null);
        public static readonly DependencyProperty TitleTemplateProperty = DependencyProperty.Register("TitleTemplate", typeof(DataTemplate), typeof(MDFGroupBox), null);
        private RectangleGeometry ContentRectangleGeometryPart
        {
            get { return this._contentRectangleGeometryPart; }
            set { this._contentRectangleGeometryPart = value; }
        }
        public new bool IsEnabled
        {
            get { return (bool)base.GetValue(MDFGroupBox.IsEnabledProperty); }
            set { base.SetValue(MDFGroupBox.IsEnabledProperty, value); }
        }
        public object Title
        {
            get { return base.GetValue(MDFGroupBox.TitleProperty); }
            set { base.SetValue(MDFGroupBox.TitleProperty, value); }
        }
        private ContentControl TitlePart
        {
            get { return this._titlePart; }
            set
            {
                ContentControl titlePart = this._titlePart;
                if (titlePart != null)
                    titlePart.SizeChanged -= (new SizeChangedEventHandler(this.TitlePart_SizeChanged));
                this._titlePart = value;
                if (this._titlePart != null)
                    this._titlePart.SizeChanged += (new SizeChangedEventHandler(this.TitlePart_SizeChanged));
            }
        }
        private RectangleGeometry TitleRectangleGeometryPart
        {
            get { return this._titleRectangleGeometryPart; }
            set { this._titleRectangleGeometryPart = value; }
        }
        public DataTemplate TitleTemplate
        {
            get { return (DataTemplate)base.GetValue(MDFGroupBox.TitleTemplateProperty); }
            set { base.SetValue(MDFGroupBox.TitleTemplateProperty, value); }
        }
        public MDFGroupBox()
        {
            base.DefaultStyleKey = (typeof(MDFGroupBox));
            base.SizeChanged += (new SizeChangedEventHandler(this.GroupBox_SizeChanged));
        }
        private void ApplyCommonState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, this.IsEnabled ? "Normal" : "Disabled", useTransitions);
        }
        private void GroupBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this._contentRectangleGeometryPart != null)
            {
                this._contentRectangleGeometryPart.Rect = (new Rect(default(Point), e.NewSize));
            }
        }
        private static void IsEnabledPropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MDFGroupBox)sender).OnIsEnabledPropertyChanged(e);
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ContentRectangleGeometryPart = (RectangleGeometry)base.GetTemplateChild("ContentRectangleGeometryPart");
            this.TitleRectangleGeometryPart = (RectangleGeometry)base.GetTemplateChild("TitleRectangleGeometryPart");
            this.TitlePart = (ContentControl)base.GetTemplateChild("TitlePart");
        }
        private void OnIsEnabledPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.IsEnabled = (this.IsEnabled);
            this.ApplyCommonState(true);
        }
        private void TitlePart_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this._titleRectangleGeometryPart != null && this._titlePart != null)
            {
                this._titleRectangleGeometryPart.Rect = (new Rect(new Point(this._titlePart.Margin.Left, 0.0), e.NewSize));
            }
        }
    }
}
