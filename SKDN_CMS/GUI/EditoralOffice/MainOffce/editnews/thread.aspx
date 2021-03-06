<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="thread.aspx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.editnews.thread" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Danh sách chủ đề</title>
    <link href="/Styles/theme/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <section id="main" class="grid_9 push_3" style="width: 96%; margin: 5px">
			<article style="padding: 10px 20px">
		<h1><asp:Label ID="lblLabel" runat="server" Text="Danh sách chủ đề"></asp:Label></h1>
		 
		<table width="100%" class="gtable sortable">
			<tr>				 
				<td width="100px" align="left" valign="middle">
					<b>Chọn danh mục:</b>
				</td>
				<td>
					<asp:DropDownList ID="cboCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboCategory_SelectedIndexChanged1">
					</asp:DropDownList>
				</td>
			</tr>
             <tr>
         
            <td>
					<b>Tìm theo từ khóa:</b>
				</td>
                    <td>                        
                        <asp:TextBox ID="txtKeyword" runat="server" CssClass="big"></asp:TextBox>&nbsp;
                        <asp:Button ID="btnSearch" runat="server" Text="Tìm kiếm" CssClass="btnUpdate" OnClick="btnSearch_Click" />
                    </td>
                 
       
    </tr>
			
		</table>
		<br />
        <asp:GridView Width="100%" ID="grdThreadList" ShowFooter="false" runat="server"  CssClass="gtable sortable"  AutoGenerateColumns="false"
                            AllowPaging="true" DataSourceID="objThreadlistSource" PageSize="12">
                            <Columns>
				                <asp:TemplateField>
					                <ItemStyle Width="10px" HorizontalAlign="Center" CssClass="ms-formbody" />
					                <ItemTemplate>
						                <input type="checkbox" style="vertical-align: middle" id="chkSelect<%#DataBinder.Eval(Container.DataItem,"Thread_ID")%>"
							                name="chkSelect" value="<%#DataBinder.Eval(Container.DataItem,"Thread_ID")%>"
							                onclick="Check('<%#DataBinder.Eval(Container.DataItem,"Thread_ID")%>','<%#DataBinder.Eval(Container.DataItem,"Title")%>')" />
						                <input type="hidden" style="vertical-align: middle" id="hid" name="hid<%#DataBinder.Eval(Container.DataItem,"Thread_ID")%>"
							                value="<%#DataBinder.Eval(Container.DataItem,"Title")%>" />
					                </ItemTemplate>
				                </asp:TemplateField>
				                <asp:BoundField DataField="Title" HeaderText="T&#234;n chủ đề ">
					                <ItemStyle />
				                </asp:BoundField>
			                </Columns>
                            <RowStyle CssClass="grdItem" />
                            <HeaderStyle CssClass="grdHeader" />
                            <AlternatingRowStyle CssClass="grdAlterItem" />
                            <PagerSettings Visible="false" />
                        </asp:GridView>



	  <div style="position: fixed; bottom: 0; left: 0; width: 100%; padding:5px 0; background-color: #EFEFEF;">
		  &nbsp; Xem trang&nbsp;<asp:DropDownList ID="cboPage" runat="Server" DataTextField="Text"
                            DataValueField="Value" AutoPostBack="true" DataSourceID="objdspage" OnSelectedIndexChanged="cboPage_SelectedIndexChanged"
                            CssClass="ms-input">
                        </asp:DropDownList>
                        
                 
					<a href="javascript:void(0)" onclick="bind_thread();" style="float: right;" class="btnUpdate">Thêm vào bài</a>
			 
        </div>

<asp:ObjectDataSource ID="objdspage" runat="server" SelectMethod="getPage" TypeName="ThreadManagement.BO.ThreadHelper"
    OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:ControlParameter DefaultValue="0" ControlID="grdThreadList" Name="numPage" PropertyName="PageCount"
            Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

<asp:ObjectDataSource ID="objThreadlistSource" runat="server" SelectMethod="GetThreadlist"
    SelectCountMethod="GetThreadRowsCount"  TypeName="ThreadManagement.BO.ThreadHelper" EnablePaging="true"
    MaximumRowsParameterName="PageSize" StartRowIndexParameterName="StartRow">
    <SelectParameters>
        <asp:Parameter Name="strWhere" DefaultValue="" Type="string" />
    </SelectParameters>
   
</asp:ObjectDataSource>

<script type="text/javascript">
function bind_thread()
{
	var gvData = document.getElementById('<%=grdThreadList.ClientID %>');
	var trs = gvData.getElementsByTagName('tr');
	var i, count = trs.length;
	var chk, hdd;
	var arr = new Array();
	for (i=1; i<count; i++)
	{
		chk = trs[i].getElementsByTagName('input')[0];
		if (chk.checked)
			arr.push(new Array(chk.value, trs[i].getElementsByTagName('input')[1].value));
	}
	opener.<%=Request.QueryString["function"] %>(arr);
	close();
}
</script>

		<script language="javascript">
		    var strThread_ID = "";
		    var strThread_Title = "";
		    function Check(strId, strTitle) {

		        if (document.getElementById('chkSelect' + strId).checked) {
		            strThread_ID = strThread_ID + "," + strId;
		            strThread_Title = strThread_Title + "|" + strTitle;
		        }
		        else {
		            strThread_ID = strThread_ID.replace("," + strId, "");
		            strThread_Title = strThread_Title.replace("|" + strTitle, "");
		        }
		    }
		    function Assign() {


		    }
		</script>
        </article></section>
    </form>
</body>
</html>
