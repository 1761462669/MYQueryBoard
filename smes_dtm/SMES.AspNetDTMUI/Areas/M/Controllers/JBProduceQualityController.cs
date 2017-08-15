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
    public class JBProduceQualityController : SMESController
    {
        // GET: M/JBProduceQuality
        public ActionResult Index()
        {
            ViewBag.Title = "卷包质量看板";
            ViewBag.Date = DateTime.Now.ToString("yyyy年MM月dd日");
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }

        List<SQLParameter> paras = new List<SQLParameter>();
        public JsonResult QueryPhysicalInspectInfo(string code, string startdt, string enddt)
        {
            DataSet ds = new DataSet();
            paras.Clear();
            paras.Add(new SQLParameter { DbType = DbType.String, Name = "@Startdt", Value = startdt });
            paras.Add(new SQLParameter { DbType = DbType.String, Name = "@Enddt", Value = enddt });
            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);

            if (ds == null || ds.Tables.Count == 0)
                return null;

            return JsonSMES(ds.Tables[0]);
        }
    }
}