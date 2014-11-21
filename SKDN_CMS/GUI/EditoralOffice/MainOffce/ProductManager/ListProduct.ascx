<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListProduct.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.ProductManager.ListProduct" %>
<%@ Import Namespace="System.Globalization" %>
<div class="container-fluid">
    <!-- BEGIN PAGE HEADER-->
    <div class="row-fluid">
        <div class="span12">
            <!-- BEGIN PAGE TITLE & BREADCRUMB-->
            <h3 class="page-title">
                Product Manager <small>Quản lý sản phẩm</small>
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
                        <i class="icon-edit"></i>List products</div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a><a href="javascript:location.reload();" class="reload">
                        </a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="dataTables_wrapper form-inline" role="grid">
                        <asp:GridView ID="grvProduct" CssClass="table table-striped table-hover table-bordered dataTable" DataKeyNames="ID" runat="server"
                        OnRowDataBound="GrdListNewsRowDataBound" 
                       OnPageIndexChanging="grvProduct_PageIndexChanging"
                        AllowPaging="true" PageSize="20" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="True" RowStyle-CssClass="odd" AlternatingRowStyle-CssClass="even" ShowFooter="False" >
                             <Columns>
                                 <asp:TemplateField HeaderText="#">
                                    <HeaderStyle CssClass="sorting_disabled bold"></HeaderStyle>
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                         <asp:HiddenField runat="server" ID="hiddenColorID" Value='<%# Eval("Id") %>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Avatar">
                                    <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                                    <ItemTemplate >
                                        <img src="<%#Eval("ProductAvatar").ToString() %>" style="max-height: 80px; max-width: 180px"/>
                                    </ItemTemplate>
                                    <ItemStyle Height="80px" Width="180px" HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name">
                                    <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                                    <ItemTemplate >
                                        <asp:Literal runat="server" ID="ltrColor"></asp:Literal>
                                        <%#Eval("ProductName").ToString() %>(Vi)
                                        <br/>
                                        <i><%#Eval("ProductName_En").ToString() %>(En)</i>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Cost">
                                    <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                                    <ItemTemplate >
                                        <%#String.Format("{0:#,###0}", Eval("ProductCost"))%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category">
                                    <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                                    <ItemTemplate >
                                        <asp:Literal runat="server" ID="ltrCategory"></asp:Literal>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Preview">
                                    <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                                    <ItemTemplate >
                                        <asp:Literal runat="server" ID="ltrLinkWeb"></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IsActive">
                                    <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                                    <ItemTemplate >
                                        <%#Convert.ToBoolean(Eval("IsActive").ToString()) ? "<span class=\"label label-success\">Approved</span>" : "<span class=\"label label-danger\">Blocked</span>"%>
                                    </ItemTemplate>
                                   
                                    <ItemStyle Width="50px"></ItemStyle>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Edit">
                                    <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                                    <ItemTemplate >
                                        <a href="/office/addproduct.aspx?pid=<%#Eval("ID")%>"  class="btn mini purple">Edit</a>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px"></ItemStyle>
                                </asp:TemplateField>
                             </Columns>
                             <%--<PagerTemplate>
                                 <div class="row-fluid">
                                    <div class="span6">
                                        
                                    </div>
                                    <div class="span6">
                                        <div class="dataTables_paginate paging_bootstrap pagination text-right">
                                            <ul>
                                                <li class="prev">
                                                    <asp:LinkButton id="lnkFirst" Text="← First" CommandName="Page" CommandArgument="First" ToolTip="First Page" Runat="server" /> 
                                                </li>
                                                 <asp:Repeater ID="repFooter" OnItemCommand="repFooter_ItemCommand" runat="server">
                                                    <ItemTemplate>
                                                        <li class='<%#(grvProduct.PageIndex == Container.ItemIndex )? "active":""%>'>
                                                            <asp:LinkButton  ID="lnkPage" Text='<%# Container.DataItem %>' CommandName="ChangePage" CommandArgument="<%# Container.DataItem %>" runat="server" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <li class="prev">
                                                    <asp:LinkButton id="lnkLast" Text="Last →" CommandName="Page" CommandArgument="Last" ToolTip="Last Page" Runat="server" /> 
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                             </PagerTemplate>--%>
                             <PagerStyle CssClass="dataTables_paginate" HorizontalAlign="Right"  />
                             <PagerSettings Mode="NumericFirstLast" FirstPageText="← First" LastPageText="Last →" PageButtonCount="3"/>  
                        </asp:GridView>
                    </div>
                    
                    <div></div>
                    <div class="table-toolbar">
                        <div class="btn-group">
                            <a href="/office/addproduct.aspx"  class="btn green">Add New <i class="icon-plus"></i></a>
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

