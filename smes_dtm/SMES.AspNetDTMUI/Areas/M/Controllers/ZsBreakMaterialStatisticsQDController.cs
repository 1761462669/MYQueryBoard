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
    public class ZsBreakMaterialStatisticsQDController : SMESController
    {
        // GET: M/ZsBreakMaterialStatistics
        public ActionResult Index()
        {
            ViewBag.Title = "制丝断料统计看板";
            ViewBag.Date = DateTime.Now.ToString("yyyy年MM月dd日");
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 大屏  天
        /// </summary>
        /// <returns></returns>
        public ActionResult LIndex_D()
        {
            ViewBag.Title = "制丝断料统计看板";
            ViewBag.Date = getDate(1);
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 大屏 周
        /// </summary>
        /// <returns></returns>
        public ActionResult LIndex_W()
        {
            ViewBag.Title = "制丝断料统计看板";
            ViewBag.Date = getDate(2);
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 大屏 月
        /// </summary>
        /// <returns></returns>
        public ActionResult LIndex_M()
        {
            ViewBag.Title = "制丝断料统计看板";
            ViewBag.Date = getDate(3);
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 获取开始统计时间
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public string getDate(int state)
        {
            string strDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            if (state == 2)
            {
                int weeknow = Convert.ToInt32(System.DateTime.Now.DayOfWeek);
                //星期日 获取weeknow为0  
                weeknow = weeknow == 0 ? 7 : weeknow;
                int daydiff = (-1) * weeknow + 1;
                int dayadd = 7 - weeknow;
                //本周第一天  
                strDate = System.DateTime.Now.AddDays(daydiff).ToString("yyyy-MM-dd");
            }
            else if (state == 3)
            {
                DateTime dttm = DateTime.Now;
                //本月第一天时间   
                DateTime dt_First = dttm.AddDays(-(dttm.Day) + 1);
                strDate = dt_First.Date.ToString("yyyy-MM-dd");
            }

            return strDate;
        }

        List<SQLParameter> paras = new List<SQLParameter>();
        public JsonResult QueryZsBreakMaterial(string code,string startdt,string enddt)
        {
            DataSet ds = new DataSet();
            paras.Clear();
            paras.Add(new SQLParameter { DbType = DbType.String, Name = "@stardate", Value = startdt });
            paras.Add(new SQLParameter { DbType = DbType.String, Name = "@enddate", Value = enddt });
            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);

            if (ds == null || ds.Tables.Count == 0)
                return null;

            return JsonSMES(ds.Tables[0]);
        }
    }
}