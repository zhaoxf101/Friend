<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tips.aspx.cs" Inherits="TIM.T_INDEX.Tips.Tips" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title>浏览器设置</title>
  <style type="text/css">
    body, p, th, td, li, ul, ol, h1, h2, h3, h4, h5, h6, pre {
      font-family: 宋体;
      line-height: 1.4;
    }

    body, p, th, td, li, ul, ol, pre {
      font-size: 12px;
    }
  </style>
</head>
<body style="color: #000000; background-color: #F9F9F9">
  <form id="form1" runat="server">
    <h2 align="center">浏览器设置</h2>
    <div>      
      &nbsp;&nbsp; 1、为保证系统正常运行，需要对IE浏览器进行一些设置。请下载<asp:LinkButton ID="btnDownload" runat="server" OnClick="btnDownload_Click" Font-Bold="true" ForeColor="red">浏览器设置工具</asp:LinkButton>
      并直接运行，该工具将自动完成所需的IE浏览器设置工作。
      <br />
      <br />
      &nbsp;&nbsp; 2、报表组件<a href="../../Ocx/UtoOcx.exe" style="color: red; font-weight: bold;">安装程序</a>下载后手工安装。
	  <br />
	  <br />
      &nbsp;&nbsp; 3、本系统必须使用IE8及以上版本访问，请下载<a href="http://10.51.23.18/down/SoftDown.asp?ID=868" style="color: red; font-weight: bold;">IE8安装程序</a>后安装。
	  <br />
	  <br />      
    </div>
    <br />
  </form>
</body>
</html>
