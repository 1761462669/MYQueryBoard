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
    public class JBCJ_JBConsumeKBController : SMESController
    {
        // GET: M/JBCJ_JBConsumeKB
        public ActionResult Index()
        {
            ViewBag.Title = "卷包物耗看板";
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM");
            return View();
        }
        /// <summary>
        /// 卷包卷烟纸消耗
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMonth_JYZ()
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBConsumeKB>().Query_JYZConsume();
            return JsonSMES(dt);
        }
        /// <summary>
        /// 卷包嘴棒消耗
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMonth_ZB()
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBConsumeKB>().Query_ZBConsume();
            return JsonSMES(dt);
        }
        /// <summary>
        /// 卷包小盒消耗
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMonth_XiaoH()
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBConsumeKB>().Query_XiaoHConsume();
            return JsonSMES(dt);
        }
        /// <summary>
        /// 卷包条盒消耗
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMonth_TiaoH()
        {
            DataTable dt = SrvProxy.CreateServices<IJBCJ_JBConsumeKB>().Query_TiaoHConsume();
            return JsonSMES(dt);
        }
    }
}