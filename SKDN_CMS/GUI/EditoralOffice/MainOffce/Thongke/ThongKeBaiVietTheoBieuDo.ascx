<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThongKeBaiVietTheoBieuDo.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.Thongke.ThongKeBaiVietTheoBieuDo" %>
<script language="javascript" src="/Scripts/jquery-1.4.1.js" type="text/javascript"></script>
<script language="javascript" src="/Scripts/highcharts.js" type="text/javascript"></script>
<%--<link href="/styles/common.css" rel="stylesheet" type="text/css" />
<style>.xemtintheongay a, .xemtintheongay img, .xemtintheongay span, .xemtintheongay input{ float:none}</style>--%>
<script type="text/javascript">
    

    $(document).ready(function () {

        $('#Label1').hide();
        $('#dllNumberDay').hide();    

        $('#checkDateStat').click(function () {
            if ($(this).attr('checked') == true) {
                $('#Label1').show();
                $('#dllNumberDay').show();
            }
            else if ($(this).attr('checked') == false) {
                $('#Label1').hide();
                $('#dllNumberDay').hide();
            }
        });

    });
</script>
<h1 style="text-align: center">
    Thống kê bài viết theo tác giả</h1>
<div hidden="true" style="text-align: center"><asp:Literal ID="Literal1" runat="server"></asp:Literal></div>

<fieldset class="xemtintheongay" style="padding:10px;float:none">
	<legend style="padding-left: 30px;">Chọn điều kiện thống kê</legend>
    
    &nbsp;&nbsp; Chọn Chuyên mục: <asp:DropDownList ID="ddlChuyenmuc" runat="server" AutoPostBack="false"></asp:DropDownList>
	&nbsp;&nbsp; 
    Số lượng bài  &nbsp;&nbsp;<asp:DropDownList ID="dllPageCount" runat="server" AutoPostBack="true">
                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                <asp:ListItem Text="50" Value="50" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="100" Value="100"></asp:ListItem>
                            </asp:DropDownList>
    &nbsp;&nbsp; Xem theo ngày &nbsp;&nbsp; <asp:CheckBox ID="checkDateStat" runat="server" ClientIDMode="Static"/>
    
     &nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Số ngày" ClientIDMode="Static"></asp:Label> &nbsp;&nbsp;<asp:DropDownList ID="dllNumberDay" ClientIDMode='Static' runat="server" Width="45px">
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="7" Value="7" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                <asp:ListItem Text="-----Chọn tất cả -----" Value="0"></asp:ListItem>
                            </asp:DropDownList>
            &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="Xem thống kê"  CssClass="btnUpdate" onclick="btnXem_Click" />
</fieldset>

<asp:GridView runat="server" ID="rptListnew" AutoGenerateColumns="false" CssClass="gtable sortable"
    Width="50%"  HorizontalAlign="Center" CellPadding="5" 
    EmptyDataText="Chưa có dữ liệu để thống kê" 
    onrowdatabound="rptListnew_RowDataBound" AllowSorting="true" OnSorting="rptListnew_Sorting" Visible="false">
    <Columns>
        <asp:TemplateField HeaderText="STT">
            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="10%" HorizontalAlign="Center"/>
            <ItemTemplate >
                <%--<%#Eval("Row")%>--%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Số lượng bài tạo" SortExpression="SoLuongBaiTao">
        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="20%" HorizontalAlign="Center"/>
            <ItemTemplate >
                <%#Eval("SoLuongBaiTao")%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Số lượng bài xuất bản" SortExpression="SoLuongBaiXB">
         <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="20%" HorizontalAlign="Center"/>
            <ItemTemplate >
                <%#Eval("SoLuongBaiXB")%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

<asp:GridView runat="server" ID="GridView1" AutoGenerateColumns="false" CssClass="gtable sortable"
    Width="70%"  HorizontalAlign="Center" CellPadding="5" 
    EmptyDataText="Chưa có dữ liệu để thống kê" 
    onrowdatabound="GridView1_RowDataBound" AllowSorting="true" OnSorting="GridView1_Sorting" Visible="false">
    <Columns>
        <asp:TemplateField HeaderText="STT">
            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="10%" HorizontalAlign="Center"/>
            <ItemTemplate >
                <%--<%#Eval("Row")%>--%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Ngày thống kê" SortExpression="DateXuatban">
            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd"/>
            <ItemStyle Width="20%" HorizontalAlign="Center" />
            <ItemTemplate >
                <asp:Literal ID="Literal3" runat="server"></asp:Literal>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Số lượng bài tạo" SortExpression="SoLuongBaiTao">
        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="20%" HorizontalAlign="Center"/>
            <ItemTemplate >
                <%#Eval("SoLuongBaiTao")%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Số lượng bài xuất bản" SortExpression="SoLuongBaiXB">
         <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="20%" HorizontalAlign="Center"/>
            <ItemTemplate >
                <%#Eval("SoLuongBaiXB")%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

<div id="container" style="min-width: 200px; height: 200px; margin: 0 auto; margin-top:100px;"></div>


