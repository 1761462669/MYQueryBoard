using MDF.Framework.Bus;
using MDF.Framework.BusinessService;
using MDF.Framework.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace SMES.Framework.Command
{
    public abstract class QueryDataCollectionCommand<T> : BaseServiceCommand<T>, IQueryDataCollectionCommand<T>
    {
        #region feilds && Properties
        private PagedCollectionView _DataSource;
        public PagedCollectionView DataSource
        {
            get { return this._DataSource; }
            set
            {
                if (this._DataSource != value)
                {
                    this._DataSource = value;
                    this.RaisePropertyChanged("DataSource");
                }
            }
        }
        #endregion


        public override object Process(ServiceResponse response)
        {
            this.ServiceError = null;
            if (response.IsSuccess)
            {
                if (response.InfoMessage == null || response.InfoMessage.ToLower() == "null")
                {
                    this.DataSource = null;
                    return null;
                }

                object current = null;
                var pageIndex = 0;
                var currentPosition = 0;
                if (this.DataSource != null && this.DataSource.CurrentItem != null)
                {
                    current = this.DataSource.CurrentItem;
                    pageIndex = this.DataSource.PageIndex;
                    currentPosition = this.DataSource.CurrentPosition;
                }
                var ds = new PagedCollectionView(InfoExchange.DeConvert<IList>(response.InfoMessage, InfoExchange.SetingsKonwnTypesBinder));
                if (current != null)
                {
                    var item = ds.OfType<object>().Where(c => c.Equals(current)).FirstOrDefault();
                    if (item != null)
                    {
                        ds.MoveCurrentTo(item);
                    }
                    else
                    {
                        try
                        {
                            ds.MoveToPage(pageIndex);
                            ds.MoveCurrentToPosition(currentPosition);
                        }
                        catch (Exception)
                        {


                        }

                    }
                }
                this.DataSource = ds;

            }
            else
            {
                this.DataSource = null;
                this.ServiceError = response.ExceptionMessage;

            }
            return this.DataSource;
        }

        public override bool CanExecute(object parameter)
        {
            if (this.DataSource == null || this.DataSource.Count == 0)
                return true;
            if (parameter != null && parameter is bool)
                return (bool)parameter;


            return true;

        }


        public virtual void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }








    }
}
