<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssignPermission.ascx.cs" Inherits="DFISYS.GUI.Users.User.AssignPermission" %>
<link rel="stylesheet" type="text/css" href="/styles/Users.css" />
<table width="100%" height=300px>
    <tr>
        <td colspan="3" >
            <input id="txtCount" runat="server" enableviewstate="true" type="hidden" /></td>
    </tr>
    <tr>
        <td colspan="3" valign="top" class="Viewuser_Head">
            Bảng gán quyền cho User: &nbsp;<font color="red"><asp:Label ID="lblUserName" runat="server"></asp:Label></font>
        </td>
    </tr>
    <tr>
        <td valign="top" width="200">
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                
                <tr>
                    <td class="Viewuser_Title_cell"  valign="top">Chọn vai trò</td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:ListBox ID="lbxRole" runat="server" AutoPostBack="True" Width="100%" Height="150px" OnSelectedIndexChanged="lbxRole_SelectedIndexChanged"></asp:ListBox>
                    </td>
                </tr>
            </table>
        </td>
        <td valign="top" style="border-left: 1px #e5e5e5 solid;border-right: 1px #e5e5e5 solid">
        <table style="width: 100%">
                <tr>
                    <td class="Viewuser_Title_cell" valign="top">Chọn Quyền</td>
                </tr>
                <tr>
                    <td  valign="top" height="100%" class="Viewuser_Title_cell">
                        <asp:CheckBoxList ID="clbPermission" runat="server" CssClass="checkListBoxText" RepeatColumns="2">
                        </asp:CheckBoxList>
                        <asp:Label ID="lblPermission" runat="server" Text="Đề nghị chọn quyền mặc định"></asp:Label></td>
                </tr>
            </table>
        </td>
        <td valign="top" style="display: none" >
            <table style="width: 100%">
                <tr>
                    <td class="Viewuser_Title_cell" valign="top">Quyền được áp dụng trên mục</td>
                </tr>
                <tr>
                    <td valign="top" height="100%" class="Viewuser_Title_cell">
                        <asp:CheckBoxList  ID="clbCategory"   runat="server" RepeatColumns="1" RepeatDirection="Horizontal">
                        </asp:CheckBoxList></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td valign="top" colspan="3" align="center" class="Menuleft_box4" >
            <asp:Panel ID=PanelButton runat=server>
            <asp:Button ID="btnAssignPermission" CssClass="btnUpdate" runat="server" Text="Gán quyền mới" OnClick="btnAssignPermission_Click1" />&nbsp;
            <asp:Button ID="btnUpdate" runat="server" CssClass="btnUpdate" Text="Cập nhật" Visible="False" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnCancel" CssClass="btnUpdate" runat="server" OnClick="btnCancel_Click" Text="Huỷ lệnh" Visible="False" />&nbsp;
            <asp:Button ID="btnGoback" CssClass="btnUpdate" runat="server" OnClick="btnGoback_Click" Text="Quay lại" />
           </asp:Panel>
       </td>
    </tr>
</table>
<script src="/scripts/Common.js"></script>
<script>

var selectedRole = false;
function SelectedRole()
{
    selectedRole = true;
}
function Validate()
{
    if(!selectedRole)
    {
        alert("Đề nghị chọn Role");
        return false;
    }
        
    var isValid = false;
    for(i=0;i<GetControlByName('txtCount').value;i++)
    {
        var input = GetControlByName('cblPemission_'+i)
        if(input.checked)
        {
            isValid = true;
        }
    }
    if(!isValid)
    {
        alert("Đề nghị chọn quyền");
        return false;
    }
    return true;
}

$("#<%=clbCategory.ClientID %> input[type='checkbox']").attr('checked', 'checked');
</script>

<table style="width: 100%;">
    <tr>
        <td valign="top" align=center>
            <asp:Label CssClass='ms-formlabel"' ForeColor="Red" ID="lblMessage" runat="server"></asp:Label>
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>
<table style="width: 100%;background-color:#C3C3C3;">
    <tr>
        <td class="Viewuser_Title_cell" colspan="4" valign="top">
            <asp:Label ID="lblMessageXoa" runat="server" CssClass='ms-formlabel"' Font-Bold="True" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
        <td class="Viewuser_Title_cell"  valign="top">&nbsp; &nbsp;Vai trò &nbsp;
            <asp:Button ID="btnRemoveVaiTro" CssClass="btnUpdate" runat="server" Text="Xóa vai trò" OnClick="btnRemoveVaiTro_Click" /></td>
         <td class="Viewuser_Title_cell"   valign="top" style="display: none">Chuyên mục &nbsp;
             <asp:Button ID="btnRemoveChuyenMuc" CssClass="btnUpdate" runat="server" Text="Xóa CM" OnClick="btnRemoveChuyenMuc_Click" /></td>
          <td class="Viewuser_Title_cell"  valign="top">Quyền hạn &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
              <asp:Button ID="btnRemoveQuyen" CssClass="btnUpdate" runat="server" Text="Xóa Quyền" OnClick="btnRemoveQuyen_Click" /></td>
           
    </tr>
    <tr>
        <td valign="top" align="center" width="25%">
            <asp:ListBox ID="lstRoles" runat="server" Width="100%" Height="120" AutoPostBack="true" OnSelectedIndexChanged="lstRoles_SelectedIndexChanged"></asp:ListBox>
        </td>
        <td valign="top" align="center" width="25%" style="display: none"> 
            <asp:ListBox ID="lstCat" runat="server" Width="100%" Height="120" AutoPostBack="true" OnSelectedIndexChanged="lstCat_SelectedIndexChanged"></asp:ListBox>
        </td>
        <td valign="top" align="center" width="40%">
            <asp:ListBox ID="lstPer" runat="server" Width="100%" Height="120"></asp:ListBox>
        </td>
        
    </tr>
</table>
