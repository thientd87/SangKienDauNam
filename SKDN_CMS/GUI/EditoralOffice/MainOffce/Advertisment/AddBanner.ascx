<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddBanner.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.Advertisment.AddBanner" %>
<script language="javascript" type="text/javascript" src="/Scripts/Newsedit.js?ver=1"></script>
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
							Banner manager <small>Quản lý banner</small>
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
									<span class="hidden-480">Banner detail</span>
								</div>
							</div>
							<div class="portlet-body form">
								<div class="tabbable portlet-tabs">
									<div class="tab-content">
										<div class="tab-pane active form-horizontal">
										    <div class="span12 hide">
										        <div style="float:left">
                                                   <label>Trang</label>
                                                    
                                                </div>
                                                <div style="float:left;margin-left:15px">
                                                    <span class="left">Vị trí</span>
                                                    <asp:DropDownList ID="ddlPos" runat="server" DataTextField="PosName" DataValueField="PosID" CssClass="ml20">
                                                    </asp:DropDownList>
                                                </div>
                                                <div>
                                                    <label>Loại quảng cáo</label>
                                                        <asp:RadioButtonList ID="adv_type" ClientIDMode="Static" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                                            <asp:ListItem Selected="True" Value="1">Ảnh</asp:ListItem>
                                                            <asp:ListItem Value="2">Flash</asp:ListItem>
                                                            <asp:ListItem Value="3">Box nhúng</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                </div>
                                                <label>Tự động chuyển</label><asp:CheckBox ID="adv_isRotate" ClientIDMode="Static" runat="server" Checked="false" />
										    </div>
										        <div class="control-group hide">
													<label class="control-label">Trang</label>
													<div class="controls CheckBoxList">
													    <asp:CheckBoxList ID="cblPages" runat="server" DataTextField="Cat_Name" DataValueField="Cat_ID">
                                                    </asp:CheckBoxList>
													</div>
												</div>
										        <div class="control-group">
													<label class="control-label">Banner name</label>
													<div class="controls">
													    <asp:TextBox ID="adv_name" CssClass="m-wrap large" ClientIDMode="Static" runat="server"></asp:TextBox>
													</div>
												</div>
												<div class="control-group">
													 <div class="control-group">
													<label class="control-label">Image</label>
													<div class="controls">
														<input ID="txtSelectedFile" runat="server" class="m-wrap large"></input>&nbsp;
                                                        <img src="/images/icons/folder.gif"onclick="openInfo('/FileManager/index.html?field_name=<%=txtSelectedFile.ClientID %>',900,700)" style="cursor: pointer; padding: 0px 3px" />
                                                        <%--<img src="/images/img_preview.png" class="tooltips" id="imgPreview" style="cursor: pointer;" data-original-title="Image preview" />--%>
													</div>
												</div>
												</div>
                                                <div class="control-group">
													<label class="control-label">Link</label>
                                                    <div class="controls">
                                                        <input ID="txtIcon" runat="server" class="m-wrap large"></input>
													</div>
                                                 </div>
												<div class="control-group">
													<label class="control-label">Description</label>
													<div class="controls">
														<asp:TextBox ID="adv_description"  ClientIDMode="Static" runat="server" Rows="6" CssClass="m-wrap large" TextMode="MultiLine"></asp:TextBox>
													</div>
												</div>
												<div class="control-group">
													<label class="control-label">Date range</label>
                                                    <div class="controls">
                                                        <input class="m-wrap small" runat="server" size="16" type="text" ID="ui_date_picker_range_from" ClientIDMode="Static"/>
											            <span class="text-inline">&nbsp;to&nbsp;</span>
                                                        <input class="m-wrap small" runat="server" size="16" type="text" ID="ui_date_picker_range_to" ClientIDMode="Static"/>
										            </div>
												</div>
										   
                                                <div class="control-group">
													<label class="control-label">Order</label>
													<div class="controls">
														<asp:TextBox ID="adv_order" ClientIDMode="Static"  runat="server" Text="0"></asp:TextBox>
													</div>
												</div>
                                                <div class="control-group">
													<label class="control-label">Active</label>
													<div class="controls">
														 <asp:CheckBox ID="adv_isActive" ClientIDMode="Static"  runat="server" Checked="true" />
													</div>
												</div>
                                               
											<div class="span12" style="margin-left: 0">
											    
                                                     
												<div class="form-actions">
												    <asp:LinkButton runat="server" CssClass="btn blue" ID="btnSave" 
                                                        OnClientClick="return ValidateProduct()" onclick="btnSave_Click"><i class="icon-ok"></i> Save</asp:LinkButton>
												
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
            <script language="javascript" type="text/javascript">
                var prefix = '<% = ClientID %>';
               
</script>
 <script>

     jQuery(document).ready(function () {
         $("#ui_date_picker_range_from").datepicker({
            // isRTL: App.isRTL(),
             defaultDate: "+1w",
             changeMonth: true,
             numberOfMonths: 2,
             onClose: function (selectedDate) {
                 $("#ui_date_picker_range_to").datepicker("option", "minDate", selectedDate);
             }
         });
         $("#ui_date_picker_range_to").datepicker({
            // isRTL: App.isRTL(),
             defaultDate: "+1w",
             changeMonth: true,
             numberOfMonths: 2,
             onClose: function (selectedDate) {
                 $("#ui_date_picker_range_from").datepicker("option", "maxDate", selectedDate);
             }
         });
        
     });
	
   </script>