<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Default.aspx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.FileManager.Default" %>

<%@ Register Src="../Media/UserControl/ctlPopupView2.ascx" TagName="ctlPopupView2"
	TagPrefix="uc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
	Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>Quản lý file</title>
	<link rel="stylesheet" href="/Scripts/ContextMenu/proto.menu.0.6.css" type="text/css"
		media="screen" />
	<link rel="stylesheet" href="fm.css" type="text/css" />
	<link rel="stylesheet" href="/styles/common.css" type="text/css" />
</head>
<body>
	<form id="form1" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server">
		</asp:ScriptManager>
		<div id="bgFilter" class="filter">
			<img src="/images/blank.gif" id="imgloading" /></div>
		<div style="text-align: center;">
			<table cellpadding="0" cellspacing="10" width="100%" align="center">
				<tr align="left" valign="top">
					<td class="td_folderlist">
						<div class="c1">
							<div class="c2">
								Thư mục</div>
							<ul class="treeview" style="margin: 0px; padding: 0px;" id="treeviewFolder">
								<asp:Literal ID="ltrFolder" runat="server"></asp:Literal></ul>
						</div>
					</td>
					<td class="td_folderContent">
						<asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
							<ProgressTemplate>
								<div style="text-align: center;">
									Xin bạn chờ trong giây lát...</div>
							</ProgressTemplate>
						</asp:UpdateProgress>
                        <div class="button">
								<a class="upload"href="javascript:upload_show2()">Upload</a>
								<a class="copy" href="javascript:grid_copy()">Copy</a> <a id="pastebutton"
									class="paste" href="javascript:grid_paste()">Paste</a> <a name="a_selectMultiFile" id="a_selectMultiFile"
										href="javascript:grid_selectMultiFile()">Chọn</a> <a id="a_grid_deleteMultiFile"
											href="javascript:grid_deleteMultiFile()">Xóa</a>
								<a class="closewindow" href="javascript:window.close()">Đóng</a> 
								
								<div class="searchBox">
									<table cellpadding="0" cellspacing="0" style="border-collapse: collapse;">
										<tr>
											<td>
												<asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox></td>
											<td>
												<asp:ImageButton ID="btnSearch" ImageUrl="/Images/kinhlup.gif" runat="server" OnClick="btnSearch_Click" /></td>
										</tr>
									</table>
								</div>
								<br style="clear: both;" />
							</div>
						<div id="grid">
							<div style="overflow: auto; width: 100%;">
								<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
									<ContentTemplate>
										<div style="height: 568px;">
											<asp:Repeater ID="Repeater1" OnItemDataBound="rptFilesOfFolder_ItemDataBound" runat="server">
												<HeaderTemplate>
													<table class="gridItems">
														<tr style="background-color: #d0d0d0; font-weight: bold;">
															<td>
																<input id="chk" onclick="tonggleCHK()" value="" type="checkbox" /></td>
															<td style="width: 400px;">
																<span style="padding: 5px;">Tên</span></td>
															<td>
																<span style="padding: 5px;">Kích thước</span></td>
															<td colspan="2">
																<span style="padding: 5px;">Thao tác</span></td>
														</tr>
												</HeaderTemplate>
												<ItemTemplate>
													<tr class="inactive" onmouseover="if (this.className != 'active') this.className = 'active2'"
														onmouseout="if (this.className != 'active') this.className = 'inactive'">
														<td>
															<input name="chk" value="<%# Eval("FullPath") %>" class="chk" type="checkbox" />
														</td>
														<td style="clear: both;">
															<a class="<%# Eval("Type") %>" title="Chọn" href="javascript:grid_selectItem('<%# Eval("FullPath") %>', '<%# Eval("Type") %>')">
																<%# Eval("Name") %>
															</a>
														</td>
														<td align="right">
															<nobr>
																<%# Eval("FileSize") %>
															</nobr>
														</td>
														<td style="width: 60px;">
															<a class="cancel" href="javascript:grid_deleteItem('<%# Eval("FullPath") %>', '<%# Eval("Type") %>')"
																onclick="return confirm('Bạn thực sự muốn xóa?');">Xóa</a>
														</td>
														<td style="width: 100px;">
															<a _path="<%# Eval("FullPath") %>" href="#" onclick="return grid_previewItem('<%# Eval("FullPath") %>', '<%# Eval("Type") %>', event)"
																class="ok">Preview </a>
														</td>
													</tr>
												</ItemTemplate>
												<FooterTemplate>
													</table>
												</FooterTemplate>
											</asp:Repeater>
										</div>
										<div  style="width:600px;overflow:hidden;">
										<div class="paging">
											<asp:Literal ID="ltrPaging" runat="server"></asp:Literal></div>
										</div>
										<asp:HiddenField ID="postBackArg" runat="server" />
										<asp:HiddenField ID="postBackArg2" runat="server" />
										<asp:HiddenField ID="postBackArg3" runat="server" />
										<asp:HiddenField ID="postBackArg4" runat="server" />
										<input type="hidden" id="hidCountFile" name="hidCountFile" value="<%=CountFile %>" />
										
										
									</ContentTemplate>
									<Triggers>
										<asp:AsyncPostBackTrigger ControlID="btnBrowseFolder" EventName="Click" />
										<asp:AsyncPostBackTrigger ControlID="btnGrid_DeleteItem" EventName="Click" />
										<asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
										<asp:AsyncPostBackTrigger ControlID="btnDeleteMultiItem" EventName="Click" />
										<asp:AsyncPostBackTrigger ControlID="btnReloadUpdatePanel" EventName="Click" />
										<asp:AsyncPostBackTrigger ControlID="btnPaging" EventName="Click" />
										<asp:AsyncPostBackTrigger ControlID="btnPaste" EventName="Click" />
									</Triggers>
								</asp:UpdatePanel>
								<uc1:ctlPopupView2 ID="CtlPopupView2_1" runat="server" />
							</div>
							<div class="button">
								<a class="upload"href="javascript:upload_show2()">Upload</a>
								<a class="copy" href="javascript:grid_copy()">Copy</a> <a id="pastebutton"
									class="paste" href="javascript:grid_paste()">Paste</a>
                                <a name="a_selectMultiFile" id="a_selectMultiFile"
										href="javascript:grid_selectMultiFile()">Chọn</a> 
                                <a id="a_grid_deleteMultiFile"
											href="javascript:grid_deleteMultiFile()">Xóa</a>
								<a class="closewindow" href="javascript:window.close()">Đóng</a> 
								
								<br style="clear: both;" />
							</div>



							<div class="popup popupborder" style="height: 500px; width: 600px;" id="UploadFileContainer">
								<div class="popupheader">
									Upload file</div>
								<div class="popupcontent">
									<iframe src="/Scripts/UploadMultiFile/Default.aspx" style="border: none;
										width: 580px; height: 445px;"></iframe>
								</div>
							</div>
						</div>
					</td>
				</tr>
			</table>
		</div>
		<div style="display: none;">
			<asp:Button ID="btnPaste" runat="server" OnClick="btnPaste_Click" Text="Paste" />
			<asp:Button ID="btnBrowseFolder" runat="server" Text="Button" OnClick="btnBrowseFolder_Click" /><asp:Button
				ID="btnNewFolder" runat="server" Text="Button" OnClick="btnNewFolder_Click" /><asp:Button
					ID="btnGrid_DeleteItem" runat="server" Text="Button" OnClick="btnGrid_DeleteItem_Click" />
			<asp:Button ID="btnDeleteFolder" runat="server" Text="Button" OnClick="btnDeleteFolder_Click" /><asp:Button
				ID="btnDeleteMultiItem" OnClick="btnDeleteMultiItem_Click" runat="server" Text="Button" /><asp:Button
					ID="btnSaveToShareFolder" runat="server" Text="Button" OnClick="btnSaveToShareFolder_Click" />
			<asp:Button ID="btnReloadUpdatePanel" runat="server" Text="Button" OnClick="btnReloadUpdatePanel_Click" /><asp:Button
				ID="btnPaging" runat="server" Text="Button" OnClick="btnPaging_Click" />
		</div>		
		<asp:Literal ID="js" runat="server"></asp:Literal>
		
	</form>

	<script type="text/javascript">
      var folder = '<%= ConfigurationManager.AppSettings["ImageUrl"] %>';
      var parentFunction = opener.<%=Request.QueryString["function"] %>;
      var share = '<%=Request.QueryString["share"] %>';     
      var mode = '<%=Request.QueryString["mode"] %>'; 
      var selectedItems = '<%=Request.QueryString["i"] %>';         
	</script>
	<script type="text/javascript" src="/AssetManager/CustomObjects/js/swfobject.packed.js"></script>
	<script type="text/javascript" src="/Scripts/library.js"></script>
	<script type="text/javascript" src="/Scripts/common.js"></script>
	<script type="text/javascript" src="/Scripts/ContextMenu/prototype-1.6.0.2.js"></script>
	<script type="text/javascript" src="/Scripts/ContextMenu/proto.menu.0.6.js"></script>
	<script type="text/javascript" src="fm.js?date=55"></script>

</body>
</html>
