using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetDTM.ICore.ProduceAncestry
{
    [InheritedExport]
    public interface ICutProduceAncestry
    {

        /// <summary>
        /// 获取批次信息列表
        /// </summary>
        /// <param name="plandate">计划日期</param>
        /// <param name="productkey">关键字</param>
        /// <returns></returns>
        DataTable QueryCutBatchInfo(DateTime plandate, string productkey);


        /// <summary>
        /// 获取批次信息列表（逆向追踪）
        /// </summary>
        /// <param name="plandate">计划日期</param>
        /// <param name="team">班组</param>
        /// <param name="product">牌号</param>
        /// <param name="stamp">钢印号</param>
        /// <param name="codeone">一号工程码</param>
        /// <param name="type">查询类型（0：按时间，1：按钢印号，2：按一号工程码）</param>
        /// <returns></returns>
        DataTable QueryPackBatchInfo(DateTime plandate, string team, string product, string stamp, string codeone, int type);

        /// <summary>
        /// 获取牌号信息列表（逆向追踪）
        /// </summary>
        /// <param name="plandate">计划日期</param>
        /// <returns></returns>
        DataTable QueryPackMatInfo(DateTime plandate);

        /// <summary>
        /// 获取统计信息 （逆向追踪）
        /// </summary>
        /// <param name="plancode">作业号</param>
        /// <param name="teamid">班组id</param>
        /// <param name="plandate">生产日期</param>
        /// <returns></returns>
        DataTable QueryPackSumInfo(string plancode, string teamid, string plandate);

        /// <summary>
        /// 获取卷包设备详细信息
        /// </summary>
        /// <param name="plancode">作业号</param>
        /// <param name="teamid">班组id</param>
        /// <param name="plandate">生产日期</param>
        /// <param name="typeid">消耗类型</param>
        /// <param name="pronm">机台名称</param>
        /// <returns></returns>
        DataTable QueryEQUInfo(string plancode, string teamid, string plandate, int typeid, string pronm);

        /// <summary>
        /// 获取工单信息 （逆向追踪）
        /// </summary>
        /// <param name="plancode">作业号</param>
        /// <param name="teamid">班组id</param>
        /// <param name="plandate">生产日期</param>
        /// <returns></returns>
        DataTable QueryPackWoInfo(string plancode, string teamid, string plandate);

        /// <summary>
        /// 查询喂丝机详细信息
        /// </summary>
        /// <param name="plancode">计划号</param>
        /// <param name="teamid">班组ID</param>
        /// <param name="plandate">生产日期</param>
        /// <param name="feedwo">喂丝机工单</param>
        /// <returns></returns>
        DataTable QueryFeedWoInfo(string plancode, string teamid, string plandate, string feedwo);

        /// <summary>
        /// 根据批次id获取工单列表
        /// </summary>
        /// <param name="batchid"></param>
        /// <returns></returns>
        DataTable QueryWorkOrderOutPut(string batchid);

        /// <summary>
        /// 根据工单id获取工单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DataTable GetCutWorkOrder(string id);

        /// <summary>
        /// 根据工单id，出入柜类型，获取出入柜信息
        /// </summary>
        /// <param name="woid"></param>
        /// <param name="typeid"></param>
        /// <returns></returns>
        DataTable GetMaterialTransferinoutsilo(string woid, int typeid);


        /// <summary>
        /// 根据工单id得到工单消耗信息
        /// </summary>
        /// <param name="woid"></param>
        /// <returns></returns>
        DataTable GetCutWorkorderConsume(string woid);


        /// <summary>
        /// 根据工单ID获得工单关键参数信息
        /// </summary>
        /// <param name="woid"></param>
        /// <returns></returns>
        DataTable GetCutAncestryKeyParamData(string woid);

        /// <summary>
        /// 根据工单号得分情况
        /// </summary>
        /// <param name="woid">工单号</param>
        /// <returns></returns>
        DataTable GetCutBatchSorce(string woid);

        /// <summary>
        /// 得到本月同牌号得分
        /// </summary>
        /// <param name="productid"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        DataTable GetProductMonthCutBatchSorce(string woid);


        /// <summary>
        /// 得到所有牌号得分
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        DataTable GetAllProductMonthCutBatchSorce(string woid);


        /// <summary>
        /// 工单参数检验情况
        /// </summary>
        /// <param name="woid"></param>
        /// <returns></returns>
        DataTable GetCutWorkOrderPramQL(string woid);

        /// <summary>
        /// 本月同牌号参数检验
        /// </summary>
        /// <param name="woid"></param>
        /// <returns></returns>
        DataTable GetCutWorkProductOrderPramQL(string woid);


        /// <summary>
        /// 本月所有牌号参数检验
        /// </summary>
        /// <param name="woid"></param>
        /// <returns></returns>
        DataTable GetCutWorkOrderAllProductPramQL(string woid);

        /// <summary>
        /// 得到工单参数得分
        /// </summary>
        /// <param name="woid"></param>
        /// <returns></returns>
        DataTable GetAllWorkorderPramQL(string woid);
    }
}
