<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="List.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.ProductColor.List" %>
<div class="container-fluid">
    <!-- BEGIN PAGE HEADER-->
    <div class="row-fluid">
        <div class="span12">
            <!-- BEGIN PAGE TITLE & BREADCRUMB-->
            <h3 class="page-title">
                Product Color manager <small>quản lý màu sắc sản phẩm</small>
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
                        <i class="icon-edit"></i>List colors</div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a><a href="javascript:location.reload();" class="reload">
                        </a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="dataTables_wrapper form-inline" role="grid">
                        <asp:GridView ID="grvColor" CssClass="table table-striped table-hover table-bordered dataTable" DataKeyNames="ID" runat="server"
                            OnRowCancelingEdit="grvCategories_RowCancelingEdit"
                            OnRowEditing="grvCategories_RowEditing" OnRowDeleting="grvCategories_RowDeleting"
                            OnSelectedIndexChanged="grvCategories_SelectedIndexChanged"
                            OnRowUpdating="grvCategories_RowUpdating"
                            OnRowCommand="grvCategories_RowCommand"
                            AllowPaging="true" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="True" RowStyle-CssClass="odd" AlternatingRowStyle-CssClass="even" ShowFooter="False" >
                             <Columns>
                                 <asp:TemplateField HeaderText="#">
                                    <HeaderStyle CssClass="sorting_disabled bold"></HeaderStyle>
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                         <asp:HiddenField runat="server" ID="hiddenColorID" Value='<%# Eval("Id") %>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ColorName">
                                    <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                                    <ItemTemplate >
                                        <div style="width: 15px; height: 15px; display: inline-block; background: <%#Eval("ColorCode").ToString()%>"></div> &nbsp; <%#Eval("ColorName").ToString() %> 
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <input type="text" runat="server" id="txt_ColorName" class="m-wrap small" value='<%#Eval("ColorName").ToString() %>'/>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <input type="text" runat="server" id="txt_NewColorName" class="m-wrap small" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ColorCode">
                                    <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                                    <ItemTemplate >
                                        <%#Eval("ColorCode").ToString()%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <input type="text" runat="server" id="txt_ColorCode" class="m-wrap small color {hash:true}" value='<%#Eval("ColorCode").ToString() %>'/>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <input type="text" runat="server" id="txt_NewColorCode" class="m-wrap small color {hash:true}"/>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IsActive">
                                    <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                                    <ItemTemplate >
                                        <%#Convert.ToBoolean(Eval("IsActive").ToString()) ? "<span class=\"label label-success\">Approved</span>" : "<span class=\"label label-danger\">Blocked</span>"%>
                                    </ItemTemplate>
                                     <EditItemTemplate >
                                        <asp:CheckBox ID="chkIsHidden" Checked='<%# Eval("IsActive") %>' runat="server" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:CheckBox ID="chkNewIsHidden"  runat="server" Checked="True"/>
                                    </FooterTemplate>
                                    <ItemStyle Width="50px"></ItemStyle>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Edit">
                                    <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                                    <ItemTemplate >
                                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' CssClass="btn mini purple"><i class="icon-edit"></i> Edit</asp:LinkButton>
                                      &nbsp;
                                        <asp:LinkButton ID="lbtnDel" runat="server" CommandName="Delete"  OnClientClick="return confirm('Do you want delete this item!');"  CommandArgument='<%# Eval("ID") %>' CssClass="btn mini black"><i class="icon-trash"></i> Delete</asp:LinkButton>
                                      
                                    </ItemTemplate>
                                     <EditItemTemplate>
                                         <asp:LinkButton ID="lbtnSave"  runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Update" CssClass="btn mini green"><i class="icon-save"></i> Save</asp:LinkButton>
                                      &nbsp;
                                        <asp:LinkButton ID="lbtnCancel" runat="server" CommandName="Cancel" CssClass="btn mini orange"><i class="icon-undo"></i> Cancel</asp:LinkButton>
                                     
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnSave"  runat="server" CommandArgument='0' CommandName="AddNew" CssClass="btn mini green"><i class="icon-save"></i> Save</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:TemplateField>
                             </Columns>
                             
                        </asp:GridView>
                    </div>
                    
                    <div></div>
                    <div class="table-toolbar">
                        <div class="btn-group">
               
                            <asp:LinkButton runat="server" ID="btnAddNewColor" CssClass="btn green" 
                                onclick="btnAddNewColor_Click" >Add New <i class="icon-plus"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END EXAMPLE TABLE PORTLET-->
        </div>
    </div>
    <!-- END PAGE CONTENT -->
</div>

<script src="/Styles/metronic/plugins/jscolor/jscolor.js"></script>
<script language="javascript">
    
    jQuery(document).ready(function () {

        
    });
</script>
