<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SumNews.ascx.cs" Inherits="DFISYS.GUI.EditoralOffice.MainOffce.OnLoad.UserControl.SumNews" %>
<style>
    .ContentBox
    {
        border:1px solid #b8c1ca;
	    border-top:none;
	    padding-left:8px;
	    visibility:visible;
    }
    .HeadBox
    {
       border:1px solid #b8c1ca;
	    background-color:#e5e5e5;
	    text-align:center;
	    font:12px Arial;
	    font-weight:bold;
	    padding-top:2px;
	    padding-bottom:2px;
	    padding-left:3px;
    }
    .Link_Item
    {
	    padding-top:3px;
	    padding-bottom:3px;
    }
    .Link_Item a
    {
	    
	    color:#333333;
	    text-decoration:none;
    }
    .Link_Item a:hover
    {
	    
	    color:red;
	    text-decoration:underline;
    }
</style>
<TABLE cellSpacing="0" cellPadding="5" width="100%" border="0">
    <tr>
        <td colspan="2" width="100%" class="HeadBox">
             Thống kê số bài viết   
        </td>
    </tr>
    <tr>
        <td class="ContentBox">
            <TABLE cellSpacing="0" cellPadding="2" width="100%" border="0">
                <tr>
                    <td class="Link_Item">
                        <asp:HyperLink ID="hplPublished" runat="server"    Text="Tổng số bài đã xuất bản:"></asp:HyperLink> <font color="red"> <asp:Literal ID="ltrSumNewsPublished" runat="server"></asp:Literal></font>
                    </td>
                </tr>
                <tr>
                    <td class="Link_Item">
                        <asp:HyperLink ID="hplNewsWaitingApprove" runat="server"  Text="Tổng số bài đang chờ duyệt:"></asp:HyperLink> <font color="red"> <asp:Literal ID="ltrSumNewsWaitingApprove" runat="server"></asp:Literal></font>
                    </td>
                </tr>
                <tr>
                    <td class="Link_Item">
                        <asp:HyperLink ID="hplNewsWaitingEdit" runat="server"  Text="Tổng số bài đang chờ biên tập:"></asp:HyperLink> <font color="red"> <asp:Literal ID="ltrSumNewsWaitingEdit" runat="server"></asp:Literal></font>
                    </td>
                </tr>
            </TABLE>
        </td>
    </tr>
</TABLE>