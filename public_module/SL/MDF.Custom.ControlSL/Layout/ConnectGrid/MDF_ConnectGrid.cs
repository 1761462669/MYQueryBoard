using MDF.Custom.ControlSL.Controls;
using MDF.Custom.ControlSL.Layout.ConnectGrid.Model;
using MDF.Custom.ControlSL.Tools;
using System;
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

namespace MDF.Custom.ControlSL.Layout.ConnectGrid
{
    [StyleTypedProperty(Property = "ConnectItemStyle", StyleTargetType = typeof(ConnectItem))]
    [StyleTypedProperty(Property = "LineStyle", StyleTargetType = typeof(MDF_Line))]
    public class MDF_ConnectGrid : Control, System.ComponentModel.INotifyPropertyChanged
    {
        public MDF_ConnectGrid()
        {
            this.DefaultStyleKey = typeof(MDF_ConnectGrid);
            ItemSource = new ObservableCollection<MDF_ConnectItemModel>();
            LineSource = new ObservableCollection<MDF_ConnectLineModel>();

            this.PropertyChanged += MDF_ConnectGrid_PropertyChanged;
        }

        private bool isShowFull;

        public bool IsShowFull
        {
            get { return isShowFull; }
            set
            {
                isShowFull = value;
                this.RaisePorpertyChanged("IsShowFull");
            }
        }


        void MDF_ConnectGrid_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsShowFull")
            {
                (sender as MDF_ConnectGrid).LoadLines();
            }
        }

        Grid gridMain;
        Canvas canvasLine;
        Dictionary<int, Grid> m_dicgrid = new Dictionary<int, Grid>();
        Dictionary<string, ConnectItem> m_dicitem = new Dictionary<string, ConnectItem>();

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.DelSelectedCmd = new RelayCommand(() =>
            {
                DeleteLine(SelectedLine);
            });

            gridMain = this.GetTemplateChild("gridMain") as Grid;
            canvasLine = this.GetTemplateChild("canvasLine") as Canvas;
            AddItems();
            if (canvasLine != null)
                canvasLine.SizeChanged += canvasLine_SizeChanged;

            this.SizeChanged += MDF_ConnectGrid_SizeChanged;
            this.LayoutUpdated += MDF_ConnectGrid_LayoutUpdated;   
            LoadLines();
        }

        void MDF_ConnectGrid_LayoutUpdated(object sender, EventArgs e)
        {
            RefreshAllLine();
        }

        void MDF_ConnectGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //RefreshAllLine();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.Delete && AllowDraw == true)
            {
                DeleteLine(SelectedLine);
            }
        }

        void canvasLine_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //RefreshAllLine();
        }

        public void RefreshAllLine()
        {
            foreach (FrameworkElement element in canvasLine.Children)
            {
                ComputeLineInfo(element as MDF_Line);
            }
        }

        #region 属性定义

        public ICommand DelSelectedCmd
        {
            get { return (ICommand)GetValue(DelSelectedComdProperty); }
            set { SetValue(DelSelectedComdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DelSelectedComd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DelSelectedComdProperty =
            DependencyProperty.Register("DelSelectedCmd", typeof(ICommand), typeof(MDF_ConnectGrid), new PropertyMetadata(default(ICommand)));


        public Style ConnectItemStyle
        {
            get { return (Style)GetValue(ConnectItemStyleProperty); }
            set { SetValue(ConnectItemStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ConnectItemStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConnectItemStyleProperty =
            DependencyProperty.Register("ConnectItemStyle", typeof(Style), typeof(MDF_ConnectGrid), new PropertyMetadata(null));

        public Style LineStyle
        {
            get { return (Style)GetValue(LineStyleProperty); }
            set { SetValue(LineStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineStyleProperty =
            DependencyProperty.Register("LineStyle", typeof(Style), typeof(MDF_ConnectGrid), new PropertyMetadata(null));

        public ObservableCollection<MDF_ConnectItemModel> ItemSource
        {
            get { return (ObservableCollection<MDF_ConnectItemModel>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(ObservableCollection<MDF_ConnectItemModel>), typeof(MDF_ConnectGrid), new PropertyMetadata(null, new PropertyChangedCallback((obj, arg) =>
            {
                (obj as MDF_ConnectGrid).AddItems();
            })));

        public ObservableCollection<MDF_ConnectLineModel> LineSource
        {
            get { return (ObservableCollection<MDF_ConnectLineModel>)GetValue(LineSourceProperty); }
            set { SetValue(LineSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineSourceProperty =
            DependencyProperty.Register("LineSource", typeof(ObservableCollection<MDF_ConnectLineModel>), typeof(MDF_ConnectGrid), new PropertyMetadata(null, new PropertyChangedCallback((obj, arg) =>
            {
                (obj as MDF_ConnectGrid).LoadLines();
            })));

        public IDrawLineCheck LineCheck
        {
            get { return (IDrawLineCheck)GetValue(LineCheckProperty); }
            set { SetValue(LineCheckProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineCheck.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineCheckProperty =
            DependencyProperty.Register("LineCheck", typeof(IDrawLineCheck), typeof(MDF_ConnectGrid), new PropertyMetadata(null));

        public MDF_ConnectItemModel SelectedItem
        {
            get { return (MDF_ConnectItemModel)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(MDF_ConnectItemModel), typeof(MDF_ConnectGrid), new PropertyMetadata(null,
                new PropertyChangedCallback((obj, arg) =>
                {
                    (obj as MDF_ConnectGrid).SetItemSelected(arg.NewValue as MDF_ConnectItemModel, arg.OldValue as MDF_ConnectItemModel);
                    (obj as MDF_ConnectGrid).ChangeSelectedDraw(arg.NewValue as MDF_ConnectItemModel, arg.OldValue as MDF_ConnectItemModel);

                })));

        public MDF_ConnectLineModel SelectedLine
        {
            get { return (MDF_ConnectLineModel)GetValue(SelectedLineProperty); }
            set { SetValue(SelectedLineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedLine.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedLineProperty =
            DependencyProperty.Register("SelectedLine", typeof(MDF_ConnectLineModel), typeof(MDF_ConnectGrid), new PropertyMetadata(null, new PropertyChangedCallback
                ((obj, arg) =>
                {
                    (obj as MDF_ConnectGrid).SetLineSelected(arg.NewValue as MDF_ConnectLineModel, arg.OldValue as MDF_ConnectLineModel);
                })));

        public bool AllowDraw
        {
            get { return (bool)GetValue(AllowDrawProperty); }
            set { SetValue(AllowDrawProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowDraw.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowDrawProperty =
            DependencyProperty.Register("AllowDraw", typeof(bool), typeof(MDF_ConnectGrid), new PropertyMetadata(false));

        #endregion

        private void ChangeSelectedDraw(MDF_ConnectItemModel to, MDF_ConnectItemModel from)
        {
            if (AllowDraw)
            {
                DrawLine(from, to);
            }
        }

        private void SetItemSelected(MDF_ConnectItemModel model, MDF_ConnectItemModel oldmodel)
        {
            if (oldmodel != null)
            {
                if (m_dicitem.ContainsKey(oldmodel.Key))
                {
                    m_dicitem[oldmodel.Key].Checked = false;
                }
            }
            if (model == null)
                return;

            if (!m_dicitem.ContainsKey(model.Key))
                return;

            m_dicitem[model.Key].Checked = true;
        }

        private void SetLineSelected(MDF_ConnectLineModel model, MDF_ConnectLineModel oldvalue)
        {
            if (oldvalue != null)
            {
                MDF_Line oldline = canvasLine.Children.FirstOrDefault(p => ((p as MDF_Line).DataContext as MDF_ConnectLineModel) == oldvalue) as MDF_Line;
                if (oldline != null)
                    oldline.Selected = false;
            }

            if (model == null)
            {
                return;
            }

            if (canvasLine == null)
                return;
            MDF_Line line = canvasLine.Children.FirstOrDefault(p => ((p as MDF_Line).DataContext as MDF_ConnectLineModel) == model) as MDF_Line;
            if (line == null)
                return;

            line.Selected = true;
        }

        private void AddItems()
        {

            if (ItemSource == null || gridMain == null)
                return;

            m_dicgrid.Clear();
            m_dicitem.Clear();
            gridMain.Children.Clear();
            gridMain.RowDefinitions.Clear();
            gridMain.ColumnDefinitions.Clear();

            //var v = ItemSource.OrderBy(p=>p.ColumnIndex);

            ReIndex(ItemSource);

            foreach (MDF_ConnectItemModel item in ItemSource)
            {
                AddItem(item);
            }            
            this.UpdateLayout();

            RefreshAllLine();
        }

        private void ReIndex(ObservableCollection<MDF_ConnectItemModel> items)
        {
            if (items == null || items.Count == 0)
                return;

            List<IGrouping<int, MDF_ConnectItemModel>> grouplist = items.GroupBy(p => p.ColumnIndex).OrderBy(p => p.Key).ToList();

            int row;
            for (int i = 0; i < grouplist.Count; i++)
            {
                //grouplist[i].k
                row = 0;
                var v = grouplist[i];
                foreach (MDF_ConnectItemModel item in v)
                {
                    item.RealColumnIndex = i;
                    item.RealRowIndex = row;
                    row++;
                }

            }
        }

        private void AddItem(MDF_ConnectItemModel item)
        {
            if (item == null)
                return;

            if (m_dicitem.ContainsKey(item.Key))
            {
                return;
            }

            Grid grd = CreateGrid(item);

            grd.RowDefinitions.Add(new RowDefinition());

            ConnectItem citem = new ConnectItem();
            citem.SetBinding(ConnectItem.StyleProperty, new Binding() { Source = this, Path = new PropertyPath("ConnectItemStyle") });
            citem.SetBinding(Grid.RowProperty, new Binding() { Source = item, Path = new PropertyPath("RealRowIndex") });

            citem.DataContext = item;

            if (citem.Height < 20 || double.IsNaN(citem.Height))
                citem.Height = 20;
            if (citem.Width < 20 || double.IsNaN(citem.Width))
                citem.Width = 20;

            m_dicitem.Add(item.Key, citem);
            grd.Children.Add(citem);

            citem.ItemChecked += citem_ItemChecked;
        }

        void citem_ItemChecked(object sender, RoutedEventArgs e)
        {
            SelectedItem = (sender as ConnectItem).DataContext as MDF_ConnectItemModel;
        }

        /// <summary>
        /// 根据下标添加Grid
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private Grid CreateGrid(MDF_ConnectItemModel item)
        {
            if (!m_dicgrid.ContainsKey(item.RealColumnIndex))
            {
                gridMain.ColumnDefinitions.Add(new ColumnDefinition());
                Grid grd = new Grid();
                grd.SetBinding(Grid.ColumnProperty, new Binding() { Source = item, Path = new PropertyPath("RealColumnIndex") });
                m_dicgrid.Add(item.RealColumnIndex, grd);
                gridMain.Children.Add(grd);
            }
            return m_dicgrid[item.RealColumnIndex];
        }


        #region 连接线

        private void LoadLines()
        {
            if (gridMain == null || canvasLine == null)
                return;

            canvasLine.Children.Clear();

            if (LineSource == null) return;

            if (m_dicitem.Count == 0) return;

            foreach (MDF_ConnectLineModel linemode in LineSource)
            {
                CreateLine(linemode);
            }

            RefreshAllLine();

            this.UpdateLayout();

        }

        private MDF_Line CreateLine(MDF_ConnectLineModel linemodel)
        {
            if (canvasLine == null)
                return null;

            if (LineSource == null)
                return null;

            if (m_dicitem.Where(p => p.Key == linemodel.From).Count() == 0)
            {
                return null;
            }

            if (m_dicitem.Where(p => p.Key == linemodel.To).Count() == 0)
            {
                return null;
            }

            ConnectItem from = m_dicitem[linemodel.From];
            ConnectItem to = m_dicitem[linemodel.To];

            if (from == null || to == null)
                return null;

            if (LineCheck != null)
            {
                if (!LineCheck.AddLineCheck(from.DataContext as MDF_ConnectItemModel, to.DataContext as MDF_ConnectItemModel))
                {
                    return null;
                }
            }

            //判断是否已存在，如果存在则执行创建线
            var v = canvasLine.Children.FirstOrDefault(p => ((p as MDF_Line).DataContext as MDF_ConnectLineModel).From == linemodel.From
                && ((p as MDF_Line).DataContext as MDF_ConnectLineModel).To == linemodel.To);

            if (v != null)
                return null;

            MDF_Line line = new MDF_Line();
            line.SetBinding(MDF_Line.StyleProperty, new Binding() { Source = this, Path = new PropertyPath("LineStyle") });
            line.DataContext = linemodel;
            canvasLine.Children.Add(line);
            ComputeLineInfo(line);
            line.LineSelected += line_LineSelected;

            return line;
        }

        void line_LineSelected(object sender, RoutedEventArgs e)
        {
            SelectedLine = ((sender as MDF_Line).DataContext) as MDF_ConnectLineModel;
        }

        //计算线位置
        private void ComputeLineInfo(MDF_Line line)
        {
            if (line == null)
                return;

            MDF_ConnectLineModel model = line.DataContext as MDF_ConnectLineModel;
            if (model == null)
                return;

            if (gridMain == null || canvasLine == null)
                return;

            if (m_dicitem.Count == 0) return;

            if (m_dicitem.Keys.Contains(model.From) && m_dicitem.Keys.Contains(model.To))
            {
                ConnectItem from = m_dicitem[model.From];
                ConnectItem to = m_dicitem[model.To];
                if (from == null || to == null)
                    return;

                //计算控件坐标点

                if (from.ActualHeight == 0) return;

                if (canvasLine.ActualHeight == 0)
                    return;

                List<Point> pfromlist = SLControlUnity.GetControlPointLeftRight(canvasLine, from);
                List<Point> ptolist = SLControlUnity.GetControlPointLeftRight(canvasLine, to);

                Tuple<Point, Point> nearpoint = SLControlUnity.ComplateNearPoint(pfromlist, ptolist);//得到最近两个点
                double len = SLControlUnity.Distance(nearpoint.Item1, nearpoint.Item2);//得到两点最近距离
                double angle = SLControlUnity.SlopeAngle(nearpoint.Item1, nearpoint.Item2);//计算两点倾斜角度

                Canvas.SetLeft(line, nearpoint.Item1.X);
                Canvas.SetTop(line, nearpoint.Item1.Y - line.ActualHeight / 2);
                line.Width = len;

                CompositeTransform transform = line.RenderTransform as CompositeTransform;
                if (transform == null)
                {
                    transform = new CompositeTransform();
                    line.RenderTransform = transform;
                }

                line.RenderTransformOrigin = new Point(0, 0.5);
                //transform.CenterX = 0;
                //transform.CenterY = 0.5;
                transform.Rotation = angle;
            }
        }

        /// <summary>
        /// 删除线
        /// </summary>
        /// <param name="model"></param>
        private void DeleteLine(MDF_ConnectLineModel model)
        {
            if (canvasLine == null)
                return;

            if (model == null)
                return;

            MDF_Line line = canvasLine.Children.FirstOrDefault(p => ((p as FrameworkElement).DataContext as MDF_ConnectLineModel) == model) as MDF_Line;

            if (line == null)
                return;

            if (LineCheck != null && !LineCheck.DeleckLine(model))
            {
                return;
            }

            line.LineSelected -= line_LineSelected;
            canvasLine.Children.Remove(line);

            LineSource.Remove(model);
        }

        private void DrawLine(MDF_ConnectItemModel from, MDF_ConnectItemModel to)
        {
            if (from == null || to == null)
                return;

            MDF_Line line = CreateLine(new MDF_ConnectLineModel() { From = from.Key, To = to.Key });
            if (line == null)
                return;
            ComputeLineInfo(line);
            if (LineSource == null)
            {
                LineSource = new ObservableCollection<MDF_ConnectLineModel>();
            }
            LineSource.Add(line.DataContext as MDF_ConnectLineModel);
            SelectedItem = null;
            this.UpdateLayout();

        }

        #endregion


        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public void RaisePorpertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(name));
            }
        }
    }
}
