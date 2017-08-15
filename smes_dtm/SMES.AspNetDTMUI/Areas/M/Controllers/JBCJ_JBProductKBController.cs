using SMES.AspNetDTM.ICore.IPCM;
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
    public class JBCJ_JBProductKBController : SMESController
    {
        // GET: M/JBCJ_JBProductKB
        public ActionResult Index()
        {
            ViewBag.Title = "卷包生产看板";
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM");
            return View();
        }
        /// <summary>
        /// 当前班次班组及其日产量
        /// </summary>
        /// <returns></returns>
        public JsonResult Shift_Team_Prdouct()
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBProductKB>().Shift_Team_Prdouct();
            return JsonSMES(dt);
        }
        /// <summary>
        /// 卷包按牌号查询产量
        /// </summary>
        /// <param name="strStartTime">开始日期</param>
        /// <param name="strEndTime">结束日期</param>
        /// <param name="strCon">其他条件</param>
        /// <returns></returns>
        public JsonResult Month_Mat_Product()
        {
            string strStartTime = DateTime.Now.ToString("yyyy-MM") + "-01";
            string strEndTime = DateTime.Now.ToString("yyyy-MM-dd");
            string strCon = "";
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBProductKB>().QueryPH(strStartTime, strEndTime, strCon);
            return JsonSMES(dt);
        }
        /// <summary>
        /// 当班机台产量
        /// </summary>
        /// <returns></returns>
        public JsonResult Team_Equ_Product()
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBProductKB>().Team_Equ_Product();
            return JsonSMES(dt);
        }
        /// <summary>
        /// 当月产量、计划量
        /// </summary>
        /// <returns></returns>
        public JsonResult Month_Prdouct()
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBProductKB>().Month_Prdouct();
            return JsonSMES(dt);
        }
    }
}