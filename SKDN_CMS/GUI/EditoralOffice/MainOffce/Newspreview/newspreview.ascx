<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="newspreview.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.Newspreview.preview" %>
<link href="/styles/NewsContent.css" rel="stylesheet" type="text/css">
<table cellpadding="0" cellspacing="0" border="0" class="NewsContent_Table" width="100%" id="tbNews" runat="server" visible=false>
	<tr>
		<td class="NewsContent_Title">
			<asp:Literal ID="ltrNewsTitle" Runat="server"></asp:Literal>
		</td>
	</tr>
	<tr>
		<td class="NewsContent_Detail">
			<table align="left" id="tblImg" runat="server">
				<tr>
					<td align="center" style="display: none;"><asp:Image ID="imgNewsAvatar" Runat="server"></asp:Image><br />
					<asp:Literal Runat="server" ID="ltrImageNote"></asp:Literal>
                    </td>
					<td class="NewsContent_Detail" style="padding-left: 10px;">
					<asp:Literal Runat="server" ID="ltrNewsInit"></asp:Literal>
					</td>
					<td>
					    <div id="divNewsRelation" runat="server" style="background-color:Olive;border-style:solid;border-width:1px;" >
            <div>Các bài liên quan</div>   
            <table cellpadding="0" cellspacing="0" width="100%">
                    <asp:DataList ID="dlNewsRelation" runat="server">
                        <ItemTemplate>    
                        <tr>
                            <td>
                               + <%# Eval("News_Title")%>                            
                            </td>    
                        </tr>
                        </ItemTemplate>
                    </asp:DataList></table>
            </div>
					</td>
				</tr>
			</table>					
		</td>
	</tr>
	<tr>
		<td class="NewsContent_Detail"><asp:Literal Runat="server" ID="ltrNewsDetail"></asp:Literal></td>
	</tr>
	<tr>
		<td></td>
	</tr>
</table>


