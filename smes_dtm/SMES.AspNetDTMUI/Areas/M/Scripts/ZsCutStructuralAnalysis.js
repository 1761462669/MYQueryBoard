$(function () {
    resize();
    $(window).on("resize", resize);
})

var SA = angular.module("CutSAApp", []);
SA.controller("CutSACtl", function ($scope, $http) {
    $("#StartDate").val(new Date().Format("yyyy-MM-dd"));
    $("#EndDate").val(new Date().Format("yyyy-MM-dd"));
    $scope.CutBrandlist;
    $scope.Teamlist;
    $scope.CutStructData;
    $scope.item;

    app.DataRequest('QueryProductionTeam', { code: 'QueryProductionTeam' }, function (data) {
        if (data.length > 0) {
            $scope.Teamlist = data;
        }
    }, null, false);

    app.DataRequest('GetCutBrandList', { code: "QueryCutBrands" }, function (data) {
        if (data.length > 0) {
            $scope.CutBrandlist = data;
        }
    }, null, false);

    $scope.QueryData = function () {
        var startdate = $("#StartDate").val();
        var enddate = $("#EndDate").val();
        var QueryBrand = new Array();
        var QueryTeam = new Array();
        var QueryPars;
        var brands;
        var dates;
        var data1;
        var data2;

        QueryPars = $("#ParaList").val();

        try {
            var BrandItems = $("#Brandtree").find("input");
            var j = 0;
            for (var i = 0; i < BrandItems.length; i++) {
                if (BrandItems[i].checked ) {
                    QueryBrand[j] = parseInt(BrandItems[i].value);
                    j++;
                }
            }
        } catch (e) {

        }

        try {
            var TeamItems = $("#Teamtree").find("input");
            var k = 0;
            for (var i = 0; i < TeamItems.length; i++) {
                if (TeamItems[i].checked) {
                    QueryTeam[k] = parseInt(TeamItems[i].value);
                    k++;
                }
            }
        } catch (e) {

        }

        app.DataRequest('GetBrandsCutStructData', { startdt: startdate, enddt: enddate, paraid: QueryPars, brandlist: QueryBrand, teamlist: QueryTeam }, function (data) {
            if (data.length > 0) {
                brands = new Array(data.length);
                data1 = new Array(data.length);
                for (var i = 0; i < data.length ; i++) {
                    brands[i] = data[i]["NAME"];
                    data1[i] = parseFloat(data[i]["AVGNUM"]);
                }
            }
        }, null, false);

        app.DataRequest('GetDatesCutStructData', { startdt: startdate, enddt: enddate, paraid: '917086', brandlist: QueryBrand, teamlist: QueryTeam }, function (data) {
            if (data.length > 0) {
                dates = new Array(data.length);
                data2 = new Array(data.length);
                for (var i = 0; i < data.length ; i++) {
                    dates[i] =StringToDate(data[i]["DATES"]).Format('DD');
                    data2[i] =parseFloat(data[i]["AVGNUM"]);
                }
            }
        }, null, false);

        app.DataRequest('GetCutStructData', { startdt: startdate, enddt: enddate,brandlist: QueryBrand, teamlist: QueryTeam }, function (data) {
            if (data.length > 0) {
                $scope.CutStructData = data;
            }
        }, null, false);

        Setbarchart1(brands,data1);
        Setbarchart2(dates,data2);

    }

    function Setbarchart1(brand,datas) {

        var chartoption = {
            chart: {
                type:'column',
                backgroundColor: "transparent",
                margin: [20, 10, 60, 60]
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
                categories: brand
            },
            yAxis: {
                min: 0,
                title: {
                    text: '平均数&lt;检验值&gt;'
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
                    name: "检验项均值",
                    data: datas
                }

            ]

        }

        $("#Chart1").highcharts(chartoption);

    }

    function Setbarchart2(dates, datas) {

        var chartoption = {
            chart: {
                type: 'line',
                backgroundColor: "transparent",
                margin: [20, 10, 60, 60]
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
                categories: dates
            },
            yAxis: {
                min: 0,
                title: {
                    text: '平均数&lt;检验值&gt;'
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
                    pointWidth: 10,
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
                    name: "检验项均值",
                    data: datas
                }
            ]

        }

        $("#Chart2").highcharts(chartoption);

    }
})

function resize() {
    Scroll1();
}

function Scroll1() {
    try {
        $("#Scroll1").slimScroll({
            height: $(".producerow").height()-50,
            alwaysVisible: false,
            color: "#38393d",
            railColor: "#525357",
            railOpacity: 1,
            railVisible: true,
            disableFadeOut: true
        });
    } catch (e) {

    }

}