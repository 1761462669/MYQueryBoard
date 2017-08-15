function setRoundChart() {

}

$(function () {
    resize();
    $(window).on("resize", resize);

    GetDayWoStatics();
    GetRunningWoSegments();
    StaticCutDayAndMonth();
    GetDayWoStaticsTeam();
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
        GetDayWoStaticsTeam();
    });
}

function GetDayWoStatics()
{
    var productdate = $("#ProducteDate").html();
    productdate = productdate.replace("年", "-").replace("月", "-").replace("日","");
    app.DataRequest('GetDayWoStatics', { code: 'GetDayWoStatics', strDate: productdate }, function (data) {

        for (var i = 0; i < data.length; i++) {
            switch (data[i].RESOURCEID) {
                //切片-9K  
                case '2187':         
                    $('#9kqpfinished').html(data[i].FINISHCOUNT);
                    $('#9kqptotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#9kqpprogress").css('width', 0);
                    else
                        $("#9kqpprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    break;
                //叶片加料-9K  
                case '2192':
                    $('#9kjlfinished').html(data[i].FINISHCOUNT);
                    $('#9kjltotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#9kjlprogress").css('width', 0);
                    else
                        $("#9kjlprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    break;
                    //切叶丝-9k  
                //case '2195':
                //    $('#9kqsfinished').html(data[i].FINISHCOUNT);
                //    $('#9kqstotal').html(data[i].TOTALCOUNT);

                //    if (data[i].TOTALCOUNT == "0")
                //        $("#9kqsprogress").css('width', 0);
                //    else
                //        $("#9kqsprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                //    break;
                    //烘叶丝-9k  
                case '2199':
                    $('#9khsfinished').html(data[i].FINISHCOUNT);
                    $('#9khstotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#9khsprogress").css('width', 0);
                    else
                        $("#9khsprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    break;
                    //掺配加香-9K 
                case '2204':
                    $('#9kcpfinished').html(data[i].FINISHCOUNT);
                    $('#9kcptotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#9kcpprogress").css('width', 0);
                    else
                        $("#9kcpprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");

                    $("#9kplan").html(data[i].TOTALCOUNT);
                    $("#9kfinish").html(data[i].FINISHCOUNT);
                    var v = Number(data[i].TOTALCOUNT) - Number(data[i].FINISHCOUNT) - 1;
                    $("#9kunstart").html(v < 0 ? 0 : v);
                    break;
                    //烟丝入库-9K
                case '2211':
                    $('#9krkfinished').html(data[i].FINISHCOUNT);
                    $('#9krktotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#9krkprogress").css('width', 0);
                    else
                        $("#9krkprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");

                    break;
                    //真空回潮-5K
                case '2120':
                    $('#5khcfinished').html(data[i].FINISHCOUNT);
                    $('#5khctotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#5khcprogress").css('width', 0);
                    else
                        $("#5khcprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    break;
                    //切片-5K
                case '2125':
                    $('#5kqpfinished').html(data[i].FINISHCOUNT);
                    $('#5kqptotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#5kqpprogress").css('width', 0);
                    else
                        $("#5kqpprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    break;
                    //叶片加料-5K
                case '2128':
                    $('#5kjlfinished').html(data[i].FINISHCOUNT);
                    $('#5kjltotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#5kjlprogress").css('width', 0);
                    else
                        $("#5kjlprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    break;
                    //切叶丝-5k
                //case '2132':
                //    $('#5ksfinished').html(data[i].FINISHCOUNT);
                //    $('#5ksptotal').html(data[i].TOTALCOUNT);

                //    if (data[i].TOTALCOUNT == "0")
                //        $("#5ksprogress").css('width', 0);
                //    else
                //        $("#5ksprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                //    break;
                    //烘叶丝-5k
                case '2136':
                    $('#5khsfinished').html(data[i].FINISHCOUNT);
                    $('#5khstotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#5khsprogress").css('width', 0);
                    else
                        $("#5khsprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    break;
                    //掺配-5K
                case '2140':
                    $('#5kcpfinished').html(data[i].FINISHCOUNT);
                    $('#5kcptotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#5kcpprogress").css('width', 0);
                    else
                        $("#5kcpprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    break;
                    //加香-5K
                case '2145':
                    $('#5kjxfinished').html(data[i].FINISHCOUNT);
                    $('#5kjxtotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#5kjxprogress").css('width', 0);
                    else
                        $("#5kjxprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");

                    $("#5kplan").html(data[i].TOTALCOUNT);
                    $("#5kfinish").html(data[i].FINISHCOUNT);
                    var v = Number(data[i].TOTALCOUNT) - Number(data[i].FINISHCOUNT) - 1;
                    $("#5kunstart").html(v < 0 ? 0 : v);
                    break;
                    //烟丝入库-5K
                case '2148':
                    $('#5krkfinished').html(data[i].FINISHCOUNT);
                    $('#5krktotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#5krkprogress").css('width', 0);
                    else
                        $("#5krkprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");

                    break;
                    //切片-3K
                case '2149':
                    $('#3kqpfinished').html(data[i].FINISHCOUNT);
                    $('#3kqptotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#3kqpprogress").css('width', 0);
                    else
                        $("#3kqpprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    break;
                    //叶片加料-3K
                case '2154':
                    $('#3kjlfinished').html(data[i].FINISHCOUNT);
                    $('#3kjltotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#3kjlprogress").css('width', 0);
                    else
                        $("#3kjlprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    break;
                    //配片预混出柜-3K
                case '2158':
                    $('#3kyhfinished').html(data[i].FINISHCOUNT);
                    $('#3kyhtotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#3kyhprogress").css('width', 0);
                    else
                        $("#3kyhprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    break;
                    //切叶丝-3K
                case '2159':
                    //$('#3kqsfinished').html(data[i].FINISHCOUNT);
                    //$('#3kqstotal').html(data[i].TOTALCOUNT);

                    //if (data[i].TOTALCOUNT == "0")
                    //    $("#3kqsprogress").css('width', 0);
                    //else
                    //    $("#3kqsprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");

                    $("#3kplan").html(data[i].TOTALCOUNT);
                    $("#3kfinish").html(data[i].FINISHCOUNT);
                    var v = Number(data[i].TOTALCOUNT) - Number(data[i].FINISHCOUNT) - 1;
                    $("#3kunstart").html(v < 0 ? 0 : v);
                    break;
                    //滚筒烘丝-3K
                case '2163':
                    $('#3kgtfinished').html(data[i].FINISHCOUNT);
                    $('#3kgttotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#3kgtprogress").css('width', 0);
                    else
                        $("#3kgtprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    
                    break;
                    //气流烘丝-3K
                case '2168':
                    $('#3kqlfinished').html(data[i].FINISHCOUNT);
                    $('#3kqltotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#3kqlprogress").css('width', 0);
                    else
                        $("#3kqlprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    break;
                    //梗预处理
                case '2173':
                    $('#gyclfinished').html(data[i].FINISHCOUNT);
                    $('#gycltotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#gyclprogress").css('width', 0);
                    else
                        $("#gyclprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    break;
                    //切梗
                case '2177':
                    $('#qgfinished').html(data[i].FINISHCOUNT);
                    $('#qgtotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#qgprogress").css('width', 0);
                    else
                        $("#qgprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");
                    break;
                    //烘梗
                case '2180':
                    $('#hgfinished').html(data[i].FINISHCOUNT);
                    $('#hgtotal').html(data[i].TOTALCOUNT);

                    if (data[i].TOTALCOUNT == "0")
                        $("#hgprogress").css('width', 0);
                    else
                        $("#hgprogress").css('width', (Number(data[i].FINISHCOUNT) / Number(data[i].TOTALCOUNT) * 100).toFixed(2).toString() + "%");

                    $("#gsxplan").html(data[i].TOTALCOUNT);
                    $("#gsxfinish").html(data[i].FINISHCOUNT);
                    var v = Number(data[i].TOTALCOUNT) - Number(data[i].FINISHCOUNT) - 1;
                    $("#gsxunstart").html(v < 0 ? 0 : v);
                    break;

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
        for (var i = 0; i < data.length; i++) {
            switch (data[i].RESOURCEID) {
                    //切片-9K  
                case '2187':
                    $('#9kqpbrand').html(data[i].NAME);
                    $('#9kqpwo').html(data[i].PLANCODE);
                    break;
                    //叶片加料-9K  
                case '2192':
                    $('#9kjlbrand').html(data[i].NAME);
                    $('#9kjlwo').html(data[i].PLANCODE);
                    break;
                    //切叶丝-9k  
                case '2195':
                    $('#9kqsbrand').html(data[i].NAME);
                    $('#9kqswo').html(data[i].PLANCODE);
                    break;
                    //烘叶丝-9k  
                case '2199':
                    $('#9khsbrand').html(data[i].NAME);
                    $('#9khswo').html(data[i].PLANCODE);
                    break;
                    //掺配加香-9K 
                case '2204':
                    $('#9kcpbrand').html(data[i].NAME);
                    $('#9kcpwo').html(data[i].PLANCODE);
                    break;
                    //烟丝入库-9K
                case '2211':
                    $('#9krkbrand').html(data[i].NAME);
                    $('#9krkwo').html(data[i].PLANCODE);
                    break;
                    //真空回潮-5K
                case '2120':
                    $('#5khcbrand').html(data[i].NAME);
                    $('#5khcwo').html(data[i].PLANCODE);
                    break;
                    //切片-5K
                case '2125':
                    $('#5kqpbrand').html(data[i].NAME);
                    $('#5kqpwo').html(data[i].PLANCODE);
                    break;
                    //叶片加料-5K
                case '2128':
                    $('#5kjlbrand').html(data[i].NAME);
                    $('#5kjlwo').html(data[i].PLANCODE);
                    break;
                    //切叶丝-5k
                case '2132':
                    $('#5ksbrand').html(data[i].NAME);
                    $('#5kswo').html(data[i].PLANCODE);
                    break;
                    //烘叶丝-5k
                case '2136':
                    $('#5khsbrand').html(data[i].NAME);
                    $('#5khswo').html(data[i].PLANCODE);
                    break;
                    //掺配-5K
                case '2140':
                    $('#5kcpbrand').html(data[i].NAME);
                    $('#5kcpwo').html(data[i].PLANCODE);
                    break;
                    //加香-5K
                case '2145':
                    $('#5kjxbrand').html(data[i].NAME);
                    $('#5kjxwo').html(data[i].PLANCODE);
                    break;
                    //烟丝入库-5K
                case '2148':
                    $('#5krkbrand').html(data[i].NAME);
                    $('#5krkwo').html(data[i].PLANCODE);
                    break;
                    //切片-3K
                case '2149':
                    $('#3kqpbrand').html(data[i].NAME);
                    $('#3kqpwo').html(data[i].PLANCODE);
                    break;
                    //叶片加料-3K
                case '2154':
                    $('#3kjlbrand').html(data[i].NAME);
                    $('#3kjlwo').html(data[i].PLANCODE);
                    break;
                    //配片预混出柜-3K
                case '2158':
                    $('#3kyhbrand').html(data[i].NAME);
                    $('#3kyhwo').html(data[i].PLANCODE);
                    break;
                    //切叶丝-3K
                case '2159':
                    $('#3kqsbrand').html(data[i].NAME);
                    $('#3kqswo').html(data[i].PLANCODE);
                    break;
                    //滚筒烘丝-3K
                case '2163':
                    $('#3kgtbrand').html(data[i].NAME);
                    $('#3kgtwo').html(data[i].PLANCODE);
                    break;
                    //气流烘丝-3K
                case '2168':
                    $('#3kqlbrand').html(data[i].NAME);
                    $('#3kqlwo').html(data[i].PLANCODE);
                    break;
                    //梗预处理
                case '2173':
                    $('#gyclbrand').html(data[i].NAME);
                    $('#gyclwo').html(data[i].PLANCODE);
                    break;
                    //切梗
                case '2177':
                    $('#qgbrand').html(data[i].NAME);
                    $('#qgwo').html(data[i].PLANCODE);
                    break;
                    //烘梗
                case '2180':
                    $('#hgbrand').html(data[i].NAME);
                    $('#hgwo').html(data[i].PLANCODE);
                    break;
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
        if (data!=null) {
            $('#DayCutNum').html(data[0].DAYTOTAL);
            $('#MonthCutNum').html(data[0].MONTHTOTAL);
        }

    })

}


function GetDayWoStaticsTeam() {
    //9k线 
    //切片2187、叶片2192、烘丝2199、掺配2204
    var _9k11 = 0, _9k12 = 0, _9k13 = 0;
    var _9k21 = 0, _9k22 = 0, _9k23 = 0;
    var _9k31 = 0, _9k32 = 0, _9k33 = 0;
    var _9k41 = 0, _9k42 = 0, _9k43 = 0;
    var _9k1 = [];
    var _9k2 = [];
    var _9k3 = [];
    //5k线
    //真空2120、切片2125、叶片2128、烘丝2136、掺配2140、加香2145
    var _5k11 = 0, _5k12 = 0, _5k13 = 0;
    var _5k21 = 0, _5k22 = 0, _5k23 = 0;
    var _5k31 = 0, _5k32 = 0, _5k33 = 0;
    var _5k41 = 0, _5k42 = 0, _5k43 = 0;
    var _5k51 = 0, _5k52 = 0, _5k53 = 0;
    var _5k61 = 0, _5k62 = 0, _5k63 = 0;
    var _5k1 = [];
    var _5k2 = [];
    var _5k3 = [];
    //3k线
    //切片2149、叶片、预混、滚筒、气流
    var _3k11 = 0, _3k12 = 0, _3k13 = 0;
    var _3k21 = 0, _3k22 = 0, _3k23 = 0;
    var _3k31 = 0, _3k32 = 0, _3k33 = 0;
    var _3k41 = 0, _3k42 = 0, _3k43 = 0;
    var _3k51 = 0, _3k52 = 0, _3k53 = 0;
    var _3k1 = [];
    var _3k2 = [];
    var _3k3 = [];
    //梗丝线
    //梗预、切梗、烘梗
    var _gs11 = 0, _gs12 = 0, _gs13 = 0;
    var _gs21 = 0, _gs22 = 0, _gs23 = 0;
    var _gs31 = 0, _gs32 = 0, _gs33 = 0;
    var _gs1 = [];
    var _gs2 = [];
    var _gs3 = [];

    var productdate = $("#ProducteDate").html();
    productdate = productdate.replace("年", "-").replace("月", "-").replace("日", "");
    app.DataRequest('GetDayWoStatics', { code: 'GetDayWoStaticsTeam', strDate: productdate }, function (data) {

        for (var i = 0; i < data.length; i++) {
            switch (data[i].RESOURCEID) {
                //切片-9K  
                case '2187':
                    _9k11 = data[i].T72922;
                    _9k12 = data[i].T72923;
                    _9k13 = data[i].T72925;
                    break;
                    //叶片加料-9K  
                case '2192':
                    _9k21 = data[i].T72922;
                    _9k22 = data[i].T72923;
                    _9k23 = data[i].T72925;
                    break;
                    //烘丝-9k
                case '2199':
                    _9k31 = data[i].T72922;
                    _9k32 = data[i].T72923;
                    _9k33 = data[i].T72925;
                    break;
                    //掺配加香-9K 
                case '2204':
                    _9k41 = data[i].T72922;
                    _9k42 = data[i].T72923;
                    _9k43 = data[i].T72925;
                    break;
                    //真空回潮-5K
                case '2120':
                    _5k11 = data[i].T72922;
                    _5k12 = data[i].T72923;
                    _5k13 = data[i].T72925;
                    break;
                    //切片-5K
                case '2125':
                    _5k21 = data[i].T72922;
                    _5k22 = data[i].T72923;
                    _5k23 = data[i].T72925;
                    break;
                    //叶片加料-5K
                case '2128':
                    _5k31 = data[i].T72922;
                    _5k32 = data[i].T72923;
                    _5k33 = data[i].T72925;
                    break;
                    //烘丝-5k
                case '2136':
                    _5k41 = data[i].T72922;
                    _5k42 = data[i].T72923;
                    _5k43 = data[i].T72925;
                    break;
                    //掺配-5K
                case '2140':
                    _5k51 = data[i].T72922;
                    _5k52 = data[i].T72923;
                    _5k53 = data[i].T72925;
                    break;
                    //加香-5K
                case '2145':
                    _5k61 = data[i].T72922;
                    _5k62 = data[i].T72923;
                    _5k63 = data[i].T72925;
                    break;
                    //切片-3K
                case '2149':
                    _3k11 = data[i].T72922;
                    _3k12 = data[i].T72923;
                    _3k13 = data[i].T72925;
                    break;
                    //叶片加料-3K
                case '2154':
                    _3k21 = data[i].T72922;
                    _3k22 = data[i].T72923;
                    _3k23 = data[i].T72925;
                    break;
                    //配片预混出柜-3K
                case '2158':
                    _3k31 = data[i].T72922;
                    _3k32 = data[i].T72923;
                    _3k33 = data[i].T72925;
                    break;
                    //滚筒烘丝-3K
                case '2163':
                    _3k41 = data[i].T72922;
                    _3k42 = data[i].T72923;
                    _3k43 = data[i].T72925;
                    break;
                    //气流烘丝-3K
                case '2168':
                    _3k51 = data[i].T72922;
                    _3k52 = data[i].T72923;
                    _3k53 = data[i].T72925;
                    break;
                    //梗预处理
                case '2173':
                    _gs11 = data[i].T72922;
                    _gs12 = data[i].T72923;
                    _gs13 = data[i].T72925;
                    break;
                    //切梗
                case '2177':
                    _gs21 = data[i].T72922;
                    _gs22 = data[i].T72923;
                    _gs23 = data[i].T72925;
                    break;
                    //烘梗
                case '2180':
                    _gs31 = data[i].T72922;
                    _gs32 = data[i].T72923;
                    _gs33 = data[i].T72925;
                    break;

                default:
                    break;
            }
        }
    }, null, false);

    //9k线
    _9k1.push(_9k11 * 1); _9k1.push(_9k21 * 1); _9k1.push(_9k31 * 1); _9k1.push(_9k41 * 1);
    _9k2.push(_9k12 * 1); _9k2.push(_9k22 * 1); _9k2.push(_9k32 * 1); _9k2.push(_9k42 * 1);
    _9k3.push(_9k13 * 1); _9k3.push(_9k23 * 1); _9k3.push(_9k33 * 1); _9k3.push(_9k43 * 1);
    
    LoadChart9k(_9k1, _9k2, _9k3);
    //5k线
    _5k1.push(_5k11 * 1); _5k1.push(_5k21 * 1); _5k1.push(_5k31 * 1); _5k1.push(_5k41 * 1); _5k1.push(_5k51 * 1); _5k1.push(_5k61 * 1);
    _5k2.push(_5k12 * 1); _5k2.push(_5k22 * 1); _5k2.push(_5k32 * 1); _5k2.push(_5k42 * 1); _5k2.push(_5k52 * 1); _5k2.push(_5k62 * 1);
    _5k3.push(_5k13 * 1); _5k3.push(_5k23 * 1); _5k3.push(_5k33 * 1); _5k3.push(_5k43 * 1); _5k3.push(_5k53 * 1); _5k3.push(_5k63 * 1);

    LoadChart5k(_5k1, _5k2, _5k3);
    //3k线
    _3k1.push(_3k11 * 1); _3k1.push(_3k21 * 1); _3k1.push(_3k31 * 1); _3k1.push(_3k41 * 1); _3k1.push(_3k51 * 1);
    _3k2.push(_3k12 * 1); _3k2.push(_3k22 * 1); _3k2.push(_3k32 * 1); _3k2.push(_3k42 * 1); _3k2.push(_3k52 * 1);
    _3k3.push(_3k13 * 1); _3k3.push(_3k23 * 1); _3k3.push(_3k33 * 1); _3k3.push(_3k43 * 1); _3k3.push(_3k53 * 1);
    LoadChart3k(_3k1, _3k2, _3k3);
    //梗丝线
    _gs1.push(_gs11 * 1); _gs1.push(_gs21 * 1); _gs1.push(_gs31 * 1);
    _gs2.push(_gs12 * 1); _gs2.push(_gs22 * 1); _gs2.push(_gs32 * 1);
    _gs3.push(_gs13 * 1); _gs3.push(_gs23 * 1); _gs3.push(_gs33 * 1);
    LoadChartgs(_gs1, _gs2, _gs3);
}

function LoadChartgs(_gs1, _gs2, _gs3) {
    var chartoptiongs = {
        chart: {
            type: 'column',
            backgroundColor: "#ffb118",
            margin: [0, 0, 22, 20]
        },
        title: {
            text: ''
        },
        subtitle: {
            text: ''
        },
        xAxis: {
            min: 0,
            categories: [
                '梗预',
                '切梗',
                '烘梗'
            ]
        },
        yAxis: {
            title: {
                text: ''
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f}批</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0,
                dataLabels: {
                    overflow: "none",
                    crop: false,
                }
            }
        },
        credits: {
            enabled: false
        },
        legend: {
            enabled: false,
            align: 'left',
            verticalAlign: 'top',
            borderWidth: 0,
            y: -17,
            style: {
                fontSize: '12px'
            }
        },
        series: [{
            name: '甲班',
            data: _gs1,
            dataLabels: {
                enabled: true,
                color: '#FFFFFF',
                align: 'center',
                verticalAlign: 'middle',
                x: 0,
                y: -1,
                style: {
                    color: '#fff',//颜色
                    fontSize: '10px'  //字体
                }
            }
        }, {
            name: '乙班',
            data: _gs2,
            dataLabels: {
                enabled: true,
                color: '#FFFFFF',
                align: 'center',
                verticalAlign: 'middle',
                x: 0,
                y: -1,
                style: {
                    color: '#fff',//颜色
                    fontSize: '10px'  //字体
                }
            }

        }, {
            name: '丙班',
            data: _gs3,
            dataLabels: {
                enabled: true,
                color: '#FFFFFF',
                align: 'center',
                verticalAlign: 'middle',
                x: 0,
                y: -1,
                style: {
                    color: '#fff',//颜色
                    fontSize: '10px'  //字体
                }
            }
        }]
    };
    $("#schartgs").highcharts(chartoptiongs);
};
function LoadChart3k(_3k1, _3k2, _3k3) {
    var chartoption3k = {
        chart: {
            type: 'column',
            backgroundColor: "#09cc52",
            margin: [0, 0, 22, 20]
        },
        title: {
            text: ''
        },
        subtitle: {
            text: ''
        },
        xAxis: {
            categories: [
                '切片',
                '叶片',
                '预混',
                '滚筒',
                '气流'
            ]
        },
        yAxis: {
            min: 0,
            title: {
                text: ''
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f}批</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0,
                dataLabels: {
                    overflow: "none",
                    crop: false,
                }
            }
        },
        credits: {
            enabled: false
        },
        legend: {
            enabled: false,
            align: 'left',
            verticalAlign: 'top',
            borderWidth: 0,
            y: -17,
            style: {
                fontSize: '12px'
            }
        },
        series: [{
            name: '甲班',
            data: _3k1,
            dataLabels: {
                enabled: true,
                color: '#FFFFFF',
                align: 'center',
                verticalAlign: 'middle',
                x: 0,
                y: -1,
                style: {
                    color: '#fff',//颜色
                    fontSize: '10px'  //字体
                }
            }
        }, {
            name: '乙班',
            data: _3k2,
            dataLabels: {
                enabled: true,
                color: '#FFFFFF',
                align: 'center',
                verticalAlign: 'middle',
                x: 0,
                y: -1,
                style: {
                    color: '#fff',//颜色
                    fontSize: '10px'  //字体
                }
            }
        }, {
            name: '丙班',
            data: _3k3,
            dataLabels: {
                enabled: true,
                color: '#FFFFFF',
                align: 'center',
                verticalAlign: 'middle',
                x: 0,
                y: -1,
                style: {
                    color: '#fff',//颜色
                    fontSize: '10px'  //字体
                }
            }
        }]
    };
    $("#schart3k").highcharts(chartoption3k);
};
function LoadChart5k(_5k1, _5k2, _5k3) {
    var chartoption5k = {
        chart: {
            type: 'column',
            backgroundColor: "#f76532",
            margin: [0, 0, 22, 20]
        },
        title: {
            text: ''
        },
        subtitle: {
            text: ''
        },
        xAxis: {
            categories: [
                '真空',
                '切片',
                '叶片',
                '烘丝',
                '掺配',
                '加香'
            ]
        },
        yAxis: {
            min: 0,
            title: {
                text: ''
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f}批</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0,
                dataLabels: {
                    overflow: "none",
                    crop: false,
                }
            }
        },
        credits: {
            enabled: false
        },
        legend: {
            enabled: false,
            align: 'left',
            verticalAlign: 'top',
            borderWidth: 0,
            y: -17,
            style: {
                fontSize: '12px'
            }
        },
        series: [{
            name: '甲班',
            data: _5k1,
            dataLabels: {
                enabled: true,
                color: '#FFFFFF',
                align: 'center',
                verticalAlign: 'middle',
                x: 0,
                y: -1,
                overflow: "none",
                crop: false,
                style: {
                    color: '#fff',//颜色
                    fontSize: '10px'  //字体
                }}
        }, {
            name: '乙班',
            data: _5k2,
            dataLabels: {
                enabled: true,
                color: '#FFFFFF',
                align: 'center',
                verticalAlign: 'middle',
                x: 0,
                y: -1,
                overflow: "none",
                crop: false,
                style: {
                    color: '#fff',//颜色
                    fontSize: '10px'  //字体
                }}

        }, {
            name: '丙班',
            data: _5k3,
            dataLabels: {
                enabled: true,
                color: '#FFFFFF',
                align: 'center',
                verticalAlign: 'middle',
                x: 0,
                y: -1,
                overflow: "none",
                crop: false,
                style: {
                    color: '#fff',//颜色
                    fontSize: '10px'  //字体
                }
            }
        }]
    };
    $("#schart5k").highcharts(chartoption5k);
};
function LoadChart9k(_9k1, _9k2, _9k3) {
    var chartoption9k = {
        chart: {
            type: 'column',
            backgroundColor: "#ffb118",
            margin: [0, 0, 22, 20]
        },
        title: {
            text: ''
        },
        subtitle: {
            text: ''
        },
        xAxis: {
            categories: [
                '切片',
                '叶片',
                '烘丝',
                '掺配'
            ]
        },
        yAxis: {
            min: 0,
            title: {
                text: ''
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f}批</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0,
                dataLabels: {
                    overflow: "none",
                    crop: false,
                }
            }
        },
        credits: {
            enabled: false
        },
        legend: {
            enabled: false,
            align: 'left',
            verticalAlign: 'top',
            borderWidth: 0,
            y: -17,
            style: {
                fontSize: '12px'
            }
        },
        series: [{
            name: '甲班',
            data: _9k1,
            dataLabels: {
                enabled: true,
                color: '#FFFFFF',
                align: 'center',
                verticalAlign: 'middle',
                x: 0,
                y: -1,
                style: {
                    color: '#fff',//颜色
                    fontSize: '10px'  //字体
                }
            }
        }, {
            name: '乙班',
            data: _9k2,
            dataLabels: {
                enabled: true,
                color: '#FFFFFF',
                align: 'center',
                verticalAlign: 'middle',
                x: 0,
                y: -1,
                style: {
                    color: '#fff',//颜色
                    fontSize: '10px'  //字体
                }
            }

        }, {
            name: '丙班',
            data: _9k3,
            dataLabels: {
                enabled: true,
                color: '#FFFFFF',
                align: 'center',
                verticalAlign: 'middle',
                x: 0,
                y: -1,
                style: {
                    color: '#fff',//颜色
                    fontSize: '10px'  //字体
                }
            }
        }]
    };
    $("#schart9k").highcharts(chartoption9k);
};