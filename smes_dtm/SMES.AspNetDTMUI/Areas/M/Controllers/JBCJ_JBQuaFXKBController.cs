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
    public class JBCJ_JBQuaFXKBController : SMESController
    {
        // GET: M/JBCJ_JBQuaFXKB
        public ActionResult Index()
        {
            ViewBag.Title = "卷包质量分析";
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM");
            return View();
        }
        /// <summary>
        /// 当月车间各等级比例
        /// </summary>
        /// <returns></returns>
        public JsonResult Month_Qua_Rate()
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBQuaFXKB>().Month_Qua_Rate();
            return JsonSMES(dt);
        }
        /// <summary>
        /// 当月牌号质量对比
        /// </summary>
        /// <returns></returns>
        public JsonResult getData_PH(string DJ)
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBQuaFXKB>().getData_PH(DJ);
            return JsonSMES(dt);
        }
        /// <summary>
        /// 当月机台质量对比
        /// </summary>
        /// <returns></returns>
        public JsonResult getData_Equ(string DJ)
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBQuaFXKB>().getData_Equ(DJ);
            return JsonSMES(dt);
        }
        /// <summary>
        /// 当月班组质量对比
        /// </summary>
        /// <returns></returns>
        public JsonResult getData_Team(string DJ)
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBQuaFXKB>().getData_Team(DJ);
            return JsonSMES(dt);
        }
    }
}