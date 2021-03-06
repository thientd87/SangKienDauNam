<%@ Page Language="C#" AutoEventWireup="true" Codebehind="blockedit.aspx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.editnews.blockedit" %>

<%@ Register TagPrefix="editor" Assembly="WYSIWYGEditor" Namespace="InnovaStudio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Sửa đoạn văn bản</title>
	<link rel="stylesheet" href="/styles/common.css" type="text/css">
	<link rel="stylesheet" type="text/css" href="StyleSheet1.css" />

	<script type="text/javascript" src="library.js"></script>

</head>
<body>
	<form id="form1" runat="server">
		<table cellpadding="5" cellspacing="0">
			<tr align="center" valign="top">
				<td>
					<editor:WYSIWYGEditor scriptPath="/Scripts/editor/" ID="NewsContent" Language="vietnamese"
						runat="server" />
				</td>
				<td>
					<table cellpadding="5" rules="all" cellspacing="0" style="border-collapse: collapse;
						border: solid 1px #c0c0c0;">
						<tr>
							<td style="background-color: #d0d0d0; font-weight: bold; text-align: center;" align="center">
								Vị trí khối văn bản
							</td>
						</tr>
						<tr>
							<td>
								<table>
									<tr>
										<td>
											Vị trí
										</td>
										<td>
											<select id="Select2">
												<option value="">Giữa</option>
												<option value="left">Trái</option>
												<option value="right">Phải</option>
											</select>
										</td>
									</tr>
									<tr>
										<td>
											Height
										</td>
										<td>
											<input id="Text6" type="text" />
										</td>
									</tr>
									<tr>
										<td>
											Width
										</td>
										<td>
											<input id="Text7" type="text" />
										</td>
									</tr>
									<tr>
										<td>
											Padding
										</td>
										<td>
											<input id="Text8" type="text" />
										</td>
									</tr>
									<tr>
										<td>
											Margin
										</td>
										<td>
											<input id="Text9" type="text" />
										</td>
									</tr>
									<tr>
										<td>
											Border
										</td>
										<td>
											<input id="Text10" type="text" />
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
									<a href="javascript:void();" class="savecontent saveblock" onclick="saveBlock();return false;">
										Save block</a>
								</td>
								<td>
									<a href="javascript:void();" class="cancel canelblock" onclick="cancelEditBlock();return false;">
										Cancel</a></td>
							</tr>
						</table>
					</center>
				</td>
			</tr>
		</table>
		<asp:Literal ID="js" runat="server"></asp:Literal>
		<div style="display: none;">
			<asp:Label ID="arg1" runat="server" Text=""></asp:Label>
			<asp:Label ID="arg2" runat="server" Text=""></asp:Label></div>

		<script type="text/javascript">
		
		var editor = ID<%= NewsContent.ClientID %>; // javascript innova editor
		
		
		function saveBlock()
			{
				if (opener)
				{
					opener.block_render(document.getElementById("idContent"+editor.oName).contentWindow.document.body.innerHTML,
						document.getElementById('Select2').options[document.getElementById('Select2').selectedIndex].value,
						document.getElementById('Text6').value,
						document.getElementById('Text7').value,
						document.getElementById('Text8').value,
						document.getElementById('Text9').value,
						document.getElementById('Text10').value);
					
					opener.resizeBGLeftAndRight();
					window.close();
				}
			}
			
		function cancelEditBlock()
			{
				if (opener)
				{
					window.close();
				}
			}
			
		function block_bind(floatvalue, height, width, padding, margin, border, html)
		{
			document.getElementById('Text6').value = height;
			document.getElementById('Text7').value = width;
			document.getElementById('Text8').value = padding;
			document.getElementById('Text9').value = margin;
			document.getElementById('Text10').value = border;
			setValueDropdownlist(document.getElementById('Select2'), floatvalue);
			
			editor.loadHTML(html);
		}
		onunload = function()
			{
				if (opener)
				{
					opener.hideBG();
				}
			}
		
		//onload = function()
		if (opener)
		{
			var cssFloat = opener.currentEditBlock.style.cssFloat;
			
			// find block content
			var span, spans = opener.currentEditBlock.getElementsByTagName('span');
			for (var i=0; i<spans.length; i++)
			{
				span = spans[i];
				if (span.className == 'content') break;
			}
			
			var html = span.innerHTML;
			
			if (!cssFloat) cssFloat = opener.currentEditBlock.style.float;
			block_bind(cssFloat,
				opener.currentEditBlock.style.height,
				opener.currentEditBlock.style.width,
				opener.currentEditBlock.style.padding,
				opener.currentEditBlock.style.margin,
				opener.currentEditBlock.style.border,
				html);
				
			opener.showBG();
		}
		
		</script>

	</form>
</body>
</html>
