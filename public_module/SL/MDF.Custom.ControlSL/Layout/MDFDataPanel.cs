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
	public class MDFDataPanel:MDFGroupBox
	{
        public MDFDataPanel()
        {
            this.DefaultStyleKey=typeof(MDFDataPanel);
            this.SizeChanged += MDFDataPanel_SizeChanged;
        }

        void MDFDataPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //if (this.ToolbarRectangleGeometryPart != null && this._toolbarPart != null)
            //{
            //    Point p = this.TransformToVisual(_toolbarPart).Transform(new Point(0, 0));
            //    this.ToolbarRectangleGeometryPart.Rect = (new Rect(new Point(this.ActualWidth - e.NewSize.Width-_toolbarPart.Margin.Right, 0), e.NewSize));
            //}
        }



        private ContentControl _toolbarPart;
        private RectangleGeometry ToolbarRectangleGeometryPart;

        private ContentControl ToolbarPart
        {
            get { return this._toolbarPart; }
            set
            {
                ContentControl titlePart = this._toolbarPart;
                if (titlePart != null)
                    titlePart.SizeChanged -= (new SizeChangedEventHandler(this.ToolbarPart_SizeChanged));
                this._toolbarPart = value;
                if (this._toolbarPart != null)
                    this._toolbarPart.SizeChanged += (new SizeChangedEventHandler(this.ToolbarPart_SizeChanged));
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ToolbarPart = this.GetTemplateChild("ToolbarPart") as ContentControl;
            ToolbarRectangleGeometryPart = this.GetTemplateChild("ToolbarRectangleGeometryPart") as RectangleGeometry;

        }

        public object ToolBar
        {
            get { return (object)GetValue(ToolBarProperty); }
            set { SetValue(ToolBarProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ToolBar.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToolBarProperty =
            DependencyProperty.Register("ToolBar", typeof(object), typeof(MDFDataPanel), new PropertyMetadata(null));

        private void ToolbarPart_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //if (this.ToolbarRectangleGeometryPart != null && this._toolbarPart != null)
            //{
            //    Point p=this.TransformToVisual(_toolbarPart).Transform(new Point(0, 0));
            //    this.ToolbarRectangleGeometryPart.Rect = (new Rect(new Point(this.ActualWidth - e.NewSize.Width - _toolbarPart.Margin.Right, 0), e.NewSize));
            //}
        }
	}
}
