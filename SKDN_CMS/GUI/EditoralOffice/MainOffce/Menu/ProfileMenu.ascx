<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileMenu.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffice.Menu.ProfileMenu" %>


<table cellpadding="0" cellspacing="0" width="100%" style="border-right:1px solid #b8c1ca;">
<tr>
    <td id="tdHeader_1" class="Menuleft_HeadBox" colspan="2" style="border-right:none;">Chức năng cá nhân</td>    
</tr>
<tr>
<td valign="top" class="Menuleft_ContentBox" colspan="2" style="border-right:none;">
<div class="Menuleft_Item" runat="server" id="diva" onmouseover="MouseOverTable(this);" onmouseout="MouseOutTable(this)">
	+&nbsp;<asp:LinkButton ID="itemAccount" runat="server" OnClick="itemAccount_Click">Tài khoản</asp:LinkButton>
</div>
<div class="Menuleft_Item" runat="server" id="div1" onmouseover="MouseOverTable(this);" onmouseout="MouseOutTable(this)">
	+&nbsp;<asp:HyperLink ID="itemProfile" runat="server">Thông tin cá nhân</asp:HyperLink>
</div>

<div class="Menuleft_Item" runat="server" id="div2" onmouseover="MouseOverTable(this);" onmouseout="MouseOutTable(this)">
	+&nbsp;<asp:HyperLink ID="itemQuanTriHeThong" NavigateUrl="~/AdminPortal.aspx" runat="server">Quản trị hệ thống</asp:HyperLink>
</div>

<div class="Menuleft_Item" runat="server" id="divb" onmouseover="MouseOverTable(this);" onmouseout="MouseOutTable(this)">
	+&nbsp;<asp:HyperLink ID="itemHelp" runat="server">Hướng dẫn - Trợ giúp</asp:HyperLink>
</div>
<div class="Menuleft_Item" runat="server" id="divc" onmouseover="MouseOverTable(this);" onmouseout="MouseOutTable(this)">
	+&nbsp;<asp:LinkButton ID="itemLogOut" runat="server" OnClick="itemLogOut_Click">Đăng xuất</asp:LinkButton>
</div>    
</td>
</tr>
</table>
