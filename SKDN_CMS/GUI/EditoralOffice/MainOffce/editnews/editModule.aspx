<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="true" Codebehind="editModule.aspx.cs"
	Inherits="Portal.GUI.EditoralOffice.MainOffce.editnews.editModule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Module chức năng</title>
	<link rel="stylesheet" href="/styles/common.css" type="text/css">
	<link rel="stylesheet" type="text/css" href="StyleSheet1.css" />
	<link rel="stylesheet" type="text/css" href="/styles/backend.css" />

<%--	<script type="text/javascript" src="/Scripts/ContextMenu/prototype-1.6.0.2.js"></script>--%>

	<script type="text/javascript" src="/Scripts/library.js"></script>

	<script src="/scripts/backend.js"></script>

	<script type="text/javascript" src="library.js"></script>

	<script type="text/javascript">var btnPostBackId = null;</script>

	<asp:literal id="ltrStyleSheet" runat="server"></asp:literal>
</head>
<body>
	<form id="form1" runat="server">
		<table cellpadding="5" cellspacing="0" width="100%">
			<tr align="center" valign="top">
				<td>
					<table cellpadding="5" rules="all" cellspacing="0" style="border-collapse: collapse;
						border: solid 1px #c0c0c0;" width="100%">
						<tr>
							<td style="background-color: #d0d0d0; font-weight: bold; text-align: center;" align="center">
								Cấu hình module chức năng
							</td>
						</tr>
						<tr>
							<td>
								<div runat="server" id="container">
								</div>
							</td>
						</tr>
					</table>
				</td>
				<td>
					<table cellpadding="5" rules="all" cellspacing="0" style="border-collapse: collapse;
						border: solid 1px #c0c0c0;">
						<tr>
							<td style="background-color: #d0d0d0; font-weight: bold; text-align: center;" align="center">
								Vị trí module chức năng
							</td>
						</tr>
						<tr style="background-color: White;">
							<td>
								<table>
									<tr>
										<td>
											Vị trí
										</td>
										<td>
											<asp:DropDownList ID="Select2" runat="server">
												<asp:ListItem Text="Giữa" Value="none"></asp:ListItem>
												<asp:ListItem Text="Trái" Value="left"></asp:ListItem>
												<asp:ListItem Text="Phải" Value="right"></asp:ListItem>
											</asp:DropDownList>
										</td>
									</tr>
									<tr>
										<td>
											Chiều cao
										</td>
										<td>
											<asp:TextBox ID="Text6" onfocus="checkStyleValue('mặc định (auto)', '', this)" onblur="checkStyleValue('', 'mặc định (auto)', this)"
												runat="server">mặc định (auto)</asp:TextBox>
										</td>
									</tr>
									<tr>
										<td>
											Chiều rộng
										</td>
										<td>
											<asp:TextBox ID="Text7" onfocus="checkStyleValue('mặc định (auto)', '', this)" onblur="checkStyleValue('', 'mặc định (auto)', this)"
												runat="server">mặc định (auto)</asp:TextBox>
										</td>
									</tr>
									<tr>
										<td>
											Cách lề
										</td>
										<td>
											<asp:TextBox ID="Text8" onfocus="checkStyleValue('mặc định (0px)', '', this)" onblur="checkStyleValue('', 'mặc định (0px)', this)"
												runat="server">mặc định (0px)</asp:TextBox>
										</td>
									</tr>
									<tr style="display: none;">
										<td>
											Margin
										</td>
										<td>
											<asp:TextBox ID="Text9" onfocus="checkStyleValue('mặc định (0px)', '', this)" onblur="checkStyleValue('', 'mặc định (0px)', this)"
												runat="server">mặc định (0px)</asp:TextBox>
										</td>
									</tr>
									<tr>
										<td>
											Viền
										</td>
										<td>
											<asp:TextBox ID="Text10" onfocus="checkStyleValue('mặc định (none)', '', this)" onblur="checkStyleValue('', 'mặc định (none)', this)"
												runat="server">mặc định (none)</asp:TextBox>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr align="center">
				<td align="center" colspan="2">
					<center>
						<table>
							<tr>
								<td>
									<a href="javascript:void();" id="btnSaveModule" class="button save">Save module</a>
								</td>
								<td>
									<a href="javascript:void();" class="button close" onclick="cancelEditModule();return false;">
										Đóng cửa sổ</a></td>
							</tr>
						</table>
					</center>
				</td>
			</tr>
		</table>
		<div id="presentationContainer" runat="server" style="display: none;">
		</div>
		<div style="display: none;">
			<asp:Label ID="arg1" runat="server" Text=""></asp:Label>
			<asp:Label ID="arg2" runat="server" Text=""></asp:Label>
			<asp:TextBox ID="TextBox1" AutoPostBack="true" runat="server"></asp:TextBox>
		</div>

		<script type="text/javascript">
		function doPostBack()
		{
			if (!btnPostBackId)
				saveModule();
			else
			{
				var btnPostBack = document.getElementById(btnPostBackId);
				if (btnPostBack)
				{
					__doPostBack(btnPostBack.name, '');
				}
				else
				{
					saveModule();
				}
			}
			return false;
		}
		function saveModule()
			{
				if (opener)
				{
					opener.module_render(document.getElementById('presentationContainer').innerHTML,
					document.getElementById('Select2').options[document.getElementById('Select2').selectedIndex].value,
					document.getElementById('Text6').value,
					document.getElementById('Text7').value,
					document.getElementById('Text8').value,
					document.getElementById('Text9').value,
					document.getElementById('Text10').value,
					document.getElementById('arg1').innerHTML,
					document.getElementById('arg2').innerHTML);
					
					onunload = null;
					close();
				}
				return false;
			}
			
		function cancelEditModule()
			{
				if (opener)
				{
					window.close();
				}
			}
			
		function module_bind(floatvalue, height, width, padding, margin, border)
		{
			document.getElementById('Text6').value = height == '' ? 'mặc định (auto)' : height;
			document.getElementById('Text7').value = width == '' ? 'mặc định (auto)' : width;
			document.getElementById('Text8').value = padding == '' ? 'mặc định (0px)' : padding;
			document.getElementById('Text9').value = margin == '' ? 'mặc định (0px)' : margin;
			document.getElementById('Text10').value = border == '' ? 'mặc định (none)' : border;
			setValueDropdownlist(document.getElementById('Select2'), floatvalue);
		}
		function enableParent()
			{
				if (opener)
				{
					if (opener.hideBG != 'undefined') opener.hideBG();
				}
			}
		function bindData()
			{
				if (opener)
				{
					var cssFloat = opener.currentEditModule.getAttribute('_float');
					
					module_bind(cssFloat,
						opener.currentEditModule.style.height,
						opener.currentEditModule.style.width,
						opener.currentEditModule.style.padding,
						opener.currentEditModule.style.margin,
						opener.currentEditModule.style.border);
					//opener.showBG();
					
					var btnSaveModule = document.getElementById('btnSaveModule');
					
					btnSaveModule.addEventListener ? btnSaveModule.addEventListener('click', saveModule, false) : btnSaveModule.attachEvent('onclick', saveModule);
				}
			}
		
		window.addEventListener ? window.addEventListener('load', bindData, false) : window.attachEvent('onload', bindData);
		
		</script>

		<asp:Literal ID="js" runat="server"></asp:Literal>
	</form>
</body>
</html>
