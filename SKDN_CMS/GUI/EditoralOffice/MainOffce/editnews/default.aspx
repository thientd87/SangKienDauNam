<%@ Page Language="C#" AutoEventWireup="true" Codebehind="default.aspx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.editnews._default" %>

<%@ Register TagPrefix="editor" Assembly="WYSIWYGEditor" Namespace="InnovaStudio" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
	Namespace="System.Web.UI" TagPrefix="asp" %>
<html>
<head runat="server">
	<title>News editor</title>
	<link rel="stylesheet" type="text/css" href="StyleSheet1.css" />
	<link rel="stylesheet" type="text/css" href="preview/family/StyleSheet1.css" />
	<link rel="stylesheet" href="/styles/common.css" type="text/css" />

	<script type="text/javascript" src="/Scripts/library.js"></script>

	<script type="text/javascript" src="library.js"></script>

	<script type="text/javascript" src="module.js"></script>

	<script type="text/javascript" src="/Scripts/Common.js"></script>

	<script type="text/javascript" src="gui.js"></script>

	<script language="JavaScript" type="text/javascript" src="jquery.js"></script>

	<asp:literal id="ltrStyleSheet" runat="server"></asp:literal>
</head>
<body style="background-color: #d0d0d0;">
	<div id="__mainDiv" style="clear: both; margin: 0px; padding: 0px; text-align: center;">
		<center>
			<form id="form1" runat="server">
				<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server">
				</asp:ScriptManager>
				<div id="editorContainer">
					<editor:WYSIWYGEditor scriptPath="/Scripts/editor/" ID="NewsContent" Width="95%"
						EditorWidth="95%" Language="vietnamese" runat="server" />
					<table style="display: none;">
						<tr align="center" valign="top">
							<td>
							</td>
							<td style="display: none;">
								<table cellpadding="5" rules="all" cellspacing="0" style="border-collapse: collapse;
									border: solid 1px #c0c0c0;">
									<tr>
										<td style="background-color: #d0d0d0; font-weight: bold; text-align: center;" align="center">
											Vị trí khối văn bản
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
															<asp:ListItem Text="Giữa"></asp:ListItem>
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
					</table>
					<center>
						<table>
							<tr>
								<td>
									<a href="javascript:void();" class="savecontent saveblock" onclick="block_save();return false;">
										Lưu lại</a>
								</td>
								<td>
									<a href="javascript:void();" class="cancel canelblock" onclick="block_cancel(); return false;">
										Hủy bỏ</a></td>
							</tr>
						</table>
					</center>
				</div>
				<table rules="none" id="listOfModules" style="display: none;">
					<tr>
						<td>
							<asp:DataList ID="dtlListOfModules" RepeatColumns="1" OnItemDataBound="dtlListOfModules_ItemDataBound"
								runat="server">
								<ItemTemplate>
									<table>
										<tr valign="middle">
											<td style="width: 26px;" align="center">
												<img src="/images/module_clientapp_obj.gif" />
											</td>
											<td>
												<asp:LinkButton ID="btnAddNewModule" Text='<%# Eval("Name") %>' ToolTip='<%# Eval("Path") %>'
													CommandArgument='<%# Eval("Path") %>' runat="server">LinkButton</asp:LinkButton>
											</td>
										</tr>
									</table>
								</ItemTemplate>
							</asp:DataList>
							<br style="clear: both;" />
						</td>
					</tr>
					<%--<tr>
						<td align="center">
							<a href="javascript:void();" class="closelistofmodule" onclick="document.getElementById('listOfModules').style.display = 'none'; hideBG(); return false;">
								Đóng cửa sổ</a>
						</td>
					</tr>--%>
				</table>
				<div style="background-color: #d0d0d0; text-align: center;">
					<center>
						<table cellpadding="0" cellspacing="0" style="width: 694px; background-color: White;
							padding: 10px; margin-top: 70px;">
							<tr style="background-color: #d0d0d0;">
								<td>
									<div id="ijCMSEditor">
										<a href="javascript:void();" title="Thêm một đoạn văn bản mẫu vào cuối trang" class="themmotdoanvanban"
											onclick="addNewSimpleTextParagrahp(); return false;">
											<%--<img src="images/stock_styles-paragraph-styles.png" />--%>
											Thêm một đoạn văn bản</a> <a title="Đóng cửa sổ và quay về chế độ soạn thảo bình thường"
												class="quayvetrangsoantin" href="javascript:void();" onclick="copyContentToEditPage2(); return false;">
												<%--<img src="images/arrow-right.gif" />--%>
												Trở về trang soạn tin</a> <a title="Convert đoạn văn bản được chọn thành đối tượng BLOCK"
													class="quayvetrangsoantin" href="javascript:paragraph_convertToBlock();">
													<%--<img src="images/arrow-right.gif" />--%>
													Chuyển thành BLOCK</a>
										<br style="clear: both;" />
									</div>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="lblTitleNews" CssClass="titleNew" runat="server"></asp:Label>
									<asp:Panel ID="panel" runat="server" CssClass="workarea">
									</asp:Panel>
								</td>
							</tr>
						</table>
					</center>
				</div>
				<div id="bgoverlay">
				</div>
				<asp:HiddenField ID="customArg" EnableViewState="false" runat="server" />
				<asp:HiddenField ID="customArg2" EnableViewState="false" runat="server" />
				<asp:HiddenField ID="customViewstate" runat="server" />

				<script type="text/javascript">
		var currentEditBlock, currentEditModule, modudlePath, moduleReference, iframe, currentEditModuleId,
			editBlockForm = document.getElementById('editBlockForm'),
			bgoverlay = document.getElementById('bgoverlay'),
			editModuleForm = document.getElementById('editModuleForm'),
			btnLoadEditModuleForm = document.getElementById('btnLoadEditModuleForm'),
			customArg = document.getElementById('customArg'),
			customArg2 = document.getElementById('customArg2'),
			iframeEditForm = document.getElementById('iframeEditForm');
			
			var currentBlockHeight, blockEditorHeight = 390;
			
			var editor = ID<%= NewsContent.ClientID %>; // javascript innova editor
			
			
			
		// init listOfModules
		{
			var listOfModulesHTML = '<ul class="listOfModules">';
			
			var a, as = document.getElementById('listOfModules').getElementsByTagName('a');
			for (var i=0; i<as.length; i++)
			{
				a = as[i];
				listOfModulesHTML += '<li><a href="javascript:void();" onclick="editmodule_show(\'' + a.getAttribute('title') + '\', \'\'); return false;">' + a.innerHTML + '</a></li>';
			}
			
			listOfModulesHTML += '</ul>';
		}
				</script>

				<script type="text/javascript">
		////////////////////////////////////////////////////////////////////////////////
		// jQuery
		////////////////////////////////////////////////////////////////////////////////
		
		$(document).ready(function()
		{
			$(".themmotdoanvanban").append("<em></em>");
			$(".quayvetrangsoantin").append("<em></em>");
			
			
			$(".quayvetrangsoantin").hover(function() {
				$(this).find("em").animate({opacity: "show", top: "10"}, "slow");
				var hoverText = $(this).attr("title");
				$(this).find("em").text(hoverText);
			
			}, function() {
				$(this).find("em").animate({opacity: "hide", top: "0"}, "fast");
			});
			$(".themmotdoanvanban").hover(function() {
				$(this).find("em").animate({opacity: "show", top: "10"}, "slow");
				var hoverText = $(this).attr("title");
				$(this).find("em").text(hoverText);
				
			}, function() {
				$(this).find("em").animate({opacity: "hide", top: "0"}, "fast");
			});
			
			ijCMSEditor_setTooltipLocation();
		});
		
		function ijCMSEditor_setTooltipLocation()
		{
			var ijCMSEditor = document.getElementById('ijCMSEditor');
			var a, as = ijCMSEditor.getElementsByTagName('a');
			a = as[0];
			a.getElementsByTagName('em')[0].style.left = findPosX(a) - 15 + 'px';
			a = as[1];
			a.getElementsByTagName('em')[0].style.left = findPosX(a) - 15 + 'px';
			a = as[2];
			a.getElementsByTagName('em')[0].style.left = findPosX(a) - 15 + 'px';
		}
		
		window.onresize = ijCMSEditor_setTooltipLocation;
		
		function paragraph_convertToBlock()
{
	var panel = document.getElementById('panel');
	

	if (document.all) // ie
	{	
	
		panel.unselectable = "off";
		panel.contentEditable = "true";
		
		var oSel = document.selection.createRange();
		
		var sHTML = genParagrahp2(oSel.htmlText);
		
		if(oSel.parentElement)oSel.pasteHTML(sHTML);
		else oSel.item(0).outerHTML=sHTML;
		
		panel.contentEditable = "false";
	}
	else // firefox
	{
		var oSel = getSelection();
		var range = oSel.getRangeAt(0);
		
		range.deleteContents();
		
    }
}
		
		
		////////////////////////////////////////////////////////////////////////////////
				</script>

				<div style="display: none;">
					<asp:LinkButton ID="btnReload" runat="server" OnClientClick="saveViewstate();" OnClick="btnReload_Click"></asp:LinkButton>
					<asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="saveViewstate();"
						OnClick="btnReload_Click2"></asp:LinkButton>
					<asp:Literal ID="js" EnableViewState="false" runat="server"></asp:Literal>
				</div>
			</form>
			<br style="clear: both;" />
		</center>
	</div>
</body>
</html>
