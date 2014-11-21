<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListSupportOnine.ascx.cs"
    Inherits="DFISYS.GUI.EditoralOffice.MainOffce.SupportOnline.ListSupportOnine" %>
<div class="container-fluid">
    <!-- BEGIN PAGE HEADER-->
    <div class="row-fluid">
        <div class="span12">
            <!-- BEGIN PAGE TITLE & BREADCRUMB-->
            <h3 class="page-title">
                Support Online manager <small>quản lý hỗ trợ trực tuyến</small>
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
                        <i class="icon-edit"></i>List support online nick</div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a><a href="javascript:location.reload();" class="reload">
                        </a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="dataTables_wrapper form-inline" role="grid">
                        <asp:GridView Width="100%" ID="grdListSupport" runat="server" CssClass="table table-striped table-hover table-bordered dataTable"
                            ShowHeaderWhenEmpty="True" RowStyle-CssClass="odd" AlternatingRowStyle-CssClass="even"
                            OnRowCancelingEdit="grvCategories_RowCancelingEdit"
                            OnRowEditing="grvCategories_RowEditing" OnRowDeleting="grvCategories_RowDeleting"
                            OnSelectedIndexChanged="grvCategories_SelectedIndexChanged"
                            OnRowUpdating="grvCategories_RowUpdating"
                            OnRowCommand="grvCategories_RowCommand"
               AutoGenerateColumns="False" AllowPaging="false" DataKeyNames="ID">
                            <Columns>
                                <asp:TemplateField HeaderText="Tên Đầy Đủ">
                                    <HeaderStyle CssClass="sorting_disabled "></HeaderStyle>
                                    <ItemTemplate >
                                        <%#Eval("FullName").ToString() %> 
                                        <asp:HiddenField runat="server" ID="hiddenColorID" Value='<%# Eval("Id") %>'/>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <input type="text" runat="server" id="txtFullName" class="m-wrap small" value='<%#Eval("FullName").ToString() %>'/>
                                        <asp:HiddenField runat="server" ID="hiddenColorID" Value='<%# Eval("Id") %>'/>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <input type="text" runat="server" id="txt_NewFullName" class="m-wrap small" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nick yahoo">
                                    <HeaderStyle CssClass="sorting_disabled "></HeaderStyle>
                                    <ItemTemplate >
                                        <%#Eval("Yahoo").ToString() %> 
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <input type="text" runat="server" id="txtYahoo" class="m-wrap small" value='<%#Eval("Yahoo").ToString() %>'/>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <input type="text" runat="server" id="txt_NewYahoo" class="m-wrap small" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nick skype">
                                    <HeaderStyle CssClass="sorting_disabled "></HeaderStyle>
                                    <ItemTemplate >
                                        <%#Eval("Skype").ToString() %> 
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <input type="text" runat="server" id="txtSkype" class="m-wrap small" value='<%#Eval("Skype").ToString() %>'/>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <input type="text" runat="server" id="txt_NewSkype" class="m-wrap small" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile">
                                    <HeaderStyle CssClass="sorting_disabled "></HeaderStyle>
                                    <ItemTemplate >
                                        <%#Eval("Mobile").ToString() %> 
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <input type="text" runat="server" id="txtMobile" class="m-wrap small" value='<%#Eval("Mobile").ToString() %>'/>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <input type="text" runat="server" id="txt_NewMobile" class="m-wrap small" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                             
                                <asp:TemplateField HeaderText="Edit">
                                    <HeaderStyle CssClass="sorting_disabled "></HeaderStyle>
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

