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
    public class JBProduceQDController : SMESController
    {
        //
        // GET: /M/JBProduceQD/
        public ActionResult Index()
        {
            ViewBag.Title = "卷包生产看板";
            ViewBag.Date = DateTime.Now.ToString("yyyy年MM月dd日");
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 大屏 天（最近有数据的一天）
        /// </summary>
        /// <returns></returns>
        public ActionResult LIndex_D()
        {
            ViewBag.Title = "卷包生产看板";
            ViewBag.Date = getDate(1);
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 大屏 周
        /// </summary>
        /// <returns></returns>
        public ActionResult LIndex_W()
        {
            ViewBag.Title = "卷包生产看板";
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
            ViewBag.Title = "卷包生产看板";
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
            string strDate = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd");
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

        List<SQLParameter> paras = new List<SQLParameter>();

        public JsonResult GetEquModelList(string code, string EquTypeNm)
        {
            DataSet ds = new DataSet();
            paras.Clear();
            paras.Add(new SQLParameter { DbType = DbType.String, Name = "@TypeName", Value = EquTypeNm });
            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);

            if (ds == null || ds.Tables.Count == 0)
                return null;

            return JsonSMES(ds.Tables[0]);
        }

        public JsonResult GetEqusOutputList(string code, int EquModelId, string EquModelNm, string StartDate, string EndDate)
        {
            paras.Clear();
            string[,] rangeList = new string[5, 2];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    rangeList[i, j] = "";
                }
            }

            DataSet ds = new DataSet();
            paras.Add(new SQLParameter { DbType = DbType.Int32, Name = "@EquModelId", Value = EquModelId });
            paras.Add(new SQLParameter { DbType = DbType.String, Name = "@StartDate", Value = StartDate });
            paras.Add(new SQLParameter { DbType = DbType.String, Name = "@EndDate", Value = EndDate });

            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);
            if (ds != null && ds.Tables.Count > 0)
            {
                int rows = ds.Tables[0].Rows.Count;
                for (int i = 0; i < rows / 2 && i < 5 && i < rows - i - 1; i++)
                {
                    rangeList[i, 0] = ds.Tables[0].Rows[i]["SHORTNAME"].ToString();
                    rangeList[i, 1] = ds.Tables[0].Rows[rows - i - 1]["SHORTNAME"].ToString();
                }
            }

            var data = new
            {
                EquModelNm = EquModelNm,
                StartFirst = rangeList[0, 0],
                EndFirst = rangeList[0, 1],
                StartSecond = rangeList[1, 0],
                EndSecond = rangeList[1, 1],
                StartThird = rangeList[2, 0],
                EndThird = rangeList[2, 1],
                StartFourth = rangeList[3, 0],
                EndtFourth = rangeList[3, 1],
                StartFifty = rangeList[4, 0],
                EndFifty = rangeList[4, 1],
            };

            return JsonSMES(data);
        }

        public JsonResult GetEqusOutputRankingList(string code1, string EquTypeNm, string code2, string StartDate, string EndDate)
        {
            DataSet ds1 = new DataSet();
            paras.Clear();
            paras.Add(new SQLParameter { DbType = DbType.String, Name = "@TypeName", Value = EquTypeNm });
            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code1, paras, out ds1);

            if (ds1 == null || ds1.Tables.Count == 0)
                return null;
            DataTable dt = ds1.Tables[0];

            paras.Clear();
            DataTable resultDt = new DataTable();
            resultDt.Columns.Add("EquModelNm", typeof(string));
            resultDt.Columns.Add("StartFirst", typeof(string));
            resultDt.Columns.Add("StartSecond", typeof(string));
            resultDt.Columns.Add("StartThird", typeof(string));
            resultDt.Columns.Add("StartFourth", typeof(string));
            resultDt.Columns.Add("StartFifty", typeof(string));
            resultDt.Columns.Add("EndFirst", typeof(string));
            resultDt.Columns.Add("EndSecond", typeof(string));
            resultDt.Columns.Add("EndThird", typeof(string));
            resultDt.Columns.Add("EndtFourth", typeof(string));
            resultDt.Columns.Add("EndFifty", typeof(string));

            resultDt.Columns.Add("StartFirstQTY", typeof(string));
            resultDt.Columns.Add("StartSecondQTY", typeof(string));
            resultDt.Columns.Add("StartThirdQTY", typeof(string));
            resultDt.Columns.Add("StartFourthQTY", typeof(string));
            resultDt.Columns.Add("StartFiftyQTY", typeof(string));
            resultDt.Columns.Add("EndFirstQTY", typeof(string));
            resultDt.Columns.Add("EndSecondQTY", typeof(string));
            resultDt.Columns.Add("EndThirdQTY", typeof(string));
            resultDt.Columns.Add("EndtFourthQTY", typeof(string));
            resultDt.Columns.Add("EndFiftyQTY", typeof(string));

            if (dt == null || dt.Rows.Count == 0)
            {
                return JsonSMES(resultDt);
            }

            for (int a = 0; a < dt.Rows.Count; a++)
            {

                string[,] rangeList = new string[5, 4];
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        rangeList[i, j] = "";
                    }
                }

                DataSet ds = new DataSet();
                paras.Add(new SQLParameter { DbType = DbType.Int32, Name = "@EquModelId", Value = Convert.ToInt32(dt.Rows[a]["ID"]) });
                paras.Add(new SQLParameter { DbType = DbType.String, Name = "@StartDate", Value = StartDate });
                paras.Add(new SQLParameter { DbType = DbType.String, Name = "@EndDate", Value = EndDate });

                SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code2, paras, out ds);
                paras.Clear();
                if (ds != null && ds.Tables.Count > 0)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        resultDt.Rows.Add(resultDt.NewRow());
                    }
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (!dic.Keys.Contains(dt.Rows[a]["NAME"].ToString()))
                        {
                            dic.Add(dt.Rows[a]["NAME"].ToString(), dt.Rows[a]["NAME"].ToString());
                            DataRow drnew = resultDt.NewRow();
                            drnew[0] = dt.Rows[a]["NAME"].ToString();
                            DataRow[] drs = ds.Tables[0].Select("", " TOTAL desc,SHORTNAME asc");
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
                                drnew[i + 1] = drs[i]["SHORTNAME"].ToString();
                                //if (EquTypeNm == "%包装机%")
                                //{
                                    drnew[i + 11] = Math.Round(decimal.Parse(drs[i]["TOTAL"].ToString()), 0, MidpointRounding.AwayFromZero);
                                //}
                                //else
                                //{
                                //    drnew[i + 11] = Math.Round(decimal.Parse(drs[i]["TOTAL"].ToString()), 2, MidpointRounding.AwayFromZero);
                                //}
                            }
                            DataRow[] drsh = ds.Tables[0].Select("", " TOTAL asc,SHORTNAME desc");
                            for (int i = 0; i < countH; i++)
                            {//后五
                                drnew[i + 6] = drsh[i]["SHORTNAME"].ToString();
                                //if (EquTypeNm == "%包装机%")
                                //{
                                    drnew[i + 16] = Math.Round(decimal.Parse(drsh[i]["TOTAL"].ToString()), 0, MidpointRounding.AwayFromZero);
                                //}
                                //else
                                //{
                                //    drnew[i + 16] = Math.Round(decimal.Parse(drsh[i]["TOTAL"].ToString()), 2, MidpointRounding.AwayFromZero);
                                //}
                            }
                            resultDt.Rows.Add(drnew);
                        }
                    }

                }
            }
            return JsonSMES(resultDt);
        }

        public JsonResult GetPackPeriodOutput(string code, string start, string end)
        {
            DataSet ds = new DataSet();
            paras.Clear();
            paras.Add(new SQLParameter { DbType = DbType.String, Name = "@SARTDT", Value = start });
            paras.Add(new SQLParameter { DbType = DbType.String, Name = "@ENDDT", Value = end });
            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);

            if (ds == null || ds.Tables.Count == 0)
                return null;

            return JsonSMES(ds.Tables[0]);
        }

        public ActionResult JBProduceEff()
        {
            return View();
        }
	}
}