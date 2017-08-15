using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SMES.Framework.Utility
{
    public static class ControlPositionHelper
    {

        public static void SetTransform(this ChildWindow target, FrameworkElement baseElement)
        {
            if (target == null || baseElement == null)
                return;

            FrameworkElement root, contentRoot;
            SizeChangedEventHandler handle = (sender, e) =>
                {
                    root = System.Windows.Media.VisualTreeHelper.GetChild(target, 0) as FrameworkElement;
                    contentRoot = root.FindName("ContentRoot") as FrameworkElement;
                    if (root == null) { return; }
                    if (contentRoot == null) { return; }

                    double pageWidth = Application.Current.Host.Content.ActualWidth;
                    double pageHeight = Application.Current.Host.Content.ActualHeight;
                    target.HorizontalAlignment = HorizontalAlignment.Left;
                    target.VerticalAlignment = VerticalAlignment.Top;
                    RoutedEventHandler loadHandel = (s, e1) =>
                    {
                        InitalWindowPosition(baseElement, contentRoot, pageWidth, pageHeight, new Size(contentRoot.ActualWidth, contentRoot.ActualHeight), true);
                    };

                    SizeChangedEventHandler sizeChanged = (content, args) =>
                        {
                            InitalWindowPosition(baseElement, contentRoot, pageWidth, pageHeight, args.NewSize, true);

                        };
                    contentRoot.SizeChanged -= sizeChanged;
                    contentRoot.SizeChanged += sizeChanged;
                    contentRoot.Loaded -= loadHandel;
                    contentRoot.Loaded += loadHandel;
                    InitalWindowPosition(baseElement, contentRoot, pageWidth, pageHeight, new Size(contentRoot.ActualWidth, contentRoot.ActualHeight), false);


                };


            target.SizeChanged -= handle;
            target.SizeChanged += handle;































        }

        private static void InitalWindowPosition(FrameworkElement baseElement, FrameworkElement contentRoot, double pageWidth, double pageHeight, Size args, bool isSetMax)
        {
            double actualWidth = args.Width;
            double actualHeight = args.Height;
            var aa = baseElement.TransformToVisual(null);
            var start = baseElement.TransformToVisual(null).Transform(new Point(0, 0));
            var top = start.Y;
            var left = start.X;
            var right = pageWidth - left;
            var bottom = pageHeight - (top + baseElement.Height);
            var tWidth = actualWidth;
            var tHeight = actualHeight;
            double leftPosition = start.X;
            double topPostion = start.Y + baseElement.Height + 1;
            var maxHeight = Math.Max(top, bottom) * 4 / 5;
            if (isSetMax)
                contentRoot.MaxHeight = Math.Min(tHeight, maxHeight);
            else
                contentRoot.Height = Math.Min(tHeight, maxHeight);
            if (right < tWidth)
            {
                leftPosition = pageWidth - tWidth - 5;
                if (leftPosition <= 0)
                {
                    leftPosition = 0;
                    if (isSetMax)
                        contentRoot.MaxWidth = pageWidth - 1;
                    else
                        contentRoot.Width = pageWidth - 1;

                }
            }
            if (top > bottom)
            {
                topPostion = topPostion - baseElement.ActualHeight - args.Height;
            }
            var group = contentRoot.RenderTransform as TransformGroup;
            if (group == null) { return; }

            TranslateTransform translateTransform = null;
            foreach (var transform in group.Children.OfType<TranslateTransform>())
            {
                translateTransform = transform;
            }

            if (translateTransform == null) { return; }



            // reset transform
            translateTransform.X = leftPosition;
            translateTransform.Y = topPostion;
        }


        /// <summary>
        /// 从某个控件位置打开窗体
        /// </summary>
        /// <param name="childWindow"></param>
        /// <param name="baseElement"></param>
        public static void GetTransform(this ChildWindow childWindow, FrameworkElement baseElement)
        {
            if (childWindow == null || baseElement == null)
                return;
            var zoomFactory = HtmlHelper.ZoomFactor;
            var position = baseElement.TransformToVisual(null).Transform(new Point(0, 0));

            position = new Point(position.X * zoomFactory, position.Y * zoomFactory);
            var pageSize = HtmlHelper.PageSize;

            pageSize = new Size(pageSize.Width * zoomFactory, pageSize.Height * zoomFactory);



            //处理边角
            //处理位置



            bool upFlag = false;
            var start = new Point(position.X, position.Y + baseElement.ActualHeight / 2);
            if (position.Y + baseElement.ActualHeight / 2 > pageSize.Height / 2)//向上延伸
                upFlag = true;

            //计算childwindows的长度，宽度。
            //计算childwindow的位置





            var left = position.X + baseElement.Height;
            //ComboBox cob;
            //childWindow.Measure()






        }
    }
}
