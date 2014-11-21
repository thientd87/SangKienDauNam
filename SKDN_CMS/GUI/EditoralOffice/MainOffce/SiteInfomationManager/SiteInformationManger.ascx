<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteInformationManger.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.SiteInfomationManager.SiteInformationManger" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register TagPrefix="editor" Namespace="InnovaStudio" Assembly="WYSIWYGEditor" %>
<div class="container-fluid">
				<!-- BEGIN PAGE HEADER-->   
				<div class="row-fluid">
					<div class="span12">
						<h3 class="page-title">
							Site infomation <small>Thông tin website</small>
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
									<span class="hidden-480">Site infomation detail</span>
								</div>
							</div>
							<div class="portlet-body form">
								<div class="tabbable portlet-tabs">
									<div class="tab-content">
										<div class="tab-pane active form-horizontal">
										        <div class="control-group">
													<label class="control-label">Site name</label>
													<div class="controls">
													    <asp:TextBox ID="txt_name" CssClass="m-wrap larger" Width="600px" ClientIDMode="Static" runat="server"></asp:TextBox>
													</div>
												</div>
												<div class="control-group">
													<label class="control-label">Description</label>
													<div class="controls">
														<asp:TextBox ID="txt_description"  ClientIDMode="Static" runat="server" Rows="8" CssClass="m-wrap larger" TextMode="MultiLine" Width="600px"></asp:TextBox>
													</div>
												</div>
                                                <div class="control-group">
													<label class="control-label">Keyword</label>
													<div class="controls">
														<asp:TextBox ID="txt_keyword" ClientIDMode="Static"  runat="server"  Rows="8" CssClass="m-wrap larger" TextMode="MultiLine" Width="600px"></asp:TextBox>
													</div>
												</div>
                                                <div class="control-group">
													<label class="control-label">Address</label>
													<div class="controls">
														<asp:TextBox ID="txt_address" ClientIDMode="Static"  runat="server"  Rows="8" CssClass="m-wrap larger" TextMode="MultiLine" Width="600px"></asp:TextBox>
													</div>
												</div>
                                             <div class="control-group">
													<label class="control-label">Footer content</label>
													<div class="controls">
														<CKEditor:CKEditorControl FilebrowserBrowseUrl="/FileManager/index.html" ID="txt_Footer" BasePath="/ckeditor/" runat="server" Width="600px"></CKEditor:CKEditorControl>
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