$(function () {

    resiz();
    $(window).on("resize", resiz);    
    
   
})

function resiz() {
    setscroll1();
}

function setscroll1() {
    $("#storelist").slimScroll({
        height: $(".miancontent").height(),
        alwaysVisible: false,
        color: "#38393d",
        railColor: "#525357",
        railOpacity: 1,
        railVisible: true,
        disableFadeOut: true
    });
}



var storeapp = angular.module('storeapp', []);
storeapp.controller('storectl', function ($scope, $http) {

    $scope.SelectType;
    $scope.SelectItem;
    var interval;
    app.DataRequest("QueryStoreType", null, function (data) {
        $scope.StoreTypeList = data;
        if (data.length > 0) {
            $scope.SelectType = data[0];
            GetStoreList();
        }
    }, null, false);

    $scope.TypeClk = function (item) {
        //debugger;
        $scope.SelectType = item;
        GetStoreList();
    };

    function GetStoreList() {
        interval = null;
        app.DataRequest("QueryStoreInfoByType", { typeid: $scope.SelectType.ID }, function (data) {
            $scope.StoreList = data;
            $scope.$apply($scope.StoreList);
            setRound();
            //interval = setInterval(GetStoreList, 5000);            
        }, function (error) {
            //interval = setInterval(GetStoreList, 5000);
        });
    };

    $scope.dlgShow = function (item) {
        $scope.SelectItem = item;
        $("#myModal").modal();
        $scope.$apply($scope.SelectItem);
    };

    $scope.StateChange = function (stateid) {        
        if (stateid == 1)
            return "正在进柜";
        else if (stateid == 2)
            return "存料";
        else if (stateid == 3)
            return "正在出柜";
        else
            return "空柜";
    }

    function setRound() {
        $(".roundproces").each(function (index, el) {
            //debugger;
            var percent = Number($(el).data("percent"));
            //var store = Number($(el).data("store"));
            //debugger;
            //var round = $(el).data("radialIndicator");
            
                var round = radialIndicator(el, {
                    barColor: "#4fbdde",
                    barBgColor: "#0d0d0d",
                    fontColor: "#fff",
                    fontWeight: "normal",
                    fontSize: "24px",
                    radius: 40,
                    maxValue: 100,
                    barWidth: 6,
                    initValue: percent,
                    roundCorner: true,
                    percentage: true

                });
                //$(el).radialIndicator()
            

            //round.animate(50);
            
        })

    }
});