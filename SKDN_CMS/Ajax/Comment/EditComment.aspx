<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditComment.aspx.cs" Inherits="DFISYS.Ajax.Comment.EditComment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/theme/grid.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" onsubmit="return false;" runat="server">
    <div>
        <table id="saveComment" class="gtable">
            <tr>
                <td colspan="2">
                    <h1 style="text-align: center">
                        Sửa nội dung bình luận</h1>
                </td>
            </tr>
            <tr>
                <td>
                    User:
                </td>
                <td>
                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="big" Text='<%# Eval("Comment_User") %>'
                        ID="txtUser"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Email
                </td>
                <td>
                   <div style="display: none"> <input type="text" id="hidCommentID" value="<% = commentId %>" /></div>
                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="big" Text='<%# Eval("Comment_Email") %>'
                        ID="txtEmail"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: middle">
                    Nội dung
                </td>
                <td>
                    <asp:TextBox runat="server" ClientIDMode="Static" Rows="20" CssClass="big" ID="txtContent"
                        Text='<%# Eval("Comment_Content") %>' TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button runat="server" CssClass="button white"
                        Text="Lưu" ID="btnSave" /> &nbsp; 
                        <asp:Button runat="server" CssClass="button white"
                        Text="Lưu và duyệt" ID="btnSaveApprove" />  &nbsp; 
                    <asp:Button 
                        runat="server" CssClass="button white"
                        Text="Xóa" ID="btnDelete" onclick="btnDelete_Click" /> &nbsp; 
                        <input class="button white" type="button" onclick="$.facebox.close()" value="Hủy"/>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
