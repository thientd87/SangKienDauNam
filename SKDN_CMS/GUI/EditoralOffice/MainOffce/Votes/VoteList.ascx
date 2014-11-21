<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VoteList.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.Votes.VoteList" %>
<div class="container-fluid">
    <!-- BEGIN PAGE HEADER-->
    <div class="row-fluid">
        <div class="span12">
            <!-- BEGIN PAGE TITLE & BREADCRUMB-->
            <h3 class="page-title">
                Vote manager <small>Quản lý bình chọn</small>
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
                        <i class="icon-edit"></i>List votes</div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a><a href="javascript:location.reload();" class="reload"></a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="dataTables_wrapper form-inline" role="grid">
                        <asp:GridView Width="100%" ID="grdVote" runat="server" CssClass="table table-striped table-hover table-bordered dataTable"  AutoGenerateColumns="False" AllowPaging="True" DataSourceID="objVoteSource" PageSize="25" OnRowCommand="grdVote_RowCommand"> 
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <%--<input type="checkbox" id="chkAll" onclick="CheckAll();"  class="hidden"/>--%> STT
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="hidden"/>
                                    </ItemTemplate>
                                    <HeaderStyle Width="2%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Chủ đề vote">
                                        <ItemTemplate>
                                            <asp:Literal ID="ltrVoteTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Vote_Title") %>'></asp:Literal>
                                        </ItemTemplate>
                                    <HeaderStyle Width="40%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ng&#224;y bắt đầu">
                                        <ItemTemplate>
                                            <asp:Literal ID="ltrStart" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Vote_StartDate") %>'></asp:Literal>
                                        </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ng&#224;y kết th&#250;c">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem,"Vote_EndDate") %>
                                        </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sửa">
                                    <ItemTemplate>
                                        <a href="/office/voteadd/<%#Eval("Vote_Id")%>.aspx" class="btn mini purple"><i class="icon-edit"></i> Edit</a>
                                      </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="X&#243;a">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDel" runat="server" CommandName="Delete"  OnClientClick="return confirm('Do you want delete this item!');"  CommandArgument='<%# Eval("Vote_ID") %>' CssClass="btn mini black"><i class="icon-trash"></i> Delete</asp:LinkButton>
					                </ItemTemplate>
                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Thêm c&#226;u hỏi cho Vote">
                                    <ItemTemplate>
                                        <center>
						                    <a  href="/office/voteitem.aspx?vote=<%#Eval("Vote_Id")%>"><img id="ImgEdit11"  alt="Thêm câu hỏi cho vote" runat="server" title="Thêm câu hỏi cho vote" src="~/Images/Icons/new.gif"/></a> 
					                    </center>
					                </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="16%" />
                                </asp:TemplateField>
                                
                            </Columns>
                                <RowStyle CssClass="grdItem" />
                                <HeaderStyle CssClass="grdHeader" />
                                <AlternatingRowStyle CssClass="grdAlterItem" />
                                <PagerSettings Visible="False"/>     
                        </asp:GridView>
                    </div>
                    <div></div>
                    <div class="table-toolbar">
                        <div class="btn-group">
                            <a class="btn green" href="/office/voteadd.aspx"> Add New <i class="icon-plus"></i></a>
                          
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End PAGE CONTENT-->
    </div>
<table width="100%">
    <tr>
        <td class="Edit_Head_Cell">
            </td>
    </tr>    
</table>
<asp:UpdatePanel ID="panel" runat="server">
<ContentTemplate>
<table cellpadding="0" cellspacing="0" border="0" width="100%" style="display: none">
    <tr>
        <td>
            <table cellpadding="0" cellspacing="5" width="100%">
                <tr>
                    <td colspan="2">
                        
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="left" colspan="2" style="padding-top:15px">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td width="30%" class="ms-input" style="height: 36px">
                                    Xem trang&nbsp;<asp:DropDownList ID="cboPage" runat="Server" DataTextField="Text" DataValueField="Value" AutoPostBack="true" DataSourceID="objdspage" CssClass="ms-input" OnSelectedIndexChanged="cboPage_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                                <td align="right" style="height: 36px" class="Menuleft_Item">
                                   <a id="a"></a><a onclick="GoselectAll();" onmouseover="this.style.cursor='hand'" onmouseout="this.style.currsor=''">Chọn tất cả</a>&nbsp;|
                                    <a onclick="GoUnselectAll();" onmouseover="this.style.cursor='hand'" onmouseout="this.style.currsor=''">Bỏ chọn</a><asp:Literal ID="Literal0" Text="&nbsp;|" runat="server"></asp:Literal>
                                      <a href="/office/voteadd.aspx"> Thêm vote</a>&nbsp;|
                                    <asp:LinkButton ID="lnkRealDel" OnClientClick="return confirm('Bạn có muốn xóa những vote đã chọn hay không?')" runat="server"  CssClass="normalLnk" OnClick="lnkRealDel_Click">Xóa vote đã chọn</asp:LinkButton>            
                                </td>
                            </tr>
                        </table>  
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr style="display: none">
        <td style="padding-top:10px;">
            <table cellpadding="0" cellspacing="0" border="0" style="width:100%; height:80px; border:1px solid #b8c1ca; background-color:#E5E5E5; clear:both">
                <tr>
                    <td class="ms-input">
                        Tìm theo từ khóa:
                        <asp:TextBox ID="txtKeyword" runat="server" CssClass="ms-long" Width="150px"></asp:TextBox>&nbsp;
                        <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="ms-input"/>
                    </td>
                </tr>
            </table>    
        </td>
    </tr>    
</table>
</ContentTemplate>
</asp:UpdatePanel>
<asp:ObjectDataSource ID="objdspage" runat="server" SelectMethod="getPage" TypeName="ThreadManagement.BO.ThreadHelper" OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:ControlParameter DefaultValue="0" ControlID="grdVote" Name="numPage" PropertyName="PageCount" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="objVoteSource" runat="server" SelectMethod="GetVoteList" DeleteMethod="DelVote" SelectCountMethod="GetVoteRowsCount" TypeName="DFISYS.BO.Editoral.Vote.VoteHelper" EnablePaging="true" MaximumRowsParameterName="PageSize" StartRowIndexParameterName="StartRow">
    <SelectParameters>
         <asp:Parameter Name="strWhere" DefaultValue="" Type ="string" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="_selected_id" Type="string" DefaultValue=""/>
    </DeleteParameters>
</asp:ObjectDataSource>


<script language="javascript">
    function EditVote(Vote_Id)
    {
        window.location.href = "/office/voteadd/"+Vote_Id+".aspx";
    }
    function AddVote()
    {
        window.location.href = "/office/voteadd.aspx";
    }
</script>