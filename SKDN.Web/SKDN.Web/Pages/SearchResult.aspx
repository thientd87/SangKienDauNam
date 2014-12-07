<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchResult.aspx.cs" Inherits="SKDN.Web.Pages.SearchResult" %>

<%@ Register Src="~/UserControls/FilterBar.ascx" TagPrefix="uc1" TagName="FilterBar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <script src="/Scripts/jscroll/jquery.mousewheel-3.1.3.js"></script>
    <script src="/Scripts/jscroll/jquery.scrollpanel-0.5.0.js"></script>
    <link href="/Scripts/fancybox/jquery.fancybox.css" rel="stylesheet" />
    <script src="/Scripts/fancybox/jquery.fancybox.js"></script>
    <div class="row">
        <uc1:FilterBar runat="server" id="FilterBar" />
    </div>
    
    <div class="list-project">
        <asp:Repeater runat="server" ID="rptListProject">
            <ItemTemplate>
                <div class="project-item">
                    <%#Eval("Image") %>
                    <a href="/ProjectDetailAjax.aspx?News_ID=<%#Eval("Id") %>" onclick="return  false" class="project-item-title fancybox" ><%#Eval("ProductName") %></a>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                <div class="clearfix"></div>
            </FooterTemplate>
        </asp:Repeater>
       
    </div>

    <script type="text/javascript">

        function DropDown(el) {
            this.dd = el;
            this.initEvents();
        }
        DropDown.prototype = {
            initEvents: function () {
                var obj = this;

                obj.dd.on('click', function (event) {
                    $(this).toggleClass('active');
                    event.stopPropagation();
                });
            }
        }

        $(function () {

            var dd = new DropDown($('.wrapper-dropdown-2'));

            $(document).click(function () {
                // all dropdowns
                $('.wrapper-dropdown-2').removeClass('active');
            });

        });

		</script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.content-project').scrollpanel({

            });

            $(".project-item  > a").fancybox({
                type: 'ajax',
                ajax: {
                    type: "GET"
                },
                afterShow: function () {

                    FB.XFBML.parse(document.body);
                }
            });
        });

    </script>
     <style>
         .fancybox-skin{background: transparent !important}
         .fancybox-inner{ width: 1300px !important; height: 450px !important}
          .fancybox-inner .col-Content{ color: #fff;height: 380px;width: 730px; font: normal 15px/20px Segoe UI}
          
          .fancybox-inner .projectContent{height: 400px}
          .fancybox-inner .comment-wrapper{height: 380px}
          .fancybox-overlay{background:rgba(0,0,0,0.9);}
     </style>
</asp:Content>