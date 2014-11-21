<%@ Control Language="c#" AutoEventWireup="True" Codebehind="uscPager.ascx.cs" Inherits="Intelliworks.Modules.CustomControls.PollManager.uscPager" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table>
	<tr>
		<td><asp:Literal ID="lblPageCaption" Runat="server"></asp:Literal><asp:Literal ID="lblPageStatus" Runat="server"></asp:Literal></td>
		<td><asp:linkbutton id="lnkFirstPage" Runat="server" onclick="lnkFirstPage_Click"></asp:linkbutton></td>
		<td><asp:linkbutton id="lnkPreviousPage" Runat="server" onclick="lnkPreviousPage_Click"></asp:linkbutton></td>
		<td><asp:repeater id="rptPageList" Runat="server">
				<ItemTemplate>
					<asp:LinkButton ID="lnkPageItem" Runat="server" CommandName="ChangePage" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PageNum")%>'>
						<%# DataBinder.Eval(Container.DataItem, "PageNum")%>
					</asp:LinkButton>
				</ItemTemplate>
			</asp:repeater></td>
		<td><asp:linkbutton id="lnkNextPage" Runat="server" onclick="lnkNextPage_Click"></asp:linkbutton></td>
		<td><asp:linkbutton id="lnkLastPage" Runat="server" onclick="lnkLastPage_Click"></asp:linkbutton></td>
	</tr>
</table>
<input id="lblMaxDisplay" type="hidden" name="lblMaxDisplay" runat="server"><input id="lblPageCount" type="hidden" name="lblPageCount" runat="server">
<input id="lblCurrentPage" type="hidden" name="lblCurrentPage" runat="server"><input id="lblPageSize" type="hidden" name="lblPageSize" runat="server">
