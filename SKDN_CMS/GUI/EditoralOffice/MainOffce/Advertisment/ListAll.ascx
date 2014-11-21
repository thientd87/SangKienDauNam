<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListAll.ascx.cs" Inherits="AddIns.GUI.EditoralOffice.MainOffce.Advertisment.ListAll" %>
<div class="container-fluid">
    <!-- BEGIN PAGE HEADER-->
    <div class="row-fluid">
        <div class="span12">
            <!-- BEGIN PAGE TITLE & BREADCRUMB-->
            <h3 class="page-title">
                Banner manager <small>Quản lý banner</small>
            </h3>
            <!-- END PAGE TITLE & BREADCRUMB-->
        </div>
    </div>
    <!-- END PAGE HEADER-->
    <!-- BEGIN PAGE CONTENT-->
    <div  class="row-fluid hide">
        <table id="tblSearch" runat="server" cellpadding="5" cellspacing="5" style="width: 100%;">
            <tr>
                <td style="vertical-align: middle;">
                    <table cellpadding="5" cellspacing="5">
                        <tr>
                            <td width="70" style="vertical-align: middle; padding: 10px 0">
                                Từ khóa
                            </td>
                            <td style="vertical-align: middle; padding: 10px 0">
                                <asp:TextBox AutoCompleteType="None" ID="txtKeyword" Width="250" runat="server"></asp:TextBox>
                            </td>
                            <td width="80" style="vertical-align: middle; padding: 10px 0; text-align: center;">
                                Chuyên mục
                            </td>
                            <td width="70" style="vertical-align: middle; padding: 10px 0">
                                <asp:DropDownList ID="ddlPages" runat="server" DataTextField="Cat_Name" DataValueField="Cat_ID"
                                    CssClass="ddlPages">
                                    
                                </asp:DropDownList>
                            </td>
                            <td width="70" style="vertical-align: middle; padding: 10px 0; text-align: center;">
                                Vị trí
                            </td>
                            <td width="70" style="vertical-align: middle; padding: 10px 0">
                                <asp:DropDownList ID="ddlPosition" runat="server" DataTextField="PosName" DataValueField="PosID"
                                    CssClass="ddlPoss">
                                    
                                </asp:DropDownList>
                            </td>
                            <td width="70" style="vertical-align: middle; padding: 10px 10px">
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
    </div>
    <div class="row-fluid">
        <div class="span12">
            <!-- BEGIN EXAMPLE TABLE PORTLET-->
            <div class="portlet box blue">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-edit"></i>List banner</div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a><a href="javascript:location.reload();" class="reload">
                        </a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="dataTables_wrapper form-inline" role="grid">
                        <asp:GridView ID="grdList"  CssClass="table table-striped table-hover table-bordered dataTable" runat="server"
                         OnRowDeleting="grdList_RowDeleting"
                        
                         AutoGenerateColumns="False" EnableModelValidation="false"
                EmptyDataText="Không có bản ghi nào!" AllowPaging="True" PageSize="20" Width="100%">
                <Columns>
                    <asp:TemplateField HeaderText="Name" HeaderStyle-Width="250px">
                        <ItemTemplate>
                            <%#HttpUtility.HtmlEncode(Eval("Name").ToString()) %>
                             <asp:HiddenField runat="server" ID="hiddenAdvID" Value='<%# Eval("AdvId") %>'/>
                        </ItemTemplate>
                        <ItemStyle Width="250px" CssClass="vertical-align-top tieudelist" VerticalAlign="Top" />
                        <HeaderStyle Width="50px" CssClass="valign-middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start date">
                        <ItemTemplate>
                            <%#Eval("StartDate", "{0:dd/MM/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End date">
                        <ItemTemplate>
                            <%#Eval("EndDate", "{0:dd/MM/yyyy}")%></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Preview">
                        <ItemTemplate>
                            <img src="<%# Eval("FilePath") %>" height="80px"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                            <ItemTemplate >
                                <%#Convert.ToBoolean(Eval("IsActive").ToString()) ? "<span class=\"label label-success\">Approved</span>" : "<span class=\"label label-danger\">Blocked</span>"%>
                            </ItemTemplate>
                     <ItemStyle Width="50px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <HeaderStyle CssClass="sorting_disabled"></HeaderStyle>
                        <ItemTemplate >
                            <a href="/office/addbanner.aspx?advid=<%#Eval("AdvID")%>"  class="btn mini purple">Edit</a>
                             &nbsp;
                            <asp:LinkButton ID="lbtnDel" runat="server" CommandName="Delete" OnClientClick="return confirm('Do you want delete this item!');" CommandArgument='<%# Eval("AdvID") %>' CssClass="btn mini black"><i class="icon-trash"></i> Delete</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="130px"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <RowStyle HorizontalAlign="Center" />
            </asp:GridView>
                    </div>
                    
                    <div></div>
                    <div class="table-toolbar">
                        <div class="btn-group">
                            <a class="btn green" href="/office/addbanner.aspx">Add New <i class="icon-plus"></i></a>
                            
                        </div>
                    </div>
                </div>
            </div>
            <!-- END EXAMPLE TABLE PORTLET-->
        </div>
    </div>
    <!-- END PAGE CONTENT -->
</div>



<script type="text/javascript">
    $(document).ready(function ($) {
      //  $('a[rel*=colorbox]').facebox({ width: 900, height: 500 });
    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(EndRequest);
    function EndRequest(sender, args) {
        $('#bgFilter').hide();
        $('#imgloading').hide();
        $('a[rel*=colorbox]').facebox({ width: 900, height: 500 });
        $('*[rel*=editbox]').facebox({ iframe: true, width: 900, height: 500 });
    }


    function Save(id, status) {

        alert('adfasdf');
        if ($('#ddlPos').val() == 0) {
            alert('Bạn chưa chọn vị trí');
            return false;
        }
        if ($('#adv_name').val() == '') {
            alert('Bạn chưa nhập tên quảng cáo');
            return false;
        }
        if ($('#txtSelectedFile').val() == '' && $('#adv_type input:checked').val() != 3) {
            alert('Chưa có đường dẫn đến file quảng cáo');
            return false;
        }
        if ($('#adv_startdate').val() == '') {
            alert('Bạn chưa chọn ngày bắt đầu');
            return false;
        }
        if ($('#adv_enddate').val() == '') {
            alert('Bạn chưa chọn ngày kết thúc');
            return false;
        }
        showLoading();
        $.post("/Ajax/Adv/adv-addnew.aspx?post=true&advId=" + id, $('#adv_add *').serialize(), function (data) {
            hidenLoading();
            $.facebox.close();
        });
 
//        if ($("#txtName").val() == "") {
//            alert("Chưa nhập Nội dung");
//            $("#txtName").focus();
//            return false;
//        }

//        if ($("#txtURL").val() == "") {
//            alert("Chưa nhập Email");
//            $("#txtURL").focus();
//            return false;
//        }
//        $.post("/Ajax/Weblinks/EditWebLink.aspx?post=true&status=" + status + "&weblinkId=" + id, $('#saveWeblink *').serialize(), function (data) {
//            $("#name_" + id).html($("#txtName").val().replace(/\n/gim, '</br>'));
//            $("#url_" + id).html($("#txtURL").val());
//            $("#url_" + id).attr("href", $("#txtURL").val());
//            $.facebox.close();
//            if (status == true)
//                $("#content_" + id).parent().parent().remove();
//        });
        return false;
    }
     
</script>
