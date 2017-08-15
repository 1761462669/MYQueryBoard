var app = ({

    colorsIndex:0,
    //颜色
    colors: ["#1bbc9b", "#58d68d", "#f1c40f", "#e67f22", "#3598db", "#9a59b5", "#e84c3d", "#91d101"],

    //数据请求 url:请求地址, data: 参数数据, succeed:调用成功后的回调方法,fail：调用失败后的回调方法,async:是否异步调用
    DataRequest: function (url, data, succeed, fail, async)
    {
        //debugger;
        if (async)
            async = true;
        $.ajax({
            url: url,
            type: 'POST',
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            error: function (xhr) {
                debugger;
                if (fail && "function" == typeof fail)
                    fail(xhr);
                else                    
                    app.alert("系统提示","请求服务器错误，请检查网络连接。")
            },
            success: function (result) {
                //debugger;
                if (result && result.IsSucceed) {
                    if (succeed && "function" == typeof succeed)
                        succeed(result.Data);
                }
                else {
                    if (fail && "function" == typeof fail)
                        fail(result);
                    else
                        app.alert("系统提示",result.Error);
                }               
            },
            async: async,
            processData: false
        });
    },

    GetColor: function ()
    {
        if (app.colorsIndex >= app.colors.length - 1)
            app.colorsIndex = 0;

        var color = app.colors[app.colorsIndex];

        app.colorsIndex++;

        return color;

    },

    //获取url参数
    QueryPara: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);   //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    },

    //日期格式化
    DateFormat: function (date,fmtstr)
    {
        var date = new Date(date);

        return date.Format(fmtstr);
    },

    //创建滚动条 
    CreateScroll: function (elment, height, wrapperclass)
    {
        elment.slimScroll({
            height: height,
            alwaysVisible: false,
            color: "#000",
            railColor: "#c0c0c1",
            wrapperClass: wrapperclass,
            railOpacity: 1,
            railVisible: true,
            disableFadeOut: true
        });
    },
    
    //确认框
    confirm: function (title, msg, confirm)
    {
        $.confirm({
            title: title,
            content: msg,
            confirmButton: '确定',
            confirmButtonClass: 'btn btn-primary',
            cancelButton: '取消',
            cancelButtonCclass: "btn btn-default",
            icon: 'fa fa-info',
            animation: 'scale',
            closeAnimation: 'scale',
            confirm: confirm
        });
    },

    //弹出框
    alert: function (title, msg, confirm)
    {
        $.alert({
            title: title,
            content: msg,
            confirmButton: '确定',
            confirmButtonClass: 'btn btn-primary',
            animation: 'scale',
            closeAnimation: 'scale',
            confirm: confirm
        });
    },

    //平层结构转层级结构 listdata:列表数据  parentfilter:父节点过滤器 function(item) childfilter:子节点过滤器 function(item,parentitem)
    ListToHierarchy: function (listdata, parentfilter, childfilter)
    {
        var parentlist = listdata.filter(parentfilter); //获取所有父节点
        var getChilds = function (item) {
            var childs = listdata.filter(function (item2) {
                return childfilter(item2, item);
            });
            for (var i = 0; i < childs.length; i++) {
                childs[i].Childs = getChilds(childs[i]);
            }
            return childs;
        }

        for (var i = 0; i < parentlist.length; i++) {
            parentlist[i].Childs = getChilds(parentlist[i]);
        }

        return parentlist;
    }

});

//分页控件
$.fn.Paginator = function (totalnumber, pageindex, pagechange)
{
    $(this).jqPaginator({
        totalPages: totalnumber,
        visiblePages: 10,
        currentPage: pageindex,
        first: '<li class="first"><a href="javascript:void(0);">首页<\/a><\/li>',
        prev: '<li class="prev"><a href="javascript:void(0);"><i class="arrow arrow2"><\/i>上一页<\/a><\/li>',
        next: '<li class="next"><a href="javascript:void(0);">下一页<i class="arrow arrow3"><\/i><\/a><\/li>',
        last: '<li class="last"><a href="javascript:void(0);">末页<\/a><\/li>',
        page: '<li class="page"><a href="javascript:void(0);">{{page}}<\/a><\/li>',
        onPageChange: pagechange
    });
}

//树形菜单
$.fn.smestree = function (options) {
    var defoptions = {
        treeclass: "tree", //树样式
        subtreeclass: "sub-tree", //子树样式
        listclass: "tree-list-style",//包含子节点的节点样式
        treeopenclass: "tree-open",//节点展开
        selectclass: "tree-select",//节点选中时样式
        selectedevent: null,//选中终节点样式 funtion select(item){}
        isopen: false,//是否默认打开
        container: null,//树容器，在允许生产滚动条时必须
        scrollbar: false,//是否生产滚动条
        createdata: false,//是否自动创建数据数据
        valuemember: "Id",//值字段名
        displaymember: "Name",//显示字段名称
        childmember: "Childs",//子节点字段名
        openone:true //是否只打开一个节点
    };    

    var tree = $(this);
    var arg = $.extend({}, defoptions, options);

    var createtree = function (treedata) {
        var subtree = function (treedata) {
            var treeul = $('<ul></ul>')
            if (!treedata || treedata.length == 0)
                return treeul.children();

            for (var i = 0; i < treedata.length; i++) {
                var li = $('<li data-value=' + treedata[i][arg.valuemember] + '><a href="javascript:;">' + treedata[i][arg.displaymember] + '</a></li>');
                li.appendTo(treeul);
                li.data("item", treedata[i]);

                if (treedata[i][arg.childmember] && treedata[i][arg.childmember].length > 0) {
                    li.append($('<ul></ul>').append(subtree(treedata[i][arg.childmember])));
                }
            }

            return treeul.children();
        }
        var lilist = subtree(arg.data);
        tree.empty();
        lilist.appendTo(tree);
    }

    if (arg.createdata && arg.data)
    {
        createtree(arg.data);
    }

    tree.addClass(arg.treeclass);

    tree.find("ul").addClass(arg.subtreeclass);

    tree.find("li").each(function (index, element) {
        //debugger;
        if ($(element).children("ul").length > 0) {
            $(element).children("a").prepend("<i class='" + arg.listclass + "'></i>");
            if (arg.isopen) {
                $(element).addClass(arg.treeopenclass);
                $(element).children("ul").show();
            }
        }

    });

    tree.find("a").click(function () {
        var sub = $(this).next();
        var li = $(this).parent("li");
        if (sub.length > 0) {

            if (li.hasClass(arg.treeopenclass)) {
                li.removeClass(arg.treeopenclass);
                sub.slideUp(200);
            }
            else {
                if (arg.openone)
                {
                    //debugger;
                    var openli = li.parent().children("."+arg.treeopenclass);
                    openli.removeClass(arg.treeopenclass);
                    openli.children("ul").slideUp(200);
                }
                
                li.addClass(arg.treeopenclass);
                sub.slideDown(200);

            }

            return false;
        }
        else {
            if (li.hasClass(arg.selectclass))
                return false;
            else {
                tree.find("." + arg.selectclass).removeClass(arg.selectclass);
                li.addClass(arg.selectclass);
                if (arg.selectedevent && "function" == typeof arg.selectedevent) {
                    arg.selectedevent(li,li.data("item"),li.data("value"));
                }
            }
        }

        return;

    })


    if (arg.scrollbar) {
        var treeresize = function () {
            app.CreateScroll(tree, arg.container.height());
        }
        treeresize();
        $(window).on("resize", treeresize);       
    }


}
