var storeapp = angular.module('jbapp', []);/*将一个些通用的低级开发操作包装起来提供给开发者,AngularJS会自动处理好这些低级操作*/
storeapp.controller('jbctl', function ($scope, $http) {
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
        i_t = hours + ":" + minutes+":"+seconds ;
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


    //班次班组，班产量
    var Shift_Team_Prdouct = function () {
        app.DataRequest('Shift_Team_Prdouct', {}, function (data) {
            //获取数据后的操作
            var dt = eval(data);
            if (dt.length > 0) {
                $.each(dt, function (n, value) {
                    $scope.shiftid = "班次:" + value.SHIFTNAME;
                    $scope.teamid = "班组:" + value.TEAMNAME;
                    $scope.teamdayproduct = "当班日产量:" + value.PRODUCT;
                    //$("#shiftid").html("班次:" + value.SHIFTNAME);
                    //$("#teamid").html("班组:" + value.TEAMNAME);
                    //$("#teamdayproduct").html("当班日产量:" + value.PRODUCT);
                })
            }
            else {
                //$("#shiftid").html("班次:未排班");
                //$("#teamid").html("班组:未排班");
                //$("#teamdayproduct").html("当班日产量:未排班");
                $scope.shiftid = "班次:未排班";
                $scope.teamid = "班组:未排班";
                $scope.teamdayproduct = "当班日产量:未排班";
            }

        }, null, false);
    }

    setInterval(function () {       
        $scope.$apply(Shift_Team_Prdouct);
    }, 1000);
    Shift_Team_Prdouct();


    ////班组产量进度
    //var gaugeOptions = {
    //    chart: {
    //        type: 'solidgauge'
    //    },
    //    title: null,
    //    pane: {
    //        center: ['50%', '85%'],
    //        size: '140%',
    //        startAngle: -90,
    //        endAngle: 90,
    //        background: {
    //            backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || '#EEE',
    //            innerRadius: '60%',
    //            outerRadius: '100%',
    //            shape: 'arc'
    //        }
    //    },
    //    tooltip: {
    //        enabled: false
    //    },
    //    // the value axis
    //    yAxis: {
    //        stops: [
    //            [0.1, '#55BF3B'], // green
    //            [0.5, '#DDDF0D'], // yellow
    //            [0.9, '#DF5353'] // red
    //        ],
    //        lineWidth: 0,
    //        minorTickInterval: null,
    //        tickPixelInterval: 400,
    //        tickWidth: 0,
    //        title: {
    //            y: -70
    //        },
    //        labels: {
    //            y: 16
    //        }
    //    },
    //    plotOptions: {
    //        solidgauge: {
    //            dataLabels: {
    //                y: 5,
    //                borderWidth: 0,
    //                useHTML: true
    //            }
    //        }
    //    }
    //};

    //$('#tdp_h').highcharts(gaugeOptions,{
    //    yAxis: {
    //        min: 0,
    //        max: 200,
    //        title: {
    //            text: '产量'
    //        }
    //    },
    //    credits: {
    //        enabled: false
    //    },
    //    series: [{
    //        name: '产量',
    //        data: [80],
    //        dataLabels: {
    //            format: '<div style="text-align:center"><span style="font-size:25px;color:' +
    //            ((Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black') + '">{y}</span><br/>' +
    //            '<span style="font-size:12px;color:silver">km/h</span></div>'
    //        },
    //        tooltip: {
    //            valueSuffix: '箱'
    //        }
    //    }]
    //});

    //setInterval(function () {
       
    //    var chart = $('#tdp_h').highcharts(),
    //        point,
    //        newVal,
    //        inc;
    //    if (chart) {
    //        point = chart.series[0].points[0];
    //        inc = Math.round((Math.random() - 0.5) * 100);
    //        newVal = point.y + inc;
    //        if (newVal < 0 || newVal > 200) {
    //            newVal = point.y - inc;
    //        }
    //        point.update(newVal);
    //    }
    //}, 1000);





    //$scope.QueryData = function () {

    //当前班次班组 从smes中取  当班日产量 从数采中获取
    //Shift_Team_Prdouct();

    ////当月产量   从人工编辑后的数据中卷包工单产耗中获取
    //Month_Product();

   

    ////当班机台日产量
    //Team_Equ_Product();
    ////当月牌号产量
    //Month_Mat_Product();
    //};




    function Month_Product() {
        app.DataRequest('Month_Product', {}, function (data) {
            //获取数据后的操作
            var dt = eval(data);
            if (dt.length > 0) {

            }

        }, null, false);
    }
    function Team_Equ_Product() {
        app.DataRequest('Team_Equ_Product', {}, function (data) {
            //获取数据后的操作
            var dt = eval(data);
            if (dt.length > 0) {

            }

        }, null, false);
    }
    function Month_Mat_Product() {
        app.DataRequest('Month_Mat_Product', {}, function (data) {
            //获取数据后的操作
            var dt = eval(data);
            if (dt.length > 0) {

            }

        }, null, false);
    }
      


})