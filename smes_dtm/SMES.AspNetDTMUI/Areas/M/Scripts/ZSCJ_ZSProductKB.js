var storeapp = angular.module('APP', []);/*将一个些通用的低级开发操作包装起来提供给开发者,AngularJS会自动处理好这些低级操作*/
storeapp.controller('CTRL', function ($scope, $http) {
    var RunYearDLx = [];
    var RunYearDLYXy = [];
    var RunYearDLGXy = [];

    var RunMonthDLx = [];
    var RunMonthDLYXy = [];
    var RunMonthDLGXy = [];

    var RunYearHGx = [];
    var RunYearHGYXy = [];
    var RunYearHGGXy = [];

    var RunMonthHGx = [];
    var RunMonthHGYXy = [];
    var RunMonthHGGXy = [];

    //3：数据查询
    $scope.QueryData = function () {

        GetYear_DLRate();
        GetMonth_DLRate();
        GetYear_HGRate();
        GetMonth_HGRate();

        $('#QueryTip').html('折线图');

        LoadChart_Rate();
    }

    function GetYear_DLRate() {
        app.DataRequest('GetYear_DLRate', {}, function (data) {
            var dt = eval(data);
            RunYearDLx = [];
            RunYearDLYXy = [];
            RunYearDLGXy = [];
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunYearDLx.push(value.PLANDATE);
                    RunYearDLYXy.push(Number(value.YXDL));
                    RunYearDLGXy.push(Number(value.GXDL));
                })
            }

            //var arr1 = RunYearDLZXy;
            //for (var i = 0; i < arr1.length; i++) {
            //    if (arr1[i] == 0) {
            //        arr1[i] = null;
            //    }
            //}
            //RunYearDLZXy = arr1;

            //var arr2 = RunYearDLPPXy;
            //for (var i = 0; i < arr2.length; i++) {
            //    if (arr2[i] == 0) {
            //        arr2[i] = null;
            //    }
            //}
            //RunYearDLPPXy = arr2;

        }, null, false);
    }

    function GetMonth_DLRate() {
        app.DataRequest('GetMonth_DLRate', {}, function (data) {
            var dt = eval(data);
            RunMonthDLx = [];
            RunMonthDLYXy = [];
            RunMonthDLGXy = [];
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunMonthDLx.push(value.PLANDATE);
                    RunMonthDLYXy.push(Number(value.YXDL));
                    RunMonthDLGXy.push(Number(value.GXDL));
                })
            }

            //var arr1 = RunMonthDLZXy;
            //for (var i = 0; i < arr1.length; i++) {
            //    if (arr1[i] == 0) {
            //        arr1[i] = null;
            //    }
            //}
            //RunMonthDLZXy = arr1;

            //var arr2 = RunMonthDLPPXy;
            //for (var i = 0; i < arr2.length; i++) {
            //    if (arr2[i] == 0) {
            //        arr2[i] = null;
            //    }
            //}
            //RunMonthDLPPXy = arr2;

        }, null, false);
    }

    function GetYear_HGRate() {
        app.DataRequest('GetYear_HGRate', {}, function (data) {
            var dt = eval(data);
            RunYearHGx = [];
            RunYearHGYXy = [];
            RunYearHGGXy = [];
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunYearHGx.push(value.PLANDATE);
                    RunYearHGYXy.push(Number(value.YXNY));
                    RunYearHGGXy.push(Number(value.GXNY));
                })
            }

            //var arr1 = RunYearHGZXy;
            //for (var i = 0; i < arr1.length; i++) {
            //    if (arr1[i] == 0) {
            //        arr1[i] = null;
            //    }
            //}
            //RunYearHGZXy = arr1;

            //var arr2 = RunYearHGPPXy;
            //for (var i = 0; i < arr2.length; i++) {
            //    if (arr2[i] == 0) {
            //        arr2[i] = null;
            //    }
            //}
            //RunYearHGPPXy = arr2;

        }, null, false);
    }

    function GetMonth_HGRate() {
        app.DataRequest('GetMonth_HGRate', {}, function (data) {
            var dt = eval(data);
            RunMonthHGx = [];
            RunMonthHGYXy = [];
            RunMonthHGGXy = [];
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunMonthHGx.push(value.PLANDATE);
                    RunMonthHGYXy.push(Number(value.YXNY));
                    RunMonthHGGXy.push(Number(value.GXNY));
                })
            }

            //var arr1 = RunMonthHGZXy;
            //for (var i = 0; i < arr1.length; i++) {
            //    if (arr1[i] == 0) {
            //        arr1[i] = null;
            //    }
            //}
            //RunMonthHGZXy = arr1;

            //var arr2 = RunMonthHGPPXy;
            //for (var i = 0; i < arr2.length; i++) {
            //    if (arr2[i] == 0) {
            //        arr2[i] = null;
            //    }
            //}
            //RunMonthHGPPXy = arr2;

        }, null, false);
    }

   
    ////:加载折线图
    function LoadChart_Rate(btn_value) {
        debugger;
        var chartoption2 = {
            chart: {
                renderTo: 'container',
                type: 'line',
                backgroundColor: "transparent",
                animation: false,
                style:
                {
                    fontFamily: 'Microsoft YaHei',
                    fontSize: '12px',
                    color: '#262626'
                },
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
                categories: RunYearDLx
            },
            yAxis: {
                //min: 0,
                //max: 0.5,    //设置Y轴最大值为“max”
                //tickInterval: 0.1,   //设置Y轴坐标值的间隔固定为100
                allowDecimals: false,
                title: {
                    text: '断料率（%）'
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
                line: {
                    pointPadding: 0,
                    borderWidth: 0,
                    dataLabels: {
                        overflow: "none",
                        crop: false,
                    }
                },
                series: {
                    borderRadius: 2,
                    connectNulls: true
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
                this.x + ':   ' + '断料率' + this.y + '%';
                }
            },
            series: [
                {
                    type: 'line',
                    name: '叶线',
                    data: RunYearDLYXy,
                    color: '#0000FF',
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

                    },
                },
                {
                    type: 'line',
                    name: '梗线',
                    data: RunYearDLGXy,
                    color: '#FF0000',
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        //color: 'blue',
                        color: '#FFFF00',
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

        var chartoption = {
            chart: {
                renderTo: 'container',
                type: 'line',
                backgroundColor: "transparent",
                animation: false,
                style:
                {
                    fontFamily: 'Microsoft YaHei',
                    fontSize: '12px',
                    color: '#262626'
                },
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
                categories: RunMonthDLx
            },
            yAxis: {
                //min: 0,
                //max: 0.5,    //设置Y轴最大值为“max”
                //tickInterval: 0.1,   //设置Y轴坐标值的间隔固定为100
                allowDecimals: false,
                title: {
                    text: '断料率（%）',
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
                line: {
                    pointPadding: 0,
                    borderWidth: 0,
                    dataLabels: {
                        overflow: "none",
                        crop: false,
                    }
                },
                series: {
                    borderRadius: 2,
                    connectNulls: true
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
                this.x + ':   ' + '断料率' + this.y + '%';
                }
            },
            series: [
                {
                    type: 'line',
                    name: '叶线',
                    data: RunMonthDLYXy,
                    color: '#0000FF',
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
                },
                {
                    type: 'line',
                    name: '梗线',
                    data: RunMonthDLGXy,
                    color: '#FF0000',
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        //color: 'blue',
                        color: '#FFFF00',
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

        var chartoption3 = {
            chart: {
                renderTo: 'container',
                type: 'line',
                backgroundColor: "transparent",
                animation: false,
                style:
                {
                    fontFamily: 'Microsoft YaHei',
                    fontSize: '12px',
                    color: '#262626'
                },
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
                categories: RunMonthHGx
            },
            yAxis: {
               // min: 92,
                max: 102,
                tickInterval: 2,   //设置Y轴坐标值的间隔固定为100
                //categories:[0,92,94,96,98,100,102],
                allowDecimals: false,
                title: {
                    text: '合格率（%）',
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
                line: {
                    pointPadding: 0,
                    borderWidth: 0,
                    dataLabels: {
                        overflow: "none",
                        crop: false,
                    }
                },
                series: {
                    borderRadius: 2,
                    connectNulls: true
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
                this.x + ':   ' + '合格率' + this.y + '%';
                }
            },
            series: [
                {
                    type: 'line',
                    name: '叶线',
                    data: RunMonthHGYXy,
                    color: '#0000FF',
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
                },
                {
                    type: 'line',
                    name: '梗线',
                    data: RunMonthHGGXy,
                    color: '#FF0000',
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        //color: 'blue',
                        color: '#FFFF00',
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

        var chartoption4 = {
            chart: {
                renderTo: 'container',
                type: 'line',
                backgroundColor: "transparent",
                animation: false,
                style:
                {
                    fontFamily: 'Microsoft YaHei',
                    fontSize: '12px',
                    color: '#262626'
                },
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
                categories: RunYearHGx
            },
            yAxis: {
                //min: 95,
                //max: 105,    //设置Y轴最大值为“max”
                //tickInterval: 2,   //设置Y轴坐标值的间隔固定为100
                allowDecimals: false,
                title: {
                    text: '合格率（%）',
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
                line: {
                    pointPadding: 0,
                    borderWidth: 0,
                    dataLabels: {
                        overflow: "none",
                        crop: false,
                    }
                },
                series: {
                    borderRadius: 2,
                    connectNulls: true
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
                this.x + ':   ' + '合格率' + this.y + '%';
                }
            },
            series: [
                {
                    type: 'line',
                    name: '叶线',
                    data: RunYearHGYXy,
                    color: '#0000FF',
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
                },
                {
                    type: 'line',
                    name: '梗线',
                    data: RunYearHGGXy,
                    color: '#FF0000',
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        //color: 'blue',
                        color: '#FFFF00',
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

        $("#ZS_Year_DLRate").highcharts(chartoption2);
        $("#ZS_Month_DLRate").highcharts(chartoption);

        $("#ZS_Year_HGRate").highcharts(chartoption4);
        $("#ZS_Month_HGRate").highcharts(chartoption3);
    }
   
    //function st() {
    //    //setInterval("GetMonth_JYZ()", 10000);
    //    //setInterval("GetMonth_ZB()", 10000);
    //    //setInterval("GetMonth_XiaoH()", 10000);
    //    //setInterval("GetMonth_TiaoH()", 10000);
    //}

    setInterval(function () {
        GetYear_DLRate();
        GetMonth_DLRate();
        GetYear_HGRate();
        GetMonth_HGRate();
        LoadChart_Rate();
    }, 60000);

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