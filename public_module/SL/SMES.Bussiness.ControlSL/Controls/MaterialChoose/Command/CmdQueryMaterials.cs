using MDF.Framework.Bus;
using MDF.Framework.Commands;
using SMES.FrameworkAdpt.MaterialChooseControl;
using SMES.FrameworkAdpt.MaterialChooseControl.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SMES.Bussiness.ControlSL.Controls.MaterialChoose.Command
{
    public class CmdQueryMaterials : BaseServiceCommand<IComMaterialTypeService>
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

        private List<MaterialModelAdpt> _Materials;
        public List<MaterialModelAdpt> Materials
        {
            get { return this._Materials; }
            set
            {
                if (this._Materials != value)
                {
                    this._Materials = value;
                    this.RaisePropertyChanged("Materials");
                }
            }
        }

        protected override System.Linq.Expressions.LambdaExpression Expression
        {
            get { return this.GetExpression(c => c.GetMaterials(TypeIds)); }
        }

        public override object Process(MDF.Framework.BusinessService.ServiceResponse response)
        {
            if (response.IsSuccess)
            {
                var list = InfoExchange.DeConvert<List<MaterialModelAdpt>>(response.InfoMessage, InfoExchange.SetingsKonwnTypesBinder);
                Materials = list;
            }

            return null;
        }
    }
}
