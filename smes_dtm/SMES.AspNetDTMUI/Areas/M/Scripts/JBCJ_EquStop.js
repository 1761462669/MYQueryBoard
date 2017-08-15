var storeapp = angular.module('APP', []);
storeapp.controller('CTRL', function ($scope, $http) {

    var Yearx = [];
    var Yeary = [];
    var Yearyy = [];

    var Monthx = [];
    var Monthy = [];
    var Monthyy = [];

   
    var MonthJTx = [];   
    var MonthJTy = [];
    var MonthJT = [];

    var DayJTx = [];  
    var DayJTy = [];
    var DayJT = [];
  
    var date_ymd = getNowFormatDate().substr(0,10);//yyyy-MM-dd 的格式
    var date_ym = date_ymd.substr(0, 7);//yyyy-MM 的格式
    var JT_M = "M";  //默认显示M型号的机台    P   M     月
    var JT_D= "M"; //日
    //年度趋势图
    var JBCJ_Year = function () {
         Yearx = [];
         Yeary = [];
         Yearyy = [];
         app.DataRequest('JBCJ_Year', {}, function (data) {
            var dt = eval(data);
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    Yearx.push(value.YM);
                    Yeary.push(Number(value.STOPCS));
                    Yearyy.push(Number(value.STOPTIME));
                })
            }
        }, null, false);
    }
    JBCJ_Year();
    //月度停机趋势图   
    var JBCJ_Month = function (date) {
        Monthx = [];
        Monthy = [];
        Monthyy = [];
        app.DataRequest('JBCJ_Month', { DATE: date }, function (data) {
            var dt = eval(data);
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    Monthx.push(value.PRODATE);
                    Monthy.push(Number(value.STOPCS));
                    Monthyy.push(Number(value.STOPTIME));
                    
                })
            }
        }, null, false);
    }
    JBCJ_Month(date_ymd);

    //月度机台停机统计图
    var JBCJ_JT_Month = function (jt,ym) {
        MonthJTx = [];
        MonthJTy = [];
        MonthJT = [];
        app.DataRequest('JBCJ_JT_Month', { DATE: ym,CODE:jt }, function (data) {
            var dt = eval(data);
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    MonthJTx.push(value.MACH_ID);
                    MonthJTy.push(Number(value.STOPTIME));

                })
            }
            var arr1 = MonthJTx,
               arr2 = MonthJTy;
            for (var i = 0, len = arr2.length; i < len; i++) {
                MonthJT.push([arr1[i], arr2[i]]);
            }
        }, null, false);
    }
    JBCJ_JT_Month(JT_M, date_ym);

    //日机台停机统计图
    var JBCJ_JT_Day = function (jt, ymd) {
        DayJTx = [];
        DayJTy = [];
        DayJT = [];
        app.DataRequest('JBCJ_JT_Day', { DATE: ymd, CODE: jt }, function (data) {
            var dt = eval(data);
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    DayJTx.push(value.MACH_ID);
                    DayJTy.push(Number(value.STOPTIME));
                })
            }
            var arr1 = DayJTx,
                arr2 = DayJTy;
            for (var i = 0, len = arr2.length; i < len; i++) {
                DayJT.push([arr1[i], arr2[i]]);
            }
        }, null, false);
    }
    JBCJ_JT_Day(JT_D, date_ymd);



    //卷包车间机台月统计图   卷接机
    $scope.MONTH_JJ1 = function () {
        JBCJ_JT_Month("M", date_ym);
        JT_M = "M";
        $scope.Month_m = "卷接机";
        PieChart("JBCJ_JT_Month", date_ym, MonthJT);
    }
    //卷包车间机台月统计图   包装机
    $scope.MONTH_BZ1 = function () {
        JBCJ_JT_Month("P", date_ym);
        JT_M = "P";
        $scope.Month_m = "包装机";
        PieChart("JBCJ_JT_Month", date_ym, MonthJT);
    }
    //卷包机台日统计图  卷接机
    $scope.DAY_JJ1 = function () {
        JBCJ_JT_Day("M", date_ymd);
        JT_D = "M";
        $scope.Day_m = "卷接机";
        PieChart("JBCJ_JT_DAY", date_ymd, DayJT);
    }
    //卷包机台日统计图  包装机
    $scope.DAY_BZ1 = function () {
        JBCJ_JT_Day("P", date_ymd);
        JT_D = "P";
        $scope.Day_m = "包装机";
        PieChart("JBCJ_JT_DAY", date_ymd, DayJT);
    }

    //柱状图、折线图
    function LoadChart_Rate_Year(id,title1,value1,title2,value2,valuex) {
        $("#" + id).highcharts({     
       
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
                categories: valuex
            },
            yAxis: [{

                title: {
                    text: title1+"(次)",
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
                    text: title2+"(时)",
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
                    },
                    point: {
                        events: {
                            click: function (e) {
                                var data = e.point.category; //此处data为 YYYY-mm 格式 需转为YYYY-mm-dd 格式
                                var data_day = data + "-01";
                                date_ym = data;
                                //date_ymd = data_day;
                                JBCJ_Month(data_day);
                                //加载月度停机趋势图
                                LoadChart_Rate_Day("JBCJ_Month", "停机次数", Monthy, "停机时间", Monthyy, Monthx);
                                //加载月度机台停机统计图
                                JBCJ_JT_Month(JT_M, date_ym);
                              
                                PieChart("JBCJ_JT_Month", date_ym, MonthJT);
                              
                              
                            }
                        }
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
                           + "<span style='color:#4572A7'>停机次数：" + this.points[0].y + "</span><br>"
                           + "<span style='color:#89A54E'>停机时间：" + this.points[1].y + " </span>"
                    ;
                }
            },
            series: [

                {
                    name: title1,
                    color: '#89A54E',
                    type: 'column',
                    yAxis: 0,
                    data: value1,
                    tooltip: {
                        formatter: function () {
                            return this.y;
                        }
                    },
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        color: '#89A54E',
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
                    name: title2,
                    color: '#4572A7',
                    type: 'line',
                    yAxis: 1,
                    data: value2,
                    tooltip: {
                        formatter: function () {
                            return this.y;
                        }
                    },
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        color: '#4572A7',
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
                    //,
                    //tooltip: {
                    //    valueSuffix: ' %'
                    //}
                }
            ]       
    })
    }
    function LoadChart_Rate_Day(id, title1, value1, title2, value2, valuex) {
        $("#" + id).highcharts({

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
                categories: valuex
            },
            yAxis: [{

                title: {
                    text: title1 + "(次)",
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
                    text: title2 + "(时)",
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
                    },
                    point: {
                        events: {
                            click: function (e) {
                                var data = e.point.category; //此处data为 YYYY-mm-dd 格式   
                                date_ymd = data;
                                //加载日机台停机统计图
                                JBCJ_JT_Day(JT_D, date_ymd);
                                $scope.Day_m = "卷接机";
                                PieChart("JBCJ_JT_DAY", date_ymd, DayJT);
                               
                            }
                        }
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
                           + "<span style='color:#4572A7'>停机次数：" + this.points[0].y + "</span><br>"
                           + "<span style='color:#89A54E'>停机时间：" + this.points[1].y + " </span>"
                    ;
                }
            },
            series: [

                {
                    name: title1,
                    color: '#89A54E',
                    type: 'column',
                    yAxis: 0,
                    data: value1,
                    tooltip: {
                        formatter: function () {
                            return this.y;
                        }
                    },
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        color: '#89A54E',
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
                    name: title2,
                    color: '#4572A7',
                    type: 'line',
                    yAxis: 1,
                    data: value2,
                    tooltip: {
                        formatter: function () {
                            return this.y;
                        }
                    },
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        color: '#4572A7',
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
                    //,
                    //tooltip: {
                    //    valueSuffix: ' %'
                    //}
                }
            ]
        })
    }

    LoadChart_Rate_Year("JBCJ_Year", "停机次数", Yeary, "停机时间", Yearyy, Yearx);
    LoadChart_Rate_Day("JBCJ_Month", "停机次数", Monthy, "停机时间", Monthyy, Monthx);

    //饼状图
    function PieChart(containerId, Date, Arr) {
        chart = new Highcharts.Chart({
            chart: {
                renderTo: containerId,
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false,
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
            colors: [
                       '#FF3300',//第一个颜色，欢迎加入Highcharts学习交流群294191384
                       '#AA6655',//第二个颜色
                       '#808080',//第三个颜色
                       '#66FF00',
                       '#00FFCC',
                       '#44A3BB',
                       '#8F6699',
                       '#CC00FF',
                       '#FF0099',
                       '#C43C57'
            ],
            title: {
                text: '时间：' + Date,
                style:
                {
                    fontFamily: 'Microsoft YaHei',
                    fontSize: '12px',
                    color: '#fff'
                },
            },
            tooltip: {
                headerFormat: '{series.name}<br>',
                pointFormat: '{point.name}: <b>{point.percentage:.2f}</b>'
            },
            legend: {
                align: 'center',
                verticalAlign: 'bottom',
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
            credits: {
                enabled: false
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        //format: '<b>{point.name}</b>: {point.percentage:.2f} % ',
                        formatter: function () {
                            return '<b>' + this.point.name + '</b>: ' + Highcharts.numberFormat(this.percentage, 2)+'%' + ' (停机时间：' +
                                Highcharts.numberFormat(this.y, 2, ',') + ' 时)';
                        },
                        style: {
                            color: '#fff'
                        }
                    },
                    showInLegend: true
                }
            },
            series: [{
                type: 'pie',
                //name: Name,
                data: Arr
            }]
        })
    }
    $scope.Day_m = "卷接机";
    $scope.Month_m = "卷接机";
    PieChart("JBCJ_JT_Month", date_ym,  MonthJT);
    PieChart("JBCJ_JT_DAY", date_ymd, DayJT);
    

})