<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FilterBar.ascx.cs" Inherits="SKDN.Web.UserControls.FilterBar" %>
<div class="filterbar">
    <div class="iconDuAnTieuBieu"><a href="/du-an.htm"><img src="/Images/iconDuAnTieuBieu.png"/></a></div>
    <div id="ddSubject" class="wrapper-dropdown-2" tabindex="1">Chọn chủ đề----------
	    <asp:Repeater runat="server" ID="rptSubject">
	        <HeaderTemplate>
	            <ul class="dropdown">
	        </HeaderTemplate>
	        <ItemTemplate>
			   <li><a href="<%#Eval("Cat_URL") %>"><%#Eval("Product_Category_Name") %></a></li>
	        </ItemTemplate>
            <FooterTemplate>
                 </ul>
            </FooterTemplate>
	    </asp:Repeater>
	</div>
    <div id="ddTimeOrder" class="wrapper-dropdown-2" tabindex="1">Thời gian----------
		<ul class="dropdown">
			<li><a href="/projects/moi-nhat.htm">Mới nhất</a></li>
			<li><a href="/projects/cu-nhat.htm">Cũ nhất</a></li>
				    
		</ul>
	</div>
    <div class="clearfix"></div>
</div>