$(function () {
    resize();
    $(window).on("resize", resize);
})

var thapp = angular.module('JBTHapp', []);
thapp.controller('JBTHctl', function ($scope) {
    $scope.TemHumData;

    $scope.Refresh = function () {
        var CurrentDt = new Date().Format("yyyy-MM-dd");

        app.DataRequest('GetAreaTemHumData', { code: 'QueryJBTemHumData', BussiDate: CurrentDt }, function (data) {
            $scope.TemHumData = data;
        }, null, false);
    }

    $scope.Refresh();
});

function resize() {
    Scroll1();
}

function Scroll1() {
    $("#Scroll1").slimScroll({
        height: $(".main-content1").height(),
        alwaysVisible: false,
        color: "#686868",
        railColor: "#525357",
        railOpacity: 1,
        railVisible: true,
        disableFadeOut: true
    });
}