<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="du-an.aspx.cs" Inherits="SKDN.Web.Pages.du_an" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
</asp:Content>
