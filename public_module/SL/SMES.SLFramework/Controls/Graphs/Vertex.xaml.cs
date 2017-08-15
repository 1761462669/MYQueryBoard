using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SMES.Framework.Controls.Graphs
{
    public partial class Vertex : UserControl
    {
        #region DependencyProperty
        // Using a DependencyProperty as the backing store for PreEdges.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreEdgesProperty =
            DependencyProperty.Register("PreEdges", typeof(ObservableCollection<Edge>), typeof(Vertex), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for NextEdges.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NextEdgesProperty =
            DependencyProperty.Register("NextEdges", typeof(ObservableCollection<Edge>), typeof(Vertex), new PropertyMetadata(null));


        // Using a DependencyProperty as the backing store for ContentTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register("ContentTemplate", typeof(DataTemplate), typeof(Vertex), new PropertyMetadata(null));
        // Using a DependencyProperty as the backing store for Point.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointProperty =
            DependencyProperty.Register("Point", typeof(Point), typeof(Vertex), new PropertyMetadata(null));





        public Visibility DrawPannelVisible
        {
            get { return (Visibility)GetValue(DrawPannelVisibleProperty); }
            set { SetValue(DrawPannelVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DrawPannelVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DrawPannelVisibleProperty =
            DependencyProperty.Register("DrawPannelVisible", typeof(Visibility), typeof(Vertex), new PropertyMetadata(Visibility.Visible));





        #endregion

        #region feilds && Properties
        /// <summary>
        /// 前序边集合
        /// </summary>
        public ObservableCollection<Edge> PreEdges
        {
            get { return (ObservableCollection<Edge>)GetValue(PreEdgesProperty); }
            set { SetValue(PreEdgesProperty, value); }
        }

        /// <summary>
        /// 后续边集合
        /// </summary>
        public ObservableCollection<Edge> NextEdges
        {
            get { return (ObservableCollection<Edge>)GetValue(NextEdgesProperty); }
            set { SetValue(NextEdgesProperty, value); }
        }
        /// <summary>
        /// 显示内容模板
        /// </summary>
        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set
            {
                SetValue(ContentTemplateProperty, value);

                if (value != null)
                    this.content.ContentTemplate = value;
            }
        }
        /// <summary>
        /// 位置
        /// </summary>
        public Point Point
        {
            get { return (Point)GetValue(PointProperty); }
            set { SetValue(PointProperty, value); }
        }

        private Graph graph { get; set; }
        #endregion

        #region Constructs
        /// <summary>
        /// 构造函数
        /// </summary>
        public Vertex(Graph graph)
        {
            InitializeComponent();
            this.graph = graph;
            this.PreEdges = new ObservableCollection<Edge>();
            this.NextEdges = new ObservableCollection<Edge>();
        }
        public Vertex()
        {

        }
        #endregion

        private void btn_NewEdge_Click(object sender, RoutedEventArgs e)
        {
            this.graph.RaiseAddEdge(new AddEdgeEventArgs(this.DataContext));
        }

        private void bt_Line_Click(object sender, RoutedEventArgs e)
        {
            this.graph.IsDrawLine = true;
            // this.graph.Selected = this;
        }


        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_del_Click(object sender, RoutedEventArgs e)
        {
            this.graph.DeleteVetext(this);

        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            if (this.graph.SelectedVertex != this)
                VisualStateManager.GoToState(this, "Normal", true);

        }
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            if (this.graph.SelectedVertex != this)
                VisualStateManager.GoToState(this, "Move", true);

        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (this.graph.IsDrawLine && this.graph.SelectedVertex != null)
            {
                if (this.graph.SelectedVertex == this)
                {
                    this.graph.IsDrawLine = false;
                    return;
                }
                if (this.graph.IsHaveCiercle(this.graph.SelectedVertex, this))
                {
                    MessageBox.Show("组成了环，不能连线！");
                    this.graph.IsDrawLine = false;
                    return;

                }
                if (this.graph.IsExistRelation(this.graph.SelectedVertex, this))
                {
                    this.graph.IsDrawLine = false;
                    return;

                }



                this.graph.RaiseDrawLine(new DrawLineEventArgs() { PreData = this.graph.SelectedVertex.DataContext, NextData = this.DataContext });
                this.graph.IsDrawLine = false;

            }

            VisualStateManager.GoToState(this, "Selected", true);
            if (this.graph.SelectedVertex != null && this.graph.SelectedVertex != this)
                VisualStateManager.GoToState(this.graph.SelectedVertex, "Normal", true);
            this.graph.SelectedVertex = this;
        }

        private void border_Loaded(object sender, RoutedEventArgs e)
        {
            this.border.Height = this.graph.VertexSize.Height - 18;
        }
    }
}
