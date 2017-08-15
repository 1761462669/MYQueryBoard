var storeapp = angular.module('APP', []);
storeapp.controller('CTRL', function ($scope, $http) {

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

    $scope.QueryData = function (btn_value) {
        $scope.SelectType = btn_value;
        switch (btn_value) {
            case 1:
                $('#QueryTip').html('今天');
                break;
            case 2:
                $('#QueryTip').html('本周');
                break;
            case 3:
                $('#QueryTip').html('本月');
                break;
            default:
                break;
        }
        init();
        $("#main").showLoading();
        app.DataRequest("GetData", { DateFlag: btn_value }, function (data) {
            var zdlsj = 0.00;
            var zsh9k = 0.00;
            var zsh5k = 0.00;
            var zsh3k = 0.00;
            var zshgsx = 0.00;
            for (var i = 0; i < data.length; i++) {
                switch (data[i].RESOURCEID) {
                    //切片-9K  
                    case '2187':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh9k = accAdd(zsh9k, data[i].ZSHCY * 1);
                        }
                        $('#9kqp_cn').html(data[i].CN);
                        $('#9kqp_cy').html(data[i].ZSHCY);
                        $('#9kqp_dl').html(data[i].DLSJ);
                        $('#9kqp_hpi').html(data[i].HPICS);
                        $('#9kqp_hpai').html(data[i].HPAICS);
                        break;
                        //叶片加料-9K  
                    case '2192':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh9k = accAdd(zsh9k, data[i].ZSHCY * 1);
                        }
                        $('#9kjl_cn').html(data[i].CN);
                        $('#9kjl_cy').html(data[i].ZSHCY);
                        $('#9kjl_dl').html(data[i].DLSJ);
                        $('#9kjl_hpi').html(data[i].HPICS);
                        $('#9kjl_hpai').html(data[i].HPAICS);
                        break;
                        //切叶丝-9k  
                    case '2195':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh9k = accAdd(zsh9k, data[i].ZSHCY * 1);
                        }
                        //$('#9kqs_cn').html(data[i].CN);
                        //$('#9kqs_cy').html('0');
                        //$('#9kqs_dl').html(data[i].DLSJ);
                        //$('#9kqs_hpi').html(data[i].HPICS);
                        //$('#9kqs_hpai').html(data[i].HPAICS);
                        break;
                        //烘叶丝-9k  
                    case '2199':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh9k = accAdd(zsh9k, data[i].ZSHCY * 1);
                        }
                        $('#9khs_cn').html(data[i].CN);
                        $('#9khs_cy').html(data[i].ZSHCY);
                        $('#9khs_dl').html(data[i].DLSJ);
                        $('#9khs_hpi').html(data[i].HPICS);
                        $('#9khs_hpai').html(data[i].HPAICS);
                        break;
                        //掺配加香-9K 
                    case '2204':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh9k = accAdd(zsh9k, data[i].ZSHCY * 1);
                        }
                        $('#9kcp_cn').html(data[i].CN);
                        $('#9kcp_cy').html(data[i].ZSHCY);
                        $('#9kcp_dl').html(data[i].DLSJ);
                        $('#9kcp_hpi').html(data[i].HPICS);
                        $('#9kcp_hpai').html(data[i].HPAICS);
                        break;
                        //烟丝入库-9K
                    case '2211':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh9k = accAdd(zsh9k, data[i].ZSHCY * 1);
                        }
                        $('#9krk_cn').html(data[i].CN);
                        $('#9krk_cy').html(data[i].ZSHCY);
                        $('#9krk_dl').html(data[i].DLSJ);
                        $('#9krk_hpi').html(data[i].HPICS);
                        $('#9krk_hpai').html(data[i].HPAICS);
                        break;
                        //真空回潮-5K
                    case '2120':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh5k = accAdd(zsh5k, data[i].ZSHCY * 1);
                        }
                        $('#5khc_cn').html(data[i].CN);
                        $('#5khc_cy').html(data[i].ZSHCY);
                        $('#5khc_dl').html(data[i].DLSJ);
                        $('#5khc_hpi').html(data[i].HPICS);
                        $('#5khc_hpai').html(data[i].HPAICS);
                        break;
                        //切片-5K
                    case '2125':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh5k = accAdd(zsh5k, data[i].ZSHCY * 1);
                        }
                        $('#5kqp_cn').html(data[i].CN);
                        $('#5kqp_cy').html(data[i].ZSHCY);
                        $('#5kqp_dl').html(data[i].DLSJ);
                        $('#5kqp_hpi').html(data[i].HPICS);
                        $('#5kqp_hpai').html(data[i].HPAICS);
                        break;
                        //叶片加料-5K
                    case '2128':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh5k = accAdd(zsh5k, data[i].ZSHCY * 1);
                        }
                        $('#5kjl_cn').html(data[i].CN);
                        $('#5kjl_cy').html(data[i].ZSHCY);
                        $('#5kjl_dl').html(data[i].DLSJ);
                        $('#5kjl_hpi').html(data[i].HPICS);
                        $('#5kjl_hpai').html(data[i].HPAICS);
                        break;
                        //切叶丝-5k
                    case '2132':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh5k = accAdd(zsh5k, data[i].ZSHCY * 1);
                        }
                        //$('5kqs_cn').html(data[i].CN);
                        //$('5kqs_cy').html('0');
                        //$('5kqs_dl').html(data[i].DLSJ);
                        //$('5kqs_hpi').html(data[i].HPICS);
                        //$('5kqs_hpai').html(data[i].HPAICS);
                        break;
                        //烘叶丝-5k
                    case '2136':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh5k = accAdd(zsh5k, data[i].ZSHCY * 1);
                        }
                        $('#5khs_cn').html(data[i].CN);
                        $('#5khs_cy').html(data[i].ZSHCY);
                        $('#5khs_dl').html(data[i].DLSJ);
                        $('#5khs_hpi').html(data[i].HPICS);
                        $('#5khs_hpai').html(data[i].HPAICS);
                        break;
                        //掺配-5K
                    case '2140':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh5k = accAdd(zsh5k, data[i].ZSHCY * 1);
                        }
                        $('#5kcp_cn').html(data[i].CN);
                        $('#5kcp_cy').html(data[i].ZSHCY);
                        $('#5kcp_dl').html(data[i].DLSJ);
                        $('#5kcp_hpi').html(data[i].HPICS);
                        $('#5kcp_hpai').html(data[i].HPAICS);
                        break;
                        //加香-5K
                    case '2145':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh5k = accAdd(zsh5k, data[i].ZSHCY * 1);
                        }
                        $('#5kjx_cn').html(data[i].CN);
                        $('#5kjx_cy').html(data[i].ZSHCY);
                        $('#5kjx_dl').html(data[i].DLSJ);
                        $('#5kjx_hpi').html(data[i].HPICS);
                        $('#5kjx_hpai').html(data[i].HPAICS);
                        break;
                        //烟丝入库-5K
                    case '2148':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh5k = accAdd(zsh5k, data[i].ZSHCY * 1);
                        }
                        $('#5krk_cn').html(data[i].CN);
                        $('#5krk_cy').html(data[i].ZSHCY);
                        $('#5krk_dl').html(data[i].DLSJ);
                        $('#5krk_hpi').html(data[i].HPICS);
                        $('#5krk_hpai').html(data[i].HPAICS);
                        break;
                        //切片-3K
                    case '2149':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh3k = accAdd(zsh3k, data[i].ZSHCY * 1);
                        }
                        $('#3kqp_cn').html(data[i].CN);
                        $('#3kqp_cy').html(data[i].ZSHCY);
                        $('#3kqp_dl').html(data[i].DLSJ);
                        $('#3kqp_hpi').html(data[i].HPICS);
                        $('#3kqp_hpai').html(data[i].HPAICS);
                        break;
                        //叶片加料-3K
                    case '2154':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh3k = accAdd(zsh3k, data[i].ZSHCY * 1);
                        }
                        $('#3kjl_cn').html(data[i].CN);
                        $('#3kjl_cy').html(data[i].ZSHCY);
                        $('#3kjl_dl').html(data[i].DLSJ);
                        $('#3kjl_hpi').html(data[i].HPICS);
                        $('#3kjl_hpai').html(data[i].HPAICS);
                        break;
                        //配片预混出柜-3K
                    case '2158':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh3k = accAdd(zsh3k, data[i].ZSHCY * 1);
                        }
                        $('#3kyhcg_cn').html(data[i].CN);
                        $('#3kyhcg_cy').html(data[i].ZSHCY);
                        $('#3kyhcg_dl').html(data[i].DLSJ);
                        $('#3kyhcg_hpi').html(data[i].HPICS);
                        $('#3kyhcg_hpai').html(data[i].HPAICS);
                        break;
                        //切叶丝-3K
                    case '2159':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh3k = accAdd(zsh3k, data[i].ZSHCY * 1);
                        }
                        //$('3kqs_cn').html(data[i].CN);
                        //$('3kqs_cy').html('0');
                        //$('3kqs_dl').html(data[i].DLSJ);
                        //$('3kqs_hpi').html(data[i].HPICS);
                        //$('3kqs_hpai').html(data[i].HPAICS);
                        break;
                        //滚筒烘丝-3K
                    case '2163':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh3k = accAdd(zsh3k, data[i].ZSHCY * 1);
                        }
                        $('#3kgths_cn').html(data[i].CN);
                        $('#3kgths_cy').html(data[i].ZSHCY);
                        $('#3kgths_dl').html(data[i].DLSJ);
                        $('#3kgths_hpi').html(data[i].HPICS);
                        $('#3kgths_hpai').html(data[i].HPAICS);
                        break;
                        //气流烘丝-3K
                    case '2168':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zsh3k = accAdd(zsh3k, data[i].ZSHCY * 1);
                        }
                        $('#3kqlhs_cn').html(data[i].CN);
                        $('#3kqlhs_cy').html(data[i].ZSHCY);
                        $('#3kqlhs_dl').html(data[i].DLSJ);
                        $('#3kqlhs_hpi').html(data[i].HPICS);
                        $('#3kqlhs_hpai').html(data[i].HPAICS);
                        break;
                        //梗预处理
                    case '2173':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zshgsx = accAdd(zshgsx, data[i].ZSHCY * 1);
                        }
                        $('#gsxgy_cn').html(data[i].CN);
                        $('#gsxgy_cy').html(data[i].ZSHCY);
                        $('#gsxgy_dl').html(data[i].DLSJ);
                        $('#gsxgy_hpi').html(data[i].HPICS);
                        $('#gsxgy_hpai').html(data[i].HPAICS);
                        break;
                        //切梗
                    case '2177':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zshgsx = accAdd(zshgsx, data[i].ZSHCY * 1);
                        }
                        $('#gsxqg_cn').html(data[i].CN);
                        $('#gsxqg_cy').html(data[i].ZSHCY);
                        $('#gsxqg_dl').html(data[i].DLSJ);
                        $('#gsxqg_hpi').html(data[i].HPICS);
                        $('#gsxqg_hpai').html(data[i].HPAICS);
                        break;
                        //烘梗
                    case '2180':
                        if (data[i].DLSJ.toString() != '0') {
                            zdlsj = accAdd(zdlsj, data[i].DLSJ * 1);
                        }
                        if (data[i].ZSHCY.toString() != '0') {
                            zshgsx = accAdd(zshgsx, data[i].ZSHCY * 1);
                        }
                        $('#gsxhg_cn').html(data[i].CN);
                        $('#gsxhg_cy').html(data[i].ZSHCY);
                        $('#gsxhg_dl').html(data[i].DLSJ);
                        $('#gsxhg_hpi').html(data[i].HPICS);
                        $('#gsxhg_hpai').html(data[i].HPAICS);
                        break;

                    default:
                        break;
                }
            }
            $('#ZDL').html(zdlsj.toString());
            $('#9kTimeDiff').html(zsh9k.toString());
            $('#5kTimeDiff').html(zsh5k.toString());
            $('#3kTimeDiff').html(zsh3k.toString());
            $('#gsxTimeDiff').html(zshgsx.toString());
            $("#main").hideLoading();
        },null,true);
    }

    function init() {//初始化界面值
        //切片-9K  
        $('#9kqp_cn').html('0');
        $('#9kqp_cy').html('0');
        $('#9kqp_dl').html('0');
        $('#9kqp_hpi').html('0');
        $('#9kqp_hpai').html('0');
        //叶片加料-9K  
        $('#9kjl_cn').html('0');
        $('#9kjl_cy').html('0');
        $('#9kjl_dl').html('0');
        $('#9kjl_hpi').html('0');
        $('#9kjl_hpai').html('0');
        //切叶丝-9k  
        //$('#9kqs_cn').html('0');
        //$('#9kqs_cy').html('0');
        //$('#9kqs_dl').html('0');
        //$('#9kqs_hpi').html('0');
        //$('#9kqs_hpai').html('0');
        //烘叶丝-9k  
        $('#9khs_cn').html('0');
        $('#9khs_cy').html('0');
        $('#9khs_dl').html('0');
        $('#9khs_hpi').html('0');
        $('#9khs_hpai').html('0');
        //掺配加香-9K 
        $('#9kcp_cn').html('0');
        $('#9kcp_cy').html('0');
        $('#9kcp_dl').html('0');
        $('#9kcp_hpi').html('0');
        $('#9kcp_hpai').html('0');
        //烟丝入库-9K
        $('#9krk_cn').html('0');
        $('#9krk_cy').html('0');
        $('#9krk_dl').html('0');
        $('#9krk_hpi').html('0');
        $('#9krk_hpai').html('0');
        //真空回潮-5K
        $('#5khc_cn').html('0');
        $('#5khc_cy').html('0');
        $('#5khc_dl').html('0');
        $('#5khc_hpi').html('0');
        $('#5khc_hpai').html('0');
        //切片-5K
        $('#5kqp_cn').html('0');
        $('#5kqp_cy').html('0');
        $('#5kqp_dl').html('0');
        $('#5kqp_hpi').html('0');
        $('#5kqp_hpai').html('0');
        //叶片加料-5K
        $('#5kjl_cn').html('0');
        $('#5kjl_cy').html('0');
        $('#5kjl_dl').html('0');
        $('#5kjl_hpi').html('0');
        $('#5kjl_hpai').html('0');
        //切叶丝-5k
        //$('5kqs_cn').html('0');
        //$('5kqs_cy').html('0');
        //$('5kqs_dl').html('0');
        //$('5kqs_hpi').html('0');
        //$('5kqs_hpai').html('0');
        //烘叶丝-5k
        $('#5khs_cn').html('0');
        $('#5khs_cy').html('0');
        $('#5khs_dl').html('0');
        $('#5khs_hpi').html('0');
        $('#5khs_hpai').html('0');
        //掺配-5K
        $('#5kcp_cn').html('0');
        $('#5kcp_cy').html('0');
        $('#5kcp_dl').html('0');
        $('#5kcp_hpi').html('0');
        $('#5kcp_hpai').html('0');
        //加香-5K
        $('#5kjx_cn').html('0');
        $('#5kjx_cy').html('0');
        $('#5kjx_dl').html('0');
        $('#5kjx_hpi').html('0');
        $('#5kjx_hpai').html('0');
        //烟丝入库-5K
        $('#5krk_cn').html('0');
        $('#5krk_cy').html('0');
        $('#5krk_dl').html('0');
        $('#5krk_hpi').html('0');
        $('#5krk_hpai').html('0');
        //切片-3K
        $('#3kqp_cn').html('0');
        $('#3kqp_cy').html('0');
        $('#3kqp_dl').html('0');
        $('#3kqp_hpi').html('0');
        $('#3kqp_hpai').html('0');
        //叶片加料-3K
        $('#3kjl_cn').html('0');
        $('#3kjl_cy').html('0');
        $('#3kjl_dl').html('0');
        $('#3kjl_hpi').html('0');
        $('#3kjl_hpai').html('0');
        //配片预混出柜-3K
        $('#3kyhcg_cn').html('0');
        $('#3kyhcg_cy').html('0');
        $('#3kyhcg_dl').html('0');
        $('#3kyhcg_hpi').html('0');
        $('#3kyhcg_hpai').html('0');

        //切叶丝-3K
        //$('3kqs_cn').html('0');
        //$('3kqs_cy').html('0');
        //$('3kqs_dl').html('0');
        //$('3kqs_hpi').html('0');
        //$('3kqs_hpai').html('0');

        //滚筒烘丝-3K
        $('#3kgths_cn').html('0');
        $('#3kgths_cy').html('0');
        $('#3kgths_dl').html('0');
        $('#3kgths_hpi').html('0');
        $('#3kgths_hpai').html('0');

        //气流烘丝-3K
        $('#3kqlhs_cn').html('0');
        $('#3kqlhs_cy').html('0');
        $('#3kqlhs_dl').html('0');
        $('#3kqlhs_hpi').html('0');
        $('#3kqlhs_hpai').html('0');

        //梗预处理
        $('#gsxgy_cn').html('0');
        $('#gsxgy_cy').html('0');
        $('#gsxgy_dl').html('0');
        $('#gsxgy_hpi').html('0');
        $('#gsxgy_hpai').html('0');

        //切梗
        $('#gsxqg_cn').html('0');
        $('#gsxqg_cy').html('0');
        $('#gsxqg_dl').html('0');
        $('#gsxqg_hpi').html('0');
        $('#gsxqg_hpai').html('0');

        //烘梗
        $('#gsxhg_cn').html('0');
        $('#gsxhg_cy').html('0');
        $('#gsxhg_dl').html('0');
        $('#gsxhg_hpi').html('0');
        $('#gsxhg_hpai').html('0');

        //总待料时间
        $('#ZDL').html('0');
    }


    $scope.QueryData(3);
    /**
     ** 加法函数，用来得到精确的加法结果
     ** 说明：javascript的加法结果会有误差，在两个浮点数相加的时候会比较明显。这个函数返回较为精确的加法结果。
     ** 调用：accAdd(arg1,arg2)
     ** 返回值：arg1加上arg2的精确结果
     **/
    function accAdd(arg1, arg2) {
        var r1, r2, m, c;
        try {
            r1 = arg1.toString().split(".")[1].length;
        }
        catch (e) {
            r1 = 0;
        }
        try {
            r2 = arg2.toString().split(".")[1].length;
        }
        catch (e) {
            r2 = 0;
        }
        c = Math.abs(r1 - r2);
        m = Math.pow(10, Math.max(r1, r2));
        if (c > 0) {
            var cm = Math.pow(10, c);
            if (r1 > r2) {
                arg1 = Number(arg1.toString().replace(".", ""));
                arg2 = Number(arg2.toString().replace(".", "")) * cm;
            } else {
                arg1 = Number(arg1.toString().replace(".", "")) * cm;
                arg2 = Number(arg2.toString().replace(".", ""));
            }
        } else {
            arg1 = Number(arg1.toString().replace(".", ""));
            arg2 = Number(arg2.toString().replace(".", ""));
        }
        return (arg1 + arg2) / m;
    }
})