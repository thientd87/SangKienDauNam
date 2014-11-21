<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewUser.ascx.cs" Inherits="DFISYS.GUI.Users.User.ViewUser" %>
<table width="100%" cellpadding="0" cellspacing="0" border="0" style="margin-bottom: 10px;">
    <tr>
        <td colspan="6">
            <h1>
                Danh sách người dùng</h1>
        </td>
    </tr>
    <tr>
        <td width="70px" class="Viewuser_Head">
            Thuộc kênh:
        </td>
        <td align="left" width="150" class="Viewuser_Head">
            <asp:DropDownList ID="ddlChannel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlChannel_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td width="80px">
            Nhóm vai trò:
        </td>
        <td align="left" width="150px">
            <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td class="ms-formlabel" style="width: 149px">
            Tìm theo tên truy nhập:
        </td>
        <td align="left">
            <asp:TextBox ID="txtSearch" CssClass="big" runat="server"></asp:TextBox>
            <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" OnClick="btnTimKiem_Click" />
        </td>
    </tr>
</table>
<asp:DataGrid ID="gridUser" runat="server" AllowCustomPaging="True" AllowPaging="True"
    AllowSorting="True" AutoGenerateColumns="False" PageSize="50" CssClass="gtable collapsed" DataKeyField="User_ID"
    ShowFooter="True" Width="100%">
    <PagerStyle CssClass="grdHeader" HorizontalAlign="Left" Mode="NumericPages" />
    <Columns>
        <asp:TemplateColumn>
            <HeaderTemplate>
                <img height="1" src="/images/icons/n.gif" width="20">
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="chkDelete" runat="server" />
            </ItemTemplate>
            <FooterTemplate>
                <asp:ImageButton ID="_DeleteButton" runat="server" AlternateText="Delete Record"
                    CommandName="Delete" ImageUrl="/images/icons/Delete.gif" />
            </FooterTemplate>
            <ItemStyle Width="20px" />
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <HeaderTemplate>
                <img height="1" src="/images/icons/n.gif" width="20">
            </HeaderTemplate>
            <ItemTemplate>
                <asp:ImageButton ID="_editButton" runat="server" AlternateText="Edit Record" CommandName="Edit"
                    ImageUrl="/images/icons/Edit.gif" />
            </ItemTemplate>
            <FooterTemplate>
                <asp:ImageButton ID="_insertButton" runat="server" AlternateText="Insert Record"
                    CommandName="Insert" ImageUrl="/images/icons/Insert.gif" />
            </FooterTemplate>
            <ItemStyle Width="20px" />
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="T&#234;n truy nhập" SortExpression="User_ID">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "User_ID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Tên người dùng" SortExpression="User_Name">
            <ItemTemplate>
                <%# Eval("User_Name")%>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Địa chỉ Email" SortExpression="User_Email">
            <ItemTemplate>
                <%# Eval("User_Email")%>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Last Access" SortExpression="User_ModifiedDate">
            <ItemTemplate>
                <%# Eval("User_ModifiedDate") %>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Điện thoại" SortExpression="User_PhoneNum">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "User_PhoneNum") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Tình trạng" SortExpression="User_isActive">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "User_isActive").ToString()=="True"?"Đã kích hoạt":"Chưa kích hoạt" %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <HeaderTemplate>
                <a>Phân quyền</a></HeaderTemplate>
            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
            <ItemTemplate>
                <center>
                    <asp:ImageButton ID="btnAssignPermisson" runat="server" CommandName="Assign" ImageUrl="/images/icons/new.gif"
                        AlternateText="Gán quyền" />
                </center>
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
</asp:DataGrid>
 
<br />
<asp:Label ID="_errorLabel" runat="server" CssClass="errorMessage" Visible="False"></asp:Label>
<script src="Scripts/Common.js"></script>
<script>
    function Validate() {
        var isValid;
        isValid = CheckRequire("txtSearch", "Ô Search");
        if (!isValid)
            return false;
        else
            return true;
    }
</script>
