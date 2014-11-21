<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VisitDetail.ascx.cs" Inherits="Nextcom.Analytics.VisitDetail" %>
<%@ Register Src="FormSearch.ascx" TagName="FormSearch" TagPrefix="uc1" %>
<link rel="stylesheet" href="/Styles/css.css" type="text/css" />
<div class="Edit_Head_Cell">Khách truy cập</div>
<div id="divChart" align="center">
    <iframe id="chart" src="/GUI/EditoralOffice/MainOffce/Analytic/VisitChart.aspx?vt=<%= ViewType %>&f=<%= From.ToString("M/d/yyyy hh:mm:ss tt") %>&t=<%= To.ToString("M/d/yyyy hh:mm:ss tt") %>" marginheight="0" marginwidth="0" width="1000" height="355" style="margin:0px" frameborder="0"></iframe>
</div>

<div class="subheadercontent o_h">
    <div class="f_l">
        <span id="spanSearchTitle"></span>
	</div>
	<div class="f_r p_t_4" id="divFormSearchRight"></div>	
</div>
<div class="o_h h_5">&nbsp;</div>
<div class="o_h" id="divSearch">
    Xem theo
    <select id="ViewType" name="ViewType">
        <option value="1">Giờ</option>
        <option value="2">Ngày</option>
        <option value="3">Tất cả</option>
        <option value="4">Tháng</option>
    </select>
    <span id="divMonth" style="display:none">
        <select id="ddlMonth" name="ddlMonth">
            <option value="1">1</option>
            <option value="2">2</option>
            <option value="3">3</option>
            <option value="4">4</option>
            <option value="5">5</option>
            <option value="6">6</option>
            <option value="7">7</option>
            <option value="8">8</option>
            <option value="9">9</option>
            <option value="10">10</option>
            <option value="11">11</option>
            <option value="12">12</option>
        </select>
        <select id="ddlYear" name="ddlYear">
            <option value="2009">2009</option>
            <option value="2010">2010</option>
        </select>
    </span>
    <span id="divFrom">
    <span id="spanFrom">Ngày</span>
    <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
	<a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.getElementById('<%=txtFrom.ClientID %>'));return false;"><img class="PopcalTrigger" height="21" src="/Scripts/DatePicker/datepicker.gif" width="34" align="top" border="0" /></a>
	</span>
	<span id="divTo">
    Đến
	<%--<input value="" type="text" id="txtTo" name="txtTo" maxlength="10" style="width:75px" class="calendar" />--%>
    <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
	<a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.getElementById('<%=txtTo.ClientID %>'));return false;"><img class="PopcalTrigger" height="21" src="/Scripts/DatePicker/datepicker.gif" width="34" align="top" border="0"></a>
	</span>
    <span id="spanscontrol"></span>
    <asp:Button Text="Xem" runat="server" ID="btnXem" OnClick="btnXem_Click" />
</div>
<div class="o_h h_5">&nbsp;</div>
<script type="text/javascript" src="/scripts/calendar.js"></script>
<iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
	src="/Scripts/DatePicker/ipopeng.htm" scrolling="no" frameborder="0" style="visibility: visible;
	z-index: 999; position: absolute; top: -500px; left: -500px;"></iframe>


<table class="records">
	<asp:Repeater ID="rpData" OnItemDataBound="rpData_ItemDataBound" runat="server">
		<HeaderTemplate>
			<tr>
				<th align="left">
					<asp:Literal ID="ltrHeaderName" runat="server"></asp:Literal>
				</th>
				<th colspan="2" align="left">
					Khách</th>
				<th colspan="2" align="left">
					Lượt xem</th>
				<th align="right">
					Trang / Khách</th>
				<th align="right">
					Khách mới</th>
			</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr class="highlight">
				<td style="width:70px;text-align:center">
					<asp:Literal ID="ltrName" runat="server"></asp:Literal>
				</td>
				<td align="right" style="text-align:right;width:45px">
					<%# ((long)Eval("Visit")).ToString("#,###")%>
				</td>
				<td align="left" style="width:320px">
					<img src="/images/green.gif" width="<%# (310*(long)Eval("Visit")/MaxVisit).ToString() %>" height="20px"/>
				</td>
				<td align="right" style="text-align:right;width:45px">
					<%# ((long)Eval("Pageview")).ToString("#,###")%>
				</td>
				<td align="left"  style="width:320px">
					<img src="/images/green.gif" width="<%# (310*(long)Eval("Pageview")/MaxPageview).ToString() %>" height="20px"/>
				</td>
				<td align="right" style="text-align:center">
					<%# (1.0*(long)Eval("Pageview") / (long)Eval("Visit")).ToString("#0.00")%>
				</td>
				<td align="right" style="text-align:center">
					<%# ((long)Eval("NewVisitor")).ToString("#,###")%>
				</td>
			</tr>
		</ItemTemplate>
        <AlternatingItemTemplate><tr>
				<td style="text-align:center">
					<asp:Literal ID="ltrName" runat="server"></asp:Literal>
				</td>
				<td align="right" style="text-align:right;">
					<%# ((long)Eval("Visit")).ToString("#,###")%>
				</td>
				<td align="left">
					<img alt="" src="/images/green.gif" width="<%# (310*(long)Eval("Visit")/MaxVisit).ToString() %>" height="20px"/>
				</td>
				<td align="right" style="text-align:right">
					<%# ((long)Eval("Pageview")).ToString("#,###")%>
				</td>
				<td align="left">
					<img alt="" src="/images/green.gif" width="<%# (310*(long)Eval("Pageview")/MaxPageview).ToString() %>" height="20px"/>
				</td>
				<td align="right" style="text-align:center">
					<%# (1.0*(long)Eval("Pageview") / (long)Eval("Visit")).ToString("#0.00")%>
				</td>
				<td align="right" style="text-align:center">
					<%# ((long)Eval("NewVisitor")).ToString("#,###")%>
				</td>
			</tr></AlternatingItemTemplate>
	</asp:Repeater>
</table>
<div id="tablePaging"></div>
<asp:Literal runat="server" ID="ltrJs" />
<asp:HiddenField ID="hidViewType" runat="server" />

<script type="text/javascript">
    $(function () {

        $("#ViewType").change(function () {
            $("#<%=hidViewType.ClientID %>").val($(this).val());
            switch ($(this).val()) {
                case "1":
                    $("#divTo").hide();
                    $("#divFrom").show();
                    $("#spanFrom").html("Ngày");
                    $("#divMonth").hide();
                    break;
                case "2":
                    $("#divTo").show();
                    $("#divFrom").show();
                    $("#spanFrom").html("Từ");
                    $("#divMonth").hide();
                    break;
                case "3":
                    $("#divFrom").hide();
                    $("#divTo").show();
                    $("#divMonth").hide();
                    break;
                case "4":
                    $("#divFrom").hide();
                    $("#divTo").hide();
                    $("#divMonth").show();
                    break;
            }
        });

        var _viewtype = $('#<%=hidViewType.ClientID%>');
        if (_viewtype.val() != null && _viewtype.val() != "") {
            $("#ViewType").val(_viewtype.val()); $("#ViewType").change();
        }

        $('#<%=btnXem.ClientID %>').click(function () {
            $('#chart').attr('src', '/GUI/EditoralOffice/MainOffce/Analytic/VisitChart.aspx?vt=' + $("#ViewType").val() + '&f=' + $('#txtFrom.ClientID').val() + '&t=' + $('#txtTo.ClientID').val() + '');
        });

    });
</script>