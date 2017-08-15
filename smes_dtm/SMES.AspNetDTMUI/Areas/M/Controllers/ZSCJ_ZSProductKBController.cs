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
    public class ZSCJ_ZSProductKBController :  SMESController
    {
        // GET: M/ZSCJ_ZSProductKB
        public ActionResult Index()
        {
            ViewBag.Title = "制丝生产看板";
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM");
            return View();
        }
        /// <summary>
        /// 制丝年度断料率
        /// </summary>
        /// <returns></returns>
        public JsonResult GetYear_DLRate()
        {
            DataTable dt = SrvProxy.CreateServices<IZSCJ_ZSProductKB>().GetYear_DLRate();
            return JsonSMES(dt);
        }
        /// <summary>
        /// 制丝月度断料率
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMonth_DLRate()
        {
            DataTable dt = SrvProxy.CreateServices<IZSCJ_ZSProductKB>().GetMonth_DLRate();
            return JsonSMES(dt);
        }
        /// <summary>
        /// 制丝年度合格率
        /// </summary>
        /// <returns></returns>
        public JsonResult GetYear_HGRate()
        {
            DataTable dt = SrvProxy.CreateServices<IZSCJ_ZSProductKB>().GetYear_HGRate();
            return JsonSMES(dt);
        }
        /// <summary>
        /// 制丝月度合格率
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMonth_HGRate()
        {
            DataTable dt = SrvProxy.CreateServices<IZSCJ_ZSProductKB>().GetMonth_HGRate();
            return JsonSMES(dt);
        }
    }
}