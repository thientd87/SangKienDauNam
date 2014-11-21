<%@ Register TagPrefix="portal" assembly="Portal.API" Namespace="Portal.API.Controls" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="GenerateTabs.ascx.cs" Inherits="Portal.GUI.Administrator.GenerateTabs.GenerateTabs" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script>
	function ShowHide(gridID)
	{
		if (gridID.style.display == "block")
			gridID.style.display = "none";
		else
			gridID.style.display = "block";
	}
</script>
<link href="/styles/backend_menu.css" rel="stylesheet" type="text/css" />
<asp:placeholder id="plhEditionsList" Runat="server">
	<TABLE class="TabGeneration_Table" cellSpacing="0" cellPadding="0" width="100%" align="center"
		border="0">
		<TR>
			<TD>
				<asp:datagrid id="dgrEditionTypes" Runat="server" AutoGenerateColumns="False" ShowHeader="False"
					BorderWidth="0" CellSpacing="0" CellPadding="0" Width="100%" HorizontalAlign="Center">
					<Columns>
						<asp:TemplateColumn>
							<ItemTemplate>
								<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" Class="EditionList_Table">
									<TR>
										<TD class="EditionList_Header">Tên trang chủ</TD>
										<TD class="EditionList_Item">
											<asp:Literal id=ltrEditionTypeName Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EditionName") %>'></asp:Literal></TD>
									</TR>
									<tr>
										<TD class="EditionList_Header">Đường dẫn đến</TD>
										<TD class="EditionList_Item">
											<asp:Literal id=ltrEditionTypeURL Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EditionDisplayURL") %>'>
											</asp:Literal></TD>
									</tr>
									<tr>
										<td colspan="2" class="EditionList_HeaderSeparator">&nbsp;</td>
									</tr>
									<TR>
										<TD colSpan="2" class="CategoriesList_TopHeader">
											Chuyên mục chính&nbsp;&nbsp;--&nbsp;
											<a href="/category/editcat.aspx">Thêm một chuyên mục chính</a>
										</TD>
									</TR>
									<tr>
										<td colspan="2">
											<asp:DataGrid id="dgrCatList" AutoGenerateColumns="False" Runat="server" CssClass="InnerCategoriesList_Table"
												BorderWidth="0" CellSpacing="0" CellPadding="0" HorizontalAlign="Center" ShowHeader="False">
												<Columns>
													<asp:TemplateColumn>
														<ItemTemplate>
															<table cellpadding="0" cellspacing="0" border="0" width="100%" class="InnerCategoriesList_TableItem">
																<tr>
																	<td class="CategoriesList_Header" width="200">Tên chuyên mục</td>
																	<td class="CategoriesList_Item">
																		<asp:Literal Runat="server" ID="ltrTopCategoryName" Text='<%# DataBinder.Eval(Container.DataItem, "Cat_Name") %>'>
																		</asp:Literal></td>
																	<td class="CategoriesList_Item" width="100" align="right">
																		<img src="~/Images/Snap/i_closed.gif" align="absmiddle" border="0" style="cursor:hand" runat="server" id="imgShowHideSubCatList" />
																	</td>
																</tr>
																<tr>
																	<td class="CategoriesList_Header">Tên hiển thị trên đường dẫn</td>
																	<td class="CategoriesList_Item">
																		<asp:Literal Runat="server" ID="ltrTopCategoryURL" Text='<%# DataBinder.Eval(Container.DataItem, "Cat_DisplayURL") %>'>
																		</asp:Literal></td>
																	<td class="CategoriesList_Item" align="right">
																		<asp:ImageButton Runat="server" ID="imgMoveUp" ImageUrl="~/Images/icons/Up.gif" ImageAlign="AbsMiddle" AlternateText="Chuyển thứ tự lên 1 mức" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Cat_ID") %>' OnCommand="MoveCategoryUp">
																		</asp:ImageButton>
																		<asp:ImageButton Runat="server" ID="imgMoveDown" ImageUrl="~/Images/icons/Down.gif" ImageAlign="AbsMiddle" AlternateText="Chuyển thứ tự xuống 1 mức" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Cat_ID") %>' OnCommand="MoveCategoryDown">
																		</asp:ImageButton>
																		<!--
																		<a href="/category/editcat.aspx?cat=<%# DataBinder.Eval(Container.DataItem, "Cat_ID") %>"><img alt="Sửa" src="/images/icons/Edit.gif" border="0"></a>
																		-->
																		<a href="/category/editcat/<%# DataBinder.Eval(Container.DataItem, "Cat_ID") %>.aspx"><img alt="Sửa" src="/images/icons/Edit.gif" border="0"></a>
																	</td>
																</tr>
																<tr>
																	<td colspan="3" class="CategoriesList_Header"></td>
																</tr>
																<tr>
																	<td colspan="3" style="display:none;" runat="server" id="htcSubCatList">
																		<table cellpadding="0" cellspacing="0" width="100%">
																			<tr>
																				<td Class="CategoriesList_SubHeader">Chuyên mục con</td>
																				<td Class="CategoriesList_SubHeader" align="right">
																					<a href="/category/editcat.aspx?parent=<%# DataBinder.Eval(Container.DataItem, "Cat_ID").ToString()%>">Thêm chuyên mục con</a>
																			    </td>
																			</tr>
																			<tr>
																				<td colspan="2">
																					<asp:DataGrid id="dgrSubCategories" AutoGenerateColumns="False" Runat="server" CssClass="InnerSubCategoriesList_Table"
																						BorderWidth="0" CellSpacing="0" CellPadding="0" HorizontalAlign="Center" ShowHeader="False">
																						<Columns>
																							<asp:TemplateColumn ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
																								<ItemStyle CssClass="CategoriesList_SubItem"></ItemStyle>
																								<ItemTemplate>
																								    <a href="/category/editcat/<%# DataBinder.Eval(Container.DataItem, "SubCat_ID") %>.aspx"><img src="/images/icons/Edit.gif" alt="Sửa"></a>
																									<asp:ImageButton Runat="server" ImageUrl="~/Images/icons/Up.gif" ImageAlign="AbsMiddle" AlternateText="Chuyển thứ tự lên 1 mức" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SubCat_ID") %>' OnCommand="MoveCategoryUp">
																									</asp:ImageButton>
																									<asp:ImageButton Runat="server" ImageUrl="~/Images/icons/Down.gif" ImageAlign="AbsMiddle" AlternateText="Chuyển thứ tự xuống 1 mức" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SubCat_ID") %>' OnCommand="MoveCategoryDown">
																									</asp:ImageButton>
																								</ItemTemplate>
																							</asp:TemplateColumn>
																							<asp:TemplateColumn>
																								<ItemStyle CssClass="CategoriesList_SubItem"></ItemStyle>
																								<ItemTemplate>
																									<asp:Literal Runat="server" ID="ltrCategoryName" Text='<%# DataBinder.Eval(Container.DataItem, "SubCat_Name") %>'>
																									</asp:Literal>
																								</ItemTemplate>
																							</asp:TemplateColumn>
																							<asp:TemplateColumn>
																								<ItemStyle CssClass="CategoriesList_SubItem"></ItemStyle>
																								<ItemTemplate>
																									<asp:Literal Runat="server" ID="ltrCategoryURL" Text='<%# DataBinder.Eval(Container.DataItem, "SubCat_DisplayURL") %>'>
																									</asp:Literal>
																								</ItemTemplate>
																							</asp:TemplateColumn>
																						</Columns>
																					</asp:DataGrid>
																				</td>
																			</tr>
																		</table>
																	</td>
																</tr>
															</table>
														</ItemTemplate>
													</asp:TemplateColumn>
												</Columns>
											</asp:DataGrid></td>
									</tr>
								</TABLE>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:datagrid></TD>
		</TR>
		<TR>
			<TD>
				<TABLE cellSpacing="0" cellPadding="0" width="50%" align="center">
					<TR>
						<TD class="GenTab_SelectTab">
							<portal:Literal id="Literal1" runat="server" LanguageRef="CurrentTemplateTabReference"></portal:Literal></TD>
						<TD class="GenTab_ActionCell">
							<asp:Literal id="ltrCurrentTemplateTabReference" Runat="server"></asp:Literal></TD>
					</TR>
					<TR>
						<TD class="GenTab_ActionCell">
							<portal:LinkButton id="btnApplyTemplateToAllTab" runat="server" LanguageRef="GenAllTabsButton" CssClass="Portal_Button" onclick="btnApplyTemplateToAllTab_Click"></portal:LinkButton></TD>
						<TD class="GenTab_ActionCell">
							<portal:LinkButton id="btnApplyTemplateToAllEditionTab" runat="server" LanguageRef="GenEditionTabsButton"
								CssClass="Portal_Button" onclick="btnApplyTemplateToAllEditionTab_Click"></portal:LinkButton><BR>
						</TD>
					</TR>
					<TR>
						<TD class="GenTab_ActionCell">
							<portal:LinkButton id="btnApplyTemplateToAllParentCategoryTab" runat="server" LanguageRef="GenParentCategoryTabsButton"
								CssClass="Portal_Button" onclick="btnApplyTemplateToAllParentCategoryTab_Click"></portal:LinkButton></TD>
						<TD class="GenTab_ActionCell">
							<portal:LinkButton id="btnApplyTemplateToAllSubCategoryTab" runat="server" LanguageRef="GenSubCategoryTabsButton"
								CssClass="Portal_Button" onclick="btnApplyTemplateToAllSubCategoryTab_Click"></portal:LinkButton><BR>
						</TD>
					</TR>
					<TR>
						<TD class="GenTab_SelectTab" colSpan="2">
							<portal:Literal id="Literal2" runat="server" LanguageRef="GenParentCategoryApplyToCustomTab"></portal:Literal>&nbsp;
							<asp:DropDownList id="drdTabsList" Runat="server"></asp:DropDownList></TD>
					</TR>
					<TR>
						<TD class="GenTab_ActionCell" colSpan="2">
							<portal:LinkButton id="btnApplyTemplateToCustomTab" runat="server" LanguageRef="GenCustomTabButton"
								CssClass="Portal_Button" onclick="btnApplyTemplateToCustomTab_Click"></portal:LinkButton></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
	</TABLE>
</asp:placeholder>
