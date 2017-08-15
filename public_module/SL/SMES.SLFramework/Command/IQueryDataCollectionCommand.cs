using MDF.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace SMES.Framework.Command
{
    public interface IQueryDataCollectionCommand<T> : ICommand, IServiceCommand, IRaiseCanExecute
    {
        /// <summary>
        /// 数据源
        /// </summary>
        PagedCollectionView DataSource { get; set; }

    }
}
