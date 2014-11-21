<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.WebLinks.List" %>
<asp:HiddenField ID="hdArgs" runat="server" />
<asp:HiddenField ID="hdNewsID" runat="server" />
<style>
    .commentUser
    {
        font-size: 12px;
        color: darkgrey;
    }
    .commentLabel
    {
        font-size: 11px;
        color: lightgrey;
        margin: 0 !important;
        padding: 0;
    }
</style>
<h1 style="text-align: center">
    Quản lý weblink</h1>
<table id="tblSearch" runat="server" cellpadding="5" cellspacing="5" style="width: 100%;">
    <tr>
        <td style="vertical-align: middle;">
            <table cellpadding="5" cellspacing="5">
                <tr>
                    <td width="120" style="vertical-align: middle; padding: 10px 0">
                        Từ khóa
                    </td>
                    <td style="vertical-align: middle; padding: 10px 0">
                        <asp:TextBox AutoCompleteType="None" ID="txtKeyword" Width="350" runat="server"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btnUpdate" OnClientClick="endRequest = 'window.scrollTo(0,0)'"
                            runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </td>
        <td>
            <br />
        </td>
    </tr>
</table>
<div style="float: left; width: 100%;">
    <asp:UpdatePanel ID="panel" UpdateMode="conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 10px; margin-bottom: 10px;">
                <a class="btnUpdate" rel="colorbox" href="/Ajax/Weblinks/EditWebLink.aspx?weblinkId=0">
                    Thêm mới</a></div>
            <asp:GridView Width="100%" ID="grdListNews" runat="server" CssClass="gtable sortable"
                EmptyDataText="<span style='color:Red'><b>Không có bài !</b></span>" AutoGenerateColumns="False"
                AllowPaging="false">
                <Columns>
                    
                    <asp:TemplateField HeaderText="Nội dung">
                        <ItemTemplate>
                            <div style="cursor: default" id="name_<%#Eval("WebLink_ID")%>">
                                <a rel="colorbox" href="/Ajax/Weblinks/EditWebLink.aspx?weblinkId=<%# Eval("WebLink_ID") %>">
                                    <%#HttpUtility.HtmlEncode(Eval("WebLink_Name").ToString()).Replace("\n", "<br/>")%></a></div>
                            <h5 class="commentLabel">
                                Đường link <a id="url_<%#Eval("WebLink_ID")%>" target="_blank" href="<%# Eval("WebLink_URL") %>">
                                    <span style="color:#c0c0c0;"><%#HttpUtility.HtmlEncode(Convert.ToString(Eval("WebLink_URL")))%></span></a></h5>
                        </ItemTemplate>
                        <ItemStyle CssClass="vertical-align-top tieudelist" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a style="float: left;" href="#" onclick="Delete(<%# Eval("WebLink_ID") %>)">
                                <img src="/Images/Icons/delete.gif" /></a> <a style="float: left;" rel="colorbox"
                                    href="/Ajax/Weblinks/EditWebLink.aspx?weblinkId=<%# Eval("WebLink_ID") %>">
                                    <img src="/Images/edit.gif" border="0" /></a>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" CssClass="valign-middle" />
                        <ItemStyle CssClass="valign-middle" Width="50px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div style="margin-top: 10px; margin-bottom: 10px;">
                <a class="btnUpdate" rel="colorbox" href="/Ajax/Weblinks/EditWebLink.aspx?weblinkId=0">
                    Thêm mới</a></div>
            <%-- <table>
                <tr>
                    <td width="100" style="vertical-align: top; padding-top: 10px">
                        Đi đến trang
                    </td>
                    <td style="vertical-align: middle; padding-top: 10px; padding-bottom: 10px;">
                        <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlPageUp" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged" />
                    </td>
                </tr>
            </table>--%>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnFilter" EventName="Click" />--%>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</div>
<script type="text/javascript" src="/scripts/calendar.js"></script>
<script language="JavaScript" type="text/javascript" src="/scripts/newslist2.js"></script>
<script language="javascript">
    function GetControlByName(id) {
        return document.getElementById("<% = ClientID %>_" + id);
    }
    function Filter() {
        var txtFromDate = GetControlByName("txtFromDate");
        var txtToDate = GetControlByName("txtToDate");
        if (txtFromDate.value == "" || txtToDate.value == "") {
            alert("Bạn cần phải chọn cả ngày bắt đầu và ngày kết thúc!");
            return false;
        }
        return true;
    }
</script>
<script type="text/javascript">
    $(document).ready(function ($) {
        $('a[rel*=colorbox]').facebox({ width: 900, height: 500 });
    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequest);
    function EndRequest(sender, args) {
        $('#bgFilter').hide();
        $('#imgloading').hide();
        $('a[rel*=colorbox]').facebox({ width: 900, height: 500 });
        $('*[rel*=editbox]').facebox({ iframe: true, width: 900, height: 500 });
    }

    function Delete(id) {
        if (!confirm('Bạn có muốn xóa weblink không')) return;

        $.post("/Ajax/Weblinks/EditWebLink.aspx?action=delete&weblinkId=" + id, {}, function (data) {
            $.facebox.close();
            $("#name_" + id).parent().parent().remove();
        });
    }

    function Save(id, status) {
        if ($("#txtName").val() == "") {
            alert("Chưa nhập Nội dung");
            $("#txtName").focus();
            return false;
        }

        if ($("#txtURL").val() == "") {
            alert("Chưa nhập Email");
            $("#txtURL").focus();
            return false;
        }
        $.post("/Ajax/Weblinks/EditWebLink.aspx?post=true&status=" + status + "&weblinkId=" + id, $('#saveWeblink *').serialize(), function (data) {
            $("#name_" + id).html($("#txtName").val().replace(/\n/gim, '</br>'));
            $("#url_" + id).html($("#txtURL").val());
            $("#url_" + id).attr("href", $("#txtURL").val());
            $.facebox.close();
            if (status == true)
                $("#content_" + id).parent().parent().remove();
        });
        return false;
    }
     
</script>
