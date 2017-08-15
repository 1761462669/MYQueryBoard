function setsize() {
    var heigth = $(".left").height() - 70 - $(".querycontent").height();
    $("#queryresult").height(heigth);
}

$(function () {

    $(".radiobtn").click(function () {
        alert(111);
    });
    resiz();
    $(window).on("resize", resiz);

    if ($("#backinput").val() == "") {
        $("#date").val(new Date().Format("yyyy-MM-dd"));

        $(".form-group .radiobtn").click(function () {
            if ($(this).hasClass("checked"))
                return;

            $(".form-group").find(".checked").removeClass("checked");

            $(this).addClass("checked");
        })

        QueryCutBatchInfo();

        $("#backinput").val("true");

        //var iframe = document.getElementById("produceprocessinfofrm");

        //if (iframe.attachEvent) {
        //    iframe.attachEvent("onload", function () {
        //        //以下操作必须在iframe加载完后才可进行
        //        //alert("aa");
        //    });
        //} else {
        //    iframe.onload = function () {
        //        //以下操作必须在iframe加载完后才可进行
        //        //alert("bb");
        //    };
        //}

    }


})


function resiz() {
    setsize();
    setScroll1();
    setScroll2();
    setScroll3();
}

function setScroll1() {
    $("#tree").slimScroll({
        height: $(".queryresult").height(),
        alwaysVisible: false,
        color: "#38393d",
        railColor: "#525357",
        railOpacity: 1,
        railVisible: true,
        disableFadeOut: true
    });
}

function setScroll2() {
    $("#Scroll2").slimScroll({
        height: $(".main").height(),
        alwaysVisible: false,
        color: "#38393d",
        railColor: "#525357",
        railOpacity: 1,
        railVisible: true,
        disableFadeOut: true
    });
}

function setScroll3() {
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

function setTree() {
    $("#tree a").click(function () {

        if ($(this).hasClass("active"))
            return;

        var batch = $(this).tmplItem().data;

        $("#tree .active").removeClass("active");
        $(this).addClass("active");

        BatchInfo(batch);
        woinfo(batch);
        return false;
    })

    $("#tree a").first().click();
}



//$(window).on("resize", setScroll);

/*********业务数据获取**********/

//var curBatch;

function QueryCutBatchInfo() {
    //debugger;
    var date = $("#date").val();
    var key = $("#txtkey").val();
    app.DataRequest('@Url.Action("QueryCutBatchInfo")', { plandate: date, productkey: key }, function (data) {

        var groupdatadic = [];

        data.forEach(function (value, index) {
            if (groupdatadic["ID" + value.PRODUCTID])
                groupdatadic["ID" + value.PRODUCTID].Child.push(value);
            else
                groupdatadic["ID" + value.PRODUCTID] = { ID: value.PRODUCTID, Name: value.PRODUCTNAME, Child: [value] };

        })

        var groupdata = [];

        for (var item in groupdatadic) {
            groupdata.push(groupdatadic[item]);
        }

        $("#tree").empty();
        $("#producttmpl").tmpl(groupdata).appendTo("#tree");
        setTree();
        setScroll1();

    });
}

//批次信息
function BatchInfo(batch) {
    $("#batchinfo").empty();
    $("#batchinfotmpl").tmpl(batch).appendTo("#batchinfo");

    $("#inputqty").html(batch.INPUTQTY);
    $("#outputqty").html(batch.OUTPUTQTY);
    $("#productqty").html(batch.PRODUCTQTY);
    $("#qlscore").html(batch.QLSCORE);
    $("#converscore").html(batch.CONVERSCORE);
    $("#defect").html(batch.DEFECT);
}

function woinfo(batch) {
    //debugger;
    app.DataRequest('@Url.Action("QueryWorkOrderOutPut")', { batchid: batch.ID }, function (data) {

        //对数据进行生产顺序分组

        var tmpgroup = [];
        //debugger;
        data.forEach(function (item, index) {
            if (tmpgroup["T" + item.PRODUCTSEQUEN])
                tmpgroup["T" + item.PRODUCTSEQUEN].ORDERS.push(item);
            else
                tmpgroup["T" + item.PRODUCTSEQUEN] = { SEQ: item.PRODUCTSEQUEN, ORDERS: [item] };
        });

        var groupdata = [];
        for (var item in tmpgroup) {
            groupdata.push(tmpgroup[item]);
        }

        $("#gyzinfo").empty();
        var storelist = $("#storelisttmpl").tmpl(groupdata, { getColor: app.GetColor });
        //storelist.css("width", 100 / storelist.length + "%");
        storelist.css("width", 100 / 4 + "%");
        storelist.appendTo("#gyzinfo");

        $(".infobtn").click(function () {


            var wo = $(this).tmplItem().data;

            //debugger;
            var url = '@Url.Action("ProduceProcessInfo")' + "?ID=" + wo.WORKORDERID + "&batchid=" + wo.BATCHID;

            var mask = $('<div id="produceprocessinfo" class="mask">' +
                            '<iframe src="' + url + '"></iframe>' +
                        '</div>');

            $("body").append(mask);
            //$("#produceprocessinfofrm").attr("src", url);


            mask.show(500);

            return false;
        });

        $(".tld").click(function () {
            var wo = $(this).tmplItem().data;

            $("#tlwoinfo").empty();

            $("#tllist").empty();

            $("#tlwoinfotmpl").tmpl(wo, {
                datefrm: app.DateFormat, getRate: function (inputqty, qty) {

                    if (Number(qty) == 0)
                        return 0;
                    else
                        return Math.round(Number(inputqty) / Number(qty) * 100);
                }
            }).appendTo("#tlwoinfo");

            $("#myModal").modal();

            //setScroll3
            app.DataRequest('@Url.Action("GetCutWorkorderConsume")', { id: wo.WORKORDERID }, function (data) {

                $("#tllisttmpl").tmpl(data).appendTo("#tllist");

                setScroll3();
            });

            return false;
        });

    });
}

function closeframe() {

    $("#produceprocessinfo").hide(300, function () {
        $("#produceprocessinfo").remove();
    });
}