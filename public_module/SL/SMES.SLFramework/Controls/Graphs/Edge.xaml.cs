using Microsoft.Expression.Media;
using System;
using System.Collections;
using System.Collections.Generic;
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
    public partial class Edge : UserControl
    {
        private Graph graph;

        #region DependencyPropertys
        // Using a DependencyProperty as the backing store for Pre.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreProperty =
            DependencyProperty.Register("Pre", typeof(Vertex), typeof(Edge), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for Next.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NextProperty =
            DependencyProperty.Register("Next", typeof(Vertex), typeof(Edge), new PropertyMetadata(null));


        // Using a DependencyProperty as the backing store for PreData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreDataProperty =
            DependencyProperty.Register("PreData", typeof(object), typeof(Edge), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for NextData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NextDataProperty =
            DependencyProperty.Register("NextData", typeof(object), typeof(Edge), new PropertyMetadata(null));











        public CornerType StartCorner
        {
            get { return (CornerType)GetValue(StartCornerProperty); }
            set { SetValue(StartCornerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartCorner.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartCornerProperty =
            DependencyProperty.Register("StartCorner", typeof(CornerType), typeof(Edge), new PropertyMetadata(CornerType.TopLeft));





        #endregion


        #region feilds && Properties
        public object PreData
        {
            get { return (object)GetValue(PreDataProperty); }
            set { SetValue(PreDataProperty, value); }
        }

        public object NextData
        {
            get { return (object)GetValue(NextDataProperty); }
            set { SetValue(NextDataProperty, value); }
        }

        /// <summary>
        /// 前序顶点
        /// </summary>
        public Vertex Pre
        {
            get { return (Vertex)GetValue(PreProperty); }
            set { SetValue(PreProperty, value); }
        }
        /// <summary>
        /// 后续顶点
        /// </summary>

        public Vertex Next
        {
            get { return (Vertex)GetValue(NextProperty); }
            set { SetValue(NextProperty, value); }
        }

        #endregion
        public Edge()
        {
            InitializeComponent();


        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Edge(Graph graph)
        {
            InitializeComponent();
            this.graph = graph;
            this.BindMenu();


        }
        private void BindMenu()
        {
            ContextMenu cm = new ContextMenu();//新建右键菜单
            MenuItem mi = new MenuItem();//新建右键菜单项
            mi.Header = "删除";
            mi.Click += (sender, e) =>
            {
                var list = this.graph.ItemSource as IList;
                if (list != null)
                    list.Remove(this.DataContext);
                else
                {
                    var pc = this.graph.ItemSource as PagedCollectionView;
                    if (pc != null)
                        pc.Remove(this.DataContext);
                }

            };//为菜单项注册事件      
            cm.Items.Add(mi);
            ContextMenuService.SetContextMenu(lineArrow, cm);//为控件绑定右键菜单
        }


        private void PART_Path_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.graph.SelectedEdge != this)
                VisualStateManager.GoToState(this, "Move", true);
        }

        private void PART_Path_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void PART_Path_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.graph.SelectedEdge != this)
                VisualStateManager.GoToState(this, "Normal", true);
        }

        private void PART_Path_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            VisualStateManager.GoToState(this, "Selected", true);
            if (this.graph.SelectedEdge != null && this.graph.SelectedEdge != this)
                VisualStateManager.GoToState(this.graph.SelectedEdge, "Normal", true);
            this.graph.SelectedEdge = this;

        }
    }
}
