/*
    File:WorkSpace.js
    author:timi
    date:2015-7-27
*/
var navTab = null;
var accordion = null;
var tree = null;

$(function ()
{    
    $("#indexLayout").ligerLayout({ isLeftCollapse: false, leftWidth: 190, height: '100%', width: "100%", heightDiff: -2, space: 4, topHeight: 78, onHeightChanged: f_heightChanged });
    //$("#Content").ligerLayout({ isLeftCollapse: false,leftWidth:0,  height: '100%', heightDiff: -2, space: 4, onHeightChanged: f_heightChanged });
    //$("#indexLayout").ligerLayout({
    //    //width: "100%",        
    //    leftWidth: 0,
    //    height: "100%",
    //    allowTopResize: false,
    //    topHeight: 78,
    //    onHeightChanged: f_heightChanged
    //});
    var wh = $(window).height();
    var height = $(".l-layout-center").height();
    if (height == null)
    {
        height = wh - 78;
    }

    $("#framecenter").ligerTab({ height: height, onAfterSelectTabItem: function (tabid) { if (tabid == "T102030001") $("#framecenter").ligerGetTabManager().reload(tabid); } });

    // 左侧面板，功能菜单
    //$("#accordion1").ligerAccordion({ height: height, speed: null });

    $(".l-link").hover(function ()
    {
        $(this).addClass("l-link-over");
    }, function ()
    {
        $(this).removeClass("l-link-over");
    });

    navTab = $("#framecenter").ligerGetTabManager();
    //  accordion = $("#accordion1").ligerGetAccordionManager();
    //    tree = $("#appModelTree").ligerGetTreeManager();

    f_addTab("T101080001", "代办事务", "../WIDGET/T_AFFAIR/TodoList.aspx?AMID=101080001");


    $("#pageloading").hide();

});

function f_heightChanged(options)
{
    if (navTab)
        navTab.addHeight(options.diff);
    //if (accordion && options.middleHeight - 24 > 0)
    //    accordion.setHeight(options.middleHeight - 24);
}
function f_addTab(tabid, text, url)
{
    navTab.addTabItem({ tabid: tabid, text: text, url: url });
    $(window).resize();
    //if (tabid == 'T102030001')
    //    $("li.l-selected", navTab.tab.links.ul).click(function () { navTab.reload(tabid); });
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