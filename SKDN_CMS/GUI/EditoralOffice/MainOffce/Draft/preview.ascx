<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="preview.ascx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.Draft.preview" %>
<table cellpadding="2" cellspacing="2" border="0">
    <tr>
        <td style="font-weight:bold;font-size:14px" colspan="2">
            <asp:Literal runat="server" ID="ltrTitle"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td valign="top" width="130px" align="left">
            <asp:Literal runat="server" ID="ltrImage"></asp:Literal>
        </td>
        <td valign="top" align="left">
            <asp:Literal runat="server" ID="ltrSapo"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td height="20px" colspan="2"></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Literal runat="server" ID="ltrContent"></asp:Literal>
        </td>
    </tr>
</table>