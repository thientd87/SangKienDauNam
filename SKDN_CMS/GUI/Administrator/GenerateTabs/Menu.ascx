<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="Portal.GUI.Administrator.GenerateTabs.Menu" %>
<div class="Menuleft_Item" runat="server" id="Div1" onmouseover="MouseOverTable(this);" onmouseout="MouseOutTable(this)">
	+&nbsp;<asp:HyperLink ID="itemAddCat" runat="server">Thêm chuyên mục mới</asp:HyperLink>
</div>
<div class="Menuleft_Item" runat="server" id="Div2" onmouseover="MouseOverTable(this);" onmouseout="MouseOutTable(this)">
	+&nbsp;<asp:HyperLink ID="itemListCat" runat="server">Danh sách chuyên mục</asp:HyperLink>
</div>
<div class="Menuleft_Item" runat="server" id="Div3" onmouseover="MouseOverTable(this);" onmouseout="MouseOutTable(this)">
	+&nbsp;<asp:HyperLink ID="itemComeAdmin" runat="server">Trang quản trị</asp:HyperLink>
</div>
<div class="Menuleft_Item" runat="server" id="Div5" onmouseover="MouseOverTable(this);" onmouseout="MouseOutTable(this)">
	+&nbsp;<asp:HyperLink ID="itemEditionType" runat="server">Quản lý nhóm chuyên mục</asp:HyperLink>
</div>
<div class="Menuleft_Item" runat="server" id="Div4" onmouseover="MouseOverTable(this);" onmouseout="MouseOutTable(this)">
	+&nbsp;<asp:HyperLink ID="itemComeOffice" runat="server">Trang tác nghiệp</asp:HyperLink>
</div>
<div class="Menuleft_Item" runat="server" id="Div6" onmouseover="MouseOverTable(this);" onmouseout="MouseOutTable(this)">
	+&nbsp;<asp:HyperLink ID="itemHeThong" NavigateUrl="~/AdminPortal.aspx" runat="server">Quản trị hệ thống</asp:HyperLink>
</div>