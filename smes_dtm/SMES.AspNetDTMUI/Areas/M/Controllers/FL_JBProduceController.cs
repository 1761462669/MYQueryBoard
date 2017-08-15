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
    public class FL_JBProduceController : SMESController
    {
        // GET: M/FL_JBProduce
        /// <summary>
        /// 功能：涪陵卷包产能看板
        /// 创建：李金花
        /// 时间：2016-12-09
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title ="卷包月计划";
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM");
            //ViewBag.Week =  DataConvert.Change(DateTime.Now.DayOfWeek);           
            return View();
          
        }


        /// <summary>
        /// 卷包按价类查询产量
        /// </summary>
        /// <param name="strStartTime">开始日期</param>
        /// <param name="strEndTime">结束日期</param>
        /// <param name="strCon">其他条件</param>
        /// <returns></returns>
        public JsonResult GetJL(string strStartTime, string strEndTime, string strCon)
        {
            DataTable dt = SrvProxy.CreateServices<IFL_JBProduce>().QueryJL(strStartTime, strEndTime, strCon);
            return JsonSMES(dt);
        }
        /// <summary>
        /// 卷包按系列查询产量
        /// </summary>
        /// <param name="strStartTime">开始日期</param>
        /// <param name="strEndTime">结束日期</param>
        /// <param name="strCon">其他条件</param>
        /// <returns></returns>
        public JsonResult GetXL(string strStartTime, string strEndTime, string strCon)
        {
            DataTable dt = SrvProxy.CreateServices<IFL_JBProduce>().QueryXL(strStartTime, strEndTime, strCon);
            return JsonSMES(dt);
        }
        /// <summary>
        /// 卷包按牌号查询产量
        /// </summary>
        /// <param name="strStartTime">开始日期</param>
        /// <param name="strEndTime">结束日期</param>
        /// <param name="strCon">其他条件</param>
        /// <returns></returns>
        public JsonResult GetPH()
        {
            string strStartTime = DateTime.Now.ToString("yyyy-MM")+"-01";
            string strEndTime = DateTime.Now.ToString("yyyy-MM-dd");
            string strCon = "";
            DataTable dt = SrvProxy.CreateServices<IFL_JBProduce>().QueryPH(strStartTime, strEndTime, strCon);
            return JsonSMES(dt);
        }
    }
}