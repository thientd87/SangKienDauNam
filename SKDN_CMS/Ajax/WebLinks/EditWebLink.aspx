<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditWebLink.aspx.cs" Inherits="DFISYS.Ajax.WebLinks.EditWebLink" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/theme/grid.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" onsubmit="return false;" runat="server">
    <div>
        <table id="saveWeblink" class="gtable">
            <tr>
                <td colspan="2">
                    <h1 style="text-align: center">
                        Sửa Weblink</h1>
                </td>
            </tr>
            <tr>
                <td>
                    Nội dung:
                </td>
                <td>
                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="big" 
                        ID="txtName"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Đường link
                </td>
                <td>
                   <div style="display: none"> <input type="text" id="hidCommentID" value="<% = webLinkID %>" /></div>
                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="big" 
                        ID="txtURL"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: middle">
                    Ghi chú
                </td>
                <td>
                    <asp:TextBox runat="server"  Rows="10" CssClass="big" ID="txtContent" ClientIDMode="Static"
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Số thứ tự
                </td>
                <td>
                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="big" 
                        ID="txtSTT"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button runat="server" CssClass="button white"
                        Text="Lưu" ID="btnSave"  /> &nbsp; 
                        &nbsp; 
                    <asp:Button 
                        runat="server" CssClass="button white"
                        Text="Xóa" ID="btnDelete" /> &nbsp; 
                        <input class="button white" type="button" onclick="$.facebox.close()" 
                        value="Đóng"/>
                </td>
            </tr>
            
        </table>
    </div>
    </form>
</body>
</html>
