<%--    
    File:DeskTop.aspx
    author:timi
    date:2015-7-27
--%>

<%@ Page Language="C#" MasterPageFile="~/T_TEMPLET/Master/NestedSite.Master" AutoEventWireup="true" CodeBehind="DeskTop.aspx.cs" Inherits="TIM.T_INDEX.DeskTop" %>

<%@ MasterType VirtualPath="~/T_TEMPLET/Master/NestedSite.Master" %>
<asp:Content runat="server" ID="IndexHead" ContentPlaceHolderID="NestedHead">
    <link rel="stylesheet" type="text/css" href="DeskTop.css" />

    <script src="../jQuery.1.9.1/Content/Scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/ligerUI/core/base.js" type="text/javascript"></script>
    <script src="../Scripts/ligerUI/plugins/ligerDrag.js" type="text/javascript"></script>
    <script src="../Scripts/ligerUI/plugins/ligerDialog.js" type="text/javascript"></script>
    <script src="../Scripts/ligerUI/plugins/ligerResizable.js" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="DeskTopBody" ContentPlaceHolderID="NestedBody" runat="server">
    <asp:updatepanel runat="server" id="UpNested" updatemode="Conditional" childrenastriggers="true" rendermode="Block">
        <ContentTemplate>
            <div id="bgImg">
                <img src="../Images/desktop/bg.jpg"></div>
            <div id="winlinks">
                <ul></ul>
            </div>
        </ContentTemplate>
    </asp:updatepanel>

    <script type="text/javascript">
        var LINKWIDTH = 90, LINKHEIGHT = 90, TASKBARHEIGHT = 43;
        var winlinksul = $("#winlinks ul");
        function f_open(url, title, icon, iHeight, iWidth, WinState)
        {
            var win = $.ligerDialog.open(
            {
                height: iHeight, url: url, width: iWidth, showMax: true, showToggle: false, showMin: true, isResize: true, modal: false, title: title, slide: false,
                data: {
                    callback: OpenCall
                }
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
        var links = [<%=objIcons%>];

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
                    OpenTaskForm(link);
                });
                jlink.appendTo(winlinksul);
            }
        }

        function OpenTaskForm(link)
        {
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
        }
        function OpenCall(link)
        {
            var win = f_open(link.url, link.title, link.icon, link.height, link.width, link.wsstate);
            link.showDialogId = win.id;
        }

        function OpenPage(owner, _url, _width, _height, _title, callback, _allowClose)
        {
            // 调整路径地址
            if (_url.indexOf('/') == -1)
            {
                // 表示在操作窗口的所在目录下打开地址
                var pathname = owner.location.pathname;
                var pathLocation = pathname.substring(0, pathname.lastIndexOf('/') + 1);
                _url = pathLocation + _url;
            }
            else
            {
                // 打开路径与操作窗口不在同一个目录下，则需给出相对于T_INDEX目录的地址
            }

            if (_width == '0')
            {
                _width = $(window).width();
            }
            if (_height == '0')
            {
                _height = $(window).height();
            }
            var diagLeft = ($(window).width() - _width + 10) / 2;
            var diagTop = ($(window).height() - _height + 36) / 2;
            var m = $.ligerDialog.open({
                opener: owner,
                title: '',
                left: diagLeft,
                top: diagTop,
                allowClose: _allowClose,
                isDrag: true,
                show: false,
                load: false,
                url: _url,
                height: _height,
                width: _width,
                timeParmName: 'TK',
                modal: true,
                isHidden: false,
                callbackFunc: callback,
                callbackArgument: [],
                returnValue: '',
                onLoaded: function ()
                {
                    // this.jiframe[0].contentWindow.document.head.attributes['0'].value
                    if (_title == undefined || _title == '')
                        this._setTitle(this.jiframe[0].contentWindow.document.title);
                    else
                        this._setTitle(_title);
                    try
                    {
                        var dialogWidth;
                        var dialogHeight;
                        try
                        {
                            dialogWidth = $(this.jiframe[0].contentWindow.document.head).attr('DialogWidth');
                            dialogHeight = $(this.jiframe[0].contentWindow.document.head).attr('DialogHeight');
                        }
                        catch (dialogError)
                        {
                        }
                        diagLeft = ($(window).width() - dialogWidth) / 2;
                        diagTop = ($(window).height() - dialogHeight) / 2;

                        if (dialogWidth != '0' && dialogHeight != '0' && _width != dialogWidth && _height != dialogHeight)
                        {
                            this._setLeft(diagLeft);
                            this._setTop(diagTop);
                            this._setWidth(dialogWidth);
                            this._setHeight(dialogHeight);
                        }
                        this.jiframe[0].contentWindow.document.body.focus();
                    } catch (err)
                    {
                    }
                },
                onClose: function ()
                {
                },
                onClosed: function ()
                {
                    if (this.options.callbackFunc)
                        this.options.callbackFunc.apply(this, this.options.callbackArgument);
                    else
                        if ($('#SiteBody_NestedBody_btnSlaveUpdate', this.options.opener.document) != null)
                            $('#SiteBody_NestedBody_btnSlaveUpdate', this.options.opener.document).click();
                    if (this.options.opener && this.options.opener.document.body)
                    {
                        this.options.opener.document.body.focus();
                    }
                }
            });
        }

        $(window).resize(onResize);
        $.ligerui.win.removeTaskbar = function () { }; //不允许移除
        $.ligerui.win.createTaskbar(); //页面加载时创建任务栏

        linksInit();
        onResize();
        onResize();

    </script>
</asp:Content>
