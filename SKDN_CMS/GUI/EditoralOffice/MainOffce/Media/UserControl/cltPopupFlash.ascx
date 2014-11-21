<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cltPopupFlash.ascx.cs" Inherits="Portal.GUI.Editoral.Gallery.UserControl.cltPopupFlash" %>
<table cellpadding="0" cellspacing="0" border="0" >
    <tr>
        <td height='25px' width="6" background="/images/icons/left_top.gif"></td>
        <td  height='25px' background="/images/icons/bg_top.gif" align=right valign="middle">
            <img src="/images/icons/close.gif" border="0" style="cursor:hand;" onclick="CloseFormFlash();"/>
            
        </td>
        <td  height='25px' width="6" background="/images/icons/right_top.gif">
        </td>
    </tr>
    <tr>
      <td background="/images/icons/bg_doc.gif" width="6"></td>
       <td>
         <table cellpadding="0" cellspacing="0" border="0">
          <tr>
           <td align="center" valign="top" width="100%" id='flashcontent'>
              
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

