using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SMES.Framework.Command
{
    /// <summary>
    /// 实现转发的命令类
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// 命令方法
        /// </summary>
        readonly Action<object> CommandAction;

        /// <summary>
        /// 命令所包含的一组条件
        /// </summary>
        readonly Predicate<object> CommandPredicate;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_CommandAction"></param>
        /// <param name="_CommandPredicate"></param>
        public RelayCommand(Action<object> _CommandAction, Predicate<object> _CommandPredicate)
        {
            if (_CommandAction == null)
            {
                throw new ArgumentNullException("命令方法为空");
            }
            CommandAction = _CommandAction;
            CommandPredicate = _CommandPredicate;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_CommandAction"></param>
        public RelayCommand(Action<object> _CommandAction) : this(_CommandAction, null)
        {

        }

        /// <summary>
        /// 命令接口方法(定义用于确定此命令是否可以在其当前状态下执行的方法)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return CommandPredicate == null || CommandPredicate(parameter);
        }

        /// <summary>
        /// 命令接口方法(当出现影响是否应执行该命令的更改时发生)
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        /// <summary>
        /// 命令接口方法(定义在调用此命令时调用的方法)
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            CommandAction(parameter);
        }
    }
}