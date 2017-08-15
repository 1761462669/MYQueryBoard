var storeapp = angular.module('APP', []);/*将一个些通用的低级开发操作包装起来提供给开发者,AngularJS会自动处理好这些低级操作*/
storeapp.controller('CTRL', function ($scope, $http) {

    var Runx = [];
    var Runy = [];//卷包过程合格率
    var Runy1 = [];//卷包成品入库合格率

    var _Runx = [];
    var _Runy = [];//制丝合格率



    //3：数据查询
    $scope.QueryData = function () {

        // $scope.SelectType = btn_value;

        GetQuery(1);
        GetQuery(2);

        $('#QueryTip').html('制丝车间');
        LoadChart_ZS(1);
        $('#QueryTip').html('卷包车间');
        LoadChart_JB(2);




    }


    //4：数据查询中的方法调用
    //a:获取展示值
    function GetQuery(btn_value) {

        var con = '';
        con += " type='" + btn_value + "' ";
        app.DataRequest('GetQua', { strCon: con }, function (data) {
            var dt = eval(data);
            if (dt.length > 0) {
                if (btn_value == 1) {
                    $.each(dt, function (n, value) {
                        _Runx.push(value.pricetypeName);
                        _Runy.push(Number(value.relqua));
                    })
                }
                else if (btn_value == 2) {
                    $.each(dt, function (n, value) {
                        Runx.push(value.pricetypeName);
                        Runy.push(Number(value.relqua));
                        Runy1.push(Number(value.daqua));
                    })
                }

            }
        }, null, false);
    }

    //:加载制丝质量图像
    function LoadChart_ZS(btn_value) {
        var chartoption = {
            chart: {
                renderTo: 'container',
                type: 'column',
                margin: [35, 10, 40, 50],
                backgroundColor: "transparent",
                animation: false,
                style:
                {
                    fontFamily: 'Microsoft YaHei',
                    fontSize: '12px',
                    color: '#262626'
                }
            },
            title: {
                text: '',
                style: {
                    color: '#fff',
                    fontSize: 30
                }
            },
            subtitle: {
                text: '',
                display: 'none'
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
                categories: _Runx
            },
            yAxis: {
                min: 0,
                allowDecimals: false,
                title: {
                    text: ''
                },
                labels: {
                    style: {
                        color: '#fff'
                    }
                }
                // , tickPositions: RunJLy  //[0, 20, 40, 60, 80, 100] // 指定竖轴坐标点的值
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
                align: 'center',
                verticalAlign: 'top',
                x: 0,
                y: 0,
                itemStyle: {
                    color: '#999',
                    fontWeight: "normal"
                },
                itemHoverStyle: {
                    color: '#44b9dc'
                },
                enabled: true
            },
            tooltip: {
                backgroundColor: "#333",
                borderWidth: 0,
                style: {
                    color: '#FFFFFF',
                    fontSize: '12px',
                    padding: '8px'
                }, formatter: function () {
                    return '' +
                this.x + ': ' + this.y + '%';
                }
            },
            series: [
                {
                    type: 'column',
                    name: '制丝质量',
                    yAxis: 0,
                    data: _Runy,
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        color: '#FFFFFF',
                        align: 'center',
                        //x: 4,
                        //y: -10,
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif',
                            textShadow: '0 0 4px black'
                        }, formatter: function () {
                            return this.y + '%';

                        }

                    }
                }
            ]

        };
        $("#Qua_ZS").highcharts(chartoption);

    }
    //:加载卷包质量
    function LoadChart_JB(btn_value) {
        var chartoption = {
            chart: {
                renderTo: 'container',
                type: 'column',
                margin: [35, 10, 40, 50],
                backgroundColor: "transparent",
                animation: false,
                style:
                {
                    fontFamily: 'Microsoft YaHei',
                    fontSize: '12px',
                    color: '#262626'
                }
            },
            title: {
                text: '',
                style: {
                    color: '#fff',
                    fontSize: 30
                }
            },
            subtitle: {
                text: '',
                display: 'none'
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
                categories: Runx
            },
            yAxis: {
                min: 0,
                allowDecimals: false,
                title: {
                    text: ''
                },
                labels: {
                    style: {
                        color: '#fff'
                    }
                }
                // , tickPositions: RunJLy  //[0, 20, 40, 60, 80, 100] // 指定竖轴坐标点的值
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
                align: 'center',
                verticalAlign: 'top',
                x: 0,
                y: 0,
                itemStyle: {
                    color: '#999',
                    fontWeight: "normal"
                },
                itemHoverStyle: {
                    color: '#44b9dc'
                },
                enabled: true
            },
            tooltip: {
                backgroundColor: "#333",
                borderWidth: 0,
                style: {
                    color: '#FFFFFF',
                    fontSize: '12px',
                    padding: '8px'
                }, formatter: function () {
                    return '' +
                this.x + ': ' + this.y + '%';
                }
            },
            series: [
                {
                    type: 'column',
                    name: '卷包过程质量',
                    yAxis: 0,
                    data: Runy,
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        color: '#FFFFFF',
                        align: 'center',
                        //x: 4,
                        //y: -10,
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif',
                            textShadow: '0 0 4px black'
                        }, formatter: function () {
                            return this.y + '%';
                        }

                    }
                },
                {
                    type: 'column',
                    name: '卷包成品入库质量',
                    yAxis: 0,
                    data: Runy1,
                    color: '#90ed7d',
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        color: '#625f5f',
                        align: 'center',
                        //x: 4,
                        //y: -10,
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif',
                            textShadow: '0 0 4px black'
                        }, formatter: function () {
                            return this.y + '%';
                        }

                    }
                }

            ]

        };
        $("#Qua_JB").highcharts(chartoption);

    }


    /**
     ** 加法函数，用来得到精确的加法结果
     ** 说明：javascript的加法结果会有误差，在两个浮点数相加的时候会比较明显。这个函数返回较为精确的加法结果。
     ** 调用：accAdd(arg1,arg2)
     ** 返回值：arg1加上arg2的精确结果
     **/
    function accAdd(arg1, arg2) {
        var r1, r2, m, c;
        try {
            r1 = arg1.toString().split(".")[1].length;
        }
        catch (e) {
            r1 = 0;
        }
        try {
            r2 = arg2.toString().split(".")[1].length;
        }
        catch (e) {
            r2 = 0;
        }
        c = Math.abs(r1 - r2);
        m = Math.pow(10, Math.max(r1, r2));
        if (c > 0) {
            var cm = Math.pow(10, c);
            if (r1 > r2) {
                arg1 = Number(arg1.toString().replace(".", ""));
                arg2 = Number(arg2.toString().replace(".", "")) * cm;
            } else {
                arg1 = Number(arg1.toString().replace(".", "")) * cm;
                arg2 = Number(arg2.toString().replace(".", ""));
            }
        } else {
            arg1 = Number(arg1.toString().replace(".", ""));
            arg2 = Number(arg2.toString().replace(".", ""));
        }
        return (arg1 + arg2) / m;
    }

    $scope.QueryData();//默认展示
})