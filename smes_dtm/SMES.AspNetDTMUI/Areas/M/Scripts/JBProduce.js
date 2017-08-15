$(function () {
    //$("#StartDate").val((new Date()).toLocaleDateString());
    //$("#EndDate").val((new Date()).toLocaleDateString());
    //$("MainContent").attr("ng-app", "jbapp");
    //$("MainContent").attr("ng-controller", "jbctl");
})

var storeapp = angular.module('jbapp', []);
storeapp.controller('jbctl', function ($scope, $http) {
        var JJResultList;
        var BZResultList;
        $scope.QueryData = function () {
            if ($("#StartDate").val()=="") {
                $("#StartDate").val(new Date().Format("yyyy-MM-dd"));
                $("#EndDate").val(new Date().Format("yyyy-MM-dd"));
            }
            var startdate = $("#StartDate").val();
            var enddate = $("#EndDate").val();
            var brands;
            var outputs;

            app.DataRequest('GetEqusOutputRankingList', { code1: "GetEquModelListByTypeNm", EquTypeNm: "%卷接%", code2: "GetEqusOutputList", StartDate: startdate, EndDate: enddate }, function (data) {
                if (data.length > 0) {
                    $scope.JJResultList = data;
                }
            }, null, false);

            app.DataRequest("GetEqusOutputRankingList", { code1: "GetEquModelListByTypeNm", EquTypeNm: "%包装%", code2: "GetEqusOutputList", StartDate: startdate, EndDate: enddate }, function (data) {
                if (data.length > 0) {
                    $scope.BZResultList = data;
                }
            }, null, false);

            app.DataRequest("GetPackPeriodOutput", { code: "QueryPackPeriodOutput", start: startdate, end: enddate }, function (OutputsList) {
                if (OutputsList.length > 0) {
                    brands = new Array(OutputsList.length);
                    outputs = new Array(OutputsList.length);
                    for (var i = 0; i < OutputsList.length; i++) {
                        brands[i] = OutputsList[i]["MATNM"];
                        outputs[i] = parseFloat(OutputsList[i]["OUTPUT"]);
                    }
                   
                }
            }, null, false);

            Setbarchart1(brands, outputs);
        }

        $scope.QueryData();
});

function Setbarchart1(BrandList,Outputs) {

    var chartoption = {
        chart: {
            type: 'column',
            backgroundColor: "transparent",
            margin:[40, 10, 40, 80]
        },
        colors: ['#f76532', '#0f98e8', '#ffb119', '#910000'],
        //title: {
        //    text: '',
        //    style: {
        //        color: '#999'
        //    }
        //},
        //subtitle: {
        //    text: ''
        //},
        xAxis: {
            //title: { text: '卷烟牌号' },
            tickColor: "#4c4d51",
            lineColor: "#4c4d51",
            labels: {
                style: {
                    color: '#999'
                }
            },
            categories: BrandList
        },
        yAxis: {
            min: 0,
            title: { text: '产量(万支)' },
            labels: {
                style: { color: '#999'}
            },
            gridLineColor: '#4c4d51'
        },
        credits: {
            enabled: false
        },

        plotOptions: {
            column: {
                pointPadding: 0,
                borderWidth: 0,
                pointWidth: 20,
            },
            series: {
                borderRadius: 2
            }
        },
        legend: {
            itemStyle: {
                color: '#999',
                fontWeight: "normal"
            },
            itemHoverStyle: {
                color: '#44b9dc'
            },
            enabled: false
        },
        tooltip: {
            backgroundColor: "#333",
            borderWidth: 0,
            style: {
                color: '#999',
                fontSize: '12px',
                padding: '8px'
            },
        },
        series: [
            {
                name: "实际产量",
                data: Outputs
            }
            //,
            //{
            //    name: "计划产量",
            //    data: [30, 30, 30]
            //}
        ]

    };

    $("#BrandProgressChart").highcharts(chartoption);
}

//function getNowFormatDate() {
//    var date = new Date();
//    var seperator1 = "-";
//    var seperator2 = ":";
//    var year = date.getFullYear();
//    var month = date.getMonth() + 1;
//    var strDate = date.getDate();
//    if (month >= 1 && month <= 9) {
//        month = "0" + month;
//    }
//    if (strDate >= 0 && strDate <= 9) {
//        strDate = "0" + strDate;
//    }
//    var currentdate = year + seperator1 + month + seperator1 + strDate;
//    return currentdate;
//}

    //function resize() {
    //    Scroll1();
    //}

    //function Scroll1() {
    //    $("#Scroll1").slimScroll({
    //        height: $(".module1-curproduce").height() - $(".module1-curproduce-title").height(),
    //        alwaysVisible: false,
    //        color: "#38393d",
    //        railColor: "#525357",
    //        railOpacity: 1,
    //        railVisible: true,
    //        disableFadeOut: true
    //    });
    //}

    //function GetProduceList() {
    //    var data = [{ ProductName: "总产量", QTY: 3200, PlanQTY: 800, Unit: "箱" },
    //    { ProductName: "利群(新版)", QTY: 3200, PlanQTY: 800, Unit: "箱" },
    //    { ProductName: "利群(新版南宁)", QTY: 125, PlanQTY: 320, Unit: "箱" },
    //    { ProductName: "利群(休闲L)", QTY: 80, PlanQTY: 280, Unit: "箱" },
    //    { ProductName: "利群(逍遥)", QTY: 10, PlanQTY: 100, Unit: "箱" },
    //    { ProductName: "摩登(南美BH)", QTY: 50, PlanQTY: 150, Unit: "箱" },
    //    { ProductName: "雄狮(硬柳州)", QTY: 33, PlanQTY: 200, Unit: "箱" },
    //    { ProductName: "雄狮(硬)", QTY: 24, PlanQTY: 240, Unit: "箱" }];

    //    $("#productListtmpl").tmpl(data, { frmcolor: app.GetColor }).appendTo("#ProductList");

    //    Scroll1();
    //}

    //function Setbarchart2() {

    //    var chartoption = {
    //        chart: {
    //            type: 'column',
    //            backgroundColor: "transparent",
    //            margin: [0, 0, 20, 0]
    //        },
    //        colors: ['#f76532', '#0f98e8', '#ffb119', '#910000'],
    //        title: {
    //            text: ''
    //        },
    //        subtitle: {
    //            text: ''
    //        },
    //        xAxis: {
    //            tickColor: "#4c4d51",
    //            lineColor: "#4c4d51",
    //            labels: {
    //                style: {
    //                    color: '#999'
    //                }
    //            },
    //            categories: ['甲班', '乙班', '丙班']
    //        },
    //        yAxis: {
    //            min: 0,
    //            title: {
    //                text: ''
    //            },
    //            labels: {
    //                style: {
    //                    color: '#999'
    //                }
    //            },
    //            gridLineColor: '#4c4d51'
    //        },
    //        credits: {
    //            enabled: false
    //        },

    //        plotOptions: {
    //            column: {
    //                pointPadding: 0,
    //                borderWidth: 0,
    //                pointWidth: 10,
    //            },
    //            series: {
    //                borderRadius: 2
    //            }
    //        },
    //        legend: {
    //            itemStyle: {
    //                color: '#999',
    //                fontWeight: "normal"
    //            },
    //            itemHoverStyle: {
    //                color: '#44b9dc'
    //            },
    //            enabled: false
    //        },
    //        tooltip: {
    //            backgroundColor: "#333",
    //            borderWidth: 0,
    //            style: {
    //                color: '#999',
    //                fontSize: '12px',
    //                padding: '8px'
    //            },
    //        },
    //        series: [
    //            {
    //                name: "实际产量",
    //                data: [12, 22, 14]
    //            },
    //            {
    //                name: "计划产量",
    //                data: [30, 30, 30]
    //            }

    //        ]

    //    }

    //    $("#barchart2").highcharts(chartoption);

    //}

    //function Setbarchart3() {

    //    var chartoption = {
    //        chart: {
    //            type: 'column',
    //            backgroundColor: "transparent",
    //            margin: [0, 0, 20, 0]
    //        },
    //        colors: ['#f76532', '#0f98e8', '#ffb119', '#910000'],
    //        title: {
    //            text: ''
    //        },
    //        subtitle: {
    //            text: ''
    //        },
    //        xAxis: {
    //            tickColor: "#4c4d51",
    //            lineColor: "#4c4d51",
    //            labels: {
    //                style: {
    //                    color: '#999'
    //                }
    //            },
    //            categories: ['甲班', '乙班', '丙班']
    //        },
    //        yAxis: {
    //            min: 0,
    //            title: {
    //                text: ''
    //            },
    //            labels: {
    //                style: {
    //                    color: '#999'
    //                }
    //            },
    //            gridLineColor: '#4c4d51'
    //        },
    //        credits: {
    //            enabled: false
    //        },

    //        plotOptions: {
    //            column: {
    //                pointPadding: 0,
    //                borderWidth: 0,
    //                pointWidth: 10,
    //            },
    //            series: {
    //                borderRadius: 2
    //            }
    //        },
    //        legend: {
    //            itemStyle: {
    //                color: '#999',
    //                fontWeight: "normal"
    //            },
    //            itemHoverStyle: {
    //                color: '#44b9dc'
    //            },
    //            enabled: false
    //        },
    //        tooltip: {
    //            backgroundColor: "#333",
    //            borderWidth: 0,
    //            style: {
    //                color: '#999',
    //                fontSize: '12px',
    //                padding: '8px'
    //            },
    //        },
    //        series: [
    //            {
    //                name: "实际产量",
    //                data: [12, 22, 14]
    //            },
    //            {
    //                name: "计划产量",
    //                data: [30, 30, 30]
    //            }

    //        ]

    //    }

    //    $("#barchart3").highcharts(chartoption);

    //}