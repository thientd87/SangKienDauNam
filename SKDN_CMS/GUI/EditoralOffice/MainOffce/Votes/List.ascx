<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.Votes.List" %>
<asp:HiddenField ID="hdArgs" runat="server" />
<asp:HiddenField ID="hdNewsID" runat="server" />
<style>
    .commentUser
    {
        font-size: 12px;
    }
    .commentLabel
    {
        font-size: 11px;
        margin: 0 !important;
        padding: 0;
    }
</style>
<h1 style="text-align: center">
    Quản lý Vote</h1>
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
                <a class="btnUpdate" rel="colorbox" href="/Ajax/Vote/EditeVote.aspx?voteId=0">Thêm mới</a></div>
            <asp:GridView Width="100%" ID="grdListNews" runat="server" CssClass="gtable sortable"
                EmptyDataText="<span style='color:Red'><b>Không có bài !</b></span>" AutoGenerateColumns="False"
                AllowPaging="false">
                <Columns>
                    <asp:TemplateField HeaderText="Nội dung">
                        <ItemTemplate>
                            <a  rel="colorbox" href="/Ajax/Vote/EditeVote.aspx?voteId=<%# Eval("Vote_ID") %>">
                                <div id="name_<%#Eval("Vote_ID")%>">
                                    <%#HttpUtility.HtmlEncode(Eval("Vote_Title").ToString()).Replace("\n", "<br/>")%></div>
                            </a>
                            <h5 class="commentLabel">
                                Thời gian từ <b>
                                    <%#Eval("Vote_StartDate")%></b> đến <b>
                                        <%#Eval("Vote_EndDate")%></b>
                            </h5>
                        </ItemTemplate>
                        <ItemStyle CssClass="vertical-align-top tieudelist" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kích hoạt">
                        <ItemTemplate>
                            <input type="checkbox" <%# Eval("isActive").ToString().Equals("True") ?  "checked='checked'" : "" %> />
                        </ItemTemplate>
                        <HeaderStyle Width="55px" CssClass="valign-middle" />
                        <ItemStyle CssClass="valign-middle" Width="25px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a style="float: left;" href="#" onclick="Delete(<%# Eval("Vote_ID") %>)">
                                <img src="/Images/Icons/delete.gif" /></a> <a style="float: left;" rel="colorbox"
                                    href="/Ajax/Vote/EditeVote.aspx?voteId=<%# Eval("Vote_ID") %>">
                                    <img src="/Images/edit.gif" border="0" /></a>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" CssClass="valign-middle" />
                        <ItemStyle CssClass="valign-middle" Width="50px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div style="margin-top: 10px; margin-bottom: 10px;">
                <a class="btnUpdate" rel="colorbox" href="/Ajax/Vote/EditeVote.aspx?voteId=0">Thêm mới</a></div>
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
        $('a[rel*=colorbox]').facebox({ width: 900, height: 500 });
        $('*[rel*=editbox]').facebox({ iframe: true, width: 900, height: 500 });
    }

    function Delete(id) {
        if (!confirm('Bạn có muốn xóa vote không')) return;
        showLoading();
        $.post("/Ajax/Vote/EditeVote.aspx?action=delete&voteId=" + id, {}, function (data) {
            $.facebox.close();
            $("#name_" + id).parent().parent().remove();
            hideLoading();
        });
    }
    function DeleteItem(id) {
        if (!confirm('Bạn có muốn xóa lựa chọn không')) return;
        showLoading();
        $.post("/Ajax/Vote/EditeVote.aspx?action=deleteitem&voteitemId=" + id, {}, function (data) {
            $("#voteitContent_" + id).parent().parent().remove();
            hideLoading();
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
        showLoading();
        $.post("/Ajax/Vote/EditeVote.aspx?post=true&status=" + status + "&voteId=" + id, $('#saveVote *').serialize(), function (data) {
            $("#name_" + id).html($("#txtName").val().replace(/\n/gim, '</br>'));
            $.facebox.close();
            if (status == true)
                $("#name_" + id).parent().parent().remove();
            hideLoading();
        });
        return false;
    }
    function SaveItem(id, status) {
        if ($("#voteitContent_" + id.toString()).val() == "") {
            alert("Bạn chưa nhập Nội dung");
            $("#voteitContent_" + id.toString()).focus();
            return false;
        }
        var stt = $('#voteitstt_' + id).val();
        var content = $('#voteitContent_' + id).val();
        showLoading();
        $.post("/Ajax/Vote/EditeVote.aspx?post=true&status=" + status + "&voteItemId=" + id + "&stt=" + stt + "&content=" + content, $('#saveVote *').serialize(), function (data) {
            $("#name_" + id).html($("#txtName").val().replace(/\n/gim, '</br>'));
            hideLoading();
        });
        return false;
    }
    function InsertItem(itemID, voteID) {
        if ($("#voteitContent_" + itemID.toString()).val() == "") {
            alert("Bạn chưa nhập Nội dung");
            $("#voteitContent_" + itemID.toString()).focus();
            return false;
        }
        var stt = $('#voteitstt_' + itemID).val();
        var content = $('#voteitContent_' + itemID).val();
        showLoading();
        $.post("/Ajax/Vote/EditeVote.aspx?post=true&action=insertVoteItem&voteID=" + voteID + "&stt=" + stt + "&content=" + content, $('#saveVote *').serialize(), function (data) {
            hideLoading();
        });
        return false;
    }

    function AddNewsVoteItem(id) {
        var voteid = $('#hidVoteID').val();
        var uuid = (new Date()).getUTCMilliseconds();
        var html = '<tr><td style="width:20px;" class="valign-middle"><input type="text" id="voteitstt_' + uuid.toString() + '" style="width: 100%; text-align: center;"></td>'
        + '<td valign="top" class="valign-middle big"><input type="text" id="voteitContent_' + uuid.toString() + '" style="width: 100%;"></td>' +
        '<td style="width:50px;" class="valign-middle"><a onclick="InsertItem(' + uuid.toString() + ',' + voteid + ')" href="javascript:void(0)" style="float: left;"><img src="/Images/Icons/save.gif"></a></td></tr>';
        $("#grdListVote").append(html);
    }
</script>
