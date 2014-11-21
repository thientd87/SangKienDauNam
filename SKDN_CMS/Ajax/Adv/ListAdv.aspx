<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListAdv.aspx.cs" Inherits="AddIns.ajax.ListAdv" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="float:left;width:100%;padding-top:10px" id="divContainer" runat="server">
            <asp:GridView ID="grdList" runat="server" AutoGenerateColumns="False" EnableModelValidation="false" EmptyDataText="Không có bản ghi nào!" AllowPaging="True" PageSize="20" CssClass="grd" Width="100%" >
                <Columns>
                    <asp:TemplateField HeaderText="Tên" HeaderStyle-Width="400px">
                        <ItemTemplate><a href="/ajax/adv-addnew.aspx?AdvId=<%#Eval("AdvID") %>&pos=<%=Request.QueryString["pos"] %>" rel="fb"><%#HttpUtility.HtmlEncode(Eval("Name").ToString()) %></a></ItemTemplate>
                        <ItemStyle Width="400px"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ngày bắt đầu">
                        <ItemTemplate><%#Eval("StartDate", "{0:dd/MM/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ngày kết thúc">
                        <ItemTemplate><%#Eval("EndDate", "{0:dd/MM/yyyy}")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lượt xem">
                        <ItemTemplate><%#Eval("ViewCount")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hoạt động">
                        <ItemTemplate><asp:CheckBox ID="CheckBox1" runat="server" Checked='<%#Eval("IsActive") %>'></asp:CheckBox></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Preview">
                        <ItemTemplate><a href="/ajax/advPreview.aspx?i=<%#Eval("AdvID") %>" rel="preview">Preview</a></ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Xóa">
                        <ItemTemplate><a href="javascript:void(0)"  onclick="DeleteAdv('<%#Eval("AdvID") %>')" rel="delete"><img src="/Images/Delete.gif" /></a></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle HorizontalAlign="Center" />
            </asp:GridView>
        </div>

        <script type="text/javascript" language="javascript">
           
        </script>
    </form>
</body>
</html>
