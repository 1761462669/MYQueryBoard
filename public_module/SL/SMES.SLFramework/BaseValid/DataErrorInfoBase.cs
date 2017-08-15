using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SMES.SLFramework.BaseValid
{
    /// <summary>
    /// 验证基类
    /// added by zongyh 2015/4/14
    /// </summary>
    public abstract class DataErrorInfoBase : INotifyPropertyChanged, IDataErrorInfo
    {
        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisedPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region IDataErrorInfo 成员

        private string _dataError = string.Empty;
        private Dictionary<string, string> _dataErrors = new Dictionary<string, string>();

        public string Error
        {
            get { return _dataError; }
        }

        public string this[string columnName]
        {
            get
            {
                if (_dataErrors.ContainsKey(columnName))
                    return _dataErrors[columnName];
                else
                    return null;
            }
        }

        #endregion

        #region 验证方法
        /// <summary>
        /// 添加错误
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="error">错误信息</param>
        public void AddError(string name, string error)
        {
            _dataErrors[name] = error;
            this.RaisedPropertyChanged(name);
        }

        /// <summary>
        /// 移除错误信息
        /// </summary>
        /// <param name="name">属性名称</param>
        public void RemoveError(string name)
        {
            if (_dataErrors.ContainsKey(name))
            {
                _dataErrors.Remove(name);
                this.RaisedPropertyChanged(name);
            }
        }
        /// <summary>
        /// 清楚所有错误信息
        /// </summary>
        public void ClearError()
        {
            var keys = new string[_dataErrors.Count];
            _dataErrors.Keys.CopyTo(keys, 0);
            foreach (var key in keys)
            {
                this.RemoveError(key);
            }
        }

        /// <summary>
        /// 全部属性验证
        /// </summary>
        /// <returns>true 验证通过，false验证失败</returns>
        public bool Validate()
        {
            this.ClearError();
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(this, new ValidationContext(this, null, null), results, true))
            {
                foreach (var result in results)
                {
                    this.AddError(result.MemberNames.First(), result.ErrorMessage);
                }
                return false;
            }
            return true;
        }
        #endregion

        #region 公共方法

        /// <summary>
        /// 属性变更后，触发属性验证
        /// </summary>
        /// <param name="value">属性的值</param>
        /// <param name="name">属性名称</param>
        public void RaiseError(object value,string name)
        {
            try
            {
                Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = name });

                this.RemoveError(name);
            }
            catch(Exception ex)
            {
                this.AddError(name, ex.Message);

                throw;
            }
        }

        #endregion

    }
}
