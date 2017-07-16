<%@ Page Title="" Language="C#" MasterPageFile="~/T_TEMPLET/Master/NestedSite.Master" AutoEventWireup="true" CodeBehind="ErrorHandler.aspx.cs" Inherits="TIM.T_INDEX.ErrorHandler" %>

<%@ MasterType VirtualPath="~/T_TEMPLET/Master/NestedSite.Master" %>

<asp:Content ID="LoginHead" ContentPlaceHolderID="NestedHead" runat="server">
  <style type="text/css">
    .error-box {
      background-image: url('<%=Page.ResolveUrl("~")%>images/err_bg.png');
      background-repeat: no-repeat;
      background-size: 100% 100%;
      margin: auto;
      height: 314px;
      width: 750px;
    }

      .error-box h2 {
        color: #1e6e19;
        height: 70px;
        line-height: 70px;
        text-align: center;
      }

      .error-box dl {
        margin: 20px 0 0 170px;
      }
  </style>
</asp:Content>

<asp:Content ID="LoginBody" ContentPlaceHolderID="NestedBody" runat="server">
  <div class="error-box">
    <h2>错误信息提示！</h2>
    <dl>
      <dt>
        <asp:label id="lblTitle" runat="server"></asp:label>
      </dt>
      <dt>
        <a onclick="if (document.all.divMsgDetail.style.display=='none') {document.all.divMsgDetail.style.display='';} else{document.all.divMsgDetail.style.display='none';}"
          style='cursor: pointer; font-weight: bold;'>详细信息......</a>
      </dt>
      <dt>
        <asp:label id="lblMessage" runat="server"></asp:label>
      </dt>
    </dl>
  </div>
</asp:Content>
