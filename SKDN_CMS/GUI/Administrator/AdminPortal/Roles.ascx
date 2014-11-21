<%@ Control EnableViewState="True" Language="c#" AutoEventWireup="True" Codebehind="Roles.ascx.cs" Inherits="Portal.GUI.Administrator.AdminPortal.Roles" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="portal" assembly="Portal.API" Namespace="Portal.API.Controls" %>
<table width="100%" cellspacing="0" rules="all" border="1" BorderColor="#B8C1CA" style="border-collapse:collapse">
	<tr>
		<td class="grdHeader">&nbsp;</td>
		<td id="tdHeaderRoleType" runat="server" class="grdHeader" width="100">
			<portal:Label runat="server" LanguageRef="RoleType"></portal:Label>
		</td>
		<td class="grdHeader">
			<portal:Label runat="server" LanguageRef="Role" ID="Label1" NAME="Label1"></portal:Label>
		</td>
	</tr>
	<asp:Repeater id="gridRoles" runat="server" OnItemDataBound="OnDataBind">
		<ItemTemplate>
			<tr>
				<td class="ListLine">
					<asp:LinkButton ID="lnkDelete" Runat="server" OnCommand="OnDelete" CausesValidation="False"><img src="Images/Icons/delete.gif" alt="Delete"></asp:LinkButton>
				</td>
				<td class="ListLine" id="tdRoleType" runat="server">
					<asp:Label Runat="server" ID="lRoleType"></asp:Label>
				</td>
				<td class="ListLine">
					<asp:Label Runat="server" ID="lRole"></asp:Label>
				</td>
			</tr>
		</ItemTemplate>
	</asp:Repeater>
	<tr>
		<td width="20"><asp:LinkButton ID="lnkAddRole" OnClick="OnAddRole" Runat="server" CausesValidation="False"><img src="Images/Icons/new.gif" alt="Add"></asp:LinkButton></td>
		<td class="ListLine" id="tdAddRoleType" runat="server">
			<portal:DropDownList Runat="server" ID="cbAddRoleType" Width="100%">
				<asp:ListItem Value=""></asp:ListItem>
				<asp:ListItem Value="view" LanguageRef="RoleView"></asp:ListItem>
				<asp:ListItem Value="edit" LanguageRef="RoleEdit"></asp:ListItem>
			</portal:DropDownList>
		</td>
		<td class="ListLine" >
			<asp:DropDownList Runat="server" ID="cbAddRole" Width="100%"></asp:DropDownList>
		</td>
	</tr>
</table>
