<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AboutUs.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.AboutUs.AboutUs" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<div class="container-fluid">
				<!-- BEGIN PAGE HEADER-->   
				<div class="row-fluid">
					<div class="span12">
						<h3 class="page-title">
							About Us <small>Thông tin giới thiệu</small>
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
									<span class="hidden-480">About us detail</span>
								</div>
							</div>
							<div class="portlet-body form">
								<div class="tabbable portlet-tabs">
									<div class="tab-content">
										<div class="tab-pane active form-horizontal">
										        <div class="control-group">
													<label class="control-label">Chúng tôi</label>
													<div class="controls">
													    <CKEditor:CKEditorControl FilebrowserBrowseUrl="/FileManager/index.html" ID="txtAboutUs" BasePath="/ckeditor/" runat="server" Width="600px"></CKEditor:CKEditorControl>
													</div>
												</div>
                                                <div class="control-group">
									                <label class="control-label">Ảnh chúng tôi</label>
									                <div class="controls">
										                 <asp:TextBox ID="txtAboutUsImage" runat="server" class="m-wrap larger"></asp:TextBox>&nbsp;
                                                         <img src="/images/icons/folder.gif" onclick="openInfo('/FileManager/index.html?field_name=<%=txtAboutUsImage.ClientID %>',900,700)" style="cursor: pointer; padding: 0px 3px" />
									                </div>
								                </div>
												<div class="control-group">
													<label class="control-label">Nhà tài trợ</label>
													<div class="controls">
														<CKEditor:CKEditorControl FilebrowserBrowseUrl="/FileManager/index.html" ID="txtSponsor" BasePath="/ckeditor/" runat="server" Width="600px"></CKEditor:CKEditorControl>
													</div>
												</div>
                                                <div class="control-group">
									                <label class="control-label">Ảnh nhà tài trợ</label>
									                <div class="controls">
										                 <asp:TextBox ID="txtSponsorImage" runat="server" class="m-wrap larger"></asp:TextBox>&nbsp;
                                                         <img src="/images/icons/folder.gif" onclick="openInfo('/FileManager/index.html?field_name=<%=txtSponsorImage.ClientID %>',900,700)" style="cursor: pointer; padding: 0px 3px" />
									                </div>
								                </div>
                                                <div class="control-group">
													<label class="control-label">Sứ mệnh</label>
													<div class="controls">
														<CKEditor:CKEditorControl FilebrowserBrowseUrl="/FileManager/index.html" ID="txtMission" BasePath="/ckeditor/" runat="server" Width="600px"></CKEditor:CKEditorControl>
													</div>
												</div>
                                                <div class="control-group">
									                <label class="control-label">Ảnh sứ mệnh</label>
									                <div class="controls">
										                 <asp:TextBox ID="txtMissionImage" runat="server" class="m-wrap larger"></asp:TextBox>&nbsp;
                                                         <img src="/images/icons/folder.gif" onclick="openInfo('/FileManager/index.html?field_name=<%=txtMissionImage.ClientID %>',900,700)" style="cursor: pointer; padding: 0px 3px" />
									                </div>
								                </div>
                                            <div class="control-group">
													<label class="control-label">Hướng dẫn đăng ký</label>
													<div class="controls">
														<CKEditor:CKEditorControl FilebrowserBrowseUrl="/FileManager/index.html" ID="txtHuongDanDangKy" BasePath="/ckeditor/" runat="server" Width="600px"></CKEditor:CKEditorControl>
													</div>
												</div>
											<div class="span12" style="margin-left: 0">
												<div class="form-actions">
												    <asp:LinkButton runat="server" CssClass="btn blue" ID="btnSave" OnClick="btnSave_Click"><i class="icon-ok"></i> Save</asp:LinkButton>
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
          
 <script type="text/javascript">

     jQuery(document).ready(function () {

     });

   </script>