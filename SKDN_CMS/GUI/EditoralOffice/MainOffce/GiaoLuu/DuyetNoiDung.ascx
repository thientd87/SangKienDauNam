<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DuyetNoiDung.ascx.cs"
    Inherits="DFISYS.GUI.EditoralOffice.MainOffce.GiaoLuu.DuyetNoiDung" %>
<asp:HiddenField ID="hdArgs" runat="server" />
<asp:HiddenField ID="hdNewsID" runat="server" />
<h1 style="text-align: center">
    DUYỆT NỘI DUNG</h1>
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
                EmptyDataText="<span style='color:Red'><b>Không có câu hỏi !</b></span>" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="40" OnRowDataBound="grdListNews_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Bạn đọc gửi câu hỏi">
                        <ItemTemplate>
                            <div style="display: none">
                                <input type="text" id="hID" value="<%#Eval("Question_ID") %>" /></div>
                            <div style="border-bottom: 1px dotted; color: #7C7C7C; font-size: 12px; line-height: 13px;">
                                <b>
                                    <%#Eval("User_Name") %>
                                    <%#Eval("User_Sex") %>
                                    -<asp:Literal ID="ltSex" runat="server"></asp:Literal>
                                    -
                                    <%#Eval("User_Age") %>
                                    tuổi</b><br />
                                Email:
                                <%#Eval("User_Email") %>
                                - thời gian gửi: <span style="color: Blue;">
                                    <%#Convert.ToDateTime(Eval("Question_Time")).ToString("dd/MM/yyyy - hh:mm:ss")%></span>
                                <br />
                                <p>
                                    <%#Eval("Question_Content").ToString()%>
                                </p>
                            </div>
                            <asp:Literal ID="userSay" runat="server"></asp:Literal>
                            <p>
                                <%#Eval("Question_Answer").ToString()%>
                            </p>
                        </ItemTemplate>
                        <ItemStyle CssClass="vertical-align-top tieudelist" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Thao tác">
                        <ItemTemplate>
                            <a class="button blue" href="/office/giao_luu_editepublic.aspx?questionID=<%#Eval("Question_ID") %>">
                                Duyệt</a> <a class="button red" href="javascript:void(0);" id="content_<%#Eval("Question_ID") %>"
                                    onclick="Delete(<%#Eval("Question_ID") %>)">Xóa</a>
                        </ItemTemplate>
                        <ItemStyle CssClass="valign-middle tieudelist" VerticalAlign="Middle" Width="130px">
                        </ItemStyle>
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
                    <td style="vertical-align: middle; padding-top: 10px; padding-bottom: 10px; padding-left: 10px;">
                        <a rel="colorbox" class="button blue" href="/Ajax/GiaoLuu/LichSuDuyet.aspx?courseID=<%=courseID %>">Xem lại các lần
                            duyệt</a>
                    </td>
                    <td style="vertical-align: middle; padding-top: 10px; padding-bottom: 10px;">
                        <a class="button blue" href="/office/giao_luu_editepublic.aspx?questionID=0&courseID=<%=courseID %>">
                            Biên tập lại nội dung</a>
                    </td>
                </tr>
            </table>
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
        hideLoading();
        $('a[rel*=colorbox]').facebox({ width: 900, height: 500 });
        $('*[rel*=editbox]').facebox({ iframe: true, width: 900, height: 500 });
    }




    function Delete(id) {
        if (!confirm('Bạn có muốn xóa nội dung này không')) return;
        showLoading();
        $.post("/Ajax/GiaoLuu/DuyetNoiDung.aspx?action=delete&questionID=" + id, {}, function (data) {
            hideLoading();
            $("#content_" + id).parent().parent().remove();
        });
    }

</script>
