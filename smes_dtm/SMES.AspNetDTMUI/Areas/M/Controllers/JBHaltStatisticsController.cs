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
    public class JBHaltStatisticsController : SMESController
    {
        // GET: M/JBHaltStatistics
        public ActionResult Index()
        {
            ViewBag.Title = "卷包停机看板";
            ViewBag.Date=DateTime.Now.ToString("yyyy年MM月dd日");
            ViewBag.Week=DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }

        List<SQLParameter> paras = new List<SQLParameter>();

        public JsonResult QueryJBHaltData(string code, string Keycode,string sdate,string edate,int down,int up)
        {
            DataSet ds = new DataSet();
            paras.Clear();
            paras.Add(new SQLParameter { DbType = DbType.String, Name = ":KeyCode", Value = Keycode });
            paras.Add(new SQLParameter { DbType = DbType.String, Name = ":Startdt", Value = sdate });
            paras.Add(new SQLParameter { DbType = DbType.String, Name = ":Enddt", Value = edate });
            paras.Add(new SQLParameter { DbType = DbType.Int32, Name = ":DownLimit", Value = down });
            paras.Add(new SQLParameter { DbType = DbType.Int32, Name = ":UpLimit", Value = up });

            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);

            if (ds == null || ds.Tables.Count == 0)
                return null;

            return JsonSMES(ds.Tables[0]);
        }
    }
}