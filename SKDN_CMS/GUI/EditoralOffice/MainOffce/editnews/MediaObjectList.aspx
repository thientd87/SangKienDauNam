<%@ Page Language="C#" AutoEventWireup="true" Codebehind="MediaObjectList.aspx.cs"
	Inherits="DFISYS.GUI.EditoralOffice.MainOffce.editnews.MediaObjectList" %>

<%@ Register Src="~/GUI/EditoralOffice/MainOffce/Media/UserControl/ctlPopupView2.ascx"
	TagName="ctlPopupView" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Danh sách các media liên quan</title>

	<script language="javascript" src="/scripts/common.js"></script>

	<link rel="stylesheet" type="text/css" href="/Styles/common.css" />

	<script type="text/javascript" src="/Scripts/library.js"></script>

</head>
<body>
	<form id="form1" runat="server">
		<div id="bgFilter">
			<img src="/images/blank.gif" />
		</div>
		<table cellpadding="0" cellspacing="0" border="0" width="100%" class="ms-formbody">
			<tr>
				<td colspan="3" class="Edit_Head_Cell">
					Danh sách các media liên quan
				</td>
			</tr>
			<tr>
				<td>
					<table cellpadding="0" cellspacing="5" width="100%">
						<tr>
							<td colspan="2">
								<asp:GridView Width="100%" ID="grdMedia" runat="server" BorderWidth="1px" BorderColor="#DFDFDF"
									HeaderStyle-CssClass="grdHeader" RowStyle-CssClass="grdItem" ShowFooter="true"
									EmptyDataText="Hien chua co du lieu" AlternatingRowStyle-CssClass="grdAlterItem"
									AutoGenerateColumns="false" AllowPaging="true" DataSourceID="objNewsMediaSource"
									PageSize="12" OnRowCommand="grdMedia_RowCommand" OnRowCancelingEdit="grdMedia_RowCancelingEdit"
									OnRowDataBound="grdMedia_RowDataBound" OnRowDeleting="grdMedia_RowDeleting" OnRowEditing="grdMedia_RowEditing"
									OnRowUpdating="grdMedia_RowUpdating">
									<Columns>
										<asp:TemplateField HeaderStyle-Width="2%">
											<HeaderTemplate>
												<input type="checkbox" id="chkAll" onclick="tonggle('<%=grdMedia.ClientID %>', this.checked, 'chkSelect')" />
											</HeaderTemplate>
											<ItemTemplate>
												<input type="checkbox" name="chkSelect" id="chkSelect<%#DataBinder.Eval(Container.DataItem,"Object_ID")%>"
													value="<%#DataBinder.Eval(Container.DataItem,"Object_ID")%>" onclick="setClassTr(this, this.checked)" />
												<input type="hidden" name="hidNewsTitle<%#DataBinder.Eval(Container.DataItem,"Object_ID")%>"
													value="<%#DataBinder.Eval(Container.DataItem,"Object_Url")%>" id="hidTitle<%#DataBinder.Eval(Container.DataItem,"Object_ID")%>" />
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Tên file" HeaderStyle-Width="40%">
											<ItemTemplate>
												<a href="javascript:preview('<%# Eval("Object_Url") %>', <%# Eval("Object_Type") %>)">
													<%# Eval("Object_Url") %>
												</a>
												<asp:HiddenField ID="hdfObject_Url" Value='<%# Eval("Object_Url") %>' runat="server" />
											</ItemTemplate>
											<%--<EditItemTemplate>
										<asp:FileUpload ID="flEObject" CssClass="ms-input" runat="server" Width="100%" />
									</EditItemTemplate>--%>
										</asp:TemplateField>
										<asp:TemplateField HeaderStyle-Width="15%" HeaderText="Kiểu" ItemStyle-HorizontalAlign="Center">
											<ItemTemplate>
												<asp:Literal ID="ltrType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Object_Type")%>' />
											</ItemTemplate>
											<EditItemTemplate>
												<asp:DropDownList ID="cboType" runat="server" Width="100%">
													<asp:ListItem Value="1" Text="Hình ảnh"></asp:ListItem>
													<asp:ListItem Value="2" Text="Video"></asp:ListItem>
												</asp:DropDownList>
											</EditItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Chú thích cho file" HeaderStyle-Width="28%" ItemStyle-HorizontalAlign="Center">
											<ItemStyle HorizontalAlign="left" Wrap="true" />
											<ItemTemplate>
												<asp:Literal ID="ltrNote" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Object_Note")%>' />
											</ItemTemplate>
											<EditItemTemplate>
												<asp:TextBox ID="txtENote" CssClass="ms-input" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Object_Note")%>'
													Width="98%" />
											</EditItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Tuỳ chọn" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="15%">
											<ItemTemplate>
												<asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Images/icons/edit.gif" AlternateText="Sửa nội dung"
													CausesValidation="False" CommandName="Edit" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Object_ID")%>'>
												</asp:ImageButton>
											</ItemTemplate>
											<EditItemTemplate>
												<asp:ImageButton ID="imgSave" runat="server" ImageUrl="~/Images/icons/save.gif" AlternateText="Lưu lại"
													ToolTip="Lưu lại" CommandName="Update" CausesValidation="False" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Object_ID")%>'>
												</asp:ImageButton>
												&nbsp;
												<asp:ImageButton ID="imgCancel" BorderWidth="0" runat="server" ImageUrl="~/Images/icons/stop.gif"
													AlternateText="Tạm dừng thay đổi" CommandName="Cancel" CausesValidation="False">
												</asp:ImageButton>
												&nbsp;
												<asp:ImageButton ID="imgDel" runat="server" ImageUrl="~/Images/icons/cancel.gif"
													AlternateText="Xóa nội dung này" CommandName="Delete" CausesValidation="False"></asp:ImageButton>
											</EditItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField HeaderText="Xóa" ItemStyle-HorizontalAlign="Center">
											<ItemTemplate>
												<asp:ImageButton ImageUrl="/images/icons/delete.gif" OnClientClick="return confirm('Bạn có muốn xóa không ?');"
													ID="ibnDelete" runat="server" CommandName="DeleteMedia" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Object_ID")%>' />
											</ItemTemplate>
										</asp:TemplateField>
										<%--<asp:TemplateField HeaderText="Xem" ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<a href="javascript:Preview('<%#DataBinder.Eval(Container.DataItem,"Object_Url")%>','<%#DataBinder.Eval(Container.DataItem,"Object_Type")%>')">
											<img src="/images/icons/preview.gif" border="0" /></a>
									</ItemTemplate>
								</asp:TemplateField>--%>
									</Columns>
									<RowStyle CssClass="grdItem" />
									<HeaderStyle CssClass="grdHeader" />
									<AlternatingRowStyle CssClass="grdAlterItem" />
									<PagerSettings Visible="false" />
								</asp:GridView>
							</td>
						</tr>
						<tr>
							<td valign="top" align="left" colspan="2" style="padding-top: 15px">
								<table cellpadding="0" cellspacing="0" border="0" width="100%">
									<tr>
										<td width="20%">
											Xem trang&nbsp;<asp:DropDownList ID="cboPage" runat="Server" DataTextField="Text"
												DataValueField="Value" AutoPostBack="true" DataSourceID="objdspage" OnSelectedIndexChanged="cboPage_SelectedIndexChanged">
											</asp:DropDownList>
										</td>
										<td align="right" style="height: 19px" class="Menuleft_Item" style="display: none">
											<a onclick="tonggle('<%=grdMedia.ClientID %>', true, 'chkSelect')" href="#a" class="normalLnk1">
												Chọn tất cả</a> | <a onclick="tonggle('<%=grdMedia.ClientID %>', false, 'chkSelect')"
													href="#a" class="normalLnk1">Bỏ chọn</a> |
											<asp:LinkButton ID="lnkAddMedia" OnClientClick="media_bindValue(); return false;"
												runat="server" CssClass="normalLnk" OnClick="lnkAddMedia_Click">Thêm vào bài</asp:LinkButton>
											<asp:Literal ID="ltrAddMedia" Text="&nbsp;|" runat="server"></asp:Literal>
											<asp:LinkButton ID="lnkRealDel" OnClientClick="return confirm('Bạn có muốn xóa những file đã chọn hay không?')"
												runat="server" CssClass="normalLnk" OnClick="lnkRealDel_Click">Xóa các file</asp:LinkButton>
											<asp:Literal ID="ltrRealDel" Text="&nbsp;|" runat="server"></asp:Literal>
											<asp:LinkButton ID="lnkSelectedMedia" runat="server" CssClass="normalLnk" OnClick="lnkSelectedMedia_Click">Hiển thị media đã chọn</asp:LinkButton>
											<asp:Literal ID="ltrSelectedMedia" Text="&nbsp;|" runat="server"></asp:Literal>
											<asp:LinkButton ID="lnkShowAllMedia" runat="server" CssClass="normalLnk" OnClick="lnkShowAllMedia_Click">Hiển thị tất cả media</asp:LinkButton>
											<asp:Literal ID="ltrShowAllMedia" Text="&nbsp;|" runat="server"></asp:Literal>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td height="10">
				</td>
			</tr>
			<tr>
				<td>
					<hr />
				</td>
			</tr>
			<tr>
				<td colspan="3" class="Edit_Head_Cell">
					Chọn các media liên quan
				</td>
			</tr>
			<tr>
				<td height="10">
				</td>
			</tr>
			<tr>
				<td width="100%" align="center">
					<table cellpadding="2" cellspacing="0" align="center" style="width: 100%; border: 1px solid #79A4D2;">
						<tr>
							<td class="grdHeader" style="width: 27px">
								STT</td>
							<td class="grdHeader">
								Ảnh
							</td>
							<td class="grdHeader">
								Kiểu
							</td>
							<td class="grdHeader" style="width: 297px">
								Chú thích cho file
							</td>
						</tr>
						<tr>
							<td align="center" style="width: 27px">
								1</td>
							<td width="200px">
								<asp:TextBox ID="txtFileName1" CssClass="ms-input" runat="server"></asp:TextBox>
							</td>
							<td width="100px">
								<asp:DropDownList ID="cboType1" CssClass="ms-input" runat="server" Width="100">
									<asp:ListItem Value="1" Text="H&#236;nh ảnh"></asp:ListItem>
									<asp:ListItem Value="2" Text="Video"></asp:ListItem>
								</asp:DropDownList>
							</td>
							<td align="center">
								<asp:TextBox ID="txtTitle1" CssClass="ms-input" runat="server" Width="200px"></asp:TextBox>
							</td>
						</tr>
						<tr class="grdAlterItem">
							<td align="center" style="width: 27px">
								2</td>
							<td width="200px">
								<asp:TextBox ID="txtFileName2" CssClass="ms-input" runat="server"></asp:TextBox>
							</td>
							<td width="100px">
								<asp:DropDownList ID="cboType2" CssClass="ms-input" runat="server" Width="100">
									<asp:ListItem Value="1" Text="H&#236;nh ảnh"></asp:ListItem>
									<asp:ListItem Value="2" Text="Video"></asp:ListItem>
								</asp:DropDownList>
							</td>
							<td align="center">
								<asp:TextBox ID="txtTitle2" CssClass="ms-input" runat="server" Width="200px"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td align="center" style="width: 27px">
								3</td>
							<td width="200px">
								<asp:TextBox ID="txtFileName3" CssClass="ms-input" runat="server"></asp:TextBox>
							</td>
							<td width="100px">
								<asp:DropDownList ID="cboType3" CssClass="ms-input" runat="server" Width="100">
									<asp:ListItem Value="1" Text="H&#236;nh ảnh"></asp:ListItem>
									<asp:ListItem Value="2" Text="Video"></asp:ListItem>
								</asp:DropDownList>
							</td>
							<td align="center">
								<asp:TextBox ID="txtTitle3" CssClass="ms-input" runat="server" Width="200px"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td align="center" style="width: 27px; background-color: #f8f8f8">
								4</td>
							<td width="200px" bgcolor="#f8f8f8">
								<asp:TextBox ID="txtFileName4" CssClass="ms-input" runat="server"></asp:TextBox>
							</td>
							<td width="100px">
								<asp:DropDownList ID="cboType4" runat="server" Width="100">
									<asp:ListItem Value="1" Text="H&#236;nh ảnh"></asp:ListItem>
									<asp:ListItem Value="2" Text="Video"></asp:ListItem>
								</asp:DropDownList>
							</td>
							<td align="center">
								<asp:TextBox ID="txtTitle4" CssClass="ms-input" runat="server" Width="200px"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td align="center" style="width: 27px">
								5</td>
							<td width="200px">
								<asp:TextBox ID="txtFileName5" CssClass="ms-input" runat="server"></asp:TextBox>
							</td>
							<td width="100px">
								<asp:DropDownList ID="cboType5" runat="server" Width="100">
									<asp:ListItem Value="1" Text="H&#236;nh ảnh"></asp:ListItem>
									<asp:ListItem Value="2" Text="Video"></asp:ListItem>
								</asp:DropDownList>
							</td>
							<td align="center">
								<asp:TextBox ID="txtTitle5" CssClass="ms-input" runat="server" Width="200px"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td>
							</td>
							<td>
								<input type="button" onclick="return chonfiletuthuvien()" value="Chọn các file từ thư viện" />
							</td>
							<td>
							</td>
							<td>
							</td>
						</tr>
						<tr>
							<td height="5" style="width: 27px">
							</td>
							<td height="5">
							</td>
						</tr>
						<tr>
							<td align="left" style="padding-left: 10px; width: 27px">
							</td>
							<td align="left" style="padding-left: 20px; width: 27px">
								<asp:Button ID="btnAddMediaObject" CssClass="ms-input" runat="server" Text="Cập nhật"
									OnClick="btnAddMediaObject_Click" />
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<asp:ObjectDataSource ID="objdspage" runat="server" SelectMethod="getPage" TypeName="DFISYS.BO.Editoral.NewsMedia.NewsMediaHelper"
			OldValuesParameterFormatString="original_{0}">
			<SelectParameters>
				<asp:ControlParameter DefaultValue="0" ControlID="grdMedia" Name="numPage" PropertyName="PageCount"
					Type="Int32" />
			</SelectParameters>
		</asp:ObjectDataSource>
		<asp:ObjectDataSource ID="objNewsMediaSource" runat="server" SelectMethod="GetMediaObjlist"
			InsertMethod="createMediaObject" UpdateMethod="UpdateMediaObj" DeleteMethod="DelMediaObj"
			SelectCountMethod="GetMediaRowsCount" TypeName="DFISYS.BO.Editoral.NewsMedia.NewsMediaHelper"
			EnablePaging="true" MaximumRowsParameterName="PageSize" StartRowIndexParameterName="StartRow">
			<SelectParameters>
				<asp:Parameter Name="strWhere" DefaultValue="" Type="string" />
			</SelectParameters>
			<UpdateParameters>
				<asp:Parameter Name="_obj_id" Type="int32" DefaultValue="0" />
				<%--<asp:Parameter Name="_obj_url" Type="string" />--%>
				<asp:Parameter Name="_obj_type" Type="int16" />
				<asp:Parameter Name="_obj_note" Type="string" />
			</UpdateParameters>
			<DeleteParameters>
				<asp:Parameter Name="_selected_id" Type="string" DefaultValue="" />
			</DeleteParameters>
			<InsertParameters>
				<asp:Parameter Name="_obj_url" Type="string" />
				<asp:Parameter Name="_obj_type" Type="Int16" />
				<asp:Parameter Name="_obj_note" Type="string" />
				<asp:Parameter Name="_obj_user" Type="string" />
				
				<asp:QueryStringParameter Name="strNewsId" QueryStringField="newsid" DefaultValue=""
					Type="string" />
				<asp:QueryStringParameter Name="strFilmId" QueryStringField="pid" DefaultValue=""
					Type="string" />
					<asp:Parameter Name="obj_value" Type="string" />
			</InsertParameters>
		</asp:ObjectDataSource>
		<uc2:ctlPopupView ID="CtlPopupView2" runat="server" />
		<input type="hidden" id="hidPrefix" name="hidPrefix" value="<% = ClientID %>_" />
		<input type="hidden" id="hidTitle" runat="server" />
		<input type="hidden" runat="server" id="txt_news_checked" />
		<input type="hidden" runat="server" id="txt_news_title_checked" />

		<script language="javascript">

function media_bindValue()
{
	var grdMedia = document.getElementById('<%=grdMedia.ClientID %>');
	var trs = grdMedia.getElementsByTagName('tr');
	var i, count = trs.length;
	var chk, hdd;
	var arr = new Array();
	for (i=1; i<count-1; i++)
	{
		chk = trs[i].getElementsByTagName('input')[0];
		if (trs[i].getElementsByTagName('input').length > 1)
			arr.push(new Array(chk.value, trs[i].getElementsByTagName('input')[1].value));
	}
	opener.<%=Request.QueryString["function"] %>(arr);
	close();
}
//onunload = media_bindValue;

//  bacth [2:06 PM 6/5/2008]
function chonfiletuthuvien()
{
	openpreview('/GUI/EditoralOffice/MainOffce/FileManager/default.aspx?function=media_loadvalue&mode=multi&share=', 900, 700);
	return false;
}
//  bacth: bind vao textbox từ mảng đường dẫn trả về từ cửa sổ quản lý file [2:06 PM 6/5/2008]
function media_loadvalue(arr)
{
	var arrControls = new Array();
	arrControls.push('<%=txtFileName1.ClientID %>');
	arrControls.push('<%=txtFileName2.ClientID %>');
	arrControls.push('<%=txtFileName3.ClientID %>');
	arrControls.push('<%=txtFileName4.ClientID %>');
	arrControls.push('<%=txtFileName5.ClientID %>');
	
	var trimPattern = '/Images/Uploaded/(.*?)$';
	var re;
	var m;
	var i = 0, j = 0;
	while (i < arr.length && j < arrControls.length)
	{
		re = new RegExp(trimPattern, 'gi');
		m = re.exec(arr[i]);
		if (document.getElementById(arrControls[j]).value == '')
		{
			document.getElementById(arrControls[j]).value = m[1];
			i++;
		}
		j++;
	}
}
function preview(path, typeIndex)
{
	var folder = '<%= ConfigurationManager.AppSettings["ImageUrl"] %>';
	if (typeIndex == 1)
		folder += 'images/Uploaded/Share/Media/picture/';
	else
		folder += 'images/Uploaded/Share/Media/video/';
	path = folder + path;
	
	showModalPopup('ctlPopupView', false);
    document.getElementById("tdPreview").innerHTML = genPreviewhtml(path);
}
		</script>

	</form>
</body>
</html>
