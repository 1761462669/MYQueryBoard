using SMES.AspNetDTM.ICore.Pub;
using SMES.AspNetFramework.MVC;
using SMES.AspNetFramework.Srv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using SMES.AspNetFramework;
using SMES.Framework.EplDb;

namespace SMES.AspNetDTM.UI.Areas.M.Controllers
{
    public class ZsStandardExecutionController : SMESController
    {
        // GET: M/ZsStandardExecution
        public ActionResult Index()
        {
            ViewBag.Title = "制丝标准执行看板";
            ViewBag.Date = DateTime.Now.ToString("yyyy年MM月dd日");
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }

        List<SQLParameter> paras = new List<SQLParameter>();

        public JsonResult GetCutBrandList(string code)
        {
            DataSet ds = new DataSet();
            paras.Clear();
            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);

            if (ds == null || ds.Tables.Count == 0)
                return null;

            return JsonSMES(ds.Tables[0]);
        }

        public JsonResult GetAllBrandExeStd(string code)
        {
            DataSet ds = new DataSet();
            paras.Clear();
            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);

            if (ds == null || ds.Tables.Count == 0)
                return null;

            return JsonSMES(ds.Tables[0]);
        }

        public JsonResult GetBrandExeStdById(string code,int matid)
        {
            DataSet ds = new DataSet();
            paras.Clear();
            paras.Add(new SQLParameter { DbType = DbType.Int32, Name = "@MatId", Value = matid });
            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);
            if (ds == null || ds.Tables.Count == 0)
                return null;

            return JsonSMES(ds.Tables[0]);
        }
    }
}