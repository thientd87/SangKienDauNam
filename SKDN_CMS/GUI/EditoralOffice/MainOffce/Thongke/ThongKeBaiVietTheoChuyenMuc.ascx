<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThongKeBaiVietTheoChuyenMuc.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.Thongke.ThongKeBaiVietTheoChuyenMuc" %>
<script language="javascript" src="/Scripts/cms.js" type="text/javascript"></script>
<script language="javascript" src="/Scripts/submodal_admin.js" type="text/javascript"></script>
<script language="javascript" src="/Scripts/submodalsource.js" type="text/javascript"></script>
<script language="javascript" src="/Scripts/fg.menu.js" type="text/javascript"></script>
<%--<link href="/styles/truongStyle.css" rel="stylesheet" type="text/css" />
<link href="/styles/submodal.css" rel="stylesheet" type="text/css" />
<link href="/styles/submodal_admin.css" rel="stylesheet" type="text/css" />--%>
<style>.xemtintheongay a, .xemtintheongay img, .xemtintheongay span, .xemtintheongay input{ float:none}</style>
<style type="text/css">
    .Level1
    {
        margin: 5px 0 0 0;    
    }
    .Level2
    {
        margin: 0 0 0 30px;
    }
    .Level3
    {
        margin: 0 0 0 60px;
    }
</style>

<script type="text/javascript">
    $(document).ready(function () {
        $(".thongKeBai").click(function () {
            openLoading();
            var divIframe = "<iframe style=\"border:0;overflow-x: hidden;\" src=\"/RequestLink/thongkebaitheochuyenmuc.aspx?fromDate=" + $(this).attr('dateFrom')
                            + "&toDate=" + $(this).attr('dateTo') + "&cateID=" + $(this).attr('cateID') + "&sortOrder=" + $(this).attr('sortOrder') + "\" width=\"100%\" height=\"99%\"></iframe>";

            $('#cmsFramePopUp').show();
            $('#cmsFramePopUpContent').html(divIframe);
            openCmsFramePopUp(800, 500, false);
        });

        $(".thongKeBieuDo").click(function () {
            openLoading();
            var divIframe = "<iframe style=\"border:0;overflow-x: hidden;\" src=\"/RequestLink/thongkebieudotheochuyenmuc.aspx?fromDate=" + $(this).attr('dateFrom')
                            + "&toDate=" + $(this).attr('dateTo') + "&cateID=" + $(this).attr('cateID') + "&sortOrder=" + $(this).attr('sortOrder') + "\" width=\"100%\" height=\"99%\"></iframe>";

            $('#cmsFramePopUp').show();
            $('#cmsFramePopUpContent').html(divIframe);
            openCmsFramePopUp(800, 500, false);
        });
    });

</script>

<h1 style="text-align: center">Thống kê bài viết theo chuyên mục</h1>
<fieldset class="xemtintheongay" style="padding:10px;float:none;width: 50%">
	<legend style="padding-left:30px;">Chọn điều kiện thống kê</legend>
    <span>Từ: </span><asp:TextBox MaxLength="10" ID="txtfromDate" Width="75px" runat="server" CssClass="calendar" />
	<a onclick="if(self.gfPop)gfPop.fPopCalendar(document.getElementById('<% = txtfromDate.ClientID %>'));return false;"
		href="javascript:void(0)"><img class="PopcalTrigger" height="21" src="/Scripts/DatePicker/datepicker.gif" width="34" align="absMiddle" border="0"></a>
    <span>Đến: </span><asp:TextBox MaxLength="10" ID="txttoDate" Width="75px" runat="server" CssClass="calendar" />&nbsp;&nbsp;
    <a onclick="if(self.gfPop)gfPop.fPopCalendar(document.getElementById('<% = txttoDate.ClientID %>'));return false;"
		href="javascript:void(0)">
		<img class="PopcalTrigger" height="21" src="/Scripts/DatePicker/datepicker.gif" width="34"
			align="absMiddle" border="0">
	</a>
    <div style="padding:5px 0px 0px 0px">
    &nbsp;&nbsp; Chọn Chuyên mục: <asp:DropDownList ID="ddlChuyenmuc" runat="server" AutoPostBack="false"></asp:DropDownList>
	&nbsp;&nbsp; &nbsp;&nbsp;<asp:Button ID="Button1" CssClass="btnUpdate" runat="server" Text="Xem thống kê" onclick="btnXem_Click" />
    </div>
</fieldset>

<asp:GridView runat="server" ID="rptListnew" AutoGenerateColumns="false" 
    Width="50%"  HorizontalAlign="Center" CellPadding="5" CssClass="gtable sortable"
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
        <asp:TemplateField HeaderText="Chuyên mục" SortExpression="Cat_Name">
        <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="20%" HorizontalAlign="Left"/>
            <ItemTemplate >
                <span class="Level<%#Eval("SortOrder")%>"><a class="thongKeBieuDo" id="linkThongKeBieuDo" clientidmode="Static" runat="server" style="cursor:pointer;"><%#Eval("Cate_Name")%></a></span>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Số lượng bài xuất bản" SortExpression="SoLuongBaiXB">
         <HeaderStyle HorizontalAlign="Center" Font-Bold="true" BackColor="#dddddd" />
            <ItemStyle Width="20%" HorizontalAlign="Center"/>
            <ItemTemplate >
                <a class="thongKeBai" id="linkThongKeBaiXB" clientidmode="Static" runat="server" style="cursor:pointer;"></a>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

<div id="overlayDiv" style="position:absolute; left:0; right:0; top:0; bottom:0; background-color:Black; z-index:100;display:none; opacity:0.5; filter:alpha(opacity=50)"></div>
<div id="cmsFramePopUp"  style="top: -15px; left: 359.5px; display:none; z-index:200">
        <img src="/Images/close.png" style="position: relative; float: right; right: -12px; top: 16px;" alt="" onclick="closeCmsFramePopUp();"/>
        <div id="cmsFramePopUpContent" style="overflow: hidden; width: 800px; height: 384px;"></div>
</div>

 <iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
	src="/Scripts/DatePicker/ipopeng.htm" scrolling="no" frameborder="0" style="visibility: visible;
	z-index: 999; position: absolute; top: -500px; left: -500px;"></iframe>
<script type="text/javascript" src="/scripts/calendar.js"></script>