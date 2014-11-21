<%@ Control Language="c#" AutoEventWireup="True" Codebehind="SearchFull.ascx.cs" Inherits="DFISYS.GUI.Share.SearchInputFull.SearchFull" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<script language="javascript">
	var objTextBox;
	var objChkDate;
	var cmbNgay;
	var cmbThang;
	var cmbNam;
	var cmbCate;
	function CheckValid()
	{
		if (cmbCate.value == "0")
		{
			alert('Chuyên m?c không ???c ch?n');
			return false;
		} 
		if (objChkDate.checked)
		{
			if (cmbNgay.value == "Ngày")
			{
				alert('Ngày tìm ki?m không ?úng ??nh d?ng');
				return false;
			}
			if (cmbThang.value == "Tháng")
			{
				alert('Tháng tìm ki?m không ?úng ??nh d?ng');
				return false;
			}
			if (cmbNam.value == "N?m")
			{
				alert('N?m tìm ki?m không ?úng ??nh d?ng');
				return false;
			}
		}
		if (objTextBox.value == "")
		{
			alert('Nh?p khóa tìm ki?m');
			return false;
		} else return true;
	}
</script>
<link href="Styles/Search.css" type="text/css" rel="stylesheet">
<fieldset class="SearchFull_Panel"><legend class="SearchFull_Header">Tìm ki?m nâng cao</legend>
	<table class="SearchFull_Table" cellSpacing="0" cellPadding="0" width="100%" align="center">
		<tr>
			<td>
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TBODY>
						<tr>
							<td class="SearchFull_Mode" colSpan="2">Ch? ?? tìm</td>
						</tr>
						<tr>
							<td class="SearchFull_Mode" colSpan="2" style="HEIGHT: 10px">Chuyên m?c&nbsp;&nbsp;
							<asp:dropdownlist id="cmbCategories" Runat="server" Font-Name="Verdana" Font-Size=11px></asp:dropdownlist>
							</td>
						</tr>
						<tr>
							<td width="50%"><asp:radiobutton id="rdadvanceword" CssClass="labelTextSearch" Runat="server" Checked="True" GroupName="search"
									Text="Tìm theo c?m t?"></asp:radiobutton></td>
							<td width="50%"><asp:radiobutton id="rdnewFocus" CssClass="labelTextSearch" Runat="server" GroupName="search" Text="Tìm trong nh?ng tin tâm ?i?m"></asp:radiobutton></td>
						</tr>
						<tr>
							<td><asp:radiobutton id="rdnewshot" CssClass="labelTextSearch" Runat="server" GroupName="search" Text="Tìm trong nh?ng tin nóng"></asp:radiobutton></td>
							<td><asp:radiobutton id="rdnewTopHot" CssClass="labelTextSearch" Runat="server" GroupName="search" Text="Tìm trong nh?ng tin nóng nh?t"></asp:radiobutton></td>
						</tr>
						<tr>
							<td>
								<table cellSpacing="0" cellPadding="0" width="100%" border="0">
									<tr>
										<td><asp:radiobutton id="rdDate" CssClass="labelTextSearch" Runat="server" GroupName="search" Text="Theo ngày tháng"></asp:radiobutton>&nbsp;
											<asp:dropdownlist id="cmbNgay" Runat="server"></asp:dropdownlist><asp:dropdownlist id="cmbThang" Runat="server"></asp:dropdownlist><asp:dropdownlist id="cmbNam" Runat="server"></asp:dropdownlist></td>
									</tr>
								</table>
							</td>
							<td vAlign="top"><asp:radiobutton id="rdnewHome" CssClass="labelTextSearch" Runat="server" GroupName="search" Text="Tìm trên trang ch?"></asp:radiobutton></td>
						</tr>
					</TBODY>
				</table>
			</td>
		</tr>
		<tr>
			<td height="50"><asp:label id="lblselect" CssClass="SearchFull_Field" Runat="server">Tìm ki?m&nbsp;&nbsp;</asp:label><asp:textbox id="txtSearch" CssClass="txtDetail" Runat="server" Width="150"></asp:textbox>&nbsp;
				<asp:button id="btnSearch" CssClass="SearchButton" Text="Search" runat="server" onclick="btnSearch_Click"></asp:button></td>
		</tr>
	</table>
</fieldset>
