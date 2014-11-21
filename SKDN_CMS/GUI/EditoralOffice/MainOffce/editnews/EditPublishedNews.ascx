<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditPublishedNews.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.editnews.EditPublishedNews" %>

<%@ Register TagPrefix="CKEditor" Namespace="CKEditor.NET" Assembly="CKEditor.NET" %>
<link rel="stylesheet" type="text/css" href="/Styles/Newsedit.css?date=1806" />
<script language="javascript" type="text/javascript" src="/Scripts/Newsedit.js?ver=1"></script>
<h1 style="text-align: center"><asp:Literal ID="Literal1" runat="server"></asp:Literal></h1>

<h1><asp:Literal ID="ltrEdit" runat="server"></asp:Literal></h1>
<div style="padding: 2px; width: 98%; padding-top: 20px;">
<div style="overflow:hidden; padding:5px; text-align:center; color:Red; font-weight:bold">
    <asp:Literal runat="server" ID="ltrXuatBan"></asp:Literal>
</div>
    <div class="form floatleftallchild">
        <div class="t1">
            <span>Chuyên mục</span> <em>*</em></div>
        <asp:DropDownList ID="lstCat" CssClass="t5" runat="server" onchange="NumberWordInSapo();">
        </asp:DropDownList>
        <img src="/images/blank.gif" class="break" />        
        <div class="t1">
            Tiêu đề nhỏ
        </div>
        <asp:TextBox ID="txtSubTitle" runat="server" CssClass="w1"></asp:TextBox>
        <img src="/images/blank.gif" class="break" />
        <div class="t1">
            <span>Tiêu đề tin</span><em>*</em>
        </div>
        <asp:TextBox ID="txtTitle" CssClass="w1" runat="server"></asp:TextBox>
        <img src="/images/blank.gif" class="break" />
        <div class="t1">
            Chọn ảnh
        </div>
        <asp:TextBox ID="txtSelectedFile" runat="server" CssClass="w1"></asp:TextBox>&nbsp;
        <img src="/images/icons/folder.gif" onclick="chooseFile('avatar', '<%=txtSelectedFile.ClientID %>')" style="cursor: pointer;" />
        <img src="/images/img_preview.png" id="imgPreview" style=""/>
        <img src="/images/blank.gif" class="break" />
        <div class="t1">
            Ảnh to
        </div>
        <asp:TextBox ID="txtIcon" runat="server" CssClass="w1"></asp:TextBox>&nbsp;
        <img src="/images/icons/folder.gif" onclick="chooseFile('icon', '<%=txtIcon.ClientID %>')"
            style="cursor: pointer;" />
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
        <span id="numberOfWord" class="t2">Phần tóm tắt không được quá 58 từ</span>
        <img src="/images/blank.gif" class="break1" />
        <div class="t1">
            Nội dung chi tiết
        </div>        
        <div id="editors">
            <CKEditor:CKEditorControl FilebrowserBrowseUrl="/FileManager/index.html" BasePath="/ckeditor/" runat="server" Width="800px" ID="NewsContent" runat="server" />
        </div>
        <asp:Panel runat="server" Visible="false" ID="pnControl" Width="100%" >
        <div style="clear:both;width:100%;">&nbsp;</div>
        <div style="float: left; width: 775px;">
            <div style="float: left; width: 540px">
                <div class="t1">
                    Copy sang WAP
                </div>
                <div style="position:relative">
                  <%--<asp:CheckBoxList ID="lstNewsLetter" runat="server" DataTextField="Name" DataValueField="ID" RepeatDirection="Horizontal" RepeatColumns="2" Visible="false">
                  </asp:CheckBoxList>--%><asp:CheckBox ID="chkCopyToWap" runat="server" />
                </div>
                <img src="/images/blank.gif" class="break" />
                <div class="t1">
                    Link gốc
                </div>
                <asp:TextBox ID="txtSourceLink" runat="server" CssClass="w1"></asp:TextBox>
                <img src="/images/blank.gif" class="break" />
                <div class="t1">
                    Chọn mã CP
                </div>
                <asp:TextBox ID="txtMaCP" runat="server" CssClass="w1"></asp:TextBox>

                <script type="text/javascript">
                    var options, a;
                    jQuery(function () {
                        options = { serviceUrl: '/Search.ashx' };
                        a = $('#<%=txtMaCP.ClientID %>').autocomplete(options);
                    });
                </script>

                <img src="/images/blank.gif" class="break" />
                <div class="t1">
                    Source:
                </div>
                <asp:TextBox ID="txtSource" runat="server" CssClass="w1"></asp:TextBox>
                <img src="/images/blank.gif" class="break" />
                <div class="t1">
                    Tỉnh thành:
                </div>
                <asp:DropDownList ID="ddlProvinces" runat="server" DataTextField="ProvinceName" DataValueField="ProvinceID">
                </asp:DropDownList>
                <br /><br />
                <table cellpadding="2" cellspacing="2" border="0">
                    <tr style="display: none">
                        <td>
                            Loại tin
                        </td>
                        <td>
                            <asp:DropDownList ID="cboIsHot" runat="server">
                                <asp:ListItem Selected="True" Value="0" Text="Tin th&#244;ng thường"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Nổi bật trang chủ"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Nổi bật ở mục"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Tin focus"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Tin giữa"></asp:ListItem>
                                <asp:ListItem Value="5" Text="Tin vắn"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <img src="/images/blank.gif" class="break" />
                <div class="t1">
                    Tác giả
                </div>
                <asp:DropDownList ID="ddlAuthor" runat="server" DataTextField="TenTacGia" DataValueField="TacGia_ID">
                </asp:DropDownList>
                <br /><br />
                <div class="t1">
                    &nbsp;
                </div>
                <div style="float:left">
                    <span>Tin Ưu Tiên</span>
                    <asp:CheckBox ID="chkIsFocus" Text="" runat="server"></asp:CheckBox>
                    <span style="width:10px">&nbsp;</span>
                    <span>Hiện ảnh</span>
                    <asp:CheckBox ID='chkShowComment' class="ms-input" runat="server" />
                    <span style="width:10px">&nbsp;</span>
                    <span>Highlight</span>
                    <asp:CheckBox ID='chkShowRate' class="ms-input" runat="server" />
                </div>
                <img src="/images/blank.gif" class="break" />
               <div class="t1">
                    &nbsp;
                </div>
                <div style="float:left">
                    <fieldset>
                        <legend>Loại file đính kèm</legend>
                        <asp:CheckBoxList ID="cblFileType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="10">
                        <asp:ListItem Value="0">Word</asp:ListItem>
                        <asp:ListItem Value="1">Excel</asp:ListItem>
                        <asp:ListItem Value="2">PDF</asp:ListItem>
                        <asp:ListItem Value="3">Ảnh</asp:ListItem>
                        <asp:ListItem Value="4">Video</asp:ListItem>
                        <%--<asp:ListItem Value="5">Powerpoint</asp:ListItem>
						<asp:ListItem Value="6">Zip</asp:ListItem>--%>
						<asp:ListItem Value="7">Biểu đồ</asp:ListItem>
                    </asp:CheckBoxList>
                    </fieldset>
                </div>
                <asp:Panel ID="pn" runat="server">
                    <%--Nhung control khong dung--%>
                    <asp:TextBox ID="txtExtension1" Visible="False" runat="server"></asp:TextBox>
                    <asp:CheckBox ID="chkNoiBatNhat" Visible="False" runat="server"></asp:CheckBox>
                </asp:Panel>
            </div>
            <div style="float: right; width: 210px; padding-right: 20px;">
                <div style="float: right; width: 210px; padding-right: 20px;">
                    <div class="t3" style="padding-bottom: 5px;">
                        <b>Chọn chuyên mục khác</b>
                    </div>
                    <div class="t3 CheckBoxList">
                        <asp:CheckBoxList ID="lstOtherCat" runat="server" Width="180px">
                        </asp:CheckBoxList>
                    </div>
                </div>
            </div>
        </div>
        <img src="/images/blank.gif" class="break" />
        <div class="t1">
            &nbsp;
        </div>
        <fieldset style="width: 640px ;">
            <legend>Cấu hình</legend>
            <div class="chonbailienquan">
                <a class="title" style="float: left;" onclick="chooseNews(); return false;" href="#">
                    Chọn bài liên quan:</a> <a style="float: right;" href="#" onclick="list_remove(document.getElementById('<%=cboNews.ClientID %>')); return false;">
                        <img src="/images/delete.gif" /></a> <a style="float: right;" href="#" onclick="list_moveup(document.getElementById('<%=cboNews.ClientID %>')); return false;">
                            <img src="/images/icons/up1.gif" /></a> <a style="float: right;" href="#" onclick="list_movedown(document.getElementById('<%=cboNews.ClientID %>')); return false;">
                                <img src="/images/icons/down1.gif" /></a>
                <asp:ListBox ID="cboNews" runat="server" Width="300px" Height="80px" />

                 <a class="title" style="float: left;" onclick="chooseThreadV2(); return false;" href="#">
                    Chọn luồng sự kiện:</a> <a style="float: right;" href="#" onclick="list_remove(document.getElementById('<%=lstThread.ClientID %>')); return false;">
                        <img src="/images/delete.gif" /></a> 
                        
                 <asp:ListBox ID="lstThread" runat="server" CssClass="t5"  Width="300px" Height="40px" DataTextField="Title" DataValueField="Thread_ID"></asp:ListBox>
            </div>
            <div class="chonmedialienquan">
                <div>
                    <span style="color:Red;font-weight:700;">Tags - Luồng sự kiện nổi bật</span>
                    <br />
                    <br />
                    <asp:CheckBoxList ID="cblTags" runat="server" Width="180px">
                        </asp:CheckBoxList>
                    <%--<a class="title" style="float: left;" onclick="chooseMedia(); return false;" href="#">
          Chọn media liên quan:</a> <a style="float: right;" href="#" onclick="list_remove(document.getElementById('<%=cboMedia.ClientID %>')); return false;">
            <img src="/images/delete.gif" /></a> <a style="float: right;" href="#" onclick="list_moveup(document.getElementById('<%=cboMedia.ClientID %>')); return false;">
            </a><a style="float: right;" href="#" onclick="list_movedown(document.getElementById('<%=cboMedia.ClientID %>')); return false;">
            </a>
        <asp:ListBox ID="cboMedia" runat="server" Width="300px" Height="100px" />--%>
                </div>
            </div>
            <img src="/images/blank.gif" class="break1" />
            <div style="float: left">
                <div>Thời gian đưa</div>
            </div>
            <div style="padding-left: 100px">
                <asp:DropDownList ID="cboYear" runat="server">
                    <asp:ListItem Text="Chọn năm"></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="cboMonth" runat="server">
                    <asp:ListItem Text="Chọn tháng"></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="cboDay" runat="server">
                    <asp:ListItem Text="Chọn ngày"></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="cboHour" runat="server">
                    <asp:ListItem Text="Chọn giờ"></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="cboMinute" runat="server">
                    <asp:ListItem Text="Chọn phút"></asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="cboSercond" runat="server">
                    <asp:ListItem Text="Chọn giây"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </fieldset>
       
        <img src="/images/blank.gif" class="break1" />
        <div class="t1">
            &nbsp;
        </div>
         </asp:Panel>
         <img src="/images/blank.gif" class="break" />
         <div style="overflow:hidden; padding-left:125px">
            <asp:Button ID="btnUpdated" Visible="false" Text="Lưu lại" runat="server" OnClientClick="return Validate();" OnClick="btnUpdated_Click" CssClass="btnUpdate"></asp:Button>
            <asp:Button ID="btnSend" Text="Xóa" Visible="false" runat="server" OnClick="btnSend_Click" OnClientClick="return Validate();" CssClass="btnUpdate"></asp:Button>
            <asp:Button ID="btnPublish" Text="Xuất bản lại" runat="server" OnClick="btnPublish_Click" OnClientClick="return Validate();" CssClass="btnUpdate"></asp:Button>
         </div>
        
      
        <img src="/images/blank.gif" class="break1" />
    </div>
</div>
<div style="display: none;">
    <asp:HiddenField ID="hidLuongSuKien" runat="server" />
    <asp:HiddenField ID="hdRelatNews" runat="server" />
    <asp:HiddenField ID="hdMedia" runat="server" />
    <asp:HiddenField ID="hidNewsID" runat="server" />
    <%--<asp:TextBox ID="txtSource" runat="server"></asp:TextBox>--%>
    <asp:HiddenField ID="hdTag" runat="server" />
    <asp:TextBox ID="txtExtension2" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtExtension3" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtExtension4" runat="server"></asp:TextBox>
    <%--<asp:Button ID="btnNhuanBut" OnClientClick="showModalPopup('editform', true, 10); return false;"
      Visible="false" runat="server" Text="Nhuận bút" OnClick="btnNhuanBut_Click" />--%>
    <asp:ListBox ID="cboTag" runat="server" Width="300px" Height="100px" />
</div>
<asp:ObjectDataSource OnUpdated="objsoure_Updated" OnInserted="objsoure_Inserted"
    ID="objsoure" InsertMethod="CreateNews_Extension" UpdateMethod="UpdateNews" runat="server"
    TypeName="DFISYS.BO.Editoral.Newsedit.NewsEditHelper">
    <InsertParameters>
        <asp:QueryStringParameter Name="_news_id" QueryStringField="NewsRef" Type="Int64" />
        <asp:ControlParameter ControlID="lstCat" DefaultValue="1" Name="_cat_id" PropertyName="SelectedValue" Type="Int32" />
        <asp:ControlParameter ControlID="txtSubTitle" DefaultValue="" Name="_news_subtitle" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="txtTitle" Name="_news_title" PropertyName="Text" Type="String" />
        <asp:Parameter Name="_news_image" Type="string" />
        <asp:ControlParameter Name="_news_source" Type="String" ControlID="txtSource" PropertyName="Text" />
        <asp:ControlParameter ControlID="txtInit" DefaultValue="" Name="_news_init" PropertyName="Text" Type="String" />
        <asp:ControlParameter Name="_news_content" Type="String" ControlID="NewsContent" PropertyName="Text" />
        <asp:Parameter DefaultValue="" Name="_poster" Type="String" />
        <asp:ControlParameter ControlID="chkIsFocus" Name="_news_isfocus" PropertyName="Checked" Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="_news_status" Type="Int32" />
        <asp:ControlParameter ControlID="cboIsHot" Name="_news_type" PropertyName="SelectedValue" Type="Int32" />
        <asp:ControlParameter ControlID="hdRelatNews" Name="_related_news" PropertyName="Value" Type="string" />
        <asp:ControlParameter ControlID="hdMedia" Name="_obj_media" PropertyName="Value" Type="String" />
        <asp:Parameter Name="_other_cat" Type="string" />
        <asp:Parameter Name="_switchtime" Type="dateTime" />
        <asp:ControlParameter ControlID="chkShowComment" Name="_isShowComment" PropertyName="Checked" Type="boolean" /> 
        <asp:ControlParameter ControlID="chkShowRate" Name="_isShowRate" PropertyName="Checked" Type="boolean" />
        <asp:ControlParameter ControlID="ddlProvinces" Name="_template" PropertyName="SelectedValue" Type="Int32" />
        <asp:ControlParameter ControlID="txtImageTitle" Name="_news_title_image" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="txtIcon" Name="_news_icon" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="hidLuongSuKien" Name="_thread_id" PropertyName="Value" Type="String" />
        <asp:ControlParameter ControlID="chkNoiBatNhat" Name="_isNoiBatNhat" PropertyName="Checked" Type="boolean" />
        <asp:ControlParameter ControlID="txtMaCP" Name="_str_Extension1" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="txtExtension2" Name="_str_Extension2" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="txtSourceLink" Name="_str_Extension3" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="ddlAuthor" Name="_str_Extension4" PropertyName="SelectedValue" Type="int32" />
        <asp:ControlParameter ControlID="hdTag" Name="_tag_id" PropertyName="Value" Type="String" />
        <asp:SessionParameter Name="_newsTemp_id" SessionField="strTempNewsID" Type="string" />
    </InsertParameters>
    <UpdateParameters>
        <asp:QueryStringParameter Name="_news_id" QueryStringField="NewsRef" Type="Int64" />
        <asp:ControlParameter ControlID="lstCat" Name="_cat_id" PropertyName="SelectedValue" Type="Int32" />
        <asp:ControlParameter ControlID="txtSubTitle" Name="_news_subtitle" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="txtTitle" Name="_news_title" PropertyName="Text" Type="String" />
        <asp:Parameter Name="_news_image" Type="string" />
        <asp:ControlParameter Name="_news_source" Type="String" ControlID="txtSource" PropertyName="Text" />
        <asp:ControlParameter ControlID="txtInit" DefaultValue="" Name="_news_init" PropertyName="Text" Type="String" />
        <asp:ControlParameter Name="_news_content" Type="String" ControlID="NewsContent" PropertyName="Text" />
        <asp:ControlParameter ControlID="chkIsFocus" Name="_news_isfocus" PropertyName="Checked" Type="Boolean" />
        <asp:Parameter DefaultValue="3" Name="_news_status" Type="Int32" />
        <asp:ControlParameter ControlID="cboIsHot" Name="_news_type" PropertyName="SelectedValue" Type="Int32" />
        <asp:ControlParameter ControlID="hdRelatNews" Name="_related_news" PropertyName="Value" Type="string" />
        <asp:Parameter Name="_other_cat" Type="string" />
        <asp:Parameter Name="_switchtime" Type="dateTime" DefaultValue="01/01/2000" />
        <asp:Parameter Name="_isSend" Type="boolean" DefaultValue="false" />
        <asp:ControlParameter ControlID="chkShowComment" Name="_isShowComment" PropertyName="Checked" Type="boolean" />
        <asp:ControlParameter ControlID="chkShowRate" Name="_isShowRate" PropertyName="Checked" Type="boolean" />
        <%--<asp:Parameter Name="_template" Type="Int32" />--%>
        <asp:ControlParameter ControlID="ddlProvinces" Name="_template" PropertyName="SelectedValue" Type="Int32" />
        <asp:ControlParameter ControlID="hdMedia" Name="_obj_media" PropertyName="Value" Type="String" />
        <asp:ControlParameter ControlID="txtImageTitle" Name="_news_title_image" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="txtIcon" Name="_news_icon" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="hidLuongSuKien" Name="_thread_id" PropertyName="Value" Type="String" />
        <asp:ControlParameter ControlID="chkNoiBatNhat" Name="_isNoiBatNhat" PropertyName="Checked" Type="boolean" />
        <asp:ControlParameter ControlID="txtMaCP" Name="_str_Extension1" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="txtExtension2" Name="_str_Extension2" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="txtSourceLink" Name="_str_Extension3" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="ddlAuthor" Name="_str_Extension4" PropertyName="SelectedValue" Type="int32" />
        <asp:ControlParameter ControlID="hdTag" Name="_tag_id" PropertyName="Value" Type="String" />
        <asp:SessionParameter Name="_newsTemp_id" SessionField="strTempNewsID" Type="string" />
    </UpdateParameters>
</asp:ObjectDataSource>

<script language="javascript">
    var prefix = '<% = ClientID %>'; var cpmode = '<% = Request.QueryString["cpmode"] %>';
    var obj = oUtil.obj;
    var editor = oUtil.obj;

    function insertMultipleImage_loadValue(arrImagesURL) {
        var html = '';
        for (var i = 0; i < arrImagesURL.length; i++) {
            html += '<div style="text-align: center;"><img border="0" src="' + arrImagesURL[i] + '" /></div><br />';
        }
        if (document.all) {
            editor.insertHTML(html);
        }
        else {
            editor.insertHTML(html);
        }

    }
    setTimeout(function () {
        $("#<% = txtTitle.ClientID %>").focus();
    }, 1000);
</script>
