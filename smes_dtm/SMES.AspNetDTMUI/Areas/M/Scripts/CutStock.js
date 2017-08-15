var container_status = [
{ ID: 0, Name: "待检" },
{ ID: 1, Name: "可用" },
{ ID: 2, Name: "不可用" },
{ ID: 3, Name: "暂存" }
];

var lock_status = [
{ ID: "L", Name: "锁定" },
{ID:"U",Name:"解锁"}
]

var selectedkcid = "-1";
var selectedlockid = "-1";

var boxsdata = [];

$(function () {

    resiz();
    $(window).on("resize", resiz);      
    addwhere();
    QueryCutStockMat();

    $("#btnquery").click(QueryCutStockMat);
})


function addwhere()
{
    $("#kcstate").append('<li><a href="#" data-id="-1">全部</a></li>');

    for (var i = 0; i < container_status.length; i++)
    {
        $("#kcstate").append('<li><a href="#" data-id="' + container_status[i].ID + '">' + container_status[i].Name + '</a></li>');
    }
    //container_status.forEach(function (item) {
    //    $("#kcstate").append('<li><a href="#" data-id="'+item.ID+'">' + item.Name + '</a></li>');
    //});

    $("#kcstate>li>a").first().addClass("productactive");

    $("#lockstate").append('<li><a href="#" data-id="-1">全部</a></li>');

    for (var i = 0; i < lock_status.length; i++)
    {
        $("#lockstate").append('<li><a href="#" data-id="' + lock_status[i].ID + '">' + lock_status[i].Name + '</a></li>');
    }
    //lock_status.forEach(function (item) {
    //    $("#lockstate").append('<li><a href="#" data-id="'+item.ID+'">' + item.Name + '</a></li>');
    //})
    $("#lockstate>li>a").first().addClass("productactive");

    $("#kcstate>li>a").click(function () {
        if ($(this).hasClass("productactive"))
            return;

        $(this).closest("ul").find(".productactive").removeClass("productactive");

        $(this).addClass("productactive");

        selectedkcid = $(this).data("id");

        TmplBoxsData();

        return false;
    })

    $("#lockstate>li>a").click(function () {

        if ($(this).hasClass("productactive"))
            return;

        $(this).closest("ul").find(".productactive").removeClass("productactive");

        $(this).addClass("productactive");

        selectedlockid = $(this).data("id");
        TmplBoxsData();

        return false;
    })
}

function resiz() {
    setscroll1();
    setScroll3();
    setScroll4();
    
}

function setscroll1() {
    //debugger;
    $(".productarea").slimScroll({
        height: $(".content-left").height() - 40 - $(".queryarea").height(),
        alwaysVisible: false,
        color: "#38393d",
        railColor: "#525357",
        railOpacity: 1,
        railVisible: true,
        disableFadeOut: true
    });
}

function setScroll3() {
    $("#scoll2").slimScroll({
        height: $(".content-right").height(),
        alwaysVisible: false,
        color: "#38393d",
        railColor: "#525357",
        railOpacity: 1,
        railVisible: true,
        disableFadeOut: true
    });
}

function setScroll4() {
    $("#scortable").slimScroll({
        height: 150,
        alwaysVisible: false,
        color: "#e7eefe",
        railColor: "#e4e5e9",
        railOpacity: 1,
        railVisible: true,
        disableFadeOut: true
    });
}

function ProductSelected()
{   
    $(".productarea a").click(function () {
        if ($(this).hasClass("productactive"))
            return;

        $("#productlist").find(".productactive").removeClass("productactive");
        $(this).addClass("productactive");

        var mat = $(this).tmplItem().data;
        if (mat == null || mat == undefined)
            return;

        QueryBatchListData(mat.MATERIAL_CD);
    })
}

function QueryCutStockMat()
{
    var key = $("#txtkey").val();
    //debugger;
    app.DataRequest("QueryCutStockMat", { key: key }, function (data) {
        $("#productlist").empty();
        $("#productlisttmp").tmpl(data).appendTo("#productlist");

        ProductSelected();
        
        $("#productlist a").first().click();       
    })
}

function QueryBatchListData(materialcd)
{
    app.DataRequest("QueryCutStockBatchList", { materialcd: materialcd }, function (data) {

        $("#batchlist").empty();
        $("#batchlisttmpl").tmpl(data, {
            fmtprocess: function (v1, v2) {
                if (v2 == 0)
                    return "0%";
                else
                    return (Number(v1)/Number(v2)*100)+"%"

            }
        }).appendTo("#batchlist");

        $(".module-batch").click(function () {
            
            selectedkcid = "-1";
            selectedlockid = "-1";

            $("#lockstate").find(".productactive").removeClass("productactive");
            $("#kcstate").find(".productactive").removeClass("productactive");

            $("#lockstate>li>a").first().addClass("productactive");
            $("#kcstate>li>a").first().addClass("productactive");

            $("#tllist").empty();

            $("#myModal").modal();

            var model = $(this).tmplItem().data;
            if (model == null || model == undefined)
                return;

            QueryBatchLockDetail(model.BATCH_NO);
        })
    });
}


function QueryBatchLockDetail(lot)
{
    app.DataRequest("QueryBatchLockDetail", { lot: lot }, function (data) {
        //debugger;
        boxsdata = data;
        TmplBoxsData();
        //TmplBoxsData(data);
    });
}

function TmplBoxsData()
{
    var data = boxsdata;

    if (selectedkcid != "-1")
    {
        data = data.filter(function (item) {
            if (item.CONTAINER_STATUS == selectedkcid)
                return 1;
            else
                return 0;
        })
    }

    
    if (selectedlockid != "-1")
    {
        
        data = data.filter(function (item) {
            if (item.LOCK_STATUS == selectedlockid)
                return 1;
            else
                return 0;
        })
    }

    //debugger;
    $("#tllist").empty();
    var index = 1;
    $("#tllisttmp").tmpl(data, {
        fmtindex: function () { return index++; },
        fmtkcstate: function (id) {
            var name = "未知状态";
            for (var i = 0; i < container_status.length; i++)
            {
                if (container_status[i].ID == id) {
                    name = container_status[i].Name;
                    break;
                }
            }            
            return name;
        },
        fmtlockstate: function (id) {
            var name = "未执行";
            for (var i = 0; i < lock_status.length; i++)
            {
                if (lock_status[i].ID == id) {
                    name = lock_status[i].Name;
                    break;
                }
            }

            return name;
        }
    }).appendTo("#tllist");
}