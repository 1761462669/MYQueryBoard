using MDF.Framework.Db;
using SMES.FrameworkAdpt.CommonPara.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.FrameworkAdpt.CommonPara.Service
{
    public class CommonParaService : ICommonParaService
    {
        [Import]
        public IDataBase Db { get; set; }

        public static Dictionary<string, string> CommonParas = new Dictionary<string, string>();

        #region 查询参数

        public System.Collections.IList GetAllParas()
        {
            using (IDbSession session = this.Db.OpenSession())
            {
                var list = session.Query<CommonParaModel>().ToList();

                CommonParas.Clear();
                foreach (var item in list)
                {
                    CommonParas.Add(item.KeyCode, item.ParaValue);
                }

                return list;
            }
        }

        [MDF.Framework.Bus.InfoExchangeAllParas(typeof(MDF.Framework.Bus.JsonKnownTypeInfoConverter))]

        public IList QueryParasByTypeId(CommonParaTypeModel type, string name)
        {
            using (IDbSession session = this.Db.OpenSession())
            {
                var query = session.Query<CommonParaModel>();
                if (type != null)
                {
                    query = query.Where(c => c.ParaType.Id == type.Id);
                }

                if(!string.IsNullOrEmpty(name))
                {
                    query = query.Where(c => c.Name.Contains(name)||c.KeyCode.Contains(name));
                }

                return query.ToList();
            }
        }

        #endregion

        [MDF.Framework.Bus.InfoExchangeAllParas(typeof(MDF.Framework.Bus.JsonKnownTypeInfoConverter))]
        public void SaveOrUpdate(CommonParaModel obj)
        {
            using (IDbSession session = this.Db.OpenSession())
            {
                obj.CreateTime = DateTime.Now;
                session.SaveOrUpdate(obj);
            }
        }

        [MDF.Framework.Bus.InfoExchangeAllParas(typeof(MDF.Framework.Bus.JsonKnownTypeInfoConverter))]
        public void Delete(CommonParaModel obj)
        {
            using (IDbSession session = this.Db.OpenSession())
            {
                session.Delete(obj);
            }
        }

        public System.Collections.IList GetParaTypes()
        {
            using (IDbSession session = this.Db.OpenSession())
            {
                var list = session.Query<CommonParaTypeModel>().ToList();

                return list;
            }
        }
    }
}
