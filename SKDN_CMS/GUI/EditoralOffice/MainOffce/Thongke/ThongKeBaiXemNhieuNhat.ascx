<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThongKeBaiXemNhieuNhat.ascx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.Thongke.ThongKeBaiXemNhieuNhat" %>
<style>.xemtintheongay a, .xemtintheongay img, .xemtintheongay span, .xemtintheongay input{ float:none}</style>
<h1 style="text-align: center">
    Thống kê bài viết theo tác giả</h1>
<fieldset class="xemtintheongay" style="padding:10px;float:none;width: 950px">
	<legend style="padding-left: 30px;">Chọn điều kiện thống kê</legend>
    <span>Từ: </span><asp:TextBox MaxLength="10" ID="txtfromDate" Width="75px" runat="server" CssClass="calendar" />
	<a onclick="if(self.gfPop)gfPop.fPopCalendar(document.getElementById('<% = txtfromDate.ClientID %>'));return false;"
		href="javascript:void(0)"><img class="PopcalTrigger" height="21" src="/Scripts/DatePicker/datepicker.gif" width="34" align="absMiddle" border="0"></a>
    <span>Đến: </span><asp:TextBox MaxLength="10" ID="txttoDate" Width="75px" runat="server" CssClass="calendar" />&nbsp;&nbsp;
    <a onclick="if(self.gfPop)gfPop.fPopCalendar(document.getElementById('<% = txttoDate.ClientID %>'));return false;"
		href="javascript:void(0)">
		<img class="PopcalTrigger" height="21" src="/Scripts/DatePicker/datepicker.gif" width="34"
			align="absMiddle" border="0">
	</a>
    &nbsp;&nbsp; Chọn Chuyên mục: <asp:DropDownList ID="ddlChuyenmuc" runat="server" AutoPostBack="false"></asp:DropDownList>
	&nbsp;&nbsp; 
    &nbsp;&nbsp; 
    &nbsp;&nbsp; 
    Số lượng bài  &nbsp;&nbsp;<asp:DropDownList ID="dllPageCount" runat="server" AutoPostBack="false">
                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                <asp:ListItem Text="50" Value="50" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="100" Value="100"></asp:ListItem>
                            </asp:DropDownList>
    &nbsp;&nbsp; &nbsp;&nbsp;<asp:Button ID="Button1" CssClass="btnUpdate" runat="server" Text="Xem thống kê" onclick="btnXem_Click" />
</fieldset>

<asp:GridView runat="server" ID="rptListnew" CssClass="gtable sortable" AutoGenerateColumns="false" Width="100%" CellPadding="5" EmptyDataText="Chưa có dữ liệu để thống kê">
    <Columns>
        <asp:TemplateField HeaderText="Tiêu đề tin">
            <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="40%" />
            <ItemTemplate >
                <%#Eval("News_Title") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Chuyên mục">
        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="20%" />
            <ItemTemplate >
                <%#Eval("Cat_Name") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Ngày xuất bản">
        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="20%" />
            <ItemTemplate >
                <%#Eval("PublishDate")%>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="PageView">
         <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="20%" />
            <ItemTemplate >
                <%#Eval("ViewCount")%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>





    <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
	src="/Scripts/DatePicker/ipopeng.htm" scrolling="no" frameborder="0" style="visibility: visible;
	z-index: 999; position: absolute; top: -500px; left: -500px;"></iframe>
<script type="text/javascript" src="/scripts/calendar.js"></script>