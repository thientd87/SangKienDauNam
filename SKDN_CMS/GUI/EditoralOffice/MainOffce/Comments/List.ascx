<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.Comments.List" %>
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
    Quản lý bình luận</h1>
<table id="tblSearch" runat="server" cellpadding="5" cellspacing="5" style="width: 100%;">
    <tr>
        <td style="vertical-align: middle;">
            <table cellpadding="5" cellspacing="5">
                <tr>
                    <td style="vertical-align: middle">
                        Loại bình luận
                    </td>
                    <td style="vertical-align: middle">
                        <asp:DropDownList ID="ddlChuyenmuc" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChuyenmuc_SelectedIndexChanged">
                            <asp:ListItem Value="0">Chưa duyệt</asp:ListItem>
                            <asp:ListItem Value="1">Đã duyệt</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="120" style="vertical-align: middle; padding: 10px 0">
                        Từ khóa
                    </td>
                    <td style="vertical-align: middle; padding: 10px 0">
                        <asp:TextBox AutoCompleteType="None" ID="txtKeyword" Width="350" runat="server"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btnUpdate" OnClientClick="endRequest = 'window.scrollTo(0,0)'"
                            runat="server" Text="Tìm kiếm" onclick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </td>
        <td>
            <%--<fieldset class="xemtintheongay">
                <legend>Xem tin theo ngày</legend><span>Từ</span>
                <asp:TextBox MaxLength="10" ID="txtFromDate" Width="75px" runat="server" CssClass="calendar" />
                <a onclick="if(self.gfPop)gfPop.fPopCalendar(document.getElementById('<% = txtFromDate.ClientID %>'));return false;"
                    href="javascript:void(0)">
                    <img class="PopcalTrigger" height="21" src="/Scripts/DatePicker/datepicker.gif" width="34"
                        align="absMiddle" border="0">
                </a><span>đến</span>
                <asp:TextBox MaxLength="10" ID="txtToDate" Width="75px" runat="server" CssClass="calendar" />
                <a onclick="if(self.gfPop)gfPop.fPopCalendar(document.getElementById('<% = txtToDate.ClientID %>'));return false;"
                    href="javascript:void(0)">
                    <img class="PopcalTrigger" height="21" src="/Scripts/DatePicker/datepicker.gif" width="34"
                        align="absMiddle" border="0">
                </a>
                <asp:ImageButton ID="btnFilter" runat="server" OnClientClick="return Filter();" Width="22px"
                    Height="21px" ImageUrl="/images/Icons/search.gif" />
            </fieldset>--%>
            <br />
        </td>
    </tr>
</table>
<div style="float: left; width: 100%;">
    <asp:UpdatePanel ID="panel" UpdateMode="conditional" runat="server">
        <ContentTemplate>
            <asp:GridView Width="100%" ID="grdListNews" runat="server" CssClass="gtable sortable"
                EmptyDataText="<span style='color:Red'><b>Không có bài !</b></span>" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="40">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <input type="checkbox" id="chkAll" onclick="tonggle(grdListNewsID, this.checked, 'chkSelect'); " />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" value='<%#Eval("Comment_ID")%>' name="chkSelect" onclick="selectRow(this)"
                                runat="server" id="chkSelect" />
                        </ItemTemplate>
                        <HeaderStyle Width="20px" CssClass="valign-middle" />
                        <ItemStyle CssClass="valign-middle" Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nội dung Bình luận">
                        <ItemTemplate>
                            <div rel="editbox" style="cursor: default" id="content_<%#Eval("Comment_ID")%>">
                                <%#HttpUtility.HtmlEncode(Eval("Comment_Content").ToString()).Replace("\n" , "<br/>")%></div>
                            <h5 class="commentLabel">
                                Trong bài viết: <a rel="colorbox" href="/preview/default.aspx?news=<%# Eval("News_ID") %>">
                                    <%#HttpUtility.HtmlEncode(Convert.ToString(Eval("News_Title")))%></a></h5>
                            <h5 style="cursor: default" class="commentLabel">
                                Người bình luận: <span id="user_<%#Eval("Comment_ID")%>" class="commentUser">
                                    <%#HttpUtility.HtmlEncode(Eval("Comment_User").ToString())%>
                                    -
                                    <%#HttpUtility.HtmlEncode(Eval("Comment_Email").ToString())%></span> vào lúc
                                <%# Eval("Comment_Date")%></h5>
                        </ItemTemplate>
                        <ItemStyle CssClass="vertical-align-top tieudelist" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a rel="editbox" href="/Ajax/Comment/EditComment.aspx?commentId=<%# Eval("Comment_ID") %>">
                                <img src="/Images/edit.gif" border="0" /></a>
                        </ItemTemplate>
                        <HeaderStyle Width="20px" CssClass="valign-middle" />
                        <ItemStyle CssClass="valign-middle" Width="20px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <table>
                <tr>
                    <td width="100" style="vertical-align: top; padding-top: 10px">
                        Đi đến trang
                    </td>
                    <td style="vertical-align: middle; padding-top: 10px; padding-bottom: 10px;">
                        <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlPageUp" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnFilter" EventName="Click" />--%>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlChuyenmuc" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlPageUp" EventName="SelectedIndexChanged" />
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
        $('*[rel*=editbox]').facebox({ iframe: true, width: 900, height: 500 });
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
        if (!confirm('Bạn có muốn xóa không')) return;
        
        $.post("/Ajax/Comment/EditComment.aspx?action=delete&commentId=" + id, {}, function (data) {
            $.facebox.close();
            $("#content_" + id).parent().parent().remove();
        });
    }

    function Save(id, status) {
        if ($("#txtUser").val() == "") {
            alert("Chưa nhập User");
            $("#txtUser").focus();
            return false;
        }

        if ($("#txtEmail").val() == "") {
            alert("Chưa nhập Email");
            $("#txtEmail").focus();
            return false;
        }

        if ($("#txtContent").val() == "") {
            alert("Chưa nhập nội dung");
            $("#txtContent").focus();
            return false;
        }

        $.post("/Ajax/Comment/EditComment.aspx?post=true&status=" + status + "&commentId=" + id, $('#saveComment *').serialize(), function (data) {
            $("#content_" + id).html($("#txtContent").val().replace(/\n/gim, '</br>'));
            $("#user_" + id).html($("#txtUser").val() + " - " + $("#txtEmail").val());
            $.facebox.close();
            if (status == true)
                $("#content_" + id).parent().parent().remove();
        });
        return false;
    }
     
</script>
