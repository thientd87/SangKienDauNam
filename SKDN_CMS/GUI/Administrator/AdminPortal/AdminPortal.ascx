<%@ Register TagPrefix="iiuga" Namespace="iiuga.Web.UI" Assembly="TreeWebControl" %>
<%@ Register TagPrefix="uc1" TagName="Tab" Src="Tab.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TabList" Src="TabList.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Template" Src="Template.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TemplateList" Src="TemplateList.ascx" %>
<%@ Control EnableViewState="True" Language="c#" AutoEventWireup="True" Codebehind="AdminPortal.ascx.cs" Inherits="Portal.GUI.Administrator.AdminPortal.AdminPortal" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register Src="../../EditoralOffice/MainOffce/OnLoad/onload.ascx" TagName="onload"
    TagPrefix="uc2" %>
<link href="/styles/backend_menu.css" rel="stylesheet" type="text/css" />

<table width="100%" cellpadding="0" cellspacing="0" border="0">
	<tr>
		<td valign="top" width="232" class="Menuleft_BoxArea">
		    <table width="100%" cellpadding="0" cellspacing="0" border="0">
		        <tr style="display:none;">
		            <td class="Menuleft_HeadBox" align="center">HỆ THỐNG</td>
		        </tr>
		        <tr runat="server" visible="false" id="Tr1">
		            <td valign="top" class="Menuleft_ContentBox">
		                <iiuga:treeweb id="tree" runat="server" CollapsedElementImage="Images/Icons/plus.jpg" ExpandedElementImage="Images/Icons/minus.jpg">
				            <ImageList>
					            <iiuga:ElementImage ImageURL="Images/Icons/Bullet.gif" />
				            </ImageList>
				            <Elements>
					            <iiuga:treeelement text="Portal" CssClass="" />
				            </Elements>
			            </iiuga:treeweb>
		            </td>
		        </tr>
		        <tr style="display:none;"><td>&nbsp;</td></tr>
		        <tr>
		            <td class="Menuleft_HeadBox">Chức năng khác</td>
		        </tr>
		        <tr>
		            <td valign="top" class="Menuleft_ContentBox">
		                <div class="Menuleft_Item" runat="server" id="div1">
	                        +&nbsp;<asp:HyperLink ID="lnkUsers" runat="server">Quản lý tài khoản</asp:HyperLink>
                        </div>
                        <div class="Menuleft_Item" runat="server" id="div3">
	                        +&nbsp;<asp:HyperLink ID="lnkEditoral" runat="server">Quản lý tác nghiệp</asp:HyperLink>
                        </div>
                         <div class="Menuleft_Item" runat="server" id="div5">
	                        +&nbsp;<asp:HyperLink ID="lnkChuyenMuc" runat="server">Quản lý chuyên mục</asp:HyperLink>
                        </div>
                        
		            </td>
		        </tr>
		        <tr><td>&nbsp;</td></tr>
		        <tr>
		            <td class="Menuleft_HeadBox">Chức năng cá nhân</td>
		        </tr>
		        <tr>
		            <td valign="top" class="Menuleft_ContentBox">
		                <div class="Menuleft_Item" runat="server" id="diva">
	                        +&nbsp;<asp:HyperLink ID="itemAccount" runat="server">Tài khoản</asp:HyperLink>
                        </div>                        
                        <div class="Menuleft_Item" runat="server" id="divc">
	                        +&nbsp;<asp:LinkButton ID="itemLogOut" runat="server" OnClick="itemLogOut_Click">Đăng xuất</asp:LinkButton>
                        </div>
		            </td>
		        </tr>
		    </table>
		</td>
		<td width="8"></td>
		<td valign="top" class="Content_OutBox">
		<uc2:onload ID="Onload1" runat="server" />
		    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="display:none;">
		        <tr>
		            <td class="Content_BoxArea">
                        
		                <uc1:Tab id="TabCtrl" runat="server" OnSave="OnSave" OnCancel="OnCancel" OnDelete="OnDelete"></uc1:Tab>
			            <uc1:TabList id="TabListCtrl" runat="server"></uc1:TabList>
			            <br />
			            <uc1:Template id="TemplateCtrl" runat="server" OnSave="OnSave" OnCancel="OnCancel" OnDelete="OnDelete"></uc1:Template>
			            <uc1:TemplateList id="TemplateListCtrl" runat="server"></uc1:TemplateList>			            
		            </td>
		        </tr>
		    </table>
		</td>
		<td width="4"></td>
	</tr>
</table>
