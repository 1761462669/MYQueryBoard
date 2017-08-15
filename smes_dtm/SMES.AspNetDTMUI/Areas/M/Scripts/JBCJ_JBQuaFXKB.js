var storeapp = angular.module('APP', []);/*将一个些通用的低级开发操作包装起来提供给开发者,AngularJS会自动处理好这些低级操作*/
storeapp.controller('CTRL', function ($scope, $http) {
    debugger;

    var RunDJ = [];
    var RunResult = [];

    var RunPhx = [];
    var RunPhy = [];
    var RunEqux = [];
    var RunEquy = [];
    var RunTeamx = [];
    var RunTeamJBy = [];
    var RunTeamYBy = [];
    var RunTeamBBy = [];

    var arr = [];

    var Month_Qua_Rate = function () {
        app.DataRequest('Month_Qua_Rate', {}, function (data) {
            //获取数据后的操作
            var dt = eval(data);
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunDJ.push(value.DJ);
                    RunResult.push(Number(value.RESULT));
                })
            }

            var arr1 = RunResult,
                arr2 = RunDJ;
            for (var i = 0, len = arr2.length; i < len; i++) {
                arr.push([arr2[i], arr1[i]]);
            }
            //console.log(JSON.stringify(arr));

        }, null, false);
    }
    Month_Qua_Rate();

    function getData_PH(dj) {
        app.DataRequest('getData_PH', {DJ:dj}, function (data) {
            //获取数据后的操作
            RunPhx.splice(0, RunPhx.length);
            RunPhy.splice(0, RunPhy.length);

            var dt = eval(data);
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunPhx.push(value.MATNAME);
                    RunPhy.push(Number(value.RESULT));
                })
            }

        }, null, false);
    }

    function getData_Equ(dj) {
        app.DataRequest('getData_Equ', { DJ: dj }, function (data) {
            //获取数据后的操作
            RunEqux.splice(0, RunEqux.length);
            RunEquy.splice(0, RunEquy.length);

            var dt = eval(data);
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    

                    RunEqux.push(value.EQUNAME);
                    RunEquy.push(Number(value.BYRESULT));
                })
            }

        }, null, false);
    }

    function getData_Team(dj) {
        app.DataRequest('getData_Team', { DJ: dj }, function (data) {
            //获取数据后的操作
            RunTeamx.splice(0, RunTeamx.length);
            RunTeamJBy.splice(0, RunTeamJBy.length);
            RunTeamYBy.splice(0, RunTeamYBy.length);
            RunTeamBBy.splice(0, RunTeamBBy.length);

            var dt = eval(data);
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    RunTeamx.push(value.plandate);
                    RunTeamJBy.push(Number(value.JB));
                    RunTeamYBy.push(Number(value.YB));
                    RunTeamBBy.push(Number(value.BB));
                })
            }

            var arr1 = RunTeamJBy;
            for (var i = 0; i < arr1.length; i++) {
                if (arr1[i] == 0) {
                    arr1[i] = null;
                }
            }
            RunTeamJBy = arr1;

            var arr2 = RunTeamYBy;
            for (var i = 0; i < arr2.length; i++) {
                if (arr2[i] == 0) {
                    arr2[i] = null;
                }
            }
            RunTeamYBy = arr2;

            var arr3 = RunTeamBBy;
            for (var i = 0; i < arr3.length; i++) {
                if (arr3[i] == 0) {
                    arr3[i] = null;
                }
            }
            RunTeamBBy = arr3;

        }, null, false);
    }

    $(function () {
        $('#MainChart_Month_QuaFX').highcharts({
            chart: {
                type: 'funnel',
                marginLeft: 300,
                marginRight: 300,
                backgroundColor: 'rgba(0,0,0,0)',
            },
            title: {
                text: '卷包质量等级',
                x: -50
            },
            credits: {
                enabled: false
            },
            plotOptions: {
                series: {
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name}</b> ({point.y:,.2f}%)',
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || '#fff',
                        softConnector: true
                    },
                    neckWidth: '25%',
                    neckHeight: '25%',
                    point: {
                        events: {
                            click: function () {
                                //alert(this.name + ' clicked\n');
                                var dj=this.name;

                                getData_PH(dj);
                                getData_Equ(dj);
                                getData_Team(dj);

                                myPHChart("MainChart_Month_PH", dj, RunPhx, RunPhy);
                                myEquChart("MainChart_Month_Equ", dj, RunEqux, RunEquy);
                                myTeamChart("MainChart_Month_Team", dj, RunTeamx, RunTeamJBy, RunTeamYBy, RunTeamBBy);
                                // location.href = this.options.url;
                                //location.href = window.open(e.options.url);
                            }
                        }
                    },
                }
            },
            legend: {
                enabled: false
            },
            series: [{
                data:arr
            }]
        });
    });

    function myPHChart(containerId,Name,Xdata,Ydata) {
        chart = new Highcharts.Chart({
            chart: {
                renderTo: containerId,
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
                marginRight: 20,
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
                categories: Xdata
            },
            yAxis: {
                min: 0,
                max: 125,    //设置Y轴最大值为“max”
                tickInterval: 25,   //设置Y轴坐标值的间隔固定为100
                allowDecimals: false,
                title: {
                    text:Name+ '(%)',
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
                    name: '当月牌号' + Name,
                    data: Ydata,
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
        })
    }

    function myEquChart(containerId,Name, Xdata, Ydata) {
        chart = new Highcharts.Chart({
            chart: {
                renderTo: containerId,
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
                marginRight: 20,
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
                categories: Xdata
            },
            yAxis: {
                min: 0,
                allowDecimals: false,
                title: {
                    text: Name+'（%）'
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
                    name: '当月机台'+Name,
                    data: Ydata,
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
        })
    }

    function myTeamChart(containerId, Name, Xdata, Ydata1,Ydata2,Ydata3) {
        chart = new Highcharts.Chart({
            chart: {
                renderTo: containerId,
                type: 'line',
                //margin: [35, 10, 40, 50],
                backgroundColor: "transparent",
                animation: false,
                style:
                {
                    fontFamily: 'Microsoft YaHei',
                    fontSize: '12px',
                    color: '#262626'
                },
                marginRight: 20,
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
                categories: Xdata
            },
            yAxis: {
                min: 0,
                allowDecimals: false,
                title: {
                    text: Name + '（%）'
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
                    type: 'line',
                    name: '甲班',
                    data: Ydata1,
                    //data:[67.8,78.5,null,34.5,56.7,45.9],
                    color: '#FF0000',
                    
                },
                  {
                      type: 'line',
                      name: '乙班',
                      //yAxis: 0,
                      data: Ydata2,
                      color: '#FFFF00',
                     
                  },
                {
                    type: 'line',
                    name: '丙班',
                    //yAxis: 0,
                    data: Ydata3,
                    color: '#0000FF',
                }

            ]
            //series: [
            //    {
            //        type: 'line',
            //        name: '当月班组' + Name + '对比图',
            //        data: Ydata,
            //        dataLabels: {
            //            enabled: true,
            //            rotation: -30,
            //            color: '#FFFFFF',
            //            align: 'center',
            //            //x: 4,
            //            y: -10,
            //            style: {
            //                fontSize: '13px',
            //                fontFamily: 'Verdana, sans-serif',
            //                textShadow: '0 0 4px black'
            //            }, formatter: function () {
            //                return this.y;
            //            }

            //        }
            //    }
            //]
        })
    }

    getData_PH('精品率');
    getData_Equ('精品率');
    getData_Team('精品率');

    myPHChart("MainChart_Month_PH", '精品率', RunPhx, RunPhy);
    myEquChart("MainChart_Month_Equ", '精品率', RunEqux, RunEquy);
    myTeamChart("MainChart_Month_Team", '精品率', RunTeamx, RunTeamJBy, RunTeamYBy, RunTeamBBy);
})