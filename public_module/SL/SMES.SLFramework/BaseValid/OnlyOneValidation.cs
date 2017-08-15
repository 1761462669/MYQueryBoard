using MDF.Framework;
using MDF.Framework.Bus;
using MDF.Framework.Validation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SMES.Framework.BaseValid
{
    public class OnlyOneValidation : IAsynValidation
    {

        public event ValidationFinishedEventHandler ValidationFinished;

        /// <summary>
        /// 模型名称
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; set; }

        public string IdPropertyName { get; set; }

        public string DisplayName { get; set; }

        private ValidationResult _validResult;
        public System.ComponentModel.DataAnnotations.ValidationResult ValidationResult
        {
            get
            {
                return _validResult;
            }
        }



        public async void Validate(object value, System.ComponentModel.DataAnnotations.ValidationContext context)
        {
            MDF.Framework.Bus.SynInvokeWcfService wcf = new MDF.Framework.Bus.SynInvokeWcfService();

            if (value == null) return;
            if (ModelName == null) return;
            if (PropertyName == null) return;
            if (DisplayName == null) return;

            if (value.ToString().Trim() == "") return;

            string hql = string.Format("select count(*) from {0} r where r.{1} = '{2}'", ModelName, PropertyName, value);

            if (!string.IsNullOrEmpty(IdPropertyName))
            {
                var propIdObj = context.ObjectInstance.GetType().GetProperty(this.IdPropertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

                if (propIdObj == null) return;

                object valueObj = propIdObj.GetValue(context.ObjectInstance, null);

                hql = string.Format("select count(*) from {0} r where r.{1} = '{2}' and r.{3} <> '{4}'", ModelName, PropertyName, value, IdPropertyName, valueObj.ToString());
            }

            MDF.Framework.Db.HqlQuerySetting hqlSet = new MDF.Framework.Db.HqlQuerySetting();

            hqlSet.QueryString = hql;
            var result = await wcf.Invoke<MDF.Framework.Db.IDatabaseService>(c => c.Query(hqlSet));
            var obj = context.ObjectInstance as IRaiseErrorChanged;
            if (obj != null)
                obj.ClearErrors(PropertyName);
            if (result.IsSuccess)
            {
                var objs = JsonConvert.DeserializeObject(result.InfoMessage) as JArray;

                if (objs[0] != null && objs[0].ToString() != "0")
                {
                    string errorMes = string.Format("{0} 不能重复", DisplayName);

                    _validResult = new System.ComponentModel.DataAnnotations.ValidationResult(errorMes);

                    obj.AddError(PropertyName, errorMes);
                }
            }
            else
            {

            }
            obj.RaiseErrorChanged(PropertyName);
        }
    }


}
