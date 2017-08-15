using SMES.AspNetFramework.MVC;
using SMES.AspNetFramework.Srv;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMES.AspNetFramework;
using SMES.Framework.EplDb;
using SMES.AspNetDTM.ICore.IPCM;

namespace SMES.AspNetDTM.UI.Areas.M.Controllers
{
    /// <summary>
    /// 功能：制丝生产效率
    /// 创建：杨玉海
    /// 日期：2016-5-13
    /// </summary>
    public class ZSProduceEfficiencyController : SMESController
    {
        //
        // GET: /M/ZSProEfficiency/
        public ActionResult Index()
        {
            ViewBag.Title = "制丝生产效率";
            ViewBag.Date = DateTime.Now.ToString("yyyy年MM月dd日");
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 大屏  天
        /// </summary>
        /// <returns></returns>
        public ActionResult LIndex_D()
        {
            ViewBag.Title = "制丝生产效率";
            ViewBag.Date = getDate(1);
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        /// <summary>
        /// 大屏  周
        /// </summary>
        /// <returns></returns>
        public ActionResult LIndex_W()
        {
            ViewBag.Title = "制丝生产效率";
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
            ViewBag.Title = "制丝生产效率";
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
        /// 获取主数据
        /// </summary>
        /// <param name="DateFlag">查询类型</param>
        /// <returns></returns>
        public JsonResult GetData(int DateFlag)
        {
            //获取开始查询时间
            string strToday = getDate(DateFlag);
            DataTable dt = SrvProxy.CreateServices<IZSProduceEfficiency>().QueryData(strToday);

            #region 定义存储空间
            DataTable dtnew = new DataTable();
            //生产线ID
            dtnew.Columns.Add("RESOURCEID");
            //工艺段ID
            dtnew.Columns.Add("PRODUCELINEID");
            //待料时间
            dtnew.Columns.Add("DLSJ");
            //换批次数
            dtnew.Columns.Add("HPICS");
            //换牌次数
            dtnew.Columns.Add("HPAICS");
            //工艺段产量(kg)
            dtnew.Columns.Add("GYDCL");
            //工艺段生产时间（h）
            dtnew.Columns.Add("SCSJ");
            //产能（kgh）
            dtnew.Columns.Add("CN");
            //准时化差异（min）
            dtnew.Columns.Add("ZSHCY");
            #endregion

            #region 整理数据格式写入存储空间
            string strProductID = "";        //牌号ID
            string strProducelineID = "";    //生产线
            string strStartTimeF = "";       //第一批开始时间
            string strStartTime = "";        //开始时间
            string strEndTime = "";          //结束时间
            string strResourceID = "";       //工艺段ID
            decimal times = 0;               //待料时间(分)      累计换批时间
            int hpics = 0;                   //换批次数
            int hpaics = 0;                  //换牌次数
            decimal QTY = 0;                 //工艺段总产量
            decimal timep = 0;               //总生产时间(小时)  工艺段生产结束时间-第一批投料开始时间
            decimal planTime = 0;            //理论时间
            foreach (DataRow dr in dt.Rows)
            {
                if (strResourceID == "")
                {
                    //初始化变量
                    strProducelineID = dr["PRODUCELINEID"] == null ? "" : dr["PRODUCELINEID"].ToString();
                    strProductID = dr["PRODUCTID"] == null ? "" : dr["PRODUCTID"].ToString();
                    strStartTimeF = dr["REALSTARTTIME"] == null ? "" : dr["REALSTARTTIME"].ToString();
                    strStartTime = dr["REALSTARTTIME"] == null ? "" : dr["REALSTARTTIME"].ToString();
                    strEndTime = dr["REALENDTIME"] == null ? "" : dr["REALENDTIME"].ToString();
                    strResourceID = dr["RESOURCEID"] == null ? "" : dr["RESOURCEID"].ToString();
                    times = 0; hpics = 0; hpaics = 0; planTime = 0;
                    if (dr["QUANTITY"] != null && dr["QUANTITY"].ToString() != "")
                    {//工艺段产量
                        QTY = decimal.Parse(dr["QUANTITY"].ToString());
                    }
                    if (strEndTime != "" && strStartTimeF != "")
                    {//总生产时间
                        if (DateTime.Parse(strEndTime) >= DateTime.Parse(strStartTimeF))
                        {
                            TimeSpan ts = DateTime.Parse(strEndTime) - DateTime.Parse(strStartTimeF);
                            timep = ts.Days * 24 + ts.Hours + decimal.Parse(ts.Minutes.ToString()) / 60 + decimal.Parse(ts.Seconds.ToString()) / 3600;
                        }
                    }
                }
                else if (dr["RESOURCEID"].ToString() == strResourceID)
                {//相同工艺段
                    if (strEndTime != "" && dr["REALSTARTTIME"] != null && dr["REALSTARTTIME"].ToString() != "")
                    {
                        //换批次数
                        hpics += 1;
                        planTime += decimal.Parse(dr["LLSJ"] == null ? "0" : dr["LLSJ"].ToString());
                        if (DateTime.Parse(dr["REALSTARTTIME"].ToString()) >= DateTime.Parse(strEndTime))
                        {//待料时间 
                            TimeSpan ts = DateTime.Parse(dr["REALSTARTTIME"].ToString()) - DateTime.Parse(strEndTime);
                            times += ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes + decimal.Parse(ts.Seconds.ToString()) / 60;
                        }
                        if (strProductID != "" && dr["PRODUCTID"] != null && dr["PRODUCTID"].ToString() != "")
                        {//换牌次数
                            if (strProductID != dr["PRODUCTID"].ToString())
                            {
                                hpaics += 1;
                            }
                        }
                        if (dr["QUANTITY"] != null && dr["QUANTITY"].ToString() != "")
                        {//工艺段产量
                            QTY += decimal.Parse(dr["QUANTITY"].ToString());
                        }
                    }
                    strStartTime = dr["REALSTARTTIME"] == null ? "" : dr["REALSTARTTIME"].ToString();
                    strEndTime = dr["REALENDTIME"] == null ? "" : dr["REALENDTIME"].ToString();
                    if (strEndTime != "" && strStartTimeF != "")
                    {//总生产时间
                        if (DateTime.Parse(strEndTime) >= DateTime.Parse(strStartTimeF))
                        {
                            TimeSpan ts = DateTime.Parse(strEndTime) - DateTime.Parse(strStartTimeF);
                            timep = ts.Days * 24 + ts.Hours + decimal.Parse(ts.Minutes.ToString()) / 60 + decimal.Parse(ts.Seconds.ToString()) / 3600;
                        }
                    }
                }
                else
                {//不同工艺段
                    //增加数据到统计表
                    //准时化差异= 实际结束时间-（当日第一批实际开始时间+理论用时+换批/换牌时间）
                    decimal ZSHCY = 0;
                    if (strEndTime != "" && strStartTimeF != "")
                    {
                        if (DateTime.Parse(strEndTime) >= DateTime.Parse(strStartTimeF))
                        {
                            TimeSpan ts = DateTime.Parse(strEndTime) - DateTime.Parse(strStartTimeF);
                            ZSHCY = ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes + decimal.Parse(ts.Seconds.ToString()) / 60;
                            ZSHCY = Math.Round((ZSHCY - times - planTime), 2, MidpointRounding.AwayFromZero);
                        }
                    }
                    decimal cn = 0;
                    if (timep != 0)
                    {
                        cn = Math.Round((QTY / timep), 2, MidpointRounding.AwayFromZero);
                    }
                    dtnew.Rows.Add(strResourceID, strProducelineID, Math.Round(times, 2, MidpointRounding.AwayFromZero), hpics, hpaics, QTY, timep, cn, ZSHCY);
                    //初始化变量
                    strProducelineID = dr["PRODUCELINEID"] == null ? "" : dr["PRODUCELINEID"].ToString();
                    strProductID = dr["PRODUCTID"] == null ? "" : dr["PRODUCTID"].ToString();
                    strStartTimeF = dr["REALSTARTTIME"] == null ? "" : dr["REALSTARTTIME"].ToString();
                    strStartTime = dr["REALSTARTTIME"] == null ? "" : dr["REALSTARTTIME"].ToString();
                    strEndTime = dr["REALENDTIME"] == null ? "" : dr["REALENDTIME"].ToString();
                    strResourceID = dr["RESOURCEID"] == null ? "" : dr["RESOURCEID"].ToString();
                    times = 0; hpics = 0; hpaics = 0; planTime = 0;
                    if (dr["QUANTITY"] != null && dr["QUANTITY"].ToString() != "")
                    {//工艺段产量
                        QTY = decimal.Parse(dr["QUANTITY"].ToString());
                    }
                    if (strEndTime != "" && strStartTimeF != "")
                    {//总生产时间
                        if (DateTime.Parse(strEndTime) >= DateTime.Parse(strStartTimeF))
                        {
                            TimeSpan ts = DateTime.Parse(strEndTime) - DateTime.Parse(strStartTimeF);
                            timep = ts.Days * 24 + ts.Hours + decimal.Parse(ts.Minutes.ToString()) / 60 + decimal.Parse(ts.Seconds.ToString()) / 3600;
                        }
                    }
                }
            }

            if (dtnew.Select(" RESOURCEID=" + strResourceID, "").Count()==0)
            {
                //增加最后一段数据到统计表
                //准时化差异= 实际结束时间-（当日第一批实际开始时间+理论用时+换批/换牌时间）
                decimal ZSHCY = 0;
                if (strEndTime != "" && strStartTimeF != "")
                {
                    if (DateTime.Parse(strEndTime) >= DateTime.Parse(strStartTimeF))
                    {
                        TimeSpan ts = DateTime.Parse(strEndTime) - DateTime.Parse(strStartTimeF);
                        ZSHCY = ts.Days * 24 * 60 + ts.Hours * 60 + ts.Minutes + decimal.Parse(ts.Seconds.ToString()) / 60;
                        ZSHCY = Math.Round((ZSHCY - times - planTime), 2, MidpointRounding.AwayFromZero);
                    }
                }
                decimal cn = 0;
                if (timep != 0)
                {
                    cn = Math.Round((QTY / timep - times), 2, MidpointRounding.AwayFromZero);
                }
                dtnew.Rows.Add(strResourceID, strProducelineID, Math.Round(times, 2, MidpointRounding.AwayFromZero), hpics, hpaics, QTY, timep, cn, ZSHCY);
            }
            #endregion

            return JsonSMES(dtnew);
        }
    }
}