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
    public class JBProduceController : SMESController
    {
        // GET: M/JBProduce
        public ActionResult Index()
        {
            ViewBag.Title = "卷包生产看板";
            ViewBag.Date = DateTime.Now.ToString("yyyy年MM月dd日");
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }
        List<SQLParameter> paras = new List<SQLParameter>();

        public JsonResult GetEquModelList(string code, string EquTypeNm)
        {
            DataSet ds = new DataSet();
            paras.Clear();
            paras.Add(new SQLParameter { DbType = DbType.String, Name = "@TypeName", Value = EquTypeNm });
            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);

            if (ds==null||ds.Tables.Count==0)
                return null;

            return JsonSMES(ds.Tables[0]);     
        }

        public JsonResult GetEqusOutputList(string code, int EquModelId,string EquModelNm,string StartDate,string EndDate)
        {
            paras.Clear();
            string[,] rangeList=new string[5,2];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    rangeList[i, j] = "";
                }    
            }

            DataSet ds = new DataSet();
            paras.Add(new SQLParameter { DbType = DbType.Int32, Name = "@EquModelId",Value=EquModelId });
            paras.Add(new SQLParameter { DbType = DbType.String, Name = "@StartDate", Value = StartDate });
            paras.Add(new SQLParameter { DbType = DbType.String, Name = "@EndDate", Value = EndDate });

            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);
            if (ds!=null && ds.Tables.Count>0 )
            {
                int rows=ds.Tables[0].Rows.Count;
                for (int i = 0; i < rows/2&& i<5 && i<rows-i-1; i++)
                {
                    rangeList[i,0]=ds.Tables[0].Rows[i]["SHORTNAME"].ToString();
                    rangeList[i,1] = ds.Tables[0].Rows[rows-i-1]["SHORTNAME"].ToString();
                }
            }

            var data = new
            {
                EquModelNm=EquModelNm,
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


            if (dt==null||dt.Rows.Count==0)
            {
                return JsonSMES(resultDt);
            }

            for (int a = 0; a < dt.Rows.Count; a++)
            {
                
                string[,] rangeList = new string[5, 2];
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 2; j++)
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
                    int rows = ds.Tables[0].Rows.Count;
                    DataRow dr = resultDt.NewRow();
                    for (int i = 0; i < rows / 2 && i < 5 && i < rows - i - 1; i++)
                    {
                        rangeList[i, 0] = ds.Tables[0].Rows[i]["SHORTNAME"].ToString();
                        rangeList[i, 1] = ds.Tables[0].Rows[rows - i - 1]["SHORTNAME"].ToString();
                    }
                   
                    dr["EquModelNm"]  = dt.Rows[a]["NAME"].ToString();
                    dr["StartFirst"]  = rangeList[0, 0];
                    dr["EndFirst"]    = rangeList[0, 1];
                    dr["StartSecond"] = rangeList[1, 0];
                    dr["EndSecond"]   = rangeList[1, 1];
                    dr["StartThird"]  = rangeList[2, 0];
                    dr["EndThird"]    = rangeList[2, 1];
                    dr["StartFourth"] = rangeList[3, 0];
                    dr["EndtFourth"]  = rangeList[3, 1];
                    dr["StartFifty"]  = rangeList[4, 0];
                    dr["EndFifty"]    = rangeList[4, 1];
                    resultDt.Rows.Add(dr);

                }
            }   
            return JsonSMES(resultDt);
        }

        public JsonResult GetPackPeriodOutput(string code, string start,string end)
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