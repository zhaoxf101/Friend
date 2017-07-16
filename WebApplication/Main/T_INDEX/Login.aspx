<%@ Page Title="" Language="C#" MasterPageFile="~/T_TEMPLET/Master/NestedSite.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TIM.T_INDEX.Login" %>

<%@ MasterType VirtualPath="~/T_TEMPLET/Master/NestedSite.Master" %>

<asp:Content ID="LoginHead" ContentPlaceHolderID="NestedHead" runat="server">
    <link rel="stylesheet" type="text/css" href="css/Log.css" />
</asp:Content>

<asp:Content ID="LoginBody" ContentPlaceHolderID="NestedBody" runat="server">
    <asp:updatepanel runat="server" id="UpNested" updatemode="Conditional" childrenastriggers="true" rendermode="Block">
        <ContentTemplate>
            <div class="LoginPage">
	            <div class="MainCont">
		            <div class="loginbar">
                        <ul>
                            <li>
                             <label>
                                 <img src="images/login_icon1.png" />用户名
                             </label>                                        
                            <Tim:TimTextBox runat="server" ID="UserName" Height="33px" CharCase="Upper" Width="300"/>    
                             </li>                                                    
                            <li>
                                <label>
                                    <img src="images/login_icon1.png" />密　码
                                </label>
                                <Tim:TimTextBox runat="server" ID="Password" Height="33px" TextMode="Password" Width="300" />
                            </li>
                            <li>
                                <Tim:TimButton CssClass="btn" runat="server" ID="btnLogin" Text="登  录" OnClientClick="setCookie('UserId',document.all.SiteBody_NestedBody_UserName.value);" OnClick="btnLogin_Click">
                                </Tim:TimButton>
                            </li>
                            <li>
                                <p class="validation-summary-errors">
                            <Tim:TimLiteral ID="litMessage" runat="server"></Tim:TimLiteral>
                        </p>
                            </li>
                        </ul>		            
		            </div>                    
	            </div>
            </div>
            <%--<div class="login">
                <div class="bg_img"></div>
                <div class="conter">
                    <div class="Center">
                        <dl>
                            <Tim:TimLabel runat="server" ID="lblUserName" Text ="用户名:"></Tim:TimLabel>
                            <Tim:TimTextBox runat="server" ID="UserName" Height="33px" CharCase="Upper" Width="300"/>                                                        
                        </dl>
                        <dl>
                            <Tim:TimLabel runat="server" ID="lblPassword" Text ="密　码:"></Tim:TimLabel>
                            <Tim:TimTextBox runat="server" ID="Password" Height="33px" TextMode="Password" Width="300" />
                        </dl>
                        <dd>
                            <Tim:TimButton runat="server" ID="btnLogin" Text="" OnClientClick="setCookie('UserId',document.all.SiteBody_NestedBody_UserName.value);" OnClick="btnLogin_Click"></Tim:TimButton>                      
                        </dd>
                        <dl>
                            <Tim:TimLabel runat="server" ID="TimLabel2" Text ="首次使用请点击" style="font-size:18pt;"></Tim:TimLabel><a href="Tips/Tips.aspx" target="_blank" style="font-size:18pt;color:#ffffff">这里</a>
                        </dl>
                        <p class="validation-summary-errors">
                            <Tim:TimLiteral ID="litMessage" runat="server"></Tim:TimLiteral>
                        </p>
                        <a>&nbsp</a>
                        <dl>
                            <Tim:TimLabel runat="server" ID="TimLabel1" Text ="技术支持 版权所有 南京提米信息技术有限公司" style="font-size:18pt;"></Tim:TimLabel>
                        </dl>
                    </div>
                </div>
            </div>--%>
        </ContentTemplate>
    </asp:updatepanel>
    <iframe id="ifrOcx" width="0" height="0"></iframe>

    <script type="text/javascript">

        function check_a(item)
        {
            if (!$(item).hasClass("on"))
            {
                $(item).addClass("on");
            }
            else
            {
                $(item).removeClass("on");
            }
        }

        $(window).load(function ()
        {
            if (top.document.parentWindow.length > 2)
            {
                window.open('../T_INDEX/Login.aspx', '_parent');
                return;
            }
            else
            {

            }
        });

        function setCookie(name, value, expires, path, domain, secure)
        {
            var f = new Date();
            f.setYear(2999);
            document.cookie = name + '=' + escape(value) +
                             ((f) ? "; expires=" + f.toGMTString() : "") +
                             ((path) ? "; path=" + path : "") +
                             ((domain) ? "; domain=" + domain : "") +
                             ((secure) ? "; secure=" : "");
        }

        function getCookie(name)
        {
            var labelLen = name.length;
            var cookieData = document.cookie;
            var cLen = cookieData.length;
            var i = 0;
            var cEnd = null;
            while (i < cLen)
            {
                var j = i + labelLen;
                if (cookieData.substring(i, j) == name)
                {
                    cEnd = cookieData.indexOf(";", j);
                    if (cEnd == -1)
                        cEnd = cookieData.length;
                    return unescape(cookieData.substring(j + 1, cEnd));
                }
                i++;
            }
            return "";
        }

        function Enter2Tab(src, evt)
        {
            var evt = event ? event : window.event;
            var nextSrc;
            if (src.AutoPostBack != 'True')
            {
                if (evt.keyCode == 13)
                {
                    evt.keyCode = 9;
                    nextSrc = getNextElement(src);
                    if (nextSrc != null)
                    {
                        nextSrc.focus();
                        evt.preventDefault();
                    }
                }
            }
        }

        function getNextElement(field)
        {
            var form = field.form;
            if (form == null || form == "undefined")
                return null;
            for (var e = 0; e < form.elements.length; e++)
            {
                if (field == form.elements[e])
                {
                    break;
                }
            }
            return form.elements[++e % form.elements.length];
        }

        function pageLoad()
        {
            var userId = getCookie('UserId');
            if (userId)
            {
                $('#SiteBody_NestedBody_UserName').attr('value', userId);
                $('#SiteBody_NestedBody_Password').focus();
            } else
                document.all.SiteBody_NestedBody_UserName.focus();
            window.status = "南京提米信息技术有限公司(C) 版权所有";
        }

        function loadOcx()
        {
            $('#ifrOcx').attr('src', 'Tips/ActiveXHtm.htm');
        }

        window.setTimeout('loadOcx()', 100);
    </script>
</asp:Content>
