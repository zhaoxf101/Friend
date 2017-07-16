<%@ Page Title="" Language="C#" MasterPageFile="~/T_TEMPLET/Master/TListing.Master" AutoEventWireup="true" CodeBehind="DocFileHandle.aspx.cs" Inherits="TIM.T_TEMPLET.CommForm.DocFileHandle" %>

<%@ MasterType VirtualPath="~/T_TEMPLET/Master/TListing.Master" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="CPHHead" runat="server">
  <script type="text/javascript">
    function checkDelete() {
      if ($get("ctl00_CPHForm_ContentGrid") == null) {
        alert("没有可删除的记录！");
        return false;
      }
      if ($get("ctl00_CPHForm_ContentGrid_selectedIndex").value == -1
        && $get("ctl00_CPHForm_ContentGrid_CheckedRowIndexs").value.trim() == "") {
        alert("请选择需要删除的附件记录！");
        return false;
      }
      else {
        if (confirm('您确实要删除该附件吗？') == false) return false;
      }
      return true;
    }
    function checkUploading() {
      if ($('#ProgressBar_currentfilenameelement').html() == undefined || $('#ProgressBar_currentfilenameelement').html() == "") {
        return true;
      }
      else {
        alert('文件正在上传中，请稍后上传......');
        return false;
      }
    }
    function uploadFile() {
      $('#<%=btnPostFile.ClientID%>').click();
    }
    function fileUploadFile() {
      $('#<%=fileUploadFile.ClientID%>').click();
    }

    function simulateClick() {
      if ($('#<%=btnUploadFile.ClientID%>').attr('disabled') != 'disabled')
        $('#<%=fileUploadFile.ClientID%>').offset({ top: 10, left: 0 });
    }
  </script>
</asp:Content>
<asp:Content ID="ContentButton" ContentPlaceHolderID="CPHButton" runat="server">
  <table>
    <tr>
      <td>
        <asp:FileUpload ID="fileUploadFile" Enabled="true" AllowMultiple="true" runat="server"
          Style="z-index: 1999; left: -40px; top: 10px; position: absolute; filter: alpha(opacity=0); opacity: 0; width: 40px;" hideFocus="true"
          onchange="uploadFile();" onclick="return checkUploading()" />
        <Tim:TimButtonMenu ID="btnUploadFile" Text="上传" runat="server" />
      </td>
      <td>
        <Tim:TimButtonMenu ID="btnDownloadFile" Text="下载" runat="server" OnClientClick="if (!checkUploading()) return false;" OnClick="btnDownloadFile_Click" />
      </td>
      <td>
        <Tim:TimButtonMenu ID="btnDeleteFile" Text="删除" runat="server" OnClientClick="if (!checkUploading()) return false;" OnClick="btnDeleteFile_Click" />
      </td>
    </tr>
  </table>
</asp:Content>
<asp:Content ID="ContentQuery" ContentPlaceHolderID="CPHQuery" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="CPHContent" runat="server">
  <Tim:TimGridView ID="gvMaster" runat="server" EnableViewState="true" OnRowDoubleClick="gvMaster_RowDoubleClick">
    <columns>
      <Tim:TimBoundField DataField="DFSFILE_FILENAME" HeaderText="文件名">
        <ItemStyle Width="400px" />
      </Tim:TimBoundField>
      <Tim:TimBoundField DataField="DFSFILE_EXTNAME" HeaderText="类型">
        <ItemStyle Width="60px" />
      </Tim:TimBoundField>
      <Tim:TimBoundField DataField="DFSFILE_FILESIZE" HeaderText="大小">
        <ItemStyle Width="90px" horizontalalign="Right"/>
      </Tim:TimBoundField>
    </columns>
  </Tim:TimGridView>
  <script type="text/javascript">
    function dfsDownloadFile(pathUrl) {
      $('#frmDownLoad').attr('src', pathUrl);
    }
    $(function () {
      $('#<%=btnUploadFile.ClientID%>').on("mouseover", simulateClick);
    });
  </script>
</asp:Content>
<asp:Content ID="ExtTemplet" ContentPlaceHolderID="CPHTemplet" runat="server">
  <div style="position: absolute; z-index: 100000; top: 420px; left: 50px">
    <Tim:TimUploadProgressBar ID="ProgressBar" runat="server" />
  </div>
  <Tim:TimHiddenField ID="hidFileGroup" runat="server"></Tim:TimHiddenField>
</asp:Content>
<asp:Content ID="ExtSync" ContentPlaceHolderID="CPHSync" runat="server">
  <Tim:TimButton style="display: none;" ID="btnPostFile" runat="server" OnClick="btnPostFile_Click"></Tim:TimButton>
  <iframe id="frmDownLoad" style="display: none;" src=""></iframe>
</asp:Content>
