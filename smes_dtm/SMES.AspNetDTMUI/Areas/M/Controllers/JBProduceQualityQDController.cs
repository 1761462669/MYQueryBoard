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
    public class JBProduceQualityQDController : SMESController
    {
        //
        // GET: /M/JBQuality/
        public ActionResult Index()
        {
            ViewBag.Title = "卷包质量";
            ViewBag.Date = DateTime.Now.ToString("yyyy年MM月dd日");
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }

        /// <summary>
        /// 获取开始统计时间
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public string getDate(int state)
        {
            string strDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            if (state == 2)
            {
                int weeknow = Convert.ToInt32(System.DateTime.Now.DayOfWeek);
                //星期日 获取weeknow为0  
                weeknow = weeknow == 0 ? 7 : weeknow;
                int daydiff = (-1) * weeknow + 1;
                int dayadd = 7 - weeknow;
                //本周第一天  
                strDate = System.DateTime.Now.AddDays(daydiff).ToString("yyyy-MM-dd");
            }
            else if (state == 3)
            {
                DateTime dttm = DateTime.Now;
                //本月第一天时间   
                DateTime dt_First = dttm.AddDays(-(dttm.Day) + 1);
                strDate = dt_First.Date.ToString("yyyy-MM-dd");
            }

            return strDate;
        }

        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="state"></param>
        /// <param name="costType"></param>
        /// <returns></returns>
        public JsonResult getDataList(int state)
        {
            string strToday = getDate(state);
            DataTable dt = SrvProxy.CreateServices<IJBQuaQD>().QueryData(strToday);
            return JsonSMES(dt);
        }

        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="state"></param>
        /// <param name="costType"></param>
        /// <returns></returns>
        public JsonResult getDataLists(int state, string matname)
        {
            string strToday = getDate(state);
            DataTable dt = SrvProxy.CreateServices<IJBQuaQD>().QueryDatas(strToday, matname);
            return JsonSMES(dt);
        }
	}
}