var BreakMaterial = angular.module("BreakMatApp", []);
BreakMaterial.controller("BreakMatCtl", function ($scope, $http) {
    $("#StartDate").val(new Date().Format("yyyy-MM-dd"));
    $("#EndDate").val(new Date().Format("yyyy-MM-dd"));
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

    $scope.QueryData = function () {
        var startdate = $("#StartDate").val();
        var enddate = $("#EndDate").val();
        var brands;
        var productseg;
        var BreakCount1;
        var BreakCount2;
        var BreakTime1;
        var BreakTime2;
        $("#main").showLoading();
        app.DataRequest('QueryZsBreakMaterial', { code: 'ProductsegBreakMatStatis', startdt: startdate, enddt: enddate }, function (data) {
            if (data.length>0) {
                productseg = new Array(data.length);
                BreakCount1 = new Array(data.length);
                BreakTime1 = new Array(data.length);
                for (var i = 0; i < data.length; i++) {
                    productseg[i] = data[i]["PSEGMENTNM"];
                    BreakCount1[i] = parseInt(data[i]["DLNUMBER"]);
                    BreakTime1[i] =parseFloat(data[i]["DLTIMES"]);
                    
                }
                Setbarchart3(productseg, BreakCount1);
                Setbarchart4(productseg, BreakTime1);
            }
        }, null, true);

        app.DataRequest('QueryZsBreakMaterial', { code: 'MatBrandBreakMatStatis', startdt: startdate, enddt: enddate }, function (data) {
            if (data.length > 0) {
                brands = new Array(data.length);
                BreakCount2 = new Array(data.length);
                BreakTime2 = new Array(data.length);
                for (var i = 0; i < data.length; i++) {
                    brands[i] = data[i]["NAME"];
                    BreakCount2[i] = parseInt(data[i]["DLNUMBER"]);
                    BreakTime2[i] = parseFloat(data[i]["DLTIMES"]);
                }
                Setbarchart1(brands, BreakCount2);
                Setbarchart2(brands, BreakTime2);
            }
                $("#main").hideLoading();
        }, null, true);

    }


    $scope.QueryData();
})

function Setbarchart1(MateBrand, breakcounts) {
    var chartoption = {
        chart: {
            type: 'column',
            backgroundColor: "transparent",
            margin: [10, 10, 120, 70]
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
                    color: '#fff'
                }
            },
            categories: MateBrand
        },
        yAxis: {
            min: 1,
            title: {
                text: '断料数（次）'
            },
            labels: {
                style: {
                    color: '#fff'
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
                borderWidth: 0
            },
            series: {
                borderRadius: 0
            }
        },
        legend: {
            itemStyle: {
                color: '#fff',
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
                color: '#fff',
                fontSize: '12px',
                padding: '8px'
            },
        },
        series: [
            {
                name: "断料次数",
                data: breakcounts,
                dataLabels: {
                    enabled: true,
                    //rotation: 0,
                    color: '#FFFFFF',
                    align: 'center',
                    verticalAlign: 'top',
                    x: 0,
                    y: -24,
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif',
                        textShadow: '0 0 3px black'
                    }
                }
            }

        ]

    }

    $("#Chart1").highcharts(chartoption);

}

function Setbarchart2(MateBrand, breaktimes1) {

    var chartoption = {
        chart: {
            type: 'column',
            backgroundColor: "transparent",
            margin: [10, 10, 120, 70]
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
                    color: '#fff'
                }
            },
            categories: MateBrand
        },
        yAxis: {
            min: 1,
            title: {
                text: '断料时长（min）'
            },
            labels: {
                style: {
                    color: '#fff'
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
                borderWidth: 0
            },
            series: {
                borderRadius: 0
            }
        },
        legend: {
            itemStyle: {
                color: '#fff',
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
                color: '#fff',
                fontSize: '12px',
                padding: '8px'
            },
        },
        series: [
            {
                name: "断料时长",
                data: breaktimes1,
                dataLabels: {
                    enabled: true,
                    //rotation: 0,
                    color: '#FFFFFF',
                    align: 'center',
                    verticalAlign: 'top',
                    x: 0,
                    y: -24,
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif',
                        textShadow: '0 0 3px black'
                    }
                }
            }

        ]

    }

    $("#Chart2").highcharts(chartoption);

}

function Setbarchart3(productseg,breakcounts) {

    var chartoption = {
        chart: {
            type: 'column',
            backgroundColor: "transparent",
            margin: [10, 10, 120, 70]
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
                    color: '#fff'
                }
            },
            categories: productseg
        },
        yAxis: {
            min: 0,
            title: {
                text: '断料数（次）'
            },
            labels: {
                style: {
                    color: '#fff'
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
                borderWidth: 0
            },
            series: {
                borderRadius: 0
            }
        },
        legend: {
            itemStyle: {
                color: '#fff',
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
                color: '#fff',
                fontSize: '12px',
                padding: '8px'
            },
        },
        series: [
            {
                name: "断料次数",
                data: breakcounts,
                dataLabels: {
                    enabled: true,
                    //rotation: 0,
                    color: '#FFFFFF',
                    align: 'center',
                    verticalAlign: 'top',
                    x: 0,
                    y: -24,
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif',
                        textShadow: '0 0 3px black'
                    }
                }
            }

        ]

    }

    $("#Chart3").highcharts(chartoption);

}

function Setbarchart4(productseg, breaktimes2) {

    var chartoption = {
        chart: {
            type: 'column',
            backgroundColor: "transparent",
            margin: [10, 10, 120, 70]
        },
        colors: ['#f76532', '#0f98e8', '#ffb119', '#910000'],
        title: {
            text: ''
        },
        subtitle: {
            text: ''
        },
        xAxis: {
            tickColor: "#fff",
            lineColor: "#4c4d51",
            labels: {
                style: {
                    color: '#fff'
                }
            },
            categories: productseg
        },
        yAxis: {
            min: 0,
            title: {
                text: '断料时长（min）'
            },
            labels: {
                style: {
                    color: '#fff'
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
                borderWidth: 0
            },
            series: {
                borderRadius: 0
            }
        },
        legend: {
            itemStyle: {
                color: '#fff',
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
                color: '#fff',
                fontSize: '12px',
                padding: '8px'
            },
        },
        series: [
            {
                name: "断料时长",
                data: breaktimes2,
                dataLabels: {
                    enabled: true,
                    //rotation: 0,
                    color: '#FFFFFF',
                    align: 'center',
                    verticalAlign: 'top',
                    x: 0,
                    y: -24,
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif',
                        textShadow: '0 0 3px black'
                    }
                }
            }

        ]

    }

    $("#Chart4").highcharts(chartoption);

}