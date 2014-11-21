<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DieuPhoiCauHoi.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.GiaoLuu.DieuPhoiCauHoi" %>
<asp:HiddenField ID="hdArgs" runat="server" />
<asp:HiddenField ID="hdNewsID" runat="server" />
<h1 style="text-align: center">
    ĐIỀU PHỐI CÂU HỎI</h1>
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
                AllowPaging="True" PageSize="40" onrowdatabound="grdListNews_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Bạn đọc gửi câu hỏi">
                        <ItemTemplate>
                        <div style="display: none">
                                <input type="text" id="hID" value="<%#Eval("Question_ID") %>" /></div>
                            <b><%#Eval("User_Name") %> - <%# Eval("User_Sex")=="1" ? "Nam":"Nữ" %> - <%#Eval("User_Age") %> tuổi</b><br />
                           Email: <%#Eval("User_Email") %> - thời gian gửi: <span style="color:Blue;"><%#Convert.ToDateTime(Eval("Question_Time")).ToString("dd/MM/yyyy - hh:mm:ss")%></span> <br />
                            <p>
                                <%#HttpUtility.HtmlEncode(Eval("Question_Content").ToString()).Replace("\n", "<br/>")%>
                            </p>
                        </ItemTemplate>
                        <ItemStyle CssClass="vertical-align-top tieudelist" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Điều phối người trả lời">
                        <ItemTemplate>
                            <b><%# Convert.ToBoolean(Eval("Status")) ? "<span style='color:red;'>Đã trả lời</span>" : ""%></b><br />
                            <asp:DropDownList ID="cboUser" runat="server" onchange="list_cboUser_selectedIndexChange(this)"
                                CausesValidation="False">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle CssClass="valign-middle tieudelist" VerticalAlign="Middle" Width="200px"></ItemStyle>
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
  
    function list_cboUser_selectedIndexChange(target) {
        showLoading();

        var select = $(target).parent().parent().find("select");
        var user = select.val();
       
        var idquestion = $(target).parent().parent().find("input[id='hID']").val();
        $.post("/Ajax/GiaoLuu/DieuPhoi.aspx?action=updateDiephoi&userResponse=" + user + "&id=" + idquestion, {}, function (data) {
            hideLoading();
        });
        return false;
    }


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
</script>
