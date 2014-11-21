<%@ Control EnableViewState="False" Language="c#" AutoEventWireup="True" Codebehind="TabPath.ascx.cs" Inherits="Portal.GUI.Administrator.TabPath.TabPath" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:Repeater ID="tabpath" Runat="server">
	<HeaderTemplate>
		<table width="100%" class="TabPath" border="0" cellpadding="0" cellspacing="0">
			<tr>
				<td class="TabPath_Content">
	</HeaderTemplate>
	<ItemTemplate>
		<asp:HyperLink CssClass="TabPathButton" ID="lnktext" Runat="server"
			NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "URL") %>'>
				<%# DataBinder.Eval(Container.DataItem, "Text") %>
			</asp:HyperLink>
	</ItemTemplate>
	<SeparatorTemplate><img src="Images/TP/right.gif" align="absmiddle" border="0" /></SeparatorTemplate>
	<FooterTemplate>
		</td></tr></table>
	</FooterTemplate>
</asp:Repeater>