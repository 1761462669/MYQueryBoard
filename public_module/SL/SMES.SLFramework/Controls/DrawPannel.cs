using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SMES.Framework.Controls
{
    [TemplatePart(Name = "canvas", Type = typeof(Canvas))]
    public class DrawPannel : Control
    {
        #region feilds && Properties
        private Panel canvas = null;



        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(DrawPannel), new PropertyMetadata(new PropertyChangedCallback(OnPropertyChangedCallback)));


        private static void OnPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = d as DrawPannel;
            box.Intial();
        }

        public IEnumerable DataSource
        {
            get { return (IEnumerable)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(IEnumerable), typeof(DrawPannel), new PropertyMetadata(new PropertyChangedCallback(OnPropertyChangedCallback)));


        #endregion
        public DrawPannel()
        {
            this.DefaultStyleKey = typeof(DrawPannel);
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.canvas = this.GetTemplateChild("canvas") as Panel;
            this.Intial();
        }
        protected override Size ArrangeOverride(Size finalSize)
        {


            return base.ArrangeOverride(finalSize);

        }
        protected override Size MeasureOverride(Size availableSize)
        {
            return base.MeasureOverride(availableSize);
        }



        internal void Intial()
        {
            if (this.canvas == null)
                return;
            this.canvas.Children.Clear();

            var width = this.canvas.ActualWidth;
            var height = this.canvas.ActualHeight;
            int i = 0;
            if (this.DataSource != null)
            {
               
                int count = (this.DataSource as IList).Count;
                if (this.canvas is Grid)
                {
                    var grid = this.canvas as Grid;
                    grid.ColumnDefinitions.Clear();
                    grid.RowDefinitions.Clear();

                    for (int j = 0; j < 4; j++)
                    {
                        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                    }
                    for (int k = 0; k < count / 4 + 1; k++)
                    {
                        grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                    }

                }
                foreach (var item in this.DataSource)
                {

                    ContentPresenter prensent = new ContentPresenter();
                    prensent.MouseEnter += (s, e) =>
                        {
                            var cp = s as ContentPresenter;
                            
                        };

                    prensent.Content = item;
                    if (this.ItemTemplate != null)
                        prensent.ContentTemplate = this.ItemTemplate;
                    canvas.Children.Add(prensent);
                    if (canvas is Grid)
                    {
                        var grid = canvas as Grid;
                        Grid.SetRow(prensent, i / 4);
                        Grid.SetColumn(prensent, i % 4);

                    }
                    else
                    {
                        Canvas.SetLeft(prensent, i * 30);
                        Canvas.SetTop(prensent, i * 15);
                    }

                    i++;

                }
            }



        }




    }


    public class ModelPositionWrapper
    {
        /// <summary>
        /// 位置
        /// </summary>
        Point Position;
        /// <summary>
        /// 大小
        /// </summary>
        Size Size;
    }


    public class RelationWrapper
    {

        ModelPositionWrapper Start;
        ModelPositionWrapper End;
    }


    public class AssociationModel
    {


    }



}
