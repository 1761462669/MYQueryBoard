using SMES.AspNetDTM.ICore.IPCM;
using SMES.AspNetFramework.MVC;
using SMES.AspNetFramework.Srv;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMES.AspNetDTM.UI.Areas.M.Controllers
{
    public class JBCJ_EquStopController : SMESController
    {
        // GET: M/JBCJ_EquStop  卷包车间 停机次数统计
        public ActionResult Index()
        {
            ViewBag.Title = "卷包停机统计";
            return View();
        }

        //年度停机趋势图
        public JsonResult JBCJ_Year()
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_EquStop>().JBCJ_Year();
            return JsonSMES(dt);
        }
        //月度机台停机次数统计图
        public JsonResult JBCJ_JT_Month(string CODE, string DATE)
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_EquStop>().JBCJ_JT_Month(CODE, DATE);
            return JsonSMES(dt);
        }
        //月度班组停机次数统计图
        public JsonResult JBCJ_Team_Month(string CODE, string DATE)
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_EquStop>().JBCJ_Team_Month(CODE, DATE);
            return JsonSMES(dt);
        }
        /// <summary>
        /// 月度停机趋势图
        /// </summary>
        /// <returns></returns>
        public JsonResult JBCJ_Month(string DATE)
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_EquStop>().JBCJ_Month(DATE);
            return JsonSMES(dt);
        }
        /// <summary>
        /// 天度停机次数统计图
        /// </summary>
        /// <param name="CODE"></param>
        /// <param name="DATE"></param>
        /// <returns></returns>
        public JsonResult JBCJ_JT_Day(string CODE, string DATE)
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_EquStop>().JBCJ_JT_Day(CODE, DATE);
            return JsonSMES(dt);
        }
    }
}