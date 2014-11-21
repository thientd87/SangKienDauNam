<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListThread.ascx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.NewsThread.ListThread" %>
<link rel="stylesheet" href="/styles/Portal.css" type="text/css">
<link rel="stylesheet" type="text/css" href="/Styles/Core.css" />
<link rel="stylesheet" type="text/css" href="/Styles/Newsedit.css" />

<table width="100%">
    <tr>
        <td class="Edit_Head_Cell">
            <asp:Label ID="lblLabel" runat="server" Text="Danh sách chủ đề"></asp:Label></td>
    </tr>    
</table>
<br />
<table width="100%">
    <tr>
        <td></td>
        <td class="ms-formbody" width="150px" align="right" valign="middle">
           <b>Chọn danh mục:</b>
        </td>
        <td class="ms-formbody" width="150px" align="right" valign="middle">
             <asp:DropDownList ID="cboCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboCategory_SelectedIndexChanged1"> </asp:DropDownList>   
        </td>
    </tr>
</table>
<br />

<asp:GridView ID="gvData" runat="server"  AutoGenerateColumns="False" HeaderStyle-CssClass="grdHeader"
    PageSize="12" RowStyle-CssClass="grdItem" EmptyDataText="<span style='color:Red'><b>Không có dữ liệu !</b></span>" AlternatingRowStyle-CssClass="grdAlterItem" Width="100%" OnRowCommand="gvData_RowCommand">
    <PagerSettings Visible="true" />
    <Columns>
         <asp:TemplateField>
            <ItemStyle Width="10px" HorizontalAlign="Center" CssClass="ms-formbody" />
            <ItemTemplate>
                <input type="checkbox" id="chkSelect<%#DataBinder.Eval(Container.DataItem,"Thread_ID")%>" name="chkSelect" value="<%#DataBinder.Eval(Container.DataItem,"Thread_ID")%>" onclick="Check('<%#DataBinder.Eval(Container.DataItem,"Thread_ID")%>','<%#DataBinder.Eval(Container.DataItem,"Title")%>')" />
                <input type="hidden" id="hid" name="hid<%#DataBinder.Eval(Container.DataItem,"Thread_ID")%>" value="<%#DataBinder.Eval(Container.DataItem,"Title")%>" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Title" HeaderText="T&#234;n chủ đề ">
            <ItemStyle />
        </asp:BoundField>
    </Columns>
    <RowStyle CssClass="grdItem" />
    <HeaderStyle CssClass="grdHeader" />
    <AlternatingRowStyle CssClass="grdAlterItem" />
</asp:GridView>

<table width="100%">
    <tr>
        <td align="right"  >
            <a href="javascript:void(0)" onclick="Assign();" class="ms-formbody">Thêm vào bài</a>
        </td>
    </tr>
</table>

<script language="javascript">
    var strThread_ID = "";
    var strThread_Title = "";
    function Check(strId, strTitle)
    {   
       
        if(document.getElementById('chkSelect'+strId).checked)
        {
             strThread_ID = strThread_ID + "," + strId;
             strThread_Title = strThread_Title + "|" + strTitle;
        }
        else
        {
            strThread_ID = strThread_ID.replace(","+strId,"");
            strThread_Title = strThread_Title.replace("|"+strTitle,"");
        }
    }
    function Assign()
    {
        var str_new_thread = "";
        var hidControlId =  window.opener.document.getElementById('<%=Request.QueryString["hidControlId"].ToString()%>');
              
        if(strThread_Title != "")
            strThread_Title = strThread_Title.substring(1);
            
         str_new_thread = strThread_ID;
         if(str_new_thread != "")
            str_new_thread = str_new_thread.substring(1);
            
         if(strThread_ID != "" && hidControlId.value == "")
            strThread_ID = strThread_ID.substring(1);
            
        hidControlId.value += strThread_ID;
        
        if(strThread_ID != "")
            window.opener.AddThread(str_new_thread,strThread_Title);
            
        window.close();
        
        return false;
    }
</script>
