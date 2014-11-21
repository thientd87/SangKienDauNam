<%@ Register TagPrefix="portal" assembly="Portal.API" Namespace="Portal.API.Controls" %>
<%@ Control EnableViewState="True" Language="C#" AutoEventWireup="true" CodeBehind="TemplateList.ascx.cs" Inherits="Portal.GUI.Administrator.AdminPortal.TemplateList" %>
<div class="SectionHeader">
	<portal:Label ID="lblTemplateList" runat="server" LanguageRef="TemplateList"></portal:Label>
</div>
<portal:LinkButton ID="lnkAddTemplate" Runat="server" OnClick="OnAddTemplate" CssClass="LinkButton" LanguageRef="AddTemplate"></portal:LinkButton>
|
<portal:LinkButton ID="lnkFilterTemplateByType" Runat="server" OnClick="OnFilterTemplateByType" CssClass="LinkButton" LanguageRef="FilterTemplateByType"></portal:LinkButton>
<asp:DropDownList Runat="server" ID="ddrTemplateType"></asp:DropDownList>
<asp:datagrid id="Templates" runat="server" AutogenerateColumns="false" Width="100%" BorderColor="#B8C1CA" BorderStyle="Solid" BorderWidth="1px">
	<HeaderStyle CssClass="grdHeader"></HeaderStyle>
	<ItemStyle CssClass="grdItem"></ItemStyle>
	<AlternatingItemStyle CssClass="grdAlterItem" />
	<Columns>
		<asp:TemplateColumn HeaderText="!" HeaderStyle-Width="16px" ItemStyle-HorizontalAlign="Center">
			<ItemTemplate>
				<asp:LinkButton Runat="server" ID="lnkReference" 
					OnCommand="OnEditTemplate" 
					CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Reference") %>' >
					<img src="Images/Icons/Edit.gif" alt="Edit">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<portal:BoundColumn DataField="Type" LanguageRef-HeaderText="TemplateType" />
		<portal:BoundColumn DataField="Reference" LanguageRef-HeaderText="TemplateReference" />
	</Columns>
</asp:datagrid>
