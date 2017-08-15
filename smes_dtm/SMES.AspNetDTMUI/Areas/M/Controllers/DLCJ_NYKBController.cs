using SMES.AspNetDTM.ICore;
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
    public class DLCJ_NYKBController : SMESController
    {
        // GET: M/DLCJ_NYKB
        public ActionResult Index()
        {
            ViewBag.Title = "能源看板";
            ViewBag.Date = DateTime.Now.ToString("yyyy-MM");
            return View();
        }
        /// <summary>
        /// 能源区域 YM
        /// </summary>
        /// <returns></returns>
        public JsonResult DLCJ_NYJS_1()
        {
            DataTable dt = SrvProxy.CreateServices<IDLCJ_NYKB>().DLCJ_NYJS_1();
            return JsonSMES(dt);
        }
        /// <summary>
        /// 温湿度  最大值、最小值、平均值、合格率
        /// </summary>
        /// <returns></returns>
        public JsonResult DLCJ_WS(string ID)
        {
            DataTable dt = SrvProxy.CreateServices<IDLCJ_NYKB>().DLCJ_WS(ID);
            return JsonSMES(dt);
        }
        /// <summary>
        /// 能源监视三——水电气汽
        /// </summary>
        /// <returns></returns>
        //public JsonResult DLCJ_NYJS_3()
        //{
        //    DataTable dt = SrvProxy.CreateServices<IDLCJ_NYKB>().DLCJ_NYJS_3();
        //    return JsonSMES(dt);
        //}
    }
}