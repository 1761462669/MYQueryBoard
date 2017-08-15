using SMES.AspNetDTM.ICore.Pub;
using SMES.AspNetFramework.MVC;
using SMES.AspNetFramework.Srv;
using SMES.AspNetDTM.UI.Areas.Public.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using SMES.AspNetFramework;
using SMES.Framework.EplDb;    

namespace SMES.AspNetDTM.UI.Areas.M.Controllers
{
    public class ZsProduceQualityController : SMESController
    {
        // GET: M/ZsProduceQuality
        public ActionResult Index()
        {
            ViewBag.Title = "制丝质量看板";
            ViewBag.Date = DateTime.Now.ToString("yyyy年MM月dd日");
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
    }
}