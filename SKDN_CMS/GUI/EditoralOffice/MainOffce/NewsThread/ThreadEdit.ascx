<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThreadEdit.ascx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.NewsThread.ThreadEdit" %>
<link rel="stylesheet" type="text/css" href="/styles/Newsedit.css" />
<link href="/styles/common.css" rel="stylesheet" type="text/css" />
<link href="/styles/Core.css" rel="stylesheet" type="text/css" />
<table width="100%">
    <tr>
        <td class="Edit_Head_Cell">
            Cập nhật chủ đề</td>
    </tr>
</table>

<table border="0" cellpadding="0" cellspacing="5" width="100%">
    <tr>
        <td class="ms-input" valign="top" style="width: 274px" >
            Tên chủ đề</td>
        <td class="ms-input">
            &nbsp;<asp:TextBox CssClass="ms-long" ID="txtThreatTitle" runat="server"></asp:TextBox>
            </td>
    </tr>
    <tr>
        <td class="ms-input" valign="top" style="width: 274px" >
            chủ đề tiêu điểm</td>
        <td>
            <asp:CheckBox ID="chkIsFocus" runat="server" CssClass="ms-input" /></td>
    </tr>    
    <tr>
        <td valign="top" class="ms-input" style="width: 274px" >
            Logo chủ đề</td>
        <td>
            &nbsp;<asp:FileUpload ID="fuLogo" runat="server" CssClass="ms-long" /></td>
    </tr>
    <tr>
        <td valign="top" class="ms-input" style="width: 274px">
            Quan hệ chủ đề</td>
        <td class="ms-input">
            &nbsp;<asp:LinkButton ID="lbChonChuDe" runat="server">Chọn chủ đề</asp:LinkButton><br />
            &nbsp;<asp:ListBox ID="listThread" runat="server" CssClass="ms-long" SelectionMode="Multiple" Height="125px" DataTextField="Title" DataValueField="Thread_ID"></asp:ListBox>
            <input id="Button1" onclick="RemoveNew('<%= listThread.ClientID %>','<%= threadIDs.ClientID %>',false);" type="button" value="Gỡ " /></td>
    </tr>
    <tr>
        <td class="ms-input" valign="top" style="width: 274px">
            Quan hệ danh mục</td>
        <td class="ms-input">
            &nbsp;<asp:LinkButton ID="lbDanhMuc" runat="server">Chọn danh mục</asp:LinkButton>&nbsp;<br />
            &nbsp;<asp:ListBox ID="listCat" runat="server" CssClass="ms-long" SelectionMode="Multiple" Height="125px" DataTextField="Cat_Name" DataValueField="Cat_ID"></asp:ListBox>
            <input id="Button2" onclick="RemoveNew('<%= listCat.ClientID %>','<%= catIDs.ClientID %>',true);" type="button" value="Gỡ " /></td>
    </tr>
    <tr>
        <td align="center" class="Edit_Foot_Cell" colspan="2">
            &nbsp;
            <asp:Button ID="btnXuatBan" runat="server" CssClass="ms-input" OnClick="txtSave_Click" Text="Cập nhật" CommandName="XuatBan" />&nbsp;
            <asp:Button ID="btnBack" runat="server" CssClass="ms-input" OnClick="btnBack_Click" Text="Quay về" CommandName="XuatBan" /></td>
    </tr>
</table>
<asp:HiddenField runat="server" ID="threadIDs" Value="" />
<asp:HiddenField runat="server" ID="catIDs" Value="" />
<script type="text/javascript">

function bindItem(control,items,hid,iscat)
{    
    if(items == '')return;
    var ite = items.split(',');    
    var text;var value;
    var it;
    var op;
    for(var i =0;i<ite.length;i++)
    {
        it = ite[i].split('#');
        text = it[1];
        value = it[0];        
        op = document.createElement("option");
        op.text = text;
        op.value = value;
        try
        {
            document.getElementById(control).add(op,null);
        }
        catch(E)
        {            
            document.getElementById(control).add(op);
        }
    }
    
    BindDataToHiddenField(control,hid,iscat);
    //alert(document.getElementById(hid).value);
    var ids = document.getElementById(hid).value;
            if(iscat)
        {
            document.getElementById('<%= lbDanhMuc.ClientID %>').onclick = function()
            {
                
                openpreview("/ListCatThread.aspx?Hid=<%= catIDs.ClientID %>&Control=<%=listCat.ClientID %>&IsCat=true&ID="+ ids,500,500) ;return false;
            };
        }
        else        
        {
            document.getElementById('<%= lbChonChuDe.ClientID %>').onclick = function()
            {
                openpreview("/ListCatThread.aspx?Hid=<%= threadIDs.ClientID %>&Control=<%=listThread.ClientID %>&IsCat=false&ID=" + ids,500,500) ;return false;
            };
        }
}

    function BindDataToHiddenField(cbo,hid,iscat)
    {
        try
        {
            var ctr = document.getElementById(cbo); //GetControlByName(cbo);        
            var hidCtr = document.getElementById(hid); //GetControlByName(hid); 
            var str = "";
            hidCtr.value = "";       
            for (var i = 0 ; i < ctr.length; i++)
            {
                str += ctr.options[i].value + ",";
            }                      
            if (str.length >= 2)
            {            
                str = str.substr(0,str.length - 1);            
            }
            hidCtr.value = str;
            //alert(str);
        }
        catch(E)
        {
            //alert(E.messge)
        }
    }
    
    function RemoveNew(id,hid,iscat)
    {
        
        //CloseFormMedia();
        //CloseFormImage();
        var ctr = document.getElementById(id); //GetControlByName(id);
        var hidCtr = document.getElementById(hid); //GetControlByName(hid);        
        hidCtr.value = "";
        if (ctr.selectedIndex != -1)
        {
            ctr.remove(ctr.selectedIndex);
            //if (ctr.length > 0)ctr.options[0].selected = true;
        }
        else if (ctr.length > 0)
        {           
                alert("Ban chua chon ban tin de xoa");
                ctr.options[0].selected = true;
                ctr.focus();         
        }              
        
        BindDataToHiddenField(id,hid,iscat);
        if(iscat)
        {
            document.getElementById('<%= lbDanhMuc.ClientID %>').onclick = function()
            {
                openpreview("/ListCatThread.aspx?Hid=<%= catIDs.ClientID %>&Control=<%=listCat.ClientID %>&IsCat=true&ID=" + hidCtr.value,500,500) ;return false;
            };
        }
        else        
        {
            document.getElementById('<%= lbChonChuDe.ClientID %>').onclick = function()
            {
                openpreview("/ListCatThread.aspx?Hid=<%= threadIDs.ClientID %>&Control=<%=listThread.ClientID %>&IsCat=false&ID=" + hidCtr.value,500,500) ;return false;
            };
        }
    }
</script>