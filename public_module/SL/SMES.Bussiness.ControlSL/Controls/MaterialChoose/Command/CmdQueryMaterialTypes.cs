using MDF.Framework.Commands;
using SMES.FrameworkAdpt.MaterialChooseControl.Service;
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
using System.Collections.Generic;
using MDF.Framework.Bus;
using SMES.FrameworkAdpt.MaterialChooseControl;

namespace SMES.Bussiness.ControlSL.Controls.MaterialChoose.Command
{
    public class CmdQueryMaterialTypes : BaseServiceCommand<IComMaterialTypeService>
    {
        
        private List<string> _TypeIds;
        public List<string> TypeIds
        {
            get { return this._TypeIds; }
            set
            {
                if (this._TypeIds != value)
                {
                    this._TypeIds = value;
                    this.RaisePropertyChanged("TypeIds");
                }
            }
        }

        
        private IList<MaterialTypeModelAdpt> _MaterialTypes;
        public IList<MaterialTypeModelAdpt> MaterialTypes
        {
            get { return this._MaterialTypes; }
            set
            {
                if (this._MaterialTypes != value)
                {
                    this._MaterialTypes = value;
                    this.RaisePropertyChanged("MaterialTypes");
                }
            }
        }

        protected override System.Linq.Expressions.LambdaExpression Expression
        {
            get { return this.GetExpression(c => c.GetMaterialTypes(TypeIds)); }
        }

        public override object Process(MDF.Framework.BusinessService.ServiceResponse response)
        {
            if(response.IsSuccess)
            {
                var list = InfoExchange.DeConvert<List<MaterialTypeModelAdpt>>(response.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);
                MaterialTypes = list;
            }

            return null;
        }
    }
}
