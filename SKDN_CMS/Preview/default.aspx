<%@ Page Language="C#" AutoEventWireup="true" Codebehind="default.aspx.cs" Inherits="DFISYS.GUI.Front_End.bacth_test8._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="ROBOTS" content="NOINDEX, NOFOLLOW">
</head>
<body>
    <form id="form1" runat="server">
        <table id="tableNewsPreview" cellpadding="0" cellspacing="0" width="100%">
            <tbody>
                <tr>
                    <td class="text_noibat_cacbaikhac" style="padding-bottom: 5px;">
                        <div class="cms_SubTitle" style="padding-bottom: 5px;">
                        </div>
                        <span class="cms_blue" style="color:#013567;font-family:Arial;font-size:16px;font-weight:bold">
                            <asp:Literal ID="ltrNewsTitle" runat="server"></asp:Literal>                            
                        </span>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <table style="padding-right: 10px;" align="left" border="0" cellpadding="0">
                            <tbody>
                                <tr>
                                    <td align="left">
                                        <asp:Image ID="imgNewsAvatar" runat="server"></asp:Image><br />
                                        <i>
                                            <asp:Literal runat="server" ID="ltrImageNote"></asp:Literal></i>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <span class="cms_new_bold" style="color:#333333;font-family:Times New Roman;font-size:12pt;font-weight:bold;vertical-align:top;">
                            <asp:Literal runat="server" ID="ltrNewsInit"></asp:Literal></span>
                        <br>
                        <br>
                        <span class="cms_news_normal10">
                            <asp:Literal runat="server" ID="ltrNewsDetail"></asp:Literal>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
