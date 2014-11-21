<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="Main.ascx.cs" Inherits="DFISYS.GUI.Users.Main"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="100%" cellpadding="0" cellspacing="0" border="0" style="margin-top: 10px">
    <tr>
        <td>
            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td width="232" style="vertical-align: top" valign="top">
                        <aside class="grid_3 pull_9" id="sidebar">
			 
			<div class="box menu">
                <h2>Quản lý người dùng<img class="toggle" src="images/icons/arrow_state_grey_expanded.png"></h2>
				        <section>
					        <ul>
						        <li><asp:HyperLink ID="lnkUsers" runat="server">Tài khoản</asp:HyperLink></li>
						        <li><asp:HyperLink ID="lnkDefault" runat="server">Xác lập quyền mặc định</asp:HyperLink></li>
						       
					        </ul>
				        </section>
			        </div>			 
		        </aside>
                    </td>
                    <td valign="top" class="Content_OutBox">
                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td class="Content_BoxArea">
                                    <section class="grid_9 push_3" id="main">
			                        <article style="float: left;width: 98%;">
                                      <!--Grid content here-->
                                      <asp:PlaceHolder ID="Container" runat="server"></asp:PlaceHolder>
                                      <!--/end of grid -->
                                      <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                      </article>
                                    </section>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
