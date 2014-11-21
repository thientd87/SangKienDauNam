<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThreadList.ascx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.NewsThread.ThreadList" %>
<table width="100%">
    <tr>
        <td class="Edit_Head_Cell">
            <asp:Label ID="lblLabel" runat="server" Text="Danh sách chủ đề"></asp:Label></td>
    </tr>    
</table>
<br />
<asp:GridView ID="gvData" runat="server" AlternatingRowStyle-CssClass="grdAlterItem"
    AutoGenerateColumns="False" HeaderStyle-CssClass="grdHeader"
    PageSize="12" RowStyle-CssClass="grdItem" Width="100%" OnRowCommand="gvData_RowCommand">
    <PagerSettings Visible="False" />
    <Columns>
        <asp:BoundField DataField="Title" HeaderText="T&#234;n chủ đề ">
            <ItemStyle Width="75%" />
        </asp:BoundField>
        <asp:TemplateField>
            <HeaderTemplate>
                Danh sách bài
            </HeaderTemplate>
            <ItemStyle Width="10%" HorizontalAlign="Center" />
            <ItemTemplate>
                <a href="threaddetails/<%#DataBinder.Eval(Container.DataItem,"Thread_ID")%>.aspx"><img src="/images/icons/folder_view.gif" border="0"/></a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle Width="5%" HorizontalAlign="Center" />
                <ItemTemplate>
<asp:ImageButton id="imgDel" runat="server" ImageUrl="~/Images/icons/cancel.gif" AlternateText="Xóa"
CommandName="del" OnClientClick="return confirm('Bạn chắc chắn muốn xóa?')" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Thread_ID")%>' CausesValidation="False"></asp:ImageButton>                
                </ItemTemplate>            
            <HeaderTemplate>
                Xóa
            </HeaderTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle Width="5%" HorizontalAlign="Center" />
            <ItemTemplate>
<asp:ImageButton id="imgEdit" runat="server" ImageUrl="~/Images/icons/edit.gif" AlternateText="Sửa chủ đề"
CausesValidation="False" CommandName="editCat" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Thread_ID")%>'></asp:ImageButton>            
            </ItemTemplate>
            <HeaderTemplate>
                Sửa
            </HeaderTemplate>
        </asp:TemplateField>
    </Columns>
    <RowStyle CssClass="grdItem" />
    <HeaderStyle CssClass="grdHeader" />
    <AlternatingRowStyle CssClass="grdAlterItem" />
</asp:GridView>

<table width="100%">
    <tr>
        <td align="right" class="Menuleft_Item" style="height: 21px">
            | <asp:LinkButton CssClass="normalLnk" ID="lbThemMoi" runat="server" OnClick="cmdThemMoi_Click">Thêm mới</asp:LinkButton> |
        </td>
    </tr>
</table>
