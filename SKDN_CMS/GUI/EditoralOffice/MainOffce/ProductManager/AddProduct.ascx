<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.ProductManager.AddProduct" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<script language="javascript" type="text/javascript" src="/Scripts/Newsedit.js?ver=1"></script>
<link rel="stylesheet" type="text/css" href="/Styles/Newsedit.css?date=1806" />

<style>
    table.radioProductType{}
    table.radioProductType label{display: inline-block}
    table.radioProductType .radio input[type="radio"]{margin-left: 0}
    div.checker{float:left}
    .CheckBoxList{ width: 250px;}
    .CheckBoxList label{font-size: 12px; color: #555555}
</style>
<div class="container-fluid">
				<!-- BEGIN PAGE HEADER-->   
				<div class="row-fluid">
					<div class="span12">
						<h3 class="page-title">
							Project manager <small>Quản lý dự án</small>
						</h3>
					</div>
				</div>
				<!-- END PAGE HEADER-->
				<!-- BEGIN PAGE CONTENT-->
				<div class="row-fluid">
					<div class="span12">
						<!-- BEGIN SAMPLE FORM PORTLET-->   
						<div class="portlet box blue tabbable">
							<div class="portlet-title">
								<div class="caption">
									<i class="icon-reorder"></i>
									<span class="hidden-480">Project detail</span>
								</div>
							</div>
							<div class="portlet-body form">
								<div class="tabbable portlet-tabs">
									<div class="tab-content">
										<div class="tab-pane active form-horizontal">
										    <div class="span12">
										        <div class="control-group">
													<label class="control-label">Tên dự án</label>
													<div class="controls">
													    <input runat="server" id="txt_Name" type="text"  class="m-wrap larger" />
													</div>
												</div>
												<div class="control-group">
													<label class="control-label">Mã dự án</label>
													<div class="controls">
													    <input runat="server" id="txt_Name_En" type="text"  class="m-wrap large" />
													</div>
												</div>
                                                <div class="control-group">
													<label class="control-label">Chủ đề</label>
													<div class="controls">
													    <asp:DropDownList runat="server" ID="ddlCategory" CssClass="medium m-wrap"></asp:DropDownList>
													</div>
												</div>
                                                  <div class="control-group">
													<label class="control-label">Ảnh đại diện</label>
													<div class="controls">
														<input ID="txtSelectedFile" runat="server" class="m-wrap larger"></input>&nbsp;
                                                        <img src="/images/icons/folder.gif" onclick="openInfo('/FileManager/index.html?field_name=<%=txtSelectedFile.ClientID %>',900,700)" style="cursor: pointer; padding: 0px 3px" />
                                                        
													</div>
												</div>
                                                <div class="control-group hidden">
													<label class="control-label">Loại dự án</label>
													<div class="controls">
														<asp:RadioButtonList Width="100%" CssClass="radioProductType"  ID="productType" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
														    <asp:ListItem Value="0" Selected="True">Thông thường</asp:ListItem>
                                                            <asp:ListItem Value="1">Tiêu biểu</asp:ListItem>
														</asp:RadioButtonList>
													</div>
												</div>
                                                <div class="control-group">
													<label class="control-label">Active</label>
													<div class="controls">
														 <asp:CheckBox ID="cb_IsActive"  runat="server" Checked="true" />
													</div>
												</div>
                                                <div class="control-group hidden">
													<label class="control-label">Tags</label>
													<div class="controls">
														<input type="text" runat="server" id="txt_tags" class="large m-wrap"/>
													</div>
												</div>
                                                <div class="control-group hidden">
													<label class="control-label">Giá sản phẩm</label>
                                                    <div class="controls">
                                                        <div class="input-prepend">
													    <span class="add-on">VND</span><input runat="server" id="txt_Cost" type="text" class="m-wrap medium" />
													</div>
                                                    </div>
													
												</div>
												<div class="control-group hidden">
													<label class="control-label">Hot line</label>
													<div class="controls">
														<textarea runat="server" id="txt_Summary" class="large m-wrap" rows="1"></textarea>
													</div>
												</div>
                                                
                                               
                                              
										    </div>
                                            
											<div class="span12" style="margin-left: 0">
											    <div class="control-group">
													<label class="control-label">Mô tả dự án</label>
													<div class="controls">
                                                        <CKEditor:CKEditorControl FilebrowserBrowseUrl="/FileManager/index.html" BasePath="/ckeditor/" runat="server" Width="800px"   Height="800px" ID="NewsContent"
                                                            runat="server" />
                                                        </div>
													</div>
											    <div class="control-group hidden">
													<label class="control-label">Mô tả</label>
													<div class="controls">
														
                                                         <CKEditor:CKEditorControl FilebrowserBrowseUrl="/FileManager/index.html" BasePath="/ckeditor/" runat="server" Width="800px" ID="txt_Video"
                                                            runat="server" />
													</div>
												</div>
											    
                                                <div class="control-group hidden">
                                                            <label class="control-label">Thông số kỹ thuật</label>
													      <div class="controls">
														    <div id="editors">
                                                        <CKEditor:CKEditorControl FilebrowserBrowseUrl="/FileManager/index.html" BasePath="/ckeditor/" runat="server" Width="800px" ID="NewsContent_En"
                                                                    runat="server" />
                                                            </div>
													  </div>
												</div> 
												<div class="form-actions">
												    <asp:LinkButton runat="server" CssClass="btn blue" ID="btnSave" OnClientClick="return ValidateProduct()"
                                                        onclick="btnSave_Click1"><i class="icon-ok"></i> Save</asp:LinkButton>
												
													<button type="reset" class="btn">Cancel</button>
												</div>
											</div>
												
											
										</div>
										
									</div>
								</div>
							</div>
						</div>
						<!-- END SAMPLE FORM PORTLET-->
					</div>
				</div>
				<!-- END PAGE CONTENT-->         
			</div>

<div class="hidden">
    
    <div class="control-group">
		<label class="control-label">Gift type</label>
		<div class="controls">
			<asp:DropDownList runat="server" ID="ddlGift" CssClass="medium m-wrap"></asp:DropDownList>
		</div>
	</div>
    <div class="control-group">
		<label class="control-label">Video</label>
		<div class="controls">
			<textarea runat="server" id="txt_Sum_En" class="large m-wrap" rows="3"></textarea>
		</div>
	</div>
    <div class="control-group ">
		<label class="control-label">Other category</label>
		<div class="controls CheckBoxList">
			<asp:CheckBoxList ID="lstOtherCat" runat="server" ></asp:CheckBoxList>
		</div>
	</div>
                                               
        <div class="control-group">
		<label class="control-label">Image size</label>
		<div class="controls">
			<asp:DropDownList runat="server" ID="ddlLayout" CssClass="large m-wrap">
				<asp:ListItem Value="1">Small</asp:ListItem>
                <asp:ListItem Value="2">Medium</asp:ListItem>
                <asp:ListItem Value="2">Lager</asp:ListItem>
			</asp:DropDownList>
		</div>
	</div>
                                             
    <div class="control-group">
		<label class="control-label">
				<a class="title" onclick="chooseMedia(0,'<%=(ID > 0 ? ID.ToString() : tmpID)%>'); return false;" href="javascript:void(0)">Chọn media liên quan:</a>
		</label>
        <div class="controls">
                <asp:ListBox ID="cboMedia" CssClass="floatLeft" runat="server" Width="300px" Height="100px" />
                <i onclick="list_moveup(document.getElementById('<%=cboMedia.ClientID %>'));" class="icon-arrow-up cursor floatLeft" style="cursor: pointer !important">&nbsp;</i>
                <i onclick="list_movedown(document.getElementById('<%=cboMedia.ClientID %>'));" class="icon-arrow-down floatLeft" style="cursor: pointer !important">&nbsp;</i>
                <i onclick="list_remove(document.getElementById('<%=cboMedia.ClientID %>'));" class="icon-remove floatLeft" style="cursor: pointer !important">&nbsp;</i>
                                                        
        </div>
													
	</div>
</div>
            <asp:HiddenField ID="hdMedia" runat="server" />
            <script language="javascript" type="text/javascript">
                var prefix = '<% = ClientID %>';
                var obj = oUtil.obj;
                var editor = oUtil.obj;
//                function insertMultipleImage_loadValue(arrImagesURL) {
//                    var html = '';
//                    for (var i = 0; i < arrImagesURL.length; i++) {
//                        html += '<div style="text-align: center;"><img border="0" src="' + arrImagesURL[i] + '" /></div><br />';
//                    }
//                    //alert(html);
//                    oUtil.obj.insertHTML(html);


//                }
                setTimeout(function () {
                    $("#<% = txt_Name.ClientID %>").focus();
                }, 1000);

                function ValidateProduct() {

                    if ($("#<%=ddlCategory.ClientID %> option:selected").val() == 0) {
                        alert("You must select a category");
                     
                        return false;
                    }
                    if (isNaN($("#<%=txt_Cost.ClientID %>").val())) {
                        alert("Cost must be number");
                        $("#<% = txt_Cost.ClientID %>").focus();
                        return false;
                    }

                    $("#<%=hdMedia.ClientID %>").val(ListBoxToString(server_getElementById('cboMedia')));
                    return true;
                }
</script>