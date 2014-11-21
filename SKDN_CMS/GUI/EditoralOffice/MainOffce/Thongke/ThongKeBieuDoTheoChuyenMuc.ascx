<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThongKeBieuDoTheoChuyenMuc.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.Thongke.ThongKeBieuDoTheoChuyenMuc" %>
<script language="javascript" src="/Scripts/jquery-1.4.1.js" type="text/javascript"></script>
<script language="javascript" src="/Scripts/highcharts.js" type="text/javascript"></script>
<%--<link href="/styles/common.css" rel="stylesheet" type="text/css" />--%>
<%--<style>.xemtintheongay a, .xemtintheongay img, .xemtintheongay span, .xemtintheongay input{ float:none}</style>--%>
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

<h1 style="text-align: center">Thống kê biểu đồ chuyên mục </h1>
<fieldset class="xemtintheongay" style="padding:10px;float:none;width:50%">
	<legend style="padding-left: 30px">Chọn điều kiện thống kê</legend>
    
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
            &nbsp;&nbsp;<asp:Button ID="Button1" CssClass="btnUpdate" runat="server" Text="Xem thống kê" onclick="btnXem_Click" />
</fieldset>

<asp:GridView runat="server" ID="rptListnew" AutoGenerateColumns="false" CssClass="gtable sortable" 
    Width="50%"  HorizontalAlign="Center" CellPadding="5" 
    EmptyDataText="Chưa có dữ liệu để thống kê" 
    onrowdatabound="rptListnew_RowDataBound" AllowSorting="true" OnSorting="rptListnew_Sorting">
    <Columns>
        <asp:TemplateField HeaderText="STT">
            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="10%" HorizontalAlign="Center"/>
            <ItemTemplate >
                <%--<%#Eval("Row")%>--%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Chuyên mục" >
        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="20%" HorizontalAlign="Center"/>
            <ItemTemplate >
                <%#Eval("Cate_Name")%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Số lượng bài xuất bản">
         <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="20%" HorizontalAlign="Center"/>
            <ItemTemplate >
                <%#Eval("SoLuongBaiTao")%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

<div id="container" style="min-width: 400px; height: 400px; margin: 0 auto"></div>


