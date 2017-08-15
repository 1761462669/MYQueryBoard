var physicInspect = angular.module("physicInspectApp", []);
physicInspect.controller("physicInspectCtl", function ($scope, $http) {
    $("#StartDate").val(new Date().Format("yyyy-MM-dd"));
    $("#EndDate").val(new Date().Format("yyyy-MM-dd"));

    $scope.QueryData = function () {
        var startdate = $("#StartDate").val();
        var enddate = $("#EndDate").val();
        var brands;
        var AvgWeilist;
        var AvgCirlist;
        var AvgPdlist;
        var AvgLenlist;

        app.DataRequest('QueryPhysicalInspectInfo', { code: 'QueryPhysicalInspectInfo', startdt: startdate, enddt: enddate }, function (data) {
            if (data.length > 0) {
                brands = new Array(data.length);
                AvgWeilist = new Array(data.length);
                AvgCirlist = new Array(data.length);
                AvgPdlist = new Array(data.length);
                AvgLenlist = new Array(data.length);

                for (var i = 0; i < data.length; i++) {
                    brands[i] = data[i]["BRAND"];
                    AvgWeilist[i] = data[i]["AVGWEI"].toString() == '' ? 0 : parseFloat(data[i]["AVGWEI"]);
                    AvgCirlist[i] = data[i]["AVGCIR"].toString() == '' ? 0 : parseFloat(data[i]["AVGCIR"]);
                    AvgPdlist[i] = data[i]["AVGPD"].toString() == '' ? 0 : parseFloat(data[i]["AVGPD"]);
                    AvgLenlist[i] = data[i]["AVGLEN"].toString() == '' ? 0 : parseFloat(data[i]["AVGLEN"]);
                }

                Setbarchart1(brands, AvgWeilist);
                Setbarchart2(brands, AvgCirlist);
                Setbarchart3(brands, AvgPdlist);
                Setbarchart4(brands, AvgLenlist);
            }
        }, null, false);
    }

})

function Setbarchart1(MateBrand, InspectData) {

    var chartoption = {
        chart: {
            type: 'column',
            backgroundColor: "transparent",
            margin: [10, 10, 90, 70]
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
            },
            categories: MateBrand
        },
        yAxis: {
            min: 1,
            title: {
                text: '（单位：mg）'
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
                pointPadding: 0,
                borderWidth: 0,
                pointWidth: 40,
            },
            series: {
                borderRadius: 0
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
                name: "重量均值",
                data: InspectData
            }

        ]

    }

    $("#Chart1").highcharts(chartoption);

}

function Setbarchart2(MateBrand, InspectData) {

    var chartoption = {
        chart: {
            type: 'column',
            backgroundColor: "transparent",
            margin: [10, 10, 90, 70]
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
            },
            categories: MateBrand
        },
        yAxis: {
            min: 1,
            title: {
                text: '（单位：mm）'
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
                pointPadding: 0,
                borderWidth: 0,
                pointWidth: 40,
            },
            series: {
                borderRadius: 0
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
                name: "圆周均值",
                data: InspectData
            }

        ]

    }

    $("#Chart2").highcharts(chartoption);

}

function Setbarchart3(MateBrand, InspectData) {

    var chartoption = {
        chart: {
            type: 'column',
            backgroundColor: "transparent",
            margin: [10, 10, 90, 70]
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
            },
            categories: MateBrand
        },
        yAxis: {
            min: 0,
            title: {
                text: '（单位：Pa）'
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
                pointPadding: 0,
                borderWidth: 0,
                pointWidth: 40,
            },
            series: {
                borderRadius: 0
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
                name: "吸阻均值",
                data: InspectData
            }

        ]

    }

    $("#Chart3").highcharts(chartoption);

}

function Setbarchart4(MateBrand, InspectData) {

    var chartoption = {
        chart: {
            type: 'column',
            backgroundColor: "transparent",
            margin: [10, 10, 90, 70]
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
            },
            categories: MateBrand
        },
        yAxis: {
            min: 0,
            title: {
                text: '（单位：mm）'
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
                pointPadding: 0,
                borderWidth: 0,
                pointWidth: 40,
            },
            series: {
                borderRadius: 0
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
                name: "长度均值",
                data: InspectData
            }

        ]

    }

    $("#Chart4").highcharts(chartoption);

}