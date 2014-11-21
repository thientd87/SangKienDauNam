<%@ Control Language="c#" EnableViewState="False" AutoEventWireup="True" Codebehind="OverlayMenu.ascx.cs" Inherits="DFISYS.GUI.Share.OverlayMenu" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table id="MenuRoot" runat="server" class="OverlayMenuRoot">
	<tr>
		<td nowrap><a href="javascript:OpenOverlayMenu(document.all.<%=Menu.ClientID%>, document.all.<%=MenuRoot.ClientID%>)" class="<%=CssClass%>"><asp:Literal ID="ltrDisplayRootText" Runat="server"><img src="Images/Snap/overlay.gif" border="0" align="absmiddle" /></asp:Literal><%=RootText%></a></td>
	</tr>
</table>
<table id="Menu" runat="server" style="DISPLAY: none; POSITION: absolute" class="OverlayMenu">
	<tr>
		<td>
			<table>
				<asp:Repeater id="MenuRepeater" Runat="server">
					<ItemTemplate>
						<tr>
							<td>
								<table 
									width="100%"
									cellpadding="0"
									cellspacing="0"
									runat="server"
									visible='<%# Container.DataItem as DFISYS.GUI.Share.OverlayMenuSeparatorItem == null && ((DFISYS.GUI.Share.OverlayMenuItem)Container.DataItem).Visible %>'
									class="OverlayMenuItem"
									onmouseover="javascript:OverlayMenuOnMouseOver(this)" 
									onmouseout="javascript:OverlayMenuOnMouseOut(this)"									
									onclick='<%# Page.GetPostBackClientHyperlink(this, DataBinder.Eval(Container.DataItem, "MenuItemIndex").ToString() ) %>'>
									<tr>
										<td width="16px"><img src='<%# DataBinder.Eval(Container.DataItem, "Icon") %>' ></td>
										<td width="5px">&nbsp;</td>
										<td nowrap width="100%"><%# DataBinder.Eval(Container.DataItem, "Text") %></td>
									</tr>
								</table>
								<table 
									cellpadding="0"
									cellspacing="0"
									width="100%"
									runat="server"
									visible='<%# Container.DataItem as DFISYS.GUI.Share.OverlayMenuSeparatorItem != null && ((DFISYS.GUI.Share.OverlayMenuItem)Container.DataItem).Visible %>'>
									<TR>
										<TD colspan="3">
											<hr class="OverlayMenuSeparator">
										</TD>
									</TR>
								</table>
							</td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>
			</table>
		</td>
	</tr>
</table>

