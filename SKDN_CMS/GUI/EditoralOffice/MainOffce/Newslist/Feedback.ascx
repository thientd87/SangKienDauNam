<%@ Control Language="C#" AutoEventWireup="true" Codebehind="Feedback.ascx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.Newslist.Feedback" %>
 
<div id="feedbackform" class="popupborder" style="width:507px;">
	<div style="text-align: center; font-weight: bold; padding-bottom: 5px; color: Black;
		background-color: #d0d0d0;">
		Ghi chú khi trả bài</div>
	<div class="popupcontent">
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td height="10">
				</td>
			</tr>
			<tr>
				<td width="20%" class="ms-input">
					Tiêu đề :
				</td>
				<td width="80%">
					<asp:TextBox ID="txtTitle" runat="server" Width="300px" CssClass="ms-long"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td width="185" class="ms-input" valign="top">
					Nội Dung :
				</td>
				<td width="80%">
					<asp:TextBox ID="txtContent" runat="server" Width="300px" TextMode="MultiLine" Height="150px"
						CssClass="ms-long"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td width="20%" class="Form_lable_cell">
					<input type="hidden" id="hdID" runat="server" />
				</td>
				<td width="80%">
					<asp:Button ID="cmdYes" runat="server" Text="Đồng ý" OnClick="cmdYes_Click" OnClientClick="hideModalPopup('feedbackform'); return true;" CssClass="ms-input">
					</asp:Button>
					<input type="button" value="Huỷ lệnh" onclick="hideModalPopup('feedbackform'); return false;" class="ms-input" />
				</td>
			</tr>
		</table>
	</div>
	<asp:ObjectDataSource ID="objsoure" UpdateMethod="Update" runat="server" TypeName="ChannelVN.Comment.BO.Helper">
		<UpdateParameters>
			<asp:ControlParameter Name="commentID" Type="Int32" DefaultValue="-1" ControlID="hdCommentID"
				PropertyName="Value" />
			<asp:ControlParameter Name="user" Type="String" DefaultValue="" ControlID="txtUserSend"
				PropertyName="Text" />
			<asp:ControlParameter Name="email" Type="string" DefaultValue="" ControlID="txtEmail"
				PropertyName="Text" />
			<asp:ControlParameter Name="content" Type="string" DefaultValue="" ControlID="txtContent"
				PropertyName="Text" />
			<asp:ControlParameter Name="commenthay" Type="boolean" DefaultValue="false" ControlID="chkCommentHay"
				PropertyName="Checked" />
		</UpdateParameters>
	</asp:ObjectDataSource>
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
	<Triggers>
		<asp:AsyncPostBackTrigger ControlID="cmdYes" EventName="Click" />
	</Triggers>
</asp:UpdatePanel>
<asp:ObjectDataSource ID="objListActionSource" runat="server" UpdateMethod="UpdateFeedbackRow"
	TypeName="Portal.BO.Editoral.Newslist.NewslistHelper">
	<UpdateParameters>
		<asp:Parameter Name="_news_id" Type="string" />
		<asp:Parameter Name="_news_status" Type="int16" DefaultValue="5" />
		<asp:Parameter Name="_action_type" Type="int16" />
		<asp:ControlParameter Name="_action_title" ControlID="txtTitle" Type="string" PropertyName="Text" />
		<asp:ControlParameter Name="_action_content" ControlID="txtContent" Type="string"
			PropertyName="Text" />
	</UpdateParameters>
</asp:ObjectDataSource>

<script type="text/javascript">var hdID = document.getElementById('<%=hdID.ClientID %>');</script>

