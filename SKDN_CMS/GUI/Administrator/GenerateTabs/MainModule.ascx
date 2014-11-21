<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainModule.ascx.cs" Inherits="Portal.GUI.Administrator.GenerateTabs.MainModule" %>
<link href="/styles/backend_menu.css" rel="stylesheet" type="text/css" />
<script language="javascript" src="/scripts/Newslist.js"></script>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
	<tr>
		<td valign="top" width="232" class="Menuleft_BoxArea">
		    <table width="100%" cellpadding="0" cellspacing="0" border="0">
		        <tr>
		            <td class="Menuleft_HeadBox">Quản lý chuyên mục</td>
		            <td width="15" class="Menuleft_RHeadBox"><img src="/images/skins/minimize.gif" id="pv" width="15" border="0" align="absmiddle" onclick="showhideMenu('pv','box_pv');" style="cursor:hand"/></td>
		        </tr>
		        <tr>
		            <td valign="top" class="Menuleft_ContentBox" colspan="2" id="box_pv">
		                <asp:PlaceHolder ID="plcMenu" runat="server"></asp:PlaceHolder>
		            </td>
		        </tr>
		        <tr><td colspan="2">&nbsp;</td></tr>
		        <tr>
		            <td class="Menuleft_HeadBox">Thông tin tài khoản</td>
		            <td width="15" class="Menuleft_RHeadBox"><img src="/images/skins/maximize.gif" width="15" border="0" align="absmiddle"/></td>
		        </tr>
		        <tr>
		            <td valign="top" class="Menuleft_ContentBox" colspan="2">
		                <asp:PlaceHolder ID="plcProfile" runat="server"></asp:PlaceHolder>
		            </td>
		        </tr>
		    </table>
		</td>
		<td width="8"></td>
		<td valign="top" class="Content_OutBox">
		    <table width="100%" cellpadding="0" cellspacing="0" border="0">
		        <tr>
		            <td class="Content_BoxArea">
		                <asp:PlaceHolder ID="plcMain" runat="server"></asp:PlaceHolder>
		            </td>
		        </tr>
		    </table>
		</td>
		<td width="4"></td>
	</tr>
</table>

<script type="text/javascript">
function createCookie(name,value,days) {
	if (days) {
		var date = new Date();
		date.setTime(date.getTime()+(days*24*60*60*1000));
		var expires = "; expires="+date.toGMTString();
	}
	else var expires = "";
	document.cookie = name+"="+value+expires+"; path=/";
}

function readCookie(name) {
	var nameEQ = name + "=";
	var ca = document.cookie.split(';');
	for(var i=0;i < ca.length;i++) {
		var c = ca[i];
		while (c.charAt(0)==' ') c = c.substring(1,c.length);
		if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
	}
	return null;
}

function eraseCookie(name) {
	createCookie(name,"",-1);
}

function MouseOverTable(td)
{
    createCookie("current_"+td.id,td.className,1);
    td.className = "Menuleft_Item_Select";
}
function MouseOutTable(td)
{
    var current = readCookie("current_"+td.id);
    td.className = current;
    eraseCookie("current_"+td.id);
}
</script>