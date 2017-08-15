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
    /// <summary>
    /// 功能：卷包单耗-按机型
    /// 创建：杨玉海
    /// 日期：2016-5-09
    /// </summary>
    public class JBConsumptionByModelController : SMESController
    {
        //
        // GET: /M/JBConsumptionByModel/
        public ActionResult Index()
        {

            ViewBag.Title = "机台消耗";
            ViewBag.Date = DateTime.Now.ToString("yyyy年MM月dd日");
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 大屏  天（昨日）
        /// </summary>
        /// <returns></returns>
        public ActionResult LIndex_D()
        {

            ViewBag.Title = "机台消耗";
            //ViewBag.Date = getDate(1);
            ViewBag.Date = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd") ;
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 大屏 周
        /// </summary>
        /// <returns></returns>
        public ActionResult LIndex_W()
        {

            ViewBag.Title = "机台消耗";
            ViewBag.Date = getDate(2);
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 大屏 月
        /// </summary>
        /// <returns></returns>
        public ActionResult LIndex_M()
        {

            ViewBag.Title = "机台消耗";
            ViewBag.Date = getDate(3);
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
        public JsonResult getDataList(string StrDate, string endDate, int state, string costType, string ShiftID, string UnitConversion)
        {
            if (string.IsNullOrEmpty(UnitConversion))
            {
                UnitConversion = "1";
            }
            string strToday = getDate(state);
            DataTable dt = new DataTable();
            if (ShiftID != "-1")
            {
                dt = SrvProxy.CreateServices<IJBConsumption>().QueryJBConsumptionByModelAndShift(strToday, costType, ShiftID, UnitConversion);
            }
            else
            {
                dt = SrvProxy.CreateServices<IJBConsumption>().QueryJBConsumptionByModel(StrDate, endDate, costType, UnitConversion);
            }
            //Consum
            DataTable dtnew = new DataTable(); 
            dtnew.Columns.Add("EquModelNm");
            dtnew.Columns.Add("StartFirst");
            dtnew.Columns.Add("StartSecond");
            dtnew.Columns.Add("StartThird");
            dtnew.Columns.Add("StartFourth");
            dtnew.Columns.Add("StartFifty");
            dtnew.Columns.Add("EndFirst");
            dtnew.Columns.Add("EndSecond");
            dtnew.Columns.Add("EndThird");
            dtnew.Columns.Add("EndtFourth");
            dtnew.Columns.Add("EndFifty");

            dtnew.Columns.Add("StartFirstConsum");
            dtnew.Columns.Add("StartSecondConsum");
            dtnew.Columns.Add("StartThirdConsum");
            dtnew.Columns.Add("StartFourthConsum");
            dtnew.Columns.Add("StartFiftyConsum");
            dtnew.Columns.Add("EndFirstConsum");
            dtnew.Columns.Add("EndSecondConsum");
            dtnew.Columns.Add("EndThirdConsum");
            dtnew.Columns.Add("EndtFourthConsum");
            dtnew.Columns.Add("EndFiftyConsum");

            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (dt.Rows.Count == 0)
            {
                dtnew.Rows.Add(dtnew.NewRow());
            }
            foreach (DataRow dr in dt.Rows)
            {
                if (!dic.Keys.Contains(dr["EQUIPMENTMODELID"].ToString()))
                {
                    dic.Add(dr["EQUIPMENTMODELID"].ToString(), dr["typeName"].ToString());
                    DataRow drnew = dtnew.NewRow();
                    drnew[0] = dr["typeName"].ToString();
                    DataRow[] drs = dt.Select("EQUIPMENTMODELID='" + dr["EQUIPMENTMODELID"].ToString() + "'", " Consum asc,PROCESSUNITID asc");
                    int countQ = 5;
                    int countH = 5;
                    int countall = drs.Count();
                    if (countall < 10)
                    {
                        countQ = int.Parse(Math.Ceiling(countall / 2.0).ToString());
                        countH = int.Parse(Math.Floor(countall / 2.0).ToString());
                    }
                    for (int i = 0; i < countQ; i++)
                    {//前五
                        drnew[i + 1] = drs[i]["equName"].ToString();
                        drnew[i + 11] = Math.Round(decimal.Parse(drs[i]["Consum"].ToString()), 2).ToString();
                    }
                    DataRow[] drsh = dt.Select("EQUIPMENTMODELID='" + dr["EQUIPMENTMODELID"].ToString() + "'", " Consum desc,PROCESSUNITID asc");
                    for (int i = 0; i < countH; i++)
                    {//后五
                        drnew[i + 6] = drsh[i]["equName"].ToString();
                        drnew[i + 16] = Math.Round(decimal.Parse(drsh[i]["Consum"].ToString()), 2).ToString();
                    }

                    dtnew.Rows.Add(drnew);
                }
            }

            return JsonSMES(dtnew);
        }

	}
}