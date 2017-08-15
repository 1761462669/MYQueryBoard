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

    $scope.QueryData = function (tp) {
        $scope.SelectType = tp;
        var JYZResultList;
        var ZBResultList;
        var XHResultList;
        var THResultList;
        var State;
        var ShiftID;
        var StrDate;
        var endDate;
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
            if (tp == null || tp == "") {
                State = 1
            }
            ShiftID = '-1';
        }

        $("#main").showLoading();
        //costType(卷烟纸：171399，嘴棒：171906，小盒：1026769，条盒：1026770)
        StrDate = $("#StartDate").val();
        endDate = $("#EndDate").val();
        //debugger;
        app.DataRequest('getDataList', { StrDate: StrDate, endDate: endDate, state: State, costType: "171399", ShiftID: ShiftID, UnitConversion: "1" }, function (data) {
            if (data.length > 0) {
                $scope.JYZResultList = data;
                $scope.$apply();
            }
        }, null, true);

        app.DataRequest("getDataList", { StrDate: StrDate, endDate: endDate, state: State, costType: "171906", ShiftID: ShiftID, UnitConversion: "10000" }, function (data) {
            if (data.length > 0) {
                $scope.ZBResultList = data;
                $scope.$apply();
            }
        }, null, true);

        app.DataRequest("getDataList", { StrDate: StrDate, endDate: endDate, state: State, costType: "1026769", ShiftID: ShiftID, UnitConversion: "10000" }, function (data) {
            if (data.length > 0) {
                $scope.XHResultList = data;
                $scope.$apply();
            }
        }, null, true);

        app.DataRequest("getDataList", { StrDate: StrDate, endDate: endDate, state: State, costType: "1026770", ShiftID: ShiftID, UnitConversion: "10000" }, function (data) {
            if (data.length > 0) {
                $scope.THResultList = data;
                $scope.$apply();
            }
            $("#main").hideLoading();
        }, null, true);

    }

    $scope.QueryData(1);
});