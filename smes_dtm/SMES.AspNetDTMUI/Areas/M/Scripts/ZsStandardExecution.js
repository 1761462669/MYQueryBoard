$(function () {
    resize();
    $(window).on("resize", resize);
})

var StdApp = angular.module('StdExeapp', []);
StdApp.controller('StdExectl', function ($scope, $http) {
    $scope.BrandList;
    $scope.ZsExeStdList;
    
    app.DataRequest('GetCutBrandList', { code: "QueryCutBrands" }, function (data) {
        if (data.length > 0) {
            $scope.BrandList = data;
        }
    }, null, false);

    app.DataRequest('GetAllBrandExeStd', { code: "QueryAllBrandStd" }, function (data) {
        if (data.length > 0) {
            $scope.ZsExeStdList = data;
        }
    }, null, false);

    $scope.QueryData = function () {
        app.DataRequest('GetBrandExeStdById', { code: "QueryBrandStdById", matid: $scope.x.ID }, function (data) {
            if (data.length > 0) {
                $scope.ZsExeStdList = data;
            }
        }, null, false);
    }
});

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