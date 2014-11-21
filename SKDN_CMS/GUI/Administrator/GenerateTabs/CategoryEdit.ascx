<%@ Register TagPrefix="portal" assembly="Portal.API" Namespace="Portal.API.Controls" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CategoryEdit.ascx.cs" Inherits="Portal.GUI.Administrator.GenerateTabs.CategoryEdit" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>

<link href="/styles/backend_menu.css" rel="stylesheet" type="text/css" />
<table cellSpacing="0" cellPadding="0" width="100%" border="0" class="CategoryEdit_Table">
	<tr>
		<td class="CategoryEdit_Header" colspan="2">Thông tin chuyên mục</td>
	</tr>
	<tr>
		<td class="CategoryEdit_Label">Tên chuyên mục</td>
		<td class="CategoryEdit_Data"><asp:textbox id="txtCategoryName" MaxLength="255" Runat="server"></asp:textbox></td>
	</tr>
	<tr>
		<td class="CategoryEdit_Label">Mô tả cho chuyên mục</td>
		<td class="CategoryEdit_Data"><asp:textbox id="txtCategoryDescription" Runat="server" TextMode="MultiLine" Height="100" Width="400"></asp:textbox></td>
	</tr>
	<tr>
		<td class="CategoryEdit_Label">Hiển thị trên đường dẫn</td>
		<td class="CategoryEdit_Data"><asp:textbox id="txtCategoryDisplayURL" Runat="server"></asp:textbox></td>
	</tr>
	<tr>
		<td class="CategoryEdit_Label">Chuyên mục cha</td>
		<td class="CategoryEdit_Data"><asp:DropDownList ID="cboCat" runat="server" onchange="OnSelectCategory(this);"></asp:DropDownList></td>
	</tr>
    <tr>
        <td class="CategoryEdit_Label">
            Nhóm chuyên mục</td>
        <td class="CategoryEdit_Data">
            <asp:DropDownList ID="drEditionType" runat="server">
            </asp:DropDownList></td>
    </tr>
	<tr>
		<td class="CategoryEdit_Label">Tên trang chủ</td>
		<td class="CategoryEdit_Data">Home</td>
	</tr>
	<tr>
		<td class="CategoryEdit_Label">Icon đại diện</td>
		<td class="CategoryEdit_Data"><asp:FileUpload runat="server" ID="fldCategoryIcon" /></td>
	</tr>
	<tr>
		<td class="CategoryEdit_Data"><asp:CheckBox  id="chkIsColumn" Runat="server" Text="Là chuyên mục đặc biệt?"></asp:CheckBox></td>
		<td class="CategoryEdit_Data"><asp:Checkbox id="chkIsHidden" Runat="server" Text="Ẩn chuyên mục ?"></asp:CheckBox></td>
	</tr>
	<tr>
		<td colSpan="2" class="CategoryEdit_Data">
		    <input type="hidden" id="hdCatID" runat="server" />
		    <asp:Button ID="btnSave" runat="server" Text="Lưu lại" OnClick="btnSave_Click" />
		    <asp:Button ID="btnUpdate" runat="server" Text="Lưu lại" OnClick="btnUpdate_Click"/>
		    <asp:Button ID="btnCancel" runat="server" Text="Quay lại" OnClick="btnCancel_Click" />
		</td>
	</tr>
</table>
<asp:ObjectDataSource ID="objCatSource" runat="server" InsertMethod="CreateCat" UpdateMethod="UpdateCate" TypeName="Portal.BO.Editoral.Category.CategoryHelper">
<InsertParameters>
    <asp:ControlParameter ControlID="txtCategoryName" DefaultValue="" Name="_catname" PropertyName="Text" Type="String" />
    <asp:ControlParameter ControlID="txtCategoryDescription" Name="_catdes" PropertyName="Text" Type="String" />
    <asp:ControlParameter Name="_caturl" Type="String" ControlID="txtCategoryDisplayURL" PropertyName="Text"/>
    <asp:ControlParameter ControlID="cboCat" DefaultValue="0" Name="_catparent" PropertyName="SelectedValue" Type="Int32" />
    
    <asp:ControlParameter ControlID="drEditionType" Name="_catedition" PropertyName="SelectedValue" Type="Int16" />
    
    <asp:ControlParameter ControlID="fldCategoryIcon" Name="_caticon" PropertyName="FileName" Type="String" DefaultValue=""/>
    <asp:ControlParameter ControlID="chkIsColumn" Name="_catiscolumn" PropertyName="Checked" Type="Boolean" />
    <asp:ControlParameter ControlID="chkIsHidden" Name="_catishide" PropertyName="Checked" Type="Boolean" />
</InsertParameters>
<UpdateParameters>
    <asp:ControlParameter ControlID="hdCatID" DefaultValue="" Name="_catid" PropertyName="Value" Type="int32" />
    <asp:ControlParameter ControlID="txtCategoryName" DefaultValue="" Name="_catname" PropertyName="Text" Type="String" />
    <asp:ControlParameter ControlID="txtCategoryDescription" Name="_catdes" PropertyName="Text" Type="String" />
    <asp:ControlParameter Name="_caturl" Type="String" ControlID="txtCategoryDisplayURL" PropertyName="Text"/>
    <asp:ControlParameter ControlID="cboCat" DefaultValue="0" Name="_catparent" PropertyName="SelectedValue" Type="Int32" />
    
    <asp:ControlParameter ControlID="drEditionType" Name="_catedition" PropertyName="SelectedValue" Type="Int16" />
    
    <asp:ControlParameter ControlID="fldCategoryIcon" Name="_caticon" PropertyName="FileName" Type="String" DefaultValue=""/>
    <asp:ControlParameter ControlID="chkIsColumn" Name="_catiscolumn" PropertyName="Checked" Type="Boolean" />
    <asp:ControlParameter ControlID="chkIsHidden" Name="_catishide" PropertyName="Checked" Type="Boolean" />
</UpdateParameters>
</asp:ObjectDataSource>
&nbsp;&nbsp;

<script type="text/javascript">
    function OnSelectCategory(cb)
    {   
        if(cb.options[cb.selectedIndex].text.indexOf("→") >= 0)
        {
            document.getElementById('<%= drEditionType.ClientID %>').disabled = "disabled";
        }
        else
        {
            document.getElementById('<%= drEditionType.ClientID %>').disabled = "";
        }
    }
    OnSelectCategory(document.getElementById('<%= cboCat.ClientID %>'));
</script>

