<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PublishedList.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.PublishedList.PublishedList" %>
<div style=" padding: 20px; width:1000px">
	Từ khóa: <asp:TextBox runat="server" ID="txtKey" Width="200"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
    Chọn chuyên mục: <asp:DropDownList ID="ddlChuyenmuc" runat="server" AutoPostBack="false"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button runat="server" ID="btnSearch" CssClass="btnUpdate" Text="Tìm kiếm" onclick="btnSearch_Click" />    
</div>
<asp:GridView Width="100%" ID="grdListNews" runat="server" CssClass="gtable" EmptyDataText="<span style='color:Red'><b>Không có bài !</b></span>"
  AutoGenerateColumns="False" AllowPaging="True" PageSize="40" OnPageIndexChanging="grdListNews_PageIndexChanging">
  <Columns>
    <asp:TemplateField>
      <HeaderTemplate>
        <input type="checkbox" id="chkAll" onclick="tonggle(grdListNewsID, this.checked, 'chkSelect')" />
      </HeaderTemplate>
      <ItemTemplate>
        <input type="checkbox" value='<%#Eval("News_Id")%>' name="chkSelect" onclick="selectRow(this)"
          runat="server" id="chkSelect" />
      </ItemTemplate>
      <HeaderStyle Width="20px" />
      <ItemStyle Width="20px" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Tiêu đề bài viết" ItemStyle-CssClass="text" SortExpression="News_Title">
      <ItemTemplate>
        <div class="contexcolumn" onmouseover="this.className = 'contexcolumn_hover'" onmouseout="this.className = 'contexcolumn'">
          <p>
            <a rel="colorbox" href="/Preview/default.aspx?news=<%#Eval("News_ID") %>" target="_blank">
              <%# HttpUtility.HtmlEncode(Convert.ToString(Eval("News_Title")))%>
            </a>
          </p>
        </div>
      </ItemTemplate>
    </asp:TemplateField>
     <asp:TemplateField HeaderText="Tác giả" >
        <ItemTemplate >
            <center><%#Eval("News_Athor")%></center>
        </ItemTemplate>
        <ItemStyle Width="150"/>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Người xuất bản" >
        <ItemTemplate >
            <center><%#Eval("News_Approver")%></center>
        </ItemTemplate>
        <ItemStyle Width="150"/>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Ngày xuất bản" >
        <ItemTemplate >
            <center><%#Eval("News_PublishDate") %></center>
        </ItemTemplate>
        <ItemStyle Width="150"/>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Lượt xem" >
        <ItemTemplate >
            <center><%#Eval("ViewCount")%></center>
        </ItemTemplate>
        <ItemStyle Width="150"/>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Edit" >
        <ItemTemplate >
           <a style='display:<%#Eval("News_Athor").ToString().ToLower() == HttpContext.Current.User.Identity.Name.ToString().ToLower() ? "" : "none" %>' href='/office/editpublist,publishedlist/<%#Eval("News_ID") %>.aspx?source=/office/publishedlistoff.aspx'>Edit</a>
        </ItemTemplate>
        <ItemStyle Width="50" HorizontalAlign="Center"/>
    </asp:TemplateField>
  </Columns>
  <PagerStyle CssClass="pagination" HorizontalAlign="left" />
  <PagerSettings Mode="NumericFirstLast" />
  <HeaderStyle CssClass="grdHeader" />
  <RowStyle CssClass="grdItem" />
  <AlternatingRowStyle CssClass="grdAlterItem" />
</asp:GridView>
<script type="text/javascript">
    $(document).ready(function ($) {
        $('a[rel*=colorbox]').facebox({ width: 900, height: 500 });
    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequest);
    function EndRequest(sender, args) {
        $('#bgFilter').hide();
        $('#imgloading').hide();
        $('a[rel*=colorbox]').facebox({ iframe: true, width: 900, height: 500 });
    }
    
</script>