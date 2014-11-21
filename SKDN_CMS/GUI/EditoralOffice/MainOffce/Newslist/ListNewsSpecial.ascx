<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListNewsSpecial.ascx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.Newslist.ListNewsSpecial" %>
<%@ Register Src="contextMenu.ascx" TagName="contextMenu" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
	Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="feedback.ascx" TagName="feedback" TagPrefix="uc1" %>
<asp:HiddenField ID="hdArgs" runat="server" />
<asp:HiddenField ID="hdNewsID" runat="server" />
<script type="text/javascript">
    var btnSetLoaiTin = null, hdArgs = null, grdListNewsID = null, hdNewsID = null, btnUpdateStatus = null, btnDeletePermanently = null, btnSearch = '<%=btnSearch.ClientID %>', commandName = null;
    var tr = null;
    var published = '<%=isXuatBan%>';
    hdArgs = document.getElementById('<%=hdArgs.ClientID %>');
    hdNewsID = document.getElementById('<%=hdNewsID.ClientID %>');
    function newslist_init() {
        btnSetLoaiTin = document.getElementById('<%=btnSetLoaiTin.ClientID %>');
        btnSetTieuDiem = document.getElementById('<%=btnSetTieuDiem.ClientID %>');
        btnUpdateStatus = document.getElementById('<%=btnUpdateStatus.ClientID %>');
        btnDeletePermanently = document.getElementById('<%=btnDeletePermanently.ClientID %>');
        grdListNewsID = '<%=grdListNews.ClientID %>';
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(newslist_removeActiveRow);
    }
    var oc = [];
</script>

<div class="Edit_Head_Cell">
	<span style="float: left;" id="tab_ctl16_ctl02_Label1">
		<asp:Literal ID="ltrLabel" runat="server"></asp:Literal></span>
	<select style="float: right;" onchange="redirectpage(this)" id="Select1">
		<option>Quản lý bài nổi bật</option>
		<option selected="selected">Danh sách bài xuất bản</option>
	</select>
	<script type="text/javascript">
	    document.getElementById('Select1').style.display = location.href.indexOf('publishedlist.aspx') >= 1 ? '' : 'none';
	    function redirectpage(cbo) {
	        if (cbo.selectedIndex == 0)
	            location.href = '/office/bainoibat.aspx';
	        else
	            location.href = '/office/publishedlist.aspx'; cbo.blur();
	    }
    </script>

	<br style="clear: both;" />
</div>

<div style="float: left; padding-top: 20px;">
	Chọn chuyên mục
	<asp:DropDownList ID="ddlChuyenmuc" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChuyenmuc_SelectedIndexChanged">
	</asp:DropDownList>
</div>
<fieldset class="xemtintheongay">
	<legend>Xem tin theo ngày</legend><span>Từ</span>
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
	<asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" OnClientClick="return Filter();"
		Width="22px" Height="21px" ImageUrl="/images/Icons/search.gif" />
</fieldset>
<br style="clear: both;" />
<table id="tblSearch" runat="server" cellpadding="2" cellspacing="2">
	<tr valign="bottom">
		<td width="79">
			Từ khóa</td>
		<td>
			<asp:TextBox AutoCompleteType="None" ID="txtKeyword" Width="350" runat="server"></asp:TextBox>
			<asp:Button ID="btnSearch" OnClientClick="endRequest = 'window.scrollTo(0,0)'" runat="server"
				Text="Tìm kiếm" OnClick="btnSearch_Click" />
		</td>
	</tr>
</table>
<br />
<div style="float:left; width:100%;">
<asp:UpdatePanel ID="panel" UpdateMode="conditional" runat="server">
	<ContentTemplate>
		<asp:GridView Width="100%" ID="grdListNews" runat="server" CssClass="grd" EmptyDataText="<span style='color:Red'><b>Không có bài !</b></span>"
			AutoGenerateColumns="False" AllowPaging="True" AllowSorting="true" DataSourceID="objListNewsSource"
			PageSize="40" OnSorted="grdListNews_Sorted" OnRowDataBound="grdListNews_RowDataBound">
			<Columns>
				<asp:TemplateField Visible="false">
					<HeaderTemplate>
						<input type="checkbox" id="chkAll" onclick="tonggle(grdListNewsID, this.checked, 'chkSelect')" />
					</HeaderTemplate>
					<ItemTemplate>
						<input type="checkbox" value='<%#Eval("News_Id")%>' name="chkSelect" onclick="selectRow(this)"
							runat="server" id="chkSelect" />
					</ItemTemplate>
					<HeaderStyle Width="20px" />
					<ItemStyle Width="20px" />
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Tiêu đề bài viết" ItemStyle-CssClass="text" SortExpression="News_Title">
					<ItemTemplate>
						<div class="contexcolumn" onmouseover="this.className = 'contexcolumn_hover'" onmouseout="this.className = 'contexcolumn'" style="position:relative">
							<p>
								<a href="javascript:void(0);" onclick="editnews('<%# Eval("News_ID") %>'); return false;">
									<%#HttpUtility.HtmlEncode(Convert.ToString(Eval("News_Title")))%>
								</a> <a runat="server" rel="facebox[.content2]"  id="aIconUpdate"  visible="false" style="float:right"><img src="/images/icon_update.gif" /></a>
							</p>
							<div>								
								<asp:Literal ID="ltrInfo" runat="server"></asp:Literal>, <b>
									<%# (int)Eval("WordCount") == 0 ? "0" : ((int)Eval("WordCount")).ToString("#,###")%>
								</b>từ
                                <br />
                                Ngày sửa cuối: <b><%#Convert.ToDateTime(Eval("News_ModifiedDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss")%></b>
								<img src="/images/blank.gif" height="1" width="100%" />
                            </div>
                           
						</div>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Chuyên mục">
					<ItemStyle HorizontalAlign="center" />
					<HeaderStyle Width="130" />
					<ItemTemplate>
						<%#Eval("Cat_Name") %>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Lượt xem" SortExpression="ViewCount">
					<ItemStyle HorizontalAlign="center" />
					<ItemTemplate>
						<a rel="tooltip" news_id="<%# Eval("News_ID") %>" news_title="<%#HttpUtility.HtmlEncode(Convert.ToString(Eval("News_Title")))%>" ><%# (long)Eval("ViewCount") == 0 ? "0" : ((long)Eval("ViewCount")).ToString("#,###")%></a>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Người nhận" Visible="false" >
					<ItemStyle HorizontalAlign="center" />
					<ItemTemplate>
						<a rel="tooltip"><%# Eval("Reciver_ID")%></a>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>Tin tiêu điểm</nobr>" SortExpression="News_isFocus"
					ItemStyle-HorizontalAlign="center" Visible="false">
					<ItemTemplate>
						<asp:CheckBox ID="chkIsFocus" runat="server" onclick="newslist_chkIsFocus_CheckedChanged(this);"
							Checked='<%# Eval("News_isFocus") %>' />
					</ItemTemplate>
					<HeaderStyle Width="60" />
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Loại tin" SortExpression="News_mode" Visible="false">
					<ItemTemplate>
						<asp:DropDownList ID="cboIsHot" runat="server" onchange="newslist_cboIsHot_selectedIndexChange(this)"
							SelectedValue='<%# Eval("News_Mode").ToString() %>' CausesValidation="False">
							<asp:ListItem Value="6" Text="Không ra trang chủ"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Thông Thường" Selected="True"></asp:ListItem>
                          <%--  <asp:ListItem Value="5" Text="Tin tiêu điểm"></asp:ListItem>--%>
                            <asp:ListItem Value="3" Text="Tin Focus"></asp:ListItem>
							<asp:ListItem Value="1" Text="Nổi bật mục"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Tiêu điểm trang chủ"></asp:ListItem>
							<asp:ListItem Value="2" Text="Nổi bật trang chủ"></asp:ListItem>						
							
                            <%--<asp:ListItem Value="5" Text="Tin vắn"></asp:ListItem>--%>
						</asp:DropDownList>
					</ItemTemplate>
					<ItemStyle Width="50px" />
				</asp:TemplateField>
			</Columns>
			<PagerStyle CssClass="paging" HorizontalAlign="left" />
			<PagerSettings Mode="NumericFirstLast" />
			<HeaderStyle CssClass="grdHeader" />
			<RowStyle CssClass="grdItem" />
			<AlternatingRowStyle CssClass="grdAlterItem" />
		</asp:GridView>
	</ContentTemplate>
	<Triggers>
		<asp:AsyncPostBackTrigger ControlID="btnFilter" EventName="Click" />
		<asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
		<asp:AsyncPostBackTrigger ControlID="ddlChuyenmuc" EventName="SelectedIndexChanged" />
	</Triggers>
</asp:UpdatePanel>
</div>
<uc2:contextMenu ID="ContextMenu1" runat="server"></uc2:contextMenu>

<div style="float: right; text-align: right; padding: 5px 0px 10px 0px; display:none">
	<a href="javascript:void()" onclick="tonggle(grdListNewsID, true, 'chkSelect'); return false;">
		Chọn tất cả</a> | <a href="javascript:void()" onclick="tonggle(grdListNewsID, false, 'chkSelect'); return false;">
			Bỏ chọn</a> |
	<asp:LinkButton ID="LinkSendAll" runat="server" CssClass="normalLnk" OnClientClick="return checkMultiAction('send');"
		OnClick="LinkSendAll_Click">Gửi tin đã chọn</asp:LinkButton><asp:Literal ID="ltrsec1"
			Text=" |" runat="server"></asp:Literal>
	<asp:LinkButton ID="LinkFeedBackAll" runat="server" CssClass="normalLnk" OnClientClick="return checkMultiAction('sendback');"
		OnClick="LinkFeedBackAll_Click">Trả tin đã chọn</asp:LinkButton><asp:Literal ID="ltrsec2"
			Text=" |" runat="server"></asp:Literal>
	<asp:LinkButton ID="LinkApproval" runat="server" CssClass="normalLnk" OnClientClick="return checkMultiAction('approved');"
		OnClick="LinkApproval_Click">Xuất bản tin đã chọn</asp:LinkButton><asp:Literal ID="ltrsec3"
			Text=" |" runat="server"></asp:Literal>
	<asp:LinkButton ID="LinkDisApproval" runat="server" CssClass="normalLnk" OnClientClick="return checkMultiAction('disapproved');"
		OnClick="LinkDisApproval_Click">Gỡ tin đã chọn</asp:LinkButton><asp:Literal ID="ltrsec4"
			Text=" |" runat="server"></asp:Literal>
	<asp:LinkButton ID="LinkDelete" OnClientClick="return checkMultiAction('delete');"
		runat="server" CssClass="normalLnk" OnClick="LinkDelete_Click">Xóa tin đã chọn</asp:LinkButton><asp:Literal
			ID="ltrsec5" Text=" |" runat="server"></asp:Literal>
	<asp:LinkButton ID="lnkRealDel" OnClientClick="return checkMultiAction('delete');"
		runat="server" CssClass="normalLnk" OnClick="lnkRealDel_Click">Xóa tin đã chọn</asp:LinkButton></div>
	          
<br style="clear: both;" />

<div id='divShowComment' class="popupborder">
	<table cellpadding="0" cellspacing="0" border="0" width="500">
		<tr>
			<td colspan="2" width="100%">
				<table cellpadding="0" cellspacing="0" border="0" width='100%' id='divheader'>
					<tr>
						<td width='4px'>
							<img src="/images/skins/lefttop.gif" border="0" /></td>
						<td background="/images/skins/toppop.gif" align="center">
							<b>Nội dung comment</b>
						</td>
						<td background="/images/skins/toppop.gif" align="right" width='4px'>
							<a href="javascript:CloseComment();">
								<img src="/images/skins/close.gif" border="0" /></a>
						</td>
						<td width='4px'>
							<img src="/images/skins/righttop.gif" border="0" /></td>
					</tr>
				</table>
			</td>
		</tr>
		<tr bgcolor="#DBDBDB">
			<td height="10" colspan="2">
			</td>
		</tr>
		<tr>
			<td width='90px' valign="top" align="left">
				Tiêu đề :
			</td>
			<td align="center">
				<input name="txtTitle" type="text" id="txtTitle" class="ms-long" runat="server" />
			</td>
		</tr>
		<tr>
			<td width='90px' valign="top" nowrap align="left">
				Nội Dung :
			</td>
			<td align="center">
				<textarea name="txtContent" id="txtContent" class="ms-long" rows="10" runat="server"></textarea>
			</td>
		</tr>
		<tr bgcolor="#DBDBDB">
			<td height="20" colspan="2" align="center">
				<input type="button" onclick="CloseComment();" value="Close" /></td>
		</tr>
	</table>
</div>

<!-- Table ToolTip (Su dung cho thong ke theo gio cua bai viet )-->
 <table width="330" cellspacing="0" cellpadding="0" border="0" id="table_StatisticContentHour"  style="display:none;position:absolute;z-index:1000" onmouseout="closeToolTip();">
    <tbody>
        <tr>
            <td valign="top" align="left" class="toppre">
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" align="left" class="cafefbg">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td valign="top" align="left" class="cafecont" id="tooltipContent"></td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" class="cafefoot">
               &nbsp;&nbsp;
            </td>
        </tr>
    </tbody>
</table>
<!-- END Table ToolTip (Su dung cho thong ke theo gio cua bai viet ) -->

<asp:UpdatePanel ID="upCommand" UpdateMode="conditional" runat="server">
	<Triggers>
		<asp:AsyncPostBackTrigger ControlID="btnSetTieuDiem" EventName="Click" />
		<asp:AsyncPostBackTrigger ControlID="btnSetLoaiTin" EventName="Click" />
		<asp:AsyncPostBackTrigger ControlID="btnUpdateStatus" EventName="Click" />
		<asp:AsyncPostBackTrigger ControlID="btnDeletePermanently" EventName="Click" />
		<asp:AsyncPostBackTrigger ControlID="LinkSendAll" EventName="Click" />
		<asp:AsyncPostBackTrigger ControlID="LinkFeedBackAll" EventName="Click" />
		<asp:AsyncPostBackTrigger ControlID="LinkApproval" EventName="Click" />
		<asp:AsyncPostBackTrigger ControlID="LinkDisApproval" EventName="Click" />
		<asp:AsyncPostBackTrigger ControlID="LinkDelete" EventName="Click" />
		<asp:AsyncPostBackTrigger ControlID="lnkRealDel" EventName="Click" />
	</Triggers>
</asp:UpdatePanel>
<div id="divCommand" style="display: none;">
	<asp:Button ID="btnSetTieuDiem" runat="server" Text="btnSetTieuDiem" OnClick="btnSetTieuDiem_Click" /><asp:Button
		ID="btnSetLoaiTin" runat="server" Text="btnSetLoaiTin" OnClick="btnSetLoaiTin_Click" />
	<asp:Button ID="btnUpdateStatus" runat="server" Text="btnUpdateStatus" OnClick="btnUpdateStatus_Click" />
	<asp:Button ID="btnDeletePermanently" runat="server" Text="btnDeletePermanently"
		OnClick="btnDeletePermanently_Click" />
</div>
<iframe width="174" height="189" name="gToday:normal:agenda.js" id="gToday:normal:agenda.js"
	src="/Scripts/DatePicker/ipopeng.htm" scrolling="no" frameborder="0" style="visibility: visible;
	z-index: 999; position: absolute; top: -500px; left: -500px;"></iframe>
<uc1:feedback ID="Feedback1" runat="server"></uc1:feedback>
<asp:ObjectDataSource ID="objListNewsSource" runat="server" SelectMethod="GetNewslistOfNewsSpecialListControl" SelectCountMethod="GetRowsCountOfNewsSpecialListControl"
	TypeName="Portal.BO.Editoral.Newslist.NewslistHelper" SortParameterName="SortExpression"
	EnablePaging="true" MaximumRowsParameterName="PageSize" StartRowIndexParameterName="StartRow">
	<SelectParameters>
		<asp:Parameter Name="strWhere" DefaultValue="" Type="String" />		
		
	</SelectParameters>
	
</asp:ObjectDataSource>
<script type="text/javascript" src="/scripts/calendar.js"></script>
<script language="JavaScript" type="text/javascript" src="/scripts/newslist2.js"></script>
<script language="javascript">
    function GetControlByName(id) {
        return document.getElementById("<% = ClientID %>_" + id);
    }
    function Filter() {
        var txtFromDate = GetControlByName("txtFromDate");
        var txtToDate = GetControlByName("txtToDate");
        if (txtFromDate.value == "" || txtToDate.value == "") {
            alert("Bạn cần phải chọn cả ngày bắt đầu và ngày kết thúc!");
            return false;
        }
        return true;

    }
</script>
<script type="text/javascript">
    $(document).ready(function ($) {
        $('a[rel*=colorbox]').facebox({ iframe: true, width: 900, height: 500 });
        $('a[rel*=facebox]').facebox({ iframe: 'true', width: '900px', height: '600px' });
    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequest);
    function EndRequest(sender, args) {

        $('#bgFilter').hide();
        $('#imgloading').hide();
        $('a[rel*=colorbox]').facebox({ iframe: true, width: 900, height: 500 });
        $('a[rel*=facebox]').facebox({ iframe: true, width: 900, height: 500 });
    }

    function GoToEditPublisheNews(newsID) {
        window.location.href = "/office/editpublist,publishedlist/" + newsID + ".aspx?source=/office/publishedlist.aspx";
    }
</script>
