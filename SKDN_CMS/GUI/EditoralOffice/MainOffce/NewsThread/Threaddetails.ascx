<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Threaddetails.ascx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.NewsThread.Threaddetails" %>
<script language="javascript" src="/scripts/Grid.js"></script>
<link href="/styles/popupform.css" rel="stylesheet" type="text/css" />
<link href="/styles/Coress.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="/Styles/Newsedit.css" />
<link href="/styles/popupform.css" rel="stylesheet" type="text/css" />
<link href="/styles/common.css" rel="stylesheet" type="text/css" />
<link href="/styles/Core.css" rel="stylesheet" type="text/css" />
<link href="/styles/autopro.css" rel="stylesheet" type="text/css" />
<link href="/styles/popupform.css" rel="stylesheet" type="text/css" />
<link href="/styles/pcal.css" rel="stylesheet" type="text/css" />

<table width="100%">
    <tr>
        <td class="Edit_Head_Cell">
            Quản lý luồng sự kiện <asp:Label ID="lblThreadTitle" runat="server"></asp:Label></td>
    </tr>    
</table>
<table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr>
        <td>
            <table cellpadding="0" cellspacing="5" width="100%">
                <tr>
                    <td colspan="2">
                        <asp:GridView Width="100%" ID="grdThreadDetails" runat="server" HeaderStyle-CssClass="grdHeader" RowStyle-CssClass="grdItem"
                                AlternatingRowStyle-CssClass="grdAlterItem" AutoGenerateColumns="False" AllowPaging="True" DataSourceID="objThreaddetailSource" PageSize="100" OnRowCommand="grdThreadDetails_RowCommand"> 
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <input type="checkbox" id="chkAll" onclick="CheckAll();"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server"/>
                                    </ItemTemplate>
                                    <HeaderStyle Width="2%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ti&#234;u đề tin">
                                        <ItemTemplate>
                                            <asp:Literal ID="ltrNewsTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"News_Title") %>'></asp:Literal>
                                        </ItemTemplate>
                                    <HeaderStyle Width="58%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Thuộc sự kiện" Visible="False">
                                        <ItemTemplate>
                                            <asp:Literal ID="ltrNews" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>'></asp:Literal>
                                        </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="X&#243;a khỏi sk">
                                    <ItemTemplate>
                                        <center>
						                    <asp:ImageButton id="btnDelete" OnClientClick="return confirm('Bạn có muốn xóa bài thuộc luồng này hay không?')" runat="server" CommandName="Delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Threaddetails_ID")%>' CausesValidation="False" ImageUrl="~/Images/delete.gif"></asp:ImageButton>
					                    </center>
					                </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                </asp:TemplateField>
                            </Columns>
                                <RowStyle CssClass="grdItem" />
                                <HeaderStyle CssClass="grdHeader" />
                                <AlternatingRowStyle CssClass="grdAlterItem" />
                                <PagerSettings Visible="False"/>     
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="left" colspan="2" style="padding-top:15px">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td width="30%" class="ms-input">
                                    Xem trang&nbsp;<asp:DropDownList ID="cboPage" runat="Server" DataTextField="Text" DataValueField="Value" AutoPostBack="true" DataSourceID="objdspage" CssClass="ms-input"></asp:DropDownList>
                                </td>
                                <td align="right" style="height: 19px" class="Menuleft_Item">
                                   <a id="a"></a><a onclick="GoselectAll();" href="javascript:void(0)">Chọn tất cả</a>&nbsp;|
                                    <a onclick="GoUnselectAll();" href="javascript:void(0)">Bỏ chọn</a><asp:Literal ID="Literal0" Text="&nbsp;|" runat="server"></asp:Literal>
                                    <asp:LinkButton ID="lnkAddNews" runat="server"  CssClass="normalLnk">Đưa tin vào sk</asp:LinkButton>&nbsp;|
                                    <asp:LinkButton ID="lnkRealDel" OnClientClick="return confirm('Bạn có muốn xóa những luồng đã chọn hay không?')" runat="server"  CssClass="normalLnk" OnClick="lnkRealDel_Click">Xóa bài đã chọn</asp:LinkButton>            
                                    |&nbsp;<a href="/office/listthread.aspx">Quay lại</a>
                                </td>
                            </tr>
                        </table>  
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="padding-top:10px;" style="display:none">
            <table cellpadding="0" cellspacing="0" border="0" style="width:100%; height:80px; border:1px solid #b8c1ca; background-color:#E5E5E5; clear:both">
                <tr>
                    <td class="ms-input" >
                        
                        Tìm theo từ khóa:
                        <asp:TextBox ID="txtKeyword" runat="server" CssClass="ms-long" Width="130px"></asp:TextBox>&nbsp;
                        <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="ms-input"/>
                    </td>
                    <td align="right" class="ms-input">
                        Xem sự kiện khác:
                        <asp:DropDownList ID="cboThread" runat="server" DataTextField="Title" DataValueField="Thread_ID" AutoPostBack="True" DataSourceID="objThreadSource" OnSelectedIndexChanged="cboThread_SelectedIndexChanged" CssClass="ms-input">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>    
        </td>
    </tr>    
</table>
<asp:ObjectDataSource ID="objdspage" runat="server" SelectMethod="getPage" TypeName="ChannelVN.CoreBO.Threads.ThreadHelper" OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:ControlParameter DefaultValue="0" ControlID="grdThreadDetails" Name="numPage" PropertyName="PageCount" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="objThreadSource" runat="server" SelectMethod="getAllThread" TypeName="ChannelVN.CoreBO.Threads.ThreadHelper"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="objThreaddetailSource" runat="server" SelectMethod="GetThreadDetails" SelectCountMethod="GetThreadRowsCount" DeleteMethod="DelNewsThread" TypeName="ChannelVN.CoreBO.Threads.Threaddetails" EnablePaging="true" MaximumRowsParameterName="PageSize" StartRowIndexParameterName="StartRow">
    <SelectParameters>
         <asp:Parameter Name="strWhere" DefaultValue="" Type ="string" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="_selected_id" Type="string" DefaultValue=""/>
    </DeleteParameters>
</asp:ObjectDataSource>