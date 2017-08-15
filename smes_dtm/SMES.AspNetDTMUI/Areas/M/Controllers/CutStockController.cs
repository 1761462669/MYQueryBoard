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
    public class CutStockController : SMESController
    {
        // GET: M/CutStock
        // GET: IPCM/CutStock
        public ActionResult Index()
        {
            return View();
        }

        public SMESJsonResult QueryCutStockMat(string key)
        {

            DataTable dt = SrvProxy.CreateServices<ICutStockSrv>().QueryCutStockMat(key);

            return JsonSMES(dt);
        }

        public SMESJsonResult QueryCutStockBatchList(string materialcd)
        {
            DataTable dt = SrvProxy.CreateServices<ICutStockSrv>().QueryCutStockBatchList(materialcd);

            return JsonSMES(dt);
        }

        public SMESJsonResult QueryBatchLockDetail(string lot)
        {
            DataTable dt = SrvProxy.CreateServices<ICutStockSrv>().QueryBatchLockDetail(lot);

            return JsonSMES(dt);
        }
    }
}