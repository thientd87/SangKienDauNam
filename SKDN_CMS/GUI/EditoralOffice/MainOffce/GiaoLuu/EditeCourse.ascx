<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditeCourse.ascx.cs"
    Inherits="DFISYS.GUI.EditoralOffice.MainOffce.GiaoLuu.EditeCourse" %>
<%@ Register TagPrefix="CKEditor" Namespace="CKEditor.NET" Assembly="CKEditor.NET" %>
<link rel="stylesheet" type="text/css" href="/Styles/Newsedit.css?date=1806" />
<script language="javascript" type="text/javascript" src="/Scripts/Newsedit.js?ver=1"></script>
<h1 style="text-align: center">
    Sửa nội dung giao lưu trực tuyến</h1>
<div class="form floatleftallchild">
    <div class="t1">
        <span>Tiêu đề giao lưu</span><em>*</em>
    </div>
    <asp:TextBox ID="txtTitle" CssClass="w1" runat="server"></asp:TextBox>
    <img src="/images/blank.gif" class="break" />
    <div class="t1">
        <span>Tiêu đề nhỏ:</span><em>*</em>
    </div>
    <asp:TextBox ID="txtSubTitle" CssClass="w1" runat="server"></asp:TextBox>
    <img src="/images/blank.gif" class="break" />
    <div class="t1">
        Chọn ảnh
    </div>
    <asp:TextBox ID="txtSelectedFile" runat="server" CssClass="w1"></asp:TextBox>&nbsp;
    <img src="/images/icons/folder.gif" onclick="chooseFile('avatar', '<%=txtSelectedFile.ClientID %>')"
        style="cursor: pointer; float: left; padding: 0px 3px" />
    <img src="/images/img_preview.png" id="imgPreview" style="cursor: pointer; float: left;
        padding: 0px 3px" />
    <span style="width: 10px">&nbsp;</span> <span>Hiện ảnh</span>
    <asp:CheckBox ID='chkShowComment' class="ms-input" runat="server" Checked="true" />
    <img src="/images/blank.gif" class="break" />
    <div class="t1">
        Chú thích ảnh
    </div>
    <asp:TextBox ID="txtImageTitle" CssClass="w1" runat="server"></asp:TextBox>
    <img src="/images/blank.gif" class="break" />
    <div class="t1">
        Tóm tắt
    </div>
    <asp:TextBox ID="txtInit" TextMode="MultiLine" Rows="5" CssClass="w1" runat="server"></asp:TextBox>
    <span id="numberOfWord" class="t2">Phần tóm tắt không được quá 50 từ</span>
    <img src="/images/blank.gif" class="break1" />
    <div class="t1">
        Nội dung chi tiết
    </div>
    <%--<a href="#" onclick="modelessDialogShowBoxPV(1); return false;"><img src="/Images/page_add.png" />Chèn mã cổ phiếu</a>
        <img src="/images/blank.gif" class="break" />
        <div class="t1">
            &nbsp;
        </div>--%>
    <div id="editors">
      <CKEditor:CKEditorControl FilebrowserBrowseUrl="/FileManager/index.html" BasePath="/ckeditor/" runat="server" Width="800px" ID="NewsContent"
            runat="server" />
    </div>
    <div style="clear: both; width: 100%">
        &nbsp;
    </div>
    <div class="t1">
        Mã bài tương ứng
    </div>
    <asp:TextBox ID="txtNewID" CssClass="w1" runat="server"></asp:TextBox>
    <img src="/images/blank.gif" class="break" />
    <div class="t1">
        Thời gian bắt đầu
    </div>
    <div style="display: none">
        <input type="text" id="hidVoteID" value="<% = courseID %>" /></div>
    <asp:TextBox MaxLength="10" ID="txtFromDate" Width="75px" runat="server" CssClass="calendar" />
    <a onclick="if(self.gfPop)gfPop.fPopCalendar(document.getElementById('<% = txtFromDate.ClientID %>'));return false;"
        href="javascript:void(0)">
        <img class="PopcalTrigger" height="21" src="/Scripts/DatePicker/datepicker.gif" width="34"
            align="absMiddle" border="0">
    </a>
    <img src="/images/blank.gif" class="break" />
    <div class="t1">
        Kích hoạt:
    </div>
    <asp:CheckBox runat="server" ClientIDMode="Static" CssClass="big" ID="chkActive">
    </asp:CheckBox>
    <img src="/images/blank.gif" class="break" />
    <div class="t1">
        Duyệt nội dung:
    </div>
    <asp:DropDownList ID="ddlOrder" runat="server" >
        <asp:ListItem Selected="True" Value="0">Duyệt lên đầu</asp:ListItem>
        <asp:ListItem Value="1">Duyệt xuống cuối</asp:ListItem>
    </asp:DropDownList>
    <img src="/images/blank.gif" class="break" />
    <div class="t1">
        Trạng thái
    </div>
    <asp:DropDownList ID="ddlStatus" runat="server">
        <asp:ListItem Selected="True" Value="0">Chưa bắt đầu</asp:ListItem>
        <asp:ListItem Value="1">Đang diễn ra</asp:ListItem>
        <asp:ListItem Value="2">Đã kết thúc</asp:ListItem>
    </asp:DropDownList>
    <img src="/images/blank.gif" class="break" />
    <asp:Button ID="btnUpdate" Text="Lưu lại" runat="server" OnClientClick="return Validate();"
        OnClick="btnUpdate_Click" CssClass="btnUpdate"></asp:Button>
</div>
<%--<div>
    <table id="saveVote" cellpadding="5" cellspacing="5">
        <tr>
            <td colspan="2">
                <h1 style="text-align: center">
                    Sửa giao lưu trực tuyến</h1>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Tiêu đề tin:
            </td>
            <td>
                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="big" ID="txtName"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Tiêu đề nhỏ:
            </td>
            <td>
                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="big" ID="TextBox1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Tóm tắt:
            </td>
            <td>
                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="big" ID="TextBox2"
                    Rows="5" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Nội dung chi tiết:
            </td>
            <td>
                <editor:WYSIWYGEditor scriptPath="/NCEditor/Scripts/" ID="NewsContent" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style1">
                Thời gian bắt đầu
            </td>
            <td>
                <div style="display: none">
                    <input type="text" id="hidVoteID" value="<% = sourseID %>" /></div>
                <asp:TextBox MaxLength="10" ID="txtFromDate" Width="75px" runat="server" CssClass="calendar" />
                <a onclick="if(self.gfPop)gfPop.fPopCalendar(document.getElementById('<% = txtFromDate.ClientID %>'));return false;"
                    href="javascript:void(0)">
                    <img class="PopcalTrigger" height="21" src="/Scripts/DatePicker/datepicker.gif" width="34"
                        align="absMiddle" border="0">
                </a>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Kích hoạt
            </td>
            <td>
                <asp:CheckBox runat="server" ClientIDMode="Static" CssClass="big" ID="chkActive">
                </asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Ảnh tham chiếu
            </td>
            <td>
                <asp:CheckBox runat="server" ClientIDMode="Static" CssClass="big" ID="CheckBox1">
                </asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Mã bài tương ứng:
            </td>
            <td>
                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="big" ID="TextBox3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Trạng thái
            </td>
            <td>
                <asp:DropDownList ID="ddlChuyenmuc" runat="server" AutoPostBack="true">
                    <asp:ListItem Selected="True" Value="0">Chưa bắt đầu</asp:ListItem>
                    <asp:ListItem Value="1">Đang diễn ra</asp:ListItem>
                    <asp:ListItem Value="2">Đã kết thúc</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button runat="server" CssClass="button white" Text="Lưu" ID="btnSave" />
                &nbsp; &nbsp;
                <asp:Button runat="server" CssClass="button white" Text="Xóa" ID="btnDelete" />
                &nbsp;
                <input class="button white" type="button" onclick="$.facebox.close()" value="Đóng" />
            </td>
        </tr>
    </table>
</div>--%>
<script language="javascript">
    var prefix = '<% = ClientID %>'; var cpmode = '<% = Request.QueryString["cpmode"] %>';
</script>
