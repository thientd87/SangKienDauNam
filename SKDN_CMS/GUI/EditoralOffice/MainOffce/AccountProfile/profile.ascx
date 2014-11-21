<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="profile.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.AccountProfile.profile" %>
<script type="text/javascript">
    var shortPass = 'Too short'
    var badPass = 'Bad'
    var goodPass = 'Good'
    var strongPass = 'Strong'

    function passwordStrength(password, username) {
        score = 0;

        //password < 4
        if (password.length < 4) {return shortPass;}

        //password == username
        if (password.toLowerCase() == username.toLowerCase()) return badPass;

        //password length
        score += password.length * 4;
        score += (checkRepetition(1, password).length - password.length) * 1;
        score += (checkRepetition(2, password).length - password.length) * 1;
        score += (checkRepetition(3, password).length - password.length) * 1;
        score += (checkRepetition(4, password).length - password.length) * 1;

        //password has 3 numbers
        if (password.match(/(.*[0-9].*[0-9].*[0-9])/)) score += 5;
        
        //password has 2 symbols
        if (password.match(/(.*[!,@,#,$,%,^,&,*,?,_,~].*[!,@,#,$,%,^,&,*,?,_,~])/)) score += 5;

        //password has Upper and Lower chars
        if (password.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) score += 10;

        //password has number and chars
        if (password.match(/([a-zA-Z])/) && password.match(/([0-9])/)) score += 15;
        //
        //password has number and symbol
        if (password.match(/([!,@,#,$,%,^,&,*,?,_,~])/) && password.match(/([0-9])/)) score += 15;

        //password has char and symbol
        if (password.match(/([!,@,#,$,%,^,&,*,?,_,~])/) && password.match(/([a-zA-Z])/)) score += 15;

        //password is just a nubers or chars
        if (password.match(/^\w+$/) || password.match(/^\d+$/)) score -= 10;
    }

    // checkRepetition(1,'aaaaaaabcbc')   = 'abcbc'
    // checkRepetition(2,'aaaaaaabcbc')   = 'aabc'
    // checkRepetition(2,'aaaaaaabcdbcd') = 'aabcd'

    /*
    function CheckRepetition(pLen, str) {
        res = "";
        for (i = 0; i < str.length; i++) {
            repeated = true;
            for (j = 0; j < pLen && (j + i + pLen) < str.length; j++)
                repeated = repeated && (str.charAt(j + i) == str.charAt(j + i + pLen));
            if (j < pLen) repeated = false;
            if (repeated) {
                i += pLen - 1;
                repeated = false;
            }
            else {
                res += str.charAt(i);
            }
        }
        return res;
    }
    */

    var _passvalidate = false;
    
    function CheckPassword(password) {
        if (password.length < 6) { return "Mật khẩu phải có ít nhất 6 ký tự"; } 
        
        if (password.indexOf($('#<%=txtUserName.ClientID %>').val()) != -1) {
            return "Mật khẩu không được trùng với tên đăng nhập";
        }

        //password has number and chars
        if (password.match(/([a-zA-Z])/) && password.match(/([0-9])/)) {
            _passvalidate = true;
        } else {
            return "Mật khẩu phải chứa cả số và chữ";
        }
        return "";
    }

    $(function () {
        $('#<%=txtPassword.ClientID %>').keyup(function(){
            $('#result').html(CheckPassword($('#<%=txtPassword.ClientID %>').val()));
        });
    });

</script>
<style>
    #result{color:red;margin-left:10px;float:left}
    input[type='text'], input[type='password']{float:left}
    .vertical-align td {vertical-align: middle;}
</style>
<h1><asp:Literal ID="ltrMessage" runat="server" Text="Thay đổi thông tin cá nhân"></asp:Literal></h1>
<table cellpadding="2" cellspacing="2" border="0" width="100%" class="gtable vertical-align">
     
    <tr>
        <td  style="width: 130px">
            Tên đăng nhập
        </td>
        <td>
            <asp:TextBox ID="txtUserName" runat="server" Width="400px" CssClass="ms-long" Enabled="false"></asp:TextBox></td>
    </tr>
    <tr>
        <td  style="width: 130px">
            Mật khẩu hiện thời</td>
        <td>
            <asp:TextBox ID="txtCurrentPassword" runat="server" Width="400px" CssClass="ms-long" TextMode="Password"></asp:TextBox></td>
    </tr>
    <tr>
        <td  style="width: 130px">
            Mật khẩu mới</td>
        <td>
            <asp:TextBox ID="txtPassword" runat="server" Width="400px" CssClass="ms-long" TextMode="Password"></asp:TextBox><div id="result"></div>
        </td>
    </tr>
    <tr>
        <td  style="width: 130px; ">
            Gõ lại mật khẩu</td>
        <td style="">
            <asp:TextBox ID="txtPasswordAgian" Width="400px" runat="server" CssClass="ms-long" TextMode="Password"></asp:TextBox></td>
    </tr>
    <tr>
        <td  style="width: 130px; ">
            Họ tên</td>
        <td style="">
            <asp:TextBox ID="txtFullName" Width="400px"  runat="server" CssClass="ms-long"></asp:TextBox></td>
    </tr>
    <tr>
        <td  style="width: 130px; ">
            Email</td>
        <td style="">
            <asp:TextBox ID="txtEmail" Width="400px" runat="server" CssClass="ms-long"></asp:TextBox></td>
    </tr>
    <tr>
        <td  style="width: 130px; ">
            Địa chỉ</td>
        <td style="">
            <asp:TextBox ID="txtAddress" Width="400px" runat="server" CssClass="ms-long"></asp:TextBox></td>
    </tr>
    <tr>
        <td  style="width: 130px; ">
            Số điện thoại</td>
        <td style="">
            <asp:TextBox ID="txtPhone" Width="400px" runat="server" CssClass="ms-long"></asp:TextBox></td>
    </tr>
    <tr>
        <td  style="width: 130px; ">
            YM</td>
        <td style="">
            <asp:TextBox ID="txtYM" Width="400px"  runat="server" CssClass="ms-long"></asp:TextBox></td>
    </tr>
    <tr>
        <td  style="width: 130px; ">
            Website</td>
        <td style="">
            <asp:TextBox ID="txtWebsite" Width="400px" runat="server" CssClass="ms-long"></asp:TextBox></td>
    </tr>   
    <tr>
        <td  style="width: 130px; ">
        </td>
        <td style="">
            <asp:Button ID="btnSave" runat="server" Text="Lưu lại" OnClientClick="return Validate();" OnClick="btnSave_Click" CssClass="ms-input" />
            <input id="Reset1" type="reset" value="Viết lại" class="ms-input" /></td>
    </tr>
</table>
<asp:ObjectDataSource ID="objsoure" UpdateMethod="UpdateUserInfo" runat="server" TypeName="DFISYS.BO.Editoral.Profile.Profile">
    <UpdateParameters>
        <asp:Parameter Name="_user_id" Type="string" DefaultValue="" />
        <asp:ControlParameter Name="_current_password" Type="String" DefaultValue="" ControlID="txtCurrentPassword" PropertyName="Text" />
        <asp:ControlParameter Name="_new_password" Type="string" DefaultValue="" ControlID="txtPassword" PropertyName="Text" />
        <asp:ControlParameter Name="_user_name" Type="String" DefaultValue="" ControlID="txtFullName" PropertyName="Text" />
        <asp:ControlParameter Name="_email" Type="string" DefaultValue="" ControlID="txtEmail" PropertyName="Text" />
        <asp:ControlParameter Name="_address" Type="string" DefaultValue="" ControlID="txtAddress" PropertyName="Text" />
        <asp:ControlParameter Name="_phone" Type="string" DefaultValue="" ControlID="txtPhone" PropertyName="Text" />
        <asp:ControlParameter Name="_ym" Type="string" DefaultValue="" ControlID="txtYM" PropertyName="Text" />
        <asp:ControlParameter Name="_website" Type="string" DefaultValue="" ControlID="txtWebsite" PropertyName="Text" />
    </UpdateParameters>
</asp:ObjectDataSource>

<script type="text/javascript">
    //Validate trước khi Save
    function Validate() {
        var current_password = $("#<%=txtCurrentPassword.ClientID %>");
        var password = $("#<%=txtPassword.ClientID %>");
        var password_again = $("#<%=txtPasswordAgian.ClientID %>");

        if (current_password.val() == "") {
            alert("Bạn chưa nhập mật khẩu hiện tại");current_password.focus();  return false;
        }
        if (password.val() == "") { alert("Bạn chưa nhập mật khẩu mới"); password.focus(); return false; }
        if (_passvalidate) {
            if (current_password.val() == password.val()) {
                alert("Mật khẩu mới không được trùng với mật khẩu cũ");
                password.focus();
                return false;
            }
            if (password.val() != password_again.val()) {
                alert("Gõ lại mật khẩu");
                password_again.focus();
                return false;
            }
        }
        else {
            CheckPassword(password);
            current_password.focus();
            return false;
        }

        return true;
    }    
</script>
