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
    public class JBCJ_JBTotalKBController : SMESController
    {
        // GET: M/JBCJ_JBTotalKB
        public ActionResult Index()
        {
            ViewBag.Title = "卷包看板";
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM");
            return View();
        }
        /// <summary>
        /// 当月产量、计划量
        /// </summary>
        /// <returns></returns>
        public JsonResult Month_Prdouct()
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBTotalKB>().Month_Prdouct();
            return JsonSMES(dt);
        }
        /// <summary>
        /// 当月质量缺陷
        /// </summary>
        /// <returns></returns>
        public JsonResult Month_Qua_QX()
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBTotalKB>().Month_Qua_QX();
            return JsonSMES(dt);
       
        }
        /// <summary>
        /// 当年车间精品率
        /// </summary>
        /// <returns></returns>
        public JsonResult Year_Rate()
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBTotalKB>().Year_Rate();
            return JsonSMES(dt);
        }
        /// <summary>
        /// 当月车间精品率
        /// </summary>
        /// <returns></returns>
        public JsonResult Month_Rate()
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBTotalKB>().Month_Rate();
            return JsonSMES(dt);
        }
        /// <summary>
        /// 当日车间精品率
        /// </summary>
        /// <returns></returns>
        public JsonResult Day_Rate()
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBTotalKB>().Day_Rate();
            return JsonSMES(dt);
        }
        /// <summary>
        /// 当前设备有效作业率
        /// </summary>
        /// <returns></returns>
        public JsonResult Equ_Efficiency()
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBTotalKB>().Equ_Efficiency();
            return JsonSMES(dt);
        }

        /// <summary>
        /// 年度产量对比
        /// </summary>
        /// <returns></returns>
        public JsonResult Year_Product()
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBTotalKB>().Year_Product();
            return JsonSMES(dt);
        }

    }
}