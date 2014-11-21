<%@ Control EnableViewState="True" Language="c#" AutoEventWireup="True" Codebehind="TabList.ascx.cs" Inherits="Portal.GUI.Administrator.AdminPortal.TabList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="portal" assembly="Portal.API" Namespace="Portal.API.Controls" %>
<div class="SectionHeader" runat="Server"  Visible="false">
	<portal:Label runat="server" LanguageRef="SubTabs"></portal:Label>
</div>
<portal:LinkButton  Visible="false" ID="lnkAddModule" Runat="server" OnClick="OnAddTab" CssClass="LinkButton" LanguageRef="AddTab"></portal:LinkButton>
<asp:datagrid Visible="false" id="Tabs" runat="server" AutogenerateColumns="false" Width="100%" BorderColor="#B8C1CA" BorderStyle="Solid" BorderWidth="1px">
	<HeaderStyle CssClass="grdHeader"></HeaderStyle>
	<ItemStyle CssClass="grdItem"></ItemStyle>
	<AlternatingItemStyle CssClass="grdAlterItem" />
	<Columns>
		<asp:TemplateColumn HeaderText="!" HeaderStyle-Width="16px" ItemStyle-HorizontalAlign="Center">
			<ItemTemplate>
				<asp:LinkButton Runat="server" ID="lnkTitle" 
					OnCommand="OnEditTab" 
					CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Reference") %>' >
					<img src="Images/Icons/Edit.gif" alt="Edit">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="!" HeaderStyle-Width="16px" ItemStyle-HorizontalAlign="Center">
			<ItemTemplate>
				<asp:LinkButton Runat="server" ID="lnkUp" 
					OnCommand="OnTabUp" 
					CommandArgument='<%# Container.ItemIndex %>' >
					<img src="Images/Icons/up.gif" alt="Up">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="!" HeaderStyle-Width="16px" ItemStyle-HorizontalAlign="Center">
			<ItemTemplate>
				<asp:LinkButton Runat="server" ID="lnkDown" 
					OnCommand="OnTabDown" 
					CommandArgument='<%# Container.ItemIndex %>' >
					<img src="Images/Icons/down.gif" alt="Down">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<portal:BoundColumn DataField="Title" LanguageRef-HeaderText="Title" />
		<portal:BoundColumn DataField="Reference" LanguageRef-HeaderText="Reference" />
	</Columns>
</asp:datagrid>