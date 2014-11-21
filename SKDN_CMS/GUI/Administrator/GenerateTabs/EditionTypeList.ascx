<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditionTypeList.ascx.cs" Inherits="Portal.GUI.Administrator.GenerateTabs.EditionTypeList" %>
<script language="javascript" src="/scripts/Grid.js"></script>
<link href="/styles/common.css" rel="stylesheet" type="text/css" />
<link href="/styles/portal.css" rel="stylesheet" type="text/css" />

<link href="/styles/common.css" rel="stylesheet" type="text/css" />
<link href="/styles/Gallery/common.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="/styles/Newsedit.css" />
<link href="/styles/Core.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="/styles/Portal.css" type="text/css">

<link href="/styles/backend_menu.css" rel="stylesheet" type="text/css" />

<table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr>
        <td>
            <table cellpadding="0" cellspacing="5" width="100%">
                <tr>
                    <td colspan="2">
                        <asp:GridView Width="100%" ID="gvData" runat="server" HeaderStyle-CssClass="grdHeader" RowStyle-CssClass="grdItem"  ShowFooter="True"
                                AlternatingRowStyle-CssClass="grdAlterItem" AutoGenerateColumns="False" AllowPaging="True" DataSourceID="odsData" PageSize="12" OnRowDataBound="gvData_RowDataBound" OnRowCommand="gvData_RowCommand" OnRowCancelingEdit="gvData_RowCancelingEdit" OnRowEditing="gvData_RowEditing" OnRowUpdating="gvData_RowUpdating" OnRowDeleting="gvData_RowDeleting"> 
                            <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <input type="checkbox" id="chkAll" onclick="CheckAll();"/>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server"/>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            &nbsp;
                                        </FooterTemplate>
                                        <HeaderStyle Width="2%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nhóm chuyên mục">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEditionName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EditionName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <center>
                                                <asp:TextBox ID="txtEditEditionName" runat="server" Width="98%" Text='<%# DataBinder.Eval(Container.DataItem,"EditionName") %>'></asp:TextBox>
                                            </center>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <center>
                                                <asp:TextBox ID="txtEditionName" runat="server" Width="98%"></asp:TextBox>
                                            </center>
                                        </FooterTemplate>   
                                        <HeaderStyle Width="30%" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Đường dẫn">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEditionDisplayURL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EditionDisplayURL") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <center>
                                                <asp:TextBox ID="txtEditEditionDisplayURL" runat="server" Width="98%" Text='<%# DataBinder.Eval(Container.DataItem,"EditionDisplayURL") %>'></asp:TextBox>
                                            </center>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <center>
                                                <asp:TextBox ID="txtEditionDisplayURL" runat="server" Width="98%"></asp:TextBox>
                                            </center>
                                        </FooterTemplate>   
                                        <HeaderStyle Width="20%" />
                                    </asp:TemplateField>                                    
                                    
                                    <asp:TemplateField HeaderText="Ch&#250; giải">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEditionDes" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EditionDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <center>
                                                <asp:TextBox ID="txtEditEditionDes" runat="server" Width="98%" Text='<%# DataBinder.Eval(Container.DataItem,"EditionDescription") %>'></asp:TextBox>
                                            </center>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <center>
                                                <asp:TextBox ID="txtEditionDes" runat="server" Width="98%"></asp:TextBox>
                                            </center>
                                        </FooterTemplate>   
                                        <HeaderStyle Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tuỳ chọn">
							            <ItemTemplate>
								            <asp:ImageButton id="imgEdit" runat="server" ImageUrl="~/Images/icons/edit.gif" AlternateText="Sửa nội dung" CausesValidation="False" CommandName="Edit" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"EditionType_ID")%>'></asp:ImageButton>
							            </ItemTemplate>
							            <EditItemTemplate>
								            <asp:ImageButton id="imgSave" runat="server" ImageUrl="~/Images/icons/save.gif" AlternateText="Ghi lại" CommandName="Update" CausesValidation="False" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"EditionType_ID")%>'></asp:ImageButton>
								            &nbsp;
								            <asp:ImageButton id="imgCancel" runat="server" ImageUrl="~/Images/icons/stop.gif" AlternateText="Tạm dừng thay đổi" CommandName="Cancel" CausesValidation="False"></asp:ImageButton>
								            &nbsp;
								            <asp:ImageButton id="imgDel" runat="server" ImageUrl="~/Images/icons/cancel.gif" AlternateText="Xóa nội dung này" CommandName="Delete" CausesValidation="False"></asp:ImageButton>
							            </EditItemTemplate>
							            <FooterTemplate>
							                <center>
							                    <asp:ImageButton id="btnNew" runat="server" CausesValidation="False" ImageUrl="~/Images/icons/new.gif" CommandName="NewRow"></asp:ImageButton>
						                    </center>
							            </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
							          </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="grdItem" />
                                <HeaderStyle CssClass="grdHeader" />
                                <AlternatingRowStyle CssClass="grdAlterItem" />
                                <PagerSettings Visible="False"/>     
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </td>
    </tr>   
</table>

<asp:ObjectDataSource ID="odsData" TypeName="Portal.BO.Editoral.EditionType.EditionTypeHelper" DeleteMethod="Delete" SelectMethod="SelectAll" InsertMethod="Insert" UpdateMethod="Update" runat="server">
    <DeleteParameters>
        <asp:Parameter Name="EditionType_ID" Type="Int32" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="EditionName" Type="String" />
        <asp:Parameter Name="EditionDescription" Type="String" />
        <asp:Parameter Name="EditionType_ID" Type="Int32" />
        <asp:Parameter Name="EditionDisplayURL" Type="String" />
    </UpdateParameters>
    <InsertParameters>
        <asp:Parameter Name="EditionName" Type="String" />
        <asp:Parameter Name="EditionDescription" Type="String" />
        <asp:Parameter Name="EditionDisplayURL" Type="String" />
    </InsertParameters>

</asp:ObjectDataSource>