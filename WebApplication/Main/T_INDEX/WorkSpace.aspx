<%--    
    File:WorkSpace.aspx
    author:timi
    date:2015-7-27
--%>

<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/T_TEMPLET/Master/NestedSite.Master" AutoEventWireup="true" CodeBehind="WorkSpace.aspx.cs" Inherits="TIM.T_INDEX.Index" %>

<%@ MasterType VirtualPath="~/T_TEMPLET/Master/NestedSite.Master" %>
<asp:Content runat="server" ID="IndexHead" ContentPlaceHolderID="NestedHead">
    <link href="css/WorkSpace.css" rel="stylesheet" />
    <link href="css/base.css" rel="stylesheet" />
    <script src="js/jquery-1.9.1.min.js"></script>
    <script src="js/base.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/WorkSpace.js"></script>
    <script type="text/javascript">
        function Logout_Click()
        {
            document.getElementById("SiteBody_NestedBody_btnLogOut").click();
        }
    </script>
</asp:Content>

<asp:Content runat="server" ID="IndexBody" ContentPlaceHolderID="NestedBody">
    <div id="pageloading"></div>
    <div id="Contain">
        <div class="Menu" id ="frameMenu">
            <div class="open_menu"><a href="javascript:void(0)"></a></div>
            <div class="logo">
                <img src="images/logo.png" />
            </div>
            <div class="MenuList">
                <ul>
                    <%=_Menu %>
                </ul>
            </div>
            <div class="bot_img">
                <img src="images/bot_img.png" />
                <%--<p>提米科技，技术支持</p>--%>
            </div>
        </div>
        <div class="Content" id="indexLayout">
            <div position="top" class="Top" id="frameTop">
                <div class="ConpanyName"><%=_ConpanyName %></div>
                <div class="loginbar">
                    <span>
                        <img src="images/top_per.png" width="17" height="21" /><%=_UserName%></span>|<a runat="server" onclick="Logout_Click()">退出</a>
                    <Tim:TimButton ID="btnLogOut" runat="server" Text="" Visible="true" OnClick="btnLogout_Click" />
                </div>
            </div>
            <div position="center" id="framecenter">
            </div>
        </div>
    </div>
</asp:Content>
