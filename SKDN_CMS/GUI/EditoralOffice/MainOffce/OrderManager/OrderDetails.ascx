<%@ Control Language="C#" AutoEventWireup="true" Codebehind="OrderDetails.ascx.cs"
    Inherits="MobileShop.GUI.Back_End.Order.OrderDetails" %>
<div class="container-fluid">
				<!-- BEGIN PAGE HEADER-->   
				<div class="row-fluid">
					<div class="span12">
						<h3 class="page-title">
							Order manager <small> Chi tiết đơn hàng</small>
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
									<span class="hidden-480">Thông tin khách hàng</span>
								</div>
							</div>
							<div class="portlet-body form">
								<div class="tabbable portlet-tabs">
									<div class="tab-content">
										<div class="tab-pane active form-horizontal">
										   <div class="control-group">
													<label class="control-label">Tên khách hàng:</label>
													<div class="controls">
													   <asp:Label runat="server" ID="lblCusName" CssClass="textFocus"></asp:Label>
													</div>
												</div>
												<div class="control-group">
													    <label class="control-label">Địa chỉ:</label>
													    <div class="controls">
														     <asp:Label runat="server" ID="lblAddress" CssClass="textFocus"></asp:Label>
													    </div>
												</div>
                                                <div class="control-group">
													<label class="control-label">Email:</label>
                                                    <div class="controls">
                                                        <asp:Label runat="server" ID="lblEmail" CssClass="textFocus"></asp:Label>
													</div>
                                                 </div>
												<div class="control-group">
													<label class="control-label">Tel</label>
													<div class="controls">
														<asp:Label runat="server" ID="lblTel" CssClass="textFocus"></asp:Label>
													</div>
												</div>
												<div class="control-group">
													<label class="control-label">Thông tin khác:</label>
                                                    <div class="controls">
                                                      <asp:Label runat="server" ID="lblSuggess"></asp:Label>
										            </div>
												</div>
										   
                                                <div class="control-group">
													<label class="control-label">Ngày đặt hàng:</label>
													<div class="controls">
														<asp:Label runat="server" ID="lblOrderDate"></asp:Label>
													</div>
												</div>
                                                <div class="control-group">
													<label class="control-label"> Ngày hêt hạn:</label>
													<div class="controls">
														 <input class="m-wrap small" runat="server" size="16" type="text" ID="rdpRequiredDate" ClientIDMode="Static"/>
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
    <div class="row-fluid">
        <div class="span12">
                                                    <!-- BEGIN EXAMPLE TABLE PORTLET-->
                                                    <div class="portlet box blue">
                                                        <div class="portlet-title">
                                                            <div class="caption">
                                                                <i class="icon-edit"></i>Chi tiết đơn hàng</div>
                                                            <div class="tools">
                                                                <a href="javascript:;" class="collapse"></a><a href="javascript:location.reload();" class="reload">
                                                                </a>
                                                            </div>
                                                        </div>
                                                        <div class="portlet-body">
                                                            <div class="dataTables_wrapper form-inline" role="grid">
                                                                <asp:GridView runat="server" ID="gvCart" Width="100%"  AutoGenerateColumns="false" CssClass="table table-striped table-hover table-bordered dataTable" RowStyle-CssClass="odd" AlternatingRowStyle-CssClass="even" 
                                                                    ShowFooter="true" EmptyDataRowStyle-ForeColor="red" OnDataBound="gvCart_DataBound" OnRowCommand="gvCart_RowCommand" OnRowDeleted="gvCart_RowDeleted" OnRowDeleting="gvCart_RowDeleting">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <span class="style1">Tên hàng</span></HeaderTemplate>
                                                                           
                                                                            <ItemTemplate>
                                                                                <a class="cartProduct" style="text-transform: uppercase">
                                                                                    <%#Eval("ProductName")%>
                                                                                </a>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                &nbsp;</FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <span class="style1">Đơn giá</span> <span class="style6">(vn&#273;)</span>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle CssClass="style3" Width="25%" />
                                                                            <ItemTemplate>
                                                                                <%#Eval("gia")%>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                &nbsp;</FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <span class="style1">Số lượng</span></HeaderTemplate>
                                                                            <ItemStyle CssClass="style3" Width="25%" />
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtQuantity" Text='<%#Eval("P_quantity")%>' onblur="return checkNumber(this)" ReadOnly="true"
                                                                                    MaxLength="2" Width="20"></asp:TextBox></ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <span class="style6"><strong>Tổng cộng:</strong></span></FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemStyle CssClass="style3" />
                                                                            <HeaderTemplate>
                                                                                <span class="style1">Thành tiền <span class="style6">(vn&#273;)</span></span></HeaderTemplate>
                                                                            <ItemStyle Width="25%" HorizontalAlign="right" />
                                                                            <ItemTemplate>
                                                                                <%#Eval("gia")%>
                                                                            </ItemTemplate>
                                                                            <FooterStyle HorizontalAlign="right" />
                                                                            <FooterTemplate>
                                                                                <asp:Label runat="server" ID="lblThanhTien" CssClass="style7"></asp:Label></FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <span class="style1">Xóa</span></HeaderTemplate>
                                                                            <ItemStyle Width="5%" />
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="ibnXoa" CommandName="del" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>'
                                                                                        runat="server" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');">
                                                                             <i class="icon-trash" style="font-size: 20px"></i>
                                                                                    </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                &nbsp;</FooterTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!-- END EXAMPLE TABLE PORTLET-->
                                                </div>
    </div>
    <div class="row-fluid">
        <div class="span12">
						<div class="form-actions">
							<asp:Button ID="btnThanhToan" runat="server" Text="Xác nhận thanh toán" OnClick="btnThanhToan_Click" Visible="false" />
                            <asp:Button ID="btnUpdate" runat="server" Text="Cập nhật" OnClick="btnUpdate_Click" Visible="false"/>
                            <asp:Button ID="btnDel" runat="server" Text="Hủy đơn hàng" OnClick="btnDel_Click" Visible="false"/>
                            <a class="vc-toolbar" href="javascript:window.history.go(-1);"><input type="button" value="Quay lại" /></a>
                            <asp:Button ID="btnDuyet" runat="server" OnClick="btnDuyet_Click" Text="Duyệt đơn hàng" Visible="false"/>
						</div>
					</div>
    </div>
     
                    
				<!-- END PAGE CONTENT-->         
			</div>
<asp:HiddenField runat="server" ID="valueTotal" />
<script type="text/javascript" language="javascript">
    function checkNumber(ctl)
    {
        if(!isNumber(ctl.id,'Số lượng phải là số')) return false;
        return true;
    }
    function GoBack()
    {
        alert(window.history.go(-1));
        window.location = window.history.go(-1);
        return true;
    }
    
</script>
<style type="text/css">
    .textFocus
    {
        font-weight:bold;
        display: inline-block; padding-top: 6px
    }
</style>

<script>

    jQuery(document).ready(function () {
        $("#rdpRequiredDate").datepicker({
        });
        

    });

   </script>