var storeapp = angular.module('APP', []);/*将一个些通用的低级开发操作包装起来提供给开发者,AngularJS会自动处理好这些低级操作*/
storeapp.controller('CTRL', function ($scope, $http) {
    var RunJPx = [];
    var RunBYJPy = [];
    var RunSYJPy = [];

    var RunTJPx = [];
    var RunTJPJBy = [];
    var RunTJPYBy = [];
    var RunTJPBBy = [];

    //3：数据查询
    $scope.QueryData = function () {
        //GetPH();
        GetTeamScore();
        GetMachineScore();
        LoadChart_Machine();
        LoadChart_Team();
    }
    //机台得分
    function GetMachineScore() {
        app.DataRequest('GetMachineScore', {}, function (data) {
            var dt = eval(data);

            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunJPx.push(value.EQUNAME);
                    RunBYJPy.push(Number(value.BYRESULT));
                    RunSYJPy.push(Number(value.SYRESULT));
                    //RunPHy1.push(Number(value.daqua));
                    //RunPHy2.push(Number(value.planqua));

                })
            }
        }, null, false);
    }

    //班组得分
    function GetTeamScore() {
        app.DataRequest('GetTeamScore', {}, function (data) {
            var dt = eval(data);
            RunTJPJBy = [];
            RunTJPYBy = [];
            RunTJPBBy = [];
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunTJPx.push(value.PLANDATE);
                    RunTJPJBy.push(Number(value.JB));
                    RunTJPYBy.push(Number(value.YB));
                    RunTJPBBy.push(Number(value.BB));
                })
            }

        }, null, false);
    }
    //卷包机台得分
    ////:加载柱状图
    function LoadChart_Team(btn_value) {
        //debugger;
        var chartoption2 = {
            chart: {
                //renderTo: 'container',
                type: 'column',
                //margin: [35, 10, 40, 50],
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
                categories: RunTJPx
            },
            yAxis: {
                min: 0,
              
                allowDecimals: false,
                title: {
                    text: '班组得分'
                },
                labels: {
                    style: {
                        color: '#fff'
                    }
                },
                gridcolumnColor: '#DEDEDE',
                gridcolumnDashStyle: 'Dot',//横向网格线样式
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
                    name: '甲班',
                    data: RunTJPJBy,
                    //data:[67.8,78.5,null,34.5,56.7,45.9],
                    color: '#FF0000',
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        color: '#FF0000',
                        align: 'center',
                        //x: 4,
                        y: -10,
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif',
                            textShadow: '0 0 4px black'
                        }, formatter: function () {
                            //return this.y + '%';
                            return this.y ;
                        }

                    }
                },
                  {
                      type: 'column',
                      name: '乙班',
                      //yAxis: 0,
                      data: RunTJPYBy,
                      color: '#FFFF00',
                      dataLabels: {
                          enabled: true,
                          rotation: -30,
                          color: '#FFFF00',
                          align: 'center',
                          //x: 4,
                          //y: -10,
                          style: {
                              fontSize: '13px',
                              fontFamily: 'Verdana, sans-serif',
                              textShadow: '0 0 4px black'
                          }, formatter: function () {
                              //return this.y + '%';
                              return this.y ;
                          }

                      }
                  },
                {
                    type: 'column',
                    name: '丙班',
                    //yAxis: 0,
                    data: RunTJPBBy,
                    color: '#0000FF',
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        color: '#0000FF',
                        align: 'center',
                        //x: 4,
                        //y: -10,
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif',
                            textShadow: '0 0 4px black'
                        }, formatter: function () {
                            //return this.y + '%';
                            return this.y ;
                        }

                    }
                }

            ]

        };
        $("#MainChart_Month_Team").highcharts(chartoption2);
    }

    function LoadChart_Machine(btn_value) {
        //debugger;
        var chartoption2 = {
            chart: {
                renderTo: 'container',
                type: 'column',
                //margin: [35, 10, 40, 50],
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
                categories: RunJPx
            },
            yAxis: {
                min: 97,
                max: 100,    //设置Y轴最大值为“max”
                tickInterval: 0.5,   //设置Y轴坐标值的间隔固定为100
                allowDecimals: false,
                title: {
                    text: '机台得分'
                },
                labels: {
                    style: {
                        color: '#fff'
                    }
                },
                gridcolumnColor: '#DEDEDE',
                gridcolumnDashStyle: 'Dot',//横向网格线样式
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
                    name: '本月得分',
                    data: RunBYJPy,
                    color: '#FF0000',
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        color: '#FF0000',
                        align: 'center',
                        //x: 4,
                        y: -10,
                        style: {
                            fontSize: '13px',
                            fontFamily: 'Verdana, sans-serif',
                            textShadow: '0 0 4px black'
                        }, formatter: function () {
                            //return this.y + '%';
                            return this.y ;
                        }

                    }
                },
                  {
                      type: 'column',
                      name: '上月得分',
                      yAxis: 0,
                      data: RunSYJPy,
                      color: '#FFFF00',
                      dataLabels: {
                          enabled: true,
                          rotation: -30,
                          color: '#FFFF00',
                          align: 'center',
                          //x: 4,
                          //y: -10,
                          style: {
                              fontSize: '13px',
                              fontFamily: 'Verdana, sans-serif',
                              textShadow: '0 0 4px black'
                          }, formatter: function () {
                              //return this.y + '%';
                              return this.y ;
                          }

                      }
                  }
            ]

        };
        $("#MainChart_Month_Machine").highcharts(chartoption2);
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

    $scope.QueryData();
})