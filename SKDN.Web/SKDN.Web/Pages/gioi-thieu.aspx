<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="gioi-thieu.aspx.cs" Inherits="SKDN.Web.Pages.gioi_thieu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Scripts/fancybox/jquery.fancybox.css" rel="stylesheet" />
    <script src="/Scripts/fancybox/jquery.fancybox.js"></script>
    <div id="divBanner">
        <a class="fancybox" href="#divChungToi"><img style="padding-left: 35px" src="/Images/bannerChungToi.jpg"/></a>
        <a class="fancybox" href="#divNhaTaiTro"><img style="padding-left: 6px" src="/Images/bannerNhaTaiTro.jpg"/></a>
        <a class="fancybox" href="#divSuMenh"><img style="padding-left: 6px" src="/Images/bannerSuMenh.jpg"/></a>
    </div>
    <div id="divChungToi">
        <img src="/Images/imgChungToi.jpg" style=" float: left"/>
        <div class="text-chung-toi">
            <div class="col1">
                <b style="font-family: Segoe UI Bold">Sáng Kiến Đầu Năm</b> là sự kiện quốc gia được thực hiện giữa sự hợp tác của Đài truyền hình Việt Nam và Tạp Chí Cộng Sản nhằm tìm kiếm ý tưởng từ người dân cả nước ở mọi lứa tuổi giúp thay đổi đất nước, xây dựng hình ảnh Việt Nam trở nên văn minh, hiện đại và thân thiện hơn về các lĩnh vực: Văn Hóa – Xã hội, Du lịch hay Giao Thông…
            </div>
            
        </div>
        <div class="arrowLeft">
            <img src="/Images/arrowLeft.png" onclick="$.fancybox.close()"/>
        </div>
        <div class="clearfix"></div>
    </div>
     <div id="divNhaTaiTro">
         <div class="arrowRight">
            <img src="/Images/arrowRight.png" onclick="$.fancybox.close()"/>
        </div>
        <%--<img src="/Images/imgChungToi.jpg" style=" float: left"/>--%>
        <div class="text-chung-toi">
            Đang cập nhật
        </div>
        <div class="arrowLeft">
            <img src="/Images/arrowLeft.png" onclick="$.fancybox.close()"/>
        </div>
        <div class="clearfix"></div>
1    </div>
     <div id="divSuMenh">
         <div class="arrowRight">
            <img src="/Images/arrowRight.png" onclick="$.fancybox.close()"/>
        </div>
        <%--<img src="/Images/imgChungToi.jpg" style=" float: left"/>--%>
        <div class="text-chung-toi">
           Đang cập nhật
            
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
                closeBtn: false,
                afterShow: function () {
                  //  alert($("#divBanner").offset().top);
                   
                }
            });
        });
</script>
    <style>
        .fancybox-wrap{top:140px !important}
    </style>
</asp:Content>
