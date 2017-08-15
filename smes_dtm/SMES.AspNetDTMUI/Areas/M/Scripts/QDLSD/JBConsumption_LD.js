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

    $scope.QueryData = function (flag) {
        $("#main").showLoading();
        $scope.SelectType = flag;
        var XHLIST;
        if (flag == 1) {
            app.DataRequest('GetDataByDay', {}, function (data) {
                if (data.length > 0) {
                    $scope.XHLIST = data;
                    $("#qDate").addClass('querychecked');
                    $scope.$apply();
                }
                $("#main").hideLoading();
            }, null, true);
        }
        if (flag == 2) {
            app.DataRequest('GetDataByWeek', {}, function (data) {
                if (data.length > 0) {
                    $scope.XHLIST = data;
                    $("#qWeek").addClass('querychecked');
                    $scope.$apply();
                }
                $("#main").hideLoading();
            }, null, true);
        }
        if (flag == 3) {
            app.DataRequest('GetDataByMonth', {}, function (data) {
                if (data.length > 0) {
                    $scope.XHLIST = data;
                    $("#qMonth").addClass('querychecked');
                    $scope.$apply();
                }
                $("#main").hideLoading();
            }, null, true);
        }
        if (flag == 4) {
            app.DataRequest('GetDataByDayAndShift', {ShiftNM:'72821'}, function (data) {
                if (data.length > 0) {
                    $scope.XHLIST = data;
                    $("#qMonth").addClass('querychecked');
                    $scope.$apply();
                }
                $("#main").hideLoading();
            }, null, true);
        }
        if (flag == 5) {
            app.DataRequest('GetDataByDayAndShift', { ShiftNM: '72822' }, function (data) {
                if (data.length > 0) {
                    $scope.XHLIST = data;
                    $("#qMonth").addClass('querychecked');
                    $scope.$apply();
                }
                $("#main").hideLoading();
            }, null, true);
        }
        if (flag == 6) {
            app.DataRequest('GetDataByDayAndShift', { ShiftNM: '72823' }, function (data) {
                if (data.length > 0) {
                    $scope.XHLIST = data;
                    $("#qMonth").addClass('querychecked');
                    $scope.$apply();
                }
                $("#main").hideLoading();
            }, null, true);
        }
    }

    $scope.QueryData(1);
});