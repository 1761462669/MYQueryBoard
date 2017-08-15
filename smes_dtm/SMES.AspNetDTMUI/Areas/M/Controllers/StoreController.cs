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
    public class StoreController : SMESController
    {
        // GET: M/Store
        public ActionResult Index()
        {
            ViewBag.Title = "储柜信息监视";
            return View();
        }

        public SMESJsonResult QueryStoreType()
        {

            DataTable dt = SrvProxy.CreateServices<ITsStockService>().QueryStoreType();
            return JsonSMES(dt);
        }

        public SMESJsonResult QueryStoreInfoByType(int typeid)
        {
            DataTable dt = SrvProxy.CreateServices<ITsStockService>().QueryStoreInfoByType(typeid);

            return JsonSMES(dt);
        }
    }
}