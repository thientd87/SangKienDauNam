<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cltPopupMedia.ascx.cs" Inherits="DFISYS.GUI.Editoral.Gallery.UserControl.cltPopupMedia" %>
<table cellpadding="0" cellspacing="0" border="0" >
    <tr>
        <td height='25px' width="6" background="/images/icons/left_top.gif"></td>
        <td  height='25px' background="/images/icons/bg_top.gif" align=right valign="middle">
            <img src="/images/icons/close.gif" border="0" style="cursor:hand;" onclick="CloseFormMedia();"/>
            
        </td>
        <td  height='25px' width="6" background="/images/icons/right_top.gif">
        </td>
    </tr>
    <tr>
      <td background="/images/icons/bg_doc.gif" width="6"></td>
       <td>
         <table cellpadding="0" cellspacing="0" border="0">
          <tr>
           <td align="center" valign="top" width="100%">
                <OBJECT id='mediaPlayer' width="320" height="285" 
	              classid='CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95' 
	              codebase='http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701'
	              standby='Loading Microsoft Windows Media Player components...' type='application/x-oleobject'>
	              <param name='fileName' value="">
	              <param name='animationatStart' value='true'>
	              <param name='transparentatStart' value='true'>
	              <param name='autoStart' value="true">
	              <param name='showControls' value="true">
	              <param name='loop' value="true">
	             </OBJECT>
           </td>   
          </tr>
        </table>     
       </td>
      <td background="/images/icons/bg_doc.gif" width="6"></td>
  </tr>
    <tr>
        <td><img src="/images/icons/left_bottom.gif" border="0" /></td>
        <td height="6px" background="/images/icons/bg_bottom.gif">
        </td>
        <td><img src="/images/icons/right_bottom.gif" border="0" /></td>
    </tr>
</table>
