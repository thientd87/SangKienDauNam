<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RolePermission.ascx.cs" Inherits="DFISYS.GUI.Users.User.RolePermission" %>
<link rel="stylesheet" type="text/css" href="/styles/Users.css" />
<table width=100% cellpadding="5" cellspacing="5" style="margin-top: 10px;">
    <tr>
        <td colspan="2"><h1>Quản lý quyền mặc định cho các nhóm</h1></td>
    </tr>
    <tr>
        <td>Danh sách nhóm</td>
        <td>Danh sách quyền</td>
    </tr>
    <tr>
        <td valign=top style="height: 140px">
            <asp:ListBox ID="lbxRole" runat="server" OnSelectedIndexChanged="lbxRole_SelectedIndexChanged" AutoPostBack="True" Height="200" Width="100%"></asp:ListBox>
        </td>
        <td style="vertical-align: top; padding:0 10px 10px">
            <asp:CheckBoxList ID="clbPermission" runat="server" CellPadding="5" CellSpacing="5" RepeatColumns="2" CssClass="ms-formlabel">
            </asp:CheckBoxList></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnUpdate" runat="server" CssClass="btnUpdate" OnClick="btnUpdate_Click" Text="Cập nhật" />&nbsp;
            <asp:Button ID="btnGoback" runat="server" CssClass="btnUpdate" OnClick="btnGoback_Click" Text="Quay lại" />
        </td>
    </tr>
</table>
