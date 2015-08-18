<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListCategory2.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.Product_Category.ListCategory2" %>
<div class="container-fluid">
    <!-- BEGIN PAGE HEADER-->
    <div class="row-fluid">
        <div class="span12">
            <!-- BEGIN PAGE TITLE & BREADCRUMB-->
            <h3 class="page-title">
                Quản lý gian hàng <small></small>
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
                        <i class="icon-edit"></i>List stores</div>
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
                                     <ItemStyle Width="30px"></ItemStyle>
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                         <asp:HiddenField runat="server" ID="hiddenColorID" Value='<%# Eval("Id") %>'/>
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shopping Name" >
                                    <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                                    <ItemStyle Width="300px"></ItemStyle>
                                    <ItemTemplate >
                                        <%#Eval("Product_Category_Name").ToString() %> 
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <input type="text" runat="server" id="txt_Product_Category_Name" class="m-wrap large" value='<%#Eval("Product_Category_Name").ToString() %>'/>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <input type="text" runat="server" id="txt_New_Product_Category_Name" class="m-wrap large" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Image">
                                    <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                                      <ItemStyle Width="300px"></ItemStyle>
                                    <ItemTemplate >
                                        <img src="<%#Eval("Product_Category_Image") %>" style="height: 80px" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <input type="text" runat="server" id="txt_Product_Category_Image" ClientIDMode="Static" class="m-wrap large" value='<%#Eval("Product_Category_Image") %>'/>
                                         <img src="/images/icons/folder.gif" onclick="openInfo('/FileManager/index.html?field_name=txt_Product_Category_Image',900,700)" style="cursor: pointer;" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <input type="text" runat="server" id="txt_New_Product_Category_Image" ClientIDMode="Static"  class="m-wrap large"/>
                                          <img src="/images/icons/folder.gif" onclick="openInfo('/FileManager/index.html?field_name=txt_New_Product_Category_Image',900,700)" style="cursor: pointer;" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Description">
                                    <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                                      <ItemStyle Width="300px"></ItemStyle>
                                    <ItemTemplate >
                                        <%#Eval("Product_Category_Desc") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" ID="txt_Product_Category_Desc" CssClass="m-wrap large" TextMode="MultiLine" Rows="5" Text='<%#Eval("Product_Category_Desc").ToString() %>' ></asp:TextBox>
                                        
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                         <asp:TextBox runat="server" ID="txt_New_Product_Category_Desc" TextMode="MultiLine" Rows="5"  CssClass="m-wrap large"  ></asp:TextBox>
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