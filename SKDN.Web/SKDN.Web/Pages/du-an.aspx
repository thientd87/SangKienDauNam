<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="du-an.aspx.cs" Inherits="SKDN.Web.Pages.du_an" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <script src="/Scripts/jscroll/jquery.mousewheel-3.1.3.js"></script>
    <script src="/Scripts/jscroll/jquery.scrollpanel-0.5.0.js"></script>
    <div class="row">
        <div class="filterbar">
            <div class="iconDuAnTieuBieu"><img src="/Images/iconDuAnTieuBieu.png"/></div>
            <div id="ddSubject" class="wrapper-dropdown-2" tabindex="1">Chọn chủ đề----------
			    <ul class="dropdown">
				    <li><a href="#">Twitter</a></li>
				    <li><a href="#">Github</a></li>
				    <li><a href="#">Facebook</a></li>
			    </ul>
		    </div>
            <div id="ddTimeOrder" class="wrapper-dropdown-2" tabindex="1">Thời gian----------
			    <ul class="dropdown">
				    <li><a href="#">Mới nhất</a></li>
				    <li><a href="#">Cũ nhất</a></li>
				    
			    </ul>
		    </div>
            <div class="clearfix"></div>
        </div>
        <div class="projectContent">
            <div class="col-Content">
                <img src="/Images/imgeDuAnAvatar.jpg" class="avatarDuAn"/>
                <div class="content-project  scrollpanel no4">
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut elementum, nisl sit amet auctor dapibus, eros arcu aliquet diam, ac hendrerit magna augue eu turpis. Nunc rutrum nulla lacus, sit amet dapibus augue mollis vitae.                    <br/>Donec ultricies vehicula porttitor. Maecenas at lorem vitae metus tincidunt fermentum. Etiam sagittis gravida lorem, eu scelerisque leo porta eleifend. Mauris at lectus eu neque egestas convallis.
                    <br/>
                     Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut elementum, nisl sit amet auctor dapibus, eros arcu aliquet diam, ac hendrerit magna augue eu turpis. Nunc rutrum nulla lacus, sit amet dapibus augue mollis vitae.                    <br/>Donec ultricies vehicula porttitor. Maecenas at lorem vitae metus tincidunt fermentum. Etiam sagittis gravida lorem, eu scelerisque leo porta eleifend. Mauris at lectus eu neque egestas convallis.
                    <br/>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut elementum, nisl sit amet auctor dapibus, eros arcu aliquet diam, ac hendrerit magna augue eu turpis. Nunc rutrum nulla lacus, sit amet dapibus augue mollis vitae.                    <br/>Donec ultricies vehicula porttitor. Maecenas at lorem vitae metus tincidunt fermentum. Etiam sagittis gravida lorem, eu scelerisque leo porta eleifend. Mauris at lectus eu neque egestas convallis.
                    <br/>
                     Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut elementum, nisl sit amet auctor dapibus, eros arcu aliquet diam, ac hendrerit magna augue eu turpis. Nunc rutrum nulla lacus, sit amet dapibus augue mollis vitae.                    <br/>Donec ultricies vehicula porttitor. Maecenas at lorem vitae metus tincidunt fermentum. Etiam sagittis gravida lorem, eu scelerisque leo porta eleifend. Mauris at lectus eu neque egestas convallis.
                    <br/>
                </div>
            </div>
            <div class="col-Comment">
                <div class="comment-wrapper">
                    <fb:comments href="<% = Request.Url.DnsSafeHost+ Request.RawUrl%>" width="520" numposts="100" colorscheme="light"></fb:comments>
                </div>
            </div>
            <div class="clearfix"></div>
        </div>
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

        });

    </script>
</asp:Content>

