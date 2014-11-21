<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThongKeChiTietBaiVietTheoChuyenMuc.ascx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.Thongke.ThongKeChiTietBaiVietTheoChuyenMuc" %>
<link href="/styles/common.css" rel="stylesheet" type="text/css" />

<asp:GridView runat="server" ID="rptListnew" AutoGenerateColumns="false" CssClass="gtable sortable"
    Width="100%"  HorizontalAlign="Center" CellPadding="5" 
    EmptyDataText="Chưa có dữ liệu để thống kê" 
    onrowdatabound="rptListnew_RowDataBound" AllowSorting="true" OnSorting="rptListnew_Sorting">
    <Columns>
        <asp:TemplateField HeaderText="STT">
            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="10%" HorizontalAlign="Center"/>
            <ItemTemplate >
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Chuyên mục" SortExpression="Cate_Name">
        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="30%" HorizontalAlign="Center"/>
            <ItemTemplate >
                <%# Eval("Cate_Name")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Tiêu đề" SortExpression="News_Title">
        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="40%" HorizontalAlign="Center"/>
            <ItemTemplate >
                <%# Eval("News_Title")%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Ngày xuất bản" SortExpression="News_PublishDate">
         <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="20%" HorizontalAlign="Center"/>
            <ItemTemplate >
                <%#Convert.ToDateTime(Eval("News_PublishDate").ToString()).ToString("dd/MM/yyyy")%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>