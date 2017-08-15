using SMES.AspNetDTM.ICore.IPCM;
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
    /// <summary>
    /// 功能：卷包生产效率
    /// 创建：杨玉海
    /// 日期：2016-5-13
    /// </summary>
    public class JBProduceEfficiencyController : SMESController
    {
        //
        // GET: /M/JBProduceEfficiency/
        public ActionResult Index()
        {
            ViewBag.Title = "卷包生产效率";
            ViewBag.Date = DateTime.Now.ToString("yyyy年MM月dd日");
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 大屏 月 三班
        /// </summary>
        /// <returns></returns>
        public ActionResult LIndex_M()
        {
            ViewBag.Title = "卷包生产效率";
            ViewBag.Date = getDate(3);
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 大屏 月 甲班
        /// </summary>
        /// <returns></returns>
        public ActionResult LIndex_MJ()
        {
            ViewBag.Title = "卷包生产效率";
            ViewBag.Date = getDate(3);
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 大屏 月 乙班
        /// </summary>
        /// <returns></returns>
        public ActionResult LIndex_MY()
        {
            ViewBag.Title = "卷包生产效率";
            ViewBag.Date = getDate(3);
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 大屏 月 丙班
        /// </summary>
        /// <returns></returns>
        public ActionResult LIndex_MB()
        {
            ViewBag.Title = "卷包生产效率";
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

        /// <summary>
        /// 卷包机台运行效率
        /// </summary>
        /// <param name="strStartTime">开始日期</param>
        /// <param name="strEndTime">结束日期</param>
        /// <param name="strCon">其他条件</param>
        /// <returns></returns>
        public JsonResult GetEfficiencyRun(string strStartTime, string strEndTime, string strCon)
        {
            DataTable dt = SrvProxy.CreateServices<IJBProduceEfficiency>().QueryEfficiencyRun(strStartTime,strEndTime,strCon);
            return JsonSMES(dt);
        }
        /// <summary>
        /// 卷包机台台时产能
        /// </summary>
        /// <param name="strStartTime">开始日期</param>
        /// <param name="strEndTime">结束日期</param>
        /// <param name="strCon">其他条件</param>
        /// <returns></returns>
        public JsonResult GetCapacity(string strStartTime, string strEndTime, string strCon)
        {
            DataTable dt = SrvProxy.CreateServices<IJBProduceEfficiency>().QueryCapacity(strStartTime, strEndTime, strCon);
            return JsonSMES(dt);
        }
	}
}