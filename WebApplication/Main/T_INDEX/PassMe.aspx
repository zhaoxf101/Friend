<%@ Page Title="" Language="C#" MasterPageFile="~/T_TEMPLET/Master/NestedSite.Master" AutoEventWireup="true" CodeBehind="PassMe.aspx.cs" Inherits="TIM.T_INDEX.PassMe" %>

<%@ MasterType VirtualPath="~/T_TEMPLET/Master/NestedSite.Master" %>

<asp:Content ID="LoginHead" ContentPlaceHolderID="NestedHead" runat="server">
    <style type="text/css">
        body {
            /*background-image: url(Images/home.png);*/
            background-position: center;
            background-repeat: no-repeat;
            background-attachment: fixed;
        }
    </style>
</asp:Content>

<asp:Content ID="LoginBody" ContentPlaceHolderID="NestedBody" runat="server">
    <asp:updatepanel runat="server" id="UpNested" updatemode="Conditional" childrenastriggers="false" rendermode="Block">
        <ContentTemplate>
            <table class="TableLayout">
                <tr>
                    <td style="width: 80px;" class="TimLabel">
                        <Tim:TimLabel ID="lblPsw" Text="当前密码：" runat="server" />
                    </td>
                    <td>
                        <Tim:TimTextBox ID="txtPsw" runat="server" TextMode="Password"></Tim:TimTextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="TimLabel">
                        <Tim:TimLabel ID="lblNewPsw" Text="新密码：" runat="server" />
                    </td>
                    <td>
                        <Tim:TimTextBox ID="txtNewPsw" runat="server" TextMode="Password"></Tim:TimTextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="TimLabel">
                        <Tim:TimLabel ID="lblConfirmNewPsw" Text="确认密码：" runat="server" />
                    </td>
                    <td>
                        <Tim:TimTextBox ID="txtConfirmNewPsw" runat="server" TextMode="Password"></Tim:TimTextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="height:20px;"></td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                    <td align="right">
                        <Tim:TimButton runat="server" ID="btnPass" Text="修改密码" OnClick="btnPass_Click"></Tim:TimButton>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:updatepanel>
</asp:Content>
