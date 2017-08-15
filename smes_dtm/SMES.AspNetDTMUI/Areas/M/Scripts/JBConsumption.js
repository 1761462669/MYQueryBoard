var storeapp = angular.module('XHAPP', []);
storeapp.controller('XHCTRL', function ($scope, $http) {
    $("#StartDate").val(getBeforeDate(1));
    $("#EndDate").val(getBeforeDate(0));


    $('#StartDate').datepicker({
        onSelect: function () {
            if ($('#StartDate').val() > $('#EndDate').val()) {
                $('#EndDate').val($('#StartDate').val());
            }
        }
    });
    $('#EndDate').datepicker({
        onSelect: function () {
            if ($('#StartDate').val() > $('#EndDate').val()) {
                $('#StartDate').val($('#EndDate').val());
            }
        }
    });

    //获取日期
    function getBeforeDate(n) {
        var n = n;
        var d = new Date();
        var year = d.getFullYear();
        var mon = d.getMonth() + 1;
        var day = d.getDate();
        if (day <= n) {
            if (mon > 1) {
                mon = mon - 1;
            }
            else {
                year = year - 1;
                mon = 12;
            }
        }
        d.setDate(d.getDate() - n);
        year = d.getFullYear();
        mon = d.getMonth() + 1;
        day = d.getDate();
        s = year + "-" + (mon < 10 ? ('0' + mon) : mon) + "-" + (day < 10 ? ('0' + day) : day);
        return s;
    }

    $scope.QueryData = function (flag) {
        $("#main").showLoading();
        $scope.SelectType = flag;
        var strDate = $("#StartDate").val();
        var eDate = $("#EndDate").val();
        //debugger;
        var XHLIST;
        //if (flag == 1) {
            app.DataRequest('GetDataByDay', {StrDate: strDate, endDate: eDate}, function (data) {
                if (data.length > 0) {
                    $scope.XHLIST = data;
                    $("#qDate").addClass('querychecked');
                    $scope.$apply();
                }
                $("#main").hideLoading();
            }, null, true);
        //}
        //if (flag == 2) {
        //    app.DataRequest('GetDataByWeek', {}, function (data) {
        //        if (data.length > 0) {
        //            $scope.XHLIST = data;
        //            $("#qWeek").addClass('querychecked');
        //            $scope.$apply();
        //        }
        //        $("#main").hideLoading();
        //    }, null, true);
        //}
        //if (flag == 3) {
        //    app.DataRequest('GetDataByMonth', {}, function (data) {
        //        if (data.length > 0) {
        //            $scope.XHLIST = data;
        //            $("#qMonth").addClass('querychecked');
        //            $scope.$apply();
        //        }
        //        $("#main").hideLoading();
        //    }, null, true);
        //}
        //if (flag == 4) {
        //    app.DataRequest('GetDataByDayAndShift', {ShiftNM:'72821'}, function (data) {
        //        if (data.length > 0) {
        //            $scope.XHLIST = data;
        //            $("#qMonth").addClass('querychecked');
        //            $scope.$apply();
        //        }
        //        $("#main").hideLoading();
        //    }, null, true);
        //}
        //if (flag == 5) {
        //    app.DataRequest('GetDataByDayAndShift', { ShiftNM: '72822' }, function (data) {
        //        if (data.length > 0) {
        //            $scope.XHLIST = data;
        //            $("#qMonth").addClass('querychecked');
        //            $scope.$apply();
        //        }
        //        $("#main").hideLoading();
        //    }, null, true);
        //}
        //if (flag == 6) {
        //    app.DataRequest('GetDataByDayAndShift', { ShiftNM: '72823' }, function (data) {
        //        if (data.length > 0) {
        //            $scope.XHLIST = data;
        //            $("#qMonth").addClass('querychecked');
        //            $scope.$apply();
        //        }
        //        $("#main").hideLoading();
        //    }, null, true);
        //}
    }

    $scope.QueryData(1);
});