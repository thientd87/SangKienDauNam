<%@ Control EnableViewState="True" Language="c#" AutoEventWireup="True" Codebehind="Tab.ascx.cs" Inherits="Portal.GUI.Administrator.AdminPortal.Tab" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="portal" assembly="Portal.API" Namespace="Portal.API.Controls" %>
<%@ Register TagPrefix="uc1" TagName="ListColumn" Src="ColumnList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Column" Src="ColumnEdit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ModuleList" Src="ModuleList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Module" Src="ModuleEdit.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Roles" Src="Roles.ascx" %>
<script type="text/javascript">
    function ShowTemplatePreview()
    {
        var ddlTemplate = document.getElementById("tab__ctl16_TabCtrl_ddrTemplateList");
        window.open("/GUI/Administrator/AdminPortal/TemplatePreview.aspx?TemplateReference=" + ddlTemplate.options[ddlTemplate.selectedIndex].value ,"TemplateReview","resizable,width=800,height=600,scrollbar=yes");                
    }
</script>
<div class="ModuleTitle" style="BORDER-BOTTOM:#CCCCCC 1px solid">
	<portal:Label runat="server" LanguageRef="TabData" ID="Label3" NAME="Label3"></portal:Label>
</div>
<portal:LinkButton ID="lnkSave" Runat="server" CssClass="LinkButton" OnClick="OnSave" CausesValidation="True" LanguageRef="Save"></portal:LinkButton>
|
<portal:LinkButton ID="lnkCancel" Runat="server" CssClass="LinkButton" OnClick="OnCancel" CausesValidation="False" LanguageRef="Cancel"></portal:LinkButton>
|
<portal:LinkButton ID="lnkDelete" Runat="server" CssClass="LinkButton" OnClick="OnDelete" CausesValidation="False" LanguageRef="Delete"></portal:LinkButton>
|
<portal:LinkButton ID="lnkSaveTemplate" Runat="server" CssClass="LinkButton" OnClick="OnSaveTemplate" CausesValidation="False" LanguageRef="SaveTemplate"></portal:LinkButton>
<br>
<br>
<asp:Label ID="lbError" Runat="server" CssClass="Error" EnableViewState="False"></asp:Label>
<asp:ValidationSummary EnableClientScript="False" ID="validation" Runat="server"></asp:ValidationSummary>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
	<tr>
		<td width="70%" valign="top">
			<table width="100%" cellpadding="0" cellspacing="0" border="1" BorderColor="#B8C1CA" style="border-collapse:collapse">
				<tr>
					<td class="Label">
						<portal:Label runat="server" LanguageRef="Title" ID="Label1" NAME="Label1"></portal:Label>
						<portal:RequiredFieldValidator EnableClientScript="False" ID="Requiredfieldvalidator1" Runat="server" ControlToValidate="txtTitle" LanguageRef="TitleRequired">*</portal:RequiredFieldValidator>
					</td>
					<td class="Data">
						<asp:TextBox ID="txtTitle" Runat="server" Width="100%"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="Label">
						<portal:Label runat="server" LanguageRef="Reference" ID="Label2" NAME="Label2">
						</portal:Label><portal:RequiredFieldValidator EnableClientScript="False" ID="validator1" Runat="server" ControlToValidate="txtReference" LanguageRef="ReferenceRequired">*</portal:RequiredFieldValidator>
					</td>
					<td class="Data">
						<asp:TextBox ID="txtReference" Runat="server" Width="100%"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td class="Label">
						<portal:Label runat="server" LanguageRef="SkinName" id="Label4"></portal:Label>
					</td>
					<td>
						<asp:DropDownList Runat="server" ID="ddrSkins"></asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td class="Label">
						<portal:Label runat="server" LanguageRef="IsHidden" id="Label5"></portal:Label>
					</td>
					<td>
						<asp:CheckBox Runat="server" ID="chkIsHidden"></asp:CheckBox>
					</td>
				</tr>
				<tr>
				    <td class="Label">
				        <portal:Label runat="server" LanguageRef="ApplyTemplateToTab" id="Label6"></portal:Label>
				    </td>
				    <td>
				        <asp:DropDownList Runat="server" ID="ddrTemplateList"></asp:DropDownList>
				        |
				        <portal:LinkButton ID="lnkPreview" Runat="server" CssClass="LinkButton" CausesValidation="False" LanguageRef="PreviewTemplate" OnClientClick="ShowTemplatePreview()"></portal:LinkButton>
				    </td>
				</tr>
			</table>
		</td>
		<td width="20">&nbsp;</td>
		<td valign="top">
			<uc1:Roles id="RolesCtrl" runat="server" ShowRoleType="False"></uc1:Roles>
		</td>
	</tr>
</table>
<uc1:Module id="ModuleEditCtrl" runat="server" Visible="False" OnCancel="OnCancelEditModule"
	OnDelete="OnDeleteModule" OnSave="OnSaveModule"></uc1:Module>
<uc1:Column id="ColumnEditCtrl" runat="server" Visible="False" OnCancel="OnCancelEditColumn"
	OnDelete="OnDeleteColumn" OnSave="OnSaveColumn"></uc1:Column>
<uc1:ListColumn id="lstColumns" runat="server" Visible="True" OnAddColumn="OnAddColumn" OnAddSubColumn="OnAddColumn"></uc1:ListColumn>
<uc1:ModuleList id="lstModules" runat="server" Visible="False" TitleLanguageRef="ModulesList"></uc1:ModuleList>
