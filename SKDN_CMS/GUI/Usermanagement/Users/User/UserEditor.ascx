<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserEditor.ascx.cs" Inherits="DFISYS.GUI.Users.User.UserEditor" %>

<table border="0" class="gtable" width="100%">
    <tr>
        <td class="Menuleft_box4" colspan="2" style="height: 21px">
           <h1> <asp:Label ID="lblTitle" runat="server"></asp:Label></h1></td>
    </tr>
    <tr>
        <td class="ms-formlabel" style="width: 139px">
            Kênh:</td>
        <td>
            <asp:DropDownList ID="ddlChannel" runat="server">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 139px">
            <span class="ms-formlabel">Tên truy cập:</span></td>
        <td>
            <asp:TextBox ID="txtUser_ID" onkeypress = CheckUserName(this) runat="server" CssClass="big" Enabled="True"
                MaxLength="200"></asp:TextBox> <span class="ms-formlabel" style="color: #ff0066">*</span></td>
    </tr>
    <tr>
        <td style="width: 139px">
            <span class="ms-formlabel" >Tên người dùng:</span></td>
        <td >
            <asp:TextBox ID="txtUser_Name" runat="server" CssClass="big" Enabled="True"
                MaxLength="1000"></asp:TextBox> <span class="ms-formlabel" style="color: #ff0066">*</span></td>
    </tr>
    <tr>
        <td style="width: 139px">
            <span class="ms-formlabel">Mật khẩu:</span></td>
        <td>
                    
               
            <asp:TextBox ID="txtUser_Pwd" onkeypress = CheckUserName(this) runat="server" CssClass="big" Enabled="True"
                MaxLength="50" TextMode="Password"></asp:TextBox> <span class="ms-formlabel" style="color: #ff0066">*</span><span
                    style="color: #ff0066"></span> <asp:CheckBox CssClass="ms-formlabel" ID="cbxPassword" runat="server" AutoPostBack="True" OnCheckedChanged="cbxPassword_CheckedChanged"
                Text="Đổi mật khẩu" /></td>
                
          </td>
    </tr>
    <tr >
        <td style="width: 139px">
            <span class="ms-formlabel"> Email:</span></td>
        <td>
            <asp:TextBox ID="txtUser_Email" runat="server" CssClass="big" Enabled="True"
                MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr >
        <td style="width: 139px">
            <span class="ms-formlabel">Địa chỉ:</span></td>
        <td>
            <asp:TextBox ID="txtUser_Address" runat="server" CssClass="big" Enabled="True"
                MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
        <td nowrap="noWrap" style="width: 139px">
            <span class="ms-formlabel" >Số điện thoại:</span></td>
        <td >
            <asp:TextBox ID="txtUser_PhoneNum" runat="server" CssClass="big" Enabled="True"
                MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 139px">
            <span class="ms-formlabel">Chức vụ:</span></td>
        <td>
            <asp:TextBox ID="txtUser_Im" runat="server" CssClass="big" Enabled="True"
                MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 139px">
            <span class="ms-formlabel"> Website:</span></td>
        <td>
            <asp:TextBox ID="txtUser_Website" runat="server" CssClass="big" Enabled="True"
                MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 139px">
            <span class="ms-formlabel">Tình trạng kích hoạt:</span></td>
        <td>
            <asp:CheckBox ID="chkUser_isActive" runat="server" Enabled="True" />
            </td>
    </tr>
    <tr>
        <td style="width: 139px">
            &nbsp;</td>
        <td style="width: 399px">
            <asp:Button ID="cmdSave" runat="server" CssClass="btnUpdate" Text="Save" />&nbsp;
            <asp:Button ID="btnCancel" runat="server" CssClass="btnUpdate" CausesValidation="False" Text="Cancel" /></td>
    </tr>
    <tr>
        <td style="width: 139px">
        </td>
        <td style="width: 399px">
            <asp:Label ID="_errorLabel" runat="server" CssClass="errorMessage" Visible="False" ForeColor="Red"></asp:Label></td>
    </tr>
</table>
<script src="/scripts/Common.js"></script>
<script language=javascript>
try
{
    GetControlByName("txtUser_ID").focus();
}
catch(asdsad){}
    function Validate()
    {
        var isValid ;
        isValid = CheckRequire("txtUser_ID","User ID");
        if(!isValid)
            return false;
            
        isValid = CheckRequire("txtUser_Name","User Name");
        if(!isValid)
            return false;
        if(!GetControlByName("cbxPassword"))
        {
            isValid = CheckRequire("txtUser_Pwd","Password");
            if(!isValid)
                return false;
        }
        else
        {
            if(GetControlByName("cbxPassword").checked)
            {
                isValid = CheckRequire("txtUser_Pwd","Password");
                if(!isValid)
                    return false;
            }
        }
        isValid = EmailCheck("txtUser_Email");
        if(!isValid)
            return false;
            
        return true;            
    }
    

</script>