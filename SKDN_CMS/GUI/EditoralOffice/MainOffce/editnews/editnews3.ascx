<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="editnews3.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.editnews.editnews3" %>

<%@ Register TagPrefix="CKEditor" Namespace="CKEditor.NET" Assembly="CKEditor.NET" %>
<script src="/ckeditor/ckeditor.js"></script>
<link rel="stylesheet" type="text/css" href="/Styles/Newsedit.css?date=1806" />
<script language="javascript" type="text/javascript" src="/Scripts/Newsedit.js?ver=1"></script>
<style>
    .m-wrap.large.txtinitContent{width: 600px !important}
</style>
<div class="container-fluid">
    <!-- BEGIN PAGE HEADER-->
    <div class="row-fluid">
        <div class="span12">
            <!-- BEGIN PAGE TITLE & BREADCRUMB-->
            <h3 class="page-title">
                News manager <small> <asp:Literal ID="ltrEdit" runat="server"></asp:Literal></small>
            </h3>
            <!-- END PAGE TITLE & BREADCRUMB-->
        </div>
    </div>
    <!-- END PAGE HEADER-->
    <!-- BEGIN PAGE CONTENT-->
    <div class="row-fluid">
        <div class="span12">
            <div class="portlet box blue">
              <div class="portlet-title">
					<div class="caption">
						<i class="icon-reorder"></i>
						<span class="hidden-480">News detail</span>
					</div>
				</div>
                <div class="portlet-body">
                    <div class="tabbable portlet-tabs">
						<div class="tab-content">
							<div class="tab-pane active form-horizontal">
							    <div class="span12">
							        <div class="span6">
							       <div class="control-group">
									    <label class="control-label">Chuyên mục tin<em>*</em></label>
									    <div class="controls">
										     <asp:DropDownList ID="lstCat" CssClass="t5" runat="server" onchange="NumberWordInSapo();">
                                               </asp:DropDownList>
									    </div>
								    </div>
                                    <div class="control-group">
									    <label class="control-label">Tiêu đề tin<em>*</em></label>
									    <div class="controls">
										     <asp:TextBox ID="txtTitle" CssClass="m-wrap larger" runat="server"></asp:TextBox>
									    </div>
								    </div>
                                    <div class="control-group">
									    <label class="control-label">Chọn ảnh</label>
									    <div class="controls">
										     <asp:TextBox ID="txtSelectedFile" runat="server" class="m-wrap larger"></asp:TextBox>&nbsp;
                                             <img src="/images/icons/folder.gif" onclick="openInfo('/FileManager/index.html?field_name=<%=txtSelectedFile.ClientID %>',900,700)" style="cursor: pointer; padding: 0px 3px" />
									    </div>
								    </div>
                                    <div class="control-group">
									    <label class="control-label">Tóm tắt</label>
									    <div class="controls">
										     <asp:TextBox ID="txtInit" TextMode="MultiLine" Rows="5" CssClass="m-wrap large txtinitContent" runat="server"></asp:TextBox>
                                             <span id="numberOfWord" class="t2 hidden">Phần tóm tắt không được quá 50 từ</span>
									    </div>
								    </div>
                                    <div class="control-group">
									    <label class="control-label">Tin đọc nhiều</label>
									    <div class="controls">
										     <asp:CheckBox ID="chkIsFocus" Text="" runat="server" Checked="true"></asp:CheckBox>
									    </div>
								    </div>
                                    <div class="control-group">
									    <label class="control-label">Loại tin</label>
									    <div class="controls">
										     <asp:DropDownList ID="cboIsHot" runat="server">
                                                <asp:ListItem Value="0" Text="Thông Thường"></asp:ListItem>
							                    <asp:ListItem Value="1" Text="Nổi bật mục"></asp:ListItem>
							                    <asp:ListItem Value="2" Text="Nổi bật trang chủ"></asp:ListItem>
                                              </asp:DropDownList>
									    </div>
								    </div>
							   </div>
                                <div class="span6">
                                     <div class="control-group hide">
									<label class="control-label">
									    <a class="title" onclick="chooseMedia('<%=strNewsID%>'); return false;" href="#">Chọn media:</a>
									</label>
									<div class="controls" style="width: 350px">
										<i onclick="list_remove(document.getElementById('<%=cboMedia.ClientID %>'));" class="icon-remove floatRight" style="cursor: pointer !important">&nbsp;</i>
                                        <i onclick="list_moveup(document.getElementById('<%=cboMedia.ClientID %>'));" class="icon-arrow-up cursor floatRight" style="cursor: pointer !important">&nbsp;</i>
                                        <i onclick="list_movedown(document.getElementById('<%=cboMedia.ClientID %>'));" class="icon-arrow-down floatRight" style="cursor: pointer !important">&nbsp;</i>
                                        <asp:ListBox ID="cboMedia" runat="server" Width="300px" Height="100px" />
									</div>
								</div>
                                </div>
							    </div>
							   
                                <div class="span12">
                                    <div class="span4"  style="margin-left: -40px">
                                    <div class="control-group">
									    <label class="control-label">Từ khóa(SEO)</label>
									    <div class="controls">
										    <asp:TextBox ID="txtSource" runat="server" TextMode="MultiLine" CssClass="m-wrap large" Rows="3"></asp:TextBox>
									    </div>
								    </div>
                                    <div class="control-group">
									    <label class="control-label">Tag</label>
									    <div class="controls">
										    <asp:TextBox ID="txtIcon" runat="server"  TextMode="MultiLine" CssClass="m-wrap large"></asp:TextBox>
									    </div>
								    </div>
                                </div>
                                <div class="span4">
                                      <div class="control-group">
									    <label class="control-label">
									        <a class="title"  onclick="chooseNews(); return false;" href="#">Chọn bài liên quan:</a>
									    </label>
									    <div class="controls" style="width: 350px">
										    <i onclick="list_remove(document.getElementById('<%=cboNews.ClientID %>'));" class="icon-remove floatRight" style="cursor: pointer !important">&nbsp;</i>
                                            <i onclick="list_moveup(document.getElementById('<%=cboNews.ClientID %>'));" class="icon-arrow-up cursor floatRight" style="cursor: pointer !important">&nbsp;</i>
                                            <i onclick="list_movedown(document.getElementById('<%=cboNews.ClientID %>'));" class="icon-arrow-down floatRight" style="cursor: pointer !important">&nbsp;</i>
                                            <asp:ListBox ID="cboNews" runat="server" Width="300px" Height="100px"  />
									    </div>
								    </div>
                                </div>
                                </div>
                                
                                <div class="span12">
                                    

                                <div class="control-group">
									<label class="control-label" style="width: 140px">
									    Nội dung chi tiết
									</label>
									<div class="controls" style="margin-left: 150px">
										<CKEditor:CKEditorControl FilebrowserBrowseUrl="/FileManager/index.html" BasePath="/ckeditor/" runat="server" Width="800px" Height="500px" ID="NewsContent" runat="server" />
									</div>
								</div>
                                     <div class="control-group">
									<label class="control-label" style="width: 140px">
									    Ngày xuất bản
									</label>
									<div class="controls" style="margin-left: 150px">
										 <asp:DropDownList ID="cboYear" runat="server" Width="120px">
                                        <asp:ListItem Text="Chọn năm"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="cboMonth" runat="server"  Width="120px">
                                        <asp:ListItem Text="Chọn tháng"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="cboDay" runat="server"  Width="120px">
                                        <asp:ListItem Text="Chọn ngày"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="cboHour" runat="server"  Width="120px">
                                        <asp:ListItem Text="Chọn giờ"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="cboMinute" runat="server"  Width="120px">
                                        <asp:ListItem Text="Chọn phút"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="cboSercond" runat="server"  Width="120px">
                                        <asp:ListItem Text="Chọn giây"></asp:ListItem>
                                    </asp:DropDownList>
									</div>
								</div>
                                </div>
                               
                              
							</div>
                        </div>
                    </div>

                    <div class="dataTables_wrapper form-inline" role="grid">
                        <div class="form floatleftallchild">
                            <asp:Button ID="btnUpdate" Text="Lưu lại" runat="server" OnClientClick="return Validate();"
                                OnClick="btnUpdate_Click" CssClass="btnUpdate"></asp:Button>
                            <asp:Button ID="btnUpdated" Text="Lưu lại" runat="server" OnClientClick="return Validate();"
                                OnClick="btnUpdated_Click" CssClass="btnUpdate"></asp:Button>
                            <asp:Button ID="btnSend" Text="Gửi thẳng" runat="server" OnClick="btnSend_Click"
                                OnClientClick="return Validate();" CssClass="btnUpdate" Visible="False" ></asp:Button>
                            <asp:Button ID="btnPublish" Text="Xuất bản" runat="server" OnClick="btnPublish_Click"
                                OnClientClick="return Validate();" CssClass="btnUpdate"></asp:Button>
                            <img src="/images/blank.gif" class="break1" />
                        </div>
                    </div>
                  
                   
                </div>
            </div>
           
        </div>
    </div>
    <!-- END PAGE CONTENT -->
</div>
<div style="float: left; width: 775px; display: none">
                                 <asp:CheckBox ID='chkShowComment' class="ms-input" runat="server" Checked="true" />
                                <div class="t1">
                                Ảnh to
                                </div>
                                &nbsp;
                                <img src="/images/icons/folder.gif" onclick="chooseFile('icon', '<%=txtIcon.ClientID %>')"
                                    style="cursor: pointer;" />
                                <img src="/images/blank.gif" class="break" />
                                <div class="t1">
                                    Chú thích ảnh
                                </div>
                                <asp:TextBox ID="txtImageTitle" CssClass="w1" runat="server"></asp:TextBox>
                                <img src="/images/blank.gif" class="break" />
                                    <img src="/images/blank.gif" class="break" />
                                <div class="t1">
                                    Tiêu đề nhỏ
                                </div>
                                <asp:TextBox ID="txtSubTitle" runat="server" CssClass="w1"></asp:TextBox>
                                <div style="float: left; width: 540px">
                                    <div class="t1" style="display: none">
                                        Copy sang WAP
                                    </div>
                                    <div style="position: relative; display: none">
                                        <%--<asp:CheckBoxList ID="lstNewsLetter" runat="server" DataTextField="Name" DataValueField="ID" RepeatDirection="Horizontal" RepeatColumns="2" Visible="false">
                                          </asp:CheckBoxList>--%><asp:CheckBox ID="chkCopyToWap" runat="server" />
                                    </div>
                                    <img src="/images/blank.gif" style="display: none" class="break" />
                                    <div class="t1">
                                        Link gốc
                                    </div>
                                    <asp:TextBox ID="txtSourceLink" runat="server" CssClass="w1"></asp:TextBox>
                                    <img style="display: none" src="/images/blank.gif" class="break" />
                                    <div class="t1" style="display: none">
                                        Chọn mã CP
                                    </div>
                                    <asp:TextBox Visible="false" ID="txtMaCP" runat="server" CssClass="w1"></asp:TextBox>
                                    <img src="/images/blank.gif" class="break" />
                                    <div class="t1" style="display: none">
                                        Source:
                                    </div>
                                    
                                    <img src="/images/blank.gif" style="display: none" class="break" />
                                    <div class="t1" style="display: none">
                                        Tỉnh thành:
                                    </div>
                                    <asp:DropDownList Visible="false" ID="ddlProvinces" runat="server" DataTextField="ProvinceName"
                                        DataValueField="ProvinceID">
                                    </asp:DropDownList>
                                    <table cellpadding="2" cellspacing="2" border="0">
                                        <tr style="display: none">
                                            <td>
                                                Loại tin
                                            </td>
                                            <td>
                                                
                                            </td>
                                        </tr>
                                    </table>
                                    <img src="/images/blank.gif" class="break" />
                                    <div class="t1">
                                        Tác giả
                                    </div>
                                    <asp:DropDownList ID="ddlAuthor" runat="server" DataTextField="TenTacGia" DataValueField="TacGia_ID">
                                    </asp:DropDownList>
                                    <span style="width: 10px">&nbsp;</span> <span style="width: 10px">&nbsp;</span>
                                    <div style="float: left">
                                        <span>Tin tiêu điểm</span>
                                        
                                        <span style="width: 10px">&nbsp;</span> <span style="">Tin nhạy cảm</span>
                                        <asp:CheckBox ID='chkShowRate' class="ms-input" runat="server" />
                                    </div>
                                    <div style="float: left; display: none">
                                        <fieldset>
                                            <legend>Loại file đính kèm</legend>
                                            <asp:CheckBoxList ID="cblFileType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table"
                                                RepeatColumns="10">
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
                                 <img src="/images/blank.gif" class="break" />
                            <div class="t1">
                                &nbsp;
                            </div>
                            <fieldset style="width: 770px">
                                <legend>Cấu hình</legend>
                                <div class="chonbailienquan">
                                    
                                    
                                    <a class="title" style="float: left;" onclick="chooseThreadV2(); return false;" href="#">
                                        Chọn luồng sự kiện:</a> <a style="float: right;" href="#" onclick="list_remove(document.getElementById('<%=lstThread.ClientID %>')); return false;">
                                            <img src="/images/delete.gif" /></a>
                                    <asp:ListBox ID="lstThread" runat="server" CssClass="t5" Width="430px" Height="80px"
                                        DataTextField="Title" DataValueField="Thread_ID"></asp:ListBox>
                                    <br />
                                
                                </div>
                                <div class="chonmedialienquan">
                                    <div style="float: right; width: 285px; padding-right: 20px;">
                                        <div class="t3" style="padding-bottom: 5px;">
                                            <b>Chọn chuyên mục khác</b>
                                        </div>
                                        <div class="t3 CheckBoxList" style="width: 280px;">
                                            <asp:CheckBoxList ID="lstOtherCat" runat="server" Width="180px">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                    <div style="padding-left: 15px; padding-top: 10px;">
                                        <span style="color: Red; font-weight: 700;">Tags - Luồng sự kiện nổi bật</span>
                                        <br />
                                        <br />
                                        <asp:CheckBoxList ID="cblTags" runat="server" Width="180px">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                                
                            </fieldset>
                            <img src="/images/blank.gif" class="break1" />
                            <div class="t1">
                                &nbsp;
                            </div>
                            </div>
<asp:HiddenField ID="hdMedia" runat="server" />
<div style="display: none;">
    <asp:HiddenField ID="hidLuongSuKien" runat="server" />
    <asp:HiddenField ID="hdRelatNews" runat="server" />
    
    <asp:HiddenField ID="hidNewsID" runat="server" />
    <asp:HiddenField ID="hdTag" runat="server" />
    <asp:TextBox ID="txtExtension2" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtExtension3" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtExtension4" runat="server"></asp:TextBox>
    <asp:ListBox ID="cboTag" runat="server" Width="300px" Height="100px" />
</div>
<asp:ObjectDataSource OnUpdated="objsoure_Updated" OnInserted="objsoure_Inserted"
    ID="objsoure" InsertMethod="CreateNews" UpdateMethod="UpdateNews" runat="server"
    TypeName="DFISYS.BO.Editoral.Newsedit.NewsEditHelper">
    <InsertParameters>
        <asp:Parameter Name="_news_id" Type="Int64" />
        <asp:ControlParameter ControlID="lstCat" DefaultValue="1" Name="_cat_id" PropertyName="SelectedValue"
            Type="Int32" />
        <asp:ControlParameter ControlID="txtSubTitle" DefaultValue="" Name="_news_subtitle"
            PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="txtTitle" Name="_news_title" PropertyName="Text"
            Type="String" />
        <asp:Parameter Name="_news_image" Type="string" />
        <asp:ControlParameter Name="_news_source" Type="String" ControlID="txtSource" PropertyName="Text" />
        <asp:ControlParameter ControlID="txtInit" DefaultValue="" Name="_news_init" PropertyName="Text"
            Type="String" />
        <asp:ControlParameter Name="_news_content" Type="String" ControlID="NewsContent"
            PropertyName="Text" />
        <asp:Parameter DefaultValue="" Name="_poster" Type="String" />
        <asp:ControlParameter ControlID="chkIsFocus" Name="_news_isfocus" PropertyName="Checked"
            Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="_news_status" Type="Int32" />
        <asp:ControlParameter ControlID="cboIsHot" Name="_news_type" PropertyName="SelectedValue"
            Type="Int32" />
        <asp:ControlParameter ControlID="hdRelatNews" Name="_related_news" PropertyName="Value"
            Type="string" />
        <asp:ControlParameter ControlID="hdMedia" Name="_obj_media" PropertyName="Value"
            Type="String" />
        <asp:Parameter Name="_other_cat" Type="string" />
        <asp:Parameter Name="_switchtime" Type="dateTime" />
        <asp:ControlParameter ControlID="chkShowComment" Name="_isShowComment" PropertyName="Checked"
            Type="boolean" />
        <asp:ControlParameter ControlID="chkShowRate" Name="_isShowRate" PropertyName="Checked"
            Type="boolean" />
        <asp:ControlParameter ControlID="ddlProvinces" Name="_template" PropertyName="SelectedValue"
            Type="Int32" />
        <asp:ControlParameter ControlID="txtImageTitle" Name="_news_title_image" PropertyName="Text"
            Type="String" />
        <asp:ControlParameter ControlID="txtIcon" Name="_news_icon" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="hidLuongSuKien" Name="_thread_id" PropertyName="Value"
            Type="String" />
        <asp:ControlParameter ControlID="chkNoiBatNhat" Name="_isNoiBatNhat" PropertyName="Checked"
            Type="boolean" />
        <asp:ControlParameter ControlID="txtMaCP" Name="_str_Extension1" PropertyName="Text"
            Type="String" />
        <asp:ControlParameter ControlID="txtExtension2" Name="_str_Extension2" PropertyName="Text"
            Type="String" />
        <asp:ControlParameter ControlID="txtSourceLink" Name="_str_Extension3" PropertyName="Text"
            Type="String" />
        <asp:ControlParameter ControlID="ddlAuthor" Name="_str_Extension4" PropertyName="SelectedValue"
            Type="int32" />
        <asp:ControlParameter ControlID="hdTag" Name="_tag_id" PropertyName="Value" Type="String" />
        <asp:SessionParameter Name="_newsTemp_id" SessionField="strTempNewsID" Type="string" />
    </InsertParameters>
    <UpdateParameters>
        <asp:QueryStringParameter Name="_news_id" QueryStringField="NewsRef" Type="Int64" />
        <asp:ControlParameter ControlID="lstCat" Name="_cat_id" PropertyName="SelectedValue"
            Type="Int32" />
        <asp:ControlParameter ControlID="txtSubTitle" Name="_news_subtitle" PropertyName="Text"
            Type="String" />
        <asp:ControlParameter ControlID="txtTitle" Name="_news_title" PropertyName="Text"
            Type="String" />
        <asp:Parameter Name="_news_image" Type="string" />
        <asp:ControlParameter Name="_news_source" Type="String" ControlID="txtSource" PropertyName="Text" />
        <asp:ControlParameter ControlID="txtInit" DefaultValue="" Name="_news_init" PropertyName="Text"
            Type="String" />
        <asp:ControlParameter Name="_news_content" Type="String" ControlID="NewsContent"
            PropertyName="Text" />
        <asp:ControlParameter ControlID="chkIsFocus" Name="_news_isfocus" PropertyName="Checked"
            Type="Boolean" />
        <asp:Parameter DefaultValue="0" Name="_news_status" Type="Int32" />
        <asp:ControlParameter ControlID="cboIsHot" Name="_news_type" PropertyName="SelectedValue"
            Type="Int32" />
        <asp:ControlParameter ControlID="hdRelatNews" Name="_related_news" PropertyName="Value"
            Type="string" />
        <asp:Parameter Name="_other_cat" Type="string" />
        <asp:Parameter Name="_switchtime" Type="dateTime" DefaultValue="01/01/2000" />
        <asp:Parameter Name="_isSend" Type="boolean" DefaultValue="false" />
        <asp:ControlParameter ControlID="chkShowComment" Name="_isShowComment" PropertyName="Checked"
            Type="boolean" />
        <asp:ControlParameter ControlID="chkShowRate" Name="_isShowRate" PropertyName="Checked"
            Type="boolean" />
        <asp:ControlParameter ControlID="ddlProvinces" Name="_template" PropertyName="SelectedValue"
            Type="Int32" />
        <asp:ControlParameter ControlID="hdMedia" Name="_obj_media" PropertyName="Value"
            Type="String" />
        <asp:ControlParameter ControlID="txtImageTitle" Name="_news_title_image" PropertyName="Text"
            Type="String" />
        <asp:ControlParameter ControlID="txtIcon" Name="_news_icon" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="hidLuongSuKien" Name="_thread_id" PropertyName="Value"
            Type="String" />
        <asp:ControlParameter ControlID="chkNoiBatNhat" Name="_isNoiBatNhat" PropertyName="Checked"
            Type="boolean" />
        <asp:ControlParameter ControlID="txtMaCP" Name="_str_Extension1" PropertyName="Text"
            Type="String" />
        <asp:ControlParameter ControlID="txtExtension2" Name="_str_Extension2" PropertyName="Text"
            Type="String" />
        <asp:ControlParameter ControlID="txtSourceLink" Name="_str_Extension3" PropertyName="Text"
            Type="String" />
        <asp:ControlParameter ControlID="ddlAuthor" Name="_str_Extension4" PropertyName="SelectedValue"
            Type="int32" />
        <asp:ControlParameter ControlID="hdTag" Name="_tag_id" PropertyName="Value" Type="String" />
        <asp:SessionParameter Name="_newsTemp_id" SessionField="strTempNewsID" Type="string" />
    </UpdateParameters>
</asp:ObjectDataSource>
<script language="javascript">
   

    var prefix = '<% = ClientID %>'; var cpmode = '<% = Request.QueryString["cpmode"] %>';
    //var obj = oUtil.obj;
    //var editor = oUtil.obj;

    //function insertMultipleImage_loadValue(arrImagesURL) {
    //    var html = '';
    //    for (var i = 0; i < arrImagesURL.length; i++) {
    //        html += '<div style="text-align: center;"><img border="0" src="' + arrImagesURL[i] + '" /></div><br />';
    //    }
    //    //alert(html);
    //    oUtil.obj.insertHTML(html);
        

    //}
    setTimeout(function () {
        $("#<% = txtTitle.ClientID %>").focus();
    }, 1000);

</script>
