$(function () {
})

var SingleAnalysisapp = angular.module('singleanalysisapp', []);
SingleAnalysisapp.controller('singleanalysisctl', function ($scope, $http) {

        $scope.ProcessList;
        $scope.RankList;
        $scope.ItemList;
        $scope.SelectPro;
        $scope.SelectRank;
        $('#QueryMonth').datepicker({
            onSelect: function () {
                $("#QueryMonth").val(new Date($("#QueryMonth").val()).Format("yyyy-MM"));
            }
        });
        $("#QueryMonth").val(new Date().Format("yyyy-MM"));
        //获取参数组
        app.DataRequest('GetProcessSegmentList', { code: "GetProductSegmentList" }, function (data) {
            if (data.length > 0) {
                $scope.ProcessList = data;
            }
            else { $scope.ProcessList = null; }
        }, null, false);
        
        //获取参数等级
        app.DataRequest('GetDefectRankingList', { code: "GetDefectLevelList" }, function (DefectRanking) {
            if (DefectRanking.length > 0) {
                $scope.RankList = DefectRanking;
            }
            else { $scope.RankList = null; }
        }, null, false);


        $scope.QueryItems1=function(x)
        {
            if ($scope.x != "" && $scope.y != "") {
                GetDefectItemList();
            }
        }

        $scope.QueryItems2 = function (y) {
            if ($scope.x!="" && $scope.y!="") {
                GetDefectItemList();
            }    
        }
        //获取缺陷项
        function GetDefectItemList() {
            app.DataRequest('GetDefectItemList', { code: "GetDefectItemList",ProId:$scope.x.ID, RankId:$scope.y.ID}, function (DefectItem) {
                if (DefectItem.length > 0) {
                    $scope.ItemList = DefectItem;
                }
                else { $scope.ItemList = null;}
            }, null, false);

            $scope.QueryData = function () {

                var month = new Date($("#QueryMonth").val()).Format("yyyy-MM");
                var equList=new Array();
                var Jdefect;
                var Ydefect;
                var Bdefect;

                app.DataRequest('GetEquMonthsDefectRate', { code: "GetEquMonthsDefectRate", strMonth: month, productseg: $scope.x.ID, parasid: $scope.z.ID, team: '甲班' }, function (Jdata) {

                    if (Jdata.length > 0) {
                        //equList = new Array(Jdata.length);
                        Jdefect = new Array(Jdata.length);
                        for (var i = 0; i < Jdata.length; i++) {
                            equList.push(Jdata[i]["EQUPMENT"]);
                            Jdefect[i] = parseFloat(Jdata[i]["BUGVALUE"]);

                        }

                }
                }, null, false);

                app.DataRequest('GetEquMonthsDefectRate', { code: "GetEquMonthsDefectRate", strMonth: month, productseg: $scope.x.ID, parasid: $scope.z.ID, team: '乙班' }, function (Ydata) {

                    if (Ydata.length > 0) {
                        Ydefect = new Array(Ydata.length);
                        for (var i = 0; i < Ydata.length; i++) {

                            Ydefect[i] = parseFloat(Ydata[i]["BUGVALUE"]);
                            if ($.inArray(Ydata[i]["EQUPMENT"], equList) < 0)
                                equList.push(Ydata[i]["EQUPMENT"]);
                        }

                    }
                }, null, false);

                app.DataRequest('GetEquMonthsDefectRate', { code: "GetEquMonthsDefectRate", strMonth: month, productseg: $scope.x.ID, parasid: $scope.z.ID, team: '丙班' }, function (Bdata) {

                    if (Bdata.length > 0) {
                        Bdefect = new Array(Bdata.length);
                        for (var i = 0; i < Bdata.length; i++) {

                            Bdefect[i] = parseFloat(Bdata[i]["BUGVALUE"]);
                            if ($.inArray(Bdata[i]["EQUPMENT"], equList) < 0)
                                equList.push(Bdata[i]["EQUPMENT"]);

                        }

                    }
                }, null, false);


                Setbarchart1(equList,Jdefect,Ydefect,Bdefect);
            }

        }
      //GetBaseDataist();   
      //Setbarchart3();
      

});

function Setbarchart1(equ,jdata,ydate,bdate) {

    var chartoption = {
        chart: {
            type: 'column',
            backgroundColor: "transparent",
            margin: [50, 80, 100, 60]
        },
        title: {  //图表标题 
            text: '卷包缺陷单项分析图',
            style: {
                color: '#C4C4C4',
                fontSize: '30px',
                fontFamily: 'Verdana, sans-serif',
                textShadow: '0 0 3px black'
            }
        },
        xAxis: { //x轴 
            categories:equ ,  //X轴类别
            labels: {
                y: 18,
                rotation: -30,//倾斜度  
                style: {
                    color: '#FFFFFF',
                    fontSize: '13px',
                    fontFamily: 'Verdana, sans-serif',
                    textShadow: '0 0 3px black'
                }
            }                         //x轴标签位置：距X轴下方18像素 
        },
        yAxis: {  //y轴 
            title: {
                text: '缺陷数量（个）',
                style: {
                    color: '#fff',
                    fontSize: '18px'
                }
            }, //y轴标题 
            lineWidth: 2, //基线宽度 
            allowDecimals: false,
            labels: {
                        style: {
                        color: '#fff'
                    }
                }
        },
        tooltip: {
            formatter: function () { //格式化鼠标滑向图表数据点时显示的提示框 
                var s;
                    s = '' + this.x + ': ' + this.y + '个';
                return s;
            }
        },
        exporting: {
            enabled: true  //设置导出按钮不可用 
        },
        credits: {
            enabled: false
        },
        legend: {
                    itemStyle: {
                    color: '#fff',
                    fontWeight: "normal"
                },
                itemHoverStyle: {
                        color: '#44b9dc'
                }
                },
        series: [{ //数据列 
            type: 'column',
            name: '甲班',
            data:jdata,
            dataLabels: {
                enabled: true,
                //rotation: 0,
                color: '#FFFFFF',
                align: 'center',
                verticalAlign: 'top',
                x: 0,
                y: -24,
                style: {
                    fontSize: '13px',
                    fontFamily: 'Verdana, sans-serif',
                    textShadow: '0 0 3px black'
                }
            }
        },
        {
            type: 'column',
            name: '乙班',
            data: ydate,
            dataLabels: {
                enabled: true,
                //rotation: 0,
                color: '#FFFFFF',
                align: 'center',
                verticalAlign: 'top',
                x: 0,
                y: -24,
                style: {
                    fontSize: '13px',
                    fontFamily: 'Verdana, sans-serif',
                    textShadow: '0 0 3px black'
                }
            }
        },
        {
            type: 'column',
            name: '丙班',
            data: bdate,
            dataLabels: {
                enabled: true,
                //rotation: 0,
                color: '#FFFFFF',
                align: 'center',
                verticalAlign: 'top',
                x: 0,
                y: -24,
                style: {
                    fontSize: '13px',
                    fontFamily: 'Verdana, sans-serif',
                    textShadow: '0 0 3px black'
                }
            }
        }]
    };

    $("#SingleAnalysisChart1").highcharts(chartoption);

}

function Setbarchart3() {

    var chartoption = {
        chart: {
            type: 'column',
            backgroundColor: "transparent",
            margin: [0, 0, 20, 0]
        },
        colors: ['#f76532', '#0f98e8', '#ffb119', '#910000'],
        title: {
            text: '当月各班产量统计',
            style: {
                color: '#999'
            }
        },
        subtitle: {
            text: ''
        },
        xAxis: {
            title: { text: '班组' },
            tickColor: "#4c4d51",
            lineColor: "#4c4d51",
            labels: {
                style: {
                    color: '#999'
                }
            },
            categories: ['甲班', '乙班', '丙班']
        },
        yAxis: {
            min: 0,
            allowDecimals: false,
            title: {
                text: '产量'
            },
            labels: {
                style: {
                    color: '#999'
                }
            },
            gridLineColor: '#4c4d51'
        },
        credits: {
            enabled: false
        },

        plotOptions: {
            column: {
                pointPadding: 0,
                borderWidth: 0,
                pointWidth: 10,
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
                color: '#999',
                fontSize: '12px',
                padding: '8px'
            },
        },
        series: [
            {
                name: "实际产量",
                data: [12, 22, 14]
            },
            {
                name: "计划产量",
                data: [30, 30, 30]
            }

        ]

    };

    $("#SingleAnalysisChart3").highcharts(chartoption);
}

function twoDecimal(x) {
    var f_x = parseFloat(x);
    if (isNaN(f_x)) {
        alert('function:changeTwoDecimal->parameter error');
        return false;
    }
    f_x = Math.round(f_x * 100) / 100;
    var s_x = f_x.toString();
    var pos_decimal = s_x.indexOf('.');
    if (pos_decimal < 0) {
        pos_decimal = s_x.length;
        s_x += '.';
    }
    while (s_x.length <= pos_decimal + 2) {
        s_x += '0';
    }
    return s_x;
}
