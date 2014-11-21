<%@ Control EnableViewState="True" Language="c#" AutoEventWireup="True" Codebehind="ColumnEdit.ascx.cs" Inherits="Portal.GUI.Administrator.AdminPortal.ColumnEdit" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="Roles" Src="Roles.ascx" %>
<%@ Register TagPrefix="portal" assembly="Portal.API" Namespace="Portal.API.Controls" %>
<div class="SectionHeader">
	<portal:Label runat="server" LanguageRef="ColumnData" ID="lblColumnData"></portal:Label>
</div>
<portal:LinkButton ID="lnkSaveColumn" Runat="server" CssClass="LinkButton" OnClick="OnSave" CausesValidation="True"
	LanguageRef="Save"></portal:LinkButton>
|
<portal:LinkButton ID="lnkCancelColumn" Runat="server" CssClass="LinkButton" OnClick="OnCancel" CausesValidation="False"
	LanguageRef="Cancel"></portal:LinkButton>
|
<portal:LinkButton ID="lnkDeleteColumn" Runat="server" CssClass="LinkButton" OnClick="OnDelete" CausesValidation="False"
	LanguageRef="Delete"></portal:LinkButton>
<br>
<br>
<asp:Label ID="lbError" Runat="server" CssClass="Error" EnableViewState="False"></asp:Label>
<asp:ValidationSummary EnableClientScript="False" ID="validation" Runat="server"></asp:ValidationSummary>
<table width="100%" cellpadding="0" cellspacing="0">
	<tr>
		<td class="Label">
			<portal:Label runat="server" LanguageRef="ColumnReference" ID="Label3"></portal:Label>
		</td>
		<td class="Data">
			<asp:Literal ID="ltrColumnReference" Runat="server"></asp:Literal>
		</td>
	</tr>
	<tr>
		<td class="Label" width="100">
			<portal:Label runat="server" LanguageRef="ColumnName" ID="Label2" NAME="Label2"></portal:Label>
			<portal:RequiredFieldValidator EnableClientScript="False" ID="validator2" Runat="server" ControlToValidate="txtTitle"
				LanguageRef="ReferenceRequired">*</portal:RequiredFieldValidator>
		</td>
		<td class="Data"><asp:TextBox ID="txtTitle" Runat="server" Width="100%"></asp:TextBox></td>
	</tr>
	<tr>
		<td class="Label">
			<portal:Label runat="server" LanguageRef="ColumnWidth" ID="Label1"></portal:Label>
		</td>
		<td class="Data">
			<portal:CheckBox runat="server" ID="chkDefaultColumnWidth" LanguageRef="DefaultColumnWidth"></portal:CheckBox>
		</td>
	</tr>
	<tr>
		<td class="Label">
			<portal:Label runat="server" LanguageRef="ColumnCustomWidth"></portal:Label>
		</td>
		<td class="Data">
			<asp:TextBox ID="txtColWidth" runat="server" Width="30" MaxLength="4"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td class="Label">
			<portal:Label runat="server" LanguageRef="ColumnBackgroundColor" ID="Label5"></portal:Label>
		</td>
		<td class="Data">
			<asp:TextBox ID="txtCustomStyle" TextMode="MultiLine" Rows="5" Runat="server" Width="100%"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td class="Label">
			<portal:Label runat="server" LanguageRef="ColumnStartLevel" ID="Label4"></portal:Label>
		</td>
		<td class="Data">
			<asp:DropDownList ID="drdColumnLevel" Runat="server"></asp:DropDownList>
		</td>
	</tr>
</table>
<br>
