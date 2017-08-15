var storeapp = angular.module('APP', []);/*将一个些通用的低级开发操作包装起来提供给开发者,AngularJS会自动处理好这些低级操作*/
storeapp.controller('CTRL', function ($scope, $http) {
    //选择时间控件触发的事件
    //$('#ConStart').datepicker({
    //    onSelect: function () {
    //        if ($('#ConStart').val() > $('#ConEnd').val()) {
    //            $('#ConEnd').val($('#ConStart').val());
    //        }
    //        debugger;
    //        $scope.QueryData($scope.SelectType);
    //    }
    //});
    //$('#ConEnd').datepicker({
    //    onSelect: function () {
    //        if ($('#ConStart').val() > $('#ConEnd').val()) {
    //            $('#ConStart').val($('#ConEnd').val());
    //        }
    //        $scope.QueryData($scope.SelectType);
    //    }
    //});
    //初始化时间控件
    //var vDate = new Date();
    //$("#ConStart").val(new Date(vDate.valueOf() - 1 * 24 * 60 * 60 * 1000).Format("yyyy-MM-dd"));
    //$("#ConEnd").val(new Date(vDate.valueOf()).Format("yyyy-MM-dd"));

    //var RunJLx = [];
    //var RunJLy = [];//实际产量
    //var RunJLy1 = [];//打码量
    //var RunXLx = [];
    //var RunXLy = [];
    //var RunXLy1 = [];
    var RunPHx = [];
    var RunPHy = [];
    var RunPHy1 = [];
    var RunPHy2 = [];


    //3：数据查询
    $scope.QueryData = function () {

        ////价类
        //GetJL(btn_value);
        ////系列
        //GetXL(btn_value);
        //牌号
        //GetPH(btn_value);
        GetPH();

        //switch (btn_value) {
        //    case 1:
        $('#QueryTip').html('柱状图');
        // console.info(btn_value);
        LoadChart_ZZ();
        //        break;
        //    case 2:
        //        $('#QueryTip').html('饼图');
        //        console.info(btn_value);
        //        LoadChart_BT(btn_value);
        //        break;
        //    case 3:
        //$('#QueryTip').html('折线图');
        //LoadChart_ZX();
        //        break;
        //    default:
        //        break;
        //}


    }


    //4：数据查询中的方法调用
    //a:价类
    //function GetJL(btn_value) {
    //    RunJLx = [];
    //    RunJLy = [];
    //    RunJLy1 = [];
    //    var startTime = $('#ConStart').val();
    //    var endTime = $('#ConEnd').val();
    //    var con = '';
    //    con += " type='" + btn_value + "' ";
    //    app.DataRequest('GetJL', { strStartTime: startTime, strEndTime: endTime, strCon: con }, function (data) {
    //        var dt = eval(data);
    //        if (dt.length > 0) {
    //            $.each(dt, function (n, value) {
    //                RunJLx.push(value.pricetypeName);
    //                RunJLy.push(Number(value.relqua));
    //                RunJLy1.push(Number(value.daqua));
    //            })
    //        }
    //    }, null, false);
    //}
    ////b:系列
    //function GetXL(btn_value) {
    //    RunXLx = [];
    //    RunXLy = [];
    //    RunXLy1 = [];
    //    var startTime = $('#ConStart').val();
    //    var endTime = $('#ConEnd').val();
    //    var con = '';
    //    con += " type='" + btn_value + "' ";
    //    app.DataRequest('GetXL', { strStartTime: startTime, strEndTime: endTime, strCon: con }, function (data) {
    //        var dt = eval(data);
    //        if (dt.length > 0) {
    //            $.each(dt, function (n, value) {
    //                RunXLx.push(value.pricetypeName);
    //                RunXLy.push(Number(value.relqua));
    //                RunXLy1.push(Number(value.daqua));
    //            })
    //        }
    //    }, null, false);
    //}
    //c:牌号
    function GetPH() {
     
       
        //var startTime = $('#ConStart').val();
        //var endTime = $('#ConEnd').val();
        //var con = '';
        //con += " type='" + btn_value + "' ";
        app.DataRequest('GetPH', {}, function (data) {
            var dt = eval(data);
            //console.info(dt.length);
           // debugger;
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunPHx.push(value.matname);
                    RunPHy.push(Number(value.relqua));
                    RunPHy1.push(Number(value.daqua));
                    RunPHy2.push(Number(value.planqua));
                   
                })
            }
        }, null, false);
    }
    //:加载折线图
    //function LoadChart_ZX(btn_value) {

    //}


    ////:加载饼状图
    //function LoadChart_BT(btn_value) {
    //    var totalMoney = 5000;
    //    var char = {
    //        chart: {
    //            renderTo: 'pie_chart',
    //            plotBackgroundColor: '#625f5f',//背景颜色  
    //           // plotBorderWidth: 0,
    //            plotShadow: false
    //        },
    //        title: {
    //            text: 'Total:$' + totalMoney,
    //            verticalAlign: 'bottom',
    //            y: -60
    //        },
    //        tooltip: {//鼠标移动到每个饼图显示的内容  
    //            pointFormat: '{point.name}: <b>{point.percentage}%</b>',
    //            percentageDecimals: 1,
    //            formatter: function () {
    //                return this.point.name + ':$' + totalMoney * this.point.percentage / 100;
    //            }
    //        },
    //        plotOptions: {
    //            pie: {
    //                size: '60%',
    //                borderWidth: 0,
    //                allowPointSelect: true,
    //                cursor: 'pointer',
    //                dataLabels: {
    //                    enabled: true,
    //                    color: '#000',
    //                    distance: -50,//通过设置这个属性，将每个小饼图的显示名称和每个饼图重叠  
    //                    style: {
    //                        fontSize: '10px',
    //                        lineHeight: '10px'
    //                    },
    //                    formatter: function (index) {
    //                        return '<span style="color:#999;font-weight:bold">' + this.point.name + '</span>';
    //                    }
    //                },
    //                padding: 20
    //            }
    //        },
    //        series: [{//设置每小个饼图的颜色、名称、百分比  
    //            type: 'pie',
    //            name: null,
    //            data: [
    //                { name: 'Base salary', color: '#3DA9FF', y: 65 },
    //                { name: 'Health insurance', color: '#008FE0', y: 20 },
    //                { name: 'Company matched', color: '#00639B', y: 10 },
    //                { name: 'Others', color: '#CBECFF', y: 5 }
    //            ]
    //        }]
    //    };
    //    $("#MainChart_JL").highcharts(char);
    //    $("#MainChart_XL").highcharts(char);
    //    $("#MainChart_PH").highcharts(char);
    //}
    ////:加载柱状图
    function LoadChart_ZZ(btn_value) {
        console.info(RunPHx);
        console.info(RunPHy);
        console.info(RunPHy1);
        console.info(RunPHy2);
        //console.info(RunPHx);
        //console.info(RunPHy);
        //var chartoption = {
        //    chart: {
        //        renderTo: 'container',
        //        type: 'column',
        //        margin: [35, 10, 40, 50],
        //        backgroundColor: "transparent",
        //        //plotBorderWidth: 1,
        //        //plotBorderColor: '#625f5f',
        //        animation: false,
        //        style:
        //        {
        //            fontFamily: 'Microsoft YaHei',
        //            fontSize: '12px',
        //            color: '#262626'
        //        }
        //    },
        //    title: {
        //        text: '',
        //        style: {
        //            color: '#fff',
        //            fontSize: 30
        //        }
        //    },
        //    subtitle: {
        //        text: '',
        //        display: 'none'
        //    },
        //    xAxis: {
        //        title: { text: '' },
        //        labels: {
        //            style: {
        //                color: '#FFFFFF',
        //                fontSize: '13px',
        //                fontFamily: 'Verdana, sans-serif',
        //                textShadow: '0 0 3px black'
        //            }
        //        },
        //        categories: RunJLx
        //    },
        //    yAxis: {
        //        min: 0,
        //        allowDecimals: false,
        //        title: {
        //            text: ''
        //        },
        //        labels: {
        //            style: {
        //                color: '#fff'
        //            }
        //        }
        //        // , tickPositions: RunJLy  //[0, 20, 40, 60, 80, 100] // 指定竖轴坐标点的值
        //    },
        //    credits: {
        //        enabled: false
        //    },

        //    plotOptions: {
        //        column: {
        //            pointPadding: 0,
        //            borderWidth: 0
        //        },
        //        series: {
        //            borderRadius: 2
        //        }
        //    },
        //    legend: {
        //        align: 'center',
        //        verticalAlign: 'top',
        //        x: 0,
        //        y:0,
        //        itemStyle: {
        //            color: '#999',
        //            fontWeight: "normal"
        //        },
        //        itemHoverStyle: {
        //            color: '#44b9dc'
        //        },
        //        enabled: true
        //    },
        //    tooltip: {
        //        backgroundColor: "#333",
        //        borderWidth: 0,
        //        style: {
        //            color: '#FFFFFF',
        //            fontSize: '12px',
        //            padding: '8px'
        //        }, formatter: function () {
        //            return '' +
        //        this.x + ': ' + this.y + '万支';
        //        }
        //    },
        //    series: [
        //        {
        //            type: 'column',
        //            name: '实际产量',
        //            yAxis: 0,
        //            data: RunJLy,
        //            dataLabels: {
        //                enabled: true,
        //                rotation: -30,
        //                color: '#FFFFFF',
        //                align: 'center',
        //                //x: 4,
        //                //y: -10,
        //                style: {
        //                    fontSize: '13px',
        //                    fontFamily: 'Verdana, sans-serif',
        //                    textShadow: '0 0 4px black'
        //                }, formatter: function () {
        //                    //return this.y + '%';
        //                    return this.y + '万支';
        //                }

        //            }
        //        },
        //        {
        //            type: 'column',
        //            name: '打码产量',
        //            yAxis: 0,
        //            data: RunJLy1,
        //            color: '#90ed7d',
        //            dataLabels: {
        //                enabled: true,
        //                rotation: -30,
        //                color: '#625f5f',
        //                align: 'center',
        //                //x: 4,
        //                //y: -10,
        //                style: {
        //                    fontSize: '13px',
        //                    fontFamily: 'Verdana, sans-serif',
        //                    textShadow: '0 0 4px black'
        //                }, formatter: function () {
        //                    //return this.y + '%';
        //                    return this.y + '万支';
        //                }

        //            }
        //        }

        //    ]

        //};

        //var chartoption1 = {
        //    chart: {
        //        renderTo: 'container',
        //        type: 'column',
        //        margin: [35, 10, 40, 50],
        //        backgroundColor: "transparent",
        //        //plotBorderWidth: 1,
        //        //plotBorderColor: '#625f5f',
        //        animation: false,
        //        style:
        //        {
        //            fontFamily: 'Microsoft YaHei',
        //            fontSize: '12px',
        //            color: '#262626'
        //        }
        //    },
        //    title: {
        //        text: '',
        //        style: {
        //            color: '#fff',
        //            fontSize: 30
        //        }
        //    },
        //    subtitle: {
        //        text: '',
        //        display: 'none'
        //    },
        //    xAxis: {
        //        title: { text: '' },
        //        labels: {
        //            style: {
        //                color: '#FFFFFF',
        //                fontSize: '13px',
        //                fontFamily: 'Verdana, sans-serif',
        //                textShadow: '0 0 3px black'
        //            }
        //        },
        //        categories: RunXLx
        //    },
        //    yAxis: {
        //        min: 0,
        //        allowDecimals: false,
        //        title: {
        //            text: ''
        //        },
        //        labels: {
        //            style: {
        //                color: '#fff'
        //            }
        //        }
        //        //, tickPositions: RunXLy  //[0, 20, 40, 60, 80, 100] // 指定竖轴坐标点的值
        //    },
        //    credits: {
        //        enabled: false
        //    },

        //    plotOptions: {
        //        column: {
        //            pointPadding: 0,
        //            borderWidth: 0
        //        },
        //        series: {
        //            borderRadius: 2
        //        }
        //    },
        //    legend: {
        //        align: 'center',
        //        verticalAlign: 'top',
        //        x: 0,
        //        y: 0,
        //        itemStyle: {
        //            color: '#999',
        //            fontWeight: "normal"
        //        },
        //        itemHoverStyle: {
        //            color: '#44b9dc'
        //        },
        //        enabled: true
        //    },
        //    tooltip: {
        //        backgroundColor: "#333",
        //        borderWidth: 0,
        //        style: {
        //            color: '#FFFFFF',
        //            fontSize: '12px',
        //            padding: '8px'
        //        },// pointFormat: '<b>{point.y:.2f}%</b>',
        //        formatter: function () {
        //            return '' +
        //        this.x + ': ' + this.y + '万支';
        //        }
        //    },
        //    series: [
        //        {
        //            name: "实际产量",
        //            data: RunXLy,
        //            dataLabels: {
        //                enabled: true,
        //                rotation: -30,
        //                color: '#FFFFFF',
        //                align: 'center',
        //                //x: 4,
        //                y: -10,
        //                style: {
        //                    fontSize: '13px',
        //                    fontFamily: 'Verdana, sans-serif',
        //                    textShadow: '0 0 4px black'
        //                }, formatter: function () {
        //                    //return this.y + '%';
        //                    return this.y + '万支';
        //                }

        //            }
        //        },
        //          {
        //              type: 'column',
        //              name: '打码产量',
        //              yAxis: 0,
        //              data: RunXLy1,
        //              color: '#90ed7d',
        //              dataLabels: {
        //                  enabled: true,
        //                  rotation: -30,
        //                  color: '#625f5f',
        //                  align: 'center',
        //                  //x: 4,
        //                  //y: -10,
        //                  style: {
        //                      fontSize: '13px',
        //                      fontFamily: 'Verdana, sans-serif',
        //                      textShadow: '0 0 4px black'
        //                  }, formatter: function () {
        //                      //return this.y + '%';
        //                      return this.y + '万支';
        //                  }

        //              }
        //          }
        //    ]

        //};

        var chartoption2 = {
            chart: {
                renderTo: 'container',
                type: 'column',
                margin: [35, 10, 40, 50],
                backgroundColor: "transparent",
                //plotBorderWidth: 1,
                //plotBorderColor:  balck, //'#625f5f',
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
                categories: RunPHx
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
                // , tickPositions: RunPHy  //[0, 20, 40, 60, 80, 100] // 指定竖轴坐标点的值
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
                this.x + ': ' + this.y + '万支';
                }
            },
            series: [
                {
                    type: 'column',
                    name: '实际产量【万支】',
                    data: RunPHy,
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
                            return this.y + '万支';
                        }

                    }
                },
                  {
                      type: 'column',
                      name: '打码产量【万支】',
                      yAxis: 0,
                      data: RunPHy1,
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
                              //return this.y + '%';
                              return this.y + '万支' ;
                          }

                      }
                  },
                {
                    type: 'column',
                    name: '计划量【万支】',
                    yAxis: 0,
                    data: RunPHy2,
                    color: 'blue',
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        color:'green',
                        align: 'center',
                        //x: 4,
                        //y: -10,
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif',
                            textShadow: '0 0 4px black'
                        }, formatter: function () {
                            //return this.y + '%';
                            return this.y + '万支';
                        }

                    }
                }

            ]

        };
        //$("#MainChart_JL").highcharts(chartoption);
        //$("#MainChart_XL").highcharts(chartoption1);
        $("#MainChart_PH").highcharts(chartoption2);
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

    // $scope.QueryData(1);
    $scope.QueryData();
})