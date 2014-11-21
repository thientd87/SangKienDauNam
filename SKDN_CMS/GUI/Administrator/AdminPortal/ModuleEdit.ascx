<%@ Register TagPrefix="portal" assembly="Portal.API" Namespace="Portal.API.Controls" %>
<%@ Register TagPrefix="uc1" TagName="Roles" Src="Roles.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ModuleEdit.ascx.cs" Inherits="Portal.GUI.Administrator.AdminPortal.ModuleEdit" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div class="ModuleTitle" style="BORDER-BOTTOM: black 1px solid">
	<portal:Label runat="server" LanguageRef="ModuleData" id="Label1"></portal:Label>
</div>
<portal:LinkButton ID="lnkSave" Runat="server" CssClass="LinkButton" OnClick="OnSave" CausesValidation="True"
	LanguageRef="Save"></portal:LinkButton>
|
<portal:LinkButton ID="lnkCancel" Runat="server" CssClass="LinkButton" OnClick="OnCancel" CausesValidation="False"
	LanguageRef="Cancel"></portal:LinkButton>
|
<portal:LinkButton ID="lnkDelete" Runat="server" CssClass="LinkButton" OnClick="OnDelete" CausesValidation="False"
	LanguageRef="Delete"></portal:LinkButton>
<br>
<br>
<asp:Label ID="lbError" Runat="server" CssClass="Error" EnableViewState="False"></asp:Label>
<asp:ValidationSummary EnableClientScript="False" ID="validation" Runat="server"></asp:ValidationSummary>
<table width="100%" cellpadding="0" cellspacing="0">
	<tr>
		<td valign="top">
			<table width="100%" cellpadding="0" cellspacing="0">
				<tr>
					<td class="Label" width="40%">
						<portal:Label runat="server" LanguageRef="Title" id="Label2"></portal:Label>
					</td>
					<td class="Data" colspan="2"><asp:TextBox ID="txtTitle" Runat="server" Width="100%"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="Label" nowrap>
						<portal:Label runat="server" LanguageRef="Reference" id="Label3"></portal:Label><portal:RequiredFieldValidator EnableClientScript="False" ID="validator1" Runat="server" ControlToValidate="txtReference"
							LanguageRef="ReferenceRequired">*</portal:RequiredFieldValidator>
					</td>
					<td class="Data" colspan="2"><asp:TextBox ID="txtReference" Runat="server" Width="100%"></asp:TextBox></td>
				</tr>
				<tr>
					<td class="Label"><portal:Label runat="server" LanguageRef="Type" id="Label4"></portal:Label><portal:CustomValidator ID="validator2" Runat="server" OnServerValidate="OnValidateCBType" EnableClientScript="False"
							LanguageRef="TypeRequired">*</portal:CustomValidator>
					</td>
					<td class="Data"><asp:DropDownList ID="cboPath" Runat="server" AutoPostBack="true" Width="99%" OnSelectedIndexChanged="cboPath_SelectedIndexChanged"></asp:DropDownList></td>
					<td class="Data"><asp:DropDownList ID="cbType" Runat="server" Width="99%"></asp:DropDownList></td>
				</tr>
				<tr>
					<td class="Label"><portal:Label runat="server" LanguageRef="CacheTime" id="Label5"></portal:Label></td>
					<td class="Data" colspan="2"><asp:TextBox ID="txtCacheTime" Runat="server" Width="100%"></asp:TextBox></td>
				</tr>
				<asp:Repeater ID="rptRuntimeProperties" Runat="server">
					<HeaderTemplate>
						<tr>
							<td class="RuntimeHeader" colspan="3">
								<portal:Label ID="Label6" LanguageRef="RuntimeProperties" runat="server"></portal:Label></td>
						</tr>
					</HeaderTemplate>
					<ItemTemplate>
						<tr>
							<td class="Label">
								<asp:Literal ID="ltrPropertyCaption" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Caption") %>'>
								</asp:Literal>
							</td>
							<td class="Data" colspan="2">
								<asp:TextBox ID="txtPropertyValue" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Value") %>' MaxLength="255" Width="100%">
								</asp:TextBox>
								<input type="hidden" id="lblPropertyName" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "Name") %>' NAME="lblPropertyName"/>
								<asp:DropDownList ID="drdAvaiableValues" Runat="server"></asp:DropDownList>
							</td>
						</tr>
					</ItemTemplate>
				</asp:Repeater>
			</table>
		</td>
		<td width="20">&nbsp;</td>
		<td valign="top">
			<uc1:Roles id="RolesCtrl" runat="server"></uc1:Roles>
		</td>
	</tr>
</table>
<br>