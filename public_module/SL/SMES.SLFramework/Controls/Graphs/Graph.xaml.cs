using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public partial class Graph : UserControl
    {
        #region DependencyProperty
        // Using a DependencyProperty as the backing store for PrePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PrePathProperty =
            DependencyProperty.Register("PrePath", typeof(string), typeof(Graph), new PropertyMetadata("Pre"));

        // Using a DependencyProperty as the backing store for NextPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NextPathProperty =
            DependencyProperty.Register("NextPath", typeof(string), typeof(Graph), new PropertyMetadata("Next"));

        // Using a DependencyProperty as the backing store for DisplayPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayPathProperty =
            DependencyProperty.Register("DisplayPath", typeof(string), typeof(Graph), new PropertyMetadata("Name"));



        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(IEnumerable), typeof(Graph), new PropertyMetadata(
                new PropertyChangedCallback(OnPropertyChangedCallback)
                ));



        // Using a DependencyProperty as the backing store for SelectedVertex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedVertexProperty =
            DependencyProperty.Register("SelectedVertex", typeof(Vertex), typeof(Graph), new PropertyMetadata(new PropertyChangedCallback(OnSelectedVertextPropertyChangedCallback)));





        // Using a DependencyProperty as the backing store for SelectedEdge.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedEdgeProperty =
            DependencyProperty.Register("SelectedEdge", typeof(Edge), typeof(Graph), new PropertyMetadata(null));





        // Using a DependencyProperty as the backing store for VertexSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VertexSizeProperty =
            DependencyProperty.Register("VertexSize", typeof(Size), typeof(Graph), new PropertyMetadata(new Size(150, 50)));


        // Using a DependencyProperty as the backing store for VertexTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VertexTemplateProperty =
            DependencyProperty.Register("VertexTemplate", typeof(DataTemplate), typeof(Graph), new PropertyMetadata(null));






        // Using a DependencyProperty as the backing store for LineWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineWidthProperty =
            DependencyProperty.Register("LineWidth", typeof(double), typeof(Graph), new PropertyMetadata(100.0));







        // Using a DependencyProperty as the backing store for CanDraw.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanDrawProperty =
            DependencyProperty.Register("CanDraw", typeof(bool), typeof(Graph), new PropertyMetadata(true));






        public double VInterval
        {
            get { return (double)GetValue(VIntervalProperty); }
            set { SetValue(VIntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VInterval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VIntervalProperty =
            DependencyProperty.Register("VInterval", typeof(double), typeof(Graph), new PropertyMetadata(0.0));






        #endregion

        #region Static Methods


        private static void OnSelectedVertextPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                Graph graph = d as Graph;
                if (graph.VertexSelectionChanged != null)
                {

                }

            }
        }

        /// <summary>
        /// 当属性变化时，重新绘图
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var graph = d as Graph;
            var oldData = e.OldValue as INotifyCollectionChanged;
            var newData = e.NewValue as INotifyCollectionChanged;
            NotifyCollectionChangedEventHandler handle = (colection, arg) =>
            {
                graph.ProcessDataAndDraw();
            };
            if (oldData != null)
                oldData.CollectionChanged -= handle;
            if (newData != null)
                newData.CollectionChanged += handle;
            graph.ProcessDataAndDraw();

        }

        #endregion

        #region feilds && Properties
        public bool CanDraw
        {
            get { return (bool)GetValue(CanDrawProperty); }
            set { SetValue(CanDrawProperty, value); }
        }

        private Dictionary<object, Vertex> vertexs = new Dictionary<object, Vertex>();//图的顶点
        private Dictionary<object, Edge> edges = new Dictionary<object, Edge>();//图的边

        private bool isDrawLine = false;
        public bool IsDrawLine
        {
            get { return this.isDrawLine; }
            set
            {
                this.isDrawLine = value;
                this.assistantLine.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        //public Vertex Selected { get; set; }

        public Edge SelectedEdge
        {
            get { return (Edge)GetValue(SelectedEdgeProperty); }
            set
            {
                SetValue(SelectedEdgeProperty, value);
                if (this.EdgeSelectionChanged != null)
                {
                    this.EdgeSelectionChanged(this, new SelectionEdgeArgs(value));
                }
            }
        }
        /// <summary>
        /// 选中的顶点
        /// </summary>
        public Vertex SelectedVertex
        {
            get { return (Vertex)GetValue(SelectedVertexProperty); }
            set
            {
                SetValue(SelectedVertexProperty, value);
                if (this.VertexSelectionChanged != null)
                {
                    this.VertexSelectionChanged(this, new SelectionVertexArgs(value));
                }
            }
        }





        /// <summary>
        /// 前序节点路径
        /// </summary>
        public string PrePath
        {
            get { return (string)GetValue(PrePathProperty); }
            set { SetValue(PrePathProperty, value); }
        }
        /// <summary>
        /// 后续节点路径
        /// </summary>
        public string NextPath
        {
            get { return (string)GetValue(NextPathProperty); }
            set { SetValue(NextPathProperty, value); }
        }
        /// <summary>
        /// 名称展示路径
        /// </summary>
        public string DisplayPath
        {
            get { return (string)GetValue(DisplayPathProperty); }
            set { SetValue(DisplayPathProperty, value); }
        }

        /// <summary>
        /// 关系集合
        /// </summary>
        public IEnumerable ItemSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        /// <summary>
        /// 图顶点模板
        /// </summary>
        public DataTemplate VertexTemplate
        {
            get { return (DataTemplate)GetValue(VertexTemplateProperty); }
            set { SetValue(VertexTemplateProperty, value); }
        }

        /// <summary>
        /// 顶点大小
        /// </summary>
        public Size VertexSize
        {
            get { return (Size)GetValue(VertexSizeProperty); }
            set { SetValue(VertexSizeProperty, value); }
        }

        public double LineWidth
        {
            get { return (double)GetValue(LineWidthProperty); }
            set { SetValue(LineWidthProperty, value); }
        }

        #endregion

        #region events
        /// <summary>
        /// 添加新边时的事件
        /// </summary>
        public event EventHandler<AddEdgeEventArgs> AddEdgeEvent;
        /// <summary>
        /// 画线的事件
        /// </summary>
        public event EventHandler<DrawLineEventArgs> DrawLineEvent;

        /// <summary>
        /// 节点选择事件
        /// </summary>
        public event EventHandler<SelectionVertexArgs> VertexSelectionChanged;
        /// <summary>
        /// 线选择事件
        /// </summary>
        public event EventHandler<SelectionEdgeArgs> EdgeSelectionChanged;




        #endregion

        #region Constructs
        public Graph()
        {
            this.PrePath = "Pre";
            this.NextPath = "Next";
            this.DisplayPath = "Name";
            InitializeComponent();
            //this.pannel.Children.Add(this.assistantLine);
            //this.Loaded += Graph_Loaded;

            //this.SizeChanged += Graph_SizeChanged;



        }

        void Graph_Loaded(object sender, RoutedEventArgs e)
        {
            this.ProcessDataAndDraw();
        }
        #endregion



        #region Methods

        //public void OnVertexSelectionChanged()


        #region 初始化数据并绘制图


        public void RaiseSelectedVertextChanged()
        {

            if (this.VertexSelectionChanged != null)
                this.VertexSelectionChanged(this, new SelectionVertexArgs(this.SelectedVertex));

        }


        protected override Size MeasureOverride(Size availableSize)
        {
            var size = base.MeasureOverride(availableSize);
            //this.pannel.Width = size.Width;
            //this.pannel.Height = size.Height;
            return base.MeasureOverride(availableSize);
        }

        /// <summary>
        /// 根据关系表初始化图数据，并绘图
        /// </summary>
        private void ProcessDataAndDraw()
        {
            this.isDrawLine = false;
            this.SelectedVertex = null;
            this.SelectedEdge = null;
            this.vertexs.Clear();
            this.edges.Clear();
            this.pannel.Children.Clear();
            if (this.ItemSource == null)
                return;
            //清除数据

            var list = new List<Vertex>();
            foreach (var item in this.ItemSource)
            {
                Edge edt = new Edge(this);
                this.edges.Add(item, edt);
                edt.DataContext = item;
                edt.SetBinding(Edge.PreDataProperty, new Binding(this.PrePath));
                edt.SetBinding(Edge.NextDataProperty, new Binding(this.NextPath));
                Vertex pre = null;
                Vertex next = null;
                if (edt.PreData != null)
                {
                    if (!this.vertexs.ContainsKey(edt.PreData))
                    {
                        pre = new Vertex(this);
                        if (this.VertexTemplate != null)
                            pre.ContentTemplate = this.VertexTemplate;
                        pre.DataContext = edt.PreData;

                        pre.Width = this.VertexSize.Width;
                        if (this.VertexSize.Height > 0)
                            pre.Height = this.VertexSize.Height;
                        pre.DrawPannelVisible = this.CanDraw ? Visibility.Visible : Visibility.Collapsed;
                        this.vertexs.Add(pre.DataContext, pre);
                    }
                    else
                        pre = this.vertexs[edt.PreData];
                }
                if (edt.NextData != null)
                {
                    if (!this.vertexs.ContainsKey(edt.NextData))
                    {
                        next = new Vertex(this);
                        if (this.VertexTemplate != null)
                            next.ContentTemplate = this.VertexTemplate;
                        next.DataContext = edt.NextData;
                        next.Width = this.VertexSize.Width;
                        if (this.VertexSize.Height > 0)
                            next.Height = this.VertexSize.Height;
                        next.DrawPannelVisible = this.CanDraw ? Visibility.Visible : Visibility.Collapsed;
                        this.vertexs.Add(next.DataContext, next);
                    }
                    else
                        next = this.vertexs[edt.NextData];
                }
                if (pre != null && edt.NextData != null)
                    pre.NextEdges.Add(edt);
                if (next != null && edt.PreData != null)
                    next.PreEdges.Add(edt);
                edt.Pre = pre;
                edt.Next = next;
                if (edt.PreData == null || edt.NextData == null)
                    this.edges.Remove(edt.DataContext);
            }


            // Dictionary<int, int> dic = new Dictionary<int, int>();


            //this.pannel.Width = 100;
            //this.pannel.Height = 100;

            //计算高度与宽度
            this.Draw();
        }



        private void IntialDrawPannelSize()
        {
            //计算画板的长度与高度
            //this.pannel.Width = this.vertexs.Count() * this.VertexSize.Width + this.LineWidth * this.vertexs.Count();
            //this.si




        }

        /// <summary>
        /// 判断是否有环
        /// </summary>
        /// <param name="pre"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public bool IsHaveCiercle(Vertex pre, Vertex next)
        {
            if (pre == null || next == null)
                return false;
            if (next == pre)
                return true;

            if (next.NextEdges != null && next.NextEdges.Count > 0)
            {
                foreach (var item in next.NextEdges)
                {
                    if (IsHaveCiercle(pre, item.Next))
                        return true;
                }
            }
            return false;

        }


        public bool IsExistRelation(Vertex pre, Vertex next)
        {
            if (this.edges == null)
                return false;
            var edg = this.edges.Values.Where(c => c.Pre == pre && c.Next == next)
                 .FirstOrDefault();
            return edg != null;

        }





        //public int MaxHeight()
        //{
        //    return 0;
        //}



        /// <summary>
        /// 画图
        /// </summary>
        public void Draw()
        {


            this.pannel.Children.Clear();
            //if (this.pannel.ActualWidth == Double.NaN || this.pannel.ActualHeight == 0)
            //    return;


            //var firsts = this.edges.Values.Where(c => c.PreData == null).Select(c => c.Pre).Distinct().ToList();
            var firsts = this.vertexs.Values.Where(c => c.PreEdges == null || c.PreEdges.Count == 0).ToList();

            var startPoint = new Point(5, 5);



            //if (firsts == null || firsts.Count == 0)
            //    return;
            var x = startPoint.X;
            var y0 = startPoint.Y;

            //var y0 = this.pannel.ActualHeight / 2 - 15 - (firsts.Count - 1) * this.VertexSize.Height / 2;


            var index = 0;
            //var point = new Point(x, y0);
            //DrawVertex(point, firsts.FirstOrDefault());
            foreach (var item in firsts)
            {
                if (index == 0)
                {
                    this.SelectedVertex = item;
                    VisualStateManager.GoToState(item, "Selected", true);
                }
                var point = new Point(x, y0);
                DrawVertex(point, item);
                index++;
                if (this.pannel.Children.Count > 0)
                {
                    y0 = this.pannel.Children.Max(c => Canvas.GetTop(c)) + this.VertexSize.Height + this.VInterval;
                }
                else
                {
                    y0 += this.VertexSize.Height + this.VInterval;
                }
                //y0 += this.VertexSize.Height+this.VInterval;

            }
            //var maxH = this.vertexs.Values.Max(c => Canvas.GetLeft(c));
            var maxv = y0 + this.VertexSize.Height + 100;

            var vers = this.vertexs.Values.Where(c => this.pannel.Children.Contains(c));
            if (vers.Count() > 0)
                maxv = vers.Max(c => Canvas.GetTop(c));
            var intiaX = 0.0;
            foreach (var item in this.vertexs.Values)
            {
                if (!this.pannel.Children.Contains(item))
                {

                    DrawVertex(new Point(intiaX, maxv), item);

                }
            }
            var width = 100.0;
            //var minHeight = 0.0;
            var height = 100.0;

            if (this.pannel.Children.Count > 0)
            {
                var minHeight = this.pannel.Children.Min(c => Canvas.GetTop(c));
                var maxHeight = this.pannel.Children.Max(c => Canvas.GetTop(c) + this.VertexSize.Height);
                var h = maxHeight - minHeight;
                foreach (UIElement item in this.pannel.Children)
                {
                    Canvas.SetTop(item, Canvas.GetTop(item) - minHeight + 5);
                }

            }




            DrawLine();

            if (this.pannel.Children.Count > 0)
            {
                width = this.pannel.Children.Max(c => Canvas.GetLeft(c)) + this.VertexSize.Width + 50;
                // minHeight += this.pannel.Children.Min(c => Canvas.GetTop(c));
                height = this.pannel.Children.Max(c => Canvas.GetTop(c)) + this.VertexSize.Height;
            }


            this.pannel.Width = width;
            this.pannel.Height = height;

        }

        //先画点
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startPoint">起始点</param>
        /// <param name="vert">顶点</param>
        private void DrawVertex(Point startPoint, Vertex vert)
        {
            if (!this.pannel.Children.Contains(vert))
            {
                this.pannel.Children.Add(vert);
                //vert.UpdateLayout();
                Canvas.SetLeft(vert, startPoint.X);
                if (vert.PreEdges == null || vert.PreEdges.Count < 2)
                    Canvas.SetTop(vert, startPoint.Y - 15);
                else
                    Canvas.SetTop(vert, startPoint.Y + (vert.PreEdges.Count - 1) * this.VertexSize.Height / 2 - 15);
            }

            if (vert.NextEdges != null && vert.NextEdges.Count > 0)
            {
                var x0 = Canvas.GetLeft(vert) + this.VertexSize.Width + this.LineWidth;
                var y0 = Canvas.GetTop(vert) + 15 - this.VertexSize.Height / 2 * (vert.NextEdges.Count - 1);
                int index = 0;
                //Edge old = null;
                foreach (var edg in vert.NextEdges)
                {

                    if (edg.Next != null)
                        DrawVertex(new Point(x0, y0), edg.Next);
                    index++;


                    y0 += this.VertexSize.Height;

                }
            }
        }
        /// <summary>
        /// 画线
        /// </summary>
        private void DrawLine()
        {

            foreach (var edg in this.edges.Values)
            {
                if (edg.Pre != null && edg.Next != null)
                {
                    var x1 = Canvas.GetLeft(edg.Pre);
                    var y1 = Canvas.GetTop(edg.Pre);
                    var x2 = Canvas.GetLeft(edg.Next);
                    var y2 = Canvas.GetTop(edg.Next);
                    x1 += edg.Pre.Width;
                    //y1 += edg.Pre.Height / 2;
                    //y2 += edg.Next.Height / 2;
                    y1 += (this.VertexSize.Height - 18) / 2;
                    y2 += (this.VertexSize.Height - 18) / 2;
                    Canvas.SetLeft(edg, x1);
                    Canvas.SetTop(edg, y1);
                    edg.Width = Math.Abs(x1 - x2);
                    edg.Height = Math.Abs(y1 - y2);
                    if (y1 > y2)
                    {
                        edg.RenderTransform = new CompositeTransform() { CenterX = 0, CenterY = 0, ScaleY = -1 };
                    }
                    if (!this.pannel.Children.Contains(edg))
                        this.pannel.Children.Add(edg);

                }
            }
        }
        #endregion

        public void RaiseAddEdge(AddEdgeEventArgs args)
        {
            if (this.AddEdgeEvent != null)
                this.AddEdgeEvent(this, args);
        }

        public void RaiseDrawLine(DrawLineEventArgs args)
        {
            if (this.DrawLineEvent != null)
                this.DrawLineEvent(this, args);
        }

        public void DeleteVetext(Vertex vertex)
        {
            if (this.ItemSource == null || vertex == null)
                return;
            Edge edt = new Edge();
            edt.SetBinding(Edge.PreDataProperty, new Binding(this.PrePath));
            edt.SetBinding(Edge.NextDataProperty, new Binding(this.NextPath));
            var delete = new List<object>();
            
            foreach (var item in this.ItemSource)
            {
                edt.DataContext = item;
                if(edt.PreData==vertex.DataContext|| edt.NextData==vertex.DataContext)
                {
                    delete.Add(item);
                }
            }
            foreach (var item in delete)
            {
                this.Remove(item);
            }
            


        }


        private void Remove(object item)
        {
            var ds = this.ItemSource as IList;
            if (ds != null)
            {
                ds.Remove(item);

            }
            else
            {
                var pd = this.ItemSource as PagedCollectionView;
                if (pd != null)
                {
                    pd.Remove(item);
                }
            }

        }


        #endregion

        void Graph_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //this.PrePath = "Pre";
            //this.NextPath = "Next";
            //this.DisplayPath = "Name";
            //this.ProcessDataAndDraw();
        }

        //private Line assistantLine = new Line() { StrokeThickness = 1, Stroke = new SolidColorBrush(Colors.Blue) };

        private void pannel_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.IsDrawLine && this.SelectedVertex != null)
            {
                var p0 = new Point(Canvas.GetLeft(this.SelectedVertex) + this.VertexSize.Width, Canvas.GetTop(this.SelectedVertex) + 15);
                var p1 = e.GetPosition(this.pannel);
                assistantLine.X1 = p0.X;
                assistantLine.Y1 = p0.Y;
                assistantLine.X2 = p1.X - 5;
                assistantLine.Y2 = p1.Y - 5;

                if (!this.pannel.Children.Contains(this.assistantLine))
                {
                    this.pannel.Children.Add(this.assistantLine);
                }



            }
        }

        private void LayoutRoot_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.isDrawLine = false;
            this.SelectedVertex = null;
        }
    }
}
