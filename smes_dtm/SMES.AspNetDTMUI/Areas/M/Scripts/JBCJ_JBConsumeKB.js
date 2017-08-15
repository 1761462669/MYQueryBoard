var storeapp = angular.module('APP', []);/*将一个些通用的低级开发操作包装起来提供给开发者,AngularJS会自动处理好这些低级操作*/
storeapp.controller('CTRL', function ($scope, $http) {
    var RunZBx = [];
    var RunZBy = [];

    var RunXHx = [];
    var RunXHy = [];

    var RunJYZx = [];
    var RunJYZy = [];

    var RunTHx = [];
    var RunTHy = [];


    //3：数据查询
    $scope.QueryData = function () {
        GetMonth_ZB();
        GetMonth_JYZ();
        GetMonth_XiaoH();
        GetMonth_TH();


        $('#QueryTip').html('柱状图');
        LoadChart_ZB();
        LoadChart_XiaoH();
        LoadChart_JYZ();
        LoadChart_TH();
        //time1();

    }


    setInterval(function () {
        GetMonth_ZB();
        GetMonth_XiaoH();
        GetMonth_JYZ();
        GetMonth_TH();
        LoadChart_ZB();
        LoadChart_XiaoH();
        LoadChart_JYZ();
        LoadChart_TH();
    }, 6000000)//10分钟


    function GetMonth_TH() {
        app.DataRequest('GetMonth_TiaoH', {}, function (data) {
            var dt = eval(data);
            RunTHx = [];
            RunTHy = [];
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunTHx.push(value.bill_date);
                    RunTHy.push(Number(value.quantity));
                })
            }
        }, null, false);
    }

    function GetMonth_JYZ() {
        app.DataRequest('GetMonth_JYZ', {}, function (data) {
            var dt = eval(data);
            RunJYZx = [];
            RunJYZy = [];
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunJYZx.push(value.bill_date);
                    RunJYZy.push(Number(value.quantity));
                })
            }
        }, null, false);
    }

    function GetMonth_ZB() {
        app.DataRequest('GetMonth_ZB', {}, function (data) {
            var dt = eval(data);
            RunZBx = [];
            RunZBy = [];
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunZBx.push(value.bill_date);
                    RunZBy.push(Number(value.quantity));
                })
            }
        }, null, false);
    }

    function GetMonth_XiaoH() {
        app.DataRequest('GetMonth_XiaoH', {}, function (data) {
            var dt = eval(data);
            RunXHx = [];
            RunXHy = [];
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunXHx.push(value.bill_date);
                    RunXHy.push(Number(value.quantity));
                })
            }
        }, null, false);
    }

    ////:加载柱状图
    function LoadChart_ZB() {
        var chartoption2 = {
            chart: {
                renderTo: 'container',
                type: 'column',
                //height: 300,
                //width:640,
                //margin: [35, 10, 40, 50],
                backgroundColor: "transparent",
                animation: false,
                style:
                {
                    fontFamily: 'Microsoft YaHei',
                    fontSize: '12px',
                    color: '#262626'
                },
                //events: {
                //    load: st// 定时器
                //},
                //marginTop: 10,
                marginRight: 20
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
                categories: RunZBx
            },
            yAxis: {
                //min: 9000,
                //max: 12000,    //设置Y轴最大值为“max”
                // tickInterval: 500,   //设置Y轴坐标值的间隔固定为100
                allowDecimals: false,
                title: {
                    text: '嘴棒单耗（支/箱）'
                },
                labels: {
                    style: {
                        color: '#fff'
                    }
                },
                gridLineColor: '#DEDEDE',
                gridLineDashStyle: 'Dot',//横向网格线样式
            },
            credits: {
                enabled: false
            },

            plotOptions: {
                column: {
                    pointPadding: 0,
                    borderWidth: 0,
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
                    fontSize: '10px',
                    padding: '8px'
                }, formatter: function () {
                    return '' +
                this.x + ':   ' + '单箱耗：' + this.y;
                }
            },
            series: [
                {
                    type: 'column',
                    name: '嘴棒单耗【支/箱】',
                    data: RunZBy,
                    //color: 'blue',
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        //color: 'blue',
                        color: '#FFFFFF',
                        align: 'center',
                        y: -10,
                        style: {
                            fontSize: '5px',
                            fontFamily: 'Verdana, sans-serif',
                            textShadow: '0 0 4px black'
                        }, formatter: function () {
                            return this.y;
                        }

                    }
                }
            ]

        };
        $("#MainChart_Month_ZB").highcharts(chartoption2);
    }

    function LoadChart_XiaoH() {
        var chartoption2 = {
            chart: {
                renderTo: 'container',
                type: 'column',
                //height: 300,
                //width:640,
                //margin: [35, 10, 40, 50],
                backgroundColor: "transparent",
                animation: false,
                style:
                {
                    fontFamily: 'Microsoft YaHei',
                    fontSize: '12px',
                    color: '#262626'
                },
                //events: {
                //    load: st// 定时器
                //},
                //marginTop: 10,
                marginRight: 20
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
                categories: RunXHx
            },
            yAxis: {
                //min: 2500,
                // max: 2520,    //设置Y轴最大值为“max”
                //tickInterval: 4,   //设置Y轴坐标值的间隔固定为100
                allowDecimals: false,
                title: {
                    text: '小盒单耗（张/箱）'
                },
                labels: {
                    style: {
                        color: '#fff'
                    }
                },
                gridLineColor: '#DEDEDE',
                gridLineDashStyle: 'Dot',//横向网格线样式
            },
            credits: {
                enabled: false
            },

            plotOptions: {
                column: {
                    pointPadding: 0,
                    borderWidth: 0,
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
                    fontSize: '10px',
                    padding: '8px'
                }, formatter: function () {
                    return '' +
                this.x + ':   ' + '单箱耗：' + this.y;
                }
            },
            series: [
                {
                    type: 'column',
                    name: '小盒单耗【张/箱】',
                    data: RunXHy,
                    //color: 'blue',
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        //color: 'blue',
                        color: '#FFFFFF',
                        align: 'center',
                        y: -10,
                        style: {
                            fontSize: '5px',
                            fontFamily: 'Verdana, sans-serif',
                            textShadow: '0 0 4px black'
                        }, formatter: function () {
                            return this.y;
                        }

                    }
                }
            ]

        };
        $("#MainChart_Month_XH").highcharts(chartoption2);
    }

    function LoadChart_TH() {
        var chartoption2 = {
            chart: {
                renderTo: 'container',
                type: 'column',
                //height: 300,
                //width:640,
                //margin: [35, 10, 40, 50],
                backgroundColor: "transparent",
                animation: false,
                style:
                {
                    fontFamily: 'Microsoft YaHei',
                    fontSize: '12px',
                    color: '#262626'
                },
                //events: {
                //    load: st// 定时器
                //},
                //marginTop: 10,
                marginRight: 20
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
                categories: RunXHx
            },
            yAxis: {
                //min: 2500,
                // max: 2520,    //设置Y轴最大值为“max”
                //tickInterval: 4,   //设置Y轴坐标值的间隔固定为100
                allowDecimals: false,
                title: {
                    text: '条盒单耗（张/箱）'
                },
                labels: {
                    style: {
                        color: '#fff'
                    }
                },
                gridLineColor: '#DEDEDE',
                gridLineDashStyle: 'Dot',//横向网格线样式
            },
            credits: {
                enabled: false
            },

            plotOptions: {
                column: {
                    pointPadding: 0,
                    borderWidth: 0,
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
                    fontSize: '10px',
                    padding: '8px'
                }, formatter: function () {
                    return '' +
                this.x + ':   ' + '单箱耗：' + this.y;
                }
            },
            series: [
                {
                    type: 'column',
                    name: '条盒单耗【张/箱】',
                    data: RunTHy,
                    //color: 'blue',
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        //color: 'blue',
                        color: '#FFFFFF',
                        align: 'center',
                        y: -10,
                        style: {
                            fontSize: '5px',
                            fontFamily: 'Verdana, sans-serif',
                            textShadow: '0 0 4px black'
                        }, formatter: function () {
                            return this.y;
                        }

                    }
                }
            ]

        };
        $("#MainChart_Month_TH").highcharts(chartoption2);
    }

    function LoadChart_JYZ() {
        var chartoption2 = {
            chart: {
                renderTo: 'container',
                type: 'column',
                //height: 300,
                //width:640,
                //margin: [35, 10, 40, 50],
                backgroundColor: "transparent",
                animation: false,
                style:
                {
                    fontFamily: 'Microsoft YaHei',
                    fontSize: '12px',
                    color: '#262626'
                },
                //events: {
                //    load: st// 定时器
                //},
                //marginTop: 10,
                marginRight: 20
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
                categories: RunXHx
            },
            yAxis: {
                //min: 2500,
                // max: 2520,    //设置Y轴最大值为“max”
                //tickInterval: 4,   //设置Y轴坐标值的间隔固定为100
                allowDecimals: false,
                title: {
                    text: '卷烟纸单耗（盘/箱）'
                },
                labels: {
                    style: {
                        color: '#fff'
                    }
                },
                gridLineColor: '#DEDEDE',
                gridLineDashStyle: 'Dot',//横向网格线样式
            },
            credits: {
                enabled: false
            },

            plotOptions: {
                column: {
                    pointPadding: 0,
                    borderWidth: 0,
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
                    fontSize: '10px',
                    padding: '8px'
                }, formatter: function () {
                    return '' +
                this.x + ':   ' + '单箱耗：' + this.y;
                }
            },
            series: [
                {
                    type: 'column',
                    name: '卷烟纸单耗【盘/箱】',
                    data: RunJYZy,
                    //color: 'blue',
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        //color: 'blue',
                        color: '#FFFFFF',
                        align: 'center',
                        y: -10,
                        style: {
                            fontSize: '5px',
                            fontFamily: 'Verdana, sans-serif',
                            textShadow: '0 0 4px black'
                        }, formatter: function () {
                            return this.y;
                        }

                    }
                }
            ]

        };
        $("#MainChart_Month_JYZ").highcharts(chartoption2);
    }
    //function st() {
    //    setInterval("GetMonth_ZB()", 3600000);
    //    setInterval("GetMonth_XiaoH()", 3600000);
    //}

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

    $scope.QueryData();
})