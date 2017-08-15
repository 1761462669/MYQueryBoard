$(function () {
    resize();
    $(window).on("resize", resize);
})

var Jbkt = angular.module('jbktApp', []);
Jbkt.controller('jbktCtl', function ($scope) {
    $("#StartDate").val(new Date().Format("yyyy-MM-dd"));
    $("#EndDate").val(new Date().Format("yyyy-MM-dd"));

    $scope.QueryData = function () {
        var startdate = $("#StartDate").val();
        var enddate = $("#EndDate").val();
        app.DataRequest('GetJbKtData', { code: 'QueryJBKTElimiRateByProduct', ParamId: 893955, startdt: startdate, enddt: enddate }, function (data) {
                var brandlist1 = new Array(data.length);
                var ktTcl1 = new Array(data.length);
                for (var i = 0; i < data.length; i++) {
                    brandlist1[i] = data[i]['NAME'];
                    ktTcl1[i] = parseFloat(data[i]['ELIM_RATE']);
                }

                Setbarchart1(brandlist1, ktTcl1);
        })

        app.DataRequest('GetJbKtData', { code: 'QueryJBKTElimiRateByTeam', ParamId: 893955, startdt: startdate, enddt: enddate }, function (data) {
            var team1 = new Array(data.length);
            var ktTcl2 = new Array(data.length);
            for (var i = 0; i < data.length; i++) {
                    team1[i] = data[i]['TEAMNAME'];
                    ktTcl2[i] = parseFloat(data[i]['ELIM_RATE']); 
                }
            Setbarchart2(team1, ktTcl2);
        })

        app.DataRequest('GetJbKtData', { code: 'QueryJBKTElimiRateByProduct', ParamId: 911141, startdt: startdate, enddt: enddate }, function (data) {
            var brandlist2 = new Array(data.length);
            var ktTcl3 = new Array(data.length);
            for (var i = 0; i < data.length; i++) {
                brandlist2[i] = data[i]['NAME'];
                ktTcl3[i] = parseFloat(data[i]['ELIM_RATE']);
            }

            Setbarchart3(brandlist2, ktTcl3);
        })

        app.DataRequest('GetJbKtData', { code: 'QueryJBKTElimiRateByTeam', ParamId: 911141, startdt: startdate, enddt: enddate }, function (data) {
            var team2 = new Array(data.length);
            var ktTcl4 = new Array(data.length);
            for (var i = 0; i < data.length; i++) {
                team2[i] = data[i]['TEAMNAME'];
                ktTcl4[i] = parseFloat(data[i]['ELIM_RATE']);
            }
            Setbarchart4(team2, ktTcl4);
        })

    }

});

function resize() {
    Scroll1();
}

function Scroll1() {
    $("#Scroll1").slimScroll({
        height: $(".main-content1").height()-20,
        alwaysVisible: false,
        color: "#686868",
        railColor: "#525357",
        railOpacity: 1,
        railVisible: true,
        disableFadeOut: true
    });
}

function Setbarchart1(Keylist, Valuelist) {

    var chartoption = {
        chart: {
            type: 'column',
            backgroundColor: "#fff",
            margin: [50, 10, 50, 60]
        },
        colors: ['#f76532', '#0f98e8', '#ffb119', '#910000'],
        title: {
            text: '卷接段牌号空头剔除率'
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
            categories: Keylist
        },
        yAxis: {
            min: 1,
            title: {
                text: '（单位：%）'
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
                name: "空头剔除率",
                data: Valuelist
            }

        ]

    }

    $("#Chart1").highcharts(chartoption);

}

function Setbarchart2(Keylist, Valuelist) {

    var chartoption = {
        chart: {
            type: 'column',
            backgroundColor: "#fff",
            margin: [50, 10, 50, 60]
        },
        colors: ['#f76532', '#0f98e8', '#ffb119', '#910000'],
        title: {
            text: '卷接段班组空头剔除率'
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
            categories: Keylist
        },
        yAxis: {
            min: 1,
            title: {
                text: '（单位：%）'
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
                pointWidth: 80,
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
                name: "空头剔除率",
                data: Valuelist
            }

        ]

    }

    $("#Chart2").highcharts(chartoption);

}

function Setbarchart3(Keylist, Valuelist) {

    var chartoption = {
        chart: {
            type: 'column',
            backgroundColor: "#fff",
            margin: [50, 10, 50, 60]
        },
        colors: ['#0f98e8', '#f76532', '#ffb119', '#910000'],
        title: {
            text: '包装段牌号空头剔除率'
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
            categories: Keylist
        },
        yAxis: {
            min: 1,
            title: {
                text: '（单位：%）'
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
                name: "空头剔除率",
                data: Valuelist
            }

        ]

    }

    $("#Chart3").highcharts(chartoption);

}

function Setbarchart4(Keylist, Valuelist) {

    var chartoption = {
        chart: {
            type: 'column',
            backgroundColor: "#fff",
            margin: [50, 10, 50, 60]
        },
        colors: ['#0f98e8', '#f76532', '#ffb119', '#910000'],
        title: {
            text: '包装段班组空头剔除率'
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
            categories: Keylist
        },
        yAxis: {
            min: 1,
            title: {
                text: '（单位：%）'
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
                pointWidth: 80,
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
                name: "空头剔除率",
                data: Valuelist
            }

        ]

    }

    $("#Chart4").highcharts(chartoption);

}