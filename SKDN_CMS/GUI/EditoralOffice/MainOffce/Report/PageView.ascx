<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageView.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.Report.PageView" %>
     <h1 style="text-align: center">
    Thống kê PageView</h1>
<table id="tblSearch" runat="server" cellpadding="5" cellspacing="5" style="width: 100%;">
    <tr>
        <td style="vertical-align: middle;">
            <table cellpadding="5" cellspacing="5">
                <tr>
                    <td style="vertical-align: middle">
                        Chọn chuyên mục &nbsp;
                    </td>
                    <td style="vertical-align: middle">
                        <asp:DropDownList ID="ddlChuyenmuc" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChuyenmuc_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
        <td>
            <fieldset class="xemtintheongay">
                <legend>Thống kê theo ngày</legend><span>Từ &nbsp;</span>
                <asp:TextBox MaxLength="10" ID="txtFromDate" Width="75px" runat="server" CssClass="calendar" />
                <a onclick="if(self.gfPop)gfPop.fPopCalendar(document.getElementById('<% = txtFromDate.ClientID %>'));return false;"
                    href="javascript:void(0)">
                    <img class="PopcalTrigger" height="21" src="/Scripts/DatePicker/datepicker.gif" width="34"
                        align="absMiddle" border="0">
                </a><span>đến &nbsp;</span>
                <asp:TextBox MaxLength="10" ID="txtToDate" Width="75px" runat="server" CssClass="calendar" />
                <a onclick="if(self.gfPop)gfPop.fPopCalendar(document.getElementById('<% = txtToDate.ClientID %>'));return false;"
                    href="javascript:void(0)">
                    <img class="PopcalTrigger" height="21" src="/Scripts/DatePicker/datepicker.gif" width="34"
                        align="absMiddle" border="0">
                </a>
                <asp:ImageButton ID="btnFilter" runat="server" OnClick="btnFilter_Click" OnClientClick="return Filter();"
                    Width="22px" Height="21px" ImageUrl="/images/Icons/search.gif" />
            </fieldset>
            <br />
        </td>
    </tr>
</table>
<div style="float: left; width: 100%;">
    <asp:UpdatePanel ID="panel" UpdateMode="conditional" runat="server">
        <ContentTemplate>
            <asp:GridView Width="100%" ID="grdListNews" runat="server" CssClass="gtable sortable"
                EmptyDataText="<span style='color:Red'><b>Không có bài !</b></span>" AutoGenerateColumns="False"
                AllowPaging="false" onrowdatabound="grdListNews_RowDataBound">
                <Columns>                    
                    <asp:TemplateField HeaderText="Tên chuyên mục">
                        <ItemTemplate>
                            <div style="cursor: default" >
                                <a style="float:left;" rel="colorbox" href="/Ajax/Report/PageView.aspx?catId=<%# Eval("PageView_ID") %>&firstDate=<%=firstDate%>&endDate=<%=endDate %>">
                                     <asp:Literal ID="ltrName" runat="server"></asp:Literal>
                                </a>
                                </div>
                        </ItemTemplate>
                        <ItemStyle CssClass="vertical-align-top tieudelist" VerticalAlign="Top" Width="200px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tỷ lệ %">
                        <ItemTemplate>
                            <table cellpadding="0" cellspacing="0" width="100%" border="0px" style="line-height:10px;" >
                                    <tr>
                                        <asp:Literal ID="ltrPhanTram" runat="server"></asp:Literal>
                                    </tr>
                                </table>
                        </ItemTemplate>
                        <ItemStyle CssClass="vertical-align-top tieudelist" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Page View">
                        <ItemTemplate>
                            <%#Eval("PageView_Count")%>
                        </ItemTemplate>
                        <ItemStyle CssClass="vertical-align-top tieudelist" VerticalAlign="Top" Width="60px"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
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
            <asp:AsyncPostBackTrigger ControlID="btnFilter" EventName="Click" />
            <%--<asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />--%>
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
        if (!confirm('Bạn có muốn xóa vote không')) return;

        $.post("/Ajax/Vote/EditeVote.aspx?action=delete&voteId=" + id, {}, function (data) {
            $.facebox.close();
            $("#name_" + id).parent().parent().remove();
        });
    }
    function DeleteItem(id) {
        if (!confirm('Bạn có muốn xóa lựa chọn không')) return;
        $.post("/Ajax/Vote/EditeVote.aspx?action=deleteitem&voteitemId=" + id, {}, function (data) {
            $("#voteitContent_" + id).parent().parent().remove();
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
        $.post("/Ajax/Vote/EditeVote.aspx?post=true&status=" + status + "&voteId=" + id, $('#saveVote *').serialize(), function (data) {
            $("#name_" + id).html($("#txtName").val().replace(/\n/gim, '</br>'));
            $.facebox.close();
            if (status == true)
                $("#name_" + id).parent().parent().remove();
        });
        return false;
    }
    function SaveItem(id, status) {
        $("#imgloading").show();
        if ($("#voteitContent_" + id.toString()).val() == "") {
            alert("Bạn chưa nhập Nội dung");
            $("#voteitContent_" + id.toString()).focus();
            return false;
        }
        var stt = $('#voteitstt_' + id).val();
        var content = $('#voteitContent_' + id).val();
        $.post("/Ajax/Vote/EditeVote.aspx?post=true&status=" + status + "&voteItemId=" + id + "&stt=" + stt + "&content=" + content, $('#saveVote *').serialize(), function (data) {
            $("#name_" + id).html($("#txtName").val().replace(/\n/gim, '</br>'));
            $("#imgloading").hide();
            //            $.facebox.close();
            //            if (status == true)
            //                $("#content_" + id).parent().parent().remove();
        });
        $("#imgloading").hide();
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
        alert(content);
        $.post("/Ajax/Vote/EditeVote.aspx?post=true&action=insertVoteItem&voteID=" + voteID + "&stt=" + stt + "&content=" + content, $('#saveVote *').serialize(), function (data) {
            //$("#name_" + itemID).html($("#txtName").val().replace(/\n/gim, '</br>'));
            //            $.facebox.close();
            //            if (status == true)
            //                $("#content_" + id).parent().parent().remove();
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
