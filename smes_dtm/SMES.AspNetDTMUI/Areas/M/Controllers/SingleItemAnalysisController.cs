using SMES.AspNetDTM.ICore.Pub;
using SMES.AspNetFramework.MVC;
using SMES.AspNetFramework.Srv;
using SMES.AspNetDTM.UI.Areas.Public.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using SMES.AspNetFramework;
using SMES.Framework.EplDb;

namespace SMES.AspNetDTM.UI.Areas.M.Controllers
{
    public class SingleItemAnalysisController : SMESController
    {
        // GET: M/SingleItemAnalysis
        public ActionResult Index()
        {
            ViewBag.Title = "卷包缺陷单项分析";
            //ViewBag.Date = DateTime.Now.ToString("yyyy年MM月dd日");
            //ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }

        List<SQLParameter> paras = new List<SQLParameter>();

        public JsonResult GetProcessSegmentList(string code)
        {
            DataSet ds = new DataSet();
            paras.Clear();
            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);

            if (ds == null || ds.Tables.Count == 0)
                return null;

            return JsonSMES(ds.Tables[0]);
        }

        public JsonResult GetDefectRankingList(string code)
        {
            DataSet ds = new DataSet();
            paras.Clear();
            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);

            if (ds == null || ds.Tables.Count == 0)
                return null;

            return JsonSMES(ds.Tables[0]);
        }

        public JsonResult GetDefectItemList(string code, int  ProId,int  RankId)
        {
            DataSet ds = new DataSet();
            paras.Clear();
            paras.Add(new SQLParameter { DbType = DbType.Int32, Name = ":SegmentId", Value = ProId });
            paras.Add(new SQLParameter { DbType = DbType.Int32, Name = ":RankId", Value = RankId });
            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);

            if (ds == null || ds.Tables.Count == 0)
                return null;

            return JsonSMES(ds.Tables[0]);
        }

        public JsonResult GetEquMonthsDefectRate(string code,string strMonth,int productseg,int parasid,string team )
        {
            DataSet ds = new DataSet();
            paras.Clear();
            paras.Add(new SQLParameter { DbType = DbType.String, Name = ":MONTHS", Value = strMonth });
            paras.Add(new SQLParameter { DbType = DbType.Int32, Name = ":PROSEG", Value = productseg });
            paras.Add(new SQLParameter { DbType = DbType.Int32, Name = ":PARAID", Value = parasid });
            paras.Add(new SQLParameter { DbType = DbType.String, Name = ":TEAMNM", Value = team });

            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);

            if (ds == null || ds.Tables.Count == 0)
                return null;

            return JsonSMES(ds.Tables[0]);

        }
    }
}