using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MDF.Custom.ControlSL.Tools
{
    public class SLControlUnity
    {
        static string _webUrl;
        public static string WebUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_webUrl))
                {
                    _webUrl = Application.Current.Host.Source.AbsoluteUri;
                    _webUrl = _webUrl.Replace(_webUrl.Substring(_webUrl.IndexOf("/ClientBin")), "");
                    return _webUrl;
                }
                else
                    return _webUrl;
            }
        }

        public static ImageSource GetWebImageSource(string url)
        {
            string imagePath = SLControlUnity.WebUrl + url;
            BitmapImage image = new BitmapImage(new Uri(imagePath));
            imagePath = null;
            return image;
        }

        /// <summary>
        /// 得到两个控件间最近的两点(上下左右)
        /// </summary>
        /// <param name="container"></param>
        /// <param name="ui"></param>
        /// <param name="toui"></param>
        /// <returns></returns>
        public static Tuple<Point, Point> GetNearPoint(UIElement container, FrameworkElement ui, FrameworkElement toui)
        {
            if (ui == null || toui == null)
                return new Tuple<Point, Point>(new Point(), new Point());
            Point uiPoint = ui.TransformToVisual(container).Transform(new Point(0, 0));
            List<Point> sourcep = new List<Point>();
            sourcep.Add(new Point(uiPoint.X + ui.Width / 2, uiPoint.Y));//上
            sourcep.Add(new Point(uiPoint.X, uiPoint.Y + ui.Height / 2));//左
            sourcep.Add(new Point(uiPoint.X + ui.Width / 2, uiPoint.Y + ui.Height));//下
            sourcep.Add(new Point(uiPoint.X + ui.Width, uiPoint.Y + ui.Height / 2));//右

            Point touiPoint = toui.TransformToVisual(container).Transform(new Point(0, 0));
            List<Point> top = new List<Point>();
            top.Add(new Point(touiPoint.X + toui.Width / 2, touiPoint.Y));
            top.Add(new Point(touiPoint.X, touiPoint.Y + toui.Height / 2));
            top.Add(new Point(touiPoint.X + toui.Width / 2, touiPoint.Y + toui.Height));
            top.Add(new Point(touiPoint.X + toui.Width, touiPoint.Y + toui.Height / 2));

            //return new Tuple<Point, Point>(new Point(uiPoint.X + ui.RenderSize.Width / 2, uiPoint.Y),
            //    new Point(touiPoint.X + toui.RenderSize.Width / 2, touiPoint.Y));
            return ComplateNearPoint(sourcep, top);

        }

        public static List<Point> GetControlPointLeftRight(UIElement container, FrameworkElement element)
        {
            List<Point> plist = new List<Point>();
            Point uiPoint = element.TransformToVisual(container).Transform(new Point(0, 0));
            plist.Add(new Point(uiPoint.X, uiPoint.Y + element.Height / 2));//左
            plist.Add(new Point(uiPoint.X + element.Width, uiPoint.Y + element.Height / 2));//右
            return plist;
        }

        public static List<Point> GetControl4Point(UIElement container, FrameworkElement element)
        {
            List<Point> plist = new List<Point>();
            Point uiPoint = element.TransformToVisual(container).Transform(new Point(0, 0));
            plist.Add(new Point(uiPoint.X, uiPoint.Y + element.ActualHeight / 2));//左
            plist.Add(new Point(uiPoint.X + element.ActualWidth, uiPoint.Y + element.ActualHeight / 2));//右
            plist.Add(new Point(uiPoint.X + element.ActualWidth / 2, uiPoint.Y)); //上
            plist.Add(new Point(uiPoint.X+element.ActualWidth/2,uiPoint.Y+element.ActualHeight)); //下点
            return plist;
        }

        public static List<Point> GetControl8Point(UIElement container, FrameworkElement element)
        {
            List<Point> plist = new List<Point>();
            Point uiPoint = element.TransformToVisual(container).Transform(new Point(0, 0));
            plist.Add(new Point(uiPoint.X, uiPoint.Y + element.Height / 2));//左
            plist.Add(new Point(uiPoint.X + element.Width, uiPoint.Y + element.Height / 2));//右
            plist.Add(new Point(uiPoint.X + element.Width / 2, uiPoint.Y)); //上
            plist.Add(new Point(uiPoint.X + element.Width / 2, uiPoint.Y + element.Height)); //下

            plist.Add(uiPoint); //左上
            plist.Add(new Point(uiPoint.X,uiPoint.Y+element.Height));//左下
            plist.Add(new Point(uiPoint.X+element.Width,uiPoint.Y)); //右上
            plist.Add(new Point(uiPoint.X+element.Width,uiPoint.Y+element.Height));//右下

            return plist;
        }

        public static double ComplateNearLength(List<Point> source, List<Point> to)
        {

            double len = 0;
            double temp = 0;
            foreach (Point p in source)
            {
                foreach (Point tp in to)
                {
                    temp = Distance(p, tp);//计算两个点的距离
                    if (len == 0)
                        len = temp;
                    else if (temp > 0 && temp < len)
                    {
                        len = temp;
                    }
                }
            }

            return len;
        }

        /// <summary>
        /// 得到指标列表中最近的两点坐标
        /// </summary>
        /// <param name="source"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static Tuple<Point, Point> ComplateNearPoint(List<Point> source, List<Point> to)
        {
            Point p1 = new Point(), p2 = new Point();
            double len = 0;
            double tmp = 0;
            foreach (Point p in source)
            {
                foreach (Point tp in to)
                {
                    tmp = Distance(p, tp);
                    if (len == 0)
                    {
                        len = tmp;
                        p1 = p;
                        p2 = tp;
                    }

                    if (tmp > 0 && tmp < len)
                    {
                        len = tmp;
                        p1 = p;
                        p2 = tp;
                    }
                }
            }

            return new Tuple<Point, Point>(p1, p2);
        }

        /// <summary>
        /// 计算两个坐标点距离
        /// </summary>
        /// <param name="p"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public static double Distance(Point p, Point top)
        {
            double a = Math.Pow(top.X - p.X, 2) + Math.Pow(top.Y - p.Y, 2);
            return Math.Sqrt(a);
        }

        /// <summary>
        /// 得到两点间倾斜角度
        /// </summary>
        /// <param name="spoint"></param>
        /// <param name="epoint"></param>
        /// <returns></returns>
        public static double SlopeAngle(Point spoint, Point epoint)
        {
            double angleofline = Math.Atan2((epoint.Y - spoint.Y), (epoint.X - spoint.X)) * 180 / Math.PI;
            return angleofline;
        }
    }
}
