using SMES.AspNetDTM.ICore.Pub;
using SMES.AspNetFramework.MVC;
using SMES.AspNetFramework.Srv;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMES.Framework.EplDb;

namespace SMES.AspNetDTM.UI.Areas.Public.Controllers
{
    
    public class DatabaseAccessController : SMESController
    {
        // GET: Public/DatabaseAccess
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 根据sql脚本记录的code码查询业务数据
        /// </summary>
        /// <param name="code">脚本记录code</param>
        /// <param name="paralist">参数列表</param>
        /// <returns></returns>
        public SMESJsonResult QueryDatasByCd(string code,List<SQLParameter> paralist)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt;
                SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paralist, out ds);
                if (ds == null || ds.Tables.Count == 0)
                {
                    dt = null;
                }
                else
                {
                    dt = ds.Tables[0];
                }

                return JsonSMES(dt);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        /// <summary>
        /// 根据sql脚本记录的id查询业务数据
        /// </summary>
        /// <param name="id">脚本记录id</param>
        /// <param name="paralist">参数列表</param>
        /// <returns></returns>
        public SMESJsonResult QueryDatasById(string id, List<SQLParameter> paralist)
        {
            DataSet ds = new DataSet();
            DataTable dt;
            SrvProxy.CreateServices<ISqlCommandConfigService>().QueryById(id, paralist, out ds);
            if (ds == null || ds.Tables.Count == 0)
            {
                dt = null;
            }
            else
            {
                dt = ds.Tables[0];
            }

            return JsonSMES(dt);
        }
   

    }
}