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
    public class FL_CutQuaController : SMESController
    {
        // GET: M/FL_CutQua
        /// <summary>
        /// 质量合格率看板
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "质量月合格率";
            ViewBag.Date = DateTime.Now.ToString("yyyy年MM月dd");
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 获取合格率
        /// </summary>
        /// <param name="strCon"></param>
        /// <returns></returns>

        public JsonResult GetQua(string strCon)
        {
            DataTable dt = SrvProxy.CreateServices<IFL_CutQua>().QueryQua(strCon);
            return JsonSMES(dt);
        }
    }
}