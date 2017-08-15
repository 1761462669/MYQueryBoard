var storeapp = angular.module('APP', []);
storeapp.controller('CTRL', function ($scope, $http) {
    // 定义获取和更新时间的函数
    $scope.clock = new Date().toLocaleString();
    var updateClock = function () {
        $scope.clock = new Date().toLocaleString();
    };
    setInterval(function () {
        $scope.$apply(updateClock);
    }, 1000);
    updateClock();

    var tips = '数据时间：' + $("#ProducteDate").html() + ' 至 ' + new Date().Format("yyyy-MM-dd");
    $('#datatimetip').html(tips);

    var ZSZLDF;
    var ZSZLPH;
    var MATNAME;
    document.getElementById("rm").focus();
    $scope.QueryData = function (tp) {
        $('#Chartscon').hide();
        $('#Chartcon').show();
        $scope.SelectType = tp;
        ZSZLDF = [];
        ZSZLPH = [];
        //$("#main").showLoading();
        app.DataRequest('getDataList', { state: tp }, function (data) {
            var dt = eval(data);
            if (dt.length > 0) {
                $.each(dt, function (i, n) {
                    ZSZLPH.push(n.PRODUCTNAME);
                    ZSZLDF.push(n.CCORE * 1);
                });
            }
            LoadChart(tp);
            //$("#main").hideLoading();
        }, null, false);
    }
    
    function LoadChart(tp) {
        var chartoption = {
            chart: {
                type: 'column',
                backgroundColor: "transparent"
                //margin: [10, 10, 90, 70]
            },
            title: {
                text: '制丝牌号质量得分统计 ',
                style: {
                    color: '#C4C4C4',
                    fontSize: '30px',
                    fontFamily: 'Verdana, sans-serif',
                    textShadow: '0 0 3px black'
                }
            },
            subtitle: {
                text: '点击柱形图查看对应牌号批次得分',
                style: {
                    color: '#fff',
                    fontSize: '12px',
                    fontFamily: 'Verdana, sans-serif',
                    textShadow: '0 0 3px black'
                }
            },
            xAxis: {
                title: { text: '' },
                labels: {
                    rotation: -30,//倾斜度  
                    style: {
                        color: '#FFFFFF',
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif',
                        textShadow: '0 0 3px black'
                    }
                },
                categories: ZSZLPH
            },
            yAxis: {
                min: 0,
                title: {
                    text: '分值',
                    style: {
                        color: '#fff',
                        fontSize: '18px'
                    }
                },
                labels: {
                    style: {
                        color: '#fff'
                    }
                }
                , tickPositions: [0, 20, 40, 60, 80, 100] // 指定竖轴坐标点的值
            },
            credits: {
                enabled: false
            },
            legend: {
                enabled: false
            },
            tooltip: {
                backgroundColor: "#333",
                borderWidth: 0,
                style: {
                    color: '#FFFFFF',
                    fontSize: '12px',
                    padding: '8px'
                }
            },
            plotOptions: {
                column: {
                    pointWidth: 30,
                    stacking: 'normal',
                    cursor: 'pointer',
                    point: {
                        events: {
                            click: function (e) {
                                //$.alert(e.point.category + ':' + e.point.y, '提示');
                                MATNAME = e.point.category;
                                $('#Chartcon').slideUp(500);
                                $('#Chartscon').show();
                                LoadCharts(tp);
                            }
                        }
                    }
                }
            },
            series: [{
                name: '平均分值',
                data: ZSZLDF,
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
            }]
        };
        $("#Chart").highcharts(chartoption);
    };
    
    function LoadCharts(tp) {

        var ZSZLPCDF = [];
        var ZSZLPC = [];
        app.DataRequest('getDataLists', { state: tp, matname: MATNAME }, function (data) {
            var dt = eval(data);
            if (dt.length > 0) {
                $.each(dt, function (i, n) {
                    ZSZLPC.push(n.BATCHCODE);
                    ZSZLPCDF.push(n.SCORE * 1);
                });
            }
        }, null, false);

        var chartoption = {
            chart: {
                type: 'column',
                backgroundColor: "transparent"
                //margin: [10, 10, 90, 70]
            },
            title: {
                text: '牌号批次得分',
                style: {
                    color: '#C4C4C4',
                    fontSize: '30px',
                    fontFamily: 'Verdana, sans-serif',
                    textShadow: '0 0 3px black'
                }
            },
            subtitle: {
                text: MATNAME,
                style: {
                    color: '#fff',
                    fontSize: '14px',
                    fontFamily: 'Verdana, sans-serif',
                    textShadow: '0 0 3px black'
                }
            },
            xAxis: {
                title: { text: '' },
                labels: {
                    rotation: -30,//倾斜度  
                    step: 1, staggerLines: 1,
                    style: {
                        color: '#FFFFFF',
                        fontSize: '10px'
                    }
                },
                categories: ZSZLPC
            },
            yAxis: {
                min: 0,
                title: {
                    text: '分值',
                    style: {
                        color: '#fff',
                        fontSize: '18px'
                    }
                },
                labels: {
                    style: {
                        color: '#fff'
                    }
                }
                , tickPositions: [0, 20, 40, 60, 80, 100] // 指定竖轴坐标点的值
            },
            credits: {
                enabled: false
            },
            legend: {
                enabled: false
            },
            tooltip: {
                backgroundColor: "#333",
                borderWidth: 0,
                style: {
                    color: '#FFFFFF',
                    fontSize: '12px',
                    padding: '8px'
                }
            },
            plotOptions: {
                column: {
                    pointWidth: 15,
                    overflow: "none",
                    crop: true,
                    point: {
                        events: {
                            click: function (e) {
                                //$.alert(e.point.category + ':' + e.point.y, '提示');
                            }
                        }
                    }
                }
            },
            series: [{
                name: '分值',
                data: ZSZLPCDF,
                dataLabels: {
                    enabled: true,
                    rotation: -45,
                    overflow: "none",
                    crop: false,
                    color: '#FFFFFF',
                    align: 'center',
                    verticalAlign: 'top',
                    x: 0,
                    y: 20,
                    style: {
                        fontSize: '10px'
                        //,
                        //fontFamily: 'Verdana, sans-serif',
                        //textShadow: '0 0 3px black'
                    }
                }
            }]
        };
        $("#Charts").highcharts(chartoption);
    };

    $scope.returnMain = function ()
    {
        $('#Chartscon').slideDown(500);
        $('#Chartcon').slideDown(500);
    }
    $scope.QueryData(2);
})