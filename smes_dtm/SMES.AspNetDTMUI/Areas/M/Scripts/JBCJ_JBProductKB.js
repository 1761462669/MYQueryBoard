var storeapp = angular.module('APP', []);/*将一个些通用的低级开发操作包装起来提供给开发者,AngularJS会自动处理好这些低级操作*/
storeapp.controller('CTRL', function ($scope, $http) {
    var PlanQua;
    var Monthrate;

    var Dayrate;
    var DayPlanProduct;

    var RunPHx = [];

    var RunPHy1 = [];
    var RunPHy2 = [];

    var RunJTx = [];
    var RunJTy = [];
    var chart;

    // 定义获取和更新时间的函数
    //$scope.times = new Date().toLocaleString();
    var showTime = function () {
        var i_t, i_w, i_n, mydate, year, month, day, hours, minutes, seconds, week, mm;
        mydate = new Date();
        year = mydate.getFullYear();
        month = mydate.getMonth() + 1;
        day = mydate.getDate();
        hours = mydate.getHours();
        minutes = mydate.getMinutes();
        seconds = mydate.getSeconds();

        switch (mydate.getDay()) {
            case 0: week = "星期日"
                break
            case 1: week = "星期一"
                break
            case 2: week = "星期二"
                break
            case 3: week = "星期三"
                break
            case 4: week = "星期四"
                break
            case 5: week = "星期五"
                break
            case 6: week = "星期六"
                break
        }
        i_t = hours + ":" + minutes + ":" + seconds;
        i_w = week;
        i_n = year + "年" + month + "月" + day + "日";
        //document.getElementById("showTime").innerHTML = i;

        $scope.times_t = i_t;
        $scope.times_w = i_w;
        $scope.times_n = i_n;

    }
    showTime();
    //班次班组，班产量
    var Shift_Team_Prdouct = function () {
        app.DataRequest('Shift_Team_Prdouct', {}, function (data) {
            //获取数据后的操作
            var dt = eval(data);
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    DayPlanProduct = value.PlanProduct;
                    Dayrate = Number(value.PRODUCT) / Number(value.PlanProduct) * 100;
                    if (Dayrate > 100) {
                        Dayrate = 100;
                    }
                    $scope.shiftid = "班次: " + value.SHIFTNAME;
                    $scope.teamid = "班组: " + value.TEAMNAME;
                    $scope.teamdayproduct = "当班产量:" + value.PRODUCT;
                    //$("#shiftid").html("班次:" + value.SHIFTNAME);
                    //$("#teamid").html("班组:" + value.TEAMNAME);
                    //$("#teamdayproduct").html("当班产量:" + value.PRODUCT);
                })
            }
            else {
                //$("#shiftid").html("班次:未排班");
                //$("#teamid").html("班组:未排班");
                //$("#teamdayproduct").html("当班产量:未排班");
                $scope.shiftid = "班次:未排班";
                $scope.teamid = "班组:未排班";
                $scope.teamdayproduct = "当班产量:未排班";
            }

        }, null, false);
    };
    Shift_Team_Prdouct();
    //当月产量,计划量
    var Month_Prdouct = function () {
        app.DataRequest('Month_Prdouct', {}, function (data) {
            //获取数据后的操作
            var dt = eval(data);
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    PlanQua = value.PlanProduct;
                    Monthrate = Number(value.PRODUCT) / Number(value.PlanProduct) * 100;
                    if (Monthrate > 100) {
                        Monthrate = 100;
                    }

                    $scope.monthproduct = "当月产量:" + value.PRODUCT;
                })
            }
            else {
                $scope.monthproduct = "当月产量:未生产";
            }

        }, null, false);
    }
    Month_Prdouct();
    //settime1();
    //clearInterval(settime1);

    //当月牌号产量
    var Month_Mat_Product = function () {
        RunPHx = [];
        RunPHy1 = [];
        RunPHy2 = [];
        app.DataRequest('Month_Mat_Product', {}, function (data) {
            var dt = eval(data);

            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunPHx.push(value.matname);
                    RunPHy1.push(Number(value.daqua));
                    RunPHy2.push(Number(value.planqua));

                })
            }
        }, null, false);
    }
    Month_Mat_Product();
    //当班机台产量
    var Team_Equ_Product = function () {
        RunJTx = [];
        RunJTy = [];
        app.DataRequest('Team_Equ_Product', {}, function (data) {
            //获取数据后的操作
            var dt = eval(data);
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunJTx.push(value.EQU);
                    // debugger;
                    RunJTy.push(Number(value.PRODUCT));

                })
            }

        }, null, false);
    }
    Team_Equ_Product();
    ////:加载柱状图
    var LoadChart_ZZ = function (btn_value) {

        var chartoption = {
            chart: {
                renderTo: 'container',
                type: 'column',

                backgroundColor: "transparent",
                animation: false,
                style:
                {
                    fontFamily: 'Microsoft YaHei',
                    fontSize: '12px',
                    color: '#262626'
                },
                marginRight: 20,
                //magrinButtom: 20
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
                categories: RunJTx
            },
            yAxis: {
                min: 0,
                allowDecimals: false,
                title: {
                    text: '当班机台产量（箱）'
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
                    //dataLabels: {
                    //    overflow: "none",
                    //    crop: false,
                    //}
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
                this.x + ': ' + this.y;
                }
            },
            series: [
                {
                    type: 'column',
                    name: '当班机台产量（箱）',
                    data: RunJTy,
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
                            return this.y;
                        }

                    }
                }
            ]

        };

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
                //marginTop: 10,
                marginRight: 20,
                //magrinButtom: 20
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
                categories: RunPHx
            },
            yAxis: {
                min: 0,
                allowDecimals: false,
                title: {
                    text: '产量（箱）'
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
                    //dataLabels: {
                    //    overflow: "none",
                    //    crop: false,
                    //}
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
                this.x + ': ' + this.y + '箱';
                }
            },
            series: [
                {
                    type: 'column',
                    name: '计划量【箱】',
                    yAxis: 0,
                    data: RunPHy2,
                    color: '#FF0000',
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        color: '#BCD2EE',
                        align: 'center',
                        //x: 4,
                        //y: -10,
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif',
                            textShadow: '0 0 4px black'
                        }, formatter: function () {
                            //return this.y + '%';
                            return this.y;
                        }

                    }
                },
                 {
                     type: 'column',
                     name: '打码产量【箱】',
                     yAxis: 0,
                     data: RunPHy1,
                     color: '#0000FF',
                     dataLabels: {
                         enabled: true,
                         rotation: -30,
                         color: '#D2691E',
                         align: 'center',
                         //x: 4,
                         //y: -10,
                         style: {
                             fontSize: '13px',
                             fontFamily: 'Verdana, sans-serif',
                             textShadow: '0 0 4px black'
                         }, formatter: function () {
                             return this.y;
                         }

                     }
                 }
            ]

        };

        $("#MainChart_PH").highcharts(chartoption2);
        $("#Team_Equ_Day_Product").highcharts(chartoption);
    }
    LoadChart_ZZ();

    var myGaugeChart = function (containerId, min, max, step, text, name, data) {
        var a = new Array();
        a.push(data);
        chart = new Highcharts.Chart({
            chart: {
                renderTo: containerId,
                type: "gauge",
                backgroundColor: 'rgba(0,0,0,0)',
                //borderWidth:0,
                plotBorderWidth: 0,
                //plotBackgroundColor: "#000000",
                // ${pageContext.request.contextPath }/js/1.jpg
                plotBackgroundImage: null,
                // width220 height135
                width: 220,
                height: 135
            },
            exporting: {
                // 是否允许导出
                enabled: false
            },
            credits: {
                enabled: false
            },
            title: {
                text: ''
            },
            pane: [{
                startAngle: -90,
                endAngle: 90,
                background: null,
                // 极坐标图或角度测量仪的中心点，像数组[x,y]一样定位。位置可以是以像素为单位的整数或者是绘图区域的百分比
                center: ['50%', '90%'],
                size: 125
            }],
            yAxis: {
                min: min,
                max: max,
                // 步长
                tickInterval: step,
                minorTickPosition: 'outside',
                tickPosition: 'outside',
                labels: {
                    // 刻度值旋转角度
                    rotation: 'auto',
                    distance: 20,
                    style: {
                        color: '#FFFFFF',
                        fontWeight: 'bold'
                    }
                },
                plotBands: [{
                    // 预警红色区域长度
                    // from: 80,
                    // to: 100,
                    // color: '#C02316',
                    // 红色区域查出刻度线
                    innerRadius: '100%',
                    outerRadius: '115%'
                }],
                // 表盘,为0时为半圆，其余都为圆
                pane: 0,
                title: {
                    style: { color: '#FFFFFF', fontSize: '12px' },
                    text: text,
                    y: -5
                }
            },
            plotOptions: {
                gauge: {
                    dataLabels: {
                        enabled: false
                    },
                    dial: {
                        backgroundColor: '#FFD700',
                        // 半径：指针长度
                        radius: '100%'
                    }
                }
            },
            series: [{
                data: a,
                name: name,
                yAxis: 0
            }]

        },
        function (chart) {
            //此函数中可以加上定时效果
        });
    }

    myGaugeChart("Month_Product", 0, 100, 20, "(单位：%)", "当月生产进度", Number(Monthrate));
    myGaugeChart("Day_Product", 0, 100, 20, "(单位：%)", "当班生产进度", Number(Dayrate));



    setInterval(function () {
        $scope.$apply(showTime);
    }, 1000);


    setInterval(function () {
        $scope.$apply(Shift_Team_Prdouct);
        $scope.$apply(Month_Prdouct);
        $scope.$apply(Team_Equ_Product);
        $scope.$apply(LoadChart_ZZ);
        myGaugeChart("Month_Product", 0, 100, 20, "(单位：%)", "当月生产进度", Number(Monthrate));
        myGaugeChart("Day_Product", 0, 100, 20, "(单位：%)", "当班生产进度", Number(Dayrate));

    }, 10000);

    //settime2();
    //clearInterval(settime2);

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


})