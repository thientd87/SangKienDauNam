<%@ Control Language="C#" AutoEventWireup="true" Codebehind="slideshow.ascx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.editnews.controls.slideshow.slideshow" %>
<center>
	<asp:Repeater ID="rptSlideshow" runat="server">
		<ItemTemplate>
			<img src="<%# PrefixURL + Eval("PictureURL") %>" />
		</ItemTemplate>
	</asp:Repeater>
</center>
