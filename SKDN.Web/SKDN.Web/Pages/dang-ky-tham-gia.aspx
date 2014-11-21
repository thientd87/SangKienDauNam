<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="dang-ky-tham-gia.aspx.cs" Inherits="SKDN.Web.Pages.dang_ky_tham_gia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="/Scripts/jscroll/jquery.mousewheel-3.1.3.js"></script>
    <script src="/Scripts/jscroll/jquery.scrollpanel-0.5.0.js"></script>
    <div class="dang-ky-tham-gia-wrapper">
        <div class="form-dang-ky">
            <img src="/Images/btnDangKyThamGia.png"/>
        </div>
        <div class="quy-dinh-tham-gia">
            <img src="/Images/btnQuyDinhThamGia.png" style="margin-left: 30px"/>
            <div class="content-quy-dinh-tham-gia scrollpanel no4">
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut elementum, nisl sit amet auctor dapibus, eros arcu aliquet diam, ac hendrerit magna augue eu turpis. Nunc rutrum nulla lacus, sit amet dapibus augue mollis vitae. Suspendisse a porta ante. Duis arcu dui, porta at semper id, commodo ut arcu. Proin consectetur bibendum orci, sed condimentum justo placerat ut. Donec blandit, turpis a porttitor finibus.</p>
                
               <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut elementum, nisl sit amet auctor dapibus, eros arcu aliquet diam, ac hendrerit magna augue eu turpis. Nunc rutrum nulla lacus, sit amet dapibus augue mollis vitae. Suspendisse a porta ante. Duis arcu dui, porta at semper id, commodo ut arcu. Proin consectetur bibendum orci, sed condimentum justo placerat ut. Donec blandit, turpis a porttitor finibus.</p>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut elementum, nisl sit amet auctor dapibus, eros arcu aliquet diam, ac hendrerit magna augue eu turpis. Nunc rutrum nulla lacus, sit amet dapibus augue mollis vitae. Suspendisse a porta ante. Duis arcu dui, porta at semper id, commodo ut arcu. Proin consectetur bibendum orci, sed condimentum justo placerat ut. Donec blandit, turpis a porttitor finibus.</p>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut elementum, nisl sit amet auctor dapibus, eros arcu aliquet diam, ac hendrerit magna augue eu turpis. Nunc rutrum nulla lacus, sit amet dapibus augue mollis vitae. Suspendisse a porta ante. Duis arcu dui, porta at semper id, commodo ut arcu. Proin consectetur bibendum orci, sed condimentum justo placerat ut. Donec blandit, turpis a porttitor finibus.</p>
               
            </div>
            <input type="checkbox"  id="checkbox1" class="checkbox" />
            <label for="checkbox1">Tôi đã đọc và đồng ý</label> 
            <a href="javascript:void(0);" onclick="alert('ok')" class="btn-nopbai">Nộp bài</a>
        </div>
        <div class="clearfix"></div>
    </div>
    <script type="text/javascript">
        $(document).ready(function()
        {
            $('.content-quy-dinh-tham-gia').scrollpanel({
               
            });
        });
        
    </script>
    <style>
       
    </style>
</asp:Content>
