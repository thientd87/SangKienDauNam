<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListGallery.ascx.cs"
    Inherits="DFISYS.GUI.EditoralOffice.MainOffce.Tool.ListGallery" %>
<asp:HiddenField ID="hdArgs" runat="server" />
<asp:HiddenField ID="hdNewsID" runat="server" />

<div class="container-fluid">
    <!-- BEGIN PAGE HEADER-->
    <div class="row-fluid">
        <div class="span12">
            <!-- BEGIN PAGE TITLE & BREADCRUMB-->
            <h3 class="page-title">
                Gallery manager <small>Quản lý Gallery</small>
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
                        <i class="icon-edit"></i>List gallery</div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a><a href="javascript:location.reload();" class="reload">
                        </a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="dataTables_wrapper form-inline" role="grid">
                        <asp:GridView runat="server" ID="grdListSupport" CssClass="table table-striped table-hover table-bordered dataTable" AllowPaging="False" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="True" RowStyle-CssClass="odd" AlternatingRowStyle-CssClass="even" ShowFooter="False"
                            OnRowDeleting="grvCategories_RowDeleting" >
                             <Columns>
                                 <asp:TemplateField HeaderText="#">
                                      <ItemStyle Width="30px"></ItemStyle>
                                    <HeaderStyle CssClass="sorting_disabled bold"></HeaderStyle>
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                         <asp:HiddenField runat="server" ID="hiddenColorID" Value='<%# Eval("Id") %>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gallery name">
                                    <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                                    <ItemTemplate >
                                        <a href="/office/addgallery.aspx?id=<%#Eval("Id")%>"><%#Eval("Name")%></a>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <input type="text" runat="server" id="txt_ColorName" class="m-wrap small" value='<%#Eval("ColorName").ToString() %>'/>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <input type="text" runat="server" id="txt_NewColorName" class="m-wrap small" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Edit">
                                    <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                                    <ItemTemplate >
                                        <a href="/office/addgallery.aspx?id=<%#Eval("Id")%>" class="btn mini purple"><i class="icon-edit"></i> Edit</a>
                                      &nbsp;
                                        <asp:LinkButton ID="lbtnDel" runat="server" CommandName="Delete"  OnClientClick="return confirm('Do you want delete this item!');"  CommandArgument='<%# Eval("ID") %>' CssClass="btn mini black"><i class="icon-trash"></i> Delete</asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:TemplateField>
                             </Columns>
                             
                        </asp:GridView>
                    </div>
                    
                    <div></div>
                    <div class="table-toolbar">
                        <div class="btn-group">
                            <a href="/office/addgallery.aspx" class="btn green">Add New <i class="icon-plus"></i></a>
                           
                        </div>
                    </div>
                </div>
            </div>
            <!-- END EXAMPLE TABLE PORTLET-->
        </div>
    </div>
    <!-- END PAGE CONTENT -->
</div>


<table id="tblSearch" runat="server" cellpadding="5" cellspacing="5" style="width: 100%; display: none">
    <tr>
        <td style="vertical-align: middle;">
            <table cellpadding="5" cellspacing="5">
                <tr>
                    <td width="120" style="vertical-align: middle; padding: 10px 0">
                        Từ khóa
                    </td>
                    <td style="vertical-align: middle; padding: 10px 0">
                        <asp:TextBox AutoCompleteType="None" ID="txtKeyword" Width="350" runat="server"></asp:TextBox>
                        <asp:Button ID="btnSearch" CssClass="btnUpdate" OnClientClick="endRequest = 'window.scrollTo(0,0)'"
                            runat="server" Text="Tìm kiếm" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </td>
        <td>
            <br />
        </td>
    </tr>
</table>

