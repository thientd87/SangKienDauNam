<%@ Register TagPrefix="portal" assembly="Portal.API" Namespace="Portal.API.Controls" %>
<%@ Control EnableViewState="True" Language="c#" AutoEventWireup="True" Codebehind="ColumnList.ascx.cs" Inherits="Portal.GUI.Administrator.AdminPortal.ColumnList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<br>
<table Class="ModuleList_Header" width="100%" cellpadding="0" cellspacing="0">
	<tr>
		<td>
			<div class="ModuleTitle"><portal:label id="lblColumnListTitle" LanguageRef="ColumnList" runat="server"></portal:label></div>
		</td>
		<td width="*" align="right">
			<portal:linkbutton id="lnkAddColumn" LanguageRef="AddColumn" CssClass="LinkButton" Runat="server" OnCommand="OnAddColumn"></portal:linkbutton>
			<portal:linkbutton id="lnkAddSubColumn" LanguageRef="AddSubColumn" CssClass="LinkButton" Runat="server" OnCommand="OnAddSubColumn"></portal:linkbutton>
		</td>
	</tr>
</table>

<asp:datagrid id="dgrColumns" runat="server" AutogenerateColumns="false" Width="100%" BorderColor="#B8C1CA" BorderStyle="Solid" BorderWidth="1px">
	<HeaderStyle CssClass="grdHeader"></HeaderStyle>
	<ItemStyle CssClass="grdItem"></ItemStyle>
	<AlternatingItemStyle CssClass="grdAlterItem" />
	<Columns>
		<asp:TemplateColumn HeaderText="" HeaderStyle-Width="16px" ItemStyle-HorizontalAlign="Center">
			<ItemTemplate>
				<asp:LinkButton Runat="server" ID="lnkTitle" OnCommand="OnEditColumn" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ColumnReference") %>' >
					<img src="Images/Icons/Edit.gif" alt="Edit">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="" HeaderStyle-Width="16px" ItemStyle-HorizontalAlign="Center">
			<ItemTemplate>
				<asp:LinkButton Runat="server" ID="lnkUp" OnCommand="OnMoveColumnLeft" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ColumnReference") %>' >
					<img src="Images/Icons/left.gif" alt="Left">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="" HeaderStyle-Width="16px" ItemStyle-HorizontalAlign="Center">
			<ItemTemplate>
				<asp:LinkButton Runat="server" ID="lnkDown" OnCommand="OnMoveColumnRight" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ColumnReference") %>' >
					<img src="Images/Icons/right.gif" alt="Right">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<portal:BoundColumn DataField="ColumnName" LanguageRef-HeaderText="ColumnName" />
		<portal:TemplateColumn LanguageRef-HeaderText="ColumnReference">
			<ItemTemplate>
				<asp:Literal ID="ltrReference" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ColumnReference").ToString() %>'></asp:Literal>
			</ItemTemplate>
		</portal:TemplateColumn>
	</Columns>
</asp:datagrid>
