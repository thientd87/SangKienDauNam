<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.Draft.List" %>
<table cellpadding="0" cellspacing="0" border="0" width="100%" class="ms-formbody">
    <tr>
        <td style="font-weight:bold;font-size:16px;background-color:#dfdfdf">
            Danh sách bài draft
        </td>
    </tr>
    <tr>
        <td height="10px"></td>
    </tr>
    <tr>
        <td>
            <table cellpadding="2" cellspacing="2" border="0" width="100%" class="ms-formbody">
                <tr>
                    <td>
                        <asp:GridView Width="100%" ID="grdListNews" runat="server" HeaderStyle-CssClass="grdHeader"
									BorderWidth="1px" BorderColor="#DFDFDF" RowStyle-CssClass="grdItem" EmptyDataText="<span style='color:Red'><b>Không có bài !</b></span>"
									AlternatingRowStyle-CssClass="grdAlterItem" AutoGenerateColumns="False" AllowPaging="True"
									AllowSorting="true" DataSourceID="objListNewsSource" PageSize="40" OnRowCommand="grdListNews_RowCommand" >
							<Columns>
							    <asp:TemplateField HeaderText="Tên bài viết" >
									<ItemStyle CssClass="ms-vb-title" HorizontalAlign="center" />
									<ItemTemplate>
										<nobr>
										  <a href="javascript:openpreview('/PreviewDraft.aspx?temp_id=<%# Eval("temp_id")%>',600,500)"><%# Eval("News_Title")%></a>
										</nobr>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Left" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Ngày lưu" >
									<ItemStyle CssClass="ms-vb-title" HorizontalAlign="center" />
									<ItemTemplate>
										<nobr>
											<%# ((DateTime)Eval("ModifyDate")).ToString("dd/MM/yyyy HH:mm")%>
										</nobr>
									</ItemTemplate>
									<ItemStyle Width="70px" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Sửa" >
									<ItemStyle CssClass="ms-vb-title" HorizontalAlign="center" />
									<ItemTemplate>
										<nobr>
											<asp:ImageButton runat="server" ID="img" CommandName="editdraft" CommandArgument='<%# Eval("temp_id")%>' ImageUrl="/Images/Icons/document_edit.gif" BorderWidth="0" />
										</nobr>
									</ItemTemplate>
									<ItemStyle Width="30px" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Xóa" >
									<ItemStyle CssClass="ms-vb-title" HorizontalAlign="center" />
									<ItemTemplate>
										<nobr>
											<asp:ImageButton runat="server" ID="imgDelete" CommandName="deletedraft" CommandArgument='<%# Eval("temp_id")%>' ImageUrl="/Images/Icons/delete.gif" BorderWidth="0" />
										</nobr>
									</ItemTemplate>
									<ItemStyle Width="30px" />
								</asp:TemplateField>
							</Columns>
							<PagerStyle CssClass="paging" HorizontalAlign="left" />
			                <PagerSettings Mode="NumericFirstLast" />
			                <HeaderStyle CssClass="grdHeader" />
			                <RowStyle CssClass="grdItem" />
			                <AlternatingRowStyle CssClass="grdAlterItem" />
					    </asp:GridView>
                    </td>
                </tr>
                <tr>
				<td style="padding-top: 10px; height: 106px;">
					<table id="tblSearch" runat="server" cellpadding="2" cellspacing="2" border="0" style="width: 100%;
						height: 60px; border: 1px solid #b8c1ca; background-color: #E5E5E5; clear: both"
						class="ms-formbody">
						<tr>
							<td width="100">
								Bài viết</td>
							<td>
								<asp:TextBox ID="txtKeyword" CssClass="ms-long" runat="server" Width="300px"></asp:TextBox>
								<asp:Button ID="btnSearch" CssClass="ms-input" runat="server" Text="Tìm ki&#7871;m" OnClick="btnSearch_Click"
									/>
							</td>
						</tr>
						<tr>
							<td>
								Thuộc nhóm tin</td>
							<td>
								<asp:DropDownList ID="cboCategory" runat="server">
								</asp:DropDownList>
							</td>
						</tr>
						<tr>
						    <td> Từ ngày </td>
						    <td> 
						        <asp:TextBox ID="txtFromDate" CssClass="ms-long" runat="server"></asp:TextBox>  
						        <a onclick="if(self.gfPop)gfPop.fPopCalendar(document.getElementById('<% = txtFromDate.ClientID %>'));return false;"
		                            href="javascript:void(0)">
		                            <img class="PopcalTrigger" height="21" src="/Scripts/DatePicker/datepicker.gif" width="34"
			                            align="absMiddle" border="0">
	                            </a>
						    </td>
						</tr>
						<tr>
						    <td> Đến ngày </td>
						    <td> 
						        <asp:TextBox ID="txtToDate" CssClass="ms-long" runat="server"></asp:TextBox> 
						        <a onclick="if(self.gfPop)gfPop.fPopCalendar(document.getElementById('<% = txtToDate.ClientID %>'));return false;"
		                            href="javascript:void(0)">
		                            <img class="PopcalTrigger" height="21" src="/Scripts/DatePicker/datepicker.gif" width="34"
			                            align="absMiddle" border="0">
	                            </a>
						    </td>
						</tr>
						</table>
					</td>
				</tr>
            </table>
        </td>
    </tr>
</table>
<asp:ObjectDataSource ID="objListNewsSource" runat="server" SelectMethod="GetList"
	SelectCountMethod="GetNumberRecord"
	TypeName="Portal.BO.Editoral.Draft.DraftHelper" 
	EnablePaging="true" MaximumRowsParameterName="PageSize" StartRowIndexParameterName="StartRow">
	<SelectParameters>
	    <asp:ControlParameter Name="fromDate" Type="string" ControlID="txtFromDate" PropertyName="Text" />
		<asp:ControlParameter Name="toDate" Type="string" ControlID="txtToDate" PropertyName="Text" />
		<asp:ControlParameter Name="news_title" Type="string" ControlID="txtKeyword" PropertyName="Text" />
		<asp:ControlParameter Name="cat_id" Type="string" ControlID="cboCategory" PropertyName="SelectedValue" />
	</SelectParameters>
</asp:ObjectDataSource>

<iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
	src="/Scripts/DatePicker/ipopeng.htm" scrolling="no" frameborder="0" style="visibility: visible;
	z-index: 999; position: absolute; top: -500px; left: -500px;"></iframe>