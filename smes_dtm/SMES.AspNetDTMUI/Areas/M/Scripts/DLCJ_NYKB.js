var storeapp = angular.module('APP', []);/*将一个些通用的低级开发操作包装起来提供给开发者,AngularJS会自动处理好这些低级操作*/


//var myApp = angular.module('myApp', []);     
//myApp.controller('MainCtrl', ['$scope','$compile',function($scope) 
//{      
//    $scope.count = 0;     
//    $scope.add = function() 
//    {       
//        if(event.target.tagName.toLowerCase()=="input")
//            return;    
//        var target=$(event.target);     
//        $scope.count++;        
//        target.html("<input value='"+$scope.count+"' ng-blur='showValue()'>" );  
//    }      
//    $scope.showValue=function(){    
//        alert(event.target.value)    
//    }    
//}])      



storeapp.controller('CTRL', ['$scope', '$compile', function ($scope, $compile,$http) {

   
    var WD_MAX = [];
    var WD_MIN = [];
    var WD_AVG = [];
    var WD_HG = [];
    var SD_MAX = [];
    var SD_MIN = [];
    var SD_AVG = [];
    var SD_HG = [];
    var WS_DATE = [];
    var FristId = "";
    
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
    setInterval(function () {
        $scope.$apply(showTime);
    }, 1000);
    showTime();

    //区域
    var DLCJ_NYJS_1 = function () {
       
        app.DataRequest('DLCJ_NYJS_1', {}, function (data) {
            //获取数据后的操作
            var dt = eval(data);
            var str = "";
            if (dt.length > 0) {
                for (i in dt) {                   
                    str += " <button id=" + dt[i].id + " ng-click='Get_wsd($event)'  class='but' value=" + dt[i].name + ">" + dt[i].name + "</button>";                 
                }             
                $("#tbody-result1").html($compile(str)($scope));             
                FristId = dt[0].id;
                   
            }
          
        }, null, false);
       
    };
    DLCJ_NYJS_1();

    $scope.Get_wsd = function ($event) {
        DLCJ_WS($event.target.id);      
    }
   

    //动力车间温湿度
    var DLCJ_WS = function (ID) {
        app.DataRequest('DLCJ_WS', {ID:ID}, function (data) {
            //获取数据后的操作
            var dt = eval(data);
          
            WD_MAX = [];
            WD_MIN = [];
            WD_AVG = [];
            WD_HG = [];
            SD_MAX = [];
            SD_MIN = [];
            SD_AVG = [];
            SD_HG = [];
            WS_DATE = [];
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    WD_MAX.push(Number(value.wd_max));
                    WD_MIN.push(Number(value.wd_min));
                    WD_AVG.push(Number(value.wd_avg));
                    WD_HG.push(Number(value.wd_pass));
                    SD_MAX.push(Number(value.sd_max));
                    SD_MIN.push(Number(value.sd_min));
                    SD_AVG.push(Number(value.SD_AVG));
                    SD_HG.push(Number(value.SD_PASS));
                    WS_DATE.push(value.time);
                })
                //console.info(WD_MAX);
            }
        }, null, false);
        Load_wsd("WD_MM", "温度", "最大值", WD_MAX, "最小值", WD_MIN);
        Load_wsd("SD_MM", "湿度", "最大值", SD_MAX, "最小值", SD_MIN);
        Load_wsd("WD_HV", "温度", "合格率", WD_HG, "平均值", WD_AVG);
        Load_wsd("SD_HV", "湿度", "合格率", SD_HG, "平均值", SD_AVG);

    }
    DLCJ_WS(FristId);
 
    //加载折线图
    function Load_wsd(id,title,name1,value1,name2,value2) {
       
        $("#"+id).highcharts({
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
            //title: {
            //    text: title,
            //    style: {
            //        color: '#fff',
            //        fontSize: '12px'
            //    }
            //},           
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
                categories: WS_DATE
            },
            yAxis: {
                //min: 0,
                //max: 0.5,    //设置Y轴最大值为“max”
              //  tickInterval: 0.5,   //设置Y轴坐标值的间隔固定为100
                allowDecimals: false,               
                title: {
                    text: title,
                    style:{
                        fontSize: 30,
                        color: '#FFFF00'
                    }                   
                },
                labels: {
                    style: {
                        color: '#FFFF00',                       
                    }
                },
                gridLineColor: '#FFFF00',
                //gridLineColor: '#DEDEDE',
                gridLineDashStyle: 'Dot',//横向网格线样式
            },
            credits: {
                enabled: false
            },
            //plotOptions: {
            //    line: {
            //        pointPadding: 0,
            //        borderWidth: 0,
            //        dataLabels: {
            //            overflow: "none",
            //            crop: false,
            //        }
            //    },
            //    series: {
            //        borderRadius: 2,
            //        connectNulls: true
            //    }
            //},
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
                this.x + ':   ' + title+ this.y;
                }
            },
            series: [
                {
                    type: 'line',
                    name: name1,
                    data: value1,
                    color: '#44BBBB',
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        //color: 'blue',
                        color: '#44BBBB',
                        align: 'center',
                        //y: -10,
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
                    name: name2,
                    data: value2,
                    color: '#52CC33'
                    ,
                    dataLabels: {
                        enabled: true,
                        rotation: -30,
                        //color: 'blue',
                        color: '#52CC33',
                        align: 'center',
                        //y: -10,
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
        });
    }  

    setInterval(function () {
        DLCJ_NYJS_1();
    }, 3600000); //1小时刷新一次   
}])