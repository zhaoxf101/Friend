<%@ Page Title="" Language="C#" MasterPageFile="~/T_TEMPLET/Master/TEditing.Master" AutoEventWireup="true" CodeBehind="WfHandle.aspx.cs" Inherits="TIM.T_TEMPLET.CommForm.WfHandle" %>

<%@ MasterType VirtualPath="~/T_TEMPLET/Master/TEditing.Master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="CPHHead" runat="server">
    <style type="text/css">
        #SiteBody_NestedBody_CPHContent_cblUsers {
            border-collapse: collapse;
            line-height: 18px;
            overflow: auto;
            font-family: '宋体', Simsun;
            white-space: pre;
            text-overflow: ellipsis;
            -o-text-overflow: ellipsis;
        }

            #SiteBody_NestedBody_CPHContent_cblUsers span {
                font-size: 12px;
            }

        .ItemBgColor {
            background-color: #dcf8a8;
        }

        .TableLayout table {
            border-spacing: 2px;
        }
    </style>
    <script type="text/javascript">
        function CColor(ele)
        {
            if (ele.checked)
            {
                ele.nextSibling.className = 'ItemBgColor';
                $('#<%=txtTodo.ClientID%>').val($('#<%=txtTodo.ClientID%>').val() + $(ele).val() + ",");
            }
            else
            {
                ele.nextSibling.className = '';
                $('#<%=txtTodo.ClientID%>').val($('#<%=txtTodo.ClientID%>').val().replace($(ele).val() + ',', ''));
            }
        }
        function GetCheckedUser()
        {
            var userList = "";
            var userListCount = 0;
            $("#<%=cblUsers.ClientID %> input:checkbox").each(function ()
            {
                if (this.checked == true)
                {
                    userList += $(this).parent("span").attr("alt") + ",";
                    userListCount++;
                }
            });

            $('#<%=hidTodo.ClientID%>').val(userList);

            if ($('#<%=hidRequiredTodo.ClientID%>').val() == "Y")
            {
                if ($('#<%=hidTodo.ClientID%>').val().trim() == "")
                {
                    alert('待处理人不满足条件！');
                    return false;
                }
                else if (userListCount < $('#<%=hidRequiredTodoUsers.ClientID%>').val())
                {
                    alert('待处理人数不满足条件！');
                    return false;
                }
            }

            if ($('#<%=hidRequiredOpinion.ClientID%>').val() == "Y" && $('#<%=txtOpinion.ClientID%>').val().trim() == "")
            {
                alert('处理意见不允许为空！');
                return false;
            }

            if ($('#<%=txtOpinion.ClientID%>').val().length > 200)
            {
                alert('处理意见超长，最大允许长度200字符！');
                return false;
            }

            $('#SiteTemplet_Templet_Action', frameElement.dialog.options.opener.document).val($('#<%=hidWfpAction.ClientID%>').val());
            $('#SiteTemplet_Templet_NextWfId', frameElement.dialog.options.opener.document).val($('#<%=hidNextWfId.ClientID%>').val());
            $('#SiteTemplet_Templet_NextWfpId', frameElement.dialog.options.opener.document).val($('#<%=hidNextWfpId.ClientID%>').val());
            $('#SiteTemplet_Templet_Todo', frameElement.dialog.options.opener.document).val(userList);
            $('#SiteTemplet_Templet_Opinion', frameElement.dialog.options.opener.document).val($('#<%=txtOpinion.ClientID%>').val().trimEnd().replace("\\", "\\\\").replace("\r\n", "\\r\\n").replace("\'", "\\\'"));
            $('#SiteTemplet_btnFlowBlock', frameElement.dialog.options.opener.document).click();
            frameElement.dialog.close();
        }
    </script>
</asp:Content>
<asp:Content ID="ContentButton" ContentPlaceHolderID="CPHButton" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="CPHContent" runat="server">
    <table class="TableLayout">
        <tr>
            <td class="TimLabel">
                <Tim:TimLabel ID="lblWfId" Text="流程：" runat="server"  Width="80px" />
            </td>
            <td>
                <Tim:TimDropDownList ID="ddlWfId" runat="server"></Tim:TimDropDownList>
            </td>
            <td class="TimLabel">
                <Tim:TimLabel ID="lblTips" runat="server" />
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="TimLabel">
                <Tim:TimLabel ID="lblWfpId" Text="当前事务：" runat="server" />
            </td>
            <td>
                <Tim:TimDropDownList ID="ddlWfpId" runat="server"></Tim:TimDropDownList>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td class="TimLabel">
                <Tim:TimLabel ID="lblNextWfpId" Text="" runat="server" />
            </td>
            <td>
                <Tim:TimDropDownList ID="ddlNextWfpId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNextWfpId_SelectedIndexChanged"></Tim:TimDropDownList>
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td class="TimLabel">
                <Tim:TimLabel ID="lblUsers" Text="人员选择：" runat="server" />
            </td>
            <td colspan="3" style="height: 120px;">
                <Tim:TimCheckBoxList ID="cblUsers" runat="server" Height="120px" Width="220px" RepeatLayout="Flow" RepeatColumns="1" RepeatDirection="Horizontal">
                </Tim:TimCheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="TimLabel">
                <Tim:TimLabel ID="lblTodo" Text="待处理人：" runat="server" />
            </td>
            <td colspan="3">
                <Tim:TimTextBox ID="txtTodo" runat="server" TextMode="MultiLine" Height="60px" Width="360px">
                </Tim:TimTextBox>
            </td>
        </tr>
        <tr>
            <td class="TimLabel">
                <Tim:TimLabel ID="lblOpinion" Text="处理意见：" runat="server" />
            </td>
            <td colspan="3">
                <Tim:TimTextBox ID="txtOpinion" runat="server" TextMode="MultiLine" Height="60px" Width="360px">
                </Tim:TimTextBox>
            </td>
        </tr>
        <tr style="height: 20px;">
            <td colspan="4">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center; width: 280px;">
                <Tim:TimButton ID="btnOk" runat="server" Text="确 定" OnClientClick="GetCheckedUser();return false;" OnClick="btnOk_Click"></Tim:TimButton>
            </td>
            <td colspan="2" style="text-align: center; width: 280px;">
                <Tim:TimButton ID="btnCancel" runat="server" Text="取 消" OnClientClick="frameElement.dialog.close();return false;"></Tim:TimButton>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="ExtTemplet" ContentPlaceHolderID="CPHTemplet" runat="server">
    <Tim:TimHiddenField ID="hidWfpAction" runat="server"></Tim:TimHiddenField>
    <Tim:TimHiddenField ID="hidNextWfId" runat="server"></Tim:TimHiddenField>
    <Tim:TimHiddenField ID="hidNextWfpId" runat="server"></Tim:TimHiddenField>
    <Tim:TimHiddenField ID="hidRequiredTodo" runat="server"></Tim:TimHiddenField>
    <Tim:TimHiddenField ID="hidRequiredTodoUsers" runat="server"></Tim:TimHiddenField>
    <Tim:TimHiddenField ID="hidRequiredOpinion" runat="server"></Tim:TimHiddenField>
    <Tim:TimHiddenField ID="hidTodo" runat="server"></Tim:TimHiddenField>
</asp:Content>
<asp:Content ID="ExtSync" ContentPlaceHolderID="CPHSync" runat="server">
</asp:Content>
