<%@ Control EnableViewState="True" Language="c#" AutoEventWireup="True" Codebehind="ModuleList.ascx.cs" Inherits="Portal.GUI.Administrator.AdminPortal.ModuleList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="portal" assembly="Portal.API" Namespace="Portal.API.Controls" %>
<table Class="ModuleList_Header" width="100%" cellpadding="0" cellspacing="0">
	<tr>
		<td>
			<div class="ModuleTitle" id="divTitle" runat="server"></div>
		</td>
		<td width="*" align="right">
			<portal:LinkButton ID="lnkAddModule" Runat="server" OnCommand="OnAddModule" CssClass="LinkButtonSmall"
				LanguageRef="AddModule"></portal:LinkButton>
		</td>
	</tr>
</table>
<input type="hidden" id="lblColumnReference" runat="server">
<asp:datagrid id="gridModules" runat="server" AutogenerateColumns="false" Width="100%" orderColor="#B8C1CA" BorderStyle="Solid" BorderWidth="1px">
	<HeaderStyle CssClass="grdHeader"></HeaderStyle>
	<ItemStyle CssClass="grdItem"></ItemStyle>
	<AlternatingItemStyle CssClass="grdAlterItem" />
	<Columns>		
		<asp:TemplateColumn HeaderStyle-Width="16px" ItemStyle-HorizontalAlign="Center">
			<ItemTemplate>
				<asp:LinkButton Runat="server" ID="lnkModule" OnCommand="OnEditModule" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Reference") %>' >
					<img src="Images/Icons/Edit.gif" alt="Edit">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderStyle-Width="16px" ItemStyle-HorizontalAlign="Center">
			<ItemTemplate>
				<asp:LinkButton Runat="server" ID="lnkUp" OnCommand="OnModuleUp" CommandArgument='<%# Container.ItemIndex %>' >
					<img src="Images/Icons/up.gif" alt="Up">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderStyle-Width="16px" ItemStyle-HorizontalAlign="Center">
			<ItemTemplate>
				<asp:LinkButton Runat="server" ID="lnkDown" OnCommand="OnModuleDown" CommandArgument='<%# Container.ItemIndex %>' >
					<img src="Images/Icons/down.gif" alt="Down">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>		
		<portal:BoundColumn DataField="Title" LanguageRef-HeaderText="Title" />
		<portal:TemplateColumn LanguageRef-HeaderText="Reference">
			<ItemTemplate>
				<asp:Literal ID="ltrReference" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Reference").ToString() %>'>
				</asp:Literal>
			</ItemTemplate>
		</portal:TemplateColumn>
		<portal:BoundColumn DataField="ModuleType" LanguageRef-HeaderText="Type" />
	</Columns>
</asp:datagrid>
<br>
