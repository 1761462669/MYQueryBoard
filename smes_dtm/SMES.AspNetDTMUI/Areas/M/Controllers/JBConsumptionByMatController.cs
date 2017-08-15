using SMES.AspNetDTM.ICore.IPCM;
using SMES.AspNetFramework;
using SMES.AspNetFramework.MVC;
using SMES.AspNetFramework.Srv;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMES.AspNetDTM.UI.Areas.M.Controllers
{
    /// <summary>
    /// 功能：卷包单耗-按牌号
    /// 创建：杨玉海
    /// 日期：2016-5-09
    /// </summary>
    public class JBConsumptionByMatController : SMESController
    {
        //
        // GET: /M/JBConsumptionByMat/
        public ActionResult Index()
        {

            ViewBag.Title = "卷包单耗";
            ViewBag.Date = DateTime.Now.ToString("yyyy年MM月dd日");
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 大屏 天(昨日)
        /// </summary>
        /// <returns></returns>
        public ActionResult LIndex_D()
        {

            ViewBag.Title = "卷包单耗";
            //ViewBag.Date = getDate(1);
            ViewBag.Date = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd");
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 大屏 周
        /// </summary>
        /// <returns></returns>
        public ActionResult LIndex_W()
        {

            ViewBag.Title = "卷包单耗";
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

            ViewBag.Title = "卷包单耗";
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
        public JsonResult GetDataByDayAndShift(string ShiftNM)
        {
            string strToday = DateTime.Now.Date.ToString("yyyy-MM-dd");
            DataTable dt = SrvProxy.CreateServices<IJBConsumption>().QueryJBConsumptionByMatAndShift(strToday,ShiftNM);
            return JsonSMES(dt);
        }

        public JsonResult GetDataByDay(string strDate, string endDate)
        {
            string strToday = DateTime.Now.Date.ToString("yyyy-MM-dd");
            DataTable dt = SrvProxy.CreateServices<IJBConsumption>().QueryJBConsumptionByMat(strDate, endDate);
            return JsonSMES(dt);
        }

        //public JsonResult GetDataByWeek()
        //{
        //    int weeknow = Convert.ToInt32(System.DateTime.Now.DayOfWeek);  
        //    //星期日 获取weeknow为0  
        //    weeknow=weeknow==0?7:weeknow;  
        //    int daydiff = (-1) * weeknow + 1;  
        //    int dayadd = 7 - weeknow;    
        //    //本周第一天  
        //    string datebegin = System.DateTime.Now.AddDays(daydiff).ToString("yyyy-MM-dd");

        //    DataTable dt = SrvProxy.CreateServices<IJBConsumption>().QueryJBConsumptionByMat(datebegin);
        //    return JsonSMES(dt);
        //}


        //public JsonResult GetDataByMonth()
        //{
        //    DateTime dttm = DateTime.Now;
        //    //本月第一天时间   
        //    DateTime dt_First = dttm.AddDays(-(dttm.Day) + 1);
        //    DataTable dt = SrvProxy.CreateServices<IJBConsumption>().QueryJBConsumptionByMat(dt_First.ToString("yyyy-MM-dd"));
        //    return JsonSMES(dt);
        //}
	}
}