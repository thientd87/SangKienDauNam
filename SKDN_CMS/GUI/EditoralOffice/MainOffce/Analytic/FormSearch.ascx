<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormSearch.ascx.cs" Inherits="CmsControl.FormSearch" %>
<div class="subheadercontent o_h">
    <div class="f_l">
        <span id="spanSearchTitle"></span>
	</div>
	<div class="f_r p_t_4" id="divFormSearchRight"></div>	
</div>
<div class="o_h h_5">&nbsp;</div>
<div class="o_h" id="divSearch" style="display:none">
    Xem theo
    <select id="sViewType" name="sViewType">
        <option value="1">Giờ</option>
        <option value="2">Ngày</option>
        <option value="3">Tất cả</option>
        <option value="4">Tháng</option>
    </select>
    <span id="divMonth" style="display:none">
        <select id="sMonth" name="sMonth">
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
        <select id="sYear" name="sYear">
            <option value="2009">2009</option>
            <option value="2010">2010</option>
        </select>
    </span>
    <span id="divFrom">
    <span id="spanFrom">Ngày</span>
	<input value="" type="text" id="txtFrom" name="txtFrom" maxlength="10" style="width:75px" class="calendar" />
	<a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.getElementById('txtFrom'));return false;"><img class="PopcalTrigger" height="21" src="/CmsControl/images/datepicker.gif" width="34" align="top" border="0" /></a>
	</span>
	<span id="divTo" style="display:none">
    Đến
	<input value="" type="text" id="txtTo" name="txtTo" maxlength="10" style="width:75px" class="calendar" />
	<a href="javascript:void(0)" onclick="if(self.gfPop)gfPop.fPopCalendar(document.getElementById('txtTo'));return false;"><img class="PopcalTrigger" height="21" src="/CmsControl/images/datepicker.gif" width="34" align="top" border="0"></a>
	</span>
    <span id="spanscontrol"></span>
    <asp:Button Text="Xem" runat="server" ID="btnXem" OnClick="btnXem_Click" />
</div>
<div class="o_h h_5">&nbsp;</div>
<script type="text/javascript" src="/scripts/calendar.js"></script>
<iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
	src="/Scripts/DatePicker/ipopeng.htm" scrolling="no" frameborder="0" style="visibility: visible;
	z-index: 999; position: absolute; top: -500px; left: -500px;"></iframe>


