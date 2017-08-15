using SMES.AspNetDTM.ICore.Pub;
using SMES.AspNetFramework.MVC;
using SMES.AspNetFramework.Srv;
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
    public class ZsCutStructuralAnalysisController : SMESController
    {
        // GET: M/ZsCutStructuralAnalysis
        public ActionResult Index()
        {
            ViewBag.Title = "烟丝结构分析看板";
            ViewBag.Date = DateTime.Now.ToString("yyyy年MM月dd日");
            ViewBag.Week = DataConvert.Change(DateTime.Now.DayOfWeek);
            return View();
        }

        List<SQLParameter> paras = new List<SQLParameter>();
        public JsonResult QueryProductionTeam(string code)
        {
            DataSet ds = new DataSet();
            paras.Clear();
            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);

            if (ds == null || ds.Tables.Count == 0)
                return null;

            return JsonSMES(ds.Tables[0]);
        }

        public JsonResult GetCutBrandList(string code)
        {
            DataSet ds = new DataSet();
            paras.Clear();
            SrvProxy.CreateServices<ISqlCommandConfigService>().Query(code, paras, out ds);

            if (ds == null || ds.Tables.Count == 0)
                return null;

            return JsonSMES(ds.Tables[0]);
        }

        public JsonResult GetBrandsCutStructData(string startdt,string enddt,string paraid,string[] brandlist,string[] teamlist)
        {
            DataSet ds = new DataSet();
            string strBrandlist = "";
            string strTeamlist = "";
            for (int i = 0; i < brandlist.Length; i++)
            {
                strBrandlist += brandlist[i] + ",";
            }
            strBrandlist = strBrandlist.Substring(strBrandlist.Length - 1, 1) == "," ? strBrandlist.Remove(strBrandlist.Length - 1) : strBrandlist;

            for (int i = 0; i < teamlist.Length; i++)
            {
                strTeamlist += teamlist[i] + ",";
            }
            strTeamlist = strTeamlist.Substring(strTeamlist.Length - 1, 1) == "," ? strTeamlist.Remove(strTeamlist.Length - 1) : strTeamlist;

            switch (paraid)
            {
                case "917085":
                    paraid = paraid + ",1117902";
                    break;
                case "917086":
                    paraid = paraid + ",1117900";
                    break;
                case "917087":
                    paraid = paraid + ",1117890";
                    break;
                case "917088":
                    paraid = paraid + ",1117901";
                    break;
                case "627074":
                    paraid = paraid + ",895065";
                    break;
                default:
                    break;
            }

            string strSqlCmd = string.Format(@"select a.productid ID,a.productname NAME,round(avg(a.value),2) AVGNUM from
                                (select w.workordercode,w.teamid,w.teamname,c.productid,c.productname,p.parameterid,p.parametername,s.value,i.inspecttime
                                 from qua.workorderinspectorderdm w 
                                 left join qua.taskinspectorderdm t on t.workorderinspectorderid=w.id and t.inspectiontypeid=5
                                 left join qua.parainspectorderdm p on p.taskinspectorderid=t.id
                                 left join qua.inspectorderdm i on i.parainspectorderid=p.id
                                 left join qua.statistictermresultdm  s on s.inspectorderid=i.id
                                 left join dps.cutworkorder c on w.workordercode=c.plancode
                                 where p.parameterid in ({0}) and w.teamid in ({1})
                                 and c.productid in ({2})) a 
                                 where  to_char(a.inspecttime,'yyyy-MM-dd')between '{3}' and '{4}'
                                 group by a.productid,a.productname order by a.productid", paraid, strTeamlist, strBrandlist, startdt, enddt);

            SrvProxy.CreateServices<ISqlCommandConfigService>().QuerySqlCommand(strSqlCmd,out ds);

            if (ds == null || ds.Tables.Count == 0)
                return null;

            return JsonSMES(ds.Tables[0]);
        }

        public JsonResult GetDatesCutStructData(string startdt, string enddt, string paraid, string[] brandlist, string[] teamlist)
        {
            DataSet ds = new DataSet();
            string strBrandlist = "";
            string strTeamlist = "";
            for (int i = 0; i < brandlist.Length; i++)
            {
                strBrandlist +=  brandlist[i] + ",";
            }
            strBrandlist = strBrandlist.Substring(strBrandlist.Length - 1, 1) == "," ? strBrandlist.Remove(strBrandlist.Length - 1) : strBrandlist;

            for (int i = 0; i < teamlist.Length; i++)
            {
                strTeamlist +=  teamlist[i] + ",";
            }
            strTeamlist = strTeamlist.Substring(strTeamlist.Length - 1, 1) == "," ? strTeamlist.Remove(strTeamlist.Length - 1) : strTeamlist;

            switch (paraid)
            {
                case "917085":
                    paraid = paraid + ",1117902";
                    break;
                case "917086":
                    paraid = paraid + ",1117900";
                    break;
                case "917087":
                    paraid = paraid + ",1117890";
                    break;
                case "917088":
                    paraid = paraid + ",1117901";
                    break;
                case "627074":
                    paraid = paraid + ",895065";
                    break;
                default:
                    break;
            }

            string strSqlCmd = string.Format(@"select a.bussidate DATES,round(avg(a.value),2) AVGNUM from
                                              (select w.workordercode,w.teamid,w.teamname,c.productid,c.productname,p.parameterid,p.parametername,s.value,
                                              to_char(i.inspecttime,'yyyy-MM-dd') bussidate
                                              from qua.workorderinspectorderdm w 
                                              left join qua.taskinspectorderdm t on t.workorderinspectorderid=w.id and t.inspectiontypeid=5
                                              left join qua.parainspectorderdm p on p.taskinspectorderid=t.id
                                              left join qua.inspectorderdm i on i.parainspectorderid=p.id
                                              left join qua.statistictermresultdm  s on s.inspectorderid=i.id
                                              left join dps.cutworkorder c on w.workordercode=c.plancode 
                                              where p.parameterid in ({0}) and w.teamid in ({1})
                                              and c.productid in ({2})) a 
                                              where  a.bussidate between '{3}' and '{4}' 
                                              group by a.bussidate order by a.bussidate", paraid, strTeamlist, strBrandlist, startdt, enddt);

            SrvProxy.CreateServices<ISqlCommandConfigService>().QuerySqlCommand(strSqlCmd, out ds);

            if (ds == null || ds.Tables.Count == 0)
                return null;

            return JsonSMES(ds.Tables[0]);
        }

        public JsonResult GetCutStructData(string startdt, string enddt, string[] brandlist, string[] teamlist)
        {
            DataSet ds = new DataSet();
            string strBrandlist = "";
            string strTeamlist = "";
            for (int i = 0; i < brandlist.Length; i++)
            {
                strBrandlist += brandlist[i] + ",";
            }
            strBrandlist = strBrandlist.Substring(strBrandlist.Length - 1, 1) == "," ? strBrandlist.Remove(strBrandlist.Length - 1) : strBrandlist;

            for (int i = 0; i < teamlist.Length; i++)
            {
                strTeamlist += teamlist[i] + ",";
            }
            strTeamlist = strTeamlist.Substring(strTeamlist.Length - 1, 1) == "," ? strTeamlist.Remove(strTeamlist.Length - 1) : strTeamlist;

            string strSqlCmd = string.Format(@"select plancode LOT,workordercode WO,teamname TEAM,productname MAT,to_char(bussidate,'yyyy-MM-dd') BUSSIDT,
                                               cjdval CJD,zslval ZSL,tczval TCZ,sslval SSL,sfval SF from qua.view_zhjy_data dt
                                               where  dt.teamid in ({0}) and dt.productid in ({1}) 
                                               and to_char(dt.bussidate,'yyyy-MM-dd') between '{2}' and '{3}'
                                               order by  plancode,bussidate", strTeamlist, strBrandlist, startdt, enddt);
            SrvProxy.CreateServices<ISqlCommandConfigService>().QuerySqlCommand(strSqlCmd, out ds);
            if (ds == null || ds.Tables.Count == 0)
                return null;

            return JsonSMES(ds.Tables[0]);
        }
    }
}