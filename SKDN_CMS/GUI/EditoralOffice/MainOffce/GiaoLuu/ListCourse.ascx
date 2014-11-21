<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListCourse.ascx.cs"
    Inherits="DFISYS.GUI.EditoralOffice.MainOffce.GiaoLuu.ListCourse" %>
<asp:HiddenField ID="hdArgs" runat="server" />
<asp:HiddenField ID="hdNewsID" runat="server" />
<h1 style="text-align: center">
    Quản lý giao lưu trực tuyến</h1>
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
    </tr>
</table>
<div style="float: left; width: 100%;">
    <asp:UpdatePanel ID="panel" UpdateMode="conditional" runat="server">
        <ContentTemplate>
            <asp:GridView Width="100%" ID="grdListNews" runat="server" CssClass="gtable sortable" 
                EmptyDataText="<span style='color:Red'><b>Không có bài !</b></span>" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="40" onrowdatabound="grdListNews_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Nội dung Giao lưu">
                        <ItemTemplate>
                            <div style="display: none">
                                <input type="text" id="hID" value="<%#Eval("Course_ID") %>" /></div>
                            <a href="/office/giao_luu_edite.aspx?courseId=<%#Eval("Course_ID")%>">
                                <%#HttpUtility.HtmlEncode(Eval("Course_Title").ToString()).Replace("\n", "<br/>")%></a>
                            <br />
                            <div>
                               <%-- <a rel="colorbox" href="/Ajax/GiaoLuu/GiaoPhongVan.aspx?sourseID=<%# Eval("Course_ID") %>">
                                    <span style="padding-left: 25px; font-size: 12px; color: #909090;">Danh sách người được
                                        phỏng vấn</span> </a>
                                <br />--%>
                                <div style="vertical-align: middle; padding-top: 10px; padding-bottom: 10px; float:left;">
                                    <a rel="colorbox" class="button blue" href="/Ajax/GiaoLuu/GiaoPhongVan.aspx?sourseID=<%# Eval("Course_ID") %>">Người phỏng vấn</a>
                                </div>
                                <div style="vertical-align: middle; padding-top: 10px; padding-bottom: 10px;float:left;">
                                    <a class="button blue" href="/office/giao_luu_dieuphoi.aspx?courseID=<%#Eval("Course_ID")%>">Điều phối người trả lời</a>
                                </div>
                                <div style="vertical-align: middle; padding-top: 10px; padding-bottom: 10px; float:left;">
                                    <a class="button blue" href="/office/giao_luu_duyet.aspx?courseID=<%#Eval("Course_ID")%>">Duyệt nội dung</a>
                                </div>
                                <div style="vertical-align: middle; padding-top: 10px; padding-bottom: 10px;float:left;">
                                    <a class="button blue" href="/office/giao_luu_traloi.aspx?courseID=<%#Eval("Course_ID")%>">Trả lời câu hỏi</a>
                                </div>
                                 

                               <%-- <a href="/office/giao_luu_duyet.aspx?courseID=<%#Eval("Course_ID")%>"><span style="padding-left: 25px; font-size: 12px; color: #909090;">Duyệt
                                    nội dung</span> </a>
                                <br />
                                <a href="/office/giao_luu_traloi.aspx?courseID=<%#Eval("Course_ID")%>"><span style="padding-left: 25px; font-size: 12px; color: #909090;">Trả lời câu hỏi
                                    </span> </a>
                                <br />
                                <a href="/office/giao_luu_dieuphoi.aspx?courseID=<%#Eval("Course_ID")%>"><span style="padding-left: 25px;
                                    font-size: 12px; color: #909090;">Điều phối người trả lời</span> </a>--%>
                            </div>
                        </ItemTemplate>
                        <ItemStyle CssClass="vertical-align-top tieudelist" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Trạng thái">
                        <ItemTemplate>
                            <asp:DropDownList ID="cboIsHot" runat="server" onchange="listCourse_cboIsHot_selectedIndexChange(this)"
                                CausesValidation="False">
                                <asp:ListItem Value="0" Text="Chưa bắt đầu"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Đang diễn ra"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Đã kết thúc"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle CssClass="valign-middle tieudelist" VerticalAlign="Middle"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a href="/office/giao_luu_edite.aspx?courseId=<%#Eval("Course_ID")%>">
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
            <div style="margin-top: 10px; margin-bottom: 10px;">
                <a class="btnUpdate" href="/office/giao_luu_edite.aspx?courseId=0">Thêm mới</a></div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
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

    function listCourse_cboIsHot_selectedIndexChange(target) {
        showLoading();
        var select = $(target).parent().parent().find("select");
        var status = select.val();
        var idquestion = $(target).parent().parent().find("input[id='hID']").val();
        $.post("/Ajax/GiaoLuu/PhongVan.aspx?action=updateStatus&status=" + status + "&id=" + idquestion, {}, function (data) {
            hideLoading();
        });
        return false;
    }


    function Delete(id) {
        if (!confirm('Bạn có muốn xóa không')) return;

        $.post("/Ajax/Comment/EditComment.aspx?action=delete&commentId=" + id, {}, function (data) {
            $.facebox.close();
            $("#content_" + id).parent().parent().remove();
        });
    }

    function Save(id, status) {
        showLoading();
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
            hideLoading();
        });
        return false;
    }

    function DeleteItem(target) {
        showLoading();
        if (!confirm('Bạn có muốn xóa không')) return;
        var sourseID = $(target).parent().parent().find("input[name='hID']").attr("value");
        $.post("/Ajax/GiaoLuu/GiaoPhongVan.aspx?action=deleteitem&id=" + sourseID, {}, function (data) {
            $(target).parent().parent().remove();
            hideLoading();
        });
    }
    function SaveItem(target) {
        showLoading();
        var select = $(target).parent().parent().find("select");
        var user = select.val();
        var name = $(target).parent().parent().find("input[name='nameManager']").val();
        var active = $(target).parent().parent().find("input[name='isActive']").attr("checked");
        var sourseID = $(target).parent().parent().find("input[name='hID']").attr("value");
        if (name == "") {
            alert("Bạn chưa nhập Nội dung");
            $(target).parent().parent().find("input[name='nameManager']").focus();
            hideLoading();
            return false;
        }

        $.post("/Ajax/GiaoLuu/GiaoPhongVan.aspx?post=true&user=" + user + "&id=" + sourseID + "&name=" + name + "&active=" + active, $('#giaoPhongVan *').serialize(), function (data) {
            hideLoading();
        });
        return false;
    }
    function InsertItem(sourseID) {
        showLoading();
        var name = $("#name").val();
        var user = $("#lsUserNew").val();
        var active = $("#active").val();
        if (name == "") {
            alert("Bạn chưa nhập Nội dung");
            $("#name").focus();
            return false;
        }
        $.post("/Ajax/GiaoLuu/GiaoPhongVan.aspx?post=true&action=insert&user=" + user + "&sourseID=" + sourseID + "&name=" + name + "&active=" + active, $('#giaoPhongVan *').serialize(), function (data) {
            $.facebox.close();
            hideLoading();
        });
       
        return false;
    }

</script>
