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
using System.Windows.Shapes;

namespace MDF.Custom.ControlSL.Controls
{
    public class MDFBusyControl : ContentControl
    {
        List<Rectangle> rects = new List<Rectangle>();
        List<Storyboard> storys = new List<Storyboard>();

        public MDFBusyControl()
        {
            this.DefaultStyleKey = typeof(MDFBusyControl);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Rectangle rect1 = this.GetTemplateChild("rect1") as Rectangle;
            Rectangle rect2 = this.GetTemplateChild("rect2") as Rectangle;
            Rectangle rect3 = this.GetTemplateChild("rect3") as Rectangle;
            Rectangle rect4 = this.GetTemplateChild("rect4") as Rectangle;
            Rectangle rect5 = this.GetTemplateChild("rect5") as Rectangle;
            Rectangle rect6 = this.GetTemplateChild("rect6") as Rectangle;
            Grid grid = this.GetTemplateChild("grid") as Grid;
            Ellipse elipse1 = this.GetTemplateChild("elipse1") as Ellipse;
            Ellipse elipse2 = this.GetTemplateChild("elipse2") as Ellipse;
            Ellipse elipse3 = this.GetTemplateChild("elipse3") as Ellipse;

            elipse1.Width = RectMaxHeight + 20;
            elipse1.Height = RectMaxHeight + 20;

            elipse2.Width = RectMaxHeight + 20 + 60;
            elipse2.Height = RectMaxHeight + 20 + 60;

            elipse3.Width = RectMaxHeight + 20 + 120;
            elipse3.Height = RectMaxHeight + 20 + 120;

            grid.Height = RectMaxHeight;

            rects.Add(rect1);
            rects.Add(rect2);
            rects.Add(rect3);
            rects.Add(rect4);
            rects.Add(rect5);
            rects.Add(rect6);

            AddStorys();

            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void AddStorys()
        {
            Random random = new Random();

            foreach (var rect in rects)
            {
                double height = 0;
                if (rects.IndexOf(rect) % 2 == 0)
                {
                    height = rect.Height;
                }

                CreateStory(rect, height, random);
            }
        }

        public void CreateStory(Rectangle rect, double initHeight, Random random)
        {
            rect.Width = RectWidth;

            DoubleAnimationUsingKeyFrames frames = new DoubleAnimationUsingKeyFrames();

            frames.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(0),
                Value = initHeight
            });

            int timeSeconds = 0;
            while (true)
            {
                timeSeconds += 200;

                int maxNum = 0;
                maxNum = random.Next(0, RectMaxHeight);

                int value = random.Next(0, maxNum);

                frames.KeyFrames.Add(new EasingDoubleKeyFrame()
                {
                    KeyTime = TimeSpan.FromMilliseconds(timeSeconds),
                    Value = value
                });

                if (frames.KeyFrames.Count == 50) break;
            }

            Storyboard.SetTarget(frames, rect);
            Storyboard.SetTargetProperty(frames, new PropertyPath("(FrameworkElement.Height)"));

            Storyboard story = new Storyboard() { Children = { frames } };
            story.AutoReverse = true;
            story.RepeatBehavior = RepeatBehavior.Forever;

            storys.Add(story);
        }

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsBusy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(MDFBusyControl), new PropertyMetadata(false, new PropertyChangedCallback((sender, e) =>
            {
                if ((bool)e.NewValue)
                {
                    (sender as MDFBusyControl).Visibility = Visibility.Visible;

                    foreach (var item in (sender as MDFBusyControl).storys)
                    {
                        item.Begin();
                    }
                }
                else
                {
                    if ((sender as MDFBusyControl).storys != null)
                    {
                        foreach (var item in (sender as MDFBusyControl).storys)
                        {
                            item.Stop();
                        }

                        (sender as MDFBusyControl).Visibility = Visibility.Collapsed;
                    }
                }

            })));



        public int RectWidth
        {
            get { return (int)GetValue(RectWidthProperty); }
            set { SetValue(RectWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RectWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RectWidthProperty =
            DependencyProperty.Register("RectWidth", typeof(int), typeof(MDFBusyControl), new PropertyMetadata(20));


        public int RectMaxHeight
        {
            get { return (int)GetValue(RectMaxHeightProperty); }
            set { SetValue(RectMaxHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RectMaxHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RectMaxHeightProperty =
            DependencyProperty.Register("RectMaxHeight", typeof(int), typeof(MDFBusyControl), new PropertyMetadata(200));

    }
}
