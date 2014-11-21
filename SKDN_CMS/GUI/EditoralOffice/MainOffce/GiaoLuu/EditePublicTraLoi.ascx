<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditePublicTraLoi.ascx.cs"
    Inherits="DFISYS.GUI.EditoralOffice.MainOffce.GiaoLuu.EditePublicTraLoi" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<link rel="stylesheet" type="text/css" href="/Styles/Newsedit.css?date=1806" />
<script language="javascript" type="text/javascript" src="/Scripts/Newsedit.js?ver=1"></script>
<h1 style="text-align: center">
    Biên tập lại nội dung trả lời</h1>
<div class="form floatleftallchild">
    <div style="overflow:hidden; width:100%"  >
        <div class="t1"><span>Chủ đề</span><em>*</em></div>
         <b><%=chude %></b>
    </div>
        Nội dung trả lời<br />
    <div id="editors">
        <CKEditor:CKEditorControl FilebrowserBrowseUrl="/FileManager/index.html" BasePath="/ckeditor/" runat="server" Width="800px" ID="NewsContent"
            runat="server" />
    </div>
    <div style="clear: both; width: 100%">
        &nbsp;
    </div>
    <asp:Button ID="btnUpdate" Text="Lưu lại" runat="server"
        OnClick="btnUpdate_Click" CssClass="button white"></asp:Button>
        <asp:Button ID="btnCancel" Text="Hủy" runat="server"
        OnClick="btnCancel_Click" CssClass="button red"></asp:Button>
</div>
<script language="javascript">
    var prefix = '<% = ClientID %>'; var cpmode = '<% = Request.QueryString["cpmode"] %>';
</script>
