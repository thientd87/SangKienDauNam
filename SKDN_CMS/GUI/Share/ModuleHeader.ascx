<%@ Control Language="c#" EnableViewState="False" AutoEventWireup="false" Codebehind="ModuleHeader.ascx.cs" Inherits="DFISYS.GUI.Share.ModuleHeader" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="portal" assembly="Portal" Namespace="Portal" %>
<%@ Register TagPrefix="ucm" TagName="OVM" Src="OverlayMenu.ascx" %>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
	<tr>		
		<td align="right">
			<!-- Edit Link for non Admins -->
			<portal:EditLink Class="LinkButton" id="lnkEditLink" runat="server"></portal:EditLink>			
		</td>
	</tr>
</table>
