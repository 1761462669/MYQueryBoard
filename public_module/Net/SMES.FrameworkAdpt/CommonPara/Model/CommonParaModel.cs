using SMES.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.CommonPara.Model
{
    public class CommonParaModel : RootModel
    {
        private int _Id;
        public int Id
        {
            get { return this._Id; }
            set
            {
                if (this._Id != value)
                {
                    this._Id = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }

        private string _Name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this._Name = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        private string _KeyCode;
        /// <summary>
        /// 唯一Key
        /// </summary>
        public string KeyCode
        {
            get { return this._KeyCode; }
            set
            {
                if (this._KeyCode != value)
                {
                    this._KeyCode = value;
                    this.RaisePropertyChanged("KeyCode");
                }
            }
        }
        
        private string _ParaValue;
        /// <summary>
        /// 参数ID
        /// </summary>
        public string ParaValue
        {
            get { return this._ParaValue; }
            set
            {
                if (this._ParaValue != value)
                {
                    this._ParaValue = value;
                    this.RaisePropertyChanged("ParaId");
                }
            }
        }

        
        private string _SrcTableName;
        /// <summary>
        /// 原始表名
        /// </summary>
        public string SrcTableName
        {
            get { return this._SrcTableName; }
            set
            {
                if (this._SrcTableName != value)
                {
                    this._SrcTableName = value;
                    this.RaisePropertyChanged("SrcTableName");
                }
            }
        }

        private string _SrcFieldName;
        /// <summary>
        /// 原始列名
        /// </summary>
        public string SrcFieldName
        {
            get { return this._SrcFieldName; }
            set
            {
                if (this._SrcFieldName != value)
                {
                    this._SrcFieldName = value;
                    this.RaisePropertyChanged("SrcFieldName");
                }
            }
        }

        private DateTime _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return this._CreateTime; }
            set
            {
                if (this._CreateTime != value)
                {
                    this._CreateTime = value;
                    this.RaisePropertyChanged("CreateTime");
                }
            }
        }
        
        private CommonParaTypeModel _ParaType;
        public CommonParaTypeModel ParaType
        {
            get { return this._ParaType; }
            set
            {
                if (this._ParaType != value)
                {
                    this._ParaType = value;
                    this.RaisePropertyChanged("ParaType");
                }
            }
        }
    }
}
