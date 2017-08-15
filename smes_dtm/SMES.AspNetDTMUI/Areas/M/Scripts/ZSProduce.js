function setRoundChart() {

}

$(function () {
    resize();
    $(window).on("resize", resize);

    GetDayWoStatics();
    GetRunningWoSegments();
    StaticCutDayAndMonth();
})

function resize() {
    Scroll1();
    Scroll2();
}

function Scroll1() {
    //debugger;
    $("#scoll1").slimScroll({
        height: $(".left-module2").height() - $(".left-module2-title").height(),
        alwaysVisible: false,
        color: "#38393d",
        railColor: "#525357",
        railOpacity: 1,
        railVisible: true,
        disableFadeOut: true
    });    
}

function Scroll2() {
    //debugger;
    $("#scoll2").slimScroll({
        height: $(".main-content").height(),
        alwaysVisible: false,
        color: "#38393d",
        railColor: "#525357",
        railOpacity: 1,
        railVisible: true,
        disableFadeOut: true
    });
}

function refresh(btn_value)
{
    var productdate = $("#ProducteDate").html();
    productdate = productdate.replace("年", "-").replace("月", "-").replace("日", "");
    app.DataRequest("ChangeProductDate", { CurrentDate: productdate, DateFlag: btn_value }, function (data) {
        $('#ProducteWeek').html(data[0].WEEK );
        $('#ProducteDate').html(data[0].PRODUCTDATE);

        GetDayWoStatics();
        GetRunningWoSegments();
        StaticCutDayAndMonth();
    });
}

function GetDayWoStatics()
{
    var productdate = $("#ProducteDate").html();
    productdate = productdate.replace("年", "-").replace("月", "-").replace("日","");
    app.DataRequest('GetDayWoStatics', { code: 'GetDayWoStatics', strDate: productdate }, function (data) {
        debugger;
        for (var i = 0; i < data.length; i++) {
            switch (data[i].RESOURCEID) {
                case '681750':          //开包回潮大段Z
                    $('#zkbfinished').html(data[i].FINISHCOUNT);
                    $('#zkbtotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#zkbprogress").css('width', 0);
                    else
                        $("#zkbprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    break;
                case '681753':          //叶片加料大段Z
                    $('#ssbfinished').html(data[i].FINISHCOUNT);
                    $('#ssbtotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#ssbprogress").css('width', 0);
                    else
                        $("#ssbprogress").css("width", (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString()+"%");
                        
                    break;
                case '681754':          //切丝大段Z
                    $('#ypbfinished').html(data[i].FINISHCOUNT);
                    $('#ypbtotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#ypbprogress").css('width', 0);
                    else
                        $("#ypbprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");

                    $("#qlxplan").html(data[i].TOTALCOUNT);
                    $("#qlxfinish").html(data[i].FINISHCOUNT);
                    var v = Number(data[i].TOTALCOUNT) - Number(data[i].FINISHCOUNT) - 1;
                    $("#qlxunstart").html(v < 0 ? 0 : v);
                    break;
                case '681757':          //切片P
                    $('#zkafinished').html(data[i].FINISHCOUNT);
                    $('#zkatotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#zkaprogress").css('width', 0);
                    else
                        $("#hchsbprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    break;
                case '681759':          //叶片加料P
                    $('#cpjxbfinished').html(data[i].FINISHCOUNT);
                    $('#cpjxbtotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#cpjxbprogress").css('width', 0);
                    else
                        $("#cpjxbprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
   
                    //$("#qlxplan").html(data[i].TOTALCOUNT);
                    //$("#qlxfinish").html(data[i].FINISHCOUNT);
                    //var v = Number(data[i].TOTALCOUNT) - Number(data[i].FINISHCOUNT) - 1;
                    //$("#qlxunstart").html(v < 0 ? 0 : v);

                    //$("#gtxplan").html(data[i].TOTALCOUNT);
                    //$("#gtxfinish").html(data[i].FINISHCOUNT);
                    //var v = Number(data[i].TOTALCOUNT) - Number(data[i].FINISHCOUNT) - 1;
                    //$("#gtxunstart").html(v < 0 ? 0 : v);

                    break;
                case '681760':          //切丝P
                    $('#cpjxafinished').html(data[i].FINISHCOUNT);
                    $('#cpjxatotal').html(data[i].TOTALCOUNT);

                    if (data[0].TOTALCOUNT == "0")
                        $("#cpjxaprogress").css('width', 0);
                    else
                        $("#cpjxaprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");

                    $("#gtxplan").html(data[i].TOTALCOUNT);
                    $("#gtxfinish").html(data[i].FINISHCOUNT);
                    var v = Number(data[i].TOTALCOUNT) - Number(data[i].FINISHCOUNT) - 1;
                    $("#gtxunstart").html(v < 0 ? 0 : v);

                    break;
                //case '195441':          //加香A
                //    $('#cpjxafinished').html(data[i].FINISHCOUNT);
                //    $('#cpjxatotal').html(data[i].TOTALCOUNT);

                //    if (data[i].TOTALCOUNT == "0")
                //        $("#cpjxaprogress").css('width', 0);  
                //    else
                //        $("#cpjxaprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    
                //    $("#gtxplan").html(data[i].TOTALCOUNT);
                //    $("#gtxfinish").html(data[i].FINISHCOUNT);
                //    var v = Number(data[i].TOTALCOUNT) - Number(data[i].FINISHCOUNT) - 1;
                //    $("#gtxunstart").html(v < 0 ? 0 : v);
                //    break;
                case '681765':          //一次贮梗(水洗梗）P
                    $('#yczgfinished').html(data[i].FINISHCOUNT);
                    $('#yczgtotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#yczgprogress").css('width', 0);
                    else
                        $("#yczgprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    break;
                case '681766':          //二次贮梗（增温增湿）P
                    $('#eczgfinished').html(data[i].FINISHCOUNT);
                    $('#eczgtotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#eczgprogress").css('width', 0);
                    else
                        $("#eczgprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    break;
                case '681767':          //梗丝加料（切梗丝）P
                    $('#gsjlfinished').html(data[i].FINISHCOUNT);
                    $('#gsjltotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#gsjlprogress").css('width', 0);
                    else
                        $("#gsjlprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");

                    $("#gsxplan").html(data[i].TOTALCOUNT);
                    $("#gsxfinish").html(data[i].FINISHCOUNT);
                    var v = Number(data[i].TOTALCOUNT) - Number(data[i].FINISHCOUNT) - 1;
                    $("#gsxunstart").html(v < 0 ? 0 : v);
                    break;
                //case '681770':          //烘梗丝加香（干燥与膨胀、加香、贮梗）
                //    $('#gsjxfinished').html(data[i].FINISHCOUNT);
                //    $('#gsjxtotal').html(data[i].TOTALCOUNT);

                //    if (data[i].TOTALCOUNT == "0")
                //        $("#gsjxprogress").css('width', 0);
                //    else
                //        $("#gsjxprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");

                //    $("#gsxplan").html(data[i].TOTALCOUNT);
                //    $("#gsxfinish").html(data[i].FINISHCOUNT);
                //    var v = Number(data[i].TOTALCOUNT) - Number(data[i].FINISHCOUNT)-1;
                //    $("#gsxunstart").html(v < 0 ? 0 : v);
                //    break;
                default:
                    break;

            }
        }

    });

}

function GetRunningWoSegments()
{
    var productdate = $("#ProducteDate").html();
    productdate = productdate.replace("年", "-").replace("月", "-").replace("日", "");
    app.DataRequest('GetRunningWoSegments', { code: 'GetRunningWoSegments', strDate: productdate }, function (data) {
        debugger;
        for (var i = 0; i < data.length; i++) {
            switch (data[i].RESOURCEID) {
                case '681750':          //开包回潮大段Z
                    $('#zkbbrand').html(data[i].NAME);
                    $('#zkbwo').html(data[i].PLANCODE);
                    break;
                case '681753':          //叶片加料Z
                    $('#ssbbrand').html(data[i].NAME);
                    $('#ssbwo').html(data[i].PLANCODE);
                    break;
                case '681754':          //切丝Z
                    $('#ypbbrand').html(data[i].NAME);
                    $('#ypbwo').html(data[i].PLANCODE);
                    break;
                //case '681754':          //切丝Z
                //    $('#hchsbbrand').html(data[i].NAME);
                //    $('#hchsbwo').html(data[i].PLANCODE);

                //    break;
                case '681757':          //切片P
                    $('#zkabrand').html(data[i].NAME);
                    $('#zkawo').html(data[i].PLANCODE);
                    break;
                case '681759':          //叶片加料P
                    $('#cpjxbbrand').html(data[i].NAME);
                    $('#cpjxbwo').html(data[i].PLANCODE);
                    break;
                case '681760':          //切丝P
                    $('#cpjxabrand').html(data[i].NAME);
                    $('#cpjxawo').html(data[i].PLANCODE);
                    break;
                case '681765':          //一次贮梗G
                    $('#yczgbrand').html(data[i].NAME);
                    $('#yczgwo').html(data[i].PLANCODE);
                    break;
                case '681766':          //二次贮梗G
                    $('#eczgbrand').html(data[i].NAME);
                    $('#eczgwo').html(data[i].PLANCODE);
                    break;
                case '681767':          //切梗丝G
                    $('#gsjlbrand').html(data[i].NAME);
                    $('#gsjlwo').html(data[i].PLANCODE);
                    break;
                //case '681770':          //烘梗丝加香（干燥与膨胀、加香、贮梗）
                //    $('#gsjxbrand').html(data[i].NAME);
                //    $('#gsjxwo').html(data[i].PLANCODE);
                //    break;
                default:
                    break;

            }
        }

    });
}

function StaticCutDayAndMonth()
{
    var productdate = $("#ProducteDate").html();
    productdate = productdate.replace("年", "-").replace("月", "-").replace("日", "");
    app.DataRequest("StaticCutDayAndMonth", { code: "StaticCutDayAndMonth", strDate: productdate }, function (data) {
        debugger;
        if (data!=null) {
            $('#DayCutNum').html(data[0].DAYTOTAL);
            $('#MonthCutNum').html(data[0].MONTHTOTAL);
        }

    })

}



//function GetBacthList()
//{
//    app.DataRequest('GetTodayProduceBatchByDate', null, function (data) {
//        var index = 0;
//        $("#batchinfotmpl").tmpl(data, {
//            GetIndex: function () {
//                index++;
//                return "No." + index;
//            }
//        }).appendTo("#batchlist");

//        Scroll1();
//    });
//}

//function GetCurMonthMonthSumData() {
//    app.DataRequest('GetCurMonthMonthSumData', null, function (data) {
       
//        if (!data)
//            data = { COMPLETENUM: 0, PLANNUM: 0 };

//        $("#monthbatchtmpl").tmpl(data, {
//            getComplaterate: function (v1, v2) {
//                if (v2 == "0")
//                    return "0%";
//                else
//                    return (Number(v1) / Number(v2) * 100).toFixed(2) + "%";
//            }
//        }).appendTo("#monthbatch");
//    });
//}

//function GetToDayBatchSum()
//{
//    app.DataRequest("GetToDayBatchSum", null, function (data) {
//        $("#todayproducttmpl").tmpl(data, {
//            getrate: function (v1, v2) {
//                if (v2 == "0")
//                    return "0%";
//                else
//                    return (Number(v1) / Number(v2) * 100).toFixed(2) + "%";
//            }
//        }).appendTo("#todayproduct");
//    });
//}

//function GetToDayWorkTime() {
//    app.DataRequest("GetToDayWorkTime", null, function (data) {
//        $("#todaytimetmpl").tmpl(data, {
//            getrate: function (v1, v2) {
//                if (v2 == "0")
//                    return "0%";
//                else
//                    return (Number(v1) / Number(v2) * 100).toFixed(2) + "%";
//            }
//        }).appendTo("#todaytime");
//    });
//}

//function GetCurMonthBatchQLAndOutRate()
//{
//    app.DataRequest("GetCurMonthBatchQLAndOutRate", null, function (data) {
//        $("#score").html(data.QLSCORE);

//        //outrate;

//        if (data.INPUTQTY == "0")
//            $("#outrate").html("0%");
//        else
//            $("#outrate").html((Number(data.OUTPUTQTY) / Number(data.INPUTQTY)*100).toFixed(2));
//    });
//}

//function GetTodayLeafBatchSumData()
//{
//    app.DataRequest("GetTodayLeafBatchSumData", null, function (data) {
//        $("#leafbatchtmpl").tmpl(data, {
//            getComplaterate: function (v1, v2) {
//                if (v2 == "0")
//                    return "0%";
//                else
//                    return (Number(v1) / Number(v2) * 100).toFixed(2) + "%";
//            }
//        }).appendTo("#leafbatch");
//    })
//}

//function GetTodayCutBatchSumData() {
//    app.DataRequest("GetTodayCutBatchSumData", null, function (data) {
//        $("#leafbatchtmpl").tmpl(data, {
//            getComplaterate: function (v1, v2) {
//                if (v2 == "0")
//                    return "0%";
//                else
//                    return (Number(v1) / Number(v2) * 100).toFixed(2) + "%";
//            }
//        }).appendTo("#cutbatch");
//    })
//}