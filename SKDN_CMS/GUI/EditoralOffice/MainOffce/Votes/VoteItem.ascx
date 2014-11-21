<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VoteItem.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.Votes.VoteItem" %>
<div class="container-fluid">
    <!-- BEGIN PAGE HEADER-->
    <div class="row-fluid">
        <div class="span12">
            <!-- BEGIN PAGE TITLE & BREADCRUMB-->
            <h3 class="page-title">
                Vote manager <small>quản lý bình chọn</small>
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
                        <i class="icon-edit"></i>List vote items</div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a><a href="javascript:location.reload();" class="reload">
                        </a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="dataTables_wrapper form-inline" role="grid">
                        <asp:GridView ID="grdThreadList" CssClass="table table-striped table-hover table-bordered dataTable" DataKeyNames="VoteIt_ID" runat="server"
                           DataSourceID="objVoteItemSource"  PageSize="12" OnRowCommand="grdThreadList_RowCommand" OnRowCancelingEdit="grdThreadList_RowCancelingEdit" OnRowEditing="grdThreadList_RowEditing" OnRowUpdating="grdThreadList_RowUpdating" OnRowDeleting="grdThreadList_RowDeleting"
                            AllowPaging="true" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="True" RowStyle-CssClass="odd" AlternatingRowStyle-CssClass="even" ShowFooter="True" >
                             <Columns>
                                  <asp:TemplateField>
                                        <HeaderTemplate>
                                            <input type="checkbox" id="chkAll" onclick="CheckAll();"/>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server"/>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            &nbsp;
                                        </FooterTemplate>
                                        <HeaderStyle Width="2%" />
                                    </asp:TemplateField>
                              <asp:TemplateField HeaderText="Nội dung câu hỏi">
                                        <ItemTemplate>
                                            <asp:Label ID="lblThreadTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"VoteIt_Content") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                                <asp:TextBox ID="txtEditVoteItem" runat="server" CssClass="m-wrap larger" Width="98%" Text='<%# DataBinder.Eval(Container.DataItem,"VoteIt_Content") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                                <asp:TextBox ID="txtVoteItem" runat="server"  CssClass="m-wrap larger" Width="98%"></asp:TextBox>
                                        </FooterTemplate>   
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Số bình chọn">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVoteRank" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"VoteIt_Rate") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                                <asp:TextBox ID="txtEditVoteRank" runat="server" Width="98%" Text='<%# DataBinder.Eval(Container.DataItem,"VoteIt_Rate") %>' ReadOnly="True"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                                <asp:TextBox ID="txtVoteRank" ReadOnly="True"  Text="0" runat="server" Width="98%"></asp:TextBox>
                                        </FooterTemplate>   
                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Edit">
                                    <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                                    <ItemTemplate >
                                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("VoteIt_ID") %>' CssClass="btn mini purple"><i class="icon-edit"></i> Edit</asp:LinkButton>
                                      &nbsp;
                                        <asp:LinkButton ID="lbtnDel" runat="server" CommandName="Delete"  OnClientClick="return confirm('Do you want delete this item!');"  CommandArgument='<%# Eval("VoteIt_ID") %>' CssClass="btn mini black"><i class="icon-trash"></i> Delete</asp:LinkButton>
                                      
                                    </ItemTemplate>
                                     <EditItemTemplate>
                                         <asp:LinkButton ID="lbtnSave"  runat="server" CommandArgument='<%# Eval("VoteIt_ID") %>' CommandName="Update" CssClass="btn mini green"><i class="icon-save"></i> Save</asp:LinkButton>
                                      &nbsp;
                                        <asp:LinkButton ID="lbtnCancel" runat="server" CommandName="Cancel" CssClass="btn mini orange"><i class="icon-undo"></i> Cancel</asp:LinkButton>
                                     
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnSave"  runat="server" CommandArgument='0' CommandName="NewVoteItem" CssClass="btn mini green"><i class="icon-save"></i> Save</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemStyle Width="150px"></ItemStyle>
                                </asp:TemplateField>
                             </Columns>
                             
                        </asp:GridView>
                    </div>
                    
                    <div></div>
                  <%--  <div class="table-toolbar">
                        <div class="btn-group">
               
                            <asp:LinkButton runat="server" ID="btnAddNewColor" CssClass="btn green" 
                                onclick="btnAddNewColor_Click" >Add New <i class="icon-plus"></i></asp:LinkButton>
                        </div>
                    </div>--%>
                </div>
            </div>
            <!-- END EXAMPLE TABLE PORTLET-->
        </div>
    </div>
    <!-- END PAGE CONTENT -->
</div>



<asp:ObjectDataSource ID="objVoteItemSource" TypeName="DFISYS.BO.Editoral.Vote.VoteItemHelper" DeleteMethod="DelItem" SelectMethod="getVoteItem" InsertMethod="CreateVoteItem" UpdateMethod="UpdateVoteItem" runat="server">
    <SelectParameters>
        <asp:QueryStringParameter Name="_vote_id" QueryStringField="vote" Type="int32" />
    </SelectParameters>
    <InsertParameters>
        <asp:QueryStringParameter QueryStringField="vote"  Name="_vote_id" Type="int32" />
        <asp:Parameter Name="_vote_item" Type="String" />
        <asp:Parameter Name ="_VotNum" Type="int32" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="_item_id" Type="int32" />
        <asp:Parameter Name="_vote_item" Type="string" />
        <asp:Parameter Name ="_VotNum" Type="int32" />
    </UpdateParameters>
    <DeleteParameters>
        <asp:Parameter Name="_item_id" Type="int32" />
    </DeleteParameters>
</asp:ObjectDataSource>