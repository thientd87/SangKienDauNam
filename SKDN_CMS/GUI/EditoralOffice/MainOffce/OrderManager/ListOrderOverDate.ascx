<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListOrderOverDate.ascx.cs" Inherits="MobileShop.GUI.Back_End.Order.ListOrderOverDate" %>
<div class="container-fluid">
    <!-- BEGIN PAGE HEADER-->
    <div class="row-fluid">
        <div class="span12">
            <!-- BEGIN PAGE TITLE & BREADCRUMB-->
            <h3 class="page-title">
                Order Manager <small>Quản lý đơn hàng</small>
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
                        <i class="icon-edit"></i>Danh sách các đơn hàng quá hạn thanh toán</div>
                    <div class="tools">
                        <a href="javascript:;" class="collapse"></a><a href="javascript:location.reload();" class="reload">
                        </a>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="dataTables_wrapper form-inline" role="grid">
                        <asp:GridView  ID="gvListProducts" runat="server" CssClass="table table-striped table-hover table-bordered dataTable" RowStyle-CssClass="odd" AlternatingRowStyle-CssClass="even" 
                            AllowPaging="True" AutoGenerateColumns="False" DataSourceID="objListOrder" PageSize="20"  Width="100%" EmptyDataText="Chưa có đơn hàng quá hạn thanh toán" OnRowCommand="gvListProducts_RowCommand" >
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <input id="chkSelect" name="chkSelect" type="checkbox" value='<%#Eval("O_ID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="M&#227; h&#243;a đơn">
                                 <ItemTemplate>                                                      
                                        <a href='/office/orderdetail.aspx?type=overdate&oid=<%#Eval("O_ID")%>' style="color:#333;">
                                                        <%#HttpUtility.HtmlEncode(Convert.ToString(Eval("O_ID")))%>
                                                    </a>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="T&#234;n kh&#225;ch h&#224;ng">
                                  <ItemTemplate>
                                        <%# HttpUtility.HtmlEncode(Eval("C_FullName").ToString())%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ng&#224;y đặt h&#224;ng">
                                   <ItemTemplate>
                                        <%# HttpUtility.HtmlEncode(Eval("Orderdate").ToString())%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <%# Eval("C_Email")%> 
                                    </ItemTemplate>
                                  </asp:TemplateField>
                                <asp:TemplateField HeaderText="Địa chỉ">
                                    <ItemTemplate>
                                        <%# Eval("C_Address")%>
                                    </ItemTemplate>
                                   </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tổng gi&#225; trị(VNĐ)">
                                   <ItemTemplate>
                                        <%# HttpUtility.HtmlEncode(Eval("O_Total").ToString())%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="X&#243;a">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtDelete" CommandName="Delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"O_ID")%>'
                                            runat="server" OnClientClick="return confirm('Bạn có chắc chắn là xóa không?');">
                                 <i class="icon-trash" style="font-size: 20px"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                  </asp:TemplateField>
                                                
                            </Columns>
                           
                            <PagerSettings FirstPageText="Trang đầu" LastPageText="Trang cuối" NextPageText="Trang sau"
                                PreviousPageText="Trang trước" />
                                <PagerStyle HorizontalAlign="Right" CssClass="paging_product" Font-Bold="True" Font-Size="10pt" />
                        </asp:GridView>
                    </div>
                    
                    <div></div>
                    <div class="table-toolbar">
                        <div class="btn-group">
                            
                                <asp:LinkButton ID="LinkDelete" OnClientClick="return getCheckedIDs();" runat="server"
                                                        CssClass="btn green" OnClick="LinkDelete_Click"><i class="icon-trash"></i> Xóa đơn hàng</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <!-- END EXAMPLE TABLE PORTLET-->
        </div>
    </div>
    <!-- END PAGE CONTENT -->
</div>
  <asp:ObjectDataSource ID="objListOrder" runat="server" SelectMethod="GetOrderOverRequiredDate" DeleteMethod="DeleteOrderByID" TypeName="BO.OrderHelper">
                            <DeleteParameters>
                                <asp:Parameter Name="O_ID" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                            <input id="hCheckedIDs" runat="server" type="hidden" />
                            <input id="hidPrefix" type="hidden" value="<% = ClientID %>_" />
<script language="javascript" type="text/javascript">
    function Load()
    {
        var obj = document.getElementById("");
        if (obj){obj.focus();obj.select();}
    }
    document.body.onload = Load;
    
    function GoselectAll(bln)
    {
        var lists = document.getElementsByName("chkSelect");
        for (var i=0; i <lists.length;i++) lists[i].checked = bln;
        var chkalls = document.getElementById("chkAll");
        if (chkalls) chkalls.checked = bln;
    }
    function getCheckedIDs()
    {
        var isSelect = false;
        
        var strIDs="";
        var lists = document.getElementsByName("chkSelect");       
        
        for (var i = 0 ; i < lists.length; i++)
        {
            if (lists[i].checked) 
            {
                isSelect = true;
               strIDs +=lists[i].value+ ",";
            }
        }
        var ctr=document.getElementById('<%=hCheckedIDs.ClientID %>');
        
        ctr.value=strIDs.substr(0,strIDs.length-1);
       
        if (!isSelect)
        {
            alert("Bạn chưa chọn đơn hàng");
            return false;
        }
        else
        {
            return confirm('Bạn có chăc chắn chọn các đơn hàng này hay không?');
             
        }

    }
        </script>

