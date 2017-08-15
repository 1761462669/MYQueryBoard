using System;
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
    public class MDFChildWindowHelper
    {
        #region 显示子窗体方法

        public static void ShowCWindow(MDFChildWindow CWindow, string title, object content, ObservableCollection<ButtonModel> buttons)
        {
            CWindow.Title = title;
            if (content != null)
                CWindow.Content = content;

            CWindow.Buttons = buttons;
            CWindow.Shown = true;
        }

        public static void ShowCWindow(MDFChildWindow CWindow, string title, object content, Action OKAction)
        {
            CWindow.Title = title;
            if (content != null)
                CWindow.Content = content;

            if (OKAction == null)
            {
                CWindow.Buttons = new ObservableCollection<ButtonModel>()
                {                    
                    new ButtonModel(){Text="确定", CommandParameter=ButtonResult.Cancel, DefaultClose=true,                
                    Image=Application.Current.Resources["img_del"] as ImageSource
                    }
                };
            }
            else
            {

                CWindow.Buttons = new ObservableCollection<ButtonModel>()
                {
                    new ButtonModel(){Text="确定", CommandParameter=ButtonResult.OK,                
                    Image=Application.Current.Resources["img_apply"] as ImageSource, BntAction=OKAction
                    },
                    new ButtonModel(){Text="取消", CommandParameter=ButtonResult.Cancel, DefaultClose=true,                
                    Image=Application.Current.Resources["img_del"] as ImageSource
                    }
                };
            }



            CWindow.Shown = true;
        }

        public static void ShowCWindow(MDFChildWindow CWindow, object content, Action OKAction)
        {
            ShowCWindow(CWindow, "数据管理", content, OKAction);
        }

        public static void ShowCWindow(MDFChildWindow CWindow, Action OKAction)
        {
            ShowCWindow(CWindow, "数据管理", null, OKAction);
        }

        public static void ShowCWindow(MDFChildWindow CWindow, string title, Action OKAction)
        {
            ShowCWindow(CWindow, title, null, OKAction);
        }

        public static void ShowCWindow(MDFChildWindow CWindow)
        {
            ShowCWindow(CWindow, null);
        }

        public static void CloseCWindow(MDFChildWindow CWindow)
        {
            CWindow.Shown = false;
        }

        #endregion

        #region 消息框弹出方法

        public static void ShowMessage(MDFChildWindow MsgWindow, string title, string msg, Action OKAction)
        {
            MsgWindow.Title = title;
            MsgWindow.Content = msg;
            if (OKAction != null)
            {
                MsgWindow.Buttons = new ObservableCollection<ButtonModel>()
                {
                    new ButtonModel(){Text="确定", CommandParameter=ButtonResult.OK, BntAction=OKAction,                
                    Image=Application.Current.Resources["img_apply"] as ImageSource
                    },
                    new ButtonModel(){Text="取消", CommandParameter=ButtonResult.Cancel, DefaultClose=true,                
                    Image=Application.Current.Resources["img_del"] as ImageSource
                    }
                };
            }
            else
            {
                MsgWindow.Buttons = new ObservableCollection<ButtonModel>()
                {
                    new ButtonModel(){Text="确定", CommandParameter=ButtonResult.OK,             
                    Image=Application.Current.Resources["img_apply"] as ImageSource,DefaultClose=true
                    }
                };

            }
            MsgWindow.Shown = true;
        }

        public static void ShowMessage(MDFChildWindow MsgWindow, string msg, Action OKAction)
        {
            ShowMessage(MsgWindow, "系统提示", msg, OKAction);
        }

        public static void ShowMessage(MDFChildWindow MsgWindow, string msg)
        {
            ShowMessage(MsgWindow, msg, null);
        }

        public static void CloseMessage(MDFChildWindow MsgWindow)
        {
            MsgWindow.Shown = false;
        }
        #endregion

    }
    public enum ButtonResult
    {
        OK,
        Cancel
    }

    public class ChildWindowViewModel : INotifyPropertyChanged
    {
        #region 弹出框基础属性
        private bool _showChildWindow = false;
        /// <summary>
        /// 是否显示弹出框
        /// </summary>
        public bool Show
        {
            get
            {
                return _showChildWindow;
            }
            set
            {
                _showChildWindow = value;
                PropertyChangedMethod("Show");
            }
        }

        private double _childWindowHeight = 200;
        /// <summary>
        /// 弹出框高度
        /// </summary>
        public double Height
        {
            get { return _childWindowHeight; }
            set
            {
                _childWindowHeight = value;
                PropertyChangedMethod("Height");
            }
        }
        private double _childWindowWidth = 300;
        /// <summary>
        /// 弹出框宽度
        /// </summary>
        public double Width
        {
            get { return _childWindowWidth; }
            set
            {
                _childWindowWidth = value;
                PropertyChangedMethod("Width");
            }
        }
        private string _childWindowTitle = "系统提示";
        /// <summary>
        /// 弹出框标题
        /// </summary>
        public string Title
        {
            get { return _childWindowTitle; }
            set
            {
                _childWindowTitle = value;
                PropertyChangedMethod("Title");
            }
        }
        private object _childWindowContent;
        /// <summary>
        /// 弹出框内容
        /// </summary>
        public object Content
        {
            get
            {
                return _childWindowContent;
            }
            set
            {
                _childWindowContent = value;
                PropertyChangedMethod("Content");
            }
        }

        private string _parameter = "";
        /// <summary>
        /// 参数
        /// </summary>
        public string Parameter
        {
            get { return _parameter; }
            set
            {
                _parameter = value;
                PropertyChangedMethod("Parameter");
            }
        }
        #endregion

        #region 属性更改通知
        private void PropertyChangedMethod(string pname)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(pname));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region 按钮信息

        private ObservableCollection<ButtonModel> _buttons;
        public ObservableCollection<ButtonModel> Buttons
        {
            get
            {
                return _buttons;
            }
            set
            {
                _buttons = value;
                PropertyChangedMethod("Buttons");
            }
        }

        #endregion

        #region 关闭Command
        public ICommand WindowCloseCommand
        {
            get;
            set;
        }
        #endregion
    }
}
