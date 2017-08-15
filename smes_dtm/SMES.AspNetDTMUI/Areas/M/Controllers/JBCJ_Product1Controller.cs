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
    public class JBCJ_Product1Controller : SMESController
    {
        // GET: M/JBCJ_Product1
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 当前班次班组及其日产量
        /// </summary>
        /// <returns></returns>
        public JsonResult Shift_Team_Prdouct()
        {                       
            DataTable dt = SrvProxy.CreateServices<IFL_JBProduce>().Shift_Team_Prdouct();
            return JsonSMES(dt);
        }
    }
}