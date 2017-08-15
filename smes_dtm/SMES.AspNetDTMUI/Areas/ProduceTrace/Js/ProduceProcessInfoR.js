
var id = app.QueryPara("ID");
var batchid = app.QueryPara("batchid");
$(function () {
    resiz();
    $(window).on("resize", resiz);
    // setroundchart();

    $(".rightarea>i").click(changetab);

    GetWorkorderInfo();
    GetOutSoilInfo();
    GetInSoilInfo();
    GetCutWorkorderConsume();
    //GetCutAncestryKeyParamData();
    GetQLScore();
    GetAllWorkorderPramQL();
})

var tabindex = 0;
function changetab() {
    var direction = $(this).data("direction");
    if (direction == 0) {
        if (tabindex == 0)
            return;
        tabindex--;


    }
    else {
        if (tabindex >= $(".tab-container").children().length - 1)
            return;

        tabindex++;


    }

    if (tabindex == 0)
        $("[data-direction='0']").removeClass("allow");
    else
        $("[data-direction='0']").addClass("allow");

    if (tabindex == $(".tab-container").children().length - 1)
        $("[data-direction='1']").removeClass("allow");
    else
        $("[data-direction='1']").addClass("allow");

    var curshowdiv = $(".tab-container>div:visible");
    var item = $(".tab-container>div").eq(tabindex);

    if (direction == "0") {
        item.css({ left: "-100%", display: "block" });
        item.animate({ left: "0" }, function () {

        });

        curshowdiv.animate({ left: "100%" }, function () {
            curshowdiv.hide();
        });
    }

    else {
        item.css({ left: "100%", display: "block" });
        item.animate({ left: "0" }, function () {

        });

        curshowdiv.animate({ left: "-100%" }, function () {
            curshowdiv.hide();
        });
    }
}

function resiz() {

    setScroll1();
    setScroll2();

    setScroll3();
    //setchart2();
}

function setroundchart() {
    $(".roundchart").each(function (index, el) {
        //debugger;
        var value = $(el).data("value");

        $(el).radialIndicator({
            barColor: app.GetColor(),
            barBgColor: "#0d0d0d",
            fontColor: "#999",
            fontWeight: "normal",
            fontSize: "12px",
            radius: 25,
            barWidth: 4,
            initValue: value,
            roundCorner: true,
            percentage: true

        })
    });

}

function setScroll1() {
    $("#scortable").slimScroll({
        height: 76,
        alwaysVisible: false,
        color: "#38393d",
        railColor: "#525357",
        railOpacity: 1,
        railVisible: true,
        disableFadeOut: true
    });
}

function setScroll2() {
    $("#scortable2").slimScroll({
        height: 76,
        alwaysVisible: false,
        color: "#38393d",
        railColor: "#525357",
        railOpacity: 1,
        railVisible: true,
        disableFadeOut: true
    });
}

function setScroll3() {

    $("#zljylist").slimScroll({

        height: $("#zljylist").parent().parent().height() - 42,
        alwaysVisible: false,
        color: "#38393d",
        railColor: "#525357",
        railOpacity: 1,
        railVisible: true,
        disableFadeOut: true
    });
}

function setchart1() {


}


/**数据获取**/

//获取工单信息
function GetWorkorderInfo() {
    app.DataRequest('@Url.Action("QueryCutWorkOrder")', { id: id }, function (data) {
        if (data) {
            $("#woinfo").empty();
            $("#woinfotmpl").tmpl(data).appendTo("#woinfo");
        }
    });
}

//获取出柜信息
function GetOutSoilInfo() {
    app.DataRequest('@Url.Action("GetMaterialTransferinoutsilo")', { id: id, typeid: 684275 }, function (data) {
        if (data) {
            $("#soilinfotmpl").tmpl(data).appendTo("#outsoilinfo");
        }
    });
}

//获取入信息
function GetInSoilInfo() {
    app.DataRequest('@Url.Action("GetMaterialTransferinoutsilo")', { id: id, typeid: 684174 }, function (data) {
        if (data) {
            $("#soilinfotmpl").tmpl(data).appendTo("#insoilinfo");
        }
    });
}

//工单消耗信息
function GetCutWorkorderConsume() {
    app.DataRequest('@Url.Action("GetCutWorkorderConsume")', { id: id }, function (data) {
        if (data) {
            $("#workorderconsumetmpl").tmpl(data).appendTo("#workorderconsume");

            setScroll1();
        }
    });
}

//或得工单关键参数信息
function GetCutAncestryKeyParamData() {
    var index = 0;
    app.DataRequest('@Url.Action("GetCutAncestryKeyParamData")', { id: id }, function (data) {
        $("#keyparamtmpl").tmpl(data, {
            getIndex: function () {
                index++;

                return index > 9 ? index + "" : "0" + index;
            }
        }).appendTo("#keyinfo");

        setroundchart();
    });
}

function GetQLScore() {


    app.DataRequest('@Url.Action("GetCutBatchSorce")', { woid: id }, function (data) {
        if (data) {
            $("#stdname").html("标准版本：" + data.PRODUCTSTANDARDNAME);
            $("#batchscore").html("得分：" + data.WORKORDERSCORE);
        }
        else {
            $("#stdname").html("标准版本：无");
            $("#batchscore").html("得分：无");
        }
    });

    app.DataRequest('@Url.Action("GetProductMonthCutBatchSorce")', { woid: id }, function (data) {
        if (data) {

            $("#batchproductscore").html("本月同牌号平均得分：" + data.SCORE);
        }
        else {
            $("#batchproductscore").html("本月同牌号平均得分：无");
        }
    });

    app.DataRequest('@Url.Action("GetAllProductMonthCutBatchSorce")', { woid: id }, function (data) {
        if (data) {

            $("#batchallproductscore").html("本月全部牌号平均得分：" + data.SCORE);
        }
        else {
            $("#batchallproductscore").html("本月全部牌号平均得分：无");
        }
    });
}

function GetAllWorkorderPramQL() {

    var chartoption = {
        chart: {
            type: 'column',
            backgroundColor: "transparent"
        },
        colors: ['#f76532', '#0f98e8', '#ffb119', '#910000'],
        title: {
            text: ''
        },
        subtitle: {
            text: ''
        },
        xAxis: {
            tickColor: "#4c4d51",
            lineColor: "#4c4d51",
            labels: {
                style: {
                    color: '#999'
                }
            }
        },
        yAxis: {
            min: 0,
            title: {
                text: ''
            },
            labels: {
                style: {
                    color: '#999'
                }
            },
            gridLineColor: '#4c4d51'
        },
        credits: {
            enabled: false
        },

        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0,
                pointWidth: 10,
            }
        },
        legend: {
            itemStyle: {
                color: '#999',
                fontWeight: "normal"
            },
            itemHoverStyle: {
                color: '#44b9dc'
            }
        },
        tooltip: {
            backgroundColor: "#333",
            borderWidth: 0,
            style: {
                color: '#999',
                fontSize: '12px',
                padding: '8px'
            },
        }

    }

    //var data = [
    //    { NAME: "本工单", PARAMETERID: "1", PARAMETERNAME: "温度", SCORE: "99" },
    //    { NAME: "本工单", PARAMETERID: "2", PARAMETERNAME: "湿度", SCORE: "93" },
    //    { NAME: "本月同牌号", PARAMETERID: "1", PARAMETERNAME: "温度", SCORE: "99" },
    //    { NAME: "本月同牌号", PARAMETERID: "2", PARAMETERNAME: "湿度", SCORE: "93" },
    //    { NAME: "本月同牌号", PARAMETERID: "3", PARAMETERNAME: "水分", SCORE: "84" },
    //    { NAME: "本月全部牌号", PARAMETERID: "1", PARAMETERNAME: "温度", SCORE: "99" },
    //    { NAME: "本月全部牌号", PARAMETERID: "2", PARAMETERNAME: "湿度", SCORE: "93" },
    //    { NAME: "本月全部牌号", PARAMETERID: "3", PARAMETERNAME: "水分", SCORE: "84" }
    //]



    app.DataRequest('@Url.Action("GetAllWorkorderPramQL")', { woid: id }, function (data) {
        if (!data)
            return;

        var scategories = [];
        var tmp = [];

        data.forEach(function (value, index) {

            if (scategories.indexOf(value.PARAMETERNAME) == -1)
                scategories.push(value.PARAMETERNAME);

            if (tmp[value.NAME]) {
                tmp[value.NAME].data.push(value);
            }
            else {
                tmp[value.NAME] = { data: [value] };
            }
        })

        var series = [];
        //debugger;
        for (var item in tmp) {
            var serie = { name: item, data: [] }

            tmp[item].data.forEach(function (value, index) {
                serie.data.push({ x: scategories.indexOf(value.PARAMETERNAME), y: Number(value.SCORE) });
            });

            series.push(serie);
        }

        chartoption.xAxis.categories = scategories;
        chartoption.series = series;

        $('#chart1').highcharts(chartoption);

    });
}