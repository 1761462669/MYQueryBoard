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

    $('#ConStart').datepicker({
        onSelect: function () {
            $scope.QueryData($scope.SelectType);
        }
    });
    $('#ConEnd').datepicker({
        onSelect: function () {
            $scope.QueryData($scope.SelectType);
        }
    });
    var vDate = new Date();
    $("#ConStart").val(new Date(vDate.valueOf() - 1 * 24 * 60 * 60 * 1000).Format("yyyy-MM-dd"));
    $("#ConEnd").val(new Date(vDate.valueOf() - 1 * 24 * 60 * 60 * 1000).Format("yyyy-MM-dd"));
    //var vDate = new Date();
    //document.getElementById('ConStart').valueAsDate = new Date(vDate.valueOf() - 1 * 24 * 60 * 60 * 1000);
    ////document.getElementById('ConEnd').valueAsDate = vDate;
    //document.getElementById('ConEnd').valueAsDate = new Date(vDate.valueOf() - 1 * 24 * 60 * 60 * 1000);
    var RunJT=[];
    var RunPCT = [];
    var RunJTC = [];
    var RunCN = [];
    $scope.QueryData = function (t) {
        $scope.SelectType = t;
        GetEffRun(t);
        GetCapacity(t);
        LoadChart(t);
    };
    function GetEffRun(t) {
        RunJT=[];
        RunPCT=[];
        var startTime = $('#ConStart').val();
        var endTime = $('#ConEnd').val();
        var con = '';
        if (t == '全部') {
            con += ' 1=1 ';
        }
        else {
            con += " TEAMNAME='"+t+"' ";
        }
        app.DataRequest('GetEfficiencyRun', { strStartTime: startTime, strEndTime: endTime, strCon: con }, function (data) {
            var dt = eval(data);
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunJT.push(value.PROCESSUNITNAME);
                    RunPCT.push(value.PCT *1);
                })
            }
        }, null, false);
    }

    function GetCapacity(t) {
        RunJTC = [];
        RunCN = [];
        var startTime = $('#ConStart').val();
        var endTime = $('#ConEnd').val();
        var con = '';
        if (t == '全部') {
            con += ' 1=1 ';
        }
        else {
            con += " TEAMNAME='" + t + "' ";
        }
        app.DataRequest('GetCapacity', { strStartTime: startTime, strEndTime: endTime, strCon: con }, function (data) {
            var dt = eval(data);
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunJTC.push(value.PROCESSUNITNAME);
                    RunCN.push(value.PCT * 1);
                })
            }
        }, null, false);
    }

    function LoadChart(t) {

        var chartoption = {
            chart: {
                type: 'column',
                backgroundColor: "transparent",
                margin: [40, 0, 60, 80]
            },
            title: {
                text: '',
                style: {
                    color: '#fff',
                    fontSize: 30
                }
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                title: { text: '' },
                labels: {
                    style: {
                        color: '#FFFFFF',
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif',
                        textShadow: '0 0 3px black'
                    }
                },
                categories: RunJT
            },
            yAxis: {
                min: 0,
                title: {
                    text: ''
                },
                labels: {
                    style: {
                        color: '#fff'
                    }
                }
                //, tickPositions: [0, 20, 40, 60, 80, 100] // 指定竖轴坐标点的值
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
                }, pointFormat: '<b>{point.y:.2f}%</b>',
            },
            series: [
                {
                    name: "占比",
                    data: RunPCT,
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        color: '#FFFFFF',
                        align: 'center',
                        //x: 4,
                        y: -10,
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif',
                            textShadow: '0 0 4px black'
                        }, formatter: function () {
                            //return this.y + '%';
                            return this.y;
                        }

                    }
                }

            ]

        };

        var chartoption1 = {
            chart: {
                type: 'column',
                backgroundColor: "transparent",
                margin: [40, 0, 60, 80]
            },
            title: {
                text: '',
                style: {
                    color: '#fff',
                    fontSize: 30
                }
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                title: { text: '' },
                labels: {
                    style: {
                        color: '#FFFFFF',
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif',
                        textShadow: '0 0 3px black'
                    }
                },
                categories: RunJTC
            },
            yAxis: {
                min: 0,
                title: {
                    text: ''
                },
                labels: {
                    style: {
                        color: '#fff'
                    }
                }
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
                }, pointFormat: '<b>{point.y:.1f}</b>',
            },
            series: [
                {
                    name: "占比",
                    data: RunCN,
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        color: '#FFFFFF',
                        align: 'center',
                        //x: 10,
                        y: -10,
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif'
                           ,textShadow: '0 0 2px black'
                        }, formatter: function () {
                            return this.y;
                        }

                    }
                }

            ]

        };

        $("#MainChart").highcharts(chartoption);
        $("#MainChart1").highcharts(chartoption1);
    }

    $scope.QueryData('全部');
})