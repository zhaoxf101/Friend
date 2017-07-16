<%@ Page Title="" Language="C#" MasterPageFile="~/T_TEMPLET/Master/TListing.Master" AutoEventWireup="true" CodeBehind="ReportStyleList.aspx.cs" Inherits="TIM.T_TEMPLET.CommForm.ReportStyleList" %>

<%@ MasterType VirtualPath="~/T_TEMPLET/Master/TListing.Master" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="CPHHead" runat="server">
</asp:Content>

<asp:Content ID="ContentButton" ContentPlaceHolderID="CPHButton" runat="server">
    <table>
        <tr>
            <td>
                <Tim:TimButtonMenu ID="btnDefault" Text="缺省" runat="server" OnClick="btnDefault_Click" />
            </td>
             <td>
                <Tim:TimButtonMenu ID="btnPublic" Text="公用" runat="server" OnClick="btnPublic_Click" />
            </td>
             <td>
                <Tim:TimButtonMenu ID="btnStyle" Text="样式定义" Width="64px" runat="server" OnClick="btnStyle_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="ContentQuery" ContentPlaceHolderID="CPHQuery" runat="server">
    <table>
        <tr>
            <td class="TimLabel">
                <Tim:TimLabel ID="lblQueryStyleName" Text="样式名称：" runat="server">
                </Tim:TimLabel></td>
            <td>
                <Tim:TimTextBox ID="txtQueryStyleName" runat="server"></Tim:TimTextBox></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="CPHContent" runat="server">
    <Tim:TimGridView ID="gvMaster" runat="server">
        <columns>
            <Tim:TimBoundField DataField="REPORTSTYLE_STYLEID" HeaderText="样式编码">
                <ItemStyle Width="200px" />
            </Tim:TimBoundField>
            <Tim:TimBoundField DataField="REPORTSTYLE_STYLENAME" HeaderText="样式名称">
                <ItemStyle Width="200px" />
            </Tim:TimBoundField>
            <Tim:TimBoundField DataField="REPORTSTYLE_ORDER" HeaderText="序号" Mode="Numeric" DecimalPlaces="0">
                <ItemStyle Width="36px" />
            </Tim:TimBoundField>
            <Tim:TimBoundField DataField="REPORTSTYLE_DEFAULT" HeaderText="缺省" Mode="CheckBox">
                <ItemStyle Width="36px" />
            </Tim:TimBoundField>
            <Tim:TimBoundField DataField="REPORTSTYLE_PUBLIC" HeaderText="公用" Mode="CheckBox">
                <ItemStyle Width="36px" />
            </Tim:TimBoundField>
             <Tim:TimBoundField DataField="MODIFIER" HeaderText="修改人">
                <ItemStyle Width="65px" />
            </Tim:TimBoundField>
             <Tim:TimBoundField DataField="MODIFIEDTIME" HeaderText="修改时间">
                <ItemStyle Width="120px" />
            </Tim:TimBoundField>
        </columns>
    </Tim:TimGridView>
</asp:Content>

