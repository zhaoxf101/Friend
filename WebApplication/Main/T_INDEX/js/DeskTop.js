/*
    File:DeskTop.js
    author:timi
    date:2015-7-27
*/

var LINKWIDTH = 90, LINKHEIGHT = 90, TASKBARHEIGHT = 43;
var winlinksul = $("#winlinks ul");
function f_open(url, title, icon, iHeight, iWidth, WinState)
{
    var win = $.ligerDialog.open(
    {
        height: iHeight, url: url, width: iWidth, showMax: true, showToggle: true, showMin: true, isResize: true, modal: false, title: title, slide: false
    });
    var task = jQuery.ligerui.win.tasks[win.id];
    if (task)
    {
        $(".l-taskbar-task-icon:first", task).html('<img src="' + icon + '" />');
    }
    task.click();
    if (WinState == 'WSMAX')
        win.max();
    return win;
}

function onResize()
{
    var linksHeight = $(window).height() - TASKBARHEIGHT;
    var winlinks = $("#winlinks");
    winlinks.height(linksHeight);

    var bgimg = $("#bgImg img");
    bgimg.height($(window).height() - TASKBARHEIGHT + 8);
    bgimg.width($(window).width());

    var colMaxNumber = parseInt(linksHeight / LINKHEIGHT);//一列最多显示几个快捷方式
    for (var i = 0, l = links.length; i < l; i++)
    {
        var link = links[i];
        var jlink = $("li[linkindex=" + i + "]", winlinks);
        var top = (i % colMaxNumber) * LINKHEIGHT, left = parseInt(i / colMaxNumber) * LINKWIDTH;
        if (isNaN(top) || isNaN(left)) continue;
        jlink.css({ top: top, left: left });
    }

}
function linksInit()
{
    for (var i = 0, l = links.length; i < l; i++)
    {
        var link = links[i];
        var jlink;
        var jlink = $("<li></li>");
        jlink.attr("linkindex", i);
        jlink.append("<img src='" + link.icon + "' />");
        jlink.append("<span>" + link.title + "</span>");
        jlink.append("<div class='bg'></div>");
        jlink.hover(function ()
        {
            $(this).addClass("l-over");
        }, function ()
        {
            $(this).removeClass("l-over");
        }).click(function ()
        {
            var linkindex = $(this).attr("linkindex");
            var link = links[linkindex];
            var task = jQuery.ligerui.win.tasks[link.showDialogId];
            if (task)
            {
                var wins = liger.find(liger.core.Win);
                for (var i in wins)
                {
                    var w = wins[i];
                    if (w.id == link.showDialogId)
                    {
                        jQuery.ligerui.win.setFront(w);
                        w.max();
                        w.show();
                        return;
                    }
                }
            }
            else
            {
                var win = f_open(link.url, link.title, link.icon, link.height, link.width, link.wsstate);
                link.showDialogId = win.id;
            }
        });
        jlink.appendTo(winlinksul);
    }
}

function FormInit()
{
    var menuBg;
    menuBg = $.ligerMenu({
        top: 100, left: 100, width: 120, items:
                [
                { text: '增加', click: onclick11, icon: 'add' },
                { text: '修改', click: onclick11, disable: true },
                { line: true },
                { text: '查看', click: onclick11 },
                { text: '关闭', click: onclick112 }
                ]
    });

    $("#bgImg").bind("contextmenu", function (e)
    {
        menuBg.show({ top: e.pageY, left: e.pageX });
        return false;
    });
}

$(window).resize(onResize);
$.ligerui.win.removeTaskbar = function () { }; //不允许移除
$.ligerui.win.createTaskbar(); //页面加载时创建任务栏

linksInit();
onResize();