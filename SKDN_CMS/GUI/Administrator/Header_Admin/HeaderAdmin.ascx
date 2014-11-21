<%@ Control Language="c#" AutoEventWireup="True" Codebehind="HeaderAdmin.ascx.cs" Inherits="DFISYS.GUI.Administrator.Header_Admin.HeaderAdmin" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<!-- BEGIN TOP NAVIGATION BAR -->
<div class="header navbar navbar-inverse navbar-fixed-top">
		<div class="navbar-inner">
			<div class="container-fluid">
				<!-- BEGIN LOGO -->
				<a class="brand" href="/office.aspx">
				<img src="/Styles/metronic/img/logo.png" alt="logo" /> CMS
				</a>
				<!-- END LOGO -->
				<!-- BEGIN TOP NAVIGATION MENU -->              
				<ul class="nav pull-right">
					<!-- BEGIN USER LOGIN DROPDOWN -->
                    <asp:Literal ID="ltrMenu" runat="server" Visible="true"></asp:Literal>
					<li class="dropdown user">
						<a href="#" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
						<img alt="" src="assets/img/avatar1_small.jpg" />
						<span class="username"><asp:Literal ID="ltrUser" runat="server"></asp:Literal></span>
						<i class="icon-angle-down"></i>
						</a>
						<ul class="dropdown-menu">
							<li><a href="/office/profile.aspx"><i class="icon-user"></i> My Profile</a></li>
							<li><asp:LinkButton ID="lbtLogout" runat="server" OnClick="lbtLogout_Click"><i class="icon-key"></i> Log Out</asp:LinkButton></li>
						</ul>
					</li>
					<!-- END USER LOGIN DROPDOWN -->
					<!-- END USER LOGIN DROPDOWN -->
				</ul>
				<!-- END TOP NAVIGATION MENU --> 
			</div>
		</div>
        </div>
		<!-- END TOP NAVIGATION BAR -->
<nav id="topmenu">
	
</nav>

 

