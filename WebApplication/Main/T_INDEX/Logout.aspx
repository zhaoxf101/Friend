<%@ Page Title="" Language="C#" MasterPageFile="~/T_TEMPLET/Master/NestedSite.Master" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="TIM.T_INDEX.Logout" %>

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
    <asp:updatepanel runat="server" id="UpNested" updatemode="Conditional" childrenastriggers="true" rendermode="Block">
        <ContentTemplate>
            <table>
                <tr style="height: 50px;">
                </tr>
                <tr>
                    <td align="right" style="width: 200px;">
                        <Tim:TimButton runat="server" ID="btnLogin" Text="注销" OnClick="btnLogout_Click"></Tim:TimButton>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:updatepanel>
</asp:Content>
