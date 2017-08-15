using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class MDFChildWindow : ContentControl
    {
        public MDFChildWindow()
        {
            this.DefaultStyleKey = typeof(MDFChildWindow);
        }

        #region 成员变量
        Storyboard Storyboard_Open, Storyboard_Close;
        Rectangle Overlay;
        Button CloseButton;
        #endregion

        #region 事件定义
        public event RoutedEventHandler CloseEvent;
        public event RoutedEventHandler ToolButtonClick;
        public event RoutedEventHandler WindowShowed;
        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Storyboard_Open = this.GetTemplateChild("Storyboard_Open") as Storyboard;

            Storyboard_Close = this.GetTemplateChild("Storyboard_Close") as Storyboard;

            CloseButton = GetTemplateChild("CloseButton") as Button;

            Overlay = GetTemplateChild("Overlay") as Rectangle;

            this.ButtonCommand = new RelayCommand<object>(ButtonClick);

            SetChildWindowState();
            initEvent();
        }

        #region 加载事件
        public void initEvent()
        {
            if (Storyboard_Close != null)
                Storyboard_Close.Completed += Storyboard_Close_Completed;

            if (CloseButton != null)
                CloseButton.Click += new RoutedEventHandler(CloseButton_Click);

            if (Overlay != null)
                Overlay.MouseLeftButtonDown += Overlay_MouseLeftButtonDown;

            if (Storyboard_Open != null)
                Storyboard_Open.Completed += Storyboard_Open_Completed;
        }

        void Storyboard_Open_Completed(object sender, EventArgs e)
        {
            if (WindowShowed != null)
                WindowShowed(this, new RoutedEventArgs());
        }

        public void CloseWindow()
        {
            Shown = false;
        }

        void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Shown = false;
        }

        void Overlay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ClickOverlayClose)
            {
                Shown = false;
            }
        }

        void Storyboard_Close_Completed(object sender, EventArgs e)
        {
            this.Focus();
            if (CloseEvent != null)
                CloseEvent(this, new RoutedEventArgs());
        }

        #endregion


        public static readonly DependencyProperty CloseCommandProperty = DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(MDFChildWindow),
           new PropertyMetadata(default(ICommand)));
        public ICommand CloseCommand
        {
            get
            {
                return (ICommand)GetValue(CloseCommandProperty);
            }
            set
            {
                SetValue(CloseCommandProperty, value);
            }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(MDFChildWindow),
            new PropertyMetadata(default(string)));

        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public static readonly DependencyProperty WindowHeightProperty = DependencyProperty.Register("WindowHeight", typeof(double), typeof(MDFChildWindow),
            new PropertyMetadata(double.NaN));

        public double WindowHeight
        {
            get
            {
                return (double)GetValue(WindowHeightProperty);
            }
            set
            {
                SetValue(WindowHeightProperty, value);
            }
        }

        public static readonly DependencyProperty WindowWidthProperty = DependencyProperty.Register("WindowWidth", typeof(double), typeof(MDFChildWindow),
            new PropertyMetadata(double.NaN));

        public double WindowWidth
        {
            get
            {
                return (double)GetValue(WindowWidthProperty);
            }
            set
            {
                SetValue(WindowWidthProperty, value);
            }
        }

        public bool Shown
        {
            get
            {
                return (bool)GetValue(ShownProperty);
            }
            set
            {
                SetValue(ShownProperty, value);
            }
        }

        public static readonly DependencyProperty ShownProperty = DependencyProperty.Register("Shown", typeof(bool), typeof(MDFChildWindow),
            new PropertyMetadata(false, new PropertyChangedCallback((sender, arg) =>
            {
                (sender as MDFChildWindow).SetChildWindowState();
            })));

        public object WindowParameter
        {
            get
            {
                return GetValue(WindowParameterProperty);
            }
            set
            {
                SetValue(WindowParameterProperty, value);
            }
        }
        public static readonly DependencyProperty WindowParameterProperty = DependencyProperty.Register("WindowParameter", typeof(object),
            typeof(MDFChildWindow), new PropertyMetadata(default(object)));


        #region 按钮列表

        public ICommand ButtonCommand
        {
            get { return (ICommand)GetValue(ButtonCommandProperty); }
            set { SetValue(ButtonCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonCommandProperty =
            DependencyProperty.Register("ButtonCommand", typeof(ICommand), typeof(MDFChildWindow), new PropertyMetadata(default(ICommand)));

        private void ButtonClick(object par)
        {
            ButtonModel bntmodel = par as ButtonModel;
            if (ToolButtonClick != null)
            {
                ToolButtonClick(par, new RoutedEventArgs());
            }
            if (bntmodel != null && bntmodel.Command != null)
            {
                bntmodel.Command.Execute(bntmodel.CommandParameter);
            }
            if (bntmodel != null && bntmodel.BntAction != null)
            {
                bntmodel.BntAction.Invoke();
            }
            if (bntmodel != null)
            {
                if (bntmodel.DefaultClose)
                    this.Shown = false;
            }

        }


        public ObservableCollection<ButtonModel> Buttons
        {
            get { return (ObservableCollection<ButtonModel>)GetValue(ButtonsProperty); }
            set { SetValue(ButtonsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Buttons.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsProperty =
            DependencyProperty.Register("Buttons",
            typeof(ObservableCollection<ButtonModel>),
            typeof(MDFChildWindow),
            new PropertyMetadata(default(ObservableCollection<ButtonModel>)));

        #endregion

        private void SetChildWindowState()
        {
            if (Shown)
                Show();
            else
                Close();

        }

        private void Show()
        {
            if (Storyboard_Open != null)
                Storyboard_Open.Begin();
        }

        private void Close()
        {
            this.Storyboard_Close.Begin();
        }

        public bool ClickOverlayClose
        { get; set; }

    }

    #region Command 后面要移动位置（MDF.MES.CommonSL）

    //MDF.MES.CommonSL.Command
    public class RelayCommand : ICommand
    {
        public Action _execute;
        public Func<bool> _canExcecute;
        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            canExecute = _canExcecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExcecute == null)
                return true;

            bool flag = _canExcecute.Invoke();
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
            return flag;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (_execute == null)
                return;
            _execute.Invoke();
        }
    }

    public class RelayCommand<T> : ICommand
    {
        Action<T> _execute;
        Func<T, bool> _canExcecute;

        public RelayCommand(Action<T> execute)
        {
            _execute = execute;
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExcecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (_execute == null)
                return;

            T value = default(T);
            try
            {
                value = (T)parameter;
            }
            catch { }
            _execute.Invoke(value);
        }

        public bool CanExecute(object parameter)
        {
            if (_canExcecute == null)
                return true;

            T value = default(T);
            try
            {
                value = (T)parameter;
            }
            catch { }

            bool flag = _canExcecute.Invoke(value);
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());

            return flag;
        }
    }

    //MDF.MES.CommonSL.Models
    public class ButtonModel : NotificationObject
    {
        private string _text;
        /// <summary>
        /// 文本
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                this.RaisePropertyChanged("Text");
            }
        }
        private bool _IsEnabled = true;
        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                _IsEnabled = value;
                this.RaisePropertyChanged("IsEnabled");
            }
        }
        private Visibility _visbility = Visibility.Visible;
        /// <summary>
        /// 是否显示
        /// </summary>
        public Visibility Visbility
        {
            get { return _visbility; }
            set
            {
                _visbility = value;
                this.RaisePropertyChanged("Visbility");
            }
        }
        private ICommand _command;
        /// <summary>
        /// Command命令
        /// </summary>
        public ICommand Command
        {
            get { return _command; }
            set
            {
                _command = value;
                this.RaisePropertyChanged("Command");
            }
        }

        private object _commandParameter;
        /// <summary>
        /// 命令参数
        /// </summary>
        public object CommandParameter
        {
            get { return _commandParameter; }
            set
            {
                _commandParameter = value;
                this.RaisePropertyChanged("CommandParameter");
            }
        }

        private ImageSource _image;
        public ImageSource Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                this.RaisePropertyChanged("Image");
            }
        }

        private bool _defaultClose = false;
        /// <summary>
        /// 默认是否关闭
        /// </summary>
        public bool DefaultClose
        {
            get
            {
                return _defaultClose;
            }
            set
            {
                _defaultClose = value;
            }
        }

        private Action _bntClick;

        public Action BntAction
        {
            get { return _bntClick; }
            set { _bntClick = value; }
        }

    }

    public class NotificationObject : INotifyPropertyChanged, INotifyDataErrorInfo
    {

        #region INotifyPropertyChanged 成员

        public virtual event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region INotifyDataErrorInfo 成员

        /// <summary>
        /// 存放错误信息，一个Property可能对应多个错误信息
        /// </summary>
        private Dictionary<string, string> errors = new Dictionary<string, string>();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// 获取错误信息列表
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <returns>错误信息列表</returns>
        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return null;
            else if (errors.ContainsKey(propertyName))
                return new List<string>() { errors[propertyName] };
            else
                return null;
        }

        /// <summary>
        /// 是否有错误信息
        /// </summary>
        public bool HasErrors
        {
            get { return errors.Count > 0; }
        }

        // <summary>
        // 设置属性的错误信息
        // </summary>
        // <param name="propertyName">属性名称</param>
        // <param name="propertyErrors">错误信息列表</param>
        //protected void SetErrors(string propertyName, List<string> propertyErrors)
        //{
        //    errors.Remove(propertyName);
        //    errors.Add(propertyName, propertyErrors);
        //    if (ErrorsChanged != null)
        //        ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        //}

        protected void AddErrors(string propertyName, string error)
        {
            errors.Remove(propertyName);
            errors.Add(propertyName, error);
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 清空属性的错误信息
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        protected void ClearErrors(string propertyName)
        {
            errors.Remove(propertyName);
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion
    }

    #endregion

}
