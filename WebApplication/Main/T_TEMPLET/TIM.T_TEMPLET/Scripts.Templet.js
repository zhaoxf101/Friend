
var postbackElement;

if (Sys.Application.get_events().getHandler("load") === null)
    Sys.Application.add_load(ApplicationLoadHandler);
function ApplicationLoadHandler(sender, args)
{
    if (!args.get_isPartialLoad())
    {
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(InitializeRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    }
}
function InitializeRequestHandler(sender, args)
{
    postbackElement = args.get_postBackElement();
    $('#sitepageloading').show();
    if (Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack())
    {
        args.set_cancel(true);
    }
}
function EndRequestHandler(sender, args)
{
    if (args.get_error() != undefined)
    {
        var errorMessage = args.get_error().message;
        if (args.get_response().get_timedOut())
        {
            if (postbackElement != null)
                alert("服务请求超时，请缩小数据范围或(关闭窗口)稍候重试！");
        }
        else if (args.get_response().get_statusCode() == '200')
        {
            if (args.get_error().name == 'Sys.WebForms.PageRequestManagerServerErrorException')
            {
                alert("服务处理产生异常，异常信息 ：\r" + errorMessage.replace('Sys.WebForms.PageRequestManagerServerErrorException: ', ''));
            }
            else if (args.get_error().name == 'Sys.WebForms.PageRequestManagerTimeoutException')
                alert("服务请求超时，请(关闭窗口)稍候重试！");
            else if (args.get_error().name == 'Sys.WebForms.PageRequestManagerParserErrorException')
            {
                var allPageInfo = args.get_response().get_responseData();
                if (allPageInfo.indexOf("<b>错误信息：</b>") >= 0)
                {
                    allPageInfo = allPageInfo.substring(allPageInfo.indexOf("<b>错误信息：</b>"), allPageInfo.length - 1);
                    allPageInfo = allPageInfo.substring(0, allPageInfo.indexOf("</div>"));
                }
                alert("页面更新失败，请(关闭窗口)稍候重试！" + "  ParserError信息：" + allPageInfo);
            }
            else if (args.get_error().name.length > 0)
                alert("页面更新失败，请(关闭窗口)稍候重试！" + args.get_error().name);
            else
                alert("页面更新失败，请(关闭窗口)稍候重试！" + "  诊断信息：" + errorMessage);
        }
        else if (args.get_response().get_statusCode() == '503')
        {
            alert("IIS应用服务器繁忙或系统正在重新启动，请(关闭窗口)稍候重试！");
        }
        else
        {
            alert("可能发生网络错误或应用服务器已经停止，请稍候重试！(" + args.get_response().get_statusCode() + ")");
        }
        args.set_errorHandled(true);
    }
    else if (args.get_dataItems()["__Page"] != null && args.get_dataItems()["__Page"] != "undefined")
    {
        alert(args.get_dataItems()["__Page"]);
    }
    $('#sitepageloading').hide();
}

var _PageParam;
var _PageCanMax;//给双击打开明细页使用
function SetPageUrlParam(_row, _param, _CanMax)
{
    _PageParam = _param;
    _PageCanMax = _CanMax;
}

function OpenPage(_state, _url, _width, _height, _title, callback, _allowClose, _allowMax)
{
    if (_state == 'undefined' || _state == '')
        _state = "VIEW";

    // 针对模板按钮及列表的双击可不指定url地址
    if (_url == undefined || _url == '')
    {
        _url = _EditingPage;
        // _PageUrlAppendParam和_EditingPage中如果都包含?则以_PageUrlAppendParam中为准进行字符串的拼接
        if (_state == 'INSERT')
        {
            _url = _PageUrlAppendParam.indexOf('?') > -1 ? _PageUrlAppendParam : _url + "?" + _PageUrlAppendParam;
            _url += _url.indexOf('?') == -1 ? "?" : "&";
            _url += "SK=" + _state;
        }
        else
        {
            if (_PageParam == undefined || _PageParam == '') return;
            if (_url == undefined || _url == '')
            {
                _url = _PageParam;
                _url += "&SK=" + _state;
            }
            else
            {
                _url += _url.indexOf('?') == -1 ? "?" : "&";
                _url += "SK=" + _state;
                _url += "&" + _PageParam;
            }
        }
    }
    if (_width == undefined || _height == undefined || _width === '' || _height === '')
    {
        //_height = $(window).height() - 100;   // + 36;
        //_width = $(window).width() - 100;     // + 10;
        _height = 600;
        _width = 900;
    }
    var showMaxBtn = false;
    showMaxBtn = (_PageCanMax == true) || (_allowMax == true);
    //OpenDialogPage(this, _url, _width, _height, _title, callback, _allowClose, _allowMax); return;
    top.OpenPage(this, _url, _width, _height, _title, callback, _allowClose, showMaxBtn); return;


    // 调整路径地址
    if (_url.indexOf('/') == -1)
    {
        // 表示在操作窗口的所在目录下打开地址
        var pathname = this.location.pathname;
        var pathLocation = pathname.substring(0, pathname.lastIndexOf('/') + 1);
        _url = pathLocation + _url;
    }
    else
    {
        // 打开路径与操作窗口不在同一个目录下，则需给出相对于U_HOME目录的地址
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
        opener: this,
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

                    //dialogWidth = this.jiframe[0].contentWindow.document.head.attributes['DialogWidth'].value;
                    //dialogHeight = this.jiframe[0].contentWindow.document.head.attributes['DialogHeight'].value;
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
            else if ($('#SiteBody_NestedBody_btnSlaveUpdate', this.options.opener.document) != null)
                $('#SiteBody_NestedBody_btnSlaveUpdate', this.options.opener.document).click();
            //if (this.options.opener){
            //    this.options.opener.document.body.focus();
            //}
        },
    });

    //var diagLeft = ($(window).width() - _width) / 2;
    //var diagTop = ($(window).height() - _height) / 2;

    //var m = $.ligerDialog.open({
    //    opener: this, title: '', left: diagLeft, top: diagTop, isDrag: true, show: false, load: false, url: _url, height: _height, width: _width, timeParmName: 'TK', modal: true, isDrag: false, isHidden: false,
    //    onLoaded: function () {
    //        // this.jiframe[0].contentWindow.document.head.attributes['0'].value
    //        if (_title == undefined || _title == '')
    //            this._setTitle(this.jiframe[0].contentWindow.document.title);
    //        else
    //            this._setTitle(_title);
    //        try {
    //            var dialogWidth = this.jiframe[0].contentWindow.document.head.attributes['DialogWidth'].value;
    //            var dialogHeight = this.jiframe[0].contentWindow.document.head.attributes['DialogHeight'].value;
    //            diagLeft = diagLeft = ($(window).width() - dialogWidth) / 2;
    //            diagTop = ($(window).height() - dialogHeight) / 2;

    //            if (dialogWidth != '0' && dialogHeight != '0' && _width != dialogWidth && _height != dialogHeight) {
    //                this._setLeft(diagLeft);
    //                this._setTop(diagTop);
    //                this._setWidth(dialogWidth);
    //                this._setHeight(dialogHeight);
    //            }
    //        } catch (err)
    //        { }
    //    },
    //    onClose: function () {
    //    },
    //    onClosed: function () {
    //        //if (owner.document.all.ctl00$ctl00$ctl00$SiteBody$NestedBody$GridPagingBar != 'undefined') {
    //        //    owner.document.all.ctl00$ctl00$ctl00$SiteBody$NestedBody$GridPagingBar.onclick();
    //        //}
    //    }
    //});


}

function CallBtnClientClick(btnClientId)
{
    var o = document.getElementById(btnClientId);
    if (o)
    {
        o.click();
    }
    if (o) o = null;
    return;
}

function DlgClosedUpdateSlave()
{ }

function AddDialogCallbackArgumentValue(argument)
{
    frameElement.dialog.options.callbackArgument.push(argument);
}

function scclf(id)
{
    try
    {
        if (document.getElementById('__LASTFOCUS') != null)
        {
            document.getElementById('__LASTFOCUS').value = id;
        }
    } catch (e) { }
}

$(function ()
{
    $(document).keydown(function (e)
    {
        if (!e) e = window.event;
        switch (e.keyCode)
        {
            case Sys.UI.Key.esc:
                var _dialog = frameElement.dialog;
                if (_dialog != null) _dialog.close();
                break;
            case Sys.UI.Key.backspace:
                var objTarget = e.srcElement ? e.srcElement : e.target;
                if ((objTarget.tagName == 'INPUT') || (objTarget.tagName == 'TEXTAREA'))
                {
                    if (objTarget.readOnly)
                        return false;
                }
                else
                {
                    return false;
                }
                break;
            default: break;
        }

        if (e.keyCode == 121 && document.all.SiteBody_NestedBody_btnSaveButtonTd != null)
        { document.all.SiteBody_NestedBody_btnSaveButtonTd.click(); document.body.focus(); }
        else if (e.keyCode == 13 && e.ctrlKey && document.all.SiteBody_NestedBody_btnEditButtonTd != null)
        { document.all.SiteBody_NestedBody_btnEditButtonTd.click(); }
        else if (e.keyCode == 120 && document.all.SiteBody_NestedBody_btnQueryButtonTd != null)
        { document.all.SiteBody_NestedBody_btnQueryButtonTd.click(); }
    });
});


String.prototype.toTimDate = function ()
{
    var time = new Date();
    var timeInt = Date.parse(this.replace('-', '/'));
    time.setTime(timeInt);
    return time;
}

Date.prototype.toUtoString = function ()
{
    var fmt = "yyyy-MM-dd HH:mm:ss";
    var o = {
        "M+": this.getMonth() + 1, //月份           
        "d+": this.getDate(), //日           
        "h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //小时           
        "H+": this.getHours(), //小时           
        "m+": this.getMinutes(), //分           
        "s+": this.getSeconds(), //秒           
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度           
        "S": this.getMilliseconds() //毫秒           
    };
    var week = {
        "0": "/u65e5",
        "1": "/u4e00",
        "2": "/u4e8c",
        "3": "/u4e09",
        "4": "/u56db",
        "5": "/u4e94",
        "6": "/u516d"
    };
    if (/(y+)/.test(fmt))
    {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(E+)/.test(fmt))
    {
        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "/u661f/u671f" : "/u5468") : "") + week[this.getDay() + ""]);
    }
    for (var k in o)
    {
        if (new RegExp("(" + k + ")").test(fmt))
        {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}

function showWorkflowInfo(tips)
{
    var _scrollWidth = Math.max(document.body.scrollWidth, document.documentElement.scrollWidth);
    var _scrollHeight = Math.max(document.body.scrollHeight, document.documentElement.scrollHeight);

    $('#workflowInfoBox').css({
        'top': '0px',
        'left': '0px',
        'position': 'absolute',
        'zIndex': 1,
        'background': '#E8E8E8',
        'font-size': '12px',
        'margin': '0 auto',
        'text-align': 'center',
        'width': _scrollWidth,
        'height': _scrollHeight,
        'filter': 'alpha(opacity=50)',
        'opacity': '0.50'
    }).html("<div align='center' style='margin-top: " + (_scrollHeight / 2 - 100) + "px;'><font size='4' color='#ff0000'><b>" + tips + "</b></font><div>").fadeIn(200).delay(3000).fadeOut(300);
}

if (typeof (Sys) !== "undefined") Sys.Application.notifyScriptLoaded();