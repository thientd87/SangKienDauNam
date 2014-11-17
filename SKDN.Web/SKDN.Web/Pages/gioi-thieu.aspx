<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="gioi-thieu.aspx.cs" Inherits="SKDN.Web.Pages.gioi_thieu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Scripts/fancybox/jquery.fancybox.css" rel="stylesheet" />
    <script src="/Scripts/fancybox/jquery.fancybox.js"></script>
    <div id="divBanner">
        <a class="fancybox" href="#divChungToi"><img style="padding-left: 35px" src="/Images/bannerChungToi.jpg"/></a>
        <a href="#"><img style="padding-left: 6px" src="/Images/bannerNhaTaiTro.jpg"/></a>
        <a href="#"><img style="padding-left: 6px" src="/Images/bannerSuMenh.jpg"/></a>
    </div>
    <div id="divChungToi">
        <img src="/Images/imgChungToi.jpg" style=" float: left"/>
        <div class="text-chung-toi">
            <div class="col1">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut elementum, nisl sit amet auctor dapibus, eros arcu aliquet diam, ac hendrerit magna augue eu turpis. Nunc rutrum nulla lacus, sit amet dapibus augue mollis vitae. Suspendisse a porta ante. Duis arcu dui, porta at semper id, commodo ut arcu. Proin consectetur bibendum orci, sed condimentum justo placerat ut. Donec blandit, turpis a porttitor finibus.                <br/>                <br/>            Donec ultricies vehicula porttitor. Maecenas at lorem vitae metus tincidunt fermentum. Etiam sagittis gravida lorem, eu scelerisque leo porta eleifend. Mauris at lectus eu neque egestas convallis. Ut vitae rutrum purus. Duis quis turpis ut lectus auctor vestibulum cursus quis justo. Donec rhoncus mi metus. Aliquam dictum odio id iaculis tristique. Nullam egestas vestibulum sem sollicitudin efficitur.     
            </div>
            
        </div>
        <div class="arrowLeft">
            <img src="/Images/arrowLeft.png" onclick="$.fancybox.close()"/>
        </div>
        <div class="clearfix"></div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".fancybox").fancybox({
                helpers: {
                    overlay: {
                        css: {
                            'background': 'rgba(255, 255, 255, 0)'
                        }
                    }
                },
                padding: 0,
                closeBtn:false
            });
        });
</script>
</asp:Content>
