<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListNewsCategory.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.NewsCategoryManager.ListNewsCategory" %>
<%@ Register Assembly="RadTreeView.Net2" Namespace="Telerik.WebControls" TagPrefix="radT" %>
<style>
    .productCategoryImage {
        max-width: 100%; max-height: 150px
    }
</style>
<script language="javascript" type="text/javascript" src="/Scripts/Newsedit.js?ver=1"></script>
<script language="javascript" type="text/javascript">
    var prefix = '<% = ClientID %>';
    function GetControlByName(id) {
        var prefix = document.getElementById("hidPrefix").value;
        return document.getElementById(prefix + id);
    }

</script>
<div class="container-fluid">
    <div class="row-fluid">
		<div class="span12">
						
			<h3 class="page-title">
		        News categories manager <small> Quản lý danh mục tin tức</small>
		        </h3>
						
		</div>
	</div>
    <div class="row-fluid">
        <div class="span6"></div>
        <div class="span6  text-center">
            <div class="btn-group ">
                <asp:LinkButton ID="lbtnAdd" tabIndex="2"  onClientClick="return Validate();"  runat="server" type="button" CssClass="btn green"  OnClick="lbtnAdd_Click" ><i class="icon-plus"></i> Add new</asp:LinkButton>
            
			    <asp:LinkButton ID="lbtnDel" tabIndex="2"  type="button" CssClass="btn green" runat="server"  OnClick="lbtnDel_Click" ><i class="icon-trash"></i> Delete</asp:LinkButton>
           
                <a  type="button" class="btn green" href="javascript:window.history.go(-1);"><i class="icon-arrow-left" ></i> Go back</a>
		    </div>
            <br/><br/><br/>
        </div>  
    </div>
    <div class="row-fluid">
    <div class="span4">
        <radT:RadTreeView ID="rtvCatList" runat="server" Style="border: 1px solid #b4cef8" Height="100%" AutoPostBack="True" AutoPostBackOnCheck="True" OnNodeClick="rtvCatList_NodeClick" >
            </radT:RadTreeView>
    </div>
    <div class="span7">
        <div class="row-fluid text-left" runat="server" id="tblEdit" visible="False">
            <div class="portlet box blue tabbable">
                <div class="portlet-title">
				<div class="caption">
					<i class="icon-reorder"></i>
					<span class="hidden-480">News category detail</span>
				</div>
			</div>
            <div class="portlet-body form">
				<div class="tabbable portlet-tabs form-horizontal">
				    <div class="control-group">
						<div class="controls">
							<asp:Image ID="img" runat="server" CssClass="productCategoryImage"/>
						</div>
					</div>
				    <div class="control-group">
				        <label class="control-label">Vietnamese name</label>
                        <div class="controls">
							<input ID="txt_cat_name" runat="server" type="text" class="m-wrap large" placeholder="Vietnamese name" />
					    </div>
				    </div>
                    <div class="control-group">
				        <label class="control-label">English name</label>
                        <div class="controls">
							<input ID="txt_cat_name_en" class="m-wrap large" runat="server" placeholder="English name"></input>
					    </div>
				    </div>
                    <div class="control-group">
				        <label class="control-label">Vietnamese description</label>
                        <div class="controls">
							<textarea ID="txt_cat_desc" class="m-wrap large" rows="5" runat="server" placeholder="Vietnamese description"></textarea>
					    </div>
				    </div>
                    <div class="control-group">
				        <label class="control-label">English description</label>
                        <div class="controls">
							<textarea ID="txt_cat_desc_en" class="m-wrap large" rows="5" runat="server" placeholder="English description"></textarea>
					    </div>
				    </div>
                    <div class="control-group">
				        <label class="control-label">Is category parent</label>
                        <div class="controls">
							<asp:CheckBox runat="server" ID="cbIsParent" OnCheckedChanged="cbIsParent_CheckedChanged" AutoPostBack="true" />
					    </div>
				    </div> 
                    <div class="control-group" id="trLSP" runat="server">
				        <label class="control-label">Select category parent</label>
                        <div class="controls">
							<asp:DropDownList runat="server" ID="cb_P_catID" AutoPostBack="false"  CssClass="ms-long"></asp:DropDownList>
					    </div>
				    </div>
				   <div class="control-group">
				        <label class="control-label">Active</label>
                        <div class="controls">
							<asp:CheckBox runat="server" ID="isActive"  Checked="True" />
                         </div>
				    </div>
                    <div class="control-group">
				        <label class="control-label">Order</label>
                        <div class="controls">
							<input ID="txt_Order" class="m-wrap large" runat="server" placeholder="Order" value="1"></input>
					    </div>
				    </div> 
                    <div class="control-group" >
				        <label class="control-label"></label>
                        <div class="controls">
							<asp:Button ID="btnSave" runat="server" Text="Lưu lại" OnClick="btnSave_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Cập nhật" OnClick="btnUpdate_Click" />
                         </div>
				    </div>
                </div>
            </div>
            </div>
</div>
</div>
</div>
</div>
<br/>


<asp:HiddenField ID="hdfImage" runat="server" />