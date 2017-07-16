<%@ Page Title="" Language="C#" MasterPageFile="~/T_TEMPLET/Master/TListing.Master" AutoEventWireup="true" CodeBehind="WfpTrace.aspx.cs" Inherits="TIM.T_TEMPLET.CommForm.WfpTrace" %>

<%@ MasterType VirtualPath="~/T_TEMPLET/Master/TListing.Master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="CPHHead" runat="server">
</asp:Content>
<asp:Content ID="ContentButton" ContentPlaceHolderID="CPHButton" runat="server">
</asp:Content>
<asp:Content ID="ContentQuery" ContentPlaceHolderID="CPHQuery" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="CPHContent" runat="server">
    <Tim:TimGridView ID="gvMaster" runat="server">
        <columns>

            <Tim:TimBoundField DataField="NO2" HeaderText="序号" Mode="Numeric" DecimalPlaces="0" ShowZero="false">
                <ItemStyle Width="60px" HorizontalAlign="center" />
            </Tim:TimBoundField>
            <Tim:TimBoundField DataField="WFPID" HeaderText="事务">
                <ItemStyle Width="60px" HorizontalAlign="center" />
            </Tim:TimBoundField>
            <Tim:TimBoundField DataField="TODO" HeaderText="待处理人">
                <ItemStyle Width="120px" />
            </Tim:TimBoundField>
            <Tim:TimBoundField DataField="AUSER" HeaderText="处理人">
                <ItemStyle Width="65px" />
            </Tim:TimBoundField>
            <Tim:TimBoundField DataField="WFPACTION" HeaderText="处理结果">
                <ItemStyle Width="60px" HorizontalAlign="center" />
            </Tim:TimBoundField>
            <Tim:TimBoundField DataField="OPINION" HeaderText="处理意见">
                <ItemStyle Width="150px" />
            </Tim:TimBoundField>
             <Tim:TimBoundField DataField="RBEGIN" HeaderText="事务开始时间">
                <ItemStyle Width="120px" />
            </Tim:TimBoundField>
             <Tim:TimBoundField DataField="REND" HeaderText="要求完成时间">
                <ItemStyle Width="120px" />
            </Tim:TimBoundField>
             <Tim:TimBoundField DataField="AEND" HeaderText="实际完成时间">
                <ItemStyle Width="120px" />
            </Tim:TimBoundField>
        </columns>
    </Tim:TimGridView>
</asp:Content>
<asp:Content ID="ExtTemplet" ContentPlaceHolderID="CPHTemplet" runat="server">
</asp:Content>
<asp:Content ID="ExtSync" ContentPlaceHolderID="CPHSync" runat="server">
</asp:Content>
