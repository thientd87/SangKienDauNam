<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditeVote.aspx.cs" Inherits="DFISYS.Ajax.Vote.EditeVote" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/theme/grid.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            white-space:nowrap;
        }
    </style>
</head>
<body>
    <form id="form1" onsubmit="return false;" runat="server">
    <div>
        <table id="saveVote" class="gtable">
            <tr>
                <td colspan="2">
                    <h1 style="text-align: center">
                        Sửa Vote</h1>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Nội dung:
                </td>
                <td>
                    <asp:TextBox runat="server" ClientIDMode="Static" CssClass="big" ID="txtName"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Chuyên mục
                </td>
                <td>
                    <asp:DropDownList ID="ddlChuyenmuc" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Thời gian
                </td>
                <td>
                    <div style="display: none">
                        <input type="text" id="hidVoteID" value="<% = voteID %>" /></div>
                    Từ
                    <asp:TextBox MaxLength="10" ID="txtFromDate" Width="75px" runat="server" CssClass="calendar" />
                    <a onclick="if(self.gfPop)gfPop.fPopCalendar(document.getElementById('<% = txtFromDate.ClientID %>'));return false;"
                        href="javascript:void(0)">
                        <img class="PopcalTrigger" height="21" src="/Scripts/DatePicker/datepicker.gif" width="34"
                            align="absMiddle" border="0">
                    </a><span>đến</span>
                    <asp:TextBox MaxLength="10" ID="txtToDate" Width="75px" runat="server" CssClass="calendar" />
                    <a onclick="if(self.gfPop)gfPop.fPopCalendar(document.getElementById('<% = txtToDate.ClientID %>'));return false;"
                        href="javascript:void(0)">
                        <img class="PopcalTrigger" height="21" src="/Scripts/DatePicker/datepicker.gif" width="34"
                            align="absMiddle" border="0">
                    </a>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Ghi chú
                </td>
                <td>
                    <asp:TextBox runat="server" ClientIDMode="Static" Rows="5" CssClass="big" ID="txtContent"
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Kích hoạt
                </td>
                <td>
                    <asp:CheckBox runat="server" ClientIDMode="Static" CssClass="big" ID="chkActive">
                    </asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td >
                    Danh sách lựa chọn
                </td>
                <td>
                    <asp:GridView Width="100%" ID="grdListVote" runat="server" CssClass="gtable sortable"
                        EmptyDataText="<span style='color:Red'><b>Không có vote !</b></span>" AutoGenerateColumns="False"
                        AllowPaging="false">
                        <Columns>
                            <asp:TemplateField HeaderText="STT">
                                <ItemTemplate>
                                    <input type="text" style="width: 100%; text-align: center;" id="voteitstt_<%#Eval("VoteIt_ID")%>" name="voteitstt_<%#Eval("VoteIt_ID")%>"
                                        value="<%# Eval("VoteIt_STT")%>" />
                                </ItemTemplate>
                                <HeaderStyle Width="20px" CssClass="valign-middle" />
                                <ItemStyle CssClass="valign-middle" Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nội dung">
                                <ItemTemplate>
                                    <input type="text" style="width: 100%;" id="voteitContent_<%#Eval("VoteIt_ID")%>"
                                        value="<%#Eval("VoteIt_Content")%>" />
                                </ItemTemplate>
                                <ItemStyle CssClass="vertical-align-top tieudelist" VerticalAlign="Top"></ItemStyle>
                                <HeaderStyle Width="350px" CssClass="valign-middle" />
                                <ItemStyle CssClass="valign-middle" Width="350px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <a style="float: left;" href="javascript:void(0)" onclick="DeleteItem(<%# Eval("VoteIt_ID") %>)">
                                        <img src="/Images/Icons/delete.gif" /></a> 
                                    <a style="float: left;" onclick="SaveItem(<%# Eval("VoteIt_ID") %>,<% = voteID %>)"
                                            href="javascript:void(0)">
                                            <img src="/Images/save.gif" border="0" /></a>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" CssClass="valign-middle" />
                                <ItemStyle CssClass="valign-middle" Width="50px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <a href="javascript:void(0)" onclick="AddNewsVoteItem(<% = voteID %>)">Thêm lựa chọn</a>
                </td>
                <td>
                </td>
            </tr>
            
            <tr>
                <td colspan="2" align="center">
                    <asp:Button runat="server" CssClass="button white" Text="Lưu" ID="btnSave" />
                    &nbsp; &nbsp;
                    <asp:Button runat="server" CssClass="button white" Text="Xóa" ID="btnDelete" />
                    &nbsp;
                    <input class="button white" type="button" onclick="$.facebox.close()" value="Đóng" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
