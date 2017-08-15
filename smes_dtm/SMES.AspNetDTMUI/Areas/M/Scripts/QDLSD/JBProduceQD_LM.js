$(function () {
    //$("#StartDate").val((new Date()).toLocaleDateString());
    //$("#EndDate").val((new Date()).toLocaleDateString());
    //$("MainContent").attr("ng-app", "jbapp");
    //$("MainContent").attr("ng-controller", "jbctl");   
})

var storeapp = angular.module('jbapp', []);
storeapp.controller('jbctl', function ($scope, $http) {
    // 定义获取和更新时间的函数
    $scope.clock = new Date().toLocaleString();
    var updateClock = function () {
        $scope.clock = new Date().toLocaleString();
    };
    setInterval(function () {
        $scope.$apply(updateClock);
    }, 1000);
    updateClock();
    $("#StartDate").val($("#ProducteDate").html());
    $("#EndDate").val(new Date().Format("yyyy-MM-dd"));

    var tips = '数据时间：' + $("#StartDate").val() + ' 至 ' + $("#EndDate").val();
    $('#datatimetip').html(tips);

    var JJResultList;
    var BZResultList;
    $scope.QueryData = function () {
        if ($("#StartDate").val() == "") {
            $("#StartDate").val(new Date().Format("yyyy-MM-dd"));
            $("#EndDate").val(new Date().Format("yyyy-MM-dd"));
        }
        var startdate = $("#StartDate").val();
        var enddate = $("#EndDate").val();
        var brands;
        var outputs;

        $("#main").showLoading();
        app.DataRequest('GetEqusOutputRankingList', { code1: "GetEquModelListByTypeNm", EquTypeNm: "%卷接机%", code2: "GetEqusOutputList", StartDate: startdate, EndDate: enddate }, function (data) {
            if (data.length > 0) {
                $scope.JJResultList = data;
                $scope.$apply();
            }
        }, null, true);

        app.DataRequest("GetEqusOutputRankingList", { code1: "GetEquModelListByTypeNm", EquTypeNm: "%包装机%", code2: "GetEqusOutputList", StartDate: startdate, EndDate: enddate }, function (data) {
            if (data.length > 0) {
                $scope.BZResultList = data;
                $scope.$apply();
            }
            $("#main").hideLoading();
        }, null, true);

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

function Setbarchart1(BrandList, Outputs) {

    var chartoption = {
        chart: {
            type: 'column',
            backgroundColor: "transparent"
        },
        colors: ['#f76532', '#0f98e8', '#ffb119', '#910000'],
        title: {
            text: '',
            style: {
                color: '#FFFFFF'
            }
        },
        subtitle: {
            text: ''
        },
        xAxis: {
            tickColor: "#4c4d51",
            lineColor: "#4c4d51",
            labels: {
                rotation: -30,//倾斜度 
                step: 1, staggerLines: 1,
                style: {
                    color: '#fff'
                }
            },
            categories: BrandList
        },
        yAxis: {
            min: 0,
            title: { text: '产量(万支)', style: { color: '#fff' } },
            labels: {
                style: { color: '#fff' }
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
                dataLabels: {
                    overflow: "none",
                    crop: false,
                }
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
                color: '#FFFFFF',
                fontSize: '12px',
                padding: '8px'
            },
        },
        series: [
            {
                name: "实际产量",
                data: Outputs,
                dataLabels: {
                    enabled: true,
                    rotation: -45,
                    color: '#FFFFFF',
                    align: 'center',
                    verticalAlign: 'top',
                    x: 0,
                    y: -24,
                    style: {
                        color: '#fff',//颜色
                        fontSize: '10px'  //字体
                    }
                }
            }
        ]

    };

    $("#BrandProgressChart").highcharts(chartoption);
}
