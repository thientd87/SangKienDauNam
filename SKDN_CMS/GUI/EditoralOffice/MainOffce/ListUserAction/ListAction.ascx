<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListAction.ascx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.ListUserAction.ListAction" %>
<table cellpadding="0" cellspacing="0" border="0" width="100%" class="ms-formbody">
    <tr>
        <td style="font-weight:bold;font-size:16px;background-color:#dfdfdf">
            Danh sách các hành động của người dùng
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
									AllowSorting="true" DataSourceID="objListNewsSource" PageSize="40" >
							<Columns>
							    <asp:TemplateField HeaderText="Hành động" >
									<ItemStyle CssClass="ms-vb-title" HorizontalAlign="center" />
									<ItemTemplate>
										<nobr>
										  <%# Eval("Action")%>
										</nobr>
									</ItemTemplate>
									<ItemStyle HorizontalAlign="Left" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Ngày" >
									<ItemStyle CssClass="ms-vb-title" HorizontalAlign="center" />
									<ItemTemplate>
										<nobr>
											<%# ((DateTime)Eval("LogDate")).ToString("dd/MM/yyyy HH:mm")%>
										</nobr>
									</ItemTemplate>
									<ItemStyle Width="70px" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="User" >
									<ItemStyle CssClass="ms-vb-title" HorizontalAlign="center" />
									<ItemTemplate>
										<nobr>
											<%# Eval("UserName")%>
										</nobr>
									</ItemTemplate>
									<ItemStyle Width="70px" />
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Preview" >
									<ItemStyle CssClass="ms-vb-title" HorizontalAlign="center" />
									<ItemTemplate>
										<nobr>
											<a href="javascript:openpreview('/preview/default.aspx?news=<%#Eval("Object_ID")%>',900,700);"> <img src='/images/icons/preview.gif' border="0" /> </a>
										</nobr>
									</ItemTemplate>
									<ItemStyle Width="50"  />
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
					     </table>
				    </td>
			    </tr>
            </table>
        </td>
    </tr>
</table>
<asp:ObjectDataSource ID="objListNewsSource" runat="server" SelectMethod="GetListLogAction"
	SelectCountMethod="GetListLogActionCount"
	TypeName="Portal.BO.Editoral.UserActionHelper.ActionHelper" 
	EnablePaging="true" MaximumRowsParameterName="PageSize" StartRowIndexParameterName="StartRow">
	<SelectParameters>
	    <asp:Parameter Name="strWhere" Type="string" DefaultValue=""   />
	</SelectParameters>
</asp:ObjectDataSource>