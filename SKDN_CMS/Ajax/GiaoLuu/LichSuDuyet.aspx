<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LichSuDuyet.aspx.cs" Inherits="DFISYS.Ajax.GiaoLuu.LichSuDuyet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/theme/grid.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 85px;
            padding-top: 10px;
        }
        .style2
        {
            width: 65%;
        }
        .style3
        {
            width: 200px;
        }
    </style>
</head>
<body>
    <form id="form1" onsubmit="return false;" runat="server">
    <div>
        <table class="gtable">
            <tr>
                <td colspan="2">
                    <h1 style="text-align: center">
                        Danh sách các lần duyệt</h1>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView Width="100%" ID="grdListPhongVan" runat="server" CssClass="gtable sortable"
                        EmptyDataText="<span style='color:Red'><b>Không có người phỏng vấn !</b></span>"
                        AutoGenerateColumns="False" AllowPaging="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Thời gian">
                                <ItemTemplate>
                                    <a href="/office/giao_luu_editepublic.aspx?courseLogID=<%#Eval("CourseLog_ID") %>">
                                        <%# Convert.ToDateTime(Eval("CourseLog_Date")).ToString("hh:mm:ss - dd/MM/yyyy")%></a>
                                </ItemTemplate>
                                <ItemStyle CssClass="vertical-align-top tieudelist" VerticalAlign="Top"></ItemStyle>
                                <HeaderStyle Width="150px" CssClass="valign-middle" />
                                <ItemStyle CssClass="valign-middle" Width="150px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 100px;" class="valign-middle">
                    <input class="button white" type="button" onclick="$.facebox.close()" value="Đóng" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
