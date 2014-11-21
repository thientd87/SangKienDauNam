<%@ Control Language="c#" AutoEventWireup="True" Codebehind="TabTree.ascx.cs" Inherits="Portal.GUI.Administrator.TabTree.TabTree" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="iiuga" Namespace="iiuga.Web.UI" Assembly="TreeWebControl" %>
<iiuga:treeweb id="tree" runat="server" CollapsedElementImage="Images/icons/plus.jpg" ExpandedElementImage="Images/icons/minus.jpg">
	<ImageList>
		<iiuga:ElementImage ImageURL="Images/icons/Bullet.gif" />
	</ImageList>
	<Elements>
		<iiuga:treeelement text="Portal" CssClass="" />
	</Elements>
</iiuga:treeweb>
