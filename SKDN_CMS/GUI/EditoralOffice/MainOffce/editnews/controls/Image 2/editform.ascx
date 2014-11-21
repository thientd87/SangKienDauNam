<%@ Control Language="C#" AutoEventWireup="true" Codebehind="editform.ascx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.editnews.controls.slideshow.editform" %>
<table>
	<asp:Repeater ID="rptSlideshow" runat="server">
		<ItemTemplate>
			<tr>
				<td>
					<img src="<%# PrefixURL + Eval("PictureURL") %>" /></td>
				<td>
					<asp:Button ID="btnRemove" OnClick="btnRemove_Click" runat="server" CommandArgument='<%# Eval("PictureURL") %>'
						Text="Remove" /></td>
			</tr>
		</ItemTemplate>
	</asp:Repeater>
	<tr>
		<td>
			<asp:FileUpload ID="fUpload" CssClass="borderinput" runat="server" /></td>
		<td>
			<asp:Button ID="btnAdd" runat="server" Text="Add new" OnClick="btnAdd_Click" /></td>
	</tr>
</table>
