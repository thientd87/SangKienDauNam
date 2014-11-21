<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageView.ascx.cs" Inherits="Nextcom.Analytics.PageView" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<link rel="stylesheet" href="/CmsControl/styles/common.css" type="text/css" />
<%@ Register src="FormSearch.ascx" tagname="FormSearch" tagprefix="uc1" %>

<div class="Edit_Head_Cell">Lượt truy cập</div>
<div id="divChart" align="center">
    <iframe src="/AnalyticsWeb/page/modules/log/PageViewChart.aspx?vt=<%= ViewType %>&w=<%= WebSite_ID %>&f=<%= From.ToString("M/d/yyyy") %>&t=<%= To.ToString("M/d/yyyy") %>" marginheight="0" marginwidth="0" width="1000" height="355" style="margin:0px" frameborder="0"></iframe>
</div>
<uc1:FormSearch runat="server" ID="FormSearch1" />
<table class="records">
	<asp:Repeater ID="rptPageviews" runat="server">
		<HeaderTemplate>
			<tr>
			    <th>STT</th>
				<th>Ngày</th>
				<th colspan="2">Lượt xem</th>
				<th>Khách/Lượt xem</th>
			</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr class="highlight">
				<td align="right" style="width: 20px;text-align:center">
					<%# Container.ItemIndex+1  %>
					</td>
				<td style="width:100px;text-align:center">
					<%# ((DateTime)Eval("Date")).ToString("dd/MM/yyyy") %>
				</td>
				<td align="right" style="width:45px;text-align:center">
					<%# ((long)Eval("PageView")).ToString("#,###")%>
				</td>
				<td align="left">
					<img style="cursor:pointer" alt="" title="<%# Eval("PageView") %> lượt xem/ <%# Eval("Visit") %> khách" src="/images/modules/log/green.gif" width="<%# (720*(long)Eval("PageView")/MaxPageView).ToString() %>" />
				</td>
				<td style="width:100px;text-align:center">
					<%# (1.0 * (long)Eval("PageView") / (long)Eval("Visit")).ToString("#0.00")%>					
				</td>
			</tr>
		</ItemTemplate><AlternatingItemTemplate><tr>
				<td align="right" style="width: 20px;text-align:center">
					<%# Container.ItemIndex+1  %>
					</td>
				<td style="width:100px;text-align:center">
					<%# ((DateTime)Eval("Date")).ToString("dd/MM/yyyy") %>
				</td>
				<td align="right" style="width:45px;text-align:center">
					<%# ((long)Eval("PageView")).ToString("#,###")%>
				</td>
				<td align="left">
					<img style="cursor:pointer" alt="" title="<%# Eval("PageView") %> lượt xem/ <%# Eval("Visit") %> khách" src="/images/modules/log/green.gif" width="<%# (720*(long)Eval("PageView")/MaxPageView).ToString() %>" />
				</td>
				<td style="width:100px;text-align:center">
					<%# (1.0 * (long)Eval("PageView") / (long)Eval("Visit")).ToString("#0.00")%>					
				</td>
			</tr></AlternatingItemTemplate>
	</asp:Repeater>

</table>
<div id="tablePaging"></div>
<asp:Literal runat="server" ID="ltrJs" />