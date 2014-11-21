<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShowCommentReturn.ascx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.Newslist.ShowCommentReturn" %>
<link rel="stylesheet" type="text/css" href="/styles/Newsedit.css" />
<link href="/styles/Core.css" rel="stylesheet" type="text/css" />
<link href="/styles/common.css" rel="stylesheet" type="text/css" />
<table width="100%">
    <tr>
        <td class="Edit_Head_Cell">
            Commnet trả lại bài viết
            </td>
    </tr>
</table>

<table border="0" cellpadding="0" cellspacing="5" width="100%">
    <tr>
        <td class="ms-input" width="20%">
            Tiêu đề comment</td>
        <td class="ms-input">
            <asp:TextBox CssClass="ms-long" ID="txtComment_Title" runat="server" Width="395px"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td valign="top" class="ms-input">
            Nội dung comment</td>
        <td>
            <asp:TextBox CssClass="ms-long"  ID="txtComment_Content" runat="server" Height="111px" TextMode="MultiLine"
                Width="396px"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="center" class="Edit_Foot_Cell" colspan="2" style="height: 30px">
            <asp:Button ID="btnThoat" OnClientClick="window.close();return false;" runat="server" Text="Thoát" CommandName="GuiThang" CssClass="ms-input" />            
        </td>
    </tr>
</table>