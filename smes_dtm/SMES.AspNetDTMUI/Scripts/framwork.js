
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


(function ($) {

    //下拉框控件
    smesdropdown = function (element, options) {

        var defsetting = {
            dropdownclass: "smes-dropdown",//下拉框组件
            displaytextclass: "smes-dropdown-display", //显示文本样式
            caretdowniconclass: "icon-caret-down",//下拉图标样式
            itemspanelclass: "smes-dropdown-items",//下拉面板样式
            itemselectedclass: "smes-dropdown-selected",//元素选中样式
            itemspanelopenclass: "smes-dropdwon-open",//元素打开样式
            data: [],//数据
            displaymember: "Name", //显示字段名称
            valuemember: "Id",//值名称
            nulltext: "--请选择--",//为空时显示文本
            nullvalue: -1,//为空时的值
            adddefaultitem: true,//是否添加默认元素
            defaultselectedtop:true,//默认选中第一个元素
            itemspaneltop: 1,//面板顶部偏移
            selectchange:undefined
        }

        var dropdown = this;

        var arg = jQuery.extend({}, defsetting, options);

        element.addClass(arg.dropdownclass);

        var displaydiv = jQuery('<div></div>').addClass(arg.displaytextclass); //显示文本div
        var dropmark = jQuery('<i></i>').addClass(arg.caretdowniconclass); //下拉图标div
        var itemsdiv = jQuery('<div><ul></ul></div>').addClass(arg.itemspanelclass); //下拉选择项面板
        var itemsul = itemsdiv.children("ul");

        

        //绑定数据
        this.binddata = function (itemsdata) {
            itemsul.empty();            
            if (arg.adddefaultitem)
            {
                var defitem = {};

                defitem[arg.displaymember] = arg.nulltext;
                defitem[arg.valuemember] = arg.nullvalue;
                this.additem(defitem);
            }

            if (itemsdata && itemsdata.length > 0) {
                for (var i = 0; i < itemsdata.length; i++) {
                    this.additem(itemsdata[i]);
                }
            }

            //是否默认选中第一个
            if (arg.defaultselectedtop)
            {
                var tmp = itemsul.children().first();
                tmp.addClass(arg.itemselectedclass);
                
                var item = tmp.data("item");
                if (item)
                    displaydiv.html(item[arg.displaymember]);
            }
        }

        //添加元素
        this.additem = function (item)
        {
            var li = jQuery('<li data-value="' + item[arg.valuemember] + '">' + item[arg.displaymember] + '</li>');
            li.data("item", item);
            li.appendTo(itemsul);
        }

        //添加数据项
        this.binddata(arg.data);

        //添加子元素
        element.empty();
        element.off("mouseleave");
        element.off("click");

        displaydiv.appendTo(element);
        dropmark.appendTo(element);
        itemsdiv.appendTo(element);
        

        element.click(function (e) {
            if (element.hasClass(arg.itemspanelopenclass)) {
                dropdown.HideDropDown();
            }
            else {                
                dropdown.ShowDropDown();
            }
        });

        //关闭下拉面板
        this.HideDropDown = function () {
            element.removeClass(arg.itemspanelopenclass);
            itemsdiv.hide();
        };

        //显示下拉面板
        this.ShowDropDown = function () {

            itemsdiv.css("top", element.outerHeight() - arg.itemspaneltop);
            itemsdiv.css("min-width", element.outerWidth());

            element.addClass(arg.itemspanelopenclass);
            itemsdiv.show();
            dropdownresize();
        }


        //设置或获取选中的值
        this.SelectValue = function (value) {
            //debugger;

            //获取当前选中项
            var curselect = itemsul.find("." + arg.itemselectedclass);
            var curselectvalue = curselect.data("value");
            if (value == undefined) {
                if (curselectvalue == undefined)
                    return nullvalue;
                else
                    return curselectvalue;
            }

            var tmp=itemsul.children().first();
            var li = itemsul.find("[data-value='"+value+"']").first();
            if (li.length == 0)
                return value;

            //curselectvalue = value;
            curselect.removeClass(arg.itemselectedclass);
            li.addClass(arg.itemselectedclass);
            displaydiv.html(li.data("item")[arg.displaymember]);
            //debugger;
            if (arg.selectchange && arg.selectchange != null && "function" == typeof arg.selectchange)
            {
                arg.selectchange(value, li.data("item"));
            }

            return value;
        }

        //设置或获取选中的模型
        this.SelectItem = function (item) {
            //获取当前选中项
            var value = item[arg.valuemember];
            var curselect = itemsul.find("." + arg.itemselectedclass);
            var curselectvalue = curselect.data("value");
            var curselectitem = curselect.data("item");

            if (item == undefined) {
                if (curselectvalue == curselectitem)
                    return undefined;
                else
                    return curselectitem;
            }

            var li = itemsul.find('[data-value="' + value + '"]').first();
            if (li.length == 0)
                return item;

            //curselectvalue = value;
            curselect.removeClass(arg.itemselectedclass);
            li.addClass(arg.itemselectedclass);
            displaydiv.html(item[arg.displaymember]);

            if (arg.selectchange && arg.selectchange != null && "function"==typeof arg.selectchange) {
                arg.selectchange(value, item);
            }

            return item;
        }

        //下拉元素点击事件
        itemsdiv.find("li").click(function (event) {
            if (!jQuery(this).hasClass(arg.itemselectedclass)) {
                var value = jQuery(this).data("value");
                dropdown.SelectValue(value);
            }
            dropdown.HideDropDown();
            return false;
        });

        element.mouseleave(function () {
            //debugger;
            if (element.hasClass(arg.itemspanelopenclass)) {
                dropdown.HideDropDown();
            }
        });

        var dropdownresize = function () {
            app.CreateScroll(itemsul, itemsdiv.height());
        }
        
        element.data("smesdropdown", this);

        return this;
    };

    //下拉框控件
    jQuery.fn.smesdropdown = function (options)
    {        
        return new smesdropdown(jQuery(this), options);
    }

    //分页控件
    jQuery.fn.Paginator = function (totalnumber, pageindex, pagechange) {
        jQuery(this).jqPaginator({
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

    //树形控件
    smestree = function (element, options)
    {
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
            openone: true //是否只打开一个节点
        };

        var tree = element;
        var arg = jQuery.extend({}, defoptions, options);

        var createtree = function (treedata) {
            var subtree = function (treedata) {
                var treeul = jQuery('<ul></ul>')
                if (!treedata || treedata.length == 0)
                    return treeul.children();

                for (var i = 0; i < treedata.length; i++) {
                    var li = jQuery('<li data-value=' + treedata[i][arg.valuemember] + '><a href="javascript:;">' + treedata[i][arg.displaymember] + '</a></li>');
                    li.appendTo(treeul);
                    li.data("item", treedata[i]);

                    if (treedata[i][arg.childmember] && treedata[i][arg.childmember].length > 0) {
                        li.append(jQuery('<ul></ul>').append(subtree(treedata[i][arg.childmember])));
                    }
                }

                return treeul.children();
            }
            var lilist = subtree(arg.data);
            tree.empty();
            lilist.appendTo(tree);
        }

        if (arg.createdata && arg.data) {
            createtree(arg.data);
        }

        tree.addClass(arg.treeclass);

        tree.find("ul").addClass(arg.subtreeclass);

        tree.find("li").each(function (index, element) {
            //debugger;
            if (jQuery(element).children("ul").length > 0) {
                jQuery(element).children("a").prepend("<i class='" + arg.listclass + "'></i>");
                if (arg.isopen) {
                    jQuery(element).addClass(arg.treeopenclass);
                    jQuery(element).children("ul").show();
                }
            }

        });

        tree.find("a").click(function () {
            var sub = jQuery(this).next();
            var li = jQuery(this).parent("li");
            if (sub.length > 0) {

                if (li.hasClass(arg.treeopenclass)) {
                    li.removeClass(arg.treeopenclass);
                    sub.slideUp(200);
                }
                else {
                    if (arg.openone) {
                        //debugger;
                        var openli = li.parent().children("." + arg.treeopenclass);
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
                        arg.selectedevent(li, li.data("item"), li.data("value"));
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
            jQuery(window).on("resize", treeresize);
        }

        tree.data("smestree", this);

        return this;
    };
    //树形菜单
    jQuery.fn.smestree = function (options) {
        return new smestree(jQuery(this), options);
    };

    //列表框
    smeslistview = function (element, options) {
        var defarg = {
            viewclass: "smes-listview",
            selectedclass: "smes-listview-select",
            selectedchange: undefined,            
        }

        var arg = jQuery.extend({}, defarg, options);

        element.addClass(arg.viewclass);

        element.children().click(function () {
            if (jQuery(this).hasClass(arg.selectedclass))
                return;
            else
            {
                element.children("." + arg.selectedclass).removeClass(arg.selectedclass);
                jQuery(this).addClass(arg.selectedclass);
            }
        });

        element.data("listview",this);
        return this;

    };

    //列表框
    jQuery.fn.smeslistview = function (options) {
        return new smeslistview(jQuery(this), options);
    };

    smesdropdowntree = function (element, options) {
        var defarg = {
            dropdownclass: "smes-dropdowntree",//下拉框组件
            displaytextclass: "smes-dropdowntree-display", //显示文本样式
            caretdowniconclass: "icon-caret-down",//下拉图标样式
            itemspanelclass: "smes-dropdowntree-panels",//下拉面板样式
            itemspanelopenclass: "smes-dropdowntree-open",//元素打开样式            
            nulltext: "--请选择--",//为空时显示文本
            nullvalue: -1,//为空时的值
            itemspaneltop: 1,//面板顶部偏移
            selectedevent:null,
            tree: {
                treeclass: "tree", //树样式
                subtreeclass: "sub-tree", //子树样式
                listclass: "tree-list-style",//包含子节点的节点样式
                treeopenclass: "tree-open",//节点展开
                selectclass: "tree-select",//节点选中时样式                
                isopen: false,//是否默认打开
                container: null,//树容器，在允许生产滚动条时必须
                scrollbar: true,//是否生产滚动条
                createdata: false,//是否自动创建数据数据
                valuemember: "Id",//值字段名
                displaymember: "Name",//显示字段名称
                childmember: "Childs",//子节点字段名
                openone: true //是否只打开一个节点
            }
        }

        
        var dropdowntree = this;
        
        element.off("mouseleave");
        element.off("click");

        var arg = jQuery.extend({}, defarg, options);

        element.addClass(arg.dropdownclass);

        var displaydiv = jQuery('<div>' +arg.nulltext + '</div>').addClass(arg.displaytextclass); //显示文本div
        var dropmark = jQuery('<i></i>').addClass(arg.caretdowniconclass); //下拉图标div
        var itemsdiv = jQuery("." + arg.itemspanelclass) //下拉选择项面板
        var itemsul = itemsdiv.children("ul");

        displaydiv.appendTo(element);
        dropmark.appendTo(element);

        //关闭下拉面板
        this.HideDropDown = function () {
            element.removeClass(arg.itemspanelopenclass);
            itemsdiv.hide();
        };

        //显示下拉面板
        this.ShowDropDown = function () {
            itemsdiv.css("top", element.outerHeight() - arg.itemspaneltop);
            itemsdiv.css("min-width", element.outerWidth());

            element.addClass(arg.itemspanelopenclass);
            itemsdiv.show();            
        }


        element.click(function (e) {
            if (element.hasClass(arg.itemspanelopenclass)) {
                dropdowntree.HideDropDown();
            }
            else {
                dropdowntree.ShowDropDown();
            }
        });

        element.mouseleave(function () {
            if (element.hasClass(arg.itemspanelopenclass)) {
                dropdowntree.HideDropDown();
            }
        });

        element.data("dropdowntree", this);

        arg.tree.container = itemsdiv;
        arg.tree.selectedevent = function (treeli)
        {
           
            var displayvalue = treeli.children("a").html();
            displaydiv.html(displayvalue);

            if (arg.selectedevent != null && arg.selectedevent != undefined && "function" == typeof arg.selectedevent)
                arg.selectedevent(treeli);
        }
        this.tree = new smestree(itemsul, arg.tree);
        return this;
    };

    jQuery.fn.smesdropdowntree = function (options)
    {
        return new smesdropdowntree(jQuery(this),options);
    }

})(jQuery);

