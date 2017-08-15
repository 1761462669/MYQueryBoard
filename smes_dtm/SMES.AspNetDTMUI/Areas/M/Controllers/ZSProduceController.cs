using SMES.AspNetDTM.ICore.Pub;
using SMES.AspNetFramework.MVC;
using SMES.AspNetFramework.Srv;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMES.AspNetFramework;
using SMES.Framework.EplDb;

namespace SMES.AspNetDTM.UI.Areas.M.Controllers
{
    public class ZSProduceController : SMESController
    {
        List<SQLParameter> paras=new List<SQLParameter>();
        // GET: M/ZSProduce
        public ActionResult Index()
        {
            ViewBag.Title = "制丝生产看板";
            ViewBag.Date = DateTime.Now.ToString("yyyy年MM月dd日");
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }

        public JsonResult ChangeProductDate(string CurrentDate, string DateFlag)
        {
            switch (DateFlag)
            {
                case "Preday":    //前一天
                    DateTime predt = Convert.ToDateTime(CurrentDate).AddDays(-1);
                    ViewBag.Date = predt.ToString("yyyy年MM月dd日");
                    ViewBag.Week = DataConvert.Change(predt.DayOfWeek);
                    break;
                case "Today":     //今天
                    ViewBag.Date = DateTime.Now.ToString("yyyy年MM月dd日");
                    ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
                    break;
                case "Nexday":    //后一天
                    DateTime nextdt = Convert.ToDateTime(CurrentDate).AddDays(1);
                    ViewBag.Date = nextdt.ToString("yyyy年MM月dd日");
                    ViewBag.Week = DataConvert.Change(nextdt.DayOfWeek);
                    break;
                case "Refresh":   //刷新
                    DateTime curdt = Convert.ToDateTime(CurrentDate);
                    ViewBag.Date = curdt.ToString("yyyy年MM月dd日");
                    ViewBag.Week = DataConvert.Change(curdt.DayOfWeek);
                    break;
                default:
                    break;
            }
            DataTable resultdt = new DataTable("DateTable");
            resultdt.Columns.Add("PRODUCTDATE", typeof(string));
            resultdt.Columns.Add("WEEK", typeof(string));

            resultdt.Rows.Add(ViewBag.Date, ViewBag.Week);
            return JsonSMES(resultdt);

            //var data = new { PRODUCTDATE = ViewBag.Date ,
            //                 WEEK = ViewBag.Week,
            //};

            //return JsonSMES(data);
        }

        public JsonResult GetDayWoStatics(string code,string strDate)
        { 
            DataSet ds = new DataSet();
            paras.Clear();
            SQLParameter dataPara = new SQLParameter();
            dataPara.DbType = DbType.String;
            dataPara.Name = "@QueryDate";
            dataPara.Value = strDate;
            paras.Add(dataPara);

            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code,paras,out ds);
            return JsonSMES(ds.Tables[0]);
        }

        public JsonResult GetRunningWoSegments(string code,string strDate)
        {
            DataSet ds = new DataSet();
            SQLParameter dataPara = new SQLParameter();
            dataPara.DbType = DbType.String;
            dataPara.Name = "@QueryDate";
            dataPara.Value = strDate;
            paras.Add(dataPara);

            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);
            return JsonSMES(ds.Tables[0]);
        }

        public JsonResult StaticCutDayAndMonth(string code, string strDate)
        {
            DataSet ds = new DataSet();
            SQLParameter dataPara = new SQLParameter();
            dataPara.DbType = DbType.String;
            dataPara.Name = "@QueryDate";
            dataPara.Value = strDate;
            paras.Add(dataPara);

            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);
            return JsonSMES(ds.Tables[0]);
        }
    }
}