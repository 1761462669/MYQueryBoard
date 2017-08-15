
var storeapp = angular.module('APP', []);/*将一个些通用的低级开发操作包装起来提供给开发者,AngularJS会自动处理好这些低级操作*/

storeapp.controller('CTRL', function ($scope, $http) {
    //setInterval(function () {
    //    $scope.$apply(Month_Prdouct);       
    //}, 1000);

    var Finished;
    var RunPHx = [];
    var RunPHy = [];

    var RunPHy2 = [];

    var RunQXx = [];
    var RunQXy = [];
    var RunQXrate = [];


    var chart;


    //3：数据查询
    $scope.QueryData = function () {
        showTime();
        Month_Prdouct();
        Year_Product();
         Month_Qua_QX();
        $('#QueryTip').html('柱状图');
        LoadChart_ZZ();
        myGaugeChart("Month_Rate", 0, 100, 20, "(单位：%)", "当月已完成", Number(Finished));
       

    }

    function Year_Product() {
        app.DataRequest('Year_Product', {}, function (data) {
            var dt = eval(data);
            RunPHx = [];
            RunPHy = [];

            RunPHy2 = [];
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunPHx.push(value.plandate);
                    RunPHy.push(Number(value.planqua));
                    RunPHy2.push(Number(value.daqua));
                })
            }
        }, null, false);
    }

    // 卷包质量缺陷
    function Month_Qua_QX() {
        app.DataRequest('Month_Qua_QX', {}, function (data) {
            var dt = eval(data);
            RunQXx = [];
            RunQXy = [];
            RunQXrate = [];
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunQXx.push(value.PROJECTNAME);
                    RunQXy.push(Number(value.QX));
                    RunQXrate.push(Number(value.rate));
                })
            }
        }, null, false);
    }



    ////:加载柱状图
    function LoadChart_ZZ(btn_value) {

        var chartoption2 = {
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
                // , tickPositions: RunPHy  //[0, 20, 40, 60, 80, 100] // 指定竖轴坐标点的值
            },
            credits: {
                enabled: false
            },

            plotOptions: {
                column: {
                    dataLabels:{enabled:true},
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
                this.x + ': ' + this.y + '箱';
                }
            },
            series: [
               {
                   type: 'column',
                   name: '计划量【箱】',
                   data: RunPHy,
                   color: '#FF0000'
               },
               {
                   type: 'column',
                   name: '打码量【箱】',
                   yAxis: 0,
                   data: RunPHy2,
                   color: '#0000FF'
               }

            ]

        };

        var chartoption = {
            chart: {
                renderTo: 'container',
                backgroundColor: "transparent",
                animation: false,
                style:
                {
                    fontFamily: 'Microsoft YaHei',
                    fontSize: '12px',
                    color: '#262626'
                },
                //zoomType: 'xy' ,//支持图表放大缩小的范围 
                marginRight: 80 // like left

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
                        textShadow: '0 0 3px black',
                        //rotation: 0,    
                        //writingMode: 'tb-rl'    //文字竖排样式
                    }
                },
                categories: RunQXx
            },
            yAxis: [{

                title: {
                    text: '缺陷数',
                    style: {
                        color: '#89A54E'
                    }
                },
                labels: {
                    //format: '{value}',//格式化Y轴刻度  
                    style: {
                        color: '#89A54E'
                    }
                },
                gridLineColor: '#DEDEDE',
                gridLineDashStyle: 'Dot',//横向网格线样式
                // max: 100
            }, { // Secondary yAxis  
                title: {
                    text: '占比(%)',
                    style: {
                        color: '#4572A7'
                    }
                },
                labels: {
                    //format: '{value}',
                    style: {
                        color: '#4572A7'
                    },

                },
                gridLineColor: '#DEDEDE',
                gridLineDashStyle: 'Dot',//横向网格线样式
                max: 100,
                opposite: true
            }],
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
                shared: true, //公用一个提示框
                backgroundColor: "#333",
                borderWidth: 0,
                style: {
                    color: '#FFFFFF',
                    fontSize: '12px',
                    padding: '8px'
                }, formatter: function () {
                    return this.x + "<br>"
                           + "<span style='color:#4572A7'>缺陷数：" + this.points[0].y + "</span><br>"
                           + "<span style='color:#89A54E'>占比：" + this.points[1].y + " %</span>"
                    ;
                }
            },
            series: [

                {
                    name: '缺陷数',
                    color: '#89A54E',
                    type: 'column',
                    yAxis: 0,
                    data: RunQXy,
                    tooltip: {
                        formatter: function () {
                            return this.y;
                        }
                    },
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

                }, {
                    name: '占比',
                    color: '#4572A7',
                    type: 'line',
                    yAxis: 1,
                    data: RunQXrate,
                    tooltip: {
                        valueSuffix: ' %'
                    }
                }
            ]

        };
        $("#MainChart_Year_Product").highcharts(chartoption2);
        $("#MainChart_Month_Qua_QX").highcharts(chartoption);


        setInterval(function () {
            $scope.$apply(Year_Product);
            $scope.$apply(Month_Qua_QX);
        }, 10000);
    }

    function myGaugeChart(containerId, min, max, step, text, name, data) {
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

    function Month_Prdouct() {
        app.DataRequest('Month_Prdouct', {}, function (data) {
            //获取数据后的操作
            var dt = eval(data);
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    Finished = value.monthrate;
                    if (Finished > 100) {
                        Finished = 100;
                    }

                    $scope.monthproduct = value.monthproduct;
                    $scope.planproduct = "月计划产量(箱)：" + value.planproduct;
                    $scope.monthproductrate = value.monthrate + '%';
                    $scope.noproduct = "月剩余产量(箱)：" + value.noproduct;
                })
            }
            else {
                $scope.monthproduct = "当月产量:未生产";
            }

        }, null, false);
    }

    function showTime() {
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


    setInterval(function () {
        showTime();
        Month_Prdouct();
        Year_Product();
        Month_Qua_QX();
        LoadChart_ZZ();
        myGaugeChart("Month_Rate", 0, 100, 20, "(单位：%)", "当月已完成", Number(Finished));
    }, 60000)


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