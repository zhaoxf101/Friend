<%@ Page Title="" Language="C#" MasterPageFile="~/T_TEMPLET/Master/TEditing.Master" AutoEventWireup="true" CodeBehind="StyleDesignInfo.aspx.cs" Inherits="TIM.T_TEMPLET.CommForm.StyleDesignInfo" %>

<%@ MasterType VirtualPath="~/T_TEMPLET/Master/TEditing.Master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="CPHHead" runat="server">
</asp:Content>

<asp:Content ID="ContentButton" ContentPlaceHolderID="CPHButton" runat="server">
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="CPHContent" runat="server">
    <div>
	  <%-- 高度=页面高度-35px --%>
        <%--<object classid="clsid:1B19543D-1C5D-40CE-9850-EB5115B1E9E7" width="100%" 
            id="UtoYSDY">
        </object>--%>
    </div>
    <Tim:TimHiddenField ID="hidStyleId" runat="server" />
    <Tim:TimHiddenField ID="hidStyleOrder" runat="server" />
    <script type="text/javascript">
        $(function () {
            $('#UtoYSDY').height($(window).height()-4);
            document.body.onhelp = function () { return false; };
            $(document).on("keydown", function (event) {
                if (event.keyCode == 27 || event.keyCode == 112)
                { event.stopPropagation = true; event.returnValue = false; event.keyCode = 0; }
                else if (event.ctrlKey && event.keyCode == 67)
                { event.stopPropagation = true; document.all.UtoYSDY.HotKeyDown(event.keyCode, event.ctrlKey); event.returnValue = false; event.keyCode = 0; }
                else if (event.ctrlKey && event.keyCode == 84)
                { event.stopPropagation = true; document.all.UtoYSDY.HotKeyDown(event.keyCode, event.ctrlKey); event.returnValue = false; event.keyCode = 0; }
                else if (event.ctrlKey && event.keyCode == 70)
                { event.stopPropagation = true; document.all.UtoYSDY.HotKeyDown(event.keyCode, event.ctrlKey); event.returnValue = false; event.keyCode = 0; }
                else if (event.ctrlKey && event.keyCode == 86)
                { event.stopPropagation = true; document.all.UtoYSDY.HotKeyDown(event.keyCode, event.ctrlKey); event.returnValue = false; event.keyCode = 0; }
                else if (event.ctrlKey && event.keyCode == 82)
                { event.stopPropagation = true; document.all.UtoYSDY.HotKeyDown(event.keyCode, event.ctrlKey); event.returnValue = false; event.keyCode = 0; }
                else if (event.ctrlKey && event.keyCode == 13)
                { event.stopPropagation = true; document.all.UtoYSDY.HotKeyDown(event.keyCode, event.ctrlKey); event.returnValue = false; event.keyCode = 0; }
                else if (event.ctrlKey && event.keyCode == 88)
                { event.stopPropagation = true; document.all.UtoYSDY.HotKeyDown(event.keyCode, event.ctrlKey); event.returnValue = false; event.keyCode = 0; }
            })
        });
    </script>

    <script language="JavaScript" type="text/javascript" for='UtoYSDY' event='OnSave'>

        function AddPostParam(source, name, value) {
            if (source.length > 0)
                source += '&';
            return source + encodeURIComponent(name) + '=' + encodeURIComponent(value);
        }

        function SaveBBYS(reportForm) {

            var postParams = '';
            postParams = AddPostParam(postParams, 'SAVEREPORT', 'Y');
            postParams = AddPostParam(postParams, 'REPORTFORM', reportForm);
            postParams = AddPostParam(postParams, 'STYLEID', $('#SiteBody_NestedBody_CPHContent_hidStyleId').attr('value'));
            postParams = AddPostParam(postParams, 'STYLEORDER', $('#SiteBody_NestedBody_CPHContent_hidStyleOrder').attr('value'));

            var webRequest = new Sys.Net.WebRequest();
            webRequest.set_url('StyleDesignInfo.aspx');
            webRequest.set_httpVerb('POST');
            webRequest.set_body(postParams);
            webRequest.add_completed(SaveReportCompleted);
            webRequest.invoke();
        }
        function SaveReportCompleted(executor, eventArgs) {
            if (executor.get_responseAvailable())
            { }
            else
            {
                if (executor.get_timedOut())
                    alert('服务器连接超时！');
                else if (executor.get_aborted())
                    alert('操作被服务器终止！');
            }
        }

        SaveBBYS(UtoYSDY.CompressUtoReport);
    </script>

    <script language="JavaScript" type="text/javascript" for='UtoYSDY' event='OnClose'>
        frameElement.dialog.close();
    </script>

</asp:Content>


