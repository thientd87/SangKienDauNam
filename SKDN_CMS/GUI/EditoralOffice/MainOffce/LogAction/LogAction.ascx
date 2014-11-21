<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LogAction.ascx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.LogAction.LogAction" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
    
<script type="text/javascript" src="/scripts/calendar.js"></script>

<link href="/styles/popupform.css" rel="stylesheet" type="text/css" />
<link href="/styles/Core.css" rel="stylesheet" type="text/css" />
<link href="/styles/Coress.css" rel="stylesheet" type="text/css" />
<link href="/styles/autopro.css" rel="stylesheet" type="text/css" />
<link href="/styles/popupform.css" rel="stylesheet" type="text/css" />
<link href="/styles/popupform.css" rel="stylesheet" type="text/css" />
<link href="/styles/pcal.css" rel="stylesheet" type="text/css" />
<link href="/styles/common.css" rel="stylesheet" type="text/css" />


 <div id="AlertDiv" class="AlertStyle">
 </div>
<script type="text/javascript" src="/scripts/ajax.js"></script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <table border="0" cellpadding="0" cellspacing="0" width="100%" class="ms-formbody">
    <tr>
        <td colspan="2" valign="top">
            <asp:GridView ID="gvDataAction" runat="server" AllowPaging="True" AutoGenerateColumns="False"
    DataSourceID="odsLogAction"
    
    HeaderStyle-CssClass="grdHeader"
    RowStyle-CssClass="grdItem" 
    AlternatingRowStyle-CssClass="grdAlterItem"
     PageSize="12" OnPageIndexChanged="gvDataAction_PageIndexChanged" OnRowDataBound="gvDataAction_RowDataBound" Width="100%">
    <Columns>
        <asp:TemplateField HeaderText="Tên đăng nhập">
        
            <ItemTemplate>
               <%#Eval("UserName")%>
            </ItemTemplate>
            <ItemStyle Width="20%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Ngày">
            <ItemTemplate>
                <%#((DateTime)Eval("CreatedDate")).ToString("dd/MM/yyyy hh:mm:ss tt")%>
            </ItemTemplate>        
            <ItemStyle Width="20%" HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Hành động">
            <ItemTemplate>
                <%#Eval("Action")%>
            </ItemTemplate>        
            <ItemStyle Width="60%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Loại" Visible="false">
            <ItemTemplate>
            <asp:Label runat="server" ID="lblType" Text='<%#Eval("Type")%>'></asp:Label>                
            </ItemTemplate>        
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
    </Columns>
    <PagerSettings Visible="False" />
                <RowStyle CssClass="grdItem" />
                <HeaderStyle CssClass="grdHeader" />
                <AlternatingRowStyle CssClass="grdAlterItem" />
</asp:GridView>
            </td>
    </tr>
    <tr>
        <td colspan="2" valign="top">
            <table id="tblPage" border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="height: 65px" width="30%">
                        &nbsp;<asp:Label ID="lblNumPageView" runat="server" Text="Xem trang:"></asp:Label>&nbsp;
                        <asp:DropDownList ID="drPage" runat="server"
    DataSourceID="odsPage" AutoPostBack="True" DataTextField="Text" DataValueField="Value" OnSelectedIndexChanged="drPage_SelectedIndexChanged">
                        </asp:DropDownList></td>
                    <td align="right" class="Menuleft_Item" style="height: 65px">
                        <a id="a"></a><a href="#a" onclick="return GoselectAll(true);"></a>
                    </td>
                </tr>
            </table>
        </td>
    </tr>    
</table>
<table id="tblSearch" runat="server" border="0" cellpadding="2" cellspacing="2" style="clear: both;
    border-right: #79a4d2 1px solid; border-top: #79a4d2 1px solid; border-left: #79a4d2 1px solid;
    width: 100%; border-bottom: #79a4d2 1px solid; height: 80px; background-color: #e5e5e5">
    <tr>
        <td style="padding-left: 10px" width="100" class="ms-input">
            Tên đăng nhập</td>
        <td>
            &nbsp;<asp:TextBox CssClass="ms-input" ID="txtUserName" runat="server"></asp:TextBox>
            <asp:Button ID="txtFind" runat="server" Text="Tìm kiếm" OnClick="txtFind_Click" /><span
                style="color: #0000ff; text-decoration: underline"></span></td>
    </tr>
    <tr>
        <td class="ms-input" style="padding-left: 10px" width="100">
            Hành động</td>
        <td>
            &nbsp;<asp:TextBox ID="txtHanhDong" runat="server" CssClass="ms-input" Width="80%"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="padding-left: 10px" class="ms-input">
            Ngày bắt đầu</td>
        <td>
            &nbsp;<asp:TextBox CssClass="ms-input" ID="txtStartDate" runat="server"></asp:TextBox>
            <img onclick="VC_Calendar(<% =txtStartDate.ClientID %>,true);" src="/Images/Calendar.gif"
                style="cursor: hand" title="Lịch" /></td>
    </tr>
    <tr>
        <td style="padding-left: 10px" class="ms-input">
            Ngày kết thúc</td>
        <td>
            &nbsp;<asp:TextBox CssClass="ms-input" ID="txtEndDate" runat="server"></asp:TextBox>
            <img onclick="VC_Calendar(<% =txtEndDate.ClientID %>,true);" src="/Images/Calendar.gif"
                style="cursor: hand" title="Lịch" /></td>
    </tr>
    &nbsp;&nbsp;&nbsp;<tr>
        <td style="padding-left: 10px">
        </td>
        <td>
            
        </td>
    </tr>
</table>
    
<br />



<asp:ObjectDataSource ID="odsPage" runat="server" SelectMethod="GetPage" TypeName="Portal.BO.Editoral.LogNews.LogHelper">
    <SelectParameters>
        <asp:ControlParameter ControlID="gvDataAction" Name="numPage" PropertyName="PageCount"
            Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsLogAction" runat="server" EnablePaging="True"
    MaximumRowsParameterName="PageSize" SelectCountMethod="GetNumRows" SelectMethod="SelectAll"
    StartRowIndexParameterName="StartRow" TypeName="Portal.BO.Editoral.LogNews.LogHelper">
    <SelectParameters>
        <asp:Parameter Name="Sort" Type="String" />
        <asp:Parameter Name="StartRow" Type="Int32" />
        <asp:Parameter Name="PageSize" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
    </ContentTemplate>
</asp:UpdatePanel>

