<%@ Control EnableViewState="False" Language="c#" AutoEventWireup="True" Codebehind="SearchSmall.ascx.cs" Inherits="DFISYS.GUI.Share.SearchInputSmall.SearchSmall" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<link href="Styles/Search.css" type="text/css" rel="stylesheet">
<script language="javascript">
function goSearch()
{
	//alert('hehe');
	document.forms[0].action="Searching.aspx";
	document.forms[0].submit();
}
function goKeySearch()
{
	if (event.type == 'keydown' && event.keyCode == 13) 
	{
	  //alert("hehe");
      goSearch()
	}
	else
		return false;

}
</script>
<table cellSpacing="0" cellPadding="0" width="100%" align="center">
	<tr>
		<td width="100%">
			<asp:Label Runat="server" CssClass="SmallSearch_Title" id="Label1">Search:</asp:Label>
			<asp:TextBox id="txtSearch" Width="255" CssClass="txtDetail" Runat="server" EnableViewState="False"></asp:TextBox> &nbsp; <input type="button" class="SearchButton" value="Search now" runat="server" id="searchBtn">
			<!--<asp:HyperLink Runat="server" NavigateUrl="~/searching.aspx" CssClass="SearchAdvanced_Link" id="HyperLink1">Advanced search</asp:HyperLink>-->
		</td>
	</tr>
</table>
