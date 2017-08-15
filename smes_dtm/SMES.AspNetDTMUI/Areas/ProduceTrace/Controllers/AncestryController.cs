using SMES.AspNetDTM.ICore.ProduceAncestry;
using SMES.AspNetFramework.MVC;
using SMES.AspNetFramework.Srv;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMES.AspNetDTM.UI.Areas.ProduceTrace.Controllers
{
    public class AncestryController : SMESController
    {
        // GET: ProduceTrace/Ancestry
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reverse()
        {
            return View();
        }

        public SMESJsonResult QueryCutBatchInfo(DateTime? plandate, string productkey)
        {
            DateTime dtime = DateTime.Now;
            if (plandate.Value != null)
                dtime = plandate.Value;
            //DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().QueryCutBatchInfo(dtime, productkey);
            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().QueryCutBatchInfo(dtime,productkey);
            return JsonSMES(dt);
        }

        public SMESJsonResult QueryPackBatchInfo(DateTime? plandate, string team, string product, string stamp, string codeone, int type)
        {
            DateTime dtime = DateTime.Now;
            if (plandate.Value != null)
                dtime = plandate.Value;
            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().QueryPackBatchInfo(dtime, team, product, stamp, codeone, type);
            return JsonSMES(dt);
        }


        public SMESJsonResult QueryPackMatInfo(DateTime? plandate)
        {
            DateTime dtime = DateTime.Now;
            if (plandate.Value != null)
                dtime = plandate.Value;
            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().QueryPackMatInfo(dtime);
            return JsonSMES(dt);
        }

        public SMESJsonResult QueryWorkOrderOutPut(string batchid)
        {
            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().QueryWorkOrderOutPut(batchid);

            return JsonSMES(dt);
        }

        public SMESJsonResult QueryPackSumInfo(string plancode, string teamid, string plandate)
        {

            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().QueryPackSumInfo(plancode, teamid, plandate);
            return JsonSMES(dt);
        }

        public SMESJsonResult QueryPackWoInfo(string plancode,string teamid,string plandate)
        {
            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().QueryPackWoInfo(plancode, teamid, plandate);
            return JsonSMES(dt); 
        }

        public SMESJsonResult QueryEQUInfo(string plancode, string teamid, string plandate, int typeid, string pronm)
        {
            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().QueryEQUInfo(plancode, teamid, plandate, typeid, pronm);
            return JsonSMES(dt);
        }

        public SMESJsonResult QueryFeedWoInfo(string plancode, string teamid, string plandate, string feedwo)
        {
            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().QueryFeedWoInfo(plancode, teamid, plandate, feedwo);
            return JsonSMES(dt);
        }

        public ActionResult ProduceProcessInfo()
        {
            ViewBag.Title = "生产过程信息";
            return View();
        }

        public SMESJsonResult QueryCutWorkOrder(string id)
        {
            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().GetCutWorkOrder(id);

            if (dt.Rows.Count > 0)
                return JsonSMES(dt.Rows[0]);
            else
                return JsonSMES(null);
        }

        public SMESJsonResult GetMaterialTransferinoutsilo(string id, int typeid)
        {
            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().GetMaterialTransferinoutsilo(id, typeid);

            return JsonSMES(dt);
        }

        public SMESJsonResult GetCutWorkorderConsume(string id)
        {
            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().GetCutWorkorderConsume(id);

            return JsonSMES(dt);
        }

        public SMESJsonResult GetCutAncestryKeyParamData(string id)
        {
            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().GetCutAncestryKeyParamData(id);

            return JsonSMES(dt);
        }

        #region 制丝工单检验得分
        public SMESJsonResult GetCutBatchSorce(string woid)
        {
            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().GetCutBatchSorce(woid);

            if (dt.Rows.Count > 0)
                return JsonSMES(dt.Rows[0]);
            else
                return JsonSMES(null);
        }

        public SMESJsonResult GetProductMonthCutBatchSorce(string woid)
        {
            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().GetProductMonthCutBatchSorce(woid);

            if (dt.Rows.Count > 0)
                return JsonSMES(dt.Rows[0]);
            else
                return JsonSMES(null);
        }

        public SMESJsonResult GetAllProductMonthCutBatchSorce(string woid)
        {
            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().GetAllProductMonthCutBatchSorce(woid);

            if (dt.Rows.Count > 0)
                return JsonSMES(dt.Rows[0]);
            else
                return JsonSMES(null);
        }
        #endregion 制丝工单检验得分

        #region 制丝参数检验得分
        public SMESJsonResult GetCutWorkOrderPramQL(string woid)
        {
            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().GetCutWorkOrderPramQL(woid);

            return JsonSMES(dt);
        }

        public SMESJsonResult GetCutWorkProductOrderPramQL(string woid)
        {
            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().GetCutWorkProductOrderPramQL(woid);

            return JsonSMES(dt);
        }

        public SMESJsonResult GetCutWorkOrderAllProductPramQL(string woid)
        {
            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().GetCutWorkOrderAllProductPramQL(woid);

            return JsonSMES(dt);
        }

        public SMESJsonResult GetAllWorkorderPramQL(string woid)
        {
            DataTable dt = SrvProxy.CreateServices<ICutProduceAncestry>().GetCutWorkOrderAllProductPramQL(woid);

            return JsonSMES(dt);
        }
        #endregion
    }
}