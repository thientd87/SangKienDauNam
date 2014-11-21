<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListCatThread.aspx.cs" Inherits="Portal.ListCatThread" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Chọn chủ đề liên quan</title>
<link href="/styles/popupform.css" rel="stylesheet" type="text/css" />
<link href="/styles/Coress.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="/Styles/Newsedit.css" />
<link href="/styles/pcal.css" rel="stylesheet" type="text/css" />
<link href="/styles/common.css" rel="stylesheet" type="text/css" />
<link href="/styles/Core.css" rel="stylesheet" type="text/css" />
<link href="/styles/autopro.css" rel="stylesheet" type="text/css" />
<link href="/styles/popupform.css" rel="stylesheet" type="text/css" />
<link href="/styles/popupform.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="/Styles/Newsedit.css" />
<link rel="stylesheet" type="text/css" href="/Styles/Portal.css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
    

<table width="100%">
    <tr>
        <td class="Edit_Head_Cell">
            <asp:Label ID="lblLabel" runat="server" Text="Danh sách"></asp:Label></td>
    </tr>    
</table>
<br />
<asp:GridView ID="gvData" runat="server" AlternatingRowStyle-CssClass="grdAlterItem"
    AutoGenerateColumns="False" HeaderStyle-CssClass="grdHeader" BorderWidth="1px" BorderColor="#DFDFDF"
    PageSize="20" RowStyle-CssClass="grdItem" Width="100%">
    <PagerSettings Visible="False" />
    <Columns>
        <asp:TemplateField>
            <ItemStyle Width="20px" />
            <ItemTemplate>
                <input type="checkbox" name="id" value="<%# Eval("ID") %>#<%#Convert.ToString(DataBinder.Eval(Container.DataItem,"Title")).Replace("&nbsp;","").Replace("'","")%>" />
            </ItemTemplate>
        </asp:TemplateField>
           
        <asp:TemplateField HeaderText="Chuyên mục">
            <ItemTemplate>
                <%#Eval("Title")%>
            </ItemTemplate>    
        </asp:TemplateField>
    </Columns>
     <RowStyle CssClass="grdItem" />
                <HeaderStyle CssClass="grdHeader" />
                <AlternatingRowStyle CssClass="grdAlterItem" />
    </asp:GridView>
        <br />
        <asp:Button ID="btnChon" runat="server" OnClick="btnChon_Click" Text="Chọn" /></div>
    </form>
</body>
</html>
