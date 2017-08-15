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
    public class JBCJ_JBQuaKBController : SMESController
    {
        // GET: M/JBCJ_JBQuaKB
        public ActionResult Index()
        {
            ViewBag.Title = "卷包质量看板";
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM");
            return View();
        }
        /// <summary>
        /// 卷包车间机台得分
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMachineScore()
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBQuaKB>().QueryMachineScore();
            return JsonSMES(dt);
        }
        /// <summary>
        /// 卷包车间班组得分
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTeamScore()
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBQuaKB>().QueryTeamScore();
            return JsonSMES(dt);
        }

    }
}