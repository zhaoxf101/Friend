<%@ Page Title="" Language="C#" MasterPageFile="~/T_TEMPLET/Master/TEditing.Master" AutoEventWireup="true" CodeBehind="ReportStyleInfo.aspx.cs" Inherits="TIM.T_TEMPLET.CommForm.ReportStyleInfo" %>


<%@ MasterType VirtualPath="~/T_TEMPLET/Master/TEditing.Master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="CPHHead" runat="server">
</asp:Content>

<asp:Content ID="ContentButton" ContentPlaceHolderID="CPHButton" runat="server">
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="CPHContent" runat="server">
    <table class="TableLayout">
        <tr>
            <td style="width: 80px;" class="TimLabel">
                <Tim:TimLabel ID="lblStyleId" Text="样式编码：" runat="server" />
            </td>
            <td>
                <Tim:TimTextBox ID="txtStyleId" runat="server" Symbol="Star"></Tim:TimTextBox>
            </td>
            <td style="width: 80px;" class="TimLabel">
                <Tim:TimLabel ID="lblStyleName" Text="样式名称：" runat="server" />
            </td>
            <td>
                <Tim:TimTextBox ID="txtStyleName" runat="server" Symbol="Star" ></Tim:TimTextBox>
            </td>
        </tr>
        <tr>
            <td class="TimLabel">
                <Tim:TimLabel ID="lblOrder" Text="序号：" runat="server" />
            </td>
            <td>
                <Tim:TimNumericTextBox ID="ntOrder" runat="server" Symbol="Star" ShowZero="false" Min="0" DecimalPlaces="0"></Tim:TimNumericTextBox>
            </td>
        </tr>
        <tr>
            <td class="TimLabel">
                <Tim:TimLabel ID="TimLabel1" Text="缺省：" runat="server" />
            </td>
            <td>
                <Tim:TimCheckBox ID="chkDefault" runat="server"></Tim:TimCheckBox>
            </td>
             <td class="TimLabel">
                <Tim:TimLabel ID="TimLabel2" Text="公用：" runat="server" />
            </td>
            <td>
                <Tim:TimCheckBox ID="chkPublic" runat="server"></Tim:TimCheckBox>
            </td>
        </tr>
        <tr>
            <td class="TimLabel">
                <Tim:TimLabel ID="lblExecOn" Text="执行条件：" runat="server" />
            </td>
            <td colspan="3">
                <Tim:TimTextBox ID="txtExecOn" TextMode="MultiLine" Height="40px" Width="400px" runat="server"></Tim:TimTextBox>
            </td>
        </tr>
        <tr>
            <td class="TimLabel">
                <Tim:TimLabel ID="lblModifier" Text="修改人：" runat="server" />
            </td>
            <td>
                <Tim:TimTextBox ID="txtModifier" Enabled="false" runat="server"></Tim:TimTextBox>
            </td>
            <td class="TimLabel">
                <Tim:TimLabel ID="lblModifierTime" Text="修改时间：" runat="server" />
            </td>
            <td>
                <Tim:TimDateTime ID="dtModifierTime" Enabled="false" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>


