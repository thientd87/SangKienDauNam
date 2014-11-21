<%@ Control Language="C#" AutoEventWireup="true" Codebehind="ctlPopupView2.ascx.cs"
	Inherits="DFISYS.GUI.EditoralOffice.MainOffce.Media.UserControl.ctlPopupView2" %>
<table cellpadding="0" cellspacing="0" border="0" id="ctlPopupView" class="popup"
	style="height: 470px; width: 520px;">
	<tr>
		<td height='25px' width="6" background="/images/icons/left_top.gif">
		</td>
		<td height='25px' background="/images/icons/bg_top.gif" align="right" valign="middle">
			<img src="/images/icons/close.gif" border="0" style="cursor: hand;" onclick="hideModalPopup('ctlPopupView')" />
		</td>
		<td height='25px' width="6" background="/images/icons/right_top.gif">
		</td>
	</tr>
	<tr>
		<td background="/images/icons/bg_doc.gif" width="6">
		</td>
		<td style="background-color:Black;">
			<table cellpadding="0" cellspacing="0" border="0">
				<tr>
					<td align="center" valign="top" width="100%" id="tdPreview">
					</td>
				</tr>
			</table>
			<!--<div style="height: 400px; width: 550px; overflow: auto;" id="tdPreview">
			</div>-->
		</td>
		<td background="/images/icons/bg_doc.gif" width="6">
		</td>
	</tr>
	<tr>
		<td>
			<img src="/images/icons/left_bottom.gif" border="0" /></td>
		<td height="6px" background="/images/icons/bg_bottom.gif">
		</td>
		<td>
			<img src="/images/icons/right_bottom.gif" border="0" /></td>
	</tr>
</table>
<script language="javascript">
 function click(e) {

  if (navigator.appName == 'Netscape'
           && e.which == 3) {
      
      return false;
      }
   else {
      if (navigator.appName == 'Microsoft Internet Explorer'
          && event.button==2)
         alert("");
         return false;
         }
   return true;
 }
document.getElementById("tdPreview").oncontextmenu=click
</script>