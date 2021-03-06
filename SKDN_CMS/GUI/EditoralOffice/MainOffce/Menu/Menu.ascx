<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.Menu.menu" %>


        <!-- BEGIN SIDEBAR MENU -->
        <ul class="page-sidebar-menu">
            <asp:Literal ID="ltrHtml" runat="server"></asp:Literal>
             
             <li runat="server" id="aUser">
					<a href="javascript:;">
					<i class="icon-user"></i> 
					<span class="title">Quản lý người dùng</span>
					<span class="arrow  open"></span>
					</a>
					<ul class="sub-menu">
						<li><a href="/users.aspx"  visible="false">Danh sách người dùng</a></li>
						</ul>
				</li>
        </ul>
        <!-- END SIDEBAR MENU -->
   

<script language="javascript" type="text/javascript">
    $("img.toggle").click(function () {
        var parent = $(this).parent().parent();
        var section = parent.find("section");
        var display = $.cookie($(this).attr("id"));
        if (display == undefined) {
            display = "none";
        }
        display = display == "block" ? "none" : "block";
        section.css("display", display);

        $.cookie($(this).attr("id"), display, { expires: 10000 });
    });

    $(document).ready(function () {
        $("img.toggle").each(function (i, e) {
            var display = $.cookie($(this).attr("id"));
            var parent = $(this).parent().parent();
            var section = parent.find("section");
            section.css("display", display);
        });
    });
</script>
