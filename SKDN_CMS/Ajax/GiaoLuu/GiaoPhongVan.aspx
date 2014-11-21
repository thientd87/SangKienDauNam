<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GiaoPhongVan.aspx.cs" Inherits="DFISYS.Ajax.GiaoLuu.GiaoPhongVan" %>

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
                        Danh sách người phỏng vấn</h1>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView Width="100%" ID="grdListPhongVan" runat="server" CssClass="gtable sortable"
                        EmptyDataText="<span style='color:Red'><b>Không có người phỏng vấn !</b></span>"
                        AutoGenerateColumns="False" AllowPaging="false" OnRowDataBound="grdListPhongVan_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="User">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="lsUser" Width="150">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle CssClass="vertical-align-top tieudelist" VerticalAlign="Top"></ItemStyle>
                                <HeaderStyle Width="150px" CssClass="valign-middle" />
                                <ItemStyle CssClass="valign-middle" Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tên hiển thị">
                                <ItemTemplate>
                                    <input type="text" style="width: 100%;" name="nameManager" value="<%#Eval("ChannelResponse_NameManager")%>" />
                                </ItemTemplate>
                                <ItemStyle CssClass="vertical-align-top tieudelist" VerticalAlign="Top"></ItemStyle>
                                <HeaderStyle Width="350px" CssClass="valign-middle" />
                                <ItemStyle CssClass="valign-middle" Width="350px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Kích hoạt">
                                <ItemTemplate>
                                    <input type="checkbox" name="isActive" <%# Convert.ToBoolean(Eval("IsActive")) ? "checked='checked'" : "" %> />
                                </ItemTemplate>
                                <ItemStyle CssClass="vertical-align-top tieudelist" VerticalAlign="Top"></ItemStyle>
                                <HeaderStyle Width="150px" CssClass="valign-middle" />
                                <ItemStyle CssClass="valign-middle" Width="150px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Thao tác">
                                <ItemTemplate>
                                    <div style="display: none">
                                        <input type="text" name="hID" value="<%# Eval("ChannelResponse_ID") %>" /></div>
                                    <a style="float: left;" href="javascript:void(0)" onclick="DeleteItem(this)">
                                        <img src="/Images/Icons/delete.gif" /></a> <a style="float: left;" onclick="SaveItem(this)"
                                            href="javascript:void(0)">
                                            <img src="/Images/save.gif" border="0" /></a>
                                </ItemTemplate>
                                <HeaderStyle Width="100px" CssClass="valign-middle" />
                                <ItemStyle CssClass="valign-middle" Width="100px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <table  id="giaoPhongVan"  cellpadding="5" cellspacing="2">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Thêm mới</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            User:
                        </td>
                        <td valign="top" style="width: 150px;" class="valign-middle">
                            <asp:DropDownList runat="server" ID="lsUserNew" Width="150" >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Tên hiển thị:
                        </td>
                        <td valign="top" style="width: 350px;" class="valign-middle">
                            <input type="text"  id="name" style="width: 100%;">
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Kích hoạt:
                        </td>
                        <td valign="top" style="width: 150px;" class="valign-middle">
                            <input type="checkbox" checked="checked" id="active">
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        </td>
                        <td style="width: 100px;" class="valign-middle">
                            <asp:Button runat="server" CssClass="button white" Text="Lưu" ID="btInsert" />
                            &nbsp;
                            <input class="button white" type="button" onclick="$.facebox.close()" value="Đóng" />
                        </td>
                    </tr>
                </table>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
