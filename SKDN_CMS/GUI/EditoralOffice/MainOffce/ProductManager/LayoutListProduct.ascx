<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LayoutListProduct.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.ProductManager.LayoutListProduct" %>
  <!-- jQuery Touch Punch - Enable Touch Drag and Drop -->
  <script src="/Scripts/jquery.shapeshift/vendor/jquery.touch-punch.min.js"></script>

  <!-- jQuery.Shapeshift -->
  <script src="/Scripts/jquery.shapeshift/jquery.shapeshift.js"></script>
<div class="container-fluid">
    <!-- BEGIN PAGE HEADER-->
    <div class="row-fluid">
        <div class="span12">
            <!-- BEGIN PAGE TITLE & BREADCRUMB-->
            <h3 class="page-title">
                Product manager <small>quản lý layout sản phẩm</small>
            </h3>
            <!-- END PAGE TITLE & BREADCRUMB-->
        </div>
    </div>
    <!-- END PAGE HEADER-->
    <!-- BEGIN PAGE CONTENT-->
    <div class="row-fluid">
        <div class="span12">
            <!-- BEGIN EXAMPLE TABLE PORTLET-->
            <div class="portlet box blue">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-edit"></i>Setting layout</div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a><a href="javascript:location.reload();" class="reload">
                        </a>
                    </div>
                </div>
                <div class="portlet-body overflow-hidden">
                    <div class="span12">
                        Chọn chuyên mục : 	<asp:DropDownList runat="server" ID="cb_P_catID" AutoPostBack="true"  CssClass="ms-long" OnSelectedIndexChanged="cb_P_catID_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                     <div class="colGrid">
                         <asp:Literal runat="server" ID="ltrGrid"></asp:Literal>
                     </div>
                    <div class="colListProduct">
                        <asp:Repeater runat="server" ID="rptListProduct">
                            <ItemTemplate>
                                <div class="productItem" data-id="<%#Eval("ID") %>"><img src="/<%#Eval("ProductAvatar").ToString() %>" style="max-height: 60px; max-width: 60px"/></div>        
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                
                </div>
                
            </div>
            <!-- END EXAMPLE TABLE PORTLET-->
        </div>
          <div class="span12">
					<asp:LinkButton runat="server" CssClass="btn blue" ID="btnSave" OnClientClick="return BeforeSave()"
                        onclick="btnSave_Click"><i class="icon-ok"></i> Save</asp:LinkButton>
												
					<button type="reset" class="btn">Cancel</button>
				</div>
    </div>
    <!-- END PAGE CONTENT -->
</div>
<style>
    .overflow-hidden {overflow:hidden;}
    .colGrid{ width: 700px;float: left; border: 1px dashed #CCC;}
    
    .colListProduct{ width: 300px;float: right;border: 1px dashed #CCC;position: relative;}
    .productItem{ width: 60px;height: 60px; float: left;margin: 5px;}
    .gridItem{width: 120px;height: 120px; float: left;margin: 5px;border: dotted 1px #999;position: relative;}
    .abc{ position: absolute !important;}
    .gridItem > .productItem{ margin: 0;}
</style>
<input type="hidden" runat="server" id="hiddenValue" />
<script language="javascript">
    $(document).ready(function() {
        $(".gridItem,.colListProduct").shapeshift({
            colWidth: 60,
            activeClass: "abc",
        });
    });

    var BeforeSave = function () {
        var stringItem = "";
        $(".gridItem").each(function() {
            if ($(this).children(".productItem").length > 0) {
                stringItem += $(this).attr("id") + "_" + $(this).children(".productItem").attr("data-id") + "$";
            } else {
                stringItem += $(this).attr("id") + "_0" + "$";
            }
        });
        $("#<%=hiddenValue.ClientID%>").val(stringItem);
        return true;
    };
</script>