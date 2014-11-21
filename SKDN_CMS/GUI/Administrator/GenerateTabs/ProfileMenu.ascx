<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileMenu.ascx.cs" Inherits="Portal.GUI.Administrator.GenerateTabs.ProfileMenu" %>
<div class="Menuleft_Item" runat="server" id="diva" onmouseover="MouseOverTable(this);" onmouseout="MouseOutTable(this)">
	+&nbsp;<asp:HyperLink ID="itemAccount" runat="server">Tài khoản</asp:HyperLink>
</div>
<div class="Menuleft_Item" runat="server" id="divb" onmouseover="MouseOverTable(this);" onmouseout="MouseOutTable(this)">
	+&nbsp;<asp:HyperLink ID="itemHelp" runat="server">Hướng dẫn - Trợ giúp</asp:HyperLink>
</div>
<div class="Menuleft_Item" runat="server" id="divc" onmouseover="MouseOverTable(this);" onmouseout="MouseOutTable(this)">
	+&nbsp;<asp:LinkButton ID="itemLogOut" runat="server" OnClick="itemLogOut_Click">Đăng xuất</asp:LinkButton>
</div>