var BreakMaterial = angular.module("BreakMatApp", []);
BreakMaterial.controller("BreakMatCtl", function ($scope, $http) {
    $("#StartDate").val(new Date().Format("yyyy-MM-dd"));
    $("#EndDate").val(new Date().Format("yyyy-MM-dd"));

    $scope.QueryData = function () {
        var startdate = $("#StartDate").val();
        var enddate = $("#EndDate").val();
        var brands;
        var productseg;
        var BreakCount1;
        var BreakCount2;
        var BreakTime1;
        var BreakTime2;

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
            }
        }, null, false);

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
            }
        }, null, false);

        Setbarchart1(brands, BreakCount2);
        Setbarchart2(brands, BreakTime2);
        Setbarchart3(productseg, BreakCount1);
        Setbarchart4(productseg, BreakTime1);
    }

})

function Setbarchart1(MateBrand, breakcounts) {

    var chartoption = {
        chart: {
            type: 'column',
            backgroundColor: "transparent",
            margin: [10, 10, 50, 70]
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
                text: '断料数（单位：次）'
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
                name: "断料次数",
                data: breakcounts
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
            margin: [10, 10, 50, 70]
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
                text: '断料时长（单位：min）'
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
                name: "断料时长",
                data: breaktimes1
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
            margin: [10, 10, 50, 70]
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
            categories: productseg
        },
        yAxis: {
            min: 0,
            title: {
                text: '断料数（单位：次）'
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
                name: "断料次数",
                data: breakcounts
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
            margin: [10, 10, 50, 70]
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
            categories: productseg
        },
        yAxis: {
            min: 0,
            title: {
                text: '断料时长（单位：min）'
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
                name: "断料时长",
                data: breaktimes2
            }

        ]

    }

    $("#Chart4").highcharts(chartoption);

}