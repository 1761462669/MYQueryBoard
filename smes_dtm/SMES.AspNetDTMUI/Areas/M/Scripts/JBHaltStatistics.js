$(function () {
    resize();
    $(window).on("resize", resize);
})

var HaltApp = angular.module('JbHaltStatisApp', []);
HaltApp.controller('JbHaltStatisCtl', function ($scope, $http) {
    $("#StartDate").val(new Date().Format("yyyy-MM-dd"));
    $("#EndDate").val(new Date().Format("yyyy-MM-dd"));
    $scope.ResultList1;
    $scope.ResultList2;
    $scope.ResultList3;
    $scope.ResultList4;
    $scope.ResultList5;
    $scope.ResultList6;

    $scope.Cnt1 = 0;
    $scope.Cnt2 = 0;
    $scope.Cnt3 = 0;
    $scope.Cnt4 = 0;
    $scope.Cnt5 = 0;
    $scope.Cnt6 = 0;

    $scope.Duration1 = 0;
    $scope.Duration2 = 0;
    $scope.Duration3 = 0;
    $scope.Duration4 = 0;
    $scope.Duration5 = 0;
    $scope.Duration6 = 0;

    $scope.RefreshData = function () {
        var startdate = $("#StartDate").val();
        var enddate = $("#EndDate").val();

        try {
            app.DataRequest('QueryJBHaltData', { code: 'QueryJBHaltEquData', Keycode: 'Pub_RollEquType', sdate: startdate, edate: enddate, down: 0, up: 10 }, function (data) {
                var Res1;
                if (data.length > 5) {
                   Res1 = new Array(5);
                }
                else {
                   Res1 = new Array(data.length);
                }
                for (var i = 0; i < data.length&&i < 5; i++) {
                       Res1[i] = { equ: data[i]['EQU'], reason: data[i]['REASON'], num: parseInt(data[i]['NUM']), time: parseFloat(data[i]['TIMES']) };
                    
                }
                $scope.ResultList1 = Res1;

            }, null, false);

            app.DataRequest('QueryJBHaltData', { code: 'QueryJBHaltEquSum', Keycode: 'Pub_RollEquType', sdate: startdate, edate: enddate, down: 0, up: 10 }, function (data) {
                if (data.length > 0) {
                    $scope.Cnt1 = parseInt(data[0]['NUM']);

                    if (data[0]['TIMES'] != '') {
                        $scope.Duration1 = parseFloat(data[0]['TIMES']);
                    }
                    else {
                        $scope.Duration1 = 0;
                    }

                }
                else
                {
                    $scope.Cnt1 = 0;
                    $scope.Duration1 = 0;
                }
            }, null, false);

            app.DataRequest('QueryJBHaltData', { code: 'QueryJBHaltEquData', Keycode: 'Pub_RollEquType', sdate: startdate, edate: enddate, down: 10, up: 30 }, function (data) {
                var Res2;
                if (data.length > 5) {
                    Res2 = new Array(5);
                }
                else {
                    Res2 = new Array(data.length);
                }
                for (var i = 0; i < data.length&&i < 5; i++) {
                    
                       Res2[i] = { equ: data[i]['EQU'], reason: data[i]['REASON'], num: parseInt(data[i]['NUM']), time: parseFloat(data[i]['TIMES']) };
                    
                }
                $scope.ResultList2 = Res2;
            }, null, false);

            app.DataRequest('QueryJBHaltData', { code: 'QueryJBHaltEquSum', Keycode: 'Pub_RollEquType', sdate: startdate, edate: enddate, down: 10, up: 30 }, function (data) {
                if (data.length > 0) {
                    $scope.Cnt2 = parseInt(data[0]['NUM']);
                    if (data[0]['TIMES'] != '') {
                        $scope.Duration2 = parseFloat(data[0]['TIMES']);
                    }
                    else {
                        $scope.Duration2 = 0;
                    }
                }
                else
                {
                    $scope.Cnt2 = 0;
                    $scope.Duration2 = 0;
                }
            }, null, false);

            app.DataRequest('QueryJBHaltData', { code: 'QueryJBHaltEquData', Keycode: 'Pub_RollEquType', sdate: startdate, edate: enddate, down: 30, up: 10000 }, function (data) {
                var Res3;
                if (data.length > 5) {
                    Res3 = new Array(5);
                }
                else {
                    Res3 = new Array(data.length);
                }
                for (var i = 0; i < data.length && i < 5; i++) {
                        Res3[i] = { equ: data[i]['EQU'], reason: data[i]['REASON'], num: parseInt(data[i]['NUM']), time: parseFloat(data[i]['TIMES']) };

                }
                $scope.ResultList3 = Res3;
            }, null, false);

            app.DataRequest('QueryJBHaltData', { code: 'QueryJBHaltEquSum', Keycode: 'Pub_RollEquType', sdate: startdate, edate: enddate, down: 30, up: 10000 }, function (data) {
                if (data.length > 0) {
                    $scope.Cnt3 = parseInt(data[0]['NUM']);
                    if (data[0]['TIMES'] != '') {
                        $scope.Duration3 = parseFloat(data[0]['TIMES']);
                    }
                    else {
                        $scope.Duration3 = 0;
                    }
                }
                else
                {
                    $scope.Cnt3 = 0;
                    $scope.Duration3 = 0;
                }
            }, null, false);

            app.DataRequest('QueryJBHaltData', { code: 'QueryJBHaltEquData', Keycode: 'Pub_PackEquType', sdate: startdate, edate: enddate, down: 0, up: 10 }, function (data) {
                var Res4;
                if (data.length > 5) {
                    Res4 = new Array(5);
                }
                else {
                    Res4 = new Array(data.length);
                }
                for (var i = 0; i < data.length&&i < 5; i++) {
                       Res4[i] = { equ: data[i]['EQU'], reason: data[i]['REASON'], num: parseInt(data[i]['NUM']), time: parseFloat(data[i]['TIMES']) };
       
                }
                $scope.ResultList4 = Res4;
            }, null, false);

            app.DataRequest('QueryJBHaltData', { code: 'QueryJBHaltEquSum', Keycode: 'Pub_PackEquType', sdate: startdate, edate: enddate, down: 0, up: 10 }, function (data) {
                if (data.length > 0) {
                    $scope.Cnt4 = parseInt(data[0]['NUM']);
                    if (data[0]['TIMES'] != '') {
                        $scope.Duration4 = parseFloat(data[0]['TIMES']);
                    }
                    else {
                        $scope.Duration4 = 0;
                    }
                }
                else
                {
                    $scope.Cnt4 = 0;
                    $scope.Duration4 = 0;
                }
            }, null, false);

            app.DataRequest('QueryJBHaltData', { code: 'QueryJBHaltEquData', Keycode: 'Pub_PackEquType', sdate: startdate, edate: enddate, down: 10, up: 30 }, function (data) {
                var Res5;
                if (data.length > 5) {
                    Res5 = new Array(5);
                }
                else {
                    Res5 = new Array(data.length);
                }
                for (var i = 0; i < data.length&&i < 5; i++) {
                       Res5[i] = { equ: data[i]['EQU'], reason: data[i]['REASON'], num: parseInt(data[i]['NUM']), time: parseFloat(data[i]['TIMES']) };
             
                }
                $scope.ResultList5 = Res5;
            }, null, false);

            app.DataRequest('QueryJBHaltData', { code: 'QueryJBHaltEquSum', Keycode: 'Pub_PackEquType', sdate: startdate, edate: enddate, down: 10, up: 30 }, function (data) {
                if (data.length > 0) {
                    $scope.Cnt5 = parseInt(data[0]['NUM']);
                    if (data[0]['TIMES'] != '') {
                        $scope.Duration5 = parseFloat(data[0]['TIMES']);
                    }
                    else {
                        $scope.Duration5 = 0;
                    }
                }
                else
                {
                    $scope.Cnt5 = 0;
                    $scope.Duration5 = 0;
                }
            }, null, false);

            app.DataRequest('QueryJBHaltData', { code: 'QueryJBHaltEquData', Keycode: 'Pub_PackEquType', sdate: startdate, edate: enddate, down: 30, up: 10000 }, function (data) {
                var Res6;
                if (data.length > 5) {
                    Res6 = new Array(5);
                }
                else {
                    Res6 = new Array(data.length);
                }
                for (var i = 0; i < data.length&&i < 5; i++) {
                        Res6[i] = { equ: data[i]['EQU'], reason: data[i]['REASON'],num: parseInt(data[i]['NUM']),  time: parseFloat(data[i]['TIMES']) };
              
                }
                $scope.ResultList6 = Res6;
            }, null, false);

            app.DataRequest('QueryJBHaltData', { code: 'QueryJBHaltEquSum', Keycode: 'Pub_PackEquType', sdate: startdate, edate: enddate, down: 30, up: 10000 }, function (data) {
                if (data.length>0) {
                    $scope.Cnt6 = parseInt(data[0]['NUM']);
                    if (data[0]['TIMES'] != '') {
                        $scope.Duration6 = parseFloat(data[0]['TIMES']);
                    }
                    else {
                        $scope.Duration6 = 0;
                    }
                }
                else
                {
                    $scope.Cnt6 = 0;
                    $scope.Duration6 = 0;
                }
      
            }, null, false);

        } catch (e) {
        }
    }
})

function resize() {
    Scroll1();
}

function Scroll1() {
    try {
        $("#Scroll1").slimScroll({
            height: $(".main-content1").height(),
            alwaysVisible: false,
            color: "#38393d",
            railColor: "#525357",
            railOpacity: 1,
            railVisible: true,
            disableFadeOut: true
        });
    } catch (e) {

    }

}