<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeedbackManger.ascx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.Feedback.FeedbackManger" %>
<script type="text/javascript" language="javascript" src="/scripts/checkvalid.js"></script>
<div class="container-fluid">
    <!-- BEGIN PAGE HEADER-->
    <div class="row-fluid">
        <div class="span12">
            <!-- BEGIN PAGE TITLE & BREADCRUMB-->
            <h3 class="page-title">
                Feedback manager <small>Ý kiến khách hàng</small>
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
                        <i class="icon-edit"></i>List feedback</div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a><a href="javascript:location.reload();" class="reload">
                        </a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="dataTables_wrapper form-inline" role="grid">
                          <asp:ObjectDataSource ID="objSupport" runat="server" TypeName="DFISYS.BO.Editoral.FeedbackManagement.FeedbackHelper" DeleteMethod="DeleteFeedback"  SelectMethod="SelectFeedBack" >
            
                            <DeleteParameters>
                                <asp:Parameter Name="ID" DbType="String" />
                            </DeleteParameters>
                      </asp:ObjectDataSource>  
                      <asp:GridView Width="100%" ID="grdListNews" runat="server" EmptyDataText="<span style='color:Red'><b>Không có ý kiến nào !</b></span>"
                        AutoGenerateColumns="False" AllowPaging="True" AllowSorting="true"  CssClass="table table-striped table-hover table-bordered dataTable"
                          PageSize="20" DataSourceID="objSupport">
                        <Columns>
                          <asp:TemplateField>
                            <HeaderTemplate>              
                            </HeaderTemplate>
                            <ItemTemplate>
                              <input type="checkbox" value='<%#Eval("ID")%>' name="chkSelect" onclick="selectRow(this)"
                                 id="chkSelect" />
                            </ItemTemplate>
                   
                            <HeaderStyle Width="20px" />
                            <ItemStyle Width="20px" />
                          </asp:TemplateField>
                          <asp:TemplateField HeaderText="Người gửi" ItemStyle-CssClass="text">
                            <ItemTemplate>
                              <b>Họ tên: </b><%#HttpUtility.HtmlEncode(Convert.ToString(Eval("Name")))%>
                              <br />
                              <b>Email: </b><%#HttpUtility.HtmlEncode(Convert.ToString(Eval("Email")))%>
                              <br />
                              <b>Tel: </b><%#HttpUtility.HtmlEncode(Convert.ToString(Eval("Tel")))%>
                            </ItemTemplate>          
                            <ItemStyle Width="300px" VerticalAlign="Top" />
                          </asp:TemplateField>          
                          <asp:TemplateField HeaderText="Nội dung" >
                            <ItemStyle HorizontalAlign="left" />
                            <ItemTemplate>
                             <%--<b>Tiêu đề: </b><%#HttpUtility.HtmlEncode(Convert.ToString(Eval("Title")))%>
                             <br /><br />--%>
                             <b>Nội dung: </b><%# Eval("Content")%>
                            </ItemTemplate>            
                          </asp:TemplateField>
         
         
                        </Columns>
                       
                      </asp:GridView>
 
                    </div>
                    
                    <div></div>
                    <div class="table-toolbar">
                        <div class="btn-group">
                            <asp:LinkButton ID="LinkDelete" OnClientClick="return getCheckedIDs();" runat="server"
                                        CssClass="btn green" OnClick="LinkDelete_Click"><i class="icon-trash"></i> Xóa các Feedback đã chọn</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END EXAMPLE TABLE PORTLET-->
        </div>
    </div>
    <!-- END PAGE CONTENT -->
</div>


<script language="javascript">

    jQuery(document).ready(function () {


    });
</script>

<input id="hCheckedIDs" runat="server" type="hidden" />
<input id="hiddUnActive" runat="server" type="hidden" />
<input id="hidPrefix" type="hidden" value="<% = ClientID %>_" />

<script language="javascript" type="text/javascript">
    function Load() {
        var obj = document.getElementById("");
        if (obj) { obj.focus(); obj.select(); }
    }
    document.body.onload = Load;

    function GoselectAll(bln) {
        var lists = document.getElementsByName("chkSelect");
        for (var i = 0; i < lists.length; i++) lists[i].checked = bln;
        var chkalls = document.getElementById("chkAll");
        if (chkalls) chkalls.checked = bln;
    }
    function getCheckedIDs() {
        var isSelect = false;

        var strIDs = "";
        var strUnIds = "";
        var lists = document.getElementsByName("chkSelect");

        for (var i = 0; i < lists.length; i++) {
            if (lists[i].checked) {
                isSelect = true;
                strIDs += lists[i].value + ",";
            }
            else {
                strUnIds += lists[i].value + ",";
            }
        }
        var ctr = document.getElementById('<%=hCheckedIDs.ClientID %>');
        var ctrUn = document.getElementById('<%=hiddUnActive.ClientID %>');
        ctr.value = strIDs.substr(0, strIDs.length - 1);
        ctrUn.value = strUnIds.substr(0, strUnIds.length - 1);

        if (!isSelect) {
            alert("Bạn chưa chọn User.");
            return false;
        }
        else {
            return confirm('Bạn có muốn xóa các Feedback này hay không?');

        }

    }

    function getCheckedIDsActive() {
        var isSelect = false;

        var strIDs = "";
        var strUnIds = "";
        var lists = document.getElementsByName("chkActive");

        for (var i = 0; i < lists.length; i++) {
            if (lists[i].checked) {
                isSelect = true;
                strIDs += lists[i].value + ",";
            }
            else {
                strUnIds += lists[i].value + ",";
            }
        }
        var ctr = document.getElementById('<%=hCheckedIDs.ClientID %>');
        var ctrUn = document.getElementById('<%=hiddUnActive.ClientID %>');
        ctr.value = strIDs.substr(0, strIDs.length - 1);
        ctrUn.value = strUnIds.substr(0, strUnIds.length - 1);

        return true;
    }
</script>