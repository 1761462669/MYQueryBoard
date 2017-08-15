var storeapp = angular.module('XHAPP', []);
storeapp.controller('XHCTRL', function ($scope, $http) {
    // 定义获取和更新时间的函数
    $scope.clock = new Date().toLocaleString();
    var updateClock = function () {
        $scope.clock = new Date().toLocaleString();
    };
    setInterval(function () {
        $scope.$apply(updateClock);
    }, 1000);
    updateClock();

    var tips = '数据时间：' + $("#ProducteDate").html() + ' 至 ' + new Date().Format("yyyy-MM-dd");
    $('#datatimetip').html(tips);

    $scope.QueryData = function (tp) {
        $scope.SelectType = tp;
        var JYZResultList;
        var ZBResultList;
        var XHResultList;
        var THResultList;
        var State;
        var ShiftID;
        if (tp == 4) {
            State = 1;
            ShiftID = '72821';
        }
        else if (tp == 5)
        {
            State = 1;
            ShiftID = '72822';
        }
        else if (tp == 6) {
            State = 1;
            ShiftID = '72823';
        }
        else {
            State = tp;
            ShiftID = '-1';
        }

        $("#main").showLoading();
        //costType(卷烟纸：432，嘴棒：223，小盒：440，条盒：441)
        app.DataRequest('getDataList', { state: State, costType: "432", ShiftID: ShiftID, UnitConversion: "1" }, function (data) {
            if (data.length > 0) {
                $scope.JYZResultList = data;
                $scope.$apply();
            }
        }, null, true);

        app.DataRequest("getDataList", { state: State, costType: "223", ShiftID: ShiftID, UnitConversion: "10000" }, function (data) {
            if (data.length > 0) {
                $scope.ZBResultList = data;
                $scope.$apply();
            }
        }, null, true);

        app.DataRequest("getDataList", { state: State, costType: "440", ShiftID: ShiftID, UnitConversion: "10000" }, function (data) {
            if (data.length > 0) {
                $scope.XHResultList = data;
                $scope.$apply();
            }
        }, null, true);

        app.DataRequest("getDataList", { state: State, costType: "441", ShiftID: ShiftID, UnitConversion: "10000" }, function (data) {
            if (data.length > 0) {
                $scope.THResultList = data;
                $scope.$apply();
            }
            $("#main").hideLoading();
        }, null, true);

    }

    $scope.QueryData(3);
});